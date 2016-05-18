Imports System.Data

Public Class frmEgresoConversiones

    Private dsDatos As DataSet

    Public Property Datos() As DataSet
        Get
            Return dsDatos
        End Get
        Set(ByVal value As DataSet)
            dsDatos = value
        End Set
    End Property

    Private Sub Volver()
        Me.Close()
    End Sub

    Private Sub frmEgresoConversiones_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                Volver()
        End Select
    End Sub

    Private Sub frmEgresoConversiones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.dgConversiones.DataSource = dsDatos.Tables(0)
        AutoSizeGrid(Me.dgConversiones, "Conversiones")
    End Sub

    Private Sub btnVolver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVolver.Click
        Volver()
    End Sub
End Class