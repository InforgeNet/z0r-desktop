<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class impostazioni
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
        Me.runasAdmin = New System.Windows.Forms.CheckBox()
        Me.admin_warn = New System.Windows.Forms.Label()
        Me.version = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'runasAdmin
        '
        Me.runasAdmin.AutoSize = True
        Me.runasAdmin.Location = New System.Drawing.Point(15, 15)
        Me.runasAdmin.Name = "runasAdmin"
        Me.runasAdmin.Size = New System.Drawing.Size(192, 19)
        Me.runasAdmin.TabIndex = 0
        Me.runasAdmin.Text = "Avvia z0r all'avvio di Windows"
        Me.runasAdmin.UseVisualStyleBackColor = True
        '
        'admin_warn
        '
        Me.admin_warn.ForeColor = System.Drawing.Color.Red
        Me.admin_warn.Location = New System.Drawing.Point(15, 43)
        Me.admin_warn.Name = "admin_warn"
        Me.admin_warn.Size = New System.Drawing.Size(297, 39)
        Me.admin_warn.TabIndex = 1
        Me.admin_warn.Text = "Avvia z0r come amministratore per abilitare la funzione"
        Me.admin_warn.Visible = False
        '
        'version
        '
        Me.version.Location = New System.Drawing.Point(12, 247)
        Me.version.Name = "version"
        Me.version.Size = New System.Drawing.Size(303, 24)
        Me.version.TabIndex = 2
        Me.version.Text = "versione 0.1"
        Me.version.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'impostazioni
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(327, 280)
        Me.Controls.Add(Me.version)
        Me.Controls.Add(Me.admin_warn)
        Me.Controls.Add(Me.runasAdmin)
        Me.Font = New System.Drawing.Font("Roboto", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "impostazioni"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "z0r Desktop [β]"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents runasAdmin As System.Windows.Forms.CheckBox
    Friend WithEvents admin_warn As System.Windows.Forms.Label
    Friend WithEvents version As System.Windows.Forms.Label
End Class
