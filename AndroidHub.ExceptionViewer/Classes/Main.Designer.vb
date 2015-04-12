<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("C:\Users\Beatsleigher\AppData\Roaming\.beatsleigher\AndroidHub\user_accessible\ex" & _
        "ception\logs\exception-2015-4-12-130733186787252119.log")
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("C:\Users\Beatsleigher\AppData\Roaming\.beatsleigher\AndroidHub\user_accessible\ex" & _
        "ception\logs\exception-2015-4-12-130733196248165337.log")
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.m_refreshButton = New System.Windows.Forms.Button()
        Me.m_deleteButton = New System.Windows.Forms.Button()
        Me.m_openButton = New System.Windows.Forms.Button()
        Me.m_jsonListView = New System.Windows.Forms.ListView()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.m_refreshButton)
        Me.FlowLayoutPanel1.Controls.Add(Me.m_deleteButton)
        Me.FlowLayoutPanel1.Controls.Add(Me.m_openButton)
        Me.FlowLayoutPanel1.Controls.Add(Me.m_jsonListView)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(634, 261)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'm_refreshButton
        '
        Me.m_refreshButton.Location = New System.Drawing.Point(3, 3)
        Me.m_refreshButton.Name = "m_refreshButton"
        Me.m_refreshButton.Size = New System.Drawing.Size(75, 23)
        Me.m_refreshButton.TabIndex = 1
        Me.m_refreshButton.Text = "Refresh"
        Me.m_refreshButton.UseVisualStyleBackColor = True
        '
        'm_deleteButton
        '
        Me.m_deleteButton.Location = New System.Drawing.Point(84, 3)
        Me.m_deleteButton.Name = "m_deleteButton"
        Me.m_deleteButton.Size = New System.Drawing.Size(75, 23)
        Me.m_deleteButton.TabIndex = 2
        Me.m_deleteButton.Text = "Delete"
        Me.m_deleteButton.UseVisualStyleBackColor = True
        '
        'm_openButton
        '
        Me.m_openButton.Location = New System.Drawing.Point(165, 3)
        Me.m_openButton.Name = "m_openButton"
        Me.m_openButton.Size = New System.Drawing.Size(75, 23)
        Me.m_openButton.TabIndex = 3
        Me.m_openButton.Text = "Open"
        Me.m_openButton.UseVisualStyleBackColor = True
        '
        'm_jsonListView
        '
        Me.m_jsonListView.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2})
        Me.m_jsonListView.Location = New System.Drawing.Point(3, 32)
        Me.m_jsonListView.Name = "m_jsonListView"
        Me.m_jsonListView.Size = New System.Drawing.Size(631, 229)
        Me.m_jsonListView.TabIndex = 0
        Me.m_jsonListView.UseCompatibleStateImageBehavior = False
        Me.m_jsonListView.View = System.Windows.Forms.View.SmallIcon
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(634, 261)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Name = "Main"
        Me.Text = "AndroidHub Exception Log Viewer"
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents m_jsonListView As System.Windows.Forms.ListView
    Friend WithEvents m_refreshButton As System.Windows.Forms.Button
    Friend WithEvents m_deleteButton As System.Windows.Forms.Button
    Friend WithEvents m_openButton As System.Windows.Forms.Button
End Class
