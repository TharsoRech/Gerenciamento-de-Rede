Imports System.DirectoryServices
Imports System.IO
Imports System.Management
Imports System.Net
Imports System.Reflection
Imports System.Text
Imports System.Text.RegularExpressions
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports MetroSet_UI.Child
Imports MetroSet_UI.Controls
Imports Microsoft.Exchange.WebServices.Data
Imports Microsoft.Win32
Imports Microsoft.Win32.TaskScheduler
Imports SnmpSharpNet
Imports Wisder.W3Common.WMetroControl.MessageBox

Public Class Form1
    Public Database As Database
    Public SelectTab2211 As String = ""
    Public SelectComputer As Servidor
    Public SelectPrinter As Impressoras
    Public SelectTab22 As String = ""
    Public Selbettireport As Boolean = False
    Public notasfaltantes As Integer = 0
    Public InicializeFirewall As Boolean = False
    Public indextabinfo As Integer = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If My.Application.CommandLineArgs.Count = 0 Then
                Iniciando.Show()
                Iniciando.MetroSetProgressBar1.Value = 20
                Application.DoEvents()
                Iniciando.Refresh()
            End If

            Dim folderdatabase As String = My.Application.Info.DirectoryPath & "\"
            My.Settings.DatabaseLocation = IO.Path.GetFullPath(My.Settings.DatabaseLocation)
            My.Settings.Save()
            If IO.Directory.Exists(My.Settings.DatabaseLocation) And My.Settings.DatabaseLocation <> "" Then
                folderdatabase = My.Settings.DatabaseLocation & "\"
            Else
                My.Settings.DatabaseLocation = folderdatabase
                My.Settings.Save()
            End If
            If IO.Directory.Exists(My.Settings.DatabaseLocation) Then
                folderdatabase = My.Settings.DatabaseLocation & "\"
                My.Settings.DatabaseLocation = folderdatabase
                My.Settings.Save()
            End If
            If IO.File.Exists(folderdatabase & "Database.Gdr") Then
                Dim infoReader As System.IO.FileInfo
                infoReader = My.Computer.FileSystem.GetFileInfo(folderdatabase & "Database.Gdr")
                Dim sizeInBytes As Long = infoReader.Length
                If IO.File.Exists(folderdatabase & "DatabaseBKP.Gdr") Then
                    Dim infoReader1 As System.IO.FileInfo
                    infoReader1 = My.Computer.FileSystem.GetFileInfo(folderdatabase & "DatabaseBKP.Gdr")
                    Dim sizeInBytes1 As Long = infoReader1.Length
                    If sizeInBytes = 0 Then
                        IO.File.Delete(folderdatabase & "Database.Gdr")
                        IO.File.Copy(folderdatabase & "DatabaseBKP.Gdr", folderdatabase & "Database.Gdr", True)
                        Application.Restart()
                    End If
                End If
            End If
            If IO.File.Exists(folderdatabase & "Database.Gdr") Then
                Dim seri As New ObjectSerializer(Of Database)
                Database = seri.GetSerializedObject(folderdatabase & "Database.Gdr")
                save()
            Else
                IO.File.Create(folderdatabase & "Database.Gdr").Close()
                Database = New Database
                Database.Computadores = New List(Of String)
                Database.Gruposad = New List(Of String)
                Database.Usuários = New List(Of Users)
                Database.Servidores = New List(Of Servidor)
                Database.Emails = New List(Of String)
                Database.Impressoras = New List(Of Impressoras)
                Database.Avisos = New List(Of AvisoseErrors)
                Database.Patrimonios = New List(Of Patrimonio)
                Database.Licenças = New List(Of Licença)
                Database.Destinatarios = New List(Of String)
                Database.ConexoesVNC = New List(Of VNCLast)
                save()
                Dim seri3 As New ObjectSerializer(Of Database)
                Database = seri3.GetSerializedObject(folderdatabase & "Database.Gdr")
            End If
            If My.Application.CommandLineArgs.Count = 0 Then
                Iniciando.MetroSetProgressBar1.Value = Iniciando.MetroSetProgressBar1.Value + 20
                Iniciando.MetroSetLabel1.Text = "Conectado ao Banco de dados,Inicializando Por Favor Espere..."
                Application.DoEvents()
                Iniciando.Refresh()
            End If
            If Database IsNot Nothing Then
                If IO.File.Exists(folderdatabase & "DatabaseBKP.Gdr") = False Then
                    IO.File.Create(folderdatabase & "DatabaseBKP.Gdr").Close()
                    Dim seri As New ObjectSerializer(Of Database)
                    seri.SaveSerializedObject(Database, folderdatabase & "DatabaseBKP.Gdr")
                Else
                    Dim seri As New ObjectSerializer(Of Database)
                    seri.SaveSerializedObject(Database, folderdatabase & "DatabaseBKP.Gdr")
                End If
            End If
            Me.Text = Me.Text & Space(1) & "-" & Space(1) & Environment.UserDomainName
            selectClick()
            If My.Application.CommandLineArgs.Count = 0 Then
                Iniciando.MetroSetProgressBar1.Value = Iniciando.MetroSetProgressBar1.Maximum
                Iniciando.Close()
            End If
            If My.Application.CommandLineArgs.Count > 0 Then
                MetroSetTabControl1.SelectedTab = MetroSetTabControl1.TabPages(2)
                For Each tr As TreeNode In ComputadoresChecked.Nodes
                    If tr.Checked = False Then
                        tr.Checked = True
                    End If
                Next
                For Each tr1 As TreeNode In ImpressorasChecked.Nodes
                    If tr1.Checked = False Then
                        tr1.Checked = True
                    End If
                Next
                Scanear.Checked = True
                Application.DoEvents()
                ScanAll.RunWorkerAsync()
            End If
            Me.Size = Screen.PrimaryScreen.WorkingArea.Size

        Catch ex As Exception

        End Try
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
    Public Sub listuser(ByVal domain As String)

        Try

            Database.Usuários.Clear()
            Dim email As String
            Dim displayname As String
            Dim samname As String
            Dim objectSid As String
            Dim searchRoot As DirectoryServices.DirectoryEntry = New DirectoryServices.DirectoryEntry("LDAP://" & domain)
            Dim search As DirectoryServices.DirectorySearcher = New DirectoryServices.DirectorySearcher(searchRoot)
            search.Filter = "(&(objectClass=user)(objectCategory=person))"
            search.PropertiesToLoad.Add("samaccountname")
            search.PropertiesToLoad.Add("mail")
            search.PropertiesToLoad.Add("displayname")
            search.PropertiesToLoad.Add("distinguishedName")
            'first Name
            Dim result As DirectoryServices.SearchResult
            Dim resultCol As DirectoryServices.SearchResultCollection = search.FindAll
            Dim counter As Integer = 0

            Do While (counter < resultCol.Count)
                Dim UserNameEmailString As String = String.Empty
                result = resultCol(counter)
                If (result.Properties.Contains("samaccountname") _
                             AndAlso result.Properties.Contains("displayname")) Then
                    If GetProperty(result, "mail") IsNot Nothing Then
                        email = (GetProperty(result, "mail"))
                    Else
                        email = String.Empty
                    End If
                    samname = CType(result.Properties("samaccountname")(0), String)
                    objectSid = CType(result.Properties("distinguishedName")(0), String)
                    displayname = CType(result.Properties("displayname")(0), String)
                    Dim row1() As String = New String() {samname, email, displayname, objectSid}
                    Dim newuser As New Users
                    newuser.Usuário = samname
                    newuser.Email = email
                    newuser.NomeCompleto = displayname
                    newuser.ConfiguraçãoAd = objectSid
                    Database.Usuários.Add(newuser)
                    UsersList.Rows.Add(row1)
                End If
                counter = (counter + 1)
            Loop
        Catch ex As Exception

        End Try
    End Sub
    Private Shared Function GetProperty(ByVal searchResult As SearchResult, ByVal PropertyName As String) As String
        If searchResult.Properties.Contains(PropertyName) Then
            Return searchResult.Properties(PropertyName)(0).ToString
        Else
            Return String.Empty
        End If
    End Function

    Public Sub save()
        Try
            Dim seri As New ObjectSerializer(Of Database)
            seri.SaveSerializedObject(Database, My.Settings.DatabaseLocation & "Database.Gdr")
        Catch ex As Exception
            While IsFileLocked(My.Settings.DatabaseLocation & "Database.Gdr")
                Application.DoEvents()
            End While
            save()
        End Try
    End Sub
    Public Function IsFileLocked(ByVal filename As String) As Boolean
        Dim Locked As Boolean = False
        Try
            Dim fs As FileStream = File.Open(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)
            fs.Close()
        Catch ex As IOException
            Locked = True
        End Try

        Return Locked
    End Function
    Public Function IsFileLocked(ByVal filename As String, ByVal Read As Boolean) As Boolean
        Dim Locked As Boolean = False
        Try
            Dim fs As FileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read)
            fs.Close()
        Catch ex As IOException
            Locked = True
        End Try

        Return Locked
    End Function

    Private Sub selectClick() Handles MetroSetTabControl1.SelectedIndexChanged
        Try
            Application.DoEvents()
            If MetroSetTabControl1.SelectedTab.Text.ToLower.Contains("configurações".ToLower) Then
                DatabaseLocation.Text = IO.Path.GetFullPath(My.Settings.DatabaseLocation)
                Email.Text = Database.Emailadmin
                Senha.Text = Database.Senhaadmin
                Servidor.Text = Database.servidorEmail
                VncPass.Text = Database.VNCpassword
                MetroGrid2.Rows.Clear()
                For Each x55 As String In Database.Destinatarios
                    MetroGrid2.Rows.Add(x55)
                Next
                Exit Sub
            End If
            Application.DoEvents()
            If MetroSetTabControl1.SelectedTab.Text.ToLower.Contains("Navegador".ToLower) Then
                If GRbrowser1.Atalhos.Controls.Count = 0 Then
                    If Database.Favoritos Is Nothing Then
                        Database.Favoritos = New List(Of Favorito)
                        save()
                    End If
                    For Each x2 As Favorito In Database.Favoritos
                        Dim newat As New AtalhoControl
                        newat.Nome.Text = x2.Nome
                        GRbrowser1.Atalhos.Controls.Add(newat)
                    Next
                End If
            End If
                Application.DoEvents()
            If MetroSetTabControl1.SelectedTab.Text.ToLower.Contains("rede".ToLower) Then
                RedeProgressBar.Value = 0
                RedeProgressBar.Maximum = 10
                RedeProgressBar.Value = RedeProgressBar.Value + 1
                Computadores.Items.Clear()
                Grupos.Items.Clear()
                UsersList.Rows.Clear()
                MetroSetRichTextBox2.Text = ""
                RedeProgressBar.Value = RedeProgressBar.Value + 1
                For Each x1 As String In Database.Computadores
                    Computadores.Items.Add(x1)
                Next
                RedeProgressBar.Value = RedeProgressBar.Value + 1
                For Each x10 As String In Database.Gruposad
                    Grupos.Items.Add(x10)
                Next
                Dim q As ArrayList = New ArrayList
                For Each o As Object In Grupos.Items
                    q.Add(o)
                Next
                q.Sort()
                Grupos.Items.Clear()
                For Each o As Object In q
                    Grupos.Items.Add(o)
                Next
                RedeProgressBar.Value = RedeProgressBar.Value + 1
                For Each x11 As Users In Database.Usuários
                    UsersList.Rows.Add({x11.Usuário, x11.Email, x11.NomeCompleto, x11.ConfiguraçãoAd})
                Next
                MetroSetRichTextBox2.AppendText("Nome Dominio:" & Database.Domain & Environment.NewLine)
                RedeProgressBar.Value = RedeProgressBar.Value + 1
                MetroSetRichTextBox2.AppendText("Usuarios(total):" & UsersList.RowCount & Environment.NewLine)
                RedeProgressBar.Value = RedeProgressBar.Value + 1
                MetroSetRichTextBox2.AppendText("Computadores(total):" & Computadores.Count & Environment.NewLine)
                RedeProgressBar.Value = RedeProgressBar.Value + 1
                MetroSetRichTextBox2.AppendText("Server: " & Database.ServidorPrincipal & Environment.NewLine)
                MetroSetRichTextBox2.AppendText("Page Size: " & Database.PageSize & Environment.NewLine)
                RedeProgressBar.Value = RedeProgressBar.Value + 1
                MetroSetRichTextBox2.AppendText("Password Encoding: " & Database.PasswordEncoding & Environment.NewLine)
                MetroSetRichTextBox2.AppendText("Password Port: " & Database.PasswordPort & Environment.NewLine)
                RedeProgressBar.Value = RedeProgressBar.Value + 1
                MetroSetRichTextBox2.AppendText("Referral: " & Database.Referral & Environment.NewLine)
                MetroSetRichTextBox2.AppendText("Security Masks: " & Database.SecurityMasks & Environment.NewLine)
                MetroSetRichTextBox2.AppendText("Is Mutually Authenticated: " & Database.IsMutuallyAuthenticated & Environment.NewLine)
                RedeProgressBar.Value = RedeProgressBar.Value + 1
                Exit Sub
            End If
            Application.DoEvents()
            If MetroSetTabControl1.SelectedTab.Text.ToLower.Contains("Servidores".ToLower) Then
                If ServidoresList.Controls.Count < 2 And Database.Servidores.Count > 0 Then
                    atualizarlista()
                End If
                Exit Sub
            End If
            Application.DoEvents()
            If MetroSetTabControl1.SelectedTab.Text.ToLower.Contains("patrimônio".ToLower) Then
                Reload()
                Reload2()
                Exit Sub
            End If
            Application.DoEvents()
            If MetroSetTabControl1.SelectedTab.Text.ToLower.Contains("Impressoras".ToLower) Then
                If ImpressorasLista.Controls.Count < 2 Then
                    atualizarlistaImpressoras()
                End If
                Exit Sub
            End If
            If MetroSetTabControl1.SelectedTab.Text.ToLower.Contains("vnc".ToLower) Then
                ReloadConexoesVNC()
                Exit Sub
            End If
            Application.DoEvents()
            If MetroSetTabControl1.SelectedTab.Text.ToLower.Contains("Monitoramento".ToLower) And ScanAll.IsBusy = False Then
                ImpressorasChecked.Nodes.Clear()
                For Each x1 As Impressoras In Database.Impressoras
                    ImpressorasChecked.Nodes.Add(x1.nome)
                Next
                ComputadoresChecked.Nodes.Clear()
                For Each x10 As Servidor In Database.Servidores
                    ComputadoresChecked.Nodes.Add(x10.ComputerName)
                Next
                Avisoseerros.Rows.Clear()
                If Database.Avisos Is Nothing Then
                    Database.Avisos = New List(Of AvisoseErrors)
                    save()
                End If
                AvisosProgressBar.Value = 0
                If Database.Avisos.Count > 0 Then
                    AvisosProgressBar.Maximum = Database.Avisos.Count
                    For Each x11 As AvisoseErrors In Database.Avisos
                        AvisosProgressBar.Value = AvisosProgressBar.Value + 1
                        Avisoseerros.Rows.Add({x11.Erro, x11.PrinterName, x11.Selb, x11.Porta, x11.TonerDisponievis, x11.Data})
                    Next
                End If
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function CheckedNames(theNodes As System.Windows.Forms.TreeNodeCollection) As List(Of [String])
        Dim aResult As New List(Of [String])()

        If theNodes IsNot Nothing Then
            For Each aNode As System.Windows.Forms.TreeNode In theNodes
                If aNode.Checked Then
                    aResult.Add(aNode.Text)
                End If

                aResult.AddRange(CheckedNames(aNode.Nodes))
            Next
        End If

        Return aResult
    End Function


    Public Sub listgroups(ByVal domain As String)

        Try
            Database.Gruposad.Clear()

            Dim dSearch As New DirectorySearcher(domain)
            dSearch.Filter = "(&(objectClass=group))"
            dSearch.SearchScope = SearchScope.Subtree

            Dim results As SearchResultCollection = dSearch.FindAll()

            For i As Integer = 0 To results.Count - 1

                'TODO with "de"
                Dim de As DirectoryEntry = results(i).GetDirectoryEntry()
                Grupos.Items.Add(de.Properties("Name").Value)
                Database.Gruposad.Add(de.Properties("Name").Value)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Sub DirectoryEntryConfigurationSettings(domainADsPath As String)
        ' Bind to current domain
        Try
            Dim entry As New DirectoryEntry(domainADsPath)
            Dim entryConfiguration As DirectoryEntryConfiguration = entry.Options

            MetroSetRichTextBox2.AppendText("Server: " & entryConfiguration.GetCurrentServerName() & Environment.NewLine)
            MetroSetRichTextBox2.AppendText("Page Size: " & entryConfiguration.PageSize.ToString() & Environment.NewLine)
            MetroSetRichTextBox2.AppendText("Password Encoding: " & entryConfiguration.PasswordEncoding.ToString() & Environment.NewLine)
            MetroSetRichTextBox2.AppendText("Password Port: " & entryConfiguration.PasswordPort.ToString() & Environment.NewLine)
            MetroSetRichTextBox2.AppendText("Referral: " & entryConfiguration.Referral.ToString() & Environment.NewLine)
            MetroSetRichTextBox2.AppendText("Security Masks: " & entryConfiguration.SecurityMasks.ToString() & Environment.NewLine)
            MetroSetRichTextBox2.AppendText("Is Mutually Authenticated: " & entryConfiguration.IsMutuallyAuthenticated().ToString() & Environment.NewLine)
            Database.ServidorPrincipal = entryConfiguration.GetCurrentServerName()
            Database.PageSize = entryConfiguration.PageSize.ToString()
            Database.PasswordEncoding = entryConfiguration.PasswordEncoding.ToString()
            Database.PasswordPort = entryConfiguration.PasswordPort.ToString()
            Database.Referral = entryConfiguration.Referral.ToString()
            Database.SecurityMasks = entryConfiguration.SecurityMasks.ToString()
            Database.IsMutuallyAuthenticated = entryConfiguration.IsMutuallyAuthenticated().ToString()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton10_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton10.Click
        Try
            Process.Start(DatabaseLocation.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton9_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton9.Click
        Try
            FolderBrowserDialog1.SelectedPath = DatabaseLocation.Text
            If Me.FolderBrowserDialog1.ShowDialog = Global.System.Windows.Forms.DialogResult.OK Then
                My.Settings.DatabaseLocation = FolderBrowserDialog1.SelectedPath
                My.Settings.Save()
                DatabaseLocation.Text = IO.Path.GetFullPath(My.Settings.DatabaseLocation)
                MetroFramework.MetroMessageBox.Show(Me, "Localização do database  Alterada com sucesso", "Alteração concluida", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception

        End Try
    End Sub





    Private Sub MetroDefaultSetButton14_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton14.Click

        Try
            Dim Prompt As String = "Informe o nome do servidor"
            Dim Titulo As String = "Adicionar servidor"
            Dim valorRetornado As String = ""

            valorRetornado = InputBox(Prompt, Titulo)
            If valorRetornado = "" Then
                MetroFramework.MetroMessageBox.Show(Me, "servidor não pode ser em branco", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Dim alreadyhave As Boolean = False
            For Each x1 As Servidor In Database.Servidores
                If x1.ComputerName = valorRetornado Then
                    alreadyhave = True
                    Exit For
                End If
            Next
            If alreadyhave = False Then
                Dim newp As New Servidor
                newp.ComputerName = valorRetornado
                Database.Servidores.Add(newp)
                save()
                Dim newp1 As New Servidorcontrol
                newp1.MetroSetBadge1.Text = newp.ComputerName
                newp1.Dock = DockStyle.Top
                If ping(newp.ComputerName) Then
                    newp1.MetroSetBadge1.NormalBadgeColor = Color.DarkGreen
                    newp1.MetroSetBadge1.BadgeText = "ON"
                Else
                    newp1.MetroSetBadge1.NormalBadgeColor = Color.DarkRed
                    newp1.MetroSetBadge1.BadgeText = "Off"
                End If
                AddHandler newp1.MetroSetBadge1.Click, Sub() clickprinter(newp1, EventArgs.Empty)
                ServidoresList.Controls.Add(newp1)
                MetroFramework.MetroMessageBox.Show(Me, "Servidor adicionado com sucesso", "Servidor adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MetroFramework.MetroMessageBox.Show(Me, "Servidor ja cadastrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroSetButton1_Click(sender As Object, e As EventArgs) Handles MetroSetButton1.Click
        Try
            RedeProgressBar.Value = 0
            RedeProgressBar.Maximum = 10
            RedeProgressBar.Value = RedeProgressBar.Value + 1
            Dim Domain As String = Environment.UserDomainName
            Database.Domain = Domain
            Database.Computadores.Clear()
            Dim domainEntry As DirectoryServices.DirectoryEntry = New DirectoryServices.DirectoryEntry("WinNT://" + Domain)
            domainEntry.Children.SchemaFilter.Add("Computer")
            RedeProgressBar.Value = RedeProgressBar.Value + 1
            For Each computer As DirectoryServices.DirectoryEntry In domainEntry.Children
                Computadores.Items.Add(computer.Name)
                Database.Computadores.Add(computer.Name)
            Next
            RedeProgressBar.Value = RedeProgressBar.Value + 1
            'gteusers(Domain)
            listuser(Domain)
            RedeProgressBar.Value = RedeProgressBar.Value + 1
            MetroSetRichTextBox2.AppendText("Nome Dominio:" & Domain & Environment.NewLine)
            RedeProgressBar.Value = RedeProgressBar.Value + 1
            MetroSetRichTextBox2.AppendText("Usuarios(total):" & UsersList.RowCount & Environment.NewLine)
            RedeProgressBar.Value = RedeProgressBar.Value + 1
            MetroSetRichTextBox2.AppendText("Computadores(total):" & Computadores.Count & Environment.NewLine)
            RedeProgressBar.Value = RedeProgressBar.Value + 1
            DirectoryEntryConfigurationSettings("LDAP://" & Domain)
            RedeProgressBar.Value = RedeProgressBar.Value + 1
            listgroups("LDAP://" & Domain)
            RedeProgressBar.Value = RedeProgressBar.Value + 1
            save()
            RedeProgressBar.Value = RedeProgressBar.Value + 1
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton22_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton22.Click
        Try
            GruposeUsuários.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton21_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton21.Click
        Try
            GruposeUsuários.Show()
            GruposeUsuários.MetroSetTabControl1.SelectedIndex = 1
        Catch ex As Exception

        End Try
    End Sub

    Private Sub selectClick(sender As Object, e As EventArgs) Handles MetroSetTabControl1.SelectedIndexChanged

    End Sub

    Private Sub RemoverDaListaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoverDaListaToolStripMenuItem.Click
        Try
            For Each x1 As Servidor In Database.Servidores
                If x1.ComputerName = SelectComputer.ComputerName Then
                    Database.Servidores.Remove(x1)
                    save()
                    For Each x10 As Control In ServidoresList.Controls
                        If x10.GetType() Is GetType(Servidorcontrol) Then
                            Dim p1 As Servidorcontrol = x10
                            If p1.MetroSetBadge1.Text = SelectComputer.ComputerName Then
                                ServidoresList.Controls.Remove(x10)
                            End If
                        End If
                    Next
                End If
            Next
            MetroSetContextMenuStrip1.Hide()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub clickprinter(ByVal sender As Object, e As EventArgs)
        Try
            If ScanerComputer.IsBusy = False Then
                If sender.GetType() Is GetType(Servidorcontrol) Then
                    Dim p1 As Servidorcontrol = sender
                    If Database IsNot Nothing Then
                        If Database.Servidores IsNot Nothing Then
                            Dim SelectTab221 As String = MetroSetTabControl3.SelectedTab.Text
                            For Each x1 As Servidor In Database.Servidores
                                If x1.ComputerName = p1.MetroSetBadge1.Text Then
                                    SelectComputer = x1
                                    ComputerName.Text = x1.ComputerName
                                    ComputerName.Refresh()
                                    OperatingSystem.Text = x1.OperatingSystem
                                    BuildNumber.Text = x1.BuildNumber
                                    CurrentUser.Text = x1.CurrentUser
                                    DefaultIPGateway.Text = x1.DefaultIPGateway
                                    IPAddress.Text = x1.IPAddress
                                    LastBootUpTime.Text = x1.LastBootUpTime
                                    LocalDateTime.Text = x1.LocalDateTime
                                    MACAddress.Text = x1.MACAddress
                                    Manufacturer.Text = x1.Manufacturer
                                    Model.Text = x1.Model
                                    OSArchitecture.Text = x1.OSArchitecture
                                    SerialNumber.Text = x1.SerialNumber
                                    Status.Text = x1.Status
                                    TotalVisibleMemorySize.Text = x1.TotalVisibleMemorySize
                                    Version.Text = x1.Version
                                    WindowsDirectory.Text = x1.WindowsDirectory
                                    InstallDate.Text = x1.InstallDate
                                    UltimaScaneada.Text = "Informações Scaneadas na data:" & x1.Ultimovezscaneada
                                    SofttwaresGrid.Rows.Clear()
                                    For Each st As Software In x1.Softwares
                                        SofttwaresGrid.Rows.Add(New String() {st.Name, st.InstallDate, st.Pacote, st.Versão})
                                    Next
                                    ProcessadorGrid.Rows.Clear()
                                    For Each pro As Processador In x1.Processadores
                                        ProcessadorGrid.Rows.Add(New String() {pro.Name, pro.Fabricante, pro.Descrição, pro.TipodeProcessador})
                                    Next
                                    ProcessosGrid.Rows.Clear()
                                    For Each pr As Processos In x1.Processos
                                        ProcessosGrid.Rows.Add(New String() {pr.Caption, pr.ComputerName, pr.Description, pr.Name, pr.Priority, pr.ProcessID, pr.SessionID, pr.User})
                                    Next
                                    DriversGrid.Rows.Clear()
                                    For Each dr As Drivers In x1.drivers
                                        DriversGrid.Rows.Add(New String() {dr.Name, dr.Location, dr.ID})
                                    Next
                                    PrintersGrid.Rows.Clear()
                                    For Each printer1 As Impressoras In x1.Impressoras
                                        PrintersGrid.Rows.Add(New String() {printer1.Name, printer1.Location, printer1.Server, printer1.Driver})
                                    Next
                                    LicencasGrid.Rows.Clear()
                                    For Each li As Licença In x1.Licenças
                                        LicencasGrid.Rows.Add(New String() {li.Tipo, li.Key, li.Produto, li.Versão})
                                    Next
                                    UserLocalGrid.Rows.Clear()
                                    For Each us As UserLocalAccount In x1.UsersLocal
                                        UserLocalGrid.Rows.Add(New String() {us.Name, us.AccountType, us.Domain, us.Status})
                                    Next
                                    DiscosGrid.Rows.Clear()
                                    For Each ds As Discos In x1.discos
                                        DiscosGrid.Rows.Add(New String() {ds.size, ds.FreeSpace})
                                    Next
                                    Dns.Text = ""
                                    For Each dns1 As String In x1.Dns
                                        Dns.Text = Dns.Text & dns1 & ";"
                                    Next
                                    For Each x3 As TabPage In MetroSetTabControl3.TabPages
                                        If x3.Text.ToLower = SelectTab221.ToLower Then
                                            MetroSetTabControl3.SelectedTab = x3
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    End If
                End If
            Else
                MetroFramework.MetroMessageBox.Show(Me, "Não é possivel selecionar ,termine o scaneamento antes,para cancelar vá na barra de progresso  com o botão direito do mouse", "scaneamento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub clickPC(ByVal pc As Servidor)
        Try
            If ScanerComputer.IsBusy = False Then
                Dim SelectTab221 As String = MetroSetTabControl3.SelectedTab.Text
                If Database IsNot Nothing Then
                    If Database.Servidores IsNot Nothing Then
                        For Each x1 As Servidor In Database.Servidores
                            If x1.ComputerName = pc.ComputerName Then
                                SelectComputer = x1
                                ComputerName.Text = x1.ComputerName
                                ComputerName.Refresh()
                                OperatingSystem.Text = x1.OperatingSystem
                                BuildNumber.Text = x1.BuildNumber
                                CurrentUser.Text = x1.CurrentUser
                                DefaultIPGateway.Text = x1.DefaultIPGateway
                                IPAddress.Text = x1.IPAddress
                                LastBootUpTime.Text = x1.LastBootUpTime
                                LocalDateTime.Text = x1.LocalDateTime
                                MACAddress.Text = x1.MACAddress
                                Manufacturer.Text = x1.Manufacturer
                                Model.Text = x1.Model
                                OSArchitecture.Text = x1.OSArchitecture
                                SerialNumber.Text = x1.SerialNumber
                                Status.Text = x1.Status
                                TotalVisibleMemorySize.Text = x1.TotalVisibleMemorySize
                                Version.Text = x1.Version
                                WindowsDirectory.Text = x1.WindowsDirectory
                                InstallDate.Text = x1.InstallDate
                                UltimaScaneada.Text = "Informações Scaneadas na data:" & x1.Ultimovezscaneada
                                SofttwaresGrid.Rows.Clear()
                                For Each st As Software In x1.Softwares
                                    SofttwaresGrid.Rows.Add(New String() {st.Name, st.InstallDate, st.Pacote, st.Versão})
                                Next
                                ProcessadorGrid.Rows.Clear()
                                For Each pro As Processador In x1.Processadores
                                    ProcessadorGrid.Rows.Add(New String() {pro.Name, pro.Fabricante, pro.Descrição, pro.TipodeProcessador})
                                Next
                                ProcessosGrid.Rows.Clear()
                                For Each pr As Processos In x1.Processos
                                    ProcessosGrid.Rows.Add(New String() {pr.Caption, pr.ComputerName, pr.Description, pr.Name, pr.Priority, pr.ProcessID, pr.SessionID, pr.User})
                                Next
                                DriversGrid.Rows.Clear()
                                For Each dr As Drivers In x1.drivers
                                    DriversGrid.Rows.Add(New String() {dr.Name, dr.Location, dr.ID})
                                Next
                                PrintersGrid.Rows.Clear()
                                For Each printer1 As Impressoras In x1.Impressoras
                                    PrintersGrid.Rows.Add(New String() {printer1.Name, printer1.Location, printer1.Server, printer1.Driver})
                                Next
                                LicencasGrid.Rows.Clear()
                                For Each li As Licença In x1.Licenças
                                    LicencasGrid.Rows.Add(New String() {li.Tipo, li.Key, li.Produto, li.Versão})
                                Next
                                UserLocalGrid.Rows.Clear()
                                For Each us As UserLocalAccount In x1.UsersLocal
                                    UserLocalGrid.Rows.Add(New String() {us.Name, us.AccountType, us.Domain, us.Status})
                                Next
                                DiscosGrid.Rows.Clear()
                                For Each ds As Discos In x1.discos
                                    DiscosGrid.Rows.Add(New String() {ds.size, ds.FreeSpace})
                                Next
                                Dns.Text = ""
                                For Each dns1 As String In x1.Dns
                                    Dns.Text = Dns.Text & dns1 & ";"
                                Next
                                For Each x3 As TabPage In MetroSetTabControl3.TabPages
                                    If x3.Text.ToLower = SelectTab221.ToLower Then
                                        MetroSetTabControl3.SelectedTab = x3
                                    End If
                                Next
                            End If
                        Next
                    End If
                End If
            Else
                MetroFramework.MetroMessageBox.Show(Me, "Não é possivel selecionar ,termine o scaneamento antes,para cancelar vá na barra de progresso  com o botão direito do mouse", "scaneamento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

            End If

        Catch ex As Exception

        End Try
    End Sub


    Public Sub atualizarlista()
        Try
            ServidoresList.Controls.Clear()
            ServidoresProgressBar.Value = 0
            ServidoresProgressBar.Maximum = Database.Servidores.Count
            For Each x11 As Servidor In Database.Servidores.OrderByDescending(Function(x) x.ComputerName)
                Application.DoEvents()
                Dim newp As New Servidorcontrol
                newp.MetroSetBadge1.Text = x11.ComputerName
                newp.Dock = DockStyle.Top
                AddHandler newp.MetroSetBadge1.Click, Sub() clickprinter(newp, EventArgs.Empty)
                ServidoresList.Controls.Add(newp)
                ServidoresProgressBar.Value = ServidoresProgressBar.Value + 1
                Application.DoEvents()
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Sub atualizarlistaImpressoras()
        Try

            ImpressorasLista.Controls.Clear()
            ImpressorasProgressBar1.Value = 0
            If Database.Impressoras.Count > 0 Then
                ImpressorasProgressBar1.Maximum = Database.Impressoras.Count
            End If
            For Each x11 As Impressoras In Database.Impressoras
                Application.DoEvents()
                Dim newp As New Servidorcontrol
                newp.MetroSetBadge1.Text = x11.nome
                newp.Dock = DockStyle.Top
                If ping(GetIp(x11.Porta)) Then
                    newp.MetroSetBadge1.NormalBadgeColor = Color.DarkGreen
                    newp.MetroSetBadge1.BadgeText = "ON"
                Else
                    newp.MetroSetBadge1.NormalBadgeColor = Color.Red
                    newp.MetroSetBadge1.BadgeText = "Off"
                End If
                newp.Panel2.Visible = True
                newp.Panel1.Visible = False
                AddHandler newp.MetroSetBadge1.Click, Sub() clickprinter2(newp, EventArgs.Empty)
                ImpressorasProgressBar1.Value = ImpressorasProgressBar1.Value + 1
                ImpressorasLista.Controls.Add(newp)
                Application.DoEvents()
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Sub clickprinter2(ByVal sender As Object, e As EventArgs)
        Try
            If StartCollect1.IsBusy = False Then
                If sender.GetType() Is GetType(Servidorcontrol) Then
                    Dim p1 As Servidorcontrol = sender
                    If Database IsNot Nothing Then
                        If Database.Impressoras IsNot Nothing Then
                            Dim lasttab As String = SelectTab22
                            For Each x1 As Impressoras In Database.Impressoras
                                If x1.nome = p1.MetroSetBadge1.Text Then
                                    SelectPrinter = x1
                                    NomeImpressora.Text = x1.nome
                                    NomeCompartilhado.Text = x1.NomeCompartilhado
                                    NomedoServidor.Text = x1.NomedoServidor
                                    PaginasContadas.Text = x1.PáginasContadas
                                    Porta.Text = x1.Porta
                                    Selb.Text = x1.Selb
                                    TipodeToner.Text = x1.Toner
                                    TonerRestante.Text = x1.TonerRestante
                                    Id.Text = x1.ID
                                    Driver.Text = x1.Driver
                                    Comentarios.Text = x1.Comentarios
                                    locationL.Text = x1.localização
                                    UltimaColetaFeita.Text = "Ultima Coleta Feita:" & x1.UltimaVezColletada
                                    UltimaTrocadeToner.Text = x1.UltimaTrocadeToner
                                    StatusPrinter.Text = x1.Status
                                    SystemName.Text = x1.SystemName
                                    Descrição.Text = x1.Descrição
                                    TonerDisponivel.Text = x1.QuantidadedeTonerDisponivel
                                    If lasttab.ToLower.Contains("Controle".ToLower) Then
                                        MetroSetTabControl4.SelectedTab = MetroSetTabControl4.TabPages(1)
                                    End If
                                    NomeImpressora.Refresh()
                                End If
                            Next
                        End If
                    End If
                End If
            Else
                MetroFramework.MetroMessageBox.Show(Me, "Não é possivel selecionar ,termine o scaneamento antes,para cancelar vá na barra de progresso  com o botão direito do mouse", "scaneamento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

            End If
        Catch ex As Exception

        End Try
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

    Private Sub MetroDefaultSetButton1_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton1.Click
        Try
            For Each valorRetornado As String In Database.Computadores
                Dim alreadyhave As Boolean = False
                For Each x1 As Servidor In Database.Servidores
                    If x1.ComputerName = valorRetornado Then
                        alreadyhave = True
                        Exit For
                    End If
                Next
                If alreadyhave = False Then
                    Application.DoEvents()
                    Dim newp As New Servidor
                    newp.ComputerName = valorRetornado
                    Database.Servidores.Add(newp)
                    save()
                    Application.DoEvents()
                    Dim newp1 As New Servidorcontrol
                    newp1.MetroSetBadge1.Text = newp.ComputerName
                    newp1.Dock = DockStyle.Top
                    AddHandler newp1.MetroSetBadge1.Click, Sub() clickprinter(newp1, EventArgs.Empty)
                    ServidoresList.Controls.Add(newp1)
                    Application.DoEvents()
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Sub scancomputerdone() Handles ScanerComputer.RunWorkerCompleted
        Try
            Me.Invoke(Sub() ServidoresProgressBar.Value = 0)
            clickPC(SelectComputer)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ScanerComputer_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles ScanerComputer.DoWork
        Try
            Me.Invoke(Sub() ServidoresProgressBar.Value = 0)
            Me.Invoke(Sub() ServidoresProgressBar.Maximum = 11)
            If ping(SelectComputer.ComputerName) Then
                Getallinfo(SelectComputer)
                save()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub Getallinfo(ByVal pc As Servidor)

        On Error Resume Next
        Getpcinfo(pc.ComputerName)
        Me.Invoke(Sub() ServidoresProgressBar.Value = ServidoresProgressBar.Value + 1)
        Me.Invoke(Sub() ServidoresProgressBar.Update())
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        GetComputerSystemInfo(pc.ComputerName)
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If

        Me.Invoke(Sub() ServidoresProgressBar.Value = ServidoresProgressBar.Value + 1)
        Me.Invoke(Sub() ServidoresProgressBar.Update())
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If

        If pc.OSArchitecture.Contains("64") Then
            Dim key1 As String = getWindowskey(pc.ComputerName, RegistryView.Registry64)
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If key1 <> "" Then
                pc.Licenças.Clear()
                Dim newl As New Licença
                newl.Tipo = pc.OperatingSystem
                newl.Key = key1
                newl.Produto = ""
                newl.Versão = ""
                AtualizarLicença(pc.ComputerName, newl)
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            getMicrosoftKey(pc.ComputerName, RegistryView.Registry64)
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            getNXkey(pc.ComputerName, RegistryView.Registry64)
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
        Else
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            Dim key1 As String = getWindowskey(pc.ComputerName, RegistryView.Registry32)
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If key1 <> "" Then
                pc.Licenças.Clear()
                Dim newl As New Licença
                newl.Tipo = pc.OperatingSystem
                newl.Key = key1
                newl.Produto = ""
                newl.Versão = ""
                AtualizarLicença(pc.ComputerName, newl)
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            getMicrosoftKey(pc.ComputerName, RegistryView.Registry32)
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            getNXkey(pc.ComputerName, RegistryView.Registry64)
        End If
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        pc.drivers.Clear()
        getDriverInfo(pc.ComputerName)
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        Me.Invoke(Sub() ServidoresProgressBar.Value = ServidoresProgressBar.Value + 1)
        Me.Invoke(Sub() ServidoresProgressBar.Update())
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        pc.discos.Clear()
        getinfodiscos(pc.ComputerName)
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If

        Me.Invoke(Sub() ServidoresProgressBar.Value = ServidoresProgressBar.Value + 1)
        Me.Invoke(Sub() ServidoresProgressBar.Update())
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        pc.Dns.Clear()
        GetInfonetwork(pc.ComputerName)
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If

        Me.Invoke(Sub() ServidoresProgressBar.Value = ServidoresProgressBar.Value + 1)
        Me.Invoke(Sub() ServidoresProgressBar.Update())
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        pc.Impressoras.Clear()
        GetPrinterInfo(pc.ComputerName)
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If

        Me.Invoke(Sub() ServidoresProgressBar.Value = ServidoresProgressBar.Value + 1)
        Me.Invoke(Sub() ServidoresProgressBar.Update())
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        pc.Processadores.Clear()
        GetProcessadorInfo(pc.ComputerName)
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If

        Me.Invoke(Sub() ServidoresProgressBar.Value = ServidoresProgressBar.Value + 1)
        Me.Invoke(Sub() ServidoresProgressBar.Update())
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        pc.Softwares.Clear()
        GetSoftwareInfo(pc.ComputerName)
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If

        Me.Invoke(Sub() ServidoresProgressBar.Value = ServidoresProgressBar.Value + 1)
        Me.Invoke(Sub() ServidoresProgressBar.Update())
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        pc.UsersLocal.Clear()
        GetUsersLocalInfo(pc.ComputerName)
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If

        Me.Invoke(Sub() ServidoresProgressBar.Value = ServidoresProgressBar.Value + 1)
        Me.Invoke(Sub() ServidoresProgressBar.Update())
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        pc.Processos.Clear()
        GetProcessosInfo(pc.ComputerName)
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        Me.Invoke(Sub() ServidoresProgressBar.Value = ServidoresProgressBar.Value + 1)
        Me.Invoke(Sub() ServidoresProgressBar.Update())
        Me.Invoke(Sub() pc.Ultimovezscaneada = DateTime.Now.ToString)
    End Sub
    Public Sub Getallinfo2(ByVal pc As Servidor)

        On Error Resume Next

        Getpcinfo(pc)
        If ScanAll.CancellationPending Then
            Exit Sub
        End If
        GetComputerSystemInfo(pc)
        If ScanAll.CancellationPending Then
            Exit Sub
        End If

        If pc.OSArchitecture.Contains("64") Then
            Dim key1 As String = getWindowskey(pc.ComputerName, RegistryView.Registry64)
            If ScanAll.CancellationPending Then
                Exit Sub
            End If
            If key1 <> "" Then
                pc.Licenças.Clear()
                Dim newl As New Licença
                newl.Tipo = pc.OperatingSystem
                newl.Key = key1
                newl.Produto = ""
                newl.Versão = ""
                AtualizarLicença(pc.ComputerName, newl)
            End If
            If ScanAll.CancellationPending Then
                Exit Sub
            End If
            getMicrosoftKey(pc, RegistryView.Registry64)
            If ScanAll.CancellationPending Then
                Exit Sub
            End If
            getNXkey(pc, RegistryView.Registry64)
            If ScanAll.CancellationPending Then
                Exit Sub
            End If
        Else
            If ScanAll.CancellationPending Then
                Exit Sub
            End If
            Dim key1 As String = getWindowskey(pc.ComputerName, RegistryView.Registry32)
            If ScanAll.CancellationPending Then
                Exit Sub
            End If
            If key1 <> "" Then
                pc.Licenças.Clear()
                Dim newl As New Licença
                newl.Tipo = pc.OperatingSystem
                newl.Key = key1
                newl.Produto = ""
                newl.Versão = ""
                AtualizarLicença(pc.ComputerName, newl)
            End If
            If ScanAll.CancellationPending Then
                Exit Sub
            End If
            getMicrosoftKey(pc, RegistryView.Registry32)
            If ScanAll.CancellationPending Then
                Exit Sub
            End If
            getNXkey(pc, RegistryView.Registry64)
        End If
        If ScanAll.CancellationPending Then
            Exit Sub
        End If

        pc.drivers.Clear()
        getDriverInfo(pc, False)
        If ScanAll.CancellationPending Then
            Exit Sub
        End If

        pc.discos.Clear()
        getinfodiscos(pc)
        If ScanAll.CancellationPending Then
            Exit Sub
        End If
        pc.Dns.Clear()
        GetInfonetwork(pc)
        If ScanAll.CancellationPending Then
            Exit Sub
        End If
        pc.Impressoras.Clear()
        GetPrinterInfo(pc, False)
        If ScanAll.CancellationPending Then
            Exit Sub
        End If
        pc.Processadores.Clear()
        GetProcessadorInfo(pc, False)
        If ScanAll.CancellationPending Then
            Exit Sub
        End If
        pc.Softwares.Clear()
        GetSoftwareInfo(pc, False)
        If ScanAll.CancellationPending Then
            Exit Sub
        End If
        pc.UsersLocal.Clear()
        GetUsersLocalInfo(pc)
        If ScanAll.CancellationPending Then
            Exit Sub
        End If
        pc.Processos.Clear()
        GetProcessosInfo(pc, False)
        If ScanAll.CancellationPending Then
            Exit Sub
        End If
        pc.Ultimovezscaneada = DateTime.Now.ToString
    End Sub

    Public Sub GetOnlyINFO(ByVal pc As Servidor, ByVal indextab As Integer)
        Try
            If ScanOnlyInfo.CancellationPending Then
                Exit Sub
            End If
            If indextab = 0 Then
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                Me.Invoke(Sub() InfoProgressBar.Value = 0)
                Me.Invoke(Sub() InfoProgressBar.Maximum = 8)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                Me.Invoke(Sub() InfoProgressBar.Value = InfoProgressBar.Value + 1)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                Getpcinfo(pc)
                Me.Invoke(Sub() InfoProgressBar.Value = InfoProgressBar.Value + 1)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                GetComputerSystemInfo(pc)
                Me.Invoke(Sub() InfoProgressBar.Value = InfoProgressBar.Value + 1)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                getinfodiscos(pc)
                Me.Invoke(Sub() InfoProgressBar.Value = InfoProgressBar.Value + 1)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                If pc.OSArchitecture.Contains("64") Then
                    If ScanOnlyInfo.CancellationPending Then
                        Exit Sub
                    End If
                    Dim key1 As String = getWindowskey(pc.ComputerName, RegistryView.Registry64)
                    If key1 <> "" Then
                        Dim newl As New Licença
                        newl.Tipo = pc.OperatingSystem
                        newl.Key = key1
                        newl.Produto = ""
                        newl.Versão = ""
                        pc.Licenças.Add(newl)
                    End If
                    getMicrosoftKey(pc, RegistryView.Registry64)
                    Me.Invoke(Sub() InfoProgressBar.Value = InfoProgressBar.Value + 1)
                    getNXkey(pc, RegistryView.Registry64)
                    Me.Invoke(Sub() InfoProgressBar.Value = InfoProgressBar.Value + 1)

                Else
                    If ScanOnlyInfo.CancellationPending Then
                        Exit Sub
                    End If
                    Dim key1 As String = getWindowskey(pc.ComputerName, RegistryView.Registry32)
                    If key1 <> "" Then
                        Dim newl As New Licença
                        newl.Tipo = pc.OperatingSystem
                        newl.Key = key1
                        newl.Produto = ""
                        newl.Versão = ""
                        pc.Licenças.Add(newl)
                    End If
                    getMicrosoftKey(pc, RegistryView.Registry32)
                    Me.Invoke(Sub() InfoProgressBar.Value = InfoProgressBar.Value + 1)
                    getNXkey(pc, RegistryView.Registry64)
                    Me.Invoke(Sub() InfoProgressBar.Value = InfoProgressBar.Value + 1)

                End If
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                GetInfonetwork(pc)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                Me.Invoke(Sub() InfoProgressBar.Value = InfoProgressBar.Value + 1)
                GetUsersLocalInfo(pc)
                Me.Invoke(Sub() InfoProgressBar.Value = InfoProgressBar.Value + 1)
                Me.Invoke(Sub() atualizarinfo(pc, indextab))
            End If

            If indextab = 1 Then
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                GetSoftwareInfo(pc, True)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                Me.Invoke(Sub() atualizarinfo(pc, indextab))
            End If
            If indextab = 2 Then
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                GetProcessadorInfo(pc, True)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                Me.Invoke(Sub() atualizarinfo(pc, indextab))
            End If
            If indextab = 3 Then
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                GetProcessosInfo(pc, True)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                Me.Invoke(Sub() atualizarinfo(pc, indextab))
            End If
            If indextab = 4 Then
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                getDriverInfo(pc, True)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                Me.Invoke(Sub() atualizarinfo(pc, indextab))
            End If
            If indextab = 5 Then
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                GetPrinterInfo(pc, True)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                Me.Invoke(Sub() atualizarinfo(pc, indextab))
            End If
        Catch ex As Exception
            ' 
        End Try
    End Sub

    Public Sub atualizarinfo(ByVal pc As Servidor, ByVal indextab As Integer)
        Try
            If indextab = 0 Then
                MetroSetEllipse1.Text = pc.ComputerName
                MetroSetEllipse1.Refresh()
                OperatingSystem2.Text = pc.OperatingSystem
                BuildNumber2.Text = pc.BuildNumber
                CurrentUser2.Text = pc.CurrentUser
                DefaultIPGateway2.Text = pc.DefaultIPGateway
                IPAddress2.Text = pc.IPAddress
                LastBootUpTime2.Text = pc.LastBootUpTime
                LocalDateTime2.Text = pc.LocalDateTime
                MACAddress2.Text = pc.MACAddress
                Manufacturer2.Text = pc.Manufacturer
                Model2.Text = pc.Model
                OSArchitecture2.Text = pc.OSArchitecture
                SerialNumber2.Text = pc.SerialNumber
                Status2.Text = pc.Status
                TotalVisibleMemorySize2.Text = pc.TotalVisibleMemorySize
                Version2.Text = pc.Version
                WindowsDirectory2.Text = pc.WindowsDirectory
                InstallDate2.Text = pc.InstallDate
                Licencasgrid2.Rows.Clear()
                For Each li As Licença In pc.Licenças
                    Licencasgrid2.Rows.Add(New String() {li.Tipo, li.Key, li.Produto, li.Versão})
                Next
                UsersLocalGrid2.Rows.Clear()
                For Each us As UserLocalAccount In pc.UsersLocal
                    UsersLocalGrid2.Rows.Add(New String() {us.Name, us.AccountType, us.Domain, us.Status})
                Next
                DiscosGrid2.Rows.Clear()
                For Each ds As Discos In pc.discos
                    DiscosGrid2.Rows.Add(New String() {ds.size, ds.FreeSpace})
                Next
                Dns2.Text = ""
                For Each dns1 As String In pc.Dns
                    Dns2.Text = Dns.Text & dns1 & ";"
                Next
            End If
            If indextab = 1 Then
                SoftwaresGrid2.Rows.Clear()
                For Each st As Software In pc.Softwares
                    SoftwaresGrid2.Rows.Add(New String() {st.Name, st.InstallDate, st.Pacote, st.Versão})
                Next
            End If
            If indextab = 2 Then
                ProcessadorGrid2.Rows.Clear()
                For Each pro As Processador In pc.Processadores
                    ProcessadorGrid2.Rows.Add(New String() {pro.Name, pro.Fabricante, pro.Descrição, pro.TipodeProcessador})
                Next
            End If
            If indextab = 3 Then
                ProcessosGrid2.Rows.Clear()
                For Each pr As Processos In pc.Processos
                    ProcessosGrid2.Rows.Add(New String() {pr.Caption, pr.ComputerName, pr.Description, pr.Name, pr.Priority, pr.ProcessID, pr.SessionID, pr.User})
                Next
            End If
            If indextab = 4 Then
                DriversGrid2.Rows.Clear()
                For Each dr As Drivers In pc.drivers
                    DriversGrid2.Rows.Add(New String() {dr.Name, dr.Location, dr.ID})
                Next
            End If
            If indextab = 5 Then
                PrintersGrid2.Rows.Clear()
                For Each printer1 As Impressoras In pc.Impressoras
                    PrintersGrid2.Rows.Add(New String() {printer1.Name, printer1.Location, printer1.Server, printer1.Driver})
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub GetUsersLocalInfo(ByVal pc As String)
        On Error Resume Next
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy

        Dim scope As New ManagementScope("\\" & pc & "\root\CIMV2", connection)
        scope.Connect()
        Dim query As New ObjectQuery("SELECT * From Win32_UserAccount")
        Dim searcher As New ManagementObjectSearcher(scope, query)
        For Each x1 As Servidor In Database.Servidores
            If x1.ComputerName.ToLower = pc.ToLower Then
                If x1.UsersLocal.Count > 0 Then
                    x1.UsersLocal.Clear()

                End If
            End If
        Next

        For Each m As ManagementObject In searcher.Get()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            Dim Name As String = ""
            Dim status As String = ""
            Dim Domain As String = ""
            Dim AccountType As String = ""
            If m("Name") IsNot Nothing Then
                Name = m("Name")
            End If
            If m("AccountType") IsNot Nothing Then
                AccountType = m("AccountType")
            End If
            If m("Domain") IsNot Nothing Then
                Domain = m("Domain")
            End If
            If m("status") IsNot Nothing Then
                status = m("status")
            End If
            If m("Domain") = pc Then
                Dim p1 As New UserLocalAccount
                p1.Name = Name
                p1.AccountType = AccountType
                p1.Domain = Domain
                p1.Status = status
                atualizarUserLocal(pc, p1)
                Me.Update()
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
        Next




    End Sub
    Public Sub GetUsersLocalInfo(ByVal pc As Servidor)
        On Error Resume Next
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy

        Dim scope As New ManagementScope("\\" & pc.ComputerName & "\root\CIMV2", connection)
        scope.Connect()
        Dim query As New ObjectQuery("SELECT * From Win32_UserAccount")
        Dim searcher As New ManagementObjectSearcher(scope, query)


        For Each m As ManagementObject In searcher.Get()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            Dim Name As String = ""
            Dim status As String = ""
            Dim Domain As String = ""
            Dim AccountType As String = ""
            If m("Name") IsNot Nothing Then
                Name = m("Name")
            End If
            If m("AccountType") IsNot Nothing Then
                AccountType = m("AccountType")
            End If
            If m("Domain") IsNot Nothing Then
                Domain = m("Domain")
            End If
            If m("status") IsNot Nothing Then
                status = m("status")
            End If
            If m("Domain") = pc.ComputerName Then
                Dim p1 As New UserLocalAccount
                p1.Name = Name
                p1.AccountType = AccountType
                p1.Domain = Domain
                p1.Status = status
                pc.UsersLocal.Add(p1)
            End If
        Next




    End Sub
    Public Sub getDriverInfo(ByVal pc As String)
        On Error Resume Next
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        Dim scope As New ManagementScope("\\" & pc & "\root\CIMV2", connection)
        scope.Connect()
        Dim query As New ObjectQuery("SELECT * FROM Win32_PnPSignedDriver")

        Dim searcher As New ManagementObjectSearcher(scope, query)
        For Each x1 As Servidor In Database.Servidores
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If x1.ComputerName = pc Then
                x1.drivers.Clear()
            End If
        Next
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        For Each m As ManagementObject In searcher.Get()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            Dim Description As String = ""
            Dim Location As String = ""
            Dim DeviceID As String = ""
            If m("Description") IsNot Nothing Then
                Description = m("Description")
            End If
            If m("Location") IsNot Nothing Then
                Location = m("Location")
            End If
            If m("DeviceID") IsNot Nothing Then
                DeviceID = m("DeviceID")
            End If
            Dim pr As New Drivers
            pr.Name = Description
            pr.Location = Location
            pr.ID = DeviceID
            atualizarDriver(pc, pr)
            Me.Update()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
        Next




    End Sub
    Public Sub getDriverInfo(ByVal pc As Servidor, ByVal progress As Boolean)
        Try

            Dim connection As New ConnectionOptions
            connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy
            Dim scope As New ManagementScope("\\" & pc.ComputerName & "\root\CIMV2", connection)
            scope.Connect()
            Dim query As New ObjectQuery("SELECT * FROM Win32_PnPSignedDriver")

            Dim searcher As New ManagementObjectSearcher(scope, query)
            If progress = True Then
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                Dim count1 As Integer = searcher.Get().Count
                Me.Invoke(Sub() DriversProgressBar.Value = 0)
                Me.Invoke(Sub() DriversProgressBar.Maximum = count1)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
            End If
            For Each m As ManagementObject In searcher.Get()
                If progress = True Then
                    If ScanOnlyInfo.CancellationPending Then
                        Exit Sub
                    End If
                End If
                Dim Description As String = ""
                Dim Location As String = ""
                Dim DeviceID As String = ""
                If m("Description") IsNot Nothing Then
                    Description = m("Description")
                End If
                If m("Location") IsNot Nothing Then
                    Location = m("Location")
                End If
                If m("DeviceID") IsNot Nothing Then
                    DeviceID = m("DeviceID")
                End If
                Dim pr As New Drivers
                pr.Name = Description
                pr.Location = Location
                pr.ID = DeviceID
                pc.drivers.Add(pr)
                If progress = True Then
                    Me.Invoke(Sub() DriversProgressBar.Value = DriversProgressBar.Value + 1)
                    If ScanOnlyInfo.CancellationPending Then
                        Exit Sub
                    End If
                End If
            Next
        Catch ex As Exception

        End Try


    End Sub
    Public Sub GetComputerSystemInfo(ByVal pc As String)
        On Error Resume Next
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy


        Dim scope As New ManagementScope("\\" & pc & "\root\CIMV2", connection)
        scope.Connect()
        Dim query As New ObjectQuery("SELECT * FROM Win32_ComputerSystem")

        Dim searcher As New ManagementObjectSearcher(scope, query)

        For Each m As ManagementObject In searcher.Get()

            Dim UserName As String = ""
            Dim model As String = ""
            If m("Model") IsNot Nothing Then
                model = m("Model")
            End If

            If m("UserName") IsNot Nothing Then
                UserName = m("UserName")
            End If
            If Database IsNot Nothing Then
                If Database.Servidores IsNot Nothing Then
                    For Each x1 As Servidor In Database.Servidores
                        If x1.ComputerName.ToLower = pc.ToLower Then
                            x1.Model = model
                            x1.CurrentUser = UserName

                        End If
                    Next
                End If
            End If
        Next




    End Sub
    Public Sub GetComputerSystemInfo(ByVal pc As Servidor)
        On Error Resume Next
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy


        Dim scope As New ManagementScope("\\" & pc.ComputerName & "\root\CIMV2", connection)
        scope.Connect()
        Dim query As New ObjectQuery("SELECT * FROM Win32_ComputerSystem")

        Dim searcher As New ManagementObjectSearcher(scope, query)

        For Each m As ManagementObject In searcher.Get()

            Dim UserName As String = ""
            Dim model As String = ""
            If m("Model") IsNot Nothing Then
                model = m("Model")
            End If

            If m("UserName") IsNot Nothing Then
                UserName = m("UserName")
            End If
            pc.Model = model
            pc.CurrentUser = UserName

        Next




    End Sub
    Public Function GetCurrentuser(ByVal pc As String) As String
        On Error Resume Next
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy


        Dim scope As New ManagementScope("\\" & pc & "\root\CIMV2", connection)
        scope.Connect()
        Dim query As New ObjectQuery("SELECT * FROM Win32_ComputerSystem")

        Dim searcher As New ManagementObjectSearcher(scope, query)

        For Each m As ManagementObject In searcher.Get()
            Dim UserName As String = ""
            If m("UserName") IsNot Nothing Then
                UserName = m("UserName")
            End If
            Return UserName
        Next



    End Function
    Public Sub GetSoftwareInfo(ByVal pc As String)
        On Error Resume Next
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy

        Dim scope As New ManagementScope("\\" & pc & "\root\CIMV2", connection)
        scope.Connect()

        Dim query As New ObjectQuery("SELECT * FROM Win32_Product")
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If

        Dim searcher As New ManagementObjectSearcher(scope, query)
        For Each x1 As Servidor In Database.Servidores
            If x1.ComputerName.ToLower = pc.ToLower Then
                If x1.Softwares.Count > 0 Then
                    x1.Softwares.Clear()

                End If
            End If
        Next
        For Each m As ManagementObject In searcher.Get()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If

            Dim name As String = ""
            Dim InstallDate As String = ""
            Dim PackageName As String = ""
            Dim Version As String = ""


            If m("name") IsNot Nothing Then
                name = m("name")
            End If

            If m("InstallDate") IsNot Nothing Then
                InstallDate = m("InstallDate")
            End If

            If m("PackageName") IsNot Nothing Then
                PackageName = m("PackageName")
            End If

            If m("Version") IsNot Nothing Then
                Version = m("Version")
            End If
            Dim newsoft As New Software
            newsoft.Name = name
            newsoft.InstallDate = InstallDate
            newsoft.Pacote = PackageName
            newsoft.Versão = Version
            atualizarSoftware(pc, newsoft)
            Me.Update()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
        Next




    End Sub
    Public Sub GetSoftwareInfo(ByVal pc As Servidor, ByVal progress As Boolean)
        Try

            Dim connection As New ConnectionOptions
            connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy
            Dim scope As New ManagementScope("\\" & pc.ComputerName & "\root\CIMV2", connection)
            scope.Connect()
            Dim query As New ObjectQuery("SELECT * FROM Win32_Product")
            Dim searcher As New ManagementObjectSearcher(scope, query)
            If progress = True Then
                Dim count1 As Integer = searcher.Get().Count
                Me.Invoke(Sub() SoftwareProgressBar.Value = 0)
                Me.Invoke(Sub() SoftwareProgressBar.Maximum = count1)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
            End If
            For Each m As ManagementObject In searcher.Get()
                If progress = True Then
                    If ScanOnlyInfo.CancellationPending Then
                        Exit Sub
                    End If
                End If
                Dim name As String = ""
                Dim InstallDate As String = ""
                Dim PackageName As String = ""
                Dim Version As String = ""


                If m("name") IsNot Nothing Then
                    name = m("name")
                End If

                If m("InstallDate") IsNot Nothing Then
                    InstallDate = m("InstallDate")
                End If

                If m("PackageName") IsNot Nothing Then
                    PackageName = m("PackageName")
                End If

                If m("Version") IsNot Nothing Then
                    Version = m("Version")
                End If
                Dim newsoft As New Software
                newsoft.Name = name
                newsoft.InstallDate = InstallDate
                newsoft.Pacote = PackageName
                newsoft.Versão = Version
                pc.Softwares.Add(newsoft)
                If progress = True Then
                    Me.Invoke(Sub() SoftwareProgressBar.Value = SoftwareProgressBar.Value + 1)
                    If ScanOnlyInfo.CancellationPending Then
                        Exit Sub
                    End If
                End If
            Next

        Catch ex As Exception
        End Try


    End Sub
    Private Sub GetProcessosInfo(ByVal pc As Servidor, ByVal progress As Boolean)

        'On Error Resume Next

        Dim connOptions As New ConnectionOptions()
        connOptions.Impersonation = ImpersonationLevel.Impersonate
        Dim myScope As New ManagementScope("\\" & pc.ComputerName & "\ROOT\CIMV2", connOptions)
        myScope.Connect()
        Dim objSearcher As New ManagementObjectSearcher("\\" & pc.ComputerName & "\ROOT\CIMV2", "SELECT * FROM Win32_Process")
        Dim opsObserver As New ManagementOperationObserver()
        objSearcher.Scope = myScope
        Dim sep As String() = {vbLf, vbTab}
        If progress = True Then
            If ScanOnlyInfo.CancellationPending Then
                Exit Sub
            End If
            Dim count1 As Integer = objSearcher.Get().Count
            Me.Invoke(Sub() ProcessosProgressBar.Value = 0)
            Me.Invoke(Sub() ProcessosProgressBar.Maximum = count1)
            If ScanOnlyInfo.CancellationPending Then
                Exit Sub
            End If
        End If
        For Each obj As ManagementObject In objSearcher.Get()
            If progress = True Then
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
            End If
            'TimeDelay()
            Dim caption As String = obj.GetText(TextFormat.Mof)
            Dim split As String() = caption.Split(sep, StringSplitOptions.RemoveEmptyEntries)
            ' Iterate through the splitter
            Dim s(1) As String
            obj.InvokeMethod("GetOwner", CType(s, Object()))
            Dim userpro As String = s(0)
            Dim p1 As New Processos
            p1.User = userpro
            For i As Integer = 0 To split.Length - 1
                'TimeDelay()
                If split(i).Split("="c).Length > 1 Then
                    Dim procDetails As String() = split(i).Split("="c)
                    procDetails(1) = procDetails(1).Replace("""", "")
                    procDetails(1) = procDetails(1).Replace(";"c, " "c)
                    Select Case procDetails(0).Trim().ToLower()
                        Case "caption"
                            p1.Caption = procDetails(1)
                            Exit Select
                        Case "csname"
                            p1.ComputerName = procDetails(1)
                            Exit Select
                        Case "description"
                            p1.Description = procDetails(1)
                            Exit Select
                        Case "name"
                            p1.Name = procDetails(1)
                            Exit Select
                        Case "priority"
                            p1.Priority = procDetails(1)
                            Exit Select
                        Case "processid"
                            p1.ProcessID = procDetails(1)
                            Exit Select
                        Case "sessionid"
                            p1.SessionID = procDetails(1)
                            Exit Select
                            ' Case "GetOwner"

                            ' Exit Select
                    End Select
                End If
            Next
            pc.Processos.Add(p1)
            If progress = True Then
                Me.Invoke(Sub() ProcessosProgressBar.Value = ProcessosProgressBar.Value + 1)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
            End If
        Next



    End Sub
    Public Sub GetProcessosInfo(ByVal pc As String)

        On Error Resume Next
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        Dim connOptions As New ConnectionOptions()
        connOptions.Impersonation = ImpersonationLevel.Impersonate
        Dim myScope As New ManagementScope("\\" & pc & "\ROOT\CIMV2", connOptions)
        myScope.Connect()
        Dim objSearcher As New ManagementObjectSearcher("\\" & pc & "\ROOT\CIMV2", "SELECT * FROM Win32_Process")
        Dim opsObserver As New ManagementOperationObserver()
        objSearcher.Scope = myScope
        Dim sep As String() = {vbLf, vbTab}

        For Each x1 As Servidor In Database.Servidores
            If x1.ComputerName.ToLower = pc.ToLower Then
                If x1.Processos.Count > 0 Then
                    x1.Processos.Clear()

                End If
            End If
        Next

        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        For Each obj As ManagementObject In objSearcher.Get()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            'TimeDelay()
            Dim caption As String = obj.GetText(TextFormat.Mof)
            Dim split As String() = caption.Split(sep, StringSplitOptions.RemoveEmptyEntries)
            ' Iterate through the splitter
            Dim s(1) As String
            obj.InvokeMethod("GetOwner", CType(s, Object()))
            Dim userpro As String = s(0)
            Dim p1 As New Processos
            p1.User = userpro
            For i As Integer = 0 To split.Length - 1
                'TimeDelay()
                If split(i).Split("="c).Length > 1 Then
                    Dim procDetails As String() = split(i).Split("="c)
                    procDetails(1) = procDetails(1).Replace("""", "")
                    procDetails(1) = procDetails(1).Replace(";"c, " "c)
                    Select Case procDetails(0).Trim().ToLower()
                        Case "caption"
                            p1.Caption = procDetails(1)
                            Exit Select
                        Case "csname"
                            p1.ComputerName = procDetails(1)
                            Exit Select
                        Case "description"
                            p1.Description = procDetails(1)
                            Exit Select
                        Case "name"
                            p1.Name = procDetails(1)
                            Exit Select
                        Case "priority"
                            p1.Priority = procDetails(1)
                            Exit Select
                        Case "processid"
                            p1.ProcessID = procDetails(1)
                            Exit Select
                        Case "sessionid"
                            p1.SessionID = procDetails(1)
                            Exit Select
                            ' Case "GetOwner"

                            ' Exit Select
                    End Select
                End If
            Next
            atualizarProcesso(pc, p1)
            Me.Update()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
        Next



    End Sub
    Public Sub Getpcinfo(ByVal pc As String)
        On Error Resume Next
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy

        Dim scope As New ManagementScope("\\" & pc & "\root\CIMV2", connection)
        scope.Connect()

        Dim query As ObjectQuery = New ObjectQuery("SELECT * FROM Win32_OperatingSystem")
        Dim searcher As ManagementObjectSearcher = New ManagementObjectSearcher(scope, query)
        Dim queryCollection As ManagementObjectCollection = searcher.Get
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        For Each m As ManagementObject In queryCollection
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            Dim WindowsDirectory As String = ""
            Dim Caption As String = ""
            Dim Version As String = ""
            Dim Manufacturer As String = ""
            Dim OSArchitecture As String = ""
            Dim BuildNumber As String = ""
            Dim LocalDateTime As String = ""
            Dim SerialNumber As String = ""
            Dim LastBootUpTime As String = ""
            Dim Status As String = ""
            Dim InstallDate As String = ""
            Dim TotalVisibleMemorySize As String = ""
            Dim Currentuser As String = ""
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("caption") IsNot Nothing Then
                Caption = m("caption")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m.GetPropertyValue("username").ToString IsNot Nothing Then
                Currentuser = m.GetPropertyValue("username").ToString
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("WindowsDirectory") IsNot Nothing Then
                WindowsDirectory = m("WindowsDirectory")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("Version") IsNot Nothing Then
                Version = m("Version")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("Manufacturer") IsNot Nothing Then
                Manufacturer = m("Manufacturer")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("OSArchitecture") IsNot Nothing Then
                OSArchitecture = m("OSArchitecture")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("BuildNumber") IsNot Nothing Then
                BuildNumber = m("BuildNumber")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("LocalDateTime") IsNot Nothing Then
                LocalDateTime = ParseCIM_DATETIME(m("LocalDateTime"))
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("SerialNumber") IsNot Nothing Then
                SerialNumber = m("SerialNumber")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("LastBootUpTime") IsNot Nothing Then
                LastBootUpTime = ParseCIM_DATETIME(m("LastBootUpTime"))
            End If
            If m("Status") IsNot Nothing Then
                Status = m("Status")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("InstallDate") IsNot Nothing Then
                InstallDate = ParseCIM_DATETIME(m("InstallDate"))
            End If

            If m("TotalVisibleMemorySize") IsNot Nothing Then
                TotalVisibleMemorySize = BytesToString(m("TotalVisibleMemorySize"))
            End If
            If Database IsNot Nothing Then
                If Database.Servidores IsNot Nothing Then
                    For Each x1 As Servidor In Database.Servidores
                        If x1.ComputerName.ToLower = pc.ToLower Then
                            x1.BuildNumber = BuildNumber
                            x1.WindowsDirectory = WindowsDirectory
                            x1.CurrentUser = Currentuser
                            x1.LastBootUpTime = LastBootUpTime
                            x1.LocalDateTime = LocalDateTime
                            x1.Manufacturer = Manufacturer
                            x1.OSArchitecture = OSArchitecture
                            x1.SerialNumber = SerialNumber
                            x1.InstallDate = InstallDate
                            x1.Status = Status
                            x1.OperatingSystem = Caption
                            x1.TotalVisibleMemorySize = TotalVisibleMemorySize
                            x1.Version = Version
                            x1.Ultimovezscaneada = DateAndTime.Now.ToString
                        End If
                    Next
                End If
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If


        Next



    End Sub
    Public Sub Getpcinfo(ByVal pc As Servidor)
        On Error Resume Next
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy

        Dim scope As New ManagementScope("\\" & pc.ComputerName & "\root\CIMV2", connection)
        scope.Connect()

        Dim query As ObjectQuery = New ObjectQuery("SELECT * FROM Win32_OperatingSystem")
        Dim searcher As ManagementObjectSearcher = New ManagementObjectSearcher(scope, query)
        Dim queryCollection As ManagementObjectCollection = searcher.Get
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        For Each m As ManagementObject In queryCollection
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            Dim WindowsDirectory As String = ""
            Dim Caption As String = ""
            Dim Version As String = ""
            Dim Manufacturer As String = ""
            Dim OSArchitecture As String = ""
            Dim BuildNumber As String = ""
            Dim LocalDateTime As String = ""
            Dim SerialNumber As String = ""
            Dim LastBootUpTime As String = ""
            Dim Status As String = ""
            Dim InstallDate As String = ""
            Dim TotalVisibleMemorySize As String = ""
            Dim Currentuser As String = ""
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("caption") IsNot Nothing Then
                Caption = m("caption")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m.GetPropertyValue("username").ToString IsNot Nothing Then
                Currentuser = m.GetPropertyValue("username").ToString
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("WindowsDirectory") IsNot Nothing Then
                WindowsDirectory = m("WindowsDirectory")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("Version") IsNot Nothing Then
                Version = m("Version")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("Manufacturer") IsNot Nothing Then
                Manufacturer = m("Manufacturer")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("OSArchitecture") IsNot Nothing Then
                OSArchitecture = m("OSArchitecture")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("BuildNumber") IsNot Nothing Then
                BuildNumber = m("BuildNumber")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("LocalDateTime") IsNot Nothing Then
                LocalDateTime = ParseCIM_DATETIME(m("LocalDateTime"))
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("SerialNumber") IsNot Nothing Then
                SerialNumber = m("SerialNumber")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("LastBootUpTime") IsNot Nothing Then
                LastBootUpTime = ParseCIM_DATETIME(m("LastBootUpTime"))
            End If
            If m("Status") IsNot Nothing Then
                Status = m("Status")
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            If m("InstallDate") IsNot Nothing Then
                InstallDate = ParseCIM_DATETIME(m("InstallDate"))
            End If

            If m("TotalVisibleMemorySize") IsNot Nothing Then
                TotalVisibleMemorySize = BytesToString(m("TotalVisibleMemorySize"))
            End If

            pc.BuildNumber = BuildNumber
            pc.WindowsDirectory = WindowsDirectory
            pc.CurrentUser = Currentuser
            pc.LastBootUpTime = LastBootUpTime
            pc.LocalDateTime = LocalDateTime
            pc.Manufacturer = Manufacturer
            pc.OSArchitecture = OSArchitecture
            pc.SerialNumber = SerialNumber
            pc.InstallDate = InstallDate
            pc.Status = Status
            pc.OperatingSystem = Caption
            pc.TotalVisibleMemorySize = TotalVisibleMemorySize
            pc.Version = Version
            pc.Ultimovezscaneada = DateAndTime.Now.ToString

            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If


        Next



    End Sub
    Public Function GetOS(ByVal pc As String) As String
        On Error Resume Next

        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy

        Dim scope As New ManagementScope("\\" & pc & "\root\CIMV2", connection)
        scope.Connect()

        Dim query As ObjectQuery = New ObjectQuery("SELECT * FROM Win32_OperatingSystem")
        Dim searcher As ManagementObjectSearcher = New ManagementObjectSearcher(scope, query)
        Dim queryCollection As ManagementObjectCollection = searcher.Get

        For Each m As ManagementObject In queryCollection

            Dim OSArchitecture As String = ""

            If m("OSArchitecture") IsNot Nothing Then
                OSArchitecture = m("OSArchitecture")
            End If
            Return OSArchitecture

        Next

    End Function
    Private Function ParseCIM_DATETIME(ByVal date1 As String) As DateTime
        'datetime object to store the return value
        Dim parsed As DateTime = DateTime.MinValue
        'check date integrity
        If ((Not (date1) Is Nothing) _
                    AndAlso (date1.IndexOf(ChrW(46)) <> -1)) Then
            'obtain the date with miliseconds
            Dim newDate As String = date1.Substring(0, (date1.IndexOf(ChrW(46)) + 4))
            'check the lenght
            If (newDate.Length = 18) Then
                'extract each date component
                Dim y As Integer = Convert.ToInt32(newDate.Substring(0, 4))
                Dim m As Integer = Convert.ToInt32(newDate.Substring(4, 2))
                Dim d As Integer = Convert.ToInt32(newDate.Substring(6, 2))
                Dim h As Integer = Convert.ToInt32(newDate.Substring(8, 2))
                Dim mm As Integer = Convert.ToInt32(newDate.Substring(10, 2))
                Dim s As Integer = Convert.ToInt32(newDate.Substring(12, 2))
                Dim ms As Integer = Convert.ToInt32(newDate.Substring(15, 3))
                'compose the new datetime object
                parsed = New DateTime(y, m, d, h, mm, s, ms)
            End If
        End If
        'return datetime
        Return parsed

    End Function
    Private Shared Function BytesToString(ByVal byteCount As Long) As String

        Dim suf() As String = New String() {"B", "KB", "MB", "GB", "TB", "PB", "EB"}
        'Longs run out around EB
        If (byteCount = 0) Then
            Return ("0" + suf(0))
        End If
        Dim bytes As Long = Math.Abs(byteCount)
        Dim place As Integer = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)))
        Dim num As Double = Math.Round((bytes / Math.Pow(1024, place)), 1)
        Return ((Math.Sign(byteCount) * num).ToString + suf(place))

    End Function
    Public Function getWindowskey(ByVal pc As String, ByVal regos As RegistryView) As String
        Dim key As String = ""
        Try
            Dim environmentKey As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, pc, regos).OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion")
            Dim value As Byte() = environmentKey.GetValue("DigitalProductId")
            key = DecodeProductKey(value)
            Return key
        Catch ex As Exception
            Return key
        End Try
    End Function

    Public Sub getNXkey(ByVal pc As String, ByVal regos As RegistryView)
        Try
            Dim environmentKey As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, pc, regos)
            Dim nxlicense As String = ""
            Dim CurrentUser As String = ""
            Dim ValueNames As String() = environmentKey.GetSubKeyNames
            For Each name As String In ValueNames
                Dim ke1 As RegistryKey = environmentKey.OpenSubKey(name & "\Software\Siemens_PLM_Software\Common_Licensing")
                If ke1 IsNot Nothing Then
                    nxlicense = ke1.GetValue("NX_BUNDLES")
                Else
                    Dim ke2 As RegistryKey = environmentKey.OpenSubKey(name & "\Environment")
                    If ke2 IsNot Nothing Then
                        nxlicense = ke2.GetValue("UGS_LICENSE_BUNDLE")
                    End If
                End If
                Dim key3 As RegistryKey = environmentKey.OpenSubKey(name & "\Volatile Environment")
                If key3 IsNot Nothing Then
                    CurrentUser = key3.GetValue("USERNAME")
                End If
                If nxlicense <> "" Then
                    Dim newl As New Licença
                    newl.Tipo = "Licença NX"
                    newl.Key = "Usuário:" & CurrentUser & ",Usando Licença:" & nxlicense
                    AtualizarLicença(pc, newl)
                End If

            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Sub getNXkey(ByVal pc As Servidor, ByVal regos As RegistryView)
        Try
            Dim environmentKey As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, pc.ComputerName, regos)
            Dim nxlicense As String = ""
            Dim CurrentUser As String = ""
            Dim ValueNames As String() = environmentKey.GetSubKeyNames
            For Each name As String In ValueNames
                Dim ke1 As RegistryKey = environmentKey.OpenSubKey(name & "\Software\Siemens_PLM_Software\Common_Licensing")
                If ke1 IsNot Nothing Then
                    nxlicense = ke1.GetValue("NX_BUNDLES")
                Else
                    Dim ke2 As RegistryKey = environmentKey.OpenSubKey(name & "\Environment")
                    If ke2 IsNot Nothing Then
                        nxlicense = ke2.GetValue("UGS_LICENSE_BUNDLE")
                    End If
                End If
                Dim key3 As RegistryKey = environmentKey.OpenSubKey(name & "\Volatile Environment")
                If key3 IsNot Nothing Then
                    CurrentUser = key3.GetValue("USERNAME")
                End If
                If nxlicense <> "" Then
                    Dim newl As New Licença
                    newl.Tipo = "Licença NX"
                    newl.Key = "Usuário:" & CurrentUser & ",Usando Licença:" & nxlicense
                    pc.Licenças.Add(newl)
                End If

            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Sub AtualizarLicença(ByVal pc As String, ByVal licença As Licença)
        Try
            Dim found As Boolean = False
            If Database IsNot Nothing Then
                If Database.Servidores IsNot Nothing Then
                    For Each x1 As Servidor In Database.Servidores
                        If x1.ComputerName.ToLower = pc.ToLower Then
                            For Each x2 As Licença In x1.Licenças
                                If x2.Tipo.ToLower = licença.Tipo.ToLower And x2.Key.ToLower = licença.Key.ToLower Then
                                    found = True
                                    x2 = licença
                                End If
                            Next
                            If found = False Then
                                x1.Licenças.Add(licença)
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function GetProductKey(ByVal HexBuf As Byte()) As String
        Dim keystring1 As String = ""
        Try

            Dim tmp As String = ""

            For l As Integer = LBound(HexBuf) To UBound(HexBuf)
                tmp = tmp & " " & Hex(HexBuf(l))
            Next

            Dim StartOffset As Integer = 52
            Dim EndOffset As Integer = 67
            Dim Digits(24) As String

            Digits(0) = "B" : Digits(1) = "C" : Digits(2) = "D" : Digits(3) = "F"
            Digits(4) = "G" : Digits(5) = "H" : Digits(6) = "J" : Digits(7) = "K"
            Digits(8) = "M" : Digits(9) = "P" : Digits(10) = "Q" : Digits(11) = "R"
            Digits(12) = "T" : Digits(13) = "V" : Digits(14) = "W" : Digits(15) = "X"
            Digits(16) = "Y" : Digits(17) = "2" : Digits(18) = "3" : Digits(19) = "4"
            Digits(20) = "6" : Digits(21) = "7" : Digits(22) = "8" : Digits(23) = "9"

            Dim dLen As Integer = 29
            Dim sLen As Integer = 15
            Dim HexDigitalPID(15) As String
            Dim Des(30) As String

            Dim tmp2 As String = ""

            For i = StartOffset To EndOffset
                HexDigitalPID(i - StartOffset) = HexBuf(i)
                tmp2 = tmp2 & " " & Hex(HexDigitalPID(i - StartOffset))
            Next

            Dim KEYSTRING As String = ""

            For i As Integer = dLen - 1 To 0 Step -1
                If ((i + 1) Mod 6) = 0 Then
                    Des(i) = "-"
                    KEYSTRING = KEYSTRING & "-"
                Else
                    Dim HN As Integer = 0
                    For N As Integer = (sLen - 1) To 0 Step -1
                        Dim Value As Integer = ((HN * 2 ^ 8) Or HexDigitalPID(N))
                        HexDigitalPID(N) = Value \ 24
                        HN = (Value Mod 24)

                    Next

                    Des(i) = Digits(HN)
                    KEYSTRING = KEYSTRING & Digits(HN)
                End If
            Next
            keystring1 = KEYSTRING
            Return StrReverse(keystring1)
        Catch ex As Exception

            Return keystring1
        End Try
    End Function
    Public Shared Function DecodeProductKeyWin8AndUp(digitalProductId As Byte()) As String
        Dim keystring As String = ""
        Try
            Dim key = [String].Empty
            Const keyOffset As Integer = 52
            Dim isWin8 = CByte((digitalProductId(66) \ 6) And 1)
            digitalProductId(66) = CByte((digitalProductId(66) And &HF7) Or (isWin8 And 2) * 4)

            ' Possible alpha-numeric characters in product key.
            Const digits As String = "BCDFGHJKMPQRTVWXY2346789"
            Dim last As Integer = 0
            For i As Integer = 24 To 0 Step -1
                Dim current = 0
                For j = 14 To 0 Step -1
                    current = current * 256
                    current = digitalProductId(j + keyOffset) + current
                    digitalProductId(j + keyOffset) = CByte(current \ 24)
                    current = current Mod 24
                    last = current
                Next
                key = digits(current) + key
            Next
            Dim keypart1 = key.Substring(1, last)
            Const insert As String = "N"
            key = key.Substring(1).Replace(keypart1, Convert.ToString(keypart1) & insert)
            If last = 0 Then
                key = insert & Convert.ToString(key)
            End If
            For i = 5 To key.Length - 1 Step 6
                key = key.Insert(i, "-")
            Next
            keystring = key
            Return keystring

        Catch ex As Exception

            Return keystring
        End Try
    End Function
    Public Function DecodeProductKey(digitalProductId As Byte()) As String
        Dim keystring As String = ""
        Try
            ' Possible alpha-numeric characters in product key.
            Const digits As String = "BCDFGHJKMPQRTVWXY2346789"
            ' Length of decoded product key in byte-form. Each byte represents 2 chars.
            Const decodeStringLength As Integer = 15
            ' Decoded product key is of length 29
            Dim decodedChars As Char() = New Char(28) {}

            ' Extract encoded product key from bytes [52,67]
            Dim hexPid As New List(Of Byte)()
            For i As Integer = 52 To 67
                hexPid.Add(digitalProductId(i))
            Next

            ' Decode characters
            For i As Integer = decodedChars.Length - 1 To 0 Step -1
                ' Every sixth char is a separator.
                If (i + 1) Mod 6 = 0 Then
                    decodedChars(i) = "-"c
                Else
                    ' Do the actual decoding.
                    Dim digitMapIndex As Integer = 0
                    For j As Integer = decodeStringLength - 1 To 0 Step -1
                        Dim byteValue As Integer = (digitMapIndex << 8) Or CByte(hexPid(j))
                        hexPid(j) = CByte(byteValue \ 24)
                        digitMapIndex = byteValue Mod 24
                        decodedChars(i) = digits(digitMapIndex)
                    Next
                End If
            Next
            keystring = decodedChars
            Return New String(keystring)
        Catch ex As Exception

            Return keystring
        End Try
    End Function


    Public Function getMicrosoftKey(ByVal pc As String, ByVal regos As RegistryView)
        Dim key As String = ""
        Try
            Dim aOffID(,) As String =
        New String(,) {{"97", "8.0"},
               {"2000", "9.0"},
               {"XP", "10.0"},
               {"2003", "11.0"},
               {"2007", "12.0"},
            {"2010", "14.0"}}
            Dim bound0 As Integer = aOffID.GetUpperBound(0)
            Dim bound1 As Integer = aOffID.GetUpperBound(1)

            ' Loop over all elements.
            For i As Integer = 0 To bound0
                For x As Integer = 0 To bound1
                    ' Get element.
                    Dim s1 As String = aOffID(i, x)
                    Dim environmentKey As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, pc, regos).OpenSubKey("SOFTWARE\Microsoft\Office\" & aOffID(i, x) & "\Registration", True)
                    If environmentKey IsNot Nothing Then
                        key = searchKey(pc, environmentKey, regos, aOffID, i, x)
                    End If
                Next
            Next
            Return key

        Catch ex As Exception

            Return key
        End Try
    End Function
    Public Function getMicrosoftKey(ByVal pc As Servidor, ByVal regos As RegistryView)
        Dim key As String = ""
        Try
            Dim aOffID(,) As String =
        New String(,) {{"97", "8.0"},
               {"2000", "9.0"},
               {"XP", "10.0"},
               {"2003", "11.0"},
               {"2007", "12.0"},
            {"2010", "14.0"}}
            Dim bound0 As Integer = aOffID.GetUpperBound(0)
            Dim bound1 As Integer = aOffID.GetUpperBound(1)

            ' Loop over all elements.
            For i As Integer = 0 To bound0
                For x As Integer = 0 To bound1
                    ' Get element.
                    Dim s1 As String = aOffID(i, x)
                    Dim environmentKey As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, pc.ComputerName, regos).OpenSubKey("SOFTWARE\Microsoft\Office\" & aOffID(i, x) & "\Registration", True)
                    If environmentKey IsNot Nothing Then
                        key = searchKey(pc, environmentKey, regos, aOffID, i, x)
                    End If
                Next
            Next
            Return key

        Catch ex As Exception

            Return key
        End Try
    End Function
    Public Function DecodeProductKey2010(digitalProductId As Byte()) As String
        Dim keystring As String = ""
        Try
            ' Possible alpha-numeric characters in product key.
            Const digits As String = "BCDFGHJKMPQRTVWXY2346789"
            ' Length of decoded product key in byte-form. Each byte represents 2 chars.
            Const decodeStringLength As Integer = 15
            ' Decoded product key is of length 29
            Dim decodedChars As Char() = New Char(28) {}

            ' Extract encoded product key from bytes [52,67]
            Dim hexPid As New List(Of Byte)()
            For i As Integer = 808 To 823
                hexPid.Add(digitalProductId(i))
            Next

            ' Decode characters
            For i As Integer = decodedChars.Length - 1 To 0 Step -1
                ' Every sixth char is a separator.
                If (i + 1) Mod 6 = 0 Then
                    decodedChars(i) = "-"c
                Else
                    ' Do the actual decoding.
                    Dim digitMapIndex As Integer = 0
                    For j As Integer = decodeStringLength - 1 To 0 Step -1
                        Dim byteValue As Integer = (digitMapIndex << 8) Or CByte(hexPid(j))
                        hexPid(j) = CByte(byteValue \ 24)
                        digitMapIndex = byteValue Mod 24
                        decodedChars(i) = digits(digitMapIndex)
                    Next
                End If
            Next
            keystring = decodedChars
            Return New String(keystring)
        Catch ex As Exception

            Return keystring
        End Try
    End Function
    Public Function GetWindowsProductKey(ByVal data() As Byte) As String

        Dim keystring As String = ""
        Try



            Dim dataKey(14) As Byte

            Dim chars() As String = {"B", "C", "D", "F", "G", "H", "J", "K", "M", "P", "Q", "R", "T", "V", "W", "X", "Y", "2", "3", "4", "6", "7", "8", "9"}

            Dim keys(24) As String

            Dim c As Long = 0




            If Not data Is Nothing Then

                Array.Copy(data, 52, dataKey, 0, 15)

                For i As Integer = keys.GetUpperBound(0) To 0 Step -1

                    c = 0

                    For n As Integer = dataKey.GetUpperBound(0) To 0 Step -1

                        c = c << 8

                        c = c + dataKey(n)

                        dataKey(n) = (c \ 24)

                        c = c Mod 24

                    Next n

                    keys(i) = chars(c)

                Next i

            End If

            Return String.Concat(keys)
        Catch ex As Exception

            Return keystring
        End Try
    End Function
    Public Function searchKey(ByVal pc As String, ByVal regKey As RegistryKey, ByVal regos As RegistryView, ByVal aOffID As Array, ByVal a As Integer, ByVal x As Integer) As String
        Dim okey As String = ""
        Dim over As String = ""
        Try

            Dim aDPIDBytes = regKey.GetValue("DigitalProductID")
            If aDPIDBytes Is Nothing Then
                Dim aGUIDKeys As String() = regKey.GetSubKeyNames
                If aGUIDKeys IsNot Nothing Then
                    For Each name As String In aGUIDKeys
                        regKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, pc, regos).OpenSubKey("SOFTWARE\Microsoft\Office\" & aOffID(a, x) & "\Registration" & "\" & name)
                        searchKey(pc, regKey, regos, aOffID, a, x)
                    Next
                End If
            Else
                If (aOffID(a, 0) = "2010") Then
                    okey = DecodeProductKey2010(aDPIDBytes)
                Else
                    okey = DecodeProductKey(aDPIDBytes)
                End If


                Dim oProd = regKey.GetValue("ProductName")
                Dim oEdit = regKey.GetValue("ProductName")
                If oEdit Is Nothing Then
                    oEdit = "Microsoft Office " & aOffID(a, 0)
                End If

                Dim overkey As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, pc, regos).OpenSubKey("SOFTWARE\Microsoft\Office\" & aOffID(a, x) & "\Common\ProductVersion")
                If overkey IsNot Nothing Then
                    over = overkey.GetValue("LastProduct")
                End If

                Dim hprod As String = ""
                Dim oProductCode As String = ""
                Dim strEdition As String = ""
                '' Office XP (http://http://support.microsoft.com/kb/302663)
                If (aOffID(a, 0) = "XP") Then
                    hprod = Mid((Strings.Right(regKey.Name, 38)), 4, 2)
                    Select Case hprod
                        Case "11"
                            oProductCode = "Microsoft Office XP Professional"
                        Case "12"
                            oProductCode = "Microsoft Office XP Standard"
                        Case "13"
                            oProductCode = "Microsoft Office XP Small Business"
                        Case "14"
                            oProductCode = "Microsoft Office XP Web Server"
                        Case "15"
                            oProductCode = "Microsoft Access 2002"
                        Case "16"
                            oProductCode = "Microsoft Excel 2002"
                        Case "17"
                            oProductCode = "Microsoft FrontPage 2002"

                        Case "18"
                            oProductCode = "Microsoft PowerPoint 2002"
                        Case "19"
                            oProductCode = "Microsoft Publisher 2002"
                        Case "1A"
                            oProductCode = "Microsoft Outlook 2002"
                        Case "1B"
                            oProductCode = "Microsoft Word 2002"
                        Case "1C"
                            oProductCode = "Microsoft Access 2002 Runtime"
                        Case "27"
                            oProductCode = "Microsoft Project 2002"
                        Case "28"
                            oProductCode = "Microsoft Office XP Professional With FrontPage"
                        Case "31"
                            oProductCode = "Microsoft Project 2002 Web Client"
                        Case "32"
                            oProductCode = "Microsoft Project 2002 Web Server"
                        Case "3A"
                            oProductCode = "Project 2002 Standard"
                        Case "3B"
                            oProductCode = "Project 2002 Professional"
                        Case "51"
                            oProductCode = "Microsoft Office Visio Professionnel 2002"
                        Case "54"
                            oProductCode = "Microsoft Office Visio Standard 2002"
                    End Select
                End If

                ' Office 2003 (http://support.microsoft.com/kb/832672)
                If (aOffID(a, 0) = "2003") Then
                    hprod = Mid((Strings.Right(regKey.Name, 38)), 4, 2)
                    Select Case hprod
                        Case "11"
                            oProductCode = "Microsoft Office Professional Enterprise Edition 2003"
                        Case "12"
                            oProductCode = "Microsoft Office Standard Edition 2003"
                        Case "13"
                            oProductCode = "Microsoft Office Basic Edition 2003"
                        Case "14"
                            oProductCode = "Microsoft Windows SharePoint Services 2.0"
                        Case "15"
                            oProductCode = "Microsoft Office Access 2003"
                        Case "16"
                            oProductCode = "Microsoft Office Excel 2003"
                        Case "17"
                            oProductCode = "Microsoft Office FrontPage 2003"
                        Case "18"
                            oProductCode = "Microsoft Office PowerPoint 2003"
                        Case "19"
                            oProductCode = "Microsoft Office Publisher 2003"
                        Case "1A"
                            oProductCode = "Microsoft Office Outlook Professional 2003"
                        Case "1B"
                            oProductCode = "Microsoft Office Word 2003"
                        Case "1C"
                            oProductCode = "Microsoft Office Access 2003 Runtime"
                        Case "1E"
                            oProductCode = "Microsoft Office 2003 User Interface Pack"
                        Case "1F"
                            oProductCode = "Microsoft Office 2003 Proofing Tools"
                        Case "23"
                            oProductCode = "Microsoft Office 2003 Multilingual User Interface Pack"
                        Case "24"
                            oProductCode = "Microsoft Office 2003 Resource Kit"
                        Case "26"
                            oProductCode = "Microsoft Office XP Web Components"
                        Case "2E"
                            oProductCode = "Microsoft Office 2003 Research Service SDK"
                        Case "44"
                            oProductCode = "Microsoft Office InfoPath 2003"
                        Case "83"
                            oProductCode = "Microsoft Office 2003 HTML Viewer"
                        Case "92"
                            oProductCode = "Windows SharePoint Services 2.0 English Template Pack"
                        Case "93"
                            oProductCode = "Microsoft Office 2003 English Web Parts And Components"
                        Case "A1"
                            oProductCode = "Microsoft Office OneNote 2003"
                        Case "A4"
                            oProductCode = "Microsoft Office 2003 Web Components"
                        Case "A5"
                            oProductCode = "Microsoft SharePoint Migration Tool 2003"
                        Case "AA"
                            oProductCode = "Microsoft Office PowerPoint 2003 Presentation Broadcast"
                        Case "AB"
                            oProductCode = "Microsoft Office PowerPoint 2003 Template Pack 1"
                        Case "AC"
                            oProductCode = "Microsoft Office PowerPoint 2003 Template Pack 2"
                        Case "AD"
                            oProductCode = "Microsoft Office PowerPoint 2003 Template Pack 3"
                        Case "AE"
                            oProductCode = "Microsoft Organization Chart 2.0"
                        Case "CA"
                            oProductCode = "Microsoft Office Small Business Edition 2003"
                        Case "D0"
                            oProductCode = "Microsoft Office Access 2003 Developer Extensions"
                        Case "DC"
                            oProductCode = "Microsoft Office 2003 Smart Document SDK"
                        Case "E0"
                            oProductCode = "Microsoft Office Outlook Standard 2003"
                        Case "E3"
                            oProductCode = "Microsoft Office Professional Edition 2003 (With InfoPath 2003)"
                        Case "FD"
                            oProductCode = "Microsoft Office Outlook 2003 (distributed by MSN)"
                        Case "FF"
                            oProductCode = "Microsoft Office 2003 Edition Language Interface Pack"
                        Case "F8"
                            oProductCode = "Remove Hidden Data Tool"
                        Case "3A"
                            oProductCode = "Microsoft Office Project Standard 2003"
                        Case "3B"
                            oProductCode = "Microsoft Office Project Professional 2003"
                        Case "32"
                            oProductCode = "Microsoft Office Project Server 2003"
                        Case "51"
                            oProductCode = "Microsoft Office Visio Professional 2003"
                        Case "52"
                            oProductCode = "Microsoft Office Visio Viewer 2003"
                        Case "53"
                            oProductCode = "Microsoft Office Visio Standard 2003"
                        Case "55"
                            oProductCode = "Microsoft Office Visio For Enterprise Architects 2003"
                        Case "5E"
                            oProductCode = "Microsoft Office Visio 2003 Multilingual User Interface Pack"
                    End Select
                End If

                ' Office 2007 (http://support.microsoft.com/kb/928516)
                If (aOffID(a, 0) = "2007") Then
                    hprod = Mid((Strings.Right(regKey.Name, 38)), 11, 4)
                    Select Case hprod
                        Case "0011"
                            oProductCode = "Microsoft Office Professional Plus 2007"
                        Case "0012"
                            oProductCode = "Microsoft Office Standard 2007"
                        Case "0013"
                            oProductCode = "Microsoft Office Basic 2007"
                        Case "0014"
                            oProductCode = "Microsoft Office Professional 2007"
                        Case "0015"
                            oProductCode = "Microsoft Office Access 2007"
                        Case "0016"
                            oProductCode = "Microsoft Office Excel 2007"
                        Case "0017"
                            oProductCode = "Microsoft Office SharePoint Designer 2007"
                        Case "0018"
                            oProductCode = "Microsoft Office PowerPoint 2007"
                        Case "0019"
                            oProductCode = "Microsoft Office Publisher 2007"
                        Case "001A"
                            oProductCode = "Microsoft Office Outlook 2007"
                        Case "001B"
                            oProductCode = "Microsoft Office Word 2007"
                        Case "001C"
                            oProductCode = "Microsoft Office Access Runtime 2007"
                        Case "0020"
                            oProductCode = "Microsoft Office Compatibility Pack For Word, Excel, And PowerPoint 2007 File Formats"
                        Case "0026"
                            oProductCode = "Microsoft Expression Web"
                        Case "002E"
                            oProductCode = "Microsoft Office Ultimate 2007"
                        Case "002F"
                            oProductCode = "Microsoft Office Home And Student 2007"
                        Case "0030"
                            oProductCode = "Microsoft Office Enterprise 2007"
                        Case "0031"
                            oProductCode = "Microsoft Office Professional Hybrid 2007"
                        Case "0033"
                            oProductCode = "Microsoft Office Personal 2007"
                        Case "0035"
                            oProductCode = "Microsoft Office Professional Hybrid 2007"
                        Case "003A"
                            oProductCode = "Microsoft Office Project Standard 2007"
                        Case "003B"
                            oProductCode = "Microsoft Office Project Professional 2007"
                        Case "0044"
                            oProductCode = "Microsoft Office InfoPath 2007"
                        Case "0051"
                            oProductCode = "Microsoft Office Visio Professional 2007"
                        Case "0052"
                            oProductCode = "Microsoft Office Visio Viewer 2007"
                        Case "0053"
                            oProductCode = "Microsoft Office Visio Standard 2007"
                        Case "00A1"
                            oProductCode = "Microsoft Office OneNote 2007"
                        Case "00A3"
                            oProductCode = "Microsoft Office OneNote Home Student 2007"
                        Case "00A7"
                            oProductCode = "Calendar Printing Assistant For Microsoft Office Outlook 2007"
                        Case "00A9"
                            oProductCode = "Microsoft Office InterConnect 2007"
                        Case "00AF"
                            oProductCode = "Microsoft Office PowerPoint Viewer 2007 (English)"
                        Case "00B0"
                            oProductCode = "The Microsoft Save As PDF add-In"
                        Case "00B1"
                            oProductCode = "The Microsoft Save As XPS add-In"
                        Case "00B2"
                            oProductCode = "The Microsoft Save As PDF Or XPS add-In"
                        Case "00BA"
                            oProductCode = "Microsoft Office Groove 2007"
                        Case "00CA"
                            oProductCode = "Microsoft Office Small Business 2007"
                        Case "10D7"
                            oProductCode = "Microsoft Office InfoPath Forms Services"
                        Case "110D"
                            oProductCode = "Microsoft Office SharePoint Server 2007"
                        Case "1122"
                            oProductCode = "Windows SharePoint Services Developer Resources 1.2"
                        Case "0010"
                            oProductCode = "SKU - Microsoft Software Update For Web Folders (English) 12"
                    End Select
                    oEdit = oProductCode
                End If

                ' Office 2010, stuff changed. Product Edition is in the registry key "DigitalProductID"
                If (aOffID(a, 0) = "2010") Then
                    For i = 280 To 312 Step 2
                        If aDPIDBytes(i) <> 0 Then strEdition = strEdition & Chr(aDPIDBytes(i))
                    Next
                    Select Case strEdition
                        Case "ProjectStdVL"
                            oEdit = "Microsoft Office Project Standard 2010 (VL)"
                        Case "ProjectProVL"
                            oEdit = "Microsoft Office Project Professional2010 (VL)"
                        Case "ProjectProMSDNR"
                            oEdit = "Microsoft Project Professional 2010 (MSDN)"
                        Case "HomeBusinessR"
                            oEdit = "Microsoft Office Home And Business 2010"
                        Case "ProfessionalR"
                            oEdit = "Microsoft Office Professional 2010"
                        Case "ProPlusR"
                            oEdit = "Microsoft Office Professional Plus 2010"
                        Case "StandardR"
                            oEdit = "Microsoft Office Standard 2010"
                        Case "StandardVL"
                            oEdit = "Microsoft Office Standard 2010 (VL)"
                        Case "HomeStudentR"
                            oEdit = "Microsoft Office Home And Student 2010"
                        Case "AccessRuntimeR"
                            oEdit = "Microsoft Office Access Runtime 2010"
                        Case "VisioSIR"
                            oEdit = "Microsoft Office Visio Professional 2010"
                        Case "SPDR"
                            oEdit = "Microsoft SharePoint Designer 2010"
                        Case "ProjectProR"
                            oEdit = "Microsoft Project Professional 2010"
                        Case "ProjectStdR"
                            oEdit = "Microsoft Project Standard 2010"
                        Case "VisioSIVL"
                            oEdit = "Microsoft Visio 2010 Standard (VL)"
                        Case "InfoPathR"
                            oEdit = "Microsoft Office InfoPath 2010"
                        Case Else
                            oEdit = "Microsoft Office Unknown Edition 2010: " & strEdition
                    End Select

                    '' Office 2010 Product ID's (http://support.microsoft.com/kb/2186281)
                    hprod = Mid((Strings.Right(regKey.Name, 38)), 11, 4)
                    Select Case hprod
                        Case "0011"
                            oProductCode = "Microsoft Office Professional Plus 2010"
                        Case "0012"
                            oProductCode = "Microsoft Office Standard 2010"
                        Case "0013"
                            oProductCode = "Microsoft Office Home and Business 2010"
                        Case "0014"
                            oProductCode = "Microsoft Office Professional 2010"
                        Case "0015"
                            oProductCode = "Microsoft Access 2010"
                        Case "0016"
                            oProductCode = "Microsoft Excel 2010"
                        Case "0017"
                            oProductCode = "Microsoft SharePoint Designer 2010"
                        Case "0018"
                            oProductCode = "Microsoft PowerPoint 2010"
                        Case "0019"
                            oProductCode = "Microsoft Publisher 2010"
                        Case "001A"
                            oProductCode = "Microsoft Outlook 2010"
                        Case "001B"
                            oProductCode = "Microsoft Word 2010"
                        Case "001C"
                            oProductCode = "Microsoft Access Runtime 2010"
                        Case "001F"
                            oProductCode = "Microsoft Office Proofing Tools Kit Compilation 2010"
                        Case "002F"
                            oProductCode = "Microsoft Office Home and Student 2010"
                        Case "003A"
                            oProductCode = "Microsoft Project Standard 2010"
                        Case "003D"
                            oProductCode = "Microsoft Office Single Image 2010"
                        Case "003B"
                            oProductCode = "Microsoft Project Professionnel 2010"
                        Case "0044"
                            oProductCode = "Microsoft InfoPath 2010"
                        Case "0052"
                            oProductCode = "Microsoft Visio Viewer 2010"
                        Case "0057"
                            oProductCode = "Microsoft Visio 2010"
                        Case "007A"
                            oProductCode = "Microsoft Outlook Connector"
                        Case "008B"
                            oProductCode = "Microsoft Office Small Business Basics 2010"
                        Case "00A1"
                            oProductCode = "Microsoft OneNote 2010"
                        Case "00AF"
                            oProductCode = "Microsoft PowerPoint Viewer 2010"
                        Case "00BA"
                            oProductCode = "Microsoft Office SharePoint Workspace 2010"
                        Case "110D"
                            oProductCode = "Microsoft Office SharePoint Server 2010"
                        Case "110F"
                            oProductCode = "Microsoft Project Server 2010"
                    End Select
                End If

                If oProductCode IsNot Nothing Then
                    oProd = oProductCode
                End If
                If oEdit IsNot Nothing Then
                    oEdit = oProd
                End If
                Dim oProdID As String = ""
                If Mid(oProdID, 7, 3) = "OEM" Then
                    Dim oOEM As String = " OEM"
                    oProd = oProd & oOEM
                End If
                oProdID = regKey.GetValue("ProductID")
                Dim newl As New Licença
                newl.Tipo = oProd
                newl.Key = okey
                newl.Produto = oProdID
                newl.Versão = over
                AtualizarLicença(pc, newl)

            End If
        Catch ex As Exception

            Return okey
        End Try
        Return okey
    End Function
    Public Function searchKey(ByVal pc As Servidor, ByVal regKey As RegistryKey, ByVal regos As RegistryView, ByVal aOffID As Array, ByVal a As Integer, ByVal x As Integer) As String
        Dim okey As String = ""
        Dim over As String = ""
        Try

            Dim aDPIDBytes = regKey.GetValue("DigitalProductID")
            If aDPIDBytes Is Nothing Then
                Dim aGUIDKeys As String() = regKey.GetSubKeyNames
                If aGUIDKeys IsNot Nothing Then
                    For Each name As String In aGUIDKeys
                        regKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, pc.ComputerName, regos).OpenSubKey("SOFTWARE\Microsoft\Office\" & aOffID(a, x) & "\Registration" & "\" & name)
                        searchKey(pc, regKey, regos, aOffID, a, x)
                    Next
                End If
            Else
                If (aOffID(a, 0) = "2010") Then
                    okey = DecodeProductKey2010(aDPIDBytes)
                Else
                    okey = DecodeProductKey(aDPIDBytes)
                End If


                Dim oProd = regKey.GetValue("ProductName")
                Dim oEdit = regKey.GetValue("ProductName")
                If oEdit Is Nothing Then
                    oEdit = "Microsoft Office " & aOffID(a, 0)
                End If

                Dim overkey As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, pc.ComputerName, regos).OpenSubKey("SOFTWARE\Microsoft\Office\" & aOffID(a, x) & "\Common\ProductVersion")
                If overkey IsNot Nothing Then
                    over = overkey.GetValue("LastProduct")
                End If

                Dim hprod As String = ""
                Dim oProductCode As String = ""
                Dim strEdition As String = ""
                '' Office XP (http://http://support.microsoft.com/kb/302663)
                If (aOffID(a, 0) = "XP") Then
                    hprod = Mid((Strings.Right(regKey.Name, 38)), 4, 2)
                    Select Case hprod
                        Case "11"
                            oProductCode = "Microsoft Office XP Professional"
                        Case "12"
                            oProductCode = "Microsoft Office XP Standard"
                        Case "13"
                            oProductCode = "Microsoft Office XP Small Business"
                        Case "14"
                            oProductCode = "Microsoft Office XP Web Server"
                        Case "15"
                            oProductCode = "Microsoft Access 2002"
                        Case "16"
                            oProductCode = "Microsoft Excel 2002"
                        Case "17"
                            oProductCode = "Microsoft FrontPage 2002"

                        Case "18"
                            oProductCode = "Microsoft PowerPoint 2002"
                        Case "19"
                            oProductCode = "Microsoft Publisher 2002"
                        Case "1A"
                            oProductCode = "Microsoft Outlook 2002"
                        Case "1B"
                            oProductCode = "Microsoft Word 2002"
                        Case "1C"
                            oProductCode = "Microsoft Access 2002 Runtime"
                        Case "27"
                            oProductCode = "Microsoft Project 2002"
                        Case "28"
                            oProductCode = "Microsoft Office XP Professional With FrontPage"
                        Case "31"
                            oProductCode = "Microsoft Project 2002 Web Client"
                        Case "32"
                            oProductCode = "Microsoft Project 2002 Web Server"
                        Case "3A"
                            oProductCode = "Project 2002 Standard"
                        Case "3B"
                            oProductCode = "Project 2002 Professional"
                        Case "51"
                            oProductCode = "Microsoft Office Visio Professionnel 2002"
                        Case "54"
                            oProductCode = "Microsoft Office Visio Standard 2002"
                    End Select
                End If

                ' Office 2003 (http://support.microsoft.com/kb/832672)
                If (aOffID(a, 0) = "2003") Then
                    hprod = Mid((Strings.Right(regKey.Name, 38)), 4, 2)
                    Select Case hprod
                        Case "11"
                            oProductCode = "Microsoft Office Professional Enterprise Edition 2003"
                        Case "12"
                            oProductCode = "Microsoft Office Standard Edition 2003"
                        Case "13"
                            oProductCode = "Microsoft Office Basic Edition 2003"
                        Case "14"
                            oProductCode = "Microsoft Windows SharePoint Services 2.0"
                        Case "15"
                            oProductCode = "Microsoft Office Access 2003"
                        Case "16"
                            oProductCode = "Microsoft Office Excel 2003"
                        Case "17"
                            oProductCode = "Microsoft Office FrontPage 2003"
                        Case "18"
                            oProductCode = "Microsoft Office PowerPoint 2003"
                        Case "19"
                            oProductCode = "Microsoft Office Publisher 2003"
                        Case "1A"
                            oProductCode = "Microsoft Office Outlook Professional 2003"
                        Case "1B"
                            oProductCode = "Microsoft Office Word 2003"
                        Case "1C"
                            oProductCode = "Microsoft Office Access 2003 Runtime"
                        Case "1E"
                            oProductCode = "Microsoft Office 2003 User Interface Pack"
                        Case "1F"
                            oProductCode = "Microsoft Office 2003 Proofing Tools"
                        Case "23"
                            oProductCode = "Microsoft Office 2003 Multilingual User Interface Pack"
                        Case "24"
                            oProductCode = "Microsoft Office 2003 Resource Kit"
                        Case "26"
                            oProductCode = "Microsoft Office XP Web Components"
                        Case "2E"
                            oProductCode = "Microsoft Office 2003 Research Service SDK"
                        Case "44"
                            oProductCode = "Microsoft Office InfoPath 2003"
                        Case "83"
                            oProductCode = "Microsoft Office 2003 HTML Viewer"
                        Case "92"
                            oProductCode = "Windows SharePoint Services 2.0 English Template Pack"
                        Case "93"
                            oProductCode = "Microsoft Office 2003 English Web Parts And Components"
                        Case "A1"
                            oProductCode = "Microsoft Office OneNote 2003"
                        Case "A4"
                            oProductCode = "Microsoft Office 2003 Web Components"
                        Case "A5"
                            oProductCode = "Microsoft SharePoint Migration Tool 2003"
                        Case "AA"
                            oProductCode = "Microsoft Office PowerPoint 2003 Presentation Broadcast"
                        Case "AB"
                            oProductCode = "Microsoft Office PowerPoint 2003 Template Pack 1"
                        Case "AC"
                            oProductCode = "Microsoft Office PowerPoint 2003 Template Pack 2"
                        Case "AD"
                            oProductCode = "Microsoft Office PowerPoint 2003 Template Pack 3"
                        Case "AE"
                            oProductCode = "Microsoft Organization Chart 2.0"
                        Case "CA"
                            oProductCode = "Microsoft Office Small Business Edition 2003"
                        Case "D0"
                            oProductCode = "Microsoft Office Access 2003 Developer Extensions"
                        Case "DC"
                            oProductCode = "Microsoft Office 2003 Smart Document SDK"
                        Case "E0"
                            oProductCode = "Microsoft Office Outlook Standard 2003"
                        Case "E3"
                            oProductCode = "Microsoft Office Professional Edition 2003 (With InfoPath 2003)"
                        Case "FD"
                            oProductCode = "Microsoft Office Outlook 2003 (distributed by MSN)"
                        Case "FF"
                            oProductCode = "Microsoft Office 2003 Edition Language Interface Pack"
                        Case "F8"
                            oProductCode = "Remove Hidden Data Tool"
                        Case "3A"
                            oProductCode = "Microsoft Office Project Standard 2003"
                        Case "3B"
                            oProductCode = "Microsoft Office Project Professional 2003"
                        Case "32"
                            oProductCode = "Microsoft Office Project Server 2003"
                        Case "51"
                            oProductCode = "Microsoft Office Visio Professional 2003"
                        Case "52"
                            oProductCode = "Microsoft Office Visio Viewer 2003"
                        Case "53"
                            oProductCode = "Microsoft Office Visio Standard 2003"
                        Case "55"
                            oProductCode = "Microsoft Office Visio For Enterprise Architects 2003"
                        Case "5E"
                            oProductCode = "Microsoft Office Visio 2003 Multilingual User Interface Pack"
                    End Select
                End If

                ' Office 2007 (http://support.microsoft.com/kb/928516)
                If (aOffID(a, 0) = "2007") Then
                    hprod = Mid((Strings.Right(regKey.Name, 38)), 11, 4)
                    Select Case hprod
                        Case "0011"
                            oProductCode = "Microsoft Office Professional Plus 2007"
                        Case "0012"
                            oProductCode = "Microsoft Office Standard 2007"
                        Case "0013"
                            oProductCode = "Microsoft Office Basic 2007"
                        Case "0014"
                            oProductCode = "Microsoft Office Professional 2007"
                        Case "0015"
                            oProductCode = "Microsoft Office Access 2007"
                        Case "0016"
                            oProductCode = "Microsoft Office Excel 2007"
                        Case "0017"
                            oProductCode = "Microsoft Office SharePoint Designer 2007"
                        Case "0018"
                            oProductCode = "Microsoft Office PowerPoint 2007"
                        Case "0019"
                            oProductCode = "Microsoft Office Publisher 2007"
                        Case "001A"
                            oProductCode = "Microsoft Office Outlook 2007"
                        Case "001B"
                            oProductCode = "Microsoft Office Word 2007"
                        Case "001C"
                            oProductCode = "Microsoft Office Access Runtime 2007"
                        Case "0020"
                            oProductCode = "Microsoft Office Compatibility Pack For Word, Excel, And PowerPoint 2007 File Formats"
                        Case "0026"
                            oProductCode = "Microsoft Expression Web"
                        Case "002E"
                            oProductCode = "Microsoft Office Ultimate 2007"
                        Case "002F"
                            oProductCode = "Microsoft Office Home And Student 2007"
                        Case "0030"
                            oProductCode = "Microsoft Office Enterprise 2007"
                        Case "0031"
                            oProductCode = "Microsoft Office Professional Hybrid 2007"
                        Case "0033"
                            oProductCode = "Microsoft Office Personal 2007"
                        Case "0035"
                            oProductCode = "Microsoft Office Professional Hybrid 2007"
                        Case "003A"
                            oProductCode = "Microsoft Office Project Standard 2007"
                        Case "003B"
                            oProductCode = "Microsoft Office Project Professional 2007"
                        Case "0044"
                            oProductCode = "Microsoft Office InfoPath 2007"
                        Case "0051"
                            oProductCode = "Microsoft Office Visio Professional 2007"
                        Case "0052"
                            oProductCode = "Microsoft Office Visio Viewer 2007"
                        Case "0053"
                            oProductCode = "Microsoft Office Visio Standard 2007"
                        Case "00A1"
                            oProductCode = "Microsoft Office OneNote 2007"
                        Case "00A3"
                            oProductCode = "Microsoft Office OneNote Home Student 2007"
                        Case "00A7"
                            oProductCode = "Calendar Printing Assistant For Microsoft Office Outlook 2007"
                        Case "00A9"
                            oProductCode = "Microsoft Office InterConnect 2007"
                        Case "00AF"
                            oProductCode = "Microsoft Office PowerPoint Viewer 2007 (English)"
                        Case "00B0"
                            oProductCode = "The Microsoft Save As PDF add-In"
                        Case "00B1"
                            oProductCode = "The Microsoft Save As XPS add-In"
                        Case "00B2"
                            oProductCode = "The Microsoft Save As PDF Or XPS add-In"
                        Case "00BA"
                            oProductCode = "Microsoft Office Groove 2007"
                        Case "00CA"
                            oProductCode = "Microsoft Office Small Business 2007"
                        Case "10D7"
                            oProductCode = "Microsoft Office InfoPath Forms Services"
                        Case "110D"
                            oProductCode = "Microsoft Office SharePoint Server 2007"
                        Case "1122"
                            oProductCode = "Windows SharePoint Services Developer Resources 1.2"
                        Case "0010"
                            oProductCode = "SKU - Microsoft Software Update For Web Folders (English) 12"
                    End Select
                    oEdit = oProductCode
                End If

                ' Office 2010, stuff changed. Product Edition is in the registry key "DigitalProductID"
                If (aOffID(a, 0) = "2010") Then
                    For i = 280 To 312 Step 2
                        If aDPIDBytes(i) <> 0 Then strEdition = strEdition & Chr(aDPIDBytes(i))
                    Next
                    Select Case strEdition
                        Case "ProjectStdVL"
                            oEdit = "Microsoft Office Project Standard 2010 (VL)"
                        Case "ProjectProVL"
                            oEdit = "Microsoft Office Project Professional2010 (VL)"
                        Case "ProjectProMSDNR"
                            oEdit = "Microsoft Project Professional 2010 (MSDN)"
                        Case "HomeBusinessR"
                            oEdit = "Microsoft Office Home And Business 2010"
                        Case "ProfessionalR"
                            oEdit = "Microsoft Office Professional 2010"
                        Case "ProPlusR"
                            oEdit = "Microsoft Office Professional Plus 2010"
                        Case "StandardR"
                            oEdit = "Microsoft Office Standard 2010"
                        Case "StandardVL"
                            oEdit = "Microsoft Office Standard 2010 (VL)"
                        Case "HomeStudentR"
                            oEdit = "Microsoft Office Home And Student 2010"
                        Case "AccessRuntimeR"
                            oEdit = "Microsoft Office Access Runtime 2010"
                        Case "VisioSIR"
                            oEdit = "Microsoft Office Visio Professional 2010"
                        Case "SPDR"
                            oEdit = "Microsoft SharePoint Designer 2010"
                        Case "ProjectProR"
                            oEdit = "Microsoft Project Professional 2010"
                        Case "ProjectStdR"
                            oEdit = "Microsoft Project Standard 2010"
                        Case "VisioSIVL"
                            oEdit = "Microsoft Visio 2010 Standard (VL)"
                        Case "InfoPathR"
                            oEdit = "Microsoft Office InfoPath 2010"
                        Case Else
                            oEdit = "Microsoft Office Unknown Edition 2010: " & strEdition
                    End Select

                    '' Office 2010 Product ID's (http://support.microsoft.com/kb/2186281)
                    hprod = Mid((Strings.Right(regKey.Name, 38)), 11, 4)
                    Select Case hprod
                        Case "0011"
                            oProductCode = "Microsoft Office Professional Plus 2010"
                        Case "0012"
                            oProductCode = "Microsoft Office Standard 2010"
                        Case "0013"
                            oProductCode = "Microsoft Office Home and Business 2010"
                        Case "0014"
                            oProductCode = "Microsoft Office Professional 2010"
                        Case "0015"
                            oProductCode = "Microsoft Access 2010"
                        Case "0016"
                            oProductCode = "Microsoft Excel 2010"
                        Case "0017"
                            oProductCode = "Microsoft SharePoint Designer 2010"
                        Case "0018"
                            oProductCode = "Microsoft PowerPoint 2010"
                        Case "0019"
                            oProductCode = "Microsoft Publisher 2010"
                        Case "001A"
                            oProductCode = "Microsoft Outlook 2010"
                        Case "001B"
                            oProductCode = "Microsoft Word 2010"
                        Case "001C"
                            oProductCode = "Microsoft Access Runtime 2010"
                        Case "001F"
                            oProductCode = "Microsoft Office Proofing Tools Kit Compilation 2010"
                        Case "002F"
                            oProductCode = "Microsoft Office Home and Student 2010"
                        Case "003A"
                            oProductCode = "Microsoft Project Standard 2010"
                        Case "003D"
                            oProductCode = "Microsoft Office Single Image 2010"
                        Case "003B"
                            oProductCode = "Microsoft Project Professionnel 2010"
                        Case "0044"
                            oProductCode = "Microsoft InfoPath 2010"
                        Case "0052"
                            oProductCode = "Microsoft Visio Viewer 2010"
                        Case "0057"
                            oProductCode = "Microsoft Visio 2010"
                        Case "007A"
                            oProductCode = "Microsoft Outlook Connector"
                        Case "008B"
                            oProductCode = "Microsoft Office Small Business Basics 2010"
                        Case "00A1"
                            oProductCode = "Microsoft OneNote 2010"
                        Case "00AF"
                            oProductCode = "Microsoft PowerPoint Viewer 2010"
                        Case "00BA"
                            oProductCode = "Microsoft Office SharePoint Workspace 2010"
                        Case "110D"
                            oProductCode = "Microsoft Office SharePoint Server 2010"
                        Case "110F"
                            oProductCode = "Microsoft Project Server 2010"
                    End Select
                End If

                If oProductCode IsNot Nothing Then
                    oProd = oProductCode
                End If
                If oEdit IsNot Nothing Then
                    oEdit = oProd
                End If
                Dim oProdID As String = ""
                If Mid(oProdID, 7, 3) = "OEM" Then
                    Dim oOEM As String = " OEM"
                    oProd = oProd & oOEM
                End If
                oProdID = regKey.GetValue("ProductID")
                Dim newl As New Licença
                newl.Tipo = oProd
                newl.Key = okey
                newl.Produto = oProdID
                newl.Versão = over
                pc.Licenças.Add(newl)

            End If
        Catch ex As Exception

            Return okey
        End Try
        Return okey
    End Function
    Public Sub atualizarSoftware(ByVal pc As String, ByVal software As Software)
        Try
            Dim found As Boolean = False
            If Database IsNot Nothing Then
                If Database.Servidores IsNot Nothing Then
                    For Each x1 As Servidor In Database.Servidores
                        If x1.ComputerName.ToLower = pc.ToLower Then
                            For Each x2 As Software In x1.Softwares
                                If x2.Name.ToLower = software.Name.ToLower Then
                                    found = True
                                    x2 = software

                                End If
                            Next
                            If found = False Then
                                x1.Softwares.Add(software)

                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub atualizarProcesso(ByVal pc As String, ByVal processo As Processos)
        Try
            Dim found As Boolean = False
            If Database IsNot Nothing Then
                If Database.Servidores IsNot Nothing Then
                    For Each x1 As Servidor In Database.Servidores
                        If x1.ComputerName.ToLower = pc.ToLower Then
                            For Each x2 As Processos In x1.Processos
                                If x2.Name.ToLower = processo.Name.ToLower And processo.ProcessID.ToLower = x2.ProcessID.ToLower Then
                                    found = True
                                End If
                            Next
                            If found = False Then
                                x1.Processos.Add(processo)

                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub atualizarGeral(ByVal pc As String, ByVal software As Software)
        Try
            Dim found As Boolean = False
            If Database IsNot Nothing Then
                If Database.Servidores IsNot Nothing Then
                    For Each x1 As Servidor In Database.Servidores
                        If x1.ComputerName.ToLower = pc.ToLower Then
                            For Each x2 As Software In x1.Softwares
                                If x2.Name.ToLower = software.Name.ToLower Then
                                    found = True
                                    x2 = software

                                End If
                            Next
                            If found = False Then
                                x1.Softwares.Add(software)

                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub atualizarImpressora(ByVal pc As String, ByVal printer As Impressoras)
        Try
            Dim found As Boolean = False
            If Database IsNot Nothing Then
                If Database.Servidores IsNot Nothing Then
                    For Each x1 As Servidor In Database.Servidores
                        If x1.ComputerName.ToLower = pc.ToLower Then
                            For Each x2 As Impressoras In x1.Impressoras
                                If x2.Name.ToLower = printer.Name.ToLower Then
                                    found = True
                                    x2 = printer

                                End If
                            Next
                            If found = False Then
                                x1.Impressoras.Add(printer)

                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub atualizarDriver(ByVal pc As String, ByVal driver As Drivers)
        Try
            Dim found As Boolean = False
            For Each x1 As Servidor In Database.Servidores
                If x1.ComputerName = pc Then
                    For Each x2 As Drivers In x1.drivers
                        If x2.Name = driver.Name Then
                            found = True
                            x2 = driver
                            Exit For
                        End If
                    Next
                    If found = False Then
                        x1.drivers.Add(driver)
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub
    Public Sub atualizarDiscos(ByVal pc As String, ByVal disco As Discos)
        Try
            Dim found As Boolean = False
            If Database IsNot Nothing Then
                If Database.Servidores IsNot Nothing Then
                    For Each x1 As Servidor In Database.Servidores
                        If x1.ComputerName.ToLower = pc.ToLower Then
                            For Each x2 As Discos In x1.discos
                                If x2.size.ToLower = disco.size.ToLower Then
                                    found = True
                                    x2 = disco

                                End If
                            Next
                            If found = False Then
                                x1.discos.Add(disco)

                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub atualizarUserLocal(ByVal pc As String, ByVal userlocal As UserLocalAccount)
        Try
            Dim found As Boolean = False
            If Database IsNot Nothing Then
                If Database.Servidores IsNot Nothing Then
                    For Each x1 As Servidor In Database.Servidores
                        If x1.ComputerName.ToLower = pc.ToLower Then
                            For Each x2 As UserLocalAccount In x1.UsersLocal
                                If x2.Name.ToLower = userlocal.Name.ToLower Then
                                    found = True
                                    x2 = userlocal

                                End If
                            Next
                            If found = False Then
                                x1.UsersLocal.Add(userlocal)

                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub getinfodiscos(ByVal pc As String)
        On Error Resume Next
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy

        Dim scope As New ManagementScope("\\" & pc & "\root\CIMV2", connection)
        scope.Connect()

        Dim query As New ObjectQuery(
            "SELECT * FROM Win32_LogicalDisk where DriveType=3")

        Dim searcher As New ManagementObjectSearcher(scope, query)
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If

        For Each x1 As Servidor In Database.Servidores
            If x1.ComputerName.ToLower = pc.ToLower Then
                If x1.discos.Count > 0 Then
                    x1.discos.Clear()

                End If
            End If
        Next

        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If

        For Each m As ManagementObject In searcher.Get()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            Dim FileSystem As String = ""
            Dim FreeSpace As String = ""
            Dim Size As String = ""
            If m("FreeSpac") IsNot Nothing Then
                FreeSpace = BytesToString(m("FreeSpace"))
            End If
            If m("Size") IsNot Nothing Then
                Size = BytesToString(m("Size"))
            End If
            Dim newc As New Discos
            newc.FreeSpace = FreeSpace
            newc.size = Size
            atualizarDiscos(pc, newc)
            Me.Update()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
        Next






    End Sub
    Public Sub getinfodiscos(ByVal pc As Servidor)
        On Error Resume Next
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy

        Dim scope As New ManagementScope("\\" & pc.ComputerName & "\root\CIMV2", connection)
        scope.Connect()

        Dim query As New ObjectQuery(
            "SELECT * FROM Win32_LogicalDisk where DriveType=3")

        Dim searcher As New ManagementObjectSearcher(scope, query)
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If



        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If

        For Each m As ManagementObject In searcher.Get()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            Dim FileSystem As String = ""
            Dim FreeSpace As String = ""
            Dim Size As String = ""
            If m("FreeSpac") IsNot Nothing Then
                FreeSpace = BytesToString(m("FreeSpace"))
            End If
            If m("Size") IsNot Nothing Then
                Size = BytesToString(m("Size"))
            End If
            Dim newc As New Discos
            newc.FreeSpace = FreeSpace
            newc.size = Size
            pc.discos.Add(newc)
            Me.Update()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
        Next






    End Sub
    Public Sub GetInfonetwork(ByVal pc As String)
        On Error Resume Next
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy
        Dim scope As New ManagementScope("\\" & pc & "\root\CIMV2", connection)
        scope.Connect()
        Dim query As New ObjectQuery(
            "SELECT * FROM Win32_NetworkAdapterConfiguration" + " WHERE IPEnabled = 'TRUE'")
        Dim searcher As New ManagementObjectSearcher(scope, query)
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        For Each m As ManagementObject In searcher.Get()
            Dim MACAddress As String = ""
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If


            If m("MACAddress") IsNot Nothing Then
                MACAddress = m("MACAddress")
                If Database IsNot Nothing Then
                    If Database.Servidores IsNot Nothing Then
                        For Each x1 As Servidor In Database.Servidores
                            If x1.ComputerName.ToLower = pc.ToLower Then
                                x1.MACAddress = MACAddress

                            End If
                        Next
                    End If
                End If
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            Dim addresses() As String = CType(m("IPAddress"), String())
            If addresses IsNot Nothing Then
                For Each ipaddress As String In addresses
                    If Database IsNot Nothing Then
                        If Database.Servidores IsNot Nothing Then
                            For Each x1 As Servidor In Database.Servidores
                                If x1.ComputerName.ToLower = pc.ToLower Then
                                    x1.IPAddress = ipaddress & ";"

                                End If
                            Next
                        End If
                    End If
                Next
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            ' DefaultIPGateways, probably have more than one value
            Dim defaultgateways() As String = CType(m("DefaultIPGateway"), String())
            If defaultgateways IsNot Nothing Then
                For Each defaultipgateway As String In defaultgateways
                    If Database IsNot Nothing Then
                        If Database.Servidores IsNot Nothing Then
                            For Each x1 As Servidor In Database.Servidores
                                If x1.ComputerName.ToLower = pc.ToLower Then
                                    x1.DefaultIPGateway = defaultipgateway & ";"

                                End If
                            Next
                        End If
                    End If
                Next
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            Dim dns() As String = CType(m("DNSServerSearchOrder"), String())
            If dns IsNot Nothing Then
                For Each dns1 As String In dns
                    If Database IsNot Nothing Then
                        If Database.Servidores IsNot Nothing Then
                            For Each x1 As Servidor In Database.Servidores
                                If x1.ComputerName.ToLower = pc.ToLower Then
                                    Dim found As Boolean = False
                                    For Each x2 As String In x1.Dns
                                        If x2.ToLower = dns1.ToLower Then
                                            found = True
                                        End If
                                    Next
                                    If found = False Then
                                        x1.Dns.Add(dns1)
                                    End If

                                End If
                            Next
                        End If
                    End If
                Next
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If


        Next




    End Sub
    Public Sub GetInfonetwork(ByVal pc As Servidor)
        On Error Resume Next
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy
        Dim scope As New ManagementScope("\\" & pc.ComputerName & "\root\CIMV2", connection)
        scope.Connect()
        Dim query As New ObjectQuery(
            "SELECT * FROM Win32_NetworkAdapterConfiguration" + " WHERE IPEnabled = 'TRUE'")
        Dim searcher As New ManagementObjectSearcher(scope, query)
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        For Each m As ManagementObject In searcher.Get()
            Dim MACAddress As String = ""
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If


            If m("MACAddress") IsNot Nothing Then
                MACAddress = m("MACAddress")
                pc.MACAddress = MACAddress
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            Dim addresses() As String = CType(m("IPAddress"), String())
            If addresses IsNot Nothing Then
                For Each ipaddress As String In addresses
                    pc.IPAddress = ipaddress & ";"
                Next
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            ' DefaultIPGateways, probably have more than one value
            Dim defaultgateways() As String = CType(m("DefaultIPGateway"), String())
            If defaultgateways IsNot Nothing Then
                For Each defaultipgateway As String In defaultgateways
                    pc.DefaultIPGateway = defaultipgateway & ";"
                Next
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            Dim dns() As String = CType(m("DNSServerSearchOrder"), String())
            If dns IsNot Nothing Then
                For Each dns1 As String In dns
                    pc.Dns.Add(dns1)
                Next
            End If
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If


        Next




    End Sub
    Public Sub GetPrinterInfo(ByVal pc As String)
        On Error Resume Next
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy

        Dim scope As New ManagementScope("\\" & pc & "\root\CIMV2", connection)
        scope.Connect()
        Dim query As New ObjectQuery("SELECT * FROM Win32_Printer ")
        Dim searcher As New ManagementObjectSearcher(scope, query)

        For Each x1 As Servidor In Database.Servidores
            If x1.ComputerName.ToLower = pc.ToLower Then
                If x1.Impressoras.Count > 0 Then
                    x1.Impressoras.Clear()

                End If
            End If
        Next

        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        For Each m As ManagementObject In searcher.Get()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            Dim name As String = ""
            Dim servername As String = ""
            Dim ShareName As String = ""
            Dim DriverName As String = ""

            If m("name") IsNot Nothing Then
                name = m("name")
            End If
            If m("servername") IsNot Nothing Then
                servername = m("servername")
            End If
            If m("location") IsNot Nothing Then
                ShareName = m("location")
            End If
            If m("DriverName") IsNot Nothing Then
                DriverName = m("DriverName")
            End If
            Dim pr As New Impressoras
            pr.Name = name
            pr.Server = servername
            pr.Location = ShareName
            pr.Driver = DriverName
            atualizarImpressora(pc, pr)

            Me.Update()

            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
        Next




    End Sub
    Public Sub GetPrinterInfo(ByVal pc As Servidor, ByVal progress As Boolean)
        Try

            Dim connection As New ConnectionOptions
            connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy

            Dim scope As New ManagementScope("\\" & pc.ComputerName & "\root\CIMV2", connection)
            scope.Connect()
            Dim query As New ObjectQuery("SELECT * FROM Win32_Printer ")
            Dim searcher As New ManagementObjectSearcher(scope, query)
            If progress = True Then
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
                Dim count1 As Integer = searcher.Get().Count
                Me.Invoke(Sub() ImpressorasProgressBar.Value = 0)
                Me.Invoke(Sub() ImpressorasProgressBar.Maximum = count1)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
            End If
            For Each m As ManagementObject In searcher.Get()
                If progress = True Then
                    If ScanOnlyInfo.CancellationPending Then
                        Exit Sub
                    End If
                End If
                Dim name As String = ""
                Dim servername As String = ""
                Dim ShareName As String = ""
                Dim DriverName As String = ""

                If m("name") IsNot Nothing Then
                    name = m("name")
                End If
                If m("servername") IsNot Nothing Then
                    servername = m("servername")
                End If
                If m("location") IsNot Nothing Then
                    ShareName = m("location")
                End If
                If m("DriverName") IsNot Nothing Then
                    DriverName = m("DriverName")
                End If
                Dim pr As New Impressoras
                pr.Name = name
                pr.Server = servername
                pr.Location = ShareName
                pr.Driver = DriverName
                pc.Impressoras.Add(pr)
                If progress = True Then
                    Me.Invoke(Sub() ImpressorasProgressBar.Value = ImpressorasProgressBar.Value + 1)
                    If ScanOnlyInfo.CancellationPending Then
                        Exit Sub
                    End If
                End If
            Next


        Catch ex As Exception
        End Try

    End Sub
    Public Sub GetProcessadorInfo(ByVal pc As String)
        On Error Resume Next
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy

        Dim scope As New ManagementScope("\\" & pc & "\root\CIMV2", connection)
        scope.Connect()
        Dim query As New ObjectQuery("SELECT * From Win32_Processor")
        Dim searcher As New ManagementObjectSearcher(scope, query)
        If ScanerComputer.CancellationPending Then
            Exit Sub
        End If
        For Each m As ManagementObject In searcher.Get()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
            Dim Name As String = ""
            Dim status As String = ""
            Dim Description As String = ""
            Dim Manufacturer As String = ""
            If m("Name") IsNot Nothing Then
                Name = m("Name")
            End If
            If m("Manufacturer") IsNot Nothing Then
                Manufacturer = m("Manufacturer")
            End If
            If m("Description") IsNot Nothing Then
                Description = m("Description")
            End If
            If m("ProcessorType") IsNot Nothing Then
                status = m("ProcessorType")
            End If
            Dim p1 As New Processador
            p1.Name = Name
            p1.Fabricante = Manufacturer
            p1.Descrição = Description
            p1.TipodeProcessador = status
            atualizarProcessador(pc, p1)
            Me.Update()
            If ScanerComputer.CancellationPending Then
                Exit Sub
            End If
        Next




    End Sub
    Public Sub GetProcessadorInfo(ByVal pc As Servidor, ByVal progress As Boolean)
        'On Error Resume Next
        Dim connection As New ConnectionOptions
        connection.Authentication = System.Management.AuthenticationLevel.PacketPrivacy

        Dim scope As New ManagementScope("\\" & pc.ComputerName & "\root\CIMV2", connection)
        scope.Connect()
        Dim query As New ObjectQuery("SELECT * From Win32_Processor")
        Dim searcher As New ManagementObjectSearcher(scope, query)
        If progress = True Then
            If ScanOnlyInfo.CancellationPending Then
                Exit Sub
            End If
            Dim count1 As Integer = searcher.Get().Count
            Me.Invoke(Sub() ProcessadorProgressBar.Value = 0)
            Me.Invoke(Sub() ProcessadorProgressBar.Maximum = count1)
            If ScanOnlyInfo.CancellationPending Then
                Exit Sub
            End If
        End If
        For Each m As ManagementObject In searcher.Get()
            If progress = True Then
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
            End If
            Dim Name As String = ""
            Dim status As String = ""
            Dim Description As String = ""
            Dim Manufacturer As String = ""
            If m("Name") IsNot Nothing Then
                Name = m("Name")
            End If
            If m("Manufacturer") IsNot Nothing Then
                Manufacturer = m("Manufacturer")
            End If
            If m("Description") IsNot Nothing Then
                Description = m("Description")
            End If
            If m("ProcessorType") IsNot Nothing Then
                status = m("ProcessorType")
            End If
            Dim p1 As New Processador
            p1.Name = Name
            p1.Fabricante = Manufacturer
            p1.Descrição = Description
            p1.TipodeProcessador = status
            pc.Processadores.Add(p1)
            If progress = True Then
                Me.Invoke(Sub() ProcessadorProgressBar.Value = ProcessadorProgressBar.Value + 1)
                If ScanOnlyInfo.CancellationPending Then
                    Exit Sub
                End If
            End If
        Next




    End Sub
    Public Sub atualizarProcessador(ByVal pc As String, ByVal processador As Processador)
        Try
            Dim found As Boolean = False
            If Database IsNot Nothing Then
                If Database.Servidores IsNot Nothing Then
                    For Each x1 As Servidor In Database.Servidores
                        If x1.ComputerName.ToLower = pc.ToLower Then
                            For Each x2 As Processador In x1.Processadores
                                If x2.Name.ToLower = processador.Name.ToLower Then
                                    found = True
                                    x2 = processador

                                End If
                            Next
                            If found = False Then
                                x1.Processadores.Add(processador)

                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ColetarInformaçãoDoServidorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ColetarInformaçãoDoServidorToolStripMenuItem.Click
        Try
            ScanerComputer.RunWorkerAsync()
            MetroSetContextMenuStrip1.Hide()
        Catch ex As Exception

        End Try
    End Sub



    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Try
            ScanerComputer.CancelAsync()
            MetroSetContextMenuStrip2.Hide()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PingToolStripMenuItem.Click
        Try
            For Each x1 As Control In ServidoresList.Controls
                If x1.GetType() Is GetType(Servidorcontrol) Then
                    Dim p1 As Servidorcontrol = x1
                    If p1.MetroSetBadge1.Text = SelectComputer.ComputerName Then
                        If ping(p1.MetroSetBadge1.Text) Then
                            p1.MetroSetBadge1.NormalBadgeColor = Color.DarkGreen
                            p1.MetroSetBadge1.BadgeText = "ON"
                        Else
                            p1.MetroSetBadge1.NormalBadgeColor = Color.DarkRed
                            p1.MetroSetBadge1.BadgeText = "Off"
                        End If
                    End If
                End If
            Next
            MetroSetContextMenuStrip1.Hide()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton26_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton26.Click
        Try
            AddPrinter.Show()
            If Database.Impressoras Is Nothing Then
                Database.Impressoras = New List(Of Impressoras)
                save()
            End If
            AddPrinter.CountPrinters = Database.Impressoras.Count
        Catch ex As Exception

        End Try
    End Sub
    Public Function TonerLevelRicoh(ByVal host As String) As String
        Dim level As String = ""
        Try

            Dim community As String = "public"
            Dim requestOid() As String
            Dim result As Dictionary(Of Oid, AsnType)
            requestOid = New String() {"1.3.6.1.4.1.367.3.2.1.2.24.1.1.5.1"}
            Dim snmp As SimpleSnmp = New SimpleSnmp(host, community)
            If Not snmp.Valid Then
                Exit Function
            End If
            result = snmp.Get(SnmpVersion.Ver1, requestOid)
            If result IsNot Nothing Then
                Dim kvp As KeyValuePair(Of Oid, AsnType)
                For Each kvp In result
                    level = kvp.Value.ToString()
                Next
            Else
                ' Console.WriteLine("No results received.")
            End If
            Return level
        Catch ex As Exception
            Return level
        End Try
    End Function
    Public Function alreadyexistsPrinter(ByVal name As String) As Boolean
        Dim alreadyhave As Boolean = False
        Try
            If Database IsNot Nothing Then
                If Database.Impressoras IsNot Nothing Then
                    For Each x1 As Impressoras In Database.Impressoras
                        If x1.nome.ToLower = name.ToLower Then
                            alreadyhave = True
                            Return alreadyhave
                            Exit For
                        End If
                    Next
                    Return alreadyhave
                End If
            End If
            Return alreadyhave
        Catch ex As Exception
            Return alreadyhave
        End Try
    End Function
    Public Function StatusPrinter1(ByVal host As String) As String
        Dim level As String = ""
        Try

            Dim community As String = "public"
            Dim requestOid() As String
            Dim result As Dictionary(Of Oid, AsnType)
            requestOid = New String() {"1.3.6.1.2.1.43.16.5.1.2.1.1"}
            Dim snmp As SimpleSnmp = New SimpleSnmp(host, community)
            If Not snmp.Valid Then
                Exit Function
            End If
            result = snmp.Get(SnmpVersion.Ver1, requestOid)
            If result IsNot Nothing Then
                Dim kvp As KeyValuePair(Of Oid, AsnType)
                For Each kvp In result
                    level = kvp.Value.ToString()
                    Dim hexavalues As String = level
                    If Uri.IsHexDigit(hexavalues) Then
                        level = ""
                        Dim strsplit As String() = hexavalues.Split(" "c)
                        For Each hexa As [String] In strsplit
                            Dim values As Integer = Convert.ToInt32(hexa, 16)
                            Dim strvalue As String = [Char].ConvertFromUtf32(values)
                            Dim character As Char = ChrW(values)
                            level = level & strvalue
                        Next
                    End If
                Next
            Else
                ' Console.WriteLine("No results received.")
            End If
            Return level
        Catch ex As Exception
            Return level
        End Try
    End Function
    Public Function TonerLevelSamsung(ByVal host As String) As String
        Dim level As String = ""
        Try

            Dim community As String = "public"
            Dim requestOid() As String
            Dim result As Dictionary(Of Oid, AsnType)
            requestOid = New String() {"1.3.6.1.2.1.43.11.1.1.9.1.1"}
            Dim snmp As SimpleSnmp = New SimpleSnmp(host, community)
            If Not snmp.Valid Then
                Exit Function
            End If
            result = snmp.Get(SnmpVersion.Ver1, requestOid)
            If result IsNot Nothing Then
                Dim kvp As KeyValuePair(Of Oid, AsnType)
                For Each kvp In result
                    level = kvp.Value.ToString()
                Next
            Else
                ' Console.WriteLine("No results received.")
            End If
            Return level
        Catch ex As Exception
            Return level
        End Try
    End Function
    Public Function TonerLevelHp(ByVal host As String) As String
        Dim level As String = ""
        Try

            Dim community As String = "public"
            Dim requestOid() As String
            Dim result As Dictionary(Of Oid, AsnType)
            requestOid = New String() {"1.3.6.1.2.43.11.1.1.6.0.1"}
            Dim snmp As SimpleSnmp = New SimpleSnmp(host, community)
            If Not snmp.Valid Then
                Exit Function
            End If
            result = snmp.Get(SnmpVersion.Ver1, requestOid)
            If result IsNot Nothing Then
                Dim kvp As KeyValuePair(Of Oid, AsnType)
                For Each kvp In result
                    level = kvp.Value.ToString()
                Next
            Else
                ' Console.WriteLine("No results received.")
            End If
            Return level
        Catch ex As Exception
            Return level
        End Try
    End Function
    Public Function TonerTotalLevelSamsung(ByVal host As String) As String
        Dim level As String = ""
        Try

            Dim community As String = "public"
            Dim requestOid() As String
            Dim result As Dictionary(Of Oid, AsnType)
            requestOid = New String() {"1.3.6.1.2.1.43.11.1.1.8.1.1"}
            Dim snmp As SimpleSnmp = New SimpleSnmp(host, community)
            If Not snmp.Valid Then
                Exit Function
            End If
            result = snmp.Get(SnmpVersion.Ver1, requestOid)
            If result IsNot Nothing Then
                Dim kvp As KeyValuePair(Of Oid, AsnType)
                For Each kvp In result
                    level = kvp.Value.ToString()
                Next
            Else
                ' Console.WriteLine("No results received.")
            End If
            Return level
        Catch ex As Exception
            Return level
        End Try
    End Function
    Public Function GetTonerLevel(ByVal host As String) As String
        Dim level1 As String = ""
        Try
            If SelectPrinter.Driver.ToLower.Contains("Samsung".ToLower) And SelectPrinter.Driver.ToLower.Contains("Hp".ToLower) = False Then
                level1 = TonerLevelSamsung(host) & Space(1) & "de:" & Space(1) & TonerTotalLevelSamsung(host)
            Else
                level1 = TonerLevelRicoh(host)
            End If
            If SelectPrinter.Driver.ToLower.Contains("Hp".ToLower) Then
                level1 = TonerLevelHp(host)
            End If
            If level1 = "-3" Then
                level1 = "Informação não suportada pela impressora"
            End If

            If String.IsNullOrWhiteSpace(level1) Then
                level1 = "Não foi possivel coletar informação"
            End If

            Return level1
        Catch ex As Exception
            Return level1
        End Try
    End Function
    Public Function GetTonerLevel(ByVal host As String, ByVal driver As String) As String
        Dim level1 As String = ""
        Try
            If driver.ToLower.Contains("Samsung".ToLower) And driver.ToLower.Contains("Hp".ToLower) = False Then
                level1 = TonerLevelSamsung(host) & Space(1) & "de:" & Space(1) & TonerTotalLevelSamsung(host)
            Else
                level1 = TonerLevelRicoh(host)
            End If
            If driver.ToLower.Contains("Hp".ToLower) Then
                level1 = TonerLevelHp(host)
            End If
            If level1 = "-3" Then
                level1 = "Informação não suportada pela impressora"
            End If

            If String.IsNullOrWhiteSpace(level1) Then
                level1 = "Não foi possivel coletar informação"
            End If

            Return level1
        Catch ex As Exception
            Return level1
        End Try
    End Function

    Public Function CountPagesSamsung(ByVal host As String) As String
        Dim level As String = ""
        Try

            Dim community As String = "public"
            Dim requestOid() As String
            Dim result As Dictionary(Of Oid, AsnType)
            requestOid = New String() {"1.3.6.1.2.1.43.10.2.1.4.1.1"}
            Dim snmp As SimpleSnmp = New SimpleSnmp(host, community)
            If Not snmp.Valid Then
                Exit Function
            End If
            result = snmp.Get(SnmpVersion.Ver1, requestOid)
            If result IsNot Nothing Then
                Dim kvp As KeyValuePair(Of Oid, AsnType)
                For Each kvp In result
                    level = kvp.Value.ToString()
                Next
            Else
                ' Console.WriteLine("No results received.")
            End If
            Return level
        Catch ex As Exception
            Return level
        End Try

    End Function
    Public Function CountPagesRicoh(ByVal host As String) As String
        Dim level As String = ""
        Try

            Dim community As String = "public"
            Dim requestOid() As String
            Dim result As Dictionary(Of Oid, AsnType)
            requestOid = New String() {"1.3.6.1.4.1.367.3.2.1.2.19.1.0"}
            Dim snmp As SimpleSnmp = New SimpleSnmp(host, community)
            If Not snmp.Valid Then
                Exit Function
            End If
            result = snmp.Get(SnmpVersion.Ver1, requestOid)
            If result IsNot Nothing Then
                Dim kvp As KeyValuePair(Of Oid, AsnType)
                For Each kvp In result
                    level = kvp.Value.ToString()
                Next
            Else
                ' Console.WriteLine("No results received.")
            End If
            Return level
        Catch ex As Exception
            Return level
        End Try
    End Function
    Public Function CountPagesHP(ByVal host As String) As String
        Dim level As String = ""
        Try

            Dim community As String = "public"
            Dim requestOid() As String
            Dim result As Dictionary(Of Oid, AsnType)
            requestOid = New String() {"1.3.6.1.2.1.43.10.2.1.4.1.1"}
            Dim snmp As SimpleSnmp = New SimpleSnmp(host, community)
            If Not snmp.Valid Then
                Exit Function
            End If
            result = snmp.Get(SnmpVersion.Ver1, requestOid)
            If result IsNot Nothing Then
                Dim kvp As KeyValuePair(Of Oid, AsnType)
                For Each kvp In result
                    level = kvp.Value.ToString()
                Next
            Else
                ' Console.WriteLine("No results received.")
            End If
            Return level
        Catch ex As Exception
            Return level
        End Try

    End Function
    Public Function GetCountPages(ByVal host As String) As String
        Dim level1 As String = ""
        Try
            If SelectPrinter.Driver.ToLower.Contains("Samsung".ToLower) And SelectPrinter.Driver.ToLower.Contains("Hp".ToLower) = False Then
                level1 = CountPagesSamsung(host)
            Else
                level1 = CountPagesRicoh(host)
            End If
            If SelectPrinter.Driver.ToLower.Contains("Hp".ToLower) Then
                level1 = CountPagesHP(host)
            End If

            Return level1
        Catch ex As Exception
            Return level1
        End Try
        Return level1
    End Function
    Public Function GetCountPages(ByVal host As String, ByVal driver As String) As String
        Dim level1 As String = ""
        Try
            If driver.ToLower.Contains("Samsung".ToLower) And driver.ToLower.Contains("Hp".ToLower) = False Then
                level1 = CountPagesSamsung(host)
            Else
                level1 = CountPagesRicoh(host)
            End If
            If driver.ToLower.Contains("Hp".ToLower) Then
                level1 = CountPagesHP(host)
            End If

            Return level1
        Catch ex As Exception
            Return level1
        End Try
        Return level1
    End Function

    Private Sub MetroDefaultSetButton27_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton27.Click
        Try
            atualizarlistaImpressoras()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        Try
            For Each x1 As Impressoras In Database.Impressoras
                If x1.nome = SelectPrinter.nome Then
                    Database.Impressoras.Remove(x1)
                    save()
                    atualizarlistaImpressoras()
                End If
            Next
            MetroSetContextMenuStrip3.Hide()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        Try
            For Each x1 As Control In ImpressorasLista.Controls
                If x1.GetType() Is GetType(Servidorcontrol) Then
                    Dim p1 As Servidorcontrol = x1
                    If p1.MetroSetBadge1.Text = SelectPrinter.nome Then
                        If ping(GetIp(SelectPrinter.Porta)) Then
                            p1.MetroSetBadge1.NormalBadgeColor = Color.DarkGreen
                            p1.MetroSetBadge1.BadgeText = "ON"
                        Else
                            p1.MetroSetBadge1.NormalBadgeColor = Color.DarkRed
                            p1.MetroSetBadge1.BadgeText = "Off"
                        End If
                    End If
                End If
            Next
            MetroSetContextMenuStrip3.Hide()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Try
            StartCollect1.RunWorkerAsync()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub StartCollect1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles StartCollect1.DoWork
        Try
            Me.Invoke(Sub() ImpressorasProgressBar1.Value = 0)
            Me.Invoke(Sub() ImpressorasProgressBar1.Maximum = 10)
            getinfopc4(SelectPrinter)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub StartCollect1_DoneWork(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles StartCollect1.RunWorkerCompleted
        Try
            Me.Invoke(Sub() ImpressorasProgressBar1.Value = ImpressorasProgressBar1.Maximum)
            Me.Invoke(Sub() clickprinter(sender, e))
        Catch ex As Exception

        End Try
    End Sub
    Public Sub getinfopc4(ByVal printer As Impressoras)
        If StartCollect1.CancellationPending Then
            Exit Sub
        End If
        On Error Resume Next
        Dim connection As New ConnectionOptions
        connection.Impersonation = ImpersonationLevel.Impersonate

        Dim scope As New ManagementScope("\\" & printer.SystemName & "\root\CIMV2", connection)
        scope.Connect()
        If StartCollect1.CancellationPending Then
            Exit Sub
        End If
        Dim query As New ObjectQuery("SELECT * FROM Win32_Printer ")
        Dim searcher As New ManagementObjectSearcher(scope, query)
        For Each m As ManagementObject In searcher.Get()
            If StartCollect1.CancellationPending Then
                Exit Sub
            End If
            Dim nome As String = ""
            If m("Name") IsNot Nothing Then
                nome = m("Name")
            End If
            If nome.ToLower = printer.nome.ToLower Then

                If StartCollect1.CancellationPending Then
                    Exit Sub
                End If
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
                If StartCollect1.CancellationPending Then
                    Exit Sub
                End If

                If m("servername") IsNot Nothing Then
                    NomedoServidor = m("servername")
                End If
                If StartCollect1.CancellationPending Then
                    Exit Sub
                End If
                ImpressorasProgressBar1.Value = ImpressorasProgressBar1.Value + 1
                If m("ShareName") IsNot Nothing Then
                    NomeCompartilhado = m("ShareName")
                End If
                If StartCollect1.CancellationPending Then
                    Exit Sub
                End If
                ImpressorasProgressBar1.Value = ImpressorasProgressBar1.Value + 1
                If m("DriverName") IsNot Nothing Then
                    Driver = m("DriverName")
                End If
                If StartCollect1.CancellationPending Then
                    Exit Sub
                End If
                ImpressorasProgressBar1.Value = ImpressorasProgressBar1.Value + 1
                If m("Description") IsNot Nothing Then
                    Descrição = m("Description")
                End If
                If StartCollect1.CancellationPending Then
                    Exit Sub
                End If
                ImpressorasProgressBar1.Value = ImpressorasProgressBar1.Value + 1
                If m("Comment") IsNot Nothing Then
                    Comentarios = m("Comment")
                End If
                If StartCollect1.CancellationPending Then
                    Exit Sub
                End If
                ImpressorasProgressBar1.Value = ImpressorasProgressBar1.Value + 1
                If m("ShareName") IsNot Nothing Then
                    NomeCompartilhado = m("ShareName")
                End If
                If StartCollect1.CancellationPending Then
                    Exit Sub
                End If
                ImpressorasProgressBar1.Value = ImpressorasProgressBar1.Value + 1
                If m("SystemName") IsNot Nothing Then
                    SystemName = m("SystemName")
                End If
                If StartCollect1.CancellationPending Then
                    Exit Sub
                End If
                ImpressorasProgressBar1.Value = ImpressorasProgressBar1.Value + 1
                If m("Location") IsNot Nothing Then
                    localização = m("Location")
                End If
                If StartCollect1.CancellationPending Then
                    Exit Sub
                End If
                ImpressorasProgressBar1.Value = ImpressorasProgressBar1.Value + 1
                If m("PortName") IsNot Nothing Then
                    Porta = m("PortName")
                End If
                If StartCollect1.CancellationPending Then
                    Exit Sub
                End If
                UltimaVezColletada = DateAndTime.Now.ToString
                If Database IsNot Nothing Then
                    If Database.Impressoras IsNot Nothing Then
                        If alreadyexistsPrinter(nome) = False Then
                            If StartCollect1.CancellationPending Then
                                Exit Sub
                            End If
                            Dim newpr As New Impressoras
                            newpr.nome = nome
                            newpr.Porta = Porta
                            newpr.Comentarios = Comentarios
                            newpr.Descrição = Descrição
                            newpr.Driver = Driver
                            newpr.localização = localização
                            newpr.NomeCompartilhado = NomeCompartilhado
                            newpr.NomedoServidor = NomedoServidor
                            newpr.PáginasContadas = GetCountPages(GetIp(Porta), Driver)
                            newpr.Status = StatusPrinter1(GetIp(Porta))
                            newpr.SystemName = SystemName
                            newpr.TonerRestante = GetTonerLevel(GetIp(Porta), Driver)
                            newpr.UltimaVezColletada = UltimaVezColletada
                            Database.Impressoras.Add(newpr)
                            save()
                            If StartCollect1.CancellationPending Then
                                Exit Sub
                            End If
                        Else
                            For Each x9 As Impressoras In Database.Impressoras
                                If StartCollect1.CancellationPending Then
                                    Exit Sub
                                End If
                                If x9.nome.ToLower = nome.ToLower Then
                                    x9.Porta = Porta
                                    x9.Comentarios = Comentarios
                                    x9.Descrição = Descrição
                                    x9.Driver = Driver
                                    x9.localização = localização
                                    x9.NomeCompartilhado = NomeCompartilhado
                                    x9.NomedoServidor = NomedoServidor
                                    x9.PáginasContadas = GetCountPages(GetIp(Porta), Driver)
                                    x9.Status = StatusPrinter1(GetIp(Porta))
                                    x9.SystemName = SystemName
                                    x9.TonerRestante = GetTonerLevel(GetIp(Porta), Driver)
                                    x9.UltimaVezColletada = UltimaVezColletada
                                    '  Me.Update()
                                    save()
                                End If
                            Next
                        End If
                    End If
                    Exit Sub
                End If
            End If

        Next


    End Sub
    Public Sub getinfoPrinter(ByVal printer As Impressoras)
        If ScanAll.CancellationPending Then
            Exit Sub
        End If
        On Error Resume Next
        Dim connection As New ConnectionOptions
        connection.Impersonation = ImpersonationLevel.Impersonate

        Dim scope As New ManagementScope("\\" & printer.SystemName & "\root\CIMV2", connection)
        scope.Connect()
        If ScanAll.CancellationPending Then
            Exit Sub
        End If
        Dim query As New ObjectQuery("SELECT * FROM Win32_Printer ")
        Dim searcher As New ManagementObjectSearcher(scope, query)
        For Each m As ManagementObject In searcher.Get()
            If ScanAll.CancellationPending Then
                Exit Sub
            End If
            Dim nome As String = ""
            If m("Name") IsNot Nothing Then
                nome = m("Name")
            End If
            If nome.ToLower = printer.nome.ToLower Then

                If ScanAll.CancellationPending Then
                    Exit Sub
                End If
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
                If ScanAll.CancellationPending Then
                    Exit Sub
                End If

                If m("servername") IsNot Nothing Then
                    NomedoServidor = m("servername")
                End If
                If ScanAll.CancellationPending Then
                    Exit Sub
                End If

                If m("ShareName") IsNot Nothing Then
                    NomeCompartilhado = m("ShareName")
                End If
                If ScanAll.CancellationPending Then
                    Exit Sub
                End If

                If m("DriverName") IsNot Nothing Then
                    Driver = m("DriverName")
                End If
                If ScanAll.CancellationPending Then
                    Exit Sub
                End If

                If m("Description") IsNot Nothing Then
                    Descrição = m("Description")
                End If
                If ScanAll.CancellationPending Then
                    Exit Sub
                End If

                If m("Comment") IsNot Nothing Then
                    Comentarios = m("Comment")
                End If
                If ScanAll.CancellationPending Then
                    Exit Sub
                End If

                If m("ShareName") IsNot Nothing Then
                    NomeCompartilhado = m("ShareName")
                End If
                If ScanAll.CancellationPending Then
                    Exit Sub
                End If

                If m("SystemName") IsNot Nothing Then
                    SystemName = m("SystemName")
                End If
                If ScanAll.CancellationPending Then
                    Exit Sub
                End If

                If m("Location") IsNot Nothing Then
                    localização = m("Location")
                End If
                If ScanAll.CancellationPending Then
                    Exit Sub
                End If

                If m("PortName") IsNot Nothing Then
                    Porta = m("PortName")
                End If
                If ScanAll.CancellationPending Then
                    Exit Sub
                End If
                UltimaVezColletada = DateAndTime.Now.ToString
                If Database IsNot Nothing Then
                    If Database.Impressoras IsNot Nothing Then
                        If alreadyexistsPrinter(nome) = False Then
                            If ScanAll.CancellationPending Then
                                Exit Sub
                            End If
                            Dim newpr As New Impressoras
                            newpr.nome = nome
                            newpr.Porta = Porta
                            newpr.Comentarios = Comentarios
                            newpr.Descrição = Descrição
                            newpr.Driver = Driver
                            newpr.localização = localização
                            newpr.NomeCompartilhado = NomeCompartilhado
                            newpr.NomedoServidor = NomedoServidor
                            newpr.PáginasContadas = GetCountPages(GetIp(Porta), Driver)
                            newpr.Status = StatusPrinter1(GetIp(Porta))
                            newpr.SystemName = SystemName
                            newpr.TonerRestante = GetTonerLevel(GetIp(Porta), Driver)
                            newpr.UltimaVezColletada = UltimaVezColletada
                            Database.Impressoras.Add(newpr)
                            save()
                            If ScanAll.CancellationPending Then
                                Exit Sub
                            End If
                        Else
                            For Each x9 As Impressoras In Database.Impressoras
                                If ScanAll.CancellationPending Then
                                    Exit Sub
                                End If
                                If x9.nome.ToLower = nome.ToLower Then
                                    x9.Porta = Porta
                                    x9.Comentarios = Comentarios
                                    x9.Descrição = Descrição
                                    x9.Driver = Driver
                                    x9.localização = localização
                                    x9.NomeCompartilhado = NomeCompartilhado
                                    x9.NomedoServidor = NomedoServidor
                                    x9.PáginasContadas = GetCountPages(GetIp(Porta), Driver)
                                    x9.Status = StatusPrinter1(GetIp(Porta))
                                    x9.SystemName = SystemName
                                    x9.TonerRestante = GetTonerLevel(GetIp(Porta), Driver)
                                    x9.UltimaVezColletada = UltimaVezColletada
                                    '  Me.Update()
                                    save()
                                End If
                            Next
                        End If
                    End If
                    Exit Sub
                End If
            End If

        Next


    End Sub





    Private Sub LimparListaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LimparListaToolStripMenuItem.Click
        Try
            MetroSetContextMenuStrip1.Hide()
            Database.Servidores.Clear()
            save()
            atualizarlista()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LimparListaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LimparListaToolStripMenuItem1.Click
        Try
            Database.Impressoras.Clear()
            save()
            MetroDefaultSetButton27_Click(sender, e)
            MetroSetContextMenuStrip3.Hide()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton23_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton23.Click
        Try
            AtualizarSelb(Selb.Text)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub AtualizarSelb(ByVal selb As String)
        Try
            If Database IsNot Nothing Then
                If Database.Impressoras IsNot Nothing Then
                    For Each x1 As Impressoras In Database.Impressoras
                        If x1.nome.ToLower = SelectPrinter.nome.ToLower Then
                            x1.Selb = selb
                            save()
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub AtualizarID(ByVal id As String)
        Try
            If Database IsNot Nothing Then
                If Database.Impressoras IsNot Nothing Then
                    For Each x1 As Impressoras In Database.Impressoras
                        If x1.nome.ToLower = SelectPrinter.nome.ToLower Then
                            x1.ID = id
                            save()
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub AtualizarTipodeToner(ByVal TipodeToner As String)
        Try
            If Database IsNot Nothing Then
                If Database.Impressoras IsNot Nothing Then
                    For Each x1 As Impressoras In Database.Impressoras
                        If x1.nome.ToLower = SelectPrinter.nome.ToLower Then
                            x1.Toner = TipodeToner
                            save()
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub AtualizarTonerRestante(ByVal tonerrestante As String)
        Try
            If Database IsNot Nothing Then
                If Database.Impressoras IsNot Nothing Then
                    For Each x1 As Impressoras In Database.Impressoras
                        If x1.nome.ToLower = SelectPrinter.nome.ToLower Then
                            x1.TonerRestante = tonerrestante
                            save()
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub AtualizarTonerDisponivel(ByVal TonerDisponivel As String)
        Try
            If Database IsNot Nothing Then
                If Database.Impressoras IsNot Nothing Then
                    For Each x1 As Impressoras In Database.Impressoras
                        If x1.nome.ToLower = SelectPrinter.nome.ToLower Then
                            x1.QuantidadedeTonerDisponivel = TonerDisponivel
                            save()
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub AtualizarUltimaTrocadeToner(ByVal UltimaTrocadeToner As String)
        Try
            If Database IsNot Nothing Then
                If Database.Impressoras IsNot Nothing Then
                    For Each x1 As Impressoras In Database.Impressoras
                        If x1.nome.ToLower = SelectPrinter.nome.ToLower Then
                            x1.UltimaTrocadeToner = UltimaTrocadeToner
                            save()
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton24_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton24.Click
        Try
            AtualizarID(Id.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton28_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton28.Click
        Try
            AtualizarTipodeToner(TipodeToner.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton25_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton25.Click
        Try
            AtualizarTonerDisponivel(TonerDisponivel.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton32_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton32.Click
        Try
            Dim value1 As String = DateAndTime.Now.ToString()
            UltimaTrocadeToner.Text = value1
            AtualizarUltimaTrocadeToner(value1)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton31_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton31.Click
        Try
            TonerRestante.Text = GetTonerLevel(GetIp(SelectPrinter.Porta))

            AtualizarTonerRestante(TonerRestante.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton30_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton30.Click
        Try
            PaginasContadas.Text = GetCountPages(GetIp(SelectPrinter.Porta))
            AtualizarPaginasContadas(PaginasContadas.Text)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub AtualizarPaginasContadas(ByVal PaginasContadas As String)
        Try
            If Database IsNot Nothing Then
                If Database.Impressoras IsNot Nothing Then
                    For Each x1 As Impressoras In Database.Impressoras
                        If x1.nome.ToLower = SelectPrinter.nome.ToLower Then
                            x1.PáginasContadas = PaginasContadas
                            save()
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton29_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton29.Click
        Try
            Dim value As String = ""
            value = StatusPrinter1(GetIp(SelectPrinter.Porta))
            AtualizarStatus(value)
            StatusPrinter.Text = value
        Catch ex As Exception

        End Try
    End Sub
    Public Sub AtualizarStatus(ByVal Status As String)
        Try
            If Database IsNot Nothing Then
                If Database.Impressoras IsNot Nothing Then
                    For Each x1 As Impressoras In Database.Impressoras
                        If x1.nome.ToLower = SelectPrinter.nome.ToLower Then
                            x1.Status = Status
                            save()
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton2_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton2.Click
        Try
            If Nomedocomputador.Text = "" Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual Computador você deseja conectar?", "Digite o nome ou IP", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Nomedocomputador.Text = StatusDate
                End If
            End If
            If ScanOnlyInfo.IsBusy Then
                MetroFramework.MetroMessageBox.Show(Me, "Não é possivel scanear essas infomações no momento!!!O scan está ocupado coletando outras informações,por favor espere acabar o coletamento dessas informações", "scaneamento", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            ScanOnlyInfo.RunWorkerAsync()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ScanOnlyInfo_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles ScanOnlyInfo.DoWork
        Try

            Dim newpc As New Servidor
            newpc.ComputerName = Me.Invoke(Function() Nomedocomputador.Text)
            indextabinfo = Me.Invoke(Function() MetroSetTabControl2.SelectedIndex)
            GetOnlyINFO(newpc, indextabinfo)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub donescanonlyinfo() Handles ScanOnlyInfo.RunWorkerCompleted
        Try
            If indextabinfo = 0 Then
                Me.Invoke(Sub() InfoProgressBar.Value = 0)
            End If
            If indextabinfo = 1 Then
                Me.Invoke(Sub() SoftwareProgressBar.Value = 0)
            End If
            If indextabinfo = 2 Then
                Me.Invoke(Sub() ProcessadorProgressBar.Value = 0)
            End If
            If indextabinfo = 3 Then
                Me.Invoke(Sub() ProcessosProgressBar.Value = 0)
            End If
            If indextabinfo = 4 Then
                Me.Invoke(Sub() DriversProgressBar.Value = 0)
            End If
            If indextabinfo = 5 Then
                Me.Invoke(Sub() ImpressorasProgressBar.Value = 0)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton4_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton4.Click
        Try
            If Nomedocomputador.Text = "" Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual Computador você deseja conectar?", "Digite o nome ou IP", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Nomedocomputador.Text = StatusDate
                End If
            End If
            If ScanOnlyInfo.IsBusy Then
                MetroFramework.MetroMessageBox.Show(Me, "Não é possivel scanear essas infomações no momento!!!O scan está ocupado coletando outras informações,por favor espere acabar o coletamento dessas informações,ou aperte com o botão direito e clique em parar coleta", "scaneamento", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            ScanOnlyInfo.RunWorkerAsync()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton5_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton5.Click
        Try
            If Nomedocomputador.Text = "" Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual Computador você deseja conectar?", "Digite o nome ou IP", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Nomedocomputador.Text = StatusDate
                End If
            End If
            If ScanOnlyInfo.IsBusy Then
                MetroFramework.MetroMessageBox.Show(Me, "Não é possivel scanear essas infomações no momento!!!O scan está ocupado coletando outras informações,por favor espere acabar o coletamento dessas informações,ou aperte com o botão direito e clique em parar coleta", "scaneamento", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            ScanOnlyInfo.RunWorkerAsync()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton6_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton6.Click
        Try
            If Nomedocomputador.Text = "" Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual Computador você deseja conectar?", "Digite o nome ou IP", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Nomedocomputador.Text = StatusDate
                End If
            End If
            If ScanOnlyInfo.IsBusy Then
                MetroFramework.MetroMessageBox.Show(Me, "Não é possivel scanear essas infomações no momento!!!O scan está ocupado coletando outras informações,por favor espere acabar o coletamento dessas informações,ou aperte com o botão direito e clique em parar coleta", "scaneamento", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            ScanOnlyInfo.RunWorkerAsync()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton7_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton7.Click
        Try
            If Nomedocomputador.Text = "" Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual Computador você deseja conectar?", "Digite o nome ou IP", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Nomedocomputador.Text = StatusDate
                End If
            End If
            If ScanOnlyInfo.IsBusy Then
                MetroFramework.MetroMessageBox.Show(Me, "Não é possivel scanear essas infomações no momento!!!O scan está ocupado coletando outras informações,por favor espere acabar o coletamento dessas informações,ou aperte com o botão direito e clique em parar coleta", "scaneamento", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            ScanOnlyInfo.RunWorkerAsync()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton8_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton8.Click
        Try
            If Nomedocomputador.Text = "" Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual Computador você deseja conectar?", "Digite o nome ou IP", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Nomedocomputador.Text = StatusDate
                End If
            End If
            If ScanOnlyInfo.IsBusy Then
                MetroFramework.MetroMessageBox.Show(Me, "Não é possivel scanear essas infomações no momento!!!O scan está ocupado coletando outras informações,por favor espere acabar o coletamento dessas informações,ou aperte com o botão direito e clique em parar coleta", "scaneamento", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            ScanOnlyInfo.RunWorkerAsync()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub EncerrarProcessoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EncerrarProcessoToolStripMenuItem.Click
        If ProcessosGrid2.Rows.Count > 0 Then

            On Error Resume Next
            Dim connOptions As New ConnectionOptions()
            connOptions.Impersonation = ImpersonationLevel.Impersonate
            Dim myScope As New ManagementScope("\\" & Nomedocomputador.Text & "\ROOT\CIMV2", connOptions)
            Dim selectrow As String = ProcessosGrid2.SelectedRows(0).Cells(0).Value

            myScope.Connect()
            Dim objSearcher As New ManagementObjectSearcher("\\" & Nomedocomputador.Text & "\ROOT\CIMV2", "SELECT * FROM Win32_Process")
            Dim opsObserver As New ManagementOperationObserver()
            objSearcher.Scope = myScope
            Dim sep As String() = {vbLf, vbTab}
            For Each obj As ManagementObject In objSearcher.Get()
                'TimeDelay()
                Dim caption As String = obj.GetText(TextFormat.Mof)
                Dim split As String() = caption.Split(sep, StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To split.Length - 1
                    'TimeDelay()
                    If split(i).Split("="c).Length > 1 Then
                        Dim procDetails As String() = split(i).Split("="c)
                        procDetails(1) = procDetails(1).Replace("""", "")
                        procDetails(1) = procDetails(1).Replace(";"c, " "c)
                        Select Case procDetails(0).Trim().ToLower()
                            Case "caption"

                                If procDetails(1).ToLower.Contains(selectrow.ToLower) Then
                                    obj.InvokeMethod("Terminate", Nothing)
                                End If
                                Exit Select
                                ' Case "GetOwner"

                                ' Exit Select
                        End Select
                        ' If procDetails(1).ToString.ToLower.Contains(DataGridView3.SelectedRows(0).Cells(0).Value) Then
                        'obj.InvokeMethod(opsObserver, "Terminate", Nothing)
                        'Exit For
                        'End If
                    End If
                Next


            Next
            ProcessosGrid2.Rows.Clear()
            MetroDefaultSetButton6_Click(sender, e)
        End If

    End Sub

    Private Sub MetroTile8_Click(sender As Object, e As EventArgs) Handles MetroTile8.Click
        Try
            If Nomedocomputador.Text = "" Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual Computador você deseja conectar?", "Digite o nome ou IP", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Nomedocomputador.Text = StatusDate
                End If
            End If
            Process.Start("eventvwr.exe ", Nomedocomputador.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroTile4_Click(sender As Object, e As EventArgs) Handles MetroTile4.Click
        Try
            If Nomedocomputador.Text = "" Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual Computador você deseja conectar?", "Digite o nome ou IP", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Nomedocomputador.Text = StatusDate
                End If
            End If
            Process.Start("fsmgmt.msc ", "/Computer:" & Nomedocomputador.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroTile3_Click(sender As Object, e As EventArgs) Handles MetroTile3.Click
        Try
            If Nomedocomputador.Text = "" Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual Computador você deseja conectar?", "Digite o nome ou IP", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Nomedocomputador.Text = StatusDate
                End If
            End If
            Process.Start("services.msc  ", "/Computer:" & Nomedocomputador.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroTile5_Click(sender As Object, e As EventArgs) Handles MetroTile5.Click
        Try
            If Nomedocomputador.Text = "" Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual Computador você deseja conectar?", "Digite o nome ou IP", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Nomedocomputador.Text = StatusDate
                End If
            End If
            Process.Start("gpedit.msc  ", "/gpcomputer:" & Space(1) & Nomedocomputador.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroTile6_Click(sender As Object, e As EventArgs) Handles MetroTile6.Click
        Try
            If Nomedocomputador.Text = "" Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual Computador você deseja conectar?", "Digite o nome ou IP", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Nomedocomputador.Text = StatusDate
                End If
            End If
            Process.Start("rundll32.exe", "printui.dll ,PrintUIEntry /s /t1 /c\\" & Nomedocomputador.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroTile7_Click(sender As Object, e As EventArgs) Handles MetroTile7.Click
        Try
            If Nomedocomputador.Text = "" Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual Computador você deseja conectar?", "Digite o nome ou IP", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Nomedocomputador.Text = StatusDate
                End If
            End If
            Process.Start("mstsc.exe", "/v:" & Nomedocomputador.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroTile1_Click(sender As Object, e As EventArgs) Handles MetroTile1.Click
        Try
            If Nomedocomputador.Text = "" Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual Computador você deseja conectar?", "Digite o nome ou IP", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Nomedocomputador.Text = StatusDate
                End If
            End If
            Readusbregistry(Nomedocomputador.Text)
            If MetroTile1.Text.ToLower.Contains("Desa".ToLower) Then
                If MetroFramework.MetroMessageBox.Show(Me, "Porta Usb Esta Desabilitado voce quer Habilitar?", "Usb", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    enabledusb(Nomedocomputador.Text)
                    Readusbregistry(Nomedocomputador.Text)
                End If
            End If
            If MetroTile1.Text.ToLower.Contains("Habil".ToLower) Then
                If MetroFramework.MetroMessageBox.Show(Me, "Porta Usb Esta Habilitada voce quer DesaHabilitar?", "Usb", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    disabledusb(Nomedocomputador.Text)
                    Readusbregistry(Nomedocomputador.Text)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroTile2_Click(sender As Object, e As EventArgs) Handles MetroTile2.Click
        Try
            If Nomedocomputador.Text = "" Then
                Dim StatusDate As String
                StatusDate = InputBox("Qual Computador você deseja conectar?", "Digite o nome ou IP", "")
                If StatusDate = "" Then
                    Exit Sub
                Else
                    Nomedocomputador.Text = StatusDate
                End If
            End If
            Dim regos As RegistryView = RegistryView.Registry32
            If GetOS(Nomedocomputador.Text).Contains("64") Then
                regos = RegistryView.Registry64
            End If
            getProxy(Nomedocomputador.Text, regos)
            If MetroTile2.Text.ToLower.Contains("desa".ToLower) Then
                If MetroFramework.MetroMessageBox.Show(Me, "Proxy Esta Desabilitado voce quer Habilitar?", "Proxy", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    ProxyEnabled(Nomedocomputador.Text, regos)
                End If
            End If
            If MetroTile2.Text.ToLower.Contains("Habi".ToLower) Then
                If MetroFramework.MetroMessageBox.Show(Me, "Proxy Esta Habilitado voce quer DesaHabilitar?", "Proxy", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    ProxyDisabled(Nomedocomputador.Text, regos)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroTile9_Click(sender As Object, e As EventArgs) Handles MetroTile9.Click
        Try
            AçõesComputador.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroTile10_Click(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception

        End Try
    End Sub
    Public Sub Readusbregistry(pc As String)
        'Clears last run result
        'Begin Error Handling
        Try
            'Sets variable for remote machine field
            Dim remotemachine As String
            remotemachine = pc
            ' hourglass cursor
            'Begin Code to check Registry
            Dim hive As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, remotemachine, RegistryView.Registry32)
            Dim key As Object = hive.OpenSubKey("SYSTEM\CurrentControlSet\Services\USBSTOR")
            If key Is Nothing Then
                hive = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, remotemachine, RegistryView.Registry64)
                key = hive.OpenSubKey("SYSTEM\CurrentControlSet\Services\USBSTOR")
            End If
            Dim oVal As Object = key.GetValue("start")
            If (Not (oVal) Is Nothing) Then
                If oVal.ToString = 3 Then
                    MetroTile1.Text = "Usb Habilitada:" & pc
                Else
                    MetroTile1.Text = "Usb Desabilitada:" & pc
                End If
                'Return Curson
                'End Registry Check
            End If
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Public Sub enabledusb(pc As String)
        'Clears last run result
        'Begin Error Handling
        Try
            'Sets variable for remote machine field
            Dim remotemachine As String
            remotemachine = pc
            ' hourglass cursor
            'Begin Code to check Registry
            Dim hive As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, remotemachine, RegistryView.Registry32)
            Dim key2 As RegistryKey = hive.OpenSubKey("SYSTEM\CurrentControlSet\Services\USBSTOR", True)
            If key2 Is Nothing Then
                hive = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, remotemachine, RegistryView.Registry64)
                key2 = hive.OpenSubKey("SYSTEM\CurrentControlSet\Services\USBSTOR", True)
            End If
            key2.SetValue("Start", 3, RegistryValueKind.DWord)
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "ToolsIT", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Public Sub disabledusb(pc As String)
        'Clears last run result
        'Begin Error Handling
        Try
            'Sets variable for remote machine field
            Dim remotemachine As String
            remotemachine = pc
            ' hourglass cursor
            'Begin Code to check Registry
            Dim hive As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, remotemachine, RegistryView.Registry32)
            ' Dim key As Object = hive.OpenSubKey("SYSTEM\CurrentControlSet\Services\USBSTOR", True)
            Dim key2 As RegistryKey = hive.OpenSubKey("SYSTEM\CurrentControlSet\Services\USBSTOR", True)
            If (key2 Is Nothing) Then
                hive = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, remotemachine, RegistryView.Registry64)
                key2 = hive.OpenSubKey("SYSTEM\CurrentControlSet\Services\USBSTOR", True)
            End If
            key2.SetValue("Start", 4, RegistryValueKind.DWord)

        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    Public Sub getProxy(ByVal pc As String, ByVal regos As RegistryView)
        On Error Resume Next
        Dim environmentKey As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, pc, regos)
        Dim nxlicense As String = ""
        Dim CurrentUser As String = ""
        Dim ValueNames As String() = environmentKey.GetSubKeyNames
        Dim currentuser1 As String = GetCurrentuser(pc)
        For Each name As String In ValueNames
            Dim key3 As RegistryKey = environmentKey.OpenSubKey(name & "\Volatile Environment")
            If key3 IsNot Nothing Then
                CurrentUser = key3.GetValue("USERNAME")
            End If
            If CurrentUser = "" Then
                If String.IsNullOrWhiteSpace(key3.GetValue("APPDATA")) Then
                Else
                    CurrentUser = key3.GetValue("APPDATA")
                    Dim compl As String = Before(After(CurrentUser, "."), "\")
                    CurrentUser = Before(CurrentUser, ".")
                    CurrentUser = After(CurrentUser, "\") & "." & compl

                End If
            End If
            If currentuser1.Contains(CurrentUser) Then
                Dim ke1 As RegistryKey = environmentKey.OpenSubKey(name & "\Software\Microsoft\Windows\CurrentVersion\Internet Settings")
                If ke1 IsNot Nothing Then
                    nxlicense = ke1.GetValue("ProxyEnable")
                End If
                If (Not (nxlicense) Is Nothing) Then
                    If nxlicense.ToString = 1 Then
                        MetroTile2.Text = "Proxy Hablitado no usuário:" & CurrentUser
                    Else
                        MetroTile2.Text = "Proxy Desablitado no usuário:" & CurrentUser
                    End If
                End If
            End If
        Next


    End Sub
    Public Sub ProxyEnabled(ByVal pc As String, ByVal regos As RegistryView)
        Try
            Dim environmentKey As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, pc, regos)
            Dim nxlicense As String = ""
            Dim CurrentUser As String = ""
            Dim ValueNames As String() = environmentKey.GetSubKeyNames
            Dim currentuser1 As String = GetCurrentuser(pc)
            For Each name As String In ValueNames
                Dim key3 As RegistryKey = environmentKey.OpenSubKey(name & "\Volatile Environment")
                If key3 IsNot Nothing Then
                    CurrentUser = key3.GetValue("USERNAME")
                End If
                If CurrentUser = "" Then
                    If String.IsNullOrWhiteSpace(key3.GetValue("APPDATA")) Then
                    Else
                        CurrentUser = key3.GetValue("APPDATA")
                        Dim compl As String = Before(After(CurrentUser, "."), "\")
                        CurrentUser = Before(CurrentUser, ".")
                        CurrentUser = After(CurrentUser, "\") & "." & compl

                    End If
                End If

                If currentuser1.Contains(CurrentUser) Then
                    Dim ke1 As RegistryKey = environmentKey.OpenSubKey(name & "\Software\Microsoft\Windows\CurrentVersion\Internet Settings")
                    ke1.SetValue("ProxyEnable", 1, RegistryValueKind.DWord)
                    If ke1 IsNot Nothing Then
                        nxlicense = ke1.GetValue("ProxyEnable")
                    End If
                    If (Not (nxlicense) Is Nothing) Then
                        If nxlicense.ToString = 1 Then
                            MetroTile2.Text = "Proxy Hablitado,usuário:" & currentuser1
                        Else
                            MetroTile2.Text = "Proxy Desablitado,usuário:" & currentuser1
                        End If
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ProxyDisabled(ByVal pc As String, ByVal regos As RegistryView)
        Try
            Dim environmentKey As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, pc, regos)
            Dim nxlicense As String = ""
            Dim CurrentUser As String = ""
            Dim ValueNames As String() = environmentKey.GetSubKeyNames
            Dim currentuser1 As String = GetCurrentuser(pc)
            For Each name As String In ValueNames
                Dim key3 As RegistryKey = environmentKey.OpenSubKey(name & "\Volatile Environment")
                If key3 IsNot Nothing Then
                    CurrentUser = key3.GetValue("USERNAME")
                End If
                If key3 IsNot Nothing Then
                    CurrentUser = key3.GetValue("USERNAME")
                End If
                If CurrentUser = "" Then
                    If String.IsNullOrWhiteSpace(key3.GetValue("APPDATA")) Then
                    Else
                        CurrentUser = key3.GetValue("APPDATA")
                        Dim compl As String = Before(After(CurrentUser, "."), "\")
                        CurrentUser = Before(CurrentUser, ".")
                        CurrentUser = After(CurrentUser, "\") & "." & compl
                    End If
                End If

                If currentuser1.Contains(CurrentUser) Then
                    Dim ke1 As RegistryKey = environmentKey.OpenSubKey(name & "\Software\Microsoft\Windows\CurrentVersion\Internet Settings")
                    ke1.SetValue("ProxyEnable", 0, RegistryValueKind.DWord)
                    If ke1 IsNot Nothing Then
                        nxlicense = ke1.GetValue("ProxyEnable")
                    End If
                    If (Not (nxlicense) Is Nothing) Then
                        If nxlicense.ToString = 1 Then
                            MetroTile2.Text = "Proxy Hablitado,usuário:" & currentuser1
                        Else
                            MetroTile2.Text = "Proxy Desablitado,usuário:" & currentuser1
                        End If
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton16_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton16.Click
        Try
            SoftwaresGrid2.Rows.Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton17_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton17.Click
        Try
            ProcessadorGrid2.Rows.Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton18_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton18.Click
        Try
            Try
                ProcessosGrid2.Rows.Clear()
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton19_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton19.Click
        Try
            Try
                DriversGrid2.Rows.Clear()
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton20_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton20.Click
        Try
            PrintersGrid2.Rows.Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton34_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton34.Click
        Try
            For Each tr As TreeNode In ComputadoresChecked.Nodes
                If tr.Checked Then
                    tr.Checked = False
                Else
                    tr.Checked = True
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton35_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton35.Click
        Try
            For Each tr As TreeNode In ImpressorasChecked.Nodes
                If tr.Checked Then
                    tr.Checked = False
                Else
                    tr.Checked = True
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub



    Private Sub MetroSetCheckBox2_CheckedChanged(sender As Object) Handles Relatorioall.CheckedChanged
        Try
            If Relatorioall.Checked Then
                For Each tr As TreeNode In ComputadoresChecked.Nodes
                    If tr.Checked = False Then
                        tr.Checked = True
                    End If
                Next
                For Each tr As TreeNode In ImpressorasChecked.Nodes
                    If tr.Checked = False Then
                        tr.Checked = True
                    End If
                Next
                ComputadoresChecked.Enabled = False
                ImpressorasChecked.Enabled = False
                MetroDefaultSetButton34.Enabled = False
                MetroDefaultSetButton35.Enabled = False
            Else
                ComputadoresChecked.Enabled = True
                ImpressorasChecked.Enabled = True
                MetroDefaultSetButton34.Enabled = True
                MetroDefaultSetButton35.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TreeView3_AfterSelect1(sender As Object, e As TreeViewEventArgs) Handles RelatoriosChecked.AfterCheck
        Try
            If e.Node.Checked And e.Node.Nodes.Count > 0 Then
                For Each x1 As TreeNode In e.Node.Nodes
                    If x1.Checked Then
                        x1.Checked = False
                    Else
                        x1.Checked = True
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker12_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles ScanAll.DoWork
        Try
            Me.Invoke(Sub() ScanProgressBar.Value = 0)
            Dim countComputers As Integer = Me.Invoke(Function() CheckedNames(ComputadoresChecked.Nodes).Count)
            Dim countprinter As Integer = Me.Invoke(Function() CheckedNames(ImpressorasChecked.Nodes).Count)
            If (countComputers + countprinter < 1) Then
                MetroFramework.MetroMessageBox.Show(Me, "Nenhuma impressora ou computador selecionado", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Me.Invoke(Sub() ScanProgressBar.Maximum = countComputers + countprinter)
            If ScanAll.CancellationPending Then
                Exit Sub
            End If
            For Each x1 As String In Me.Invoke(Function() CheckedNames(ComputadoresChecked.Nodes))
                For Each x2 As Servidor In Database.Servidores
                    If ScanAll.CancellationPending Then
                        Exit Sub
                    End If
                    If x1 = x2.ComputerName Then
                        Me.Invoke(Sub() StatusScan.Text = "Scaneando Informação do computador:" & x1)
                        If ping(x2.ComputerName) Then
                            Getallinfo2(x2)
                            If ScanAll.CancellationPending Then
                                Exit Sub
                            End If
                        End If
                        Me.Invoke(Sub() ScanProgressBar.Value = ScanProgressBar.Value + 1)
                        Me.Invoke(Sub() save())
                    End If
                Next
            Next
            If ScanAll.CancellationPending Then
                Exit Sub
            End If
            For Each x10 As String In Me.Invoke(Function() CheckedNames(ImpressorasChecked.Nodes))
                For Each x2 As Impressoras In Database.Impressoras
                    If ScanAll.CancellationPending Then
                        Exit Sub
                    End If
                    If x10 = x2.nome Then
                        Me.Invoke(Sub() StatusScan.Text = "Scaneando Informação da impressora:" & x10)
                        getinfoPrinter(x2)
                        If ScanAll.CancellationPending Then
                            Exit Sub
                        End If
                        Me.Invoke(Sub() ScanProgressBar.Value = ScanProgressBar.Value + 1)
                        Me.Invoke(Sub() save())
                    End If
                Next
            Next
            Me.Invoke(Sub() StatusScan.Text = "Scaneamento terminado com sucesso")
            If My.Application.CommandLineArgs.Count > 0 Then
                startReportAll()
                Application.Exit()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub startReportAll()
        Dim patch As String = My.Settings.DatabaseLocation & "\Reports" & "\" & Date.Now.ToLongDateString
        Try
            patch = IO.Path.GetFullPath(patch)
            If IO.Directory.Exists(patch) Then
            Else
                IO.Directory.CreateDirectory(patch)
            End If
            If IO.Directory.Exists(patch) Then
                For Each x1 As Servidor In Database.Servidores
                    savepdf(x1.ComputerName, x1, patch)
                Next
                For Each x11 As Impressoras In Database.Impressoras
                    savepdf(x11.nome, x11, patch)
                Next
                For Each x111 As Patrimonio In Database.Patrimonios
                    savepdf(x111.Patrimonio, x111, patch)
                Next
                For Each x1111 As Licença In Database.Licenças
                    savepdf(x1111.Descrição, x1111, patch)
                Next
                Me.Invoke(Sub() Reportallinfoandsendemail())
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton15_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton15.Click
        Try
            If Scanear.Checked Then
                ScanAll.CancelAsync()
            End If
            If GerarRelatorio.Checked Then
                ReportNow.CancelAsync()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton37_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton37.Click
        Try
            If Scanear.Checked Then
                If ScanAll.IsBusy Then
                    MetroFramework.MetroMessageBox.Show(Me, "Não é possivel scanear essas infomações no momento!!!O scan está ocupado coletando outras informações,por favor espere acabar o coletamento dessas informações ou clique em parar execução", "scaneamento", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    ScanAll.RunWorkerAsync()
                End If
            End If
            If GerarRelatorio.Checked Then
                If ReportNow.IsBusy Then
                    MetroFramework.MetroMessageBox.Show(Me, "Não é possivel gerar relatório  dessas infomações no momento!!!O scan está ocupado coletando outras informações,por favor espere acabar o coletamento dessas informações ou clique em parar execução", "scaneamento", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    ReportNow.RunWorkerAsync()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BackgroundWorker13_finish(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles ScanAll.RunWorkerCompleted
        Try
            If Me.Invoke(Function() StatusScan.Text.Contains("Scaneamento terminado com sucesso")) = False Then
                Me.Invoke(Sub() StatusScan.Text = "Scaneamento cancelado com sucesso")
            End If
            Me.Invoke(Sub() ScanProgressBar.Value = 0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton36_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton36.Click
        Try
            Try
                Process.Start("Taskschd.msc")
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton33_Click_1(sender As Object, e As EventArgs) Handles MetroDefaultSetButton33.Click
        Try
            Using ts As New TaskService(My.Computer.Name)
                ' Create a new task definition and assign properties
                Dim td As TaskDefinition = ts.NewTask()
                td.Principal.LogonType = TaskLogonType.InteractiveToken
                td.RegistrationInfo.Description = "Coletar Informações"
                td.Data = HorarioInicial.Text
                td.Triggers.Add(New WeeklyTrigger(DaysOfTheWeek.AllDays, 1)).StartBoundary = Convert.ToDateTime(HorarioInicial.Text)
                ' Create an ac.Ação that will launch Notepad whenever the trigger fires
                td.Actions.Add(New ExecAction(Application.ExecutablePath, "Check"))

                ' Register the task in the root folder
                ts.RootFolder.RegisterTaskDefinition("Coletar informações", td, TaskCreation.CreateOrUpdate, UsuarioWindows.Text, SenhaWindows.Text, TaskLogonType.InteractiveTokenOrPassword, Nothing)
                MetroFramework.MetroMessageBox.Show(Me, "Agendamento Concluido com Sucesso", "Agendamento", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub MetroDefaultSetButton39_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton39.Click
        Try
            If login() Then
                Database.Emailadmin = Email.Text
                Database.Senhaadmin = Senha.Text
                Database.servidorEmail = Servidor.Text
                save()
                MetroFramework.MetroMessageBox.Show(Me, "Conta de Email configurada com sucesso", "Concluido", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MetroFramework.MetroMessageBox.Show(Me, "Não foi possivel configurar essa conta de email ,verifique se suas informações estão corretas ou se há conexão com a internet", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function login() As Boolean
        Dim ok As Boolean = False
        Try
            Dim user As String = Email.Text
            Dim pass As String = Senha.Text
            Dim server As String = Servidor.Text
            Dim service As New Global.Microsoft.Exchange.WebServices.Data.ExchangeService
            service.Credentials = New WebCredentials(user, pass, Environment.UserDomainName)
            service.Url = New Uri("https://" & server & "/ews/Exchange.asmx")
            Dim rootfolder As Folder = Folder.Bind(service, WellKnownFolderName.MsgFolderRoot)
            rootfolder.Load()
            If rootfolder.FindFolders(New FolderView(100)).Count <> 0 Then
                ok = True
                Return ok
            End If
            Return ok
        Catch ex As Exception
            Return ok
        End Try
    End Function

    Private Sub MetroDefaultSetButton38_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton38.Click
        Try
            Database.Emailadmin = ""
            Database.Senhaadmin = ""
            Database.servidorEmail = ""
            Email.Text = ""
            Senha.Text = ""
            Servidor.Text = ""
            save()
            MetroFramework.MetroMessageBox.Show(Me, "Conta de Email removida com sucesso", "Concluido", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReportNow_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles ReportNow.DoWork
        Try
            If Me.Invoke(Function() Relatorioall.Checked) = True Then
                ReportallPrintersandcomputers("Relatório Geral" & DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss"))
                Exit Sub
            End If
            Me.Invoke(Sub() ScanProgressBar.Value = 0)
            Dim countComputers As Integer = Me.Invoke(Function() CheckedNames(ComputadoresChecked.Nodes).Count)
            Dim countprinter As Integer = Me.Invoke(Function() CheckedNames(ImpressorasChecked.Nodes).Count)
            If (countComputers + countprinter < 1) Then
                MetroFramework.MetroMessageBox.Show(Me, "Nenhuma impressora ou computador selecionado", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Me.Invoke(Sub() ScanProgressBar.Maximum = countComputers + countprinter)
            If ReportNow.CancellationPending Then
                Exit Sub
            End If
            Dim patch As String = System.IO.Path.GetTempPath & "Relátorio" & DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") & ".pdf"
            If System.IO.File.Exists(patch) Then
                System.IO.File.Delete(patch)
            End If
            Dim doc = New iTextSharp.text.Document()
            PdfWriter.GetInstance(doc, New IO.FileStream(patch, IO.FileMode.Create))
            doc.Open()
            For Each x1 As String In Me.Invoke(Function() CheckedNames(ComputadoresChecked.Nodes))
                For Each x2 As Servidor In Database.Servidores
                    If ReportNow.CancellationPending Then
                        Exit Sub
                    End If
                    If x1 = x2.ComputerName Then
                        Me.Invoke(Sub() StatusScan.Text = "Gerando relatório do computador:" & x1)
                        ReportComputer(x2, doc, Me.Invoke(Function() CheckedNames(RelatoriosChecked.Nodes)))
                        If ReportNow.CancellationPending Then
                            Exit Sub
                        End If
                        Me.Invoke(Sub() ScanProgressBar.Value = ScanProgressBar.Value + 1)
                    End If
                Next
            Next
            If ReportNow.CancellationPending Then
                Exit Sub
            End If
            For Each x10 As String In Me.Invoke(Function() CheckedNames(ImpressorasChecked.Nodes))
                For Each x2 As Impressoras In Database.Impressoras
                    If ReportNow.CancellationPending Then
                        Exit Sub
                    End If
                    If x10 = x2.nome Then
                        Me.Invoke(Sub() StatusScan.Text = "Gerando relatório da impressora:" & x10)
                        ReportPrinter(x2, doc, Me.Invoke(Function() CheckedNames(RelatoriosChecked.Nodes)))
                        If ReportNow.CancellationPending Then
                            Exit Sub
                        End If
                        Me.Invoke(Sub() ScanProgressBar.Value = ScanProgressBar.Value + 1)

                    End If
                Next
            Next
            doc.Close()
            Process.Start(patch)
            Me.Invoke(Sub() StatusScan.Text = "Relatório terminado com sucesso")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BackgroundWorker12_finish(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles ReportNow.RunWorkerCompleted
        Try
            If Me.Invoke(Function() StatusScan.Text.Contains("Relatório terminado com sucesso")) = False Then
                Me.Invoke(Sub() StatusScan.Text = "Relátorio cancelado com sucesso")
            End If
            Me.Invoke(Sub() ScanProgressBar.Value = 0)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ReportallPrintersandcomputers(ByVal namepdf)
        Try
            Me.Invoke(Sub() ScanProgressBar.Value = 0)
            Me.Invoke(Sub() ScanProgressBar.Maximum = Database.Computadores.Count + Database.Impressoras.Count)
            Dim patch As String = System.IO.Path.GetTempPath & namepdf & ".pdf"

            If System.IO.File.Exists(patch) Then
                System.IO.File.Delete(patch)

            End If


            Dim doc = New iTextSharp.text.Document()
            PdfWriter.GetInstance(doc, New IO.FileStream(patch, IO.FileMode.Create))
            doc.Open()
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            For Each x1 As Impressoras In Database.Impressoras
                Me.Invoke(Sub() ScanProgressBar.Value = ScanProgressBar.Value + 1)
                Me.Invoke(Sub() StatusScan.Text = "Gerando relatório da impressora:" & x1.Name)
                Dim prh1 As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(x1.nome, fontTable3)
                prh1.Alignment = iTextSharp.text.Element.ALIGN_CENTER
                doc.Add(prh1)
                doc.Add(New iTextSharp.text.Paragraph("Comentários:" & x1.Comentarios))
                doc.Add(New iTextSharp.text.Paragraph("Descrição:" & x1.Descrição))
                doc.Add(New iTextSharp.text.Paragraph("Driver:" & x1.Driver))
                doc.Add(New iTextSharp.text.Paragraph("ID:" & x1.ID))
                doc.Add(New iTextSharp.text.Paragraph("Localização:" & x1.localização))
                doc.Add(New iTextSharp.text.Paragraph("Nome Compartilhado:" & x1.NomeCompartilhado))
                doc.Add(New iTextSharp.text.Paragraph("Nome do  Servidor:" & x1.NomedoServidor))
                doc.Add(New iTextSharp.text.Paragraph("Porta:" & x1.Porta))
                doc.Add(New iTextSharp.text.Paragraph("Páginas Contadas:" & x1.PáginasContadas))
                doc.Add(New iTextSharp.text.Paragraph("Toner Reservas disponiveis:" & x1.QuantidadedeTonerDisponivel))
                doc.Add(New iTextSharp.text.Paragraph("Selb:" & x1.Selb))
                doc.Add(New iTextSharp.text.Paragraph("Status:" & x1.Status))
                doc.Add(New iTextSharp.text.Paragraph("System Name:" & x1.SystemName))
                doc.Add(New iTextSharp.text.Paragraph("Tipo de Toner:" & x1.Toner))
                doc.Add(New iTextSharp.text.Paragraph("Toner Restante:" & x1.TonerRestante))
                doc.Add(New iTextSharp.text.Paragraph("Ultima Troca de Toner:" & x1.UltimaTrocadeToner))
                doc.Add(New iTextSharp.text.Paragraph("Ultima Coleta feita:" & x1.UltimaVezColletada))
            Next
            For Each pc As Servidor In Database.Servidores
                Me.Invoke(Sub() ScanProgressBar.Value = ScanProgressBar.Value + 1)
                Me.Invoke(Sub() StatusScan.Text = "Gerando relatório do computador:" & pc.ComputerName)
                If pc.Ultimovezscaneada <> "" Then
                    Dim prh2 As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(pc.ComputerName, fontTable2)
                    prh2.Alignment = iTextSharp.text.Element.ALIGN_CENTER
                    doc.Add(prh2)
                    doc.Add(New iTextSharp.text.Paragraph("Sistema Operacional:" & pc.OperatingSystem, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Usuário Logado:" & pc.CurrentUser, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Ip:" & pc.IPAddress, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Modelo:" & pc.Model, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Memória Disponivel:" & pc.TotalVisibleMemorySize, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Número de fabricação:" & pc.BuildNumber, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Arquitetura:" & pc.OSArchitecture, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Fabricante:" & pc.Manufacturer, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Versão:" & pc.Version, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Status:" & pc.Status, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Diretório do Windows:" & pc.WindowsDirectory, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Número serial:" & pc.SerialNumber, fontTable3))
                    Dim dns2 As String = ""
                    For Each dns1 As String In pc.Dns
                        dns2 = dns1 & ";"
                    Next
                    doc.Add(New iTextSharp.text.Paragraph("Dns:" & dns2, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Gateway:" & pc.DefaultIPGateway, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("MacAdress:" & pc.MACAddress, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Data da ultima instalação:" & pc.InstallDate, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Horário Local:" & pc.LocalDateTime, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Ultimo Reinicio:" & pc.LastBootUpTime, fontTable3))
                    doc.Add(New iTextSharp.text.Paragraph("Scaneado pela última vez na data:" & pc.Ultimovezscaneada, fontTable3))
                End If
                If pc.discos.Count > 0 Then
                    tablepdf(doc, "Discos", pc.discos, 2)
                End If
                If pc.Licenças.Count > 0 Then
                    tablepdf(doc, "Licenças", pc.Licenças, 4)
                End If
                If pc.Processadores.Count > 0 Then
                    tablepdf(doc, "Processadores", pc.Processadores, 4)
                End If
                If pc.Softwares.Count > 0 Then
                    tablepdf(doc, "Softwares", pc.Softwares, 4)
                End If
                If pc.Impressoras.Count > 0 Then
                    tablepdf(doc, "Impressoras", pc.Impressoras, 4)
                End If
                If pc.drivers.Count > 0 Then
                    tablepdf(doc, "Drivers", pc.drivers, 3)
                End If
                If pc.UsersLocal.Count > 0 Then
                    tablepdf(doc, "Usuários Locais", pc.UsersLocal, 4)
                End If
                If pc.Processos.Count > 0 Then
                    tablepdf(doc, "Processos", pc.Processos, 8)
                End If
            Next
            doc.Close()
            Process.Start(patch)


        Catch ex As Exception

        End Try
    End Sub
    Public Sub Reportallinfoandsendemail()
        Try
            Database.Avisos.Clear()
            checkerros()
            checkerrosexistentes()
            save()
            Dim locate1 As String = IO.Path.GetFullPath(My.Settings.DatabaseLocation & "\Reports" & "\" & Date.Now.ToLongDateString)
            If locate1.Contains(" ") Then
                locate1 = locate1.Replace(" ", "%20")
            End If
            Dim html1 As String = "<html><meta http-equiv=""Content-type"" content=""text/html;charset=UTF-8""><body><br>Os relátorios de hoje foram guardados em:<p>" & "<a href = " & locate1 & "> " & IO.Path.GetFullPath(My.Settings.DatabaseLocation & "\Reports" & "\" & Date.Now.ToLongDateString) & "</a>" & "</p><br>"
            sendmessage(html1)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub tablepdf(ByVal doc As iTextSharp.text.Document, ByVal title As String, ByVal st1 As List(Of UserLocalAccount), ByVal colcount As Integer)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim table As New PdfPTable(colcount)
            table.SpacingBefore = 45.0F
            table.TotalWidth = 300
            table.DefaultCell.Phrase = New iTextSharp.text.Phrase()
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(title, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            table.AddCell(New iTextSharp.text.Phrase("Nome", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Tipo", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Status", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Dominio", fontTable))
            table.HeaderRows = 1
            For Each st As UserLocalAccount In st1
                table.AddCell(New iTextSharp.text.Phrase(st.Name, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.AccountType, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Status, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Domain, fontTable))
            Next




            doc.Add(table)

        Catch ex As Exception

        End Try

    End Sub
    Public Sub tablepdf(ByVal doc As iTextSharp.text.Document, ByVal title As String, ByVal st1 As List(Of Discos), ByVal colcount As Integer)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim table As New PdfPTable(colcount)
            table.SpacingBefore = 45.0F
            table.TotalWidth = 300
            table.DefaultCell.Phrase = New iTextSharp.text.Phrase()
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(title, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            table.AddCell(New iTextSharp.text.Phrase("Tamanho Total", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Espaço Livre", fontTable))
            table.HeaderRows = 1
            For Each st As Discos In st1
                table.AddCell(New iTextSharp.text.Phrase(st.size, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.FreeSpace, fontTable))
            Next




            doc.Add(table)

        Catch ex As Exception

        End Try


    End Sub
    Public Sub tablepdf(ByVal doc As iTextSharp.text.Document, ByVal title As String, ByVal st1 As List(Of Licença), ByVal colcount As Integer)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim table As New PdfPTable(colcount)
            table.SpacingBefore = 45.0F
            table.TotalWidth = 300
            table.DefaultCell.Phrase = New iTextSharp.text.Phrase()
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(title, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            table.AddCell(New iTextSharp.text.Phrase("Tipo", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Chave", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("produto ID", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Versão", fontTable))
            table.HeaderRows = 1
            For Each st As Licença In st1
                table.AddCell(New iTextSharp.text.Phrase(st.Tipo, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Key, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Produto, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Versão, fontTable))
            Next




            doc.Add(table)

        Catch ex As Exception

        End Try


    End Sub
    Public Sub tablepdf(ByVal doc As iTextSharp.text.Document, ByVal title As String, ByVal st1 As List(Of Impressoras), ByVal colcount As Integer)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim table As New PdfPTable(colcount)
            table.SpacingBefore = 45.0F
            table.TotalWidth = 300
            table.DefaultCell.Phrase = New iTextSharp.text.Phrase()
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(title, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            table.AddCell(New iTextSharp.text.Phrase("Nome", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Servidor", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Driver", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Localização", fontTable))
            table.HeaderRows = 1
            For Each st As Impressoras In st1
                table.AddCell(New iTextSharp.text.Phrase(st.Name, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Server, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Driver, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Location, fontTable))
            Next




            doc.Add(table)

        Catch ex As Exception

        End Try


    End Sub

    Public Sub tablepdf(ByVal doc As iTextSharp.text.Document, ByVal title As String, ByVal st1 As List(Of Drivers), ByVal colcount As Integer)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim table As New PdfPTable(colcount)
            table.SpacingBefore = 45.0F
            table.TotalWidth = 300
            table.DefaultCell.Phrase = New iTextSharp.text.Phrase()
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(title, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            table.AddCell(New iTextSharp.text.Phrase("Nome", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Localização", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("ID", fontTable))
            table.HeaderRows = 1
            For Each st As Drivers In st1
                table.AddCell(New iTextSharp.text.Phrase(st.Name, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Location, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.ID, fontTable))
            Next




            doc.Add(table)

        Catch ex As Exception

        End Try


    End Sub
    Public Sub tablepdf(ByVal doc As iTextSharp.text.Document, ByVal title As String, ByVal st1 As List(Of Processos), ByVal colcount As Integer)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim table As New PdfPTable(colcount)
            table.SpacingBefore = 45.0F
            table.TotalWidth = 300
            table.DefaultCell.Phrase = New iTextSharp.text.Phrase()
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(title, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            table.AddCell(New iTextSharp.text.Phrase("Nome", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Descrição", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Titulo", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Nome do Computador", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Usuário", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Id do Processo", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Id da sessão", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Prioridade", fontTable))
            table.HeaderRows = 1
            For Each st As Processos In st1
                table.AddCell(New iTextSharp.text.Phrase(st.Name, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Description, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Caption, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.ComputerName, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.User, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.ProcessID, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.SessionID, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Priority, fontTable))
            Next




            doc.Add(table)

        Catch ex As Exception

        End Try


    End Sub
    Public Sub tablepdf(ByVal doc As iTextSharp.text.Document, ByVal title As String, ByVal st1 As List(Of Processador), ByVal colcount As Integer)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim table As New PdfPTable(colcount)
            table.SpacingBefore = 45.0F
            table.TotalWidth = 300
            table.DefaultCell.Phrase = New iTextSharp.text.Phrase()
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(title, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            table.AddCell(New iTextSharp.text.Phrase("Nome", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Tipo", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Descrição", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Fabricante", fontTable))
            table.HeaderRows = 1
            For Each st As Processador In st1
                table.AddCell(New iTextSharp.text.Phrase(st.Name, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.TipodeProcessador, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Descrição, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Fabricante, fontTable))
            Next




            doc.Add(table)

        Catch ex As Exception

        End Try


    End Sub

    Public Sub tablepdf(ByVal doc As iTextSharp.text.Document, ByVal title As String, ByVal st1 As List(Of Software), ByVal colcount As Integer)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim table As New PdfPTable(colcount)
            table.SpacingBefore = 45.0F
            table.TotalWidth = 300
            table.DefaultCell.Phrase = New iTextSharp.text.Phrase()
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(title, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            table.AddCell(New iTextSharp.text.Phrase("Nome", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Data de Instalação", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Pacote", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Versão", fontTable))
            table.HeaderRows = 1
            For Each st As Software In st1
                table.AddCell(New iTextSharp.text.Phrase(st.Name, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.InstallDate, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Pacote, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Versão, fontTable))
            Next




            doc.Add(table)

        Catch ex As Exception

        End Try


    End Sub
    Public Sub ReportPrinter(ByVal x1 As Impressoras, ByVal doc As iTextSharp.text.Document, ByVal selectinfos As List(Of String))
        Try


            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            Dim prh1 As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(x1.nome, fontTable3)
            prh1.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh1)
            For Each selectinfo As String In selectinfos
                If selectinfo.ToLower.Contains("Informações das impressoras".ToLower) Then
                    doc.Add(New iTextSharp.text.Paragraph("Comentários:" & x1.Comentarios))
                    doc.Add(New iTextSharp.text.Paragraph("Descrição:" & x1.Descrição))
                    doc.Add(New iTextSharp.text.Paragraph("Driver:" & x1.Driver))
                    doc.Add(New iTextSharp.text.Paragraph("ID:" & x1.ID))
                    doc.Add(New iTextSharp.text.Paragraph("Localização:" & x1.localização))
                    doc.Add(New iTextSharp.text.Paragraph("Nome Compartilhado:" & x1.NomeCompartilhado))
                    doc.Add(New iTextSharp.text.Paragraph("Nome do  Servidor:" & x1.NomedoServidor))
                    doc.Add(New iTextSharp.text.Paragraph("Porta:" & x1.Porta))
                    doc.Add(New iTextSharp.text.Paragraph("Páginas Contadas:" & x1.PáginasContadas))
                    doc.Add(New iTextSharp.text.Paragraph("Toner Reservas disponiveis:" & x1.QuantidadedeTonerDisponivel))
                    doc.Add(New iTextSharp.text.Paragraph("Selb:" & x1.Selb))
                    doc.Add(New iTextSharp.text.Paragraph("Status:" & x1.Status))
                    doc.Add(New iTextSharp.text.Paragraph("System Name:" & x1.SystemName))
                    doc.Add(New iTextSharp.text.Paragraph("Tipo de Toner:" & x1.Toner))
                    doc.Add(New iTextSharp.text.Paragraph("Toner Restante:" & x1.TonerRestante))
                    doc.Add(New iTextSharp.text.Paragraph("Ultima Troca de Toner:" & x1.UltimaTrocadeToner))
                    doc.Add(New iTextSharp.text.Paragraph("Ultima Coleta feita:" & x1.UltimaVezColletada))
                Else
                    If selectinfo.ToLower.Contains("selb".ToLower) Then
                        doc.Add(New iTextSharp.text.Paragraph("Selb:" & x1.Selb))
                    End If
                    If selectinfo.ToLower.Contains("ID".ToLower) Then
                        doc.Add(New iTextSharp.text.Paragraph("ID:" & x1.ID))
                    End If
                    If selectinfo.ToLower.Contains("Toner".ToLower) Then
                        doc.Add(New iTextSharp.text.Paragraph("Tipo de Toner:" & x1.Toner))
                        doc.Add(New iTextSharp.text.Paragraph("Toner Restante:" & x1.TonerRestante))
                    End If
                    If selectinfo.ToLower.Contains("Status".ToLower) Then
                        doc.Add(New iTextSharp.text.Paragraph("Status:" & x1.Status))
                    End If
                    If selectinfo.ToLower = ("Driver".ToLower) Then
                        doc.Add(New iTextSharp.text.Paragraph("Driver:" & x1.Driver))
                    End If
                    If selectinfo.ToLower.Contains("Páginas".ToLower) Then
                        doc.Add(New iTextSharp.text.Paragraph("Páginas Contadas:" & x1.PáginasContadas))
                    End If

                End If
            Next

        Catch ex As Exception

        End Try
    End Sub
    Public Sub ReportPrinter(ByVal x1 As Impressoras, ByVal doc As iTextSharp.text.Document)
        Try


            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            Dim prh1 As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(x1.nome, fontTable3)
            prh1.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh1)
            doc.Add(New iTextSharp.text.Paragraph("Comentários:" & x1.Comentarios))
            doc.Add(New iTextSharp.text.Paragraph("Descrição:" & x1.Descrição))
            doc.Add(New iTextSharp.text.Paragraph("Driver:" & x1.Driver))
            doc.Add(New iTextSharp.text.Paragraph("ID:" & x1.ID))
            doc.Add(New iTextSharp.text.Paragraph("Localização:" & x1.localização))
            doc.Add(New iTextSharp.text.Paragraph("Nome Compartilhado:" & x1.NomeCompartilhado))
            doc.Add(New iTextSharp.text.Paragraph("Nome do  Servidor:" & x1.NomedoServidor))
            doc.Add(New iTextSharp.text.Paragraph("Porta:" & x1.Porta))
            doc.Add(New iTextSharp.text.Paragraph("Páginas Contadas:" & x1.PáginasContadas))
            doc.Add(New iTextSharp.text.Paragraph("Toner Reservas disponiveis:" & x1.QuantidadedeTonerDisponivel))
            doc.Add(New iTextSharp.text.Paragraph("Selb:" & x1.Selb))
            doc.Add(New iTextSharp.text.Paragraph("Status:" & x1.Status))
            doc.Add(New iTextSharp.text.Paragraph("System Name:" & x1.SystemName))
            doc.Add(New iTextSharp.text.Paragraph("Tipo de Toner:" & x1.Toner))
            doc.Add(New iTextSharp.text.Paragraph("Toner Restante:" & x1.TonerRestante))
            doc.Add(New iTextSharp.text.Paragraph("Ultima Troca de Toner:" & x1.UltimaTrocadeToner))
            doc.Add(New iTextSharp.text.Paragraph("Ultima Coleta feita:" & x1.UltimaVezColletada))

        Catch ex As Exception

        End Try
    End Sub
    Public Sub ReportLicense(ByVal x1 As Licença, ByVal doc As iTextSharp.text.Document)
        Try


            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            Dim prh1 As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(x1.Descrição, fontTable3)
            prh1.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh1)
            ReportLicenses3(doc, x1)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ReportPats(ByVal x1 As Patrimonio, ByVal doc As iTextSharp.text.Document)
        Try


            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            Dim prh1 As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(x1.Patrimonio, fontTable3)
            prh1.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh1)
            doc.Add(New iTextSharp.text.Paragraph("Sistema Operacional:" & x1.Windows, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Descrição:" & x1.Descrição, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Nota Fiscal:" & x1.NotaFiscal, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Usuário Atual:" & x1.UsuárioAtual, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Nome do Computador Atual:" & x1.NomeDoComputadorAtual, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Atualizado para windows 10:" & x1.AtualizadoParaWindows10.ToString, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Recovery Embutido:" & x1.RecoveryEmbutido.ToString, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Etiqueta com número de série:" & x1.EtiquetaComNumerodeserie, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Adicionado em:" & x1.Adicionadoem, fontTable3))
            ReportLicenses(doc, "Licenças do patrimônio:" & Space(2) & x1.Patrimonio, x1.Licenças, 6)


        Catch ex As Exception

        End Try
    End Sub
    Public Sub ReportComputer(ByVal pc As Servidor, ByVal doc As iTextSharp.text.Document, ByVal selectinfos As List(Of String))
        Try


            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            If pc.Ultimovezscaneada <> "" Then
                Dim prh1 As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(pc.ComputerName, fontTable3)
                prh1.Alignment = iTextSharp.text.Element.ALIGN_CENTER
                doc.Add(prh1)
                For Each selectinfo As String In selectinfos
                    If selectinfo.ToLower.Contains("Informações do computador".ToLower) Then
                        doc.Add(New iTextSharp.text.Paragraph("Sistema Operacional:" & pc.OperatingSystem, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Usuário Logado:" & pc.CurrentUser, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Ip:" & pc.IPAddress, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Modelo:" & pc.Model, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Memória Disponivel:" & pc.TotalVisibleMemorySize, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Número de fabricação:" & pc.BuildNumber, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Arquitetura:" & pc.OSArchitecture, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Fabricante:" & pc.Manufacturer, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Versão:" & pc.Version, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Status:" & pc.Status, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Diretório do Windows:" & pc.WindowsDirectory, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Número serial:" & pc.SerialNumber, fontTable3))
                        Dim dns2 As String = ""
                        For Each dns1 As String In pc.Dns
                            dns2 = dns1 & ";"
                        Next
                        doc.Add(New iTextSharp.text.Paragraph("Dns:" & dns2, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Gateway:" & pc.DefaultIPGateway, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("MacAdress:" & pc.MACAddress, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Data da ultima instalação:" & pc.InstallDate, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Horário Local:" & pc.LocalDateTime, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Ultimo Reinicio:" & pc.LastBootUpTime, fontTable3))
                        doc.Add(New iTextSharp.text.Paragraph("Scaneado pela última vez na data:" & pc.Ultimovezscaneada, fontTable3))
                        If pc.discos.Count > 0 Then
                            tablepdf(doc, "Discos", pc.discos, 2)
                        End If
                        If pc.Licenças.Count > 0 Then
                            tablepdf(doc, "Licenças", pc.Licenças, 4)
                        End If
                        If pc.Processadores.Count > 0 Then
                            tablepdf(doc, "Processadores", pc.Processadores, 4)
                        End If
                        If pc.Softwares.Count > 0 Then
                            tablepdf(doc, "Softwares", pc.Softwares, 4)
                        End If
                        If pc.Impressoras.Count > 0 Then
                            tablepdf(doc, "Impressoras", pc.Impressoras, 4)
                        End If
                        If pc.drivers.Count > 0 Then
                            tablepdf(doc, "Drivers", pc.drivers, 3)
                        End If
                        If pc.UsersLocal.Count > 0 Then
                            tablepdf(doc, "Usuários Locais", pc.UsersLocal, 4)
                        End If
                        If pc.Processos.Count > 0 Then
                            tablepdf(doc, "Processos", pc.Processos, 8)
                        End If
                    Else
                        If selectinfo.ToLower.Contains("Gerais".ToLower) Then
                            If pc.Ultimovezscaneada <> "" Then
                                doc.Add(New iTextSharp.text.Paragraph("Sistema Operacional:" & pc.OperatingSystem, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Usuário Logado:" & pc.CurrentUser, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Ip:" & pc.IPAddress, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Modelo:" & pc.Model, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Memória Disponivel:" & pc.TotalVisibleMemorySize, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Número de fabricação:" & pc.BuildNumber, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Arquitetura:" & pc.OSArchitecture, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Fabricante:" & pc.Manufacturer, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Versão:" & pc.Version, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Status:" & pc.Status, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Diretório do Windows:" & pc.WindowsDirectory, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Número serial:" & pc.SerialNumber, fontTable3))
                                Dim dns2 As String = ""
                                For Each dns1 As String In pc.Dns
                                    dns2 = dns1 & ";"
                                Next
                                doc.Add(New iTextSharp.text.Paragraph("Dns:" & dns2, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Gateway:" & pc.DefaultIPGateway, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("MacAdress:" & pc.MACAddress, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Data da ultima instalação:" & pc.InstallDate, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Horário Local:" & pc.LocalDateTime, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Ultimo Reinicio:" & pc.LastBootUpTime, fontTable3))
                                doc.Add(New iTextSharp.text.Paragraph("Scaneado pela última vez na data:" & pc.Ultimovezscaneada, fontTable3))
                            End If
                        End If
                        If selectinfo.ToLower.Contains("Processador".ToLower) Then
                            If pc.Processadores.Count > 0 Then
                                tablepdf(doc, "Processadores", pc.Processadores, 4)
                            End If
                        End If
                        If selectinfo.ToLower.Contains("Processos".ToLower) Then
                            If pc.Processos.Count > 0 Then
                                tablepdf(doc, "Processos", pc.Processos, 8)
                            End If
                        End If
                        If selectinfo.ToLower = ("Drivers".ToLower) Then
                            If pc.drivers.Count > 0 Then
                                tablepdf(doc, "Drivers", pc.drivers, 3)
                            End If
                        End If
                        If selectinfo.ToLower.Contains("Impressoras".ToLower) Then
                            If pc.Impressoras.Count > 0 Then
                                tablepdf(doc, "Impressoras", pc.Impressoras, 4)
                            End If
                        End If
                        If selectinfo.ToLower.Contains("Licenças".ToLower) Then
                            If pc.Licenças.Count > 0 Then
                                tablepdf(doc, "Licenças", pc.Licenças, 4)
                            End If
                        End If
                        If selectinfo.ToLower.Contains("Softwares".ToLower) Then
                            If pc.Softwares.Count > 0 Then
                                tablepdf(doc, "Softwares", pc.Softwares, 4)
                            End If
                        End If
                        If selectinfo.ToLower.Contains("Discos".ToLower) Then
                            If pc.discos.Count > 0 Then
                                tablepdf(doc, "Discos", pc.discos, 2)
                            End If
                        End If
                        If selectinfo.ToLower.Contains("Locais".ToLower) Then
                            If pc.UsersLocal.Count > 0 Then
                                tablepdf(doc, "Usuários Locais", pc.UsersLocal, 4)
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ReportComputer(ByVal pc As Servidor, ByVal doc As iTextSharp.text.Document)
        Try


            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            If pc.Ultimovezscaneada <> "" Then
                Dim prh1 As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(pc.ComputerName, fontTable3)
                prh1.Alignment = iTextSharp.text.Element.ALIGN_CENTER
                doc.Add(prh1)
                doc.Add(New iTextSharp.text.Paragraph("Sistema Operacional:" & pc.OperatingSystem, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Usuário Logado:" & pc.CurrentUser, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Ip:" & pc.IPAddress, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Modelo:" & pc.Model, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Memória Disponivel:" & pc.TotalVisibleMemorySize, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Número de fabricação:" & pc.BuildNumber, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Arquitetura:" & pc.OSArchitecture, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Fabricante:" & pc.Manufacturer, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Versão:" & pc.Version, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Status:" & pc.Status, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Diretório do Windows:" & pc.WindowsDirectory, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Número serial:" & pc.SerialNumber, fontTable3))
                Dim dns2 As String = ""
                For Each dns1 As String In pc.Dns
                    dns2 = dns1 & ";"
                Next
                doc.Add(New iTextSharp.text.Paragraph("Dns:" & dns2, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Gateway:" & pc.DefaultIPGateway, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("MacAdress:" & pc.MACAddress, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Data da ultima instalação:" & pc.InstallDate, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Horário Local:" & pc.LocalDateTime, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Ultimo Reinicio:" & pc.LastBootUpTime, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Scaneado pela última vez na data:" & pc.Ultimovezscaneada, fontTable3))
                If pc.discos.Count > 0 Then
                    tablepdf(doc, "Discos", pc.discos, 2)
                End If
                If pc.Licenças.Count > 0 Then
                    tablepdf(doc, "Licenças", pc.Licenças, 4)
                End If
                If pc.Processadores.Count > 0 Then
                    tablepdf(doc, "Processadores", pc.Processadores, 4)
                End If
                If pc.Softwares.Count > 0 Then
                    tablepdf(doc, "Softwares", pc.Softwares, 4)
                End If
                If pc.Impressoras.Count > 0 Then
                    tablepdf(doc, "Impressoras", pc.Impressoras, 4)
                End If
                If pc.drivers.Count > 0 Then
                    tablepdf(doc, "Drivers", pc.drivers, 3)
                End If
                If pc.UsersLocal.Count > 0 Then
                    tablepdf(doc, "Usuários Locais", pc.UsersLocal, 4)
                End If
                If pc.Processos.Count > 0 Then
                    tablepdf(doc, "Processos", pc.Processos, 8)
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton40_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton40.Click
        Try
            Database.Avisos.Clear()
            save()
            checkerros()
            checkerrosexistentes()
            Avisoseerros.Rows.Clear()
            If Database.Avisos Is Nothing Then
                Database.Avisos = New List(Of AvisoseErrors)
                save()
            End If
            AvisosProgressBar.Value = 0
            If Database.Avisos.Count > 0 Then
                AvisosProgressBar.Maximum = Database.Avisos.Count
                For Each x11 As AvisoseErrors In Database.Avisos
                    AvisosProgressBar.Value = AvisosProgressBar.Value + 1
                    Avisoseerros.Rows.Add({x11.Erro, x11.PrinterName, x11.Selb, x11.Porta, x11.TonerDisponievis, x11.Data})
                Next
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub checkerrosexistentes()
        Try
            If Database IsNot Nothing Then
                If Database.Impressoras IsNot Nothing Then
                    For Each x1 As AvisoseErrors In Database.Avisos
                        For Each x2 As Impressoras In Database.Impressoras
                            If x2.nome.ToLower = x1.PrinterName.ToLower Then
                                If x1.TonerDisponievis = 0 And x2.QuantidadedeTonerDisponivel > 0 And x1.Erro.ToLower.Contains("Sem toner Reserva".ToLower) Then
                                    Database.Avisos.Remove(x1)
                                    save()
                                End If
                                If ping(GetIp(x1.Porta)) Then
                                    If x1.Erro.ToLower.Contains("inacessivel".ToLower) Then
                                        Database.Avisos.Remove(x1)
                                        save()
                                    End If
                                    If x1.Erro.ToLower.Contains("Pouca quantidade de toner disponivel:".ToLower) Then
                                        If x2.Driver.ToLower.Contains("Samsung".ToLower) And x2.Driver.ToLower.Contains("Hp".ToLower) = False Then
                                            Dim tonerresto As String = GetTonerLevel(GetIp(x2.Porta))
                                            If tonerresto.ToLower.Contains("de:".ToLower) Then
                                                Dim total As String = After(tonerresto, "de:")
                                                Dim current As String = Before(tonerresto, "de:")
                                                If (current / total).ToString("0.00%") > "30%" Then
                                                    Database.Avisos.Remove(x1)
                                                    save()
                                                End If
                                            End If
                                        Else
                                            Dim tonerresto As String = GetTonerLevel(GetIp(x2.Porta))
                                            If tonerresto.ToLower.Contains("Info".ToLower) = False And tonerresto <> "" Then
                                                If tonerresto > 30 Then
                                                    Database.Avisos.Remove(x1)
                                                    save()
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    Next
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LimparListaToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles LimparListaToolStripMenuItem2.Click
        Try
            Avisoseerros.Rows.Clear()
            Database.Avisos.Clear()
            save()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub checkerros()
        Try
            If Database IsNot Nothing Then
                If Database.Impressoras IsNot Nothing Then
                    For Each x1 As Impressoras In Database.Impressoras
                        If x1.QuantidadedeTonerDisponivel = 0 Then
                            adderro(x1.nome, x1.Selb, x1.Porta, "Sem Toner Reserva", x1.QuantidadedeTonerDisponivel, DateAndTime.Now.ToString)
                        End If
                        If ping(GetIp(x1.Porta)) Then

                            If x1.Driver.ToLower.Contains("Samsung".ToLower) And x1.Driver.ToLower.Contains("Hp".ToLower) = False Then
                                If x1.TonerRestante.ToLower.Contains("de:".ToLower) Then
                                    Dim total As String = After(x1.TonerRestante, "de:")
                                    Dim current As String = Before(x1.TonerRestante, "de:")
                                    If (current / total).ToString("0.00%") < "30%" Then
                                        adderro(x1.nome, x1.Selb, x1.Porta, "Pouca quantidade de toner disponivel:" & (current / total).ToString("0.00%") & "de:" & "100%" & ",Recomendado o pedido de  toner", x1.QuantidadedeTonerDisponivel, DateAndTime.Now.ToString)
                                    End If
                                End If
                            Else
                                If x1.TonerRestante.ToLower.Contains("Info".ToLower) = False And x1.TonerRestante <> "" Then
                                    If x1.TonerRestante < 30 Then
                                        adderro(x1.nome, x1.Selb, x1.Porta, "Pouca quantidade de toner disponivel:" & x1.TonerRestante & "de 100,Recomendado o pedido de toner", x1.QuantidadedeTonerDisponivel, DateAndTime.Now.ToString)
                                    End If
                                End If
                            End If
                        Else
                            adderro(x1.nome, x1.Selb, x1.Porta, "Impressora Inacessivel", x1.QuantidadedeTonerDisponivel, DateAndTime.Now.ToString)
                        End If

                    Next
                End If
            End If
            For Each st As Licença In Database.Licenças
                If st.PatrimonioInstalados.Count > st.Quantidade Then
                    Dim newerror As New AvisoseErrors
                    newerror.Erro = "* Aviso:Foram encontrados mais licenças em uso do que a quantidade permitida da licença:" & Space(1) & st.Chave & Space(1) & "do programa:" & Space(1) & st.Descrição
                    newerror.Data = DateAndTime.Now.ToString
                    Database.Avisos.Add(newerror)
                    save()
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Sub adderro(ByVal PrinterName As String, ByVal Selb As String, ByVal Porta As String, ByVal erro As String, ByVal tonerDisponiveis As Integer, ByVal datadoerro As String)
        Try
            If Database IsNot Nothing Then
                If Database.Avisos IsNot Nothing Then
                    If alreadyexistsaviso(PrinterName, erro) = False Then
                        Dim novoaviso As New AvisoseErrors
                        novoaviso.PrinterName = PrinterName
                        novoaviso.Selb = Selb
                        novoaviso.Porta = Porta
                        novoaviso.TonerDisponievis = tonerDisponiveis
                        novoaviso.Erro = erro
                        novoaviso.Data = datadoerro
                        Database.Avisos.Add(novoaviso)
                        save()
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function alreadyexistsaviso(ByVal printername As String, ByVal erro As String)
        Dim have As Boolean = False
        Try
            If Database IsNot Nothing Then
                If Database.Avisos IsNot Nothing Then
                    For Each x1 As AvisoseErrors In Database.Avisos
                        If x1.PrinterName.ToLower = printername.ToLower And x1.Erro.ToLower = erro.ToLower Then
                            have = True
                            Return have
                            Exit For
                        End If
                    Next
                End If
            End If
            Return have
        Catch ex As Exception
            Return have
        End Try
        Return have
    End Function

    Public Function sendmessage(ByVal html As String) As Boolean
        Dim send As Boolean = False
        Try
            Dim user As String = Database.Emailadmin
            Dim pass As String = Database.Senhaadmin
            Dim server As String = Database.servidorEmail
            Dim service As New Global.Microsoft.Exchange.WebServices.Data.ExchangeService
            service.Credentials = New WebCredentials(user, pass, Environment.UserDomainName)
            service.Url = New Uri("https://" & server & "/ews/Exchange.asmx")
            Try
                Dim message As New EmailMessage(service)
                message.Subject = "Relatório Diário"
                message.Body = New MessageBody(BodyType.HTML, html)
                For Each word In Database.Destinatarios
                    If word.Contains("@") Then
                        message.ToRecipients.Add(word)
                    End If
                Next
                message.Send()
                send = True
                Return send
            Catch ex As ServiceRequestException

                Return send

            Catch ex As WebException

                Return send
            End Try
            Return send
        Catch ex As Exception

            Return send
        End Try
        Return send
    End Function










    Private Sub MetroDefaultSetButton47_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton47.Click
        Try
            Try
                If OpenFileDialog1.ShowDialog <> DialogResult.Cancel Then
                    NotaFiscal.Text = OpenFileDialog1.FileName
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton46_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton46.Click
        Try

            If IO.File.Exists(NotaFiscal.Text) Then
                Process.Start(NotaFiscal.Text)

            Else
                MetroFramework.MetroMessageBox.Show(Me, "Não foi possivel abrir nota,arquivo não encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton48_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton48.Click
        Try
            MetroGrid1.Rows.Remove(MetroGrid1.SelectedRows(0))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton49_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton49.Click
        Try
            If IO.File.Exists(MetroGrid1.SelectedRows(0).Cells(2).Value) Then
                Process.Start(MetroGrid1.SelectedRows(0).Cells(2).Value)

            Else
                MetroFramework.MetroMessageBox.Show(Me, "Não foi possivel abrir nota,arquivo não encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton50_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton50.Click
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
            For Each x1 As Licença In Database.Licenças
                If x1.Chave = MetroGrid1.SelectedRows(0).Cells(1).Value Then
                    For Each lc2 As Licença In Database.Licenças
                        EditLicense.MetroComboBox1.Items.Add(lc2.Descrição & "-" & lc2.Chave)
                    Next
                    EditLicense.MetroComboBox1.Text = x1.Descrição
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton52_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton52.Click
        Try
            EditLicense.Show()
            For Each lc2 As Licença In Database.Licenças
                EditLicense.MetroComboBox1.Items.Add(lc2.Descrição & "-" & lc2.Chave)
            Next
            EditLicense.Descricao.Enabled = True
            EditLicense.MetroGrid1.Rows.Add(Patrimonio.Text)
            EditLicense.SelectMetrogrid = MetroGrid1
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton51_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton51.Click
        Try
            For Each x1 As Patrimonio In Database.Patrimonios
                If x1.Patrimonio = Patrimonio.Text Then
                    x1.Patrimonio = Patrimonio.Text
                    x1.Windows = Windows.Text
                    x1.Descrição = Descricao.Text
                    x1.UsuárioAtual = UsuarioAtual.Text
                    x1.NotaFiscal = NotaFiscal.Text
                    x1.NomeDoComputadorAtual = NomeDoComputadorAtual.Text
                    x1.RecoveryEmbutido = RecoveryEmbutido.Checked
                    x1.AtualizadoParaWindows10 = AtualizadoParaWindowsDez.Checked
                    x1.EtiquetaComNumerodeserie = EtiquetaComNumerodesrie.Checked
                    x1.Adicionadoem = DateAndTime.Now.ToString
                    x1.Licenças.Clear()
                    For Each x20 As DataGridViewRow In MetroGrid1.Rows
                        If x20.Cells(0).Value <> "" Then
                            Dim newl1 As New Licença
                            newl1.Descrição = x20.Cells(0).Value
                            newl1.Quantidade = 1
                            newl1.Chave = x20.Cells(1).Value
                            newl1.Adicionadoem = x20.Cells(4).Value
                            newl1.EmUso = True
                            Dim nf As String = ""
                            If x20.Cells(2).Value <> "" Then
                                nf = IO.Path.GetFullPath(x20.Cells(2).Value)
                            End If
                            newl1.NotaFiscal = nf
                            If x20.Cells(3).Value = "True" Then
                                newl1.MidiaFisica = True
                            End If
                            If newl1.NotaFiscal <> "" Then
                                Dim evidenciacaminho1 = newl1.NotaFiscal
                                If IO.File.Exists(My.Settings.DatabaseLocation & "\" & Windows.Text & "\" & Patrimonio.Text & "\" & IO.Path.GetFileName(evidenciacaminho1)) = False Then
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
                                End If
                            End If
                            x1.Licenças.Add(newl1)
                            newl1.PatrimonioInstalados.Add(x1)
                        End If
                    Next
                    Dim evidenciacaminho = NotaFiscal.Text
                    If IO.File.Exists(My.Settings.DatabaseLocation & "\" & Windows.Text & "\" & Patrimonio.Text & "\" & IO.Path.GetFileName(evidenciacaminho)) = False Then

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
                                x1.NotaFiscal = IO.Path.GetFullPath(newFullPath)
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
                                x1.NotaFiscal = IO.Path.GetFullPath(newFullPath)
                            End If
                        End If
                    End If

                    save()
                    MetroFramework.MetroMessageBox.Show(Me, "Patrimônio  ALterado com sucesso", "Concluido", MessageBoxButtons.OK, MessageBoxIcon.Information)


                End If
            Next
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
            For Each x1 As Licença In Database.Licenças
                If x1.Chave = MetroGrid1.SelectedRows(0).Cells(1).Value Then
                    For Each lc2 As Licença In Database.Licenças
                        EditLicense.MetroComboBox1.Items.Add(lc2.Descrição & "-" & lc2.Chave)
                    Next
                    EditLicense.MetroComboBox1.Text = x1.Descrição
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton56_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton56.Click
        Try
            MetroGrid11.Rows.Remove(MetroGrid11.SelectedRows(0))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton58_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton58.Click
        Try
            MetroGrid1.Rows.Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton57_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton57.Click
        Try
            SearchPat.Show()
            SearchPat.Selectgrid = MetroGrid11
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton54_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton54.Click
        Try
            If OpenFileDialog1.ShowDialog <> DialogResult.Cancel Then
                NotaFiscal1.Text = OpenFileDialog1.FileName
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton55_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton55.Click
        Try

            If IO.File.Exists(NotaFiscal1.Text) Then
                Process.Start(NotaFiscal1.Text)

            Else
                MetroFramework.MetroMessageBox.Show(Me, "Não foi possivel abrir nota,arquivo não encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton45_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton45.Click
        Try
            Dim find As Boolean = False
            For Each x1 As Licença In Database.Licenças
                If x1.Chave.ToLower.Contains(LicenseSearch.Text.ToLower) Or x1.NotaFiscal.ToLower.Contains(LicenseSearch.Text.ToLower) Or x1.Descrição.ToLower.Contains(LicenseSearch.Text.ToLower) Then
                    If find = False Then
                        LicensetreeView.Nodes.Clear()
                    End If
                    find = True
                    LicensetreeView.Nodes.Add(x1.Descrição).Nodes.Add(x1.Chave)
                End If
            Next
            If find = False Then
                MetroFramework.MetroMessageBox.Show(Me, "Nenhum Resultado Achado na pesquisa,tente pesquisa por outra palavra ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception

        End Try
    End Sub


    Public Sub Reload()
        Try
            PatProgressBar.Value = 0
            PatProgressBar.Maximum = Database.Patrimonios.Count + Database.Licenças.Count
            PatTreeView.Nodes.Clear()
            LicensetreeView.Nodes.Clear()
            For Each x1 As Patrimonio In Database.Patrimonios

                Dim aleradyhave As Boolean = False
                Dim nodetoadd As New TreeNode
                For Each nd As TreeNode In PatTreeView.Nodes
                    If nd.Text.ToLower = x1.Windows.ToLower Then
                        nodetoadd = nd
                        aleradyhave = True
                        Exit For
                    End If
                Next
                If aleradyhave = False Then
                    nodetoadd = PatTreeView.Nodes.Add(x1.Windows)
                End If
                nodetoadd.Nodes.Add(x1.Patrimonio)
                PatProgressBar.Value = PatProgressBar.Value + 1
            Next
            For Each x2 As Licença In Database.Licenças

                Dim aleradyhave As Boolean = False
                Dim nodetoadd As New TreeNode
                For Each nd As TreeNode In LicensetreeView.Nodes
                    If nd.Text.ToLower = x2.Descrição.ToLower Then
                        nodetoadd = nd
                        aleradyhave = True
                        Exit For
                    End If
                Next
                If aleradyhave = False Then
                    nodetoadd = LicensetreeView.Nodes.Add(x2.Descrição)
                End If
                nodetoadd.Nodes.Add(x2.Chave)
                PatProgressBar.Value = PatProgressBar.Value + 1
            Next
            For Each x10 As TreeNode In PatTreeView.Nodes
                x10.Text = x10.Text & "(" & x10.Nodes.Count & ")"
            Next
            For Each x10 As TreeNode In LicensetreeView.Nodes
                x10.Text = x10.Text & "(" & x10.Nodes.Count & ")"
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton44_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton44.Click
        Try
            Reload()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton42_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton42.Click
        Try
            Dim find As Boolean = False
            For Each x1 As Patrimonio In Database.Patrimonios
                If x1.Patrimonio.ToLower.Contains(PatSearch.Text.ToLower) Or x1.NotaFiscal.ToLower.Contains(PatSearch.Text.ToLower) Or x1.NomeDoComputadorAtual.ToLower.Contains(PatSearch.Text.ToLower) Or x1.UsuárioAtual.ToLower.Contains(PatSearch.Text.ToLower) Or x1.Descrição.ToLower.Contains(PatSearch.Text.ToLower) Or x1.Windows.ToLower.Contains(PatSearch.Text.ToLower) Then
                    If find = False Then
                        PatTreeView.Nodes.Clear()
                    End If
                    find = True
                    PatTreeView.Nodes.Add(x1.Patrimonio)
                End If
            Next
            If find = False Then
                MetroFramework.MetroMessageBox.Show(Me, "Nenhum Resultado Achado na pesquisa,tente pesquisa por outra palavra ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton43_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton43.Click
        Try
            Reload()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PatTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles PatTreeView.AfterSelect
        Try
            For Each x1 As Patrimonio In Database.Patrimonios
                If x1.Patrimonio = PatTreeView.SelectedNode.Text Then
                    Adicionadoem.Text = "Patrimônio alterado em:" & Space(2) & x1.Adicionadoem
                    Patrimonio.Text = x1.Patrimonio
                    Windows.Text = x1.Windows
                    NomeDoComputadorAtual.Text = x1.NomeDoComputadorAtual
                    UsuarioAtual.Text = x1.UsuárioAtual
                    Descricao.Text = x1.Descrição
                    NotaFiscal.Text = x1.NotaFiscal
                    EtiquetaComNumerodesrie.Checked = x1.EtiquetaComNumerodeserie
                    RecoveryEmbutido.Checked = x1.RecoveryEmbutido
                    AtualizadoParaWindowsDez.Checked = x1.AtualizadoParaWindows10
                    MetroGrid1.Rows.Clear()
                    For Each x10 As Licença In x1.Licenças
                        If x10.Descrição <> "" Then
                            MetroGrid1.Rows.Add({x10.Descrição, x10.Chave, x10.NotaFiscal, x10.MidiaFisica.ToString, x10.Adicionadoem})
                        End If
                    Next
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub
    Private Sub LicenseTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles LicensetreeView.AfterSelect
        Try
            For Each x1 As Licença In Database.Licenças
                If e.Node.Parent.Text.Contains(x1.Descrição) And x1.Chave = e.Node.Text Then
                    Adicionadoem1.Text = "Licença alterada em:" & Space(2) & x1.Adicionadoem
                    descricao1.Text = x1.Descrição
                    NotaFiscal1.Text = x1.NotaFiscal
                    Chave.Text = x1.Chave
                    EmUso.Checked = x1.EmUso
                    MidiaFisica.Checked = x1.MidiaFisica
                    Qauntidade.Text = x1.Quantidade
                    MetroGrid11.Rows.Clear()
                    For Each x2 As Patrimonio In x1.PatrimonioInstalados
                        MetroGrid11.Rows.Add(x2.NomeDoComputadorAtual, x2.UsuárioAtual, x2.Windows, x2.Patrimonio)
                    Next
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton53_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton53.Click
        Try
            For Each x1 As Licença In Database.Licenças
                If x1.Descrição = descricao1.Text And x1.Chave = Chave.Text Then
                    x1.Descrição = descricao1.Text
                    x1.Chave = Chave.Text
                    x1.Adicionadoem = DateAndTime.Now.ToString
                    x1.EmUso = EmUso.Checked
                    x1.Quantidade = Qauntidade.Text
                    x1.MidiaFisica = MidiaFisica.Checked
                    x1.PatrimonioInstalados.Clear()
                    For Each x10 As DataGridViewRow In MetroGrid11.Rows
                        For Each x11 As Patrimonio In Database.Patrimonios
                            If x11.Patrimonio = x10.Cells(3).Value Then
                                x1.PatrimonioInstalados.Add(x11)
                            End If
                        Next
                    Next
                    Dim evidenciacaminho = NotaFiscal1.Text
                    If IO.File.Exists(My.Settings.DatabaseLocation & "\Licenças\" & descricao1.Text & "\" & IO.Path.GetFileName(evidenciacaminho)) = False Then
                        If IO.Directory.Exists(My.Settings.DatabaseLocation & "\Licenças\" & descricao1.Text) Then
                            Dim count As Integer = 0
                            Dim fileNameOnly As String = IO.Path.GetFileNameWithoutExtension(evidenciacaminho)
                            Dim extension As String = IO.Path.GetExtension(evidenciacaminho)
                            Dim path As String = My.Settings.DatabaseLocation & "\Licenças\" & descricao1.Text
                            Dim newFullPath As String = My.Settings.DatabaseLocation & "\Licenças\" & descricao1.Text & "\" & IO.Path.GetFileName(evidenciacaminho)
                            While IO.File.Exists(newFullPath)
                                Dim tempFileName As String = fileNameOnly & "(" & count + 1 & ")"
                                newFullPath = IO.Path.Combine(path, (tempFileName + extension))
                                count = count + 1
                            End While
                            If IO.File.Exists(evidenciacaminho) Then
                                IO.File.Copy(evidenciacaminho, newFullPath, True)
                                x1.NotaFiscal = IO.Path.GetFullPath(newFullPath)
                            End If
                        Else
                            IO.Directory.CreateDirectory(My.Settings.DatabaseLocation & "\Licenças\" & descricao1.Text)
                            Dim count As Integer = 0
                            Dim fileNameOnly As String = IO.Path.GetFileNameWithoutExtension(evidenciacaminho)
                            Dim extension As String = IO.Path.GetExtension(evidenciacaminho)
                            Dim path As String = My.Settings.DatabaseLocation & "\Licenças\" & descricao1.Text
                            Dim newFullPath As String = My.Settings.DatabaseLocation & "\Licenças\" & descricao1.Text & "\" & IO.Path.GetFileName(evidenciacaminho)
                            While IO.File.Exists(newFullPath)
                                Dim tempFileName As String = fileNameOnly & "(" & count + 1 & ")"
                                newFullPath = IO.Path.Combine(path, (tempFileName + extension))
                                count = count + 1
                            End While
                            If IO.File.Exists(evidenciacaminho) Then
                                IO.File.Copy(evidenciacaminho, newFullPath, True)
                                x1.NotaFiscal = IO.Path.GetFullPath(newFullPath)
                            End If
                        End If
                    End If
                    If MetroGrid11.Rows.Count > 0 Then
                        For Each x12 As DataGridViewRow In MetroGrid11.Rows
                            For Each x11 As Patrimonio In Database.Patrimonios
                                If x11.Patrimonio = x12.Cells(3).Value Then
                                    Dim alreadyhave As Boolean = False
                                    For Each lice As Licença In x11.Licenças
                                        If lice.Chave = Chave.Text Then
                                            alreadyhave = True
                                        End If
                                    Next
                                    If alreadyhave = False Then
                                        x11.Licenças.Add(x1)
                                    End If
                                End If
                            Next
                        Next
                    End If
                    save()
                    MetroFramework.MetroMessageBox.Show(Me, "Licença  Alterada com sucesso", "Concluido!!!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton11_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton11.Click
        Try
            AddPat.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton41_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton41.Click
        Try
            AddLicense.Show()
        Catch ex As Exception

        End Try
    End Sub



    Private Sub MetroDefaultSetButton59_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton59.Click
        Try
            notasfaltantes = 0
            If CheckedNames(PatTreeView1.Nodes).Count > 1 And ReportTudo.Checked = False Then
                MetroMessageBox.Show(Me, "Não pode ser assinalada mais que uma opção para o report ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            If CheckedNames(PatTreeView1.Nodes).Count = 1 Or ReportTudo.Checked Then
                SaveFileDialog1.Filter = "PDF files (*.pdf)|"
                If SaveFileDialog1.ShowDialog <> DialogResult.Cancel Then
                    If ReportTudo.Checked Then
                        savepdfall(IO.Path.GetFileNameWithoutExtension(SaveFileDialog1.FileName), IO.Path.GetDirectoryName(SaveFileDialog1.FileName))
                        Exit Sub
                    End If
                    For Each xstring As String In CheckedNames(PatTreeView1.Nodes)
                        If xstring.ToLower.Contains("Patri".ToLower) Then
                            savepdfPatrimonios(IO.Path.GetFileNameWithoutExtension(SaveFileDialog1.FileName), IO.Path.GetDirectoryName(SaveFileDialog1.FileName))
                        End If
                        If xstring.ToLower.Contains("Licen".ToLower) Then
                            savepdfLicenses(IO.Path.GetFileNameWithoutExtension(SaveFileDialog1.FileName), IO.Path.GetDirectoryName(SaveFileDialog1.FileName))
                        End If
                        If xstring.ToLower.Contains("Nota".ToLower) Then
                            saveNotaFiscal(IO.Path.GetFileNameWithoutExtension(SaveFileDialog1.FileName), IO.Path.GetDirectoryName(SaveFileDialog1.FileName))
                        End If
                        If xstring.ToLower.Contains("inexistente".ToLower) Then
                            saveNotaFiscalinexistente(IO.Path.GetFileNameWithoutExtension(SaveFileDialog1.FileName), IO.Path.GetDirectoryName(SaveFileDialog1.FileName))
                        End If
                    Next
                End If
            Else
                MetroMessageBox.Show(Me, "é preciso que seja assinalda pelo menos uma opção para fazer o report ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReportTudo_CheckedChanged(sender As Object) Handles ReportTudo.CheckedChanged
        Try
            For Each x1 As TreeNode In PatTreeView1.Nodes
                If x1.Checked = True Then
                    x1.Checked = False
                Else
                    x1.Checked = True
                End If
            Next
            If ReportTudo.Checked = True Then
                If PatTreeView1.Enabled = True Then
                    PatTreeView1.Enabled = False
                Else
                    PatTreeView1.Enabled = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub savepdf(ByVal namepdf As String, ByVal pc As Servidor, ByVal patch1 As String)
        Dim patch As String = patch1 & "\Computadores\" & namepdf & ".pdf"
        Try
            If IO.Directory.Exists(patch1 & "\Computadores\") = False Then
                IO.Directory.CreateDirectory(patch1 & "\Computadores\")
            End If
            Dim evidenciacaminho1 = patch
            If System.IO.File.Exists(patch) Then
                Dim count As Integer = 0
                Dim fileNameOnly As String = IO.Path.GetFileNameWithoutExtension(evidenciacaminho1)
                Dim extension As String = IO.Path.GetExtension(evidenciacaminho1)
                Dim path As String = IO.Path.GetDirectoryName(evidenciacaminho1)
                Dim newFullPath As String = evidenciacaminho1
                While IO.File.Exists(newFullPath)
                    Dim tempFileName As String = fileNameOnly & "(" & "(" & count + 1 & ")" & ")"
                    newFullPath = IO.Path.Combine(path, (tempFileName + extension))
                    count = count + 1
                End While
                If IO.File.Exists(evidenciacaminho1) Then
                    IO.File.Move(evidenciacaminho1, newFullPath)
                End If
            End If
            Dim doc = New iTextSharp.text.Document()
            PdfWriter.GetInstance(doc, New IO.FileStream(patch, IO.FileMode.Create))
            doc.Open()
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            If pc.Ultimovezscaneada <> "" Then
                Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(pc.ComputerName, fontTable2)
                prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
                doc.Add(prh)
                doc.Add(New iTextSharp.text.Paragraph("Sistema Operacional:" & pc.OperatingSystem, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Usuário Logado:" & pc.CurrentUser, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Ip:" & pc.IPAddress, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Modelo:" & pc.Model, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Memória Disponivel:" & pc.TotalVisibleMemorySize, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Número de fabricação:" & pc.BuildNumber, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Arquitetura:" & pc.OSArchitecture, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Fabricante:" & pc.Manufacturer, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Versão:" & pc.Version, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Status:" & pc.Status, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Diretório do Windows:" & pc.WindowsDirectory, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Número serial:" & pc.SerialNumber, fontTable3))
                Dim dns2 As String = ""
                For Each dns1 As String In pc.Dns
                    dns2 = dns1 & ";"
                Next
                doc.Add(New iTextSharp.text.Paragraph("Dns:" & dns2, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Gateway:" & pc.DefaultIPGateway, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("MacAdress:" & pc.MACAddress, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Data da ultima instalação:" & pc.InstallDate, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Horário Local:" & pc.LocalDateTime, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Ultimo Reinicio:" & pc.LastBootUpTime, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Scaneado pela última vez na data:" & pc.Ultimovezscaneada, fontTable3))
            End If
            If pc.discos.Count > 0 Then
                tablepdf(doc, "Discos", pc.discos, 2)
            End If
            If pc.Licenças.Count > 0 Then
                tablepdf(doc, "Licenças", pc.Licenças, 4)
            End If
            If pc.Processadores.Count > 0 Then
                tablepdf(doc, "Processadores", pc.Processadores, 4)
            End If
            If pc.Softwares.Count > 0 Then
                tablepdf(doc, "Softwares", pc.Softwares, 4)
            End If
            If pc.Impressoras.Count > 0 Then
                tablepdf(doc, "Impressoras", pc.Impressoras, 4)
            End If
            If pc.drivers.Count > 0 Then
                tablepdf(doc, "Drivers", pc.drivers, 3)
            End If
            If pc.UsersLocal.Count > 0 Then
                tablepdf(doc, "Usuários Locais", pc.UsersLocal, 4)
            End If
            If pc.Processos.Count > 0 Then
                tablepdf(doc, "Processos", pc.Processos, 8)
            End If
            doc.Close()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub savepdf(ByVal namepdf As String, ByVal pc As Impressoras, ByVal patch1 As String)
        While namepdf.Contains(".")
            namepdf = namepdf.Replace(".", ",")
        End While
        If namepdf.Contains("\\") Then
            namepdf = namepdf.Replace("\\", "")
        End If
        While namepdf.Contains("\")
            namepdf = namepdf.Replace("\", ",")
        End While
        Dim patch As String = patch1 & "\Impressoras\" & namepdf & ".pdf"
        Try
            If IO.Directory.Exists(patch1 & "\Impressoras\") = False Then
                IO.Directory.CreateDirectory(patch1 & "\Impressoras\")
            End If
            Dim evidenciacaminho1 = patch
            If System.IO.File.Exists(patch) Then
                Dim count As Integer = 0
                Dim fileNameOnly As String = IO.Path.GetFileNameWithoutExtension(evidenciacaminho1)
                Dim extension As String = IO.Path.GetExtension(evidenciacaminho1)
                Dim path As String = IO.Path.GetDirectoryName(evidenciacaminho1)
                Dim newFullPath As String = evidenciacaminho1
                While IO.File.Exists(newFullPath)
                    Dim tempFileName As String = fileNameOnly & "(" & "(" & count + 1 & ")" & ")"
                    newFullPath = IO.Path.Combine(path, (tempFileName + extension))
                    count = count + 1
                End While
                If IO.File.Exists(evidenciacaminho1) Then
                    IO.File.Move(evidenciacaminho1, newFullPath)
                End If
            End If
            Dim doc = New iTextSharp.text.Document()
            PdfWriter.GetInstance(doc, New IO.FileStream(patch, IO.FileMode.Create))
            doc.Open()
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(pc.nome, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            doc.Add(New iTextSharp.text.Paragraph("Comentários:" & pc.Comentarios))
            doc.Add(New iTextSharp.text.Paragraph("Descrição:" & pc.Descrição))
            doc.Add(New iTextSharp.text.Paragraph("Driver:" & pc.Driver))
            doc.Add(New iTextSharp.text.Paragraph("ID:" & pc.ID))
            doc.Add(New iTextSharp.text.Paragraph("Localização:" & pc.localização))
            doc.Add(New iTextSharp.text.Paragraph("Nome Compartilhado:" & pc.NomeCompartilhado))
            doc.Add(New iTextSharp.text.Paragraph("Nome do  Servidor:" & pc.NomedoServidor))
            doc.Add(New iTextSharp.text.Paragraph("Porta:" & pc.Porta))
            doc.Add(New iTextSharp.text.Paragraph("Páginas Contadas:" & pc.PáginasContadas))
            doc.Add(New iTextSharp.text.Paragraph("Toner Reservas disponiveis:" & pc.QuantidadedeTonerDisponivel))
            doc.Add(New iTextSharp.text.Paragraph("Selb:" & pc.Selb))
            doc.Add(New iTextSharp.text.Paragraph("Status:" & pc.Status))
            doc.Add(New iTextSharp.text.Paragraph("System Name:" & pc.SystemName))
            doc.Add(New iTextSharp.text.Paragraph("Tipo de Toner:" & pc.Toner))
            doc.Add(New iTextSharp.text.Paragraph("Toner Restante:" & pc.TonerRestante))
            doc.Add(New iTextSharp.text.Paragraph("Ultima Troca de Toner:" & pc.UltimaTrocadeToner))
            doc.Add(New iTextSharp.text.Paragraph("Ultima Coleta feita:" & pc.UltimaVezColletada))
            doc.Close()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub savepdf(ByVal namepdf As String, ByVal x1 As Patrimonio, ByVal patch1 As String)
        Dim patch As String = patch1 & "\Patrimonios\" & namepdf & ".pdf"
        Try
            If IO.Directory.Exists(patch1 & "\Patrimonios\") = False Then
                IO.Directory.CreateDirectory(patch1 & "\Patrimonios\")
            End If
            Dim evidenciacaminho1 = patch
            If System.IO.File.Exists(patch) Then
                Dim count As Integer = 0
                Dim fileNameOnly As String = IO.Path.GetFileNameWithoutExtension(evidenciacaminho1)
                Dim extension As String = IO.Path.GetExtension(evidenciacaminho1)
                Dim path As String = IO.Path.GetDirectoryName(evidenciacaminho1)
                Dim newFullPath As String = evidenciacaminho1
                While IO.File.Exists(newFullPath)
                    Dim tempFileName As String = fileNameOnly & "(" & "(" & count + 1 & ")" & ")"
                    newFullPath = IO.Path.Combine(path, (tempFileName + extension))
                    count = count + 1
                End While
                If IO.File.Exists(evidenciacaminho1) Then
                    IO.File.Move(evidenciacaminho1, newFullPath)
                End If
            End If
            Dim doc = New iTextSharp.text.Document()
            PdfWriter.GetInstance(doc, New IO.FileStream(patch, IO.FileMode.Create))
            doc.Open()
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(x1.Patrimonio, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            doc.Add(New iTextSharp.text.Paragraph("Sistema Operacional:" & x1.Windows, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Descrição:" & x1.Descrição, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Nota Fiscal:" & x1.NotaFiscal, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Usuário Atual:" & x1.UsuárioAtual, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Nome do Computador Atual:" & x1.NomeDoComputadorAtual, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Atualizado para windows 10:" & x1.AtualizadoParaWindows10.ToString, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Recovery Embutido:" & x1.RecoveryEmbutido.ToString, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Etiqueta com número de série:" & x1.EtiquetaComNumerodeserie, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Adicionado em:" & x1.Adicionadoem, fontTable3))
            ReportLicenses(doc, "Licenças do patrimônio:" & Space(2) & x1.Patrimonio, x1.Licenças, 6)
            doc.Close()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub savepdf(ByVal namepdf As String, ByVal x1 As Licença, ByVal patch1 As String)

        Dim patch As String = patch1 & "\Licenca\" & namepdf & ".pdf"
        Try
            If IO.Directory.Exists(patch1 & "\Licenca\") = False Then
                IO.Directory.CreateDirectory(patch1 & "\Licenca\")
            End If
            Dim evidenciacaminho1 = patch
            If System.IO.File.Exists(patch) Then
                Dim count As Integer = 0
                Dim fileNameOnly As String = IO.Path.GetFileNameWithoutExtension(evidenciacaminho1)
                Dim extension As String = IO.Path.GetExtension(evidenciacaminho1)
                Dim path As String = IO.Path.GetDirectoryName(evidenciacaminho1)
                Dim newFullPath As String = evidenciacaminho1
                While IO.File.Exists(newFullPath)
                    Dim tempFileName As String = fileNameOnly & "(" & "(" & count + 1 & ")" & ")"
                    newFullPath = IO.Path.Combine(path, (tempFileName + extension))
                    count = count + 1
                End While
                If IO.File.Exists(evidenciacaminho1) Then
                    IO.File.Move(evidenciacaminho1, newFullPath)
                End If
            End If
            Dim doc = New iTextSharp.text.Document()
            PdfWriter.GetInstance(doc, New IO.FileStream(patch, IO.FileMode.Create))
            doc.Open()
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(x1.Descrição, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            ReportLicenses3(doc, x1)
            doc.Close()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub savepdfall(ByVal namepdf As String, ByVal patch1 As String)
        Dim patch As String = patch1 & "\" & namepdf & ".pdf"
        Try
            If System.IO.File.Exists(patch) Then
                System.IO.File.Delete(patch)

            End If


            Dim doc = New iTextSharp.text.Document()
            PdfWriter.GetInstance(doc, New IO.FileStream(patch, IO.FileMode.Create))
            doc.Open()
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            For Each x1 As Patrimonio In Database.Patrimonios
                Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph("Patrimônio:" & Space(2) & x1.Patrimonio, fontTable2)
                prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
                doc.Add(prh)
                doc.Add(New iTextSharp.text.Paragraph("Sistema Operacional:" & x1.Windows, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Descrição:" & x1.Descrição, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Nota Fiscal:" & x1.NotaFiscal, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Usuário Atual:" & x1.UsuárioAtual, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Nome do Computador Atual:" & x1.NomeDoComputadorAtual, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Atualizado para windows 10:" & x1.AtualizadoParaWindows10.ToString, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Recovery Embutido:" & x1.RecoveryEmbutido.ToString, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Etiqueta com número de série:" & x1.EtiquetaComNumerodeserie, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Adicionado em:" & x1.Adicionadoem, fontTable3))
                ReportLicenses(doc, "Licenças do patrimônio:" & Space(2) & x1.Patrimonio, x1.Licenças, 6)

            Next
            ReportLicenses(doc, "Todas Licenças:", Database.Licenças, 6)
            doc.Add(New iTextSharp.text.Paragraph("Total de patrimônionios encontrados:" & Database.Patrimonios.Count, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Total de Licenças encontradas:" & Database.Licenças.Count, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Total de Licenças encontradas(em patrimônios e licenças de programas):" & Database.Licenças.Count + Database.Patrimonios.Count, fontTable3))
            doc.Close()
            Process.Start(patch)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub savepdfPatrimonios(ByVal namepdf As String, ByVal patch1 As String)
        Dim patch As String = patch1 & "\" & namepdf & ".pdf"
        Try
            If System.IO.File.Exists(patch) Then
                System.IO.File.Delete(patch)

            End If


            Dim doc = New iTextSharp.text.Document()
            PdfWriter.GetInstance(doc, New IO.FileStream(patch, IO.FileMode.Create))
            doc.Open()
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 14, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            For Each x1 As Patrimonio In Database.Patrimonios
                Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph("Patrimônio:" & Space(2) & x1.Patrimonio, fontTable2)
                prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
                doc.Add(prh)
                doc.Add(New iTextSharp.text.Paragraph("Sistema Operacional:" & x1.Windows, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Descrição:" & x1.Descrição, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Nota Fiscal:" & x1.NotaFiscal, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Usuário Atual:" & x1.UsuárioAtual, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Nome do Computador Atual:" & x1.NomeDoComputadorAtual, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Atualizado para windows 10:" & x1.AtualizadoParaWindows10.ToString, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Recovery Embutido:" & x1.RecoveryEmbutido.ToString, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Etiqueta com número de série:" & x1.EtiquetaComNumerodeserie, fontTable3))
                doc.Add(New iTextSharp.text.Paragraph("Adicionado em:" & x1.Adicionadoem, fontTable3))
                ReportLicenses(doc, "Licenças do patrimônio:" & Space(2) & x1.Patrimonio, x1.Licenças, 6)

            Next
            doc.Add(New iTextSharp.text.Paragraph("Total de patrimônionios encontrados:" & Database.Patrimonios.Count, fontTable3))
            doc.Close()
            Process.Start(patch)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ReportLicenses(ByVal doc As iTextSharp.text.Document, ByVal title As String, ByVal st1 As List(Of Licença), ByVal colcount As Integer)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)
            Dim fontTable4 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.RED)
            Dim table As New PdfPTable(colcount)
            table.SpacingBefore = 45.0F
            table.TotalWidth = 300
            table.DefaultCell.Phrase = New iTextSharp.text.Phrase()
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(title, fontTable2)
            Dim listparagrafh As New List(Of Paragraph)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            table.AddCell(New iTextSharp.text.Phrase("Descrição", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Chave", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Nota Fiscal", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Quantidade", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Midia Fisica", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Em Uso", fontTable))
            table.HeaderRows = 1
            For Each st As Licença In st1
                table.AddCell(New iTextSharp.text.Phrase(st.Descrição, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Chave, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.NotaFiscal, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Quantidade, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.MidiaFisica, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.EmUso, fontTable))
            Next




            doc.Add(table)
            For Each pr1 As Paragraph In listparagrafh
                doc.Add(pr1)
            Next

        Catch ex As Exception

        End Try
    End Sub
    Public Sub ReportLicenses2(ByVal doc As iTextSharp.text.Document, ByVal title As String, ByVal st1 As List(Of Licença), ByVal colcount As Integer)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)
            Dim fontTable4 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.RED)
            Dim table As New PdfPTable(colcount)
            table.SpacingBefore = 45.0F
            table.TotalWidth = 300
            table.DefaultCell.Phrase = New iTextSharp.text.Phrase()
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(title, fontTable2)
            Dim listparagrafh As New List(Of Paragraph)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            table.AddCell(New iTextSharp.text.Phrase("Descrição", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Chave", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Nota Fiscal", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Computadores Instalados", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Usuários usando", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Quantidade", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Midia Fisica", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Em Uso", fontTable))
            table.HeaderRows = 1
            For Each st As Licença In st1
                Dim patstring As String = ""
                Dim users As String = ""
                If st.PatrimonioInstalados IsNot Nothing Then
                    For Each x3 As Patrimonio In st.PatrimonioInstalados
                        patstring = patstring & x3.NomeDoComputadorAtual & ";"
                        users = users & x3.UsuárioAtual & ";"
                    Next
                End If
                table.AddCell(New iTextSharp.text.Phrase(st.Descrição, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Chave, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.NotaFiscal, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(patstring, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(users, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Quantidade, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.MidiaFisica, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.EmUso, fontTable))
                If st.PatrimonioInstalados.Count > st.Quantidade Then
                    listparagrafh.Add(New iTextSharp.text.Paragraph("* Aviso:Foram encontrados mais licenças em uso do que a quantidade permitida da licença:" & Space(1) & st.Chave & Space(1) & "do programa:" & Space(1) & st.Descrição, fontTable4))
                End If
                If st.PatrimonioInstalados.Count < st.Quantidade Then
                    listparagrafh.Add(New iTextSharp.text.Paragraph("* Aviso:há a" & Space(1) & st.Quantidade - st.PatrimonioInstalados.Count & Space(1) & " licença ainda disponivel para uso da licença:" & Space(1) & st.Chave & Space(1) & "do programa:" & Space(1) & st.Descrição, fontTable3))
                End If
            Next




            doc.Add(table)
            For Each pr1 As Paragraph In listparagrafh
                doc.Add(pr1)
            Next

        Catch ex As Exception
        End Try

    End Sub
    Public Sub ReportLicenses3(ByVal doc As iTextSharp.text.Document, ByVal st As Licença)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)
            Dim fontTable4 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.RED)
            Dim listparagrafh As New List(Of Paragraph)
            Dim patstring As String = ""
            Dim users As String = ""
            If st.PatrimonioInstalados IsNot Nothing Then
                For Each x3 As Patrimonio In st.PatrimonioInstalados
                    patstring = patstring & x3.NomeDoComputadorAtual & ";"
                    users = users & x3.UsuárioAtual & ";"
                Next
            End If
            doc.Add(New iTextSharp.text.Paragraph("Descrição:" & st.Descrição, fontTable))
            doc.Add(New iTextSharp.text.Paragraph("Chave:" & st.Chave, fontTable))
            doc.Add(New iTextSharp.text.Paragraph("Nota Fiscal:" & st.NotaFiscal, fontTable))
            doc.Add(New iTextSharp.text.Paragraph("Computadores Instalados:" & patstring, fontTable))
            doc.Add(New iTextSharp.text.Paragraph("Usuários usando:" & users, fontTable))
            doc.Add(New iTextSharp.text.Paragraph("Quantidade:" & st.Quantidade, fontTable))
            doc.Add(New iTextSharp.text.Paragraph("Midia Fisica:" & st.MidiaFisica, fontTable))
            doc.Add(New iTextSharp.text.Paragraph("Em Uso:" & st.EmUso, fontTable))

            If st.PatrimonioInstalados.Count > st.Quantidade Then
                listparagrafh.Add(New iTextSharp.text.Paragraph("* Aviso:Foram encontrados mais licenças em uso do que a quantidade permitida da licença:" & Space(1) & st.Chave & Space(1) & "do programa:" & Space(1) & st.Descrição, fontTable4))
            End If
            If st.PatrimonioInstalados.Count < st.Quantidade Then
                listparagrafh.Add(New iTextSharp.text.Paragraph("* Aviso:há a" & Space(1) & st.Quantidade - st.PatrimonioInstalados.Count & Space(1) & " licença ainda disponivel para uso da licença:" & Space(1) & st.Chave & Space(1) & "do programa:" & Space(1) & st.Descrição, fontTable3))
            End If
            For Each pr1 As Paragraph In listparagrafh
                doc.Add(pr1)
            Next

        Catch ex As Exception

        End Try

    End Sub
    Public Sub savepdfLicenses(ByVal namepdf As String, ByVal patch1 As String)
        Dim patch As String = patch1 & "\" & namepdf & ".pdf"
        Try
            If System.IO.File.Exists(patch) Then
                System.IO.File.Delete(patch)

            End If


            Dim doc = New iTextSharp.text.Document()
            PdfWriter.GetInstance(doc, New IO.FileStream(patch, IO.FileMode.Create))
            doc.Open()
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            ReportLicenses2(doc, "Todas Licenças:", Database.Licenças, 8)
            doc.Add(New iTextSharp.text.Paragraph("Total de Licenças encontradas:" & Database.Licenças.Count, fontTable3))
            doc.Close()
            Process.Start(patch)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub saveNotaFiscal(ByVal namepdf As String, ByVal patch1 As String)
        Dim patch As String = patch1 & "\" & namepdf & ".pdf"
        Try
            If System.IO.File.Exists(patch) Then
                System.IO.File.Delete(patch)

            End If


            Dim doc = New iTextSharp.text.Document()
            PdfWriter.GetInstance(doc, New IO.FileStream(patch, IO.FileMode.Create))
            doc.Open()
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            NotasPatrimonios(doc, "Todos Patrimônios:", Database.Patrimonios, 4)
            NotasLicenças(doc, "Todas Licenças:", Database.Licenças, 3)
            doc.Add(New iTextSharp.text.Paragraph("Total de Notas Fiscais faltantes:" & notasfaltantes, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Total de patrimônionios encontrados:" & Database.Patrimonios.Count, fontTable3))
            doc.Add(New iTextSharp.text.Paragraph("Total de Licenças encontradas(em patrimônios e licenças de programas):" & Database.Licenças.Count + Database.Patrimonios.Count, fontTable3))
            doc.Close()
            Process.Start(patch)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub saveNotaFiscalinexistente(ByVal namepdf As String, ByVal patch1 As String)
        Dim patch As String = patch1 & "\" & namepdf & ".pdf"
        Try
            If System.IO.File.Exists(patch) Then
                System.IO.File.Delete(patch)

            End If


            Dim doc = New iTextSharp.text.Document()
            PdfWriter.GetInstance(doc, New IO.FileStream(patch, IO.FileMode.Create))
            doc.Open()
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable3 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)
            NotasPatrimoniosinexistente(doc, "Todos Patrimônios:", Database.Patrimonios, 4)
            NotasLicençasinexistente(doc, "Todas Licenças:", Database.Licenças, 3)
            doc.Add(New iTextSharp.text.Paragraph("Total de Notas Fiscais Inexistentes ou perdidas:" & notasfaltantes, fontTable3))
            doc.Close()
            Process.Start(patch)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub NotasLicenças(ByVal doc As iTextSharp.text.Document, ByVal title As String, ByVal st1 As List(Of Licença), ByVal colcount As Integer)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 20, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim table As New PdfPTable(colcount)
            table.SpacingBefore = 45.0F
            table.TotalWidth = 300
            table.DefaultCell.Phrase = New iTextSharp.text.Phrase()
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(title, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            table.AddCell(New iTextSharp.text.Phrase("Descrição", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Nota Fiscal", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Midia Fisica", fontTable))
            table.HeaderRows = 1
            For Each st As Licença In st1
                If st.NotaFiscal = "" Then
                    notasfaltantes = notasfaltantes + 1
                End If
                table.AddCell(New iTextSharp.text.Phrase(st.Descrição, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.NotaFiscal, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.MidiaFisica, fontTable))
            Next




            doc.Add(table)

        Catch ex As Exception

        End Try


    End Sub
    Public Sub NotasPatrimonios(ByVal doc As iTextSharp.text.Document, ByVal title As String, ByVal st1 As List(Of Patrimonio), ByVal colcount As Integer)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim table As New PdfPTable(colcount)
            table.SpacingBefore = 45.0F
            table.TotalWidth = 300
            table.DefaultCell.Phrase = New iTextSharp.text.Phrase()
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(title, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            table.AddCell(New iTextSharp.text.Phrase("Patrimônio", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Nota Fiscal", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Nome do Computador Atual", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Sistema Operacional", fontTable))
            table.HeaderRows = 1
            For Each st As Patrimonio In st1
                If st.NotaFiscal = "" Then
                    notasfaltantes = notasfaltantes + 1
                End If
                table.AddCell(New iTextSharp.text.Phrase(st.Patrimonio, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.NotaFiscal, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.NomeDoComputadorAtual, fontTable))
                table.AddCell(New iTextSharp.text.Phrase(st.Windows, fontTable))
            Next




            doc.Add(table)

        Catch ex As Exception

        End Try


    End Sub
    Public Sub NotasLicençasinexistente(ByVal doc As iTextSharp.text.Document, ByVal title As String, ByVal st1 As List(Of Licença), ByVal colcount As Integer)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 20, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim table As New PdfPTable(colcount)
            table.SpacingBefore = 45.0F
            table.TotalWidth = 300
            table.DefaultCell.Phrase = New iTextSharp.text.Phrase()
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(title, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            table.AddCell(New iTextSharp.text.Phrase("Descrição", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Nota Fiscal", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Midia Fisica", fontTable))
            table.HeaderRows = 1
            For Each st As Licença In st1
                If st.NotaFiscal = "" Then
                    notasfaltantes = notasfaltantes + 1
                    table.AddCell(New iTextSharp.text.Phrase(st.Descrição, fontTable))
                    table.AddCell(New iTextSharp.text.Phrase(st.NotaFiscal, fontTable))
                    table.AddCell(New iTextSharp.text.Phrase(st.MidiaFisica, fontTable))
                Else
                    If IO.File.Exists(st.NotaFiscal) = False Then
                        notasfaltantes = notasfaltantes + 1
                        table.AddCell(New iTextSharp.text.Phrase(st.Descrição, fontTable))
                        table.AddCell(New iTextSharp.text.Phrase(st.NotaFiscal, fontTable))
                        table.AddCell(New iTextSharp.text.Phrase(st.MidiaFisica, fontTable))
                    End If
                End If
            Next




            doc.Add(table)

        Catch ex As Exception

        End Try


    End Sub
    Public Sub NotasPatrimoniosinexistente(ByVal doc As iTextSharp.text.Document, ByVal title As String, ByVal st1 As List(Of Patrimonio), ByVal colcount As Integer)

        Try
            Dim fontTable As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)
            Dim fontTable2 As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK)
            Dim table As New PdfPTable(colcount)
            table.SpacingBefore = 45.0F
            table.TotalWidth = 300
            table.DefaultCell.Phrase = New iTextSharp.text.Phrase()
            Dim prh As iTextSharp.text.Paragraph = New iTextSharp.text.Paragraph(title, fontTable2)
            prh.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            doc.Add(prh)
            table.AddCell(New iTextSharp.text.Phrase("Patrimônio", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Nota Fiscal", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Nome do Computador Atual", fontTable))
            table.AddCell(New iTextSharp.text.Phrase("Sistema Operacional", fontTable))
            table.HeaderRows = 1
            For Each st As Patrimonio In st1
                If st.NotaFiscal = "" Then
                    notasfaltantes = notasfaltantes + 1
                    table.AddCell(New iTextSharp.text.Phrase(st.Patrimonio, fontTable))
                    table.AddCell(New iTextSharp.text.Phrase(st.NotaFiscal, fontTable))
                    table.AddCell(New iTextSharp.text.Phrase(st.NomeDoComputadorAtual, fontTable))
                    table.AddCell(New iTextSharp.text.Phrase(st.Windows, fontTable))
                Else
                    If IO.File.Exists(st.NotaFiscal) = False Then
                        notasfaltantes = notasfaltantes + 1
                        table.AddCell(New iTextSharp.text.Phrase(st.Patrimonio, fontTable))
                        table.AddCell(New iTextSharp.text.Phrase(st.NotaFiscal, fontTable))
                        table.AddCell(New iTextSharp.text.Phrase(st.NomeDoComputadorAtual, fontTable))
                        table.AddCell(New iTextSharp.text.Phrase(st.Windows, fontTable))
                    End If
                End If
            Next




            doc.Add(table)

        Catch ex As Exception

        End Try


    End Sub


    Private Sub tabControl1_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Vnctab.MouseClick
        Dim ix As Integer = 0
        Do While (ix < Vnctab.TabCount)
            If Vnctab.GetTabRect(ix).Contains(e.Location) Then
                If Vnctab.SelectedTab.Text.ToLower.Contains("Adicionar conexão+".ToLower) Then
                    Application.DoEvents()
                    Nova_Conexão.Show()
                    Nova_Conexão.BringToFront()
                    Application.DoEvents()
                End If
            End If

            ix = (ix + 1)
        Loop

    End Sub

    Private Sub MetroDefaultSetButton60_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton60.Click
        Try
            Database.VNCpassword = VncPass.Text
            save()
            MetroFramework.MetroMessageBox.Show(Me, "informações Alterada com sucesso", "Alteração concluida", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton63_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton63.Click
        Try
            MetroGrid2.Rows.Clear()
            Database.Destinatarios.Clear()
            save()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton62_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton62.Click
        Try
            For Each x1 As String In Database.Destinatarios
                If x1 = MetroGrid2.SelectedRows(0).Cells(0).Value Then
                    Database.Destinatarios.Remove(x1)
                    MetroGrid2.Rows.Remove(MetroGrid2.SelectedRows(0))
                    save()
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton61_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton61.Click
        Try
            If Database.Destinatarios Is Nothing Then
                Database.Destinatarios = New List(Of String)
                save()
            End If
            Dim Prompt As String = "Informe o nome do destintário"
            Dim Titulo As String = "Adicionar destinatário"
            Dim valorRetornado As String = ""

            valorRetornado = InputBox(Prompt, Titulo)
            If valorRetornado = "" Then
                MetroFramework.MetroMessageBox.Show(Me, "destinatário não pode ser em branco", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Dim alreadyhave As Boolean = False
            For Each x1 As String In Database.Destinatarios
                If x1 = valorRetornado Then
                    alreadyhave = True
                    Exit For
                End If
            Next
            If alreadyhave = False Then
                Database.Destinatarios.Add(valorRetornado)
                save()
                MetroGrid2.Rows.Add(valorRetornado)
                MetroFramework.MetroMessageBox.Show(Me, "destinatário adicionado com sucesso", "Servidor adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MetroFramework.MetroMessageBox.Show(Me, "destinatário ja cadastrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton64_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton64.Click
        Try
            atualizarlista()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton65_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton65.Click
        Try
            ServidoresList.Controls.Clear()
            ServidoresProgressBar.Value = 0
            ServidoresProgressBar.Maximum = Database.Servidores.Count
            For Each x11 As Servidor In Database.Servidores
                If x11.ComputerName.ToString.ToLower.Contains(PCSearch.Text.ToLower) Or x11.CurrentUser.ToString.ToLower.Contains(PCSearch.Text.ToLower) Or x11.IPAddress.ToString.ToLower.Contains(PCSearch.Text.ToLower) Or x11.OperatingSystem.ToString.ToLower.Contains(PCSearch.Text.ToLower) Then
                    Dim newp As New Servidorcontrol
                    newp.MetroSetBadge1.Text = x11.ComputerName
                    newp.Dock = DockStyle.Top
                    If ping(x11.ComputerName) Then
                        newp.MetroSetBadge1.NormalBadgeColor = Color.DarkGreen
                        newp.MetroSetBadge1.BadgeText = "ON"
                    Else
                        newp.MetroSetBadge1.NormalBadgeColor = Color.Red
                        newp.MetroSetBadge1.BadgeText = "Off"
                    End If
                    AddHandler newp.MetroSetBadge1.Click, Sub() clickprinter(newp, EventArgs.Empty)
                    ServidoresList.Controls.Add(newp)
                End If
                ServidoresProgressBar.Value = ServidoresProgressBar.Value + 1
            Next
            If ServidoresList.Controls.Count = 0 Then
                MetroFramework.MetroMessageBox.Show(Me, "Nenhum Resultado Achado na pesquisa,tente pesquisa por outra palavra ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroSetTabPage1asa(sender As Object, e As EventArgs) Handles Vnctab.SelectedIndexChanged, Vnctab.ControlAdded
        Try
            Application.DoEvents()
            Vnctab.SelectedTab.Update()
            Vnctab.SelectedTab.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripMenuItem3_Click_1(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Try
            ScanOnlyInfo.CancelAsync()
            MetroSetContextMenuStrip5.Hide()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton66_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton66.Click
        Try
            For Each x1 As TreeNode In PatTreeView2.Nodes
                For Each x2 As String In CheckedNames(x1.Nodes)
                    For Each pat1 As Patrimonio In Database.Patrimonios
                        If x2 = pat1.Patrimonio Then
                            Database.Patrimonios.Remove(pat1)
                            save()
                            Exit For
                        End If
                    Next
                    For Each lic1 As Licença In Database.Licenças
                        If x2 = lic1.Chave Then
                            Database.Licenças.Remove(lic1)
                            save()
                            Exit For
                        End If
                    Next
                Next
            Next
            Reload()
            Reload2()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub Reload2()
        Try
            PatTreeView2.Nodes.Clear()
            For Each x1 As Patrimonio In Database.Patrimonios

                Dim aleradyhave As Boolean = False
                Dim nodetoadd As New TreeNode
                For Each nd As TreeNode In PatTreeView2.Nodes
                    If nd.Text.ToLower = x1.Windows.ToLower Then
                        nodetoadd = nd
                        aleradyhave = True
                        Exit For
                    End If
                Next
                If aleradyhave = False Then
                    nodetoadd = PatTreeView2.Nodes.Add(x1.Windows)
                End If
                nodetoadd.Nodes.Add(x1.Patrimonio)
            Next
            For Each x2 As Licença In Database.Licenças

                Dim aleradyhave As Boolean = False
                Dim nodetoadd As New TreeNode
                For Each nd As TreeNode In PatTreeView2.Nodes
                    If nd.Text.ToLower = x2.Descrição Then
                        nodetoadd = nd
                        aleradyhave = True
                        Exit For
                    End If
                Next
                If aleradyhave = False Then
                    nodetoadd = PatTreeView2.Nodes.Add(x2.Descrição)
                End If
                nodetoadd.Nodes.Add(x2.Chave)
            Next

        Catch ex As Exception

        End Try
    End Sub
    Private Sub treeView11_AfterCheck(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles PatTreeView2.AfterCheck
        Try
            For Each childNode As TreeNode In e.Node.Nodes
                childNode.Checked = e.Node.Checked
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton13_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton13.Click
        Try
            For Each tr As TreeNode In PatTreeView2.Nodes
                If tr.Checked Then
                    tr.Checked = False
                Else
                    tr.Checked = True
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ReloadConexoesVNC()
        Try
            If Database.ConexoesVNC Is Nothing Then
                Database.ConexoesVNC = New List(Of VNCLast)
                save()
            End If
            Panel1.Controls.Clear()
            Dim list1 As List(Of VNCLast) = Database.ConexoesVNC.OrderByDescending(Function(x) x.Ultimoacesso).ToList()
            For Each x20 As VNCLast In list1
                Dim newv As New VNCpc
                newv.NOME.Text = x20.Nome
                newv.AcessadoEM.Text = x20.Ultimoacesso
                newv.Ip.Text = x20.Ip
                Panel1.Controls.Add(newv)
            Next


        Catch ex As Exception
            MsgBox(ex.Message & ex.StackTrace)
        End Try
    End Sub

End Class
