Imports System
Imports System.IO
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions

Public Class espandi

    Dim isactive As Boolean ' Utilizzata come semaforo
    Dim expanded_url As String ' Url espanso
    Dim last_url As String ' Ultimo link espanso
    Dim url_id As String ' Parte finale url

    ' Effettua la GET al z0r.it ed ottiene il link espando
    Public Function expand(link As String) As String
        url_id = get_id(link)
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
    End Function

    ' Controllo url nella clipboard
    Private Sub check_url()
        ' Viene controllato se la stringa nella clipboard è un url z0r valido
        If String.Compare(last_url, Clipboard.GetText()) = 0 Then
            ' Se l'url nella clipboard corrisponde all'ultimo link espanso
            z0rurl_warning.Text = "Link espanso con successo!" ' Imposta il testo dell'avviso
            z0rurl_warning.ForeColor = Color.Green ' Imposta il colore del font dell'avviso
            z0rurl_warning.Visible = True ' Mostra l'avviso di errore
            isactive = False ' Imposta il semaforo bloccando il pulsante ESPANDI
        ElseIf isz0r(Clipboard.GetText) = "errore" Then
            ' Se l'url non è un link z0r valido
            z0rurl_warning.Text = "Non hai un url z0r valido nella clipboard" ' Imposta il testo dell'avviso
            z0rurl_warning.ForeColor = Color.Red ' Imposta il colore del font dell'avviso
            z0rurl_warning.Visible = True ' Mostra l'avviso di errore
            isactive = False ' Imposta il semaforo bloccando il pulsante ESPANDI
        Else
            ' Altrimenti viene nascosto l'avviso ed abilitato il pulsante ESPANDI
            z0rurl_warning.Visible = False
            isactive = True
        End If
    End Sub

    ' Per aggiungere un clipboard change event avrei dovuto buttar dentro un sacco di codice
    ' Meglio un timer che controlla costantemente la clipboard
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        check_url() ' Controlla la clipboard
    End Sub

    ' All'apertura avvia il timer
    Private Sub espandi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    ' Regex
    Dim z0r_regex As New Regex("http://z0r.it/[^ ]+")

    ' Controlla se la stringa è un url z0r
    Public Function isz0r(link As String) As String
        If z0r_regex.IsMatch(link) Then
            Return link
        Else
            link = "http://" & link
            If z0r_regex.IsMatch(link) Then
                Return link
            Else
                Return "errore"
            End If
        End If
    End Function

    ' Quando viene premuto il bottone ESPANDI
    Private Sub expand_button_Click(sender As Object, e As EventArgs) Handles expand_button.Click
        ' Se il semaforo è attivo
        If isactive = True Then
            expanded_url = expand(Clipboard.GetText) ' Ricava l'url espanso
            Clipboard.SetText(expanded_url) ' Lo copia nella clipboard
            expanded_link.Text = expanded_url ' Lo mostra nella textbox
            last_url = expanded_url ' Lo copia in last_url per controllare che non vi siano doppie espansioni
        End If
    End Sub

    Dim first_split As String ' Serve per memorizzare uno split temporaneo

    ' Preleva l'ID da un link z0r
    Private Function get_id(link As String) As String
        first_split = link.Split(".").GetValue(1) ' Primo split, questo rimuove la necessità di controllare se si tratti di un link con http o no
        Return first_split.Split("/").GetValue(1) ' Restituisce l'ID trovato
    End Function
End Class