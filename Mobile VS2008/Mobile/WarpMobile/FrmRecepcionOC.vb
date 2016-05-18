Imports System.Data
Imports System.Data.SqlClient

Public Class FrmRecepcionOC
    Private Const FrmName As String = "Recepcion Orden de Compra"
    Private Const ErrCon As String = "No se pudo conectar con la base de datos."
    Private Producto_id As String = ""
    Private Producto As String
    Private Oc As String
    Private Cliente As String
    Private Cantidad As Double
    Private Descripcion As String
    Private xEsFraccionable As Boolean = False
    Private ToleranciaMax As Double
    Private ToleranciaMin As Double
    Private DocumentoId As Long
    Private UnidadDesc As String
    Private IsContenedora As Boolean = False
    Private IsFinalizar As Boolean = False
    Private LoteProveedor As String
    Private Partida As String
    Private loteProveedorInicio As String
    Private partidaInicio As String
    Private CargaConfirmada As Boolean = False
    Private doc_ext As String
    Private blnFVto As Boolean = False
    Private FechaVto As String

    Private Property FlagVenc() As Boolean
        Get
            Return blnFVto
        End Get
        Set(ByVal value As Boolean)
            blnFVto = value
        End Set
    End Property

    Private Property FlagContenedora() As Boolean
        Get
            Return IsContenedora
        End Get
        Set(ByVal value As Boolean)
            IsContenedora = value
        End Set
    End Property

    Private Property Unidad() As String
        Get
            Return UnidadDesc
        End Get
        Set(ByVal value As String)
            UnidadDesc = value
        End Set
    End Property

    Private Sub FrmRecepcionOC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Inicio()
    End Sub

    Private Sub Inicio()
        Dim vError As String = ""
        If Not GetClientes(vError) Then
            MsgBox(vError, MsgBoxStyle.Critical, FrmName)
            Me.Close()
        End If
        btnInicio.Enabled = True
        btnFin.Enabled = False
        btnIngresados.Enabled = True
        lblMsg.Text = "Presione inicio o F1 para cargar la Orden de Compra"
        Me.lblLotePartida.Visible = False
        LimpiarVar()
    End Sub

    Private Function GetClientes(ByRef verror As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Da As New SqlDataAdapter
        Dim Ds As New DataSet
        Dim dt As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "GET_CLIENTES_FOR_RODC"
                Da = New SqlDataAdapter(xCmd)
                Da.Fill(Ds, "Clientes")
                If Ds.Tables("Clientes").Rows.Count > 0 Then
                    dt.Columns.Add("Cliente_id", GetType(System.String))
                    For Each drDSRow In Ds.Tables("Clientes").Rows()
                        drNewRow = dt.NewRow()
                        drNewRow("Cliente_id") = drDSRow("Cliente_id")
                        dt.Rows.Add(drNewRow)
                    Next
                    Me.cboCliente.DropDownStyle = ComboBoxStyle.DropDownList
                    With cboCliente
                        .DataSource = dt
                        .DisplayMember = "Cliente_id"
                        .ValueMember = "Cliente_id"
                        .SelectedIndex = 0
                    End With
                    If Me.cboCliente.Items.Count = 1 Then
                        Me.cboCliente.Enabled = False
                    End If
                Else
                    verror = "No tiene clientes asignados, por favor verifique su configuracion."
                    Return False
                End If
            Else
                MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            verror = SQLEx.Message
        Catch ex As Exception
            verror = ex.Message
        Finally
            xCmd = Nothing
            Da = Nothing
            Ds = Nothing
        End Try
    End Function


    Private Sub LimpiarVar()
        If Me.cboCliente.Items.Count > 1 Then
            Me.cboCliente.Enabled = True
        Else
            Me.cboCliente.Enabled = False
        End If
        Me.txtOC.Text = ""
        Me.txtOC.Enabled = False
        Me.txtRemito.Text = ""
        Me.txtRemito.Enabled = False
        Me.txtProducto.Text = ""
        Me.txtProducto.Enabled = False
        Me.txtCantidad.Text = ""
        Me.txtCantidad.Enabled = False
        Me.TxtUnidCont.Text = ""
        Me.TxtUnidCont.Enabled = False
        Me.lblNOC.Visible = False
        Me.txtOC.Visible = False
        Me.txtRemito.Visible = False
        Me.lblRemito.Visible = False
        Me.lblProducto.Visible = False
        Me.txtProducto.Visible = False
        Me.LblTitDescripcion.Visible = False
        Me.lblDescripcion.Visible = False
        Me.lblcantidad.Visible = False
        Me.txtCantidad.Visible = False
        Me.LblUnidCont.Visible = False
        Me.TxtUnidCont.Visible = False
    End Sub

    Private Sub txtOC_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOC.KeyUp
        If e.KeyValue = 13 Then
            Oc = Me.txtOC.Text
            If Trim(Me.txtOC.Text) = "" Then
                Exit Sub
            End If
            If VerificaOC() Then
                'Me.txtRemito.Enabled = tru
                lblMsg.Text = "Ingrese Nº de Remito"
                Me.txtOC.ReadOnly = True
                Me.txtOC.Text = Me.txtOC.Text.ToUpper()
                ' = Me.txtOC.Text.ToUpper
                Me.txtRemito.Enabled = True
                Me.lblRemito.Visible = True
                Me.txtRemito.Visible = True
                Me.txtRemito.ReadOnly = False
                Me.txtRemito.Focus()

                'Me.txtProducto.Enabled = True
                'lblMsg.Text = "Ingrese Producto"
                'Me.txtOC.ReadOnly = True
                'txtProducto.Visible = True
                'lblProducto.Visible = True
                'txtProducto.Focus()
            Else
                Me.txtOC.Text = ""
                Me.txtOC.Focus()
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
                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Me.cboCliente.Text.ToString
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@odc", SqlDbType.VarChar, 100)
                Pa.Value = Trim(UCase(Me.txtOC.Text.ToString))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@status", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                If xCmd.Parameters("@status").Value <> "1" Then
                    Me.txtOC.Text = ""
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

    Private Function Tolerancia() As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "DBO.TOLERANCIA_OC"
                xCmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Me.cboCliente.Text.ToString 'Cliente 'Me.cboCliente.Text.ToString
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@oc", SqlDbType.VarChar, 100)
                Pa.Value = Trim(UCase(Me.txtOC.Text.ToString))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 30)
                Pa.Value = Trim(UCase(Me.txtProducto.Text.ToString))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_LOTE", SqlDbType.VarChar, 100)
                Pa.Value = IIf(IsDBNull(UCase(Me.loteProveedorInicio)), "", UCase(Me.loteProveedorInicio))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 100)
                Pa.Value = IIf(IsDBNull(UCase(Me.partidaInicio)), "", UCase(Me.partidaInicio))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@TolMax", SqlDbType.Float)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@TolMin", SqlDbType.Float)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                ToleranciaMax = xCmd.Parameters("@TolMax").Value
                ToleranciaMin = xCmd.Parameters("@TolMin").Value
                'Cantidad = CDbl(Replace(txtCantidad.Text, ".", ","))
                Cantidad = CDbl(txtCantidad.Text)

                If Cantidad <= ToleranciaMax And Cantidad >= ToleranciaMin Then
                    Return True
                Else
                    MsgBox("La cantidad excede la tolerancia permitida", MsgBoxStyle.Critical, FrmName)
                    txtCantidad.Text = ""
                    txtCantidad.Focus()
                    Return False
                End If
            Else : MsgBox(ErrCon, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub btnAtrasVolver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAtrasVolver.Click
        SalirForm()
    End Sub

    Private Sub SalirForm()
        Try
            If IsFinalizar = False Then
                If MsgBox("Desea salir?" & vbNewLine & "La operacion en curso sera cancelada.", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                    Me.Close()
                End If
            Else
                If MsgBox("Desea salir de Recepción OC?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

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
                Pa.Value = Me.cboCliente.SelectedValue
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 50)
                Pa.Value = Producto
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

                Pa = New SqlParameter("@USA_NROLOTE", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USA_NROPARTIDA", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USA_FECHAV", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

                'agregado dfernandez para que se le asigne el valor a la variable xEsFraccionable
                Dim flgfracc As String = ""
                GetFlgFraccionable(cboCliente.Text.ToString, txtProducto.Text, flgfracc)

                Me.LblTitDescripcion.Visible = True
                Me.lblDescripcion.Visible = True
                Me.lblDescripcion.Text = IIf(IsDBNull(Cmd.Parameters("@DESCRIPCION").Value), "", Cmd.Parameters("@DESCRIPCION").Value)
                Unidad = IIf(IsDBNull(Cmd.Parameters("@UNIDAD").Value), "", Cmd.Parameters("@UNIDAD").Value)
                If (Cmd.Parameters("@USA_FECHAV").Value = "1") Then
                    FlagVenc = True
                End If
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
    Public Sub ValidarCaracterNumerico(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If (Asc(e.KeyChar) >= 32 And Asc(e.KeyChar) <= 47) Or Asc(e.KeyChar) >= 58 Then
                e.Handled = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetFlgFraccionable(ByVal Cliente_id As String, ByVal Producto_id As String, ByRef FlgFraccionable As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        GetFlgFraccionable = False
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "GET_FLG_FRACCIONABLE"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Cliente_id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 30)
                Pa.Value = Producto_id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Flg_Fraccionable", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                xCmd.ExecuteNonQuery()
                FlgFraccionable = (xCmd.Parameters("@Flg_Fraccionable").Value.ToString)
                xEsFraccionable = FlgFraccionable
                Return True
            Else : MsgBox(ErrCon, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function muestroPopUpLotePartida(ByVal obligatorioLoteProveedor As Boolean, ByVal obligatorioPartida As Boolean, ByVal obligatorioFechaVto As Boolean, ByRef pLote As String, ByRef pPartida As String, ByRef pFechaVto As String, ByVal pModoPantalla As String) As Boolean
        Dim dialogLotePartida As New frmRecepcionOCLotePartida()

        Dim loteProveedor As String = ""
        Dim partida As String = ""
        Dim fecha As String = ""

        dialogLotePartida.loteObligatorio = obligatorioLoteProveedor
        dialogLotePartida.partidaObligatoria = obligatorioPartida
        dialogLotePartida.fechaObligatoria = obligatorioFechaVto
        dialogLotePartida.ordenDeCompra = Me.txtOC.Text
        dialogLotePartida.Producto = Me.txtProducto.Text
        dialogLotePartida.clienteID = Me.cboCliente.Text
        dialogLotePartida.lote = pLote
        dialogLotePartida.partida = pPartida
        dialogLotePartida.fecha = pFechaVto
        dialogLotePartida.modoPantalla = pModoPantalla

        If dialogLotePartida.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            pLote = dialogLotePartida.lote
            pPartida = dialogLotePartida.partida
            pFechaVto = dialogLotePartida.fecha
            Return True
        Else
            pLote = ""
            pPartida = ""
            pFechaVto = ""
            Return False
        End If

        dialogLotePartida.Dispose()
    End Function

    Private Function getFlagPartidaProducto() As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim ret As String = "0"
        Dim retorno As Boolean = False

        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.getFlagPartidaProducto"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Me.cboCliente.Text.ToString
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = txtProducto.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OUTPARTIDA", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                ret = IIf(IsDBNull(Cmd.Parameters("@OUTPARTIDA").Value), "0", Cmd.Parameters("@OUTPARTIDA").Value)
                If (ret = "1") Then
                    retorno = True
                Else
                    retorno = False
                End If

                Return retorno
            Else
                Return False
            End If
            'Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, "Ocurrió un error")
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function getFlagLoteProveedorProducto() As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim ret As String = "0"
        Dim retorno As Boolean = False

        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.getFlagLoteProveedorProducto"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Me.cboCliente.Text.ToString
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = txtProducto.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OUTLOTEPROVEEDOR", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                ret = IIf(IsDBNull(Cmd.Parameters("@OUTLOTEPROVEEDOR").Value), "0", Cmd.Parameters("@OUTLOTEPROVEEDOR").Value)
                If (ret = "1") Then
                    retorno = True
                Else
                    retorno = False
                End If

                Return retorno
            Else
                Return False
            End If
            'Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, "Ocurrió un error")
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function getFlagProdsConLotePartExiten(ByRef pLoteProveedor As String, ByRef pPartida As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim ret As String = "0"
        Dim retorno As Boolean = False

        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.getFlagProdsConLotePartExiten"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Me.cboCliente.Text.ToString
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = txtProducto.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OC", SqlDbType.VarChar, 100)
                Pa.Value = Me.txtOC.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OUT", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@LOTEPROVEEDOR", SqlDbType.VarChar, 100)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PARTIDA", SqlDbType.VarChar, 100)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

                pLoteProveedor = IIf(IsDBNull(Cmd.Parameters("@LOTEPROVEEDOR").Value), "", Cmd.Parameters("@LOTEPROVEEDOR").Value)
                pPartida = IIf(IsDBNull(Cmd.Parameters("@PARTIDA").Value), "", Cmd.Parameters("@PARTIDA").Value)
                ret = IIf(IsDBNull(Cmd.Parameters("@OUT").Value), "0", Cmd.Parameters("@OUT").Value)
                If (ret = "1") Then
                    retorno = True
                Else
                    retorno = False
                End If

                Return retorno
            Else
                Return False
            End If
            'Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, "Ocurrió un error")
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub txtProducto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProducto.KeyPress

    End Sub

    Private Sub txtProducto_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProducto.KeyUp
        'Estas variables corresponden al lote de proveedor y la partida del producto en la OC.
        'Una vez validado el producto ingresado SIEMPRE muestro el popup de lote y partida.
        'Esto es porque o bien el usuario debe ingresar algun dato obligatorio o porque quiere ingresar los datos aunque no sea obligatorio.
        'Pero mas que nada porque si una orden de compra tiene productos con diferente partida y/o lote cargado tengo que poder especificar
        'que estoy recibiendo.
        'Tambien podría existir el caso de que algunos productos tengan cargado lote y/o partida y otros no tengan cargado nada. En este caso el usuario puede
        'optar por no ingresar estos datos en el popup (siempre y cuando los flags para el producto esten deshabilitados) y se va a buscar el producto que
        'no tenga ni lote ni partida.

        Dim vLoteProveedor As String = ""
        Dim vPartida As String = ""
        Dim obligatorioPartida As Boolean = False
        Dim obligatorioLoteProveedor As Boolean = False
        Dim vLotePartida As String = ""
        Dim tieneProdsConLotePart As Boolean = False
        Dim faltanDatos As Boolean = False
        Dim vDoc_ext As String = ""
        Dim obligatorioFechaVto As Boolean = False
        Dim vFechaVto As String = ""
        Dim Inact As Boolean = False

        If (e.KeyValue = 13) And Trim(Me.txtProducto.Text) <> "" Then
            'Valido código de barras o producto_id ingresado
            o2D.CLIENTE_ID = Me.cboCliente.SelectedValue
            o2D.Decode(Me.txtProducto.Text)
            Me.txtProducto.Text = o2D.PRODUCTO_ID

            ProductoInhabilidato(Me.cboCliente.SelectedValue, Me.txtProducto.Text, Inact)

            If Inact Then
                MsgBox("El producto se encuentra inhabilitado, no es posible realizar una recepcion del mismo.", MsgBoxStyle.Information, FrmName)
                Me.txtProducto.Text = ""
                Me.txtProducto.Focus()
                Exit Sub
            End If

            If ValidarBarra() = True Then
                Producto = txtProducto.Text.ToUpper
                txtProducto.Text = txtProducto.Text.ToUpper

                'Me fijo si existe el producto cargado con propiedades distintas, si es así el usuario debe especificar que producto va a recibir
                tieneProdsConLotePart = getFlagProdsConLotePartExiten(vLoteProveedor, vPartida)

                If (tieneProdsConLotePart) Then
                    obligatorioFechaVto = getFlagProdsConFVto()
                    If Not muestroPopUpLotePartida(obligatorioLoteProveedor, obligatorioPartida, obligatorioFechaVto, vLoteProveedor, vPartida, vFechaVto, "Seleccion") Then
                        Exit Sub
                    End If
                Else
                    Me.LoteProveedor = ""
                    Me.Partida = ""
                End If

                Me.LoteProveedor = vLoteProveedor
                Me.Partida = vPartida
                Me.loteProveedorInicio = Me.LoteProveedor
                Me.partidaInicio = Me.Partida

                'MessageBox.Show("LOTE: " + Me.LoteProveedor + ", PARTIDA: " + Me.Partida)

                'En este punto ya tengo identificado el producto que voy a ingresar.

                'Valido que haya alguna Orden de compra con ese producto, lote de proveedor y/o partida que no esté finalizada.
                If ValidarIngreso(vDoc_ext) = True Then
                    'En este punto se que el  producto que habia seleccionado esta en condiciones de ingresarse.
                    'Ahora controlo los datos obligatorios del mismo, si falta alguno debe ingresarlo. Si no falta nada ingresa cantidad.
                    Me.doc_ext = vDoc_ext
                    'Ahora controlo que con todos los datos CLIENTE_ID, ORDEN_DE_COMPRA, PRODUCTO_ID, NRO_LOTE, NRO_PARTIDA no se encuentre ya cargado en la tabla
                    'INGRESO_OC.
                    If GetYaExisteProductoCargado() = True Then
                        Etiqueta()

                        BuscarDescripcion()
                    Else
                        MsgBox("Ya existe el producto, vaya a ingresados si quiere modificar", MsgBoxStyle.OkOnly)
                        txtProducto.Text = ""
                        txtProducto.Focus()
                        Exit Sub
                    End If

                    'Obtengo si la partida es obligatoria.
                    obligatorioPartida = getFlagPartidaProducto()
                    'Obtengo si el lote de proveedor es obligatorio.
                    obligatorioLoteProveedor = getFlagLoteProveedorProducto()

                    obligatorioFechaVto = getFlagProdsConFVto()

                    'Me fijo dependiendo los datos obligatorios si falta alguno.
                    If ((obligatorioLoteProveedor And Me.LoteProveedor = "") Or (obligatorioPartida And Me.Partida = "") Or (obligatorioFechaVto And Me.FechaVto = "")) Then
                        'Falta cargar algún dato, por lo tanto muestro la ventana popup
                        'Asi algun dato ya está cargado no puede modificarse.
                        'MessageBox.Show("vLoteProveedor: " + vLoteProveedor + ", vPartida: " + vPartida)
                        If Not muestroPopUpLotePartida(obligatorioLoteProveedor, obligatorioPartida, obligatorioFechaVto, vLoteProveedor, vPartida, vFechaVto, "Ingreso") Then
                            Exit Sub
                        End If

                        Me.Partida = vPartida
                        Me.LoteProveedor = vLoteProveedor
                        Me.FechaVto = vFechaVto
                    End If

                    Me.txtProducto.ReadOnly = True
                    Me.txtCantidad.Visible = True
                    Me.lblcantidad.Visible = True
                    Me.txtCantidad.Enabled = True
                    Me.txtCantidad.ReadOnly = False
                    txtCantidad.Text = ""
                    txtCantidad.Focus()

                    faltanDatos = False

                    If (obligatorioPartida And Me.Partida = "") Then
                        faltanDatos = True
                    End If

                    If (obligatorioLoteProveedor And Me.LoteProveedor = "") Then
                        faltanDatos = True
                    End If

                    If (obligatorioFechaVto And Me.FechaVto = "") Then
                        faltanDatos = True
                    End If

                    If (faltanDatos) Then
                        MessageBox.Show("No se completaron los datos obligatorios, no puede continuar con el ingreso", "Faltan datos")
                        txtProducto.Text = ""
                        txtProducto.Focus()
                        Exit Sub
                    End If

                    Me.LoteProveedor = vLoteProveedor
                    Me.Partida = vPartida
                    Me.FechaVto = vFechaVto

                    vLotePartida = ""

                    If (Not String.IsNullOrEmpty(Me.LoteProveedor)) Then
                        vLotePartida = "Lote: " + Me.LoteProveedor
                    End If

                    If (vLotePartida <> "" And Not String.IsNullOrEmpty(Me.Partida)) Then
                        vLotePartida += ", "
                    End If

                    If (Not String.IsNullOrEmpty(Me.Partida)) Then
                        vLotePartida += "Partida: " + Me.Partida
                    End If

                    If (vLotePartida <> "") Then
                        Me.lblLotePartida.Text = vLotePartida
                        Me.lblLotePartida.Visible = True
                    Else
                        Me.lblLotePartida.Visible = False
                    End If

                    Me.LoteProveedor = vLoteProveedor
                    Me.Partida = vPartida

                    If o2D.SerializacionIngreso Then
                        Me.txtCantidad.Text = o2D.QtySeries
                        Me.txtCantidad.Enabled = True
                        Me.txtCantidad.SelectAll()
                        Me.txtCantidad.Focus()
                        Me.TxtUnidCont.Text = 1
                    End If

                Else
                    If (vLotePartida <> "") Then
                        MsgBox("No hay pendientes de ingreso con el producto: " + txtProducto.Text + ", " + vLotePartida, MsgBoxStyle.OkOnly)
                    Else
                        MsgBox("No hay pendientes de ingreso con el producto: " + txtProducto.Text, MsgBoxStyle.OkOnly)
                    End If

                    txtProducto.Text = ""
                    txtProducto.Focus()
                End If
            End If
        End If
    End Sub
    Private Sub Etiqueta()
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter

        'Dim xdesc As String
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.ConfEtibyProd"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 30)
                Pa.Value = txtProducto.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 20)
                Pa.Value = Me.cboCliente.Text.ToString
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Msg", SqlDbType.VarChar, 200)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                lblMsg.Text = IIf(IsDBNull(Cmd.Parameters("@Msg").Value), "", Cmd.Parameters("@Msg").Value)
            Else
                lblMsg.Text = ErrCon
                'Return False
            End If
            'Return True
        Catch SQLEx As SqlException
            MsgBox("ExisteNavePosicion SQL: " & SQLEx.Message)
            'Return False
        Catch ex As Exception
            MsgBox("ExisteNavePosicion: " & ex.Message)
            'Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Sub

    Private Function ValidarBarra() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.Val_Prod"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@CLIENTE_ID", Data.SqlDbType.VarChar, 50)
                Pa.Value = Me.cboCliente.Text.ToString
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

                If Producto = "" Then
                    MsgBox("No se encontró el producto")
                    Return False
                Else
                    Me.txtProducto.Text = Trim(UCase(Producto))
                End If

                Return True
            Else
                MsgBox(ErrCon, MsgBoxStyle.OkOnly)
                Return False
            End If
        Catch SQLEx As SqlException
            MsgBox("SQL ValidarIngreso: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Me.txtProducto.Text = ""
            Return False
        Catch ex As Exception
            MsgBox("ValidarIngreso: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function
    Private Sub Inicializar()
        Try
            Me.lblNOC.Visible = True
            Me.txtOC.Visible = True
            Me.txtOC.ReadOnly = False
            txtOC.Enabled = True
            txtProducto.Enabled = True
            txtProducto.ReadOnly = False
            txtCantidad.Enabled = True
            txtCantidad.ReadOnly = False
            'TxtUnidCont.Enabled = False
            'TxtUnidCont.ReadOnly = True
            btnInicio.Enabled = False
            btnFin.Enabled = True
            btnIngresados.Enabled = True
            lblMsg.Text = "Ingrese Nº Orden de Compra"
            IsFinalizar = False
            txtOC.Focus()
        Catch ex As Exception
            MsgBox(ErrCon, MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Function ValidarIngreso(ByRef pDoc_ext As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "DBO.EXIST_OCP"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Me.cboCliente.Text.ToString
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@odc", SqlDbType.VarChar, 100)
                Pa.Value = Trim(UCase(Me.txtOC.Text.ToString))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@producto_id", SqlDbType.VarChar, 30)
                Pa.Value = Trim(UCase(Me.txtProducto.Text.ToString))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@loteProveedor", SqlDbType.VarChar, 100)
                Pa.Value = Trim(UCase(Me.LoteProveedor))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@partida", SqlDbType.VarChar, 100)
                Pa.Value = Trim(UCase(Me.Partida))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@status", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@doc_ext", SqlDbType.VarChar, 100)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()
                If xCmd.Parameters("@status").Value = "1" Then
                    'MessageBox.Show("Status= " + xCmd.Parameters("@status").Value.ToString + "; Doc_ext = " + xCmd.Parameters("@doc_ext").Value.ToString)
                    pDoc_ext = xCmd.Parameters("@doc_ext").Value.ToString
                    Me.txtProducto.Focus()
                    Return True
                End If
            Else : MsgBox(ErrCon, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch sqlex As SqlException
            'MsgBox("No existe producto " & Me.txtProducto.Text & " para esta Orden de Compra " & Me.txtOC.Text, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub btnInicio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInicio.Click
        Inicializar()
    End Sub
    Private Sub resizeContenedora()
        If FlagContenedora = True Then
            TxtUnidCont.Visible = True
            TxtUnidCont.Enabled = True
            LblUnidCont.Visible = True
            If Trim(TxtUnidCont.Text) <> "" Then
                TxtUnidCont.SelectAll()
            End If
            TxtUnidCont.Focus()
        End If
    End Sub

    Private Sub txtCantidad_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyUp
        If e.KeyCode = 13 Then 'And txtCantidad.Text <> "" Then
            If txtCantidad.Text <> "" Then
                If Trim(Me.txtCantidad.Text) <> "" Then
                    If IsNumeric(Me.txtCantidad.Text) And o2D.SerializacionIngreso Then
                        If o2D.QtySeries <> CLng(Me.txtCantidad.Text) Then
                            MsgBox("La cantidad ingresada no se corresponde con la cantidad de series leidas.", MsgBoxStyle.OkOnly, FrmName)
                            Me.txtCantidad.Text = ""
                            Me.txtCantidad.Text = o2D.QtySeries
                            Me.txtCantidad.SelectAll()
                            Me.txtCantidad.Focus()
                            Exit Sub
                        End If
                    End If
                End If
                If Tolerancia() = True Then
                    Try
                        Dim clsGuardado As New clsGuardadoManual

                        If clsGuardado.GetFlgContenedora(cboCliente.Text.ToString, txtProducto.Text) Then
                            FlagContenedora = True
                            txtCantidad.ReadOnly = True
                            resizeContenedora()
                        Else
                            Dim Pa As SqlParameter
                            Dim Cmd As SqlCommand
                            If GetYaExisteProductoCargado() = True Then
                                'If Val(txtCantidad.Text) > 0 Then 'validacion incorrecta
                                If txtCantidad.Text > 0 Then
                                    Dim Rta As Object = MsgBox("¿Confirma el Ingreso de la nueva Orden de Compra?", MsgBoxStyle.YesNo, FrmName)
                                    If Rta = vbYes Then
                                        If VerifyConnection(SQLc) Then
                                            Cmd = SQLc.CreateCommand
                                            Cmd.CommandText = "Dbo.INGRESO_OC_ALTA"
                                            Cmd.CommandType = Data.CommandType.StoredProcedure
                                            Cmd.Parameters.Clear()

                                            Cmd.Transaction = SQLc.BeginTransaction()
                                            Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                                            Pa.Value = Me.cboCliente.SelectedValue
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                                            Pa.Value = txtProducto.Text
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@ORDEN_COMPRA", SqlDbType.VarChar, 100)
                                            Pa.Value = txtOC.Text
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@CANTIDAD", SqlDbType.Float)
                                            'Pa.Value = CDbl(Replace(txtCantidad.Text, ".", ","))
                                            Pa.Value = CDbl(txtCantidad.Text)
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@FECHA", SqlDbType.DateTime)
                                            Pa.Value = DateTime.Today
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@PROCESADO", SqlDbType.Char, 1)
                                            Pa.Value = "0"
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@LOTEPROVEEDOR", SqlDbType.VarChar, 100)
                                            Pa.Value = Me.LoteProveedor
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@PARTIDA", SqlDbType.VarChar, 100)
                                            Pa.Value = Me.Partida
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@DOC_EXT", SqlDbType.VarChar, 100)
                                            Pa.Value = Me.doc_ext
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@FECHA_VTO", SqlDbType.VarChar, 20)
                                            Pa.Value = Me.FechaVto
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Cmd.ExecuteNonQuery()
                                            Cmd.Transaction.Commit()
                                            Me.txtProducto.Text = ""
                                            Me.txtProducto.ReadOnly = False
                                            Me.lblDescripcion.Visible = False
                                            lblDescripcion.Text = ""
                                            Me.LblTitDescripcion.Visible = False
                                            Me.lblcantidad.Visible = False
                                            txtCantidad.Text = ""
                                            Me.txtCantidad.Visible = False
                                            lblMsg.Text = "Ingrese Producto"
                                            Me.LoteProveedor = ""
                                            Me.Partida = ""
                                            Me.FechaVto = ""
                                            Me.lblLotePartida.Text = ""
                                            Me.lblLotePartida.Visible = False
                                            txtProducto.Focus()
                                        Else
                                            MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
                                        End If
                                    End If
                                End If
                            Else
                                'If Val(txtCantidad.Text) > 0 Then
                                If txtCantidad.Text > 0 Then
                                    Dim Rta As Object = MsgBox("¿Confirma la modificacion de la Orden de Compra?", MsgBoxStyle.YesNo, FrmName)
                                    If Rta = vbYes Then
                                        If VerifyConnection(SQLc) Then
                                            Cmd = SQLc.CreateCommand
                                            Cmd.CommandText = "Dbo.INGRESO_OC_ACTUALIZA"
                                            Cmd.CommandType = Data.CommandType.StoredProcedure
                                            Cmd.Parameters.Clear()

                                            Cmd.Transaction = SQLc.BeginTransaction()
                                            Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                                            Pa.Value = Me.cboCliente.SelectedValue
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                                            Pa.Value = txtProducto.Text
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@ORDEN_COMPRA", SqlDbType.VarChar, 100)
                                            Pa.Value = txtOC.Text
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@CANTIDAD", SqlDbType.Float, 20)
                                            Pa.Value = txtCantidad.Text
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@LOTEPROVEEDOR", SqlDbType.VarChar, 100)
                                            Pa.Value = Me.LoteProveedor
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@PARTIDA", SqlDbType.VarChar, 100)
                                            Pa.Value = Me.Partida
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Pa = New SqlParameter("@DOC_EXT", SqlDbType.VarChar, 100)
                                            Pa.Value = Me.doc_ext
                                            Cmd.Parameters.Add(Pa)
                                            Pa = Nothing

                                            Cmd.ExecuteNonQuery()
                                            Cmd.Transaction.Commit()
                                            Me.txtProducto.Text = ""
                                            Me.txtProducto.ReadOnly = False
                                            Me.lblDescripcion.Visible = False
                                            lblDescripcion.Text = ""
                                            Me.LblTitDescripcion.Visible = False
                                            Me.lblcantidad.Visible = False
                                            txtCantidad.Text = ""
                                            Me.txtCantidad.Visible = False
                                            lblMsg.Text = "Ingrese Producto"
                                            Me.LoteProveedor = ""
                                            Me.Partida = ""
                                            Me.doc_ext = ""
                                            Me.FechaVto = ""
                                            Me.lblLotePartida.Text = ""
                                            Me.lblLotePartida.Visible = False
                                            txtProducto.Focus()
                                        Else
                                            MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
                    End Try
                End If 'end tolerancia
                'Else
                '    MsgBox("Ingrese Cantidad mayor a 0", MsgBoxStyle.Exclamation, FrmName)
            End If 'end txtCantidad.Text=""
        End If 'end de e.keyCode =13
    End Sub

    Private Function GetYaExisteProductoCargado() As Boolean
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim Cmd As SqlCommand
        Dim Ds As New Data.DataSet

        If VerifyConnection(SQLc) Then
            Cmd = SQLc.CreateCommand
            Da = New SqlDataAdapter(Cmd)
            Cmd.Parameters.Add("@CLIENTE_ID", Data.SqlDbType.VarChar, 15).Value = UCase(Me.cboCliente.Text) & ""
            Cmd.Parameters.Add("@PRODUCTO_ID", Data.SqlDbType.VarChar, 30).Value = txtProducto.Text & ""
            Cmd.Parameters.Add("@ORDEN_COMPRA", Data.SqlDbType.VarChar, 100).Value = txtOC.Text
            Cmd.Parameters.Add("@LOTE_PROVEEDOR", Data.SqlDbType.VarChar, 100).Value = Me.LoteProveedor
            Cmd.Parameters.Add("@PARTIDA", Data.SqlDbType.VarChar, 100).Value = Me.Partida

            Cmd.CommandType = Data.CommandType.StoredProcedure
            Cmd.CommandText = "INGRESO_OC_EXISTE_PROD"
            Da.Fill(Ds, "Consulta")
            If Not (Ds Is Nothing) And Ds.Tables.Count > 0 And Ds.Tables("Consulta").Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Else
            MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
            Return False
        End If
    End Function
    Private Sub Consultaingresados()
        Dim Nfrm As New FrmConsultaRecepOC
        Try
            Nfrm.vCliente = Me.cboCliente.SelectedValue
            Nfrm.vOC = txtOC.Text
            Nfrm.ShowDialog()

            If Nfrm.Producto_ID <> Nothing Then
                LblTitDescripcion.Visible = True
                txtCantidad.Visible = True
                lblcantidad.Visible = True
                lblDescripcion.Visible = True
                txtProducto.ReadOnly = True
                txtProducto.Text = Nfrm.Producto_ID
                lblDescripcion.Text = Nfrm.DescripcionProducto
                txtCantidad.Text = Nfrm.Cantidad
                TxtUnidCont.Text = Nfrm.Cant_Cont
                resizeContenedora()
                lblMsg.Text = "Ingrese Cantidad"
                txtCantidad.ReadOnly = False
                txtCantidad.Focus()
            Else
                If txtCantidad.Visible = False Then
                    LblTitDescripcion.Visible = False
                    txtCantidad.Visible = False
                    lblcantidad.Visible = False
                    lblDescripcion.Visible = False
                    txtProducto.ReadOnly = False
                    txtProducto.Text = ""
                    txtCantidad.Text = ""
                    txtProducto.Focus()
                End If
            End If

        Catch ex As Exception

        Finally
            Nfrm = Nothing
        End Try
    End Sub
    Private Sub Consultacontenedoras()
        Dim Nfrm As New FrmContenedorasOC
        Try
            Nfrm.Confirmado = False
            Nfrm.Cantidad = txtCantidad.Text
            Nfrm.Cant_Cont = TxtUnidCont.Text
            Nfrm.Producto_ID = txtProducto.Text
            Nfrm.DescripcionProducto = lblDescripcion.Text
            Nfrm.vCliente = Me.cboCliente.SelectedValue
            Nfrm.ContenedoraEspecifica = o2D.Contenedora
            Nfrm.vOC = txtOC.Text
            Nfrm.Unidad = Unidad()
            Nfrm.loteProveedor = Me.LoteProveedor
            Nfrm.partida = Me.Partida
            Nfrm.doc_ext = Me.doc_ext
            Nfrm.fecha = Me.FechaVto

            Nfrm.ShowDialog()
            CargaConfirmada = Nfrm.Confirmado
            If Nfrm.Producto_ID <> Nothing Then
                LblTitDescripcion.Visible = True
                txtCantidad.Visible = True
                lblcantidad.Visible = True
                lblDescripcion.Visible = True
                txtProducto.ReadOnly = True
                txtProducto.Text = Nfrm.Producto_ID
                lblDescripcion.Text = Nfrm.DescripcionProducto
                txtCantidad.Text = Nfrm.Cantidad
                TxtUnidCont.Text = Nfrm.Cant_Cont
                TxtUnidCont.Enabled = FlagContenedora
                TxtUnidCont.ReadOnly = FlagContenedora
                lblMsg.Text = "Ingrese Cantidad"
                txtCantidad.Focus()
            Else
                Me.txtProducto.Text = ""
                Me.txtProducto.ReadOnly = False
                Me.lblDescripcion.Visible = False
                lblDescripcion.Text = ""
                Me.LblTitDescripcion.Visible = False
                Me.lblcantidad.Visible = False
                txtCantidad.Text = ""
                Me.txtCantidad.Visible = False
                LblUnidCont.Visible = False
                TxtUnidCont.Text = ""
                TxtUnidCont.Visible = False
                LblUnidCont.Visible = False
                lblMsg.Text = "Ingrese Producto"

                Me.lblLotePartida.Text = ""
                Me.lblLotePartida.Visible = False
                txtProducto.Focus()
            End If

        Catch ex As Exception

        Finally
            Nfrm = Nothing
        End Try
    End Sub
    Private Sub btnIngresados_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIngresados.Click
        Consultaingresados()
    End Sub

    Private Sub btnFin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFin.Click
        Finalizar()
    End Sub
    Private Sub Finalizar()
        'MessageBox.Show("Finalizar")
        Dim Trans As SqlTransaction
        Try
            Trans = SQLc.BeginTransaction
            If IngresarOC(Me.cboCliente.Text, txtOC.Text, ErrCon, Trans) Then
                If Not o2D.GuardarSeries(Trans) Then
                    Exit Sub
                End If
                Trans.Commit()
                txtProducto.Text = ""
                lblDescripcion.Text = ""
                txtCantidad.Text = ""
                txtOC.Text = ""
                Me.LoteProveedor = ""
                Me.Partida = ""
                Me.FechaVto = ""
                Me.doc_ext = ""
                Me.lblLotePartida.Text = ""
                Me.lblLotePartida.Visible = False
                lblProducto.Visible = False
                txtProducto.Visible = False
                IsFinalizar = True
                Inicio()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub
    Private Function IngresarOC(ByVal ClienteId As String, ByVal OC As String, ByRef vError As String, Optional ByVal Trans As SqlTransaction = Nothing) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                'MessageBox.Show("IngresarOC")
                xCmd = SQLc.CreateCommand
                If Not IsNothing(Trans) Then
                    xCmd.Transaction = Trans
                End If

                xCmd.CommandText = "DBO.INGRESA_OC"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Cliente_ID", SqlDbType.VarChar, 15)
                Pa.Value = ClienteId
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OC", SqlDbType.VarChar, 50)
                Pa.Value = OC
                xCmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@Remito", SqlDbType.VarChar, 30)
                Pa.Value = txtRemito.Text
                xCmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@Documento_ID", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                'LRojas 02/03/2012 TrackerID 3806: Usuario demonio de Impresion
                Pa = New SqlParameter("@Usuario_Imp", SqlDbType.VarChar, 20)
                Pa.Value = vUsr.CodUsuario
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                DocumentoId = xCmd.Parameters("@Documento_id").Value
                'MessageBox.Show("Documento ID: " + DocumentoId.ToString())
            Else : MsgBox(ErrCon, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SqlEx As SqlException
            vError = SqlEx.Message
            MessageBox.Show(vError)
        Catch ex As Exception
            vError = ex.Message
            MessageBox.Show(vError)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function
    ' Private Function IngresarODC(ByVal ClienteId As String, ByVal ProductoId As String, ByVal ODC As String, ByVal Qty As Double, _
    '                                 ByVal LoteProv As String, ByVal FechaVto As String, ByVal Pallet As String, ByRef vError As String, _
    '                                 ByVal TipoEti As String, ByVal PatVehic As String, ByVal CantContenedoras As Integer) As Boolean
    'Dim xCmd As SqlCommand
    'Dim Pa As SqlParameter
    '    Try
    '        If VerifyConnection(SQLc) Then
    '            xCmd = SQLc.CreateCommand
    '            xCmd.CommandText = "DBO.INGRESA_OC"
    '            xCmd.CommandType = CommandType.StoredProcedure
    '            Pa = New SqlParameter("@Cliente_ID", SqlDbType.VarChar, 15)
    '            Pa.Value = ClienteId
    '            xCmd.Parameters.Add(Pa)
    '            Pa = Nothing
    '            Pa = New SqlParameter("@Producto_ID", SqlDbType.VarChar, 30)
    '            Pa.Value = ProductoId
    '            xCmd.Parameters.Add(Pa)
    '            Pa = Nothing
    '            Pa = New SqlParameter("@ODC", SqlDbType.VarChar, 50)
    '            Pa.Value = ODC
    '            xCmd.Parameters.Add(Pa)

    'xCmd.ExecuteNonQuery()
    'Else : MsgBox(ErrCon, MsgBoxStyle.Critical, FrmName)
    '            Return False
    '        End If
    '        Return True
    '    Catch SqlEx As SqlException
    '        vError = SqlEx.Message
    '    Catch ex As Exception
    '        vError = ex.Message
    '    Finally
    '        xCmd = Nothing
    '        Pa = Nothing
    '    End Try
    'End Function
    'Sub Validar_IngresoOC(ByVal Cant As Double, ByVal Cant_Cont As Double, Optional ByVal CableBolsa As Boolean = False)
    '    Dim Pa As SqlParameter
    '    Dim Da As SqlDataAdapter
    '    Dim Cmd As SqlCommand

    '    Dim Str_Sql, Str_Msg As String

    '    If GetYaExisteProductoCargado() = True Then
    '        Str_Sql = "Dbo.INGRESO_OC_ALTA"
    '        Str_Msg = "¿Confirma el Ingreso de la nueva Orden de Compra?"
    '    Else
    '        Str_Sql = "Dbo.INGRESO_OC_ACTUALIZA"
    '        Str_Msg = "¿Confirma la modificacion de la Orden de Compra?"
    '    End If
    '    If Cant > 0 And CableBolsa = False Then
    '        Dim Rta As Object = MsgBox(Str_Msg, MsgBoxStyle.YesNo, FrmName)
    '        If Rta = vbYes Then
    '            If VerifyConnection(SQLc) Then
    '                Cmd = SQLc.CreateCommand
    '                Cmd.CommandText = Str_Sql
    '                Cmd.CommandType = Data.CommandType.StoredProcedure
    '                Cmd.Parameters.Clear()

    '                Cmd.Transaction = SQLc.BeginTransaction()
    '                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
    '                Pa.Value = Me.cboCliente.SelectedValue
    '                Cmd.Parameters.Add(Pa)
    '                Pa = Nothing

    '                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
    '                Pa.Value = txtProducto.Text
    '                Cmd.Parameters.Add(Pa)
    '                Pa = Nothing

    '                Pa = New SqlParameter("@ORDEN_COMPRA", SqlDbType.VarChar, 100)
    '                Pa.Value = txtOC.Text
    '                Cmd.Parameters.Add(Pa)
    '                Pa = Nothing

    '                Pa = New SqlParameter("@CANTIDAD", SqlDbType.Float)
    '                Pa.Value = Cant
    '                Cmd.Parameters.Add(Pa)
    '                Pa = Nothing

    '                If Me.PnlContenedoras.Visible Then
    '                    Pa = New SqlParameter("@CANT_CONTENEDORAS", SqlDbType.Float)
    '                    Pa.Value = Cant_cont
    '                    Cmd.Parameters.Add(Pa)
    '                    Pa = Nothing
    '                End If

    '                If Str_Sql = "Dbo.INGRESO_OC_ALTA" Then
    '                    Pa = New SqlParameter("@FECHA", SqlDbType.DateTime)
    '                    Pa.Value = DateTime.Today
    '                    Cmd.Parameters.Add(Pa)
    '                    Pa = Nothing

    '                    Pa = New SqlParameter("@PROCESADO", SqlDbType.Char, 1)
    '                    Pa.Value = "0"
    '                    Cmd.Parameters.Add(Pa)
    '                    Pa = Nothing
    '                End If


    '                Cmd.ExecuteNonQuery()
    '                Cmd.Transaction.Commit()
    '                Me.txtProducto.Text = ""
    '                Me.txtProducto.ReadOnly = False
    '                Me.lblDescripcion.Visible = False
    '                lblDescripcion.Text = ""
    '                Me.LblTitDescripcion.Visible = False
    '                Me.lblcantidad.Visible = False
    '                txtCantidad.Text = ""
    '                Me.txtCantidad.Visible = False
    '                TxtUnidCont.Text = ""
    '                Me.PnlContenedoras.Visible = False
    '                lblMsg.Text = "Ingrese Producto"
    '                txtProducto.Focus()
    '            Else
    '                MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
    '            End If
    '        End If
    '    End If



    'End Sub

    Private Sub txtCantidad_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress
        Dim Search As String
        Dim Pos As Integer
        Search = "."
        If Not xEsFraccionable Then
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

    Private Sub TxtUnidCont_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtUnidCont.GotFocus
        If o2D.SerializacionEgreso Then
            TxtUnidCont.Text = 1
            TxtUnidCont.SelectAll()
        End If
    End Sub

    Private Sub TxtUnidCont_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtUnidCont.KeyPress
        Dim Search As String
        Dim Pos As Integer
        Search = "."
        If Not xEsFraccionable Then
            ValidarCaracterNumerico(e)
        Else
            Pos = InStr(1, Me.TxtUnidCont.Text, Search)
            If Pos > 0 And Asc(e.KeyChar) <> 46 Then
                If Len(Mid(Me.TxtUnidCont.Text, Pos + 1, Len(Me.TxtUnidCont.Text))) >= 5 And Asc(e.KeyChar) <> 8 Then
                    e.Handled = True
                    Me.TxtUnidCont.Focus()
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

    Private Sub TxtUnidCont_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtUnidCont.KeyUp
        Dim TmpCliente As String = "", TmpOC As String = "", TmpRemito As String = ""
        If e.KeyCode = 13 Then
            If TxtUnidCont.Text <> "" And Val(Me.TxtUnidCont.Text) > 0 Then
                Try
                    If Val(TxtUnidCont.Text) <= Val(txtCantidad.Text) Then
                        If o2D.SerializacionIngreso And o2D.QtySeries > 0 Then
                            If Me.TxtUnidCont.Text > 1 Then
                                MsgBox("El producto serializado solo puede ser recepcionado en una sola contenedora.", MsgBoxStyle.Information, FrmName)
                                Me.TxtUnidCont.Text = ""
                                Exit Try
                            End If
                        End If

                        If o2D.SerializacionEgreso Then
                            If Me.TxtUnidCont.Text > 1 Then
                                MsgBox("El producto solo puede ser recepcionado en una sola contenedora.", MsgBoxStyle.Information, FrmName)
                                Me.TxtUnidCont.Text = ""
                                Exit Try
                            End If
                        End If

                        Consultacontenedoras()
                        If CargaConfirmada Then
                            If o2D.SerializacionIngreso Then
                                TmpCliente = Me.cboCliente.SelectedValue
                                TmpOC = Me.txtOC.Text
                                TmpRemito = Me.txtRemito.Text
                                Finalizar()
                                Me.cboCliente.SelectedValue = TmpCliente
                                Me.cboCliente.Visible = True
                                Me.cboCliente.Enabled = False

                                Me.txtOC.Visible = True
                                Me.txtOC.Enabled = False
                                Me.txtOC.Text = TmpOC
                                Me.lblNOC.Visible = True

                                Me.txtRemito.Text = TmpRemito
                                Me.txtRemito.Visible = True
                                Me.txtRemito.Enabled = False
                                Me.lblRemito.Visible = True

                                Me.txtProducto.Visible = True
                                Me.txtProducto.Text = ""
                                Me.txtProducto.Enabled = True
                                Me.lblProducto.Visible = True
                                Me.txtProducto.Focus()
                                lblMsg.Text = "Ingrese Producto"
                            End If
                        Else
                            TxtUnidCont.Enabled = True
                            TxtUnidCont.Focus()
                            Return
                        End If
                        'Validar_IngresoOC(Val(txtCantidad.Text), Val(TxtUnidCont.Text))
                    Else
                        MsgBox("La Cantidad de Unidades Contenedoras debe ser menor o igual que la Cantidad Total", MsgBoxStyle.Exclamation, FrmName)
                        TxtUnidCont.Text = "0"
                        TxtUnidCont.Focus()
                    End If
                Catch ex As Exception
                    MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
                End Try
            End If
        End If
    End Sub
    Private Sub FrmRecepcionOC_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Inicializar()
                'cerrar()
            Case Keys.F2
                Finalizar()
                'Modifica()
                'cancelar()
            Case Keys.F3
                Consultaingresados()
            Case Keys.F4
                SalirForm()
        End Select
    End Sub

    Private Sub txtRemito_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRemito.KeyUp
        If e.KeyValue = 13 Then
            'If Me.txtRemito.Text = "" Then
            'Me.txtRemito.Text = Me.txtRemito.Text.Trim.PadLeft(10, "0")
            Me.txtProducto.Enabled = True
            lblMsg.Text = "Ingrese Producto"
            Me.txtRemito.ReadOnly = True
            'Me.txtOC.ReadOnly = True
            txtProducto.Visible = True
            lblProducto.Visible = True
            txtProducto.Focus()
            'End If

        End If
    End Sub

    Private Sub txtRemito_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemito.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Function getFlagProdsConFVto() As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim ret As String = "0"
        Dim retorno As Boolean = False

        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.getFlagVtoProducto"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Me.cboCliente.Text.ToString
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = txtProducto.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OUTFVTO", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

                ret = IIf(IsDBNull(Cmd.Parameters("@OUTFVTO").Value), "0", Cmd.Parameters("@OUTFVTO").Value)
                If (ret = "1") Then
                    retorno = True
                Else
                    retorno = False
                End If

                Return retorno
            Else
                Return False
            End If
            'Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, "Ocurrió un error")
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub txtOC_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOC.TextChanged

    End Sub

    Private Sub txtProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProducto.TextChanged

    End Sub

    Private Sub txtCantidad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.TextChanged

    End Sub
End Class