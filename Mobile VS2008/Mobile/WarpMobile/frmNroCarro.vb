Public Class frmNroCarro

    Public isCancel As Boolean
    Public sViaje As String
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        isCancel = True
        Me.Close()
    End Sub

    Private Sub frmNroCarro_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtNroCarro.Focus()
    End Sub

    Private Sub txtNroCarro_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNroCarro.KeyUp
        Dim Fe As New FuncionesEgreso

        If e.KeyCode = 13 Then
            If Me.txtNroCarro.Text <> "" Then
                If IsNumeric(txtNroCarro.Text) Then
                    'chequeo que le nro de carro no este en uso
                    If Not Fe.CarroenUso(txtNroCarro.Text) Then
                        lblMensaje.Text = ""
                        isCancel = False
                        Me.Close()
                    Else
                        'el carro pertenece al viaje
                        If Fe.esCarroenViaje(txtNroCarro.Text, sViaje) Then
                            lblMensaje.Text = ""
                            isCancel = False
                            Me.Close()
                        Else
                            lblMensaje.Text = "El carro ingresado se encuentra en uso."
                        End If
                    End If
                Else
                    lblMensaje.Text = "El Nro. de Carro solo puede ser un valor numerico."
                End If
            Else
                lblMensaje.Text = "Debe ingresar el Nro. de carro."
            End If
        End If
    End Sub

End Class