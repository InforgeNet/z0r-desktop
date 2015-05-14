<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class personalizza
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
        Me.components = New System.ComponentModel.Container()
        Me.custom_desc = New System.Windows.Forms.TextBox()
        Me.shrink_button = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.url_warning = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'custom_desc
        '
        Me.custom_desc.Location = New System.Drawing.Point(16, 16)
        Me.custom_desc.Name = "custom_desc"
        Me.custom_desc.Size = New System.Drawing.Size(316, 23)
        Me.custom_desc.TabIndex = 0
        '
        'shrink_button
        '
        Me.shrink_button.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.shrink_button.Cursor = System.Windows.Forms.Cursors.Hand
        Me.shrink_button.Font = New System.Drawing.Font("Roboto", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.shrink_button.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.shrink_button.Location = New System.Drawing.Point(338, 16)
        Me.shrink_button.Name = "shrink_button"
        Me.shrink_button.Size = New System.Drawing.Size(60, 23)
        Me.shrink_button.TabIndex = 3
        Me.shrink_button.Text = "SHRINK"
        Me.shrink_button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Timer1
        '
        '
        'url_warning
        '
        Me.url_warning.AutoSize = True
        Me.url_warning.ForeColor = System.Drawing.Color.Red
        Me.url_warning.Location = New System.Drawing.Point(15, 45)
        Me.url_warning.Name = "url_warning"
        Me.url_warning.Size = New System.Drawing.Size(225, 15)
        Me.url_warning.TabIndex = 4
        Me.url_warning.Text = "Non hai un url z0r valido nella clipboard"
        '
        'personalizza
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(415, 67)
        Me.Controls.Add(Me.url_warning)
        Me.Controls.Add(Me.shrink_button)
        Me.Controls.Add(Me.custom_desc)
        Me.Font = New System.Drawing.Font("Roboto", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "personalizza"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Custom link"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents custom_desc As System.Windows.Forms.TextBox
    Friend WithEvents shrink_button As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents url_warning As System.Windows.Forms.Label
End Class
