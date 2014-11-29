Imports System.ComponentModel
Imports System.IO
Imports GalaSoft.MvvmLight.Command
Imports GalaSoft.MvvmLight.Messaging
Imports Microsoft.Win32
Public Class CheatPageViewModel
    Implements INotifyPropertyChanged

    Sub New()
        rtcwCheatClass = New CheatClass(rtcwConfigFile)
        Raise("CheatCodeList")
    End Sub


    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
    ''' <summary>
    ''' Used Config File of RTCW
    ''' </summary>
    Private rtcwConfigFile As String = GameProcess.RTCWPath + "\main\wolfconfig.cfg"
    ''' <summary>
    ''' Cheat class contains cheatcode actions of config file
    ''' </summary>
    Private rtcwCheatClass As CheatClass

    Private Sub Raise(ParamArray PropertyNames As String())
        For Each item In PropertyNames
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(item))
        Next
    End Sub

#Region "Bindings"

    Private _IsActivatedCheatMode As Boolean
    ''' <summary>
    ''' Enable or disable cheat mode
    ''' </summary>
    Public Property IsActivatedCheatMode As Boolean
        Set(value As Boolean)
            Me._IsActivatedCheatMode = value
            Raise("IsActivatedCheatMode")
        End Set
        Get
            Return _IsActivatedCheatMode
        End Get
    End Property
    ''' <summary>
    ''' Set default cfg as config file
    ''' </summary>
    Public Property SetMainCFG As ICommand = New RelayCommand(Sub()
                                                                  Me.rtcwConfigFile = GameProcess.RTCWPath + "\main\wolfconfig.cfg"
                                                                  rtcwCheatClass = New CheatClass(rtcwConfigFile)
                                                                  Raise("IsSelectedMainCFG", "CheatCodeList")
                                                              End Sub)
    ''' <summary>
    ''' Set other cfg user selected as config file
    ''' </summary>
    Public Property SetOtherCFG As ICommand = New RelayCommand(Sub()
                                                                   Dim open As New OpenFileDialog
                                                                   open.Filter = "RTCW配置文件(wolfconfig.cfg)|wolfconfig.cfg"
                                                                   If open.ShowDialog().Value = True Then
                                                                       Me.rtcwConfigFile = open.FileName
                                                                       rtcwCheatClass = New CheatClass(rtcwConfigFile)
                                                                       Raise("IsSelectedOtherCFG", "CheatCodeList")
                                                                   End If
                                                               End Sub)
    ''' <summary>
    ''' If reading main cfg content
    ''' </summary>
    Public ReadOnly Property IsSelectedMainCFG As Boolean
        Get
            Return New FileInfo(rtcwConfigFile).FullName = New FileInfo(GameProcess.RTCWPath + "\main\wolfconfig.cfg").FullName
        End Get
    End Property

    ''' <summary>
    ''' If reading other cfg content
    ''' </summary>
    Public ReadOnly Property IsSelectedOtherCFG As Boolean
        Get
            Return New FileInfo(rtcwConfigFile).FullName <> New FileInfo(GameProcess.RTCWPath + "\main\wolfconfig.cfg").FullName
        End Get
    End Property


    Private _SelectedCheatCode As RtCWConfigBind
    ''' <summary>
    ''' Set or get user selected cheat code
    ''' </summary>
    Public Property SelectedCheatCode As RtCWConfigBind
        Set(value As RtCWConfigBind)
            _SelectedCheatCode = value
            Raise("SelectedCheatCode")
        End Set
        Get
            Return _SelectedCheatCode
        End Get
    End Property

    ''' <summary>
    ''' Get cheat codes
    ''' </summary>
    Public ReadOnly Property CheatCodeList As List(Of RtCWConfigBind)
        Get
            Return Me.rtcwCheatClass.CheatCodes.ToList()
        End Get
    End Property

    ''' <summary>
    ''' Add cheat code
    ''' </summary>
    Public Property AddCheatCode As ICommand = New RelayCommand(Sub()
                                                                    Dim add As New KeyBindingEditor(CheatClass.CommonCheatCodes.ToList())
                                                                    If add.ShowDialog().Value = True Then
                                                                        Me.rtcwCheatClass.AddCode(add.Command, add.Key)
                                                                    End If
                                                                    Raise("CheatCodeList")
                                                                End Sub)
    ''' <summary>
    ''' Delete cheat code
    ''' </summary>
    Public Property DeleteCheatCode As ICommand = New RelayCommand(Sub()
                                                                       If SelectedCheatCode IsNot Nothing Then Me.rtcwCheatClass.DeleteCode(SelectedCheatCode.Command)
                                                                       Raise("CheatCodeList")
                                                                   End Sub)
    ''' <summary>
    ''' Remove all cheat code
    ''' </summary>
    Public Property RemoveAllCheatCode As ICommand = New RelayCommand(Sub()
                                                                          Dim cl = Me.rtcwCheatClass.CheatCodes
                                                                          For Each item In cl
                                                                              Me.rtcwCheatClass.DeleteCode(item.Command)
                                                                          Next
                                                                          Raise("CheatCodeList")
                                                                      End Sub)
    ''' <summary>
    ''' apply cheat code
    ''' </summary>
    Public Property ApplyCheatCode As ICommand = New RelayCommand(Sub()
                                                                      Try
                                                                          Me.rtcwCheatClass.Save()
                                                                      Catch ex As Exception
                                                                          Messenger.Default.Send(New NotificationMessage(My.Resources.Msg_Error + ex.Message), "msg")
                                                                      End Try
                                                                      Messenger.Default.Send(New NotificationMessage(My.Resources.Msg_Done), "msg")
                                                                      Raise("CheatCodeList")
                                                                  End Sub)
#End Region
End Class
