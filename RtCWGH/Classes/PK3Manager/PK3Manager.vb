Imports System.Text.RegularExpressions
Imports System.IO
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
            'Sort it!
            If IsSort = True Then

            End If
            Return pk3File
        End Function
    End Class
End Namespace