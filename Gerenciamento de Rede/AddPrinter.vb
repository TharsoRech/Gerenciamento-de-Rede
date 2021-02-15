Imports System.Management
Imports System.Text.RegularExpressions
Imports MetroFramework

Public Class AddPrinter
    Public CountPrinters As Integer = 0
    Private Sub MetroDefaultSetButton2_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton2.Click
        Try
            getinfopc(Nomedocomputador.Text)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub getinfopc(ByVal pc As String)

        On Error Resume Next
        Dim connection As New ConnectionOptions
        connection.Impersonation = ImpersonationLevel.Impersonate
        Dim scope As New ManagementScope("\\" & pc & "\root\CIMV2", connection)
        scope.Connect()
        Dim query As New ObjectQuery("SELECT * FROM Win32_Printer ")
        Dim searcher As New ManagementObjectSearcher(scope, query)
        Application.DoEvents()
        MetroSetProgressBar2.Value = 0
        MetroSetProgressBar2.Maximum = searcher.Get().Count
        For Each m As ManagementObject In searcher.Get()
            Application.DoEvents()
            MetroSetProgressBar2.Value += 1
            Application.DoEvents()
            Dim nome As String = ""
            Dim SystemName As String = ""
            Dim Porta As String = ""
            Application.DoEvents()

            If m("Name") IsNot Nothing Then
                nome = m("Name")
            End If
            Application.DoEvents()
            If m("SystemName") IsNot Nothing Then
                SystemName = m("SystemName")
            End If
            Application.DoEvents()
            Application.DoEvents()
            If m("PortName") IsNot Nothing Then
                Porta = m("PortName")
            End If
            Application.DoEvents()
            PrintersGrid.Rows.Add(New String() {nome, Porta, SystemName})
            PrintersGrid.Update()
            Application.DoEvents()
        Next
        MetroSetProgressBar2.Value = MetroSetProgressBar2.Maximum


    End Sub

    Private Sub MetroDefaultSetButton4_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton4.Click
        Try
            PrintersGrid.Rows.Clear()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub getinfopc4(ByVal pc As String)

        On Error Resume Next
        Application.DoEvents()
        Dim connection As New ConnectionOptions
        connection.Impersonation = ImpersonationLevel.Impersonate
        Dim scope As New ManagementScope("\\" & pc & "\root\CIMV2", connection)
        scope.Connect()
        Dim query As New ObjectQuery("SELECT * FROM Win32_Printer ")
        Dim searcher As New ManagementObjectSearcher(scope, query)
        MetroSetProgressBar1.Maximum = searcher.Get().Count
        For Each m As ManagementObject In searcher.Get()
            Application.DoEvents()
            Dim nome As String = ""
            Dim NomedoServidor As String = ""
            Dim Descrição As String = ""
            Dim Driver As String = ""
            Dim Comentarios As String = ""
            Dim SystemName As String = ""
            Dim localização As String = ""
            Dim NomeCompartilhado As String = ""
            Dim Porta As String = ""
            Dim UltimaTrocadeToner As String = ""
            Dim UltimaVezColletada As String = ""
            Dim Status As String = ""
            Dim PáginasContadas As String = ""
            Dim QuantidadedeTonerDisponivel As Integer = 0
            Dim TonerRestante As String = ""

            If m("Name") IsNot Nothing Then
                nome = m("Name")
            End If
            Application.DoEvents()
            If m("servername") IsNot Nothing Then
                NomedoServidor = m("servername")
            End If
            Application.DoEvents()
            If m("ShareName") IsNot Nothing Then
                NomeCompartilhado = m("ShareName")
            End If
            If m("DriverName") IsNot Nothing Then
                Driver = m("DriverName")
            End If
            Application.DoEvents()
            If m("Description") IsNot Nothing Then
                Descrição = m("Description")
            End If
            Application.DoEvents()
            If m("Comment") IsNot Nothing Then
                Comentarios = m("Comment")
            End If
            Application.DoEvents()
            If m("ShareName") IsNot Nothing Then
                NomeCompartilhado = m("ShareName")
            End If
            Application.DoEvents()
            If m("SystemName") IsNot Nothing Then
                SystemName = m("SystemName")
            End If
            Application.DoEvents()
            If m("Location") IsNot Nothing Then
                localização = m("Location")
            End If
            Application.DoEvents()
            If m("PortName") IsNot Nothing Then
                Porta = m("PortName")
            End If
            Application.DoEvents()
            UltimaVezColletada = DateAndTime.Now.ToString
            Application.DoEvents()
            Dim newpr As New Impressoras
            newpr.nome = nome
            newpr.Porta = Porta
            newpr.Comentarios = Comentarios
            newpr.Descrição = Descrição
            newpr.Driver = Driver
            newpr.localização = localização
            newpr.NomeCompartilhado = NomeCompartilhado
            newpr.NomedoServidor = NomedoServidor
            newpr.PáginasContadas = Form1.GetCountPages(GetIp(Form1.SelectPrinter.Porta))
            newpr.Status = Form1.StatusPrinter1(GetIp(Form1.SelectPrinter.Porta))
            newpr.SystemName = SystemName
            newpr.TonerRestante = Form1.GetTonerLevel(GetIp(Form1.SelectPrinter.Porta))
            newpr.UltimaVezColletada = UltimaVezColletada
            Application.DoEvents()
            If Form1.Database IsNot Nothing Then
                If Form1.Database.Impressoras Is Nothing Then
                    Form1.Database.Impressoras = New List(Of Impressoras)
                Else
                    If Form1.alreadyexistsPrinter(nome) = False Then
                        Application.DoEvents()
                        Form1.Database.Impressoras.Add(newpr)
                        Form1.save()
                    End If
                End If
            End If
            MetroSetProgressBar1.Value = MetroSetProgressBar1.Value + 1
            Application.DoEvents()

        Next
        MetroFramework.MetroMessageBox.Show(Form1, "Impressoras Importadas com sucesso", "Adição de Impressora", MessageBoxButtons.OK, MessageBoxIcon.Information)


    End Sub
    Public Function GetIp(ByVal sourceString As String) As String
        Dim Ip As String = sourceString
        Try
            If sourceString.Contains("_") And sourceString.Contains("\") = False Then
                If sourceString.ToLower.Contains("IP_".ToLower) Then
                    sourceString = After(sourceString, "IP_")
                Else
                    sourceString = Before(sourceString, "_")
                End If
                Ip = sourceString
            Else
                Dim match = Regex.Match(sourceString, "\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b")
                If match.Success Then
                    Ip = match.Captures(0).ToString
                End If
            End If
            Return Ip
        Catch ex As Exception
            Return Ip
        End Try
    End Function
    Function Between(value As String, a As String,
        b As String) As String
        ' Get positions for both string arguments.
        Dim posA As Integer = value.IndexOf(a)
        Dim posB As Integer = value.LastIndexOf(b)
        If posA = -1 Then
            Return ""
        End If
        If posB = -1 Then
            Return ""
        End If

        Dim adjustedPosA As Integer = posA + a.Length
        If adjustedPosA >= posB Then
            Return ""
        End If

        ' Get the substring between the two positions.
        Return value.Substring(adjustedPosA, posB - adjustedPosA)
    End Function

    Function Before(value As String, a As String) As String
        ' Get index of argument and return substring up to that point.
        Dim posA As Integer = value.IndexOf(a)
        If posA = -1 Then
            Return ""
        End If
        Return value.Substring(0, posA)
    End Function

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

    Private Sub MetroDefaultSetButton3_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton3.Click
        Try
            If Form1.Database IsNot Nothing Then
                If Form1.Database.Impressoras Is Nothing Then
                    Form1.Database.Impressoras = New List(Of Impressoras)
                Else
                    Dim newpr As New Impressoras
                    newpr.nome = NomedaImpressora.Text
                    newpr.Porta = IpdaImpressora.Text
                    newpr.SystemName = Nomedocomputador.Text
                    If Form1.alreadyexistsPrinter(newpr.nome) = False Then
                        Form1.Database.Impressoras.Add(newpr)
                        Form1.save()
                        MetroFramework.MetroMessageBox.Show(Form1, "Impressora Adicionada com sucesso", "Adição de Impressora", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Form1, ex.Message, "Adição de Impressora", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MetroDefaultSetButton1_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton1.Click
        Try
            If Form1.Database.Impressoras.Count <> CountPrinters Then
                Form1.atualizarlistaImpressoras()
            End If
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton23_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton23.Click
        Try
            getinfopc4(NomedoServidor.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PrintersGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles PrintersGrid.CellContentDoubleClick
        Try
            NomedaImpressora.Text = PrintersGrid.SelectedRows(0).Cells(0).Value
            IpdaImpressora.Text = PrintersGrid.SelectedRows(0).Cells(1).Value
        Catch ex As Exception

        End Try
    End Sub
End Class