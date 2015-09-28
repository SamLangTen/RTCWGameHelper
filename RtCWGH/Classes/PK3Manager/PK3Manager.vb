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
            Dim bspCollection = (From f In ZipHelper.ViewZipFile(PK3Filename) Where Path.GetExtension(f).ToLower = ".bsp")
            Dim pk3File As New MapPK3File() With {.PK3Filename = PK3Filename,
                                          .Maps = (From i In bspCollection Select New RTCWMapInfo With {.MapName = Path.GetFileNameWithoutExtension(i), .PK3Filename = PK3Filename})
            }
            'Sort
            If IsSort = True Then
                Dim bspFileCollection = ZipHelper.Decompress(pk3File.PK3Filename, "", ({".*\.bsp"}).ToList())
                Dim bspMapCollection = bspFileCollection.Select(Function(z) New bspSortingHelper() With {.CurrentBsp = Path.GetFileNameWithoutExtension(z.Filename), .NextBsp = New Regex("(?<=changelevel\s)[^\s]*").Match(Encoding.ASCII.GetChars(z.Data)).Value})
                Dim sortedMaps As New List(Of RTCWMapInfo)
                While True
                    Dim latestMap As bspSortingHelper
                    'sort end by end
                    latestMap = bspMapCollection.Where(Function(m) m.NextBsp.ToLower = If(sortedMaps.Count = 0, "gamefinished", sortedMaps(0).MapName.ToLower)).SingleOrDefault()
                    If latestMap Is Nothing Then Exit While
                    sortedMaps.Insert(0, pk3File.Maps.Where(Function(m) m.MapName = latestMap.CurrentBsp).SingleOrDefault)
                End While
                pk3File.Maps = sortedMaps
            End If
            Return pk3File
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