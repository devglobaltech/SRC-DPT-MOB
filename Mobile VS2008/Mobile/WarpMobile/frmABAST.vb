Imports System.Data.SqlClient
Imports System.Data

Public Class frmABAST

    Private Const frmName As String = "Abastecimiento"
    Private oAbast As New clsABAST

    Private Sub frmABAST_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                ComenzarProceso()
            Case Keys.F2

            Case Keys.F3
                CerrarForm()
            Case Keys.F4
                DescargarMaterial()
        End Select
    End Sub

    Private Sub CerrarForm()
        Dim EnProgreso As Boolean = False, Msg As String = ""
        Try
            If Me.txtContenedora.Visible = True Then
                EnProgreso = True
            End If

            If EnProgreso Then
                If MsgBox("¿Desea cancelar la tarea en curso y salir?", MsgBoxStyle.YesNo, frmName) = MsgBoxResult.No Then
                    Exit Try
                Else
                    Me.Close()
                End If
            Else
                Me.Close()
            End If

        Catch ex As Exception
            Mensaje(ex)
        End Try
    End Sub

    Private Sub ComenzarProceso()
        Try
            Me.lblTareaCompleta.Visible = False
            lblNroContenedora.Visible = True
            Me.txtContenedora.Visible = True
            Me.bComenzar.Enabled = False
            Me.txtContenedora.Focus()
        Catch ex As Exception
            Mensaje(ex)
        End Try
    End Sub

    Private Sub Mensaje(ByVal ex As Exception)
        MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
    End Sub

    Private Sub frmABAST_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oAbast.Conexion = SQLc
        Inicializacion()
    End Sub

    Private Sub Inicializacion()
        Dim Ctrl As Control
        Try
            For Each Ctrl In Me.Controls
                'Textbox
                If (Ctrl.GetType() Is GetType(TextBox)) Then
                    Dim txt As TextBox = CType(Ctrl, TextBox)
                    txt.Enabled = True
                    txt.Text = ""
                    txt.Visible = False
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
            Next

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        oAbast = Nothing
        MyBase.Finalize()
    End Sub

    Private Sub bSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSalir.Click
        CerrarForm()
    End Sub

    Private Sub txtContenedora_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtContenedora.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub txtContenedora_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtContenedora.KeyUp
        If e.KeyValue = 13 Then
            If Trim(Me.txtContenedora.Text) <> "" Then
                If Not oAbast.ValidarContenedora(Me.txtContenedora.Text) Then
                    MsgBox("La contenedora indicada se encuentra en uso", MsgBoxStyle.Information, frmName)
                    Me.txtContenedora.Text = ""
                    Me.txtContenedora.Focus()
                Else
                    If Not Me.lstInformacion.Visible Then
                        Me.txtContenedora.Enabled = False
                        'Aca me traigo la tarea o la genero.
                        If oAbast.GenerarTareaAbastecimiento(Me.lstInformacion) Then
                            Me.lstInformacion.Visible = True
                            Application.DoEvents()
                            'Envio a buscar el detalle del material.
                            oAbast.DetalleTareaAbastecimiento(Me.lstInformacion, Me.txtContenedora.Text, Me.lblUbicacionOrigen)
                            Me.lblUbicacionOrigen.Visible = True
                            Me.txtUbicacion.Visible = True
                            Me.txtUbicacion.Focus()
                        Else
                            Me.Inicializacion()
                            Exit Sub
                        End If
                    Else
                        If Me.txtConf.Visible Then
                            Me.txtConf.Focus()
                        End If
                        If Me.txtUbicacion.Visible Then
                            Me.txtUbicacion.Focus()
                        End If
                        Me.txtContenedora.Enabled = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub bComenzar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bComenzar.Click
        ComenzarProceso()
    End Sub

    Private Sub txtContenedora_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContenedora.TextChanged

    End Sub

    Private Sub txtUbicacion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUbicacion.KeyUp
        Dim Cierre As Boolean = False
        If e.KeyValue = 13 Then
            If Trim(Me.txtUbicacion.Text) <> "" Then
                'ValidarPosicion_Serie()
                If Not oAbast.ValidarPosicion_Serie(Me.txtUbicacion.Text, Me.txtContenedora.Text) Then
                    If oAbast.ConfirmaPorContenedora Then
                        MsgBox("La contenedora indicada no es la correcta.", MsgBoxStyle.Information, frmName)
                        Me.txtUbicacion.Text = ""
                        Me.txtUbicacion.Focus()
                        Exit Sub
                    End If

                    'No se valido nada.
                    If oAbast.EsSerializado Then
                        MsgBox("La serie indicada no es correcta.", MsgBoxStyle.Information, frmName)
                        Me.txtUbicacion.Text = ""
                        Me.txtUbicacion.Focus()
                        Exit Sub
                    Else
                        MsgBox("La posicion indicada no es correcta.", MsgBoxStyle.Information, frmName)
                        Me.txtUbicacion.Text = ""
                        Me.txtUbicacion.Focus()
                        Exit Sub
                    End If
                Else
                    If Not oAbast.EsSerializado Then
                        If Not oAbast.ConfirmaPorContenedora Then
                            Me.lblConf.Visible = True
                            Me.lblConf.Text = "Confirme la cantidad"
                            Me.txtConf.Visible = True
                            Me.txtConf.Focus()
                        Else
                            If oAbast.ConfirmarRetiroOrigen(0, Me.txtContenedora.Text, Cierre) Then
                                Me.txtUbicacion.Enabled = True
                                oAbast.DetalleTareaAbastecimiento(Me.lstInformacion, Me.txtContenedora.Text, Me.lblUbicacionOrigen)
                                If Not oAbast.CierreForzado Then
                                    Me.txtConf.Visible = False
                                    Me.txtConf.Text = ""
                                    Me.lblConf.Visible = False
                                    Me.txtUbicacion.Text = ""
                                    Me.txtUbicacion.Focus()
                                Else
                                    lblTareaCompleta.Visible = True
                                    Me.Inicializacion()
                                    lblTareaCompleta.Text = "Se completo la tarea de abastecimiento"
                                    Me.lblTareaCompleta.Visible = True
                                End If
                            Else
                                If Cierre Then
                                    MsgBox("La tarea de abastecimiento fue cancelada.", MsgBoxStyle.Information, frmName)
                                    Me.Inicializacion()
                                End If
                            End If
                        End If
                    Else
                        'tiene serie, a confirmar.
                        If oAbast.ConfirmarRetiroOrigen(1, Me.txtContenedora.Text, Cierre) Then
                            Me.txtUbicacion.Enabled = True
                            oAbast.DetalleTareaAbastecimiento(Me.lstInformacion, Me.txtContenedora.Text, Me.lblUbicacionOrigen)
                            If Not oAbast.CierreForzado Then
                                Me.txtConf.Visible = False
                                Me.txtConf.Text = ""
                                Me.lblConf.Visible = False
                                Me.txtUbicacion.Text = ""
                                Me.txtUbicacion.Focus()
                            Else
                                Me.Inicializacion()
                                lblTareaCompleta.Visible = True
                                lblTareaCompleta.Text = "Se completo la tarea de abastecimiento"
                            End If
                        Else
                        If Cierre Then
                            MsgBox("La tarea de abastecimiento fue cancelada.", MsgBoxStyle.Information, frmName)
                            Me.Inicializacion()
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtConf_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtConf.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub txtConf_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtConf.KeyUp
        Dim Cierre As Boolean = False
        If e.KeyValue = 13 Then
            If Trim(Me.txtConf.Text) <> "" Then

                If Not oAbast.ConfirmaPorContenedora And Not oAbast.SERIALIZADO Then
                    If CDbl(Me.txtConf.Text) > oAbast.CANT_POS_OR Then
                        Me.txtConf.Text = ""
                        MsgBox("La cantidad confirmada no puede superar a la cantidad solicitada", MsgBoxStyle.Information, frmName)
                        Me.txtConf.Focus()
                        Return
                    End If
                End If

                If oAbast.ConfirmarRetiroOrigen(Me.txtConf.Text, Me.txtContenedora.Text, Cierre) Then
                    Me.txtUbicacion.Enabled = True
                    oAbast.DetalleTareaAbastecimiento(Me.lstInformacion, Me.txtContenedora.Text, Me.lblUbicacionOrigen)
                    If Not oAbast.CierreForzado Then
                        Me.txtConf.Visible = False
                        Me.txtConf.Text = ""
                        Me.lblConf.Visible = False
                        Me.txtUbicacion.Text = ""
                        Me.txtUbicacion.Focus()
                    Else
                        Inicializacion()
                        lblTareaCompleta.Visible = True
                        lblTareaCompleta.Text = "Se completo la tarea de abastecimiento"
                    End If
                End If
            Else
                If Cierre Then
                    MsgBox("La tarea de abastecimiento fue cancelada.", MsgBoxStyle.Information, frmName)
                    Inicializacion()
                Else
                    Me.txtConf.Text = ""
                    Me.txtConf.Focus()
                End If
            End If
        End If
    End Sub

    Public Sub ValidarCaracterNumerico(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        Try
            'Valida que el caracter ingreado sea un nro
            If (Asc(e.KeyChar) >= 32 And Asc(e.KeyChar) <= 47) Or Asc(e.KeyChar) >= 58 Then
                e.Handled = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtConf_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConf.TextChanged

    End Sub

    Private Sub bCerrarCont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bCerrarCont.Click
        CerrarContenedora()
    End Sub

    Private Sub CerrarContenedora()
        Try
            Me.txtContenedora.Enabled = True
            Me.txtContenedora.SelectAll()
            Me.txtContenedora.Focus()
        Catch ex As Exception
            Mensaje(ex)
        End Try
    End Sub

    Private Sub lblTareaCompleta_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTareaCompleta.ParentChanged

    End Sub

    Private Sub txtUbicacion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUbicacion.TextChanged

    End Sub


    Private Sub DescargarMaterial()
        Dim Frm As frmABASTDescarga
        Try
            Frm = New frmABASTDescarga
            Frm.ObjAbastecimiento = oAbast
            Frm.ShowDialog()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, frmName)
        Finally
            Frm.Dispose()
        End Try
    End Sub

    Private Sub frmDescargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmDescargar.Click
        DescargarMaterial()
    End Sub

End Class