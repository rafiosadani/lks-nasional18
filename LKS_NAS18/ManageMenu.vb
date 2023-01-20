Imports System.ComponentModel
Imports System.Data.Odbc
Public Class ManageMenu
    Const DSN = "DSN=db_lks18"
    Dim koneksi, koneksi2 As OdbcConnection
    Dim TblMenu As New DataTable

    Private Sub ManageMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi = New OdbcConnection(DSN)
        koneksi2 = New OdbcConnection(DSN)
        Awal()
        Menuid()
        CreateTable()
        ColumnWidth()
        LoadTable()
    End Sub

    Private Sub ManageMenu_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        AdminNav.Show()
        Me.Hide()
    End Sub

    Private Sub Awal()
        DGV.DataSource = Nothing
        DGV.DataSource = TblMenu
        TxtID.Enabled = False
        TxtID.Text = ""
        TxtName.Text = ""
        TxtPrice.Text = ""
        TxtPhoto.Text = ""
        TxtPhoto.Enabled = False
        PictureBox1.ImageLocation = TxtPhoto.Text
    End Sub

    Private Sub CreateTable()
        TblMenu.Columns.Add("Menuid", Type.GetType("System.String"))
        TblMenu.Columns.Add("Name", Type.GetType("System.String"))
        TblMenu.Columns.Add("Price", Type.GetType("System.String"))
    End Sub

    Private Sub BtnBrowse_Click(sender As Object, e As EventArgs) Handles BtnBrowse.Click
        BukaFile.Filter = "Image files | *.jpg;*.jpeg;*.png;*.bmp"
        BukaFile.ShowDialog()
        TxtPhoto.Text = BukaFile.FileName
        PictureBox1.ImageLocation = TxtPhoto.Text
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub ColumnWidth()
        DGV.Columns(0).Width = 195
        DGV.Columns(1).Width = 195
        DGV.Columns(2).Width = 195
    End Sub

    Private Sub Menuid()
        Dim sql As String = "SELECT menuid FROM menu ORDER BY menuid DESC"
        Dim cmd As New OdbcCommand With {
            .CommandText = sql,
            .Connection = koneksi
        }
        koneksi.Open()
        Dim dr As OdbcDataReader
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            TxtID.Text = 1
        Else
            TxtID.Text = dr.Item("menuid") + 1
        End If
        koneksi.Close()
    End Sub

    Private Sub BtnInsert_Click(sender As Object, e As EventArgs) Handles BtnInsert.Click
        If TxtName.Text = "" And TxtPrice.Text = "" Then
            MsgBox("Data tidak boleh kosong! harus diisi!", MsgBoxStyle.Critical)
            Awal()
            LoadTable()
            ColumnWidth()
            Menuid()
        Else
            Dim queryinsert As String = "INSERT INTO menu VALUES('" + TxtName.Text + "', '" + TxtPrice.Text + "', '" + TxtPhoto.Text + "')"
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
            Menuid()
        End If
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        If TxtName.Text = "" And TxtPrice.Text = "" Then
            MsgBox("Data tidak bisa diupdate! data tidak ditemukan!", MsgBoxStyle.Critical)
            Awal()
            LoadTable()
            ColumnWidth()
            Menuid()
        Else
            Dim update As String = "UPDATE menu SET name = '" + TxtName.Text + "', price = '" + TxtPrice.Text + "', photo = '" + TxtPhoto.Text + "' WHERE menuid = '" + TxtID.Text + "'"
            Dim cmd As New OdbcCommand With {
                .CommandText = update,
                .Connection = koneksi
            }
            koneksi.Open()
            cmd.ExecuteNonQuery()
            koneksi.Close()

            MsgBox("Data berhasil diupdate!", MsgBoxStyle.Information)
            Awal()
            LoadTable()
            ColumnWidth()
            Menuid()
        End If
    End Sub

    Private Sub LoadTable()
        TblMenu.Rows.Clear()
        Dim querydata As String = "SELECT * FROM menu"
        Dim cmddata As New OdbcCommand With {
            .CommandText = querydata,
            .Connection = koneksi
        }
        koneksi.Open()
        Dim drdata As OdbcDataReader
        drdata = cmddata.ExecuteReader
        While drdata.Read
            TblMenu.Rows.Add(drdata.Item("menuid"), drdata.Item("name"), drdata.Item("price"))
        End While
        DGV.Refresh()
        koneksi.Close()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If TxtName.Text = "" And TxtPrice.Text = "" Then
            MsgBox("Data tidak bisa didelete! data tidak ditemukan!", MsgBoxStyle.Critical)
            Awal()
            LoadTable()
            ColumnWidth()
            Menuid()
        Else
            If MsgBox("Apakah anda yakin mau menghapus data ini?", MsgBoxStyle.YesNo, "Delete Data") = MsgBoxResult.Yes Then
                Dim delete As String = "DELETE FROM menu WHERE menuid = '" + TxtID.Text + "'"
                Dim cmddelete As New OdbcCommand With {
                    .CommandText = delete,
                    .Connection = koneksi
                }
                koneksi.Open()
                cmddelete.ExecuteNonQuery()
                koneksi.Close()

                MsgBox("Data berhasil didelete!", MsgBoxStyle.Information)
                Awal()
                LoadTable()
                ColumnWidth()
                Menuid()
            Else
                Exit Sub
            End If
        End If
    End Sub

    Private Sub DGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV.CellMouseClick
        On Error Resume Next

        TxtID.Text = DGV.Rows(e.RowIndex).Cells(0).Value
        TxtName.Text = DGV.Rows(e.RowIndex).Cells(1).Value
        TxtPrice.Text = DGV.Rows(e.RowIndex).Cells(2).Value

        Dim sql As String = "SELECT photo FROM menu WHERE menuid = '" + DGV.Rows(e.RowIndex).Cells(0).Value + "'"
        Dim cmd As New OdbcCommand With {
            .CommandText = sql,
            .Connection = koneksi
        }
        koneksi.Open()
        Dim dr As OdbcDataReader
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            TxtPhoto.Text = dr.Item("photo")
            PictureBox1.ImageLocation = TxtPhoto.Text
        End If
        koneksi.Close()
    End Sub

    Private Sub TxtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPrice.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
            MsgBox("Yang diinputkan harus angka!", MsgBoxStyle.Critical)
        End If
    End Sub
End Class