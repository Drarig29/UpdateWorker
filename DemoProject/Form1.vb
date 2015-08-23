Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not UpdateWorker1.IsChecking Then
            UpdateWorker1.CheckForUpdates("https://dl.dropboxusercontent.com/s/5ogqwr9kc31r61w/Update.txt", Application.ProductVersion)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        UpdateWorker1.Cancel()
    End Sub

    Private Sub UpdateWorker1_CheckUpdatesCompleted(sender As Object, eventArgs As UpdateWorker.UpdateWorker.CheckUpdatesCompletedEventArgs) Handles UpdateWorker1.CheckUpdatesCompleted
        If eventArgs.Cancelled Then
            MsgBox("Cancelled")
        Else
            Select Case eventArgs.Result
                Case UpdateWorker.UpdateWorker.UP_TO_DATE
                    MsgBox("It's up to date (Latest : " & eventArgs.LatestVersion & ", Installed : " & eventArgs.InstalledVersion & ")")
                Case UpdateWorker.UpdateWorker.OUTDATED
                    MsgBox("It's outdated (Latest : " & eventArgs.LatestVersion & ", Installed : " & eventArgs.InstalledVersion & ")")
                Case Else
                    MsgBox(eventArgs.Result)
            End Select
        End If
    End Sub
End Class
