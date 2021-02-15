Public Class AdicionarUsuário
    Private Sub MetroDefaultSetButton1_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton1.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton4_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton4.Click
        Try
            Dim aduser As ADUser = New ADUser
            aduser.Name = Nome.Text
            aduser.UserName = Login.Text
            aduser.Description = Descricao.Text
            aduser.Password = Senha.Text
            ADHelper.AddUser(aduser)
            MetroFramework.MetroMessageBox.Show(Me, "Usuário adicionado com sucesso", "adiçao de usuário concluida", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception

        End Try
    End Sub
End Class