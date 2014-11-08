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
        Messenger.Default.Register(Of CommonDialogMessage(Of OpenFileDialogDto))(Me, Sub(m)
                                                                                         Dim open As New OpenFileDialog
                                                                                         open.Filter = m.Content.Filter
                                                                                         open.Title = m.Content.Title
                                                                                         If open.ShowDialog().Value = True Then
                                                                                             m.Content.Filename = open.FileName
                                                                                             m.Content.DialogResult = True
                                                                                         Else
                                                                                             m.Content.DialogResult = False
                                                                                         End If
                                                                                         m.ProcessCallback(m.Content)
                                                                                     End Sub)
    End Sub


End Class
