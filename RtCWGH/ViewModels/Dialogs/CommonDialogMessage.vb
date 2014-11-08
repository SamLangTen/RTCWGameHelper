Imports GalaSoft.MvvmLight.Messaging

Public Class CommonDialogMessage(Of T)
    Inherits GenericMessage(Of T)

    Sub New(content As T, callback As Action(Of T))
        MyBase.New(content)
        Me.Callback = callback
    End Sub

    Private Property Callback() As Action(Of T)

    Public Sub ProcessCallback(returnValue As T)
        If Me.Callback IsNot Nothing Then Me.Callback.Invoke(returnValue)
    End Sub
End Class
