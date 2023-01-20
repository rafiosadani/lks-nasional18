<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminNav
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LblAdminName = New System.Windows.Forms.Label()
        Me.BtnEmployee = New System.Windows.Forms.Button()
        Me.BtnMenu = New System.Windows.Forms.Button()
        Me.BtnMember = New System.Windows.Forms.Button()
        Me.BtnPassword = New System.Windows.Forms.Button()
        Me.BtnLogout = New System.Windows.Forms.Button()
        Me.BtnReport = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(112, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(180, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Admin Navigation"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(23, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Welcome,"
        '
        'LblAdminName
        '
        Me.LblAdminName.AutoSize = True
        Me.LblAdminName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAdminName.Location = New System.Drawing.Point(86, 77)
        Me.LblAdminName.Name = "LblAdminName"
        Me.LblAdminName.Size = New System.Drawing.Size(97, 16)
        Me.LblAdminName.TabIndex = 2
        Me.LblAdminName.Text = "[Admin Name] "
        '
        'BtnEmployee
        '
        Me.BtnEmployee.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEmployee.Location = New System.Drawing.Point(121, 114)
        Me.BtnEmployee.Name = "BtnEmployee"
        Me.BtnEmployee.Size = New System.Drawing.Size(162, 41)
        Me.BtnEmployee.TabIndex = 3
        Me.BtnEmployee.Text = "Manage Employee"
        Me.BtnEmployee.UseVisualStyleBackColor = True
        '
        'BtnMenu
        '
        Me.BtnMenu.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMenu.Location = New System.Drawing.Point(120, 161)
        Me.BtnMenu.Name = "BtnMenu"
        Me.BtnMenu.Size = New System.Drawing.Size(162, 41)
        Me.BtnMenu.TabIndex = 4
        Me.BtnMenu.Text = "Manage Menu"
        Me.BtnMenu.UseVisualStyleBackColor = True
        '
        'BtnMember
        '
        Me.BtnMember.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMember.Location = New System.Drawing.Point(120, 208)
        Me.BtnMember.Name = "BtnMember"
        Me.BtnMember.Size = New System.Drawing.Size(162, 41)
        Me.BtnMember.TabIndex = 5
        Me.BtnMember.Text = "Manage Member"
        Me.BtnMember.UseVisualStyleBackColor = True
        '
        'BtnPassword
        '
        Me.BtnPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPassword.Location = New System.Drawing.Point(120, 255)
        Me.BtnPassword.Name = "BtnPassword"
        Me.BtnPassword.Size = New System.Drawing.Size(162, 41)
        Me.BtnPassword.TabIndex = 6
        Me.BtnPassword.Text = "Change Password"
        Me.BtnPassword.UseVisualStyleBackColor = True
        '
        'BtnLogout
        '
        Me.BtnLogout.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLogout.Location = New System.Drawing.Point(120, 349)
        Me.BtnLogout.Name = "BtnLogout"
        Me.BtnLogout.Size = New System.Drawing.Size(162, 41)
        Me.BtnLogout.TabIndex = 7
        Me.BtnLogout.Text = "Logout"
        Me.BtnLogout.UseVisualStyleBackColor = True
        '
        'BtnReport
        '
        Me.BtnReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReport.Location = New System.Drawing.Point(120, 302)
        Me.BtnReport.Name = "BtnReport"
        Me.BtnReport.Size = New System.Drawing.Size(162, 41)
        Me.BtnReport.TabIndex = 8
        Me.BtnReport.Text = "View Report"
        Me.BtnReport.UseVisualStyleBackColor = True
        '
        'AdminNav
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(391, 399)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnReport)
        Me.Controls.Add(Me.BtnLogout)
        Me.Controls.Add(Me.BtnPassword)
        Me.Controls.Add(Me.BtnMember)
        Me.Controls.Add(Me.BtnMenu)
        Me.Controls.Add(Me.BtnEmployee)
        Me.Controls.Add(Me.LblAdminName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "AdminNav"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Admin Navigation"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LblAdminName As Label
    Friend WithEvents BtnEmployee As Button
    Friend WithEvents BtnMenu As Button
    Friend WithEvents BtnMember As Button
    Friend WithEvents BtnPassword As Button
    Friend WithEvents BtnLogout As Button
    Friend WithEvents BtnReport As Button
End Class
