Imports System.Data
Imports System.Data.SqlClient


Public Class clsRecepcionGuardado

#Region "Variables"

    Private Conn As SqlConnection
    Private Const clsName As String = "Recepcion y Guardado."
    Private Const SQLConErr As String = "No se pudo conectar a la base de datos."
    Private vCliente_ID As String = ""
    Private vNumPallet As String
    Private DsConfProducto As New DataSet
    Private vRemanente As Double = 0
    Private DocumentoId As Long = 0
    Private vToleranciaMin As Double = 0
    Private vToleranciaMax As Double = 0
    Private vPosicionID As Long = 0
    Private vPosicionCOD As String = ""
    Private vNaveID As Long = 0
    Private OrdenLocator As String = ""


    Public Enum tblProducto
        CLIENTE_ID = 0
        PRODUCTO_ID = 1
        CODIGO_PRODUCTO = 2
        SUBCODIGO_1 = 3
        SUBCODIGO_2 = 4
        DESCRIPCION = 5
        NOMBRE = 6
        MARCA = 7
        FRACCIONABLE = 8
        UNIDAD_FRACCION = 9
        COSTO = 10
        UNIDAD_ID = 11
        TIPO_PRODUCTO_ID = 12
        PAIS_ID = 13
        FAMILIA_ID = 14
        CRITERIO_ID = 15
        OBSERVACIONES = 16
        POSICIONES_PURAS = 17
        KIT = 18
        SERIE_EGR = 19
        MONEDA_ID = 20
        NO_AGRUPA_ITEMS = 21
        LARGO = 22
        ALTO = 23
        ANCHO = 24
        UNIDAD_VOLUMEN = 25
        VOLUMEN_UNITARIO = 26
        PESO = 27
        UNIDAD_PESO = 28
        PESO_UNITARIO = 29
        LOTE_AUTOMATICO = 30
        PALLET_AUTOMATICO = 31
        INGRESO = 32
        EGRESO = 33
        INVENTARIO = 34
        TRANSFERENCIA = 35
        TOLERANCIA_MIN = 36
        TOLERANCIA_MAX = 37
        BACK_ORDER = 38
        CLASIFICACION_COT = 39
        CODIGO_BARRA = 40
        ING_CAT_LOG_ID = 41
        EGR_CAT_LOG_ID = 42
        SUB_FAMILIA_ID = 43
        TIPO_CONTENEDORA = 44
        GRUPO_PRODUCTO = 45
        ENVASE = 46
        VAL_COD_ING = 47
        VAL_COD_EGR = 48
        ROTACION_ID = 49
        FLG_BULTO = 50
        QTY_BULTO = 51
        FLG_VOLUMEN_ETI = 52
        QTY_VOLUMEN_ETI = 53
        FLG_CONTENEDORA = 54
        SERIE_ING = 55
        TIE_IN = 56
        INGLOTEPROVEEDOR = 57
        INGPARTIDA = 58
        NRO_PARTIDA_AUTOMATICO = 59
        TRANSF_PICKING = 60
        ET_TAREA_CONF = 61
    End Enum

