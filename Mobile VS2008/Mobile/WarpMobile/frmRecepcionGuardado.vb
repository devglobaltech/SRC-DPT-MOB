Imports System.Data
Imports System.Data.SqlClient


Public Class frmRecepcionGuardado

    Private Obj As New clsRecepcionGuardado
    Private frmName As String = "Recepcion y Guardado."

    Private Sub frmRecepcionGuardado_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Comenzar()
            Case Keys.F2
                Finalizar()
            Case Keys.F3
                Contenido()
            Case Keys.F4
                Cancelar()
            Case Keys.F5
                Salir()
        End Select
    End Sub

    Private Sub Salir()
        Dim Msg As String = ""
        Msg = "¿Desea cancelar la recepción actual y salir?"
        If Me.cmbCliente.Visible Then
            If MsgBox(Msg, MsgBoxStyle.YesNo, frmName) = MsgBoxResult.Yes Then
                If Me.txtNroPallet.Text.Trim <> "" Then
                    Obj.EliminarPallet(Me.txtNroPallet.Text)
                End If
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub Comenzar()
        If Me.cmbCliente.Visible = False Then
            Me.lblCliente.Visible = True
            Obj.GetClientes(Me.cmbCliente, vUsr.CodUsuario)
            Me.cmbCliente.Visible = True
            Me.cmbCliente.Focus()
        End If
    End Sub

    Private Sub Finalizar()
        Dim Msg As String = "¿Desea realizar el guardado del pallet " & Me.txtNroPallet.Text & "?"
        Dim frm As New frmRecepcionGuardadoUbicacion
        Dim MiOC As String = ""
        Dim MiCliente As String = ""
        Try

            If Not Me.cmbCliente.Visible Then
                'SI ESTO NO ESTA VISIBLE NO HAY UNA OPERACION EN CURSO.
                Exit Sub
            End If

            If Me.txtNroPallet.Text.Trim = "" Then
                Exit Sub
            End If

            If MsgBox(Msg, MsgBoxStyle.YesNo, frmName) = MsgBoxResult.Yes Then

                If Obj.CrearDocumentoIngreso(Me.txtNroPallet.Text) Then

                    frm.ObjRecepcionGuardado = Obj

                    frm.Cliente = Me.cmbCliente.SelectedValue

                    If Me.txtOC.Text <> "" Then
                        frm.OrdenDeCompra = Me.txtOC.Text
                    Else
                        frm.OrdenDeCompra = "N/A"
                    End If

                    frm.NroPallet = Me.txtNroPallet.Text

                    frm.ShowDialog()

                    MiCliente = Me.cmbCliente.SelectedValue
                    MiOC = Me.txtOC.Text

                    InicializarControles()

                    Me.cmbCliente.Visible = True
                    Me.cmbCliente.SelectedValue = MiCliente

                    Me.Obj.Cliente_ID = Me.cmbCliente.SelectedValue
                    Me.cmbCliente.Enabled = False
                    Me.lblCliente.Visible = True
                    If Me.Obj.ValidacionContraOC() Then

                        If Obj.FinOC(MiOC) Then
                            MsgBox("Se completo el ingreso de la OC " & MiOC & ".", MsgBoxStyle.Information, frmName)
                            Me.InicializarControles()
                            Exit Try
                        End If
                        'Valida contra la Orden de Compra
                        Me.cmbCliente.Enabled = False
                        Me.lblOC.Visible = True
                        Me.txtOC.Visible = True
                        Me.txtOC.Text = MiOC
                        Me.txtOC.ReadOnly = True
                        Obj.ManejoPallet(Me.txtNroPallet)
                        If Me.txtNroPallet.Text.Trim <> "" Then
                            Me.txtNroPallet.Visible = True
                            Me.lblNroPallet.Visible = True
                            Me.txtNroPallet.ReadOnly = True
                            Me.lblProducto.Visible = True
                            Me.txtProducto.Visible = True
                            Me.txtProducto.Focus()
                        Else
                            Me.txtNroPallet.Text = ""
                            Me.txtNroPallet.ReadOnly = False
                            Me.txtNroPallet.Visible = True
                            Me.lblNroPallet.Visible = True
                            Me.txtNroPallet.Focus()
                        End If

                    Else
                        Obj.ManejoPallet(Me.txtNroPallet)
                        If Me.txtNroPallet.Text.Trim <> "" Then
                            Me.txtNroPallet.Visible = True
                            Me.lblNroPallet.Visible = True
                            Me.txtNroPallet.ReadOnly = True
                            Me.lblProducto.Visible = True
                            Me.txtProducto.Visible = True
                            Me.txtProducto.Focus()
                        Else
                            Me.txtNroPallet.Text = ""
                            Me.txtNroPallet.ReadOnly = False
                            Me.txtNroPallet.Visible = True
                            Me.lblNroPallet.Visible = True
                            Me.txtNroPallet.Focus()
                        End If
                    End If

   
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

    Private Sub Contenido()
        Dim fr As New frmRecepcionGuardadoContenido, DS As New DataSet
        Try
            If Me.cmbCliente.Visible Then
                If Trim(Me.txtNroPallet.Text) <> "" Then

                    If Not Obj.ContenidoPallet(Me.txtNroPallet.Text, DS) Then
                        MsgBox("No se encontraron datos.", MsgBoxStyle.Information, frmName)
                        Exit Try
                    End If

                    fr.NroPallet = Me.txtNroPallet.Text
                    fr.ObjRecepcion = Obj
                    fr.ShowDialog()
                Else
                    If Not Me.txtNroPallet.ReadOnly Then
                        Me.txtNroPallet.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        Finally
            DS.Dispose()
            fr.Dispose()
        End Try
    End Sub

    Private Sub Cancelar()
        Dim Msg As String = "¿Desea cancelar la operacion en curso?"

        If Me.cmbCliente.Visible Then
            If MsgBox(Msg, MsgBoxStyle.YesNo, frmName) = MsgBoxResult.Yes Then
                If Me.txtNroPallet.Text.Trim <> "" Then
                    Obj.EliminarPallet(Me.txtNroPallet.Text)
                End If
                Me.InicializarControles()
            End If
        End If
    End Sub

    Private Sub InicializarObjeto()
        Try
            Obj.Database = SQLc
            Obj.Inicializar()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

    Private Sub frmRecepcionGuardado_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InicializarObjeto()
        InicializarControles()
    End Sub

    Private Sub InicializarControles()
        Try
            Dim ctrl As Control
            For Each ctrl In Me.Controls

                If (ctrl.GetType() Is GetType(TextBox)) Then
                    Dim txt As TextBox = CType(ctrl, TextBox)
                    txt.Text = ""
                    txt.Enabled = True
                    txt.ReadOnly = False
                End If

                If (ctrl.GetType() Is GetType(ComboBox)) Then
                    Dim cbobx As ComboBox = CType(ctrl, ComboBox)
                    cbobx.SelectedIndex = -1
                    cbobx.Enabled = True
                    cbobx.Visible = False
                End If

                If (ctrl.GetType() Is GetType(Label)) Then
                    Dim clbl As Label = CType(ctrl, Label)
                    clbl.Visible = False
                End If

                If Not (ctrl.GetType() Is GetType(Button)) Then
                    'Sino es un boton no se muestra
                    ctrl.Visible = False
                Else
                    ctrl.Visible = True
                    ctrl.Enabled = True
                End If

            Next
            Obj.Inicializar()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try

    End Sub

    Private Sub btnComenzar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComenzar.Click
        Comenzar()
    End Sub

    Private Sub cmbCliente_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCliente.KeyUp
        If e.KeyValue = 13 Then
            If Me.cmbCliente.SelectedValue <> "" Then
                Me.Obj.Cliente_ID = Me.cmbCliente.SelectedValue
                Me.cmbCliente.Enabled = False
                If Me.Obj.ValidacionContraOC() Then
                    'Valida contra la Orden de Compra
                    Me.lblOC.Visible = True
                    Me.txtOC.Visible = True
                    Me.txtOC.Focus()
                Else
                    Obj.ManejoPallet(Me.txtNroPallet)
                    If Me.txtNroPallet.Text.Trim <> "" Then
                        Me.txtNroPallet.Visible = True
                        Me.lblNroPallet.Visible = True
                        Me.txtNroPallet.ReadOnly = True
                        Me.lblProducto.Visible = True
                        Me.txtProducto.Visible = True
                        Me.txtProducto.Focus()
                    Else
                        Me.txtNroPallet.Text = ""
                        Me.txtNroPallet.ReadOnly = False
                        Me.txtNroPallet.Visible = True
                        Me.lblNroPallet.Visible = True
                        Me.txtNroPallet.Focus()
                    End If
                End If
            Else
                MsgBox("Debe seleccionar un cliente", MsgBoxStyle.Information, frmName)
                Me.cmbCliente.Focus()
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtOC_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOC.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtOC.Text.Trim <> "" Then
                If Obj.ValidarOC(Me.txtOC.Text.Trim.ToUpper) Then
                    'Oc Valida, solicito el producto.
                    Me.txtOC.ReadOnly = True
                    Obj.ManejoPallet(Me.txtNroPallet)
                    If Me.txtNroPallet.Text.Trim <> "" Then
                        Me.txtNroPallet.Visible = True
                        Me.lblNroPallet.Visible = True
                        Me.txtNroPallet.ReadOnly = True
                        Me.lblProducto.Visible = True
                        Me.txtProducto.Visible = True
                        Me.txtProducto.Focus()
                    Else
                        Me.txtNroPallet.Text = ""
                        Me.txtNroPallet.ReadOnly = False
                        Me.txtNroPallet.Visible = True
                        Me.lblNroPallet.Visible = True
                        Me.txtNroPallet.Focus()
                    End If
                Else
                    MsgBox("No se encontro la orden de compra o bien fue recibida en su totalidad.", MsgBoxStyle.OkOnly, frmName)
                    Me.txtOC.Text = ""
                    Me.txtOC.Focus()
                End If
            Else
                Me.txtOC.Text = ""
                Me.txtOC.Focus()
            End If
        End If
    End Sub

    Private Sub txtProducto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProducto.KeyUp
        Dim Inact As Boolean = False
        If e.KeyValue = 13 Then
            If Me.txtProducto.Text <> "" Then
                Me.txtProducto.Text = UCase(Replace(Me.txtProducto.Text, "'", ""))

                ProductoInhabilidato(Me.cmbCliente.SelectedValue, Me.txtProducto.Text, Inact)

                If Inact Then
                    MsgBox("El producto se encuentra inhabilitado, no es posible realizar una recepcion del mismo.", MsgBoxStyle.Information, frmName)
                    Me.txtProducto.Text = ""
                    Me.txtProducto.Focus()
                    Exit Sub
                End If

                If Obj.ValidacionContraOC Then
                    Obj.ObtenerConfiguracionProducto(Me.txtProducto.Text.Trim.ToUpper)

                    If Not Obj.ValidaProducto Then
                        MsgBox("El producto ingresado no existe.", MsgBoxStyle.Exclamation, frmName)
                        Me.txtProducto.Text = ""
                        Me.txtProducto.Focus()
                        Exit Sub
                    End If

                    If Obj.ValidarProductoOC(Me.txtOC.Text.Trim.ToUpper, Me.txtProducto.Text.Trim.ToUpper) Then

                        If Obj.CantidadRemanente <= 0 Then
                            MsgBox("No quedan recepciones pendientes para el producto.", MsgBoxStyle.Exclamation, frmName)
                            Me.txtProducto.Text = ""
                            Me.txtProducto.Focus()
                            Exit Sub
                        End If

                        Me.txtProducto.ReadOnly = True
                        Me.lblDescripcion.Text = Obj.GetConfiguracionEspecificaProducto(clsRecepcionGuardado.tblProducto.DESCRIPCION)
                        Me.lblDescripcion.Visible = True
                        If Obj.SolicitaLote Then
                            Me.lblNroLote.Visible = True
                            Me.txtNroLote.Visible = True
                            Me.txtNroLote.ReadOnly = False
                            '------------------------------------------------------------------
                            'Consulto si es lote Automatico
                            '------------------------------------------------------------------
                            If Obj.LoteAutomatico(Me.txtNroLote) Then
                                Me.txtNroLote.ReadOnly = True
                            Else
                                Me.txtNroLote.Focus()
                                Exit Sub
                            End If
                        End If
                        '-------------------------------------------------------------------
                        'Si llega hasta aca es porque no requiere lote.
                        '-------------------------------------------------------------------
                        If Obj.SolicitaPartida Then
                            If Obj.PartidaAutomatica(Me.txtNroPartida) Then
                                Me.txtNroPartida.ReadOnly = True
                                Me.txtNroPartida.Visible = True
                                Me.lblNroPartida.Visible = True
                                Me.lblF_Vto.Visible = True
                                Me.lblEj.Visible = True
                                Me.txtf_vto.Visible = True
                                Me.txtf_vto.Focus()
                            Else
                                Me.txtNroPartida.ReadOnly = False
                                Me.txtNroPartida.Visible = True
                                Me.lblNroPartida.Visible = True
                                Me.txtNroPartida.Focus()
                                Exit Sub
                            End If
                        End If

                        If (Obj.SolicitaVencimiento) Then
                            Me.txtf_vto.Text = ""
                            Me.lblEj.Visible = True
                            Me.lblF_Vto.Visible = True
                            Me.txtf_vto.Visible = True
                            Me.txtf_vto.Focus()
                            Exit Sub
                        Else
                            Me.txtf_vto.Visible = False
                            Me.lblF_Vto.Visible = False
                            Me.txtCantidad.Visible = True
                            Me.lblCantidad.Visible = True
                            Me.txtCantidad.Text = ""
                            Me.txtCantidad.Focus()
                        End If
                    Else
                        Me.txtProducto.Text = ""
                        Me.txtProducto.Focus()
                        e.Handled = True
                    End If
                Else
                    Obj.ObtenerConfiguracionProducto(Me.txtProducto.Text.Trim.ToUpper)
                    If Obj.ExisteProducto Then
                        Me.txtProducto.ReadOnly = True
                        Me.lblDescripcion.Text = Obj.GetConfiguracionEspecificaProducto(clsRecepcionGuardado.tblProducto.DESCRIPCION)
                        Me.lblDescripcion.Visible = True
                        If Obj.SolicitaLote Then
                            Me.lblNroLote.Visible = True
                            Me.txtNroLote.Visible = True
                            Me.txtNroLote.ReadOnly = False
                            '------------------------------------------------------------------
                            'Consulto si es lote Automatico
                            '------------------------------------------------------------------
                            If Obj.LoteAutomatico(Me.txtNroLote) Then
                                Me.txtNroLote.ReadOnly = True
                            Else
                                Me.txtNroLote.Focus()
                                Exit Sub
                            End If
                        End If
                        '-------------------------------------------------------------------
                        'Si llega hasta aca es porque no requiere lote.
                        '-------------------------------------------------------------------
                        If Obj.SolicitaPartida Then
                            If Obj.PartidaAutomatica(Me.txtNroPartida) Then
                                Me.txtNroPartida.ReadOnly = True
                                Me.txtNroPartida.Visible = True
                                Me.lblNroPartida.Visible = True
                            Else
                                Me.txtNroPartida.ReadOnly = False
                                Me.txtNroPartida.Visible = True
                                Me.lblNroPartida.Visible = True
                                Me.txtNroPartida.Focus()
                                Exit Sub
                            End If
                        End If

                        If (Obj.SolicitaVencimiento) Then
                            Me.txtf_vto.Text = ""
                            Me.lblF_Vto.Visible = True
                            Me.lblEj.Visible = True
                            Me.txtf_vto.Visible = True
                            Me.txtf_vto.Focus()
                        Else
                            Me.txtf_vto.Visible = False
                            Me.lblF_Vto.Visible = False
                            Me.txtCantidad.Visible = True
                            Me.lblCantidad.Visible = True
                            Me.txtCantidad.Text = ""
                            Me.txtCantidad.Focus()
                            Exit Sub
                        End If
                    Else
                        MsgBox("El producto solicitado no existe.", MsgBoxStyle.Information, frmName)
                        Me.txtProducto.Text = ""
                        Me.txtProducto.Focus()
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub txtProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProducto.TextChanged

    End Sub

    Protected Overrides Sub Finalize()
        Obj = Nothing
        MyBase.Finalize()
    End Sub

    Private Sub txtOC_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOC.TextChanged

    End Sub

    Private Sub cmbCliente_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCliente.SelectedIndexChanged

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Cancelar()
    End Sub

    Private Sub txtNroPallet_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNroPallet.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtNroPallet.Text.Trim.ToUpper <> "" Then
                Me.txtNroPallet.Text = UCase(Replace(Me.txtNroPallet.Text, "'", ""))
                If Not Obj.ValidarPallet(Me.txtNroPallet.Text.Trim.ToUpper) Then
                    Me.txtProducto.Focus()
                    Me.txtProducto.Text = ""
                    Me.txtProducto.ReadOnly = False
                    Me.txtProducto.Visible = True
                    Me.lblProducto.Visible = True
                    Me.txtOC.ReadOnly = True
                    Me.txtNroPallet.ReadOnly = True
                    Me.txtProducto.Focus()
                Else
                    MsgBox("El numero de pallet ya se encuentra ingresado.", MsgBoxStyle.Information, frmName)
                    Me.txtNroPallet.Text = ""
                    Me.txtNroPallet.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub txtNroPallet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNroPallet.TextChanged

    End Sub

    Private Sub txtNroPartida_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNroPartida.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtNroPartida.Text.Trim <> "" Then
                Me.txtNroPartida.Text = UCase(Replace(Me.txtNroPartida.Text, "'", ""))
                Me.txtNroPartida.ReadOnly = True
                If (Obj.SolicitaVencimiento) Then
                    Me.txtf_vto.Text = ""
                    Me.lblF_Vto.Visible = True
                    Me.lblEj.Visible = True
                    Me.txtf_vto.Visible = True
                    Me.txtf_vto.Focus()
                Else
                    Me.txtCantidad.Visible = True
                    Me.lblCantidad.Visible = True
                    Me.txtCantidad.Text = ""
                    Me.txtCantidad.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub txtNroPartida_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNroPartida.TextChanged

    End Sub

    Private Sub txtNroLote_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNroLote.KeyPress

    End Sub

    Private Sub txtNroLote_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNroLote.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtNroLote.Text.Trim <> "" Then
                Me.txtNroLote.Text = UCase(Replace(Me.txtNroLote.Text, "'", ""))
                Me.txtNroLote.ReadOnly = True
                If Obj.SolicitaPartida Then
                    If Obj.PartidaAutomatica(Me.txtNroPartida) Then
                        Me.txtNroPartida.ReadOnly = True
                        Me.txtNroPartida.Visible = True
                        Me.lblNroPartida.Visible = True
                        Me.lblF_Vto.Visible = True
                        Me.lblEj.Visible = True
                        Me.txtf_vto.Visible = True
                        Me.txtf_vto.Focus()
                    Else
                        Me.txtNroPartida.ReadOnly = False
                        Me.txtNroPartida.Visible = True
                        Me.lblNroPartida.Visible = True
                        Me.txtNroPartida.Focus()
                    End If
                Else
                    If Obj.SolicitaVencimiento Then
                        Me.txtf_vto.Text = ""
                        Me.lblF_Vto.Visible = True
                        Me.lblEj.Visible = True
                        Me.txtf_vto.Visible = True
                        Me.txtf_vto.Focus()
                        Exit Sub
                    Else
                        Me.txtCantidad.Visible = True
                        Me.txtCantidad.Text = ""
                        Me.lblCantidad.Visible = True
                        Me.txtCantidad.Focus()
                    End If

                End If
            End If
        End If
    End Sub

    Private Sub txtNroLote_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNroLote.TextChanged

    End Sub

    Private Sub txtCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress
        Dim Search As String
        Dim Pos As Integer
        Search = "."
        If Not Obj.GetConfiguracionEspecificaProducto(clsRecepcionGuardado.tblProducto.FRACCIONABLE) = "1" Then
            ValidarCaracterNumerico(e)
        Else
            Pos = InStr(1, Me.txtCantidad.Text, Search)
            If Pos > 0 And Asc(e.KeyChar) <> 46 Then
                If Len(Mid(Me.txtCantidad.Text, Pos + 1, Len(Me.txtCantidad.Text))) >= 5 And Asc(e.KeyChar) <> 8 Then
                    e.Handled = True
                    Me.txtCantidad.Focus()
                End If
            Else
                If Pos <> 0 And (Asc(e.KeyChar) = 46) Then
                    e.Handled = True
                ElseIf Pos = 0 And (Asc(e.KeyChar) = 44) Then
                    e.Handled = True
                ElseIf Pos = 0 And (Asc(e.KeyChar) = 46) Then
                    e.Handled = False
                Else
                    ValidarCaracterNumerico(e)
                End If
            End If
        End If
    End Sub

    Private Sub txtCantidad_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtCantidad.Text.Trim <> "" Then
                If CDbl(Me.txtCantidad.Text) <= 0 Then
                    MsgBox("La cantidad ingresada debe ser mayor a 0", MsgBoxStyle.Exclamation, frmName)
                    Me.txtCantidad.Text = ""
                    Me.txtCantidad.Focus()
                    Exit Sub
                End If
                '---------------------------------------------------------------------------------------------
                'Si va contra una oc, controlo los saldos.
                '---------------------------------------------------------------------------------------------
                If Obj.ValidacionContraOC Then

                    If CDbl(Me.txtCantidad.Text) < Obj.ToleranciaMin Then
                        MsgBox("La cantidad ingresada esta por debajo de la tolerancia minima permitida.", MsgBoxStyle.Exclamation, frmName)
                        Me.txtCantidad.Text = ""
                        Me.txtCantidad.Focus()
                        Exit Sub
                    End If

                    If CDbl(Me.txtCantidad.Text) > Obj.ToleranciaMax Then
                        MsgBox("La cantidad ingresada esta por encima de la tolerancia maxima permitida.", MsgBoxStyle.Exclamation, frmName)
                        Me.txtCantidad.Text = ""
                        Me.txtCantidad.Focus()
                        Exit Sub
                    End If
                End If
                '---------------------------------------------------------------------------------------------
                'Impacto los registros en un tabla persistente.
                '---------------------------------------------------------------------------------------------
                If Obj.GuardarTemporal(Me.txtOC.Text, Me.txtNroPallet.Text, Me.txtProducto.Text, CDbl(Me.txtCantidad.Text), Me.txtNroLote.Text, Me.txtNroPartida.Text, Me.txtf_vto.Text) Then
                    Me.txtProducto.Text = ""
                    Me.txtProducto.ReadOnly = False
                    Me.lblDescripcion.Text = ""
                    Me.lblDescripcion.Visible = False
                    Me.lblNroLote.Visible = False
                    Me.txtNroLote.Text = ""
                    Me.txtNroLote.Visible = False
                    Me.lblNroPartida.Visible = False
                    Me.txtNroPartida.Text = ""
                    Me.txtNroPartida.ReadOnly = False
                    Me.txtNroPartida.Visible = False
                    Me.lblF_Vto.Visible = False
                    Me.txtf_vto.ReadOnly = False
                    Me.txtf_vto.Text = ""
                    Me.txtf_vto.Visible = False
                    Me.lblCantidad.Visible = False
                    Me.txtCantidad.Text = ""
                    Me.txtCantidad.Visible = False
                    Me.txtProducto.Focus()
                    Me.lblEj.Visible = False
                Else
                    Me.txtCantidad.Text = ""
                    Me.txtCantidad.Focus()
                End If
            Else
                Me.txtCantidad.Text = ""
                Me.txtCantidad.Focus()
            End If
        End If
    End Sub

    Private Sub txtCantidad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.TextChanged

    End Sub

    Private Sub txtf_vto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtf_vto.KeyUp
        Dim miFecha As String
        If e.KeyValue = 8 Then
            Me.txtf_vto.Text = ""
            Me.txtf_vto.Focus()
        End If
        If e.KeyValue = 13 Then
            If Me.txtf_vto.Text <> "" Then
                If Not IsDate(Me.txtf_vto.Text) Then
                    MsgBox("Se ingreso una fecha invalida.")
                    Me.txtf_vto.Text = ""
                    Me.txtf_vto.Focus()
                    Exit Sub
                End If
                miFecha = Format(CDate(Me.txtf_vto.Text), "yyyyMMdd")
                If Not Obj.ValidarFechaVencimiento(miFecha) Then
                    MsgBox("La fecha ingresada tiene un vencimiento menor al configurado en el producto.", MsgBoxStyle.Information, frmName)
                    Me.txtf_vto.Text = ""
                    Return
                End If
            Else
                Me.txtf_vto.Text = ""
                Me.txtf_vto.Focus()
                Exit Sub
            End If
            Me.txtf_vto.ReadOnly = True
            Me.lblCantidad.Visible = True
            Me.txtCantidad.Visible = True
            Me.txtCantidad.Focus()
        End If
    End Sub

    Private Sub txtf_vto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtf_vto.TextChanged

        If Len(Me.txtf_vto.Text) = 2 Then
            Me.txtf_vto.Text = Me.txtf_vto.Text & "/"
            Me.txtf_vto.Select(Len(Me.txtf_vto.Text), Len(Me.txtf_vto.Text))
        End If

        If Len(Me.txtf_vto.Text) = 5 Then
            Me.txtf_vto.Text = Me.txtf_vto.Text & "/"
            Me.txtf_vto.Select(Len(Me.txtf_vto.Text), Len(Me.txtf_vto.Text))
        End If

    End Sub

    Private Sub btnContenido_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContenido.Click
        Me.Contenido()
    End Sub

    Private Sub btnFinalizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinalizar.Click
        Finalizar()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Salir()
    End Sub
End Class