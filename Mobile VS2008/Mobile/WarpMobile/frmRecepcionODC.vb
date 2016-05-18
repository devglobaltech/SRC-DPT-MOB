Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Sockets

Public Class frmRecepcionODC
    Private Const FrmName As String = "Recepcion Orden de Compra"
    Private Const ErrCon As String = "No se pudo conectar con la base de datos."
    Private blnFVto As Boolean = False
    Private blnLoteP As Boolean = False
    Private Producto_id As String = ""
    Private Unidad_ID As String = ""
    Private lngPallet As Long
    Private Remanente As Double = 0
    Private Limpiar As Boolean = True
    Private QtyErr As Boolean = False
    Private VidaUtil As Long = 0
    Private DocumentoId As Long
    Private PROD_FRACCIONABLE As Boolean = False

    Private Function ObtenerServidor(ByRef Ip As String, ByRef Port As String) As Boolean
        Dim xcmd As SqlCommand
        Dim xSQL As String = ""
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xcmd = SQLc.CreateCommand
                xcmd.CommandText = "GET_DATA_CONNECTION_RODC"
                xcmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@IP", SqlDbType.VarChar, 13)
                Pa.Direction = ParameterDirection.Output
                xcmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Port", SqlDbType.VarChar, 5)
                Pa.Direction = ParameterDirection.Output
                xcmd.Parameters.Add(Pa)

                xcmd.ExecuteNonQuery()
                Ip = xcmd.Parameters("@IP").Value
                Port = xcmd.Parameters("@Port").Value
            Else : MsgBox(ErrCon, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SqlEx As SqlException
            MsgBox(SqlEx.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xcmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub Connect(ByVal message As String)
        Dim output As String = ""
        Dim ServerIp As String = ""
        Dim Client As TcpClient
        Dim port As Int32
        Dim data(255) As [Byte]
        Try
            If ObtenerServidor(ServerIp, port) Then
                Client = New TcpClient(ServerIp, port)
                Dim stream As NetworkStream = Client.GetStream()
                data = System.Text.Encoding.ASCII.GetBytes(message)
                stream.Write(data, 0, data.Length)
                stream.Close()
                Client.Close()
                Application.DoEvents()
            End If
        Catch e As ArgumentNullException
            output = "ArgumentNullException: " + e.ToString()
            MessageBox.Show(output)
        Catch e As SocketException
            MsgBox("Error Code: " & e.ErrorCode & vbNewLine & "Mensaje: " & e.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            Client = Nothing
        End Try
    End Sub


    Private Sub LimpiarVar()
        blnFVto = False
        blnLoteP = False
        Producto_id = ""
        Unidad_ID = ""
        QtyErr = False
        Remanente = 0
        DocumentoId = 0
        Me.lblInformacion.Text = ""
        lngPallet = 0
        If Me.cmbClientes.Items.Count > 1 Then
            Me.cmbClientes.Enabled = True
        Else
            Me.cmbClientes.Enabled = False
        End If
        Me.cmbTipoImpresion.Enabled = False
        Me.lblPallet.Text = "Pallet:"
        Me.txtProducto.Text = ""
        Me.txtProducto.Enabled = False
        Me.txtCantidad.Text = ""
        Me.txtCantidad.Enabled = False
        Me.TxtCantContenedoras.Visible = False
        Me.LblCantContenedoras.Visible = False
        Me.TxtCantContenedoras.Text = ""
        Me.TxtCantContenedoras.Enabled = False
        Me.txtLoteProveedor.Text = ""
        Me.txtLoteProveedor.Enabled = False
        Me.PROD_FRACCIONABLE = False
        If Limpiar Then
            Me.txtFechaVto.Text = ""
            Me.txtFechaVto.Enabled = False
        End If
        Limpiar = True
        If Me.cmbClientes.Enabled Then
            Me.cmbClientes.Focus()
        Else
            If Me.txtODC.Enabled Then
                Me.txtODC.Enabled = True
                Me.txtODC.Focus()
            Else
                Me.txtProducto.Enabled = True
                Me.txtProducto.Focus()
            End If
        End If
    End Sub

    Private Sub frmRecepcionODC_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyValue
            Case 112
                PreCarga()
            Case 113
                CancelarRecepcion()
            Case 114
                SalirForm()
        End Select
    End Sub

    Private Sub frmRecepcionODC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim vError As String = ""
        If Not GetClientes(vError) Then
            MsgBox(vError, MsgBoxStyle.Critical, FrmName)
            Me.Close()
        End If
        LlenarComboImpresion()
        LimpiarVar()
    End Sub

    Private Sub LlenarComboImpresion()
        Dim drNewRow As DataRow
        Dim dt As New DataTable
        Try
            With cmbTipoImpresion
                cmbTipoImpresion.Items.Add("Pallet")
                cmbTipoImpresion.Items.Add("Bulto")
            End With
            Me.cmbTipoImpresion.DropDownStyle = ComboBoxStyle.DropDownList
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            drNewRow = Nothing
        End Try
    End Sub

    Private Function VerificaODC() As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "DBO.EXIST_ODC"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Me.cmbClientes.Text.ToString
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@odc", SqlDbType.VarChar, 100)
                Pa.Value = Trim(UCase(Me.txtODC.Text.ToString))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@status", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                If xCmd.Parameters("@status").Value <> "1" Then
                    Me.txtODC.Text = ""
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
                    Me.cmbClientes.DropDownStyle = ComboBoxStyle.DropDownList
                    With cmbClientes
                        .DataSource = dt
                        .DisplayMember = "Cliente_id"
                        .ValueMember = "Cliente_id"
                        .SelectedIndex = 0
                    End With
                    If Me.cmbClientes.Items.Count = 1 Then
                        Me.cmbClientes.Enabled = False
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

    Private Sub txtODC_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtODC.KeyUp
        If e.KeyValue = 13 Then
            If Trim(Me.txtODC.Text) = "" Then
                Exit Sub
            End If
            If VerificaODC() Then
                Me.txtODC.Enabled = False
                Me.txtProducto.Enabled = True
                txtProducto.Focus()
            Else
                Me.txtODC.Text = ""
                Me.txtODC.Focus()
            End If
        End If
    End Sub

    Private Sub txtProducto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProducto.KeyUp
        Dim Inact As Boolean = False
        If e.KeyValue = 13 Then
            'aca no se como tengo que validar el producto o el dun14 o el ean13.
            If Trim(Me.txtProducto.Text) = "" Then
                Exit Sub
            Else
                o2D.Decode(Me.txtProducto.Text)
                Me.txtProducto.Text = o2D.PRODUCTO_ID
            End If

            ProductoInhabilidato(Me.cmbClientes.SelectedValue, Me.txtProducto.Text, Inact)

            If Inact Then
                MsgBox("El producto se encuentra inhabilitado, no es posible realizar una recepcion del mismo.", MsgBoxStyle.Information, FrmName)
                Me.txtProducto.Text = ""
                Me.txtProducto.Focus()
                Exit Sub
            End If

            If Me.GetNumberofPallet(Me.lngPallet) Then
                Me.lblPallet.Text = "Pallet: " & Me.lngPallet
            End If
            If ValidacionCodigos(Me.cmbClientes.Text, Me.txtODC.Text, Me.txtProducto.Text) Then
                Me.txtProducto.Enabled = False
                Me.txtCantidad.Enabled = True

                PROD_FRACCIONABLE = ProductoFraccionable(Me.cmbClientes.SelectedValue, Me.txtProducto.Text)

                If PROD_FRACCIONABLE Then
                    Me.txtCantidad.MaxLength = 15
                Else : Me.txtCantidad.MaxLength = 20
                End If

                Me.txtCantidad.Focus()
            Else
                Me.txtProducto.Text = ""
                Me.txtProducto.Focus()
            End If
        End If
    End Sub

    Private Function MuestraAdicionales(ByVal Cliente As String, ByVal Producto As String, ByRef FVto As Boolean, _
                                        ByRef LoteP As Boolean) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "DBO.SOLICITA_MAND"
                xCmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 30)
                Pa.Value = Producto
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@LOTE_PROV", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@FVTO", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()
                LoteP = IIf(xCmd.Parameters("@LOTE_PROV").Value = "1", True, False)
                FVto = IIf(xCmd.Parameters("@FVTO").Value = "1", True, False)
            Else : MsgBox(ErrCon, MsgBoxStyle.Information, FrmName)
                Return False
            End If
            Return True
        Catch SQLEX As SqlException
            MsgBox(SQLEX.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function GetFlgContenedora(ByVal Cliente_id As String, ByVal Producto_id As String, ByRef Flg_Contenedora As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        GetFlgContenedora = False
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "GET_FLG_CONTENEDORA"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Cliente_id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 30)
                Pa.Value = Producto_id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Flg_Contenedora", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                xCmd.ExecuteNonQuery()
                Flg_Contenedora = (xCmd.Parameters("@Flg_Contenedora").Value.ToString)
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

    Private Function ValidacionCodigos(ByVal Cliente As String, ByVal ODC As String, ByVal Codigo As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "VALIDA_CODIGO_RODC"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@CODIGO", SqlDbType.VarChar, 50)
                Pa.Value = Codigo
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Odc", SqlDbType.VarChar, 50)
                Pa.Value = ODC
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                '----------------------------------------------------
                'los de salida.
                '----------------------------------------------------
                Pa = New SqlParameter("@status", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Prod_id", SqlDbType.VarChar, 30)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Unidad_Id", SqlDbType.VarChar, 5)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Remanente", SqlDbType.Float)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()

                If xCmd.Parameters("@Status").Value.ToString = "1" Then
                    Me.Producto_id = xCmd.Parameters("@Prod_Id").Value.ToString
                    Me.txtProducto.Text = Me.Producto_id
                    Me.Unidad_ID = xCmd.Parameters("@Unidad_ID").Value.ToString
                    Me.lblInformacion.Text = "Unidad: " & Unidad_ID
                    Me.Remanente = xCmd.Parameters("@Remanente").Value
                    Me.lblInformacion.Text = Me.lblInformacion.Text & vbNewLine & "Pendiente de Recepcion: " & Remanente
                End If
            Else : MsgBox(ErrCon, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub txtCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress
        Dim Search As String
        Dim Pos As Integer
        Search = "."
        If Not Me.PROD_FRACCIONABLE Then
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
        If e.KeyValue = 13 Then
            Dim TEti As String = ""
            Dim vError As String = ""
            Dim FlgContenedora As String = ""
            If Me.txtCantidad.Text = "" Then
                Exit Sub
            End If
            If Me.TipoImpresion(Me.cmbClientes.Text, Me.txtProducto.Text, Me.txtCantidad.Text, TEti) Then
                If TEti = "1" Then
                    Me.cmbTipoImpresion.Text = "Bulto"
                ElseIf TEti = "0" Then
                    Me.cmbTipoImpresion.Text = "Pallet"
                End If
            End If
            'If Me.GetFlgContenedora(Me.cmbClientes.Text, Me.txtProducto.Text, FlgContenedora) Then
            'If FlgContenedora = "0" Then
            If MuestraAdicionales(Me.cmbClientes.Text, Me.Producto_id, Me.blnFVto, Me.blnLoteP) Then
                If Me.blnLoteP Then
                    Me.txtCantidad.Enabled = False
                    Me.txtLoteProveedor.Enabled = True
                    Me.txtLoteProveedor.Focus()
                    Exit Sub
                End If
                If Me.blnFVto Then
                    Me.txtCantidad.Enabled = False
                    Me.txtFechaVto.Enabled = True
                    Me.txtFechaVto.Focus()
                    Exit Sub
                End If
                If Not (Me.blnFVto) And Not (Me.blnLoteP) Then
                    'aca tengo que cerrar la entrada del pallet.
                    Me.txtCantidad.Enabled = False
                    Me.cmbTipoImpresion.Enabled = True
                    Me.cmbTipoImpresion.Focus()
                End If

            End If
            'Else
            'Me.txtCantidad.Enabled = False
            'Me.TxtCantContenedoras.Visible = True
            'Me.LblCantContenedoras.Visible = True
            'Me.TxtCantContenedoras.Enabled = True
            'Me.TxtCantContenedoras.Focus()
            'End If
            'Else
            'Me.txtCantidad.Focus()
            'End If
        End If
    End Sub

    Private Sub PreCarga()
        Try
            'Debo controlar los mandatorios.
            Dim vError As String = ""
            Dim Fecha As String = ""
            Dim Lp As String = ""
            Dim TipoImpresion As String = ""

            If Trim(Me.txtODC.Text) = "" Then
                Me.txtODC.Focus()
                Exit Sub
            End If
            If Trim(Me.txtProducto.Text) = "" Then
                Me.txtProducto.Focus()
                Exit Sub
            End If
            If Trim(Me.txtCantidad.Text) = "" Then
                Me.txtCantidad.Focus()
                Exit Sub
            End If
            If Trim(Me.txtLoteProveedor.Text) = "" And blnLoteP Then
                Me.txtLoteProveedor.Focus()
                Exit Sub
            End If
            If Trim(Me.txtFechaVto.Text) = "" And blnFVto Then
                Me.txtFechaVto.Focus()
                Exit Sub
            End If
            If Trim(Me.txtFechaVto.Text) <> "" Then
                Fecha = Me.txtFechaVto.Text
            End If
            Lp = Me.txtLoteProveedor.Text

            If Me.cmbTipoImpresion.Text = "Pallet" Then
                TipoImpresion = "0"
            Else : TipoImpresion = "1"
            End If

            If (Me.TxtCantContenedoras.Text = "") And (Me.TxtCantContenedoras.Visible = True) Then
                Me.TxtCantContenedoras.Focus()
                Exit Sub
            End If
            If Me.IngresarODC(Me.cmbClientes.Text, Me.Producto_id, Me.txtODC.Text, CDbl(Me.txtCantidad.Text), Lp, Fecha, Me.lngPallet, vError, TipoImpresion, Val(Me.TxtCantContenedoras.Text)) Then
                If CrossDock() Then
                    Limpiar = False
                    LimpiarVar()
                End If
            Else
                Console.WriteLine("Salida con variable de error. verifico el primer caracter de la linea")
                If Mid(vError, 1, 1) = "1" Then
                    Me.txtCantidad.Enabled = True
                    Me.txtCantidad.Text = ""
                    Me.txtCantidad.Focus()
                    Me.QtyErr = True
                End If
                MsgBox(vError, MsgBoxStyle.Critical, FrmName)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Function CrossDock() As Boolean
        Try
            Dim Nave As Long
            Dim NaveCod As String = ""
            Dim OpCross As String = ""
            If Not VerificaCrossDock(DocumentoId, 1, Nave, OpCross, NaveCod) Then
                Exit Try 'camino no feliz.
            Else
                If OpCross = "1" Then
                    'no se pregunta nada simplemente lo mando como esta.
                    If Not Procesar(Nave) Then Return False
                End If
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Function

    Private Function Procesar(ByVal Ubicacion As Long) As Boolean
        Dim CI As New ClsIngreso
        Dim CA As New clsAceptar
        Dim Trans As SqlTransaction
        Dim Ds As New DataSet
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim Linea As Long = 0
        Dim Envase As Integer = 0
        Dim NavId As Integer = 0
        Dim PosId As Integer = 0
        Dim dSTMP As New DataSet
        Dim vError As String = ""
        Dim Nro_Linea As Long
        Trans = SQLc.BeginTransaction
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.Connection = SQLc
                Cmd.Transaction = Trans
                CI.objConnection = SQLc
                CI.Cmd = Cmd
                GetValueForPalletMP(lngPallet, DocumentoId, Cmd, dSTMP)
                Cmd.Parameters.Clear()
                For I = 0 To dSTMP.Tables("TABLE").Rows.Count - 1
                    Nro_Linea = dSTMP.Tables("TABLE").Rows(I)(0)
                    Envase = dSTMP.Tables("TABLE").Rows(I)(1)
                    If Envase = 0 Then
                        If Not CI.ExecuteAll(DocumentoId, Nro_Linea, Nothing, Ubicacion) Then
                            Throw New Exception("Error en modulo ExecuteAll.")
                        End If
                    End If
                    CA.DocumentoID = DocumentoId
                    CA.Cmd = Cmd
                    CA.NroLinea = Nro_Linea
                    CA.objConnection = SQLc
                    CA.OperacionID = "ING"
                    CA.UsuarioID = vUsr.CodUsuario
                    If Not CA.Aceptar() Then
                        Throw New Exception("Error en modulo Aceptar.")
                    End If
                Next
                If Not IngresaAuditoria(DocumentoId, Nro_Linea, Cmd, "", vError) Then
                    Throw New Exception(vError)
                End If
                If Not Sys_Dev(DocumentoId, 1, Cmd) Then
                    Throw New Exception("No se pudo completar la operacion Sys_Dev")
                End If
            Else : MsgBox(ErrCon, MsgBoxStyle.OkOnly, FrmName)
                Procesar = False
            End If
            Trans.Commit()
            Return True
        Catch ex As Exception
            Trans.Rollback()
            Procesar = False
            MsgBox("Procesar: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            CI = Nothing
            CA = Nothing
        End Try
    End Function

    Private Function Sys_Dev(ByVal DocumentoId As Long, ByVal Estado As Integer, ByRef oCmd As SqlCommand) As Boolean

        Try
            If VerifyConnection(SQLc) Then
                oCmd.Parameters.Clear()
                oCmd.CommandText = "Sys_dev"
                oCmd.CommandType = CommandType.StoredProcedure
                oCmd.Parameters.Add("@Documento_ID", SqlDbType.Int).Value = DocumentoId
                oCmd.Parameters.Add("@Estado", SqlDbType.Int).Value = Estado
                oCmd.ExecuteNonQuery()
            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True

        Catch SQLEx As SqlException
            'MsgBox("Sys_Dev SQL - " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            'MsgBox("Sys_Dev - " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        End Try

    End Function

    Private Function IngresaAuditoria(ByVal Documento_Id As Long, ByVal Nro_Linea As Long, _
                                  ByRef xCmd As SqlCommand, ByVal Ubicacion As String, _
                                  ByRef vError As String) As Boolean
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd.Parameters.Clear()
                xCmd.CommandText = "AUDITORIA_HIST_INSERT_UBIC"
                xCmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@DOC", SqlDbType.BigInt)
                Pa.Value = Documento_Id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_LINEA", SqlDbType.BigInt)
                Pa.Value = Nro_Linea
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@UBIC", SqlDbType.VarChar, 45)
                Pa.Value = Trim(UCase(Ubicacion))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@SWITCH", SqlDbType.Char, 1)
                Pa.Value = "1"
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                Return True
            Else : MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
        Catch SqlEx As SqlException
            vError = SqlEx.Message
            Return False
        Catch ex As Exception
            vError = ex.Message
            Return False
        Finally
            Pa = Nothing
        End Try
    End Function

    Private Function VerificaCrossDock(ByVal vDocumentoId As Long, ByVal vNroLinea As Long, _
                                   ByRef NavCrossDock As Long, ByRef OpCrossDock As String, ByRef NaveCod As String) As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Cmd = SQLc.CreateCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd.CommandText = "VerificaCrossDock"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Documento_id", SqlDbType.BigInt)
                Pa.Value = vDocumentoId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Nro_Linea", SqlDbType.BigInt)
                Pa.Value = vNroLinea
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Nav_CrossDock", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@UsaCrossDock", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NaveCod", SqlDbType.VarChar, 45)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)



                Cmd.ExecuteNonQuery()

                NavCrossDock = IIf(IsDBNull(Cmd.Parameters("@Nav_CrossDock").Value), 0, Cmd.Parameters("@Nav_CrossDock").Value)
                OpCrossDock = IIf(IsDBNull(Cmd.Parameters("@UsaCrossDock").Value), 0, Cmd.Parameters("@UsaCrossDock").Value)
                NaveCod = IIf(IsDBNull(Cmd.Parameters("@NaveCod").Value), "", Cmd.Parameters("@NaveCod").Value)
            Else : MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("VerificaCrossDock SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("VerificaCrossDock: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function IngresarODC(ByVal ClienteId As String, ByVal ProductoId As String, ByVal ODC As String, ByVal Qty As Double, _
                                 ByVal LoteProv As String, ByVal FechaVto As String, ByVal Pallet As String, ByRef vError As String, _
                                 ByVal TipoEti As String, ByVal CantContenedoras As Integer) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "DBO.INGRESA_ODC"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Cliente_ID", SqlDbType.VarChar, 15)
                Pa.Value = ClienteId
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Producto_ID", SqlDbType.VarChar, 30)
                Pa.Value = ProductoId
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@ODC", SqlDbType.VarChar, 50)
                Pa.Value = ODC
                xCmd.Parameters.Add(Pa)
                Pa = New SqlParameter("@Qty", SqlDbType.Float)
                Pa.Value = Qty
                xCmd.Parameters.Add(Pa)
                Pa = New SqlParameter("@LOTEPROV", SqlDbType.VarChar, 50)
                If LoteProv <> "" Then
                    Pa.Value = LoteProv
                Else
                    Pa.Value = DBNull.Value
                End If
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@FECHA_VTO", SqlDbType.VarChar, 20)
                If FechaVto <> "" Then
                    Pa.Value = FechaVto
                Else
                    Pa.Value = DBNull.Value
                End If
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Pallet", SqlDbType.VarChar, 50)
                Pa.Value = Pallet
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@tipo_eti", SqlDbType.Char, 1)
                Pa.Value = TipoEti
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Documento_ID", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                'LRojas 02/03/2012 TrackerID 3806: Usuario demonio de Impresion
                Pa = New SqlParameter("@Usuario_Imp", SqlDbType.VarChar, 20)
                Pa.Value = vUsr.CodUsuario
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                DocumentoId = xCmd.Parameters("@Documento_id").Value
            Else : MsgBox(ErrCon, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SqlEx As SqlException
            vError = SqlEx.Message
        Catch ex As Exception
            vError = ex.Message
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Public Function GetNumberofPallet(ByRef Pallet_id As Long) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "Get_Value_For_Sequence"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                xCmd.Parameters.Clear()
                Pa = New SqlParameter("@SECUENCIA", Data.SqlDbType.VarChar, 50)
                Pa.Value = "NROPALLET_SEQ"
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VALUE", Data.SqlDbType.Int)
                Pa.Direction = Data.ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                Pallet_id = IIf(IsDBNull(xCmd.Parameters("@VALUE").Value), 0, xCmd.Parameters("@VALUE").Value)
                Return True
            Else
                MsgBox(ErrCon, MsgBoxStyle.OkOnly, FrmName)
            End If
        Catch SQLEx As SqlException
            MsgBox("GetNumberofPallet SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("GetNumberofPallet: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub txtLoteProveedor_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLoteProveedor.KeyUp
        If e.KeyValue = 13 Then
            If Trim(Me.txtLoteProveedor.Text) <> "" Then
                If Me.blnFVto Then
                    Me.txtLoteProveedor.Enabled = False
                    Me.txtFechaVto.Enabled = True
                    Me.txtFechaVto.Focus()
                    Exit Sub
                Else
                    Me.txtLoteProveedor.Enabled = False
                    Me.cmbTipoImpresion.Enabled = True
                    Me.cmbTipoImpresion.Focus()
                End If
            Else
                Me.txtLoteProveedor.Focus()
            End If
        End If
    End Sub

    Private Sub cmbClientes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbClientes.KeyUp
        If e.KeyValue = 13 Then
            Me.cmbClientes.Enabled = False
            Me.txtODC.Enabled = True
            Me.txtODC.Focus()
        End If
    End Sub

    Public Sub ValidarCaracterNumerico(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If (Asc(e.KeyChar) >= 32 And Asc(e.KeyChar) <= 47) Or Asc(e.KeyChar) >= 58 Then
                e.Handled = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtFechaVto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFechaVto.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub txtFechaVto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFechaVto.KeyUp
        If (e.KeyCode = 13) Or (e.KeyCode = 8) Then
            If e.KeyCode = 8 Then
                Me.txtFechaVto.Text = ""
            End If
            If Me.txtFechaVto.Text <> "" Then
                If Me.txtFechaVto.Text.Length >= 10 Then
                    If Not IsDate(Me.txtFechaVto.Text) Then
                        MsgBox("No es una fecha valida.", MsgBoxStyle.OkOnly, FrmName)
                        Me.txtFechaVto.Text = ""
                        Me.txtFechaVto.Focus()
                    Else
                        If ValidaFechaVencimiento(Me.txtFechaVto.Text, Me.cmbClientes.Text, Me.txtProducto.Text) Then
                            Me.txtFechaVto.Enabled = False
                            Me.cmbTipoImpresion.Enabled = True
                            Me.cmbTipoImpresion.Focus()
                        Else
                            Me.txtFechaVto.Text = ""
                            Me.txtFechaVto.Focus()
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Function ValidaFechaVencimiento(ByVal Fechavto As String, ByVal Cliente As String, ByVal Producto As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "VALIDA_VENCIMIENTO_RODC"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@fvto", SqlDbType.VarChar, 10)
                Pa.Value = Fechavto
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Cliente", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Producto_ID", SqlDbType.VarChar, 30)
                Pa.Value = Producto
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

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

    Private Sub txtFechaVto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFechaVto.TextChanged
        If Me.txtFechaVto.Text.Length = 2 Then
            Me.txtFechaVto.Text = Me.txtFechaVto.Text & "/"
            Me.txtFechaVto.SelectionStart = Me.txtFechaVto.Text.Length
        ElseIf Me.txtFechaVto.Text.Length = 5 Then
            Me.txtFechaVto.Text = Me.txtFechaVto.Text & "/"
            Me.txtFechaVto.SelectionStart = Me.txtFechaVto.Text.Length
        End If
    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        CancelarRecepcion()
    End Sub

    Private Sub CancelarRecepcion()
        Try
            If MsgBox("Desea Cancelar la operacion en curso?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                Me.txtODC.Text = ""
                Me.txtODC.Enabled = True
                LimpiarVar()
                Me.txtODC.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        SalirForm()
    End Sub

    Private Sub SalirForm()
        Try
            If MsgBox("Desea salir?" & vbNewLine & "La operacion en curso sera cancelada.", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Function TipoImpresion(ByVal Cliente As String, ByVal Producto As String, ByVal Qty As Double, ByRef tipoEti As String) As Boolean
        Dim xcmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xcmd = SQLc.CreateCommand
                xcmd.CommandText = "DBO.GET_TIPO_IMPRESION_RODC"
                xcmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Cliente_ID", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xcmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Producto_Id", SqlDbType.VarChar, 30)
                Pa.Value = Producto
                xcmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Qty", SqlDbType.Float)
                Pa.Value = Qty
                xcmd.Parameters.Add(Pa)
                Pa = Nothing
                'El de salida.
                Pa = New SqlParameter("@Tipo_Eti", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xcmd.Parameters.Add(Pa)
                xcmd.ExecuteNonQuery()
                tipoEti = xcmd.Parameters("@Tipo_Eti").Value
            Else : MsgBox(ErrCon, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SqlEx As SqlException
            MsgBox(SqlEx.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xcmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub cmdAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
        PreCarga()
    End Sub

    Private Sub cmbTipoImpresion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbTipoImpresion.KeyUp
        If e.KeyValue = 13 Then
            PreCarga()
        End If
    End Sub

    Private Sub GetValueForPalletMP(ByVal Pallet As String, ByVal DocumentoID As Long, ByVal xCmd As SqlCommand, ByVal dS As DataSet)
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim vInt As Integer = 0

        Try
            If VerifyConnection(SQLc) Then
                Da = New SqlDataAdapter(xCmd)

                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = "GETCANTPROD"

                Pa = New SqlParameter("@documento_id", SqlDbType.Int)
                Pa.Value = DocumentoID
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@pallet", SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Out", SqlDbType.Int)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                Da.Fill(dS, "table")
            End If
        Catch ex As Exception
            MsgBox("GetValueForPalletMP: " & ex.Message)
        Finally
            Pa = Nothing
        End Try
    End Sub

    Private Sub cmbTipoImpresion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipoImpresion.SelectedIndexChanged

    End Sub

    Private Sub cmbClientes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClientes.SelectedIndexChanged

    End Sub

    Private Sub txtODC_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtODC.TextChanged

    End Sub

    Private Sub txtCantidad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.TextChanged

    End Sub

    Private Sub txtProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProducto.TextChanged

    End Sub

    Private Sub txtLoteProveedor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLoteProveedor.TextChanged

    End Sub

    Private Sub lblODC_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblODC.ParentChanged

    End Sub

    Private Function ValidarPatente(ByVal Pat As String) As Boolean
        If Pat.Length >= 2 And Pat.Length <= 10 Then
            Dim expReg As String = "[a-zA-Z]{1,3}[0-9]{1,5}" 'valida string de tipo xxx111 o XX11111
            Dim val As New System.Text.RegularExpressions.Regex(expReg)
            Return val.IsMatch(Pat)
        Else
            Return False
        End If

        'Dim expReg As String = "[a-zA-Z]{1,3}[0-9]{1,5}" 'valida string de tipo xxx111
        'Dim expReg As String = "[a-zA-Z0-9]*" 'valida string que solo tengan carateres y numeros
        'Dim val As New System.Text.RegularExpressions.Regex(expReg)
        'Return val.IsMatch(Pat)
    End Function

    Private Sub TxtCantContenedoras_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCantContenedoras.KeyUp
        If e.KeyValue = 13 Then
            If Me.TxtCantContenedoras.Text = "" Then
                Me.TxtCantContenedoras.Focus()
                Exit Sub
            End If
            Me.TxtCantContenedoras.Enabled = False
            If MuestraAdicionales(Me.cmbClientes.Text, Me.Producto_id, Me.blnFVto, Me.blnLoteP) Then
                If Me.blnLoteP Then
                    Me.txtCantidad.Enabled = False
                    Me.txtLoteProveedor.Enabled = True
                    Me.txtLoteProveedor.Focus()
                    Exit Sub
                End If
                If Me.blnFVto Then
                    Me.txtCantidad.Enabled = False
                    Me.txtFechaVto.Enabled = True
                    Me.txtFechaVto.Focus()
                    Exit Sub
                End If
                If Not (Me.blnFVto) And Not (Me.blnLoteP) Then
                    'aca tengo que cerrar la entrada del pallet.
                    Me.txtCantidad.Enabled = False
                    Me.cmbTipoImpresion.Enabled = True
                    Me.cmbTipoImpresion.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub TxtCantContenedoras_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtCantContenedoras.KeyPress        
        ValidarCaracterNumerico(e)
    End Sub
    Private Function GetFlgFraccionable(ByVal Cliente_id As String, ByVal Producto_id As String, ByRef FlgFraccionable As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        GetFlgFraccionable = False
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "GET_FLG_CONTENEDORA"
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

    Private Sub Label3_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.ParentChanged

    End Sub
End Class
