Imports System
Imports System.IO
Imports System.Net

Public Class customize

    Dim isactive As Boolean ' Used like semaphore
    Dim last_url As String = "none" ' Used to check repeated shrinks
    Dim keyword As String ' Keyword personalizzata

    ' Performs the GET request to z0r.it
    Public Function shrink_custom(link As String, custom_desc As String) As String
        keyword = Replace(custom_desc, " ", "-") ' are removed eventual spaces
        Try
            Dim request As WebRequest
            request = _
           WebRequest.Create("http://z0r.it/yourls-api.php?signature=4e4b657a91&action=shorturl&keyword=" & keyword & "&format=simply&url=" & link & "&title=upload_wth_z0r_desktp")
            request.Credentials = CredentialCache.DefaultCredentials
            Dim response As WebResponse = request.GetResponse()
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            response.Close()
            Return responseFromServer
        Catch ex As Exception
            main.NotifyIcon1.ShowBalloonTip(1, "z0r", "Connection error", ToolTipIcon.Error)
            Exit Function
        End Try
    End Function

    ' URL check
    Public Sub check_url()
        ' It's invoked the function (regex) to check if there is a valid URL
        If main.isurl(Clipboard.GetText) = "error" Then
            ' If it is "error"
            custom_desc.Enabled = False ' Disables the checkbox
            url_warning.Text = main.invalid_url  ' Sets the notice text
            url_warning.ForeColor = Color.Red ' Sets the notice color
            url_warning.Visible = True ' Displays the notice
            isactive = False ' Locks the semaphore to stop repeated shrinks
        ElseIf String.Compare(Clipboard.GetText, last_url) = 0 Then
            ' If there is the last shirnked url (shrink done) displays a succes notice and locks the semaphore to avoid repeated shrinks
            custom_desc.Enabled = False ' Disables the texbox
            url_warning.Text = main.valid_url ' Sets notice text
            url_warning.ForeColor = Color.Green ' Sets the notice color
            url_warning.Visible = True ' displays the notice
            isactive = False ' Locks the semaphore to stop repeated shrinks
        Else
            custom_desc.Enabled = True
            url_warning.Visible = False
            isactive = True
        End If
    End Sub

    ' Timer which repeatedly check the clipboard 
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        check_url() ' Clipboard check
    End Sub

    ' At startup the timer starts
    Private Sub personalizza_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    ' SHRINK button click event
    Private Sub shrink_button_Click(sender As Object, e As EventArgs) Handles shrink_button.Click
        ' If the URL is valid and there is somethink in the textbox
        If isactive = True And custom_desc.TextLength > 0 Then
            main.NotifyIcon1.ShowBalloonTip(1, "z0r", "Shrinking..", ToolTipIcon.Info)
            Clipboard.SetText(shrink_custom(Clipboard.GetText, custom_desc.Text))
            Try
                ' Try to play the file
                My.Computer.Audio.Play("http://l33tspace.altervista.org/Ding.wav") ' Ding
            Catch ex As Exception
                ' If not, amen. The important is to avoid crashes and errors for a fucking ding in a crappy server
            End Try
            custom_desc.Text = "" ' Empties the textbox
            main.NotifyIcon1.ShowBalloonTip(1, "z0r", main.custom_shrink_success, ToolTipIcon.Info) ' Displays a success notice
            last_url = Clipboard.GetText() ' Saves the current URL in last_url
        End If
    End Sub
End Class