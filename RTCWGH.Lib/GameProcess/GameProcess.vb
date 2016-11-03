Imports System.Threading.Tasks
Namespace Game
    ''' <summary>
    ''' A Class used to set the args and run the game
    ''' </summary>
    Public Class GameProcess

        ''' <summary>
        ''' Means the arguments of the game.
        ''' </summary>
        Public Shared ReadOnly Property Arguments As GameArgument()
            Get
                Dim raF = From i In GameProcess.RealArgumentsFromFirst
                Dim raA = From i In GameProcess.RealArgumentsAnywhere
                Dim raL = From i In GameProcess.RealArgumentsFromLast
                raL.Reverse()
                Return raF.Union(raA).Union(raL)
            End Get
        End Property

        ''' <summary>
        ''' Add an argument to ArgumentList
        ''' </summary>
        Public Shared Sub AddArgument(arg As GameArgument)
            Dim realArguments
            Select Case arg.OrderType
                Case ArgumentOrder.Anywhere
                    realArguments = GameProcess.RealArgumentsAnywhere
                Case ArgumentOrder.FromFirst
                    realArguments = GameProcess.RealArgumentsFromFirst
                Case ArgumentOrder.FromLast
                    realArguments = GameProcess.RealArgumentsFromLast
                Case Else
                    realArguments = GameProcess.RealArgumentsAnywhere
            End Select
            If arg.Order > realArguments.Count Then realArguments.Add(arg) Else realArguments.Insert(arg.Order - 1, arg)
            SortArguments()

        End Sub

        ''' <summary>
        ''' Remove an argument from ArgumentList
        ''' </summary>
        Public Shared Sub DeleteArgument(arg As GameArgument)
            If RealArgumentsAnywhere.SingleOrDefault(Function(r) r.ArgumentString = arg.ArgumentString) IsNot Nothing Then
                RealArgumentsAnywhere.Remove(RealArgumentsAnywhere.SingleOrDefault(Function(r) r.ArgumentString = arg.ArgumentString))
                Exit Sub
            End If
            If RealArgumentsFromFirst.SingleOrDefault(Function(r) r.ArgumentString = arg.ArgumentString) IsNot Nothing Then
                RealArgumentsFromFirst.Remove(RealArgumentsFromFirst.SingleOrDefault(Function(r) r.ArgumentString = arg.ArgumentString))
                Exit Sub
            End If
            If RealArgumentsFromLast.SingleOrDefault(Function(r) r.ArgumentString = arg.ArgumentString) IsNot Nothing Then
                RealArgumentsFromLast.Remove(RealArgumentsFromLast.SingleOrDefault(Function(r) r.ArgumentString = arg.ArgumentString))
                Exit Sub
            End If
            SortArguments()
        End Sub

        ''' <summary>
        ''' Remove an argument from ArgumentList
        ''' </summary>
        Public Shared Sub DeleteArgument(argString As String)
            If RealArgumentsAnywhere.SingleOrDefault(Function(r) r.ArgumentString = argString) IsNot Nothing Then
                RealArgumentsAnywhere.Remove(RealArgumentsAnywhere.SingleOrDefault(Function(r) r.ArgumentString = argString))
                Exit Sub
            End If
            If RealArgumentsFromFirst.SingleOrDefault(Function(r) r.ArgumentString = argString) IsNot Nothing Then
                RealArgumentsFromFirst.Remove(RealArgumentsFromFirst.SingleOrDefault(Function(r) r.ArgumentString = argString))
                Exit Sub
            End If
            If RealArgumentsFromLast.SingleOrDefault(Function(r) r.ArgumentString = argString) IsNot Nothing Then
                RealArgumentsFromLast.Remove(RealArgumentsFromLast.SingleOrDefault(Function(r) r.ArgumentString = argString))
                Exit Sub
            End If
            SortArguments()
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
        ''' Sort all arguments order by their settings.
        ''' </summary>
        Private Shared Sub SortArguments()
            For i As Integer = 1 To GameProcess.RealArgumentsAnywhere.Count Step 1
                GameProcess.RealArgumentsAnywhere(i - 1).Order = i
            Next
            For i As Integer = 1 To GameProcess.RealArgumentsFromFirst.Count Step 1
                GameProcess.RealArgumentsFromFirst(i - 1).Order = i
            Next
            For i As Integer = 1 To GameProcess.RealArgumentsFromLast.Count Step 1
                GameProcess.RealArgumentsFromLast(i - 1).Order = i
            Next
        End Sub

        ''' <summary>
        ''' The storage of arguments(From First)
        ''' </summary>
        Private Shared Property RealArgumentsFromFirst As New List(Of GameArgument)
        ''' <summary>
        ''' The storage of arguments(Anywhere)
        ''' </summary>
        Private Shared Property RealArgumentsAnywhere As New List(Of GameArgument)
        ''' <summary>
        ''' The storage of arguments(From Last)
        ''' </summary>
        Private Shared Property RealArgumentsFromLast As New List(Of GameArgument)

        ''' <summary>
        ''' How the arguments will be used.
        ''' </summary>
        Private Shared Property GameMode As GameRunningMode = GameRunningMode.PermanentArgs + GameRunningMode.TemporaryArgs

        ''' <summary>
        ''' Background Method of game running.
        ''' </summary>
        Private Shared Sub RunGameBackground()
            Dim p As New Process()
            p.StartInfo.FileName = RTCWPath + "/wolfsp.exe"
            Dim aF As String = String.Join(" ", GameProcess.RealArgumentsFromFirst.Where(Function(k) GameMode.HasFlag(k.ArgType)).Select(Function(k) k.ArgumentString))
            Dim aA As String = String.Join(" ", GameProcess.RealArgumentsAnywhere.Where(Function(k) GameMode.HasFlag(k.ArgType)).Select(Function(k) k.ArgumentString))
            Dim aL As String = String.Join(" ", GameProcess.RealArgumentsFromLast.Where(Function(k) GameMode.HasFlag(k.ArgType)).Select(Function(k) k.ArgumentString).Reverse())
            p.StartInfo.Arguments = $"{aF} {aA} {aL}"
            p.StartInfo.UseShellExecute = False
            p.StartInfo.WorkingDirectory = RTCWPath
            RaiseEvent BeforeGameRun()
            p.Start()
            RaiseEvent GameHasRun()
            p.WaitForExit()
            GameProcess.RealArgumentsFromFirst = GameProcess.RealArgumentsFromFirst.Where(Function(r) r.ArgType = ArgumentType.Permanent).ToList()
            GameProcess.RealArgumentsAnywhere = GameProcess.RealArgumentsAnywhere.Where(Function(r) r.ArgType = ArgumentType.Permanent).ToList()
            GameProcess.RealArgumentsFromLast = GameProcess.RealArgumentsFromLast.Where(Function(r) r.ArgType = ArgumentType.Permanent).ToList()
            SortArguments()
            RaiseEvent GameHasExited()
        End Sub
    End Class

    Public Enum GameRunningMode
        PermanentArgs = 1
        TemporaryArgs = 2
        NoArgs = 4
    End Enum
End Namespace