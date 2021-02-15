Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports HtmlAgilityPack

Public Class GRbrowser
    Private Sub ChromeTabControl1_Click(sender As Object, e As EventArgs) Handles Me.Load
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            AddAtalho.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GerarRelatórioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GerarRelatórioToolStripMenuItem.Click
        Try
            Dim texto As String = ""
            Dim FILE_NAME2 As String = Path.Combine(Path.GetTempPath, "call.txt")
            For Each x1 In ChromeTabControl1.Controls
                For Each x2 As Browser1 In x1.Controls
                    x2.GeckoWebBrowser1.SaveDocument(FILE_NAME2)
                    Dim readText() As String = File.ReadAllLines(FILE_NAME2)
                    Dim s As String
                    For Each s In readText
                        texto = texto + s
                    Next
                    MsgBox(texto)
                Next
            Next
            Dim idList As New List(Of String)
            Dim problemas As New List(Of String)
            Dim datadeaberturas As New List(Of String)
            Dim datadefechadura As New List(Of String)
            Dim Requerente As New List(Of String)


            Dim strings() As String = texto.Split(New Char() {" "c})
            For Each s As String In strings
                If s.ToString.Contains("http://home.bepo.com.br/call/front/ticket.form.php?") Then
                    Dim val As String = GetJustNumebrs(s)
                    idList.Add(val)

                End If
            Next

            For Each id As String In idList
                Dim Problema As String = After(texto, "http://home.bepo.com.br/call/front/ticket.form.php?id=" & id)
                Dim problema2 As String = After(Before(Problema, "</a>"), ">")
                problemas.Add(problema2)
                Dim Dataabertura As String = After(Before(Problema, ">Média</td>"), ">&nbsp;Fechado</td>")
                Dim Dataabertura2 As String = After(Before(Dataabertura, "</td>"), ">").Replace("-", "/")
                datadefechadura.Add(Dataabertura2)
                Dim DataFechada As String = Before(After(Problema, After(Before(Dataabertura, "</td>"), ">")), "<td style")
                Dim DataFechada2 As String = After(DataFechada.Replace("</td>", ""), ">").Replace("-", "/")
                datadeaberturas.Add(DataFechada2)
                Dim usuario As String = After(Problema, DataFechada)
                Dim usuario2 As String = After(Before(usuario, "<a id="), ">")
                Requerente.Add(usuario2)
            Next
            Dim FILE_NAME As String = Path.Combine(Path.GetTempPath, "Relatorio Call center.txt")
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME)

            For Each id2 As String In idList
                objWriter.WriteLine(id2)
            Next
            For Each prob As String In problemas
                objWriter.WriteLine(prob)
            Next
            For Each date1 As String In datadeaberturas
                objWriter.WriteLine(date1)
            Next
            For Each date2 As String In datadefechadura
                objWriter.WriteLine(date2)
            Next
            For Each req As String In Requerente
                objWriter.WriteLine(req)
            Next
            objWriter.Close()
            Process.Start(FILE_NAME)
        Catch ex As Exception
            MsgBox(ex.Message & ex.StackTrace)
        End Try
    End Sub
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
    Function Before(value As String, a As String) As String
        ' Get index of argument and return substring up to that point.
        Dim posA As Integer = value.IndexOf(a)
        If posA = -1 Then
            Return ""
        End If
        Return value.Substring(0, posA)
    End Function

    Function GetJustNumebrs(ByVal s) As String
        Dim val As Integer
        Try
            Dim b As String = String.Empty
            Dim i As Integer = 0
            Do While (i < s.Length)
                If Char.IsDigit(s(i)) Then
                    b = (b + s(i))
                End If

                i = (i + 1)
            Loop

            If (b.Length > 0) Then
                val = Integer.Parse(b)
            End If
            Return val
        Catch ex As Exception
            Return val
        End Try
    End Function

    Private Sub GerarRelatóioCallCenterDeHTMLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GerarRelatóioCallCenterDeHTMLToolStripMenuItem.Click
        Try
            If OpenFileDialog1.ShowDialog <> DialogResult.Cancel Then
                Dim texto As String = IO.File.ReadAllText(OpenFileDialog1.FileName)
                Dim idList As New List(Of String)
                Dim problemas As New List(Of String)
                Dim datadeaberturas As New List(Of String)
                Dim datadefechadura As New List(Of String)
                Dim Requerente As New List(Of String)


                Dim strings() As String = texto.Split(New Char() {" "c})
                For Each s As String In strings
                    If s.ToString.Contains("http://home.bepo.com.br/call/front/ticket.form.php?") Then
                        Dim val As String = GetJustNumebrs(s)
                        idList.Add(val)

                    End If
                Next

                For Each id As String In idList
                    Dim Problema As String = After(texto, "http://home.bepo.com.br/call/front/ticket.form.php?id=" & id)
                    Dim problema2 As String = After(Before(Problema, "</a>"), ">")
                    problemas.Add(problema2)
                    Dim Dataabertura As String = After(Before(Problema, ">Média</td>"), ">&nbsp;Fechado</td>")
                    Dim Dataabertura2 As String = After(Before(Dataabertura, "</td>"), ">").Replace("-", "/")
                    datadefechadura.Add(Dataabertura2)
                    Dim DataFechada As String = Before(After(Problema, After(Before(Dataabertura, "</td>"), ">")), "<td style")
                    Dim DataFechada2 As String = After(DataFechada.Replace("</td>", ""), ">").Replace("-", "/")
                    datadeaberturas.Add(DataFechada2)
                    Dim usuario As String = After(Problema, DataFechada)
                    Dim usuario2 As String = After(Before(usuario, "<a id="), ">")
                    Requerente.Add(usuario2)
                Next
                Dim FILE_NAME As String = Path.Combine(Path.GetTempPath, "Relatorio Call center.txt")
                Dim objWriter As New System.IO.StreamWriter(FILE_NAME)

                For Each id2 As String In idList
                    objWriter.WriteLine(id2)
                Next
                For Each prob As String In problemas
                    objWriter.WriteLine(prob)
                Next
                For Each date1 As String In datadeaberturas
                    objWriter.WriteLine(date1)
                Next
                For Each date2 As String In datadefechadura
                    objWriter.WriteLine(date2)
                Next
                For Each req As String In Requerente
                    objWriter.WriteLine(req)
                Next
                objWriter.Close()
                Process.Start(FILE_NAME)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & ex.StackTrace)
        End Try
    End Sub
End Class

