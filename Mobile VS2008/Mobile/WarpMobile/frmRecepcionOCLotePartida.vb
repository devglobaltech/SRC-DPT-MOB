Imports System.Data
Imports System.Data.SqlClient

Public Class frmRecepcionOCLotePartida
    Private vModoPantalla As String = ""

    Private vLoteProveedor As String = ""
    Private vPartida As String = ""
    Private vFechaVto As String = ""

    Private vLoteObligatorio As Boolean
    Private vPartidaObligatoria As Boolean
    Private vFechaObligatoria As Boolean

    Private vCLienteID As String = ""
    Private vOC As String = ""
    Private vProducto As String = ""

    Property modoPantalla() As String
        Get
            Return vModoPantalla
        End Get
        Set(ByVal value As String)
            vModoPantalla = value
        End Set
    End Property

    Property loteObligatorio() As Boolean
        Get
            Return vLoteObligatorio
        End Get
        Set(ByVal value As Boolean)
            vLoteObligatorio = value
        End Set
    End Property

    Property partidaObligatoria() As Boolean
        Get
            Return vPartidaObligatoria
        End Get
        Set(ByVal value As Boolean)
            vPartidaObligatoria = value
        End Set
    End Property

    Property fechaObligatoria() As Boolean
        Get
            Return vFechaObligatoria
        End Get
        Set(ByVal value As Boolean)
            vFechaObligatoria = value
        End Set
    End Property

    Property lote() As String
        Get
            Return vLoteProveedor
        End Get
        Set(ByVal value As String)
            vLoteProveedor = value
        End Set
    End Property

    Property partida() As String
        Get
            Return vPartida
        End Get
        Set(ByVal value As String)
            vPartida = value
        End Set
    End Property

    Property fecha() As String
        Get
            Return vFechaVto
        End Get
        Set(ByVal value As String)
            vFechaVto = value
        End Set
    End Property

    Property clienteID() As String
        Get
            Return vCLienteID
        End Get
        Set(ByVal value As String)
            vCLienteID = value
        End Set
    End Property

    Property ordenDeCompra() As String
        Get
            Return vOC
        End Get
        Set(ByVal value As String)
            vOC = value
        End Set
    End Property

    Property Producto() As String
        Get
            Return vProducto
        End Get
        Set(ByVal value As String)
            vProducto = value
        End Set
    End Property

    Private Sub frmRecepcionOCLotePartida_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Ontengo el Lote de Proveedor y/o partida si es que el producto de la orden de compra lo tiene especificado.

        Me.lblDebeCompletarPartida.Visible = False
        Me.lblDebeCompletarLote.Visible = False
        Me.lblDebeCompletarFecha.Visible = False

        If (Me.modoPantalla = "Seleccion") Then
            Me.Text = "Selección"
            Me.lblMensajeTitulo.Text = "Por favor ingrese el lote de proveedor y/o partida del producto a seleccionar."

            If (Me.fechaObligatoria) Then
                Me.lblVencimiento.ForeColor = Color.Red
            Else
                Me.lblVencimiento.ForeColor = Color.Black
            End If
        Else
            Me.Text = "Ingreso de Datos"
            Me.lblMensajeTitulo.Text = "Debe ingresar los datos lote de proveedor, partida y/o fecha de vencimiento para continuar. Los campos rojos son obligatorios."
        End If

        If (Me.modoPantalla <> "Seleccion") Then
            'Me.vPartida = getPartida()
            'Me.vLoteProveedor = getLoteProveedor()

            If (Me.loteObligatorio) Then
                Me.lblLoteProveedor.ForeColor = Color.Red
            Else
                Me.lblLoteProveedor.ForeColor = Color.Black
            End If

            If (Me.partidaObligatoria) Then
                Me.lblPartida.ForeColor = Color.Red
            Else
                Me.lblPartida.ForeColor = Color.Black
            End If

            If (Me.fechaObligatoria) Then
                Me.lblVencimiento.ForeColor = Color.Red
            Else
                Me.lblVencimiento.ForeColor = Color.Black
            End If

            Me.txtLoteProveedor.Text = Me.lote
            Me.txtPartida.Text = Me.partida

            If (Me.txtLoteProveedor.Text <> "") Then
                Me.txtLoteProveedor.Enabled = False
            Else
                Me.txtLoteProveedor.Enabled = True
            End If

            If (Me.txtPartida.Text <> "") Then
                Me.txtPartida.Enabled = False
            Else
                Me.txtPartida.Enabled = True
            End If
        End If

    End Sub

    'Private Function getPartida() As String
    '    Dim Cmd As SqlCommand
    '    Dim Pa As SqlParameter
    '    Dim ret As String = ""

    '    Try
    '        If VerifyConnection(SQLc) Then
    '            Cmd = SQLc.CreateCommand
    '            Cmd.CommandText = "Dbo.getPartidaOCProd"
    '            Cmd.CommandType = CommandType.StoredProcedure
    '            Cmd.Connection = SQLc
    '            Cmd.Parameters.Clear()

    '            Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
    '            Pa.Value = Me.clienteID
    '            Cmd.Parameters.Add(Pa)
    '            Pa = Nothing

    '            Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
    '            Pa.Value = Me.Producto
    '            Cmd.Parameters.Add(Pa)
    '            Pa = Nothing

    '            Pa = New SqlParameter("@OC", SqlDbType.VarChar, 100)
    '            Pa.Value = Me.ordenDeCompra
    '            Cmd.Parameters.Add(Pa)
    '            Pa = Nothing

    '            Pa = New SqlParameter("@OUTPARTIDA", SqlDbType.VarChar, 100)
    '            Pa.Direction = ParameterDirection.Output
    '            Cmd.Parameters.Add(Pa)
    '            Pa = Nothing

    '            Cmd.ExecuteNonQuery()
    '            ret = IIf(IsDBNull(Cmd.Parameters("@OUTPARTIDA").Value), "", Cmd.Parameters("@OUTPARTIDA").Value)

    '            Return ret
    '        Else
    '            Return ""
    '        End If
    '        'Return True
    '    Catch ex As Exception
    '        Return ""
    '    Finally
    '        Cmd = Nothing
    '        Pa = Nothing
    '    End Try
    'End Function

    'Private Function getLoteProveedor() As String
    '    Dim Cmd As SqlCommand
    '    Dim Pa As SqlParameter
    '    Dim ret As String = ""

    '    Try
    '        If VerifyConnection(SQLc) Then
    '            Cmd = SQLc.CreateCommand
    '            Cmd.CommandText = "Dbo.getLoteProveedorOCProd"
    '            Cmd.CommandType = CommandType.StoredProcedure
    '            Cmd.Connection = SQLc
    '            Cmd.Parameters.Clear()

    '            Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
    '            Pa.Value = Me.clienteID
    '            Cmd.Parameters.Add(Pa)
    '            Pa = Nothing

    '            Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
    '            Pa.Value = Me.Producto
    '            Cmd.Parameters.Add(Pa)
    '            Pa = Nothing

    '            Pa = New SqlParameter("@OC", SqlDbType.VarChar, 100)
    '            Pa.Value = Me.ordenDeCompra
    '            Cmd.Parameters.Add(Pa)
    '            Pa = Nothing

    '            Pa = New SqlParameter("@OUTLOTEPROVEEDOR", SqlDbType.VarChar, 100)
    '            Pa.Direction = ParameterDirection.Output
    '            Cmd.Parameters.Add(Pa)
    '            Pa = Nothing

    '            Cmd.ExecuteNonQuery()
    '            ret = IIf(IsDBNull(Cmd.Parameters("@OUTLOTEPROVEEDOR").Value), "", Cmd.Parameters("@OUTLOTEPROVEEDOR").Value)

    '            Return ret
    '        Else
    '            Return ""
    '        End If
    '        'Return True
    '    Catch ex As Exception
    '        Return ""
    '    Finally
    '        Cmd = Nothing
    '        Pa = Nothing
    '    End Try
    'End Function

    Private Sub Aceptar()
        Dim validaDatos As Boolean = True

        If (Me.modoPantalla <> "Seleccion") Then
            If (Me.loteObligatorio And String.IsNullOrEmpty(Me.txtLoteProveedor.Text)) Then
                Me.lblDebeCompletarLote.Visible = True
                validaDatos = False
            End If

            If (Me.partidaObligatoria And String.IsNullOrEmpty(Me.txtPartida.Text)) Then
                Me.lblDebeCompletarPartida.Visible = True
                validaDatos = False
            End If

            If (Me.fechaObligatoria And String.IsNullOrEmpty(Me.txtVencimiento.Text)) Then
                Me.lblDebeCompletarFecha.Visible = True
                validaDatos = False
            End If

        End If

        If (validaDatos) Then
            Me.lote = Me.txtLoteProveedor.Text
            Me.partida = Me.txtPartida.Text
            Me.fecha = Me.txtVencimiento.Text
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Aceptar()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Cancelar()
    End Sub

    Private Sub Cancelar()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub txtLoteProveedor_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLoteProveedor.KeyUp
        If (e.KeyValue = 13) And Trim(Me.txtLoteProveedor.Text) <> "" Then
            Me.txtPartida.Focus()
        End If
    End Sub

    Private Sub txtPartida_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPartida.KeyUp
        If (e.KeyValue = 13) And Trim(Me.txtPartida.Text) <> "" Then
            Me.txtVencimiento.Focus()
        End If
    End Sub

    Private Sub txtVencimiento_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtVencimiento.KeyUp

        If (e.KeyCode = 13 And Trim(Me.txtVencimiento.Text) <> "") Or (e.KeyCode = 8) Then
            If e.KeyCode = 8 Then
                Me.txtVencimiento.Text = ""
            End If
            If Me.txtVencimiento.Text <> "" Then
                If Me.txtVencimiento.Text.Length >= 10 Then
                    If Not IsDate(Me.txtVencimiento.Text) Then
                        MsgBox("No es una fecha valida.", MsgBoxStyle.OkOnly, "Advertencia")
                        Me.txtVencimiento.Text = ""
                        Me.txtVencimiento.Focus()
                    Else
                        Me.txtVencimiento.Enabled = False
                        Aceptar()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtVencimiento_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVencimiento.TextChanged
        If Me.txtVencimiento.Text.Length = 2 Then
            Me.txtVencimiento.Text = Me.txtVencimiento.Text & "/"
            Me.txtVencimiento.SelectionStart = Me.txtVencimiento.Text.Length
        ElseIf Me.txtVencimiento.Text.Length = 5 Then
            Me.txtVencimiento.Text = Me.txtVencimiento.Text & "/"
            Me.txtVencimiento.SelectionStart = Me.txtVencimiento.Text.Length
        End If
    End Sub

    Private Sub txtVencimiento_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVencimiento.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Public Sub ValidarCaracterNumerico(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If (Asc(e.KeyChar) >= 32 And Asc(e.KeyChar) <= 47) Or Asc(e.KeyChar) >= 58 Then
                e.Handled = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmRecepcionOCLotePartida_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Aceptar()
            Case Keys.F2
                cancelar()
        End Select
    End Sub
End Class