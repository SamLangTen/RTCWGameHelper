Public Class KeyBindingEditor
    Sub New(CommandList As List(Of String))

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Me.DataContext = New KeyBindingEditorViewModel(CommandList)
    End Sub

    Public ReadOnly Property Command As String
        Get
            Return DirectCast(Me.DataContext, KeyBindingEditorViewModel).SelectedCommand
        End Get
    End Property

    Public ReadOnly Property Key As String
        Get
            Return DirectCast(Me.DataContext, KeyBindingEditorViewModel).SelectedKey
        End Get
    End Property

    Private Sub OK_Click(sender As Object, e As RoutedEventArgs) Handles OK.Click
        Me.DialogResult = True
    End Sub

    Private Sub Cancel_Click(sender As Object, e As RoutedEventArgs) Handles Cancel.Click
        Me.DialogResult = False
    End Sub
End Class
