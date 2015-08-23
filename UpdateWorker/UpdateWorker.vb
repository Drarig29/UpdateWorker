Imports System.Net
Imports System.ComponentModel

<DefaultEvent("CheckUpdatesCompleted")>
Public Class UpdateWorker
    Inherits Component

    Private WithEvents BackgroundWorker As New BackgroundWorker
    Public Event CheckUpdatesCompleted As CheckUpdatesCompletedEventHandler
    Public Const UP_TO_DATE As String = "UpToDate"
    Public Const OUTDATED As String = "Outdated"
    Private WebClient As New WebClient
    Private LatestVersion As String

    Sub New()
        AddHandler WebClient.DownloadStringCompleted, AddressOf WebClient_DownloadStringCompleted
    End Sub

    ''' <summary>
    ''' Gets or sets the link where the latest version is located on the web.
    ''' For example, you can use a FTP server, Dropbox, etc. to host the file.
    ''' </summary>
    Public Property UpdateLink As String

    ''' <summary>
    ''' Gets or sets the installed (current) version of the software to compare to the latest version.
    ''' </summary>
    Public Property InstalledVersion As String

    ''' <summary>
    ''' Gets a value indicating whether the UpdateWorker is checking for updates.
    ''' </summary>
    <DefaultValue(False), Browsable(False)> Public ReadOnly Property IsChecking As Boolean
        Get
            Return BackgroundWorker.IsBusy
        End Get
    End Property

    ''' <summary>
    ''' Checks for updates asynchronously.
    ''' </summary>
    Public Sub CheckForUpdates()
        BackgroundWorker.RunWorkerAsync()
    End Sub

    ''' <summary>
    ''' Checks for updates asynchronously.
    ''' </summary>
    ''' <param name="UpdateLink">The link where the latest version is located on the web.</param>
    ''' <param name="InstalledVersion">The installed (current) version of the software to compare to the latest version.</param>
    ''' <remarks></remarks>
    Public Sub CheckForUpdates(UpdateLink As String, InstalledVersion As String)
        Me.UpdateLink = UpdateLink
        Me.InstalledVersion = InstalledVersion
        BackgroundWorker.RunWorkerAsync()
    End Sub

    ''' <summary>
    ''' Cancels checking for updates.
    ''' </summary>
    Public Sub Cancel()
        WebClient.CancelAsync()
    End Sub

    Private Sub BackgroundWorker_DoWork() Handles BackgroundWorker.DoWork
        WebClient.DownloadStringAsync(New Uri(UpdateLink))
    End Sub

    Private Sub WebClient_DownloadStringCompleted(sender As Object, e As System.Net.DownloadStringCompletedEventArgs)
        Dim Output As String = Nothing

        If Not e.Cancelled Then
            LatestVersion = e.Result

            If LatestVersion.Contains(InstalledVersion) Then
                Output = UP_TO_DATE
            Else
                Output = OUTDATED
            End If
        End If

        RaiseEvent CheckUpdatesCompleted(sender, If(e.Cancelled, New CheckUpdatesCompletedEventArgs(e.Cancelled), New CheckUpdatesCompletedEventArgs(Output, e.Cancelled, LatestVersion, InstalledVersion)))
    End Sub

    Public Delegate Sub CheckUpdatesCompletedEventHandler(sender As Object, eventArgs As CheckUpdatesCompletedEventArgs)

    Class CheckUpdatesCompletedEventArgs
        Inherits EventArgs

        Property Result As String
        Property Cancelled As Boolean
        Property LatestVersion As String
        Property InstalledVersion As String

        Sub New(Cancelled As Boolean)
            Me.Result = Nothing
            Me.Cancelled = Cancelled
            Me.LatestVersion = Nothing
            Me.InstalledVersion = Nothing
        End Sub

        Sub New(Result As String, Cancelled As Boolean, LatestVersion As String, InstalledVersion As String)
            Me.Result = Result
            Me.Cancelled = Cancelled
            Me.LatestVersion = LatestVersion
            Me.InstalledVersion = InstalledVersion
        End Sub
    End Class

End Class