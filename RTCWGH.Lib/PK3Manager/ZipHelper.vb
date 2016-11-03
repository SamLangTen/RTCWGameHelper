Imports ICSharpCode.SharpZipLib.Zip
Imports System.IO
Imports System.Text.RegularExpressions

Namespace Game
    ''' <summary>
    ''' Used to decompress pk3 package
    ''' </summary>
    Public Class ZipHelper
        ''' <summary>
        ''' Decompress some data in memory
        ''' </summary>
        ''' <param name="Data">Data in memory</param>
        ''' <param name="Password">Password of data</param>
        ''' <param name="Selector">some regex used to select files</param>
        Public Shared Function Decompress(Data() As Byte, Password As String, Optional ByRef Selector As List(Of String) = Nothing) As List(Of ZipData)
            Dim zipReader As New ZipInputStream(New MemoryStream(Data))
            Dim rtnZipDataList As New List(Of ZipData)
            Dim entry As ZipEntry = zipReader.GetNextEntry()
            While (entry) IsNot Nothing
                'Check if the file can be decompressed
                If Selector Is Nothing Then
                    entry = zipReader.GetNextEntry()
                    Continue While
                Else
                    Dim list = (From i In Selector Select New Regex(i).IsMatch(entry.Name))
                    If list.Where(Function(r) r = True).Count() = 0 Then
                        entry = zipReader.GetNextEntry()
                        Continue While
                    End If
                End If
                'Decompress the file
                Dim zd As New ZipData
                zd.Filename = entry.Name
                'Read
                Dim memoryWriter As New MemoryStream()
                Dim size As Integer = 2048
                Dim readData As Byte() = New Byte(2047) {}
                zipReader.Password = Password
                While True
                    size = zipReader.Read(Data, 0, Data.Length)
                    If size > 0 Then memoryWriter.Write(readData, 0, size) Else Exit While
                End While
                Dim buffer(memoryWriter.Length - 1) As Byte
                memoryWriter.Position = 0
                memoryWriter.Read(buffer, 0, memoryWriter.Length)
                zd.Data = buffer
                rtnZipDataList.Add(zd)
                'Read next entry
                entry = zipReader.GetNextEntry()
            End While
            Return rtnZipDataList
        End Function

        ''' <summary>
        ''' Decompress a filename into memory
        ''' </summary>
        ''' <param name="Filename">Filename</param>
        ''' <param name="Password">Password of data</param>
        ''' <param name="Selector">some regex used to select files</param>
        Public Shared Function Decompress(Filename As String, Password As String, Optional ByRef Selector As List(Of String) = Nothing) As List(Of ZipData)
            Dim sr As New FileStream(Filename, FileMode.Open)
            Dim data(sr.Length - 1) As Byte
            Dim reader As New BinaryReader(sr)
            reader.Read(data, 0, data.Length)
            Return ZipHelper.Decompress(data, Password, Selector)
        End Function

        ''' <summary>
        ''' Open and get file list of a zip file
        ''' </summary>
        ''' <param name="Data">data in memory</param>
        Public Shared Function ViewZipFile(Data() As Byte) As List(Of String)
            Dim zipReader As New ZipInputStream(New MemoryStream(Data))
            Dim filenames As New List(Of String)
            Dim entry As ZipEntry = zipReader.GetNextEntry()
            While (entry) IsNot Nothing
                filenames.Add(entry.Name)
                entry = zipReader.GetNextEntry()
            End While
            Return filenames
        End Function

        ''' <summary>
        ''' Open and get file list of a zip file
        ''' </summary>
        ''' <param name="Filename">Filename</param>
        ''' <returns>each name of file in zip package</returns>
        Public Shared Function ViewZipFile(Filename As String) As List(Of String)
            Dim sr As New FileStream(Filename, FileMode.Open)
            Dim data(sr.Length - 1) As Byte
            Dim reader As New BinaryReader(sr)
            reader.Read(data, 0, data.Length)
            Return ZipHelper.ViewZipFile(data)
        End Function
    End Class
    ''' <summary>
    ''' A file in zip file
    ''' </summary>
    Public Class ZipData
        Public Property Filename As String
        Public Property Data As Byte()
    End Class
End Namespace
