Namespace Game
    ''' <summary>
    ''' A map(.bsp file) of RTCW in pk3
    ''' </summary>
    Public Class RTCWMapInfo
        Public Property MapName As String
        Public Property PK3Filename As String
    End Class
    ''' <summary>
    ''' A pk3 containing bsp file
    ''' </summary>
    Public Class MapPK3File
        Inherits PK3File
        Public Property Maps As New List(Of RTCWMapInfo)
        Public Property CustomMenus As New List(Of String)
    End Class

    ''' <summary>
    ''' A pk3 containing resource files
    ''' </summary>
    Public Class ResourcePK3File
        Inherits PK3File

        Public Property Type As ResourceType
    End Class

    ''' <summary>
    ''' A pk3 file
    ''' </summary>
    Public Class PK3File
        Public Property PK3Filename As String
    End Class

    ''' <summary>
    ''' Represent resource type in pk3
    ''' </summary>
    Public Enum ResourceType
        Model
        Sound
        Texture
    End Enum
End Namespace