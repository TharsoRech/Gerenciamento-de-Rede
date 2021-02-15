<Serializable>
Public Class Patrimonio
    Public Patrimonio As String = ""
    Public Descrição As String = ""
    Public NomeDoComputadorAtual = ""
    Public Windows As String = ""
    Public NotaFiscal As String = ""
    Public Adicionadoem As String = ""
    Public LicençadoWindows As String = ""
    Public UsuárioAtual As String = ""
    Public Licenças As New List(Of Licença)
    Public AtualizadoParaWindows10 As Boolean = False
    Public RecoveryEmbutido As Boolean = False
    Public EtiquetaComNumerodeserie As Boolean = False

End Class

