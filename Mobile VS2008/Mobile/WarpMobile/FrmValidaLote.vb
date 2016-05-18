Public Class FrmValidaLote

    Private Viaje As String = ""
    Private Producto As String = ""
    Private NroLote As String = ""
    Private BlnLoteValido As Boolean = False

    Public WriteOnly Property pViaje() As String
        Set(ByVal value As String)
            Viaje = value
        End Set
    End Property

    Public WriteOnly Property pProducto() As String
        Set(ByVal value As String)
            Producto = value
        End Set
    End Property

    Public WriteOnly Property pNroLote() As String
        Set(ByVal value As String)
            NroLote = value
        End Set
    End Property

    Public ReadOnly Property LoteValido() As Boolean
        Get
            Return BlnLoteValido
        End Get
    End Property

    Private Sub FrmValidaLote_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress

    End Sub

    Private Sub FrmValidaLote_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case 112 'Keys.F1
                If (Trim(Me.txtLote.Text) <> "") Then
                    If Trim(UCase(NroLote)) = Trim(UCase(Me.txtLote.Text)) Then
                        BlnLoteValido = True
                        Me.Close()
                    Else
                        BlnLoteValido = False
                        MsgBox("El lote ingresado es invalido. Por favor ingrese nuevamente el lote", MsgBoxStyle.Information, "Verificacion de Lote.")
                        Me.txtLote.Text = ""
                        Me.txtLote.Focus()
                    End If
                End If
            Case 113 'Keys.F2
                Me.txtLote.Text = ""
                Me.txtLote.Focus()
            Case 114 'Keys.F3
                If MsgBox("Aun no se valido el Nro. de Lote. No podra continuar con el picking hasta valide el mismo." & vbNewLine & "¿Desea continuar?", MsgBoxStyle.YesNo, "Validacion de Lotes") = MsgBoxResult.Yes Then
                    Me.BlnLoteValido = False
                    Me.Close()
                Else
                    Me.txtLote.Text = ""
                    Me.txtLote.Focus()
                End If
        End Select
    End Sub


    Private Sub FrmValidaLote_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.lblViaje.Text = "Picking / Viaje: " & Viaje.ToString
            Me.lblProducto.Text = "Producto: " & Producto.ToString
            Me.lblNroLote.Text = "Lote Proveedor: " & NroLote.ToString
            Me.txtLote.Focus()
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, "Exception")
        End Try
    End Sub

    Private Sub txtLote_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLote.KeyUp
        If (e.KeyValue = 13) And (Trim(Me.txtLote.Text) <> "") Then
            If Trim(UCase(NroLote)) = Trim(UCase(Me.txtLote.Text)) Then
                BlnLoteValido = True
                Me.Close()
            Else
                BlnLoteValido = False
                MsgBox("El lote ingresado es invalido. Por favor ingrese nuevamente el lote", MsgBoxStyle.Information, "Verificacion de Lote.")
                Me.txtLote.Text = ""
                Me.txtLote.Focus()
            End If
        End If
    End Sub

    Private Sub cmdAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
        If (Trim(Me.txtLote.Text) <> "") Then
            If Trim(UCase(NroLote)) = Trim(UCase(Me.txtLote.Text)) Then
                BlnLoteValido = True
                Me.Close()
            Else
                BlnLoteValido = False
                MsgBox("El lote ingresado es invalido. Por favor ingrese nuevamente el lote", MsgBoxStyle.Information, "Verificacion de Lote.")
                Me.txtLote.Text = ""
                Me.txtLote.Focus()
            End If
        End If
    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Try
            Me.txtLote.Text = ""
            Me.txtLote.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        If MsgBox("Aun no se valido el Nro. de Lote. No podra continuar con el picking hasta valide el mismo." & vbNewLine & "¿Desea continuar?", MsgBoxStyle.YesNo, "Validacion de Lotes") = MsgBoxResult.Yes Then
            Me.BlnLoteValido = False
            Me.Close()
        Else
            Me.txtLote.Text = ""
            Me.txtLote.Focus()
        End If
    End Sub
End Class