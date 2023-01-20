Imports System.ComponentModel
Imports System.Data.Odbc

Public Class FormPayment
    Const DSN = "DSN=db_lks18"
    Dim koneksi, koneksi2 As OdbcConnection
    Dim TblPayment As New DataTable

    Private Sub FormPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi = New OdbcConnection(DSN)
        koneksi2 = New OdbcConnection(DSN)
        Awal()
        CreateTable()
        OrderID()
        ComboPilihan()
        TotalHarga()
    End Sub

    Private Sub Awal()
        DGV.DataSource = Nothing
        DGV.DataSource = TblPayment
        TblPayment.Rows.Clear()
        TxtJumlah.Enabled = False
        TxtNumber.Enabled = False
        TxtStatus.Enabled = False
        TxtKembali.Enabled = False
        ComboBank.Enabled = False
        TxtNumber.Text = ""
        TxtJumlah.Text = ""
        BtnSave.Enabled = False
    End Sub

    Private Sub CreateTable()
        TblPayment.Columns.Add("Menu", Type.GetType("System.String"))
        TblPayment.Columns.Add("Qty", Type.GetType("System.String"))
        TblPayment.Columns.Add("Price", Type.GetType("System.String"))
        TblPayment.Columns.Add("Total", Type.GetType("System.String"))

        DGV.Columns(0).Width = 160
        DGV.Columns(1).Width = 125
        DGV.Columns(2).Width = 130
        DGV.Columns(3).Width = 155

        DGV.Columns(0).ReadOnly = True
        DGV.Columns(1).ReadOnly = True
        DGV.Columns(2).ReadOnly = True
        DGV.Columns(3).ReadOnly = True
    End Sub

    Private Sub OrderID()
        Dim sql As String = "SELECT DISTINCT headerorder.orderid FROM detailorder JOIN headerorder ON detailorder.orderid = headerorder.orderid WHERE detailorder.status = 'Finished' AND headerorder.payment = ''"
        Dim cmd As New OdbcCommand With {
            .CommandText = sql,
            .Connection = koneksi
        }
        koneksi.Open()
        Dim dr As OdbcDataReader
        dr = cmd.ExecuteReader
        While dr.Read
            ComboOrder.Items.Add(dr.Item("orderid"))
        End While
        koneksi.Close()
    End Sub

    Private Sub TampilGrid()
        DGV.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    End Sub

    Private Sub ComboOrder_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboOrder.SelectedValueChanged
        TblPayment.Rows.Clear()
        Try
            Dim sql As String = "SELECT detailorder.*, menu.name FROM detailorder JOIN menu ON detailorder.menuid = menu.menuid WHERE detailorder.orderid = '" + ComboOrder.Text + "'"
            Dim cmd As New OdbcCommand With {
                .CommandText = sql,
                .Connection = koneksi
            }
            koneksi.Open()
            Dim dr As OdbcDataReader
            dr = cmd.ExecuteReader
            While dr.Read
                TampilGrid()
                TblPayment.Rows.Add(dr.Item("name"), dr.Item("qty"), dr.Item("price"), Val(dr.Item("qty")) * Val(dr.Item("price")))
            End While
            koneksi.Close()
            TotalHarga()
        Catch ex As Exception
            koneksi.Close()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ComboOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboOrder.KeyPress
        If Char.IsLetter(e.KeyChar) Or Char.IsNumber(e.KeyChar) Then
            e.Handled = True
            MsgBox("Tidak boleh diketik! harus memilih data!", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub ComboType_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboType.KeyPress
        If Char.IsLetter(e.KeyChar) Or Char.IsNumber(e.KeyChar) Then
            e.Handled = True
            MsgBox("Tidak boleh diketik! harus memilih data!", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub ComboBank_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBank.KeyPress
        If Char.IsLetter(e.KeyChar) Or Char.IsNumber(e.KeyChar) Then
            e.Handled = True
            MsgBox("Tidak boleh diketik! harus memilih data!", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub ComboPilihan()
        ComboType.Items.Add("Debit")
        ComboType.Items.Add("Cash")

        ComboBank.Items.Add("Bank BRI")
        ComboBank.Items.Add("Bank BNI")
        ComboBank.Items.Add("Bank Mandiri")
    End Sub

    Private Sub TotalHarga()
        Dim hitung As Integer = 0
        For baris As Integer = 0 To DGV.Rows.Count - 1
            hitung = hitung + DGV.Rows(baris).Cells(3).Value
        Next
        LblTotal.Text = hitung
        'LblTotal.Text = "Rp. " + Format(Val(hitung), "###,###,###")
    End Sub

    Private Sub ComboType_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboType.SelectedValueChanged
        TxtJumlah.Enabled = False
        TxtNumber.Enabled = False
        TxtStatus.Enabled = False
        TxtKembali.Enabled = False
        ComboBank.Enabled = False
        TxtNumber.Text = ""
        TxtJumlah.Text = ""
        BtnSave.Enabled = False

        If ComboType.Text = "Cash" Then
            TxtJumlah.Enabled = True
            TxtJumlah.Focus()
        ElseIf ComboType.Text = "Debit" Then
            ComboBank.Enabled = True
            TxtNumber.Enabled = True
            TxtNumber.Focus()
        End If
    End Sub

    Private Sub TxtJumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtJumlah.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
            MsgBox("Yang diinputkan harus angka!", MsgBoxStyle.Critical)
        End If

        If e.KeyChar = Chr(13) Then
            If Val(TxtJumlah.Text) = Val(LblTotal.Text) Then
                BtnSave.Enabled = True
                BtnSave.Focus()
                TxtKembali.Text = 0
                TxtStatus.Text = "LUNAS"
            ElseIf Val(TxtJumlah.Text) > Val(LblTotal.Text) Then
                BtnSave.Enabled = True
                BtnSave.Focus()
                TxtKembali.Text = Val(TxtJumlah.Text) - Val(LblTotal.Text)
                TxtStatus.Text = "LUNAS"
                MsgBox(Val(TxtJumlah.Text) - Val(LblTotal.Text))
            ElseIf Val(TxtJumlah.Text) < Val(LblTotal.Text) Then
                TxtJumlah.Text = ""
                TxtJumlah.Focus()
                TxtStatus.Text = "BELUM LUNAS"
                TxtKembali.Text = 0
                BtnSave.Enabled = False
                MsgBox("BELUM LUNAS! UANG MASIH KURANG", MsgBoxStyle.Critical)
            End If
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            Dim jumlah As Integer
            Dim bank As String
            If TxtJumlah.Text = "" Then
                jumlah = 0
            Else
                jumlah = TxtJumlah.Text
            End If

            If ComboBank.Text = "-- Pilih Bank --" Then
                bank = "Cash"
            ElseIf ComboBank.Text = "" Then
                bank = "Cash"
            Else
                bank = ComboBank.Text
            End If

            Dim sqlupdate As String = "UPDATE headerorder SET payment =  '" + ComboType.Text + "', bank = '" + bank + "', jumlah = '" + jumlah.ToString + "' WHERE orderid = '" + ComboOrder.Text + "'"
            Dim cmd As New OdbcCommand With {
                .CommandText = sqlupdate,
                .Connection = koneksi
            }
            koneksi.Open()
            cmd.ExecuteNonQuery()
            koneksi.Close()

            MsgBox("Pembayaran sukses!", MsgBoxStyle.Information)
            Awal()
            TblPayment.Columns.Clear()
            CreateTable()
            ComboOrder.Items.Add("-- Pilih Nomor Order --")
            OrderID()
        Catch ex As Exception
            koneksi.Close()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNumber.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
            MsgBox("Yang diinputkan harus angka!", MsgBoxStyle.Critical)
        End If

        If e.KeyChar = Chr(13) Then
            If Not TxtNumber.Text = "" And Not ComboBank.Text = "-- Pilih Bank --" Then
                BtnSave.Enabled = True
                BtnSave.Focus()
            ElseIf ComboBank.Text = "-- Pilih Bank --" Then
                ComboBank.Focus()
                MsgBox("Pilih bank terlebih dahulu!", MsgBoxStyle.Critical)
            Else
                MsgBox("Data harus diisi!", MsgBoxStyle.Critical)
            End If
        End If
    End Sub

    Private Sub FormPayment_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CashierNavigation.Show()
        Me.Hide()
    End Sub
End Class