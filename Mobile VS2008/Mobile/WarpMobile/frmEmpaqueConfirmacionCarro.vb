Public Class frmEmpaqueConfirmacionCarro

    Private Const frmName As String = "Empaque"
    Private oEMP As clsEmpaque
    Private vOK As Boolean = False

    Public ReadOnly Property VALIDACION_OK() As Boolean
        Get
            Return vOK
        End Get
    End Property

    Public Property oEMPAQUE() As clsEmpaque
        Get
            Return oEMP
        End Get
        Set(ByVal value As clsEmpaque)
            oEMP = value
        End Set
    End Property

    Private Sub frmEmpaqueConfirmacionCarro_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.vOK = False
        Me.lblCodigoViaje.Text = "Codigo de Ola " & oEMP.CodigoDeOla
        Me.lblContenedora.Text = "Confirme Contenedora " & oEMP.U_EMPAQUE
        Me.txtContenedora.Text = ""
        Me.txtContenedora.Focus()
    End Sub

    Private Sub RutinaAceptar()
        Try
            If Trim(Me.txtContenedora.Text) <> "" Then
                If Trim(Me.txtContenedora.Text) <> oEMP.U_EMPAQUE Then
                    Me.txtContenedora.Text = ""
                    Me.txtContenedora.Focus()
                    Me.lblMsg.Text = "El numero de contenedora de empaque no es valido."
                Else
                    vOK = True
                    Me.Close()
                End If
            Else
                Me.txtContenedora.Text = ""
                Me.txtContenedora.Focus()
                Me.lblMsg.Text = "Debe confirmar la contenedora."
            End If
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        oEMP.LiberarTarea()
        vOK = False
        Me.Close()
    End Sub

    Private Sub txtContenedora_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtContenedora.KeyUp
        If e.KeyValue = 13 Then
            If Trim(Me.txtContenedora.Text) <> "" Then
                RutinaAceptar()
            Else
                Me.txtContenedora.Text = ""
                Me.txtContenedora.Focus()
            End If
        End If
    End Sub

    Private Sub txtContenedora_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContenedora.TextChanged
        Me.lblMsg.Text = ""
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Me.RutinaAceptar()
    End Sub
End Class