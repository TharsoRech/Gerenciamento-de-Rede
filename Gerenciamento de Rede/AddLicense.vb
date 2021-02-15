Imports MetroFramework

Public Class AddLicense
    Private Sub MetroButton3_Click(sender As Object, e As EventArgs) Handles MetroButton3.Click

        Try
            If OpenFileDialog1.ShowDialog <> DialogResult.Cancel Then
                NotaFiscal.Text = OpenFileDialog1.FileName
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles AddNewlc.Click
        Try
            For Each x1 As Licença In Form1.Database.Licenças
                If x1.Descrição = Descricao.Text And x1.Chave = Chave.Text Then
                    MetroMessageBox.Show(Me, "Erro ao adicionar nova ação ,Licença ja adicionada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Next
            Dim newl As New Licença
            newl.Descrição = Descricao.Text
            newl.Chave = Chave.Text
            newl.Adicionadoem = DateAndTime.Now.ToString
            newl.EmUso = EmUso.Checked
            newl.Quantidade = Qauntidade.Text
            newl.MidiaFisica = MidiaFisica.Checked
            For Each x10 As DataGridViewRow In MetroGrid1.Rows
                For Each x1 As Patrimonio In Form1.Database.Patrimonios
                    If x1.Patrimonio = x10.Cells(3).Value Then
                        newl.PatrimonioInstalados.Add(x1)
                    End If
                Next
            Next
            Dim evidenciacaminho = NotaFiscal.Text
            If IO.Directory.Exists(My.Settings.DatabaseLocation & "\Licenças\" & Descricao.Text) Then
                Dim count As Integer = 0
                Dim fileNameOnly As String = IO.Path.GetFileNameWithoutExtension(evidenciacaminho)
                Dim extension As String = IO.Path.GetExtension(evidenciacaminho)
                Dim path As String = My.Settings.DatabaseLocation & "\Licenças\" & Descricao.Text
                Dim newFullPath As String = My.Settings.DatabaseLocation & "\Licenças\" & Descricao.Text & "\" & IO.Path.GetFileName(evidenciacaminho)
                While IO.File.Exists(newFullPath)
                    Dim tempFileName As String = fileNameOnly & "(" & count + 1 & ")"
                    newFullPath = IO.Path.Combine(path, (tempFileName + extension))
                    count = count + 1
                End While
                If IO.File.Exists(evidenciacaminho) Then
                    IO.File.Copy(evidenciacaminho, newFullPath, True)
                    newl.NotaFiscal = IO.Path.GetFullPath(newFullPath)
                End If
            Else
                IO.Directory.CreateDirectory(My.Settings.DatabaseLocation & "\Licenças\" & Descricao.Text)
                Dim count As Integer = 0
                Dim fileNameOnly As String = IO.Path.GetFileNameWithoutExtension(evidenciacaminho)
                Dim extension As String = IO.Path.GetExtension(evidenciacaminho)
                Dim path As String = My.Settings.DatabaseLocation & "\Licenças\" & Descricao.Text
                Dim newFullPath As String = My.Settings.DatabaseLocation & "\Licenças\" & Descricao.Text & "\" & IO.Path.GetFileName(evidenciacaminho)
                While IO.File.Exists(newFullPath)
                    Dim tempFileName As String = fileNameOnly & "(" & count + 1 & ")"
                    newFullPath = IO.Path.Combine(path, (tempFileName + extension))
                    count = count + 1
                End While
                If IO.File.Exists(evidenciacaminho) Then
                    IO.File.Copy(evidenciacaminho, newFullPath, True)
                    newl.NotaFiscal = IO.Path.GetFullPath(newFullPath)
                End If
            End If
            If MetroGrid1.Rows.Count > 0 Then
                For Each x12 As DataGridViewRow In MetroGrid1.Rows
                    For Each x1 As Patrimonio In Form1.Database.Patrimonios
                        If x1.Patrimonio = x12.Cells(3).Value Then
                            Dim alreadyhave As Boolean = False
                            For Each lice As Licença In x1.Licenças
                                If lice.Chave = Chave.Text Then
                                    alreadyhave = True
                                End If
                            Next
                            If alreadyhave = False Then
                                x1.Licenças.Add(newl)
                            End If
                        End If
                    Next
                Next
            End If
            Form1.Database.Licenças.Add(newl)
            Form1.save()
            MetroMessageBox.Show(Me, "Licença  adicionada com sucesso", "Muito Bem!!!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Form1.Reload()
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroButton4_Click(sender As Object, e As EventArgs) Handles MetroButton4.Click
        Try
            MetroGrid1.Rows.Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroButton5_Click(sender As Object, e As EventArgs) Handles MetroButton5.Click
        Try
            MetroGrid1.Rows.Remove(MetroGrid1.SelectedRows(0))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroButton1_Click_1(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Try
            SearchPat.Show()
            SearchPat.Selectgrid = MetroGrid1

        Catch ex As Exception

        End Try
    End Sub
End Class