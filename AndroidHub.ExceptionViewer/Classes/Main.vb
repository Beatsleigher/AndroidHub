Option Strict On

Imports System.IO
Imports System.Threading.Tasks

Public Class Main

    Private ReadOnly m_imageList As ImageList
    Private ReadOnly m_logDir As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Icon = My.Resources.AndroidHub_Icon_No_Background
        Me.MaximizeBox = False
        m_imageList = New ImageList()
        m_imageList.Images.Add(My.Resources.json)
        m_jsonListView.StateImageList = m_imageList
        Me.m_logDir = String.Format("{0}\.beatsleigher\AndroidHub\user_accessible\exception\logs", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))
        LoadJsonFilesAsync(m_logDir)
        m_deleteButton.Enabled = False
        m_openButton.Enabled = False
        m_jsonListView.MultiSelect = False
    End Sub

    Private Async Sub m_refreshButton_OnClick(m_sender As Object, m_event As EventArgs) Handles m_refreshButton.Click
        Await LoadJsonFilesAsync(m_logDir)
    End Sub

    Private Sub m_deleteButton_OnClick(m_sender As Object, m_event As EventArgs) Handles m_deleteButton.Click
        Dim m_selectedItem As ListViewItem = m_jsonListView.SelectedItems(0)
        File.Delete(m_selectedItem.Text)
    End Sub

    Private Sub m_openButton_OnClick(m_sender As Object, m_event As EventArgs) Handles m_openButton.Click

    End Sub

    Private Sub m_jsonListView_OnItemSelectionChanged(m_sender As Object, m_event As ListViewItemSelectionChangedEventArgs) Handles m_jsonListView.ItemSelectionChanged
        If (m_event.Item.Selected) Then
            m_deleteButton.Enabled = True
            m_openButton.Enabled = True
        Else : m_deleteButton.Enabled = False : m_openButton.Enabled = False
        End If
    End Sub

    ''' private async Task<bool> LoadJsonFilesAsync(string m_directory) {
    '''     await Task.Run(new Action(() => {
    '''         foreach (string m_jsonFile in Directory.GetFiles(m_directory, "*.json", SearchOptions.TopDirectoryOnly)
    '''             listView1.Items.Add(new ListViewItem() {
    '''                 .StateImageIndex = 0,
    '''                 .Text = m_jsonFile
    '''             });
    '''     });
    ''' return true;
    ''' }
    Friend Async Function LoadJsonFilesAsync(m_directory As String) As Task(Of Boolean)
        m_jsonListView.Items.Clear()
        Await Task.Run(Sub()
                           For Each m_jsonFile As String In Directory.GetFiles(m_directory)
                               m_jsonListView.Invoke(Sub()
                                                         Dim m_listViewItem As New ListViewItem()
                                                         m_listViewItem.Text = m_jsonFile
                                                         m_listViewItem.StateImageIndex = 0
                                                         m_jsonListView.Items.Add(m_listViewItem)
                                                     End Sub)
                           Next
                       End Sub)
        Return True
    End Function

End Class