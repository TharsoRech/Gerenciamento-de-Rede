Imports System.DirectoryServices
Imports MetroSet_UI.Controls

Public Class GruposeUsuários
    Public adspath As String = ""
    Private Sub GruposeUsuários_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            For Each x1 As String In Form1.Database.Gruposad
                GruposList.Items.Add(x1)
            Next
            For Each x2 As Users In Form1.Database.Usuários
                UsuariosList.Items.Add(x2.Usuário)
                MetroSetComboBox1.Items.Add(x2.Usuário)
            Next
            Dim q As ArrayList = New ArrayList
            For Each o As Object In GruposList.Items
                q.Add(o)
            Next
            q.Sort()
            GruposList.Items.Clear()
            For Each o As Object In q
                GruposList.Items.Add(o)
            Next
            Dim q1 As ArrayList = New ArrayList
            For Each o As Object In UsuariosList.Items
                q1.Add(o)
            Next
            q1.Sort()
            UsuariosList.Items.Clear()
            For Each o As Object In q1
                UsuariosList.Items.Add(o)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GruposList_SelectedIndexChanged(sender As Object) Handles GruposList.SelectedIndexChanged
        Try
            Usuarios.Items.Clear()
            GruposList.SelectedItem = GruposList.Items(GruposList.SelectedIndex)
            GetAllUsersFromGroup(Form1.Database.Domain, GruposList.SelectedItem.ToString)
            Gruposelecionado.Text = GruposList.SelectedItem.ToString
            Dim q1 As ArrayList = New ArrayList
            For Each o As Object In Usuarios.Items
                q1.Add(o)
            Next
            q1.Sort()
            Usuarios.Items.Clear()
            For Each o As Object In q1
                Usuarios.Items.Add(o)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Sub GetAllUsersFromGroup(domain As String, group As String)
        Try
            Dim retVal As New List(Of String)()
            Dim entry As New DirectoryEntry([String].Concat("LDAP://", domain))
            Dim searcher As New DirectorySearcher("(&(objectCategory=group)(cn=" & group & "))")
            searcher.SearchRoot = entry
            searcher.SearchScope = SearchScope.Subtree
            Dim result As SearchResult = searcher.FindOne()
            For Each member As String In result.Properties("member")
                Dim de As New DirectoryEntry([String].Concat("LDAP://", domain, "/", member.ToString()))
                If de.Properties("objectClass").Contains("user") AndAlso de.Properties("cn").Count > 0 Then
                    Usuarios.Items.Add(de.Properties("cn")(0).ToString())
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Public Function SearchAllLockedObject2(ByVal root As DirectoryEntry, ByVal username As String)
        Dim userdn As String = ""
        Try




            'OU=Computers,OU=Home,dc=ds,dc=apollo,dc=com
            Dim searcher As DirectoryServices.DirectorySearcher = New DirectoryServices.DirectorySearcher(root)


            searcher.Filter = "(&(objectClass=user)(cn=" + username + "))"




            Dim results As SearchResultCollection = searcher.FindAll()
            For Each result As SearchResult In results
                Dim searchpath As String = result.Path

                Dim rpc As ResultPropertyCollection = result.Properties
                For Each [property] As String In rpc.PropertyNames
                    For Each value As Object In rpc([property])
                        'RichTextBox1.AppendText([property] & ":" & Space(2) & Convert.ToString(value) & Environment.NewLine)
                        Dim value2 As String = ([property] & Convert.ToString(value))
                        If value2.Contains("distinguished") Then
                            userdn = Convert.ToString(value)
                            Return userdn
                        End If

                    Next
                Next
            Next
            Return userdn
        Catch ex As Exception


            Return userdn
        End Try
    End Function

    Private Sub MetroDefaultSetButton1_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton1.Click
        Try
            UsuariosList.SelectedItem = UsuariosList.Items(UsuariosList.SelectedIndex)
            If UsuariosList.SelectedItem.ToString <> "" And Gruposelecionado.Text <> "" Then
                AddToGroup(SearchAllLockedObject(createDirectoryEntry, UsuariosList.SelectedItem.ToString), Getadspathgroups(Form1.Database.Domain, Gruposelecionado.Text))
            Else
                MetroFramework.MetroMessageBox.Show(Me, "Selecione um grupo e um usuário para adicionar", "Não foi possivel adicionar", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            GruposList_SelectedIndexChanged(GruposList)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub RemoveUserFromGroup(userDn As String, groupDn As String)
        Try
            Dim dirEntry As New DirectoryEntry("LDAP://" & groupDn)
            dirEntry.Properties("member").Remove(userDn)
            dirEntry.CommitChanges()
            dirEntry.Close()
            'doSomething with E.Message.ToString();
            MetroFramework.MetroMessageBox.Show(Me, "Usuário removido com sucesso", "remoção concluida", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch E As Exception

        End Try
    End Sub
    Public Function Getadspathgroups(domain As String, group As String)
        Dim groupdn As String = ""
        Try
            Dim retVal As New List(Of String)()
            Dim entry As New DirectoryEntry([String].Concat("LDAP://", domain))
            Dim searcher As New DirectorySearcher("(&(objectCategory=group)(cn=" & group & "))")
            searcher.SearchRoot = entry
            searcher.SearchScope = SearchScope.Subtree
            Dim result As SearchResult = searcher.FindOne()
            groupdn = result.Properties("distinguishedName")(0).ToString
            Return groupdn
        Catch ex As Exception
            Return groupdn

        End Try

    End Function
    Private Shared Function createDirectoryEntry() As DirectoryServices.DirectoryEntry
        Try
            ' create and return new LDAP connection with desired settings
            Dim ldapConnection As DirectoryServices.DirectoryEntry = New DirectoryServices.DirectoryEntry("LDAP://" & Form1.Database.Domain)
            Return ldapConnection
        Catch ex As Exception

        End Try
    End Function

    Public Sub AddToGroup(userDn As String, groupDn As String)
        Try
            Dim dirEntry As New DirectoryEntry("LDAP://" & groupDn)
            dirEntry.Properties("member").Add(userDn)
            dirEntry.CommitChanges()
            dirEntry.Close()
            'doSomething with E.Message.ToString();
            MetroFramework.MetroMessageBox.Show(Me, "Usuário adicionadom com sucesso", "adiçao concluida", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch E As Exception

        End Try
    End Sub
    Public Function SearchAllLockedObject(ByVal root As DirectoryEntry, ByVal username As String)
        Dim userdn As String = ""
        Try
            Dim searcher As DirectoryServices.DirectorySearcher = New DirectoryServices.DirectorySearcher(root)
            searcher.Filter = "(&(objectClass=user)(SAMAccountName=" + username + "))"

            Dim results As SearchResultCollection = searcher.FindAll()
            For Each result As SearchResult In results
                Dim searchpath As String = result.Path

                Dim rpc As ResultPropertyCollection = result.Properties
                For Each [property] As String In rpc.PropertyNames
                    For Each value As Object In rpc([property])
                        Dim value2 As String = ([property] & Convert.ToString(value))
                        If value2.Contains("distinguished") Then
                            userdn = Convert.ToString(value)
                            Return userdn
                        End If

                    Next
                Next
            Next
            Return userdn
        Catch ex As Exception
            Return userdn

        End Try
    End Function

    Private Sub MetroDefaultSetButton2_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton2.Click
        Try
            Usuarios.SelectedItem = Usuarios.Items(Usuarios.SelectedIndex)
            If Usuarios.SelectedItem.ToString <> "" And Gruposelecionado.Text <> "" Then
                RemoveUserFromGroup(SearchAllLockedObject2(createDirectoryEntry, Usuarios.SelectedItem.ToString), Getadspathgroups(Form1.Database.Domain, Gruposelecionado.Text))
            Else
                MetroFramework.MetroMessageBox.Show(Me, "Selecione um grupo e um usuário para remover", "Não foi possivel remover", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            GruposList_SelectedIndexChanged(GruposList)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton3_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton3.Click
        Try
            If MetroSetComboBox1.Text <> "" Then
                searchusers(MetroSetComboBox1.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub searchusers(ByVal username As String)

        Try
            RichTextBox1.Text = ""


            Dim searchRoot As DirectoryServices.DirectoryEntry = createDirectoryEntry()
            Dim search As DirectoryServices.DirectorySearcher = New DirectoryServices.DirectorySearcher(searchRoot)

            search.Filter = "(&(objectClass=user)(SAMAccountName=" + username + "))"
            'first Name
            ' Dim result As DirectoryServices.SearchResult
            Dim resultCol As DirectoryServices.SearchResultCollection = search.FindAll
            If resultCol.Count > 0 Then
            Else

                Exit Sub

            End If
            ' get all entries from the active directory.
            ' Last Name, name, initial, homepostaladdress, title, company etc..
            For Each sResultSet As SearchResult In resultCol

                ' Login Name
                Nome.Text = (GetProperty(sResultSet, "cn"))

                Dim userAccountControl As Integer = Convert.ToInt32(GetProperty(sResultSet, "userAccountControl"))
                Dim disabled As Boolean = ((userAccountControl And 2) > 0)
                If disabled = True Then
                    MetroSetLabel6.Text = "Habilitar Conta:"
                    Desabilitar.Text = "Habilitar"
                    Status.Text = "Disabled"
                ElseIf disabled = False Then
                    Status.Text = "Enabled"
                End If


                Dim lastlog = DateTime.FromFileTime((GetProperty(sResultSet, "lastlogon")))
                UltimoLogon.Text = lastlog
                UltimaModificacao.Text = (GetProperty(sResultSet, "whenchanged"))
                Criadoem.Text = (GetProperty(sResultSet, "whencreated"))
                Email.Text = (GetProperty(sResultSet, "mail"))
                Compania.Text = (GetProperty(sResultSet, "company"))
                adspath = (GetProperty(sResultSet, "adspath"))
                Cidade.Text = (GetProperty(sResultSet, "l"))
                Pais.Text = (GetProperty(sResultSet, "co"))
                Departamento.Text = (GetProperty(sResultSet, "department"))
                Telefone.Text = (GetProperty(sResultSet, "telephoneNumber"))
                sResultSet.GetDirectoryEntry()

                Dim searchpath As String = sResultSet.Path

                Dim rpc As ResultPropertyCollection = sResultSet.Properties
                For Each [property] As String In rpc.PropertyNames
                    For Each value As Object In rpc([property])
                        RichTextBox1.AppendText([property] & ":" & Space(2) & Convert.ToString(value) & Environment.NewLine)
                        Dim value2 As String = ([property] & Convert.ToString(value))
                        If value2.Contains("lock") Then
                            If value2 > 0 Then
                                TrancadosStatus.Text = "lock"
                            Else
                                TrancadosStatus.Text = "Unlock"
                            End If
                        End If

                    Next
                Next
                islockout(searchRoot, username)
            Next
            Alterarsenha.Enabled = True
            Trancar.Enabled = True
            Desabilitar.Enabled = True
            Remover.Enabled = True
        Catch ex As Exception

        End Try
    End Sub
    Public Sub islockout(ByVal root As DirectoryEntry, ByVal username As String)
        Try




            'OU=Computers,OU=Home,dc=ds,dc=apollo,dc=com
            Dim searcher As DirectoryServices.DirectorySearcher = New DirectoryServices.DirectorySearcher(root)


            searcher.Filter = "(&(objectClass=user)(sAMAccountName=" & username & ")(lockoutTime>=1))"




            Dim results As SearchResultCollection = searcher.FindAll()




            If results.Count > 0 Then
                TrancadosStatus.Text = "lock"
                MetroSetLabel7.Text = "Destrancar Conta"
                Trancar.Text = "Destrancar"
            Else
                TrancadosStatus.Text = "Unlock"
            End If



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

    Public Sub Disable(userDn As String)
        Try
            Dim user As New DirectoryEntry(userDn)
            Dim val As Integer = CInt(user.Properties("userAccountControl").Value)
            user.Properties("userAccountControl").Value = val Or &H2
            'ADS_UF_ACCOUNTDISABLE;

            user.CommitChanges()
            user.Close()
            'DoSomethingWith --> E.Message.ToString();
            MetroFramework.MetroMessageBox.Show(Me, "Conta desativada com sucesso", "desativação concluida", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch E As Exception

        End Try
    End Sub
    Public Sub Enable(userDn As String)
        Try
            Dim user As New DirectoryEntry(userDn)
            Dim val As Integer = CInt(user.Properties("userAccountControl").Value)
            user.Properties("userAccountControl").Value = val And Not &H2
            'ADS_UF_NORMAL_ACCOUNT;

            user.CommitChanges()
            user.Close()
            'DoSomethingWith --> E.Message.ToString();
            MetroFramework.MetroMessageBox.Show(Me, "Conta ativada com sucesso", "ativação concluida", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch E As Exception

        End Try
    End Sub
    Public Sub ResetPassword(userDn As String, password As String)
        Try
            Dim uEntry As New DirectoryEntry(userDn)
            uEntry.Invoke("SetPassword", New Object() {password})
            uEntry.Properties("LockOutTime").Value = 0
            'unlock account
            uEntry.Close()
            MetroFramework.MetroMessageBox.Show(Me, "senha alterada com sucesso", "alteração de senha concluida", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch E As Exception
        End Try
    End Sub
    Public Sub unlockaccount(userDn As String)
        Try
            Dim uEntry As New DirectoryEntry(userDn)
            uEntry.Properties("LockOutTime").Value = 0
            'unlock account
            uEntry.Close()
            MetroFramework.MetroMessageBox.Show(Me, "Conta desbloqueada com sucesso", "desbloqueio  concluido", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch E As Exception

        End Try
    End Sub
    Public Sub lockaccount(userDn As String)
        Try
            Dim uEntry As New DirectoryEntry(userDn)
            uEntry.Properties("LockOutTime").Value = 1
            'unlock account
            uEntry.Close()
            MetroFramework.MetroMessageBox.Show(Me, "Conta bloqueada com sucesso", "Bloqueio  concluido", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch E As Exception

        End Try
    End Sub

    Private Sub MetroSetComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MetroSetComboBox1.SelectedIndexChanged
        Try
            If MetroSetComboBox1.Text <> "" Then
                searchusers(MetroSetComboBox1.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton8_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton8.Click
        Try
            If Telefone.Text <> Nothing Then
                updateinfo(Nome.Text, "telephoneNumber", Telefone.Text)
            End If
            If Compania.Text <> Nothing Then
                updateinfo(Nome.Text, "company", Compania.Text)
            End If
            If Departamento.Text <> Nothing Then
                updateinfo(Nome.Text, "department", Departamento.Text)
            End If

            If Cidade.Text <> Nothing Then
                updateinfo(Nome.Text, "l", Cidade.Text)
            End If
            If Pais.Text <> Nothing Then
                updateinfo(Nome.Text, "co", Pais.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub updateinfo(ByVal username As String, ByVal propertieuser As String, ByVal newnamepropertie As String)
        Try
            Dim myLdapConnection As DirectoryEntry = createDirectoryEntry()

            Dim search As New DirectorySearcher(myLdapConnection)
            search.Filter = "(cn=" & username & ")"
            search.PropertiesToLoad.Add(propertieuser)

            Dim result As SearchResult = search.FindOne()

            If result IsNot Nothing Then
                ' create new object from search result

                Dim entryToUpdate As DirectoryEntry = result.GetDirectoryEntry()

                ' show existing title

                ' Console.WriteLine("Current title   : " & entryToUpdate.Properties("title")(0).ToString())

                ' Console.Write(vbLf & vbLf & "Enter new title : ")

                ' get new title and write to AD

                Dim newTitle As [String] = newnamepropertie
                If entryToUpdate.Properties(propertieuser).Value = newTitle Then
                    Exit Sub
                Else
                    entryToUpdate.Properties(propertieuser).Value = newTitle
                    entryToUpdate.CommitChanges()

                End If

                ' Console.WriteLine(vbLf & vbLf & "...new title saved")
            Else

                'Console.WriteLine("User not found!")
            End If
            MetroFramework.MetroMessageBox.Show(Me, "Informações atualizada com sucesso", "atualização de informações  concluida", MessageBoxButtons.OK, MessageBoxIcon.Information)

            If MetroSetComboBox1.Text <> "" Then
                searchusers(MetroSetComboBox1.Text)
            End If

        Catch e As Exception

        End Try
    End Sub

    Private Sub Alterarsenha_Click(sender As Object, e As EventArgs) Handles Alterarsenha.Click
        Try
            If Senha.Text <> Nothing Then
                ResetPassword(adspath, Senha.Text)
            Else
                MetroFramework.MetroMessageBox.Show(Me, "senha não pode ser em branco", "erro ao altera senha", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Desabilitar_Click(sender As Object, e As EventArgs) Handles Desabilitar.Click
        Try
            If Desabilitar.Text.Contains("Desabi") Then
                Dim result As Integer = MetroFramework.MetroMessageBox.Show(Me, "Você tem certeze que quer desabilitar a conta do usuario" & Space(2) & Nome.Text & " ?", "Desabilitar Conta", MessageBoxButtons.YesNo)
                If result = DialogResult.No Then
                    Exit Sub
                End If
                Disable(adspath)
                If MetroSetComboBox1.Text <> "" Then
                    searchusers(MetroSetComboBox1.Text)
                End If
            End If
            If Desabilitar.Text.Contains("Habili") Then
                Dim result As Integer = MetroFramework.MetroMessageBox.Show(Me, "Você tem certeze que quer habilitar a conta do usuario" & Space(2) & Nome.Text & " ?", "habilitar Conta", MessageBoxButtons.YesNo)
                If result = DialogResult.No Then
                    Exit Sub
                End If
                Enable(adspath)
                If MetroSetComboBox1.Text <> "" Then
                    searchusers(MetroSetComboBox1.Text)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Trancar_Click(sender As Object, e As EventArgs) Handles Trancar.Click
        Try
            If Desabilitar.Text.Contains("Destra") Then
                Dim result As Integer = MetroFramework.MetroMessageBox.Show(Me, "Você tem certeze que quer destrancar a conta do usuario" & Space(2) & Nome.Text & " ?", "Desabilitar Conta", MessageBoxButtons.YesNo)
                If result = DialogResult.No Then
                    Exit Sub
                End If
                unlockaccount(adspath)
                If MetroSetComboBox1.Text <> "" Then
                    searchusers(MetroSetComboBox1.Text)
                End If
            End If
            If Desabilitar.Text.Contains("Tranc") Then
                Dim result As Integer = MetroFramework.MetroMessageBox.Show(Me, "Você tem certeze que quer trancar a conta do usuario" & Space(2) & Nome.Text & " ?", "habilitar Conta", MessageBoxButtons.YesNo)
                If result = DialogResult.No Then
                    Exit Sub
                End If
                lockaccount(adspath)
                If MetroSetComboBox1.Text <> "" Then
                    searchusers(MetroSetComboBox1.Text)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Remover_Click(sender As Object, e As EventArgs) Handles Remover.Click
        Try
            Dim result As Integer = MetroFramework.MetroMessageBox.Show(Me, "Você tem certeze que quer Remover a conta do usuario" & Space(2) & Nome.Text & " ?", "Remover Usuario ", MessageBoxButtons.YesNo)
            If result = DialogResult.No Then
                Exit Sub
            ElseIf result = DialogResult.Yes Then

            End If
            ADHelper.DeleteUser(MetroSetCheckBox1.Text)
            MetroFramework.MetroMessageBox.Show(Me, "Usuário removido com sucesso", "remoção de usuário concluida", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroSetCheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles MetroSetCheckBox1.Click
        Try
            If MetroSetCheckBox1.Checked Then
                Senha.UseSystemPasswordChar = False
            Else
                Senha.UseSystemPasswordChar = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroDefaultSetButton4_Click(sender As Object, e As EventArgs) Handles MetroDefaultSetButton4.Click
        Try
            AdicionarUsuário.Show()
        Catch ex As Exception

        End Try
    End Sub
End Class