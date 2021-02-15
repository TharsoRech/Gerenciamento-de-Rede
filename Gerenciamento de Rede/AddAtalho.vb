Imports MetroFramework

Public Class AddAtalho
    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AddNewlc_Click(sender As Object, e As EventArgs) Handles AddNewlc.Click
        Try

            If AddNewlc.Text.ToLower.Contains("Salvar".ToLower) = True Then
                For Each x2 As Favorito In Form1.Database.Favoritos
                    If x2.Nome = Nome.Text Then
                        x2.Url = url.Text
                        x2.Login = Usuario.Text
                        x2.Senha = Senha.Text
                        Form1.save()
                        MetroMessageBox.Show(Me, "Atalho atualizado com sucesso", "Muito Bem!!!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                    End If
                Next
            Else
                Dim newfav As New Favorito
                newfav.Nome = Nome.Text
                newfav.Url = url.Text
                newfav.Login = Usuario.Text
                newfav.Senha = Senha.Text
                If Form1.Database.Favoritos Is Nothing Then
                    Form1.Database.Favoritos = New List(Of Favorito)
                End If
                Form1.Database.Favoritos.Add(newfav)
                    Form1.save()
                    Form1.GRbrowser1.Atalhos.Controls.Clear()
                    For Each x2 As Favorito In Form1.Database.Favoritos
                        Dim newat As New AtalhoControl
                        newat.Nome.Text = x2.Nome
                        Form1.GRbrowser1.Atalhos.Controls.Add(newat)
                    Next
                    Me.Close()
                    MetroMessageBox.Show(Me, "Atalho adicionado com sucesso", "Muito Bem!!!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
        Catch ex As Exception
            MsgBox(ex.Message & ex.StackTrace)
        End Try
    End Sub
End Class