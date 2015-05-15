Imports Microsoft.Win32

Public Class impostazioni

    Private Sub runasAdmin_CheckedChanged(sender As Object, e As EventArgs) Handles runasAdmin.CheckedChanged
        ' Se la checkbox viene selezionata
        If runasAdmin.CheckState = CheckState.Checked Then
            ' E se il programma non si trova già nella startup
            If isinStartup() = False Then
                ' Inserisce nela startup il programma
                My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).SetValue(Application.ProductName, Application.ExecutablePath)
            End If

        Else ' Se la checkbox viene invece deselezionata
            ' E il programma si trova nella startup
            If isinStartup() = True Then
                ' Rimuove il programma dalla startup
                My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).DeleteValue(Application.ProductName)
            End If
        End If
    End Sub

    ' Controlla se il programma è già presenta nella startup di windows
    Public Function isinStartup() As Boolean
        If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
            If My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).GetValue(Application.ProductName) <> Nothing Then
                Return True
            End If
            Return False
        End If
        ' Se non si hanno i permessi admin non effettua il controllo o esplode tutto.
        Return False
    End Function


    Private Sub impostazioni_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Se l'utente non ha permessi da amministrazione
        If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) = False Then
            ' Nasconde l'impostazione per avviare z0r all'avvio di Windows, altrimenti esplode tutto.
            runasAdmin.Enabled = False
            ' E avvisa l'utente che non può fare un cavolo
            admin_warn.Visible = True
        End If
        ' Se il programma si trova/non si trova nella startup aggiorna di conseguenza la checkbox
        If isinStartup() = True Then
            runasAdmin.Checked = True
        Else
            runasAdmin.Checked = False
        End If
    End Sub
End Class