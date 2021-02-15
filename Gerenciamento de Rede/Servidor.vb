<Serializable>
Public Class Servidor
    Public discos As New List(Of Discos)
    Public drivers As New List(Of Drivers)
    Public Impressoras As New List(Of Impressoras)
    Public Licenças As New List(Of Licença)
    Public Processadores As New List(Of Processador)
    Public Processos As New List(Of Processos)
    Public Softwares As New List(Of Software)
    Public UsersLocal As New List(Of UserLocalAccount)
    Public ComputerName As String = ""
    Public WindowsDirectory As String = ""
    Public OperatingSystem As String = ""
    Public Version As String = ""
    Public Manufacturer As String = ""
    Public OSArchitecture As String = ""
    Public BuildNumber As String = ""
    Public LocalDateTime As String = ""
    Public SerialNumber As String = ""
    Public LastBootUpTime As String = ""
    Public Status As String = ""
    Public TotalVisibleMemorySize As String = ""
    Public Model As String = ""
    Public CurrentUser As String = ""
    Public IPAddress As String = ""
    Public MACAddress As String = ""
    Public DefaultIPGateway As String = ""
    Public Dns As New List(Of String)
    Public Ultimovezscaneada As String = ""
    Public InstallDate As String = ""
    Public Anotações As String = ""
End Class

