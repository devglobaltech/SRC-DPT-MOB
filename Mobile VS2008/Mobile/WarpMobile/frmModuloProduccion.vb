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

            Case Keys.F3
                Salir()
        End Select
    End Sub

    Private Sub Salir()
        If Me.OperacionEnCurso Then
            If MsgBox("¿Desea cancelar la operacion en curso?", MsgBoxStyle.YesNo, frmName) = MsgBoxResult.Yes Then
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
        Dim Cierra As Boolean = False
        Try
            If OBJ.Get_Tareas_Transferencia(Cierra) Then
                If Not Cierra Then
                    OBJ.LlenarFormulario(Me)
                Else
                    'Cierra la operación.
                    If Cierra Then
                        If Not OBJ.GeneroTareas Then
                            MsgBox("No hay tareas pendientes para el cliente seleccionado.", MsgBoxStyle.Information, frmName)
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
        Try
            Me.lblTitulo.Visible = True
            Me.cmbClientes.Visible = True
            Me.OBJ.GetClientesByUser(Me.cmbClientes)
            Me.cmbClientes.Focus()

            OperacionEnCurso = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

    Private Sub frmModuloProduccion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = System.Windows.Forms.Keys.Up) Then
            'Subir oscilador
            'Subir
        End If
        If (e.KeyCode = System.Windows.Forms.Keys.Down) Then
            'Bajar oscilador
            'Bajar
        End If
        If (e.KeyCode = System.Windows.Forms.Keys.Left) Then
            'Izquierda
        End If
        If (e.KeyCode = System.Windows.Forms.Keys.Right) Then
            'Derecha
        End If
        If (e.KeyCode = System.Windows.Forms.Keys.Enter) Then
            'Entrar
        End If

    End Sub

    Private Sub cmbClientes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClientes.SelectedIndexChanged

    End Sub

    Private Sub txtPalletContenedora_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPalletContenedora.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtPalletContenedora.Text.Trim <> "" Then
                If OBJ.TipoMovimiento = "1" Then
                    If Me.txtPalletContenedora.Text.Trim <> Me.OBJ.Nro_Bulto Then
                        MsgBox("El numero de contenedora escaneado no coincide con el solicitado.", MsgBoxStyle.Information, frmName)
                        Me.txtPalletContenedora.Text = ""
                        Return
                    End If
                ElseIf OBJ.TipoMovimiento = "2" Then
                    If Me.txtPalletContenedora.Text.Trim <> Me.OBJ.Nro_pallet Then
                        MsgBox("El numero de pallet escaneado no coincide con el solicitado.", MsgBoxStyle.Information, frmName)
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
        Dim Cierre As Boolean = False

        If e.KeyValue = 13 Then
            If Me.txtUbicacionOrigen.Text.Trim <> "" Then
                If Me.txtUbicacionOrigen.Text.Trim <> Me.OBJ.UbicacionOrigen Then
                    MsgBox("La ubicacion escaneada no coincide con la solicitada.", MsgBoxStyle.Information, frmName)
                    Me.txtUbicacionOrigen.Text = ""
                    Return
                Else
                    'Todo Ok, asi que tendria que guardar el registro.
                    'y recuperar la siguiente tarea.
                    Me.OBJ.InsertarMovimiento()
                    If Me.OBJ.Get_Tareas_Transferencia(Cierre) Then
                        If Not Cierre Then
                            Me.OBJ.LlenarFormulario(Me)
                        Else
                            If MsgBox("¿Desea finalizar las transferencias?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, frmName) = MsgBoxResult.Yes Then
                                Me.lblZonaPreparacion.Visible = True
                                Me.txtZonaPreparacion.Visible = True
                                Me.txtZonaPreparacion.Text = ""
                                Me.txtZonaPreparacion.Focus()
                                Me.txtUbicacionOrigen.Enabled = False
                            Else
                                Me.txtUbicacionOrigen.Text = ""
                                Me.txtUbicacionOrigen.Focus()
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Salir()
    End Sub

    Private Sub txtZonaPreparacion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtZonaPreparacion.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtZonaPreparacion.Text.Trim <> "" Then
                If OBJ.FinalizarTransferencias(Me.txtZonaPreparacion.Text.Trim.ToUpper) Then
                    Me.InicializarFormulario()
                End If
            Else
                Me.txtZonaPreparacion.Text = ""
                Me.txtZonaPreparacion.Focus()
            End If
        End If
    End Sub

End Class