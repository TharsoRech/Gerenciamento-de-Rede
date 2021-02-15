<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdicionarUsuário
    Inherits MetroSet_UI.Forms.MetroSetForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdicionarUsuário))
        Me.Senha = New MetroSet_UI.Controls.MetroSetTextBox()
        Me.MetroSetLabel29 = New MetroSet_UI.Controls.MetroSetLabel()
        Me.Login = New MetroSet_UI.Controls.MetroSetTextBox()
        Me.MetroSetLabel30 = New MetroSet_UI.Controls.MetroSetLabel()
        Me.Nome = New MetroSet_UI.Controls.MetroSetTextBox()
        Me.MetroSetLabel1 = New MetroSet_UI.Controls.MetroSetLabel()
        Me.MetroSetLabel2 = New MetroSet_UI.Controls.MetroSetLabel()
        Me.Descricao = New MetroSet_UI.Controls.MetroSetTextBox()
        Me.MetroDefaultSetButton4 = New MetroSet_UI.Controls.MetroDefaultSetButton()
        Me.MetroDefaultSetButton1 = New MetroSet_UI.Controls.MetroDefaultSetButton()
        Me.SuspendLayout()
        '
        'Senha
        '
        Me.Senha.AutoCompleteCustomSource = Nothing
        Me.Senha.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.Senha.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.Senha.BorderColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.Senha.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Senha.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Senha.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Senha.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Senha.HoverColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Senha.Image = Nothing
        Me.Senha.Lines = Nothing
        Me.Senha.Location = New System.Drawing.Point(140, 232)
        Me.Senha.MaxLength = 32767
        Me.Senha.Multiline = False
        Me.Senha.Name = "Senha"
        Me.Senha.ReadOnly = False
        Me.Senha.Size = New System.Drawing.Size(399, 28)
        Me.Senha.Style = MetroSet_UI.Design.Style.Dark
        Me.Senha.StyleManager = Nothing
        Me.Senha.TabIndex = 135
        Me.Senha.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Senha.ThemeAuthor = "Narwin"
        Me.Senha.ThemeName = "MetroDark"
        Me.Senha.UseSystemPasswordChar = True
        Me.Senha.WatermarkText = ""
        '
        'MetroSetLabel29
        '
        Me.MetroSetLabel29.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroSetLabel29.Location = New System.Drawing.Point(53, 182)
        Me.MetroSetLabel29.Name = "MetroSetLabel29"
        Me.MetroSetLabel29.Size = New System.Drawing.Size(70, 23)
        Me.MetroSetLabel29.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroSetLabel29.StyleManager = Nothing
        Me.MetroSetLabel29.TabIndex = 134
        Me.MetroSetLabel29.Text = "Login:"
        Me.MetroSetLabel29.ThemeAuthor = "Narwin"
        Me.MetroSetLabel29.ThemeName = "MetroDark"
        '
        'Login
        '
        Me.Login.AutoCompleteCustomSource = Nothing
        Me.Login.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.Login.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.Login.BorderColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.Login.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Login.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Login.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Login.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Login.HoverColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Login.Image = Nothing
        Me.Login.Lines = Nothing
        Me.Login.Location = New System.Drawing.Point(140, 177)
        Me.Login.MaxLength = 32767
        Me.Login.Multiline = False
        Me.Login.Name = "Login"
        Me.Login.ReadOnly = True
        Me.Login.Size = New System.Drawing.Size(399, 28)
        Me.Login.Style = MetroSet_UI.Design.Style.Dark
        Me.Login.StyleManager = Nothing
        Me.Login.TabIndex = 133
        Me.Login.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Login.ThemeAuthor = "Narwin"
        Me.Login.ThemeName = "MetroDark"
        Me.Login.UseSystemPasswordChar = False
        Me.Login.WatermarkText = ""
        '
        'MetroSetLabel30
        '
        Me.MetroSetLabel30.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroSetLabel30.Location = New System.Drawing.Point(53, 119)
        Me.MetroSetLabel30.Name = "MetroSetLabel30"
        Me.MetroSetLabel30.Size = New System.Drawing.Size(59, 23)
        Me.MetroSetLabel30.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroSetLabel30.StyleManager = Nothing
        Me.MetroSetLabel30.TabIndex = 132
        Me.MetroSetLabel30.Text = "Nome:"
        Me.MetroSetLabel30.ThemeAuthor = "Narwin"
        Me.MetroSetLabel30.ThemeName = "MetroDark"
        '
        'Nome
        '
        Me.Nome.AutoCompleteCustomSource = Nothing
        Me.Nome.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.Nome.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.Nome.BorderColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.Nome.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Nome.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Nome.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Nome.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Nome.HoverColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Nome.Image = Nothing
        Me.Nome.Lines = Nothing
        Me.Nome.Location = New System.Drawing.Point(140, 114)
        Me.Nome.MaxLength = 32767
        Me.Nome.Multiline = False
        Me.Nome.Name = "Nome"
        Me.Nome.ReadOnly = True
        Me.Nome.Size = New System.Drawing.Size(399, 28)
        Me.Nome.Style = MetroSet_UI.Design.Style.Dark
        Me.Nome.StyleManager = Nothing
        Me.Nome.TabIndex = 131
        Me.Nome.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Nome.ThemeAuthor = "Narwin"
        Me.Nome.ThemeName = "MetroDark"
        Me.Nome.UseSystemPasswordChar = False
        Me.Nome.WatermarkText = ""
        '
        'MetroSetLabel1
        '
        Me.MetroSetLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroSetLabel1.Location = New System.Drawing.Point(53, 237)
        Me.MetroSetLabel1.Name = "MetroSetLabel1"
        Me.MetroSetLabel1.Size = New System.Drawing.Size(70, 23)
        Me.MetroSetLabel1.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroSetLabel1.StyleManager = Nothing
        Me.MetroSetLabel1.TabIndex = 136
        Me.MetroSetLabel1.Text = "Senha:"
        Me.MetroSetLabel1.ThemeAuthor = "Narwin"
        Me.MetroSetLabel1.ThemeName = "MetroDark"
        '
        'MetroSetLabel2
        '
        Me.MetroSetLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroSetLabel2.Location = New System.Drawing.Point(42, 284)
        Me.MetroSetLabel2.Name = "MetroSetLabel2"
        Me.MetroSetLabel2.Size = New System.Drawing.Size(81, 23)
        Me.MetroSetLabel2.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroSetLabel2.StyleManager = Nothing
        Me.MetroSetLabel2.TabIndex = 138
        Me.MetroSetLabel2.Text = "Descrição:"
        Me.MetroSetLabel2.ThemeAuthor = "Narwin"
        Me.MetroSetLabel2.ThemeName = "MetroDark"
        '
        'Descricao
        '
        Me.Descricao.AutoCompleteCustomSource = Nothing
        Me.Descricao.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.Descricao.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.Descricao.BorderColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.Descricao.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Descricao.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Descricao.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Descricao.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Descricao.HoverColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Descricao.Image = Nothing
        Me.Descricao.Lines = Nothing
        Me.Descricao.Location = New System.Drawing.Point(140, 284)
        Me.Descricao.MaxLength = 32767
        Me.Descricao.Multiline = False
        Me.Descricao.Name = "Descricao"
        Me.Descricao.ReadOnly = False
        Me.Descricao.Size = New System.Drawing.Size(399, 28)
        Me.Descricao.Style = MetroSet_UI.Design.Style.Dark
        Me.Descricao.StyleManager = Nothing
        Me.Descricao.TabIndex = 137
        Me.Descricao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Descricao.ThemeAuthor = "Narwin"
        Me.Descricao.ThemeName = "MetroDark"
        Me.Descricao.UseSystemPasswordChar = True
        Me.Descricao.WatermarkText = ""
        '
        'MetroDefaultSetButton4
        '
        Me.MetroDefaultSetButton4.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.MetroDefaultSetButton4.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton4.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroDefaultSetButton4.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton4.HoverColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton4.HoverTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton4.Location = New System.Drawing.Point(386, 358)
        Me.MetroDefaultSetButton4.Name = "MetroDefaultSetButton4"
        Me.MetroDefaultSetButton4.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MetroDefaultSetButton4.NormalColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.MetroDefaultSetButton4.NormalTextColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MetroDefaultSetButton4.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton4.PressColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton4.PressTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton4.Size = New System.Drawing.Size(153, 30)
        Me.MetroDefaultSetButton4.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroDefaultSetButton4.StyleManager = Nothing
        Me.MetroDefaultSetButton4.TabIndex = 140
        Me.MetroDefaultSetButton4.Text = "Adicionar Novo "
        Me.MetroDefaultSetButton4.ThemeAuthor = "Narwin"
        Me.MetroDefaultSetButton4.ThemeName = "MetroDark"
        '
        'MetroDefaultSetButton1
        '
        Me.MetroDefaultSetButton1.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.MetroDefaultSetButton1.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton1.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroDefaultSetButton1.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton1.HoverColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton1.HoverTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton1.Location = New System.Drawing.Point(140, 358)
        Me.MetroDefaultSetButton1.Name = "MetroDefaultSetButton1"
        Me.MetroDefaultSetButton1.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MetroDefaultSetButton1.NormalColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.MetroDefaultSetButton1.NormalTextColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MetroDefaultSetButton1.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton1.PressColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton1.PressTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton1.Size = New System.Drawing.Size(153, 30)
        Me.MetroDefaultSetButton1.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroDefaultSetButton1.StyleManager = Nothing
        Me.MetroDefaultSetButton1.TabIndex = 141
        Me.MetroDefaultSetButton1.Text = "Cancelar"
        Me.MetroDefaultSetButton1.ThemeAuthor = "Narwin"
        Me.MetroDefaultSetButton1.ThemeName = "MetroDark"
        '
        'AdicionarUsuário
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(668, 426)
        Me.Controls.Add(Me.MetroDefaultSetButton1)
        Me.Controls.Add(Me.MetroDefaultSetButton4)
        Me.Controls.Add(Me.MetroSetLabel2)
        Me.Controls.Add(Me.Descricao)
        Me.Controls.Add(Me.MetroSetLabel1)
        Me.Controls.Add(Me.Senha)
        Me.Controls.Add(Me.MetroSetLabel29)
        Me.Controls.Add(Me.Login)
        Me.Controls.Add(Me.MetroSetLabel30)
        Me.Controls.Add(Me.Nome)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AdicionarUsuário"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Style = MetroSet_UI.Design.Style.Dark
        Me.Text = "AdicionarUsuário"
        Me.TextColor = System.Drawing.Color.White
        Me.ThemeName = "MetroDark"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Senha As MetroSet_UI.Controls.MetroSetTextBox
    Friend WithEvents MetroSetLabel29 As MetroSet_UI.Controls.MetroSetLabel
    Friend WithEvents Login As MetroSet_UI.Controls.MetroSetTextBox
    Friend WithEvents MetroSetLabel30 As MetroSet_UI.Controls.MetroSetLabel
    Friend WithEvents Nome As MetroSet_UI.Controls.MetroSetTextBox
    Friend WithEvents MetroSetLabel1 As MetroSet_UI.Controls.MetroSetLabel
    Friend WithEvents MetroSetLabel2 As MetroSet_UI.Controls.MetroSetLabel
    Friend WithEvents Descricao As MetroSet_UI.Controls.MetroSetTextBox
    Friend WithEvents MetroDefaultSetButton4 As MetroSet_UI.Controls.MetroDefaultSetButton
    Friend WithEvents MetroDefaultSetButton1 As MetroSet_UI.Controls.MetroDefaultSetButton
End Class
