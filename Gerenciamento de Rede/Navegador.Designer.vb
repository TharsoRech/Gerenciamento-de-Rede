<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Navegador
    Inherits MetroSet_UI.Forms.MetroSetForm

    'Descartar substituições de formulário para limpar a lista de componentes.
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

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Navegador))
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.MetroDefaultSetButton3 = New MetroSet_UI.Controls.MetroDefaultSetButton()
        Me.SuspendLayout()
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(2, 60)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.ScriptErrorsSuppressed = True
        Me.WebBrowser1.Size = New System.Drawing.Size(875, 630)
        Me.WebBrowser1.TabIndex = 0
        '
        'MetroDefaultSetButton3
        '
        Me.MetroDefaultSetButton3.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MetroDefaultSetButton3.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(155, Byte), Integer))
        Me.MetroDefaultSetButton3.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        Me.MetroDefaultSetButton3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroDefaultSetButton3.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.MetroDefaultSetButton3.HoverColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.MetroDefaultSetButton3.HoverTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton3.Location = New System.Drawing.Point(703, 1)
        Me.MetroDefaultSetButton3.Name = "MetroDefaultSetButton3"
        Me.MetroDefaultSetButton3.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MetroDefaultSetButton3.NormalColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.MetroDefaultSetButton3.NormalTextColor = System.Drawing.Color.Black
        Me.MetroDefaultSetButton3.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.MetroDefaultSetButton3.PressColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.MetroDefaultSetButton3.PressTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton3.Size = New System.Drawing.Size(160, 29)
        Me.MetroDefaultSetButton3.Style = MetroSet_UI.Design.Style.Light
        Me.MetroDefaultSetButton3.StyleManager = Nothing
        Me.MetroDefaultSetButton3.TabIndex = 3
        Me.MetroDefaultSetButton3.Text = "Sair"
        Me.MetroDefaultSetButton3.ThemeAuthor = "Narwin"
        Me.MetroDefaultSetButton3.ThemeName = "MetroLite"
        Me.MetroDefaultSetButton3.UseWaitCursor = True
        '
        'Navegador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(879, 692)
        Me.Controls.Add(Me.MetroDefaultSetButton3)
        Me.Controls.Add(Me.WebBrowser1)
        Me.DisplayHeader = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Navegador"
        Me.Padding = New System.Windows.Forms.Padding(2, 60, 2, 2)
        Me.ShowHeader = True
        Me.ShowLeftRect = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NAVEGADOR"
        Me.TextColor = System.Drawing.Color.White
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents WebBrowser1 As WebBrowser
    Friend WithEvents MetroDefaultSetButton3 As MetroSet_UI.Controls.MetroDefaultSetButton
End Class
