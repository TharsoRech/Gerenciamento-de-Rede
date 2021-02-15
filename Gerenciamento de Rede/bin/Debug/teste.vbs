on error resume next
Dim objShell
Set objShell = WScript.CreateObject( "WScript.Shell" )
objShell.Run """Gerenciamento de Rede.exe""" & "teste"
Set objShell = Nothing