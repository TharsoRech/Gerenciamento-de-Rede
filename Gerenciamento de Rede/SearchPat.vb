Public Class SearchPat
    Public Selectgrid As Wisder.W3Common.WMetroControl.Controls.MetroGrid
    Private Sub SearchPat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            For Each x1 As Patrimonio In Form1.Database.Patrimonios
                MetroGrid1.Rows.Add({x1.NomeDoComputadorAtual, x1.UsuárioAtual, x1.Windows, x1.Patrimonio})
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AddNewlc_Click(sender As Object, e As EventArgs) Handles AddNewlc.Click

        Try
            For Each dr1 As DataGridViewRow In MetroGrid1.SelectedRows
                Selectgrid.Rows.Add({dr1.Cells(0).Value, dr1.Cells(1).Value, dr1.Cells(2).Value, dr1.Cells(3).Value})
            Next
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MetroGrid1_CellContentdbClick(sender As Object, e As DataGridViewCellEventArgs) Handles MetroGrid1.CellContentDoubleClick
        Try
            Selectgrid.Rows.Add({MetroGrid1.SelectedRows(0).Cells(0).Value, MetroGrid1.SelectedRows(0).Cells(1).Value, MetroGrid1.SelectedRows(0).Cells(2).Value, MetroGrid1.SelectedRows(0).Cells(3).Value})
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class