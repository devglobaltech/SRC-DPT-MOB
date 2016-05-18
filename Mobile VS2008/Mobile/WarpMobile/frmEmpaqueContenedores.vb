Public Class frmEmpaqueContenedores

    Private OEMP As New clsEmpaque
    Private Const FrmName As String = "Empaque."

    Public Property OEMPAQUE() As clsEmpaque
        Get
            Return OEMP
        End Get
        Set(ByVal value As clsEmpaque)
            OEMP = value
        End Set
    End Property

    Private Sub frmEmpaqueContenedores_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Salir()
            Case Keys.F2
                CerrarContenedora()

        End Select
    End Sub

    Private Sub frmEmpaqueContenedores_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ObtenerDatos()
    End Sub

    Private Sub ObtenerDatos()
        Try
            If Not OEMP.GetContenedoresGenerados(Me.dgContenedoresGenerados) Then
                Me.Close()
            Else
                Me.dgContenedoresGenerados.Focus()
            End If
        Catch ex As Exception
            MsgBox("Excepcion: " & ex.Message)
        End Try
    End Sub

    Private Sub CerrarContenedora()
        Dim Msg As String = "", vCont_Seleccionada As String = "", vPEDIDO As String = "", vEstado As String = ""
        Try

            vCont_Seleccionada = Me.dgContenedoresGenerados.Item(dgContenedoresGenerados.CurrentRowIndex, 0).ToString()
            vPEDIDO = Me.dgContenedoresGenerados.Item(dgContenedoresGenerados.CurrentRowIndex, 1).ToString()
            vEstado = Me.dgContenedoresGenerados.Item(dgContenedoresGenerados.CurrentRowIndex, 2).ToString()

            If vEstado = "CERRADO" Then
                MsgBox("La contenedora de empaque se encuentra cerrada.", MsgBoxStyle.OkOnly, FrmName)
                Return
            End If

            Msg = "¿Desea cerrar la contenedora " & OEMP.ULTIMO_UC_EMPAQUE & " correspondiente al pedido " & OEMP.ULTIMO_PEDIDO
            If OEMP.CerrarContenedora(vCont_Seleccionada, vPEDIDO) Then
                ObtenerDatos()
            Else
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub cmdCerrarContenedora_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CerrarContenedora()
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Salir()
    End Sub

    Private Sub Salir()
        Try
            Me.Close()
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub btnCerrarContenedora_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrarContenedora.Click
        CerrarContenedora()
    End Sub

    Private Sub btnVerContenido_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerContenido.Click
        VerContenido()
    End Sub

    Private Sub VerContenido()

        Dim Msg As String = "", vCont_Seleccionada As String = "", vPedido As String = "", F As New frmEmpaqueContenido
        Try
            vCont_Seleccionada = Me.dgContenedoresGenerados.Item(dgContenedoresGenerados.CurrentRowIndex, 0).ToString()
            vPedido = Me.dgContenedoresGenerados.Item(dgContenedoresGenerados.CurrentRowIndex, 1).ToString()
            F.CONTENEDOR = vCont_Seleccionada
            F.PEDIDO = vPedido
            F.OEMPAQUE = OEMP
            F.ShowDialog()
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            F.Dispose()
        End Try

    End Sub

    Private Sub dgContenedoresGenerados_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgContenedoresGenerados.CurrentCellChanged
        Try
            dgContenedoresGenerados.Select(dgContenedoresGenerados.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgContenedoresGenerados_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgContenedoresGenerados.GotFocus
        Try
            dgContenedoresGenerados.Select(dgContenedoresGenerados.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgContenedoresGenerados_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgContenedoresGenerados.KeyPress
        Try
            dgContenedoresGenerados.Select(dgContenedoresGenerados.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAbrirContenedora_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbrirContenedora.Click
        Me.AbrirContenedora()
    End Sub

    Private Sub AbrirContenedora()
        Dim PEDIDO As String, CONTENEDOR As String, ESTADO As String
        Try
            ESTADO = Me.dgContenedoresGenerados.Item(dgContenedoresGenerados.CurrentRowIndex, 2).ToString()
            CONTENEDOR = Me.dgContenedoresGenerados.Item(dgContenedoresGenerados.CurrentRowIndex, 0).ToString()
            PEDIDO = Me.dgContenedoresGenerados.Item(dgContenedoresGenerados.CurrentRowIndex, 1).ToString()
            If ESTADO = "ABIERTO" Then
                MsgBox("La contenedora de empaque se encuentra abierta.", MsgBoxStyle.OkOnly, FrmName)
            Else
                If MsgBox("¿Desea re-abrir la contenedora?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                    If OEMP.AbrirContenedora(PEDIDO, CONTENEDOR) Then
                        ObtenerDatos()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub
End Class