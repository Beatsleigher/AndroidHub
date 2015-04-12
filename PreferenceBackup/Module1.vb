Namespace AndroidHub.IO

    Module Main

#Region "Constant Args"
        Const APP_VERSION = "Version 1.0"
        Const ARG_BACKUP = "-backup"
        Const ARG_BACKUP01 = "--b"
        Const ARG_HELP = "-help"
        Const ARG_HELP01 = "--h"
        Const ARG_PATH = "-path"
        Const ARG_PATH01 = "--p"
        Const ARG_RESTORE = "-restore"
        Const ARG_RESTORE01 = "--r"
        Const WINDOW_TITLE = "AndroidHub Preference Backup Utility"
#End Region

        Sub Main()
            Dim m_args As String() = Environment.GetCommandLineArgs()
            Dim m_mode As Mode = Nothing

            For m_i As Integer = 0 To m_args.Length
                If (m_args(m_i).Equals(ARG_BACKUP) OrElse m_args(m_i).Equals(ARG_BACKUP01)) Then
                    ' Check path and backup
                ElseIf (m_args(m_i).Equals(ARG_HELP) OrElse m_args(m_i).Equals(ARG_HELP01)) Then
                    ' Print help. Exit.
                    PrintHelp() : Return
                End If
            Next
        End Sub

        Sub PrintHelp()

        End Sub

        Function GetSpaces(amount As Integer) As String
            Dim m_returnVal As String = ""
            For i As Integer = 0 To amount
                m_returnVal = String.Concat(m_returnVal, " ")
            Next
            Return m_returnVal
        End Function

        Enum Mode
            Backup
            Restore
        End Enum

    End Module
End Namespace
