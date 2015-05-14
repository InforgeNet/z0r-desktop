Imports System
Imports System.IO
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions

Public Class main

    Dim url_toshrink As String ' URL da shrinkare
    Dim clipboard_string As String ' Stringa nella clipboard

    ' Effettua la GET al z0r.it ed ottiene il link shrinkato
    Public Function shrink(link As String) As String
        Dim request As WebRequest
        request = _
       WebRequest.Create("http://z0r.it/yourls-api.php?signature=4e4b657a91&action=shorturl&format=simply&url=" & link & "&title=upload_wth_z0r_desktp")
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
        RegisterHotKey(Me.Handle, 100, MOD_ALT, Keys.Z) ' Hotkey per shrinkare
    End Sub

    ' Legge la pressione di eventuali hotkeys
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            url_toshrink = isurl(Clipboard.GetText) ' Viene effettuato il controllo sulla stringa nella clipboard
            ' Se il link è diverso da 0 e quindi va bene
            If url_toshrink <> "errore" Then
                Clipboard.SetText(shrink(url_toshrink)) ' shrinka e mette il link nella clipboard
                My.Computer.Audio.Play("http://l33tspace.altervista.org/Ding.wav") ' Ding
                NotifyIcon1.ShowBalloonTip(1, "z0r", "Link shrinkato!", ToolTipIcon.Info) ' Notifica di successo
                ' Se il link invece non va bene
            Else
                NotifyIcon1.ShowBalloonTip(1, "z0r", "Link non valido", ToolTipIcon.Error) ' Notifica di errore
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    ' Chiusura di z0r
    Private Sub Form1_FormClosing(ByVal sender As System.Object, _
                        ByVal e As System.Windows.Forms.FormClosingEventArgs) _
                        Handles MyBase.FormClosing
        UnregisterHotKey(Me.Handle, 100) ' Shrink
        Application.Exit()
    End Sub

    ' Regex
    Dim url_regex As New Regex("(https?|ftp|file)://[-A-Za-z0-9\+&@#/%?=~_|!:,.;]*\.[-A-Za-z0-9\+&@#/%=~_|():?]+")

    ' Controlla se la stringa è un url
    Public Function isurl(link As String) As String
        If url_regex.IsMatch(link) Then
            Return link
        Else
            link = "http://" & link
            If url_regex.IsMatch(link) Then
                Return link
            Else
                Return "errore"
            End If
        End If
    End Function

    ' ########## tasti funzione menu tray icon

    Private Sub PersonalizzaLinkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PersonalizzaLinkToolStripMenuItem.Click
        personalizza.Show()
    End Sub

    Private Sub EstendiLinkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EstendiLinkToolStripMenuItem.Click
        espandi.show()
    End Sub

End Class
