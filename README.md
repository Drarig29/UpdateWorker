# 7-Segment Display
## Description
A WinForms Component that checks for updates (VB.NET).<br>It's a **Component**, so **it doesn't have a UI**, just like a `Timer`, or a `BackgroundWorker`...<br>
<br>The library is called **UpdateWorker**. A [demo form](https://github.com/Drarig29/UpdateWorker/tree/master/DemoProject) is included so you can easily see how it works.
## Methods
* **CheckForUpdates** - Checks for updates asynchronously.
* **Cancel** - Cancels checking for updates.

`Public Sub CheckForUpdates()`<br>
`Public Sub CheckForUpdates(UpdateLink As String, InstalledVersion As String)`

## Properties
* **UpdateLink** - The link where the latest version is located on the web.
* **InstalledVersion** - The installed (current) version of the software to compare to the latest version.
* **IsChecking** - Indicates whether the UpdateWorker is checking for updates.

##Events
* **CheckUpdatesCompleted** - Fired when the checking is completed.
  * EventArgs Properties :
    * **Result** As String - The result of the checking (Contains `Const UP_TO_DATE`, or `Const OUTDATED`, or an exception message).
    * **Cancelled** As Boolean - Checking cancelled or not.
    * **LatestVersion** As String - The latest version downloaded string.
    * **InstalledVersion** As String - The installed version associated with the last checking.
    
`Public Event CheckUpdatesCompleted As CheckUpdatesCompletedEventHandler`

>Note : If the `Cancelled` property is `True`, then the 3 others EventArgs properties (`Result`, `LatestVersion`, and `InstalledVersion`) will be `Nothing`.

##How to use
To use this control, [download it](https://raw.githubusercontent.com/Drarig29/UpdateWorker/master/UpdateWorker/bin/Release/UpdateWorker.dll), and **add it to the Toolbox** :<br>
* Right-click on the Toolbox,
* Select "Choose Items...",
* Wait for the loading, and click "Browse...",
* Choose the control (.dll file),
* Click OK, then it's added to Toolbox.

##About
**Latest version** : 1.0<br><br>
Good use!<br>
If you have problems, you can [contact me](mailto:corentinleguitariste@hotmail.fr).
