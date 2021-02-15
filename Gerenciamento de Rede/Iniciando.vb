Public Class Iniciando
    Private Sub FButton1_Click(sender As Object, e As EventArgs) Handles FButton1.Click
        Try
            Process.GetCurrentProcess.Kill()
        Catch ex As Exception

        End Try
    End Sub
End Class