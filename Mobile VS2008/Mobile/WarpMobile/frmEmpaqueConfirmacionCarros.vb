Public Class frmEmpaqueConfirmacionCarros

    Public oEmp As New clsEmpaque
    Dim FrmName As String = "Empaque."
    Public Cancelo As Boolean = False
    Public Empaquetar As Boolean = False

    Private Sub frmEmpaqueConfirmacionCarros_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F2
                Me.cancelar()
            Case Keys.F5
                Me.AgregarCarro()
            Case Keys.F6
                Me.QuitarCarros()
            Case Keys.F7
                Me.ContinuarEmpaque()
        End Select
    End Sub

    Private Sub txtCarros_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCarros.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub txtCarros_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCarros.KeyUp
        If e.KeyValue = 13 Then
            AgregarCarro()
            e.Handled = True
        End If
    End Sub

    Private Sub QuitarCarros()
        Try
            If Trim(Me.txtCarros.Text) <> "" Then
                oEmp.QuitarCarro(Trim(Me.txtCarros.Text))
                Me.txtCarros.Text = ""
                Me.txtCarros.Focus()
                Me.lblNoValidos.Text = "Validar: " & oEmp.ContenedorasNoValidadas
                Me.lblValidos.Text = "Validados: " & oEmp.ContenedorasDesconsolidacion
            End If
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub cancelar()
        Dim Msg As String = "¿Desea cancelar la operación actual?"
        If Me.oEmp.CantidadContenedores > 0 Then
            If MsgBox(Msg, MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                oEmp.Limpiar()
                Me.Close()
            End If
        Else
            Me.Cancelo = True
            Me.Close()
        End If
    End Sub

    Private Sub btnQuitarCarros_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitarCarros.Click
        Me.QuitarCarros()
    End Sub

    Private Sub Empaquetado()
        Try
            Me.Empaquetar = True
            Me.Close()
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub ContinuarEmpaque()
        Try
            If oEmp.ContenedorasNoValidadas = "" Then
                If oEmp.CantidadContenedores > 0 And Me.pnlCarros.Visible And Me.btnContinuar.Visible Then
                    If MsgBox("¿Desea comenzar a empaquetar?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                        Empaquetado()
                    Else
                        Me.txtCarros.Text = ""
                        Me.txtCarros.Focus()
                    End If
                End If
            Else
                MsgBox("Antes de comenzar a empaquetar debe validar todos los contenedores de picking.", MsgBoxStyle.Information, FrmName)
                Me.txtCarros.Text = ""
                Me.txtCarros.Focus()
            End If
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub AgregarCarro()
        Dim Cont As String
        Try
            If Trim(Me.txtCarros.Text) <> "" Then
                If oEmp.AgregarCarroEmpaque(Trim(Me.txtCarros.Text)) Then
                    Me.txtCarros.Text = ""
                    Me.txtCarros.Focus()

                    Me.lblNoValidos.Visible = True
                    Me.lblValidos.Visible = True
                    Me.lblValidos.Text = "Validados: " & oEmp.ContenedorasDesconsolidacion
                    Me.lblNoValidos.Text = "No Validados: " & oEmp.ContenedorasNoValidadas
                    Cont = oEmp.ContenedorasNoValidadas
                    Me.lblCodigoViaje.Text = "Codigo de Ola: " & oEmp.CodigoDeOla
                    Me.lblCodigoViaje.Visible = True
                    If Cont <> "" Then
                        Me.lblNoValidos.Text = "Validar: " & Cont
                    Else : Me.ContinuarEmpaque()
                    End If
                Else : Me.txtCarros.Text = ""
                End If
            End If

        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub btnAgregarCarro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarCarro.Click
        Me.AgregarCarro()
    End Sub

    Private Sub btnContinuar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinuar.Click
        Me.ContinuarEmpaque()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.cancelar()
    End Sub

    Private Sub frmEmpaqueConfirmacionCarros_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblCodigoViaje.Visible = False
        Me.lblNoValidos.Visible = False
        Me.lblValidos.Visible = False
    End Sub

    Private Sub txtCarros_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCarros.TextChanged

    End Sub

End Class