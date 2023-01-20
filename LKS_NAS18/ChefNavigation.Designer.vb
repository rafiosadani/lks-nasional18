<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChefNavigation
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BtnLogout = New System.Windows.Forms.Button()
        Me.BtnPassword = New System.Windows.Forms.Button()
        Me.BtnView = New System.Windows.Forms.Button()
        Me.LblChefName = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BtnLogout
        '
        Me.BtnLogout.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLogout.Location = New System.Drawing.Point(121, 210)
        Me.BtnLogout.Name = "BtnLogout"
        Me.BtnLogout.Size = New System.Drawing.Size(162, 41)
        Me.BtnLogout.TabIndex = 15
        Me.BtnLogout.Text = "Logout"
        Me.BtnLogout.UseVisualStyleBackColor = True
        '
        'BtnPassword
        '
        Me.BtnPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPassword.Location = New System.Drawing.Point(120, 163)
        Me.BtnPassword.Name = "BtnPassword"
        Me.BtnPassword.Size = New System.Drawing.Size(162, 41)
        Me.BtnPassword.TabIndex = 14
        Me.BtnPassword.Text = "Change Password"
        Me.BtnPassword.UseVisualStyleBackColor = True
        '
        'BtnView
        '
        Me.BtnView.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnView.Location = New System.Drawing.Point(120, 116)
        Me.BtnView.Name = "BtnView"
        Me.BtnView.Size = New System.Drawing.Size(162, 41)
        Me.BtnView.TabIndex = 11
        Me.BtnView.Text = "View Order"
        Me.BtnView.UseVisualStyleBackColor = True
        '
        'LblChefName
        '
        Me.LblChefName.AutoSize = True
        Me.LblChefName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblChefName.Location = New System.Drawing.Point(85, 79)
        Me.LblChefName.Name = "LblChefName"
        Me.LblChefName.Size = New System.Drawing.Size(86, 16)
        Me.LblChefName.TabIndex = 10
        Me.LblChefName.Text = "[Chef Name] "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 16)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Welcome,"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(111, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(165, 25)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Chef Navigation"
        '
        'ChefNavigation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 275)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnLogout)
        Me.Controls.Add(Me.BtnPassword)
        Me.Controls.Add(Me.BtnView)
        Me.Controls.Add(Me.LblChefName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ChefNavigation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Chef Navigation"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnLogout As Button
    Friend WithEvents BtnPassword As Button
    Friend WithEvents BtnView As Button
    Friend WithEvents LblChefName As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
End Class