#End Region
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
#Region "Propertys"

    Public ReadOnly Property ToleranciaMin() As Double
        Get
            Return Me.vToleranciaMin
        End Get
    End Property

    Public ReadOnly Property ToleranciaMax() As Double
        Get
            Return Me.vToleranciaMax
        End Get
    End Property

    Public ReadOnly Property PalletDevolucion() As String
        Get
            Return vNumPallet
        End Get
    End Property

    Public Property Database() As SqlConnection
        Get
            Return Conn
        End Get
        Set(ByVal value As SqlConnection)
            Conn = value
        End Set
    End Property

    Public Property Cliente_ID() As String
        Get
            Return Me.vCliente_ID
        End Get
        Set(ByVal value As String)
            Me.vCliente_ID = value
        End Set
    End Property

    Public ReadOnly Property ValidacionContraOC() As Boolean

        Get
            Return ValidaContraOC(Me.Cliente_ID)
        End Get

    End Property

    Public ReadOnly Property Descripcion() As String
        Get
            Return Me.DsConfProducto.Tables(0).Rows(0)(tblProducto.DESCRIPCION).ToString()
        End Get
    End Property

    Public ReadOnly Property CantidadRemanente() As Double
        Get
            Return Me.vRemanente
        End Get
    End Property

    Public ReadOnly Property ValidaProducto() As Boolean
        Get
            If Me.DsConfProducto.Tables.Count > 0 Then
                If Me.DsConfProducto.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property SolicitaLote() As String
        Get
            If (Me.DsConfProducto.Tables(0).Rows(0)(tblProducto.LOTE_AUTOMATICO).ToString() = "1") Or (Me.DsConfProducto.Tables(0).Rows(0)(tblProducto.INGLOTEPROVEEDOR).ToString() = "1") Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property SolicitaPartida() As String
        Get
            If (Me.DsConfProducto.Tables(0).Rows(0)(tblProducto.INGPARTIDA).ToString() = "1") Or (Me.DsConfProducto.Tables(0).Rows(0)(tblProducto.NRO_PARTIDA_AUTOMATICO).ToString() = "1") Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property SolicitaVencimiento() As Boolean
        Get
            If (Me.DsConfProducto.Tables(0).Rows(0)(tblProducto.CODIGO_PRODUCTO).ToString() <> "") Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property Ubicacion() As String
        Get
            Return IIf(Me.vPosicionCOD = "0", "", Me.vPosicionCOD)
        End Get
    End Property

#End Region

