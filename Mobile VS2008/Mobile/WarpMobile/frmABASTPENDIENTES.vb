Public Class frmABASTPENDIENTES

    Private Sub frmABASTPENDIENTES_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Me.Close()
        End Select
    End Sub

    Private Sub frmABASTPENDIENTES_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AutoSizeGrid(DataGrid1, "Abastecimiento")
        Me.DataGrid1.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub DataGrid1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Click
        Try
            DataGrid1.Select(DataGrid1.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DataGrid1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.CurrentCellChanged
        Try
            DataGrid1.Select(DataGrid1.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DataGrid1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.GotFocus
        Try
            DataGrid1.Select(DataGrid1.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DataGrid1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DataGrid1.KeyPress
        Try
            DataGrid1.Select(DataGrid1.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DataGrid1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGrid1.KeyUp
        Try
            DataGrid1.Select(DataGrid1.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub
End Class