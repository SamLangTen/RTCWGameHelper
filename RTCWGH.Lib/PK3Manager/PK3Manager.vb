Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Text
Namespace Game
    ''' <summary>
    ''' PK3 manager for RTCW
    ''' </summary>
    Public Class PK3Manager
        ''' <summary>
        ''' RTCW Offcial PK3 Files(In main folder)
        ''' </summary>
        Public Shared ReadOnly Property OffcialPK3Names As String()
            Get
                Return {"mp_bin.pk3",
                "mp_pak0.pk3",
                "mp_pak1.pk3",
                "mp_pak2.pk3",
                "mp_pak3.pk3",
                "mp_pak4.pk3",
                "mp_pak5.pk3",
                "mp_pakmaps0.pk3",
                "mp_pakmaps1.pk3",
                "mp_pakmaps2.pk3",
                "mp_pakmaps3.pk3",
                "mp_pakmaps4.pk3",
                "mp_pakmaps5.pk3",
                "mp_pakmaps6.pk3",
                "pak0.pk3",
                "rtw_pak0.pk3",
                "sp_pak1.pk3",
                "sp_pak2.pk3",
                "sp_pak3.pk3",
                "sp_pak4.pk3"}
            End Get
        End Property

        ''' <summary>
        ''' Load map info in pk3
        ''' </summary>
        ''' <param name="PK3Filename">PK3 Filename</param>
        ''' <param name="IsSort">If sort maps by playing order.</param>
        Public Shared Function LoadPK3Maps(PK3Filename As String, IsSort As Boolean) As MapPK3File
            'Get bsp file
            Dim bspCollection = ZipHelper.ViewZipFile(PK3Filename).Where(Function(f) Path.GetExtension(f).ToLower = ".bsp")
            'Get ui file
            Dim uiCollection = ZipHelper.ViewZipFile(PK3Filename).Where(Function(f) Path.GetExtension(f).ToLower = ".menu").Select(Function(f) Path.GetFileNameWithoutExtension(f))
            Dim pk3File As New MapPK3File() With {.PK3Filename = PK3Filename,
            .Maps = bspCollection.Select(Function(i) New RTCWMapInfo() With {.MapName = Path.GetFileNameWithoutExtension(i), .PK3Filename = PK3Filename}),
            .CustomMenus = uiCollection
            }
            'Sort
            If IsSort = True Then
                Dim bspFileCollection = ZipHelper.Decompress(pk3File.PK3Filename, "", ({".*\.bsp"}).ToList())
                Dim bspMapCollection = bspFileCollection.Select(Function(z) New bspSortingHelper() With {.CurrentBsp = Path.GetFileNameWithoutExtension(z.Filename), .NextBsp = New Regex("(?<=changelevel\s)[^\s]*").Match(Encoding.ASCII.GetChars(z.Data)).Value})
                Dim sortedMaps As New List(Of RTCWMapInfo)
                While True
                    Dim latestMap As bspSortingHelper
                    'sort end by end
                    If (sortedMaps.Count = 0) Then
                        'The last map must link to "gamefinished"
                        'Or link to a map which don't belong to this pk3
                        'e.g.: Mod "Fast_NewPharao" will link to RTCW XLab after you finished the last map

                        'Particularly,there are more than one map which links to gamefinished,but one of them won't be loaded by other maps
                        'This complicated code aims to solve problems above.
                        latestMap = bspMapCollection.Where(Function(m) (m.NextBsp.ToLower = "gamefinished" Or bspMapCollection.Where(Function(n) n.CurrentBsp = m.NextBsp).Count = 0) And ((bspMapCollection.Where(Function(n) n.NextBsp = m.CurrentBsp).Count <> 0) Or （bspMapCollection.Count = 1))).SingleOrDefault()
                    Else
                        latestMap = bspMapCollection.Where(Function(m) m.NextBsp.ToLower = sortedMaps(0).MapName.ToLower).SingleOrDefault()
                    End If
                    If latestMap Is Nothing Then Exit While
                    sortedMaps.Insert(0, pk3File.Maps.Where(Function(m) m.MapName = latestMap.CurrentBsp).SingleOrDefault)
                End While
                pk3File.Maps = sortedMaps
            End If
            Return pk3File
        End Function

        ''' <summary>
        ''' Search and load all map info of all mod pk3
        ''' </summary>
        ''' <param name="IsSort">If sort maps by playing order.</param>
        Public Shared Function GetAllMapPK3FilesInMain(IsSort As Boolean) As List(Of MapPK3File)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Search and load all resource pk3 in game path
        ''' </summary>
        Public Shared Function GetAllResourcePK3Files() As List(Of ResourcePK3File)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Used to storage data while sorting
        ''' </summary>
        Private Class bspSortingHelper
            Property CurrentBsp As String
            Property NextBsp As String
        End Class

    End Class
End Namespace