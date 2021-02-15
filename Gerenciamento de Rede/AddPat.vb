Imports MetroFramework

Public Class AddPat
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

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        Try
            For Each x1 As Patrimonio In Form1.Database.Patrimonios
                If x1.Patrimonio = Patrimonio.Text Then
                    MetroMessageBox.Show(Me, "Erro ao adicionar nova ação ,Patrimônio ja adicionado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Next
            Dim newpt As New Patrimonio
            newpt.Patrimonio = Patrimonio.Text
            newpt.Windows = Windows.Text
            newpt.LicençadoWindows = LicencadoWindows.Text
            newpt.Descrição = Descricao.Text
            newpt.UsuárioAtual = UsuarioAtual.Text
            newpt.NotaFiscal = NotaFiscal.Text
            newpt.NomeDoComputadorAtual = NomeDoComputadorAtual.Text
            newpt.RecoveryEmbutido = RecoveryEmbutido.Checked
            newpt.AtualizadoParaWindows10 = AtualizadoParaWindowsDez.Checked
            newpt.EtiquetaComNumerodeserie = EtiquetaComNumerodesrie.Checked
            newpt.Adicionadoem = DateAndTime.Now.ToString
            Dim newl As New Licença
            newl.Descrição = Windows.Text
            newl.Quantidade = 1
            newl.Chave = LicencadoWindows.Text
            newl.Adicionadoem = DateAndTime.Now.ToString
            newl.EmUso = True
            newl.NotaFiscal = NotaFiscal.Text
            newpt.Licenças.Add(newl)
            For Each x20 As DataGridViewRow In MetroGrid1.Rows
                If x20.Cells(0).Value <> "" Then
                    Dim newl1 As New Licença
                    newl1.Descrição = x20.Cells(0).Value
                    newl1.Quantidade = 1
                    newl1.Chave = x20.Cells(1).Value
                    newl1.Adicionadoem = x20.Cells(4).Value
                    newl1.EmUso = True
                    newl1.NotaFiscal = IO.Path.GetFullPath(x20.Cells(2).Value)
                    If x20.Cells(3).Value = "True" Then
                        newl1.MidiaFisica = True
                    End If
                    Dim evidenciacaminho1 = x20.Cells(2).Value
                    If IO.Directory.Exists(My.Settings.DatabaseLocation & "\" & Windows.Text & "\" & Patrimonio.Text) Then
                        Dim count As Integer = 0
                        Dim fileNameOnly As String = IO.Path.GetFileNameWithoutExtension(evidenciacaminho1)
                        Dim extension As String = IO.Path.GetExtension(evidenciacaminho1)
                        Dim path As String = My.Settings.DatabaseLocation & "\" & Windows.Text & "\" & Patrimonio.Text
                        Dim newFullPath As String = My.Settings.DatabaseLocation & "\" & Windows.Text & "\" & Patrimonio.Text & "\" & IO.Path.GetFileName(evidenciacaminho1)
                        While IO.File.Exists(newFullPath)
                            Dim tempFileName As String = fileNameOnly & "(" & count + 1 & ")"
                            newFullPath = IO.Path.Combine(path, (tempFileName + extension))
                            count = count + 1
                        End While
                        If IO.File.Exists(evidenciacaminho1) Then
                            IO.File.Copy(evidenciacaminho1, newFullPath, True)
                            newl1.NotaFiscal = IO.Path.GetFullPath(newFullPath)
                        End If
                    Else
                        IO.Directory.CreateDirectory(My.Settings.DatabaseLocation & "\" & Windows.Text & "\" & Patrimonio.Text)
                        Dim count As Integer = 0
                        Dim fileNameOnly As String = IO.Path.GetFileNameWithoutExtension(evidenciacaminho1)
                        Dim extension As String = IO.Path.GetExtension(evidenciacaminho1)
                        Dim path As String = My.Settings.DatabaseLocation & "\" & Windows.Text & "\" & Patrimonio.Text
                        Dim newFullPath As String = My.Settings.DatabaseLocation & "\" & Windows.Text & "\" & Patrimonio.Text & "\" & IO.Path.GetFileName(evidenciacaminho1)
                        While IO.File.Exists(newFullPath)
                            Dim tempFileName As String = fileNameOnly & "(" & count + 1 & ")"
                            newFullPath = IO.Path.Combine(path, (tempFileName + extension))
                            count = count + 1
                        End While
                        If IO.File.Exists(evidenciacaminho1) Then
                            IO.File.Copy(evidenciacaminho1, newFullPath, True)
                            newl1.NotaFiscal = IO.Path.GetFullPath(newFullPath)
                        End If
                    End If
                    newpt.Licenças.Add(newl1)
                    newl1.PatrimonioInstalados.Add(newpt)
                End If
            Next
            Dim evidenciacaminho = newpt.NotaFiscal
            If IO.Directory.Exists(My.Settings.DatabaseLocation & "\" & Windows.Text & "\" & Patrimonio.Text) Then
                Dim count As Integer = 0
                Dim fileNameOnly As String = IO.Path.GetFileNameWithoutExtension(evidenciacaminho)
                Dim extension As String = IO.Path.GetExtension(evidenciacaminho)
                Dim path As String = My.Settings.DatabaseLocation & "\" & Windows.Text & "\" & Patrimonio.Text
                Dim newFullPath As String = My.Settings.DatabaseLocation & "\" & Windows.Text & "\" & Patrimonio.Text & "\" & IO.Path.GetFileName(evidenciacaminho)
                While IO.File.Exists(newFullPath)
                    Dim tempFileName As String = fileNameOnly & "(" & count + 1 & ")"
                    newFullPath = IO.Path.Combine(path, (tempFileName + extension))
                End While
                If IO.File.Exists(evidenciacaminho) Then
                    IO.File.Copy(evidenciacaminho, newFullPath, True)
                    newpt.NotaFiscal = IO.Path.GetFullPath(newFullPath)
                    newl.NotaFiscal = IO.Path.GetFullPath(newFullPath)
                End If
            Else
                IO.Directory.CreateDirectory(My.Settings.DatabaseLocation & "\" & Windows.Text & "\" & Patrimonio.Text)
                Dim count As Integer = 0
                Dim fileNameOnly As String = IO.Path.GetFileNameWithoutExtension(evidenciacaminho)
                Dim extension As String = IO.Path.GetExtension(evidenciacaminho)
                Dim path As String = My.Settings.DatabaseLocation & "\" & Windows.Text & "\" & Patrimonio.Text
                Dim newFullPath As String = My.Settings.DatabaseLocation & "\" & Windows.Text & "\" & Patrimonio.Text & "\" & IO.Path.GetFileName(evidenciacaminho)
                While IO.File.Exists(newFullPath)
                    Dim tempFileName As String = fileNameOnly & "(" & count + 1 & ")"
                    newFullPath = IO.Path.Combine(path, (tempFileName + extension))
                End While
                If IO.File.Exists(evidenciacaminho) Then
                    IO.File.Copy(evidenciacaminho, newFullPath, True)
                    newpt.NotaFiscal = IO.Path.GetFullPath(newFullPath)
                    newl.NotaFiscal = IO.Path.GetFullPath(newFullPath)
                End If
            End If
            newl.PatrimonioInstalados.Add(newpt)
            Form1.Database.Patrimonios.Add(newpt)
            Form1.save()
            MetroMessageBox.Show(Me, "Patrimônio  adicionado com sucesso", "Muito Bem!!!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Form1.Reload()
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RemoveLicense_Click(sender As Object, e As EventArgs) Handles RemoveLicense.Click
        Try
            For Each x1 As DataGridViewRow In MetroGrid1.SelectedRows
                MetroGrid1.Rows.Remove(x1)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroButton5_Click(sender As Object, e As EventArgs) Handles MetroButton5.Click
        Try
            If IO.File.Exists(MetroGrid1.SelectedRows(0).Cells(2).Value) Then
                Process.Start(MetroGrid1.SelectedRows(0).Cells(2).Value)

            Else
                MetroMessageBox.Show(Form1, "Não foi possivel abrir nota,arquivo não encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroButton4_Click(sender As Object, e As EventArgs) Handles MetroButton4.Click
        Try
            EditLicense.Show()
            EditLicense.Text = "Alterar Licença"
            EditLicense.Descricao.Text = MetroGrid1.SelectedRows(0).Cells(0).Value
            EditLicense.Chave.Text = MetroGrid1.SelectedRows(0).Cells(1).Value
            EditLicense.NotaFiscal.Text = MetroGrid1.SelectedRows(0).Cells(2).Value
            If MetroGrid1.SelectedRows(0).Cells(3).Value = "True" Then
                EditLicense.MidiaFisica.Checked = True
            End If
            EditLicense.EmUso.Checked = True
            EditLicense.MetroGrid1.Rows.Add(Patrimonio.Text)
            EditLicense.Qauntidade.Text = 1
            EditLicense.MetroComboBox1.Enabled = False
            EditLicense.SelectGrid = MetroGrid1.SelectedRows(0)
            EditLicense.SelectMetrogrid = MetroGrid1
            For Each x1 As Licença In Form1.Database.Licenças
                If x1.Chave = MetroGrid1.SelectedRows(0).Cells(1).Value Then
                    For Each lc2 As Licença In Form1.Database.Licenças
                        EditLicense.MetroComboBox1.Items.Add(lc2.Descrição & "-" & lc2.Chave)
                    Next
                    EditLicense.MetroComboBox1.Text = x1.Descrição
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroButton6_Click(sender As Object, e As EventArgs) Handles MetroButton6.Click
        Try
            EditLicense.Show()

            For Each lc2 As Licença In Form1.Database.Licenças
                EditLicense.MetroComboBox1.Items.Add(lc2.Descrição & "-" & lc2.Chave)
            Next
            EditLicense.Descricao.Enabled = True
            EditLicense.MetroGrid1.Rows.Add(Patrimonio.Text)
            EditLicense.SelectMetrogrid = MetroGrid1

        Catch ex As Exception

        End Try
    End Sub
    Private Sub MetroGrid1_CellContentClickdpouble(sender As Object, e As DataGridViewCellEventArgs) Handles MetroGrid1.CellContentDoubleClick
        Try
            EditLicense.Show()
            EditLicense.Text = "Alterar Licença"
            EditLicense.Descricao.Text = MetroGrid1.SelectedRows(0).Cells(0).Value
            EditLicense.Chave.Text = MetroGrid1.SelectedRows(0).Cells(1).Value
            EditLicense.NotaFiscal.Text = MetroGrid1.SelectedRows(0).Cells(2).Value
            If MetroGrid1.SelectedRows(0).Cells(3).Value = "True" Then
                EditLicense.MidiaFisica.Checked = True
            End If
            EditLicense.EmUso.Checked = True
            EditLicense.MetroGrid1.Rows.Add(Patrimonio.Text)
            EditLicense.Qauntidade.Text = 1
            EditLicense.MetroComboBox1.Enabled = False
            EditLicense.SelectGrid = MetroGrid1.SelectedRows(0)
            EditLicense.SelectMetrogrid = MetroGrid1
            For Each x1 As Licença In Form1.Database.Licenças
                If x1.Chave = MetroGrid1.SelectedRows(0).Cells(1).Value Then
                    For Each lc2 As Licença In Form1.Database.Licenças
                        EditLicense.MetroComboBox1.Items.Add(lc2.Descrição & "-" & lc2.Chave)
                    Next
                    EditLicense.MetroComboBox1.Text = x1.Descrição
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
End Class