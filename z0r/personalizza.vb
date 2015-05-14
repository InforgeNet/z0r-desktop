Imports System
Imports System.IO
Imports System.Net

Public Class personalizza

    Dim isactive As Boolean ' Utilizzata come semaforo
    Dim last_url As String = "none" ' Utilizzato come controllore per shrink ripetuti

    ' Effettua la GET al z0r.it ed ottiene il link shrinkato custom
    Public Function shrink_custom(link As String, custom_desc As String) As String
        Dim request As WebRequest
        request = _
       WebRequest.Create("http://z0r.it/yourls-api.php?signature=4e4b657a91&action=shorturl&keyword=" & custom_desc & "&format=simply&url=" & link)
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

    ' Controllo url nella clipboard
    Private Sub check_url()
        ' Viene richiamata la funzione per controllare se la stringa nella clipboard è un url valido
        If main.isurl(Clipboard.GetText) = "errore" Then
            ' Se è "errore"
            custom_desc.Enabled = False ' Disabilita la text box
            url_warning.Text = "Non hai un url valido nella clipboard" ' Imposta il testo dell'avviso
            url_warning.ForeColor = Color.Red ' Imposta il colore del font dell'avviso
            url_warning.Visible = True ' Mostra l'avviso di errore
            isactive = False ' Imposta il semaforo bloccando il pulsante SHRINK
        ElseIf String.Compare(Clipboard.GetText, last_url) = 0 Then
            ' Se è presente l'ultimo link shrinkato, mostra un avviso di avvenuto shrink e blocca un eventuale doppio shrink
            custom_desc.Enabled = False ' Disabilita la text box
            url_warning.Text = "Link shrinkato con successo!" ' Imposta il testo dell'avviso
            url_warning.ForeColor = Color.Green ' Imposta il colore del font dell'avviso
            url_warning.Visible = True ' Mostra l'avviso di errore
            isactive = False ' Imposta il semaforo bloccando il pulsante SHRINK
        Else
            ' Altrimenti, se l'url è valido viene abilitata la funzione e rimosso il warning
            custom_desc.Enabled = True
            url_warning.Visible = False
            isactive = True
        End If
    End Sub

    ' Per aggiungere un clipboard change event avrei dovuto buttar dentro un sacco di codice
    ' Meglio un timer che controlla costantemente la clipboard
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        check_url() ' Controlla la clipboard
    End Sub

    ' All'apertura avvia il timer
    Private Sub personalizza_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    ' Quando viene cliccato il bottone SHIRNK
    Private Sub shrink_button_Click(sender As Object, e As EventArgs) Handles shrink_button.Click
        ' Se l'URL è valido ed è stato inserito qualcosa nella textbox
        If isactive = True And custom_desc.TextLength > 0 Then
            Clipboard.SetText(shrink_custom(Clipboard.GetText, custom_desc.Text)) ' Viene generato l'url e messo nella clipboard
            My.Computer.Audio.Play("http://l33tspace.altervista.org/Ding.wav") ' Ding
            custom_desc.Text = "" ' Svuotata la textbox
            main.NotifyIcon1.ShowBalloonTip(1, "z0r", "Link custom shrinkato!", ToolTipIcon.Info) ' E mostrata la notifica
            last_url = Clipboard.GetText() ' Salva in last_url il link corrente
        End If
    End Sub
End Class