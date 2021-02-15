Imports System.Net

Public Class Browser1
    Public Fav As Favorito
    Public Iniciado As Boolean

    Private Sub IgnoreSSLError(ByVal sender As Object, ByVal e As Gecko.Events.GeckoNSSErrorEventArgs) Handles GeckoWebBrowser1.NSSError
        Try
            Gecko.CertOverrideService.RememberRecentBadCert(e.Uri)
            GeckoWebBrowser1.Navigate(e.Uri.AbsoluteUri)
            e.Handled = True
        Catch ex As Exception
        End Try
    End Sub


    Public Sub endcharge() Handles GeckoWebBrowser1.DocumentCompleted
        Try
            If Form1.Database IsNot Nothing Then
                Dim element As Gecko.GeckoHtmlElement = GeckoWebBrowser1.Document.DocumentElement
                Dim innerHtml = element.InnerHtml
                If Fav.Login <> "" And Fav.Senha <> "" And Iniciado = False Then
                    If innerHtml.Contains("txtDsLogin") Then
                        GeckoWebBrowser1.Document.GetElementById("txtDsLogin").SetAttribute("value", Fav.Login)
                        GeckoWebBrowser1.Document.GetElementById("txtDsSenha").SetAttribute("value", Fav.Senha)
                        GeckoWebBrowser1.Document.GetElementsByName("btnLogar").Item(0).Click()
                        Iniciado = True
                    Else
                        If innerHtml.Contains("CallCenter Bepo") Then
                            If GeckoWebBrowser1.Document.GetElementById("login_name") IsNot Nothing Then
                                GeckoWebBrowser1.Document.GetElementById("login_name").SetAttribute("value", Fav.Login)
                            End If
                            If GeckoWebBrowser1.Document.GetElementById("login_password") IsNot Nothing Then
                                GeckoWebBrowser1.Document.GetElementById("login_password").SetAttribute("value", Fav.Senha)
                            End If
                            If GeckoWebBrowser1.Document.GetElementsByName("submit") IsNot Nothing Then
                                GeckoWebBrowser1.Document.GetElementsByName("submit").Item(0).Click()
                            End If
                        Else
                            If GeckoWebBrowser1.Document.GetElementById("username") IsNot Nothing Then
                                GeckoWebBrowser1.Document.GetElementById("username").SetAttribute("value", Fav.Login)
                            End If
                            If GeckoWebBrowser1.Document.GetElementById("password") IsNot Nothing Then
                                GeckoWebBrowser1.Document.GetElementById("password").SetAttribute("value", Fav.Senha)
                            End If
                            If GeckoWebBrowser1.Document.GetElementsByName("loginbutton") IsNot Nothing Then
                                GeckoWebBrowser1.Document.GetElementsByName("loginbutton").Item(0).Click()
                            End If
                        End If
                        Iniciado = True
                    End If

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GeckoWebBrowser1_Click(sender As Object, e As EventArgs) Handles GeckoWebBrowser1.Click

    End Sub
End Class
