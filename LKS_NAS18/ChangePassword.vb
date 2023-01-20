Imports System.ComponentModel
Imports System.Data.Odbc

Public Class ChangePassword
    Const DSN = "DSN=db_lks18"
    Dim koneksi, koneksi2 As OdbcConnection

    Private Sub ChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi = New OdbcConnection(DSN)
        koneksi2 = New OdbcConnection(DSN)
        Awal()
    End Sub

    Private Sub ChangePassword_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If My.Settings.position = "Admin" Then
            AdminNav.Show()
            Me.Hide()
        ElseIf My.Settings.position = "Chef" Then
            ChefNavigation.Show
            Me.Hide()
        ElseIf My.Settings.position = "Cashier" Then
            CashierNavigation.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If TxtOld.Text = "" And TxtNew.Text = "" And TxtConfirm.Text = "" Then
            MsgBox("Data tidak boleh kosong! harus diisi!", MsgBoxStyle.Critical)
        Else
            If TxtOld.Text = "" Or TxtNew.Text = "" Or TxtConfirm.Text = "" Then
                MsgBox("Data tidak boleh kosong! harus diisi!", MsgBoxStyle.Critical)
            Else
                Dim sql As String = "SELECT password FROM msemployee WHERE employeeid = '" + My.Settings.employeeid + "'"
                Dim cmd As New OdbcCommand With {
                    .CommandText = sql,
                    .Connection = koneksi
                }
                koneksi.Open()
                Dim dr As OdbcDataReader
                dr = cmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    If TxtOld.Text <> dr.Item("password") Then
                        MsgBox("Harus sama dengan password yang lama!", MsgBoxStyle.Critical)
                        TxtOld.Focus()
                    Else
                        If TxtOld.Text = TxtNew.Text Then
                            MsgBox("Password baru tidak boleh sama dengan password lama!", MsgBoxStyle.Critical)
                            TxtNew.Focus()
                        Else
                            If TxtNew.Text <> TxtConfirm.Text Then
                                MsgBox("Password dengan Konfirmasi password harus sama!", MsgBoxStyle.Critical)
                                TxtConfirm.Focus()
                            Else
                                Dim queryupdate As String = "UPDATE msemployee SET password = '" + TxtConfirm.Text + "' WHERE employeeid = '" + My.Settings.employeeid + "'"
                                Dim cmdupdate As New OdbcCommand With {
                                    .CommandText = queryupdate,
                                    .Connection = koneksi2
                                }
                                koneksi2.Open()
                                cmdupdate.ExecuteNonQuery()
                                koneksi2.Close()

                                Awal()
                                MsgBox("Password berhasil diubah!", MsgBoxStyle.Information)
                            End If
                        End If
                    End If
                End If
                koneksi.Close()
            End If
        End If
    End Sub

    Private Sub Awal()
        TxtOld.Text = ""
        TxtNew.Text = ""
        TxtConfirm.Text = ""
    End Sub
End Class