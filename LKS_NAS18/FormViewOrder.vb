Imports System.Data.Odbc

Public Class FormViewOrder
    Const DSN = "DSN=db_lks18"
    Dim koneksi, koneksi2 As OdbcConnection
    Dim TblOrder As New DataTable

    Private Sub FormViewOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi = New OdbcConnection(DSN)
        koneksi2 = New OdbcConnection(DSN)
        Awal()
        OrderID()
        CreateTable()
    End Sub

    Private Sub Awal()
        DGV.DataSource = Nothing
        DGV.DataSource = TblOrder
        TblOrder.Rows.Clear()
    End Sub

    Private Sub OrderID()
        Dim sql As String = "SELECT DISTINCT orderid FROM detailorder WHERE status = 'Pending'"
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

    Private Sub ComboOrder_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboOrder.SelectedValueChanged
        TblOrder.Rows.Clear()
        Dim sql As String = "SELECT detailorder.*, menu.name FROM detailorder JOIN menu ON detailorder.menuid = menu.menuid WHERE detailorder.orderid = '" + ComboOrder.Text + "'"
        Dim cmd As New OdbcCommand With {
            .CommandText = sql,
            .Connection = koneksi
        }
        koneksi.Open()
        Dim dr As OdbcDataReader
        dr = cmd.ExecuteReader
        While dr.Read
            TblOrder.Rows.Add(dr.Item("name"), dr.Item("qty"))
        End While
        koneksi.Close()
    End Sub

    Private Sub BtnBeres_Click(sender As Object, e As EventArgs) Handles BtnBeres.Click
        On Error Resume Next
        For baris As Integer = 0 To DGV.Rows.Count - 1
            If DGV.Rows(baris).Cells(2).Value <> "Finished" Then
                MsgBox("Action harus finished semua!", MsgBoxStyle.Critical)
            Else
                Dim update As String = "UPDATE detailorder SET status = 'Finished' WHERE orderid = '" + ComboOrder.Text + "'"
                Dim cmdupdate As New OdbcCommand With {
                    .CommandText = update,
                    .Connection = koneksi
                }
                koneksi.Open()
                cmdupdate.ExecuteNonQuery()
                koneksi.Close()

                MsgBox("Masakan sudah beres!", MsgBoxStyle.Information)
                Awal()
                TblOrder.Columns.Clear()
                CreateTable()
                OrderID()
            End If
        Next
    End Sub

    Private Sub CreateTable()
        TblOrder.Columns.Add("Menu", Type.GetType("System.String"))
        TblOrder.Columns.Add("Qty", Type.GetType("System.String"))

        Dim cols As New DataGridViewComboBoxColumn
        cols.Items.Add("Deliver")
        cols.Items.Add("Pending")
        cols.Items.Add("Cooking")
        cols.Items.Add("Finished")
        cols.HeaderText = "Action"
        DGV.Columns.Add(cols)

        DGV.Columns(0).Width = 164
        DGV.Columns(1).Width = 150
        DGV.Columns(2).Width = 150
        DGV.Columns(0).ReadOnly = True
        DGV.Columns(1).ReadOnly = True
    End Sub
End Class