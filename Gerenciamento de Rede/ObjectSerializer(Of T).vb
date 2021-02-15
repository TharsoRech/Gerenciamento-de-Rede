
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO

Public Class ObjectSerializer(Of T)
    Protected iformatter As IFormatter

    Public Sub New()
        Me.iformatter = New BinaryFormatter()
    End Sub

    Public Function GetSerializedObject(filename As String) As T
        If File.Exists(filename) Then
            Dim inStream As Stream = New FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read)
            Dim obj As T = DirectCast(Me.iformatter.Deserialize(inStream), T)
            inStream.Close()
            Return obj
        End If
        Return Nothing
    End Function

    Public Sub SaveSerializedObject(obj As T, filename As String)
        Dim outStream As Stream = New FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None)
        Me.iformatter.Serialize(outStream, obj)
        outStream.Close()
    End Sub

End Class



