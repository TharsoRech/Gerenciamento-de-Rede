Imports System.IO

Public Class Navegador
    Public Fav As Favorito
    Public Iniciado As Boolean

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

    Public Sub endcharge() Handles WebBrowser1.DocumentCompleted
        Try
            If Form1.Database IsNot Nothing Then
                Dim innerHtml = WebBrowser1.DocumentText
                If Fav.Login <> "" And Fav.Senha <> "" And Iniciado = False Then
                    If innerHtml.Contains("txtDsLogin") Then
                        WebBrowser1.Document.GetElementById("txtDsLogin").SetAttribute("value", Fav.Login)
                        WebBrowser1.Document.GetElementById("txtDsSenha").SetAttribute("value", Fav.Senha)
                        WebBrowser1.Document.GetElementsByTagName("btnLogar").Item(0).InvokeMember("click")
                        Iniciado = True
                    Else
                        If innerHtml.Contains("CallCenter Bepo") Then
                            If WebBrowser1.Document.GetElementById("login_name") IsNot Nothing Then
                                WebBrowser1.Document.GetElementById("login_name").SetAttribute("value", Fav.Login)
                            End If
                            If WebBrowser1.Document.GetElementById("login_password") IsNot Nothing Then
                                WebBrowser1.Document.GetElementById("login_password").SetAttribute("value", Fav.Senha)
                            End If
                            If WebBrowser1.Document.GetElementsByTagName("submit") IsNot Nothing Then
                                WebBrowser1.Document.GetElementsByTagName("submit").Item(0).InvokeMember("click")
                            End If
                        Else
                            If WebBrowser1.Document.GetElementById("username") IsNot Nothing Then
                                WebBrowser1.Document.GetElementById("username").SetAttribute("value", Fav.Login)
                            End If
                            If WebBrowser1.Document.GetElementById("password") IsNot Nothing Then
                                WebBrowser1.Document.GetElementById("password").SetAttribute("value", Fav.Senha)
                            End If
                            If WebBrowser1.Document.GetElementsByTagName("loginbutton") IsNot Nothing Then
                                WebBrowser1.Document.GetElementsByTagName("loginbutton").Item(0).InvokeMember("click")
                            End If
                        End If
                        Iniciado = True
                    End If

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Sub MetroDefaultSetButton3_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton3.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton1_Click(sender As Object, e As EventArgs) 
        Try
            Dim texto As String = WebBrowser1.DocumentText
            MsgBox(texto)
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
                Dim usuario As String = After(Problema, DataFechada.Replace("</td>", ""))
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
End Class