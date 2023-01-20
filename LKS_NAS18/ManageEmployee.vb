Imports System.Data.Odbc
Imports System.ComponentModel

Public Class ManageEmployee
    Const DSN = "DSN=db_lks18"
    Dim koneksi, koneksi2 As OdbcConnection
    Dim TblEmployee As New DataTable

    Private Sub ManageEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi = New OdbcConnection(DSN)
        koneksi2 = New OdbcConnection(DSN)
        Awal()
        ComboPos()
        CreateTable()
        LoadTable()
        ColumnWidth()
    End Sub

    Private Sub ManageEmployee_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        AdminNav.Show()
        Me.Hide()
    End Sub

    Private Sub BtnInsert_Click(sender As Object, e As EventArgs) Handles BtnInsert.Click
        If TxtName.Text = "" And TxtID.Text = "" And TxtHP.Text = "" And TxtEmail.Text = "" And ComboPosition.Text = "" Then
            MsgBox("Data harus di isi! tidak boleh kosong!", MsgBoxStyle.Critical)
            Awal()
            LoadTable()
            ColumnWidth()
        Else
            Dim queryinsert As String = "INSERT INTO msemployee VALUES('" + TxtID.Text + "', '" + TxtName.Text + "', '" + TxtEmail.Text + "', '" + TxtID.Text + "', '" + TxtHP.Text + "', '" + ComboPosition.Text + "')"
            Dim cmdinsert As New OdbcCommand With {
                .CommandText = queryinsert,
                .Connection = koneksi
            }
            koneksi.Open()
            cmdinsert.ExecuteNonQuery()
            koneksi.Close()

            MsgBox("Data berhasil ditambahkan!", MsgBoxStyle.Information)
            Awal()
            LoadTable()
            ColumnWidth()
        End If
    End Sub

    Private Sub Awal()
        DGV.DataSource = Nothing
        DGV.DataSource = TblEmployee
        TxtID.Enabled = True
        TxtID.Text = ""
        TxtName.Text = ""
        TxtHP.Text = ""
        TxtEmail.Text = ""
    End Sub

    Private Sub ComboPos()
        ComboPosition.Items.Add("Admin")
        ComboPosition.Items.Add("Cashier")
        ComboPosition.Items.Add("Chef")
    End Sub

    Private Sub CreateTable()
        TblEmployee.Columns.Add("Employeeid", Type.GetType("System.String"))
        TblEmployee.Columns.Add("Name", Type.GetType("System.String"))
        TblEmployee.Columns.Add("Email", Type.GetType("System.String"))
        TblEmployee.Columns.Add("Handphone", Type.GetType("System.String"))
        TblEmployee.Columns.Add("Position", Type.GetType("System.String"))
    End Sub

    Private Sub LoadTable()
        TblEmployee.Rows.Clear()
        Dim querydata As String = "SELECT * FROM msemployee"
        Dim cmddata As New OdbcCommand With {
            .CommandText = querydata,
            .Connection = koneksi
        }
        koneksi.Open()
        Dim drdata As OdbcDataReader
        drdata = cmddata.ExecuteReader
        While drdata.Read
            TblEmployee.Rows.Add(drdata.Item("employeeid"), drdata.Item("name"), drdata.Item("email"), drdata.Item("handphone"), drdata.Item("position"))
        End While
        DGV.Refresh()
        koneksi.Close()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        If TxtName.Text = "" And TxtID.Text = "" And TxtHP.Text = "" And TxtEmail.Text = "" And ComboPosition.Text = "" Then
            MsgBox("Data tidak bisa diupdate! data tidak ditemukan!", MsgBoxStyle.Critical)
            Awal()
            LoadTable()
            ColumnWidth()
        Else
            Dim queryupdate As String = "UPDATE msemployee SET employeeid = '" + TxtID.Text + "', name = '" + TxtName.Text + "', email = '" + TxtEmail.Text + "', handphone = '" + TxtHP.Text + "', position = '" + ComboPosition.Text + "' WHERE employeeid = '" + TxtID.Text + "'"
            Dim cmdupdate As New OdbcCommand With {
                .CommandText = queryupdate,
                .Connection = koneksi
            }
            koneksi.Open()
            cmdupdate.ExecuteNonQuery()
            koneksi.Close()

            MsgBox("Data berhasil diupdate!", MsgBoxStyle.Information)
            Awal()
            LoadTable()
            ColumnWidth()
        End If
    End Sub

    Private Sub ColumnWidth()
        DGV.Columns(0).Width = 160
        DGV.Columns(1).Width = 158
        DGV.Columns(2).Width = 158
        DGV.Columns(3).Width = 155
        DGV.Columns(4).Width = 141
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If TxtName.Text = "" And TxtID.Text = "" And TxtHP.Text = "" And TxtEmail.Text = "" And ComboPosition.Text = "" Then
            MsgBox("Data tidak bisa didelete! data tidak ditemukan!", MsgBoxStyle.Critical)
            Awal()
            LoadTable()
            ColumnWidth()
        Else
            If MsgBox("Apakah anda yakin mau menghapus data ini?", MsgBoxStyle.YesNo, "Delete Data") = MsgBoxResult.Yes Then
                Dim querydelete As String = "DELETE FROM msemployee WHERE employeeid = '" + TxtID.Text + "'"
                Dim cmddelete As New OdbcCommand With {
                    .CommandText = querydelete,
                    .Connection = koneksi
                }
                koneksi.Open()
                cmddelete.ExecuteNonQuery()
                koneksi.Close()

                MsgBox("Data berhasil didelete!", MsgBoxStyle.Information)

                Awal()
                LoadTable()
                ColumnWidth()
            Else
                Exit Sub
            End If
        End If
    End Sub

    Private Sub DGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV.CellMouseClick
        On Error Resume Next
        TxtID.Enabled = False
        TxtID.Text = DGV.Rows(e.RowIndex).Cells(0).Value
        TxtName.Text = DGV.Rows(e.RowIndex).Cells(1).Value
        TxtEmail.Text = DGV.Rows(e.RowIndex).Cells(2).Value
        TxtHP.Text = DGV.Rows(e.RowIndex).Cells(3).Value
        ComboPosition.Text = DGV.Rows(e.RowIndex).Cells(4).Value
    End Sub

    Private Sub ComboPosition_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboPosition.KeyPress
        If Char.IsLetter(e.KeyChar) Or Char.IsNumber(e.KeyChar) Then
            e.Handled = True
            MsgBox("Tidak bisa diketik, harus pilih!", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub TxtHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtHP.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
            MsgBox("Yang diinputkan harus angka!", MsgBoxStyle.Critical)
        End If
    End Sub
End Class