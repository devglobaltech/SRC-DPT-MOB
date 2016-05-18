Imports System.Data
Imports System.Data.SqlClient

Public Class FrmOrdenCompra
    Private Const SQLConErr As String = "No se pudo conectar a la base de datos."
    Private Const ErrCon As String = "No se pudo conectar con la base de datos."
    Private Const FrmName As String = "Orden de Compra"
    Private IDPROVEEDOR As String
    Private Producto As String
    Private ClienteId As String
    Private Oc As String
    Private Nro_OC As String
    Private Vsecuencia As String
    Private xEsFraccionable As Boolean = False
    Private remitoNro As String
    Private TipoProceso As String
    Private estadoPrecarga As Boolean = False

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        BorrarNoProcesados()
        Me.Close()
    End Sub

    Sub BorrarNoProcesados()
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim Cmd As SqlCommand
        If VerifyConnection(SQLc) Then
            Try
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.MOB_BORRAR_TMP_PENDIENTE"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Cmd.ExecuteNonQuery()
            Catch sqlex As SqlException
                MsgBox(sqlex.Message, MsgBoxStyle.Critical, FrmName)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
            Finally
                Cmd = Nothing
                Pa = Nothing
            End Try
        Else
            MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
        End If
    End Sub

    Private Sub btnProveedor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProveedor.Click
        buscarProveedor()
    End Sub

    Private Sub buscarProveedor()
        Dim Nfrm As New FrmBuscarProveedor
        Try
            Nfrm.Cliente_id = Me.ClienteId
            Nfrm.ShowDialog()
            If Nfrm.Proveedor_ID <> Nothing Then
                Me.txtproveedor.Text = Nfrm.Proveedor_ID
                Me.lblproveedor.Text = Nfrm.Proveedor_Des
                Me.txtproveedor.Focus()
            Else
                Me.lblproveedor.Text = ""
                Me.txtproveedor.Text = ""
                lblsms.Text = "Ingrese Proveedor"
            End If
        Catch ex As Exception

        Finally
            Nfrm = Nothing
        End Try
    End Sub


    Private Sub txtproveedor_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtproveedor.KeyUp
        If e.KeyCode = Keys.F2 Then
            buscarProveedor()
        End If
        'If e.KeyValue = 13 Then
        '    If ValidarProveedor() = True Then
        '        IDPROVEEDOR = txtproveedor.Text
        '        lblsms.Text = "Ingreso Proveedor F1 para ingresar OC"
        '    End If
        'End If
        If (e.KeyCode = Keys.F1 Or e.KeyCode = Keys.Enter) And Me.txtproveedor.Text <> "" Then
            If ValidarProveedor() = True Then
                IDPROVEEDOR = txtproveedor.Text
                Me.txtproveedor.Enabled = False
                Me.btnProveedor.Enabled = True
                Me.cmbClienteId.Enabled = False
                Me.lblremito.Visible = True
                Me.txtRemitoNro.Visible = True
                Me.txtRemitoPrefijo.Visible = True
                Me.btnRemito.Visible = True
                Me.lblsms.Text = "Ingrese Remito"
                Me.txtRemitoPrefijo.Focus()

            Else
                lblsms.Text = "Ingrese Proveedor"
                Me.txtproveedor.Text = ""
                Me.txtproveedor.Focus()
            End If
        End If
    End Sub

    Private Function ValidarProveedor() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.MOB_GET_SUC_NOMBRE"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@SUCURSAL_ID", Data.SqlDbType.VarChar, 20)
                Pa.Value = Me.txtproveedor.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE_ID", Data.SqlDbType.VarChar, 15)
                Pa.Value = Me.ClienteId
                Cmd.Parameters.Add(Pa)

                Pa = Nothing
                Pa = New SqlParameter("@NOMBRE", SqlDbType.VarChar, 50)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                lblproveedor.Text = IIf(IsDBNull(Cmd.Parameters("@NOMBRE").Value), "", Cmd.Parameters("@NOMBRE").Value)
                'Nro_OC = IIf(IsDBNull(Cmd.Parameters("@NRO_OC").Value), "", Cmd.Parameters("@NRO_OC").Value)
                Return True
            Else
                MsgBox(ErrCon, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("SQL ValidarIngreso: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Me.txtProducto.Text = ""
            Return False
        Catch ex As Exception
            MsgBox("ValidarIngreso: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function

    Private Sub txtOc_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F3 Then
            buscarOC()
        End If
        If e.KeyValue = 13 Or e.KeyCode = Keys.F1 Then
            If e.KeyCode = Keys.F1 Then
                Me.lblremito.Visible = True
                Me.txtRemitoNro.Visible = True
                Me.txtRemitoPrefijo.Visible = True
                Me.btnRemito.Visible = True
                Me.lblsms.Text = "Ingrese Remito"
                Me.txtRemitoPrefijo.Focus()
            Else
                Dim Pa As SqlParameter
                Dim Da As SqlDataAdapter
                Dim Cmd As SqlCommand
                If VerificaOC() = True Then
                    If VerifyConnection(SQLc) Then
                        Try
                            Cmd = SQLc.CreateCommand
                            Cmd.CommandText = "Dbo.OC_ALTA_TMP"
                            Cmd.CommandType = Data.CommandType.StoredProcedure
                            Cmd.Parameters.Clear()

                            Pa = New SqlParameter("@ID_OC", SqlDbType.VarChar, 20)
                            Pa.Value = Vsecuencia 'Nro_OC
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@ORDEN_COMPRA", SqlDbType.VarChar, 100)
                            Pa.Value = ""
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@IDPROVEEDOR", SqlDbType.VarChar, 20)
                            Pa.Value = IDPROVEEDOR
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@PROCESADO", SqlDbType.Char, 1)
                            Pa.Value = "0"
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing
                            Cmd.ExecuteNonQuery()
                            Me.lblsms.Text = "Ingreso OC presione F1 para cargar Remito"
                            'lblmensaje.Text = "Ingrese Orden de compra Nº o F1 para continuar"
                        Catch sqlex As SqlException
                            MsgBox(sqlex.Message, MsgBoxStyle.Information, FrmName)
                            Me.lblsms.Text = "Ingrese OC o F1 para cargar Remito"
                        Catch ex As Exception
                            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
                            Me.lblsms.Text = "Ingrese OC o F1 para cargar Remito"
                        Finally
                            Cmd = Nothing
                            Pa = Nothing
                        End Try
                    End If
                End If
            End If
        End If
    End Sub

    Private Function VerificaOC() As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "DBO.EXIST_ODC"
                xCmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Me.ClienteId 'Me.cboCliente.Text.ToString
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PROVEEDOR", SqlDbType.VarChar, 20)
                Pa.Value = Me.txtproveedor.Text 'Me.cboCliente.Text.ToString
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@ODC", SqlDbType.VarChar, 100)
                Pa.Value = ""
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@STATUS", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                If xCmd.Parameters("@status").Value <> "1" Then
                    Return False
                End If
            Else : MsgBox(ErrCon, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub FrmOrdenCompra_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BorrarNoProcesados()
        limpiar()
        Me.estadoPrecarga = True
        TipoProceso = ""
        lblsms.Text = "F1 Iniciar carga o F2 Cargar Documento"
    End Sub

    Sub secuencia()
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        If VerifyConnection(SQLc) Then
            Cmd = SQLc.CreateCommand
            Cmd.CommandText = "get_value_for_sequence" '"Dbo.GET_DESCRIPCION_UNIDAD_PRODUCTO"
            Cmd.CommandType = Data.CommandType.StoredProcedure
            Cmd.Parameters.Clear()

            Pa = New SqlParameter("@SECUENCIA", Data.SqlDbType.VarChar, 50)
            Pa.Value = "NRO_OC"
            Cmd.Parameters.Add(Pa)
            Pa = Nothing

            Pa = New SqlParameter("@VALUE", SqlDbType.NChar, 38)
            Pa.Direction = ParameterDirection.Output
            Cmd.Parameters.Add(Pa)
            Pa = Nothing

            Cmd.ExecuteNonQuery()
            'Nro_OC = IIf(IsDBNull(Cmd.Parameters("@NRO_OC").Value), "", Cmd.Parameters("@NRO_OC").Value)
            Vsecuencia = IIf(IsDBNull(Cmd.Parameters("@VALUE").Value), "", Cmd.Parameters("@VALUE").Value)

        Else
            MsgBox(ErrCon, MsgBoxStyle.OkOnly)
            txtProducto.Focus()
        End If
    End Sub
    Private Sub Inicio()
        Dim vError As String = ""
        If Not GetClientes(vError) Then
            MsgBox(vError, MsgBoxStyle.Critical, FrmName)
            Me.Close()
        End If
        btnInicio.Enabled = True
        Me.ClienteId = Me.cmbClienteId.SelectedValue.ToString

    End Sub

    Private Function GetClientes(ByRef verror As String) As Boolean
        Dim Da As SqlDataAdapter
        Dim Ds As New System.Data.DataSet
        Dim drDSRow As Data.DataRow
        Dim drNewRow As Data.DataRow
        Dim dt As New Data.DataTable
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter
        Try
            If VerifyConnection(SQLc) Then

                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.GET_CLIENTES_BY_USER"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@USER", SqlDbType.VarChar, 30)
                Pa.Value = vUsr.CodUsuario
                xCmd.Parameters.Add(Pa)
                xCmd.Connection = SQLc
                Da.Fill(Ds, "CLIENTES")
                dt.Columns.Add("RazonSocial", GetType(System.String))
                dt.Columns.Add("Cliente_id", GetType(System.String))
                If Ds.Tables("CLIENTES").Rows.Count > 0 Then
                    'Hay mas de un cliente, los cargo en el combo.
                    For Each drDSRow In Ds.Tables("CLIENTES").Rows()
                        drNewRow = dt.NewRow()
                        drNewRow("RazonSocial") = drDSRow("RazonSocial")
                        drNewRow("Cliente_id") = drDSRow("Cliente_id")
                        dt.Rows.Add(drNewRow)
                    Next
                    Me.cmbClienteId.DropDownStyle = ComboBoxStyle.DropDownList
                    With cmbClienteId
                        .DataSource = dt
                        .DisplayMember = "RazonSocial"
                        .ValueMember = "Cliente_id"
                        .SelectedIndex = 0
                    End With
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, FrmName)
            End If
            Return True
        Catch ex As Exception
            verror = "No se puedieron cargar los clientes"
            Return False
        Finally
            Da = Nothing
            Ds = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub btnOc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        buscarOC()
    End Sub

    Sub buscarOC()
        Dim Nfrm As New FrmBuscaOc
        Try
            Nfrm.Orden_ID = txtproveedor.Text
            Nfrm.SecuenciaId = Vsecuencia
            Nfrm.ShowDialog()
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            Nfrm = Nothing
        End Try
    End Sub
    Private Sub btnInicio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInicio.Click
        iniciandoCarga()

    End Sub

    Sub iniciandoCarga()
        secuencia()
        Inicializar()
        Me.txtproveedor.ReadOnly = False
        Me.txtproveedor.Enabled = True
        Me.txtRemitoNro.ReadOnly = False
        Me.txtRemitoNro.Enabled = True
        Me.txtRemitoPrefijo.ReadOnly = False
        Me.txtRemitoPrefijo.Enabled = True
        Me.lblcodproveedor.Visible = True
        Me.txtproveedor.Visible = True
        Me.lblproveedor.Visible = True
        Me.btnProveedor.Visible = True
        Me.cmbClienteId.Enabled = True
        Me.txtproveedor.Focus()

        Inicio()

        TipoProceso = "CargaNormal"
        'lblmensaje.Text = "Ingrese Proveedor o presiones F2 para buscar"
    End Sub
    Sub limpiar()
        Me.btnFin.Visible = False
        Me.txtcantidad.Visible = False
        Me.lblcantidad.Visible = False
        Me.lblProducto.Visible = False
        Me.lblcodproducto.Visible = False
        Me.txtProducto.Visible = False
        Me.lblremito.Visible = False
        Me.txtRemitoNro.Visible = False
        Me.txtRemitoPrefijo.Visible = False
        Me.lblproveedor.Visible = False
        Me.lblcodproveedor.Visible = False
        Me.txtproveedor.Visible = False
        Me.btnProducto.Visible = False
        Me.btnRemito.Visible = False
        Me.btnProveedor.Visible = False
        Me.txtproveedor.ReadOnly = False
        Me.btnProveedor.Visible = False
        Me.lblproveedor.Visible = False
        Me.cmbClienteId.Visible = False
        Me.lblClienteId.Visible = False
        Me.lblProducto.Text = ""
        Me.lblproveedor.Text = ""
        Me.txtproveedor.Text = ""
        Me.txtRemitoPrefijo.Text = ""
        Me.txtRemitoNro.Text = ""
        Me.txtProducto.Text = ""
        'Me.lblmensaje.Text = ""
        Me.lblsms.Text = ""
    End Sub

    Sub Inicializar()

        If Me.btnInicio.Text = "F1)Inicio" Then
            Me.btnInicio.Visible = False
            Me.btnPrecargar.Visible = True
            Me.btnPrecargar.Text = "Cancelar"
            Me.estadoPrecarga = False
            'Me.btnInicio.Text = "F1)Continuar"
            Me.btnFin.Visible = True
            Me.lblproveedor.Visible = False
            Me.lblcodproveedor.Visible = False
            Me.txtproveedor.Visible = False
            Me.btnProveedor.Visible = False
            Me.lblClienteId.Visible = True
            Me.cmbClienteId.Visible = True
            Me.cmbClienteId.Focus()
        Else
            If Me.btnInicio.Text = "F1)Continuar" And Me.txtproveedor.Text <> "" Then
                If Me.txtproveedor.ReadOnly = False Then

                End If
            End If
        End If
        'Me.txtproveedor.Focus()
    End Sub

    Private Sub FrmOrdenCompra_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1 And Me.txtproveedor.ReadOnly = False And TipoProceso = ""
                iniciandoCarga()
                'secuencia()
                'Inicializar()
            Case Keys.F2 And TipoProceso <> "" And Me.txtproveedor.ReadOnly = False
                buscarProveedor()
            Case Keys.F2 And TipoProceso = ""
                precargar()
            Case Keys.F4 And Me.txtRemitoNro.ReadOnly = True
                buscarRemito()
            Case Keys.F5 And Me.txtProducto.ReadOnly = True
                buscarProducto()
        End Select
    End Sub

    Private Sub txtRemito_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRemitoNro.KeyUp
        If e.KeyCode = Keys.F4 Then
            buscarRemito()
        End If
        If (e.KeyValue = 13 Or e.KeyCode = Keys.F1) And (Me.txtRemitoPrefijo.Text <> "" And Me.txtRemitoNro.Text <> "") Then

            Dim Pa As SqlParameter
            Dim Da As SqlDataAdapter
            Dim Cmd As SqlCommand
            If Trim(Me.txtRemitoNro.Text) = "" Then
                Exit Sub
            Else
                txtRemitoNro.Text = txtRemitoNro.Text.PadLeft(8, "0"c)
                txtRemitoPrefijo.Text = txtRemitoPrefijo.Text.PadLeft(4, "0"c)
                remitoNro = txtRemitoPrefijo.Text + "-" + txtRemitoNro.Text
            End If

            If VerifyConnection(SQLc) Then
                Try
                    Cmd = SQLc.CreateCommand
                    Cmd.CommandText = "Dbo.MOB_REMITO_ALTA"
                    Cmd.CommandType = Data.CommandType.StoredProcedure
                    Cmd.Parameters.Clear()

                    'Cmd.Transaction = SQLc.BeginTransaction()
                    Pa = New SqlParameter("@ID_REMITO", SqlDbType.VarChar, 20)
                    Pa.Value = Vsecuencia 'Nro_OC 'este variable es la secuencia
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@REMITO", SqlDbType.VarChar, 20)
                    Pa.Value = remitoNro 'Me.txtRemitoNro.Text
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@IDPROVEEDOR", SqlDbType.Char, 20)
                    Pa.Value = IDPROVEEDOR
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Cmd.ExecuteNonQuery()
                    txtRemitoPrefijo.Text = ""
                    txtRemitoNro.Text = ""
                    Me.lblsms.Text = "Ingreso Remito F1 para cargar Producto"
                    txtRemitoPrefijo.Focus()
                Catch sqlex As SqlException
                    MsgBox(sqlex.Message, MsgBoxStyle.Critical, FrmName)
                    Me.lblsms.Text = "Ingrese Remito F1 para cargar Producto"
                    Me.txtRemitoNro.Text = ""
                    Me.txtRemitoPrefijo.Text = ""
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
                    Me.lblsms.Text = "Ingrese Remito F1 para cargar Producto"
                    Me.txtRemitoNro.Text = ""
                    Me.txtRemitoPrefijo.Text = ""
                Finally
                    Cmd = Nothing
                    Pa = Nothing
                End Try
            End If
            If e.KeyCode = Keys.F1 Then
                Me.txtRemitoNro.Text = ""
                Me.lblProducto.Visible = True
                Me.txtProducto.Visible = True
                Me.btnProducto.Visible = True
                Me.lblProducto.Visible = True
                lblcodproducto.Visible = True
                Me.txtProducto.Focus()
            End If
        End If
    End Sub

    Private Sub txtProducto_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProducto.KeyUp
        Dim Inact As Boolean = False
        If e.KeyValue = 13 Or e.KeyCode = Keys.F1 And txtProducto.Text <> "" Then

            o2D.Decode(Me.txtProducto.Text)
            Me.txtProducto.Text = o2D.PRODUCTO_ID

            ProductoInhabilidato(Me.cmbClienteId.SelectedValue, Me.txtProducto.Text, Inact)

            If Inact Then
                MsgBox("El producto se encuentra inhabilitado, no es posible realizar una recepcion del mismo.", MsgBoxStyle.Information, FrmName)
                Me.txtProducto.Text = ""
                Me.txtProducto.Focus()
                Exit Sub
            End If

            If ValidarBarra() = True Then
                Dim Pa As SqlParameter
                Dim Da As SqlDataAdapter
                Dim Cmd As SqlCommand
                If Trim(Me.txtProducto.Text) = "" Then
                    Exit Sub
                End If
                If VerifyConnection(SQLc) Then
                    Cmd = SQLc.CreateCommand
                    Cmd.CommandText = "Val_Pro_OC"
                    Cmd.CommandType = Data.CommandType.StoredProcedure
                    Cmd.Parameters.Clear()

                    Pa = New SqlParameter("@CLIENTE_ID", Data.SqlDbType.VarChar, 15)
                    Pa.Value = Me.ClienteId
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@PROVEEDOR_ID", Data.SqlDbType.VarChar, 20)
                    Pa.Value = IDPROVEEDOR
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                    Pa.Value = txtProducto.Text 'Producto
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@PRODUCTO", SqlDbType.VarChar, 30)
                    Pa.Direction = ParameterDirection.Output
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Cmd.ExecuteNonQuery()
                    Me.lblProducto.Text = IIf(IsDBNull(Cmd.Parameters("@PRODUCTO").Value), "", Cmd.Parameters("@PRODUCTO").Value)
                    If Me.lblProducto.Text = "" Then
                        MsgBox("Producto no existe para las OC ingresadas", MsgBoxStyle.OkOnly, FrmName)
                        txtProducto.Text = ""
                    Else
                        'lblsms.Text = "Ingreso el producto F1 para ingresar cantidad"
                        Me.lblcantidad.Visible = True
                        Me.txtcantidad.Visible = True
                        lblsms.Text = "Ingrese la cantidad."
                        Me.txtcantidad.Focus()
                    End If
                Else
                    MsgBox(ErrCon, MsgBoxStyle.OkOnly)
                    lblsms.Text = "Ingrese el producto o F1 para ingresar cantidad"
                    txtProducto.Focus()
                End If

                If e.KeyCode = Keys.F1 Then
                    Me.lblcantidad.Visible = True
                    Me.txtcantidad.Visible = True
                    lblsms.Text = "Ingreso el producto, F1 para ingresar cantidad"
                    Me.txtcantidad.Focus()
                End If
            End If
        End If
    End Sub

    Private Function ValidarBarra() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If Me.txtProducto.Text <> "" Then
                If VerifyConnection(SQLc) Then
                    Cmd = SQLc.CreateCommand
                    Cmd.CommandText = "Dbo.Val_Prod"
                    Cmd.CommandType = Data.CommandType.StoredProcedure
                    Cmd.Parameters.Clear()

                    Pa = New SqlParameter("@CLIENTE_ID", Data.SqlDbType.VarChar, 50)
                    Pa.Value = Me.ClienteId 'Me.cboCliente.Text.ToString
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@CODIGO", SqlDbType.VarChar, 50)
                    Pa.Value = Me.txtProducto.Text
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                    Pa.Direction = ParameterDirection.Output
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Cmd.ExecuteNonQuery()
                    Producto = IIf(IsDBNull(Cmd.Parameters("@PRODUCTO_ID").Value), "", Cmd.Parameters("@PRODUCTO_ID").Value)
                    Me.txtProducto.Text = Trim(UCase(Producto))
                    Return True
                Else
                    MsgBox(ErrCon, MsgBoxStyle.OkOnly)
                End If
            End If
        Catch SQLEx As SqlException
            MsgBox("SQL ValidarIngreso: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Me.txtProducto.Text = ""
            Return False
        Catch ex As Exception
            MsgBox("ValidarIngreso: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function

    Private Function BuscarDescripcion() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.GET_DESCRIPCION_UNIDAD_PRODUCTO"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@CLIENTE_ID", Data.SqlDbType.VarChar, 50)
                Pa.Value = Me.ClienteId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 50)
                Pa.Value = txtProducto.Text 'Producto
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@DESCRIPCION", SqlDbType.VarChar, 30)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@UNIDAD", SqlDbType.VarChar, 30)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                lblProducto.Text = IIf(IsDBNull(Cmd.Parameters("@DESCRIPCION").Value), "", Cmd.Parameters("@DESCRIPCION").Value)

                Return True
            Else
                MsgBox(ErrCon, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("SQL BuscarDescripcion: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("BuscarDescripcion: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function


    Private Sub btnRemito_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemito.Click
        buscarRemito()
    End Sub

    Sub buscarRemito()
        Dim Nfrm As New FrmBuscarRemito
        Try
            Nfrm.Proveedor_ID = Me.txtproveedor.Text
            Nfrm.SecuenciaId = Vsecuencia
            Nfrm.ShowDialog()
            Me.txtRemitoPrefijo.Focus()
        Catch ex As Exception

        Finally
            Nfrm = Nothing
        End Try
    End Sub

    Private Sub btnProducto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProducto.Click
        buscarProducto()
    End Sub

    Sub buscarProducto()
        Dim Nfrm As New FrmBuscarProducto
        Try
            Nfrm.Proveedor_ID = Me.txtproveedor.Text
            Nfrm.SecuenciaId = Vsecuencia
            Nfrm.ShowDialog()

            If Nfrm.Producto_ID <> Nothing Then
                Me.txtProducto.Text = Nfrm.Producto_ID
                Me.lblProducto.Text = Nfrm.Producto_Des
                Me.txtcantidad.Visible = True
                Me.lblcantidad.Visible = True
                Me.lblsms.Text = "F1 Agregar cantidad"
                Me.txtcantidad.Focus()
            Else
                Me.txtProducto.Text = ""
                Me.lblProducto.Text = ""
                Me.lblsms.Text = "Ingrese Producto"
                Me.txtProducto.Focus()
            End If
        Catch ex As Exception

        Finally
            Nfrm = Nothing
        End Try
    End Sub

    Private Sub txtcantidad_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcantidad.KeyPress
        Dim Search As String
        Dim Pos As Integer
        Search = "."
        If Not xEsFraccionable Then
            ValidarCaracterNumerico(e)
        Else
            Pos = InStr(1, Me.txtcantidad.Text, Search)
            If Pos > 0 And Asc(e.KeyChar) <> 46 Then
                If Len(Mid(Me.txtcantidad.Text, Pos + 1, Len(Me.txtcantidad.Text))) >= 5 And Asc(e.KeyChar) <> 8 Then
                    e.Handled = True
                    Me.txtcantidad.Focus()
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

    Private Sub txtcantidad_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcantidad.KeyUp
        Dim sResultado As String
        If e.KeyCode = Keys.F1 Or e.KeyCode = Keys.Enter Then
            If txtcantidad.Text <> "" And Val(Me.txtcantidad.Text) > 0 Then
                Dim Pa As SqlParameter
                Dim Da As SqlDataAdapter
                Dim Cmd As SqlCommand
                If VerifyConnection(SQLc) Then
                    If ValidarCantidadOC(sResultado) Then

                        Select Case sResultado
                            Case "0"
                                Try
                                    Cmd = SQLc.CreateCommand
                                    Cmd.CommandText = "Dbo.MOB_ALTA_PROD_TMP"
                                    Cmd.CommandType = Data.CommandType.StoredProcedure
                                    Cmd.Parameters.Clear()

                                    Pa = New SqlParameter("@ID", SqlDbType.VarChar, 20)
                                    Pa.Value = Vsecuencia
                                    Cmd.Parameters.Add(Pa)
                                    Pa = Nothing

                                    Pa = New SqlParameter("@PROVEEDOR_ID", SqlDbType.VarChar, 20)
                                    Pa.Value = Me.txtproveedor.Text
                                    Cmd.Parameters.Add(Pa)
                                    Pa = Nothing

                                    Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                                    Pa.Value = Me.txtProducto.Text
                                    Cmd.Parameters.Add(Pa)
                                    Pa = Nothing

                                    Pa = New SqlParameter("@CANTIDAD", SqlDbType.Float, 20.5)
                                    Pa.Value = Me.txtcantidad.Text
                                    Cmd.Parameters.Add(Pa)
                                    Pa = Nothing

                                    Cmd.ExecuteNonQuery()
                                    Me.txtProducto.Text = ""
                                    Me.txtcantidad.Text = ""
                                    Me.lblProducto.Text = ""
                                    Me.txtcantidad.Visible = False
                                    Me.lblcantidad.Visible = False
                                    lblsms.Text = "Ingreso cantidad, ingrese el producto"
                                    Me.txtProducto.Focus()
                                Catch sqlex As SqlException
                                    MsgBox(sqlex.Message, MsgBoxStyle.Critical, FrmName)
                                    lblsms.Text = "Ingrese cantidad"
                                    Me.txtcantidad.Text = ""
                                Catch ex As Exception
                                    MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
                                    lblsms.Text = "Ingrese cantidad"
                                    Me.txtcantidad.Text = ""
                                Finally
                                    Cmd = Nothing
                                    Pa = Nothing
                                End Try
                            Case "-1"
                                lblsms.Text = "La cantidad ingresada es inferior a la tolerancia minima del producto."
                                Me.txtcantidad.Text = ""
                            Case "1"
                                lblsms.Text = "La cantidad ingresada es superior a la tolerancia maxima del producto."
                                Me.txtcantidad.Text = ""
                        End Select
                    End If
                Else
                    MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
                End If
            Else
                lblsms.Text = "La cantidad debe ser mayor a cero."
            End If
        End If
    End Sub

    Private Function ValidarCantidadOC(ByRef sResultado As String) As Boolean
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "Mob_ValidadCantidadOC"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                'parametro de entrada
                Cmd.Parameters.Clear()
                Cmd.Parameters.Add("@cantidad", SqlDbType.BigInt, 20).Value = Me.txtcantidad.Text
                Cmd.Parameters.Add("@producto_id", SqlDbType.VarChar, 20).Value = Me.txtProducto.Text
                Cmd.Parameters.Add("@agente_id", SqlDbType.VarChar, 20).Value = Me.txtproveedor.Text
                'parametro de salida
                Cmd.Parameters.Add("@result", SqlDbType.VarChar, 5).Direction = ParameterDirection.Output

                Cmd.ExecuteNonQuery()
                sResultado = Cmd.Parameters("@result").Value
                Return True
            Else
                MsgBox(ErrCon, MsgBoxStyle.OkOnly)
            End If


        Catch SQLEx As SqlException
            MsgBox("ValidarCantidadOC: " & SQLEx.Message, MsgBoxStyle.OkOnly)
            Me.txtcantidad.Text = ""
            Return False
        Catch ex As Exception
            MsgBox("ValidarCantidadOC: " & ex.Message, MsgBoxStyle.OkOnly)
            Me.txtcantidad.Text = ""
        Finally
            Cmd = Nothing
        End Try
    End Function

    Private Sub btnFin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFin.Click
        If controlOC(Nro_OC, IDPROVEEDOR, ErrCon) Then
            Finalizar()
            limpiar()
            'Inicio()

            Me.btnInicio.Visible = True
            Me.btnInicio.Text = "F1)Inicio"
            Me.btnPrecargar.Visible = True
            Me.btnPrecargar.Text = "F2) Precargar"
            Me.estadoPrecarga = True
            Me.lbldoc.Visible = False
            Me.txtdocumento.Visible = False
            Me.txtdocumento.Text = ""
        End If
    End Sub

    Private Function controlOC(ByVal Id As String, ByVal ProveedorId As String, ByRef vError As String) As Boolean
        Dim cmd As SqlCommand
        Dim pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                cmd = SQLc.CreateCommand
                cmd.CommandText = "verificaOc"
                cmd.CommandType = CommandType.StoredProcedure

                pa = New SqlParameter("@ID", SqlDbType.VarChar, 20)
                pa.Value = Vsecuencia
                cmd.Parameters.Add(pa)
                pa = Nothing

                pa = New SqlParameter("@PROVEEDOR_ID", SqlDbType.VarChar, 20)
                pa.Value = txtproveedor.Text
                cmd.Parameters.Add(pa)
                pa = Nothing

                cmd.ExecuteNonQuery()

            Else : MsgBox(ErrCon, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SqlEx As SqlException
            MsgBox(SqlEx.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            cmd = Nothing
            pa = Nothing

        End Try
    End Function

    Private Sub Finalizar()
        If IngresarOC(Me.ClienteId, "", ErrCon) Then
            txtProducto.Text = ""
            txtcantidad.Text = ""
            lblProducto.Visible = False
            txtProducto.Visible = False
            'Inicio()
        End If
    End Sub
    Private Function IngresarOC(ByVal ClienteId As String, ByVal OC As String, ByRef vError As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "MOB_INGRESO_ODC" '"DBO.MOB_INGRESO_OC"
                xCmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@ID", SqlDbType.VarChar, 20)
                Pa.Value = Vsecuencia
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Cliente_ID", SqlDbType.VarChar, 15)
                Pa.Value = ClienteId
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PROVEEDOR_ID", SqlDbType.VarChar, 20)
                Pa.Value = Me.txtproveedor.Text
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
            Else : MsgBox(ErrCon, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SqlEx As SqlException
            MsgBox(SqlEx.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function



    Public Sub ValidarCaracterNumerico(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If (Asc(e.KeyChar) >= 32 And Asc(e.KeyChar) <= 47) Or Asc(e.KeyChar) >= 58 Then
                e.Handled = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtRemito_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemitoNro.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub txtRemitoPrefijo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemitoPrefijo.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub txtRemitoPrefijo_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRemitoPrefijo.KeyUp
        If e.KeyValue = 13 Then
            txtRemitoPrefijo.Text = txtRemitoPrefijo.Text.PadLeft(4, "0"c)
            txtRemitoNro.Focus()
        End If
        If e.KeyCode = Keys.F1 Then
            Me.txtRemitoNro.Text = ""
            Me.txtRemitoPrefijo.Text = ""
            Me.lblProducto.Visible = True
            Me.txtProducto.Visible = True
            Me.btnProducto.Visible = True
            Me.lblProducto.Visible = True
            lblcodproducto.Visible = True
            lblsms.Text = "Ingrese Producto"
            Me.txtProducto.Focus()
        End If
    End Sub


    Private Sub txtdocumento_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtdocumento.KeyUp
        If e.KeyCode = Keys.F1 Or e.KeyCode = Keys.Enter Then
            Dim Pa As SqlParameter
            Dim Da As SqlDataAdapter
            Dim Cmd As SqlCommand
            If Trim(Me.txtdocumento.Text) = "" Then
                Exit Sub
            End If
            Try
                If VerifyConnection(SQLc) Then
                    Cmd = SQLc.CreateCommand
                    Cmd.CommandText = "Mob_BuscarDocID"
                    Cmd.CommandType = Data.CommandType.StoredProcedure
                    Cmd.Parameters.Clear()

                    Pa = New SqlParameter("@DOCUMENTO_ID", Data.SqlDbType.VarChar, 20)
                    Pa.Value = Me.txtdocumento.Text
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@VALUE", SqlDbType.NChar, 38)
                    Pa.Direction = ParameterDirection.Output
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@SUCURSAL_ID", SqlDbType.VarChar, 20)
                    Pa.Direction = ParameterDirection.Output
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 20)
                    Pa.Direction = ParameterDirection.Output
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Cmd.ExecuteNonQuery()
                    Vsecuencia = IIf(IsDBNull(Cmd.Parameters("@VALUE").Value), "", Cmd.Parameters("@VALUE").Value)
                    If Vsecuencia <> "" Then
                        Me.lblproveedor.Visible = True
                        Me.txtproveedor.Visible = True
                        Me.txtproveedor.Text = IIf(IsDBNull(Cmd.Parameters("@SUCURSAL_ID").Value), "", Cmd.Parameters("@SUCURSAL_ID").Value)
                        IDPROVEEDOR = IIf(IsDBNull(Cmd.Parameters("@SUCURSAL_ID").Value), "", Cmd.Parameters("@SUCURSAL_ID").Value)
                        Me.ClienteId = Cmd.Parameters("@CLIENTE_ID").Value
                        Me.txtproveedor.ReadOnly = True
                        ValidarProveedor()
                        Me.btnProveedor.Visible = True
                        Me.btnProveedor.Enabled = False
                        Me.lblcodproveedor.Visible = True
                        Me.lblremito.Visible = True
                        Me.txtRemitoPrefijo.Visible = True
                        Me.txtRemitoNro.Visible = True
                        Me.btnRemito.Visible = True
                        Me.lblcodproducto.Visible = True
                        Me.txtProducto.Visible = True
                        Me.lblProducto.Visible = True
                        Me.btnProducto.Visible = True
                        Me.btnFin.Visible = True
                        Me.btnInicio.Visible = False
                        Me.txtdocumento.ReadOnly = True
                        Me.lblsms.Text = "Ingrese Producto"
                        Me.txtProducto.Focus()
                    End If

                Else : MsgBox(ErrCon, MsgBoxStyle.Critical, FrmName)
                    Me.txtdocumento.Text = ""

                End If
            Catch SqlEx As SqlException
                MsgBox(SqlEx.Message, MsgBoxStyle.Critical, FrmName)
                Me.txtdocumento.Text = ""
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
                Me.txtdocumento.Text = ""
            Finally
                Cmd = Nothing
                Pa = Nothing
            End Try

        End If

    End Sub

    Private Sub txtdocumento_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdocumento.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub btnPrecargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrecargar.Click
        precargar()
    End Sub

    Sub precargar()
        'If btnPrecargar.Text = "F2) Precargar" Then
        If Me.estadoPrecarga Then
            TipoProceso = "Precarga"
            Me.lbldoc.Visible = True
            Me.txtdocumento.Visible = True
            Me.txtdocumento.ReadOnly = False
            Me.txtdocumento.Text = ""
            Me.btnPrecargar.Text = "Cancelar"
            Me.estadoPrecarga = False
            Me.btnInicio.Visible = False
            Me.btnFin.Visible = True
            lblsms.Text = "Ingrese Documento, F1 para ingresar cantidad"
            Me.txtdocumento.Focus()
        Else
            BorrarNoProcesados()
            Me.lbldoc.Visible = False
            Me.txtdocumento.Visible = False
            Me.btnInicio.Visible = True
            Me.btnFin.Visible = False
            Me.btnPrecargar.Text = "F2) Precargar"
            Me.estadoPrecarga = True
            limpiar()
            Me.btnInicio.Visible = True
            Me.btnInicio.Text = "F1)Inicio"
            Me.btnPrecargar.Visible = True
            TipoProceso = ""
            lblsms.Text = "F1 Iniciar carga, F2 Cargar Nº Doc"
        End If
    End Sub

    Private Sub txtproveedor_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtproveedor.GotFocus
        lblsms.Text = "Ingrese proveedor o F2 para buscar"
    End Sub

    Private Sub txtproveedor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtproveedor.TextChanged

    End Sub

    Private Sub txtOc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtRemitoNro_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRemitoNro.TextChanged

    End Sub

    Private Sub txtProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProducto.TextChanged

    End Sub

    Private Sub txtcantidad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcantidad.TextChanged

    End Sub

    Private Sub cmbClienteId_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClienteId.SelectedIndexChanged
        Me.ClienteId = Me.cmbClienteId.SelectedValue.ToString
    End Sub

    Private Sub txtdocumento_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdocumento.LostFocus

    End Sub
End Class