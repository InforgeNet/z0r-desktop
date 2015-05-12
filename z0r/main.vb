Imports System
Imports System.IO
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions

Public Class main

    Dim url_toshrink As String

    ' Effettua la GET al z0r.it ed ottiene il link shrinkato
    Public Function shrinka(link As String) As String
        Dim request As WebRequest
        request = _
       WebRequest.Create("http://z0r.it/yourls-api.php?signature=4e4b657a91&action=shorturl&format=simply&url=" & link)
        request.Credentials = CredentialCache.DefaultCredentials
        Dim response As WebResponse = request.GetResponse()
        Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
        Dim dataStream As Stream = response.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim responseFromServer As String = reader.ReadToEnd()
        reader.Close()
        response.Close()
        Return responseFromServer
    End Function

    ' Domanda prima di chiudere il bot
    Private Sub ChiudiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChiudiToolStripMenuItem.Click
        Dim risp As Integer
        risp = MessageBox.Show("Sei sicuro di voler chiudere z0r?", "Chiudi z0r", _
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If risp = vbYes Then
            Application.Exit()
        End If
    End Sub

    ' Abilita l'icona nel tray e mostra un messaggio di avvenuto avvio
    Private Sub zor_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Try
            If Me.WindowState = FormWindowState.Minimized Then
                Me.Visible = False
                NotifyIcon1.Visible = True
                NotifyIcon1.ShowBalloonTip(1, "z0r", "Attivo", ToolTipIcon.Info)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ' Minimizza all'avvio
    Private Sub zor_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.WindowState = FormWindowState.Minimized
    End Sub

    ' Event che apre il box informazioni se cliccato il relativo bottone
    Private Sub InformazioniToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InformazioniToolStripMenuItem.Click
        informazioni.Show()
    End Sub

    ' ######## Hotkeys
    Public Const MOD_ALT As Integer = &H1 'Alt key
    Public Const WM_HOTKEY As Integer = &H312

    <DllImport("User32.dll")> _
    Public Shared Function RegisterHotKey(ByVal hwnd As IntPtr, _
                        ByVal id As Integer, ByVal fsModifiers As Integer, _
                        ByVal vk As Integer) As Integer
    End Function

    <DllImport("User32.dll")> _
    Public Shared Function UnregisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    End Function
    ' ##########

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RegisterHotKey(Me.Handle, 100, MOD_ALT, Keys.Z)
        RegisterHotKey(Me.Handle, 200, MOD_ALT, Keys.S) ' per ora non utilizzata, ma non si sa mai
    End Sub

    ' Legge la pressione di eventuali hotkeys
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            url_toshrink = Clipboard.GetText ' prendo il link dalla clipboard
            Clipboard.SetText(shrinka(url_toshrink)) ' e lo rimetto dentro shrinkato
            My.Computer.Audio.Play("http://l33tspace.altervista.org/Ding.wav")
            NotifyIcon1.ShowBalloonTip(1, "z0r", "Link shrinkato!", ToolTipIcon.Info)

        End If
        MyBase.WndProc(m)
    End Sub

    ' Chiusura di z0r
    Private Sub Form1_FormClosing(ByVal sender As System.Object, _
                        ByVal e As System.Windows.Forms.FormClosingEventArgs) _
                        Handles MyBase.FormClosing
        NotifyIcon1.Visible = False
        UnregisterHotKey(Me.Handle, 100)
        UnregisterHotKey(Me.Handle, 200) ' attualmente inutilizzata
    End Sub

End Class
