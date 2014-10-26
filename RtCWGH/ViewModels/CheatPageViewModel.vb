Imports System.ComponentModel

Public Class CheatPageViewModel
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
    Private _IsActivatedCheatMod As Boolean
    Public Property IsActivatedCheatMode As Boolean

End Class
