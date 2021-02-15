<Serializable>
Public Class Licença
    Public Tipo As String = ""
    Public Key As String = ""
    Public Versão As String = ""
    Public Produto As String = ""
    Public Descrição As String = ""
    Public Chave As String = ""
    Public PatrimonioInstalados As New List(Of Patrimonio)
    Public Quantidade As String = ""
    Public NotaFiscal As String = ""
    Public Adicionadoem As String = ""
    Public EmUso As Boolean = False
    Public MidiaFisica As Boolean = False
End Class
