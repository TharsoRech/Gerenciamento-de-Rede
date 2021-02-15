<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Servidorcontrol
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Servidorcontrol))
        Me.MetroSetBadge1 = New MetroSet_UI.Controls.MetroSetBadge()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'MetroSetBadge1
        '
        Me.MetroSetBadge1.BackColor = System.Drawing.Color.Transparent
        Me.MetroSetBadge1.BadgeAlignment = MetroSet_UI.Enums.BadgeAlign.TopRight
        Me.MetroSetBadge1.BadgeText = "?"
        Me.MetroSetBadge1.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.MetroSetBadge1.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroSetBadge1.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroSetBadge1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MetroSetBadge1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroSetBadge1.HoverBadgeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.MetroSetBadge1.HoverBadgeTextColor = System.Drawing.Color.White
        Me.MetroSetBadge1.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroSetBadge1.HoverColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroSetBadge1.HoverTextColor = System.Drawing.Color.White
        Me.MetroSetBadge1.Location = New System.Drawing.Point(0, 0)
        Me.MetroSetBadge1.Name = "MetroSetBadge1"
        Me.MetroSetBadge1.NormalBadgeColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.MetroSetBadge1.NormalBadgeTextColor = System.Drawing.Color.White
        Me.MetroSetBadge1.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MetroSetBadge1.NormalColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.MetroSetBadge1.NormalTextColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MetroSetBadge1.PressBadgeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.MetroSetBadge1.PressBadgeTextColor = System.Drawing.Color.White
        Me.MetroSetBadge1.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroSetBadge1.PressColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroSetBadge1.PressTextColor = System.Drawing.Color.White
        Me.MetroSetBadge1.Size = New System.Drawing.Size(248, 67)
        Me.MetroSetBadge1.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroSetBadge1.StyleManager = Nothing
        Me.MetroSetBadge1.TabIndex = 0
        Me.MetroSetBadge1.Text = "MetroSetBadge1"
        Me.MetroSetBadge1.ThemeAuthor = "Narwin"
        Me.MetroSetBadge1.ThemeName = "MetroDark"
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Panel1.Location = New System.Drawing.Point(3, 21)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(54, 43)
        Me.Panel1.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Panel2.Location = New System.Drawing.Point(3, 21)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(54, 43)
        Me.Panel2.TabIndex = 2
        Me.Panel2.Visible = False
        '
        'Servidorcontrol
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MetroSetBadge1)
        Me.Name = "Servidorcontrol"
        Me.Size = New System.Drawing.Size(248, 67)
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents MetroSetBadge1 As MetroSet_UI.Controls.MetroSetBadge
    Public WithEvents Panel1 As Panel
    Public WithEvents Panel2 As Panel
End Class
