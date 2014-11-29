Imports System.ComponentModel
Imports System.IO
Imports GalaSoft.MvvmLight.Command
Imports GalaSoft.MvvmLight.Messaging
Imports Microsoft.Win32
Public Class CheatPageViewModel
    Implements INotifyPropertyChanged

    Sub New()
        ReadConfigFile()
    End Sub


    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Private rtcwConfigFile As String = GameProcess.RTCWPath + "\main\wolfconfig.cfg"
    Private rtcwCheatClass As CheatClass
    Private _IsActivatedCheatMode As Boolean

    Private Sub ReadConfigFile()
        rtcwCheatClass = New CheatClass(rtcwConfigFile)
    End Sub

#Region "Bindings"

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
                                                                  ReadConfigFile()
                                                                  RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SelectMainCFG"))
                                                              End Sub)

    Public Property SetOtherCFG As ICommand = New RelayCommand(Sub()
                                                                   Dim open As New OpenFileDialog
                                                                   open.Filter = "RTCW配置文件(wolfconfig.cfg)|wolfconfig.cfg"
                                                                   If open.ShowDialog().Value = True Then
                                                                       Me.rtcwConfigFile = open.FileName
                                                                       ReadConfigFile()
                                                                       RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SelectOtherCFG"))
                                                                   End If
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

    Private _SelectedCheatCode As RtCWConfigBind
    Public Property SelectedCheatCode As RtCWConfigBind
        Set(value As RtCWConfigBind)
            _SelectedCheatCode = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SelectedCheatCode"))
        End Set
        Get
            Return _SelectedCheatCode
        End Get
    End Property

    Public ReadOnly Property CheatCodeList As List(Of RtCWConfigBind)
        Get
            Return Me.rtcwCheatClass.CheatCodes.ToList()
        End Get
    End Property

    Public Property AddCheatCode As ICommand = New RelayCommand(Sub()
                                                                    Dim add As New KeyBindingEditor(CheatClass.CommonCheatCodes.ToList())
                                                                    If add.ShowDialog().Value = True Then
                                                                        Me.rtcwCheatClass.AddCode(add.Command, add.Key)
                                                                    End If
                                                                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("CheatCodeList"))
                                                                End Sub)
    Public Property DeleteCheatCode As ICommand = New RelayCommand(Sub()
                                                                       If SelectedCheatCode IsNot Nothing Then Me.rtcwCheatClass.DeleteCode(SelectedCheatCode.Command)
                                                                       RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("CheatCodeList"))
                                                                   End Sub)

    Public Property RemoveAllCheatCode As ICommand = New RelayCommand(Sub()
                                                                          Dim cl = Me.rtcwCheatClass.CheatCodes
                                                                          For Each item In cl
                                                                              Me.rtcwCheatClass.DeleteCode(item.Command)
                                                                          Next
                                                                          RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("CheatCodeList"))
                                                                      End Sub)
    Public Property ApplyCheatCode As ICommand = New RelayCommand(Sub()
                                                                      Try
                                                                          Me.rtcwCheatClass.Save()
                                                                      Catch ex As Exception
                                                                          Messenger.Default.Send(New NotificationMessage(My.Resources.Msg_Error + ex.Message), "msg")
                                                                      End Try
                                                                      Messenger.Default.Send(New NotificationMessage(My.Resources.Msg_Done), "msg")
                                                                      RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("CheatCodeList"))
                                                                  End Sub)
#End Region
End Class
