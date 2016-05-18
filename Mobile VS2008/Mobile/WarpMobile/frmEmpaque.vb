Option Explicit On

Imports System.Data

Public Class frmEmpaque

    Private Const FrmName As String = "Empaque."
    Private LastEnter As Date
    Private oEmp As New clsEmpaque

    Private Sub Pendientes()
        Dim f As New frmEmpaquePendientes
        Try
            f.oEMPAQUE = oEmp
            f.ShowDialog()
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            f.Dispose()
        End Try
    End Sub

    Private Sub btnPendientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPendientes.Click
        Pendientes()
    End Sub

    Private Sub frmEmpaque_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Me.Comenzar()
            Case Keys.F2
                Me.cancelar()
            Case Keys.F3
                Me.Salir()
            Case Keys.F4
                Contenedora()
            Case Keys.F5
                Pendientes()
            Case Keys.F6
                Finalizar()
        End Select
    End Sub

    Private Sub frmEmpaque_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oEmp.Conexion = SQLc
        InicializarFormulario()
    End Sub

    Private Sub InicializarFormulario()
        Dim Control As Control
        'limpio el objeto
        oEmp.Limpiar()
        'limpio el form
        Me.btnComenzar.Enabled = True
        For Each Control In Me.Controls
            If (Control.GetType() Is GetType(TextBox)) Then
                Dim txt As TextBox = CType(Control, TextBox)
                txt.Text = ""
            End If
            Control.Visible = False
        Next
        Me.btnComenzar.Visible = True
        Me.btnCancelar.Visible = True
        Me.btnCancelar.Enabled = False
        Me.btnComenzar.Enabled = True
        Me.btnCancelar.Enabled = True
        Me.btnSalir.Visible = True
        Me.btnFinalizar.Visible = False
    End Sub

    Private Sub btnComenzar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComenzar.Click
        Comenzar()
    End Sub

    Private Sub Comenzar()
        Dim frm As New frmEmpaqueConfirmacionCarros
        If Me.btnComenzar.Enabled Then
            frm.oEmp = Me.oEmp
            frm.ShowDialog()
            If frm.Cancelo Then
                cancelar()
                Exit Sub
            End If
            If frm.Empaquetar Then

                Me.btnComenzar.Enabled = False

                ComenzarEmpaquetado()

                If oEmp.VerificarEmpaqueCompletado Then
                    Me.lblProducto.Visible = False
                    Me.txtProducto.Visible = False
                    MsgBox("Se finalizo con el empaquetado. Presione Finalizar para terminar...", MsgBoxStyle.OkOnly, FrmName)
                End If

            End If
        End If
        frm.Dispose()
    End Sub

    Private Sub ComenzarEmpaquetado()
        Try
            Me.lblCodigoOla.Visible = True
            Me.txtCodigoOla.Visible = True
            Me.txtCodigoOla.Text = oEmp.CodigoDeOla
            Me.txtCodigoOla.Enabled = False
            Me.lblContenedoras.Visible = True
            Me.lblContenedoras.Text = "C.Picking: " & oEmp.ContenedorasDesconsolidacion
            Me.lblProducto.Visible = True
            Me.txtProducto.Visible = True
            Me.txtProducto.Focus()
            Me.btnContenedoras.Visible = True
            Me.btnPendientes.Visible = True
            Me.btnFinalizar.Visible = True
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub cancelar(Optional ByVal CierraPantalla As Boolean = False)
        Dim Msg As String = "¿Desea cancelar la operación actual?"
        If Me.oEmp.CantidadContenedores > 0 Then

            If MsgBox(Msg, MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then

                If Me.lblDescripcion.Visible Then oEmp.LiberarTarea()

                Me.InicializarFormulario()

                If CierraPantalla Then

                    Me.Close()

                End If

            End If

        Else
            Me.InicializarFormulario()
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        cancelar()
    End Sub

    Private Sub txtProducto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProducto.KeyPress

    End Sub

    Private Sub txtProducto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProducto.KeyUp
        If e.KeyValue = 13 Then
            BuscarDescripcionProducto()
        End If
    End Sub



    Private Sub BuscarDescripcionProducto()
        Dim oControl As Control, f As New frmEmpaqueConfirmacionCarro
        Try
            If Trim(Me.txtProducto.Text) <> "" Then
                If oEmp.BuscarDescripcion(Me.lblDescripcion, Me.txtProducto) Then
                    f.oEMPAQUE = oEmp
                    f.ShowDialog()
                    If Not f.VALIDACION_OK Then
                        Me.lblNroLote.Visible = False
                        Me.txtNroLote.Text = ""
                        Me.txtNroLote.Visible = False
                        Me.lblNroPartida.Visible = False
                        Me.txtPartida.Text = ""
                        Me.txtPartida.Visible = False
                        Me.lblCantidad.Visible = False
                        Me.txtCantidad.Text = ""
                        Me.txtCantidad.Visible = False
                        Me.lblDescripcion.Text = ""
                        Me.lblDescripcion.Visible = False
                        Me.txtProducto.Text = ""
                        Me.txtProducto.Focus()
                        Me.btnContenedoras.Visible = True
                        Me.lblNroSerie.Visible = False
                        Me.txtSerie.Text = ""
                        Me.txtSerie.Visible = False
                        Exit Try
                    End If

                    If o2D.QtySeries > 0 Then
                        If oEmp.RegistrarProductoEnContenedora2D() Then
                            Me.lblNroLote.Visible = False
                            Me.txtNroLote.Text = ""
                            Me.txtNroLote.Visible = False
                            Me.lblNroPartida.Visible = False
                            Me.txtPartida.Text = ""
                            Me.txtPartida.Visible = False
                            Me.lblCantidad.Visible = False
                            Me.txtCantidad.Text = ""
                            Me.txtCantidad.Visible = False
                            Me.lblDescripcion.Text = ""
                            Me.lblDescripcion.Visible = False
                            Me.txtProducto.Text = ""
                            Me.txtProducto.Focus()
                            Me.btnContenedoras.Visible = True
                            Me.lblNroSerie.Visible = False
                            Me.txtSerie.Text = ""
                            Me.txtSerie.Visible = False
                            If oEmp.EmpaquetadoCompleto Then
                                Me.InicializarFormulario()
                            End If
                        Else
                            Me.txtProducto.Text = ""
                        End If
                        Return
                    End If

                    Me.lblDescripcion.Visible = True
                    If (oEmp.SolicitaLotePartida) And (oEmp.NumeroPartida <> "" Or oEmp.NumeroLote <> "" Or oEmp.NRO_SERIE <> "") Then

                        If oEmp.NumeroLote <> "" Then
                            oControl = Me.txtNroLote
                            Me.lblNroLote.Visible = True
                            Me.txtNroLote.Visible = True
                            Me.txtNroLote.Text = ""
                        End If

                        If oEmp.NumeroPartida <> "" Then
                            Me.lblNroPartida.Visible = True
                            Me.txtPartida.Visible = True
                            Me.txtPartida.Text = ""
                            If IsNothing(oControl) Then
                                oControl = Me.txtPartida
                            End If
                        End If

                        If oEmp.NRO_SERIE <> "" Then
                            Me.lblNroSerie.Visible = True
                            Me.txtSerie.Visible = True
                            Me.txtSerie.Text = ""
                            If IsNothing(oControl) Then
                                oControl = Me.txtSerie
                            End If
                        End If

                        oControl.Focus()
                    Else
                        Me.lblCantidad.Visible = True
                        Me.txtCantidad.Visible = True
                        Me.txtCantidad.Focus()
                    End If
                Else : Me.txtProducto.Text = ""
                    Me.txtProducto.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            f.Dispose()
        End Try
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Salir()
    End Sub

    Private Sub Salir()
        Try
            Me.cancelar(True)
            Me.Close()
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub txtNroLote_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNroLote.KeyUp
        If (e.KeyValue = 13) Then
            If VALIDAR_LOTE() Then
                If oEmp.NumeroPartida <> "" Then
                    Me.lblNroPartida.Visible = True
                    Me.txtPartida.Visible = True
                Else
                    Me.lblCantidad.Visible = True
                    Me.txtCantidad.Visible = True
                    Me.txtCantidad.Focus()
                End If
            End If
        End If
    End Sub

    Private Function VALIDAR_LOTE() As Boolean
        Try
            If oEmp.NumeroLote <> Trim(Me.txtNroLote.Text) Then
                MsgBox("El lote seleccionado no es correcto.", MsgBoxStyle.OkCancel, FrmName)
                Return False
            End If
            Return True
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally

        End Try
    End Function

    Private Sub txtPartida_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPartida.KeyUp
        If ((e.KeyValue = 13) And (Trim(Me.txtPartida.Text) <> "")) Then
            If VALIDAR_PARTIDA() Then
                Me.lblCantidad.Visible = True
                Me.txtCantidad.Visible = True
                Me.txtCantidad.Focus()
            Else
                Me.txtPartida.Text = ""
                Me.txtPartida.Focus()
            End If
        End If
    End Sub

    Private Function VALIDAR_PARTIDA() As Boolean
        Try
            If oEmp.NumeroPartida <> Trim(Me.txtPartida.Text) Then
                MsgBox("La partida seleccionada no es correcta.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally

        End Try
    End Function

    Private Sub txtCantidad_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyDown

    End Sub

    Private Sub txtCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress

    End Sub

    Private Sub txtCantidad_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyUp
        If e.KeyValue = 13 Then
            If Trim(Me.txtCantidad.Text) <> "" Then
                ConfirmarCantidad()

            End If
        End If
    End Sub

    Private Sub ConfirmarCantidad()
        Try
            If oEmp.ConfirmarCantidad(CDbl(Me.txtCantidad.Text)) Then
                Me.lblNroLote.Visible = False
                Me.txtNroLote.Text = ""
                Me.txtNroLote.Visible = False
                Me.lblNroPartida.Visible = False
                Me.txtPartida.Text = ""
                Me.txtPartida.Visible = False
                Me.lblCantidad.Visible = False
                Me.txtCantidad.Text = ""
                Me.txtCantidad.Visible = False
                Me.lblDescripcion.Text = ""
                Me.lblDescripcion.Visible = False
                Me.txtProducto.Text = ""
                Me.txtProducto.Focus()
                Me.btnContenedoras.Visible = True
                Me.lblNroSerie.Visible = False
                Me.txtSerie.Text = ""
                Me.txtSerie.Visible = False

                If oEmp.VerificarEmpaqueCompletado Then
                    Me.lblProducto.Visible = False
                    Me.txtProducto.Visible = False
                    MsgBox("Se finalizo con el empaquetado. Presione Finalizar para terminar...", MsgBoxStyle.OkOnly, FrmName)
                End If

            Else
                Me.txtCantidad.Text = ""
                Me.txtCantidad.Focus()
            End If

        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub cmdCerrarContenedora_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Contenedora()
    End Sub

    Private Sub Contenedora()
        Dim C As New frmEmpaqueContenedores
        Try
            C.OEMPAQUE = oEmp
            C.ShowDialog()

            If oEmp.VerificarEmpaqueCompletado Then
                Me.lblProducto.Visible = False
                Me.txtProducto.Visible = False
            Else
                Me.lblProducto.Visible = True
                Me.txtProducto.Visible = True
            End If

        Catch ex As Exception
            MsgBox("Excepcion: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            C.Dispose()
        End Try
    End Sub

    Private Sub btnContenedoras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContenedoras.Click
        Contenedora()
    End Sub

    Private Sub txtProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProducto.TextChanged

    End Sub

    Private Sub txtCantidad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.TextChanged

    End Sub

    Private Sub txtSerie_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSerie.KeyUp
        If e.KeyValue = 13 Then
            PRE_VALIDAR_SERIE()
        End If
    End Sub

    Private Sub txtSerie_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSerie.TextChanged

    End Sub

    Private Sub PRE_VALIDAR_SERIE()
        Try

            If Trim(Me.txtSerie.Text) = "" Then
                Exit Sub
            End If

            If oEmp.VALIDAR_SERIE(Me.txtSerie.Text) Then
                Me.txtCantidad.Text = 1
                ConfirmarCantidad()
            Else
                Me.txtSerie.Text = ""
                Me.txtSerie.Focus()
            End If

        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub btnFinalizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinalizar.Click
        Finalizar()
    End Sub

    Private Sub Finalizar()
        Try
            If Me.btnFinalizar.Visible Then
                If oEmp.EmpaquetadoCompleto Then
                    Me.InicializarFormulario()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub
End Class