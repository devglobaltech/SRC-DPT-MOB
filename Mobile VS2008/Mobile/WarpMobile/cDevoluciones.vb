Imports System.Data
Imports System.Data.SqlClient

Public Class cDevoluciones
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
#Region "Variables"

    Private Conn As SqlConnection
    Private Const clsName As String = "Devoluciones."
    Private Const SQLConErr As String = "No se pudo conectar a la base de datos."
    Private vCliente_ID As String = ""
    Private vCodigoViaje As String = ""
    Private vPedido As String = ""
    Private vPalletDevolucion As String = ""
    Private vProducto_ID As String = ""
    Private o2D As New clsDecode2D
#End Region
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
#Region "Propertys"

    Public ReadOnly Property ProductoId() As String
        Get
            Return vProducto_ID
        End Get
    End Property

    Public ReadOnly Property PalletDevolucion() As String
        Get
            Return vPalletDevolucion
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

    Public ReadOnly Property GetProductoDescripcion() As String
        Get
            Return ProductoDescripcion
        End Get
    End Property

#End Region
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
#Region "Metodos"

    Private Function ProductoDescripcion() As String
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, SQL As String = "", Retorno As String = ""
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                SQL = ""
                SQL = SQL & "SELECT	PRODUCTO_ID + ' - ' + DESCRIPCION FROM PRODUCTO WHERE CLIENTE_ID='" & Me.Cliente_ID & "' AND PRODUCTO_ID='" & Me.vProducto_ID & "'" & vbNewLine

                Cmd.CommandText = SQL
                Cmd.CommandType = CommandType.Text

                DA.Fill(DS, "CLIENTES")

                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        Retorno = DS.Tables(0).Rows(0)(0).ToString
                    End If
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Information, clsName)
                Return False
            End If

            Return Retorno
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

    Public Function Inicializar() As Boolean
        vCliente_ID = ""
        vCodigoViaje = ""
        vPedido = ""
        vPalletDevolucion = ""
        GetPalletDevolucion()
    End Function

    Private Function GetPalletDevolucion() As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand

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

                vPalletDevolucion = xCmd.Parameters("@VALUE").Value.ToString

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

    Public Function GetMotivos(ByRef Cmb As ComboBox) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, SQL As String = ""
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                SQL = "SELECT MOTIVO_ID,DESCRIPCION FROM DBO.MOTIVO WHERE TIPO_OPERACION_ID='ING'"
                Cmd.CommandText = SQL
                Cmd.CommandType = CommandType.Text

                DA.Fill(DS, "MOTIVOS")

                Cmb.DataSource = DS.Tables(0)
                Cmb.ValueMember = "MOTIVO_ID"
                Cmb.DisplayMember = "DESCRIPCION"

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

    Public Function BuscarViajeExistente(ByVal CodigoViaje As String, ByRef DgResultado As DataGrid) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, XSQL As String = "", RETORNO As Boolean
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                XSQL = ""
                XSQL = XSQL & "SELECT  DISTINCT NRO_DESPACHO_IMPORTACION AS [PICKING/VIAJE]," & vbNewLine
                XSQL = XSQL & "            CLIENTE_ID AS [CLIENTE]" & vbNewLine
                XSQL = XSQL & "FROM     vDOCUMENTO (nolock)" & vbNewLine
                XSQL = XSQL & "WHERE   STATUS >= 'D40'" & vbNewLine
                XSQL = XSQL & "             AND NRO_DESPACHO_IMPORTACION IS NOT NULL" & vbNewLine
                XSQL = XSQL & "             AND TIPO_OPERACION_ID='EGR'" & vbNewLine
                XSQL = XSQL & "             AND NRO_DESPACHO_IMPORTACION LIKE '%" & CodigoViaje & "%'" & vbNewLine

                Cmd.CommandText = XSQL
                Cmd.CommandType = CommandType.Text

                DA.Fill(DS, "CODIGO_VIAJE")

                If DS.Tables(0).Rows.Count > 0 Then
                    RETORNO = True
                    DgResultado.DataSource = DS.Tables(0)
                Else
                    RETORNO = False
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Information, clsName)
                Return False
            End If

            Return RETORNO

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

    Public Function BuscarViajeExistentePorPedido(ByVal Pedido As String, ByRef dgResultado As DataGrid) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, XSQL As String = "", RETORNO As Boolean
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                XSQL = ""
                XSQL = XSQL & "SELECT  DISTINCT NRO_DESPACHO_IMPORTACION AS [PICKING/VIAJE]," & vbNewLine
                XSQL = XSQL & "            CLIENTE_ID AS [CLIENTE]" & vbNewLine
                XSQL = XSQL & "FROM    vDOCUMENTO (nolock)" & vbNewLine
                XSQL = XSQL & "WHERE  STATUS >= 'D40'" & vbNewLine
                XSQL = XSQL & "            AND NRO_DESPACHO_IMPORTACION IS NOT NULL" & vbNewLine
                XSQL = XSQL & "            AND TIPO_OPERACION_ID='EGR'" & vbNewLine
                XSQL = XSQL & "            AND NRO_REMITO='%" & Pedido & "%'"

                Cmd.CommandText = XSQL
                Cmd.CommandType = CommandType.Text

                DA.Fill(DS, "CODIGO_VIAJE")

                If DS.Tables(0).Rows.Count > 0 Then
                    RETORNO = True
                    dgResultado.DataSource = DS.Tables(0)
                Else
                    RETORNO = False
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Information, clsName)
                Return False
            End If

            Return RETORNO

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

    Private Function SetearNroCmr(ByRef Cmd As SqlCommand, ByVal NroCMR As String, ByVal Cliente As String, ByVal Documento As String, ByVal Linea As String) As Boolean
        Dim DA As SqlDataAdapter, XSQL As String = "", PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)

                XSQL = "[dbo].[SETNROCMR]"
                Cmd.CommandText = XSQL
                Cmd.CommandType = CommandType.StoredProcedure

                PA = New SqlParameter("@NRO_CMR", SqlDbType.VarChar, 40)
                PA.Value = NroCMR
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@DOCUMENTO_ID", SqlDbType.VarChar, 40)
                PA.Value = Documento
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 40)
                PA.Value = Cliente
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@NRO_LINEA", SqlDbType.VarChar, 40)
                PA.Value = Linea
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()

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
            PA = Nothing
        End Try
    End Function

    Public Function GetProductos(ByRef DS As DataSet) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, XSQL As String = "", RETORNO As Boolean, PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                XSQL = "dbo.Frontera_IngresoxEgreso"
                Cmd.CommandText = XSQL
                Cmd.CommandType = CommandType.StoredProcedure

                PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.vCodigoViaje
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                If Trim(Me.vPedido) = "" Then
                    PA.Value = DBNull.Value
                Else
                    PA.Value = Me.vCodigoViaje
                End If
                Cmd.Parameters.Add(PA)
                PA = Nothing

                DA.Fill(DS, "PRODUCTOS")

                If DS.Tables(0).Rows.Count > 0 Then
                    RETORNO = True
                    vCliente_ID = DS.Tables(0).Rows(0)(1).ToString
                Else
                    RETORNO = False
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Information, clsName)
                Return False
            End If

            Return RETORNO

        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, clsName)
        Finally
            DA.Dispose()
            Cmd.Dispose()
            PA = Nothing
        End Try
    End Function

    Public Function BuscarProducto(ByVal Lectura As String, ByRef lblDescripcion As Label) As Boolean
        Try
            Me.o2D.CLIENTE_ID = Me.Cliente_ID
            Me.o2D.Decode(Lectura)



            If o2D.QtySeries > 0 Then
                'VALIDAR LA LECTURA...
                Me.vProducto_ID = o2D.PRODUCTO_ID

                If Not ValidarLectura() Then
                    Return False
                End If

                lblDescripcion.Text = Me.ProductoDescripcion & " - " & "Cant. Series: " & o2D.QtySeries
                Return True
            Else
                MsgBox("La lectura no corresponde a un codigo 2D.", MsgBoxStyle.OkOnly, clsName)
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, clsName)
        End Try

    End Function

    Public Function GuardarDevolucion(ByVal Motivo As String, ByVal Observaciones As String) As Boolean

        Dim DA As SqlDataAdapter, Cmd As SqlCommand, XSQL As String = "", RETORNO As Boolean, PA As SqlParameter
        Dim mArray As New Collection, NroSerie As String, Trans As SqlTransaction
        Try
            If VerifyConnection(Conn) Then

                Trans = Conn.BeginTransaction
                Cmd = Conn.CreateCommand
                Cmd.Transaction = Trans
                Cmd.CommandText = "DBO.MOB_DEVOLUCION_INSERT_REG"
                Cmd.CommandType = CommandType.StoredProcedure
                mArray = o2D.aSeries

                For Each NroSerie In mArray

                    PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                    PA.Value = Me.Cliente_ID
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                    PA.Value = Me.ProductoId
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@PALLET_DEVOLUCION", SqlDbType.VarChar, 100)
                    PA.Value = Me.PalletDevolucion
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@MOTIVO_ID", SqlDbType.VarChar, 100)
                    PA.Value = Motivo
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@NRO_LOTE", SqlDbType.VarChar, 100)
                    PA.Value = DBNull.Value
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@NRO_PALLET", SqlDbType.VarChar, 100)
                    PA.Value = DBNull.Value
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 100)
                    PA.Value = DBNull.Value
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 100)
                    PA.Value = NroSerie
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@OBSERVACIONES", SqlDbType.VarChar, 200)
                    If Replace(Observaciones, vbCrLf, "") <> "" Then
                        PA.Value = Replace(Observaciones, vbCrLf, "")
                    Else
                        PA.Value = DBNull.Value
                    End If
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                    PA.Value = vUsr.CodUsuario
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    Cmd.ExecuteNonQuery()
                    Cmd.Parameters.Clear()

                Next

                Trans.Commit()

            Else : MsgBox(SQLConErr, MsgBoxStyle.Information, clsName)
                Return False
            End If

            Return True

        Catch SQLEx As SqlException
            Trans.Rollback()
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            Trans.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.Information, clsName)
        Finally
            Try
                DA.Dispose()
                Cmd.Dispose()
                Trans.Dispose()
                PA = Nothing
            Catch ex As Exception
            End Try
        End Try
    End Function

    Public Function GetContenidoPallet(ByVal Pallet As String, ByRef DS As DataSet)
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, XSQL As String = "", RETORNO As Boolean, PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                XSQL = "DBO.MOB_DEV_CONTENIDO_PALLET"
                Cmd.CommandText = XSQL
                Cmd.CommandType = CommandType.StoredProcedure

                PA = New SqlParameter("@PALLET_DEVOLUCION", SqlDbType.VarChar, 100)
                PA.Value = Pallet
                Cmd.Parameters.Add(PA)
                PA = Nothing

                DA.Fill(DS, "PRODUCTOS")

                If DS.Tables(0).Rows.Count > 0 Then
                    RETORNO = True
                Else
                    RETORNO = False
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Information, clsName)
                Return False
            End If

            Return RETORNO

        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, clsName)
        Finally
            DA.Dispose()
            Cmd.Dispose()
            PA = Nothing
        End Try
    End Function

    Public Function CancelarPallet() As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, SQL As String = ""
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                SQL = ""
                SQL = SQL & "DELETE FROM MOB_DEVOLUCIONES_TMP WHERE PALLET_DEVOLUCION=" & Me.PalletDevolucion

                Cmd.CommandText = SQL
                Cmd.CommandType = CommandType.Text

                Cmd.ExecuteNonQuery()

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

    Public Function FinalizarDevolucion() As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, XSQL As String = "", RETORNO As Boolean, PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then

                If o2D.QtySeries = 0 Then Return True

                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)

                XSQL = "DBO.MOB_DEVOLUCION_FINALIZAR"
                Cmd.CommandText = XSQL
                Cmd.CommandType = CommandType.StoredProcedure

                PA = New SqlParameter("@PALLET_DEVOLUCION", SqlDbType.VarChar, 100)
                PA.Value = Me.PalletDevolucion
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                PA.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()

            Else : MsgBox(SQLConErr, MsgBoxStyle.Information, clsName)
                Return False
            End If

            Return True

        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, clsName)
        Finally
            Try
                DA.Dispose()
                Cmd.Dispose()
                PA = Nothing
            Catch ex As Exception
            End Try
        End Try
    End Function

    Public Function ValidarLectura() As Boolean

        Dim DA As SqlDataAdapter, Cmd As SqlCommand, XSQL As String = "", RETORNO As Boolean, PA As SqlParameter
        Dim mArray As New Collection, NroSerie As String, Trans As SqlTransaction
        Try
            If VerifyConnection(Conn) Then

                Cmd = Conn.CreateCommand
                Cmd.CommandText = "[dbo].[MOB_DEVOLUCION_VALIDACION]"
                Cmd.CommandType = CommandType.StoredProcedure
                mArray = o2D.aSeries

                For Each NroSerie In mArray

                    PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                    PA.Value = Me.Cliente_ID
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                    PA.Value = Me.ProductoId
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@PALLET_DEVOLUCION", SqlDbType.VarChar, 100)
                    PA.Value = Me.PalletDevolucion
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@MOTIVO_ID", SqlDbType.VarChar, 100)
                    PA.Value = DBNull.Value
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@NRO_LOTE", SqlDbType.VarChar, 100)
                    PA.Value = DBNull.Value
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@NRO_PALLET", SqlDbType.VarChar, 100)
                    PA.Value = DBNull.Value
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 100)
                    PA.Value = DBNull.Value
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 100)
                    PA.Value = NroSerie
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@OBSERVACIONES", SqlDbType.VarChar, 200)
                    PA.Value = DBNull.Value
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                    PA.Value = vUsr.CodUsuario
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    Cmd.ExecuteNonQuery()
                    Cmd.Parameters.Clear()

                Next

            Else : MsgBox(SQLConErr, MsgBoxStyle.Information, clsName)
                Return False
            End If

            Return True

        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, clsName)
        Finally
            Try
                DA.Dispose()
                Cmd.Dispose()
                Trans.Dispose()
                PA = Nothing
            Catch ex As Exception
            End Try
        End Try
    End Function

#End Region
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
End Class
