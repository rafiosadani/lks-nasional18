Imports System.ComponentModel
Imports System.Data.Odbc

Public Class FormReport
    Const DSN = "DSN=db_lks18"
    Dim koneksi, koneksi2 As OdbcConnection
    Dim TblReport As New DataTable

    Private Sub FormReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi = New OdbcConnection(DSN)
        koneksi2 = New OdbcConnection(DSN)
        Awal()
        tampilcombo()
        createtable()
    End Sub

    Private Sub Awal()
        DGV.DataSource = Nothing
        DGV.DataSource = TblReport
        TblReport.Rows.Clear()
        Me.Chart1.Series("Income").Points.Clear()
    End Sub

    Private Sub BtnGenerate_Click(sender As Object, e As EventArgs) Handles BtnGenerate.Click
        Awal()
        TblReport.Rows.Clear()
        createtable()

        Dim bulan1 As String = ""
        Dim bulan2 As String = ""

        If ComboFrom.Text = "January" Then
            bulan1 = "1"
        ElseIf ComboFrom.Text = "February" Then
            bulan1 = "2"
        ElseIf ComboFrom.Text = "March" Then
            bulan1 = "3"
        ElseIf ComboFrom.Text = "April" Then
            bulan1 = "4"
        ElseIf ComboFrom.Text = "May" Then
            bulan1 = "5"
        ElseIf ComboFrom.Text = "June" Then
            bulan1 = "6"
        ElseIf ComboFrom.Text = "July" Then
            bulan1 = "7"
        ElseIf ComboFrom.Text = "August" Then
            bulan1 = "8"
        ElseIf ComboFrom.Text = "September" Then
            bulan1 = "9"
        ElseIf ComboFrom.Text = "October" Then
            bulan1 = "10"
        ElseIf ComboFrom.Text = "November" Then
            bulan1 = "11"
        ElseIf ComboFrom.Text = "December" Then
            bulan1 = "12"
        End If

        If ComboTo.Text = "January" Then
            bulan2 = "1"
        ElseIf ComboTo.Text = "February" Then
            bulan2 = "2"
        ElseIf ComboTo.Text = "March" Then
            bulan2 = "3"
        ElseIf ComboTo.Text = "April" Then
            bulan2 = "4"
        ElseIf ComboTo.Text = "May" Then
            bulan2 = "5"
        ElseIf ComboTo.Text = "June" Then
            bulan2 = "6"
        ElseIf ComboTo.Text = "July" Then
            bulan2 = "7"
        ElseIf ComboTo.Text = "August" Then
            bulan2 = "8"
        ElseIf ComboTo.Text = "September" Then
            bulan2 = "9"
        ElseIf ComboTo.Text = "October" Then
            bulan2 = "10"
        ElseIf ComboTo.Text = "November" Then
            bulan2 = "11"
        ElseIf ComboTo.Text = "December" Then
            bulan2 = "12"
        End If

        If ComboTo.Text = "" Or ComboFrom.Text = "" Then
            Awal()
            tampilcombo()
            MsgBox("Harus Memilih data terlebih dahulu!", MsgBoxStyle.Critical)
        ElseIf ComboTo.Text = "" And ComboFrom.Text = "" Then
            Awal()
            tampilcombo()
            MsgBox("Harus Memilih data terlebih dahulu!", MsgBoxStyle.Critical)
        Else
            Dim sql As String = "select MONTH(headerorder.date) as Bulan, sum(detailorder.qty*detailorder.price) as Total FROM headerorder JOIN detailorder ON headerorder.orderid = detailorder.orderid WHERE MONTH(headerorder.date) >= '" + bulan1 + "' AND MONTH (headerorder.date) <= '" + bulan2 + "' GROUP BY MONTH(date)"
            Dim cmd As New OdbcCommand With {
                .CommandText = sql,
                .Connection = koneksi
            }
            koneksi.Open()
            Dim dr As OdbcDataReader
            dr = cmd.ExecuteReader
            While dr.Read
                Dim Bulan As String = ""
                Dim full As String = ""
                If dr.Item("Bulan") = "1" Then
                    full = "January"
                    Bulan = "Jan"
                ElseIf dr.Item("Bulan") = "2" Then
                    full = "February"
                    Bulan = "Feb"
                ElseIf dr.Item("Bulan") = "3" Then
                    full = "March"
                    Bulan = "Mar"
                ElseIf dr.Item("Bulan") = "4" Then
                    full = "April"
                    Bulan = "Apr"
                ElseIf dr.Item("Bulan") = "5" Then
                    full = "May"
                    Bulan = "May"
                ElseIf dr.Item("Bulan") = "6" Then
                    full = "June"
                    Bulan = "Jun"
                ElseIf dr.Item("Bulan") = "7" Then
                    full = "July"
                    Bulan = "Jul"
                ElseIf dr.Item("Bulan") = "8" Then
                    full = "August"
                    Bulan = "Aug"
                ElseIf dr.Item("Bulan") = "9" Then
                    full = "September"
                    Bulan = "Sep"
                ElseIf dr.Item("Bulan") = "10" Then
                    full = "October"
                    Bulan = "Oct"
                ElseIf dr.Item("Bulan") = "11" Then
                    full = "November"
                    Bulan = "Nov"
                ElseIf dr.Item("Bulan") = "12" Then
                    full = "December"
                    Bulan = "Dec"
                End If

                TblReport.Rows.Add(full, dr.Item("Total"))
                Me.Chart1.Series("Income").Points.AddXY(Bulan, dr.Item("Total"))
            End While
            DGV.Refresh()
            koneksi.Close()
        End If



    End Sub

    Private Sub tampilcombo()
        ComboFrom.Items.Add("January")
        ComboFrom.Items.Add("February")
        ComboFrom.Items.Add("March")
        ComboFrom.Items.Add("April")
        ComboFrom.Items.Add("May")
        ComboFrom.Items.Add("June")
        ComboFrom.Items.Add("July")
        ComboFrom.Items.Add("August")
        ComboFrom.Items.Add("September")
        ComboFrom.Items.Add("October")
        ComboFrom.Items.Add("November")
        ComboFrom.Items.Add("December")

        ComboTo.Items.Add("January")
        ComboTo.Items.Add("February")
        ComboTo.Items.Add("March")
        ComboTo.Items.Add("April")
        ComboTo.Items.Add("May")
        ComboTo.Items.Add("June")
        ComboTo.Items.Add("July")
        ComboTo.Items.Add("August")
        ComboTo.Items.Add("September")
        ComboTo.Items.Add("October")
        ComboTo.Items.Add("November")
        ComboTo.Items.Add("December")
    End Sub

    Private Sub createtable()
        TblReport.Columns.Add("Month", Type.GetType("System.String"))
        TblReport.Columns.Add("Income", Type.GetType("System.String"))

        DGV.Columns(0).Width = 200
        DGV.Columns(1).Width = 200
    End Sub

    Private Sub FormReport_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        AdminNav.Show()
        Me.Hide()
    End Sub
End Class