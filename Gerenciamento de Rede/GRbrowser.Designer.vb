<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GRbrowser
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GRbrowser))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Atalhos = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ChromeTabControl1 = New DotNetChromeTabs.ChromeTabControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.GerarRelatórioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GerarRelatóioCallCenterDeHTMLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Atalhos)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(92, 654)
        Me.Panel1.TabIndex = 0
        '
        'Atalhos
        '
        Me.Atalhos.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Atalhos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Atalhos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.Atalhos.Location = New System.Drawing.Point(0, 53)
        Me.Atalhos.Name = "Atalhos"
        Me.Atalhos.Size = New System.Drawing.Size(92, 601)
        Me.Atalhos.TabIndex = 2
        Me.Atalhos.WrapContents = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 28)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(92, 25)
        Me.Panel2.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(0, 0)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(92, 25)
        Me.Button2.TabIndex = 0
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(0, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(92, 28)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Atalhos"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ChromeTabControl1
        '
        Me.ChromeTabControl1.AllowDrop = True
        Me.ChromeTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChromeTabControl1.Location = New System.Drawing.Point(92, 0)
        Me.ChromeTabControl1.Name = "ChromeTabControl1"
        Me.ChromeTabControl1.NewTabButton = False
        Me.ChromeTabControl1.Size = New System.Drawing.Size(999, 654)
        Me.ChromeTabControl1.TabIndex = 1
        Me.ChromeTabControl1.Text = "ChromeTabControl1"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GerarRelatórioToolStripMenuItem, Me.GerarRelatóioCallCenterDeHTMLToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(261, 48)
        '
        'GerarRelatórioToolStripMenuItem
        '
        Me.GerarRelatórioToolStripMenuItem.Image = CType(resources.GetObject("GerarRelatórioToolStripMenuItem.Image"), System.Drawing.Image)
        Me.GerarRelatórioToolStripMenuItem.Name = "GerarRelatórioToolStripMenuItem"
        Me.GerarRelatórioToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.GerarRelatórioToolStripMenuItem.Text = "Gerar Relatório Call center"
        '
        'GerarRelatóioCallCenterDeHTMLToolStripMenuItem
        '
        Me.GerarRelatóioCallCenterDeHTMLToolStripMenuItem.Image = CType(resources.GetObject("GerarRelatóioCallCenterDeHTMLToolStripMenuItem.Image"), System.Drawing.Image)
        Me.GerarRelatóioCallCenterDeHTMLToolStripMenuItem.Name = "GerarRelatóioCallCenterDeHTMLToolStripMenuItem"
        Me.GerarRelatóioCallCenterDeHTMLToolStripMenuItem.Size = New System.Drawing.Size(260, 22)
        Me.GerarRelatóioCallCenterDeHTMLToolStripMenuItem.Text = "Gerar Relatóio Call Center de HTML"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'GRbrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ChromeTabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "GRbrowser"
        Me.Size = New System.Drawing.Size(1091, 654)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents ChromeTabControl1 As DotNetChromeTabs.ChromeTabControl
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents Atalhos As FlowLayoutPanel
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents GerarRelatórioToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GerarRelatóioCallCenterDeHTMLToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
