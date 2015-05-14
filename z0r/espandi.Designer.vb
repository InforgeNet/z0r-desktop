<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class espandi
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(espandi))
        Me.expanded_link = New System.Windows.Forms.TextBox()
        Me.expand_button = New System.Windows.Forms.Label()
        Me.z0rurl_warning = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'expanded_link
        '
        Me.expanded_link.Font = New System.Drawing.Font("Roboto", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.expanded_link.Location = New System.Drawing.Point(16, 16)
        Me.expanded_link.Name = "expanded_link"
        Me.expanded_link.ReadOnly = True
        Me.expanded_link.Size = New System.Drawing.Size(355, 23)
        Me.expanded_link.TabIndex = 0
        '
        'expand_button
        '
        Me.expand_button.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.expand_button.Cursor = System.Windows.Forms.Cursors.Hand
        Me.expand_button.Font = New System.Drawing.Font("Roboto", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.expand_button.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.expand_button.Location = New System.Drawing.Point(377, 16)
        Me.expand_button.Name = "expand_button"
        Me.expand_button.Size = New System.Drawing.Size(60, 23)
        Me.expand_button.TabIndex = 4
        Me.expand_button.Text = "ESPANDI"
        Me.expand_button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'z0rurl_warning
        '
        Me.z0rurl_warning.AutoSize = True
        Me.z0rurl_warning.Font = New System.Drawing.Font("Roboto", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.z0rurl_warning.ForeColor = System.Drawing.Color.Red
        Me.z0rurl_warning.Location = New System.Drawing.Point(15, 45)
        Me.z0rurl_warning.Name = "z0rurl_warning"
        Me.z0rurl_warning.Size = New System.Drawing.Size(205, 15)
        Me.z0rurl_warning.TabIndex = 5
        Me.z0rurl_warning.Text = "Non hai un url valido nella clipboard"
        '
        'Timer1
        '
        '
        'espandi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(454, 67)
        Me.Controls.Add(Me.z0rurl_warning)
        Me.Controls.Add(Me.expand_button)
        Me.Controls.Add(Me.expanded_link)
        Me.Font = New System.Drawing.Font("Roboto Cn", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "espandi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Espandi link"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents expanded_link As System.Windows.Forms.TextBox
    Friend WithEvents expand_button As System.Windows.Forms.Label
    Friend WithEvents z0rurl_warning As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
