Imports System.ComponentModel
Imports System.Data.Odbc

Public Class FormOrder
    Const DSN = "DSN=db_lks18"
    Dim koneksi, koneksi2 As OdbcConnection
    Dim TblOrder As New DataTable
    Dim TblOderDua As New DataTable

    Private Sub FormOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi = New OdbcConnection(DSN)
        koneksi2 = New OdbcConnection(DSN)
        Awal()
        CreateTable()
        CreateTableDua()
        ColumnWidth()
        ColumnWidth2()
        LoadTable()
        Member()
        TampilGrid()
    End Sub

    Private Sub Awal()
        DGV.DataSource = Nothing
        DGV.DataSource = TblOrder
        DGV2.DataSource = Nothing
        DGV2.DataSource = TblOderDua
        TxtName.Text = ""
        TxtPrice.Text = ""
        TxtQty.Text = ""
        TxtPrice.Enabled = False
        PictureBox1.ImageLocation = ""
        TblOderDua.Rows.Clear()
    End Sub

    Private Sub CreateTable()
        TblOrder.Columns.Add("Menu", Type.GetType("System.String"))
        TblOrder.Columns.Add("Price", Type.GetType("System.String"))
        DGV.ReadOnly = True
    End Sub

    Private Sub CreateTableDua()
        TblOderDua.Columns.Add("Menu", Type.GetType("System.String"))
        TblOderDua.Columns.Add("Qty", Type.GetType("System.String"))
        TblOderDua.Columns.Add("Price", Type.GetType("System.String"))
        TblOderDua.Columns.Add("Total", Type.GetType("System.String"))
        DGV2.Columns(0).ReadOnly = True
        DGV2.Columns(2).ReadOnly = True
        DGV2.Columns(3).ReadOnly = True
    End Sub

    Private Sub ColumnWidth()
        DGV.Columns(0).Width = 230
        DGV.Columns(1).Width = 230
    End Sub

    Private Sub ColumnWidth2()
        DGV2.Columns(0).Width = 160
        DGV2.Columns(1).Width = 135
        DGV2.Columns(2).Width = 160
        DGV2.Columns(3).Width = 160
    End Sub

    Private Sub TampilGrid()
        DGV.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV2.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV2.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV2.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    End Sub

    Private Sub LoadTable()
        TblOrder.Rows.Clear()
        Dim sql As String = "SELECT * FROM menu"
        Dim cmd As New OdbcCommand With {
            .CommandText = sql,
            .Connection = koneksi
        }
        koneksi.Open()
        Dim dr As OdbcDataReader
        dr = cmd.ExecuteReader
        While dr.Read
            TblOrder.Rows.Add(dr.Item("name"), dr.Item("price"))
        End While
        DGV.Refresh()
        koneksi.Close()
    End Sub

    Private Sub Bersih()
        TxtName.Text = ""
        TxtQty.Text = ""
        TxtPrice.Text = ""
        PictureBox1.ImageLocation = ""
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        TblOderDua.Rows.Add(TxtName.Text, TxtQty.Text, TxtPrice.Text, Val(TxtPrice.Text) * Val(TxtQty.Text))
        DGV.Focus()
        On Error Resume Next
        For barisatas As Integer = 0 To DGV2.Rows.Count - 2
            For barisbawah As Integer = barisatas + 1 To DGV2.Rows.Count - 2
                If DGV2.Rows(barisbawah).Cells(0).Value = DGV2.Rows(barisatas).Cells(0).Value Then
                    DGV2.Rows(barisatas).Cells(1).Value = DGV2.Rows(barisatas).Cells(1).Value + Convert.ToInt32(TxtQty.Text)
                    DGV2.Rows(barisatas).Cells(3).Value = DGV2.Rows(barisatas).Cells(1).Value * DGV2.Rows(barisatas).Cells(2).Value
                    DGV2.Rows.RemoveAt(barisbawah)
                    Call TotalHarga()
                End If
            Next
        Next
        TampilGrid()
        Bersih()
        TotalHarga()
    End Sub

    'Private Sub DGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV.CellMouseClick
    '    TxtName.Text = DGV.Rows(e.RowIndex).Cells(0).Value
    '    TxtQty.Text = 1
    '    TxtPrice.Text = DGV.Rows(e.RowIndex).Cells(1).Value

    '    If Not TxtName.Text = "" Then
    '        If DGV.Rows(e.RowIndex).Cells(0).Value = TxtName.Text Then
    '            TxtQty.Text = TxtQty.Text + 1
    '        End If
    '    End If

    '    Dim sql As String = "SELECT photo FROM menu WHERE name = '" + DGV.Rows(e.RowIndex).Cells(0).Value + "'"
    '    Dim cmd As New OdbcCommand With {
    '        .CommandText = sql,
    '        .Connection = koneksi
    '    }
    '    koneksi.Open()
    '    Dim dr As OdbcDataReader
    '    dr = cmd.ExecuteReader
    '    dr.Read()
    '    If dr.HasRows Then
    '        PictureBox1.ImageLocation = dr.Item("photo")
    '    End If
    '    koneksi.Close()
    'End Sub

    Private Sub TotalHarga()
        Dim hitung As Integer
        For baris As Integer = 0 To DGV2.Rows.Count - 1
            hitung = hitung + DGV2.Rows(baris).Cells(3).Value
        Next
        LblTotal.Text = hitung
    End Sub

    Private Sub BtnOrder_Click(sender As Object, e As EventArgs) Handles BtnOrder.Click
        Dim idorder As String
        Dim sql As String = "SELECT TOP 1 orderid FROM detailorder ORDER BY orderid DESC"
        Dim cmd As New OdbcCommand With {
            .CommandText = sql,
            .Connection = koneksi
        }
        koneksi.Open()
        Dim dr As OdbcDataReader
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            idorder = Format(Now, "yyyyMM") + "0001"
        Else
            If Microsoft.VisualBasic.Left(dr.Item("orderid"), 6) = Format(Now, "yyyyMM") Then
                idorder = dr.Item("orderid") + 1
            Else
                idorder = Format(Now, "yyyyMM") + "0001"
            End If
        End If
        koneksi.Close()

        Dim insertheaderorder As String = "INSERT INTO headerorder VALUES('" + idorder + "', '" + My.Settings.employeeid + "', '" + ComboMember.SelectedValue + "', '" + Format(Now, "yyyy-MM-dd") + "', '', '', '0')"
        Dim cmdinsertorder As New OdbcCommand With {
                .CommandText = insertheaderorder,
                .Connection = koneksi
            }
        koneksi.Open()
        cmdinsertorder.ExecuteNonQuery()
        koneksi.Close()

        For baris As Integer = 0 To DGV2.Rows.Count - 2
            Dim menuid As Integer
            Dim ambilmenuid As String = "select menuid from menu WHERE name = '" + DGV2.Rows(baris).Cells(0).Value + "'"
            Dim cmdambil As New OdbcCommand With {
                .CommandText = ambilmenuid,
                .Connection = koneksi
            }
            koneksi.Open()
            Dim drambil As OdbcDataReader
            drambil = cmdambil.ExecuteReader
            drambil.Read()
            If drambil.HasRows Then
                menuid = drambil.Item("menuid")
            End If
            koneksi.Close()

            Dim status As String = "Pending"

            Dim qty As Integer = DGV2.Rows(baris).Cells(1).Value
            Dim price As Integer = DGV2.Rows(baris).Cells(2).Value


            Dim insert As String = "INSERT INTO detailorder VALUES('" + idorder + "', '" + menuid.ToString + "', '" + qty.ToString + "', '" + price.ToString + "', '" + status + "')"
            Dim cmdinsert As New OdbcCommand With {
                .CommandText = insert,
                .Connection = koneksi
            }
            koneksi.Open()
            cmdinsert.ExecuteNonQuery()
            koneksi.Close()
        Next

        MsgBox("Pesanan berhasil diorder!", MsgBoxStyle.Information)
        Awal()
        LoadTable()
        ColumnWidth()
        ColumnWidth2()
        Member()
        TotalHarga()
    End Sub

    Private Sub FormOrder_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CashierNavigation.Show()
        Me.Hide()
    End Sub

    Private Sub Member()
        koneksi.Open()
        Dim da As New OdbcDataAdapter("select * from msmember", koneksi)
        Dim dt As New DataTable
        da.Fill(dt)
        ComboMember.DataSource = dt
        ComboMember.DisplayMember = "name"
        ComboMember.ValueMember = "memberid"
        koneksi.Close()
    End Sub

    Private Sub DGV_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellClick
        TxtName.Text = DGV.Rows(e.RowIndex).Cells(0).Value
        TxtQty.Text = 1
        TxtPrice.Text = DGV.Rows(e.RowIndex).Cells(1).Value

        Dim sql As String = "SELECT photo FROM menu WHERE name = '" + DGV.Rows(e.RowIndex).Cells(0).Value + "'"
        Dim cmd As New OdbcCommand With {
            .CommandText = sql,
            .Connection = koneksi
        }
        koneksi.Open()
        Dim dr As OdbcDataReader
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            PictureBox1.ImageLocation = dr.Item("photo")
        End If

        TxtQty.Focus()
        koneksi.Close()
    End Sub

    Private Sub DGV2_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV2.CellEndEdit
        If e.ColumnIndex = 1 Then
            DGV2.Rows(e.RowIndex).Cells(3).Value = DGV2.Rows(e.RowIndex).Cells(1).Value * DGV2.Rows(e.RowIndex).Cells(2).Value
            Call TotalHarga()
        End If
    End Sub

    Private Sub TxtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtQty.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
            MsgBox("Yang diinputkan harus angka!", MsgBoxStyle.Critical)
        End If
    End Sub
End Class