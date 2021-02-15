Imports MetroFramework
Imports VncSharp

Public Class Nova_Conexão
    Public rd As VncSharp.RemoteDesktop
    Private Sub MetroDefaultSetButton1_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton1.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Nova_Conexão_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Application.DoEvents()
            MetroSetComboBox1.Text = "Spy Mode"
            MetroSetTextBox1.Text = Form1.Database.VNClastConnection
            Application.DoEvents()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton60_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton60.Click
        Try
            If Form1.Database.ConexoesVNC Is Nothing Then
                Form1.Database.ConexoesVNC = New List(Of VNCLast)
                Form1.save()
            End If
            If MetroSetTextBox1.Text <> "" Then
                If MetroSetComboBox1.Text.Contains("Spy") Then
                    Dim newtab As New MetroSet_UI.Child.MetroSetTabPage
                    newtab.Text = MetroSetTextBox1.Text
                    newtab.Style = MetroSet_UI.Design.Style.Dark
                    Dim newvnc As New VncControl_
                    newvnc.Host1 = MetroSetTextBox1.Text
                    newvnc.Spymode.Style = MetroSet_UI.Design.Style.Light
                    newvnc.RD.Enabled = False
                    rd = newvnc.RD
                    newvnc.Dock = DockStyle.Fill
                    newtab.Controls.Add(newvnc)
                    Form1.Vnctab.TabPages.Add(newtab)
                    rd.Connect(MetroSetTextBox1.Text, False, True)
                Else
                    Dim newtab As New MetroSet_UI.Child.MetroSetTabPage
                    newtab.Text = MetroSetTextBox1.Text
                    newtab.Style = MetroSet_UI.Design.Style.Dark
                    Dim newvnc As New VncControl_
                    newvnc.Host1 = MetroSetTextBox1.Text
                    rd = newvnc.RD
                    newvnc.Dock = DockStyle.Fill
                    newtab.Controls.Add(newvnc)
                    Form1.Vnctab.TabPages.Add(newtab)
                    rd.Connect(MetroSetTextBox1.Text, False, True)
                End If
                Me.Close()
            End If


        Catch ex As Exception
            MetroMessageBox.Show(Me, ex.Message & "," & ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            For Each x1 As MetroSet_UI.Child.MetroSetTabPage In Form1.Vnctab.TabPages
                If MetroSetTextBox1.Text.ToLower.Contains(x1.Text.ToLower) Then
                    Form1.Vnctab.TabPages.Remove(x1)
                End If
            Next
        End Try
    End Sub
    Public Sub cancellingconnect()
        Try
            For Each x1 As MetroSet_UI.Child.MetroSetTabPage In Form1.Vnctab.TabPages
                If rd.Hostname.ToLower.Contains(MetroSetTextBox1.Text.ToLower) Then
                    Form1.Vnctab.TabPages.Remove(x1)
                End If
            Next
            Me.Close()
        Catch ex As Exception

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
End Class