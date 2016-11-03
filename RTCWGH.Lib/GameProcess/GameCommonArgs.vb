Namespace Game
    ''' <summary>
    ''' Some common arguments
    ''' </summary>
    Public Class GameCommonArgs

        ''' <summary>
        ''' This argument is used when loading a mod out of main folder.
        ''' </summary>
        ''' <remarks>{0}:The folder of the mod</remarks>
        Public Shared SetSingleMapLocation As String = "+set fs_home . +set fs_game ""{0}"""

        ''' <summary>
        ''' This argument is used to load a map(Without Animation).
        ''' </summary>
        ''' <remarks>{0}:the name of map which is in main folder or has been loaded by SetSingleMapLocation. </remarks>
        Public Shared SetMapNameWillLoadN As String = "+spdevmap {0}"

        ''' <summary>
        ''' This argument is used to load a map(With Animation).
        ''' </summary>
        ''' <remarks>{0}:the name of map which is in main folder or has been loaded by SetSingleMapLocation. </remarks>
        Public Shared SetMapNameWillLoadA As String = "+set nextmap ""spdevmap {0}"""

        ''' <summary>
        ''' This argument is used to enable cheat mode of RTCW(All Versions)
        ''' </summary>
        Public Shared EnableCheatMode As String = "+set sv_cheats 1"

    End Class
End Namespace