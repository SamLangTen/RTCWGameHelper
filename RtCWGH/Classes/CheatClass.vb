''' <summary>
''' A Class used to deal with cheat.
''' </summary>
Public Class CheatClass

    Private _Filename As String

    Private gcf As GameConfigFile

    ''' <summary>
    ''' Create a new cheat class
    ''' </summary>
    ''' <param name="Filename">RTCW game config file(wolfconfig.cfg)</param>
    Sub New(Filename As String)
        Me._Filename = Filename
        Me.gcf = New GameConfigFile(_Filename)
    End Sub

    ''' <summary>
    ''' Get common cheat codes
    ''' </summary>
    Public Shared ReadOnly Property CommonCheatCodes As String()
        Get
            Return {"god", "noclip", "notarget", "give all", "nofatigue"}
        End Get
    End Property

    ''' <summary>
    ''' Get all cheat code bindings in config file
    ''' </summary>
    Public ReadOnly Property CheatCodes As RtCWConfigBind()
        Get
            Return gcf.ConfigBind.Where(Function(r) CheatClass.CommonCheatCodes.ToList().Contains(r.Command)).ToArray()
        End Get
    End Property

    ''' <summary>
    ''' Add cheat code binding
    ''' </summary>
    ''' <param name="code">Cheat Code</param>
    ''' <param name="key">Key</param>
    Public Sub AddCode(code As String, key As String)
        gcf.AddBinding(code, key)
    End Sub

    ''' <summary>
    ''' Delete cheat code binding
    ''' </summary>
    ''' <param name="code">Cheat Code</param>
    Public Sub DeleteCode(code As String)
        gcf.DeleteBinding(code)
    End Sub

    ''' <summary>
    ''' Update cheat code binding
    ''' </summary>
    ''' <param name="code">Cheat Code</param>
    ''' <param name="NewKey">Key</param>
    Public Sub UpdateCode(code As String, NewKey As String)
        gcf.ModifyBinding(code, NewKey)
    End Sub

    ''' <summary>
    ''' Save all settings.
    ''' </summary>
    Public Sub Save()
        gcf.SaveConfigFile()
    End Sub
End Class