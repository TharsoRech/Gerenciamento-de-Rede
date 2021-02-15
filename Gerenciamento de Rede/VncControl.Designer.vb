<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VncControl_
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
        Me.MetroSetPanel12 = New MetroSet_UI.Controls.MetroSetPanel()
        Me.MetroDefaultSetButton3 = New MetroSet_UI.Controls.MetroDefaultSetButton()
        Me.MetroDefaultSetButton2 = New MetroSet_UI.Controls.MetroDefaultSetButton()
        Me.MetroDefaultSetButton1 = New MetroSet_UI.Controls.MetroDefaultSetButton()
        Me.MetroComboBox1 = New MetroSet_UI.Controls.MetroSetComboBox()
        Me.MetroDefaultSetButton63 = New MetroSet_UI.Controls.MetroDefaultSetButton()
        Me.MetroDefaultSetButton62 = New MetroSet_UI.Controls.MetroDefaultSetButton()
        Me.MetroDefaultSetButton61 = New MetroSet_UI.Controls.MetroDefaultSetButton()
        Me.Spymode = New MetroSet_UI.Controls.MetroDefaultSetButton()
        Me.RD = New VncSharp.RemoteDesktop()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.MetroSetPanel12.SuspendLayout()
        Me.SuspendLayout()
        '
        'MetroSetPanel12
        '
        Me.MetroSetPanel12.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.MetroSetPanel12.BorderColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.MetroSetPanel12.BorderThickness = 0
        Me.MetroSetPanel12.Controls.Add(Me.MetroDefaultSetButton3)
        Me.MetroSetPanel12.Controls.Add(Me.MetroDefaultSetButton2)
        Me.MetroSetPanel12.Controls.Add(Me.MetroDefaultSetButton1)
        Me.MetroSetPanel12.Controls.Add(Me.MetroComboBox1)
        Me.MetroSetPanel12.Controls.Add(Me.MetroDefaultSetButton63)
        Me.MetroSetPanel12.Controls.Add(Me.MetroDefaultSetButton62)
        Me.MetroSetPanel12.Controls.Add(Me.MetroDefaultSetButton61)
        Me.MetroSetPanel12.Controls.Add(Me.Spymode)
        Me.MetroSetPanel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.MetroSetPanel12.Location = New System.Drawing.Point(0, 0)
        Me.MetroSetPanel12.Name = "MetroSetPanel12"
        Me.MetroSetPanel12.Size = New System.Drawing.Size(1082, 25)
        Me.MetroSetPanel12.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroSetPanel12.StyleManager = Nothing
        Me.MetroSetPanel12.TabIndex = 1
        Me.MetroSetPanel12.ThemeAuthor = "Narwin"
        Me.MetroSetPanel12.ThemeName = "MetroDark"
        '
        'MetroDefaultSetButton3
        '
        Me.MetroDefaultSetButton3.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.MetroDefaultSetButton3.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton3.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton3.Dock = System.Windows.Forms.DockStyle.Right
        Me.MetroDefaultSetButton3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroDefaultSetButton3.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton3.HoverColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton3.HoverTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton3.Location = New System.Drawing.Point(838, 0)
        Me.MetroDefaultSetButton3.Name = "MetroDefaultSetButton3"
        Me.MetroDefaultSetButton3.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MetroDefaultSetButton3.NormalColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.MetroDefaultSetButton3.NormalTextColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MetroDefaultSetButton3.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton3.PressColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton3.PressTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton3.Size = New System.Drawing.Size(122, 25)
        Me.MetroDefaultSetButton3.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroDefaultSetButton3.StyleManager = Nothing
        Me.MetroDefaultSetButton3.TabIndex = 7
        Me.MetroDefaultSetButton3.Text = "Tela Cheia"
        Me.MetroDefaultSetButton3.ThemeAuthor = "Narwin"
        Me.MetroDefaultSetButton3.ThemeName = "MetroDark"
        '
        'MetroDefaultSetButton2
        '
        Me.MetroDefaultSetButton2.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.MetroDefaultSetButton2.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton2.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton2.Dock = System.Windows.Forms.DockStyle.Left
        Me.MetroDefaultSetButton2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroDefaultSetButton2.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton2.HoverColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton2.HoverTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton2.Location = New System.Drawing.Point(734, 0)
        Me.MetroDefaultSetButton2.Name = "MetroDefaultSetButton2"
        Me.MetroDefaultSetButton2.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MetroDefaultSetButton2.NormalColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.MetroDefaultSetButton2.NormalTextColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MetroDefaultSetButton2.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton2.PressColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton2.PressTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton2.Size = New System.Drawing.Size(105, 25)
        Me.MetroDefaultSetButton2.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroDefaultSetButton2.StyleManager = Nothing
        Me.MetroDefaultSetButton2.TabIndex = 6
        Me.MetroDefaultSetButton2.Text = "Atualizar"
        Me.MetroDefaultSetButton2.ThemeAuthor = "Narwin"
        Me.MetroDefaultSetButton2.ThemeName = "MetroDark"
        '
        'MetroDefaultSetButton1
        '
        Me.MetroDefaultSetButton1.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.MetroDefaultSetButton1.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton1.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton1.Dock = System.Windows.Forms.DockStyle.Right
        Me.MetroDefaultSetButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroDefaultSetButton1.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton1.HoverColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton1.HoverTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton1.Location = New System.Drawing.Point(960, 0)
        Me.MetroDefaultSetButton1.Name = "MetroDefaultSetButton1"
        Me.MetroDefaultSetButton1.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MetroDefaultSetButton1.NormalColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.MetroDefaultSetButton1.NormalTextColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MetroDefaultSetButton1.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton1.PressColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton1.PressTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton1.Size = New System.Drawing.Size(122, 25)
        Me.MetroDefaultSetButton1.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroDefaultSetButton1.StyleManager = Nothing
        Me.MetroDefaultSetButton1.TabIndex = 5
        Me.MetroDefaultSetButton1.Text = "Fechar Conexão"
        Me.MetroDefaultSetButton1.ThemeAuthor = "Narwin"
        Me.MetroDefaultSetButton1.ThemeName = "MetroDark"
        '
        'MetroComboBox1
        '
        Me.MetroComboBox1.AllowDrop = True
        Me.MetroComboBox1.ArrowColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.MetroComboBox1.BackColor = System.Drawing.Color.Transparent
        Me.MetroComboBox1.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.MetroComboBox1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.MetroComboBox1.CausesValidation = False
        Me.MetroComboBox1.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.MetroComboBox1.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroComboBox1.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroComboBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.MetroComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.MetroComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.MetroComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.MetroComboBox1.FormattingEnabled = True
        Me.MetroComboBox1.ItemHeight = 20
        Me.MetroComboBox1.Items.AddRange(New Object() {"CMD ", "Windows+E(Meu Computador)", "Windows+D(Mostrar Desktop)", "Abrir Gerenciador De Tarefas", "Windows+Pause Break(Propriedades Do Computador)", "Alt+F4(Fechar Janela)", "Bloquear Computador", "Menu Executar", "Abrir Menu Iniciar", "Executar Um Comando", "Executar Um arquivo"})
        Me.MetroComboBox1.Location = New System.Drawing.Point(424, 0)
        Me.MetroComboBox1.Name = "MetroComboBox1"
        Me.MetroComboBox1.SelectedItemBackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.MetroComboBox1.SelectedItemForeColor = System.Drawing.Color.White
        Me.MetroComboBox1.Size = New System.Drawing.Size(310, 26)
        Me.MetroComboBox1.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroComboBox1.StyleManager = Nothing
        Me.MetroComboBox1.TabIndex = 4
        Me.MetroComboBox1.ThemeAuthor = "Narwin"
        Me.MetroComboBox1.ThemeName = "MetroDark"
        '
        'MetroDefaultSetButton63
        '
        Me.MetroDefaultSetButton63.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.MetroDefaultSetButton63.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton63.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton63.Dock = System.Windows.Forms.DockStyle.Left
        Me.MetroDefaultSetButton63.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroDefaultSetButton63.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton63.HoverColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton63.HoverTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton63.Location = New System.Drawing.Point(308, 0)
        Me.MetroDefaultSetButton63.Name = "MetroDefaultSetButton63"
        Me.MetroDefaultSetButton63.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MetroDefaultSetButton63.NormalColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.MetroDefaultSetButton63.NormalTextColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MetroDefaultSetButton63.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton63.PressColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton63.PressTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton63.Size = New System.Drawing.Size(116, 25)
        Me.MetroDefaultSetButton63.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroDefaultSetButton63.StyleManager = Nothing
        Me.MetroDefaultSetButton63.TabIndex = 3
        Me.MetroDefaultSetButton63.Text = "Salvar Print"
        Me.MetroDefaultSetButton63.ThemeAuthor = "Narwin"
        Me.MetroDefaultSetButton63.ThemeName = "MetroDark"
        '
        'MetroDefaultSetButton62
        '
        Me.MetroDefaultSetButton62.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.MetroDefaultSetButton62.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton62.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton62.Dock = System.Windows.Forms.DockStyle.Left
        Me.MetroDefaultSetButton62.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroDefaultSetButton62.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton62.HoverColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton62.HoverTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton62.Location = New System.Drawing.Point(203, 0)
        Me.MetroDefaultSetButton62.Name = "MetroDefaultSetButton62"
        Me.MetroDefaultSetButton62.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MetroDefaultSetButton62.NormalColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.MetroDefaultSetButton62.NormalTextColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MetroDefaultSetButton62.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton62.PressColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton62.PressTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton62.Size = New System.Drawing.Size(105, 25)
        Me.MetroDefaultSetButton62.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroDefaultSetButton62.StyleManager = Nothing
        Me.MetroDefaultSetButton62.TabIndex = 2
        Me.MetroDefaultSetButton62.Text = "Colar texto"
        Me.MetroDefaultSetButton62.ThemeAuthor = "Narwin"
        Me.MetroDefaultSetButton62.ThemeName = "MetroDark"
        '
        'MetroDefaultSetButton61
        '
        Me.MetroDefaultSetButton61.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.MetroDefaultSetButton61.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton61.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton61.Dock = System.Windows.Forms.DockStyle.Left
        Me.MetroDefaultSetButton61.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroDefaultSetButton61.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton61.HoverColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton61.HoverTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton61.Location = New System.Drawing.Point(100, 0)
        Me.MetroDefaultSetButton61.Name = "MetroDefaultSetButton61"
        Me.MetroDefaultSetButton61.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MetroDefaultSetButton61.NormalColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.MetroDefaultSetButton61.NormalTextColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MetroDefaultSetButton61.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton61.PressColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton61.PressTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton61.Size = New System.Drawing.Size(103, 25)
        Me.MetroDefaultSetButton61.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroDefaultSetButton61.StyleManager = Nothing
        Me.MetroDefaultSetButton61.TabIndex = 1
        Me.MetroDefaultSetButton61.Text = "Ctrl +alt+ del"
        Me.MetroDefaultSetButton61.ThemeAuthor = "Narwin"
        Me.MetroDefaultSetButton61.ThemeName = "MetroDark"
        '
        'Spymode
        '
        Me.Spymode.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Spymode.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Spymode.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Spymode.Dock = System.Windows.Forms.DockStyle.Left
        Me.Spymode.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Spymode.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.Spymode.HoverColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.Spymode.HoverTextColor = System.Drawing.Color.White
        Me.Spymode.Location = New System.Drawing.Point(0, 0)
        Me.Spymode.Name = "Spymode"
        Me.Spymode.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Spymode.NormalColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.Spymode.NormalTextColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.Spymode.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Spymode.PressColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Spymode.PressTextColor = System.Drawing.Color.White
        Me.Spymode.Size = New System.Drawing.Size(100, 25)
        Me.Spymode.Style = MetroSet_UI.Design.Style.Dark
        Me.Spymode.StyleManager = Nothing
        Me.Spymode.TabIndex = 0
        Me.Spymode.Text = "Spy Mode"
        Me.Spymode.ThemeAuthor = "Narwin"
        Me.Spymode.ThemeName = "MetroDark"
        '
        'RD
        '
        Me.RD.AutoScroll = True
        Me.RD.AutoScrollMinSize = New System.Drawing.Size(608, 427)
        Me.RD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RD.Location = New System.Drawing.Point(0, 25)
        Me.RD.Name = "RD"
        Me.RD.Size = New System.Drawing.Size(1082, 608)
        Me.RD.TabIndex = 2
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Timer2
        '
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'VncControl_
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RD)
        Me.Controls.Add(Me.MetroSetPanel12)
        Me.Name = "VncControl_"
        Me.Size = New System.Drawing.Size(1082, 633)
        Me.MetroSetPanel12.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MetroSetPanel12 As MetroSet_UI.Controls.MetroSetPanel
    Friend WithEvents MetroComboBox1 As MetroSet_UI.Controls.MetroSetComboBox
    Friend WithEvents MetroDefaultSetButton63 As MetroSet_UI.Controls.MetroDefaultSetButton
    Friend WithEvents MetroDefaultSetButton62 As MetroSet_UI.Controls.MetroDefaultSetButton
    Friend WithEvents MetroDefaultSetButton61 As MetroSet_UI.Controls.MetroDefaultSetButton
    Public WithEvents RD As VncSharp.RemoteDesktop
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Public WithEvents Spymode As MetroSet_UI.Controls.MetroDefaultSetButton
    Friend WithEvents Timer1 As Timer
    Friend WithEvents MetroDefaultSetButton1 As MetroSet_UI.Controls.MetroDefaultSetButton
    Friend WithEvents Timer2 As Timer
    Friend WithEvents MetroDefaultSetButton2 As MetroSet_UI.Controls.MetroDefaultSetButton
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
	Friend WithEvents MetroDefaultSetButton3 As MetroSet_UI.Controls.MetroDefaultSetButton
End Class
