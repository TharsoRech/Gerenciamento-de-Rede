Imports MetroFramework

Public Class VNCpc
    Public rd As VncSharp.RemoteDesktop
    Private Sub AtualizarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AtualizarToolStripMenuItem.Click
        Try
            Form1.ReloadConexoesVNC()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RemoverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoverToolStripMenuItem.Click
        Try
            For Each x20 As VNCLast In Form1.Database.ConexoesVNC
                If x20.Nome = NOME.Text Then
                    Form1.Database.ConexoesVNC.Remove(x20)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton60_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton60.Click
        Try
            Dim newtab As New MetroSet_UI.Child.MetroSetTabPage
            newtab.Text = NOME.Text
            newtab.Style = MetroSet_UI.Design.Style.Dark
            Dim newvnc As New VncControl_
            newvnc.Spymode.Style = MetroSet_UI.Design.Style.Light
            newvnc.RD.Enabled = False
            rd = newvnc.RD
            newvnc.Host1 = NOME.Text
            newvnc.Dock = DockStyle.Fill
            newtab.Controls.Add(newvnc)
            Form1.Vnctab.TabPages.Add(newtab)
            rd.Connect(RemoveWhitespace(Ip.Text), False, True)
        Catch ex As Exception
            MetroMessageBox.Show(Form1, ex.Message & "," & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            For Each x1 As MetroSet_UI.Child.MetroSetTabPage In Form1.Vnctab.TabPages
                If NOME.Text.ToLower.Contains(x1.Text.ToLower) Then
                    Form1.Vnctab.TabPages.Remove(x1)
                End If
            Next
        End Try
    End Sub
    Function RemoveWhitespace(fullString As String) As String
        Return New String(fullString.Where(Function(x) Not Char.IsWhiteSpace(x)).ToArray())
    End Function
End Class
