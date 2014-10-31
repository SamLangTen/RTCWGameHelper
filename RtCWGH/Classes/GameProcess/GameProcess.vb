Imports System.Threading.Tasks

''' <summary>
''' A Class used to set the args and run the game
''' </summary>
Public Class GameProcess

    ''' <summary>
    ''' Means the arguments of the game.
    ''' </summary>
    Public Shared Property Arguments As New List(Of GameArgument)

    ''' <summary>
    ''' The Path of RTCW,not main folder.
    ''' </summary>
    Public Shared Property RTCWPath As String

    ''' <summary>
    ''' Some flags about this version of RTCW,such as (ani,noani)
    ''' </summary>
    Public Shared Property RTCWVersionFlags As New List(Of String)

    Public Shared Sub RunGame()
        Dim t As New task(AddressOf RunGameBackground)
        t.Start()
    End Sub

    ''' <summary>
    ''' When the game has run.
    ''' </summary>
    Public Shared Event GameHasRun()

    ''' <summary>
    ''' Before the game run.
    ''' </summary>
    Public Shared Event BeforeGameRun()

    ''' <summary>
    ''' When the gamme has exited.
    ''' </summary>
    Public Shared Event GameHasExited()

    Private Shared Sub RunGameBackground()

    End Sub
End Class

