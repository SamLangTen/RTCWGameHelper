Imports System.ComponentModel
Imports System.IO
Imports GalaSoft.MvvmLight.Command
Imports GalaSoft.MvvmLight.Messaging
Public Class KeyBindingEditorViewModel
    Implements INotifyPropertyChanged

    Sub New(CommandList As List(Of String))
        Me.CommandLists = CommandList
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Private _CommandLists As New List(Of String)
    Public Property CommandLists As List(Of String)
        Set(value As List(Of String))
            _CommandLists = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("CommandLists"))
        End Set
        Get
            Return _CommandLists
        End Get
    End Property
    Public ReadOnly Property KeyLists As List(Of String)
        Get
            Return [Enum].GetNames(GetType(Key)).ToList()
        End Get
    End Property


    Public Property SelectedCommand As String

    Public Property SelectedKey As String

End Class
