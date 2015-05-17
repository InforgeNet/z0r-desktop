Imports System
Imports System.IO
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Globalization

Public Class main

    Dim url_toshrink As String ' URL ready to shrink
    Dim clipboard_string As String ' String in clipboard
    Dim last_shrinked As String ' Last URL shrinked to avoid double shrinks

    ' Languages
    Dim system_lang As String ' Get the three letters ISO language

    ' Lang var initialization 
    Dim closing_question As String
    Dim closing_title As String
    Dim z0r_active As String
    Dim shrink_done As String
    Dim shrink_warning As String
    Dim shrink_error As String
    Public invalid_url As String
    Public valid_url As String
    Public custom_shrink_success As String
    Public expand_success As String
    Public invalid_z0r As String
    Dim downloading_files As String

    ' Se the language according to the three lang ISO letters
    Public Sub set_language()
        system_lang = CultureInfo.CurrentCulture.ThreeLetterISOLanguageName
        Select Case system_lang
            Case "ita"
                closing_question = "Sei sicuro di voler chiudere z0r?"
                closing_title = "Chiudi z0r"
                z0r_active = "Attivo!"
                shrink_done = "Link shrinkato e copiato nella clipboard!"
                shrink_warning = "Hai ancora nella clipboard l'ultimo link shrinkato: "
                shrink_error = "URL non valido nella clipboard"
                invalid_url = "Non hai un url valido nella clipboard"
                valid_url = "Link shrinkato con successo!"
                custom_shrink_success = "Link custom shrinkato!"
                expand_success = "Link espanso con successo!"
                invalid_z0r = "Non hai un url z0r valido nella clipboard"
                expand.expand_button.Text = "ESPANDI"
                settings.runasAdmin.Text = "Avvia z0r all'avvio di Windows"
                settings.admin_warn.Text = "Avvia z0r come amministratore per modificare la funzione"
                downloading_files = "Sto scaricando i file necessari..."
            Case Else
                closing_question = "Are you sure to quit z0r?"
                closing_question = "Quit z0r"
                z0r_active = "Working!"
                shrink_done = "Link shrinked and copied to the clipboard!"
                shrink_warning = "You still have the last shrinked link in the clipboard: "
                shrink_error = "Invalid URL in the clipbaord"
                invalid_url = "You don't have a valid URL in the clipboard"
                valid_url = "URL shrinked successfully!"
                custom_shrink_success = "Custom link shrinked!"
                expand_success = "Link expanded successfully!"
                invalid_z0r = "Your don't have a valid z0r URL in the clipboard"
                expand.expand_button.Text = "EXPAND"
                settings.runasAdmin.Text = "Run z0r at Windows startup"
                settings.admin_warn.Text = "Run z0r as administrator to unlick this option"
                downloading_files = "Downloading necessary files..."
        End Select

    End Sub

    Dim main_folder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "/z0r" ' Main z0r folder (actually unused)
    Dim settings_file As String = main_folder & "/z0r_settings.ini" ' Settings file in z0r folder (actually unused)
    Public sound_file As String = main_folder & "/z0r_ding.wav"


    Dim request As WebRequest, response As WebResponse, dataStream As Stream, responseFromServer As String

    ' Performs the GET request to z0r.it
    Public Function shrink(link As String) As String
        NotifyIcon1.ShowBalloonTip(1, "z0r", "Shrinking...", ToolTipIcon.Info)
        Try
            request = _
       WebRequest.Create("http://z0r.it/yourls-api.php?signature=4e4b657a91&action=shorturl&format=simply&url=" & link & "&title=upload_wth_z0r_desktp")
            request.Credentials = CredentialCache.DefaultCredentials
            response = request.GetResponse()
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            responseFromServer = reader.ReadToEnd()
            reader.Close()
            response.Close()
            Return remove_www(responseFromServer)
        Catch ex As Exception
            NotifyIcon1.ShowBalloonTip(1, "z0r", "Connection error", ToolTipIcon.Error)
            Exit Function
        End Try
    End Function

    ' Question at form closing
    Private Sub ChiudiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChiudiToolStripMenuItem.Click
        Dim risp As Integer
        risp = MessageBox.Show(closing_question, closing_title, _
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If risp = vbYes Then
            Application.Exit()
        End If
    End Sub

    ' Enable the tray icon and displays a startup message
    Private Sub zor_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Try
            If Me.WindowState = FormWindowState.Minimized Then
                Me.Visible = False
                NotifyIcon1.Visible = True
                NotifyIcon1.ShowBalloonTip(1, "z0r", z0r_active, ToolTipIcon.Info)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ' Minimize at startup
    Private Sub zor_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.WindowState = FormWindowState.Minimized
    End Sub

    ' Click event to show the settings
    Private Sub InformazioniToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InformazioniToolStripMenuItem.Click
        settings.Show()
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
        set_language() ' Checks and sets the language
        RegisterHotKey(Me.Handle, 100, MOD_ALT, Keys.Z) ' Hotkey for shrink
        check_files() ' File check
    End Sub

    ' Read eventual hotkeys
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            url_toshrink = isurl(Clipboard.GetText) ' Clipbaord strink check
            ' If there is a valid URL in the clipbaord then
            If url_toshrink <> "error" And url_toshrink <> last_shrinked Then
                Clipboard.SetText(shrink(url_toshrink)) ' shrink the URL and put it in the clipboard
                last_shrinked = Clipboard.GetText ' Save the shrinked URL to avoid double shrinking
                My.Computer.Audio.Play(sound_file)
                NotifyIcon1.ShowBalloonTip(1, "z0r", shrink_done, ToolTipIcon.Info) ' Success notification
                ' If the URL is not valid
            ElseIf url_toshrink = last_shrinked Then
                ' If in the clipbaord there is the last shrinked URL
                NotifyIcon1.ShowBalloonTip(1, "z0r", shrink_warning & vbCrLf & last_shrinked, ToolTipIcon.Warning) ' Warning notificatio
            Else
                NotifyIcon1.ShowBalloonTip(1, "z0r", shrink_error, ToolTipIcon.Error) ' Error notification
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    ' z0r closing
    Private Sub Form1_FormClosing(ByVal sender As System.Object, _
                        ByVal e As System.Windows.Forms.FormClosingEventArgs) _
                        Handles MyBase.FormClosing
        UnregisterHotKey(Me.Handle, 100) ' Shrink hotkey
        Application.Exit()
    End Sub

    ' Regex
    Dim url_regex As New Regex("(https?|ftp|file)://[-A-Za-z0-9\+&@#/%?=~_|!:,.;]*\.[-A-Za-z0-9\+&@#/%=~_|():?]+")

    ' Check if the strink is a valid URL
    Public Function isurl(link As String) As String
        If url_regex.IsMatch(link) Then
            Return link
        Else
            link = "http://" & link
            If url_regex.IsMatch(link) Then
                Return link
            Else
                Return "error"
            End If
        End If
    End Function

    ' ########## Tray icon menu function keys

    Private Sub PersonalizzaLinkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PersonalizzaLinkToolStripMenuItem.Click
        customize.Show()
    End Sub

    Private Sub EstendiLinkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EstendiLinkToolStripMenuItem.Click
        expand.Show()
    End Sub

    ' Check if the files are OK (unused at the moment)
    Private Sub check_files()
        If Directory.Exists(main_folder) = False Then
            Directory.CreateDirectory(main_folder) ' Crea la directory per z0r
        End If
        If File.Exists(settings_file) = False Then
            ' C'è la directory ma per qualche motivo non il file settings
            ' Non si fa niente per ora, ma può servire
        End If
        If File.Exists(sound_file) = False Then
            NotifyIcon1.ShowBalloonTip(1, "z0r", downloading_files, ToolTipIcon.Info)
            Dim WebClient As New WebClient()
            WebClient.DownloadFile("http://l33tspace.altervista.org/Ding.wav", sound_file)
        End If

    End Sub

    ' Remove www in the URL
    Public Function remove_www(url As String) As String
        If url.Split(".").GetValue(0) = "http://www" Then
            Return "http://z0r." & url.Split(".").GetValue(2)
        Else
            Return url
        End If
    End Function

End Class