''' <summary>
''' Help to divide ViewModel and View
''' </summary>
Public Class DialogInteraction

    Public Shared Property OpenFileDialogFunc As OpenFileDialogDelegate

    Public Shared Function ShowOpenFileDialog(Filter As String) As OpenFileDialogReturnInfo
        If Not DialogInteraction.OpenFileDialogFunc Is Nothing Then Return DialogInteraction.OpenFileDialogFunc(Filter) Else Return Nothing
    End Function

End Class

