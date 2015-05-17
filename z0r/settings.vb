Imports Microsoft.Win32

Public Class settings

    Private Sub runasAdmin_CheckedChanged(sender As Object, e As EventArgs) Handles runasAdmin.CheckedChanged
        ' If the checkbox is checked
        If runasAdmin.CheckState = CheckState.Checked Then
            ' and z0r isn't already in the startup register
            If isinStartup() = False Then
                ' Inserts z0r
                My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).SetValue(Application.ProductName, Application.ExecutablePath)
            End If

        Else ' If the checkbox is unchecked
            ' And z0r is in the startup register
            If isinStartup() = True Then
                ' Removes z0r
                My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).DeleteValue(Application.ProductName)
            End If
        End If
    End Sub

    ' Check if z0r already is in the startup register
    Public Function isinStartup() As Boolean
        ' The check is made only if there are admin privileges...
        If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
            If My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).GetValue(Application.ProductName) <> Nothing Then
                Return True
            End If
            Return False
        End If
        Return False
        '... or everything will explode
    End Function


    Private Sub impostazioni_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' If the aren't admin privileges
        If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) = False Then
            ' Hide the setting
            runasAdmin.Enabled = False
            ' and notice the user
            admin_warn.Visible = True
        End If
        ' If the program is/isn't in the startup register,,
        If isinStartup() = True Then
            runasAdmin.Checked = True
        Else
            runasAdmin.Checked = False
        End If
    End Sub
End Class