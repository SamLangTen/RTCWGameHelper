Imports RtCWGH.Lib.Game
Class Application


    ' 应用程序级事件(例如 Startup、Exit 和 DispatcherUnhandledException)
    ' 可以在此文件中进行处理。

    Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        'System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("zh-HK")
        GameProcess.RTCWPath = My.Settings.RTCWPath
    End Sub
End Class
