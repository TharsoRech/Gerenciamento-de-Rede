Public Class EditLicense
    Public SelectMetrogrid As Wisder.W3Common.WMetroControl.Controls.MetroGrid
    Public SelectGrid As DataGridViewRow


    Private Sub MetroComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MetroComboBox1.SelectedIndexChanged
        Try
            For Each x1 As Licença In Form1.Database.Licenças
                If MetroComboBox1.Text.Contains(x1.Chave) Then
                    Descricao.Text = x1.Descrição
                    Qauntidade.Text = x1.Quantidade
                    NotaFiscal.Text = IO.Path.GetFullPath(x1.NotaFiscal)
                    Chave.Text = x1.Chave
                    MetroGrid1.Rows.Clear()
                    For Each x2 As Patrimonio In x1.PatrimonioInstalados
                        MetroGrid1.Rows.Add({x2.Patrimonio, x2.Windows, x2.Descrição, x2.UsuárioAtual})
                    Next
                    EmUso.Checked = x1.EmUso
                    MidiaFisica.Checked = x1.MidiaFisica
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AddNewlc_Click(sender As Object, e As EventArgs) Handles AddNewlc.Click
        Try
            Dim mf As String = ""
            If MidiaFisica.Checked Then
                mf = "True"
            Else
                mf = "False"
            End If
            If Me.Text.ToLower.Contains("Alterar".ToLower) Then
                SelectGrid.Cells(1).Value = Chave.Text
                SelectGrid.Cells(2).Value = NotaFiscal.Text
                SelectGrid.Cells(3).Value = mf
                Me.Close()
            Else
                SelectMetrogrid.Rows.Add({Descricao.Text, Chave.Text, NotaFiscal.Text, mf, DateAndTime.Now.ToString})
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroButton3_Click(sender As Object, e As EventArgs) Handles MetroButton3.Click
        Try
            If OpenFileDialog1.ShowDialog <> DialogResult.Cancel Then
                NotaFiscal.Text = OpenFileDialog1.FileName
            End If
        Catch ex As Exception

        End Try
    End Sub
    Function After(value As String, a As String) As String
        ' Get index of argument and return substring after its position.
        Dim posA As Integer = value.LastIndexOf(a)
        If posA = -1 Then
            Return ""
        End If
        Dim adjustedPosA As Integer = posA + a.Length
        If adjustedPosA >= value.Length Then
            Return ""
        End If
        Return value.Substring(adjustedPosA)
    End Function
End Class