<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Iniciando
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Iniciando))
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.MetroSetLabel1 = New MetroSet_UI.Controls.MetroSetLabel()
        Me.FButton1 = New WinForm.UI.Controls.FButton()
        Me.MetroSetProgressBar1 = New MetroSet_UI.Controls.MetroSetProgressBar()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = CType(resources.GetObject("PictureBox2.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox2.Location = New System.Drawing.Point(12, 16)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(35, 41)
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(502, 10)
        Me.Panel1.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Black
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 151)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(502, 10)
        Me.Panel2.TabIndex = 3
        '
        'MetroSetLabel1
        '
        Me.MetroSetLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroSetLabel1.Location = New System.Drawing.Point(34, 92)
        Me.MetroSetLabel1.Name = "MetroSetLabel1"
        Me.MetroSetLabel1.Size = New System.Drawing.Size(418, 33)
        Me.MetroSetLabel1.Style = MetroSet_UI.Design.Style.Light
        Me.MetroSetLabel1.StyleManager = Nothing
        Me.MetroSetLabel1.TabIndex = 4
        Me.MetroSetLabel1.Text = "Iniciando Aplicativo....Por Favor Espere"
        Me.MetroSetLabel1.ThemeAuthor = "Narwin"
        Me.MetroSetLabel1.ThemeName = "MetroLite"
        '
        'FButton1
        '
        Me.FButton1.BackColor = System.Drawing.Color.Transparent
        Me.FButton1.BackgroundImage = CType(resources.GetObject("FButton1.BackgroundImage"), System.Drawing.Image)
        Me.FButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.FButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.FButton1.ForeColor = System.Drawing.Color.Black
        Me.FButton1.Location = New System.Drawing.Point(448, 12)
        Me.FButton1.Name = "FButton1"
        Me.FButton1.Size = New System.Drawing.Size(54, 28)
        Me.FButton1.TabIndex = 6
        Me.FButton1.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'MetroSetProgressBar1
        '
        Me.MetroSetProgressBar1.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.MetroSetProgressBar1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.MetroSetProgressBar1.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.MetroSetProgressBar1.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.MetroSetProgressBar1.DisabledProgressColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.MetroSetProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MetroSetProgressBar1.Location = New System.Drawing.Point(0, 128)
        Me.MetroSetProgressBar1.Maximum = 100
        Me.MetroSetProgressBar1.Minimum = 0
        Me.MetroSetProgressBar1.Name = "MetroSetProgressBar1"
        Me.MetroSetProgressBar1.Orientation = MetroSet_UI.Enums.ProgressOrientation.Horizontal
        Me.MetroSetProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.MetroSetProgressBar1.Size = New System.Drawing.Size(502, 23)
        Me.MetroSetProgressBar1.Style = MetroSet_UI.Design.Style.Light
        Me.MetroSetProgressBar1.StyleManager = Nothing
        Me.MetroSetProgressBar1.TabIndex = 7
        Me.MetroSetProgressBar1.Text = "MetroSetProgressBar1"
        Me.MetroSetProgressBar1.ThemeAuthor = "Narwin"
        Me.MetroSetProgressBar1.ThemeName = "MetroLite"
        Me.MetroSetProgressBar1.Value = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.Location = New System.Drawing.Point(75, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(349, 66)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Iniciando
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(502, 161)
        Me.Controls.Add(Me.MetroSetProgressBar1)
        Me.Controls.Add(Me.MetroSetLabel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.FButton1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Iniciando"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Iniciando"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox2 As PictureBox
	Friend WithEvents Panel1 As Panel
	Friend WithEvents Panel2 As Panel
	Public WithEvents MetroSetLabel1 As MetroSet_UI.Controls.MetroSetLabel
	Friend WithEvents FButton1 As WinForm.UI.Controls.FButton
	Public WithEvents MetroSetProgressBar1 As MetroSet_UI.Controls.MetroSetProgressBar
	Friend WithEvents PictureBox1 As PictureBox
End Class