#Region "Metodos"

    Public Function PartidaAutomatica(ByVal TxtPartida As TextBox) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Try
            If Me.DsConfProducto.Tables(0).Rows(0)(tblProducto.NRO_PARTIDA_AUTOMATICO).ToString() = "1" Then
                If VerifyConnection(Conn) Then
                    xCmd = Conn.CreateCommand

                    Da = New SqlDataAdapter(xCmd)
                    xCmd.CommandText = "dbo.GET_VALUE_FOR_SEQUENCE"
                    xCmd.CommandType = Data.CommandType.StoredProcedure

                    Pa = New SqlParameter("@SECUENCIA", SqlDbType.VarChar, 50)
                    Pa.Value = "NRO_PARTIDA"
                    xCmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@VALUE", SqlDbType.BigInt)
                    Pa.Direction = ParameterDirection.Output
                    xCmd.Parameters.Add(Pa)

                    xCmd.ExecuteNonQuery()

                    TxtPartida.Text = xCmd.Parameters("@VALUE").Value.ToString
                    Return True
                End If
            Else
                Return False
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, clsName)
        End Try
    End Function

    Public Function LoteAutomatico(ByVal TxtLote As TextBox) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Try
            If Me.DsConfProducto.Tables(0).Rows(0)(tblProducto.LOTE_AUTOMATICO).ToString() = "1" Then
                If VerifyConnection(Conn) Then
                    xCmd = Conn.CreateCommand

                    Da = New SqlDataAdapter(xCmd)
                    xCmd.CommandText = "dbo.GET_VALUE_FOR_SEQUENCE"
                    xCmd.CommandType = Data.CommandType.StoredProcedure

                    Pa = New SqlParameter("@SECUENCIA", SqlDbType.VarChar, 50)
                    Pa.Value = "NROLOTE_SEQ"
                    xCmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@VALUE", SqlDbType.BigInt)
                    Pa.Direction = ParameterDirection.Output
                    xCmd.Parameters.Add(Pa)

                    xCmd.ExecuteNonQuery()

                    TxtLote.Text = xCmd.Parameters("@VALUE").Value.ToString
                    Return True
                End If
            Else
                Return False
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, clsName)
        End Try
    End Function

    Public Function ManejoPallet(ByRef Txt As TextBox) As Boolean
        Dim Msg As String = "¿Ingresa el Nro. de Pallet?"
        Try
            If MsgBox(Msg, MsgBoxStyle.YesNo, clsName) = MsgBoxResult.No Then
                Txt.Text = GetPallet()
            Else
                Txt.Text = ""
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, clsName)
        End Try
    End Function

    Private Function GetPallet() As String
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Try
            If VerifyConnection(Conn) Then
                xCmd = Conn.CreateCommand

                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "dbo.GET_VALUE_FOR_SEQUENCE"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@SECUENCIA", SqlDbType.VarChar, 50)
                Pa.Value = "NROPALLET_SEQ"
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VALUE", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()

                Return xCmd.Parameters("@VALUE").Value.ToString
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, clsName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function GetClientes(ByRef Cmb As ComboBox, ByVal UsuarioID As String) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, SQL As String = ""
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                SQL = SQL & "SELECT	R.CLIENTE_ID, C.RAZON_SOCIAL " & vbNewLine
                SQL = SQL & "FROM	DBO.RL_SYS_CLIENTE_USUARIO R INNER JOIN CLIENTE C" & vbNewLine
                SQL = SQL & "ON(R.CLIENTE_ID = C.CLIENTE_ID )" & vbNewLine
                SQL = SQL & "WHERE	R.USUARIO_ID ='" & UsuarioID & "'" & vbNewLine

                Cmd.CommandText = SQL
                Cmd.CommandType = CommandType.Text

                DA.Fill(DS, "CLIENTES")

                Cmb.DataSource = DS.Tables(0)
                Cmb.ValueMember = "CLIENTE_ID"
                Cmb.DisplayMember = "RAZON_SOCIAL"

            Else : MsgBox(SQLConErr, MsgBoxStyle.Information, clsName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, clsName)
        Finally
            DA.Dispose()
            Cmd.Dispose()
            DS.Dispose()
        End Try
    End Function

    Private Function GetPalletIngreso() As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Try
            If VerifyConnection(Conn) Then
                xCmd = Conn.CreateCommand

                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "dbo.GET_VALUE_FOR_SEQUENCE"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@SECUENCIA", SqlDbType.VarChar, 50)
                Pa.Value = "NROPALLET_SEQ"
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VALUE", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()

                vNumPallet = xCmd.Parameters("@VALUE").Value.ToString

            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, clsName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function Inicializar() As Boolean
        Me.vCliente_ID = ""
        Me.vNumPallet = ""
        Me.GetPalletIngreso()
        Me.DsConfProducto = Nothing
        Me.DsConfProducto = New DataSet
        Me.vRemanente = 0
        Me.DocumentoId = 0
        Me.vToleranciaMax = 0
        Me.vToleranciaMin = 0
        Me.vPosicionID = 0
        Me.vPosicionCOD = 0
        Me.vNaveID = 0
    End Function

    Public Function ObtenerConfiguracionProducto(ByVal ProductoID As String) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, SQL As String = ""
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DsConfProducto = Nothing
                DsConfProducto = New DataSet
                SQL = ""
                SQL = SQL & "SELECT	* FROM PRODUCTO WHERE CLIENTE_ID='" & Me.Cliente_ID & "' AND PRODUCTO_ID='" & ProductoID & "'" & vbNewLine

                Cmd.CommandText = SQL
                Cmd.CommandType = CommandType.Text

                DA.Fill(DsConfProducto, "PRODUCTO")

            Else : MsgBox(SQLConErr, MsgBoxStyle.Information, clsName)
                Return False
            End If

            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, clsName)
        Finally
            DA.Dispose()
            Cmd.Dispose()
        End Try

    End Function

    Public Function ExisteProducto() As Boolean
        Try
            If DsConfProducto.Tables.Count > 0 Then
                If DsConfProducto.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, clsName)
        End Try
    End Function

    Private Function ValidaContraOC(ByVal ClienteID As String) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, SQL As String = ""
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                SQL = ""
                SQL = SQL & "SELECT	ISNULL(C.FLG_VALIDA_RECEPCION_OC,'0')AS FLG_VALIDA_RECEPCION_OC" & vbNewLine
                SQL = SQL & "FROM 	CLIENTE_PARAMETROS C" & vbNewLine
                SQL = SQL & "WHERE	CLIENTE_ID='" & ClienteID & "'" & vbNewLine

                Cmd.CommandText = SQL
                Cmd.CommandType = CommandType.Text

                DA.Fill(DS, "CLIENTES")

                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        If Trim(DS.Tables(0).Rows(0)(0).ToString) = "0" Then
                            Return False
                        Else
                            Return True
                        End If
                    End If
                End If

            Else : MsgBox(SQLConErr, MsgBoxStyle.Information, clsName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, clsName)
        Finally
            DA.Dispose()
            Cmd.Dispose()
            DS.Dispose()
        End Try
    End Function

    Private Function VerificaOC(ByVal NroOC As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                xCmd = Conn.CreateCommand
                xCmd.CommandText = "DBO.EXIST_ODC"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Me.Cliente_ID
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@odc", SqlDbType.VarChar, 100)
                Pa.Value = Trim(UCase(NroOC))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@status", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                If xCmd.Parameters("@status").Value <> "1" Then
                    Return False
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Critical, clsName)
                Return False
            End If
            Return True
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Critical, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Public Function ValidarProductoOC(ByVal ODC As String, ByVal Producto As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                xCmd = Conn.CreateCommand
                xCmd.CommandText = "[dbo].[MOB_RG_VALIDAR_OC_PRODUCTO]"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@CODIGO", SqlDbType.VarChar, 100)
                Pa.Value = Producto
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Odc", SqlDbType.VarChar, 50)
                Pa.Value = ODC
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Me.Cliente_ID
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
                    Me.vRemanente = xCmd.Parameters("@Remanente").Value
                    CalcularTolerancias(Me.vRemanente)
                Else
                    Return False
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Critical, clsName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub CalcularTolerancias(ByVal Cantidad As Double)
        Dim cdblMin As Double = 0, cdblMax As Double = 0
        Try
            cdblMin = CDbl(Me.GetConfiguracionEspecificaProducto(tblProducto.TOLERANCIA_MIN))
            cdblMax = CDbl(Me.GetConfiguracionEspecificaProducto(tblProducto.TOLERANCIA_MAX))

            If cdblMin > 0 Then
                Me.vToleranciaMin = Cantidad - ((Cantidad * cdblMin) / 100)
            End If

            If cdblMax > 0 Then
                Me.vToleranciaMax = Cantidad + ((Cantidad * cdblMax) / 100)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        End Try
    End Sub

    Public Function GetConfiguracionEspecificaProducto(ByVal Field As tblProducto) As String
        Dim Retorno As String = ""
        Try
            Retorno = Me.DsConfProducto.Tables(0).Rows(0)(Field).ToString()
            Return Retorno
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, clsName)
        End Try
    End Function

    Public Function ValidarOC(ByVal ODC As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                xCmd = Conn.CreateCommand
                xCmd.CommandText = "[dbo].[MOB_RG_VALIDAR_OC]"
                xCmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Me.Cliente_ID
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Odc", SqlDbType.VarChar, 50)
                Pa.Value = ODC
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                '----------------------------------------------------
                'los de salida.
                '----------------------------------------------------
                Pa = New SqlParameter("@status", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()

                If xCmd.Parameters("@Status").Value.ToString = "1" Then
                    Return True
                Else
                    Return False
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Critical, clsName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Public Function ValidarPallet(ByVal NroPallet As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try

            If VerifyConnection(Conn) Then
                xCmd = Conn.CreateCommand
                xCmd.CommandText = "DBO.MOB_RG_VALIDA_PALLET"
                xCmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@NRO_PALLET", SqlDbType.VarChar, 100)
                Pa.Value = NroPallet
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                '----------------------------------------------------
                'los de salida.
                '----------------------------------------------------
                Pa = New SqlParameter("@status", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()

                If xCmd.Parameters("@Status").Value.ToString = "1" Then
                    Return True
                Else
                    Return False
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Critical, clsName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        End Try
    End Function

    Public Function ValidarFechaVencimiento(ByVal FechaIngresada As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try

            If VerifyConnection(Conn) Then
                xCmd = Conn.CreateCommand
                xCmd.CommandText = "DBO.MOB_RG_VALIDACION_VENCIMIENTO"
                xCmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@CANT_DIAS", SqlDbType.VarChar, 100)
                Pa.Value = Me.GetConfiguracionEspecificaProducto(tblProducto.CODIGO_PRODUCTO)
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@FECHA", SqlDbType.VarChar, 100)
                Pa.Value = FechaIngresada
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                '----------------------------------------------------
                'los de salida.
                '----------------------------------------------------
                Pa = New SqlParameter("@status", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()

                If xCmd.Parameters("@Status").Value.ToString = "1" Then
                    Return True
                Else
                    Return False
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Critical, clsName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        End Try
    End Function

    Public Function GuardarTemporal(ByVal OC As String, ByVal NroPallet As String, ByVal Producto As String, ByVal Cantidad As Double, ByVal NroLote As String, ByVal NroPartida As String, ByVal FVto As String)
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                xCmd = Conn.CreateCommand
                xCmd.Parameters.Clear()
                xCmd.CommandText = "DBO.MOB_RG_REGISTRO_TEMPORAL"
                xCmd.CommandType = CommandType.StoredProcedure

                '@OC				VARCHAR(100),  1
                Pa = New SqlParameter("@OC", SqlDbType.VarChar, 100)
                If Me.ValidacionContraOC Then
                    Pa.Value = OC.Trim.ToUpper
                Else
                    Pa.Value = DBNull.Value
                End If
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                '@CLIENTE_ID		VARCHAR(15),2
                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Me.Cliente_ID.Trim.ToUpper
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                '@PRODUCTO_ID	VARCHAR(30),3
                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = Producto
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                '@CANTIDAD		NUMERIC(20,5),4
                Pa = New SqlParameter("@CANTIDAD", SqlDbType.Float)
                Pa.Value = CDbl(Cantidad)
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                '@NRO_LOTE		VARCHAR(100),5
                Pa = New SqlParameter("@NRO_LOTE", SqlDbType.VarChar, 100)
                If Me.SolicitaLote Then
                    Pa.Value = NroLote
                Else
                    Pa.Value = DBNull.Value
                End If
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                '@NRO_PARTIDA	VARCHAR(100),6
                Pa = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 100)
                If Me.SolicitaPartida Then
                    Pa.Value = NroPartida
                Else
                    Pa.Value = DBNull.Value
                End If
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                '@F_VENCIMIENTO	VARCHAR(100),7
                Pa = New SqlParameter("@F_VENCIMIENTO", SqlDbType.VarChar, 100)
                If Me.SolicitaVencimiento Then
                    Pa.Value = FVto
                Else
                    Pa.Value = DBNull.Value
                End If
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                '@NRO_PALLET		VARCHAR(100),8
                Pa = New SqlParameter("@NRO_PALLET", SqlDbType.VarChar, 100)
                Pa.Value = NroPallet
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                '@USUARIO_ID		VARCHAR(100)9
                Pa = New SqlParameter("@USUARIO_ID", SqlDbType.VarChar, 100)
                Pa.Value = vUsr.CodUsuario
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()

            Else : MsgBox(SQLConErr, MsgBoxStyle.Critical, clsName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        End Try
    End Function

    Public Function ContenidoPallet(ByVal NroPallet As String, ByRef DS As DataSet) As Boolean
        Dim xCmd As SqlCommand, Pa As SqlParameter, Da As SqlDataAdapter
        Try

            If VerifyConnection(Conn) Then
                xCmd = Conn.CreateCommand
                Da = New SqlDataAdapter(xCmd)

                xCmd.CommandText = "DBO.MOB_RG_CONTENIDO_PALLET"
                xCmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@PALLET", SqlDbType.VarChar, 100)
                Pa.Value = NroPallet
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Da.Fill(DS, "CONTENIDO")

                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        Return True
                    End If
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Critical, clsName)
                Return False
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        Finally
            xCmd.Dispose()
            Da.Dispose()
        End Try

    End Function

    Public Function QuitarContenido(ByVal ID As String) As Boolean
        Dim xCMD As SqlCommand
        Try
            If VerifyConnection(Conn) Then
                xCMD = Conn.CreateCommand
                xCMD.CommandType = CommandType.Text
                xCMD.CommandText = "delete from recepcion_guardado where rcp_id=" & ID

                xCMD.ExecuteNonQuery()

            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        Finally
            xCMD.Dispose()
        End Try
    End Function

    Public Function CrearDocumentoIngreso(ByVal NroPallet As String) As Boolean
        Dim xCMD As SqlCommand, Pa As SqlParameter, Trans As SqlTransaction, MsgErr As String = ""
        Try
            If VerifyConnection(Conn) Then

                xCMD = Conn.CreateCommand
                Trans = Conn.BeginTransaction

                xCMD.Transaction = Trans
                xCMD.CommandType = CommandType.StoredProcedure
                xCMD.CommandText = "DBO.MOB_RG_CREAR_DOCUMENTO"

                Pa = New SqlParameter("@PNRO_PALLET", SqlDbType.VarChar, 100)
                Pa.Value = NroPallet
                xCMD.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@DOCUMENTO_ID", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                xCMD.Parameters.Add(Pa)
                Pa = Nothing

                xCMD.ExecuteNonQuery()

                DocumentoId = xCMD.Parameters("@DOCUMENTO_ID").Value
                Trans.Commit()
            End If
            Return True
        Catch SQLEx As SqlException
            MsgErr = SQLEx.Message
            Trans.Rollback()
            MsgBox(MsgErr, MsgBoxStyle.Information, clsName)

        Catch ex As Exception
            MsgErr = ex.Message
            Trans.Rollback()
            MsgBox(MsgErr, MsgBoxStyle.Critical, clsName)
        Finally
            Trans.Dispose()
            xCMD.Dispose()
        End Try
    End Function

    Public Function Locator_Ing(ByVal NroPallet As String, ByRef LblUbicacion As Label, ByRef lblErr As Label) As Boolean
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Try

            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "LOCATOR_ING_X_ALTURA"
                Cmd.Connection = SQLc
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Documento_id", SqlDbType.Int)
                Pa.Value = Me.DocumentoId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Nro_linea", SqlDbType.Int)
                Pa.Value = "1"
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NroPallet", SqlDbType.VarChar, 100)
                Pa.Value = NroPallet
                Cmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@CANT", SqlDbType.Int)
                Pa.Value = 0
                Cmd.Parameters.Add(Pa)

                Da.Fill(Ds, "Locator_ing")
                vPosicionID = IIf(Ds.Tables(0).Rows(0)(0) Is DBNull.Value, 0, Ds.Tables(0).Rows(0)(0))
                vPosicionCOD = IIf(Ds.Tables(0).Rows(0)(1) Is DBNull.Value, "", Ds.Tables(0).Rows(0)(1))
                vNaveID = IIf(Ds.Tables(0).Rows(0)(2) Is DBNull.Value, 0, Ds.Tables(0).Rows(0)(2))
                OrdenLocator = IIf(Ds.Tables(0).Rows(0)(3) Is DBNull.Value, 0, Ds.Tables(0).Rows(0)(3))

                If Me.vPosicionCOD.ToString <> "" Then
                    LblUbicacion.Text = "Destino: " & Me.vPosicionCOD
                End If
            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, clsName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            lblErr.Text = SQLEx.Message
            Return False
        Catch ex As Exception
            MsgBox("Locator_Ing: " & ex.Message, MsgBoxStyle.OkOnly, clsName)
            Return False
        Finally
            Ds.Dispose()
            Cmd = Nothing
            Da = Nothing
            Pa = Nothing
        End Try
    End Function

    Public Function Procesar(ByVal NroPallet As String, ByVal Ubicacion As String) As Boolean
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
        Dim Nro_Linea As Long = 0

        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(Conn) Then

                BuscarPosicionId(Ubicacion)

                Trans = Conn.BeginTransaction
                Cmd = Conn.CreateCommand
                Cmd.Connection = Conn
                Cmd.Transaction = Trans
                CI.objConnection = Conn
                CI.Cmd = Cmd
                GC.Collect()
                GetValueForPalletMP(NroPallet, DocumentoId, Cmd, dSTMP)

                Cmd.Parameters.Clear()
                For I = 0 To dSTMP.Tables("TABLE").Rows.Count - 1
                    Nro_Linea = dSTMP.Tables("TABLE").Rows(I)(0)

                    If Not CI.ExecuteAll(DocumentoId, Nro_Linea, IIf(Me.vPosicionID = 0, Nothing, Me.vPosicionID), Me.vNaveID) Then
                        Throw New Exception("Error en modulo ExecuteAll.")
                    End If

                    If Not IngresaAuditoria(DocumentoId, Nro_Linea, Cmd, Me.vPosicionID, vError) Then
                        Throw New Exception(vError)
                    End If

                Next
                CA.DocumentoID = DocumentoId
                CA.Cmd = Cmd
                CA.NroLinea = 1
                CA.objConnection = Conn
                CA.OperacionID = "ING"
                CA.UsuarioID = vUsr.CodUsuario
                If Not CA.Aceptar() Then
                    Throw New Exception("Error en modulo Aceptar.")
                End If
                If Not Sys_Dev(DocumentoId, 1, Cmd) Then
                    Throw New Exception("No se pudo completar la operacion Sys_Dev")
                End If
            Else
                'AGREGAR EL ERROR DE CONEXION
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly, clsName)
                Procesar = False
            End If
            Trans.Commit()
            Procesar = True
        Catch ex As Exception
            Trans.Rollback()
            Procesar = False
            MsgBox("Procesar: " & ex.Message, MsgBoxStyle.OkOnly, clsName)
        Finally
            CI = Nothing
            CA = Nothing
        End Try
    End Function

    Private Function IngresaAuditoria(ByVal Documento_Id As Long, ByVal Nro_Linea As Long, _
                                     ByRef xCmd As SqlCommand, ByVal Ubicacion As String, _
                                     ByRef vError As String) As Boolean
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(Conn) Then
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
            Else : MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, clsName)
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

    Private Sub GetValueForPalletMP(ByVal Pallet As String, ByVal DocumentoID As Long, ByVal xCmd As SqlCommand, ByVal dS As DataSet)
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim vInt As Integer = 0

        Try
            If VerifyConnection(Conn) Then
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
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        Finally
            Pa = Nothing
        End Try
    End Sub

    Private Function Sys_Dev(ByVal DocumentoId As Long, ByVal Estado As Integer, ByRef oCmd As SqlCommand) As Boolean

        Try
            If VerifyConnection(Conn) Then
                oCmd.Parameters.Clear()
                oCmd.CommandText = "Sys_dev"
                oCmd.CommandType = CommandType.StoredProcedure
                oCmd.Parameters.Add("@Documento_ID", SqlDbType.Int).Value = DocumentoId
                oCmd.Parameters.Add("@Estado", SqlDbType.Int).Value = Estado
                oCmd.ExecuteNonQuery()
            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, clsName)
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

    Private Function BuscarPosicionId(ByVal Posicion_Codigo As String) As Long
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim Posicion_Id As Long
        Try

            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "MobBuscarPosicion"
                Cmd.Connection = Conn
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@POSICION_COD", SqlDbType.VarChar, 45)
                Pa.Value = Posicion_Codigo
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Da.Fill(Ds, "MobBuscarPosicion")
                Posicion_Id = Ds.Tables("MobBuscarPosicion").Rows(0)("POSICION_ID").ToString()

                If Ds.Tables("MobBuscarPosicion").Rows(0)("TIPO").ToString() = "POS" Then
                    Me.vPosicionID = Ds.Tables("MobBuscarPosicion").Rows(0)("POSICION_ID").ToString()
                    Me.vNaveID = Nothing
                Else
                    If Ds.Tables("MobBuscarPosicion").Rows(0)("TIPO").ToString() = "NAVE" Then
                        Me.vNaveID = Ds.Tables("MobBuscarPosicion").Rows(0)("POSICION_ID").ToString()
                        Me.vPosicionID = Nothing
                    End If
                End If
                Return Posicion_Id
            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, clsName)
                Return Nothing
            End If

        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, clsName)
            Return Nothing
        Catch ex As Exception
            MsgBox("BuscarPosicionId: " & ex.Message, MsgBoxStyle.OkOnly, clsName)
            Return Nothing
        Finally
            Cmd = Nothing
            Da = Nothing
            Pa = Nothing
        End Try
    End Function

    Public Function FinOC(ByVal Oc As String) As Boolean
        Dim vSQL As String, CMD As SqlCommand, DS As New DataSet, Da As SqlDataAdapter
        Try
            vSQL = vSQL & "SELECT	COUNT(*)" & vbNewLine
            vSQL = vSQL & "FROM	SYS_INT_DET_DOCUMENTO SDD" & vbNewLine
            vSQL = vSQL & "WHERE	EXISTS (SELECT	1" & vbNewLine
            vSQL = vSQL & "	                        FROM 	SYS_INT_DOCUMENTO SD" & vbNewLine
            vSQL = vSQL & "	                        WHERE	SD.ORDEN_DE_COMPRA='" & Oc & "'" & vbNewLine
            vSQL = vSQL & "			                            AND SDD.CLIENTE_ID=SD.CLIENTE_ID" & vbNewLine
            vSQL = vSQL & "			                            AND SDD.DOC_EXT=SD.DOC_EXT)" & vbNewLine
            vSQL = vSQL & "AND SDD.ESTADO_GT IS NULL" & vbNewLine

            If VerifyConnection(Conn) Then

                CMD = Conn.CreateCommand
                Da = New SqlDataAdapter(CMD)
                CMD.CommandText = vSQL
                CMD.Connection = Conn
                CMD.CommandType = CommandType.Text

                Da.Fill(DS, "PEND")
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        If DS.Tables(0).Rows(0)(0) > 0 Then
                            Return False
                        Else
                            Return True
                        End If
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, clsName)
                Return Nothing
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.OkOnly, clsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, clsName)
            Return False
        Finally
            CMD.Dispose()
            DS.Dispose()
            Da.Dispose()
        End Try
    End Function

    Public Function EliminarPallet(ByVal NroPallet As String) As Boolean
        Dim xCMD As SqlCommand
        Try
            If VerifyConnection(Conn) Then
                xCMD = Conn.CreateCommand
                xCMD.CommandType = CommandType.Text
                xCMD.CommandText = "delete from recepcion_guardado where nro_pallet='" & NroPallet & "' and isnull(procesado,'0')='0'"

                xCMD.ExecuteNonQuery()

            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        Finally
            xCMD.Dispose()
        End Try
    End Function

#End Region

End Class
