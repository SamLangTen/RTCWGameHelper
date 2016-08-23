''' <summary>
''' Help to divide ViewModel and View
''' </summary>
Public Class DialogInteraction

    Public Shared Property OpenFileDialogFunc As OpenFileDialogDelegate

    Public Shared Function ShowOpenFileDialog(Filter As String) As OpenFileDialogReturnInfo
        Return OpenFileDialogFunc?(Filter)
    End Function

End Class

