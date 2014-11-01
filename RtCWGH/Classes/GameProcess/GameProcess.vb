Imports System.Threading.Tasks

''' <summary>
''' A Class used to set the args and run the game
''' </summary>
Public Class GameProcess

    ''' <summary>
    ''' Means the arguments of the game.
    ''' </summary>
    Public Shared ReadOnly Property Arguments As GameArgument()
        Get
            Return RealArguments.ToArray()
        End Get
    End Property

    Public Shared Sub AddArgument(arg As GameArgument)
        Select Case arg.OrderType
            Case ArgumentOrder.Anywhere
                'If order type of arg is anywhere,it will be inserted into the list and its location is between FromFirst and FromLast
                Dim i As Integer
                If RealArguments.SingleOrDefault(Function(r) r.OrderType = ArgumentOrder.FromFirst) IsNot Nothing Then
                    i = RealArguments.LastIndexOf(RealArguments.LastOrDefault(Function(r) r.OrderType = ArgumentOrder.FromFirst))
                Else
                    i = 0
                End If
                RealArguments.Insert(i + 1, arg)
            Case ArgumentOrder.FromFirst
                'First,we have to check out whether there are more than "order" items.
                'If its count is more than "order" item,we must insert it into list as the "order" item.
                'If not,we insert it into the list as the last one of the FromFirst
                Dim totalArg = RealArguments.Where(Function(r) r.OrderType = ArgumentOrder.FromFirst)
                Dim last As Integer = RealArguments.LastIndexOf(RealArguments.LastOrDefault(Function(r) r.OrderType = ArgumentOrder.FromFirst))
                If totalArg.Count < arg.Order Then
                    arg.Order = totalArg.Count + 1
                    RealArguments.Insert(last + 1, arg)
                Else
                    RealArguments.Insert(arg.Order - 1, arg)
                    For i As Integer = arg.Order To last Step 1
                        RealArguments(i).Order = i + 1
                    Next
                End If
            Case ArgumentOrder.FromLast
                'Similar with the previous
                Dim totalArg = RealArguments.Where(Function(r) r.OrderType = ArgumentOrder.FromLast)
                Dim first As Integer = RealArguments.IndexOf(RealArguments.FirstOrDefault(Function(r) r.OrderType = ArgumentOrder.FromLast))
                If totalArg.Count < arg.Order Then
                    arg.Order = totalArg.Count + 1
                    RealArguments.Insert(first - 1, arg)
                Else
                    RealArguments.Insert(RealArguments.Count - arg.Order + 1, arg)
                    For i As Integer = first To RealArguments.Count - arg.Order Step 1
                        RealArguments(i).Order = totalArg.Count() - (i - (RealArguments.Count() - totalArg.Count()))
                    Next
                End If
        End Select
    End Sub

    Public Shared Sub DeleteArgument(arg As GameArgument)

    End Sub

    Public Shared Sub DeleteArgument(argString As String)

    End Sub

    ''' <summary>
    ''' The Path of RTCW,not main folder.
    ''' </summary>
    Public Shared Property RTCWPath As String

    ''' <summary>
    ''' Some flags about this version of RTCW,such as (ani,noani)
    ''' </summary>
    Public Shared Property RTCWVersionFlags As New List(Of String)

    ''' <summary>
    ''' Run RTCW
    ''' </summary>
    ''' <param name="Mode">How the arguments will be used.</param>
    Public Shared Sub RunGame(Mode As GameRunningMode)
        GameMode = Mode
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

    ''' <summary>
    ''' The storage of arguments
    ''' </summary>
    Private Shared Property RealArguments As New List(Of GameArgument)

    ''' <summary>
    ''' How the arguments will be used.
    ''' </summary>
    Private Shared Property GameMode As GameRunningMode = GameRunningMode.BothPerAndTemp

    ''' <summary>
    ''' Background Method of game running.
    ''' </summary>
    Private Shared Sub RunGameBackground()

    End Sub
End Class

Public Enum GameRunningMode
    OnlyPermanentArgs
    OnlyTemporaryArgs
    BothPerAndTemp
    NoArgs
End Enum
