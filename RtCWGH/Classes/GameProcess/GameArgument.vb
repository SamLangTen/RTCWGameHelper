''' <summary>
''' Means an argument of the game
''' </summary>
Public Class GameArgument

    ''' <summary>
    ''' Create a new argument class.
    ''' </summary>
    ''' <param name="Type">If the argument can be used all the time.</param>
    ''' <param name="OrderType">How this class be sort</param>
    ''' <param name="Order">The order of it("1" maybe mean the last one or the first one.</param>
    ''' <param name="OriginalArgumentString">A common argument with string like "{0}"</param>
    ''' <param name="Args">some real args which will replace "{0}"</param>
    Sub New(Type As ArgumentType, OrderType As ArgumentOrder, Order As Integer, OriginalArgumentString As String, ParamArray Args As String())
        Me.Order = Order
        Me.OrderType = OrderType
        Me.ArgType = Type
        'Replace {0},{1}..... with real arguments
        Dim argText As String = OriginalArgumentString
        If Args IsNot Nothing Then
            For i As Integer = 0 To Args.Length Step 1
                argText = argText.Replace("{" + i.ToString() + "}", Args(i))
            Next
        End If
        Me.ArgumentString = argText
    End Sub

    ''' <summary>
    ''' The argument
    ''' </summary>
    Public Property ArgumentString As String

    ''' <summary>
    ''' The type of argument
    ''' </summary>
    Public Property ArgType As ArgumentType

    ''' <summary>
    ''' How the class be sort
    ''' </summary>
    Public Property OrderType As ArgumentOrder

    ''' <summary>
    ''' The order
    ''' </summary>
    Public Property Order As Integer

End Class
Public Enum ArgumentOrder
    FromFirst
    FromLast
    Anywhere
End Enum
Public Enum ArgumentType
    Temporary
    Permanent
End Enum