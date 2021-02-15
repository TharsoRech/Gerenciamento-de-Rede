Imports System.Management
Imports MetroFramework

Public Class AçõesComputador
    Private Sub MetroDefaultSetButton3_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton3.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AçõesComputador_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Nomeatual.Text = Form1.Nomedocomputador.Text
        Catch ex As Exception

        End Try
    End Sub

    Public Sub MetroDefaultSetButton2_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton2.Click
        Try
            If MetroSetComboBox1.Text.ToLower.Contains("Renomear".ToLower) Then
                If MetroFramework.MetroMessageBox.Show(Me, "Você tem certeza que deseja renomear  o Computador " & Nomeatual.Text & Space(2) & "Para" & Space(2) & NovoNome.Text & "?", "Renomar Computador", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    Dim account As Net.NetworkCredential = New Net.NetworkCredential
                    account.Domain = Environment.UserDomainName
                    account.UserName = Usuario.Text
                    account.Password = Senha.Text
                    RenameRemotePC(Nomeatual.Text, NovoNome.Text, Environment.UserDomainName, account)
                    Exit Sub
                End If
            End If

            If MetroFramework.MetroMessageBox.Show(Me, "Você tem certeza que quer Reiniciar/Desligar o Computador " & Nomeatual.Text & "?", "Reiniciar Computador/Desligar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                If MetroSetComboBox1.Text.ToLower.Contains("Reini".ToLower) Then
                    Shutdown(Nomeatual.Text, Usuario.Text, Senha.Text, True)
                    MetroFramework.MetroMessageBox.Show(Me, "Computador Reiniciado com sucesso", "ToolsIT", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                If MetroSetComboBox1.Text.ToLower.Contains("Deslig".ToLower) Then
                    Shutdown(Nomeatual.Text, Usuario.Text, Senha.Text, False)
                    MetroFramework.MetroMessageBox.Show(Me, "Computador Desligado com sucesso", "ToolsIT", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Public Sub RenameRemotePC(ByVal oldName As String, ByVal newName As String, ByVal domain As String, ByVal accountWithPermissions As Net.NetworkCredential)
        Try
            Dim remoteControlObject As ManagementPath = New ManagementPath()
            remoteControlObject.ClassName = "Win32_ComputerSystem"
            remoteControlObject.Server = oldName
            remoteControlObject.Path = oldName + "\root\cimv2:Win32_ComputerSystem.Name='" + oldName + "'"
            remoteControlObject.NamespacePath = "\\" + oldName + "\root\cimv2"
            Dim conn As ConnectionOptions = New ConnectionOptions()
            Dim remoteScope As ManagementScope = New ManagementScope(remoteControlObject, conn)
            remoteScope.Options.Authentication = AuthenticationLevel.PacketPrivacy
            remoteScope.Options.Impersonation = ImpersonationLevel.Impersonate
            Dim remoteSystem As ManagementObject = New ManagementObject(remoteScope, remoteControlObject, Nothing)
            Dim newRemoteSystemName As ManagementBaseObject = remoteSystem.GetMethodParameters("Rename")
            Dim methodOptions As InvokeMethodOptions = New InvokeMethodOptions
            newRemoteSystemName.SetPropertyValue("Name", newName)
            newRemoteSystemName.SetPropertyValue("UserName", accountWithPermissions.UserName)
            newRemoteSystemName.SetPropertyValue("Password", accountWithPermissions.Password)
            methodOptions.Timeout = New TimeSpan(0, 10, 0)
            Dim outParams As ManagementBaseObject = remoteSystem.InvokeMethod("Rename", newRemoteSystemName, Nothing)
            MetroFramework.MetroMessageBox.Show(Me, "Computaor Renomeado com sucesso", "ToolsIT", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' Form3.Close()
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Try
    End Sub
    Public Sub Shutdown(computerName As String, username As String, password As String, ByVal reboot As Boolean)
        Try
            Dim mgtScope As ManagementScope = Nothing
            Dim connectionOption As ConnectionOptions = Nothing
            Dim objectQuery As ObjectQuery = Nothing
            Dim objectSearcher As ManagementObjectSearcher = Nothing
            Try
                connectionOption = New ConnectionOptions()
                connectionOption.Impersonation = ImpersonationLevel.Impersonate
                connectionOption.EnablePrivileges = True
                'local machine
                If computerName.ToUpper() = Environment.MachineName.ToUpper() Then
                    mgtScope = New ManagementScope("\ROOT\CIMV2", connectionOption)
                Else
                    'remote machine
                    connectionOption.Username = username
                    connectionOption.Password = password
                    mgtScope = New ManagementScope("\\" & computerName & "\ROOT\CIMV2", connectionOption)
                End If
                mgtScope.Connect()
                objectQuery = New ObjectQuery("SELECT * FROM Win32_OperatingSystem")
                objectSearcher = New ManagementObjectSearcher(mgtScope, objectQuery)
                For Each os As ManagementObject In objectSearcher.[Get]()
                    If reboot = False Then
                        '  MessageBox.Show(computerName & " is going to Shutdown")
                        Dim outparam As ManagementBaseObject = os.InvokeMethod("Shutdown", Nothing, Nothing)
                    ElseIf reboot = True Then
                        'MessageBox.Show(computerName & " is going to Reboot")
                        Dim outparam As ManagementBaseObject = os.InvokeMethod("Reboot", Nothing, Nothing)
                    End If
                Next
            Catch excep As Exception
            Finally
            End Try
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
End Class