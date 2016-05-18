Public Class frmStockInicialConf

    Private Sti As clsStockInicial
    Private Const SQLConErr As String = "Se perdio la conexion con la base de datos."
    Private Const FrmName As String = "Toma de Stock Inicial."
    Private vComenzar As Boolean = False
    Private Reconf As Boolean = False

    Public Property Reconfigurar() As Boolean
        Get
            Return Reconf
        End Get
        Set(ByVal value As Boolean)
            Reconf = value
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

    Public ReadOnly Property ComenzarOperacion() As Boolean
        Get
            Return vComenzar
        End Get
    End Property

    Private Sub frmConfStockInicial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Sti.ObtenerClientes(Me.cmbClientes)
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub cmdContinuar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdContinuar.Click
        Comenzar()
    End Sub

    Private Sub Comenzar()
        Dim vErr As Integer
        Try
            If Sti.PuedoComenzar(vErr) Then
                'abro el formulario para que comience con la toma de inventario
                'inicial.
                vComenzar = True
                Me.Close()
            Else
                Select Case vErr
                    Case 1
                        MsgBox("Debe indicar el codigo de cliente.", MsgBoxStyle.Information, FrmName)
                        Me.cmbClientes.Focus()
                    Case 2
                        MsgBox("Debe seleccionar el tipo de conteo.", MsgBoxStyle.Information, FrmName)
                        Me.cmdTipoConteo.Focus()
                    Case 3
                        MsgBox("Debe seleccionar si valida o no el Sku.", MsgBoxStyle.Information, FrmName)
                        Me.cmbValidarSKU.Focus()
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

    Private Sub cmbClientes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbClientes.KeyUp
        If e.KeyValue = 13 Then
            If Me.cmbClientes.SelectedValue <> "" Then
                Sti.CodigoCliente = Me.cmbClientes.SelectedValue
                Sti.RazonSocial = Me.cmbClientes.Text
                Me.cmdTipoConteo.Focus()
            End If
        End If
    End Sub

    Private Sub cmdTipoConteo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmdTipoConteo.KeyUp
        If e.KeyValue = 13 Then
            If Me.cmdTipoConteo.Text <> "" Then
                If Me.cmdTipoConteo.Text = "Codigo de Barras" Then
                    Sti.ModoConteo = clsStockInicial.TipoConteo.Etiqueta
                ElseIf Me.cmdTipoConteo.Text = "Conteo de Cantidades" Then
                    Sti.ModoConteo = clsStockInicial.TipoConteo.Cantidad
                End If
                Me.cmbValidarSKU.Focus()
            End If
        End If
    End Sub

    Private Sub cmbValidarSKU_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbValidarSKU.KeyUp
        If e.KeyValue = 13 Then
            If Me.cmbClientes.Text <> "" Then
                If Me.cmbValidarSKU.Text = "Si" Then
                    Sti.ValidaSku = "S"
                    Comenzar()
                Else
                    Sti.ValidaSku = "N"
                    Sti.Lote = False
                    Sti.Partida = False
                    Comenzar()
                End If
            Else
                Me.cmbValidarSKU.Focus()
            End If
        End If
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Salir()
    End Sub

    Private Sub Salir()
        Me.Close()
    End Sub

    Private Sub cmdTipoConteo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTipoConteo.LostFocus
        If Me.cmdTipoConteo.Text <> "" Then
            If Me.cmdTipoConteo.Text = "Codigo de Barras" Then
                Sti.ModoConteo = clsStockInicial.TipoConteo.Etiqueta
            ElseIf Me.cmdTipoConteo.Text = "Conteo de Cantidades" Then
                Sti.ModoConteo = clsStockInicial.TipoConteo.Cantidad
            End If
        End If
    End Sub

    Private Sub cmbValidarSKU_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbValidarSKU.LostFocus
        If Me.cmbClientes.Text <> "" Then
            If Me.cmbValidarSKU.Text = "Si" Then
                Sti.ValidaSku = "S"
            Else
                Sti.ValidaSku = "N"
            End If
        End If
    End Sub

    Private Sub cmbClientes_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbClientes.LostFocus
        If Me.cmbClientes.SelectedValue <> "" Then
            Sti.CodigoCliente = Me.cmbClientes.SelectedValue
            Sti.RazonSocial = Me.cmbClientes.Text
        End If
    End Sub

    Private Sub cmbValidarSKU_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbValidarSKU.SelectedIndexChanged

    End Sub
End Class