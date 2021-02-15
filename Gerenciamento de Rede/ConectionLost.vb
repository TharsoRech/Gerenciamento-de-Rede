Public Class ConectionLost
    Public count As Integer = 0
    Public vnccontrol As VncControl_
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        On Error Resume Next
        count = count + 1
        Application.DoEvents()
        If vnccontrol.RD.IsConnected = True Then
            vnccontrol.Timer2.Enabled = False
            Timer1.Enabled = False
            Me.Parent.Controls.Remove(Me)
        Else
            vnccontrol.Timer2.Enabled = False
        End If
        If count = 10 Then
            Dim ComboboxesEventAddress As New EventHandler(AddressOf vnccontrol.rd_Conncetionlost)
            RemoveHandler vnccontrol.RD.ConnectionLost, ComboboxesEventAddress
            If (vnccontrol.RD.IsConnected) Then
                vnccontrol.RD.Disconnect()
                Timer1.Enabled = False
            End If
            Form1.Vnctab.TabPages.Remove(vnccontrol.Parent)
        Else
            Application.DoEvents()
            If vnccontrol.RD.IsConnected Then
                vnccontrol.Timer2.Enabled = False
                Timer1.Enabled = False
                Me.Parent.Controls.Remove(Me)
            Else
                vnccontrol.Timer2.Enabled = False
            End If
            MetroSetLabel1.Text = "Tentando Reconectar...tentartiva:" & Space(2) & count
            Application.DoEvents()
            vnccontrol.Timer2.Enabled = True
            If vnccontrol.RD.IsConnected Then
                vnccontrol.Timer2.Enabled = False
                Timer1.Enabled = False
                Me.Parent.Controls.Remove(Me)
            Else
                vnccontrol.Timer2.Enabled = False
            End If
            vnccontrol.Timer2.Enabled = True
            If vnccontrol.RD.IsConnected = False And ping(Form1.Before(vnccontrol.Parent.Text, "(").Trim) And vnccontrol.Timer2.Enabled Then
                vnccontrol.RD.Connect(Form1.Before(vnccontrol.Parent.Text, "(").Trim, False, True)
            End If
            MetroSetProgressBar1.Value = MetroSetProgressBar1.Value + 1
            Application.DoEvents()
            If vnccontrol.RD.IsConnected Then
                    vnccontrol.Timer2.Enabled = False
                    Timer1.Enabled = False
                    Me.Parent.Controls.Remove(Me)
                Else
                    vnccontrol.Timer2.Enabled = False
                End If
            End If
    End Sub
    Public Function ping(ByVal sourceString As String) As Boolean
        Dim pingright As Boolean = False
        Try
            If My.Computer.Network.Ping(sourceString) = True Then
                pingright = True
            End If
            Return pingright
        Catch ex As Exception
            Return pingright
        End Try
    End Function

    Private Sub MetroSetButton1_Click(sender As Object, e As EventArgs) Handles MetroSetButton1.Click
        Try
            Dim ComboboxesEventAddress As New EventHandler(AddressOf vnccontrol.rd_Conncetionlost)
            RemoveHandler vnccontrol.RD.ConnectionLost, ComboboxesEventAddress
            If (vnccontrol.RD.IsConnected) Then
                vnccontrol.RD.Disconnect()
            End If
            Timer1.Enabled = False
            vnccontrol.Timer1.Enabled = False
            vnccontrol.Timer2.Enabled = False
            Form1.Vnctab.TabPages.Remove(vnccontrol.Parent)
        Catch ex As Exception

        End Try
    End Sub
End Class
