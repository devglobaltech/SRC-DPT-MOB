Imports System.Data

Public Class frmModuloProduccion

    Private OBJ As cModuloProduccion
    Private Const SQLConErr As String = "Fallo al intentar conectar con la base de datos."
    Private Const frmName As String = "Modulo de ensamble"
    Private OperacionEnCurso As Boolean = False

    Public Property TipoMovimiento() As String
        Get
            Return OBJ.TipoMovimiento
        End Get
        Set(ByVal value As String)
            OBJ.TipoMovimiento = value
        End Set
    End Property

    Private Sub frmModuloProduccion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Me.Comenzar()
            Case Keys.F2
                Cancelar()
            Case Keys.F3
                Salir()
        End Select
    End Sub

    Private Sub Salir()
        Dim strSalir As String = "¿Desea cancelar la operacion en curso?"
        If Me.OperacionEnCurso Then
            If MsgBox(strSalir, MsgBoxStyle.YesNo, frmName) = MsgBoxResult.Yes Then
                OBJ.CancelarMovimientos()
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub frmModuloProduccion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InicializarFormulario()
    End Sub

    Private Sub InicializarFormulario()

        Dim Ctrl As Control
        Try
            For Each Ctrl In Me.Controls
                'Textbox
                If (Ctrl.GetType() Is GetType(TextBox)) Then
                    Dim txt As TextBox = CType(Ctrl, TextBox)
                    txt.Enabled = True
                    txt.Text = ""
                    txt.Visible = False
                    txt.Enabled = True
                End If

                If (Ctrl.GetType() Is GetType(Label)) Then
                    Dim lbl As Label = CType(Ctrl, Label)
                    lbl.Visible = False
                End If

                If (Ctrl.GetType() Is GetType(ListBox)) Then
                    Dim LstF As ListBox = CType(Ctrl, ListBox)
                    LstF.Items.Clear()
                    LstF.Visible = False
                End If

                If (Ctrl.GetType() Is GetType(Button)) Then
                    Dim btn As Button = CType(Ctrl, Button)
                    btn.Enabled = True
                    btn.Visible = True
                End If

                If (Ctrl.GetType() Is GetType(ComboBox)) Then
                    Dim cmb As ComboBox = CType(Ctrl, ComboBox)
                    cmb.Enabled = True
                    cmb.Visible = False
                End If
            Next
            Me.cmbClientes.DataSource = Nothing
            If Not OBJ.GetClientesByUser(Me.cmbClientes) Then Exit Try
            Me.cmbClientes.Visible = False
            OperacionEnCurso = False
        Catch ex As Exception
            MsgBox(ex, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

    Public Sub New()
        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        OBJ = New cModuloProduccion
        OBJ.Conexion = SQLc
        OBJ.Usuario = vUsr.CodUsuario

    End Sub

    Protected Overrides Sub Finalize()
        OBJ = Nothing
        MyBase.Finalize()
    End Sub

    Private Sub cmbClientes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbClientes.KeyUp
        If e.KeyValue = 13 Then
            OBJ.Cliente = Trim(Me.cmbClientes.SelectedValue)
            Me.cmbClientes.Enabled = False
            BuscarTarea()
        End If
    End Sub

    Private Sub BuscarTarea()
        '----------------------------------------------------------------------------------
        Dim Cierra As Boolean = False, Pendientes As Boolean = False, _
            SinTareas As String = "No hay tareas pendientes para el cliente seleccionado."
        '----------------------------------------------------------------------------------
        Try
            '-------------------------------------------------------
            'Si tiene una tarea sin confirmar, la recupera.
            '-------------------------------------------------------
            If OBJ.Get_Tareas_Transferencia_Pendiente(Pendientes) Then
                If Pendientes Then
                    'Lleno el formulario con los pendientes.
                    OBJ.LlenarFormularioPendientes(Me)
                    Exit Sub
                End If
            End If
            '-------------------------------------------------------
            'Como no hubo pendiente sin confirmar, genero una nueva.
            '-------------------------------------------------------
            If OBJ.Get_Tareas_Transferencia(Cierra) Then
                If Not Cierra Then
                    OBJ.LlenarFormulario(Me)
                Else
                    '-------------------------------------------------------
                    'Cierra la operación.
                    '-------------------------------------------------------
                    If Cierra Then
                        If Not OBJ.GeneroTareas Then
                            MsgBox(SinTareas, MsgBoxStyle.Information, frmName)
                            InicializarFormulario()
                            Exit Sub
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

    Private Sub btnComenzar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComenzar.Click
        Comenzar()
    End Sub

    Private Sub Comenzar()
        Dim MsgEnCurso As String = "Hay una operacion en curso."
        Try
            If Not Me.OperacionEnCurso Then
                Me.lblTitulo.Visible = True
                Me.cmbClientes.Visible = True
                Me.OBJ.GetClientesByUser(Me.cmbClientes)
                Me.cmbClientes.Focus()
                OperacionEnCurso = True
            Else
                MsgBox(MsgEnCurso, MsgBoxStyle.Information, frmName)
                If Me.txtPalletContenedora.Visible And Me.txtPalletContenedora.Enabled Then
                    Me.txtPalletContenedora.Focus()
                ElseIf Me.txtUbicacionOrigen.Visible And Me.txtUbicacionOrigen.Enabled Then
                    Me.txtUbicacionOrigen.Focus()
                ElseIf Me.txtZonaPreparacion.Visible And Me.txtZonaPreparacion.Enabled Then
                    Me.txtUbicacionOrigen.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

    Private Sub cmbClientes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClientes.SelectedIndexChanged

    End Sub

    Private Sub txtPalletContenedora_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPalletContenedora.KeyUp
        '----------------------------------------------------------------------------------------------
        Dim ContenedorNOK As String = "El numero de contenedora escaneado no coincide con el solicitado.", _
            PalletNOK As String = "El numero de pallet escaneado no coincide con el solicitado."
        '----------------------------------------------------------------------------------------------
        If e.KeyValue = 13 Then
            If Me.txtPalletContenedora.Text.Trim <> "" Then
                If OBJ.TipoMovimiento = "1" Then
                    If Me.txtPalletContenedora.Text.Trim <> Me.OBJ.Nro_Bulto Then
                        MsgBox(ContenedorNOK, MsgBoxStyle.Information, frmName)
                        Me.txtPalletContenedora.Text = ""
                        Return
                    End If
                ElseIf OBJ.TipoMovimiento = "2" Then
                    If Me.txtPalletContenedora.Text.Trim <> Me.OBJ.Nro_pallet Then
                        MsgBox(PalletNOK, MsgBoxStyle.Information, frmName)
                        Me.txtPalletContenedora.Text = ""
                        Return
                    End If
                End If
                Me.txtPalletContenedora.Enabled = False
                Me.lblUbicacionOrigen.Visible = True
                Me.txtUbicacionOrigen.Visible = True
                Me.txtUbicacionOrigen.Focus()
            End If
        End If
    End Sub

    Private Sub txtUbicacionOrigen_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUbicacionOrigen.KeyUp
        '----------------------------------------------------------------------------------
        Dim Cierre As Boolean = False, _
            UbicacionNOK As String = "La ubicacion escaneada no coincide con la solicitada."
        '----------------------------------------------------------------------------------
        If e.KeyValue = 13 Then
            If Me.txtUbicacionOrigen.Text.Trim <> "" Then
                If Me.txtUbicacionOrigen.Text.Trim <> Me.OBJ.UbicacionOrigen Then
                    MsgBox(UbicacionNOK, MsgBoxStyle.Information, frmName)
                    Me.txtUbicacionOrigen.Text = ""
                    Return
                Else
                    '-------------------------------------------------------
                    'Todo Ok, asi que tendria que guardar el registro.
                    'y recuperar la siguiente tarea.
                    '-------------------------------------------------------
                    Me.OBJ.InsertarMovimiento()
                    Me.lblZonaPreparacion.Visible = True
                    Me.txtZonaPreparacion.Visible = True
                    Me.txtZonaPreparacion.Text = ""
                    Me.txtZonaPreparacion.Focus()
                    Me.txtUbicacionOrigen.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Salir()
    End Sub

    Private Sub txtZonaPreparacion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtZonaPreparacion.KeyUp
        '-------------------------------------------------------
        'Declaracion de Variables.
        '-------------------------------------------------------
        Dim Msg As String = "La nave de preparacion es diferente a la nave configurada." & vbNewLine & "¿Desea continuar?"
        Dim MsgNaveNOK As String = "La nave indicada no es una zona de preparación."
        Dim MsgConfirmacionTransferencia As String = "El movimiento se realizo correctamente."
        Dim Continuar As Boolean = False
        '-------------------------------------------------------
        If e.KeyValue = 13 Then

            If Me.txtZonaPreparacion.Text.Trim = "" Then Exit Sub

            If Me.txtZonaPreparacion.Text.Trim <> OBJ.NavePreparacion Then
                If OBJ.EsNavePreparacion(Me.txtZonaPreparacion.Text.Trim.ToUpper, Continuar) Then
                    If Continuar Then
                        If MsgBox(Msg, MsgBoxStyle.Question + MsgBoxStyle.YesNo, frmName) = MsgBoxResult.No Then
                            Me.txtZonaPreparacion.Text = ""
                            Me.txtZonaPreparacion.Focus()
                            Exit Sub
                        End If
                    Else
                        MsgBox(MsgNaveNOK, MsgBoxStyle.Information, frmName)
                        Me.txtZonaPreparacion.Text = ""
                        Me.txtZonaPreparacion.Focus()
                        Exit Sub
                    End If
                End If
            End If
            '-------------------------------------------------------
            'Comienzo con la confirmacion de la tarea.
            '-------------------------------------------------------
            If OBJ.FinalizarTransferencias(Me.txtZonaPreparacion.Text.Trim.ToUpper) Then
                MsgBox(MsgConfirmacionTransferencia, MsgBoxStyle.Information, frmName)
                Me.ContinuarProceso()
            End If
        End If
    End Sub

    Private Function ContinuarProceso() As Boolean
        '-------------------------------------------------------
        'Declaracion de Variables.
        '-------------------------------------------------------
        Dim CierreForzado As Boolean = False, _
            MovimientoOK As String = "Se completaron los movimientos de la operación "
        '-------------------------------------------------------
        Try
            Me.lblZonaPreparacion.Visible = False
            Me.txtZonaPreparacion.Text = ""
            Me.txtZonaPreparacion.Visible = False
            Me.txtUbicacionOrigen.Text = ""
            Me.txtUbicacionOrigen.Enabled = True
            Me.txtUbicacionOrigen.Visible = False
            Me.txtPalletContenedora.Text = ""
            Me.txtPalletContenedora.Enabled = True
            Me.lblUbicacionOrigen.Visible = False
            Me.lblUbicacionOrigen.Text = "Ubicacion: "

            If OBJ.Get_Tareas_Transferencia(CierreForzado) Then
                If Not CierreForzado Then
                    OBJ.LlenarFormulario(Me)
                Else
                    MsgBox(MovimientoOK, MsgBoxStyle.Information, frmName)
                    Me.InicializarFormulario()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Function

    Private Sub Cancelar()
        Dim CancelarTareas As String = "¿Desea cancelar la tarea en curso?"
        Try
            If OperacionEnCurso Then
                If MsgBox(CancelarTareas, _
                        MsgBoxStyle.Question + MsgBoxStyle.YesNo, frmName) = MsgBoxResult.Yes Then
                    If OBJ.CancelarMovimientos Then
                        Me.InicializarFormulario()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Cancelar()
    End Sub
End Class