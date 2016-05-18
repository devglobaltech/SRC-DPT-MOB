Imports System.Data

Public Class frmCargaSeriesInvalidas

    Private DsErr As DataSet

    Public Property SeriesError() As DataSet
        Get
            Return DsErr
        End Get
        Set(ByVal value As DataSet)
            DsErr = value
        End Set
    End Property

    Private Sub frmCargaSeriesInvalidas_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Salir()
        End Select
    End Sub

    Private Sub frmCargaSeriesInvalidas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DgSeries.DataSource = DsErr.Tables(0)
        AutoSizeGrid(Me.DgSeries, "Series Invalidas")
    End Sub

    Public Sub Salir()
        Me.Close()
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Salir()
    End Sub

End Class