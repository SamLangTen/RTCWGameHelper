Imports System.ComponentModel

Public Class CheatPageViewModel
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
    Private _IsActivatedCheatMode As Boolean
    Public Property IsActivatedCheatMode As Boolean
        Set(value As Boolean)
            Me._IsActivatedCheatMode = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("IsActivatedCheatMode"))
        End Set
        Get
            Return _IsActivatedCheatMode
        End Get
    End Property

End Class
