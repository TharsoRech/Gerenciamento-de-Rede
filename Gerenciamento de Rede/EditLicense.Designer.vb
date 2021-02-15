<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditLicense
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditLicense))
        Me.MetroSetLabel97 = New MetroSet_UI.Controls.MetroSetLabel()
        Me.MetroComboBox1 = New MetroSet_UI.Controls.MetroSetComboBox()
        Me.MetroSetLabel51 = New MetroSet_UI.Controls.MetroSetLabel()
        Me.Descricao = New MetroSet_UI.Controls.MetroSetTextBox()
        Me.MetroSetLabel1 = New MetroSet_UI.Controls.MetroSetLabel()
        Me.Chave = New MetroSet_UI.Controls.MetroSetTextBox()
        Me.MetroGrid1 = New Wisder.W3Common.WMetroControl.Controls.MetroGrid()
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MetroSetLabel2 = New MetroSet_UI.Controls.MetroSetLabel()
        Me.Qauntidade = New MetroSet_UI.Controls.MetroSetTextBox()
        Me.MetroSetLabel3 = New MetroSet_UI.Controls.MetroSetLabel()
        Me.NotaFiscal = New MetroSet_UI.Controls.MetroSetTextBox()
        Me.MidiaFisica = New MetroSet_UI.Controls.MetroSetCheckBox()
        Me.EmUso = New MetroSet_UI.Controls.MetroSetCheckBox()
        Me.AddNewlc = New MetroSet_UI.Controls.MetroDefaultSetButton()
        Me.MetroButton2 = New MetroSet_UI.Controls.MetroDefaultSetButton()
        Me.MetroButton3 = New MetroSet_UI.Controls.MetroDefaultSetButton()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        CType(Me.MetroGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MetroSetLabel97
        '
        Me.MetroSetLabel97.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.MetroSetLabel97.Location = New System.Drawing.Point(24, 91)
        Me.MetroSetLabel97.Name = "MetroSetLabel97"
        Me.MetroSetLabel97.Size = New System.Drawing.Size(238, 24)
        Me.MetroSetLabel97.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroSetLabel97.StyleManager = Nothing
        Me.MetroSetLabel97.TabIndex = 37
        Me.MetroSetLabel97.Text = "Escolha uma licença existente:"
        Me.MetroSetLabel97.ThemeAuthor = "Narwin"
        Me.MetroSetLabel97.ThemeName = "MetroDark"
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
        Me.MetroComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.MetroComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.MetroComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.MetroComboBox1.FormattingEnabled = True
        Me.MetroComboBox1.ItemHeight = 20
        Me.MetroComboBox1.Location = New System.Drawing.Point(24, 118)
        Me.MetroComboBox1.Name = "MetroComboBox1"
        Me.MetroComboBox1.SelectedItemBackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.MetroComboBox1.SelectedItemForeColor = System.Drawing.Color.White
        Me.MetroComboBox1.Size = New System.Drawing.Size(748, 26)
        Me.MetroComboBox1.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroComboBox1.StyleManager = Nothing
        Me.MetroComboBox1.TabIndex = 38
        Me.MetroComboBox1.ThemeAuthor = "Narwin"
        Me.MetroComboBox1.ThemeName = "MetroDark"
        '
        'MetroSetLabel51
        '
        Me.MetroSetLabel51.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroSetLabel51.Location = New System.Drawing.Point(14, 183)
        Me.MetroSetLabel51.Name = "MetroSetLabel51"
        Me.MetroSetLabel51.Size = New System.Drawing.Size(83, 23)
        Me.MetroSetLabel51.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroSetLabel51.StyleManager = Nothing
        Me.MetroSetLabel51.TabIndex = 40
        Me.MetroSetLabel51.Text = "Descrição:"
        Me.MetroSetLabel51.ThemeAuthor = "Narwin"
        Me.MetroSetLabel51.ThemeName = "MetroDark"
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
        Me.Descricao.Location = New System.Drawing.Point(103, 183)
        Me.Descricao.MaxLength = 32767
        Me.Descricao.Multiline = False
        Me.Descricao.Name = "Descricao"
        Me.Descricao.ReadOnly = False
        Me.Descricao.Size = New System.Drawing.Size(669, 28)
        Me.Descricao.Style = MetroSet_UI.Design.Style.Dark
        Me.Descricao.StyleManager = Nothing
        Me.Descricao.TabIndex = 39
        Me.Descricao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Descricao.ThemeAuthor = "Narwin"
        Me.Descricao.ThemeName = "MetroDark"
        Me.Descricao.UseSystemPasswordChar = False
        Me.Descricao.WatermarkText = ""
        '
        'MetroSetLabel1
        '
        Me.MetroSetLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroSetLabel1.Location = New System.Drawing.Point(35, 238)
        Me.MetroSetLabel1.Name = "MetroSetLabel1"
        Me.MetroSetLabel1.Size = New System.Drawing.Size(52, 23)
        Me.MetroSetLabel1.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroSetLabel1.StyleManager = Nothing
        Me.MetroSetLabel1.TabIndex = 42
        Me.MetroSetLabel1.Text = "Chave:"
        Me.MetroSetLabel1.ThemeAuthor = "Narwin"
        Me.MetroSetLabel1.ThemeName = "MetroDark"
        '
        'Chave
        '
        Me.Chave.AutoCompleteCustomSource = Nothing
        Me.Chave.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.Chave.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.Chave.BorderColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.Chave.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Chave.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Chave.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Chave.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Chave.HoverColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Chave.Image = Nothing
        Me.Chave.Lines = Nothing
        Me.Chave.Location = New System.Drawing.Point(103, 238)
        Me.Chave.MaxLength = 32767
        Me.Chave.Multiline = False
        Me.Chave.Name = "Chave"
        Me.Chave.ReadOnly = False
        Me.Chave.Size = New System.Drawing.Size(669, 28)
        Me.Chave.Style = MetroSet_UI.Design.Style.Dark
        Me.Chave.StyleManager = Nothing
        Me.Chave.TabIndex = 41
        Me.Chave.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Chave.ThemeAuthor = "Narwin"
        Me.Chave.ThemeName = "MetroDark"
        Me.Chave.UseSystemPasswordChar = False
        Me.Chave.WatermarkText = ""
        '
        'MetroGrid1
        '
        Me.MetroGrid1.AllowUserToAddRows = False
        Me.MetroGrid1.AllowUserToDeleteRows = False
        Me.MetroGrid1.AllowUserToResizeRows = False
        Me.MetroGrid1.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        Me.MetroGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.MetroGrid1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.MetroGrid1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MetroGrid1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.MetroGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MetroGrid1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column16, Me.Column17, Me.Column18, Me.Column9})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.MetroGrid1.DefaultCellStyle = DataGridViewCellStyle2
        Me.MetroGrid1.EnableHeadersVisualStyles = False
        Me.MetroGrid1.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.MetroGrid1.GridColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        Me.MetroGrid1.Location = New System.Drawing.Point(24, 294)
        Me.MetroGrid1.Name = "MetroGrid1"
        Me.MetroGrid1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MetroGrid1.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.MetroGrid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.MetroGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.MetroGrid1.Size = New System.Drawing.Size(748, 174)
        Me.MetroGrid1.TabIndex = 43
        Me.MetroGrid1.Theme = Wisder.W3Common.WMetroControl.MetroThemeStyle.Dark
        '
        'Column16
        '
        Me.Column16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column16.HeaderText = "Patrimônio"
        Me.Column16.Name = "Column16"
        Me.Column16.ReadOnly = True
        Me.Column16.Width = 86
        '
        'Column17
        '
        Me.Column17.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column17.HeaderText = "Windows "
        Me.Column17.Name = "Column17"
        Me.Column17.ReadOnly = True
        Me.Column17.Width = 82
        '
        'Column18
        '
        Me.Column18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column18.HeaderText = "Descrição"
        Me.Column18.Name = "Column18"
        Me.Column18.ReadOnly = True
        Me.Column18.Width = 79
        '
        'Column9
        '
        Me.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column9.HeaderText = "Usuário atual"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 99
        '
        'MetroSetLabel2
        '
        Me.MetroSetLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroSetLabel2.Location = New System.Drawing.Point(35, 491)
        Me.MetroSetLabel2.Name = "MetroSetLabel2"
        Me.MetroSetLabel2.Size = New System.Drawing.Size(91, 23)
        Me.MetroSetLabel2.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroSetLabel2.StyleManager = Nothing
        Me.MetroSetLabel2.TabIndex = 45
        Me.MetroSetLabel2.Text = "Quantidade:"
        Me.MetroSetLabel2.ThemeAuthor = "Narwin"
        Me.MetroSetLabel2.ThemeName = "MetroDark"
        '
        'Qauntidade
        '
        Me.Qauntidade.AutoCompleteCustomSource = Nothing
        Me.Qauntidade.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.Qauntidade.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.Qauntidade.BorderColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.Qauntidade.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Qauntidade.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Qauntidade.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Qauntidade.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Qauntidade.HoverColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.Qauntidade.Image = Nothing
        Me.Qauntidade.Lines = Nothing
        Me.Qauntidade.Location = New System.Drawing.Point(132, 491)
        Me.Qauntidade.MaxLength = 32767
        Me.Qauntidade.Multiline = False
        Me.Qauntidade.Name = "Qauntidade"
        Me.Qauntidade.ReadOnly = False
        Me.Qauntidade.Size = New System.Drawing.Size(640, 28)
        Me.Qauntidade.Style = MetroSet_UI.Design.Style.Dark
        Me.Qauntidade.StyleManager = Nothing
        Me.Qauntidade.TabIndex = 44
        Me.Qauntidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Qauntidade.ThemeAuthor = "Narwin"
        Me.Qauntidade.ThemeName = "MetroDark"
        Me.Qauntidade.UseSystemPasswordChar = False
        Me.Qauntidade.WatermarkText = ""
        '
        'MetroSetLabel3
        '
        Me.MetroSetLabel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroSetLabel3.Location = New System.Drawing.Point(35, 543)
        Me.MetroSetLabel3.Name = "MetroSetLabel3"
        Me.MetroSetLabel3.Size = New System.Drawing.Size(91, 23)
        Me.MetroSetLabel3.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroSetLabel3.StyleManager = Nothing
        Me.MetroSetLabel3.TabIndex = 47
        Me.MetroSetLabel3.Text = "Nota fiscal:"
        Me.MetroSetLabel3.ThemeAuthor = "Narwin"
        Me.MetroSetLabel3.ThemeName = "MetroDark"
        '
        'NotaFiscal
        '
        Me.NotaFiscal.AutoCompleteCustomSource = Nothing
        Me.NotaFiscal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.NotaFiscal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.NotaFiscal.BorderColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.NotaFiscal.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.NotaFiscal.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.NotaFiscal.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.NotaFiscal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.NotaFiscal.HoverColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.NotaFiscal.Image = Nothing
        Me.NotaFiscal.Lines = Nothing
        Me.NotaFiscal.Location = New System.Drawing.Point(132, 543)
        Me.NotaFiscal.MaxLength = 32767
        Me.NotaFiscal.Multiline = False
        Me.NotaFiscal.Name = "NotaFiscal"
        Me.NotaFiscal.ReadOnly = False
        Me.NotaFiscal.Size = New System.Drawing.Size(426, 28)
        Me.NotaFiscal.Style = MetroSet_UI.Design.Style.Dark
        Me.NotaFiscal.StyleManager = Nothing
        Me.NotaFiscal.TabIndex = 46
        Me.NotaFiscal.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.NotaFiscal.ThemeAuthor = "Narwin"
        Me.NotaFiscal.ThemeName = "MetroDark"
        Me.NotaFiscal.UseSystemPasswordChar = False
        Me.NotaFiscal.WatermarkText = ""
        '
        'MidiaFisica
        '
        Me.MidiaFisica.BackColor = System.Drawing.Color.Transparent
        Me.MidiaFisica.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.MidiaFisica.BorderColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(155, Byte), Integer))
        Me.MidiaFisica.Checked = False
        Me.MidiaFisica.CheckSignColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.MidiaFisica.CheckState = MetroSet_UI.Enums.CheckState.Unchecked
        Me.MidiaFisica.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MidiaFisica.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.MidiaFisica.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MidiaFisica.Location = New System.Drawing.Point(258, 595)
        Me.MidiaFisica.Name = "MidiaFisica"
        Me.MidiaFisica.SignStyle = MetroSet_UI.Enums.SignStyle.Sign
        Me.MidiaFisica.Size = New System.Drawing.Size(115, 16)
        Me.MidiaFisica.Style = MetroSet_UI.Design.Style.Dark
        Me.MidiaFisica.StyleManager = Nothing
        Me.MidiaFisica.TabIndex = 49
        Me.MidiaFisica.Text = "Midia Fisica"
        Me.MidiaFisica.ThemeAuthor = "Narwin"
        Me.MidiaFisica.ThemeName = "MetroDark"
        '
        'EmUso
        '
        Me.EmUso.BackColor = System.Drawing.Color.Transparent
        Me.EmUso.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.EmUso.BorderColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(155, Byte), Integer))
        Me.EmUso.Checked = False
        Me.EmUso.CheckSignColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.EmUso.CheckState = MetroSet_UI.Enums.CheckState.Unchecked
        Me.EmUso.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EmUso.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.EmUso.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.EmUso.Location = New System.Drawing.Point(132, 595)
        Me.EmUso.Name = "EmUso"
        Me.EmUso.SignStyle = MetroSet_UI.Enums.SignStyle.Sign
        Me.EmUso.Size = New System.Drawing.Size(78, 16)
        Me.EmUso.Style = MetroSet_UI.Design.Style.Dark
        Me.EmUso.StyleManager = Nothing
        Me.EmUso.TabIndex = 48
        Me.EmUso.Text = "Em Uso"
        Me.EmUso.ThemeAuthor = "Narwin"
        Me.EmUso.ThemeName = "MetroDark"
        '
        'AddNewlc
        '
        Me.AddNewlc.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.AddNewlc.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.AddNewlc.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.AddNewlc.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.AddNewlc.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.AddNewlc.HoverColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.AddNewlc.HoverTextColor = System.Drawing.Color.White
        Me.AddNewlc.Location = New System.Drawing.Point(623, 621)
        Me.AddNewlc.Name = "AddNewlc"
        Me.AddNewlc.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.AddNewlc.NormalColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.AddNewlc.NormalTextColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.AddNewlc.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.AddNewlc.PressColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.AddNewlc.PressTextColor = System.Drawing.Color.White
        Me.AddNewlc.Size = New System.Drawing.Size(149, 28)
        Me.AddNewlc.Style = MetroSet_UI.Design.Style.Dark
        Me.AddNewlc.StyleManager = Nothing
        Me.AddNewlc.TabIndex = 50
        Me.AddNewlc.Text = "Salvar alterações"
        Me.AddNewlc.ThemeAuthor = "Narwin"
        Me.AddNewlc.ThemeName = "MetroDark"
        '
        'MetroButton2
        '
        Me.MetroButton2.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.MetroButton2.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroButton2.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroButton2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroButton2.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroButton2.HoverColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroButton2.HoverTextColor = System.Drawing.Color.White
        Me.MetroButton2.Location = New System.Drawing.Point(452, 621)
        Me.MetroButton2.Name = "MetroButton2"
        Me.MetroButton2.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MetroButton2.NormalColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.MetroButton2.NormalTextColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MetroButton2.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroButton2.PressColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroButton2.PressTextColor = System.Drawing.Color.White
        Me.MetroButton2.Size = New System.Drawing.Size(149, 28)
        Me.MetroButton2.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroButton2.StyleManager = Nothing
        Me.MetroButton2.TabIndex = 51
        Me.MetroButton2.Text = "Cancelar"
        Me.MetroButton2.ThemeAuthor = "Narwin"
        Me.MetroButton2.ThemeName = "MetroDark"
        '
        'MetroButton3
        '
        Me.MetroButton3.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.MetroButton3.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroButton3.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroButton3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroButton3.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroButton3.HoverColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroButton3.HoverTextColor = System.Drawing.Color.White
        Me.MetroButton3.Location = New System.Drawing.Point(564, 543)
        Me.MetroButton3.Name = "MetroButton3"
        Me.MetroButton3.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MetroButton3.NormalColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.MetroButton3.NormalTextColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MetroButton3.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroButton3.PressColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroButton3.PressTextColor = System.Drawing.Color.White
        Me.MetroButton3.Size = New System.Drawing.Size(208, 28)
        Me.MetroButton3.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroButton3.StyleManager = Nothing
        Me.MetroButton3.TabIndex = 53
        Me.MetroButton3.Text = "Anexar Nota"
        Me.MetroButton3.ThemeAuthor = "Narwin"
        Me.MetroButton3.ThemeName = "MetroDark"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'EditLicense
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(787, 664)
        Me.Controls.Add(Me.MetroButton3)
        Me.Controls.Add(Me.MetroButton2)
        Me.Controls.Add(Me.AddNewlc)
        Me.Controls.Add(Me.MidiaFisica)
        Me.Controls.Add(Me.EmUso)
        Me.Controls.Add(Me.MetroSetLabel3)
        Me.Controls.Add(Me.NotaFiscal)
        Me.Controls.Add(Me.MetroSetLabel2)
        Me.Controls.Add(Me.Qauntidade)
        Me.Controls.Add(Me.MetroGrid1)
        Me.Controls.Add(Me.MetroSetLabel1)
        Me.Controls.Add(Me.Chave)
        Me.Controls.Add(Me.MetroSetLabel51)
        Me.Controls.Add(Me.Descricao)
        Me.Controls.Add(Me.MetroComboBox1)
        Me.Controls.Add(Me.MetroSetLabel97)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "EditLicense"
        Me.Style = MetroSet_UI.Design.Style.Dark
        Me.Text = "Editar licença"
        Me.TextColor = System.Drawing.Color.White
        Me.ThemeName = "MetroDark"
        CType(Me.MetroGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MetroSetLabel97 As MetroSet_UI.Controls.MetroSetLabel
    Friend WithEvents MetroComboBox1 As MetroSet_UI.Controls.MetroSetComboBox
    Friend WithEvents MetroSetLabel51 As MetroSet_UI.Controls.MetroSetLabel
    Friend WithEvents Descricao As MetroSet_UI.Controls.MetroSetTextBox
    Friend WithEvents MetroSetLabel1 As MetroSet_UI.Controls.MetroSetLabel
    Friend WithEvents Chave As MetroSet_UI.Controls.MetroSetTextBox
    Friend WithEvents MetroGrid1 As Wisder.W3Common.WMetroControl.Controls.MetroGrid
    Friend WithEvents Column16 As DataGridViewTextBoxColumn
    Friend WithEvents Column17 As DataGridViewTextBoxColumn
    Friend WithEvents Column18 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents MetroSetLabel2 As MetroSet_UI.Controls.MetroSetLabel
    Friend WithEvents Qauntidade As MetroSet_UI.Controls.MetroSetTextBox
    Friend WithEvents MetroSetLabel3 As MetroSet_UI.Controls.MetroSetLabel
    Friend WithEvents NotaFiscal As MetroSet_UI.Controls.MetroSetTextBox
    Friend WithEvents MidiaFisica As MetroSet_UI.Controls.MetroSetCheckBox
    Friend WithEvents EmUso As MetroSet_UI.Controls.MetroSetCheckBox
    Friend WithEvents AddNewlc As MetroSet_UI.Controls.MetroDefaultSetButton
    Friend WithEvents MetroButton2 As MetroSet_UI.Controls.MetroDefaultSetButton
    Friend WithEvents MetroButton3 As MetroSet_UI.Controls.MetroDefaultSetButton
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
