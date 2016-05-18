Public Class frmEmpaqueContenido

    Private Const FrmName As String = "Empaque."
    Private vPEDIDO As String = ""
    Private vCONTENEDOR As String
    Private oEMP As clsEmpaque


    Public Property PEDIDO() As String
        Get
            Return vPEDIDO
        End Get
        Set(ByVal value As String)
            vPEDIDO = value
        End Set
    End Property

    Public Property CONTENEDOR() As String
        Get
            Return vCONTENEDOR
        End Get
        Set(ByVal value As String)
            vCONTENEDOR = value
        End Set
    End Property

    Public Property OEMPAQUE() As clsEmpaque
        Get
            Return oEMP
        End Get
        Set(ByVal value As clsEmpaque)
            oEMP = value
        End Set
    End Property

    Private Sub frmEmpaqueContenido_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp

        Select Case e.KeyCode
            Case Keys.F1
                Salir()
            Case Keys.F2
                QuitarContenido()
        End Select

    End Sub

    Private Sub frmEmpaqueContenido_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetearValores()
    End Sub

    Private Sub SetearValores()

        Me.lblContenedor.Text = "Nro. Contenedor Empaque: " & vCONTENEDOR
        If oEMP.ContenidoCajaEmpaque(vCONTENEDOR, vPEDIDO, Me.dgContenido) Then
            Me.dgContenido.Focus()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Salir()
    End Sub

    Private Sub Salir()
        Try
            Me.Close()
        Catch ex As Exception
            MsgBox("excepcion: " & ex.Message, MsgBoxStyle.Critical, frmname)
        End Try
    End Sub

    Private Sub dgContenido_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgContenido.GotFocus
        Try
            dgContenido.Select(dgContenido.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgContenido_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgContenido.KeyUp
        Try
            dgContenido.Select(dgContenido.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        QuitarContenido()
    End Sub

    Private Sub QuitarContenido()
        Try

            ConfirmarQuita()
            SetearValores()
        Catch ex As Exception
            MsgBox("excepcion: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub txtCant_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub ConfirmarQuita()
        Dim PEDIDO As String, LOTE As String, PARTIDA As String, SERIE As String, PRODUCTO As String, CONTENEDOR As String, CANTIDAD As String
        Try
            If MsgBox("¿Desea retirar el material de la contenedora?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                PEDIDO = Me.dgContenido.Item(dgContenido.CurrentRowIndex, 7).ToString()
                LOTE = Me.dgContenido.Item(dgContenido.CurrentRowIndex, 1).ToString()
                PARTIDA = Me.dgContenido.Item(dgContenido.CurrentRowIndex, 2).ToString()
                SERIE = Me.dgContenido.Item(dgContenido.CurrentRowIndex, 3).ToString()
                PRODUCTO = Me.dgContenido.Item(dgContenido.CurrentRowIndex, 0).ToString()
                CONTENEDOR = Me.vCONTENEDOR
                CANTIDAD = Me.dgContenido.Item(dgContenido.CurrentRowIndex, 4).ToString()
                If oEMP.QuitarContenido(PEDIDO, LOTE, PARTIDA, SERIE, PRODUCTO, CONTENEDOR, CANTIDAD) Then
                    MsgBox("El material fue retirado correctamente.", MsgBoxStyle.OkOnly, FrmName)
                End If
            End If
        Catch ex As Exception
            MsgBox("excepcion: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub txtCant_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class