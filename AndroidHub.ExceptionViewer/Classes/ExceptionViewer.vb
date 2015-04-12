Option Strict On

Imports Newtonsoft.Json
Imports System.IO

Public Class ExceptionViewer

    Public Property Exception As JsonException

    Public Sub New(m_logFile As String)
        InitializeComponent()
        Exception = JsonConvert.DeserializeObject(Of JsonException)(m_logFile)
    End Sub

End Class