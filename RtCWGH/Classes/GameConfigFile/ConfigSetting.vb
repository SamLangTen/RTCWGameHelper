''' <summary>
''' A Key Binding in RtCW Config File.
''' </summary>
Public Class RtCWConfigBind

    Public Property Key As String

    Public Property Command As String

    Sub New(Line As String)
        If Line.Contains("bind") = True Then
            Dim t = Line.Split(" ").ToList()
            Me.Key = t(1)
            t.RemoveRange(0, 2)
            Me.Command = String.Join(" ", t).Trim("""")
        End If
    End Sub

    Sub New(Key As String, Command As String)
        Me.Key = Key
        Me.Command = Command
    End Sub

    Sub New()

    End Sub

    Public Overrides Function ToString() As String
        Return "bind " + Me.Key + " """ + Me.Command + """"
    End Function
End Class

''' <summary>
''' A Setting in RtCW Config File.
''' </summary>
Public Class RtCWConfigSeta

    Public Property Key As String

    Public Property Value As String

    Sub New(Line As String)
        If Line.Contains("seta") = True Then
            Dim t = Line.Split(" ").ToList()
            Me.Key = t(1)
            t.RemoveRange(0, 2)
            Me.Value = String.Join(" ", t).Trim("""")
        End If
    End Sub

    Sub New(Key As String, Value As String)
        Me.Key = Key
        Me.Value = Value
    End Sub

    Sub New()

    End Sub

    Public Overrides Function ToString() As String
        Return "seta " + Me.Key + " """ + Me.Value + """"
    End Function

End Class