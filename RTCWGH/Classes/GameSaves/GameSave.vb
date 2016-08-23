Imports System.IO
Imports System.Text
Namespace Game

    ''' <summary>
    ''' A game save in RTCW
    ''' </summary>
    Public Class GameSave

        'A rtcw game save will contain health,time and map name
        'map name starts at offset 5
        'other info starts at offset 85 
        Private Sub ReadSVG(Filename As String)
            'Get map name
            Dim r1 As New StreamReader(File.OpenRead(Filename))
            Dim c As String = ""
            Dim mapname As String = ""
            r1.Read(c, 4, 1)
            While (c <> Encoding.ASCII.GetChars({0}))
                mapname += c
                r1.Read(c, 0, 1)
            End While
            Me.MapName = mapname
            'Get other info
            c = ""
            Dim health As String = ""
            Dim time As String = ""
            Dim buffer As String = ""
            r1.Read(c, 92, 1)
            While (c <> Encoding.ASCII.GetChars({0}))
                buffer += c
                r1.Read(c, 0, 1)
            End While
            r1.Close()
            'Split
            time = buffer.Split(Encoding.ASCII.GetChars({10}))(0)
            health = buffer.Split(Encoding.ASCII.GetChars({10}))(1).Substring(7).Trim()
            Me.Health = Integer.Parse(health)
            Me.Time = New TimeSpan(Integer.Parse(time.Split("h")(0)), Integer.Parse(time.Split("h")(1).Split("m")(0)), Integer.Parse(time.Split("s")(1)))
        End Sub

        Sub New()
        End Sub
        ''' <summary>
        ''' Create an instance with a svg file
        ''' </summary>
        ''' <param name="Filename">svg file</param>
        Sub New(Filename As String)
            ReadSVG(Filename)
        End Sub
        Public Property MapName As String
        Public Property Health As Int32
        Public Property Time As TimeSpan

    End Class

End Namespace

