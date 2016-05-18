Public Class FrmProcesoIngreso

    Private Cpte_Prefijo As String
    Private Cpte_Numero As String
    Private Cod_Origen As String
    Private Nombre_Origen As String
    Private Cliente As String
    Private Descripcion As String
    Private Unidad As String
    Private CategoriaLogica As String
    Private Cantidad As Double
    Private Producto_Id As String
    Private EsFraccionable As Char
    Private Const FrmName As String = "Proceso de Ingreso."

    Private Sub txtNroDocumento_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNroDocumento.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub txtNroDocumento_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNroDocumento.KeyUp
        Dim vError As String = ""
        Dim val As clsProcesoIngreso
        Try
            If e.KeyValue = 13 And (Trim(Me.txtNroDocumento.Text) <> "") Then
                val = New clsProcesoIngreso
                val.Conexion = SQLc
                If Not val.ExistsDocument(Trim(Me.txtNroDocumento.Text), Cpte_Prefijo, Cpte_Numero, Cod_Origen, Nombre_Origen, Cliente, vError) Then
                    Me.txtNroDocumento.Text = ""
                    Me.txtNroDocumento.Focus()
                    Throw New Exception(vError)
                Else
                    Me.txtNroDocumento.ReadOnly = True
                    MostrarControles()
                    Me.txtCodProducto.Focus()
                End If
            ElseIf e.KeyCode = Keys.F2 Then
                F2()
            ElseIf e.KeyCode = Keys.F3 Then
                F3()
            ElseIf e.KeyCode = Keys.F1 Then
                F1()
            ElseIf e.KeyCode = Keys.F4 Then
                F4()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        Finally
            val = Nothing
        End Try
        
    End Sub

    Public Sub MostrarControles()
        Me.lblRemito.Visible = True
        Me.lblNroRemito.Visible = True
        Me.lblRemito.Text = Cpte_Prefijo + " " + Cpte_Numero
        Me.lblProveedor.Visible = True
        Me.lblProveedorOrigen.Visible = True
        Me.lblProveedorOrigen.Text = Cod_Origen + " " + Nombre_Origen
        Me.lblProducto.Visible = True
        Me.txtCodProducto.Visible = True
    End Sub
    Public Sub MostrarControlesProducto()
        Me.Panel1.Visible = True
        Me.lblDescripcionProd.Text = Descripcion
        Me.lblDescUnidad.Text = Unidad
        Me.lblCatLogica.Text = CategoriaLogica

        If Me.btnEspCantidad.BackColor = Color.Gainsboro Then
            Me.lbltxtcantidad.Visible = True
            Me.txtCantidad.Visible = False
            Me.txtCantidad.Text = ""
        Else
            Me.lbltxtcantidad.Visible = False
            Me.lbltxtcantidad.Text = ""
            Me.txtCantidad.Visible = True
            Me.txtCantidad.Focus()

        End If

    End Sub
    Public Sub F1()
        Me.txtNroDocumento.Text = ""
        Me.lblNroRemito.Visible = False
        Me.lblRemito.Visible = False
        Me.lblRemito.Text = ""
        Me.lblProveedor.Visible = False
        Me.lblProveedorOrigen.Visible = False
        Me.lblProveedorOrigen.Text = ""
        Me.lblProducto.Visible = False
        Me.txtCodProducto.Text = ""
        Me.lblDescripcionProd.Text = ""
        Me.lblDescUnidad.Text = ""
        Me.lblCatLogica.Text = ""
        Me.txtCantidad.Text = ""
        Me.lbltxtcantidad.Text = ""
        Me.txtCodProducto.Visible = False
        Me.txtNroDocumento.ReadOnly = False
        Me.Panel1.Visible = False
        Me.txtNroDocumento.Focus()

    End Sub
    Public Sub F2()
        If Me.btnEspCantidad.BackColor = Color.Gainsboro Then
            Me.btnEspCantidad.BackColor = SystemColors.Control
        Else
            Me.btnEspCantidad.BackColor = Color.Gainsboro
        End If
        Me.txtCodProducto.Focus()

    End Sub
    Public Sub F3()
        Dim frmProductos As New FrmProductosCargados
        Dim vError As String = ""
        Dim val As New clsProcesoIngreso
        Try
            If Trim(Me.txtNroDocumento.Text) <> "" Then
                If val.GetProductosCargados(Me.txtNroDocumento.Text, vError).Rows.Count > 0 Then
                    frmProductos.Documento_Id = Me.txtNroDocumento.Text
                    frmProductos.ShowDialog()
                Else
                    Throw New Exception(vError)
                End If
            Else
                vError = "Debe ingresar un número de documento"
                Throw New Exception(vError)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        End Try
    End Sub
    Public Sub F4()
        F1()
        Me.Close()
    End Sub

    Private Sub txtCodProducto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCodProducto.KeyUp
        Dim vError As String = ""
        Dim val As clsProcesoIngreso
        Try
            If e.KeyValue = 13 And (Trim(Me.txtCodProducto.Text) <> "") Then
                val = New clsProcesoIngreso
                val.Conexion = SQLc

                o2D.Decode(Me.txtCodProducto.Text)
                Me.txtCodProducto.Text = o2D.PRODUCTO_ID

                If Not val.ExistsProduct(Trim(Me.txtCodProducto.Text), Descripcion, Unidad, CategoriaLogica, Producto_Id, Cliente, EsFraccionable, vError) Then
                    Me.txtCodProducto.Text = ""
                    Me.txtCodProducto.Focus()
                    Throw New Exception(vError)
                Else
                    Me.txtCodProducto.ReadOnly = True
                    MostrarControlesProducto()
                    If Me.lbltxtcantidad.Visible = True Then
                        Cantidad = 1
                        If Not val.GuardarDetalle(Producto_Id, CDbl(Me.txtNroDocumento.Text), Cantidad, Cliente, vError) Then
                            IngresoNuevoproducto()
                            Throw New Exception(vError)
                        Else
                            IngresoNuevoproducto()
                        End If
                    End If
                End If
            ElseIf e.KeyCode = Keys.F2 Then
                F2()
            ElseIf e.KeyCode = Keys.F1 Then
                F1()
            ElseIf e.KeyCode = Keys.F4 Then
                F4()
            ElseIf e.KeyCode = Keys.F3 Then
                F3()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        Finally
            val = Nothing
        End Try

    End Sub

    Private Sub btnEspCantidad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEspCantidad.Click
        F2()
    End Sub

   
    Private Sub FrmProcesoIngreso_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case e.KeyCode = Keys.F1
                F1()
            Case e.KeyCode = Keys.F2
                F2()
            Case e.KeyCode = Keys.F3
                F3()
            Case e.KeyCode = Keys.F4
                F4()
        End Select
    End Sub
    Public Sub IngresoNuevoproducto()
        Me.lblDescripcionProd.Text = ""
        Me.lblDescUnidad.Text = ""
        Me.lblCatLogica.Text = ""
        Me.txtCantidad.Text = ""
        Me.lbltxtcantidad.Text = ""
        Me.Panel1.Visible = False
        Me.txtCodProducto.Text = ""
        Me.txtCodProducto.ReadOnly = False
        Me.txtCodProducto.Focus()
    End Sub

    Private Sub btnNuevaCarga_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevaCarga.Click
        F1()
    End Sub

    Private Sub btnFinalizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinalizar.Click
        F4()
    End Sub

    Private Sub txtCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress
        'ValidarCaracterNumerico(e)
        Dim Search As String
        Dim Pos As Integer
        Search = "."
        If EsFraccionable = "0" Then
            ValidarCaracterNumerico(e)
        Else
            Pos = InStr(1, Me.txtCantidad.Text, Search)
            If Pos > 0 And Asc(e.KeyChar) <> 46 Then
                If Len(Mid(Me.txtCantidad.Text, Pos + 1, Len(Me.txtCantidad.Text))) >= 5 And Asc(e.KeyChar) <> 8 Then
                    e.Handled = True
                    Me.txtCantidad.Focus()
                End If
            Else
                If Pos <> 0 And (Asc(e.KeyChar) = 46) Then
                    e.Handled = True
                ElseIf Pos = 0 And (Asc(e.KeyChar) = 44) Then
                    e.Handled = True
                ElseIf Pos = 0 And (Asc(e.KeyChar) = 46) Then
                    e.Handled = False
                Else
                    ValidarCaracterNumerico(e)
                End If
            End If
        End If
    End Sub

    Private Sub txtCantidad_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyUp
        Dim vError As String = ""
        Dim val As clsProcesoIngreso
        Try
            If e.KeyValue = 13 Then
                val = New clsProcesoIngreso
                val.Conexion = SQLc
               
                If Not val.GuardarDetalle(Producto_Id, CDbl(Me.txtNroDocumento.Text), CDbl(Me.txtCantidad.Text), Cliente, vError) Then
                    IngresoNuevoproducto()
                    Throw New Exception(vError)
                Else
                    IngresoNuevoproducto()
                End If
            ElseIf e.KeyCode = Keys.F2 Then
                F2()
            ElseIf e.KeyCode = Keys.F1 Then
                F1()
            ElseIf e.KeyCode = Keys.F4 Then
                F4()
            ElseIf e.KeyCode = Keys.F3 Then
                F3()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        Finally
            val = Nothing
        End Try
    End Sub

    Private Sub btnVerCargados_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerCargados.Click
        F3()
    End Sub

   
    Private Sub txtCodProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodProducto.TextChanged

    End Sub
End Class