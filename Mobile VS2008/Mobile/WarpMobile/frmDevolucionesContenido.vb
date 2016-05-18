Imports System.Data

Public Class frmDevolucionesContenido

    Private oDEVO As cDevoluciones
    Private o2D As clsDecode2D
    Private Const FrmName As String = "Devoluciones"

    Public Property oDEVOLUCION() As cDevoluciones
        Get
            Return oDEVO
        End Get
        Set(ByVal value As cDevoluciones)
            oDEVO = value
        End Set
    End Property

    Public Property Titulo() As String
        Get
            Return ""
        End Get
        Set(ByVal value As String)
            Me.lblTitulo.Text = "Contenido Pallet: " & value
        End Set
    End Property

    Private Sub frmDevolucionesContenido_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyData
            Case Keys.F1
                Me.Close()
        End Select
    End Sub

    Private Sub frmDevolucionesContenido_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mostrarInfo()
    End Sub

    Private Sub mostrarInfo()
        Try
            Dim DS As New DataSet
            Me.lblTitulo.Text = "Contenido Pallet: " & oDEVO.PalletDevolucion
            oDEVO.GetContenidoPallet(oDEVO.PalletDevolucion, DS)
            Me.dgResult.DataSource = DS.Tables(0)
            AutoSizeGrid(Me.dgResult, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        End Try
    End Sub

    Private Sub btnVolver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVolver.Click
        Me.Close()
    End Sub
End Class