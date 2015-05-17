Imports System
Imports System.IO
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions

Public Class expand

    Dim isactive As Boolean ' Used like semaphore
    Dim expanded_url As String ' Expanded url
    Dim last_url As String ' Last expanded url
    Dim url_id As String ' Finale part of the z0r link

    ' Performs the GET request to z0r.it
    Public Function expand(link As String) As String
        url_id = get_id(link)
        Try
            Dim request As WebRequest
            request = _
           WebRequest.Create("http://z0r.it/yourls-api.php?signature=4e4b657a91&action=expand&format=simply&shorturl=" & url_id)
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

    ' Clipboard URL check
    Private Sub check_url()
        If String.Compare(last_url, Clipboard.GetText()) = 0 Then
            ' If the link in the clipboard is the last expanded url
            z0rurl_warning.Text = main.expand_success  ' Sets the notice text
            z0rurl_warning.ForeColor = Color.Green ' Sets the notice color
            z0rurl_warning.Visible = True ' Display the notice
            isactive = False ' Locks the semaphore
        ElseIf isz0r(Clipboard.GetText) = "error" Then
            '  If the url in clipboard is not a valid z0r link
            z0rurl_warning.Text = main.invalid_z0r ' Sets the notice text
            z0rurl_warning.ForeColor = Color.Red ' Sets the notice color
            z0rurl_warning.Visible = True ' Displays the notice
            isactive = False ' Locks the semaphores
        Else
            z0rurl_warning.Visible = False
            isactive = True
        End If
    End Sub

    ' Timer which repeatedly check the clipboard 
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        check_url() ' clipboard check
    End Sub

    ' Timer start at startup
    Private Sub espandi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    ' Regex
    Dim z0r_regex As New Regex("(http://|http://www)*z0r.it[^ ]+")

    ' z0r URL check
    Public Function isz0r(link As String) As String
        If z0r_regex.IsMatch(link) Then
            Return link
        Else
            link = "http://" & link
            If z0r_regex.IsMatch(link) Then
                Return link
            Else
                Return "error"
            End If
        End If
    End Function

    ' EXPAND button click event
    Private Sub expand_button_Click(sender As Object, e As EventArgs) Handles expand_button.Click
        ' If semaphore is active
        If isactive = True Then
            expanded_url = expand(Clipboard.GetText) ' Get the expanded url
            Clipboard.SetText(expanded_url) ' Put the URL in the clipboard
            expanded_link.Text = expanded_url ' show the url in the textbox
            last_url = expanded_url ' Saves the link in last_url
        End If
    End Sub

    ' Gets the z0r url ID
    Private Function get_id(link As String) As String
        Return link.Split("/").GetValue(link.Split("/").Length - 1)
    End Function
End Class