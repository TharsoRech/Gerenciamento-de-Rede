<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VNCpc
    Inherits System.Windows.Forms.UserControl

    'O UserControl substitui o descarte para limpar a lista de componentes.
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VNCpc))
        Me.NOME = New System.Windows.Forms.Label()
        Me.AcessadoEM = New System.Windows.Forms.Label()
        Me.Ip = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AtualizarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoverToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MetroDefaultSetButton60 = New MetroSet_UI.Controls.MetroDefaultSetButton()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'NOME
        '
        Me.NOME.AutoSize = True
        Me.NOME.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NOME.ForeColor = System.Drawing.Color.White
        Me.NOME.Location = New System.Drawing.Point(0, 0)
        Me.NOME.Name = "NOME"
        Me.NOME.Size = New System.Drawing.Size(77, 25)
        Me.NOME.TabIndex = 0
        Me.NOME.Text = "Label1"
        '
        'AcessadoEM
        '
        Me.AcessadoEM.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.AcessadoEM.AutoSize = True
        Me.AcessadoEM.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AcessadoEM.ForeColor = System.Drawing.Color.White
        Me.AcessadoEM.Location = New System.Drawing.Point(3, 68)
        Me.AcessadoEM.Name = "AcessadoEM"
        Me.AcessadoEM.Size = New System.Drawing.Size(66, 24)
        Me.AcessadoEM.TabIndex = 1
        Me.AcessadoEM.Text = "Label1"
        Me.AcessadoEM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Ip
        '
        Me.Ip.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Ip.AutoSize = True
        Me.Ip.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ip.ForeColor = System.Drawing.Color.White
        Me.Ip.Location = New System.Drawing.Point(3, 35)
        Me.Ip.Name = "Ip"
        Me.Ip.Size = New System.Drawing.Size(66, 24)
        Me.Ip.TabIndex = 2
        Me.Ip.Text = "Label1"
        Me.Ip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AtualizarToolStripMenuItem, Me.RemoverToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(122, 48)
        '
        'AtualizarToolStripMenuItem
        '
        Me.AtualizarToolStripMenuItem.Image = CType(resources.GetObject("AtualizarToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AtualizarToolStripMenuItem.Name = "AtualizarToolStripMenuItem"
        Me.AtualizarToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
        Me.AtualizarToolStripMenuItem.Text = "Atualizar"
        '
        'RemoverToolStripMenuItem
        '
        Me.RemoverToolStripMenuItem.Image = CType(resources.GetObject("RemoverToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RemoverToolStripMenuItem.Name = "RemoverToolStripMenuItem"
        Me.RemoverToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
        Me.RemoverToolStripMenuItem.Text = "Remover"
        '
        'MetroDefaultSetButton60
        '
        Me.MetroDefaultSetButton60.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.MetroDefaultSetButton60.DisabledBorderColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton60.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.MetroDefaultSetButton60.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.MetroDefaultSetButton60.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton60.HoverColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.MetroDefaultSetButton60.HoverTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton60.Location = New System.Drawing.Point(232, 68)
        Me.MetroDefaultSetButton60.Name = "MetroDefaultSetButton60"
        Me.MetroDefaultSetButton60.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MetroDefaultSetButton60.NormalColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.MetroDefaultSetButton60.NormalTextColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.MetroDefaultSetButton60.PressBorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton60.PressColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.MetroDefaultSetButton60.PressTextColor = System.Drawing.Color.White
        Me.MetroDefaultSetButton60.Size = New System.Drawing.Size(92, 18)
        Me.MetroDefaultSetButton60.Style = MetroSet_UI.Design.Style.Dark
        Me.MetroDefaultSetButton60.StyleManager = Nothing
        Me.MetroDefaultSetButton60.TabIndex = 5
        Me.MetroDefaultSetButton60.Text = "Conectar"
        Me.MetroDefaultSetButton60.ThemeAuthor = "Narwin"
        Me.MetroDefaultSetButton60.ThemeName = "MetroDark"
        '
        'VNCpc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Controls.Add(Me.MetroDefaultSetButton60)
        Me.Controls.Add(Me.Ip)
        Me.Controls.Add(Me.AcessadoEM)
        Me.Controls.Add(Me.NOME)
        Me.ForeColor = System.Drawing.Color.Black
        Me.Name = "VNCpc"
        Me.Size = New System.Drawing.Size(327, 92)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents NOME As Label
    Public WithEvents AcessadoEM As Label
    Public WithEvents Ip As Label
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents AtualizarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RemoverToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MetroDefaultSetButton60 As MetroSet_UI.Controls.MetroDefaultSetButton
End Class
