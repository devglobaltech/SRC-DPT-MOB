Imports System.Data
Imports System.Data.SqlClient

Public Class frmStockInicialStockByPos
    Private Const SQLConErr As String = "Se perdio la conexion con la base de datos."
    Private Const FrmName As String = "Toma de Stock Inicial."
    Private Sti As clsStockInicial
    Private Ubic As String = ""
    Private Ds As DataSet

    Public Property Informacion() As DataSet
        Get
            Return Ds
        End Get
        Set(ByVal value As DataSet)
            Ds = value
        End Set
    End Property

    Public Property oStockInicial() As clsStockInicial
        Get
            Return Sti
        End Get
        Set(ByVal value As clsStockInicial)
            Sti = value
        End Set
    End Property

    Public Property Ubicacion() As String
        Get
            Return Ubic
        End Get
        Set(ByVal value As String)
            Ubic = value
            Me.lblUbicacion.Text = "Ubicacion: " & value
        End Set
    End Property

    Private Sub frmStockInicialStockByPos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                EliminarRegistro()
            Case Keys.F2
                Volver()
        End Select
    End Sub

    Private Sub EliminarRegistro()
        Dim Id As Long = 0
        Const ConstID As Integer = 5
        If MsgBox("¿Desea borrar el registro seleccionado?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, FrmName) = MsgBoxResult.Yes Then
            Id = Me.dgStock.Item(dgStock.CurrentRowIndex, ConstID).ToString
            If Id <> 0 Then
                If Sti.BorrarStock(Id) Then
                    Ds = Nothing
                    Ds = New DataSet
                    Sti.GetStockByPosicion(Me.Ubicacion, Ds)
                    If Ds.Tables("STOCK").Rows.Count > 0 Then
                        Me.dgStock.DataSource = Nothing
                        Me.dgStock.DataSource = Ds.Tables("STOCK")
                        AutoSizeGrid(Me.dgStock, FrmName)
                        Me.dgStock.Focus()
                    Else
                        MsgBox("No se encontro stock inicial en la posicion indicada.", MsgBoxStyle.Information, FrmName)
                        Volver()
                    End If
                End If
            End If
        Else
            Me.dgStock.Focus()
        End If
    End Sub

    Private Sub FormatearGrilla()


        Dim Style As New DataGridTableStyle
        dgStock.TableStyles.Clear()
        Style.MappingName = "STOCK"

        Dim tc1 As New DataGridTextBoxColumn
        With tc1
            .MappingName = "Cliente_ID"
            .HeaderText = "Cod. Cliente"
            .Width = 60
        End With
        Style.GridColumnStyles.Add(tc1)

        Dim tc2 As New DataGridTextBoxColumn
        With tc2
            .MappingName = "Producto_Id"
            .HeaderText = "Cod. Producto"
            .Width = 100
        End With
        Style.GridColumnStyles.Add(tc2)

        Dim tc3 As New DataGridTextBoxColumn
        With tc3
            .MappingName = "Cantidad"
            .HeaderText = "Cantidad"
            .Width = 50
        End With
        Style.GridColumnStyles.Add(tc3)


        Dim tc4 As New DataGridTextBoxColumn
        With tc4
            .MappingName = "Nro_lote"
            .HeaderText = "Nro.Lote"
            .Width = 50
        End With
        Style.GridColumnStyles.Add(tc4)

        Dim tc5 As New DataGridTextBoxColumn
        With tc5
            .MappingName = "Nro_Partida"
            .HeaderText = "Nro.Partida"
            .Width = 70
        End With
        Style.GridColumnStyles.Add(tc5)


        Dim tc6 As New DataGridTextBoxColumn
        With tc6
            .MappingName = "Stock_ID"
            .HeaderText = "Stock_ID"
            .Width = 10
        End With
        Style.GridColumnStyles.Add(tc6)

        dgStock.TableStyles.Add(Style)

    End Sub

    Private Sub Volver()
        Me.Close()
    End Sub

    Private Sub dgStock_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgStock.GotFocus
        Try
            dgStock.Select(dgStock.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgStock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgStock.KeyDown
        Try
            dgStock.Select(dgStock.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmStockInicialStockByPos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.dgStock.DataSource = Informacion.Tables("STOCK")
        'FormatearGrilla()
        AutoSizeGrid(Me.dgStock, FrmName)
        Me.dgStock.Focus()
    End Sub

    Private Sub btnVolver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVolver.Click
        Volver()
    End Sub

    Private Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBorrar.Click
        EliminarRegistro()
    End Sub

End Class