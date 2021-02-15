<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AtalhoControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AtalhoControl))
        Me.MetroContextMenu1 = New Wisder.W3Common.WMetroControl.Controls.MetroContextMenu(Me.components)
        Me.AbrirPeloBrowserOpcionalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoverToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Nome = New System.Windows.Forms.Label()
        Me.MetroContextMenu1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MetroContextMenu1
        '
        Me.MetroContextMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbrirPeloBrowserOpcionalToolStripMenuItem, Me.EditarToolStripMenuItem, Me.RemoverToolStripMenuItem})
        Me.MetroContextMenu1.Name = "MetroContextMenu1"
        Me.MetroContextMenu1.Size = New System.Drawing.Size(155, 70)
        '
        'AbrirPeloBrowserOpcionalToolStripMenuItem
        '
        Me.AbrirPeloBrowserOpcionalToolStripMenuItem.Image = CType(resources.GetObject("AbrirPeloBrowserOpcionalToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AbrirPeloBrowserOpcionalToolStripMenuItem.Name = "AbrirPeloBrowserOpcionalToolStripMenuItem"
        Me.AbrirPeloBrowserOpcionalToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.AbrirPeloBrowserOpcionalToolStripMenuItem.Text = "Abrir usando IE"
        '
        'EditarToolStripMenuItem
        '
        Me.EditarToolStripMenuItem.Image = CType(resources.GetObject("EditarToolStripMenuItem.Image"), System.Drawing.Image)
        Me.EditarToolStripMenuItem.Name = "EditarToolStripMenuItem"
        Me.EditarToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.EditarToolStripMenuItem.Text = "Editar"
        '
        'RemoverToolStripMenuItem
        '
        Me.RemoverToolStripMenuItem.Image = CType(resources.GetObject("RemoverToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RemoverToolStripMenuItem.Name = "RemoverToolStripMenuItem"
        Me.RemoverToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.RemoverToolStripMenuItem.Text = "Remover"
        '
        'Nome
        '
        Me.Nome.AutoSize = True
        Me.Nome.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Nome.Font = New System.Drawing.Font("Century", 10.0!)
        Me.Nome.ForeColor = System.Drawing.Color.White
        Me.Nome.Location = New System.Drawing.Point(0, 0)
        Me.Nome.Name = "Nome"
        Me.Nome.Size = New System.Drawing.Size(52, 17)
        Me.Nome.TabIndex = 2
        Me.Nome.Text = "Label1"
        '
        'AtalhoControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.ContextMenuStrip = Me.MetroContextMenu1
        Me.Controls.Add(Me.Nome)
        Me.Name = "AtalhoControl"
        Me.Size = New System.Drawing.Size(146, 25)
        Me.MetroContextMenu1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MetroContextMenu1 As Wisder.W3Common.WMetroControl.Controls.MetroContextMenu
    Friend WithEvents EditarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RemoverToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Nome As Label
    Friend WithEvents AbrirPeloBrowserOpcionalToolStripMenuItem As ToolStripMenuItem
End Class
