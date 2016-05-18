Public Class frmRecepcionGuardadoUbicacion

    Private obj As clsRecepcionGuardado
    Private vNroPallet As String = ""
    Private vCliente As String = ""
    Private vOC As String = "N/A"
    Private frmName As String = "Recepcion y Guardado."


    Public Property ObjRecepcionGuardado() As clsRecepcionGuardado
        Get
            Return obj
        End Get
        Set(ByVal value As clsRecepcionGuardado)
            obj = value
        End Set
    End Property

    Public Property NroPallet() As String
        Get
            Return vNroPallet
        End Get
        Set(ByVal value As String)
            vNroPallet = value
        End Set
    End Property

    Public Property Cliente() As String
        Get
            Return vCliente
        End Get
        Set(ByVal value As String)
            vCliente = value
        End Set
    End Property

    Public Property OrdenDeCompra() As String
        Get
            Return vOC
        End Get
        Set(ByVal value As String)
            vOC = value
        End Set
    End Property

    Private Sub frmRecepcionGuardadoUbicacion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                If Me.txtDestino.Text <> "" Then
                    Procesar()
                End If
            Case Keys.F2
                Me.CancelarGuardado()
        End Select
    End Sub

    Private Sub frmRecepcionGuardadoUbicacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InicializarPantalla()
    End Sub

    Private Sub InicializarPantalla()
        Try
            Me.lblPallet.Text = "Nro. de Pallet: " & Me.NroPallet
            Me.lblCliente.Text = "Cliente: " & Me.Cliente
            Me.lblOc.Text = "Orden de compra: " & Me.OrdenDeCompra

            'Comienzo la busqueda de la posicion para guardar el pallet.
            If obj.Locator_Ing(Me.vNroPallet, Me.lblDestino, Me.lblError) Then
                Me.txtDestino.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

    Private Sub txtDestino_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDestino.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtDestino.Text <> "" Then
                Me.txtDestino.Text = UCase(Me.txtDestino.Text)
                Procesar()
            End If
        End If
    End Sub

    Private Sub Procesar()
        Try
            If obj.Ubicacion <> Me.txtDestino.Text And obj.Ubicacion <> "" Then
                If MsgBox("La posicion destino no es la indicada, ¿desea continuar?", MsgBoxStyle.YesNo, frmName) = MsgBoxResult.No Then
                    Me.txtDestino.Text = ""
                    Me.txtDestino.Focus()
                    Exit Sub
                End If
            End If

            If MsgBox("¿Confirma la ubicacion?", MsgBoxStyle.YesNo, frmName) = MsgBoxResult.Yes Then
                If obj.Procesar(Me.NroPallet, Me.txtDestino.Text) Then
                    MsgBox("El proceso de recepcion y guardado del pallet finalizo correctamente.", MsgBoxStyle.Information, frmName)
                    Me.Close()
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

    Private Sub txtDestino_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDestino.TextChanged

    End Sub

    Private Sub CmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCancelar.Click
        Me.CancelarGuardado()
    End Sub

    Private Sub CancelarGuardado()
        Dim Msg As String = "¿Desea cancelar el guardado del pallet?, posteriormente puede guardarlo por el menu de UBICACION MERCADERIA"
        Try
            If MsgBox(Msg, MsgBoxStyle.YesNo, frmName) = MsgBoxResult.Yes Then
                Me.Close()
            Else
                Me.txtDestino.Text = ""
                Me.txtDestino.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

    Private Sub BtnContinuar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnContinuar.Click
        Procesar()
    End Sub

End Class