<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConectionLost
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.MetroSetProgressBar1 = New MetroSet_UI.Controls.MetroSetProgressBar()
        Me.MetroSetLabel1 = New MetroSet_UI.Controls.MetroSetLabel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.MetroSetButton1 = New MetroSet_UI.Controls.MetroSetButton()
        Me.SuspendLayout()
        '
        'MetroSetProgressBar1
        '
        Me.MetroSetProgressBar1.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.MetroSetProgressBar1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.MetroSetProgressBar1.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.MetroSetProgressBar1.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.MetroSetProgressBar1.DisabledProgressColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.MetroSetProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MetroSetProgressBar1.Location = New System.Drawing.Point(0, 0)
        Me.MetroSetProgressBar1.Maximum = 10
        Me.MetroSetProgressBar1.Minimum = 0
        Me.MetroSetProgressBar1.Name = "MetroSetProgressBar1"
        Me.MetroSetProgressBar1.Orientation = MetroSet_UI.Enums.ProgressOrientation.Horizontal
        Me.MetroSetProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.MetroSetProgressBar1.Size = New System.Drawing.Size(451, 262)
        Me.MetroSetProgressBar1.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroSetProgressBar1.StyleManager = Nothing
        Me.MetroSetProgressBar1.TabIndex = 0
        Me.MetroSetProgressBar1.Text = "MetroSetProgressBar1"
        Me.MetroSetProgressBar1.ThemeAuthor = "Narwin"
        Me.MetroSetProgressBar1.ThemeName = "MetroDark"
        Me.MetroSetProgressBar1.Value = 0
        '
        'MetroSetLabel1
        '
        Me.MetroSetLabel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MetroSetLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroSetLabel1.Location = New System.Drawing.Point(0, 239)
        Me.MetroSetLabel1.Name = "MetroSetLabel1"
        Me.MetroSetLabel1.Size = New System.Drawing.Size(451, 23)
        Me.MetroSetLabel1.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroSetLabel1.StyleManager = Nothing
        Me.MetroSetLabel1.TabIndex = 2
        Me.MetroSetLabel1.ThemeAuthor = "Narwin"
        Me.MetroSetLabel1.ThemeName = "MetroDark"
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'MetroSetButton1
        '
        Me.MetroSetButton1.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.MetroSetButton1.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.MetroSetButton1.DisabledForeColor = System.Drawing.Color.Gray
        Me.MetroSetButton1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MetroSetButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroSetButton1.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MetroSetButton1.HoverColor = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MetroSetButton1.HoverTextColor = System.Drawing.Color.White
        Me.MetroSetButton1.Location = New System.Drawing.Point(0, 216)
        Me.MetroSetButton1.Name = "MetroSetButton1"
        Me.MetroSetButton1.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.MetroSetButton1.NormalColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.MetroSetButton1.NormalTextColor = System.Drawing.Color.White
        Me.MetroSetButton1.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.MetroSetButton1.PressColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.MetroSetButton1.PressTextColor = System.Drawing.Color.White
        Me.MetroSetButton1.Size = New System.Drawing.Size(451, 23)
        Me.MetroSetButton1.Style = MetroSet_UI.Design.Style.Light
        Me.MetroSetButton1.StyleManager = Nothing
        Me.MetroSetButton1.TabIndex = 3
        Me.MetroSetButton1.Text = "Cancelar"
        Me.MetroSetButton1.ThemeAuthor = "Narwin"
        Me.MetroSetButton1.ThemeName = "MetroLite"
        '
        'ConectionLost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MetroSetButton1)
        Me.Controls.Add(Me.MetroSetLabel1)
        Me.Controls.Add(Me.MetroSetProgressBar1)
        Me.Name = "ConectionLost"
        Me.Size = New System.Drawing.Size(451, 262)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Timer1 As Timer
    Public WithEvents MetroSetProgressBar1 As MetroSet_UI.Controls.MetroSetProgressBar
    Public WithEvents MetroSetLabel1 As MetroSet_UI.Controls.MetroSetLabel
    Friend WithEvents MetroSetButton1 As MetroSet_UI.Controls.MetroSetButton
End Class
