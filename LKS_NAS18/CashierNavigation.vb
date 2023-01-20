Public Class CashierNavigation
    Private Sub BtnOrder_Click(sender As Object, e As EventArgs) Handles BtnOrder.Click
        FormOrder.Show()
        Me.Hide()
    End Sub

    Private Sub BtnPayment_Click(sender As Object, e As EventArgs) Handles BtnPayment.Click
        FormPayment.Show()
        Me.Hide()
    End Sub

    Private Sub BtnPassword_Click(sender As Object, e As EventArgs) Handles BtnPassword.Click
        ChangePassword.Show()
        Me.Hide()
    End Sub

    Private Sub BtnLogout_Click(sender As Object, e As EventArgs) Handles BtnLogout.Click
        If MsgBox("Apakah anda yakin mau logout?", MsgBoxStyle.YesNo, "Logout") = MsgBoxResult.Yes Then
            My.Settings.employeeid = ""
            FormLogin.Show()
            Me.Hide()
            MsgBox("Anda berhasil Logout!", MsgBoxStyle.Information)
        Else
            Exit Sub
        End If
    End Sub
End Class