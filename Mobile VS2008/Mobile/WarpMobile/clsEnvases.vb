Imports System.Data.SqlClient


Public Class clsEnvases

#Region "Declaraciones"
    Private Const ClsErr As String = "obj. Envase"
    Private xCmd As SqlCommand
    Private lngDocId As Long
    Private lngDocTrans As Long
    Private xErr As String
    Private xViajeId As String = ""
#End Region

#Region "Propertys"

    Public Property Viaje_Id() As String
        Get
            Return xViajeId
        End Get
        Set(ByVal value As String)
            xViajeId = value
        End Set
    End Property

    Public Property Cmd() As SqlCommand
        Get
            Return xCmd
        End Get
        Set(ByVal value As SqlCommand)
            xCmd = value
        End Set
    End Property

    Public ReadOnly Property DocumentoId() As Long
        Get
            Return lngDocId
        End Get
    End Property

    Public ReadOnly Property DocumentoTransaccion() As Long
        Get
            Return lngDocTrans
        End Get
    End Property

    Public ReadOnly Property GetError() As String
        Get
            Return xErr
        End Get
    End Property

#End Region

    Public Function CargaCabecera(ByVal ClienteId As String, ByVal TipoComprobanteId As String, ByVal Viaje_id As String) As Boolean
        Dim Pa As SqlParameter
        Try
            Cmd.Parameters.Clear()
            Cmd.CommandText = "Documento_Api#InsertRecord"
            Cmd.CommandType = Data.CommandType.StoredProcedure
            Pa = New SqlParameter("@P_Documento_id", Data.SqlDbType.BigInt)
            Pa.Direction = Data.ParameterDirection.Output
            Cmd.Parameters.Add(Pa)
            Pa = Nothing
            Cmd.Parameters.Add("@p_Cliente_Id", Data.SqlDbType.VarChar, 15).Value = ClienteId
            Cmd.Parameters.Add("@P_Tipo_Comprobante_Id", Data.SqlDbType.VarChar, 5).Value = TipoComprobanteId
            Cmd.Parameters.Add("@P_Tipo_Operacion_Id", Data.SqlDbType.VarChar, 5).Value = "EGR"
            Cmd.Parameters.Add("@P_Det_Tipo_Operacion_Id", Data.SqlDbType.VarChar, 5).Value = "MAN"
            Cmd.Parameters.Add("@P_Cpte_Prefijo", Data.SqlDbType.VarChar, 6).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Cpte_Numero", Data.SqlDbType.VarChar, 20).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Fecha_Cpte", Data.SqlDbType.VarChar, 20).Value = Now.ToString
            Cmd.Parameters.Add("@P_Fecha_Pedida_Ent", Data.SqlDbType.varchar, 20).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Sucursal_Origen", Data.SqlDbType.VarChar, 20).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Sucursal_Destino", Data.SqlDbType.VarChar, 20).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Anulado", Data.SqlDbType.VarChar, 1).Value = "0"
            Cmd.Parameters.Add("@P_Motivo_Anulacion", Data.SqlDbType.VarChar, 15).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Peso_Total", Data.SqlDbType.Float).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Unidad_Peso", Data.SqlDbType.VarChar, 5).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Volumen_Total", Data.SqlDbType.Float).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Unidad_Volumen", Data.SqlDbType.VarChar, 5).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Total_Bultos", Data.SqlDbType.Int).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Valor_Declarado", Data.SqlDbType.Float).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Orden_De_Compra", Data.SqlDbType.VarChar, 20).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Cant_Items", Data.SqlDbType.Int).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Observaciones", Data.SqlDbType.VarChar, 200).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Status", Data.SqlDbType.VarChar, 3).Value = DBNull.Value
            Cmd.Parameters.Add("@P_NroRemito", Data.SqlDbType.VarChar, 30).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Fecha_Alta_Gtw", Data.SqlDbType.VarChar, 20).Value = Now.ToString
            Cmd.Parameters.Add("@P_Fecha_Fin_Gtw", Data.SqlDbType.VarChar, 20).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Personal_Id", Data.SqlDbType.VarChar, 20).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Transporte_Id", Data.SqlDbType.VarChar, 20).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Nro_Despacho_Importacion", Data.SqlDbType.VarChar, 30).Value = Viaje_id
            Cmd.Parameters.Add("@P_Alto", Data.SqlDbType.Float).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Ancho", Data.SqlDbType.Float).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Largo", Data.SqlDbType.Float).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Unidad_Medida", Data.SqlDbType.VarChar, 5).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Grupo_Picking", Data.SqlDbType.VarChar, 50).Value = DBNull.Value
            Cmd.Parameters.Add("@P_Prioridad_Picking", Data.SqlDbType.Int).Value = DBNull.Value


            Cmd.ExecuteNonQuery()

            lngDocId = IIf(IsDBNull(Cmd.Parameters("@P_Documento_id").Value), 0, Cmd.Parameters("@P_Documento_id").Value)

            Return True
        Catch SQLEx As SqlException
            xErr = "CargaCabecera SQL: " & SQLEx.Message
            Return False
        Catch ex As Exception
            xErr = "CargaCabecera : " & ex.Message
            Return False
        Finally
            Pa = Nothing
        End Try
    End Function

    Public Function CargaDetalle(ByVal ProductoId As String, ByVal Cantidad As Double, _
                                  ByVal ClienteId As String) As Boolean
        Dim Pa As SqlParameter
        Try
            Cmd.Parameters.Clear()
            Cmd.CommandText = "Det_Documento_Api#InsertRecord"
            Cmd.CommandType = Data.CommandType.StoredProcedure
            Cmd.Parameters.Add("@p_documento_id", Data.SqlDbType.Int).Value = Me.lngDocId
            Cmd.Parameters.Add("@p_nro_linea", Data.SqlDbType.Int).Value = DBNull.Value
            Cmd.Parameters.Add("@p_cliente_id", Data.SqlDbType.VarChar, 15).Value = ClienteId
            Cmd.Parameters.Add("@p_producto_id", Data.SqlDbType.VarChar, 30).Value = ProductoId
            Cmd.Parameters.Add("@p_cantidad", Data.SqlDbType.Float).Value = Cantidad
            Cmd.Parameters.Add("@p_nro_serie", Data.SqlDbType.VarChar, 50).Value = DBNull.Value
            Cmd.Parameters.Add("@p_nro_serie_padre", Data.SqlDbType.VarChar, 50).Value = DBNull.Value
            Cmd.Parameters.Add("@p_est_merc_id", Data.SqlDbType.VarChar, 15).Value = DBNull.Value
            Cmd.Parameters.Add("@p_cat_log_id", Data.SqlDbType.VarChar, 50).Value = DBNull.Value
            Cmd.Parameters.Add("@p_nro_bulto", Data.SqlDbType.VarChar, 50).Value = DBNull.Value
            Cmd.Parameters.Add("@p_descripcion", Data.SqlDbType.VarChar, 200).Value = DBNull.Value
            Cmd.Parameters.Add("@p_nro_lote", Data.SqlDbType.VarChar, 50).Value = DBNull.Value
            Cmd.Parameters.Add("@p_fecha_vencimiento", Data.SqlDbType.DateTime).Value = DBNull.Value
            Cmd.Parameters.Add("@p_nro_despacho", Data.SqlDbType.VarChar, 50).Value = DBNull.Value
            Cmd.Parameters.Add("@p_nro_partida", Data.SqlDbType.VarChar, 50).Value = DBNull.Value
            Cmd.Parameters.Add("@p_unidad_id", Data.SqlDbType.VarChar, 5).Value = DBNull.Value
            Cmd.Parameters.Add("@p_peso", Data.SqlDbType.Float).Value = DBNull.Value
            Cmd.Parameters.Add("@p_unidad_peso", Data.SqlDbType.VarChar, 5).Value = DBNull.Value
            Cmd.Parameters.Add("@p_volumen", Data.SqlDbType.Float).Value = DBNull.Value
            Cmd.Parameters.Add("@p_unidad_volumen", Data.SqlDbType.VarChar, 5).Value = DBNull.Value
            Cmd.Parameters.Add("@p_busc_individual", Data.SqlDbType.VarChar, 1).Value = DBNull.Value
            Cmd.Parameters.Add("@p_tie_in", Data.SqlDbType.VarChar, 1).Value = "0"
            Cmd.Parameters.Add("@p_nro_tie_in_padre", Data.SqlDbType.VarChar, 100).Value = DBNull.Value
            Cmd.Parameters.Add("@p_nro_tie_in", Data.SqlDbType.VarChar, 100).Value = DBNull.Value
            Cmd.Parameters.Add("@p_item_ok", Data.SqlDbType.VarChar, 1).Value = DBNull.Value
            Cmd.Parameters.Add("@p_moneda_id", Data.SqlDbType.VarChar, 20).Value = DBNull.Value
            Cmd.Parameters.Add("@p_costo", Data.SqlDbType.Float).Value = DBNull.Value
            Cmd.Parameters.Add("@p_cat_log_id_final", Data.SqlDbType.VarChar, 50).Value = "DISPONIBLE"
            Cmd.Parameters.Add("@p_prop1", Data.SqlDbType.VarChar, 100).Value = DBNull.Value
            Cmd.Parameters.Add("@p_prop2", Data.SqlDbType.VarChar, 100).Value = DBNull.Value
            Cmd.Parameters.Add("@p_prop3", Data.SqlDbType.VarChar, 100).Value = DBNull.Value
            Cmd.Parameters.Add("@p_largo", Data.SqlDbType.Float).Value = DBNull.Value
            Cmd.Parameters.Add("@p_alto", Data.SqlDbType.Float).Value = DBNull.Value
            Cmd.Parameters.Add("@p_ancho", Data.SqlDbType.Float).Value = DBNull.Value
            Cmd.Parameters.Add("@p_volumen_unitario", Data.SqlDbType.VarChar, 1).Value = DBNull.Value
            Cmd.Parameters.Add("@p_peso_unitario", Data.SqlDbType.VarChar, 1).Value = DBNull.Value
            Cmd.Parameters.Add("@p_cant_solicitada", Data.SqlDbType.Float).Value = Cantidad

            Cmd.ExecuteNonQuery()

            Return True
        Catch SQLEx As SqlException
            xErr = "CargaDetalle SQL: " & SQLEx.Message
            Return False
        Catch ex As Exception
            xErr = "CargaDetalle: " & ex.Message
            Return False
        Finally
            Pa = Nothing
        End Try
    End Function

    Private Function Aceptar() As Boolean
        Try
            Cmd.Parameters.Clear()
            Cmd.CommandText = "Funciones_Stock_Api#Documento_A_Egreso"
            Cmd.CommandType = Data.CommandType.StoredProcedure
            Cmd.Parameters.Add("@pdocumentoid", Data.SqlDbType.Int).Value = Me.DocumentoId
            Cmd.ExecuteNonQuery()
            Return True
        Catch SQLEx As SqlException
            xErr = "Aceptar SQL: " & SQLEx.Message
            Return False
        Catch ex As Exception
            xErr = "Aceptar: " & ex.Message
            Return False
        End Try
    End Function

    Private Function AsignarTratamiento() As Boolean
        Try
            Cmd.Parameters.Clear()
            Cmd.CommandType = Data.CommandType.StoredProcedure
            Cmd.CommandText = "Asigna_Tratamiento#Asigna_Tratamiento_EGR"
            Cmd.Parameters.Add("@P_Doc_Id", Data.SqlDbType.BigInt).Value = Me.DocumentoId

            Cmd.ExecuteNonQuery()
            Return True
        Catch SQLEx As SqlException
            xErr = "AsignarTratamiento SQL: " & SQLEx.Message
            Return False
        Catch ex As Exception
            xErr = "AsignarTratamiento: " & ex.Message
            Return False
        End Try
    End Function

    Private Function GetDocTransId() As Boolean
        Dim Pa As SqlParameter
        Try
            Cmd.Parameters.Clear()
            Cmd.CommandText = "Mob_GetDocTransId"
            Cmd.CommandType = Data.CommandType.StoredProcedure

            Cmd.Parameters.Add("@DocumentoId", Data.SqlDbType.BigInt).Value = Me.DocumentoId
            Pa = New SqlParameter("@DocTransId", Data.SqlDbType.BigInt)
            Pa.Direction = Data.ParameterDirection.Output
            Cmd.Parameters.Add(Pa)

            Cmd.ExecuteNonQuery()

            Me.lngDocTrans = IIf(IsDBNull(Cmd.Parameters("@DocTransId").Value), 0, Cmd.Parameters("@DocTransId").Value)
            Return True
        Catch SQLEx As SqlException
            xErr = "AsignarTratamiento SQL: " & SQLEx.Message
            Return False
        Catch ex As Exception
            xErr = "AsignarTratamiento: " & ex.Message
            Return False
        Finally
            Pa = Nothing
        End Try
    End Function

    Private Function AutoCompletarEstacion() As Boolean
        Try
            Cmd.Parameters.Clear()
            Cmd.CommandType = Data.CommandType.StoredProcedure
            Cmd.CommandText = "AutoCompletarEstacion"
            Cmd.Parameters.Add("@pDocumento_id", Data.SqlDbType.BigInt).Value = Me.DocumentoId
            Cmd.ExecuteNonQuery()
            Return True
        Catch SQLEx As SqlException
            xErr = "AsignarTratamiento SQL: " & SQLEx.Message
            Return False
        Catch ex As Exception
            xErr = "AsignarTratamiento: " & ex.Message
            Return False
        End Try
 
    End Function

    Private Function EgrAceptar() As Boolean
        Try
            Cmd.Parameters.Clear()
            Cmd.CommandText = "egr_aceptar"
            Cmd.CommandType = Data.CommandType.StoredProcedure
            Cmd.Parameters.Add("@Doc_Trans_Id", Data.SqlDbType.BigInt).Value = Me.DocumentoTransaccion
            Cmd.ExecuteNonQuery()
            Return True
        Catch SQLEx As SqlException
            xErr = "AsignarTratamiento SQL: " & SQLEx.Message
            Return False
        Catch ex As Exception
            xErr = "AsignarTratamiento: " & ex.Message
            Return False
        End Try
    End Function

    Public Function Procesar() As Boolean
        Try
            If Not Aceptar() Then Throw New Exception()
            If Not AsignarTratamiento() Then Throw New Exception
            If Not GetDocTransId() Then Throw New Exception
            If Not AutoCompletarEstacion() Then Throw New Exception
            If Not EgrAceptar() Then Throw New Exception
            If Not GrabarDoc() Then Throw New Exception
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GrabarDoc() As Boolean
        Try
            Cmd.Parameters.Clear()
            Cmd.CommandText = "Mob_GrabarDocumento"
            Cmd.CommandType = Data.CommandType.StoredProcedure
            Cmd.Parameters.Add("@Viaje_Id", Data.SqlDbType.VarChar, 100).Value = Me.Viaje_Id
            Cmd.Parameters.Add("@Documento_Id", Data.SqlDbType.Float).Value = Me.DocumentoId
            Cmd.ExecuteNonQuery()
            Return True
        Catch SQLEx As SqlException
            xErr = "GrabarDoc SQL: " & SQLEx.Message
            Return False
        Catch ex As Exception
            xErr = "GrabarDoc: " & ex.Message
            Return False
        End Try
    End Function

End Class
