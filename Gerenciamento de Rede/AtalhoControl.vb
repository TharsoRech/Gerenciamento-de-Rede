Public Class AtalhoControl
    Private Sub Nome_Click(sender As Object, e As EventArgs) Handles Me.Click, Nome.Click
        Try
            For Each x1 As DotNetChromeTabs.ChromeTabControl.TabPage In Form1.GRbrowser1.ChromeTabControl1.TabPages
                If x1.Title = Nome.Text Then
                    Form1.GRbrowser1.ChromeTabControl1.TabIndex = x1.TabIndex
                    Exit Sub
                End If
            Next
            For Each x2 As Favorito In Form1.Database.Favoritos
                If x2.Nome = Nome.Text Then
                    Dim newtb As New DotNetChromeTabs.ChromeTabControl.TabPage
                    newtb.CanClose = True
                    newtb.SingleInstance = False
                    newtb.Title = x2.Nome
                    Form1.GRbrowser1.ChromeTabControl1.TabPages.Add(newtb)
                    Dim newbr As New Browser1
                    newbr.Fav = x2
                    newbr.Dock = DockStyle.Fill
                    newtb.Controls.Add(newbr)
                    newbr.GeckoWebBrowser1.Navigate(x2.Url)
                    Form1.GRbrowser1.ChromeTabControl1.TabIndex = newtb.TabIndex
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RemoverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoverToolStripMenuItem.Click
        Try
            For Each x2 As Favorito In Form1.Database.Favoritos
                If x2.Nome = Nome.Text Then
                    Form1.Database.Favoritos.Remove(x2)
                    Form1.save()
                    Form1.GRbrowser1.Atalhos.Controls.Remove(Me)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub EditarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditarToolStripMenuItem.Click
        Try
            AddAtalho.Show()
            AddAtalho.AddNewlc.Text = "Salvar"
            AddAtalho.Nome.Enabled = False
            For Each x2 As Favorito In Form1.Database.Favoritos
                If x2.Nome = Nome.Text Then
                    AddAtalho.Nome.Text = x2.Nome
                    AddAtalho.url.Text = x2.Url
                    AddAtalho.Usuario.Text = x2.Login
                    AddAtalho.Senha.Text = x2.Senha
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AbrirPeloBrowserOpcionalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirPeloBrowserOpcionalToolStripMenuItem.Click
        Try
            For Each x2 As Favorito In Form1.Database.Favoritos
                If x2.Nome = Nome.Text Then
                    Navegador.Show()
                    Navegador.WebBrowser1.Navigate(x2.Url)
                    Navegador.Fav = x2
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub
End Class
