Option Strict On

Imports Newtonsoft.Json
Imports System.IO
Imports System.Windows.Forms.ListViewItem

Public Class ExceptionViewer

    Public Sub New()
        InitializeComponent()
        ListView1.Items.Clear()
        For Each m_log As String In Directory.GetFiles(String.Format("{0}\.beatsleigher\AndroidHub\user_accessible\exception\logs", _
                                                                     Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)))
            Dim m_baseListViewItem = New ListViewItem()
            Dim m_listViewItemArray = New ListViewSubItem(8) {}
            Dim m_exception = JsonConvert.DeserializeObject(Of JsonException)(m_log)

            ' Load each ListViewItem
            m_baseListViewItem.Text = m_exception.GetType().FullName
            m_listViewItemArray(0) = New ListViewSubItem(m_baseListViewItem, m_exception.Message)
            m_listViewItemArray(1) = New ListViewSubItem(m_baseListViewItem, m_exception.Data.ToString())
            m_listViewItemArray(2) = New ListViewSubItem(m_baseListViewItem, m_exception.InnerException.ToString())
            m_listViewItemArray(3) = New ListViewSubItem(m_baseListViewItem, m_exception.HelpLink)
            m_listViewItemArray(4) = New ListViewSubItem(m_baseListViewItem, m_exception.StackTrace)
            m_listViewItemArray(5) = New ListViewSubItem(m_baseListViewItem, m_exception.TargetSite.Name)
            m_listViewItemArray(6) = New ListViewSubItem(m_baseListViewItem, m_exception.HResult.ToString())
            m_listViewItemArray(7) = New ListViewSubItem(m_baseListViewItem, m_exception.Source)

            ' Load data to ListView
            ListView1.Items.Add(m_baseListViewItem)
        Next
    End Sub

End Class