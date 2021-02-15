Imports MetroFramework
Imports Microsoft.Win32
Imports VncSharp

Public Class VncControl_
    Public Host1 As String
    Private Sub MetroDefaultSetButton61_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton61.Click
        Try
            If (RD.IsConnected) Then
                RD.SendSpecialKeys(SpecialKeys.CtrlAltDel)
                trycrtlalt()
                Dim pc As String = RD.Hostname
                Dim th As New Threading.Thread(Sub() cmdDisableduac(pc))
                th.TrySetApartmentState(Threading.ApartmentState.STA)
                th.Start()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub trycrtlalt()
        Try
            Dim key As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, Me.Invoke(Function() RD.Hostname)).OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\", True)
            key.SetValue("SoftwareSASGeneration", 1, RegistryValueKind.DWord)
            key.Close()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub cmdDisableduac(ByVal pc As String)
        Try
            Dim info As New ProcessStartInfo()
            info.RedirectStandardOutput = True
            info.UseShellExecute = False
            info.CreateNoWindow = True
            info.WindowStyle = ProcessWindowStyle.Hidden
            info.FileName = "PsTools\psexec.exe"
            info.Arguments = "\\" & pc & " -s C:\Windows\System32\cmd.exe /k %windir%\System32\reg.exe ADD HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System /v EnableLUA /t REG_DWORD /d 0 /f"
            Dim p As Process = Process.Start(info)
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.RedirectStandardError = True
            p.StartInfo.RedirectStandardInput = True
            p.WaitForExit()
            p.Refresh()
            p.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MetroDefaultSetButton62_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton62.Click
        Try
            If (RD.IsConnected) Then
                RD.FillServerClipboard()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MetroDefaultSetButton63_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton63.Click
        Try
            Dim bitmap As Bitmap
            bitmap = New Bitmap(RD.Width, RD.Height)
            RD.DrawToBitmap(bitmap, New Rectangle(0, 0, bitmap.Width, bitmap.Height))

            Dim image1 As Image = bitmap
            SaveFileDialog1.FileName = ExtractNumber(DateTime.Now)
            If SaveFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Abort Then
                image1.Save(SaveFileDialog1.FileName & ".jpeg", Imaging.ImageFormat.Jpeg)
            End If
        Catch ex As Exception
            MetroMessageBox.Show(Form1, ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Public Function ExtractNumber(original As String) As String
        Return New String(original.Where(Function(c) [Char].IsDigit(c)).ToArray())
    End Function

    Private Sub MetroComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MetroComboBox1.SelectedIndexChanged
        Try
            If MetroComboBox1.SelectedItem.ToString.ToLower.Contains("CMD".ToLower) Then
                Dim pc As String = Before(RD.Hostname, "(")
                Dim th1 As New Threading.Thread(Sub() startcmd(pc, "CMD.exe"))
                th1.TrySetApartmentState(Threading.ApartmentState.STA)
                th1.Start()
            End If
            If MetroComboBox1.SelectedItem.ToString.ToLower.Contains("Bloquear".ToLowerInvariant) Then
                RD.SendSpecialKeys(VncSharp.SpecialKeys.BlockWorkstation)
            End If
            If MetroComboBox1.SelectedItem.ToString.ToLower.Contains("Tarefas".ToLowerInvariant) Then
                Dim pc As String = Before(RD.Hostname, "(")
                Dim th1 As New Threading.Thread(Sub() startcmd(pc, "taskmgr.exe"))
                th1.TrySetApartmentState(Threading.ApartmentState.STA)
                th1.Start()
            End If
            If MetroComboBox1.SelectedItem.ToString.ToLower.Contains("Windows+d".ToLowerInvariant) Then
                RD.SendSpecialKeys(VncSharp.SpecialKeys.WindowsD)
            End If
            If MetroComboBox1.SelectedItem.ToString.ToLower.Contains("Windows+E".ToLowerInvariant) Then
                RD.SendSpecialKeys(VncSharp.SpecialKeys.windowse)
            End If
            If MetroComboBox1.SelectedItem.ToString.ToLower.Contains("Menu Executar".ToLowerInvariant) Then
                RD.SendSpecialKeys(VncSharp.SpecialKeys.WindowsR)
            End If
            If MetroComboBox1.SelectedItem.ToString.ToLower.Contains("Pause".ToLowerInvariant) Then
                RD.SendSpecialKeys(VncSharp.SpecialKeys.WindowsPauseBreak)
            End If
            If MetroComboBox1.SelectedItem.ToString.ToLower.Contains("F4".ToLowerInvariant) Then
                RD.SendSpecialKeys(VncSharp.SpecialKeys.AltF4)
            End If
            If MetroComboBox1.SelectedItem.ToString.ToLower.Contains("Iniciar".ToLowerInvariant) Then
                RD.SendSpecialKeys(VncSharp.SpecialKeys.CtrlEsc)
            End If
            If MetroComboBox1.SelectedItem.ToString.ToLower.Contains("Comando".ToLowerInvariant) Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual comando você deseja usar no compuatdor remoto?", "Digite o comando", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Dim pc As String = Before(RD.Hostname, "(")
                    Dim th1 As New Threading.Thread(Sub() startcmd(pc, StatusDate))
                    th1.TrySetApartmentState(Threading.ApartmentState.STA)
                    th1.Start()
                End If
            End If
            If MetroComboBox1.SelectedItem.ToString.ToLower.Contains("Arquivo".ToLowerInvariant) Then
                If OpenFileDialog1.ShowDialog <> DialogResult.Cancel Then
                    Dim pc As String = Before(RD.Hostname, "(")
                    Dim fl As String = IO.Path.GetFileName(OpenFileDialog1.FileName)
                    IO.File.Copy(OpenFileDialog1.FileName, "\\" & pc & "\c$\" & fl, True)
                    Dim th1 As New Threading.Thread(Sub() startcmd(pc, "C:\" & fl))
                    th1.TrySetApartmentState(Threading.ApartmentState.STA)
                    th1.Start()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & ex.StackTrace)
            ' MetroMessageBox.Show(Form1, ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Function Before(value As String, a As String) As String
        ' Get index of argument and return substring up to that point.
        Dim posA As Integer = value.IndexOf(a)
        If posA = -1 Then
            Return ""
        End If
        Return value.Substring(0, posA)
    End Function
    Public Sub startcmd(ByVal pc As String, ByVal parameter As String)
        Try
            Dim info As New ProcessStartInfo()
            info.RedirectStandardOutput = True
            info.UseShellExecute = False
            info.CreateNoWindow = True
            info.WindowStyle = ProcessWindowStyle.Hidden
            info.FileName = "PsTools\psexec.exe"
            info.Arguments = "\\" & pc & " -s -i   " & parameter
            Dim p As Process = Process.Start(info)
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.RedirectStandardError = True
            p.StartInfo.RedirectStandardInput = True
            p.WaitForExit()
            p.Refresh()
            p.Close()
        Catch ex As Exception
            MsgBox(ex.Message & ex.StackTrace)
        End Try
    End Sub
    Public Sub rd_ConnectComplete(ByVal sender As System.Object, ByVal e As VncSharp.ConnectEventArgs) Handles RD.ConnectComplete

        Try
            Timer2.Enabled = False
            Me.Visible = True
            Form1.Database.VNClastConnection = Host1
            Form1.save()
            RD.Focus()
            Timer1.Enabled = True
            Form1.Vnctab.SelectedTab = Me.Parent
            Form1.Vnctab.Update()
            Form1.Vnctab.Refresh()
            Me.Parent.Text = RD.Hostname
            Application.DoEvents()
            If Form1.WindowState = FormWindowState.Normal Then
                Form1.WindowState = FormWindowState.Maximized
                Form1.WindowState = FormWindowState.Normal
            Else
                Form1.WindowState = FormWindowState.Normal
            End If
            Application.DoEvents()
            Dim alreadyhave As Boolean = False
            For Each x20 As VNCLast In Form1.Database.ConexoesVNC
                If x20.Nome = Before(RD.Hostname, "(").Trim Then
                    x20.Ultimoacesso = DateAndTime.Now.ToString
                    Form1.save()
                    alreadyhave = True
                    Exit For
                End If
            Next
            If alreadyhave = False Then
                Dim nv As New VNCLast
                nv.Nome = Before(RD.Hostname, "(").Trim
                nv.Ip = Between(RD.Hostname, "(", ")")
                nv.Ultimoacesso = DateAndTime.Now.ToString
                Form1.Database.ConexoesVNC.Add(nv)
                Form1.save()
            End If

        Catch ex As Exception
            MetroMessageBox.Show(Me, ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            RD.Refresh()
            RD.Update()
            Me.Parent.Update()
            Me.Update()
        Catch ex As Exception

        End Try

    End Sub
    Public Sub rd_Conncetionlost(ByVal sender As System.Object, ByVal e As EventArgs) Handles RD.ConnectionLost
        Try
            Me.Visible = False
            Dim newlostconect As New ConectionLost
            newlostconect.Dock = DockStyle.Fill
            newlostconect.vnccontrol = Me
            Me.Parent.Controls.Add(newlostconect)
            newlostconect.Timer1.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Spymode_Click(sender As Object, e As EventArgs) Handles Spymode.Click
        Try
            If Spymode.Style = MetroSet_UI.Design.Style.Dark Then
                Spymode.Style = MetroSet_UI.Design.Style.Light
                RD.Enabled = False
            Else
                Spymode.Style = MetroSet_UI.Design.Style.Dark
                RD.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub OnShown1(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Try
            Timer2.Enabled = True

        Catch ex As Exception

        End Try
    End Sub
    Public Sub cancellingconnect()
        Try

            Form1.Vnctab.TabPages.Remove(Me.Parent)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton1_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton1.Click
        On Error Resume Next
        Form1.ReloadConexoesVNC()
        Dim ComboboxesEventAddress As New EventHandler(AddressOf rd_Conncetionlost)
        RemoveHandler RD.ConnectionLost, ComboboxesEventAddress
        Timer1.Enabled = False
        If (RD.IsConnected) Then
            RD.Disconnect()
        End If
        If MetroDefaultSetButton3.Text.ToLower.Contains("cheia") Then
            Form1.Vnctab.SelectedTab = Form1.Vnctab.TabPages(0)
            Form1.Vnctab.Update()
            Form1.Vnctab.Refresh()
            Application.DoEvents()
            Form1.Vnctab.TabPages.Remove(Me.Parent)
        Else
            Dim curretform As Form = Me.Parent
            curretform.Close()
        End If


    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            If RD.IsConnected = False Then
                If Form1.Database.VNCpassword <> "" Then
                    For Each x As Form In Application.OpenForms
                        Application.DoEvents()
                        Me.Refresh()
                        If x.Name.ToLower.Contains("Password".ToLower) Then
                            Dim x2 As PasswordDialog = x
                            Dim button2 As Button = x2.CancelButton
                            AddHandler button2.Click, AddressOf cancellingconnect
                            x2.Password = Form1.Database.VNCpassword
                            x2.AcceptButton.PerformClick()
                            Timer2.Enabled = False
                            Exit Sub
                        End If
                    Next
                Else
                    For Each x As Form In Application.OpenForms
                        Application.DoEvents()
                        Me.Refresh()
                        If x.Name.ToLower.Contains("Password".ToLower) Then
                            Dim x2 As PasswordDialog = x
                            Dim button2 As Button = x2.CancelButton
                            AddHandler button2.Click, AddressOf cancellingconnect
                            Timer2.Enabled = False
                            Exit Sub
                        End If
                    Next
                End If
            End If
            Application.DoEvents()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton2_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton2.Click
        Try
            Me.Parent.Update()
            Me.Update()
            RD.Update()
            RD.Refresh()
            RD.FullScreenUpdate()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton3_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton3.Click
        Try
            If MetroDefaultSetButton3.Text.ToLower.Contains("cheia") Then
                Dim P100 As TabPage = Me.Parent
                Dim p1 As New Form
                p1.Controls.Add(Me)
                p1.Size = Form1.Size
                p1.ControlBox = False
                p1.Icon = Form1.Icon
                MetroDefaultSetButton3.Text = "Minimizar"
                p1.Show()
                p1.WindowState = FormWindowState.Maximized
                Form1.Vnctab.TabPages.Remove(P100)
            Else
                MetroDefaultSetButton3.Text = "Tela Cheia"
                Dim newtab As New MetroSet_UI.Child.MetroSetTabPage
                newtab.Text = Me.RD.Hostname
                Dim curretform As Form = Me.Parent
                newtab.Style = MetroSet_UI.Design.Style.Dark
                newtab.Controls.Add(Me)
                curretform.Close()
                Form1.Vnctab.TabPages.Add(newtab)
                Application.DoEvents()
                Form1.Vnctab.SelectedTab = newtab
                Form1.Vnctab.Update()
                Form1.Vnctab.Refresh()
                Application.DoEvents()
                If Form1.WindowState = FormWindowState.Normal Then
                    Form1.WindowState = FormWindowState.Maximized
                    Form1.WindowState = FormWindowState.Normal
                Else
                    Form1.WindowState = FormWindowState.Normal
                End If
                Application.DoEvents()
            End If
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
