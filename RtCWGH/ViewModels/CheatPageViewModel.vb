Imports System.ComponentModel
Imports System.IO
Imports GalaSoft.MvvmLight.Command
Imports GalaSoft.MvvmLight.Messaging
Public Class CheatPageViewModel
    Implements INotifyPropertyChanged



    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Private rtcwConfigFile As String = GameProcess.RTCWPath + "\main\wolfconfig.cfg"
    Private _IsActivatedCheatMode As Boolean

    Public Property IsActivatedCheatMode As Boolean
        Set(value As Boolean)
            Me._IsActivatedCheatMode = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("IsActivatedCheatMode"))
        End Set
        Get
            Return _IsActivatedCheatMode
        End Get
    End Property

    Public Property SetMainCFG As ICommand = New RelayCommand(Sub()
                                                                  Me.rtcwConfigFile = GameProcess.RTCWPath + "\main\wolfconfig.cfg"
                                                                  RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SelectMainCFG"))
                                                              End Sub)

    Public Property SetOtherCFG As ICommand = New RelayCommand(Sub()
                                                                   Messenger.Default.Send(Of CommonDialogMessage(Of OpenFileDialogDto))(New CommonDialogMessage(Of OpenFileDialogDto)(New OpenFileDialogDto() With {.Filter = "RTCW配置文件(wolfconfig.cfg)|wolfconfig.cfg"}, Sub(m)
                                                                                                                                                                                                                                                                              If m.DialogResult = True Then Me.rtcwConfigFile = m.Filename
                                                                                                                                                                                                                                                                          End Sub))
                                                                   RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SelectOtherCFG"))
                                                               End Sub)

    Public Property SelectMainCFG As Boolean
        Set(value As Boolean)

        End Set
        Get
            Return New FileInfo(rtcwConfigFile).FullName = New FileInfo(GameProcess.RTCWPath + "\main\wolfconfig.cfg").FullName
        End Get
    End Property

    Public Property SelectOtherCFG As Boolean
        Set(value As Boolean)

        End Set
        Get
            Return New FileInfo(rtcwConfigFile).FullName <> New FileInfo(GameProcess.RTCWPath + "\main\wolfconfig.cfg").FullName
        End Get
    End Property

End Class
