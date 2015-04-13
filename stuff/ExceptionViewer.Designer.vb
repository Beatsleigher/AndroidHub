<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExceptionViewer
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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"00", "01", "02", "03", "04", "05", "06", "07", "08"}, -1)
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.m_classNameColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_messageColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_dataColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_innerExceptionColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_helpUrlColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_stackTraceColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_exceptionMethodColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_hResultColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_sourceColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.m_classNameColumn, Me.m_messageColumn, Me.m_dataColumn, Me.m_innerExceptionColumn, Me.m_helpUrlColumn, Me.m_stackTraceColumn, Me.m_exceptionMethodColumn, Me.m_hResultColumn, Me.m_sourceColumn})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(1074, 435)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'm_classNameColumn
        '
        Me.m_classNameColumn.Text = "Class Name"
        Me.m_classNameColumn.Width = 83
        '
        'm_messageColumn
        '
        Me.m_messageColumn.Text = "Message"
        Me.m_messageColumn.Width = 71
        '
        'm_dataColumn
        '
        Me.m_dataColumn.Text = "Data"
        '
        'm_innerExceptionColumn
        '
        Me.m_innerExceptionColumn.Text = "Inner Exception"
        Me.m_innerExceptionColumn.Width = 101
        '
        'm_helpUrlColumn
        '
        Me.m_helpUrlColumn.Text = "Help URL"
        '
        'm_stackTraceColumn
        '
        Me.m_stackTraceColumn.Text = "Stack Trace"
        Me.m_stackTraceColumn.Width = 87
        '
        'm_exceptionMethodColumn
        '
        Me.m_exceptionMethodColumn.Text = "Exception Method"
        Me.m_exceptionMethodColumn.Width = 101
        '
        'm_hResultColumn
        '
        Me.m_hResultColumn.Text = "HResult"
        '
        'm_sourceColumn
        '
        Me.m_sourceColumn.Text = "Source"
        '
        'ExceptionViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1074, 435)
        Me.Controls.Add(Me.ListView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "ExceptionViewer"
        Me.Text = "AndroidHub | Exception Viewer"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents m_classNameColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents m_messageColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents m_dataColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents m_innerExceptionColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents m_helpUrlColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents m_stackTraceColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents m_exceptionMethodColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents m_hResultColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents m_sourceColumn As System.Windows.Forms.ColumnHeader
End Class
