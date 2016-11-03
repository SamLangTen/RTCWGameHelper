Imports System.IO

Namespace Game

    ''' <summary>
    ''' Represent a series maps in a pk3 with independent save and setting folder
    ''' </summary>
    Public Class MissionPackage

        Public Property MapPK3s As New List(Of MapPK3File)
        Public Property CustomComponents As New List(Of String)
        Public Property Settings As GameConfigFile
        Public Property Saves As New List(Of GameSave)
    End Class

End Namespace