Imports System.Data.SqlClient
Imports System.Data
Public Class ClsIngreso

    Private Cnx As SqlConnection
    Private Const ClsName As String = "Ingreso"
    Private Const ErrorCon As String = "Fallo al conectar con la base de datos."
    'Private Cmd As SqlClient.SqlCommand
    'Private Trans As SqlTransaction
    Private oCmd As SqlCommand

    Public Property Cmd() As SqlCommand
        Get
            Return oCmd
        End Get
        Set(ByVal value As SqlCommand)
            oCmd = value
        End Set
    End Property

    Private vTransacc As SqlTransaction
    Public Property Trans() As SqlTransaction
        Get
            Return vTransacc
        End Get
        Set(ByVal value As SqlTransaction)
            vTransacc = value
        End Set
    End Property

    Private Enum ORDEN_VPAP
        DOC_TRANS_ID = 0
        NRO_LINEA_TRANS
        POS_ANTERIOR
        NAVE_ANTERIOR
        POSICION_ACTUAL
        NAVE_ACTUAL
        CANTIDAD
        MOV_PENDIENTE
        CLIENTE_ID
        CAT_LOG_ID
        CAT_LOG_ID_FINAL
        EST_MERC_ID
    End Enum

    Private Structure Rl_Struct
        Dim RL_ID As Object
        Dim DOC_TRANS_ID As Object
        Dim nro_linea_trans As Object
        Dim POSICION_ANTERIOR As Object
        Dim POSICION_ACTUAL As Object
        Dim CANTIDAD As Object
        Dim TIPO_MOVIMIENTO_ID As Object
        Dim ULTIMA_ESTACION As Object
        Dim ULTIMA_SECUENCIA As Object
        Dim NAVE_ANTERIOR As Object
        Dim NAVE_ACTUAL As Object
        Dim DOCUMENTO_ID As Object
        Dim NRO_LINEA As Object
        Dim DISPONIBLE As Object
        Dim DOC_TRANS_ID_EGR As Object
        Dim NRO_LINEA_TRANS_EGR As Object
        Dim DOC_TRANS_ID_TR As Object
        Dim NRO_LINEA_TRANS_TR As Object
        Dim CLIENTE_ID As Object
        Dim CAT_LOG_ID As Object
        Dim CAT_LOG_ID_FINAL As Object
        Dim Est_Merc_Id As Object
    End Structure

    Public Property objCMD() As SqlCommand
        Get
            Return Cmd
        End Get
        Set(ByVal value As SqlCommand)
            Cmd = value
        End Set
    End Property

    Public Property objConnection() As SqlConnection
        Get
            Return Cnx
        End Get
        Set(ByVal value As SqlConnection)
            Cnx = value
        End Set
    End Property


    Private Function OBTENER_POS_ANTERIORES_V320(ByVal P_DOC_TRANS_ID As Object, _
                                                ByVal p_nro_linea_trans As Object, _
                                                ByVal P_CAT_LOG_ID As Object, _
                                                ByVal p_Est_Merc_Id As Object, _
                                                ByRef pcur As DataSet) As Boolean
        Dim STR1 As String = ""
        Dim STR3 As String = ""
        Dim strsql As String = ""
        'Dim Cmd As SqlClient.SqlCommand
        Dim Da As SqlClient.SqlDataAdapter
        Try

            STR1 = "   SELECT DISTINCT RL.POSICION_ACTUAL " & vbNewLine
            STR1 = STR1 & " FROM RL_DET_DOC_TRANS_POSICION RL " & vbNewLine
            STR1 = STR1 & " WHERE 1<>0 " & vbNewLine
            STR1 = STR1 & "   AND RL.POSICION_ACTUAL IS NOT NULL" & vbNewLine
            STR1 = STR1 & "   AND RL.DOC_TRANS_ID =" & P_DOC_TRANS_ID & vbNewLine
            STR1 = STR1 & "   AND RL.NRO_LINEA_TRANS = " & p_nro_linea_trans & vbNewLine
            If IsNothing(P_CAT_LOG_ID) Then
                P_CAT_LOG_ID = "1"
                STR3 = STR3 & " AND RL.CAT_LOG_ID_FINAL IS NULL AND '1' = '" & P_CAT_LOG_ID & "'"
            Else
                STR3 = STR3 & " AND RL.CAT_LOG_ID_FINAL = '" & P_CAT_LOG_ID & "'"
            End If
            If IsNothing(p_Est_Merc_Id) Then
                p_Est_Merc_Id = "1"
                STR3 = STR3 & " AND RL.EST_MERC_ID IS NULL AND '1' = '" & p_Est_Merc_Id & "'"
            Else
                STR3 = STR3 & " AND RL.EST_MERC_ID = '" & p_Est_Merc_Id & "'"
            End If
            strsql = ""
            strsql = strsql & STR1 & STR3

            Da = New SqlDataAdapter(oCmd)
            oCmd.CommandText = strsql
            oCmd.CommandType = CommandType.Text
            Da.Fill(pcur, "Obtener_Pos_Anteriores")
            Return True
        Catch SQLEx As SqlException
            MsgBox("SQLExOBTENER_POS_ANTERIORES_V320: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("OBTENER_POS_ANTERIORES_V320: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Da = Nothing
        End Try
    End Function

    Private Function VER_POS_ANTERIORES_PROD_v320(ByVal P_DOC_TRANS_ID As Object, ByVal p_nro_linea_trans As Object, ByVal P_CAT_LOG_ID As Object, _
                                                 ByVal p_Est_Merc_Id As Object, ByRef pcur As DataSet) As Boolean
        'Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            Dim SQL1 As String = ""
            Dim SQL2 As String = ""
            Dim SQL3 As String = ""
            Dim xCAT As String = ""
            Dim xEST As String = ""
            '-- PARA LA UBICACION EN EL INGRESO, NECESITO SABER EL ESTADO DE LA MERCADERIA DESDE DET_DOCUMENTO.
            SQL1 = "      SELECT " & vbNewLine
            SQL1 = SQL1 & "             RL.DOC_TRANS_ID " & vbNewLine
            SQL1 = SQL1 & "            ,RL.NRO_LINEA_TRANS " & vbNewLine
            SQL1 = SQL1 & "            ,RL.POSICION_ANTERIOR " & vbNewLine
            SQL1 = SQL1 & "            ,RL.NAVE_ANTERIOR " & vbNewLine
            SQL1 = SQL1 & "            ,RL.POSICION_ACTUAL " & vbNewLine
            SQL1 = SQL1 & "            ,RL.NAVE_ACTUAL " & vbNewLine
            SQL1 = SQL1 & "            ,RL.CANTIDAD " & vbNewLine
            SQL1 = SQL1 & "            ,DDT.MOVIMIENTO_PENDIENTE " & vbNewLine
            SQL1 = SQL1 & "            ,RL.CLIENTE_ID " & vbNewLine
            SQL1 = SQL1 & "            ,RL.CAT_LOG_ID " & vbNewLine
            SQL1 = SQL1 & "            ,RL.CAT_LOG_ID_FINAL " & vbNewLine
            SQL1 = SQL1 & "            ,ISNULL(RL.EST_MERC_ID,DD.EST_MERC_ID) AS EST_MERC_ID " & vbNewLine
            SQL2 = "    FROM RL_DET_DOC_TRANS_POSICION RL " & vbNewLine
            SQL2 = SQL2 & "     INNER JOIN DET_DOCUMENTO_TRANSACCION DDT ON (RL.DOC_TRANS_ID = DDT.DOC_TRANS_ID AND RL.NRO_LINEA_TRANS = DDT.NRO_LINEA_TRANS) " & vbNewLine
            SQL2 = SQL2 & "              INNER JOIN DOCUMENTO_TRANSACCION     DT  ON (DT.DOC_TRANS_ID = DDT.DOC_TRANS_ID) " & vbNewLine
            SQL2 = SQL2 & "              INNER JOIN DET_DOCUMENTO             DD  ON (DD.DOCUMENTO_ID = DDT.DOCUMENTO_ID AND DD.NRO_LINEA = DDT.NRO_LINEA_DOC) " & vbNewLine
            SQL2 = SQL2 & "    WHERE 1<>0 " & vbNewLine
            SQL3 = " AND RL.DOC_TRANS_ID =" & P_DOC_TRANS_ID
            SQL3 = SQL3 & " AND RL.NRO_LINEA_TRANS =" & p_nro_linea_trans
            If IsNothing(P_CAT_LOG_ID) Then
                P_CAT_LOG_ID = "1"
                SQL3 = SQL3 & " AND RL.CAT_LOG_ID_FINAL IS NULL AND '1' = '" & P_CAT_LOG_ID & "'" & vbNewLine
            Else
                SQL3 = SQL3 & " AND RL.CAT_LOG_ID_FINAL ='" & P_CAT_LOG_ID & "'" & vbNewLine
            End If

            If IsNothing(p_Est_Merc_Id) Then
                p_Est_Merc_Id = "1"
                SQL3 = SQL3 & " AND RL.EST_MERC_ID IS NULL AND '1' = '" & p_Est_Merc_Id & "'" & vbNewLine
            Else
                SQL3 = SQL3 & "  AND RL.EST_MERC_ID ='" & p_Est_Merc_Id & "'" & vbNewLine
                xEST = p_Est_Merc_Id
            End If
            'If VerifyConnection(Cnx) Then
            'Cmd = Cnx.CreateCommand
            Da = New SqlDataAdapter(Cmd)
            Cmd.CommandText = SQL1 & SQL2 & SQL3
            Cmd.CommandType = CommandType.Text
            'Cmd.Connection = Cnx
            Da.Fill(pcur, "VerPosAnterioresProd")
            'Else
            'MsgBox(ErrorCon, MsgBoxStyle.OkOnly, ClsName)
            'Return False
            'End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("VER_POS_ANTERIORES_PROD_v320: " & SQLEx.Message, MsgBoxStyle.OkCancel, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("VER_POS_ANTERIORES_PROD_v320: " & ex.Message, MsgBoxStyle.OkCancel, ClsName)
            Return False
        Finally
            'Cmd = Nothing
            Da = Nothing
        End Try
    End Function

    Private Function BORRAR_POS_ASIGNADAS_V320(ByVal P_DOC_TRANS_ID As Object, ByVal p_nro_linea_trans As Object, _
                                              ByVal P_CAT_LOG_ID As Object, ByVal p_Est_Merc_Id As Object) As Boolean
        'Dim Cmd As SqlCommand
        Dim xSQL As String = ""
        Dim SQL1 As String = ""
        Dim SQL3 As String = ""
        Dim xCAT As String = ""
        Dim xEST As String = ""
        Try
            'If VerifyConnection(Cnx) Then
            '    Cmd = Cnx.CreateCommand
            'Cmd.Connection = Cnx
            xSQL = "  UPDATE HISTORICO_POS_OCUPADAS2 SET FECHA=Getdate(); "
            Cmd.CommandText = xSQL
            Cmd.CommandType = CommandType.Text
            Cmd.ExecuteNonQuery()
            SQL1 = "      DELETE RL_DET_DOC_TRANS_POSICION" & vbNewLine
            SQL1 = SQL1 & "      WHERE DOC_TRANS_ID =" & P_DOC_TRANS_ID & vbNewLine
            SQL1 = SQL1 & "         AND NRO_LINEA_TRANS =" & p_nro_linea_trans & vbNewLine
            SQL3 = ""
            If IsNothing(P_CAT_LOG_ID) Then
                P_CAT_LOG_ID = "1"
                SQL3 = SQL3 & " AND CAT_LOG_ID_FINAL IS NULL AND '1' ='" & P_CAT_LOG_ID & "'" & vbNewLine
            Else
                SQL3 = SQL3 & " AND CAT_LOG_ID_FINAL ='" & P_CAT_LOG_ID & "'" & vbNewLine
            End If
            If IsNothing(p_Est_Merc_Id) Then
                p_Est_Merc_Id = "1"
                SQL3 = SQL3 & "  AND EST_MERC_ID IS NULL AND '1' ='" & p_Est_Merc_Id & "'" & vbNewLine
            Else
                SQL3 = SQL3 & " AND EST_MERC_ID ='" & p_Est_Merc_Id & "'" & vbNewLine
            End If
            'bd.Execute(SQL1 & SQL3)
            Cmd.CommandText = SQL1 & SQL3
            Cmd.CommandType = CommandType.Text
            Cmd.ExecuteNonQuery()
            'Else
            'MsgBox(ErrorCon, MsgBoxStyle.OkOnly, ClsName)
            'End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("BORRAR_POS_ASIGNADAS_V320: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("BORRAR_POS_ASIGNADAS_V320: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
            'Finally
            'Cmd = Nothing
        End Try
    End Function

    Private Function Rl_InsertRecord(ByRef P_RL_ID As Object, ByVal P_DOC_TRANS_ID As Object, _
                                    ByVal p_nro_linea_trans As Object, ByVal P_POSICION_ANTERIOR As Object, _
                                    ByVal P_POSICION_ACTUAL As Object, ByVal P_CANTIDAD As Object, _
                                    ByVal P_TIPO_MOVIMIENTO_ID As Object, ByVal P_ULTIMA_ESTACION As Object, _
                                    ByVal P_ULTIMA_SECUENCIA As Object, ByVal P_NAVE_ANTERIOR As Object, _
                                    ByVal P_NAVE_ACTUAL As Object, ByVal P_DOCUMENTO_ID As Object, _
                                    ByVal P_NRO_LINEA As Object, ByVal P_DISPONIBLE As Object, _
                                    ByVal P_DOC_TRANS_ID_EGR As Object, ByVal P_NRO_LINEA_TRANS_EGR As Object, _
                                    ByVal P_DOC_TRANS_ID_TR As Object, ByVal P_NRO_LINEA_TRANS_TR As Object, _
                                    ByVal P_CLIENTE_ID As Object, ByVal P_CAT_LOG_ID As Object, _
                                    ByVal P_CAT_LOG_ID_FINAL As Object, ByVal p_Est_Merc_Id As Object) As Boolean
        Dim strsql As String
        'Dim Cmd As SqlCommand
        Try
            'If VerifyConnection(Cnx) Then
            'Cmd = Cnx.CreateCommand
            'Cmd.Connection = Cnx
            Cmd.CommandType = CommandType.Text
            strsql = ""
            strsql = strsql & "Insert Into RL_DET_DOC_TRANS_POSICION (" & vbNewLine
            strsql = strsql & "        DOC_TRANS_ID," & vbNewLine
            strsql = strsql & "        NRO_LINEA_TRANS," & vbNewLine
            strsql = strsql & "        POSICION_ANTERIOR," & vbNewLine
            strsql = strsql & "        POSICION_ACTUAL," & vbNewLine
            strsql = strsql & "        CANTIDAD," & vbNewLine
            strsql = strsql & "        TIPO_MOVIMIENTO_ID," & vbNewLine
            strsql = strsql & "        ULTIMA_ESTACION," & vbNewLine
            strsql = strsql & "        ULTIMA_SECUENCIA," & vbNewLine
            strsql = strsql & "        NAVE_ANTERIOR," & vbNewLine
            strsql = strsql & "        NAVE_ACTUAL," & vbNewLine
            strsql = strsql & "        DOCUMENTO_ID," & vbNewLine
            strsql = strsql & "        NRO_LINEA," & vbNewLine
            strsql = strsql & "        DISPONIBLE," & vbNewLine
            strsql = strsql & "        DOC_TRANS_ID_EGR," & vbNewLine
            strsql = strsql & "        NRO_LINEA_TRANS_EGR," & vbNewLine
            strsql = strsql & "        DOC_TRANS_ID_TR," & vbNewLine
            strsql = strsql & "        NRO_LINEA_TRANS_TR," & vbNewLine
            strsql = strsql & "        CLIENTE_ID," & vbNewLine
            strsql = strsql & "        CAT_LOG_ID," & vbNewLine
            strsql = strsql & "        CAT_LOG_ID_FINAL," & vbNewLine
            strsql = strsql & "        EST_MERC_ID)" & vbNewLine
            strsql = strsql & " Values (" & vbNewLine
            strsql = strsql & "        " & IIf(IsDBNull(P_DOC_TRANS_ID) Or IsNothing(P_DOC_TRANS_ID), "Null", P_DOC_TRANS_ID) & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_NRO_LINEA) Or IsNothing(p_nro_linea_trans), "Null", p_nro_linea_trans) & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_POSICION_ANTERIOR) Or IsNothing(P_POSICION_ANTERIOR), "Null", P_POSICION_ANTERIOR) & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_POSICION_ACTUAL) Or IsNothing(P_POSICION_ACTUAL), "Null", P_POSICION_ACTUAL) & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_CANTIDAD) Or IsNothing(P_CANTIDAD), "Null", CDbl(P_CANTIDAD)) & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_TIPO_MOVIMIENTO_ID) Or IsNothing(P_TIPO_MOVIMIENTO_ID) Or P_TIPO_MOVIMIENTO_ID = "", "Null", "'" & UCase(Trim(P_TIPO_MOVIMIENTO_ID)) & "'") & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_ULTIMA_ESTACION) Or IsNothing(P_ULTIMA_ESTACION) Or P_ULTIMA_ESTACION = "", "Null", "'" & UCase(Trim(P_ULTIMA_ESTACION)) & "'") & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_ULTIMA_SECUENCIA) Or IsNothing(P_ULTIMA_SECUENCIA), "Null", P_ULTIMA_SECUENCIA) & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_NAVE_ANTERIOR) Or IsNothing(P_NAVE_ANTERIOR), "Null", P_NAVE_ANTERIOR) & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_NAVE_ACTUAL) Or IsNothing(P_NAVE_ACTUAL) Or P_NAVE_ACTUAL = 0, "Null", P_NAVE_ACTUAL) & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_DOCUMENTO_ID) Or IsNothing(P_DOCUMENTO_ID), "Null", P_DOCUMENTO_ID) & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_NRO_LINEA) Or IsNothing(P_NRO_LINEA), "Null", P_NRO_LINEA) & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_DISPONIBLE) Or IsNothing(P_DISPONIBLE), "Null", "'" & P_DISPONIBLE & "'") & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_DOC_TRANS_ID_EGR) Or IsNothing(P_DOC_TRANS_ID_EGR), "Null", P_DOC_TRANS_ID_EGR) & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_NRO_LINEA_TRANS_EGR) Or IsNothing(P_NRO_LINEA_TRANS_EGR), "Null", P_NRO_LINEA_TRANS_EGR) & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_DOC_TRANS_ID_TR) Or IsNothing(P_DOC_TRANS_ID_TR), "Null", P_DOC_TRANS_ID_TR) & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_NRO_LINEA_TRANS_TR) Or IsNothing(P_NRO_LINEA_TRANS_TR), "Null", P_NRO_LINEA_TRANS_TR) & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_CLIENTE_ID) Or IsNothing(P_CLIENTE_ID) Or UCase(Trim(P_CLIENTE_ID)) = "", "Null", "'" & UCase(Trim(P_CLIENTE_ID)) & "'") & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_CAT_LOG_ID) Or IsNothing(P_CAT_LOG_ID) Or UCase(Trim(P_CAT_LOG_ID)) = "", "Null", "'" & UCase(Trim(P_CAT_LOG_ID)) & "'") & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(P_CAT_LOG_ID_FINAL) Or IsNothing(P_CAT_LOG_ID_FINAL) Or UCase(Trim(P_CAT_LOG_ID_FINAL)) = "", "Null", "'" & UCase(Trim(P_CAT_LOG_ID_FINAL)) & "'") & vbNewLine
            strsql = strsql & "        ," & IIf(IsDBNull(p_Est_Merc_Id) Or IsNothing(p_Est_Merc_Id) Or UCase(Trim(p_Est_Merc_Id)) = "", "Null", "'" & UCase(Trim(p_Est_Merc_Id)) & "'") & vbNewLine
            strsql = strsql & ")"
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()
            'revisar si hace falta agregar la llamada a funciones generales para obtener el id.
            'bd.Execute(strsql)
            'Else
            'MsgBox(ErrorCon, MsgBoxStyle.OkOnly, ClsName)
            'Return False
            'End If
            Return True
        Catch SQLEx As SqlException

            MsgBox("Rl_InsertRecord: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception

            MsgBox("Rl_InsertRecord: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Private Function Actualizar_Estado_Ubicac_Item(ByVal P_DOC_TRANS_ID As Object, ByVal p_nro_linea_trans As Object) As Boolean
        Dim total As Integer
        Dim txt As String = ""
        Dim aux_where As String = ""
        Dim aux_where1 As String = ""
        Dim xSQL As String = ""
        Dim DsTmp As New DataSet
        'Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            'Dim funciones_movimiento_api As New funciones_movimiento_api
            'funciones_movimiento_api.Conexion = bd
            'If VerifyConnection(Cnx) Then
            '    Cmd = Cnx.CreateCommand
            Da = New SqlDataAdapter(Cmd)

            xSQL = " UPDATE det_documento_transaccion" & vbNewLine
            xSQL = xSQL & " SET item_ok = '1' , movimiento_pendiente = '1' "
            xSQL = xSQL & " Where doc_trans_id =" & P_DOC_TRANS_ID & vbNewLine
            xSQL = xSQL & " AND nro_linea_trans =" & p_nro_linea_trans & vbNewLine
            xSQL = xSQL & " AND 0 <> (SELECT Count(RL_ID)AS PREINGRESO " & vbNewLine
            xSQL = xSQL & "           FROM RL_dET_DOC_TRANS_POSICION RL " & vbNewLine
            xSQL = xSQL & "           Where RL.doc_trans_id =" & P_DOC_TRANS_ID & vbNewLine
            xSQL = xSQL & "                 AND RL.NRO_LINEA_TRANS =" & p_nro_linea_trans & vbNewLine
            xSQL = xSQL & "                 AND NAVE_ACTUAL = (SELECT NAVE_ID FROM NAVE WHERE PRE_INGRESO = '1')); " & vbNewLine
            'bd.Execute(xSQL)
            Cmd.CommandText = xSQL
            Cmd.Connection = Cnx
            Cmd.CommandType = CommandType.Text
            Cmd.ExecuteNonQuery()

            xSQL = " SELECT ISNULL(Count(ddt.item_ok),0) as total"
            xSQL = xSQL & " FROM det_documento_transaccion ddt "
            xSQL = xSQL & " WHERE ddt.doc_trans_id =" & P_DOC_TRANS_ID & " and ddt.item_ok =0 "

            Cmd.CommandText = xSQL
            Cmd.CommandType = CommandType.Text
            Da.Fill(DsTmp, "Count")
            total = CInt(DsTmp.Tables("Count").Rows(0)(0))
            'Rstmp = bd.Execute(xSQL)
            'total = CInt(Rstmp!total)
            If (total = 0) Then
                Tareas_Movimientos(P_DOC_TRANS_ID)
            End If
            'Else
            'MsgBox(ErrorCon, MsgBoxStyle.OkOnly, ClsName)
            'Return False
            'End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("Actualizar_Estado_Ubicac_Item: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Actualizar_Estado_Ubicac_Item: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            DsTmp = Nothing
            'Cmd = Nothing
            Da = Nothing
        End Try
    End Function
    Public Function Tareas_Movimientos(ByVal P_DOC_TRANS_ID As Object) As Boolean
        Dim strsql As String
        Dim v_secuencia_realizada As Integer
        Dim Ds As New DataSet
        'Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Valor As Integer
        Dim i As Integer
        'Dim pcur As ADODB.Recordset
        'pcur = New ADODB.Recordset
        'Dim pcur1 As ADODB.Recordset
        'pcur1 = New ADODB.Recordset
        'Dim rs As ADODB.Recordset
        'rs = New ADODB.Recordset
        Try
            'If VerifyConnection(Cnx) Then
            'Cmd = Cnx.CreateCommand()
            Da = New SqlDataAdapter(Cmd)
            Tareas_Pre_Movimiento(P_DOC_TRANS_ID)
            strsql = ""
            strsql = strsql & "SELECT est_mov_actual From documento_transaccion  Where doc_trans_id = " & P_DOC_TRANS_ID
            Cmd.CommandText = strsql
            Cmd.CommandType = CommandType.Text
            '****
            'Cmd.Connection = Cnx
            Da.Fill(Ds, "pcur")
            strsql = ""
            strsql = strsql & "SELECT valor From sys_parametro_proceso  WHERE proceso_id='WARP' AND subproceso_id='MOVIMIENTOS' AND parametro_id='MOVIM_AUTOM' "
            Cmd.CommandText = strsql
            Da.Fill(Ds, "pcur1")
            Valor = CInt(Ds.Tables("pcur1").Rows(0)(0))
            If CInt(Valor) = 1 Then
                Ver_Documento_Movim_Pendientes(P_DOC_TRANS_ID, Ds.Tables("pcur").Rows(0)(0), Ds)
                For i = 0 To Ds.Tables("VerDocMovPen").Rows.Count - 1
                    v_secuencia_realizada = 1
                    Realizar_Movimiento(Ds.Tables("VerDocMovPen").Rows(0)("rl_id"), v_secuencia_realizada)
                Next
                Tareas_Post_Movimiento(P_DOC_TRANS_ID)
            End If
            'Else
            'MsgBox(ErrorCon, MsgBoxStyle.OkOnly, ClsName)
            'Return False
            'End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Da = Nothing
            'Cmd = Nothing
            Ds = Nothing
        End Try
    End Function


    Private Function Tareas_Pre_Movimiento(ByVal P_DOC_TRANS_ID As Object) As Boolean
        Dim strsql As String, aux_where As String = ""
        Dim tipo_movimiento As String = ""
        Dim Ds As New DataSet
        'Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim i As Integer
        Try
            'If VerifyConnection(Cnx) Then
            '    Cmd = Cnx.CreateCommand
            '    Cmd.Connection = Cnx
            Da = New SqlDataAdapter(Cmd)
            'Else
            'MsgBox(ErrorCon, MsgBoxStyle.OkOnly, ClsName)
            'Return False
            'End If
            strsql = ""
            strsql = strsql & "SELECT tipo_operacion_id FROM documento_transaccion WHERE doc_trans_id = " & P_DOC_TRANS_ID
            Cmd.CommandText = strsql
            Cmd.CommandType = CommandType.Text
            Da.Fill(Ds, "pcur")

            Select Case CStr(Ds.Tables("pcur").Rows(0)(0).ToString) '!tipo_operacion_id)
                Case "ING"
                    aux_where = " WHERE rl.doc_trans_id = " & P_DOC_TRANS_ID
                Case "EGR"
                    aux_where = " WHERE rl.doc_trans_id_egr = " & P_DOC_TRANS_ID
                Case "TR"
                    aux_where = " WHERE rl.doc_trans_id_tr =  " & P_DOC_TRANS_ID
            End Select

            strsql = ""
            strsql = strsql & " SELECT rl_id , nave_anterior, posicion_anterior, posicion_actual, nave_actual" & vbNewLine
            strsql = strsql & " FROM  RL_DET_DOC_TRANS_POSICION rl"

            strsql = strsql & aux_where
            Cmd.CommandText = strsql
            Da.Fill(Ds, "Pcur1")
            For i = 0 To Ds.Tables("Pcur1").Rows.Count - 1
                strsql = ""
                strsql = strsql & "Update RL_DET_DOC_TRANS_POSICION SET tipo_movimiento_id = '1',ultima_estacion = 'A',ultima_secuencia = Null" & vbNewLine
                strsql = strsql & "Where rl_id =" & Ds.Tables("Pcur1").Rows(i)(0) 'pcur1!rl_id
                Cmd.CommandText = strsql
                Cmd.ExecuteNonQuery()
            Next

            strsql = ""
            strsql = strsql & "Update documento_transaccion" & vbNewLine
            strsql = strsql & "SET EST_MOV_ACTUAL = 'A'" & vbNewLine
            strsql = strsql & "WHERE doc_trans_id =" & P_DOC_TRANS_ID
            'pcur3 = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()

            Call Actualizar_Reglas_Pendientes(P_DOC_TRANS_ID, "RM_1", 0)
            Call Actualizar_Reglas_Pendientes(P_DOC_TRANS_ID, "RU_1", 1)

            Return False
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Ds = Nothing
            'Cmd = Nothing
            Da = Nothing
        End Try
    End Function

    Public Function Actualizar_Reglas_Pendientes(ByVal P_DOC_TRANS_ID As Object, _
                                                 ByVal pregla As Object, _
                                                 ByVal opcion As Object) As Boolean
        Dim strsql As String
        Dim ID As Long
        Dim Ds As New DataSet
        'Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            'Cmd = Cnx.CreateCommand
            Da = New SqlDataAdapter(Cmd)
            'Cmd.Connection = Cnx
            Cmd.CommandType = CommandType.Text
            If (pregla = "RM_1") Then
                If (opcion = 1) Then
                    strsql = ""
                    strsql = strsql & "Delete PENDIENTE_DOC_TRANS" & vbNewLine
                    strsql = strsql & "Where doc_trans_id =" & P_DOC_TRANS_ID & vbNewLine
                    strsql = strsql & "AND regla_id = 'RM_1'"
                    Cmd.CommandText = strsql
                    Cmd.ExecuteNonQuery()
                Else
                    strsql = ""
                    strsql = strsql & "SELECT isnull(Count(regla_id),0) as total" & vbNewLine
                    strsql = strsql & "From PENDIENTE_DOC_TRANS" & vbNewLine
                    strsql = strsql & "Where doc_trans_id =" & P_DOC_TRANS_ID & vbNewLine
                    strsql = strsql & "AND regla_id = 'RM_1'"
                    Cmd.CommandText = strsql
                    Da.Fill(Ds, "pcur1")
                    If (CInt(Ds.Tables("pcur1").Rows(0)(0)) = 0) Then
                        Call PendienteDocTransInsertRecord(ID, P_DOC_TRANS_ID, "MOV", "RM_1", 1)
                    End If
                End If
            Else
                If (opcion = 1) Then
                    strsql = ""
                    strsql = strsql & "DELETE PENDIENTE_DOC_TRANS" & vbNewLine
                    strsql = strsql & "WHERE doc_trans_id =" & P_DOC_TRANS_ID & vbNewLine
                    strsql = strsql & "AND regla_id = 'RU_1'"
                    Cmd.CommandText = strsql
                    Cmd.ExecuteNonQuery()
                End If
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Da = Nothing
            Ds = Nothing
            'Cmd = Nothing
        End Try
    End Function

    Private Function PendienteDocTransInsertRecord(ByVal p_id As Object, ByVal P_DOC_TRANS_ID As Object, _
                                                    ByVal P_TIPO_REGLA As Object, ByVal P_REGLA_ID As Object, _
                                                    ByVal P_NRO_LINEA As Object) As Boolean
        Dim strsql As String
        'Dim Cmd As SqlCommand
        'Cmd = Cnx.CreateCommand
        'Cmd.Connection = Cnx
        Cmd.CommandType = CommandType.Text
        Try
            strsql = ""
            strsql = strsql & "Insert Into PENDIENTE_DOC_TRANS(" & vbNewLine
            strsql = strsql & "        DOC_TRANS_ID," & vbNewLine
            strsql = strsql & "        TIPO_REGLA," & vbNewLine
            strsql = strsql & "        REGLA_ID," & vbNewLine
            strsql = strsql & "        NRO_LINEA)" & vbNewLine
            strsql = strsql & " Values (" & vbNewLine
            strsql = strsql & "        " & IIf(UCase(Trim(P_DOC_TRANS_ID)) = "" Or IsNothing(P_DOC_TRANS_ID), "Null", UCase(Trim(P_DOC_TRANS_ID))) & vbNewLine
            strsql = strsql & "        ," & IIf(UCase(Trim(P_TIPO_REGLA)) = "" Or IsNothing(P_TIPO_REGLA), "Null", "'" & UCase(Trim(P_TIPO_REGLA)) & "'") & vbNewLine
            strsql = strsql & "        ," & IIf(UCase(Trim(P_REGLA_ID)) = "" Or IsNothing(P_REGLA_ID), "Null", "'" & UCase(Trim(P_REGLA_ID)) & "'") & vbNewLine
            strsql = strsql & "        ," & IIf(UCase(Trim(P_NRO_LINEA)) = "" Or IsNothing(P_NRO_LINEA), "Null", UCase(Trim(P_NRO_LINEA))) & vbNewLine
            strsql = strsql & "     )"
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()
            p_id = OBTENER_SECUENCIA()
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Private Function OBTENER_SECUENCIA() As Object
        Dim strsql As String
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        'Dim Cmd As SqlCommand
        Try
            'Cmd = Cnx.CreateCommand
            Da = New SqlDataAdapter(Cmd)
            'Cmd.Connection = Cnx
            Cmd.CommandType = CommandType.Text
            strsql = ""
            strsql = strsql & "SELECT SCOPE_IDENTITY()" & vbNewLine
            Cmd.CommandText = strsql
            Da.Fill(Ds, "Secuencia")
            Return Ds.Tables("Secuencia").Rows(0)(0)
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return Nothing
        Finally
            Ds = Nothing
            Da = Nothing
            'Cmd = Nothing
        End Try
    End Function
    Private Function Ver_Documento_Movim_Pendientes(ByVal P_DOC_TRANS_ID As Object, _
                                                   ByVal P_ESTACION_ACTUAL As Object, _
                                                   ByRef pcur As DataSet) As Boolean

        Dim strsql As String, aux_where As String = ""
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        'Dim Cmd As SqlCommand
        Try
            'Cmd = Cnx.CreateCommand
            Da = New SqlDataAdapter(Cmd)
            Cmd.CommandType = CommandType.Text
            'Cmd.Connection = Cnx
            'Dim rs As ADODB.Recordset
            'rs = New ADODB.Recordset
            strsql = ""
            strsql = strsql & "SELECT tipo_operacion_id FROM documento_transaccion WHERE doc_trans_id = " & P_DOC_TRANS_ID
            'rs = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Da.Fill(Ds, "RS")

            Select Case CStr(Ds.Tables("RS").Rows(0)(0)) '!tipo_operacion_id)
                Case "ING"
                    aux_where = " rl.doc_trans_id = " & P_DOC_TRANS_ID
                Case "EGR"
                    aux_where = " rl.doc_trans_id_egr = " & P_DOC_TRANS_ID
                Case "TR"
                    aux_where = " rl.doc_trans_id_tr =  " & P_DOC_TRANS_ID
            End Select

            strsql = ""
            strsql = strsql & "SELECT DISTINCT" & vbNewLine
            strsql = strsql & "       0 as Realizar," & vbNewLine
            strsql = strsql & "       secuencia Secuencia," & vbNewLine
            strsql = strsql & "       Case Origen" & vbNewLine
            strsql = strsql & "            WHEN 'ORIGEN' THEN ISNULL(ISNULL(p1.posicion_cod, n1.nave_cod), 'PRE-INGRESO')" & vbNewLine
            strsql = strsql & "            Else" & vbNewLine
            strsql = strsql & "               Case Origen" & vbNewLine
            strsql = strsql & "                    WHEN 'DESTINO' THEN ISNULL(p2.posicion_cod, n2.nave_cod)" & vbNewLine
            strsql = strsql & "                    Else: Origen" & vbNewLine
            strsql = strsql & "                End" & vbNewLine
            strsql = strsql & "       END as Origen," & vbNewLine
            strsql = strsql & "       Case Destino" & vbNewLine
            strsql = strsql & "            WHEN 'ORIGEN' THEN ISNULL(ISNULL(p1.posicion_cod, n1.nave_cod), 'PRE-INGRESO')" & vbNewLine
            strsql = strsql & "            Else" & vbNewLine
            strsql = strsql & "                Case Destino" & vbNewLine
            strsql = strsql & "                     WHEN 'DESTINO' THEN ISNULL(p2.posicion_cod, n2.nave_cod)" & vbNewLine
            strsql = strsql & "                     Else: Destino" & vbNewLine
            strsql = strsql & "                End" & vbNewLine
            strsql = strsql & "       END as Destino," & vbNewLine
            strsql = strsql & "       estacion," & vbNewLine
            strsql = strsql & "       dd.cliente_id AS  Cliente," & vbNewLine
            strsql = strsql & "       dd.producto_id  AS Producto," & vbNewLine
            strsql = strsql & "       rl.cantidad AS Cantidad," & vbNewLine
            strsql = strsql & "       dd.fecha_vencimiento as Fecha_Vto," & vbNewLine
            strsql = strsql & "       dd.nro_lote as Nro_Lote," & vbNewLine
            strsql = strsql & "       dd.nro_serie as Nro_Serie," & vbNewLine
            strsql = strsql & "       dd.nro_partida as Nro_Partida," & vbNewLine
            strsql = strsql & "       dd.nro_despacho as Nro_Despacho," & vbNewLine
            strsql = strsql & "       dd.prop1 as Property_1," & vbNewLine
            strsql = strsql & "       dd.prop2 as Property_2," & vbNewLine
            strsql = strsql & "       dd.prop3 as Property_3," & vbNewLine
            strsql = strsql & "       rl.rl_id," & vbNewLine
            strsql = strsql & "       ISNULL(ddt2.documento_id, dd.documento_id) as documento_id," & vbNewLine
            strsql = strsql & "       ISNULL(ddt2.nro_linea_doc, dd.nro_linea)as nro_linea ," & vbNewLine
            strsql = strsql & "       dd.peso," & vbNewLine
            strsql = strsql & "       dd.volumen," & vbNewLine
            strsql = strsql & "       dd.unidad_id," & vbNewLine
            strsql = strsql & "       dd.unidad_peso," & vbNewLine
            strsql = strsql & "       dd.unidad_volumen," & vbNewLine
            strsql = strsql & "       dd.est_merc_id," & vbNewLine
            strsql = strsql & "       dd.moneda_id," & vbNewLine
            strsql = strsql & "       dd.costo" & vbNewLine
            strsql = strsql & " FROM RL_DET_DOC_TRANS_POSICION rl" & vbNewLine
            strsql = strsql & "      LEFT OUTER JOIN  posicion p2  ON  rl.posicion_actual  = p2.posicion_id" & vbNewLine
            strsql = strsql & "      LEFT OUTER JOIN  nave n2  ON  rl.nave_actual  = n2.nave_id" & vbNewLine
            strsql = strsql & "      LEFT OUTER JOIN  posicion p1  ON  rl.posicion_anterior  = p1.posicion_id" & vbNewLine
            strsql = strsql & "      LEFT OUTER JOIN  nave n1  ON  rl.nave_anterior  = n1.nave_id" & vbNewLine

            Select Case Ds.Tables("RS").Rows(0)(0)
                Case "ING"
                    strsql = strsql & "      LEFT OUTER JOIN det_documento_transaccion ddt2 ON rl.doc_trans_id_egr = ddt2.doc_trans_id  AND rl.nro_linea_trans_egr = ddt2.nro_linea_trans," & vbNewLine
                Case "EGR"
                    strsql = strsql & "      LEFT OUTER JOIN det_documento_transaccion ddt2 ON rl.doc_trans_id_egr = ddt2.doc_trans_id  AND rl.nro_linea_trans_egr = ddt2.nro_linea_trans," & vbNewLine
                Case "TR"
                    strsql = strsql & "      LEFT OUTER JOIN det_documento_transaccion ddt2 ON rl.doc_trans_id_tr = ddt2.doc_trans_id  AND rl.nro_linea_trans_tr = ddt2.nro_linea_trans," & vbNewLine
            End Select

            strsql = strsql & "      det_tipo_movimiento dtm," & vbNewLine
            strsql = strsql & "      det_documento_transaccion ddt," & vbNewLine
            strsql = strsql & "      det_documento dd" & vbNewLine
            strsql = strsql & "Where dtm.tipo_movimiento_id = rl.tipo_movimiento_id" & vbNewLine
            strsql = strsql & "  AND    ddt.doc_trans_id  = rl.doc_trans_id" & vbNewLine
            strsql = strsql & "  AND    ddt.nro_linea_trans  = rl.nro_linea_trans" & vbNewLine
            strsql = strsql & "  AND    dd.documento_id  = ddt.documento_id" & vbNewLine
            strsql = strsql & "  AND    dd.nro_linea  = ddt.nro_linea_doc" & vbNewLine
            strsql = strsql & "  AND    rl.doc_trans_id  = ddt.doc_trans_id" & vbNewLine
            strsql = strsql & "  AND    rl.ultima_estacion  = dtm.estacion" & vbNewLine
            strsql = strsql & "  AND    ((ultima_secuencia  IS NOT NULL" & vbNewLine
            strsql = strsql & "  AND    ultima_secuencia  < secuencia)" & vbNewLine
            strsql = strsql & "  OR (ultima_secuencia  IS NULL))" & vbNewLine
            strsql = strsql & "  AND    rl.ultima_estacion  = '" & P_ESTACION_ACTUAL & "'" & vbNewLine
            strsql = strsql & "  AND " & aux_where & vbNewLine
            strsql = strsql & "ORDER BY 13,14,rl.rl_id,secuencia"
            Cmd.CommandText = strsql
            Da.Fill(pcur, "VerDocMovPen")
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Ds = Nothing
            Da = Nothing
            'Cmd = Nothing
        End Try
    End Function

    Private Sub Realizar_Movimiento(ByVal p_rl_pos_id As Object, _
                                   ByVal secuencia_realizada As Object)

        Dim strsql As String, aux_where As String = ""
        Dim Ds As New DataSet
        'Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        'Dim pcur As ADODB.Recordset
        'pcur = New ADODB.Recordset
        'Dim pcur1 As ADODB.Recordset
        'pcur1 = New ADODB.Recordset
        'Dim pcur2 As ADODB.Recordset
        'pcur2 = New ADODB.Recordset
        'Dim pcur3 As ADODB.Recordset
        'pcur3 = New ADODB.Recordset
        'Dim pcur4 As ADODB.Recordset
        'pcur4 = New ADODB.Recordset
        'Dim pcur5 As ADODB.Recordset
        'pcur5 = New ADODB.Recordset
        'Dim rs As ADODB.Recordset
        'rs = New ADODB.Recordset
        'Dim rs1 As ADODB.Recordset
        'rs1 = New ADODB.Recordset
        Try
            'Cmd = Cnx.CreateCommand
            Da = New SqlDataAdapter(Cmd)
            'Cmd.Connection = Cnx
            Cmd.CommandType = CommandType.Text

            strsql = ""
            strsql = strsql & "SELECT  dt.doc_trans_id,rl.nro_linea_trans,dt.tipo_operacion_id" & vbNewLine
            strsql = strsql & "FROM RL_DET_DOC_TRANS_POSICION rl,documento_transaccion dt" & vbNewLine
            strsql = strsql & " WHERE rl_id = " & p_rl_pos_id & vbNewLine
            strsql = strsql & "       and isnull(isnull(rl.doc_trans_id_egr,rl.doc_trans_id_tr),rl.doc_trans_id)=dt.doc_trans_id"
            'pcur = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Da.Fill(Ds, "pcur")

            Select Case CStr(Ds.Tables("pcur").Rows(0)("tipo_operacion_id")) 'pcur!tipo_operacion_id)
                Case "ING"
                    aux_where = "and rl.doc_trans_id = " & Ds.Tables("pcur").Rows(0)("doc_trans_id") 'pcur!doc_trans_id
                Case "EGR"
                    aux_where = "and rl.doc_trans_id_egr = " & Ds.Tables("pcur").Rows(0)("doc_trans_id") 'pcur!doc_trans_id
                Case "TR"
                    aux_where = "and rl.doc_trans_id_tr =  " & Ds.Tables("pcur").Rows(0)("doc_trans_id") 'pcur!doc_trans_id
            End Select

            strsql = ""
            strsql = strsql & "Update RL_DET_DOC_TRANS_POSICION" & vbNewLine
            strsql = strsql & "Set ultima_secuencia =" & secuencia_realizada & vbNewLine
            strsql = strsql & "WHERE rl_id =" & p_rl_pos_id
            'pcur1 = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()

            strsql = ""
            strsql = strsql & "Update det_documento_transaccion" & vbNewLine
            strsql = strsql & "SET movimiento_pendiente='1'" & vbNewLine
            strsql = strsql & "Where doc_trans_id =" & Ds.Tables("pcur").Rows(0)("doc_trans_id") 'pcur!doc_trans_id & vbNewLine
            strsql = strsql & "      AND nro_linea_trans=" & Ds.Tables("pcur").Rows(0)("nro_linea_trans") 'pcur!nro_linea_trans
            'pcur1 = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()

            strsql = ""
            strsql = strsql & "Update documento_transaccion" & vbNewLine
            strsql = strsql & "Set it_mover = 1" & vbNewLine
            strsql = strsql & "WHERE doc_trans_id= " & Ds.Tables("pcur").Rows(0)("doc_trans_id") 'pcur!doc_trans_id
            'pcur1 = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()

            '**************************************************
            '**  Libero el lockeo sobre la posiciones origen y destino (si el origen es una posicion y el destino es una posicion)
            '**  Si el origen quedo con cantidad 0 => la posicion queda vacia
            '**  Ojo antes de deslockear la posicion chequeo que el que la haya lockeado sea yo, y no el inventario....
            '**  se supone que solamente la posicion puede haberse lockeado por este documento con esta operacion
            '**  o por un inventario
            '**************************************************
            strsql = ""
            strsql = strsql & "SELECT posicion_actual ,posicion_anterior , tipo_movimiento_id" & vbNewLine
            strsql = strsql & " From RL_DET_DOC_TRANS_POSICION" & vbNewLine
            strsql = strsql & " Where rl_id =" & p_rl_pos_id
            Cmd.CommandText = strsql
            Da.Fill(Ds, "pcur2")
            'pcur2 = bd.Execute(strsql)

            If Ds.Tables("pcur2").Rows.Count > 0 Then
                '**************************************************
                '** determino la secuencia maxima de movimientos a realizar de acuerdo al tipo
                '** de movimiento previamente determinado.
                '**************************************************
                strsql = ""
                strsql = strsql & "SELECT Max(secuencia) as max_secuencia FROM det_tipo_movimiento" & vbNewLine
                If Not IsNothing(Ds.Tables("pcur2").Rows(0)("tipo_movimiento_id")) Then
                    strsql = strsql & "Where tipo_movimiento_id =" & Ds.Tables("pcur2").Rows(0)("tipo_movimiento_id") 'pcur2!tipo_movimiento_id
                Else
                    strsql = strsql & "Where tipo_movimiento_id =null"
                End If
                'pcur3 = bd.Execute(strsql)
                Cmd.CommandText = strsql
                Da.Fill(Ds, "pcur3")

                If Not IsNothing(Ds.Tables("pcur2").Rows(0)("posicion_actual")) Then 'pcur2!posicion_actual) Then
                    strsql = ""
                    strsql = strsql & "SELECT Count(rl_id) as total" & vbNewLine
                    strsql = strsql & "FROM rl_det_doc_trans_posicion rl" & vbNewLine
                    strsql = strsql & "Where posicion_actual = " & Ds.Tables("pcur2").Rows(0)("posicion_actual") & vbNewLine 'pcur2!posicion_actual & vbNewLine
                    strsql = strsql & "      and ultima_secuencia IS NULL " & aux_where
                    'pcur4 = bd.Execute(strsql)
                    Cmd.CommandText = strsql
                    Da.Fill(Ds, "pcur4")

                    If Ds.Tables("pcur4").Rows(0)("total") = 0 Then 'pcur4!total = 0 Then
                        '**************************************************
                        '**  ESTA FUNCION DESMARCA LAS POSICIONES UNICAMENTE SI LA POSICION ESTA LOCKEADA
                        '**  (actualmente y para las transferencias multiples es una traba)
                        '**************************************************

                        strsql = ""
                        strsql = strsql & "SELECT pos_lockeada as ppos_lock,lck_tipo_operacion as plock_tipo_op,lck_doc_trans_id as  plock_doc_trans_id" & vbNewLine
                        strsql = strsql & "From posicion" & vbNewLine
                        strsql = strsql & "WHERE posicion_id =" & Ds.Tables("pcur2").Rows(0)("posicion_actual")
                        'pcur5 = bd.Execute(strsql)
                        Cmd.CommandText = strsql
                        Da.Fill(Ds, "pcur5")
                        Try
                            If (Ds.Tables("pcur5").Rows(0)("plock_tipo_op") = Ds.Tables("pcur").Rows(0)("tipo_operacion_id") _
                                And Ds.Tables("pcur5").Rows(0)("plock_doc_trans_id") = Ds.Tables("pcur").Rows(0)("doc_trans_id") _
                                And Ds.Tables("pcur3").Rows(0)("max_secuencia") = secuencia_realizada) Then
                                strsql = ""
                                strsql = strsql & "Update posicion" & vbNewLine
                                strsql = strsql & " SET pos_vacia=0," & vbNewLine
                                strsql = strsql & "     pos_lockeada=0," & vbNewLine
                                strsql = strsql & "     LCK_TIPO_OPERACION=NULL," & vbNewLine
                                strsql = strsql & "     LCK_USUARIO_ID=NULL," & vbNewLine
                                strsql = strsql & "     LCK_DOC_TRANS_ID=NULL," & vbNewLine
                                strsql = strsql & "     LCK_OBS = Null" & vbNewLine
                                strsql = strsql & " WHERE posicion_id = " & Ds.Tables("pcur2").Rows(0)("posicion_actual")
                                'rs = bd.Execute(strsql)
                                Cmd.CommandText = strsql
                                Cmd.ExecuteNonQuery()
                            End If
                        Catch ex As Exception
                        End Try
                    End If
                End If

                If Not IsNothing(Ds.Tables("pcur2").Rows(0)("posicion_anterior")) And Not IsDBNull(Ds.Tables("pcur2").Rows(0)("posicion_anterior")) Then
                    strsql = ""
                    strsql = strsql & "SELECT Count(rl_id) as total" & vbNewLine
                    strsql = strsql & "FROM rl_det_doc_trans_posicion rl" & vbNewLine
                    strsql = strsql & "Where posicion_actual = " & Ds.Tables("pcur2").Rows(0)("posicion_anterior") & vbNewLine
                    strsql = strsql & "      and ultima_secuencia IS NULL " & aux_where
                    'pcur4 = bd.Execute(strsql)
                    Cmd.CommandText = strsql
                    Da.Fill(Ds, "pcur41")
                    If Ds.Tables("pcur41").Rows(0)("total") = 0 Then
                        '**************************************************
                        '**  ESTA FUNCION DESMARCA LAS POSICIONES UNICAMENTE SI LA POSICION ESTA LOCKEADA
                        '**  (actualmente y para las transferencias multiples es una traba)
                        '**************************************************

                        strsql = ""
                        strsql = strsql & "SELECT pos_lockeada as ppos_lock, lck_tipo_operacion as plock_tipo_op,lck_doc_trans_id as plock_doc_trans_id,pos_vacia" & vbNewLine
                        strsql = strsql & "From posicion" & vbNewLine
                        If Not IsNothing(Ds.Tables("pcur2").Rows(0)("posicion_anterior")) Then
                            strsql = strsql & "WHERE posicion_id =" & Ds.Tables("pcur2").Rows(0)("posicion_anterior")
                        Else
                            strsql = strsql & "WHERE posicion_id = null"
                        End If

                        If Ds.Tables("pcur5") IsNot Nothing Then
                            Ds.Tables.Remove("pcur5")
                        End If
                        Cmd.CommandText = strsql
                        Da.Fill(Ds, "pcur5")
                        If (Ds.Tables("pcur5").Rows(0)("plock_tipo_op") = Ds.Tables("pcur").Rows(0)("tipo_operacion_id") _
                            And Ds.Tables("pcur5").Rows(0)("plock_doc_trans_id") = Ds.Tables("pcur").Rows(0)("doc_trans_id") _
                            And Ds.Tables("pcur3").Rows(0)("max_secuencia") = secuencia_realizada) Then

                            strsql = ""
                            strsql = strsql & "Update posicion" & vbNewLine
                            strsql = strsql & " SET pos_lockeada=0," & vbNewLine
                            strsql = strsql & "     LCK_TIPO_OPERACION = NULL," & vbNewLine
                            strsql = strsql & "     LCK_USUARIO_ID = NULL," & vbNewLine
                            strsql = strsql & "     LCK_DOC_TRANS_ID=NULL," & vbNewLine
                            strsql = strsql & "     LCK_OBS = Null" & vbNewLine
                            strsql = strsql & " WHERE posicion_id = " & Ds.Tables("pcur2").Rows(0)("posicion_anterior")
                            Cmd.CommandText = strsql
                            Cmd.ExecuteNonQuery()

                            If (Ds.Tables("pcur5").Rows(0)("pos_vacia") = 0 _
                              And Posicion_Vacia_PorCantidad(Ds.Tables("pcur2").Rows(0)("posicion_anterior"))) Then
                                strsql = ""
                                strsql = strsql & " Update posicion Set pos_vacia = 1  WHERE posicion_id =" & Ds.Tables("pcur2").Rows(0)("posicion_anterior")
                                Cmd.CommandText = strsql
                                Cmd.ExecuteNonQuery()
                            End If
                        End If
                    End If
                End If
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            'Cmd = Nothing
            Da = Nothing
            Ds = Nothing
        End Try
    End Sub
    Private Function Posicion_Vacia_PorCantidad(ByVal p_posicion As Object) As Boolean

        Dim strsql As String
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        'Dim Cmd As SqlCommand
        'Dim RSvalor As ADODB.Recordset
        'RSvalor = New ADODB.Recordset
        Try
            'Cmd = Cnx.CreateCommand
            Da = New SqlDataAdapter(Cmd)
            'Cmd.Connection = Cnx
            Cmd.CommandType = CommandType.Text
            strsql = ""
            strsql = strsql & "SELECT Sum(isnull(cantidad,0)) as total_ubic" & vbNewLine
            strsql = strsql & "FROM RL_DET_DOC_TRANS_POSICION RL" & vbNewLine
            strsql = strsql & "WHERE posicion_actual=" & p_posicion
            'RSvalor = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Da.Fill(Ds, "RsValor")
            If Ds.Tables("RSvalor").Rows.Count > 0 Then
                If (Ds.Tables("RSvalor").Rows(0)("total_ubic") > 0) Then
                    Return False
                Else
                    Return True
                End If
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Ds = Nothing
            Da = Nothing
            'Cmd = Nothing
        End Try
    End Function

    Private Sub Tareas_Post_Movimiento(ByVal P_DOC_TRANS_ID As Object)
        Dim strsql As String, aux_where As String = ""
        Dim tipo_movimiento As String = ""
        'Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Ds As New DataSet
        'Dim pcur As ADODB.Recordset
        'pcur = New ADODB.Recordset
        'Dim pcur1 As ADODB.Recordset
        'pcur1 = New ADODB.Recordset
        'Dim pcur2 As ADODB.Recordset
        'pcur2 = New ADODB.Recordset
        Try
            'Cmd = Cnx.CreateCommand
            Da = New SqlDataAdapter(Cmd)
            'Cmd.Connection = Cnx
            Cmd.CommandType = CommandType.Text
            strsql = ""
            strsql = strsql & "SELECT tipo_operacion_id FROM documento_Transaccion  Where doc_trans_id = " & P_DOC_TRANS_ID
            'pcur = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Da.Fill(Ds, "pcur")

            strsql = ""
            strsql = strsql & "SELECT  min(estacion) as nueva_estacion " & vbNewLine
            strsql = strsql & "FROM  det_tipo_movimiento t1, RL_DET_DOC_TRANS_POSICION t2," & vbNewLine
            strsql = strsql & "      det_documento_transaccion t3,det_documento t4" & vbNewLine
            strsql = strsql & "WHERE t1.tipo_movimiento_id = t2.tipo_movimiento_id " & vbNewLine
            strsql = strsql & "      and t3.nro_linea_trans = t2.nro_linea_trans " & vbNewLine
            strsql = strsql & "      and t4.documento_id = t3.documento_id AND  t4.nro_linea = t3.nro_linea_doc " & vbNewLine
            strsql = strsql & "      and t2.doc_trans_id =" & P_DOC_TRANS_ID & " AND t2.ultima_estacion < t1.estacion AND" & vbNewLine
            strsql = strsql & "      ((ultima_secuencia IS NOT NULL and ultima_secuencia < secuencia) OR" & vbNewLine
            strsql = strsql & "      (ultima_secuencia IS NULL)  ) AND t3.doc_trans_id =" & P_DOC_TRANS_ID & vbNewLine
            Select Case CStr(Ds.Tables("pcur").Rows(0)("tipo_operacion_id"))
                Case "ING"
                    strsql = strsql & "       and t2.doc_trans_id=t3.doc_Trans_id " & vbNewLine
                Case "EGR"
                    strsql = strsql & "       and t2.doc_trans_id_egr=t3.doc_Trans_id" & vbNewLine
                Case "TR"
                    strsql = strsql & "       and t2.doc_trans_id_tr=t3.doc_Trans_id " & vbNewLine
            End Select
            Cmd.CommandText = strsql
            Da.Fill(Ds, "pcur1")
            strsql = ""
            strsql = strsql & "UPDATE RL_DET_DOC_TRANS_POSICION" & vbNewLine
            strsql = strsql & "SET ultima_estacion =  " & IIf(IsDBNull(Ds.Tables("pcur1").Rows(0)("nueva_estacion")), "NULL", Ds.Tables("pcur1").Rows(0)("nueva_estacion")) & vbNewLine
            strsql = strsql & "   ,ultima_secuencia = NULL " & vbNewLine
            strsql = strsql & "   ,tipo_movimiento_ID=null " & vbNewLine
            Select Case CStr(Ds.Tables("pcur").Rows(0)("tipo_operacion_id"))
                Case "ING"
                    strsql = strsql & "where doc_trans_id=" & P_DOC_TRANS_ID
                Case "EGR"
                    strsql = strsql & "where doc_trans_id_egr=" & P_DOC_TRANS_ID
                Case "TR"
                    strsql = strsql & "where doc_trans_id_tr=" & P_DOC_TRANS_ID
            End Select
            Cmd.CommandText = strsql
            Da.Fill(Ds, "pcur2")
            Actualizar_Reglas_Pendientes(P_DOC_TRANS_ID, "RM_1", 1)
            strsql = ""
            strsql = strsql & "UPDATE documento_transaccion SET it_mover=0 Where doc_trans_id = " & P_DOC_TRANS_ID
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            'Cmd = Nothing
            Da = Nothing
            Ds = Nothing
        End Try
    End Sub

    Public Function ExecuteAll(ByVal DocumentoID As Long, ByVal NroLinea As Long, ByVal Pos_id As Object, ByVal vNaveId As Object) As Boolean
        Dim Ds As New DataSet
        Dim PCUR As New DataSet
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim vRl As Rl_Struct
        Dim xSQL As String = ""
        Dim Pa As SqlParameter
        'Cnx.Open()
        'Cmd = Cnx.CreateCommand
        'Trans = Cnx.BeginTransaction()
        'Cmd.Connection = Cnx
        'Cmd.Transaction = Trans
        'Dim DocTransId As Object = 0, NroLineaTrans As Object = 0, CatLogId As Object = "", EstMercId As Object = "", CatLogIdFinal As Object = ""
        Try
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandText = "[DBO].[EXECUTE_ALL]"
            Cmd.Parameters.Clear()
            'Cmd.Parameters.Add("@DOCUMENTO_ID", SqlDbType.BigInt).Value = DocumentoID
            Pa = New SqlParameter("@DOCUMENTO_ID", SqlDbType.BigInt)
            Pa.Value = DocumentoID
            Cmd.Parameters.Add(Pa)
            Pa = Nothing
            'Cmd.Parameters.Add("@NRO_LINEA", SqlDbType.BigInt).Value = NroLinea
            Pa = New SqlParameter("@NRO_LINEA", SqlDbType.BigInt)
            Pa.Value = NroLinea
            Cmd.Parameters.Add(Pa)
            Pa = Nothing

            'Cmd.Parameters.Add("@POS_ID", SqlDbType.BigInt).Value = Pos_id
            Pa = New SqlParameter("@POS_ID", SqlDbType.BigInt)
            Pa.Value = Pos_id
            Cmd.Parameters.Add(Pa)
            Pa = Nothing
            'Cmd.Parameters.Add("@VNAVE_ID", SqlDbType.BigInt).Value = vNaveId
            Pa = New SqlParameter("@VNAVEID", SqlDbType.BigInt)
            Pa.Value = vNaveId
            Cmd.Parameters.Add(Pa)
            Pa = Nothing
            Cmd.ExecuteNonQuery()


            '    ObtenerValores(DocumentoID, NroLinea, DocTransId, NroLineaTrans, CatLogId, EstMercId, CatLogIdFinal)
            '    If Not OBTENER_POS_ANTERIORES_V320(DocTransId, NroLineaTrans, CatLogIdFinal, EstMercId, Ds) Then
            '        Throw New Exception()
            '    End If
            '    If Not VER_POS_ANTERIORES_PROD_v320(DocTransId, NroLineaTrans, CatLogIdFinal, EstMercId, PCUR) Then
            '        Throw New Exception()
            '    End If
            '    If Not BORRAR_POS_ASIGNADAS_V320(DocTransId, NroLineaTrans, CatLogIdFinal, EstMercId) Then
            '        Throw New Exception()
            '    End If
            '    CargarStruct(PCUR, "VerPosAnterioresProd", 0, vRl)
            '    With vRl
            '        If Not Rl_InsertRecord(.RL_ID, .DOC_TRANS_ID, .nro_linea_trans, .POSICION_ANTERIOR, Pos_id, _
            '                        .CANTIDAD, .TIPO_MOVIMIENTO_ID, .ULTIMA_ESTACION, .ULTIMA_SECUENCIA, .NAVE_ANTERIOR, _
            '                         vNaveId, .DOCUMENTO_ID, .NRO_LINEA, .DISPONIBLE, .DOC_TRANS_ID_EGR, .NRO_LINEA_TRANS_EGR, _
            '                        .DOC_TRANS_ID_TR, .NRO_LINEA_TRANS_TR, .CLIENTE_ID, .CAT_LOG_ID, .CAT_LOG_ID_FINAL, _
            '                        .Est_Merc_Id) Then
            '            Throw New Exception()
            '        End If
            '    End With
            '    If Not Actualizar_Estado_Ubicac_Item(DocTransId, NroLineaTrans) Then
            '        Throw New Exception()
            '    End If
            '    If Not OcuparPosicionesVacias_DocTR(DocTransId) Then
            '        Throw New Exception()
            '    End If
            '    If Not DEL_SYS_LOCATOR(DocumentoID, NroLinea) Then
            '        Throw New Exception()
            '    End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("ExecuteAll. SQL Server- " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("ExecuteAll. - " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Private Function DEL_SYS_LOCATOR(ByVal DocumentoID As Long, ByVal NroLinea As Long) As Boolean
        Dim Pa As SqlParameter
        'Dim Cmd As SqlCommand
        Try
            'Cmd = Cnx.CreateCommand
            'Cmd.Connection = Cnx
            Cmd.Parameters.Clear()
            Cmd.CommandText = "MOB_ELIMINAR_LOCATOR_ING"
            Cmd.CommandType = CommandType.StoredProcedure

            Pa = New SqlParameter("@Documento_Id", SqlDbType.Int)
            Pa.Value = DocumentoID
            Cmd.Parameters.Add(Pa)
            Pa = Nothing
            Pa = New SqlParameter("@Nro_linea", SqlDbType.Int)
            Pa.Value = NroLinea
            Cmd.Parameters.Add(Pa)

            Cmd.ExecuteNonQuery()

            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Pa = Nothing
        End Try
    End Function

    Private Sub ObtenerValores(ByVal DocumentoId As Long, ByVal NroLinea As Long, ByRef DocTransId As Object, _
                               ByRef NroLineaTrans As Object, ByRef CatLogId As Object, _
                               ByRef EstMercId As Object, ByRef CatLogIdFinal As Object)
        Dim xSQL As String
        'Dim Cmd As SqlCommand
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        Try
            Da = New SqlDataAdapter(Cmd)
            Cmd.CommandType = CommandType.Text
            xSQL = "        SELECT 	DOC_TRANS_ID, NRO_LINEA_TRANS,DDT.EST_MERC_ID,DDT.CAT_LOG_ID,DD.CAT_LOG_ID_FINAL "
            xSQL = xSQL & " FROM    DET_DOCUMENTO DD INNER JOIN DET_DOCUMENTO_TRANSACCION DDT"
            xSQL = xSQL & " ON(DD.DOCUMENTO_ID=DDT.DOCUMENTO_ID AND DD.NRO_LINEA=DDT.NRO_LINEA_DOC)"
            xSQL = xSQL & " WHERE DD.DOCUMENTO_ID =" & DocumentoId
            xSQL = xSQL & " AND DD.NRO_LINEA=" & NroLinea
            Cmd.CommandText = xSQL
            Da.Fill(Ds, "Datos")

            DocTransId = CLng(Ds.Tables("Datos").Rows(0)("DOC_TRANS_ID"))
            NroLineaTrans = CLng(Ds.Tables("Datos").Rows(0)("NRO_LINEA_TRANS"))
            EstMercId = IIf(IsDBNull(Ds.Tables("Datos").Rows(0)("EST_MERC_ID")), Nothing, Ds.Tables("Datos").Rows(0)("EST_MERC_ID"))
            CatLogId = IIf(IsDBNull(Ds.Tables("Datos").Rows(0)("CAT_LOG_ID")), Nothing, Ds.Tables("Datos").Rows(0)("CAT_LOG_ID"))
            CatLogIdFinal = IIf(IsDBNull(Ds.Tables("Datos").Rows(0)("CAT_LOG_ID_FINAL")), Nothing, Ds.Tables("Datos").Rows(0)("CAT_LOG_ID_FINAL"))
        Catch ex As Exception
            MsgBox("ObtenerValores: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            'Cmd = Nothing
            Da = Nothing
            Ds = Nothing
        End Try
    End Sub

    Private Sub CargarStruct(ByVal Ds As DataSet, ByVal Table As String, ByVal Pos As Integer, ByRef Struct As Rl_Struct)
        Try
            With Struct
                .DOC_TRANS_ID = CLng(Ds.Tables(Table).Rows(Pos)("DOC_TRANS_ID"))
                .nro_linea_trans = CLng(Ds.Tables(Table).Rows(Pos)("NRO_LINEA_TRANS"))
                .POSICION_ANTERIOR = IIf(IsDBNull(Ds.Tables(Table).Rows(Pos)("POSICION_ANTERIOR")), Nothing, Ds.Tables(Table).Rows(Pos)("POSICION_ANTERIOR"))
                .NAVE_ANTERIOR = IIf(IsDBNull(Ds.Tables(Table).Rows(Pos)("NAVE_ANTERIOR")), Nothing, Ds.Tables(Table).Rows(Pos)("NAVE_ANTERIOR"))
                .POSICION_ACTUAL = IIf(IsDBNull(Ds.Tables(Table).Rows(Pos)("POSICION_ACTUAL")), Nothing, Ds.Tables(Table).Rows(Pos)("POSICION_ACTUAL"))
                .NAVE_ACTUAL = IIf(IsDBNull(Ds.Tables(Table).Rows(Pos)("NAVE_ACTUAL")), Nothing, Ds.Tables(Table).Rows(Pos)("NAVE_ACTUAL"))
                .CANTIDAD = IIf(IsDBNull(Ds.Tables(Table).Rows(Pos)("CANTIDAD")), Nothing, Ds.Tables(Table).Rows(Pos)("CANTIDAD"))
                '.TIPO_MOVIMIENTO_ID = IIf(IsDBNull(Ds.Tables(Table).Rows(Pos)("MOVIMIENTO_PENDIENTE")), Nothing, Ds.Tables(Table).Rows(Pos)("MOVIMIENTO_PENDIENTE"))
                .CLIENTE_ID = IIf(IsDBNull(Ds.Tables(Table).Rows(Pos)("CLIENTE_ID")), Nothing, Ds.Tables(Table).Rows(Pos)("CLIENTE_ID"))
                .CAT_LOG_ID = IIf(IsDBNull(Ds.Tables(Table).Rows(Pos)("CAT_LOG_ID")), Nothing, Ds.Tables(Table).Rows(Pos)("CAT_LOG_ID"))
                .CAT_LOG_ID_FINAL = IIf(IsDBNull(Ds.Tables(Table).Rows(Pos)("CAT_LOG_ID_FINAL")), Nothing, Ds.Tables(Table).Rows(Pos)("CAT_LOG_ID_FINAL"))
                .Est_Merc_Id = IIf(IsDBNull(Ds.Tables(Table).Rows(Pos)("EST_MERC_ID")), Nothing, Ds.Tables(Table).Rows(Pos)("EST_MERC_ID"))
                .RL_ID = Nothing
            End With
        Catch ex As Exception
            MsgBox(ex.Message & " Error al cargar estructura.", MsgBoxStyle.OkOnly, ClsName)
        End Try
    End Sub

    Private Function OcuparPosicionesVacias_DocTR(ByVal P_DOC_TRANS_ID As Object) As Boolean
        'Dim Cmd As SqlCommand
        Try
            Dim xSQL As String
            'Cmd = Cnx.CreateCommand
            Cmd.CommandType = CommandType.Text
            xSQL = " UPDATE posicion SET pos_vacia='0' "
            xSQL = xSQL & " WHERE posicion_id IN"
            xSQL = xSQL & " (SELECT posicion_actual"
            xSQL = xSQL & " FROM rl_det_doc_trans_posicion rl, posicion p"
            xSQL = xSQL & " WHERE rl.posicion_actual = p.posicion_id AND p.pos_vacia = '1' AND"
            xSQL = xSQL & " rl.doc_trans_id=" & P_DOC_TRANS_ID & " )"
            Cmd.CommandText = xSQL
            Cmd.ExecuteNonQuery()
            Return True
        Catch SQLEx As SqlException
            MsgBox("OcuparPosicionesVacias_DocTR: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("OcuparPosicionesVacias_DocTR: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
            'Finally
            '    Cmd = Nothing
        End Try
    End Function


    Private Sub AGRUPA_RLS(ByVal P_DOC_TRANS_ID As Object)

        Dim strsql As String
        Dim Ds As New DataSet
        'Dim Cmd As New SqlCommand
        Dim Da As SqlDataAdapter
        Dim i As Integer
        'Dim fg As New rl_det_doc_trans_posicion_api
        'fg.Conexion = bd
        'Dim RSdatos As ADODB.Recordset
        'Dim RSInfo As ADODB.Recordset
        'RSInfo = New ADODB.Recordset
        'RSdatos = New ADODB.Recordset

        Try
            'Cmd = Cnx.CreateCommand
            Cmd.CommandType = CommandType.Text
            'Cmd.Connection = Cnx
            Da = New SqlDataAdapter(Cmd)


            strsql = ""
            strsql = strsql & "SELECT  0 AS rl_id" & vbNewLine
            strsql = strsql & "        ,doc_trans_id" & vbNewLine
            strsql = strsql & "        ,nro_linea_trans" & vbNewLine
            strsql = strsql & "        ,posicion_anterior" & vbNewLine
            strsql = strsql & "        ,posicion_actual" & vbNewLine
            strsql = strsql & "        ,Sum(cantidad) AS cantidad" & vbNewLine
            strsql = strsql & "        ,tipo_movimiento_id " & vbNewLine
            strsql = strsql & "        ,ultima_estacion" & vbNewLine
            strsql = strsql & "        ,ultima_secuencia" & vbNewLine
            strsql = strsql & "        ,nave_anterior" & vbNewLine
            strsql = strsql & "        ,nave_actual" & vbNewLine
            strsql = strsql & "        ,documento_id" & vbNewLine
            strsql = strsql & "        ,nro_linea" & vbNewLine
            strsql = strsql & "        ,disponible" & vbNewLine
            strsql = strsql & "        ,doc_trans_id_egr" & vbNewLine
            strsql = strsql & "        ,nro_linea_trans_egr" & vbNewLine
            strsql = strsql & "        ,doc_trans_id_tr" & vbNewLine
            strsql = strsql & "        ,nro_linea_trans_tr" & vbNewLine
            strsql = strsql & "        ,cliente_id" & vbNewLine
            strsql = strsql & "        ,cat_log_id" & vbNewLine
            strsql = strsql & "        ,cat_log_id_final" & vbNewLine
            strsql = strsql & "        ,est_merc_id" & vbNewLine
            strsql = strsql & "From    rl_det_doc_Trans_posicion" & vbNewLine
            strsql = strsql & "Where   doc_trans_id =" & P_DOC_TRANS_ID & vbNewLine
            strsql = strsql & "        AND CAT_LOG_ID = 'TRAN_ING'" & vbNewLine
            strsql = strsql & "Group By" & vbNewLine
            strsql = strsql & "         doc_trans_id" & vbNewLine
            strsql = strsql & "        ,nro_linea_trans" & vbNewLine
            strsql = strsql & "        ,posicion_anterior" & vbNewLine
            strsql = strsql & "        ,posicion_actual" & vbNewLine
            strsql = strsql & "        ,cantidad" & vbNewLine
            strsql = strsql & "        ,tipo_movimiento_id " & vbNewLine
            strsql = strsql & "        ,ultima_estacion" & vbNewLine
            strsql = strsql & "        ,ultima_secuencia" & vbNewLine
            strsql = strsql & "        ,nave_anterior" & vbNewLine
            strsql = strsql & "        ,nave_actual" & vbNewLine
            strsql = strsql & "        ,documento_id" & vbNewLine
            strsql = strsql & "        ,nro_linea" & vbNewLine
            strsql = strsql & "        ,disponible" & vbNewLine
            strsql = strsql & "        ,doc_trans_id_egr" & vbNewLine
            strsql = strsql & "        ,nro_linea_trans_egr" & vbNewLine
            strsql = strsql & "        ,doc_trans_id_tr" & vbNewLine
            strsql = strsql & "        ,nro_linea_trans_tr" & vbNewLine
            strsql = strsql & "        ,cliente_id" & vbNewLine
            strsql = strsql & "        ,cat_log_id" & vbNewLine
            strsql = strsql & "        ,cat_log_id_final" & vbNewLine
            strsql = strsql & "        ,est_merc_id" & vbNewLine
            strsql = strsql & "ORDER BY 1,2"
            Cmd.CommandText = strsql
            Da.Fill(Ds, "RsDatos")

            strsql = ""
            strsql = strsql & "DELETE FROM rl_det_doc_Trans_posicion" & vbNewLine
            strsql = strsql & "WHERE doc_Trans_id =" & P_DOC_TRANS_ID & vbNewLine
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()

            For i = 0 To Ds.Tables("RsDatos").Rows.Count - 1
                Rl_InsertRecord(Ds.Tables("RsDatos").Rows(i)("RL_ID"), Ds.Tables("rsdatos").Rows(i)("doc_trans_id"), _
                                Ds.Tables("RsDatos").Rows(i)("nro_linea_trans"), Ds.Tables("RSdatos").Rows(i)("posicion_anterior"), _
                                Ds.Tables("RSdatos").Rows(i)("posicion_actual"), Ds.Tables("RSdatos").Rows(i)("cantidad"), _
                                Ds.Tables("RSdatos").Rows(i)("tipo_movimiento_id"), Ds.Tables("RSdatos").Rows(i)("ultima_estacion"), _
                                Ds.Tables("RSdatos").Rows(i)("ultima_secuencia"), Ds.Tables("RSdatos").Rows(i)("nave_anterior"), _
                                Ds.Tables("RSdatos").Rows(i)("nave_actual"), Ds.Tables("RSdatos").Rows(i)("documento_id"), _
                                Ds.Tables("RSdatos").Rows(i)("nro_linea"), Ds.Tables("RSdatos").Rows(i)("disponible"), _
                                Ds.Tables("RSdatos").Rows(i)("doc_trans_id_egr"), Ds.Tables("RSdatos").Rows(i)("nro_linea_trans_egr"), _
                                Ds.Tables("RSdatos").Rows(i)("doc_trans_id_tr"), Ds.Tables("RSdatos").Rows(i)("nro_linea_trans_tr"), _
                                Ds.Tables("RSdatos").Rows(i)("cliente_id"), Ds.Tables("RSdatos").Rows(i)("cat_log_id"), _
                                Ds.Tables("RSdatos").Rows(i)("cat_log_id_final"), Ds.Tables("RSdatos").Rows(i)("est_merc_id"))
            Next
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            'Cmd = Nothing
            Da = Nothing
            Ds = Nothing
        End Try
    End Sub
End Class
