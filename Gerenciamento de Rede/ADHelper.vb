Imports System.DirectoryServices

Public Class ADHelper

    Private Shared Function GetDirectoryEntry() As DirectoryEntry
        Return New DirectoryEntry(String.Format("LDAP://{0}", Environment.UserDomainName))
    End Function
    Public Shared Function UserIsExist(userName As String) As Boolean
        Dim deUser = GetUserByName(userName)
        Return If(deUser IsNot Nothing, True, False)
    End Function
    Public Shared Function GetUserByName(userName As String) As DirectoryEntry
        Dim de = GetDirectoryEntry()
        Try
            If String.IsNullOrEmpty(userName) Then
                Throw New ArgumentNullException("Username is null or empty!")
            End If
            Dim deSearch As New DirectorySearcher()
            deSearch.SearchRoot = de
            deSearch.Filter = String.Format("(&(objectClass=user)(sAMAccountName={0}))", userName)
            Dim result = deSearch.FindOne()
            If result IsNot Nothing Then
                Return result.GetDirectoryEntry()
            End If
            Return Nothing
        Catch ex As Exception
            Throw ex
        Finally
            de.Dispose()
        End Try
    End Function
    Public Shared Function AddUser(adUser As ADUser) As Boolean
        If String.IsNullOrEmpty(adUser.UserName) Then
            Throw New ArgumentNullException("Username is null or empty!")
        End If
        If String.IsNullOrEmpty(adUser.Password) Then
            Throw New ArgumentNullException("Password is null or empty!")
        End If
        Dim de = GetDirectoryEntry()
        Dim deUser = de.Children.Add(String.Format("CN={0},CN=users", adUser.UserName), "user")
        Try
            deUser.Properties("sAMAccountName").Value = adUser.UserName
            deUser.Properties("userPrincipalName").Value = String.Format("{0}@{1}", adUser.UserName, Environment.UserDomainName)
            If Not String.IsNullOrEmpty(adUser.Name) Then
                deUser.Properties("displayName").Value = adUser.Name
            End If
            If Not String.IsNullOrEmpty(adUser.Description) Then
                deUser.Properties("description").Value = adUser.Description
            End If
            deUser.CommitChanges()
            ChangePassword(adUser.UserName, "", adUser.Password)
            EnableUser(adUser.UserName)
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            deUser.Dispose()
            de.Dispose()
        End Try
    End Function
    Public Shared Function DeleteUser(userName As String) As Boolean
        Dim deUser = GetUserByName(userName)
        If deUser Is Nothing Then
            Throw New Exception("User dese not exist!")
        End If
        Try
            deUser.DeleteTree()
            deUser.CommitChanges()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            deUser.Dispose()
        End Try
    End Function
    Public Shared Function EnableUser(userName As String) As Boolean
        Dim deUser = GetUserByName(userName)
        If deUser Is Nothing Then
            Throw New Exception("User dese not exist!")
        End If
        Try
            deUser.Properties("userAccountControl")(0) = ADS_USER_FLAG_ENUM.ADS_UF_NORMAL_ACCOUNT Or ADS_USER_FLAG_ENUM.ADS_UF_DONT_EXPIRE_PASSWD
            deUser.CommitChanges()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            deUser.Dispose()
        End Try
    End Function
    Public Shared Function ChangePassword(userName As String, oldPassword As String, password As String) As Boolean
        Dim deUser = GetUserByName(userName)
        If deUser Is Nothing Then
            Throw New Exception("User dese not exist!")
        End If
        Try
            deUser.Invoke("ChangePassword", New Object() {oldPassword, password})
            deUser.CommitChanges()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            deUser.Dispose()
        End Try
    End Function
    ''' <summary>
    ''' User properties
    ''' </summary>
    Public Enum ADS_USER_FLAG_ENUM
        ADS_UF_SCRIPT = &H1
        ADS_UF_ACCOUNTDISABLE = &H2
        ADS_UF_HOMEDIR_REQUIRED = &H8
        ADS_UF_LOCKOUT = &H10
        ADS_UF_PASSWD_NOTREQD = &H20
        ADS_UF_PASSWD_CANT_CHANGE = &H40
        ADS_UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED = &H80
        ADS_UF_TEMP_DUPLICATE_ACCOUNT = &H100
        ADS_UF_NORMAL_ACCOUNT = &H200
        ADS_UF_INTERDOMAIN_TRUST_ACCOUNT = &H800
        ADS_UF_WORKSTATION_TRUST_ACCOUNT = &H1000
        ADS_UF_SERVER_TRUST_ACCOUNT = &H2000
        ADS_UF_DONT_EXPIRE_PASSWD = &H10000
        ADS_UF_MNS_LOGON_ACCOUNT = &H20000
        ADS_UF_SMARTCARD_REQUIRED = &H40000
        ADS_UF_TRUSTED_FOR_DELEGATION = &H80000
        ADS_UF_NOT_DELEGATED = &H100000
        ADS_UF_USE_DES_KEY_ONLY = &H200000
        ADS_UF_DONT_REQUIRE_PREAUTH = &H4000000
        ADS_UF_PASSWORD_EXPIRED = &H800000
        ADS_UF_TRUSTED_TO_AUTHENTICATE_FOR_DELEGATION = &H1000000
    End Enum
End Class
