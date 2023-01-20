Imports System.ComponentModel
Imports System.Data.Odbc
Public Class ManageMember
    Const DSN = "DSN=db_lks18"
    Dim koneksi, koneksi2 As OdbcConnection
    Dim TblMember As New DataTable

    Private Sub ManageMember_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi = New OdbcConnection(DSN)
        koneksi2 = New OdbcConnection(DSN)
        Awal()
        CreateTable()
        ColumnWidth()
        LoadTable()
    End Sub

    Private Sub ManageMember_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        AdminNav.Show()
        Me.Hide()
    End Sub

    Private Sub Awal()
        DGV.DataSource = Nothing
        DGV.DataSource = TblMember
        TxtID.Enabled = True
        TxtID.Text = ""
        TxtName.Text = ""
        TxtHP.Text = ""
        TxtEmail.Text = ""
    End Sub

    Private Sub CreateTable()
        TblMember.Columns.Add("Memberid", Type.GetType("System.String"))
        TblMember.Columns.Add("Name", Type.GetType("System.String"))
        TblMember.Columns.Add("Email", Type.GetType("System.String"))
        TblMember.Columns.Add("Handphone", Type.GetType("System.String"))
    End Sub

    Private Sub ColumnWidth()
        DGV.Columns(0).Width = 142
        DGV.Columns(1).Width = 175
        DGV.Columns(2).Width = 175
        DGV.Columns(3).Width = 175
    End Sub

    Private Sub BtnInsert_Click(sender As Object, e As EventArgs) Handles BtnInsert.Click
        If TxtName.Text = "" And TxtID.Text = "" And TxtHP.Text = "" And TxtEmail.Text = "" Then
            MsgBox("Data harus di isi! tidak boleh kosong!", MsgBoxStyle.Critical)
            Awal()
            LoadTable()
            ColumnWidth()
        Else
            Dim joindate As Date = Format(Now, "yyyy-MM-dd")
            Dim queryinsert As String = "INSERT INTO msmember VALUES('" + TxtID.Text + "', '" + TxtName.Text + "', '" + TxtEmail.Text + "', '" + TxtHP.Text + "', '" + joindate.ToString + "')"
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

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        If TxtName.Text = "" And TxtID.Text = "" And TxtHP.Text = "" And TxtEmail.Text = "" Then
            MsgBox("Data tidak bisa diupdate! data tidak ditemukan!", MsgBoxStyle.Critical)
            Awal()
            LoadTable()
            ColumnWidth()
        Else
            Dim queryupdate As String = "UPDATE msmember SET memberid = '" + TxtID.Text + "', name = '" + TxtName.Text + "', email = '" + TxtEmail.Text + "', handphone = '" + TxtHP.Text + "' WHERE memberid = '" + TxtID.Text + "'"
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

    Private Sub LoadTable()
        TblMember.Rows.Clear()
        Dim querydata As String = "SELECT * FROM msmember"
        Dim cmddata As New OdbcCommand With {
            .CommandText = querydata,
            .Connection = koneksi
        }
        koneksi.Open()
        Dim drdata As OdbcDataReader
        drdata = cmddata.ExecuteReader
        While drdata.Read
            TblMember.Rows.Add(drdata.Item("memberid"), drdata.Item("name"), drdata.Item("email"), drdata.Item("handphone"))
        End While
        DGV.Refresh()
        koneksi.Close()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If TxtName.Text = "" And TxtID.Text = "" And TxtHP.Text = "" And TxtEmail.Text = "" Then
            MsgBox("Data tidak bisa delete! data tidak ditemukan!", MsgBoxStyle.Critical)
            Awal()
            LoadTable()
            ColumnWidth()
        Else
            If MsgBox("Apakah anda yakin mau menghapus data ini?", MsgBoxStyle.YesNo, "Delete Data") = MsgBoxResult.Yes Then
                Dim querydelete As String = "DELETE FROM msmember WHERE memberid = '" + TxtID.Text + "'"
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
    End Sub

    Private Sub TxtHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtHP.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
            MsgBox("Yang diinputkan harus angka!", MsgBoxStyle.Critical)
        End If
    End Sub
End Class