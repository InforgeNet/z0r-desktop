﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(main))
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AvanzateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PersonalizzaLinkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EstendiLinkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InformazioniToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChiudiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "z0r Desktop"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AvanzateToolStripMenuItem, Me.InformazioniToolStripMenuItem, Me.ChiudiToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(143, 70)
        '
        'AvanzateToolStripMenuItem
        '
        Me.AvanzateToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PersonalizzaLinkToolStripMenuItem, Me.EstendiLinkToolStripMenuItem})
        Me.AvanzateToolStripMenuItem.Name = "AvanzateToolStripMenuItem"
        Me.AvanzateToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.AvanzateToolStripMenuItem.Text = "Avanzate"
        '
        'PersonalizzaLinkToolStripMenuItem
        '
        Me.PersonalizzaLinkToolStripMenuItem.Name = "PersonalizzaLinkToolStripMenuItem"
        Me.PersonalizzaLinkToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.PersonalizzaLinkToolStripMenuItem.ShowShortcutKeys = False
        Me.PersonalizzaLinkToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.PersonalizzaLinkToolStripMenuItem.Text = "Custom link"
        '
        'EstendiLinkToolStripMenuItem
        '
        Me.EstendiLinkToolStripMenuItem.Name = "EstendiLinkToolStripMenuItem"
        Me.EstendiLinkToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.EstendiLinkToolStripMenuItem.ShowShortcutKeys = False
        Me.EstendiLinkToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.EstendiLinkToolStripMenuItem.Text = "Espandi z0r link"
        '
        'InformazioniToolStripMenuItem
        '
        Me.InformazioniToolStripMenuItem.Name = "InformazioniToolStripMenuItem"
        Me.InformazioniToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.InformazioniToolStripMenuItem.Text = "Impostazioni"
        '
        'ChiudiToolStripMenuItem
        '
        Me.ChiudiToolStripMenuItem.Name = "ChiudiToolStripMenuItem"
        Me.ChiudiToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.ChiudiToolStripMenuItem.Text = "Chiudi"
        '
        'main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(82, 24)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "main"
        Me.Text = "z0r Desktop [α]"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents InformazioniToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChiudiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AvanzateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PersonalizzaLinkToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EstendiLinkToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
