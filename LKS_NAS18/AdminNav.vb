Public Class AdminNav
    Private Sub BtnEmployee_Click(sender As Object, e As EventArgs) Handles BtnEmployee.Click
        ManageEmployee.Show()
        Me.Hide()
    End Sub

    Private Sub BtnMenu_Click(sender As Object, e As EventArgs) Handles BtnMenu.Click
        ManageMenu.Show()
        Me.Hide()
    End Sub

    Private Sub BtnMember_Click(sender As Object, e As EventArgs) Handles BtnMember.Click
        ManageMember.Show()
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

    Private Sub BtnReport_Click(sender As Object, e As EventArgs) Handles BtnReport.Click
        FormReport.Show()
        Me.Hide()
    End Sub
End Class