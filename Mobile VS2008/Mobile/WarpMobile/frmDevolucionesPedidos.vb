Imports System.Data
Imports System.Data.SqlClient

Public Class frmDevolucionesPedidos

    Private Const FrmName As String = "Devoluciones"
    Dim oDevo As New cDevoluciones

    Private Sub frmDevolucionesPedidos_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Me.Inicializar()
            Case Keys.F2
                Me.CancelarOperacion()
            Case Keys.F3
                FinalizarPallet()
            Case Keys.F4
                VerContenido()
            Case Keys.F5
                Salir()
        End Select
    End Sub

    Private Sub frmDevolucionesPedidos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDevo.Database = SQLc
        Inicializar_Formulario()
    End Sub

    Private Sub Inicializar_Formulario()
        Try
            Dim ctrl As Control
            For Each ctrl In Me.Controls

                If (ctrl.GetType() Is GetType(TextBox)) Then
                    Dim txt As TextBox = CType(ctrl, TextBox)
                    txt.Text = ""
                    txt.Enabled = True
                End If

                If (ctrl.GetType() Is GetType(ComboBox)) Then
                    Dim cbobx As ComboBox = CType(ctrl, ComboBox)
                    cbobx.SelectedIndex = -1
                    cbobx.Enabled = True
                End If

                If (ctrl.GetType() Is GetType(Label)) Then
                    Dim clbl As Label = CType(ctrl, Label)
                End If

                If Not (ctrl.GetType() Is GetType(Button)) Then
                    ctrl.Visible = False
                Else
                    ctrl.Visible = True
                    ctrl.Enabled = True
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        oDevo = Nothing
        MyBase.Finalize()
    End Sub

    Private Sub btnComenzar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComenzar.Click
        Inicializar()
    End Sub

    Private Sub Inicializar()
        Try
            If Not Me.cmbCliente.Visible Then
                oDevo.Inicializar()
                If oDevo.GetClientes(Me.cmbCliente, vUsr.CodUsuario) Then
                    Me.lblPallet.Text = "Pallet Devolución " & oDevo.PalletDevolucion
                    oDevo.GetMotivos(Me.cmbMotivos)
                    Me.lblPallet.Visible = True
                    Me.lblCliente.Visible = True
                    Me.cmbCliente.Visible = True
                    Me.cmbCliente.Focus()
                    Me.btnComenzar.Enabled = False
                Else
                    MsgBox("Ocurrio un error al recuperar los clientes para el usuario.", MsgBoxStyle.Information, FrmName)
                End If
            Else
                MsgBox("Ya hay una operacion en curso.", MsgBoxStyle.Information, FrmName)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        End Try
    End Sub

    Private Sub CancelarOperacion()
        Try
            If Me.cmbCliente.Visible = True Then
                If Me.txtProducto.Text <> "" Then
                    If MsgBox("¿Desea cancelar el producto ingresado?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                        Me.NuevaTarea()
                    End If
                Else
                    If MsgBox("¿Desea cancelar el pallet completo?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                        oDevo.CancelarPallet()
                        Me.Inicializar_Formulario()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        End Try
    End Sub

    Private Sub FinalizarPallet()
        Dim Msg As String = "¿Desea confirmar el pallet de devolucion?"
        Try
            If MsgBox(Msg, MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then

                If oDevo.FinalizarDevolucion Then
                    Me.Inicializar_Formulario()
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        End Try
    End Sub

    Private Sub VerContenido()
        Dim frm As New frmDevolucionesContenido
        Try
            If Me.cmbCliente.Visible Then
                frm.oDEVOLUCION = oDevo
                frm.ShowDialog()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            frm.Dispose()
        End Try
    End Sub

    
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        CancelarOperacion()
    End Sub

    Private Sub btnFinalizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinalizar.Click
        FinalizarPallet()
    End Sub

    Private Sub btnVerSeries_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerContenido.Click
        VerContenido()
    End Sub

    Private Sub cmbCliente_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCliente.KeyUp
        If e.KeyValue = 13 Then
            If Me.cmbCliente.SelectedValue <> "" Then
                Me.lblProducto.Visible = True
                Me.txtProducto.Visible = True
                oDevo.Cliente_ID = Me.cmbCliente.SelectedValue
                Me.cmbCliente.Enabled = False
                Me.txtProducto.Focus()
            End If
        End If
    End Sub

    Private Sub txtProducto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProducto.KeyUp
        If e.KeyValue = 13 Then
            If Trim(Me.txtProducto.Text) <> "" Then
                If oDevo.BuscarProducto(Trim(Me.txtProducto.Text), Me.lblDescripcion) Then
                    Me.lblDescripcion.Visible = True
                    Me.txtProducto.Enabled = False
                    Me.lblMotivo.Visible = True
                    Me.cmbMotivos.Visible = True
                    Me.cmbMotivos.Focus()
                Else
                    Me.txtProducto.Text = ""
                    Me.txtProducto.Focus()
                End If
            Else
                Me.txtProducto.Text = ""
                Me.txtProducto.Focus()
            End If
        End If
    End Sub

    Private Sub txtProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProducto.TextChanged

    End Sub

    Private Sub cmbMotivos_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbMotivos.KeyUp
        If e.KeyValue = 13 Then
            If Me.cmbMotivos.SelectedValue <> "" Then
                Me.lblObservaciones.Visible = True
                Me.txtObservaciones.Visible = True
                Me.txtObservaciones.Focus()
            End If
        End If
    End Sub

    Private Sub txtObservaciones_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtObservaciones.KeyUp
        If e.KeyValue = 13 Then
            GuardarRegistro()
        End If
    End Sub

    Private Sub GuardarRegistro()
        Try
            Dim Msg As String = "¿Desea confirmar la tarea?"
            If MsgBox(Msg, MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                If oDevo.GuardarDevolucion(Me.cmbMotivos.SelectedValue, Me.txtObservaciones.Text) Then
                    NuevaTarea()
                End If
            Else
                Me.txtObservaciones.Text = ""
                Me.btnFinalizar.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        End Try
    End Sub

    Private Sub NuevaTarea()
        Me.txtObservaciones.Text = ""
        Me.txtObservaciones.Visible = False
        Me.lblObservaciones.Visible = False
        Me.cmbMotivos.Visible = False
        Me.lblMotivo.Visible = False
        Me.lblDescripcion.Visible = False
        Me.txtProducto.Text = ""
        Me.txtProducto.Focus()
        Me.txtProducto.Enabled = True
    End Sub

    Private Sub cmbMotivos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMotivos.SelectedIndexChanged

    End Sub

    Private Sub txtObservaciones_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtObservaciones.TextChanged

    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Salir()
    End Sub

    Private Sub Salir()
        If Me.cmbCliente.Visible Then
            If MsgBox("¿Desea cancelar la operacion en curso y salir?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                oDevo.CancelarPallet()
                Me.Inicializar_Formulario()
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub
End Class