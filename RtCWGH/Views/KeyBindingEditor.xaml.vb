Public Class KeyBindingEditor
    Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Me.DataContext = New KeyBindingEditorViewModel
    End Sub
End Class
