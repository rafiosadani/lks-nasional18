Imports System.Data.Odbc
Public Class FormLogin
    Const DSN = "DSN=db_lks18"
    Dim koneksi, koneksi2 As OdbcConnection

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi = New OdbcConnection(DSN)
        koneksi2 = New OdbcConnection(DSN)
        Awal()
    End Sub

    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click
        If TxtEmail.Text = "" And TxtPassword.Text = "" Then
            MsgBox("Data tidak boleh kosong!", MsgBoxStyle.Critical)
            Awal()
        ElseIf Not TxtEmail.Text.Contains("@") Then
            TxtEmail.Focus()
            MsgBox("Yang diinputkan harus email yang sesuai!", MsgBoxStyle.Critical)
        Else
            Dim query As String = "SELECT * FROM msemployee WHERE email = '" + TxtEmail.Text + "' AND password = '" + TxtPassword.Text + "'"
            Dim cmd As New OdbcCommand With {
                .CommandText = query,
                .Connection = koneksi
            }
            koneksi.Open()
            Dim dr As OdbcDataReader
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                Select Case dr.Item("position")
                    Case "Admin"
                        AdminNav.LblAdminName.Text = dr.Item("name")
                        My.Settings.employeeid = dr.Item("employeeid")
                        My.Settings.position = dr.Item("position")
                        AdminNav.Show()
                        Me.Hide()
                        MsgBox("Anda berhasil Login!", MsgBoxStyle.Information)
                        Awal()
                    Case "Chef"
                        ChefNavigation.LblChefName.Text = dr.Item("name")
                        My.Settings.employeeid = dr.Item("employeeid")
                        My.Settings.position = dr.Item("position")
                        ChefNavigation.Show()
                        Me.Hide()
                        MsgBox("Anda berhasil Login!", MsgBoxStyle.Information)
                        Awal()
                    Case "Cashier"
                        CashierNavigation.LblCashierName.Text = dr.Item("name")
                        My.Settings.employeeid = dr.Item("employeeid")
                        My.Settings.position = dr.Item("position")
                        CashierNavigation.Show()
                        Me.Hide()
                        MsgBox("Anda berhasil Login!", MsgBoxStyle.Information)
                        Awal()
                End Select
            Else
                MsgBox("username atau password salah!", MsgBoxStyle.Critical)
                Awal()
            End If
            koneksi.Close()
            Awal()
        End If
    End Sub

    Private Sub Awal()
        TxtEmail.Focus()
        TxtEmail.Text = ""
        TxtPassword.Text = ""
    End Sub
End Class
