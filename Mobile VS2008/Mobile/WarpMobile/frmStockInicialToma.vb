Imports System.Data

Public Class frmStockInicialToma

    Private Sti As clsStockInicial
    Private Const SQLConErr As String = "Se perdio la conexion con la base de datos."
    Private Const FrmName As String = "Toma de Stock Inicial."
    Private UltimoSku As String = ""
    Private UltimaCantidad As Integer = 0

    Public Property oStockInicial() As clsStockInicial
        Get
            Return Sti
        End Get
        Set(ByVal value As clsStockInicial)
            Sti = value
        End Set
    End Property

    Private Sub frmStockInicialToma_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Comenzar()
            Case Keys.F2
                CambiarUbicacion()
            Case Keys.F3

            Case Keys.F4
                Salir()
        End Select
    End Sub

    Private Sub frmStockInicialToma_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InicializarFormulario()
    End Sub


    Private Sub InicializarFormulario()
        Try
            Dim Ctl As Control
            For Each Ctl In Me.Controls
                If TypeOf Ctl Is Button Then
                    Ctl.Visible = True
                Else
                    Ctl.Visible = False
                End If
                If TypeOf Ctl Is TextBox Then
                    Ctl.Text = ""
                    Ctl.Enabled = True
                End If
            Next
            'Devuelvo la Razon Social para que sepa que material debe tomar.
            Me.lblCliente.Text = "Cliente: " & Sti.RazonSocial
            Me.lblCliente.Visible = True
            Me.btnStockPos.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        End Try
    End Sub

    Private Sub txtUbicacion_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUbicacion.GotFocus
        Me.txtUbicacion.SelectAll()
    End Sub

    Private Sub txtUbicacion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUbicacion.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtUbicacion.Text.Trim <> "" Then
                If Sti.ValidarPosicion(Me.txtUbicacion.Text) Then
                    'debo continuar con el Sku.
                    Me.txtUbicacion.Enabled = False
                    Me.lblProducto.Visible = True
                    Me.txtProducto.Visible = True
                    Me.txtProducto.Text = ""
                    Me.lblDescripcion.Text = ""
                    Me.lblAdicionales.Visible = False
                    Me.lblLote.Visible = False
                    Me.lblPartida.Visible = False
                    Me.txtLote.Visible = False
                    Me.txtPartida.Visible = False
                    Me.txtProducto.Focus()
                Else
                    MsgBox("La ubicacion indicada no existe. " & vbNewLine & "Ingrese nuevamente la ubicación.", MsgBoxStyle.Information, FrmName)
                    Me.txtUbicacion.Text = ""
                    Me.txtUbicacion.Focus()
                End If
            Else
                Me.txtUbicacion.Text = ""
                Me.txtUbicacion.Focus()
            End If
        End If
    End Sub

    Private Sub txtProducto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProducto.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtProducto.Text.Trim <> "" Then

                o2D.Decode(Me.txtProducto.Text)
                Me.txtProducto.Text = o2D.PRODUCTO_ID

                If Sti.ValidarProducto(Me.txtProducto, Me.lblDescripcion) Then
                    Me.UltimoSku = Me.txtProducto.Text
                    If Sti.ModoConteo = clsStockInicial.TipoConteo.Etiqueta Then
                        Me.UltimaCantidad = 1
                        Confirmar()
                    ElseIf Sti.ModoConteo = clsStockInicial.TipoConteo.Cantidad Then
                        Me.lblCantidad.Visible = True
                        Me.txtCantidad.Visible = True
                        Me.txtCantidad.Text = ""
                        Me.txtCantidad.Focus()
                    End If
                Else
                    MsgBox("No se encontro el producto indicado para el cliente seleccionado.", MsgBoxStyle.Information, FrmName)
                    Me.txtProducto.Text = ""
                    Me.txtProducto.Focus()
                End If
            Else
                Me.txtProducto.Text = ""
                Me.txtProducto.Focus()
            End If
        End If
    End Sub

    Private Sub Confirmar()

        'Obligatorio.
        If (Me.txtUbicacion.Text.Trim = "") Then
            MsgBox("Debe indicar la Ubicacion.", MsgBoxStyle.Information, FrmName)
            Me.txtUbicacion.Text = ""
            Me.txtUbicacion.Focus()
            Return
        End If

        'Obligatorio.
        If (Me.txtProducto.Text.Trim = "") Then
            MsgBox("Debe indicar el codigo de producto.", MsgBoxStyle.Information, FrmName)
            Me.txtProducto.Text = ""
            Me.txtProducto.Focus()
            Return
        End If

        'Tiene que cargar el Lote.
        If Sti.Lote = True And Not Me.txtLote.Visible Then
            Me.lblAdicionales.Visible = True
            Me.lblLote.Visible = True
            Me.txtLote.Text = ""
            Me.txtLote.Visible = True
            Me.txtLote.Focus()
            Return
        End If

        'Tiene que cargar la partida.
        If Sti.Partida = True And Not Me.txtPartida.Visible Then
            Me.lblAdicionales.Visible = True
            Me.lblPartida.Visible = True
            Me.txtPartida.Text = ""
            Me.txtPartida.Visible = True
            Me.txtPartida.Focus()
            Return
        End If
        'Guardar.
        'Es posible que pueda confirmar.
        If Sti.Guardar(Me.txtUbicacion.Text, Me.txtProducto.Text, Me.txtCantidad.Text, Me.txtLote.Text, Me.txtPartida.Text) = True Then
            Recomenzar()
            UltimaLectura()
        End If
    End Sub

    Private Sub UltimaLectura()
        Dim msg As String = "", Desc As String = ""
        Desc = Sti.GetDescripcion(Me.UltimoSku)
        msg = "Ultima Lectura: " & Me.UltimoSku & " - " & IIf(Trim(Desc) = "", "PROD.NO ENCONTRADO", Trim(Desc)) & " | Cantidad: " & Me.UltimaCantidad
        Me.lblDescripcion.Text = msg
        Me.lblDescripcion.Visible = True
    End Sub

    Private Sub Recomenzar()
        Dim Ctl As Control
        Dim Ubicacion As String
        Ubicacion = Me.txtUbicacion.Text
        For Each Ctl In Me.Controls
            If TypeOf Ctl Is Button Then
                Ctl.Visible = True
            Else
                Ctl.Visible = False
            End If
            If TypeOf Ctl Is TextBox Then
                Ctl.Text = ""
                Ctl.Enabled = True
            End If
        Next
        Me.lblCliente.Text = "Cliente: " & Sti.RazonSocial
        Me.lblCliente.Visible = True
        Me.lblUbicacion.Visible = True
        Me.txtUbicacion.Text = Ubicacion
        Me.txtUbicacion.Enabled = False
        Me.txtUbicacion.Visible = True
        Me.lblProducto.Visible = True
        Me.txtProducto.Visible = True
        Me.txtProducto.Text = ""
        Me.txtProducto.Focus()
    End Sub

    Private Sub btnComenzar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComenzar.Click
        Comenzar()
    End Sub

    Private Sub Comenzar()
        If Not Me.txtUbicacion.Visible Then
            Me.lblUbicacion.Visible = True
            Me.txtUbicacion.Visible = True
            Me.txtUbicacion.Text = ""
            Me.btnStockPos.Visible = True
            Me.txtUbicacion.Focus()
        End If
    End Sub

    Private Sub txtCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub txtCantidad_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtCantidad.Text.Trim <> "" Then
                Me.UltimaCantidad = Me.txtCantidad.Text
                Confirmar()
            Else
                Me.txtCantidad.Text = ""
                Me.txtCantidad.Focus()
            End If
        End If
    End Sub

    Private Sub txtCantidad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.TextChanged

    End Sub

    Private Sub txtPartida_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPartida.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtPartida.Text.Trim <> "" Then
                Confirmar()
            Else
                Me.txtPartida.Text = ""
                Me.txtPartida.Focus()
            End If
        End If
    End Sub


    Private Sub txtLote_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLote.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtLote.Text.Trim <> "" Then
                Confirmar()
            Else
                Me.txtLote.Text = ""
                Me.txtLote.Focus()
            End If
        End If
    End Sub

    Private Sub txtLote_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLote.TextChanged
        Dim L_nOldPos As Integer = Me.txtLote.SelectionStart
        Me.txtLote.Text = UCase(Me.txtLote.Text)
        Me.txtLote.SelectionStart = L_nOldPos
    End Sub

    Private Sub btnCambiarUbicacion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCambiarUbicacion.Click
        CambiarUbicacion()
    End Sub

    Private Sub CambiarUbicacion()
        Me.txtProducto.Text = ""
        Me.lblDescripcion.Text = ""
        Me.lblAdicionales.Visible = False
        Me.lblLote.Visible = False
        Me.lblPartida.Visible = False
        Me.txtLote.Visible = False
        Me.txtPartida.Visible = False
        Me.txtCantidad.Text = ""
        Me.txtCantidad.Visible = False
        Me.lblCantidad.Visible = False
        Me.txtUbicacion.Enabled = True
        Me.txtUbicacion.Text = ""
        Me.txtUbicacion.Focus()
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Salir()
    End Sub

    Private Sub Salir()
        If MsgBox("¿Desea cancelar la Toma en curso y salir?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnConf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConf.Click
        Reconfigurar()
    End Sub

    Private Sub Reconfigurar()
        Dim St As New frmStockInicialConf
        Dim StT As New frmStockInicialToma
        Try
            St.oStockInicial = Sti
            St.Reconfigurar = True
            St.cmdContinuar.Text = "Continuar..."

            St.cmbClientes.ValueMember = Sti.CodigoCliente

            If Sti.ModoConteo = clsStockInicial.TipoConteo.Cantidad Then
                St.cmdTipoConteo.Text = "Conteo de Cantidades"
            ElseIf Sti.ModoConteo = clsStockInicial.TipoConteo.Etiqueta Then
                St.cmdTipoConteo.Text = "Codigo de Barras"
            End If

            If Sti.ValidaSku = "S" Then
                St.cmbValidarSKU.Text = "Si"
            Else
                St.cmbValidarSKU.Text = "No"
            End If
            St.ShowDialog()
            If St.ComenzarOperacion Then
                Me.Sti = St.oStockInicial
            End If
        Catch ex As Exception
            MsgBox("Toma Stock Inicial: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            St = Nothing
            StT = Nothing
        End Try
    End Sub

    Private Sub txtProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProducto.TextChanged
        Dim L_nOldPos As Integer = Me.txtProducto.SelectionStart
        Me.txtProducto.Text = UCase(Me.txtProducto.Text)
        Me.txtProducto.SelectionStart = L_nOldPos
    End Sub

    Private Sub txtUbicacion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUbicacion.TextChanged
        Dim L_nOldPos As Integer = Me.txtUbicacion.SelectionStart
        Me.txtUbicacion.Text = UCase(Me.txtUbicacion.Text)
        Me.txtUbicacion.SelectionStart = L_nOldPos
    End Sub

    Private Sub btnStockPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStockPos.Click
        ConsultarStock()
    End Sub

    Private Sub ConsultarStock()
        Dim ds As New DataSet
        Dim fStock As New frmStockInicialStockByPos
        Try
            If Me.txtUbicacion.Text.Trim <> "" Then
                Sti.GetStockByPosicion(Me.txtUbicacion.Text, ds)
                If (ds.Tables("Stock").Rows.Count > 0) Then
                    fStock.Ubicacion = Me.txtUbicacion.Text
                    fStock.oStockInicial = Sti
                    fStock.Informacion = ds
                    fStock.ShowDialog()
                Else
                    MsgBox("La ubicacion seleccionada no tiene conteos realizados.", MsgBoxStyle.Information, FrmName)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            fStock = Nothing
            ds = Nothing
        End Try
    End Sub

    Private Sub txtPartida_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPartida.TextChanged
        Dim L_nOldPos As Integer = Me.txtPartida.SelectionStart
        Me.txtPartida.Text = UCase(Me.txtPartida.Text)
        Me.txtPartida.SelectionStart = L_nOldPos
    End Sub
End Class