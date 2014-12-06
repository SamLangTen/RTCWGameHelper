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
        Public Property PK3Filename As String
        Public Property Maps As New List(Of RTCWMapInfo)
    End Class

End Namespace