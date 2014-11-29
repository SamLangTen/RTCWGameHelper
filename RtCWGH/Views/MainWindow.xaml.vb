Imports GalaSoft.MvvmLight.Helpers
Imports GalaSoft.MvvmLight.Messaging
Imports Microsoft.Win32
Public Class MainWindow

    Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Me.CheatPage.DataContext = New CheatPageViewModel()

        'Open File Dialog
        Messenger.Default.Register(Of NotificationMessage)(Me, "msg", Sub(M)
                                                                          MessageBox.Show(M.Notification(0), "RTCW Game Helper")
                                                                      End Sub)
    End Sub


End Class
