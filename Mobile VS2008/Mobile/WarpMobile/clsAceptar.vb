Imports System.Data
Imports System.Data.SqlClient


Public Class clsAceptar

#Region "Declaraciones Warp"
    Private Const OP_Aprobar = 1        ' TR_APR  1
    Private Const OP_Rechazar = 2         ' TR_REC  2
    Private Const OP_Movimientos = 3              ' TR_MOV  3
    Private Const OP_Ubicar = 4                  ' TR_UBI  4
    Private Const OP_Sacar = 5                   ' TR_SAC  5
    Private Const OP_Imp_Cpte = 6                ' TR_IMC  6
    Private Const OP_Imp_RE = 7                  ' TR_IMR  7
    Private Const OP_Mod_Cabecera = 8            ' TR_MOD  8
    Private Const OP_Configurar = 9              ' TR_CON  9
    Private Const OP_Ing_Conteos = 10             ' TR_INC 10
    Private Const OP_Analisis = 11                ' TR_ANA 11
    Private Const OP_Anular = 12                  ' ?????? 12
    Private Const OP_Etiquetas = 13             ' Impresion de Etiquetas
    Private Const OP_Imp_Re_Anexo = 14             ' Impresion de Anexo al Remito
    Private Const Op_Modifica_CatLog = 15         ' Modificacion de Categoria Logica.
    Private Const OP_Modifica_Cant = 16             'Modificacion de la cantidad pedida

    Public Enum eDireccion
        adelante = 1
        atras = 2
    End Enum

#End Region


    Private Const ClsName As String = "Operaciones de Estacion."
    'Private Cmd As SqlCommand
    Private Cnx As New SqlConnection
    Private vUsrid As String = ""
    Private vOperacionId As String = ""
    Private vDocumentoId As Long = 0
    Private vNroLinea As Long = 0
    Private EstacionId As String = ""
    Private BlnLibero As Boolean = False

    Public Property UsuarioID() As String
        Get
            Return vUsrid
        End Get
        Set(ByVal value As String)
            vUsrid = value
        End Set
    End Property

    Public Property objConnection() As SqlConnection
        Get
            Return Cnx
        End Get
        Set(ByVal value As SqlConnection)
            Cnx = value
            'Cmd = Cnx.CreateCommand
        End Set
    End Property

    'Public Property objCmd() As SqlCommand
    '    Get
    '        Return Cmd
    '    End Get
    '    Set(ByVal value As SqlCommand)
    '        Cmd = value
    '    End Set
    'End Property

    Private objCmd As SqlCommand

    Public Property Cmd() As SqlCommand
        Get
            Return objCmd
        End Get
        Set(ByVal value As SqlCommand)
            objCmd = value
        End Set
    End Property

    Public Property OperacionID()
        Get
            Return vOperacionId
        End Get
        Set(ByVal value)
            vOperacionId = value
        End Set
    End Property

    Public Property DocumentoID() As Long
        Get
            Return vDocumentoId
        End Get
        Set(ByVal value As Long)
            vDocumentoId = value
        End Set
    End Property

    Public Property NroLinea() As Long
        Get
            Return vNroLinea
        End Get
        Set(ByVal value As Long)
            vNroLinea = value
        End Set
    End Property

    Private vTransacc As SqlTransaction

    Public Property Transacc() As SqlTransaction
        Get
            Return vTransacc
        End Get
        Set(ByVal value As SqlTransaction)
            vTransacc = value
        End Set
    End Property

    Public Function CreateTemporales(ByVal Cmd As SqlCommand) As Boolean
        Dim xSQL As String = ""
        Dim strCollate As String = ""
        Cmd.CommandType = CommandType.Text
        strCollate = "COLLATE SQL_Latin1_General_CP1_CI_AS"

        xSQL = " CREATE TABLE #temp_saldos_catlog (" & vbNewLine
        xSQL = xSQL & " cliente_id     VARCHAR(15)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " producto_id    VARCHAR(30)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " cat_log_id     VARCHAR(50)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " cantidad       NUMERIC(20,5) NOT NULL," & vbNewLine
        xSQL = xSQL & " categ_stock_id VARCHAR(15)    " & strCollate & " NULL," & vbNewLine
        xSQL = xSQL & " est_merc_id    VARCHAR(15)    " & strCollate & " NULL" & vbNewLine
        xSQL = xSQL & " )"

        Cmd.CommandText = xSQL
        Cmd.ExecuteNonQuery()

        xSQL = " CREATE TABLE #temp_saldos_stock (" & vbNewLine
        xSQL = xSQL & " cliente_id  VARCHAR(15)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " producto_id VARCHAR(30)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " cant_tr_ing NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " cant_stock  NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " cant_tr_egr NUMERIC(20,5) NULL" & vbNewLine
        xSQL = xSQL & " )" & vbNewLine

        Cmd.CommandText = xSQL
        Cmd.ExecuteNonQuery()

        xSQL = " CREATE TABLE #temp_saldos_stock (" & vbNewLine
        xSQL = xSQL & " cliente_id  VARCHAR(15)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " producto_id VARCHAR(30)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " cant_tr_ing NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " cant_stock  NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " cant_tr_egr NUMERIC(20,5) NULL" & vbNewLine
        xSQL = xSQL & " )" & vbNewLine

        Cmd.CommandText = xSQL
        Cmd.ExecuteNonQuery()

        Return True
    End Function

    Public Function Aceptar() As Boolean
        Dim StrError As String = ""
        Dim StrMensaje As String = ""
        Dim BO_Commit As Boolean = False
        Dim blnEsta As Boolean
        Dim dblDocTransID As Double = 0
        Dim strTransaccionID As String = ""
        Dim rsTr As New DataSet
        Try

            If Not CollectData(dblDocTransID, strTransaccionID, rsTr, True, True, OP_Aprobar) Then
                Return False
            End If
            If (rsTr.Tables(0).Rows(0)("actualiza_stock") = "1") Or _
                (rsTr.Tables(0).Rows(0)("fin") = "1" And _
                 rsTr.Tables(0).Rows(0)("actualiza_stock") = "1") Then
                Select Case OperacionID
                    Case "ING"
                        If Not oItemsEnPreIngreso(dblDocTransID, blnEsta, StrError) Then
                            Throw New Exception("1")
                        End If
                        If blnEsta Then
                            Libera_Transaccion(dblDocTransID)
                            Return True
                        End If
                        If Not ItemsEnIntermedia(dblDocTransID, blnEsta) Then
                            Throw New Exception("1")
                        End If
                        If blnEsta Then
                            Libera_Transaccion(dblDocTransID)
                            Return True
                        End If
                    Case "EGR"

                End Select
            End If
            If (rsTr.Tables(0).Rows(0)("fin") = "1") Then
                'Aca debo guardar quien fue el que termino la operacion de Ingreso.
                'CierreIngreso(dblDocTransID)
                If Not PasarDocIngreso(dblDocTransID) Then
                    Throw New Exception("1")
                    Exit Function
                End If
            End If

            'If Not OPasarDoc(rsTr, EstacionId, strTransaccionID, dblDocTransID, 1, OperacionID, StrError) Then
            '    Throw New Exception("1")
            '    Exit Function
            'End If

            'If Not BlnLibero Then
            'Libera_Transaccion(dblDocTransID)
            'End If


            Return True
        Catch ex As Exception
            If ex.Message <> "1" Then
                'MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            End If
            Return False
        End Try
    End Function

    Private Function PasarDocIngreso(ByVal DocTransID As Long) As Boolean
        Dim pa As SqlParameter
        Try
            Cmd.Parameters.Clear()
            Cmd.CommandText = "[DBO].[PASARDOC_ING]"
            Cmd.CommandType = CommandType.StoredProcedure

            pa = New SqlParameter("@DOC_TRANS_ID", SqlDbType.BigInt)
            pa.Value = DocTransID
            Cmd.Parameters.Add(pa)
            Cmd.ExecuteNonQuery()
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message)
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub CierreIngreso(ByVal DocumentoId As String)
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd.Parameters.Clear()
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = "Dbo.Fin_Documento_Hist_Insert"

                Pa = New SqlParameter("@Doc", SqlDbType.BigInt)
                Pa.Value = DocumentoId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Tipo", SqlDbType.Char)
                Pa.Value = 2
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
            Else

            End If
        Catch ex As Exception

        Finally
            Pa = Nothing
        End Try
    End Sub

    Private Function ItemsEnIntermedia(ByVal DocTransId As Long, ByRef Bln As Boolean) As Boolean
        Dim Pa As SqlParameter
        Dim vInt As Integer
        Try
            Cmd.Parameters.Clear()
            Cmd.CommandText = "Mob_IngVerificaIntermedia"
            Cmd.CommandType = CommandType.StoredProcedure

            Cmd.Parameters.Add("@Doc_trans_id", SqlDbType.Int).Value = DocTransId
            Pa = New SqlParameter("@Out", SqlDbType.Int)
            Pa.Direction = ParameterDirection.Output
            Cmd.Parameters.Add(Pa)

            Cmd.ExecuteNonQuery()
            vInt = IIf(IsDBNull(Cmd.Parameters("@Out").Value), 0, Cmd.Parameters("@Out").Value)

            If vInt = 1 Then
                Bln = True
            Else
                Bln = False
            End If
            Return True
        Catch SQLEx As SqlException
            'MsgBox("ItemsEnIntermedia SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("ItemsEnIntermedia: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Pa = Nothing
        End Try
    End Function

    Public Function OPasarDoc(ByVal rsFlags As DataSet, _
                             ByVal strEstacionID As String, _
                             ByVal strTransaccionID As String, _
                             ByVal dblDocTransID As Double, _
                             ByVal Direccion As eDireccion, _
                             ByVal strTipoOperacion As String, _
                             ByRef StrError As String) As Boolean
        Dim Ci As New ClsIngreso
        Try
            Dim strSigEstacion As String = ""
            Dim strOldEstacion As String = ""
            Dim strAuxEstacion As String = ""
            Dim StrSql As String = ""
            Dim iFinal As Integer
            Dim iPos As Integer
            Dim iOrdenEstacion As Integer
            Dim oStation As Object
            iFinal = 0

            iOrdenEstacion = rsFlags.Tables(0).Rows(0)("orden")
            If Direccion = 1 Then
                'StrSql = "am_funciones_estacion_API.GetNextEstacion"
                oStation = GetNextEstacion(strTransaccionID, iOrdenEstacion, dblDocTransID)
            Else
                'StrSql = "am_funciones_estacion_API.GetPrevEstacion"
            End If
            strOldEstacion = strEstacionID
            strAuxEstacion = IIf(IsNothing(oStation) Or IsDBNull(oStation), "", oStation.ToString)
            If strAuxEstacion <> "" Then
                iPos = InStr(1, strAuxEstacion, "|")
                strSigEstacion = Mid(strAuxEstacion, 1, iPos - 1)
                iOrdenEstacion = Val(Mid(strAuxEstacion, iPos + 1, Len(strSigEstacion)))
            End If

            '******verificar si el flag de actualizacion de stock***********************
            '*****esto se refiere a la mercaderia, o cualquier cambio en el stock
            If rsFlags.Tables(0).Rows(0)("actualiza_stock") = 1 And Direccion = 1 Then

                Select Case strTipoOperacion
                    Case "ING"
                        iFinal = 1
                    Case "EGR"
                        Ci.objConnection = Cnx
                        Ci.Cmd = Cmd
                        If Not Ci.Tareas_Movimientos(dblDocTransID) Then
                            Throw New Exception
                        End If
                        Ci = Nothing
                        iFinal = 2
                    Case "TR"
                        iFinal = 3
                    Case "INV"
                        iFinal = 4
                End Select

                If Not UpdateEstacionActual_Stock(dblDocTransID, iFinal) Then
                    Throw New Exception
                End If

            End If
            iFinal = 0
            '******************verifica si es el final de la ruta*************************
            'se refiere exclusivamente al documento,no la mercaderia.
            If rsFlags.Tables(0).Rows(0)("fin") = 1 And Direccion = 1 Then
                Select Case strTipoOperacion
                    Case "ING"
                        iFinal = 1
                    Case "EGR"
                        iFinal = 2
                    Case "TR"
                        iFinal = 3
                    Case "INV"
                        iFinal = 4
                End Select
            End If
            If Not UpdateEstacionActual(strTransaccionID, dblDocTransID, strSigEstacion, iOrdenEstacion, iFinal) Then
                Throw New Exception
            End If
            'si tuvo exito en pasar el doc en la transaccion actual, tiene que verificar pendientes
            ' para la nueva estacion.
            If rsFlags.Tables(0).Rows(0)("fin") = 0 Then
                If Not ComprobarMovimientosPendientes(dblDocTransID, strTransaccionID, _
                                                        strSigEstacion, iOrdenEstacion, StrError) Then
                    Throw New Exception
                    Exit Function
                End If
                If Not ComprobarMovimientosPendientes_2(dblDocTransID, StrError) Then
                    Throw New Exception
                    Exit Function
                End If
                If Not ComprobarMandatoriosPendientes(dblDocTransID, strTransaccionID, strSigEstacion, iOrdenEstacion, StrError) Then
                    Throw New Exception
                    Exit Function
                End If
                If Not ComprobarInventariosPendientes_Conteo(dblDocTransID, _
                                                             strTransaccionID, _
                                                             strSigEstacion, iOrdenEstacion, StrError) Then
                    Throw New Exception
                    Exit Function
                End If
                If Not ComprobarInventariosPendientes_Adm(dblDocTransID, _
                                                          strTransaccionID, _
                                                          strSigEstacion, iOrdenEstacion, StrError) Then
                    Throw New Exception
                    Exit Function
                End If

            End If
            '******************************************************************************
            Return True
        Catch ex As Exception
            Return False
        Finally
            Ci = Nothing
        End Try
    End Function

    Public Function ComprobarInventariosPendientes_Adm(ByVal dblDOCTR_ID As Double, _
                                                       ByVal strTRANSACCION_ID As String, _
                                                       ByVal strESTACION_ID As String, _
                                                       ByVal iOrden As Integer, ByRef vErr As Object) As Boolean
        Try
            Comprobar_RINV_Adm(strTRANSACCION_ID, dblDOCTR_ID, strESTACION_ID, iOrden)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub Comprobar_RINV_Adm(ByVal pTransaccionID As Object, ByVal DocTransID As Object, _
                                  ByVal EstacionID As Object, ByVal pOrdenEstacion As Object)
        Dim strsql As String
        Dim Da As New SqlDataAdapter(Cmd)
        Dim pcur As New DataSet
        Dim pAux As New DataSet
        Cmd.CommandType = CommandType.Text
        strsql = ""
        strsql = strsql & "SELECT TOP 1 IsNull(inv_adm,'0') as R_ID"
        strsql = strsql & " From RL_TRANSACCION_ESTACION" & vbNewLine
        strsql = strsql & " Where transaccion_id = '" & pTransaccionID & "'" & vbNewLine
        strsql = strsql & " AND estacion_id = '" & UCase(Trim(EstacionID)) & "'" & vbNewLine
        strsql = strsql & " AND orden = " & pOrdenEstacion & vbNewLine
        Cmd.CommandText = strsql
        Da.Fill(pcur, "pcur")

        If pcur.Tables("pcur").Rows.Count > 0 Then
            If pcur.Tables("pcur").Rows(0)("R_ID") <> "0" Then
                'Chequear que no este cerrado el Inventario, antes de ponerle el Flag.
                strsql = ""
                strsql = strsql & "SELECT cerrado"
                strsql = strsql & " From inventario"
                strsql = strsql & " WHERE doc_Trans_id = " & DocTransID
                Cmd.CommandText = strsql
                Da.Fill(pAux, "paux")
                If pAux.Tables("paux").Rows.Count > 0 Then
                    If pAux.Tables("paux").Rows(0)("cerrado") <> "1" Then
                        SetPendienteRINV_Adm(DocTransID)
                    End If
                End If
            End If
        End If
        Da = Nothing
        pcur = Nothing
        pAux = Nothing
    End Sub

    Public Sub SetPendienteRINV_Adm(ByVal pDocTRID As Object)
        Dim ID As Long
        Call pdt_InsertRecord(ID, pDocTRID, "INV", "RINV3", 1)
    End Sub

    Public Function ComprobarInventariosPendientes_Conteo(ByVal dblDOCTR_ID As Double, _
                                                          ByVal strTRANSACCION_ID As String, _
                                                          ByVal strESTACION_ID As String, _
                                                          ByVal iOrden As Integer, ByRef vErr As Object) As Boolean
        Try
            'Dim StrSql As String
            'StrSql = "am_funciones_estacion_api.
            Comprobar_RINV_Conteo(strTRANSACCION_ID, dblDOCTR_ID, strESTACION_ID, iOrden)
            Return True
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Public Sub Comprobar_RINV_Conteo(ByVal pTransaccionID As Object, ByVal DocTransID As Object, _
                                     ByVal EstacionID As Object, ByVal pOrdenEstacion As Object)
        Dim strsql As String
        Dim da As New SqlDataAdapter(Cmd)
        Dim pcur As New DataSet
        Dim pAux As New DataSet
        Dim Cerrado As String = ""
        strsql = ""
        strsql = strsql & "SELECT TOP 1 IsNull(inv_contar,'0') As R_ID" & vbNewLine
        strsql = strsql & " From RL_TRANSACCION_ESTACION" & vbNewLine
        strsql = strsql & " Where transaccion_id = '" & pTransaccionID & "'" & vbNewLine
        strsql = strsql & " AND estacion_id = '" & UCase(Trim(EstacionID)) & "'" & vbNewLine
        strsql = strsql & " AND orden = " & pOrdenEstacion & vbNewLine
        'pcur = bd.Execute(strsql)
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = strsql
        da.Fill(pcur, "pcur")

        If pcur.Tables("pcur").Rows.Count > 0 Then
            If pcur.Tables("pcur").Rows(0)("R_ID") <> "0" Then
                'Chequear que no este cerrado el Inventario, antes de ponerle el Flag.
                strsql = ""
                strsql = strsql & "SELECT cerrado" & vbNewLine
                strsql = strsql & " From inventario" & vbNewLine
                strsql = strsql & " WHERE doc_Trans_id = " & DocTransID & vbNewLine
                Cmd.CommandText = strsql
                da.Fill(pAux, "paux")
                If pAux.Tables("pAux").Rows.Count > 0 Then
                    If pAux.Tables("pAux").Rows(0)("cerrado") <> "1" Then
                        SetPendienteRINV_Conteo(DocTransID)
                    End If
                End If
            End If
        End If
        da = Nothing
        pAux = Nothing
        pcur = Nothing
    End Sub

    Public Sub SetPendienteRINV_Conteo(ByVal pDocTRID As Object)
        Dim ID As Long
        Call pdt_InsertRecord(ID, pDocTRID, "INV", "RINV2", 1)
    End Sub

    Public Function ComprobarMandatoriosPendientes(ByVal dblDOCTR_ID As Double, _
                                                   ByVal strTRANSACCION_ID As String, _
                                                   ByVal strESTACION_ID As String, _
                                                   ByVal iOrden As Integer, ByRef vErr As Object) As Boolean
        Try
            'StrSql = "am_funciones_estacion_api.
            Comprobar_ReglaInformacion(strTRANSACCION_ID, dblDOCTR_ID, strESTACION_ID, iOrden)
            Return True
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Public Sub Comprobar_ReglaInformacion(ByVal pTransaccionID As Object, ByVal DocTransID As Object, _
                                          ByVal EstacionID As Object, ByVal pOrdenEstacion As Object)
        Dim strsql As String
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Pcur As New DataSet
        Cmd.CommandType = CommandType.Text
        strsql = ""
        strsql = strsql & "SELECT TOP 1 IsNull(r_informacion_id, '0') As R_ID" & vbNewLine
        strsql = strsql & " From RL_TRANSACCION_ESTACION" & vbNewLine
        strsql = strsql & " Where transaccion_id = '" & pTransaccionID & "'" & vbNewLine
        strsql = strsql & " AND estacion_id = '" & UCase(Trim(EstacionID)) & "'" & vbNewLine
        strsql = strsql & " AND orden = " & pOrdenEstacion & vbNewLine
        'pcur = bd.Execute(strsql)
        Cmd.CommandText = strsql
        Da.Fill(Pcur, "pcur")

        If Pcur.Tables("pcur").Rows.Count > 0 Then
            If Pcur.Tables("pcur").Rows(0)("R_ID") <> "0" Then
                Call SetPendienteReglaInformacion(DocTransID, Pcur.Tables("pcur").Rows(0)("R_ID"))
            End If
        End If
        Da = Nothing
        Pcur = Nothing
    End Sub

    Public Sub SetPendienteReglaInformacion(ByVal pDocTRID As Object, ByVal pR_ID As String)
        Dim strsql As String = ""
        Dim pcur As New DataSet
        Dim ID As Long = 0
        Dim Da As New SqlDataAdapter(Cmd)
        Dim i As Integer = 0
        strsql = "" & vbNewLine
        strsql = strsql & "SELECT dri.nro_linea" & vbNewLine
        strsql = strsql & " FROM  det_regla_informacion dri" & vbNewLine
        strsql = strsql & " WHERE dri.r_informacion_id = '" & pR_ID & "'" & vbNewLine
        Da.Fill(pcur, "pcur")
        For i = 0 To pcur.Tables("pcur").Rows.Count - 1
            pdt_InsertRecord(ID, pDocTRID, "INF", pR_ID, pcur.Tables("pcur").Rows(0)("nro_linea"))
        Next
    End Sub

    Public Function ComprobarMovimientosPendientes_2(ByVal dblDOCTR_ID As Double, _
                                                     ByRef vErr As Object) As Boolean
        Try
            'StrSql = "am_funciones_estacion_api.
            Comprobar_ReglaMovimiento(dblDOCTR_ID)
            Return True
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Public Sub Comprobar_ReglaMovimiento(ByVal pDocTransID As Object)
        Dim strsql As String
        Dim pcur As New DataSet
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Ci As New ClsIngreso
        Cmd.CommandType = CommandType.Text
        Ci.objConnection = Cnx
        strsql = ""
        strsql = strsql & "SELECT MAX(ultima_estacion) as nueva_estacion" & vbNewLine
        strsql = strsql & " From RL_DET_DOC_TRANS_POSICION" & vbNewLine
        strsql = strsql & " WHERE doc_trans_id = " & pDocTransID & vbNewLine
        Cmd.CommandText = strsql
        Da.Fill(pcur, "pcur")
        If Not IsDBNull(pcur.Tables(0).Rows(0)("nueva_estacion")) Then
            'No se hizo todos los movimientos =>  le quedan movim. pendientes
            Ci.Actualizar_Reglas_Pendientes(pDocTransID, "RM_1", 0)
        End If
        Ci = Nothing
    End Sub

    Public Function ComprobarMovimientosPendientes(ByVal dblDOCTR_ID As Double, _
                                                   ByVal strTRANSACCION_ID As String, _
                                                   ByVal strESTACION_ID As String, _
                                                   ByVal iOrden As Integer, ByRef vErr As Object) As Boolean
        Try
            Dim StrSql As String
            StrSql = "am_funciones_estacion_api."
            Comprobar_Si_debe_ubicar(strTRANSACCION_ID, dblDOCTR_ID, strESTACION_ID, iOrden)
            Return True
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Public Sub Comprobar_Si_debe_ubicar(ByVal pTransaccionID As Object, ByVal DocTransID As Object, _
                                        ByVal EstacionID As Object, ByVal pOrdenEstacion As Object)

        Dim strsql As String
        Dim pcur As New DataSet
        Dim Da As New SqlDataAdapter(Cmd)
        Cmd.CommandType = CommandType.Text
        strsql = ""
        strsql = strsql & "SELECT ubicacion_obligatoria" & vbNewLine
        strsql = strsql & "  FROM RL_TRANSACCION_ESTACION" & vbNewLine
        strsql = strsql & " WHERE transaccion_id = '" & UCase(Trim(pTransaccionID)) & "'" & vbNewLine
        strsql = strsql & "   AND estacion_id = '" & UCase(Trim(EstacionID)) & "'" & vbNewLine
        strsql = strsql & "   AND orden = " & pOrdenEstacion & vbNewLine
        'pcur = bd.Execute(strsql)
        Cmd.CommandText = strsql
        Da.Fill(pcur, "pcur")

        If pcur.Tables(0).Rows.Count > 0 Then
            If pcur.Tables(0).Rows(0)("ubicacion_obligatoria") = "1" Then
                Call SetPendienteReglaUbicacion(DocTransID)
            End If
        End If
        Da = Nothing
        pcur = Nothing
    End Sub

    Public Sub SetPendienteReglaUbicacion(ByVal pDocTRID As Object)
        Dim strsql As String
        Dim pcur As New DataSet
        Dim Da As New SqlDataAdapter(Cmd)
        Dim ID As Long
        'Dim pdt As New pendiente_doc_trans_api
        Cmd.CommandType = CommandType.Text
        strsql = ""
        strsql = strsql & "SELECT regla_id" & vbNewLine
        strsql = strsql & " FROM pendiente_doc_trans" & vbNewLine
        strsql = strsql & "WHERE doc_trans_id = " & pDocTRID & vbNewLine
        strsql = strsql & "  AND regla_id = 'RU_1'" & vbNewLine
        'pcur = bd.Execute(strsql)
        Cmd.CommandText = strsql
        Da.Fill(pcur, "pcur")

        If pcur.Tables(0).Rows.Count > 0 Then
            Call pdt_InsertRecord(ID, pDocTRID, "UBIC", "RU_1", 1)
        End If

    End Sub

    Public Sub pdt_InsertRecord(ByVal p_id As Object, ByVal P_DOC_TRANS_ID As Object, _
                                ByVal P_TIPO_REGLA As Object, ByVal P_REGLA_ID As Object, _
                                ByVal P_NRO_LINEA As Object)

        Dim strsql As String
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
        Cmd.CommandType = CommandType.Text
        Cmd.ExecuteNonQuery()
        p_id = OBTENER_SECUENCIA()

    End Sub

    Private Function GetDocTransTratamiento(ByRef DocTransId As Long, ByRef Tratamiento As String, ByRef EstacionId As String) As Boolean
        Dim Da As SqlDataAdapter
        Dim Ds As New DataSet
        Dim xSQL As String = ""
        Try
            Cmd.CommandType = CommandType.Text
            Da = New SqlDataAdapter(Cmd)
            xSQL = "        SELECT 	DISTINCT " & vbNewLine
            xSQL = xSQL & " DT.TRANSACCION_ID, DT.DOC_TRANS_ID,DT.ESTACION_ACTUAL " & vbNewLine
            xSQL = xSQL & " FROM 	DET_DOCUMENTO DD INNER JOIN DET_DOCUMENTO_TRANSACCION DDT " & vbNewLine
            xSQL = xSQL & "         ON(DD.DOCUMENTO_ID=DDT.DOCUMENTO_ID AND DD.NRO_LINEA=DDT.NRO_LINEA_DOC) " & vbNewLine
            xSQL = xSQL & "         INNER JOIN DOCUMENTO_TRANSACCION DT " & vbNewLine
            xSQL = xSQL & "         ON(DT.DOC_TRANS_ID=DDT.DOC_TRANS_ID) " & vbNewLine
            xSQL = xSQL & "WHERE	DD.DOCUMENTO_ID= " & vDocumentoId & " AND " & vbNewLine
            xSQL = xSQL & "         DD.NRO_LINEA = " & vNroLinea & vbNewLine
            Cmd.CommandText = xSQL
            Da.Fill(Ds, "Table")
            Tratamiento = IIf(IsDBNull(Ds.Tables("Table").Rows(0)("TRANSACCION_ID")), "", Ds.Tables("Table").Rows(0)("TRANSACCION_ID").ToString)
            DocTransId = IIf(IsDBNull(Ds.Tables("table").Rows(0)("DOC_TRANS_ID")), 0, Ds.Tables("table").Rows(0)("DOC_TRANS_ID"))
            EstacionId = IIf(IsDBNull(Ds.Tables("table").Rows(0)("ESTACION_ACTUAL")), "", Ds.Tables("table").Rows(0)("ESTACION_ACTUAL"))
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message & "-GetDocTransTratamiento", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox(ex.Message & "-GetDocTransTratamiento", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Da = Nothing
            Ds = Nothing
        End Try
    End Function

    Private Function CollectData(ByRef LngDocTransID As Long, _
                                 ByRef strTransaccionID As String, _
                                 ByRef rsFlags As DataSet, _
                                 ByVal blnValidaMandatorios As Boolean, _
                                 ByVal blnVerboseMandatorios As Boolean, _
                                 ByVal TipOp As Integer, _
                                 Optional ByRef iOrdenEstacion As Integer = Nothing) As Boolean

        Dim blnTienePendientes As Boolean = False
        Dim iOrden As Integer = 0
        Dim iCol_TransaccionID As Integer = 0
        Dim iCol_DocTransID As Integer = 0
        Dim StrError As String = ""
        Dim StrErrSource As String = ""
        Dim StrErrDescr As String = ""
        Dim DblNumError As Double = 0
        Dim strMsgPendientes As String = ""
        Dim StrMensaje As String = ""
        Try
            Select Case TipOp
                Case OP_Aprobar ' Aprobar          1 ING/EGR/TR/INV
                    strMsgPendientes = "No es posible aprobar el Documento ya que tiene operaciones pendientes."
                Case OP_Rechazar ' Rechazar        2 ING/EGR/TR/INV
                    strMsgPendientes = "No es posible rechazar el Documento ya que tiene operaciones pendientes."
                Case OP_Imp_Cpte, OP_Imp_RE, OP_Imp_Re_Anexo ' Imprimir
                    strMsgPendientes = "No es posible Imprimir el Documento ya que tiene operaciones pendientes."
                Case Else
                    strMsgPendientes = "No es posible procesar esta operacion, ya que tiene operaciones pendientes."
            End Select
            If Not GetDocTransTratamiento(LngDocTransID, strTransaccionID, EstacionId) Then
                Throw New Exception("1")
            End If
            If Not Verifica_Estado(CDbl(LngDocTransID), TipOp, StrMensaje, StrError) Then
                LngDocTransID = 0
                Throw New Exception("1")
            End If

            If blnValidaMandatorios Then
            End If
            If blnTienePendientes Then
                MsgBox(strMsgPendientes, vbInformation)
            Else
                If Not GetOrdenEstacionForDocTr(LngDocTransID, iOrden, StrError) Then
                    Throw New Exception("1")
                End If
                iOrdenEstacion = iOrden
                If Not GetFlagsForTR(EstacionId, strTransaccionID, iOrden, rsFlags, StrError) Then
                    Throw New Exception(StrError)
                End If
            End If
            Return True
        Catch ex As Exception
            If ex.Message <> "1" Then
                'MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            End If
            Return False
        End Try
    End Function

    Private Function GetFlagsForTR(ByVal strEstacionID As String, ByVal strTransaccionID As String, _
                                   ByVal iOrden As Integer, ByRef RsDatos As DataSet, _
                                   ByRef strErr As String) As Boolean
        Try
            If Not GetDataForTR(strEstacionID, strTransaccionID, iOrden, RsDatos) Then
                Throw New Exception
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetOrdenEstacionForDocTr(ByVal dblDocTr As Double, _
                                              ByRef iOrden As Integer, _
                                              ByRef StrError As String) As Boolean
        Try
            If Not GetOrdenEstacionForDocTrID(dblDocTr, iOrden) Then
                Throw New Exception
            End If
            Return True
        Catch ex As Exception
            StrError = ex.Message
            Return False
        End Try
    End Function

    Public Function Verifica_Estado(ByVal DblTR As Double, _
                                     ByVal Operacion As Integer, _
                                     ByRef StrMensaje As String, _
                                     ByRef StrError As String) As Boolean
        Dim Ds As New DataSet
        Try

            If Not Actualiza_Estado(DblTR, Operacion, Ds) Then
                Throw New Exception
            End If
            If Ds.Tables("pcur").Columns.Count = 1 Then
                StrMensaje = ""
            ElseIf Ds.Tables("pcur").Rows(0)("tr_activo") = 1 Then
                StrMensaje = "Se esta procesado por el Usuario: " & Ds.Tables("pcur").Rows(0)("Usuario") & vbCrLf & "En la Terminal: " & Ds.Tables("pcur").Rows(0)("Terminal") & "#U#"
                Throw New Exception(StrMensaje)
            ElseIf Ds.Tables("pcur").Rows(0)("Status") <> "T10" Then
                StrMensaje = "Este Documento esta procesado totalmente por el Usuario " & Ds.Tables("pcur").Rows(0)("Usuario") & vbCrLf & "En la Terminal " & Ds.Tables("pcur").Rows(0)("Terminal") & "#U#"
                Throw New Exception(StrMensaje)
            End If
            If Len(StrMensaje) > 0 Then
                StrError = StrMensaje
                Exit Function
            End If
            Ds = Nothing
            Return True
        Catch ex As Exception
            'MsgBox(ex.Message & "- Verifica_Estado", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Ds = Nothing
        End Try
    End Function

    Public Function Actualiza_Estado(ByVal DocTrID As Object, ByVal TipoOperacion As Object, ByVal pcur As DataSet) As Boolean
        Dim Da As SqlDataAdapter
        Try
            Da = New SqlDataAdapter(Cmd)
            Cmd.CommandType = CommandType.Text
            Dim strsql As String
            Dim pAux As New DataSet
            Dim salida As Long = 0
            Dim strEstacion As String = ""
            Dim vStatus As String = ""
            Dim vTr_Activo As String = ""
            Dim vUsuario_ID As String = ""
            Dim vTerminal As String = ""
            Dim vSession_ID As String = ""
            Dim v1Usuario_ID As String = ""
            Dim v1Terminal As String = ""
            Dim fecha_actual As Object
            Cmd.CommandType = CommandType.Text
            fecha_actual = Obtener_FechaActual()
            strsql = ""
            strsql = strsql & "SELECT" & vbNewLine
            strsql = strsql & "     DT.Status," & vbNewLine
            strsql = strsql & "     DT.TR_ACTIVO," & vbNewLine
            strsql = strsql & "     DT.USUARIO_ID," & vbNewLine
            strsql = strsql & "     DT.TERMINAL" & vbNewLine
            strsql = strsql & " FROM  DOCUMENTO_TRANSACCION DT" & vbNewLine
            strsql = strsql & " WHERE DT.DOC_TRANS_ID = " & DocTrID & vbNewLine
            Cmd.CommandText = strsql
            Da.Fill(pAux, "DDT")

            If pAux.Tables("DDT").Rows.Count > 0 Then
                vStatus = "" & pAux.Tables("DDT").Rows(0)("Status")
                vTr_Activo = "" & pAux.Tables("DDT").Rows(0)("TR_ACTIVO")
                v1Usuario_ID = "" & pAux.Tables("DDT").Rows(0)("USUARIO_ID")
                v1Terminal = "" & pAux.Tables("DDT").Rows(0)("TERMINAL")
            End If

            'Obtengo los datos de la sesion que va a utilizar este documento
            strsql = ""
            strsql = strsql & "SELECT '" & vUsrid & "' as USUARIO_ID," & vbNewLine
            strsql = strsql & "     USER_NAME() as session_id," & vbNewLine
            strsql = strsql & "     HOST_NAME() as terminal" & vbNewLine
            Cmd.CommandText = strsql
            If pAux.Tables("DDT") IsNot Nothing Then
                pAux.Tables.Remove("DDT")
            End If
            Da.Fill(pAux, "TERMINAL")

            If pAux.Tables("TERMINAL").Rows.Count > 0 Then
                vUsuario_ID = "" & pAux.Tables("TERMINAL").Rows(0)("USUARIO_ID")
                vTerminal = "" & pAux.Tables("TERMINAL").Rows(0)("TERMINAL")
                vSession_ID = "" & pAux.Tables("TERMINAL").Rows(0)("session_id")
            End If

            If vTr_Activo = "0" Then
                'Devolver "" Nada.
                strsql = ""
                strsql = strsql & "SELECT '' TR_ACTIVO" & vbNewLine
                strsql = strsql & " FROM DOCUMENTO_TRANSACCION" & vbNewLine
                strsql = strsql & " WHERE DOC_TRANS_ID = " & DocTrID & vbNewLine
                'pcur = bd.Execute(strsql)
                Cmd.CommandText = strsql
                Da.Fill(pcur, "PCUR")

                'v305 Agregado para que deslockee todas las operaciones del usuario actual (que puedan haber quedado lockeadas).
                strsql = ""
                strsql = strsql & "Update DOCUMENTO_TRANSACCION" & vbNewLine
                strsql = strsql & "     SET TR_ACTIVO = '0'," & vbNewLine
                strsql = strsql & "         TR_ACTIVO_ID = Null," & vbNewLine
                strsql = strsql & "         SESSION_ID = Null," & vbNewLine
                strsql = strsql & "         FECHA_CAMBIO_TR = Null" & vbNewLine
                strsql = strsql & " WHERE Usuario_Id = '" & vUsuario_ID & "'" & vbNewLine
                strsql = strsql & "     AND TERMINAL = '" & vTerminal & "'" & vbNewLine
                strsql = strsql & "     AND TR_ACTIVO = '1'" & vbNewLine
                'pAux = bd.Execute(strsql)
                Cmd.CommandText = strsql
                Cmd.ExecuteNonQuery()

                'Cambiar el Flag a 1 para que otro usuario no lo pueda tomar.
                strsql = ""
                strsql = strsql & "UPDATE DOCUMENTO_TRANSACCION" & vbNewLine
                strsql = strsql & "     SET TR_ACTIVO = '1'" & vbNewLine
                strsql = strsql & "         ,TR_ACTIVO_ID = (SELECT TR_ACTIVO_ID" & vbNewLine
                strsql = strsql & "                         From SYS_TR_ACTIVO_MOTIVO" & vbNewLine
                strsql = strsql & "                         WHERE TIPO_OPERACION_ID = '" & TipoOperacion & "')" & vbNewLine

                strsql = strsql & "         ,USUARIO_ID = " & IIf(UCase(Trim(vUsuario_ID)) = "" Or IsNothing(vUsuario_ID), "Null", "'" & UCase(Trim(vUsuario_ID)) & "'") & vbNewLine
                strsql = strsql & "         ,TERMINAL = " & IIf(UCase(Trim(vTerminal)) = "" Or IsNothing(vTerminal), "Null", "'" & UCase(Trim(vTerminal)) & "'") & vbNewLine
                strsql = strsql & "         ,SESSION_ID = " & IIf(UCase(Trim(vSession_ID)) = "" Or IsNothing(vSession_ID), "Null", "'" & UCase(Trim(vSession_ID)) & "'") & vbNewLine
                strsql = strsql & "         ,FECHA_CAMBIO_TR = getdate()" '& IIf(UCase(Trim(fecha_actual)) = "" Or IsNothing(fecha_actual), "Null", "'" & Format(fecha_actual, "dd/MM/yyyy hh:nn:ss") & "'") & vbNewLine
                strsql = strsql & " WHERE DOC_TRANS_ID = " & DocTrID & vbNewLine
                'pAux = bd.Execute(strsql)
                Cmd.CommandText = strsql
                Cmd.ExecuteNonQuery()

            ElseIf vTr_Activo = "1" And UCase(Trim(vStatus)) = "T10" And v1Usuario_ID = vUsuario_ID And v1Terminal = vTerminal Then
                'Devolver "" Nada.
                strsql = ""
                strsql = strsql & "SELECT '' TR_ACTIVO" & vbNewLine
                strsql = strsql & " FROM DOCUMENTO_TRANSACCION" & vbNewLine
                strsql = strsql & " WHERE DOC_TRANS_ID = " & DocTrID & vbNewLine
                'pcur = bd.Execute(strsql)
                Cmd.CommandText = strsql
                Da.Fill(pcur, "PCUR")

                'v305 Agregado para que deslockee todas las operaciones del usuario actual (que puedan haber quedado lockeadas).
                strsql = ""
                strsql = strsql & "Update DOCUMENTO_TRANSACCION" & vbNewLine
                strsql = strsql & "     SET TR_ACTIVO = '0'," & vbNewLine
                strsql = strsql & "         TR_ACTIVO_ID = Null," & vbNewLine
                strsql = strsql & "         SESSION_ID = Null," & vbNewLine
                strsql = strsql & "         FECHA_CAMBIO_TR = Null" & vbNewLine
                strsql = strsql & " WHERE Usuario_Id = '" & vUsuario_ID & "'" & vbNewLine
                strsql = strsql & "     AND TERMINAL = '" & vTerminal & "'" & vbNewLine
                strsql = strsql & "     AND TR_ACTIVO = '1'" & vbNewLine
                'pAux = bd.Execute(strsql)
                Cmd.CommandText = strsql
                Cmd.ExecuteNonQuery()

                strsql = ""
                strsql = strsql & "UPDATE DOCUMENTO_TRANSACCION" & vbNewLine
                strsql = strsql & "     SET TR_ACTIVO = '1'" & vbNewLine
                strsql = strsql & "         ,TR_ACTIVO_ID = (SELECT TR_ACTIVO_ID" & vbNewLine
                strsql = strsql & "                         From SYS_TR_ACTIVO_MOTIVO" & vbNewLine
                strsql = strsql & "                         WHERE TIPO_OPERACION_ID = '" & TipoOperacion & "')" & vbNewLine
                strsql = strsql & "         ,USUARIO_ID = " & IIf(UCase(Trim(vUsuario_ID)) = "" Or IsNothing(vUsuario_ID), "Null", "'" & UCase(Trim(vUsuario_ID)) & "'") & vbNewLine
                strsql = strsql & "         ,TERMINAL = " & IIf(UCase(Trim(vTerminal)) = "" Or IsNothing(vTerminal), "Null", "'" & UCase(Trim(vTerminal)) & "'") & vbNewLine
                strsql = strsql & "         ,SESSION_ID = " & IIf(UCase(Trim(vSession_ID)) = "" Or IsNothing(vSession_ID), "Null", "'" & UCase(Trim(vSession_ID)) & "'") & vbNewLine
                strsql = strsql & "         ,FECHA_CAMBIO_TR = getdate()" '& IIf(UCase(Trim(fecha_actual)) = "" Or IsNothing(fecha_actual), "Null", "'" & Format(fecha_actual, "dd/MM/yyyy hh:nn:ss") & "'") & vbNewLine
                strsql = strsql & " WHERE DOC_TRANS_ID = " & DocTrID & vbNewLine
                Cmd.CommandText = strsql
                Cmd.ExecuteNonQuery()

            ElseIf vTr_Activo = "1" Or UCase(Trim(vStatus)) <> "T10" Then
                'Devolver los datos de esta Transaccion.
                strsql = ""
                strsql = strsql & "SELECT DT.TR_ACTIVO," & vbNewLine
                strsql = strsql & "       DT.STATUS," & vbNewLine
                strsql = strsql & "       SU.NOMBRE AS USUARIO," & vbNewLine
                strsql = strsql & "       TERMINAL" & vbNewLine
                strsql = strsql & " FROM DOCUMENTO_TRANSACCION DT" & vbNewLine
                strsql = strsql & " INNER JOIN SYS_USUARIO SU ON (SU.USUARIO_ID = DT.USUARIO_ID)" & vbNewLine
                strsql = strsql & " WHERE DT.DOC_TRANS_ID = " & DocTrID & vbNewLine
                Cmd.CommandText = strsql
                Da.Fill(pcur, "PCUR")
            End If
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message & "- Actualiza Estado.", MsgBoxStyle.OkOnly, ClsName)
        Catch ex As Exception
            'MsgBox(ex.Message & "- Actualiza Estado.", MsgBoxStyle.OkOnly, ClsName)
        End Try
    End Function

    Private Function Obtener_FechaActual() As Object
        Dim Da As SqlDataAdapter
        Try
            Da = New SqlDataAdapter(Cmd)
            Dim strsql As String
            Dim RSvalor As New DataSet
            strsql = ""
            strsql = strsql & "SELECT GetDate() As Fecha"
            Cmd.CommandText = strsql
            Da.Fill(RSvalor, "rsvalor")
            Return RSvalor.Tables("rsvalor").Rows(0)("Fecha")
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message & "- Obtener_FechaActual.", MsgBoxStyle.OkOnly, ClsName)
            Return Nothing
        Catch ex As Exception
            'MsgBox(ex.Message & "- Obtener_FechaActual.", MsgBoxStyle.OkOnly, ClsName)
            Return Nothing
        Finally
            Da = Nothing
        End Try
    End Function

    Private Function GetRSPendientesForDoc(ByVal vDocTransID As Object, ByVal pcur As DataSet) As Boolean
        Dim Da As SqlDataAdapter
        Try
            Da = New SqlDataAdapter(Cmd)
            Dim strsql As String

            '**********levantar las RI**********************************

            strsql = "SELECT pdt.tipo_regla, 'DATOS ADICIONALES' AS Descripcion, pdt.doc_trans_id, PDT.REGLA_ID" & vbNewLine
            strsql = strsql & " ,(SELECT (tabla + '/' + campo) FROM  det_regla_informacion dri" & vbNewLine
            strsql = strsql & "     WHERE dri.r_informacion_id = PDT.REGLA_ID and dri.nro_linea = pdt.nro_linea) AS DescID" & vbNewLine
            strsql = strsql & " FROM PENDIENTE_doc_trans pdt WHERE pdt.doc_trans_id = " & vDocTransID & " AND pdt.tipo_regla = 'INF'" & vbNewLine
            strsql = strsql & " UNION ALL" & vbNewLine

            '************MOVIMIENTO********************************
            strsql = strsql & " SELECT pdt.tipo_regla, 'MOVIMIENTOS' AS Descripcion," & vbNewLine
            strsql = strsql & " pdt.doc_trans_id, PDT.REGLA_ID, 'REGLA DE MOVIMIENTOS'" & vbNewLine
            strsql = strsql & " FROM PENDIENTE_doc_trans pdt" & vbNewLine
            strsql = strsql & " WHERE pdt.doc_trans_id = " & vDocTransID & " AND pdt.tipo_regla = 'MOV'" & vbNewLine
            strsql = strsql & " UNION ALL" & vbNewLine

            '******IMPRESION*****************************************
            strsql = strsql & " SELECT pdt.tipo_regla, 'REPORTES'  AS Descripcion, pdt.doc_trans_id," & vbNewLine
            strsql = strsql & " PDT.REGLA_ID, (SELECT descripcion FROM det_regla_impresion rimp" & vbNewLine
            strsql = strsql & " WHERE rimp.regla_impresion_id = PDT.REGLA_ID and pdt.nro_linea = rimp.nro_linea) AS DescID" & vbNewLine
            strsql = strsql & " FROM PENDIENTE_doc_trans pdt" & vbNewLine
            strsql = strsql & " WHERE pdt.doc_trans_id = " & vDocTransID & " AND pdt.tipo_regla = 'IMP'" & vbNewLine
            strsql = strsql & " UNION ALL" & vbNewLine

            '******UBICACION*****************************************
            strsql = strsql & " SELECT pdt.tipo_regla, 'UBICACION' AS Descripcion, pdt.doc_trans_id," & vbNewLine
            strsql = strsql & " PDT.REGLA_ID, 'REGLA DE UBICACION'" & vbNewLine
            strsql = strsql & " FROM PENDIENTE_doc_trans pdt" & vbNewLine
            strsql = strsql & " WHERE pdt.doc_trans_id = " & vDocTransID & " AND pdt.tipo_regla = ' UBIC'" & vbNewLine
            strsql = strsql & " UNION ALL" & vbNewLine

            '******Transferencia*****************************************
            strsql = strsql & " SELECT pdt.tipo_regla, 'TRANSFERENCIA' AS Descripcion, pdt.doc_trans_id," & vbNewLine
            strsql = strsql & " PDT.REGLA_ID, 'REGLA DE TRANSFERENCIA'" & vbNewLine
            strsql = strsql & " FROM PENDIENTE_doc_trans pdt" & vbNewLine
            strsql = strsql & " WHERE pdt.doc_trans_id = " & vDocTransID & " AND pdt.tipo_regla = 'TR'" & vbNewLine

            '******INVENTARIO****************************************
            strsql = strsql & " UNION ALL" & vbNewLine
            strsql = strsql & " SELECT pdt.tipo_regla, 'INVENTARIO' AS Descripcion, pdt.doc_trans_id, PDT.REGLA_ID," & vbNewLine
            strsql = strsql & " CASE" & vbNewLine
            strsql = strsql & "    when PDT.REGLA_ID = 'RINV1' then 'CONFIGURAR INVENTARIO'" & vbNewLine
            strsql = strsql & "    when PDT.REGLA_ID = 'RINV2' then 'INGRESAR CONTEOS'" & vbNewLine
            strsql = strsql & "    when PDT.REGLA_ID = 'RINV3' then 'CERRAR INVENTARIO'" & vbNewLine
            strsql = strsql & " END" & vbNewLine
            strsql = strsql & " FROM PENDIENTE_doc_trans pdt" & vbNewLine
            strsql = strsql & " WHERE pdt.doc_trans_id = " & vDocTransID & " AND pdt.tipo_regla = 'INV'" & vbNewLine

            '******CARGA DE NUMEROS DE SERIE AL EGRESO*********************
            strsql = strsql & " UNION ALL" & vbNewLine
            strsql = strsql & " SELECT pdt.tipo_regla , 'DATOS ADICIONALES' AS Descripcion, pdt.doc_trans_id, PDT.REGLA_ID," & vbNewLine
            strsql = strsql & " CASE" & vbNewLine
            strsql = strsql & "    when PDT.REGLA_ID = 'SER_1' then 'CARGA DE NUMEROS DE SERIE'" & vbNewLine
            strsql = strsql & " END" & vbNewLine
            strsql = strsql & " FROM PENDIENTE_doc_trans pdt" & vbNewLine
            strsql = strsql & " WHERE pdt.doc_trans_id = " & vDocTransID & " AND pdt.tipo_regla = 'SER'" & vbNewLine

            '-------------------------------------
            strsql = strsql & " ORDER BY 2" & vbNewLine
            'pcur = bd.Execute(strsql)
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strsql
            Da.Fill(pcur, strsql)
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message & "- GetRsPendientesForDoc", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox(ex.Message & "- GetRsPendientesForDoc", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Da = Nothing
        End Try
    End Function

    Private Function GetOrdenEstacionForDocTrID(ByVal pDocTRID As Object, ByRef IOrden As Object) As Boolean
        Dim pcur As New DataSet
        Dim Da As SqlDataAdapter
        Try
            Da = New SqlDataAdapter(Cmd)
            Dim strsql As String
            Dim vInt As Integer
            Cmd.CommandType = CommandType.Text
            strsql = ""
            strsql = strsql & "SELECT orden_estacion as vOrden" & vbNewLine
            strsql = strsql & "from  documento_transaccion" & vbNewLine
            strsql = strsql & "WHERE doc_trans_id=" & pDocTRID
            'pcur = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Da.Fill(pcur, "pcur")
            If pcur.Tables("pcur").Rows.Count > 0 Then
                vInt = CInt(pcur.Tables("pcur").Rows(0)(0))
                IOrden = vInt
            End If
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message & "- GetOrdenEstacionForDocTrID", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox(ex.Message & "- GetOrdenEstacionForDocTrID", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Da = Nothing
            pcur = Nothing
        End Try
    End Function

    Private Function GetDataForTR(ByVal pEstacionID As String, ByVal pTrID As String, ByVal pOrden As Object, _
                                  ByRef pcur As DataSet) As Boolean
        Dim Da As SqlDataAdapter
        Try
            Cmd.CommandType = CommandType.Text
            Da = New SqlDataAdapter(Cmd)
            Dim strsql As String
            strsql = ""
            strsql = strsql & " SELECT * FROM  rl_transaccion_estacion " & vbNewLine
            strsql = strsql & " Where transaccion_id ='" & Trim(UCase(pTrID)) & "'" & vbNewLine
            strsql = strsql & "     AND ESTACION_ID='" & Trim(UCase(pEstacionID)) & "'" & vbNewLine
            strsql = strsql & "     AND ORDEN=" & pOrden
            Cmd.CommandText = strsql
            Da.Fill(pcur, "Flags")
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message & "- GetDataForTR", MsgBoxStyle.OkOnly, ClsName)
            Return True
        Catch ex As Exception
            'MsgBox(ex.Message & "- GetDataForTR", MsgBoxStyle.OkOnly, ClsName)
            Return True
        Finally
            Da = Nothing
        End Try
    End Function

    Public Function oItemsEnPreIngreso(ByVal dblDocTransID As Double, _
                                        ByRef blnEsta As Boolean, _
                                        ByRef StrError As String) As Boolean
        Try

            blnEsta = ItemsEnPreIngreso(dblDocTransID)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ItemsEnPreIngreso(ByVal pDocTRID As Object) As Integer
        Dim Da As SqlDataAdapter
        Dim Ds As New DataSet
        Try
            Da = New SqlDataAdapter(Cmd)
            Dim strsql As String = ""
            Dim vEmplazamientoID As String = ""
            Dim vDepositoID As String = ""
            Dim vNaveID As Long = 0
            Dim Flag As Integer = 0
            Dim i As Integer = 0
            Cmd.CommandType = CommandType.Text
            'Dim funciones_loggin_api As New funciones_loggin_api
            ' --esta funcion se fija si todos los items de un doc_tr de ingreso estan
            ' --en pre-ingreso, si es asi devuelve un 1, si no 0.
            strsql = "Select emplazamiento_default, deposito_default "
            strsql = strsql & " from sys_perfil_usuario"
            strsql = strsql & " where usuario_id='" & vUsrid & "'"
            Cmd.CommandText = strsql
            Da.Fill(Ds, "Sys_Perfil_Usuario")

            vEmplazamientoID = Ds.Tables("Sys_Perfil_Usuario").Rows(0)("Emplazamiento_default")
            vDepositoID = Ds.Tables("Sys_Perfil_Usuario").Rows(0)("deposito_default")

            strsql = ""
            strsql = strsql & "SELECT nave_id  as vNaveID FROM  nave" & vbNewLine
            strsql = strsql & "Where emplazamiento_id ='" & vEmplazamientoID & "'" & vbNewLine
            strsql = strsql & "      AND deposito_id='" & vDepositoID & "'" & vbNewLine
            strsql = strsql & "      AND pre_ingreso=1"
            Cmd.CommandText = strsql
            Da.Fill(Ds, "pcur")

            strsql = ""
            strsql = strsql & "SELECT rl_id AS vRL_ID," & vbNewLine
            strsql = strsql & "       doc_trans_id AS vDocTrId," & vbNewLine
            strsql = strsql & "       nro_linea_trans AS vNroLineaDocTrans," & vbNewLine
            strsql = strsql & "       cantidad AS vCantidad," & vbNewLine
            strsql = strsql & "       nave_actual As vNaveActual" & vbNewLine
            strsql = strsql & "FROM rl_det_doc_trans_posicion" & vbNewLine
            strsql = strsql & "WHERE doc_trans_id=" & pDocTRID
            Cmd.CommandText = strsql
            Da.Fill(Ds, "pcur1")

            For i = 0 To Ds.Tables("pcur1").Rows.Count - 1
                If (Ds.Tables("pcur1").Rows(i)("vNaveActual") IsNot Nothing) And (Not IsDBNull(Ds.Tables("pcur1").Rows(i)("vNaveActual"))) Then
                    Dim valPcur1 As Object = CInt(Ds.Tables("pcur1").Rows(i)("vNaveActual"))
                    Dim valPcur As Object = CInt(Ds.Tables("pcur").Rows(0)("vNaveID"))

                    If CInt(valPcur1) = CInt(valPcur) Then
                        Flag = 1
                    End If
                Else
                    If 0 = CInt(Ds.Tables("pcur").Rows(0)("vnaveid")) Then
                        Flag = 1
                    End If
                End If

                If Flag = 1 Then
                    Exit For
                End If
            Next i
            If Flag = 1 Then
                Return 1
            Else
                Return 0
            End If

        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message & "- ItemsEnPreIngreso", MsgBoxStyle.OkOnly, ClsName)
            Return True
        Catch ex As Exception
            'MsgBox(ex.Message & "- ItemsEnPreIngreso", MsgBoxStyle.OkOnly, ClsName)
            Return True
        Finally
            Da = Nothing
            Ds = Nothing
        End Try
    End Function

    Private Function GetNextEstacion(ByVal pTransaccionID As String, ByVal pOrden As Object, ByVal pDocTRID As Object) As String
        Dim Da As SqlDataAdapter
        Dim pcur As New DataSet
        Try
            Da = New SqlDataAdapter(Cmd)
            Dim strsql As String = ""
            Dim i As Integer = 0
            Dim salida As Long = 0
            Dim strEstacion As String = ""
            Dim vTransaccionID As String = ""
            Dim vEstacionActualID As String = ""
            Dim vOrdenEstacion As Long = 0
            Dim vID As Long = 0

            salida = 0
            Cmd.CommandType = CommandType.Text
            strsql = "" & vbNewLine
            strsql = strsql & "SELECT" & vbNewLine
            strsql = strsql & "     TRANSACCION_ID," & vbNewLine
            strsql = strsql & "     estacion_actual," & vbNewLine
            strsql = strsql & "     orden_estacion" & vbNewLine
            strsql = strsql & " FROM documento_transaccion" & vbNewLine
            strsql = strsql & " WHERE doc_trans_id = " & pDocTRID & vbNewLine
            strsql = strsql & " AND transaccion_id = '" & UCase(pTransaccionID) & "'" & vbNewLine
            strsql = strsql & " AND (status = 'T10' or status = 'T20')" & vbNewLine
            Cmd.CommandText = strsql
            Da.Fill(pcur, "pcur")

            If pcur.Tables("pcur").Rows.Count > 0 Then
                vTransaccionID = "" & pcur.Tables("pcur").Rows(0)("TRANSACCION_ID")
                vEstacionActualID = "" & pcur.Tables("pcur").Rows(0)("estacion_actual")
                vOrdenEstacion = pcur.Tables("pcur").Rows(0)("orden_estacion")
            End If

            strsql = "" & vbNewLine
            strsql = strsql & "select" & vbNewLine
            strsql = strsql & "     rte.TRANSACCION_ID," & vbNewLine
            strsql = strsql & "     rte.estacion_id," & vbNewLine
            strsql = strsql & "     rte.orden" & vbNewLine
            strsql = strsql & " FROM rl_transaccion_estacion rte" & vbNewLine
            strsql = strsql & " WHERE rte.TRANSACCION_ID = '" & UCase(Trim(vTransaccionID)) & "'" & vbNewLine
            strsql = strsql & " ORDER BY TRANSACCION_ID, orden" & vbNewLine
            If pcur.Tables("pcur") IsNot Nothing Then
                pcur.Tables.Remove("pcur")
            End If
            Cmd.CommandText = strsql
            Da.Fill(pcur, "pcur")
            For i = 0 To pcur.Tables("pcur").Rows.Count - 1
                strEstacion = pcur.Tables("pcur").Rows(i)("estacion_id") & "|" & CStr(pcur.Tables("pcur").Rows(i)("orden"))
                If salida = 1 And CInt(pcur.Tables("pcur").Rows(i)("orden")) > CInt(vOrdenEstacion) Then
                    Exit For
                End If
                If pcur.Tables("pcur").Rows(i)("estacion_id") = UCase(Trim(vEstacionActualID)) Then
                    salida = 1
                End If
            Next
            If salida = 0 Then
                strEstacion = ""
            End If
            Return strEstacion
        Catch SQLEx As SqlException
            Return ""
        Catch ex As Exception
            Return ""
        Finally
            pcur = Nothing
            Da = Nothing
        End Try
    End Function

    Private Function UpdateEstacionActual_Stock(ByVal DocTransID As Object, ByVal Flag As Object) As Boolean
        Dim pcur As New DataSet
        Dim pAux As New DataSet
        Dim Da As SqlDataAdapter
        Try
            Da = New SqlDataAdapter(Cmd)
            Dim strsql As String
            Dim i As Integer
            
            If Flag = 1 Then 'INGRESO, el documento paso una estacion conel flag actualiza_stock=1 y es un ingreso.


                Cmd.CommandType = CommandType.Text
                strsql = ""
                strsql = strsql & "SELECT rl_id" & vbNewLine
                strsql = strsql & " FROM RL_DET_DOC_TRANS_POSICION" & vbNewLine
                strsql = strsql & " WHERE DOC_TRANS_ID = " & DocTransID & vbNewLine
                'pcur = bd.Execute(strsql)

                Cmd.CommandText = strsql
                Da.Fill(pcur, "pcur")
                For i = 0 To pcur.Tables("pcur").Rows.Count - 1

                    If Not Actualizar_Historicos_X_Mov(pcur.Tables("pcur").Rows(0)("rl_id")) Then
                        Throw New Exception()
                    End If
                Next
                'DRS -----------------------------------------------------------------------------------------------------------------
                strsql = ""
                strsql = strsql & "UPDATE RL_DET_DOC_TRANS_POSICION" & vbNewLine
                strsql = strsql & "         SET CAT_LOG_ID=CAT_LOG_ID_FINAL," & vbNewLine
                strsql = strsql & "         DISPONIBLE = '1'" & vbNewLine
                strsql = strsql & " WHERE DOC_TRANS_ID = " & DocTransID & vbNewLine

                Cmd.CommandText = strsql
                Cmd.ExecuteNonQuery()

                strsql = ""
                strsql = strsql & "UPDATE DOCUMENTO_TRANSACCION" & vbNewLine
                strsql = strsql & "         SET status = 'T20'" & vbNewLine
                strsql = strsql & " WHERE doc_trans_id = " & DocTransID & vbNewLine
                'pAux = bd.Execute(strsql)

                Cmd.CommandText = strsql
                Cmd.ExecuteNonQuery()

                Enviar_RL_a_Historico(DocTransID, "ING") 'Copia el RL Actual al Historico


                'If Not Actualizar_HistSaldos_STOCK(Nothing, DocTransID, Nothing) Then
                'Throw New Exception
                'End If                'Maria Laura.

                'Call fh.Actualizar_HistSaldos_CatLog(Nothing, DocTransID, Nothing) 'Maria Laura.

                'If Not Actualizar_HistSaldos_CatLog(Nothing, DocTransID, Nothing) Then
                ' Throw New Exception
                'End If  'Maria Laura.

            ElseIf Flag = 2 Then 'EGRESO, el documento paso una estacion conel flag actualiza_stock=1 y es un egreso.
                ''DRS CAMBIO 29/12/2004
                ''Actualiza el Stock, (desde aca no hay vuelta atras en las ubicaciones).
                'strsql = ""
                'strsql = strsql & "SELECT rl_id" & vbNewLine
                'strsql = strsql & " FROM RL_DET_DOC_TRANS_POSICION" & vbNewLine
                'strsql = strsql & " WHERE DOC_TRANS_ID_EGR = " & DocTransID & vbNewLine
                'pcur = bd.Execute(strsql)
                'Do While Not pcur.EOF
                '    Call fh.Actualizar_Historicos_X_Mov(pcur.Fields("rl_id"))

                '    pcur.MoveNext()
                'Loop
                ''DRS -----------------------------------------------------------------------------------------------------------------
                ''Invocar procedure donde se haga la imagen para los reportes.
                'Call fh.Enviar_RL_a_Historico(DocTransID, "EGR")
                ''Borra el doc de egreso, de la tabla rl.
                'Call fe.BorrarDocTREgreso(DocTransID)
                'strsql = ""
                'strsql = strsql & "UPDATE DOCUMENTO_TRANSACCION" & vbNewLine
                'strsql = strsql & "       SET status = 'T20'" & vbNewLine
                'strsql = strsql & " WHERE doc_trans_id = " & DocTransID & vbNewLine
                'pAux = bd.Execute(strsql)
                'Call fh.Actualizar_HistSaldos_STOCK(Null, DocTransID, Null)
                'Call fh.Actualizar_HistSaldos_CatLog(Null, DocTransID, Null)
            ElseIf Flag = 3 Then 'TRANSFERENCIA, el documento paso una estacion conel flag actualiza_stock=1 y es una transferencia.
                ''DRS CAMBIO 29/12/2004
                ''Actualiza el Stock, (desde aca no hay vuelta atras en las ubicaciones).
                'strsql = ""
                'strsql = strsql & "SELECT rl_id" & vbNewLine
                'strsql = strsql & " FROM RL_DET_DOC_TRANS_POSICION" & vbNewLine
                'strsql = strsql & " WHERE DOC_TRANS_ID_TR = " & DocTransID & vbNewLine
                'pcur = bd.Execute(strsql)
                'Do While Not pcur.EOF
                '    Call fh.Actualizar_Historicos_X_Mov(pcur.Fields("rl_id"))

                '    pcur.MoveNext()
                'Loop
                ''DRS -----------------------------------------------------------------------------------------------------------------
                ''Actualiza los detalles a disponible
                'strsql = ""
                'strsql = strsql & "UPDATE RL_DET_DOC_TRANS_POSICION" & vbNewLine
                'strsql = strsql & "       SET DISPONIBLE = '1'" & vbNewLine
                'strsql = strsql & " WHERE DOC_TRANS_ID_TR = " & DocTransID & vbNewLine
                'pAux = bd.Execute(strsql)
                'Call fh.Enviar_RL_a_Historico(DocTransID, "TR")
            End If
            Return True
        Catch ex As Exception
            If ex.Message <> "" Then
                'MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            End If
            Return False
        End Try
    End Function

    Private Function Actualizar_Historicos_X_Mov(ByVal p_rl_pos_id As Object) As Boolean
        Dim Da As SqlDataAdapter
        Dim pcur As New DataSet
        Dim rs As New DataSet
        Try
            Da = New SqlDataAdapter(Cmd)
            Dim strsql As String = ""
            Dim P_HIST_ID As Long = 0
            Dim pusuario As String = ""
            Dim codigo As String = ""
            Dim codigo2 As String = ""
            Dim es_pre_egreso As String = ""
            Dim descr As String = ""
            Dim fecha_actual As Object
            Cmd.CommandType = CommandType.Text
            'Dim hp As New Historico_Producto_api
            'Dim hpos As New historico_posicion_api
            'Dim fg As New funciones_generales_api
            'hp.Conexion = bd
            'hpos.Conexion = bd
            'fg.Conexion = bd
            fecha_actual = Obtener_FechaActual()
            strsql = ""
            strsql = strsql & "SELECT rl.posicion_actual," & vbNewLine
            strsql = strsql & "        rl.nave_actual," & vbNewLine
            strsql = strsql & "        rl.posicion_anterior," & vbNewLine
            strsql = strsql & "        rl.nave_anterior," & vbNewLine
            strsql = strsql & "        rl.cantidad," & vbNewLine
            strsql = strsql & "        ddt.documento_id," & vbNewLine
            strsql = strsql & "        ddt.nro_linea_doc," & vbNewLine
            strsql = strsql & "        rl.doc_trans_id_egr," & vbNewLine
            strsql = strsql & "        dd.cliente_id," & vbNewLine
            strsql = strsql & "        dd.producto_id," & vbNewLine
            strsql = strsql & "        dd.nro_serie," & vbNewLine
            strsql = strsql & "        dd.nro_lote," & vbNewLine
            strsql = strsql & "        CAST( day(dd.fecha_vencimiento) AS VARCHAR) + '/' +CAST(MONTH(dd.FECHA_VENCIMIENTO)AS VARCHAR(2)) +'/'+CAST(YEAR(dd.FECHA_VENCIMIENTO)AS VARCHAR(4)) AS FECHA_VENCIMIENTO," & vbNewLine
            strsql = strsql & "        dd.nro_partida," & vbNewLine
            strsql = strsql & "        dd.nro_despacho" & vbNewLine
            strsql = strsql & " FROM  RL_DET_DOC_TRANS_POSICION rl," & vbNewLine
            strsql = strsql & "        det_documento_transaccion ddt," & vbNewLine
            strsql = strsql & "        det_documento_transaccion ddt2," & vbNewLine
            strsql = strsql & "        det_documento dd" & vbNewLine
            strsql = strsql & " WHERE ddt2.documento_id = dd.documento_id" & vbNewLine
            strsql = strsql & "        And ddt2.nro_linea_doc = dd.nro_linea" & vbNewLine
            strsql = strsql & "        And rl.doc_trans_id = ddt2.doc_trans_id" & vbNewLine
            strsql = strsql & "        And rl.nro_linea_trans = ddt2.nro_linea_trans" & vbNewLine
            strsql = strsql & "        And IsNull(rl.doc_trans_id_tr, IsNull(rl.doc_trans_id_egr, rl.doc_trans_id)) = ddt.doc_trans_id" & vbNewLine
            strsql = strsql & "        And IsNull(rl.nro_linea_trans_tr, IsNull(rl.nro_linea_trans_egr, rl.nro_linea_trans)) = ddt.nro_linea_trans" & vbNewLine
            strsql = strsql & "        And rl.rl_id = " & p_rl_pos_id
            'pcur = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Da.Fill(pcur, "pcur")

            If pcur.Tables("pcur").Rows.Count > 0 Then
                pusuario = vUsrid

                If Not IsDBNull(pcur.Tables("pcur").Rows(0)("doc_trans_id_egr")) Then 'no es un egreso
                    codigo = "TR"
                    codigo2 = "+"
                Else
                    codigo = "EGR"
                    If Not IsDBNull(pcur.Tables("pcur").Rows(0)("nave_actual")) Then
                        Dim vNavActual As Object
                        vNavActual = pcur.Tables("pcur").Rows(0)("nave_actual")
                        strsql = ""
                        strsql = strsql & "SELECT pre_egreso" & vbNewLine
                        strsql = strsql & " FROM nave" & vbNewLine
                        strsql = strsql & " WHERE nave_id =" & pcur.Tables("pcur").Rows(0)("nave_actual")
                        Cmd.CommandText = strsql
                        Da.Fill(rs, "rs")

                        If rs.Tables("rs").Rows.Count > 0 Then
                            es_pre_egreso = rs.Tables("rs").Rows(0)("pre_egreso")
                        End If
                    Else
                        es_pre_egreso = "0"
                    End If
                    If es_pre_egreso = "1" Then
                        codigo2 = "0"
                    Else
                        codigo2 = "+"
                    End If
                End If

                hp_InsertRecord(P_HIST_ID, _
                                     fecha_actual, _
                                     pcur.Tables("pcur").Rows(0)("posicion_anterior"), _
                                     pcur.Tables("pcur").Rows(0)("cantidad"), _
                                     codigo, _
                                     descr, _
                                     pcur.Tables("pcur").Rows(0)("nave_anterior"), _
                                     pcur.Tables("pcur").Rows(0)("documento_id"), _
                                     pcur.Tables("pcur").Rows(0)("nro_linea_doc"), _
                                     pusuario, _
                                     "-", _
                                     pcur.Tables("pcur").Rows(0)("cliente_id"), _
                                     pcur.Tables("pcur").Rows(0)("producto_id"), _
                                     pcur.Tables("pcur").Rows(0)("nro_serie"), _
                                     IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_lote")), Nothing, pcur.Tables("pcur").Rows(0)("nro_lote")), _
                                     IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("fecha_vencimiento")), Nothing, pcur.Tables("pcur").Rows(0)("fecha_vencimiento")), _
                                     IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_partida")), Nothing, pcur.Tables("pcur").Rows(0)("nro_partida")), _
                                     IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_despacho")), Nothing, pcur.Tables("pcur").Rows(0)("nro_despacho")))

                hp_InsertRecord(P_HIST_ID, _
                                     IIf(IsNothing(fecha_actual) Or IsDBNull(fecha_actual), Nothing, fecha_actual), _
                                     pcur.Tables("pcur").Rows(0)("posicion_actual"), _
                                     pcur.Tables("pcur").Rows(0)("cantidad"), _
                                     codigo, _
                                     descr, _
                                     pcur.Tables("pcur").Rows(0)("nave_actual"), _
                                     pcur.Tables("pcur").Rows(0)("documento_id"), _
                                     pcur.Tables("pcur").Rows(0)("nro_linea_doc"), _
                                     pusuario, _
                                     codigo2, _
                                     pcur.Tables("pcur").Rows(0)("cliente_id"), _
                                     pcur.Tables("pcur").Rows(0)("producto_id"), _
                                     pcur.Tables("pcur").Rows(0)("nro_serie"), _
                                     IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_lote")), Nothing, pcur.Tables("pcur").Rows(0)("nro_lote")), _
                                     IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("fecha_vencimiento")), Nothing, pcur.Tables("pcur").Rows(0)("fecha_vencimiento")), _
                                     IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_partida")), Nothing, pcur.Tables("pcur").Rows(0)("nro_partida")), _
                                     IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_despacho")), Nothing, pcur.Tables("pcur").Rows(0)("nro_despacho")))

                hPOS_InsertRecord(P_HIST_ID, _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("posicion_anterior")), Nothing, pcur.Tables("PCUR").Rows(0)("POSICION_ANTERIOR")), _
                                       "EGR", _
                                       fecha_actual, _
                                       IIf(descr = "", Nothing, descr), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("cantidad")), Nothing, pcur.Tables("pcur").Rows(0)("cantidad")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("documento_id")), Nothing, pcur.Tables("pcur").Rows(0)("documento_id")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_linea_doc")), Nothing, pcur.Tables("pcur").Rows(0)("nro_linea_doc")), _
                                       pusuario, _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nave_anterior")), Nothing, pcur.Tables("pcur").Rows(0)("nave_anterior")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("cliente_id")), Nothing, pcur.Tables("pcur").Rows(0)("cliente_id")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("producto_id")), Nothing, pcur.Tables("pcur").Rows(0)("producto_id")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_serie")), Nothing, pcur.Tables("pcur").Rows(0)("nro_serie")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_lote")), Nothing, pcur.Tables("pcur").Rows(0)("nro_lote")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("fecha_vencimiento")), Nothing, pcur.Tables("pcur").Rows(0)("fecha_vencimiento")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_partida")), Nothing, pcur.Tables("pcur").Rows(0)("nro_partida")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_despacho")), Nothing, pcur.Tables("pcur").Rows(0)("nro_despacho")))

                hPOS_InsertRecord(P_HIST_ID, _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("posicion_actual")), Nothing, pcur.Tables("pcur").Rows(0)("posicion_actual")), _
                                       "ING", _
                                       fecha_actual, _
                                       IIf(descr = "", Nothing, descr), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("cantidad")), Nothing, pcur.Tables("pcur").Rows(0)("cantidad")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("documento_id")), Nothing, pcur.Tables("pcur").Rows(0)("documento_id")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_linea_doc")), Nothing, pcur.Tables("pcur").Rows(0)("nro_linea_doc")), _
                                       pusuario, _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nave_actual")), Nothing, pcur.Tables("pcur").Rows(0)("nave_actual")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("cliente_id")), Nothing, pcur.Tables("pcur").Rows(0)("cliente_id")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("producto_id")), Nothing, pcur.Tables("pcur").Rows(0)("producto_id")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_serie")), Nothing, pcur.Tables("pcur").Rows(0)("nro_serie")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_lote")), Nothing, pcur.Tables("pcur").Rows(0)("nro_lote")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("fecha_vencimiento")), Nothing, pcur.Tables("pcur").Rows(0)("fecha_vencimiento")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_partida")), Nothing, pcur.Tables("pcur").Rows(0)("nro_partida")), _
                                       IIf(IsDBNull(pcur.Tables("pcur").Rows(0)("nro_despacho")), Nothing, pcur.Tables("pcur").Rows(0)("nro_despacho")))
            End If
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message & "- Actualizar_Historico_x_Mov", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox(ex.Message & "- Actualizar_Historico_x_Mov", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Da = Nothing
            pcur = Nothing
            rs = Nothing
        End Try
    End Function

    Private Sub hp_InsertRecord(ByVal P_HIST_PROD_ID As Object, ByVal P_FECHA As Object, _
                                ByVal P_POSICION_ID As Object, ByVal P_CANTIDAD As Object, _
                                ByVal P_TIPO_OPERACION_ID As Object, ByVal P_DESCRIPCION As Object, _
                                ByVal P_NAVE_ID As Object, ByVal P_DOCUMENTO_ID As Object, _
                                ByVal P_NRO_LINEA As Object, ByVal P_USUARIO_ID As Object, _
                                ByVal P_SIGNO As Object, ByVal P_CLIENTE_ID As Object, _
                                ByVal P_PRODUCTO_ID As Object, ByVal p_nro_serie As Object, _
                                ByVal p_nro_lote As Object, ByVal p_fecha_vencimiento As Object, _
                                ByVal p_nro_partida As Object, ByVal p_Nro_Despacho As Object)

        Cmd.CommandType = CommandType.Text

        Dim strsql As String
        strsql = ""
        strsql = strsql & "Insert Into HISTORICO_PRODUCTO(" & vbNewLine
        strsql = strsql & "        FECHA," & vbNewLine
        strsql = strsql & "        POSICION_ID," & vbNewLine
        strsql = strsql & "        CANTIDAD," & vbNewLine
        strsql = strsql & "        TIPO_OPERACION_ID," & vbNewLine
        strsql = strsql & "        DESCRIPCION," & vbNewLine
        strsql = strsql & "        NAVE_ID," & vbNewLine
        strsql = strsql & "        DOCUMENTO_ID," & vbNewLine
        strsql = strsql & "        NRO_LINEA," & vbNewLine
        strsql = strsql & "        USUARIO_ID," & vbNewLine
        strsql = strsql & "        SIGNO," & vbNewLine
        strsql = strsql & "        CLIENTE_ID," & vbNewLine
        strsql = strsql & "        PRODUCTO_ID," & vbNewLine
        strsql = strsql & "        NRO_SERIE," & vbNewLine
        strsql = strsql & "        NRO_LOTE," & vbNewLine
        strsql = strsql & "        FECHA_VENCIMIENTO," & vbNewLine
        strsql = strsql & "        NRO_PARTIDA," & vbNewLine
        strsql = strsql & "        NRO_DESPACHO)" & vbNewLine
        strsql = strsql & " Values (" & vbNewLine
        If IsNothing(P_FECHA) Or IsDBNull(P_FECHA) Then
            strsql = strsql & "        Null," & vbNewLine
        Else
            strsql = strsql & "        GETDATE()," & vbNewLine '& Format(P_FECHA, "dd/MM/yyyy hh:mm:ss") & "'," & vbNewLine
        End If
        strsql = strsql & "        " & IIf(IsNothing(P_POSICION_ID) Or IsDBNull(P_POSICION_ID), "null", P_POSICION_ID) & "," & vbNewLine
        strsql = strsql & "        " & IIf(IsNothing(P_CANTIDAD) Or IsDBNull(P_CANTIDAD), "null", CDbl(P_CANTIDAD)) & "," & vbNewLine
        strsql = strsql & "        " & IIf(IsNothing(P_TIPO_OPERACION_ID) Or IsDBNull(P_TIPO_OPERACION_ID), "NULL", "'" & UCase(Trim(P_TIPO_OPERACION_ID)) & "'") & "," & vbNewLine
        strsql = strsql & "        " & IIf(IsNothing(P_DESCRIPCION) Or IsDBNull(P_DESCRIPCION) Or P_DESCRIPCION = "", "NULL", "'" & UCase(Trim(P_DESCRIPCION)) & "'") & "," & vbNewLine
        strsql = strsql & "        " & IIf(IsNothing(P_NAVE_ID) Or IsDBNull(P_NAVE_ID), "NULL", P_NAVE_ID) & "," & vbNewLine
        strsql = strsql & "        " & IIf(IsNothing(P_DOCUMENTO_ID) Or IsDBNull(P_DOCUMENTO_ID), "NULL", P_DOCUMENTO_ID) & "," & vbNewLine
        strsql = strsql & "        " & IIf(IsNothing(P_NRO_LINEA) Or IsDBNull(P_NRO_LINEA), "Null", P_NRO_LINEA) & "," & vbNewLine
        strsql = strsql & "        " & IIf(IsNothing(P_USUARIO_ID) Or IsDBNull(P_USUARIO_ID), "Null", "'" & UCase(Trim(P_USUARIO_ID)) & "'") & "," & vbNewLine
        strsql = strsql & "        " & IIf(IsNothing(P_SIGNO) Or IsDBNull(P_SIGNO), "Null", "'" & UCase(Trim(P_SIGNO)) & "'") & "," & vbNewLine
        strsql = strsql & "        " & IIf(IsNothing(P_CLIENTE_ID) Or IsDBNull(P_CLIENTE_ID), "NULL", "'" & UCase(Trim(P_CLIENTE_ID)) & "'") & "," & vbNewLine
        strsql = strsql & "        " & IIf(IsNothing(P_PRODUCTO_ID) Or IsDBNull(P_PRODUCTO_ID), "NULL", "'" & UCase(Trim(P_PRODUCTO_ID)) & "'") & "," & vbNewLine
        strsql = strsql & "        " & IIf((IsDBNull(p_nro_serie)), "NULL", "'" & p_nro_serie & "'") & "," & vbNewLine
        'strsql = strsql & "        " & IIf(IsNothing(p_nro_serie) Or IsDBNull(p_nro_serie), "NULL", "'" & UCase(Trim(p_nro_serie)) & "'") & "," & vbNewLine
        strsql = strsql & "        " & IIf(IsDBNull(p_nro_lote) Or IsNothing(p_nro_lote), "NULL", "'" & UCase(Trim(p_nro_lote)) & "'") & "," & vbNewLine
        If IsNothing(p_fecha_vencimiento) Or IsDBNull((p_fecha_vencimiento)) Then
            strsql = strsql & "        Null," & vbNewLine
        Else
            strsql = strsql & "        CAST ('" & p_fecha_vencimiento & "' AS DATETIME)" & "," & vbNewLine
        End If
        strsql = strsql & "        " & IIf(IsNothing(p_nro_partida) Or IsDBNull(p_nro_partida), "NULL", "'" & UCase(Trim(p_nro_partida)) & "'") & "," & vbNewLine
        strsql = strsql & "        " & IIf(IsNothing(p_Nro_Despacho) Or IsDBNull(p_Nro_Despacho), "NULL", "'" & UCase(Trim(p_Nro_Despacho)) & "'") & ")"
        Cmd.CommandText = strsql
        Cmd.ExecuteNonQuery()

        P_HIST_PROD_ID = OBTENER_SECUENCIA()
    End Sub

    Private Function OBTENER_SECUENCIA() As Object
        Dim strsql As String
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        Try
            Da = New SqlDataAdapter(Cmd)
            Cmd.CommandType = CommandType.Text
            strsql = ""
            strsql = strsql & "SELECT SCOPE_IDENTITY()" & vbNewLine
            Cmd.CommandText = strsql
            Da.Fill(Ds, "Secuencia")
            Return Ds.Tables("Secuencia").Rows(0)(0)
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return Nothing
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return Nothing
        Finally
            Ds = Nothing
            Da = Nothing
        End Try
    End Function

    Private Sub hPOS_InsertRecord(ByVal P_HIST_POS_ID As Object, ByVal P_POSICION_ID As Object, _
                                    ByVal P_TIPO_OPERACION_ID As Object, ByVal P_FECHA As Object, _
                                     ByVal P_DESCRIPCION As Object, ByVal P_CANTIDAD As Object, _
                                     ByVal P_DOCUMENTO_ID As Object, ByVal P_NRO_LINEA As Object, _
                                     ByVal P_USUARIO_ID As Object, ByVal P_NAVE_ID As Object, _
                                     ByVal P_CLIENTE_ID As Object, _
                                     ByVal P_PRODUCTO_ID As Object, ByVal p_nro_serie As Object, _
                                     ByVal p_nro_lote As Object, ByVal p_fecha_vencimiento As Object, _
                                     ByVal p_nro_partida As Object, ByVal p_Nro_Despacho As Object)

        Dim strsql As String
        Cmd.CommandType = CommandType.Text
        strsql = ""
        strsql = strsql & "Insert Into HISTORICO_POSICION(" & vbNewLine
        strsql = strsql & "        POSICION_ID," & vbNewLine
        strsql = strsql & "        TIPO_OPERACION_ID," & vbNewLine
        strsql = strsql & "        FECHA," & vbNewLine
        strsql = strsql & "        DESCRIPCION," & vbNewLine
        strsql = strsql & "        CANTIDAD," & vbNewLine
        strsql = strsql & "        DOCUMENTO_ID," & vbNewLine
        strsql = strsql & "        NRO_LINEA," & vbNewLine
        strsql = strsql & "        USUARIO_ID," & vbNewLine
        strsql = strsql & "        NAVE_ID," & vbNewLine
        strsql = strsql & "        CLIENTE_ID," & vbNewLine
        strsql = strsql & "        PRODUCTO_ID," & vbNewLine
        strsql = strsql & "        NRO_SERIE," & vbNewLine
        strsql = strsql & "        NRO_LOTE," & vbNewLine
        strsql = strsql & "        FECHA_VENCIMIENTO," & vbNewLine
        strsql = strsql & "        NRO_PARTIDA," & vbNewLine
        strsql = strsql & "        NRO_DESPACHO)" & vbNewLine
        strsql = strsql & " Values (" & vbNewLine
        strsql = strsql & "        " & IIf(IsDBNull(P_POSICION_ID) Or IsNothing(P_POSICION_ID), "Null", UCase(Trim(P_POSICION_ID))) & vbNewLine
        strsql = strsql & "        ," & IIf(IsDBNull(P_TIPO_OPERACION_ID) Or IsNothing(P_TIPO_OPERACION_ID), "Null", "'" & UCase(Trim(P_TIPO_OPERACION_ID)) & "'") & vbNewLine

        If IsNothing(P_FECHA) Then
            strsql = strsql & "        ,Null" & vbNewLine
        Else
            strsql = strsql & "        ,GETDATE() " '& Format(P_FECHA, "dd/MM/yyyy hh:nn:ss") & "'" & vbNewLine
        End If

        strsql = strsql & "        ," & IIf(IsNothing(P_DESCRIPCION), "Null", "'" & UCase(Trim(P_DESCRIPCION)) & "'") & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(P_CANTIDAD), "Null", CDbl((P_CANTIDAD))) & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(P_DOCUMENTO_ID), "Null", "'" & UCase(Trim(P_DOCUMENTO_ID)) & "'") & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(P_NRO_LINEA), "Null", UCase(Trim(P_NRO_LINEA))) & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(P_USUARIO_ID), "Null", "'" & UCase(Trim(P_USUARIO_ID)) & "'") & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(P_NAVE_ID), "Null", UCase(Trim(P_NAVE_ID))) & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(P_CLIENTE_ID), "Null", "'" & UCase(Trim(P_CLIENTE_ID)) & "'") & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(P_PRODUCTO_ID), "Null", "'" & UCase(Trim(P_PRODUCTO_ID)) & "'") & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(p_nro_serie), "Null", "'" & UCase(Trim(p_nro_serie)) & "'") & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(p_nro_lote), "Null", "'" & UCase(Trim(p_nro_lote)) & "'") & vbNewLine
        If IsNothing(p_fecha_vencimiento) Then 'Or (p_fecha_vencimiento = "") Then
            strsql = strsql & "        ,Null" & vbNewLine
        Else
            strsql = strsql & "        ,CAST('" & p_fecha_vencimiento & "' AS DATETIME)" & vbNewLine '" & Format(p_fecha_vencimiento, "dd/MM/yyyy") & "'" & vbNewLine
        End If
        strsql = strsql & "        ," & IIf(IsNothing(p_nro_partida), "Null", "'" & UCase(Trim(p_nro_partida)) & "'") & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(p_Nro_Despacho), "Null", "'" & UCase(Trim(p_Nro_Despacho)) & "'") & vbNewLine
        strsql = strsql & "  )"
        Cmd.CommandText = strsql
        Cmd.CommandType = CommandType.Text
        Cmd.ExecuteNonQuery()
        P_HIST_POS_ID = OBTENER_SECUENCIA()
    End Sub

    Private Function UpdateEstacionActual(ByVal pTransaccionID As Object, ByVal DocTransID As Object, _
                                          ByVal EstacionID As Object, ByVal pOrdenEstacion As Object, _
                                          ByVal Final As Object) As Boolean

        Dim strsql As String
        Dim pcur As New DataSet
        Dim pAux As New DataSet
        Dim Da As SqlDataAdapter
        'Dim fh As New funciones_historicos_api
        'Dim ed As New ent_documento_api
        'Dim fg As New funciones_generales_api

        Dim fecha_actual As Object

        'fh.Conexion = bd
        'ed.Conexion = bd
        'fg.Conexion = bd
        Try
            Da = New SqlDataAdapter(Cmd)
            fecha_actual = Obtener_FechaActual()
            Cmd.CommandType = CommandType.Text
            'ACTUALIZAR LA CATEGORIA DE STOCK.
            Select Case Final
                Case 0
                    strsql = ""
                    strsql = strsql & "Update DOCUMENTO_TRANSACCION" & vbNewLine
                    strsql = strsql & "       Set ESTACION_ACTUAL = " & IIf(IsNothing((EstacionID)) Or IsDBNull(EstacionID), "Null", "'" & UCase(Trim(EstacionID)) & "'") & vbNewLine
                    strsql = strsql & "       ,ORDEN_ESTACION = " & IIf(IsNothing((pOrdenEstacion)) Or IsDBNull(pOrdenEstacion), "Null", UCase(Trim(pOrdenEstacion))) & vbNewLine
                    strsql = strsql & "       ,EST_MOV_ACTUAL = (select Max(ultima_estacion) from RL_DET_DOC_TRANS_POSICION" & vbNewLine
                    strsql = strsql & "                                 WHERE doc_trans_id = " & DocTransID & ")" & vbNewLine
                    strsql = strsql & "       ,IT_MOVER = 0" & vbNewLine
                    strsql = strsql & " Where DOC_TRANS_ID = " & DocTransID & vbNewLine
                    Cmd.CommandText = strsql
                    Da.Fill(pcur, "pcur")

                    '------------------------------------------------
                    '-- Veo si tiene que hacer una ubicacion obligatoria en esta nueva estacion.
                    '-- En caso de que asi sea, ingreso el correspondiente registro en la tabla PENDIENTE_DOC_TRANS
                    '-- para ese documento-transaccion.
                    'Call Comprobar_Si_debe_ubicar(pTransaccionID, DocTransID, EstacionID, pOrdenEstacion)
                    '------------------------------------------------
                    Call ed_Set_Status_Documento_por_TR(DocTransID)

                Case 1 'ultima estacion de un ingreso
                    'Termina la transaccion.
                    strsql = ""
                    strsql = strsql & "Update DOCUMENTO_TRANSACCION" & vbNewLine
                    strsql = strsql & "       SET ESTACION_ACTUAL = null" & vbNewLine
                    strsql = strsql & "           ,STATUS = 'T40'" & vbNewLine
                    strsql = strsql & "           ,FECHA_FIN_GTW = getdate()" & vbNewLine '& IIf(IsDBNull((fecha_actual)) Or IsNothing(fecha_actual), "Null", "'" & Format(fecha_actual, "dd/MM/yyyy hh:nn:ss") & "'") & vbNewLine
                    strsql = strsql & "    WHERE   DOC_TRANS_ID = " & DocTransID & vbNewLine
                    'pcur = bd.Execute(strsql)
                    Cmd.CommandText = strsql
                    Cmd.ExecuteNonQuery()

                    Call ed_Set_Status_Documento_por_TR(DocTransID)

                Case 2 'Ultima estacion de un egreso
                    'Termina la transaccion.
                    strsql = ""
                    strsql = strsql & "Update DOCUMENTO_TRANSACCION" & vbNewLine
                    strsql = strsql & "       SET ESTACION_ACTUAL = null" & vbNewLine
                    strsql = strsql & "           ,STATUS = 'T40'" & vbNewLine
                    strsql = strsql & "           ,FECHA_FIN_GTW = " & IIf(UCase(Trim(fecha_actual)) = "" Or IsNothing(fecha_actual), "Null", "'" & Format(fecha_actual, "dd/MM/yyyy hh:nn:ss") & "'") & vbNewLine
                    strsql = strsql & "     WHERE DOC_TRANS_ID = " & DocTransID & vbNewLine
                    'pcur = bd.Execute(strsql)
                    Cmd.CommandText = strsql
                    Cmd.ExecuteNonQuery()

                    Call ed_Set_Status_Documento_por_TR(DocTransID)



                Case 3 'Ultima estacion de una transferencia
                    'Termina la transaccion.
                    strsql = ""
                    strsql = strsql & "Update DOCUMENTO_TRANSACCION" & vbNewLine
                    strsql = strsql & "       SET ESTACION_ACTUAL = null" & vbNewLine
                    strsql = strsql & "           ,STATUS = 'T40'" & vbNewLine
                    strsql = strsql & "           ,FECHA_FIN_GTW = " & IIf(UCase(Trim(fecha_actual)) = "" Or IsNothing(fecha_actual), "Null", "'" & Format(fecha_actual, "dd/MM/yyyy hh:nn:ss") & "'") & vbNewLine
                    strsql = strsql & "     WHERE DOC_TRANS_ID = " & DocTransID & vbNewLine
                    'pcur = bd.Execute(strsql)
                    Cmd.CommandText = strsql
                    Cmd.ExecuteNonQuery()

                    strsql = ""
                    strsql = strsql & "UPDATE rl_det_doc_trans_posicion" & vbNewLine
                    strsql = strsql & "       SET doc_trans_id_tr = Null," & vbNewLine
                    strsql = strsql & "       nro_linea_trans_tr = Null" & vbNewLine
                    strsql = strsql & " WHERE doc_trans_id_tr = " & DocTransID & vbNewLine
                    Cmd.CommandText = strsql
                    Cmd.ExecuteNonQuery()

                Case 4 'Ultima estacion de un inventario
                    'Termina la transaccion.
                    strsql = ""
                    strsql = strsql & "Update DOCUMENTO_TRANSACCION" & vbNewLine
                    strsql = strsql & "       SET ESTACION_ACTUAL = null" & vbNewLine
                    strsql = strsql & "           ,STATUS = 'T40'" & vbNewLine
                    strsql = strsql & "           ,FECHA_FIN_GTW = GETDATE()" & vbNewLine '& IIf(IsNothing((fecha_actual)) Or IsDBNull(fecha_actual), "Null", "CAST('" & fecha_actual & "AS DATETIME)" & "'") & vbNewLine
                    strsql = strsql & "     WHERE DOC_TRANS_ID = " & DocTransID & vbNewLine
                    Cmd.CommandText = strsql
                    Cmd.ExecuteNonQuery()

            End Select
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message & "- UpdateEstacionActual", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox(ex.Message & "- UpdateEstacionActual", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            pcur = Nothing
            Da = Nothing
            pAux = Nothing
        End Try
    End Function

    Private Sub ed_Set_Status_Documento_por_TR(ByVal P_DOC_TRANS_ID As Object)

        Dim Da As SqlDataAdapter
        Dim strsql As String
        Dim total_finalizados As Long
        Dim total_no_finalizados As Long
        Dim Ds As New DataSet
        Da = New SqlDataAdapter(Cmd)
        'Dim pcur As ADODB.Recordset
        'Dim pcur1 As ADODB.Recordset
        'Dim pcur2 As ADODB.Recordset
        'Dim pcur3 As ADODB.Recordset
        'Dim pcur4 As ADODB.Recordset
        total_finalizados = 0
        total_no_finalizados = 0

        Cmd.CommandType = CommandType.Text
        strsql = ""
        strsql = strsql & " SELECT tipo_operacion_id as v_tipo_operacion_id" & vbNewLine
        strsql = strsql & " From documento_transaccion" & vbNewLine
        strsql = strsql & " Where doc_trans_id =" & P_DOC_TRANS_ID
        'pcur = bd.Execute(strsql)
        Cmd.CommandText = strsql
        Da.Fill(Ds, "pcur")

        If (Ds.Tables("pcur").Rows(0)("v_tipo_operacion_id") <> "TR" And Ds.Tables("pcur").Rows(0)("v_tipo_operacion_id") <> "INV") Then

            strsql = ""
            strsql = strsql & " SELECT DISTINCT ddt.documento_id as v_documento_id" & vbNewLine
            strsql = strsql & " FROM det_documento_transaccion ddt" & vbNewLine
            strsql = strsql & " Where ddt.doc_trans_id =" & P_DOC_TRANS_ID
            'pcur1 = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Da.Fill(Ds, "pcur1")


            strsql = ""
            strsql = strsql & " select count(dt.doc_trans_id) as total_finalizados" & vbNewLine
            strsql = strsql & " FROM documento_transaccion dt," & vbNewLine
            strsql = strsql & "      det_documento_transaccion ddt" & vbNewLine
            strsql = strsql & " Where dt.doc_trans_id = ddt.doc_trans_id" & vbNewLine
            strsql = strsql & "       and ddt.documento_id=" & Ds.Tables("pcur1").Rows(0)("v_documento_id") & vbNewLine
            strsql = strsql & " and dt.status = 'T40' "
            'pcur2 = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Da.Fill(Ds, "pcur2")


            If Ds.Tables("pcur2").Rows.Count > 0 Then
                total_finalizados = Ds.Tables("pcur2").Rows(0)("total_finalizados")
            End If

            strsql = ""
            strsql = strsql & " select count(dt.doc_trans_id) as total_no_finalizados" & vbNewLine
            strsql = strsql & " from documento_transaccion dt," & vbNewLine
            strsql = strsql & "      det_documento_transaccion ddt" & vbNewLine
            strsql = strsql & " Where dt.doc_trans_id = ddt.doc_trans_id" & vbNewLine
            strsql = strsql & "      and ddt.documento_id=" & Ds.Tables("pcur1").Rows(0)("v_documento_id") & vbNewLine
            strsql = strsql & "      and dt.status < 'T40' "
            'pcur3 = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Da.Fill(Ds, "pcur3")

            If Ds.Tables("pcur3").Rows.Count > 0 Then
                total_no_finalizados = Ds.Tables("pcur3").Rows(0)("total_no_finalizados")
            End If

            If (total_finalizados > 0) Then
                If (total_no_finalizados > 0) Then
                    strsql = ""
                    strsql = strsql & " Update documento" & vbNewLine
                    strsql = strsql & " set status = 'D35'" & vbNewLine
                    strsql = strsql & " where documento_id = " & Ds.Tables("pcur1").Rows(0)("v_documento_id")
                    'bd.Execute(strsql)
                    Cmd.CommandText = strsql
                    Cmd.ExecuteNonQuery()

                Else
                    strsql = ""
                    strsql = strsql & " update documento" & vbNewLine
                    strsql = strsql & " set status = 'D40'," & vbNewLine
                    strsql = strsql & "     fecha_fin_gtw = getdate()" & vbNewLine
                    strsql = strsql & " where documento_id = " & Ds.Tables("pcur1").Rows(0)("v_documento_id")
                    'bd.Execute(strsql)
                    Cmd.CommandText = strsql
                    Cmd.ExecuteNonQuery()


                    '*** DRS CHEQUEA SI SE PUEDE CORRER EL PROCESO DE GT-TRANSPORT.
                    strsql = ""
                    strsql = strsql & " SELECT valor as v_flag" & vbNewLine
                    strsql = strsql & " FROM sys_parametro_proceso" & vbNewLine
                    strsql = strsql & " WHERE PROCESO_ID = 'GTT'" & vbNewLine
                    strsql = strsql & "      AND SUBPROCESO_ID = 'INTERFAZ'" & vbNewLine
                    strsql = strsql & "      AND PARAMETRO_ID = 'RUN'" & vbNewLine
                    'pcur4 = bd.Execute(strsql)
                    Cmd.CommandText = strsql
                    Da.Fill(Ds, "pcur4")
                End If
            End If
        End If
    End Sub

    Private Sub Enviar_RL_a_Historico(ByVal pdoc_trans_id As Object, ByVal ptipo_operacion As Object)
        Dim str_sql As String
        Dim aux_where As String
        Dim Da As New SqlDataAdapter(Cmd)
        Dim pcur As New DataSet
        Dim new_rl As Long = 0
        Dim i As Integer
        'Dim fg As New funciones_generales_api

        'fg.Conexion = bd
        Cmd.CommandType = CommandType.Text
        If ptipo_operacion = "ING" Then
            aux_where = "DOC_TRANS_ID = "
        ElseIf (ptipo_operacion = "EGR") Then
            aux_where = "DOC_TRANS_ID_EGR = "
        Else
            aux_where = "DOC_TRANS_ID_TR = "
        End If

        str_sql = ""
        str_sql = str_sql & "SELECT * FROM RL_DET_DOC_TRANS_POSICION" & vbNewLine
        str_sql = str_sql & " WHERE " & aux_where & pdoc_trans_id
        'pcur = bd.Execute(str_sql)

        Cmd.CommandText = str_sql
        Da.Fill(pcur, "pcur")

        str_sql = ""
        str_sql = str_sql & " INSERT INTO RL_DET_DOC_TR_POS_HIST "
        str_sql = str_sql & "   select 	 "
        str_sql = str_sql & "    doc_trans_id"
        str_sql = str_sql & "   ,nro_linea_trans"
        str_sql = str_sql & "   ,posicion_anterior"
        str_sql = str_sql & "   ,posicion_actual"
        str_sql = str_sql & "   ,cantidad"
        str_sql = str_sql & "   ,tipo_movimiento_id"
        str_sql = str_sql & "   ,ultima_estacion"
        str_sql = str_sql & "   ,ultima_secuencia"
        str_sql = str_sql & "   ,nave_anterior"
        str_sql = str_sql & "   ,nave_actual"
        str_sql = str_sql & "   ,documento_id"
        str_sql = str_sql & "   ,nro_linea"
        str_sql = str_sql & "   ,disponible"
        str_sql = str_sql & "   ,doc_trans_id_egr"
        str_sql = str_sql & "   ,nro_linea_trans_egr"
        str_sql = str_sql & "   ,doc_trans_id_tr"
        str_sql = str_sql & "   ,nro_linea_trans_tr"
        str_sql = str_sql & "   ,cliente_id"
        str_sql = str_sql & "   ,cat_log_id"
        str_sql = str_sql & "   ,cat_log_id_final"
        str_sql = str_sql & "   ,est_merc_id"
        str_sql = str_sql & "   FROM RL_DET_DOC_TRANS_POSICION"
        str_sql = str_sql & "   WHERE DOC_TRANS_ID = " & pdoc_trans_id
        'rs = bd.Execute(str_sql)

        Cmd.CommandText = str_sql
        Cmd.ExecuteNonQuery()
        Da = Nothing
        pcur = Nothing

    End Sub

    Private Function Obtener_ID(ByVal tabla As Object, ByVal campo As Object) As Long

        Dim Da As New SqlDataAdapter(Cmd)
        Dim strsql As String
        Dim RSvalor As New DataSet
        Try
            strsql = ""
            strsql = strsql & "SELECT ISNULL(MAX(" & campo & "), 0)+1 as valor" & vbNewLine
            strsql = strsql & "From " & tabla
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strsql
            Da.Fill(RSvalor, "rsvalor")
            If RSvalor.Tables("rsvalor").Rows.Count > 0 Then
                Return RSvalor.Tables("rsvalor").Rows(0)("valor")
            End If
        Catch ex As Exception
            'MsgBox(ex.Message & "- Obtener_ID", MsgBoxStyle.OkOnly, ClsName)
        Finally
            Da = Nothing
            RSvalor = Nothing
        End Try
    End Function

    Private Sub hss_InsertRecord(ByVal P_HIST_ID As Object, ByVal P_FECHA As Object, _
                            ByVal P_CLIENTE_ID As Object, ByVal P_PRODUCTO_ID As Object, _
                            ByVal P_CANT_TR_ING As Object, ByVal P_CANT_STOCK As Object, _
                            ByVal P_CANT_TR_EGR As Object, ByVal P_DOCUMENTO_ID As Object, _
                            ByVal P_NRO_LINEA As Object, ByVal P_DOC_STATUS As Object)

        Dim strsql As String
        strsql = ""
        strsql = strsql & "Insert Into HISTORICO_SALDOS_STOCK(" & vbNewLine
        strsql = strsql & "        Fecha," & vbNewLine
        strsql = strsql & "        CLIENTE_ID," & vbNewLine
        strsql = strsql & "        PRODUCTO_ID," & vbNewLine
        strsql = strsql & "        CANT_TR_ING," & vbNewLine
        strsql = strsql & "        CANT_STOCK," & vbNewLine
        strsql = strsql & "        CANT_TR_EGR," & vbNewLine
        strsql = strsql & "        DOCUMENTO_ID," & vbNewLine
        strsql = strsql & "        NRO_LINEA," & vbNewLine
        strsql = strsql & "        DOC_STATUS)" & vbNewLine
        strsql = strsql & " Values (" & vbNewLine
        If IsNothing(P_FECHA) Then
            strsql = strsql & "        Null" & vbNewLine
        Else
            strsql = strsql & "        getdate()" & vbNewLine '& Format(P_FECHA, "dd/MM/yyyy hh:nn:ss") & "'" & vbNewLine
        End If
        strsql = strsql & "        ," & IIf(IsNothing(P_CLIENTE_ID), "Null", "'" & UCase(Trim(P_CLIENTE_ID)) & "'") & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(P_PRODUCTO_ID), "Null", "'" & UCase(Trim(P_PRODUCTO_ID)) & "'") & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(P_CANT_TR_ING), "Null", CDbl((P_CANT_TR_ING))) & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(P_CANT_STOCK), "Null", CDbl((P_CANT_STOCK))) & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(P_CANT_TR_EGR), "Null", CDbl((P_CANT_TR_EGR))) & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(P_DOCUMENTO_ID), "Null", UCase(Trim(P_DOCUMENTO_ID))) & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(P_NRO_LINEA), "Null", UCase(Trim(P_NRO_LINEA))) & vbNewLine
        strsql = strsql & "        ," & IIf(IsNothing(P_DOC_STATUS), "Null", "'" & UCase(Trim(P_DOC_STATUS)) & "'") & vbNewLine
        strsql = strsql & "        )"
        'bd.Execute(strsql)
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = strsql
        Cmd.ExecuteNonQuery()
        P_HIST_ID = OBTENER_SECUENCIA()
    End Sub

    Private Sub Libera_Transaccion(ByVal pTransaccionID As Object)
        Dim strsql As String
        Dim pcur As New DataSet
        strsql = ""
        strsql = strsql & "Update DOCUMENTO_TRANSACCION" & vbNewLine
        strsql = strsql & "   Set TR_ACTIVO = 0," & vbNewLine
        strsql = strsql & "       TR_ACTIVO_ID = Null," & vbNewLine
        strsql = strsql & "       SESSION_ID = Null," & vbNewLine
        strsql = strsql & "       FECHA_CAMBIO_TR = Null" & vbNewLine
        strsql = strsql & " WHERE DOC_TRANS_ID = " & pTransaccionID & vbNewLine
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = strsql

        Cmd.ExecuteNonQuery()
        BlnLibero = True
        'NOTA
        'El parametro tenia tipo de dato: transaccion.transaccion_id%TYPE que es
        '   varchar(15) y DOC_TRANS_ID es Numeric(20,0)
    End Sub

    Private Function Insertar_Lista_Productos(ByVal PCLIENTE_ID As Object, ByVal PPRODUCTO_ID As Object) As Boolean

        Try
            Dim strsql As String
            Dim P_CLIENTE_ID As Object
            Dim P_PRODUCTO_ID As Object

            P_CLIENTE_ID = PCLIENTE_ID
            P_PRODUCTO_ID = PPRODUCTO_ID

            strsql = "Delete From #TEMP_SALDOS_STOCK"
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()

            'inserta los productos que esten en RL en status D30 ya sea para ingreso o egreso
            'y los que estan en D20 que no tienen RL
            strsql = ""
            strsql = "INSERT INTO #TEMP_SALDOS_STOCK (" & vbNewLine
            strsql = strsql & "        cliente_id," & vbNewLine
            strsql = strsql & "        producto_id," & vbNewLine
            strsql = strsql & "        cant_tr_ing," & vbNewLine
            strsql = strsql & "        cant_stock," & vbNewLine
            strsql = strsql & "        cant_tr_egr)" & vbNewLine
            strsql = strsql & " (SELECT DISTINCT p.cliente_id As Cliente," & vbNewLine
            strsql = strsql & "        p.producto_id As Producto," & vbNewLine
            strsql = strsql & "        0," & vbNewLine
            strsql = strsql & "        0," & vbNewLine
            strsql = strsql & "        0" & vbNewLine
            strsql = strsql & "        FROM producto As p " & vbNewLine
            strsql = strsql & " WHERE 1 <> 0"
            If Not IsNothing(P_CLIENTE_ID) Or Not IsDBNull((P_CLIENTE_ID)) Then
                strsql = strsql & "        AND p.cliente_id = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
            Else
                P_CLIENTE_ID = "1"
                strsql = strsql & "        AND '1' = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine

            End If
            If Not IsNothing(P_PRODUCTO_ID) Or Not IsDBNull((P_PRODUCTO_ID)) Then
                strsql = strsql & "        AND p.producto_id = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine
            Else
                P_PRODUCTO_ID = "1"
                strsql = strsql & "        AND '1' = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine

            End If
            strsql = strsql & ")"
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Insertar_Lista_Productos: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Private Function Obtener_Saldo_TR_ING(ByVal PCLIENTE_ID As Object, ByVal PPRODUCTO_ID As Object, ByVal pcur As DataSet) As Boolean
        Dim Da As SqlDataAdapter
        Try
            Da = New SqlDataAdapter(Cmd)
            Dim strsql As String

            Dim P_CLIENTE_ID As Object
            Dim P_PRODUCTO_ID As Object

            P_CLIENTE_ID = PCLIENTE_ID
            P_PRODUCTO_ID = PPRODUCTO_ID

            'parte 1: items que estan en D20 y no tienen transaccion asignada.
            'parte 2: items que estan en D30 y que tienen transaccion asignada.

            strsql = ""
            strsql = strsql & "SELECT dd.cliente_id," & vbNewLine
            strsql = strsql & "        dd.producto_id," & vbNewLine
            strsql = strsql & "        Sum(IsNull(rl.cantidad, 0)) As cantidad" & vbNewLine
            strsql = strsql & " FROM rl_det_doc_trans_posicion rl, det_documento dd, categoria_logica cl" & vbNewLine
            strsql = strsql & " WHERE rl.documento_id = dd.documento_id" & vbNewLine
            strsql = strsql & "        AND rl.nro_linea = dd.nro_linea" & vbNewLine
            strsql = strsql & "        AND rl.cliente_id=cl.cliente_id" & vbNewLine
            strsql = strsql & "        AND rl.cat_log_id=cl.cat_log_id" & vbNewLine
            strsql = strsql & "        AND cl.categ_stock_id = 'TRAN_ING'" & vbNewLine
            If Not IsNothing(P_CLIENTE_ID) Or Not IsDBNull((P_CLIENTE_ID)) Then
                strsql = strsql & " And dd.cliente_id = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
            Else
                strsql = strsql & " And '1' ='" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
            End If
            If Not IsNothing(P_PRODUCTO_ID) Or Not IsDBNull((P_PRODUCTO_ID)) Then
                strsql = strsql & " And dd.producto_id = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine
            Else
                strsql = strsql & " And '1' = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine
            End If
            strsql = strsql & " GROUP BY dd.cliente_id ,dd.producto_id , cl.categ_stock_id" & vbNewLine

            strsql = strsql & " UNION all" & vbNewLine

            strsql = strsql & " SELECT dd.cliente_id," & vbNewLine
            strsql = strsql & "        dd.producto_id," & vbNewLine
            strsql = strsql & "        Sum(IsNull(rl.cantidad, 0)) AS cantidad" & vbNewLine
            strsql = strsql & " FROM rl_det_doc_trans_posicion rl,det_documento_transaccion  ddt , det_documento dd, categoria_logica cl" & vbNewLine
            strsql = strsql & " WHERE rl.doc_trans_id = ddt.doc_trans_id AND rl.nro_linea_trans = ddt.nro_linea_trans" & vbNewLine
            strsql = strsql & "        AND ddt.documento_id=dd.documento_id  AND ddt.nro_linea_doc = dd.nro_linea" & vbNewLine
            strsql = strsql & "        AND rl.cliente_id=cl.cliente_id AND rl.cat_log_id=cl.cat_log_id" & vbNewLine
            strsql = strsql & "        AND cl.categ_stock_id = 'TRAN_ING'" & vbNewLine
            If Not IsNothing(P_CLIENTE_ID) Or Not IsDBNull((P_CLIENTE_ID)) Then
                strsql = strsql & " And dd.cliente_id = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
            Else
                P_CLIENTE_ID = "1"
                strsql = strsql & " And '1' = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine

            End If
            If Not IsNothing(P_PRODUCTO_ID) Or Not IsDBNull((P_PRODUCTO_ID)) Then
                strsql = strsql & " And dd.producto_id = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine
            Else
                P_PRODUCTO_ID = "1"
                strsql = strsql & " And '1' = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine

            End If
            strsql = strsql & " GROUP BY dd.cliente_id, dd.producto_id, cl.categ_stock_id"
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strsql
            Da.Fill(pcur, "PCUR")
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Obtener_Saldo_TR_ING: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Da = Nothing
        End Try

    End Function

    Private Function Actualizar_Cantidades(ByVal pcur As DataSet, ByVal caso As Object) As Boolean

        Try

            'Dim PCLIENTE_ID As Object
            'Dim PPRODUCTO_ID As Object
            'Dim PCANTIDAD As Long
            Dim campo As String
            Dim msg As String


            If caso = "TR_ING" Then
                campo = "cant_tr_ing"
            ElseIf caso = "TR_EGR" Then
                campo = "cant_tr_egr"
            Else
                campo = "cant_stock"
            End If
            Dim i As Long
            For i = 0 To pcur.Tables("pcur").Rows.Count - 1
                msg = "Update #TEMP_SALDOS_STOCK Set " & campo & " = " & campo & " + " & CDbl(pcur.Tables("pcur").Rows(0)("cantidad")) & vbNewLine
                msg = msg & "   Where cliente_id = '" & pcur.Tables("pcur").Rows(0)("cliente_id") & "'" & vbNewLine
                msg = msg & "   And producto_id = '" & pcur.Tables("pcur").Rows(0)("producto_id") & "'"
                Cmd.CommandType = CommandType.Text
                Cmd.CommandText = msg
                Cmd.ExecuteNonQuery()
            Next
            'vrsOut = pcur
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Actualizar_Cantidades: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Private Function Obtener_Saldo_TR_EGR(ByVal PCLIENTE_ID As Object, ByVal PPRODUCTO_ID As Object, ByVal pcur As DataSet) As Boolean
        Dim Da As SqlDataAdapter
        Try
            Da = New SqlDataAdapter(Cmd)
            Dim strsql As String
            Dim P_CLIENTE_ID As Object
            Dim P_PRODUCTO_ID As Object

            P_CLIENTE_ID = PCLIENTE_ID
            P_PRODUCTO_ID = PPRODUCTO_ID

            'parte 1: items que estan en D20 y no tienen asignada transaccion (serian los reservados)
            'parte 2: items que estan en D30 y que tienen transacc asignada (siguen reservados sin split)
            'parte 3: items que estan en D30 y que ya fueron descontados (se aplico el proceso de split).

            strsql = ""
            strsql = strsql & "SELECT dd.cliente_id," & vbNewLine
            strsql = strsql & "        dd.producto_id," & vbNewLine
            strsql = strsql & "        Sum(IsNull(dd.cantidad, 0)) AS cantidad" & vbNewLine
            strsql = strsql & " FROM det_documento dd," & vbNewLine
            strsql = strsql & "        documento d," & vbNewLine
            strsql = strsql & "        categoria_logica cl" & vbNewLine
            strsql = strsql & " WHERE dd.documento_id  = d.documento_id" & vbNewLine
            strsql = strsql & "        And d.status = 'D20'" & vbNewLine
            strsql = strsql & "        And dd.cliente_id = cl.cliente_id" & vbNewLine
            strsql = strsql & "        And cl.cliente_id = dd.cliente_id" & vbNewLine
            strsql = strsql & "        And dd.cat_log_id = cl.cat_log_id" & vbNewLine
            strsql = strsql & "        And cl.categ_stock_id = 'TRAN_EGR'" & vbNewLine
            If Not IsNothing(P_CLIENTE_ID) Or Not IsDBNull((P_CLIENTE_ID)) Then
                strsql = strsql & "        And dd.cliente_id = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
            Else
                strsql = strsql & "        And '1' = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
            End If
            If Not IsNothing(P_PRODUCTO_ID) Or Not IsDBNull((P_PRODUCTO_ID)) Then
                strsql = strsql & "        And dd.producto_id = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine
            Else
                strsql = strsql & "        And '1' = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine
            End If
            strsql = strsql & " GROUP BY dd.cliente_id, dd.producto_id, cl.categ_stock_id" & vbNewLine

            strsql = strsql & " UNION ALL" & vbNewLine

            strsql = strsql & " SELECT dd.cliente_id," & vbNewLine
            strsql = strsql & "        dd.producto_id," & vbNewLine
            strsql = strsql & "        Sum(IsNull(dd.cantidad, 0)) AS cantidad" & vbNewLine
            strsql = strsql & " FROM  det_documento_transaccion ddt," & vbNewLine
            strsql = strsql & "        det_documento dd," & vbNewLine
            strsql = strsql & "        documento_transaccion dt," & vbNewLine
            strsql = strsql & "        categoria_logica cl" & vbNewLine
            strsql = strsql & " WHERE ddt.cliente_id = cl.cliente_id" & vbNewLine
            strsql = strsql & "        And ddt.cat_log_id = cl.cat_log_id" & vbNewLine
            strsql = strsql & "        And cl.cliente_id = dd.cliente_id" & vbNewLine
            strsql = strsql & "        And cl.categ_stock_id = 'TRAN_EGR'" & vbNewLine
            strsql = strsql & "        And ddt.documento_id = dd.documento_id" & vbNewLine
            strsql = strsql & "        And ddt.nro_linea_doc = dd.nro_linea" & vbNewLine
            strsql = strsql & "        And ddt.doc_trans_id = dt.doc_trans_id" & vbNewLine
            strsql = strsql & "        And dt.status = 'T10'" & vbNewLine
            strsql = strsql & "        And Not Exists (SELECT rl_id FROM rl_det_doc_trans_posicion rl" & vbNewLine
            strsql = strsql & "                             WHERE rl.doc_trans_id_egr = ddt.doc_trans_id" & vbNewLine
            strsql = strsql & "                             And rl.nro_linea_trans_egr = ddt.nro_linea_trans)" & vbNewLine
            If Not IsNothing(P_CLIENTE_ID) Or Not IsDBNull((P_CLIENTE_ID)) Then
                strsql = strsql & "        And dd.cliente_id = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
            Else
                strsql = strsql & "        And '1' = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
            End If
            If Not IsNothing(P_PRODUCTO_ID) Or Not IsDBNull((P_PRODUCTO_ID)) Then
                strsql = strsql & "        And dd.producto_id = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine
            Else
                strsql = strsql & "        And '1' = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine
            End If
            strsql = strsql & " GROUP BY dd.cliente_id, dd.producto_id, cl.categ_stock_id" & vbNewLine

            strsql = strsql & " UNION ALL" & vbNewLine

            strsql = strsql & " SELECT dd.cliente_id," & vbNewLine
            strsql = strsql & "        dd.producto_id," & vbNewLine
            strsql = strsql & "        Sum(IsNull(dd.cantidad, 0)) AS cantidad" & vbNewLine
            strsql = strsql & " FROM  det_documento_transaccion ddt," & vbNewLine
            strsql = strsql & "        det_documento dd," & vbNewLine
            strsql = strsql & "        rl_det_doc_trans_posicion rl," & vbNewLine
            strsql = strsql & "        documento d," & vbNewLine
            strsql = strsql & "        categoria_logica cl" & vbNewLine
            strsql = strsql & " WHERE dd.documento_id = d.documento_id" & vbNewLine
            strsql = strsql & "        And rl.cat_log_id = cl.cat_log_id" & vbNewLine
            strsql = strsql & "        And d.status = 'D30'" & vbNewLine
            strsql = strsql & "        And rl.cliente_id = cl.cliente_id" & vbNewLine
            strsql = strsql & "        And CL.cliente_id = DD.cliente_id" & vbNewLine
            strsql = strsql & "        And cl.categ_stock_id = 'TRAN_EGR'" & vbNewLine
            strsql = strsql & "        And ddt.documento_id = dd.documento_id" & vbNewLine
            strsql = strsql & "        And ddt.nro_linea_doc = dd.nro_linea" & vbNewLine
            strsql = strsql & "        And rl.doc_trans_id_egr = ddt.doc_trans_id" & vbNewLine
            strsql = strsql & "        And rl.nro_linea_trans_egr = ddt.nro_linea_trans" & vbNewLine
            If Not IsNothing(P_CLIENTE_ID) Or Not IsDBNull((P_CLIENTE_ID)) Then
                strsql = strsql & "        And dd.cliente_id = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
            Else
                P_CLIENTE_ID = "1"
                strsql = strsql & "        And '1' = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine

            End If
            If Not IsNothing(P_PRODUCTO_ID) Or Not IsDBNull((P_PRODUCTO_ID)) Then
                strsql = strsql & "        And dd.producto_id = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine
            Else
                P_PRODUCTO_ID = "1"
                strsql = strsql & "        And '1' = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine

            End If
            strsql = strsql & " GROUP BY dd.cliente_id, dd.producto_id, cl.categ_stock_id"
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strsql
            Da.Fill(pcur, "PCUR")
            'pcur = bd.Execute(strsql)
            'vrsOut = pcur
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Obtener_Saldo_TR_EGR: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Da = Nothing
        End Try
    End Function

    Private Function Obtener_Saldo_Stock(ByVal PCLIENTE_ID As Object, ByVal PPRODUCTO_ID As Object, ByVal pcur As DataSet) As Boolean
        Dim Da As SqlDataAdapter
        Try
            Da = New SqlDataAdapter(Cmd)
            Dim strsql As String

            Dim P_CLIENTE_ID As Object
            Dim P_PRODUCTO_ID As Object


            P_CLIENTE_ID = PCLIENTE_ID
            P_PRODUCTO_ID = PPRODUCTO_ID

            strsql = ""
            strsql = strsql & "SELECT dd.cliente_id," & vbNewLine
            strsql = strsql & "        dd.producto_id," & vbNewLine
            strsql = strsql & "        Sum(IsNull(rl.cantidad, 0)) As cantidad" & vbNewLine
            strsql = strsql & " FROM rl_det_doc_trans_posicion rl" & vbNewLine
            strsql = strsql & " Inner Join det_documento_transaccion ddt" & vbNewLine
            strsql = strsql & "        ON (ddt.doc_trans_id = rl.doc_trans_id" & vbNewLine
            strsql = strsql & "        And ddt.nro_linea_trans =rl.nro_linea_trans)" & vbNewLine
            strsql = strsql & " Inner Join det_documento dd" & vbNewLine
            strsql = strsql & "        ON (dd.documento_id = ddt.documento_id" & vbNewLine
            strsql = strsql & "        And dd.nro_linea = ddt.nro_linea_doc)" & vbNewLine
            strsql = strsql & " Inner Join categoria_logica cl" & vbNewLine
            strsql = strsql & "        ON (cl.cliente_id = rl.cliente_id" & vbNewLine
            strsql = strsql & "        And cl.cat_log_id = rl.cat_log_id" & vbNewLine
            strsql = strsql & "        And cl.categ_stock_id = 'STOCK')" & vbNewLine
            If Not IsNothing(P_CLIENTE_ID) Or Not IsDBNull((P_CLIENTE_ID)) Then
                strsql = strsql & " And dd.cliente_id = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
            Else
                P_CLIENTE_ID = "1"
                strsql = strsql & " And '1' = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine

            End If
            If Not IsNothing(P_PRODUCTO_ID) Or Not IsDBNull((P_PRODUCTO_ID)) Then
                strsql = strsql & " And dd.producto_id = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine
            Else
                P_PRODUCTO_ID = "1"
                strsql = strsql & " And '1' = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine

            End If
            strsql = strsql & " GROUP BY dd.cliente_id, dd.producto_id" & vbNewLine
            strsql = strsql & " ORDER BY dd.cliente_id, dd.producto_id"
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strsql
            Da.Fill(pcur, "PCUR")
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Obtener_Saldo_Stock: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Da = Nothing
        End Try
    End Function

    Private Function Actualizar_Saldos_STOCK1() As Boolean
        Try
            Dim strsql As String

            strsql = "Update #TEMP_SALDOS_STOCK Set CANT_STOCK = CANT_STOCK  - CANT_TR_EGR"

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Actualizar_Saldos_STOCK1: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Private Function Actualizar_Saldos_STOCK2(ByVal PCLIENTE_ID As Object, ByVal PPRODUCTO_ID As Object) As Boolean
        Dim Da As SqlDataAdapter
        Dim pcur As DataSet
        Try
            Da = New SqlDataAdapter(Cmd)
            pcur = New DataSet
            Dim strsql As String


            If Not Obtener_ProdTR_Egr_A_Sumar(PCLIENTE_ID, PPRODUCTO_ID, pcur) Then
                Throw New Exception
            End If

            Dim i As Long
            For i = 0 To pcur.Tables("pcur").Rows.Count - 1

                strsql = "Update #TEMP_SALDOS_STOCK Set CANT_STOCK = CANT_STOCK - " & CDbl(pcur.Tables("pcur").Rows(0)("cantidad")) & vbNewLine
                strsql = strsql & "        WHERE cliente_id = '" & pcur.Tables("pcur").Rows(0)("cliente_id") & "'" & vbNewLine
                strsql = strsql & "        And producto_id = '" & pcur.Tables("pcur").Rows(0)("producto_id") & "'"
                Cmd.CommandType = CommandType.Text
                Cmd.CommandText = strsql
                Cmd.ExecuteNonQuery()
            Next
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Actualizar_Saldos_STOCK2: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Da = Nothing
            pcur = Nothing
        End Try
    End Function

    Private Function Obtener_ProdTR_Egr_A_Sumar(ByVal PCLIENTE_ID As Object, ByVal PPRODUCTO_ID As Object, ByVal pcur As DataSet)
        Dim Da As SqlDataAdapter
        Try
            Da = New SqlDataAdapter(Cmd)
            Dim strsql As String
            Dim P_CLIENTE_ID As Object
            Dim P_PRODUCTO_ID As Object

            P_CLIENTE_ID = PCLIENTE_ID
            P_PRODUCTO_ID = PPRODUCTO_ID

            strsql = ""
            strsql = strsql & "SELECT dd.cliente_id," & vbNewLine
            strsql = strsql & "        dd.producto_id," & vbNewLine
            strsql = strsql & "        Sum(IsNull(dd.cantidad, 0)) AS cantidad" & vbNewLine
            strsql = strsql & " FROM det_documento_transaccion ddt," & vbNewLine
            strsql = strsql & "        det_documento dd," & vbNewLine
            strsql = strsql & "        documento d," & vbNewLine
            strsql = strsql & "        categoria_logica cl" & vbNewLine
            strsql = strsql & " WHERE dd.documento_id = d.documento_id" & vbNewLine
            strsql = strsql & "        And d.status = 'D30'" & vbNewLine
            strsql = strsql & "        And dd.cliente_id = cl.cliente_id" & vbNewLine
            strsql = strsql & "        And dd.cat_log_id = cl.cat_log_id" & vbNewLine
            strsql = strsql & "        AND cl.categ_stock_id = 'TRAN_EGR'" & vbNewLine
            strsql = strsql & "        AND ddt.documento_id = dd.documento_id" & vbNewLine
            strsql = strsql & "        AND ddt.nro_linea_doc = dd.nro_linea" & vbNewLine
            If Not IsNothing(P_CLIENTE_ID) Or Not IsDBNull((P_CLIENTE_ID)) Then
                strsql = strsql & "        And dd.cliente_id = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
            Else
                P_CLIENTE_ID = "1"
                strsql = strsql & "        And '1' = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine

            End If
            If Not IsNothing(P_PRODUCTO_ID) Or Not IsDBNull((P_PRODUCTO_ID)) Then
                strsql = strsql & "        And dd.producto_id = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine
            Else
                P_PRODUCTO_ID = "1"
                strsql = strsql & "        And '1' = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine

            End If

            strsql = strsql & " And Exists (SELECT rl_id FROM rl_det_doc_trans_posicion rl" & vbNewLine
            strsql = strsql & "                 WHERE rl.doc_trans_id_egr=ddt.doc_trans_id" & vbNewLine
            strsql = strsql & "                 And rl.nro_linea_trans_egr=ddt.nro_linea_trans)" & vbNewLine
            strsql = strsql & " GROUP BY dd.cliente_id, dd.producto_id, cl.categ_stock_id"
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strsql
            Da.Fill(pcur, "PCUR")
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Obtener_ProdTR_Egr_A_Sumar: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Da = Nothing
        End Try
    End Function

    Public Function Generar_Saldos_Stock(ByVal PCLIENTE_ID As Object, ByVal PPRODUCTO_ID As Object) As Boolean
        Dim rs As DataSet
        Try
            rs = New DataSet

            If Not Insertar_Lista_Productos(PCLIENTE_ID, PPRODUCTO_ID) Then
                Throw New Exception
            End If

            If Not Obtener_Saldo_TR_ING(PCLIENTE_ID, PPRODUCTO_ID, rs) Then
                Throw New Exception
            End If

            If Not Actualizar_Cantidades(rs, "TR_ING") Then
                Throw New Exception
            End If

            If Not Obtener_Saldo_TR_EGR(PCLIENTE_ID, PPRODUCTO_ID, rs) Then
                Throw New Exception
            End If

            If Not Actualizar_Cantidades(rs, "TR_EGR") Then
                Throw New Exception
            End If

            If Not Obtener_Saldo_Stock(PCLIENTE_ID, PPRODUCTO_ID, rs) Then
                Throw New Exception
            End If

            If Not Actualizar_Cantidades(rs, "STOCK") Then
                Throw New Exception
            End If

            If Not Actualizar_Saldos_STOCK1() Then
                Throw New Exception
            End If

            If Not Actualizar_Saldos_STOCK2(PCLIENTE_ID, PPRODUCTO_ID) Then
                Throw New Exception
            End If

            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Generar_Saldos_Stock: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            rs = Nothing
        End Try
    End Function

    Private Function Actualizar_HistSaldos_STOCK(ByVal P_DOCUMENTO_ID As Object, ByVal P_DOC_TRANS_ID As Object, ByVal P_RL_ID As Object) As Boolean
        Dim pcur As DataSet
        Dim pcur2 As DataSet
        Dim Da As SqlDataAdapter
        Dim Da2 As SqlDataAdapter
        Try

            pcur = New DataSet
            pcur2 = New DataSet

            Dim strsql As String

            Dim PHIST_ID As Long
            Dim PCLIENTE_ID As String
            Dim PPRODUCTO_ID As String

            Dim fecha_actual As Object

            Dim p_Documento As Object
            Dim p_Doc_trans As Object
            Dim p_Rl As Object

            fecha_actual = Obtener_FechaActual()

            p_Documento = P_DOCUMENTO_ID
            p_Doc_trans = P_DOC_TRANS_ID
            p_Rl = P_RL_ID
            Da = New SqlDataAdapter(Cmd)
            Cmd.CommandType = CommandType.Text
            strsql = ""
            If Not IsNothing(P_DOCUMENTO_ID) And Not IsDBNull((P_DOCUMENTO_ID)) Then

                strsql = "SELECT dd.cliente_id," & vbNewLine
                strsql = strsql & "       dd.producto_id," & vbNewLine
                strsql = strsql & "       d.documento_id," & vbNewLine
                strsql = strsql & "       dd.nro_linea," & vbNewLine
                strsql = strsql & "       d.status" & vbNewLine
                strsql = strsql & " FROM det_documento dd," & vbNewLine
                strsql = strsql & "       documento d" & vbNewLine
                strsql = strsql & " WHERE dd.documento_id = d.documento_id" & vbNewLine
                strsql = strsql & "       And dd.cliente_id = d.cliente_id" & vbNewLine
                strsql = strsql & "       And d.documento_id = " & P_DOCUMENTO_ID
                'pcur = bd.Execute(strsql)

                Cmd.CommandText = strsql
                Da.Fill(pcur, "PCUR")
            ElseIf Not IsNothing(P_DOC_TRANS_ID) And Not IsDBNull((P_DOC_TRANS_ID)) Then

                strsql = "SELECT dd.cliente_id," & vbNewLine
                strsql = strsql & "       dd.producto_id," & vbNewLine
                strsql = strsql & "       d.documento_id," & vbNewLine
                strsql = strsql & "       dd.nro_linea," & vbNewLine
                strsql = strsql & "       d.status" & vbNewLine
                strsql = strsql & " FROM det_documento_transaccion ddt," & vbNewLine
                strsql = strsql & "       det_documento dd," & vbNewLine
                strsql = strsql & "       documento d" & vbNewLine
                strsql = strsql & " WHERE dd.documento_id = ddt.documento_id" & vbNewLine
                strsql = strsql & "       And ddt.documento_id = d.documento_id" & vbNewLine
                strsql = strsql & "       And dd.nro_linea = ddt.nro_linea_doc" & vbNewLine
                strsql = strsql & "       And ddt.doc_trans_id = " & P_DOC_TRANS_ID
                Cmd.CommandText = strsql
                Da.Fill(pcur, "PCUR")
            ElseIf Not IsNothing(P_RL_ID) And Not IsDBNull((P_RL_ID)) Then

                strsql = "SELECT dd.cliente_id," & vbNewLine
                strsql = strsql & "       dd.producto_id," & vbNewLine
                strsql = strsql & "       d.documento_id," & vbNewLine
                strsql = strsql & "       dd.nro_linea," & vbNewLine
                strsql = strsql & "       d.status" & vbNewLine
                strsql = strsql & " FROM rl_det_doc_trans_posicion rl," & vbNewLine
                strsql = strsql & "       det_documento_transaccion ddt," & vbNewLine
                strsql = strsql & "       det_documento dd," & vbNewLine
                strsql = strsql & "       documento d" & vbNewLine
                strsql = strsql & " WHERE dd.documento_id = ddt.documento_id" & vbNewLine
                strsql = strsql & "       And ddt.documento_id = d.documento_id" & vbNewLine
                strsql = strsql & "       And dd.nro_linea = ddt.nro_linea_doc" & vbNewLine
                strsql = strsql & "       And ddt.doc_trans_id = rl.doc_trans_id" & vbNewLine
                strsql = strsql & "       And ddt.nro_linea_trans = rl.nro_linea_trans" & vbNewLine
                strsql = strsql & "       And rl.rl_id = " & P_RL_ID
                Cmd.CommandText = strsql

                Da.Fill(pcur, "PCUR")
            End If

            Dim i As Long
            Dim e As Long
            For i = 0 To pcur.Tables("PCUR").Rows.Count - 1
                PCLIENTE_ID = pcur.Tables("PCUR").Rows(0)("cliente_id")
                PPRODUCTO_ID = pcur.Tables("PCUR").Rows(0)("producto_id")


                If Not Generar_Saldos_Stock(pcur.Tables("PCUR").Rows(0)("cliente_id"), pcur.Tables("pcur").Rows(0)("producto_id")) Then
                    Throw New Exception
                End If


                strsql = "SELECT * FROM #temp_saldos_stock"
                'pcur2 = bd.Execute(strsql)

                Da2 = New SqlDataAdapter(Cmd)
                Cmd.CommandText = strsql
                Da2.Fill(pcur2, "pcur2")

                For e = 0 To pcur2.Tables("PCUR2").Rows.Count - 1

                    Call hss_InsertRecord(PHIST_ID, _
                                           IIf(IsDBNull(fecha_actual) Or IsNothing(fecha_actual), Nothing, fecha_actual), _
                                           PCLIENTE_ID, _
                                           PPRODUCTO_ID, _
                                           IIf(IsDBNull(pcur2.Tables("PCUR2").Rows(0)("CANT_TR_ING")), Nothing, pcur2.Tables("PCUR2").Rows(0)("CANT_TR_ING")), _
                                           IIf(IsDBNull(pcur2.Tables("PCUR2").Rows(0)("CANT_STOCK")), Nothing, pcur2.Tables("PCUR2").Rows(0)("CANT_STOCK")), _
                                           IIf(IsDBNull(pcur2.Tables("PCUR2").Rows(0)("CANT_TR_EGR")), Nothing, pcur2.Tables("PCUR2").Rows(0)("CANT_TR_EGR")), _
                                           IIf(IsDBNull(pcur.Tables("PCUR").Rows(0)("documento_id")), Nothing, pcur.Tables("PCUR").Rows(0)("documento_id")), _
                                           IIf(IsDBNull(pcur.Tables("PCUR").Rows(0)("nro_linea")), Nothing, pcur.Tables("PCUR").Rows(0)("nro_linea")), _
                                           IIf(IsDBNull(pcur.Tables("PCUR").Rows(0)("status")), Nothing, pcur.Tables("PCUR").Rows(0)("status")))


                Next

            Next
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Actualizar_HistSaldos_STOCK: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            pcur = Nothing
            pcur2 = Nothing
            Da = Nothing
            Da2 = Nothing
        End Try
    End Function

    Private Function Actualizar_HistSaldos_CatLog(ByVal P_DOCUMENTO_ID As Object, ByVal P_DOC_TRANS_ID As Object, ByVal P_RL_ID As Object) As Boolean
        Dim Da As SqlDataAdapter
        Dim Da2 As SqlDataAdapter
        Dim pcur As DataSet
        Dim pcur2 As DataSet
        Try
            Dim strsql As String
            Dim PHIST_ID As Long
            Dim PCLIENTE_ID As String
            Dim PPRODUCTO_ID As String
            Dim fecha_actual As Object
            pcur = New DataSet
            pcur2 = New DataSet

            fecha_actual = Obtener_FechaActual()

            strsql = ""
            If Not IsNothing(P_DOCUMENTO_ID) Then
                ' If Not IsNull(P_DOCUMENTO_ID) Then
                strsql = "SELECT dd.cliente_id," & vbNewLine
                strsql = strsql & "       dd.producto_id," & vbNewLine
                strsql = strsql & "       d.documento_id," & vbNewLine
                strsql = strsql & "       dd.nro_linea," & vbNewLine
                strsql = strsql & "       d.status" & vbNewLine
                strsql = strsql & " FROM det_documento dd," & vbNewLine
                strsql = strsql & "       documento d" & vbNewLine
                strsql = strsql & " WHERE dd.documento_id = d.documento_id" & vbNewLine
                strsql = strsql & "       And dd.cliente_id = d.cliente_id" & vbNewLine
                strsql = strsql & "       And d.documento_id = " & P_DOCUMENTO_ID
                'pcur = bd.Execute(strsql)

                Cmd.CommandText = strsql
                Da = New SqlDataAdapter(Cmd)
                Da.Fill(pcur, "PCUR")

            ElseIf Not IsNothing(P_DOC_TRANS_ID) Then
                strsql = "SELECT dd.cliente_id," & vbNewLine
                strsql = strsql & "       dd.producto_id," & vbNewLine
                strsql = strsql & "       d.documento_id," & vbNewLine
                strsql = strsql & "       dd.nro_linea," & vbNewLine
                strsql = strsql & "       d.status" & vbNewLine
                strsql = strsql & " FROM det_documento_transaccion ddt," & vbNewLine
                strsql = strsql & "       det_documento dd," & vbNewLine
                strsql = strsql & "       documento d" & vbNewLine
                strsql = strsql & " WHERE dd.documento_id = ddt.documento_id" & vbNewLine
                strsql = strsql & "       And ddt.documento_id = d.documento_id" & vbNewLine
                strsql = strsql & "       And dd.nro_linea = ddt.nro_linea_doc" & vbNewLine
                strsql = strsql & "       And ddt.doc_trans_id = " & P_DOC_TRANS_ID

                Cmd.CommandText = strsql
                Da = New SqlDataAdapter(Cmd)
                Da.Fill(pcur, "PCUR")
            ElseIf Not IsNothing(P_RL_ID) Then
                strsql = "SELECT dd.cliente_id," & vbNewLine
                strsql = strsql & "       dd.producto_id," & vbNewLine
                strsql = strsql & "       d.documento_id," & vbNewLine
                strsql = strsql & "       dd.nro_linea," & vbNewLine
                strsql = strsql & "       d.status" & vbNewLine
                strsql = strsql & " FROM rl_det_doc_trans_posicion rl," & vbNewLine
                strsql = strsql & "       det_documento_transaccion ddt," & vbNewLine
                strsql = strsql & "       det_documento dd," & vbNewLine
                strsql = strsql & "       documento d" & vbNewLine
                strsql = strsql & " WHERE dd.documento_id = ddt.documento_id" & vbNewLine
                strsql = strsql & "       And ddt.documento_id = d.documento_id" & vbNewLine
                strsql = strsql & "       And dd.nro_linea = ddt.nro_linea_doc" & vbNewLine
                strsql = strsql & "       And ddt.doc_trans_id = rl.doc_trans_id" & vbNewLine
                strsql = strsql & "       And ddt.nro_linea_trans = rl.nro_linea_trans" & vbNewLine
                strsql = strsql & "       And rl.rl_id = " & P_RL_ID

                Cmd.CommandText = strsql
                Da = New SqlDataAdapter(Cmd)
                Da.Fill(pcur, "PCUR")
            End If
            Dim i As Long
            For i = 0 To pcur.Tables("PCUR").Rows.Count - 1

                PCLIENTE_ID = pcur.Tables("PCUR").Rows(0)("cliente_id")
                PPRODUCTO_ID = pcur.Tables("PCUR").Rows(0)("producto_id")

                If Not Generar_Saldos_CategLog(pcur.Tables("PCUR").Rows(0)("cliente_id"), pcur.Tables("PCUR").Rows(0)("producto_id")) Then
                    Throw New Exception
                End If

                strsql = "SELECT * FROM #TEMP_SALDOS_CATLOG" & vbNewLine
                strsql = strsql & " WHERE cantidad <> 0"
                'pcur2 = bd.Execute(strsql)
                Cmd.CommandText = strsql
                Da2 = New SqlDataAdapter(Cmd)
                Da2.Fill(pcur2, "PCUR2")
                Dim e As Long
                Dim CAT_LOG_ID As Object
                Dim CANTIDAD As Object
                Dim CATEG_STOCK_ID As Object
                Dim documento_id As Object
                Dim nro_linea As Object
                Dim status As Object
                Dim est_merc_id As Object
                For e = 0 To pcur2.Tables("PCUR2").Rows.Count - 1
                    CAT_LOG_ID = IIf(IsDBNull(pcur2.Tables("PCUR2").Rows(e)("CAT_LOG_ID")), Nothing, pcur2.Tables("PCUR2").Rows(e)("CAT_LOG_ID"))
                    CANTIDAD = IIf(IsDBNull(pcur2.Tables("PCUR2").Rows(e)("CANTIDAD")), Nothing, CDbl(pcur2.Tables("PCUR2").Rows(e)("CANTIDAD")))
                    CATEG_STOCK_ID = IIf(IsDBNull(pcur2.Tables("PCUR2").Rows(e)("CATEG_STOCK_ID")), Nothing, pcur2.Tables("PCUR2").Rows(e)("CATEG_STOCK_ID").ToString)
                    documento_id = DocumentoID
                    nro_linea = NroLinea
                    status = Nothing
                    est_merc_id = IIf(IsDBNull(pcur2.Tables("PCUR2").Rows(e)("est_merc_id")), Nothing, pcur2.Tables("PCUR2").Rows(e)("est_merc_id").ToString)
                    
                    hscInsertRecord(PHIST_ID, fecha_actual, PCLIENTE_ID, PPRODUCTO_ID, CAT_LOG_ID, CANTIDAD, CATEG_STOCK_ID, _
                                    documento_id, nro_linea, status, est_merc_id)

                Next
            Next
            Return True
        Catch SQLEx As SqlException
            'MsgBox("Actualizar_HistSaldos_CatLog SQLServer: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Actualizar_HistSaldos_CatLog: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            pcur = Nothing
            pcur2 = Nothing
            Da = Nothing
            Da2 = Nothing
        End Try

    End Function

    Private Function Generar_Saldos_CategLog(ByVal PCLIENTE_ID As Object, ByVal PPRODUCTO_ID As Object) As Boolean
        Try
            Dim strsql As String

            If Not Insertar_Lista_ProdCatLogING(PCLIENTE_ID, PPRODUCTO_ID) Then
                Throw New Exception
            End If

            If Not Insertar_Lista_ProdCatLogEGR(PCLIENTE_ID, PPRODUCTO_ID) Then
                Throw New Exception
            End If

            If Not Actualizar_Saldos_CatLog1() Then
                Throw New Exception
            End If

            If Not Actualizar_Saldos_CatLog2(PCLIENTE_ID, PPRODUCTO_ID) Then
                Throw New Exception
            End If

            'ELIMINO LAS LINEAS QUE QUEDARON CON VALOR 0
            strsql = "DELETE FROM #TEMP_SALDOS_CATLOG WHERE CANTIDAD = 0"
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("BorrarDocTREgreso: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False

        End Try
    End Function

    Private Function Insertar_Lista_ProdCatLogING(ByVal PCLIENTE_ID As Object, ByVal PPRODUCTO_ID As Object) As Boolean
        Try


            Dim strsql As String

            Dim P_CLIENTE_ID As Object
            Dim P_PRODUCTO_ID As Object


            P_CLIENTE_ID = PCLIENTE_ID
            P_PRODUCTO_ID = PPRODUCTO_ID

            strsql = "DELETE FROM #TEMP_SALDOS_CATLOG"
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()

            'La primera parte ingresa los items que estan en transito de ingreso con status = D20
            'La segunda parte ingresa todos los items que estan en stock o en transito de ingreso con status > D20
            strsql = ""
            strsql = "INSERT INTO #TEMP_SALDOS_CATLOG (" & vbNewLine
            strsql = strsql & "         CLIENTE_ID," & vbNewLine
            strsql = strsql & "         PRODUCTO_ID," & vbNewLine
            strsql = strsql & "         CAT_LOG_ID," & vbNewLine
            strsql = strsql & "         CATEG_STOCK_ID," & vbNewLine
            strsql = strsql & "         CANTIDAD," & vbNewLine
            strsql = strsql & "         EST_MERC_ID" & vbNewLine
            strsql = strsql & "         )" & vbNewLine
            strsql = strsql & " (SELECT DISTINCT " & vbNewLine
            strsql = strsql & "         DD.CLIENTE_ID," & vbNewLine
            strsql = strsql & "         DD.PRODUCTO_ID," & vbNewLine
            strsql = strsql & "         dd.CAT_LOG_ID_FINAL AS CATEGORIA_LOGICA," & vbNewLine
            strsql = strsql & "         CL.CATEG_STOCK_ID," & vbNewLine
            strsql = strsql & "         SUM(RL.CANTIDAD)," & vbNewLine
            strsql = strsql & "         RL.EST_MERC_ID" & vbNewLine
            strsql = strsql & "    FROM RL_DET_DOC_TRANS_POSICION RL," & vbNewLine
            strsql = strsql & "         DET_DOCUMENTO_TRANSACCION DDT," & vbNewLine
            strsql = strsql & "         DET_DOCUMENTO DD," & vbNewLine
            strsql = strsql & "         CATEGORIA_LOGICA CL" & vbNewLine
            strsql = strsql & "   WHERE RL.DOC_TRANS_ID = DDT.DOC_TRANS_ID" & vbNewLine
            strsql = strsql & "     AND RL.NRO_LINEA_TRANS = DDT.NRO_LINEA_TRANS" & vbNewLine
            strsql = strsql & "     AND DD.DOCUMENTO_ID = DDT.DOCUMENTO_ID" & vbNewLine
            strsql = strsql & "     AND DD.NRO_LINEA = DDT.NRO_LINEA_DOC" & vbNewLine
            strsql = strsql & "     AND RL.CLIENTE_ID = CL.CLIENTE_ID" & vbNewLine
            strsql = strsql & "     AND RL.CAT_LOG_ID = CL.CAT_LOG_ID" & vbNewLine
            strsql = strsql & "     AND CL.CATEG_STOCK_ID <> 'TRAN_EGR'" & vbNewLine

            If Not IsNothing(P_CLIENTE_ID) Then
                strsql = strsql & "     AND DD.CLIENTE_ID = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
            Else
                'ORIGINAL strSQL = strSQL & "     AND FUNCIONES_GENERALES_API.CLIENTEENUSUARIO(DD.CLIENTE_ID,FUNCIONES_GENERALES_API.GETUSUARIOACTIVO) = 1" & vbNewLine
                strsql = strsql & "     AND "
                strsql = strsql & "        (SELECT CASE WHEN (Count(cliente_id)) > 0 THEN 1 ELSE 0 END" & vbNewLine
                strsql = strsql & "         FROM   rl_sys_cliente_usuario" & vbNewLine
                strsql = strsql & "         WHERE  cliente_id = dd.cliente_id" & vbNewLine
                strsql = strsql & "         And    usuario_id = '" & UCase(Trim(vUsr.CodUsuario)) & "') = 1"

                P_CLIENTE_ID = "1"
                strsql = strsql & "     AND '1' = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine

            End If
            If Not IsNothing(P_PRODUCTO_ID) Then
                strsql = strsql & "     AND DD.PRODUCTO_ID = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine
            Else
                P_PRODUCTO_ID = "1"
                strsql = strsql & "     AND '1' = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine

            End If
            strsql = strsql & " GROUP BY dd.cliente_id," & vbNewLine
            strsql = strsql & "          dd.producto_id," & vbNewLine
            strsql = strsql & "          DD.cat_log_id_final," & vbNewLine
            strsql = strsql & "          cl.categ_stock_id," & vbNewLine
            strsql = strsql & "          rl.est_merc_id" & vbNewLine
            strsql = strsql & " )"
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()

            strsql = ""
            strsql = "INSERT INTO #TEMP_SALDOS_CATLOG" & vbNewLine
            strsql = strsql & "         (" & vbNewLine
            strsql = strsql & "         CLIENTE_ID," & vbNewLine
            strsql = strsql & "         PRODUCTO_ID," & vbNewLine
            strsql = strsql & "         CAT_LOG_ID," & vbNewLine
            strsql = strsql & "         CATEG_STOCK_ID," & vbNewLine
            strsql = strsql & "         CANTIDAD," & vbNewLine
            strsql = strsql & "         EST_MERC_ID" & vbNewLine
            strsql = strsql & "         )" & vbNewLine
            strsql = strsql & " (SELECT DISTINCT " & vbNewLine
            strsql = strsql & "         DD.CLIENTE_ID," & vbNewLine
            strsql = strsql & "         DD.PRODUCTO_ID," & vbNewLine
            strsql = strsql & "         DD.CAT_LOG_ID_FINAL CATEGORIA_LOGICA," & vbNewLine
            strsql = strsql & "         CL.CATEG_STOCK_ID," & vbNewLine
            strsql = strsql & "         SUM(RL.CANTIDAD)," & vbNewLine
            strsql = strsql & "         DD.EST_MERC_ID" & vbNewLine
            strsql = strsql & "    FROM RL_DET_DOC_TRANS_POSICION RL," & vbNewLine
            strsql = strsql & "         DET_DOCUMENTO DD," & vbNewLine
            strsql = strsql & "         CATEGORIA_LOGICA CL" & vbNewLine
            strsql = strsql & "   WHERE RL.DOCUMENTO_ID = DD.DOCUMENTO_ID" & vbNewLine
            strsql = strsql & "     AND RL.NRO_LINEA = DD.NRO_LINEA" & vbNewLine
            strsql = strsql & "     AND RL.CLIENTE_ID = CL.CLIENTE_ID" & vbNewLine
            strsql = strsql & "     AND RL.CAT_LOG_ID = CL.CAT_LOG_ID" & vbNewLine

            If Not IsNothing(PCLIENTE_ID) Then
                strsql = strsql & "     AND DD.CLIENTE_ID = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
            Else
                'ORIGINAL strSQL = strSQL & "     AND FUNCIONES_GENERALES_API.CLIENTEENUSUARIO(DD.CLIENTE_ID,FUNCIONES_GENERALES_API.GETUSUARIOACTIVO) = 1" & vbNewLine
                strsql = strsql & "     AND "
                strsql = strsql & "        (SELECT CASE WHEN (Count(cliente_id)) > 0 THEN 1 ELSE 0 END" & vbNewLine
                strsql = strsql & "         FROM   rl_sys_cliente_usuario" & vbNewLine
                strsql = strsql & "         WHERE  cliente_id = dd.cliente_id" & vbNewLine
                strsql = strsql & "         And    usuario_id = '" & UCase(Trim(vUsr.CodUsuario)) & "') = 1"

                P_CLIENTE_ID = "1"
                strsql = strsql & "     AND '1' = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine

            End If

            If Not IsNothing(PPRODUCTO_ID) Then
                strsql = strsql & "     AND DD.PRODUCTO_ID = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine
            Else
                P_PRODUCTO_ID = "1"
                strsql = strsql & "     AND '1' = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine

            End If

            strsql = strsql & "     AND NOT EXISTS (" & vbNewLine
            strsql = strsql & "                 SELECT *" & vbNewLine
            strsql = strsql & "                   FROM #TEMP_SALDOS_CATLOG T" & vbNewLine
            strsql = strsql & "                  WHERE T.CLIENTE_ID = DD.CLIENTE_ID" & vbNewLine
            strsql = strsql & "                    AND T.PRODUCTO_ID = DD.PRODUCTO_ID" & vbNewLine
            strsql = strsql & "                    AND T.CAT_LOG_ID = DD.CAT_LOG_ID" & vbNewLine
            strsql = strsql & "                    AND T.CATEG_STOCK_ID = CL.CATEG_STOCK_ID" & vbNewLine
            strsql = strsql & "                    AND T.EST_MERC_ID = DD.EST_MERC_ID" & vbNewLine
            strsql = strsql & "                 )" & vbNewLine
            strsql = strsql & "  GROUP BY DD.CLIENTE_ID," & vbNewLine
            strsql = strsql & "           DD.PRODUCTO_ID," & vbNewLine
            strsql = strsql & "           DD.CAT_LOG_ID_FINAL," & vbNewLine
            strsql = strsql & "           CL.CATEG_STOCK_ID," & vbNewLine
            strsql = strsql & "           DD.EST_MERC_ID" & vbNewLine
            strsql = strsql & "  )"

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Insertar_Lista_ProdCatLogING: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Private Function Insertar_Lista_ProdCatLogEGR(ByVal PCLIENTE_ID As Object, ByVal PPRODUCTO_ID As Object) As Boolean
        Try
            Dim StrSql1 As String = ""
            Dim StrSql2 As String = ""
            Dim StrSql3 As String = ""
            Dim strSQL3b As String = ""
            Dim StrSql4 As String = ""
            Dim strSQLW As String = ""

            Dim P_CLIENTE_ID As String = ""
            Dim P_PRODUCTO_ID As String = ""

            'parte1 : crea registros ficticios para hacer el join , iniciando cantidad en 0
            'parte2 : todos los doc de egr que estan en D20
            'parte3 : todos los doc de egr que estan en D30 y no se les hizo split
            'parte4 : todos los doc de egr que estan en D30 y que se les hizo split

            StrSql1 = "INSERT INTO #TEMP_SALDOS_CATLOG (cliente_id," & vbNewLine
            StrSql1 = StrSql1 & "                      producto_id," & vbNewLine
            StrSql1 = StrSql1 & "                      cat_log_id," & vbNewLine
            StrSql1 = StrSql1 & "                      categ_stock_id," & vbNewLine
            StrSql1 = StrSql1 & "                      cantidad," & vbNewLine
            StrSql1 = StrSql1 & "                      est_merc_id)" & vbNewLine
            StrSql1 = StrSql1 & " (SELECT cliente_id," & vbNewLine
            StrSql1 = StrSql1 & "         producto_id," & vbNewLine
            StrSql1 = StrSql1 & "         cat_log_id_final," & vbNewLine
            StrSql1 = StrSql1 & "         categ_stock_id," & vbNewLine
            StrSql1 = StrSql1 & "         Sum(cantidad) AS CANTIDAD," & vbNewLine
            StrSql1 = StrSql1 & "         est_merc_id" & vbNewLine
            StrSql1 = StrSql1 & "    FROM" & vbNewLine
            StrSql1 = StrSql1 & "  (SELECT dd.cliente_id," & vbNewLine
            StrSql1 = StrSql1 & "          dd.producto_id," & vbNewLine
            StrSql1 = StrSql1 & "          dd.cat_log_id_final," & vbNewLine
            StrSql1 = StrSql1 & "          cl.categ_stock_id," & vbNewLine
            StrSql1 = StrSql1 & "          Sum(dd.cantidad) AS cantidad," & vbNewLine
            StrSql1 = StrSql1 & "          dd.est_merc_id" & vbNewLine
            StrSql1 = StrSql1 & "     FROM documento d," & vbNewLine
            StrSql1 = StrSql1 & "          det_documento dd," & vbNewLine
            StrSql1 = StrSql1 & "          categoria_logica cl" & vbNewLine
            StrSql1 = StrSql1 & "    WHERE dd.documento_id = d.documento_id" & vbNewLine
            StrSql1 = StrSql1 & "      AND dd.cliente_id = cl.cliente_id" & vbNewLine
            StrSql1 = StrSql1 & "      AND dd.cat_log_id = cl.cat_log_id" & vbNewLine
            StrSql1 = StrSql1 & "      AND d.status = 'D20'" & vbNewLine
            StrSql1 = StrSql1 & "      AND cl.categ_stock_id = 'TRAN_EGR'" & vbNewLine

            StrSql2 = " GROUP BY dd.cliente_id," & vbNewLine
            StrSql2 = StrSql2 & "          dd.producto_id," & vbNewLine
            StrSql2 = StrSql2 & "          dd.producto_id," & vbNewLine
            StrSql2 = StrSql2 & "          dd.cat_log_id_final," & vbNewLine
            StrSql2 = StrSql2 & "          cl.categ_stock_id," & vbNewLine
            StrSql2 = StrSql2 & "          dd.est_merc_id" & vbNewLine
            StrSql2 = StrSql2 & " UNION ALL" & vbNewLine
            StrSql2 = StrSql2 & "   SELECT dd.cliente_id," & vbNewLine
            StrSql2 = StrSql2 & "          dd.producto_id," & vbNewLine
            StrSql2 = StrSql2 & "          dd.cat_log_id_final," & vbNewLine
            StrSql2 = StrSql2 & "          cl.categ_stock_id," & vbNewLine
            StrSql2 = StrSql2 & "          Sum(dd.cantidad)," & vbNewLine
            StrSql2 = StrSql2 & "          dd.est_merc_id" & vbNewLine
            StrSql2 = StrSql2 & "     FROM documento d," & vbNewLine
            StrSql2 = StrSql2 & "          det_documento dd," & vbNewLine
            StrSql2 = StrSql2 & "          categoria_logica cl," & vbNewLine
            StrSql2 = StrSql2 & "          det_documento_transaccion ddt," & vbNewLine
            StrSql2 = StrSql2 & "          documento_Transaccion dt" & vbNewLine
            StrSql2 = StrSql2 & "    WHERE ddt.documento_id = dd.documento_id" & vbNewLine
            StrSql2 = StrSql2 & "      AND ddt.nro_linea_doc = dd.nro_linea" & vbNewLine
            StrSql2 = StrSql2 & "      AND dd.documento_id = d.documento_id" & vbNewLine
            StrSql2 = StrSql2 & "      AND dd.cliente_id = cl.cliente_id" & vbNewLine
            StrSql2 = StrSql2 & "      AND dd.cat_log_id = cl.cat_log_id" & vbNewLine
            StrSql2 = StrSql2 & "      AND dt.doc_trans_id = ddt.doc_trans_id" & vbNewLine

            StrSql3 = "      AND d.status = 'D30'" & vbNewLine
            StrSql3 = StrSql3 & "      AND dt.status = 'T10'" & vbNewLine
            StrSql3 = StrSql3 & "      AND cl.categ_stock_id = 'TRAN_EGR'" & vbNewLine
            StrSql3 = StrSql3 & "      and not EXISTS (SELECT rl_id" & vbNewLine
            StrSql3 = StrSql3 & "                        FROM rl_det_doc_trans_posicion rl" & vbNewLine
            StrSql3 = StrSql3 & "                       WHERE rl.doc_trans_id_egr = ddt.doc_trans_id" & vbNewLine
            StrSql3 = StrSql3 & "                         AND rl.nro_linea_trans_egr = ddt.nro_linea_trans)" & vbNewLine
            StrSql3 = StrSql3 & "                    GROUP BY dd.cliente_id," & vbNewLine
            StrSql3 = StrSql3 & "                             dd.producto_id," & vbNewLine
            StrSql3 = StrSql3 & "                             dd.cat_log_id_final," & vbNewLine
            StrSql3 = StrSql3 & "                             cl.categ_stock_id," & vbNewLine
            StrSql3 = StrSql3 & "                             dd.est_merc_id" & vbNewLine

            strSQL3b = " UNION ALL" & vbNewLine
            strSQL3b = strSQL3b & "    SELECT dd.cliente_id," & vbNewLine
            strSQL3b = strSQL3b & "           dd.producto_id," & vbNewLine
            strSQL3b = strSQL3b & "           dd.cat_log_id_final," & vbNewLine
            strSQL3b = strSQL3b & "           cl.categ_stock_id," & vbNewLine
            strSQL3b = strSQL3b & "           Sum(RL.cantidad)," & vbNewLine
            strSQL3b = strSQL3b & "           rl.est_merc_id" & vbNewLine
            strSQL3b = strSQL3b & "      FROM documento d," & vbNewLine
            strSQL3b = strSQL3b & "           det_documento dd," & vbNewLine
            strSQL3b = strSQL3b & "           categoria_logica cl," & vbNewLine
            strSQL3b = strSQL3b & "           det_documento_transaccion ddt," & vbNewLine
            strSQL3b = strSQL3b & "           rl_det_doc_trans_posicion rl" & vbNewLine
            strSQL3b = strSQL3b & "    WHERE rl.cliente_id = cl.cliente_id" & vbNewLine
            strSQL3b = strSQL3b & "      AND rl.cat_log_id = cl.cat_log_id" & vbNewLine
            strSQL3b = strSQL3b & "      AND rl.doc_trans_id_egr = ddt.doc_trans_id" & vbNewLine
            strSQL3b = strSQL3b & "      AND rl.nro_linea_trans_egr = ddt.nro_linea_trans" & vbNewLine
            strSQL3b = strSQL3b & "      AND ddt.documento_id = dd.documento_id" & vbNewLine
            strSQL3b = strSQL3b & "      AND ddt.nro_linea_doc = dd.nro_linea" & vbNewLine
            strSQL3b = strSQL3b & "      AND dd.documento_id = d.documento_id" & vbNewLine
            strSQL3b = strSQL3b & "      AND d.status = 'D30'" & vbNewLine
            strSQL3b = strSQL3b & "      AND cl.categ_stock_id = 'TRAN_EGR'" & vbNewLine

            StrSql4 = " GROUP BY dd.cliente_id," & vbNewLine
            StrSql4 = StrSql4 & "          dd.producto_id," & vbNewLine
            StrSql4 = StrSql4 & "          dd.cat_log_id_final," & vbNewLine
            StrSql4 = StrSql4 & "          cl.categ_stock_id," & vbNewLine
            StrSql4 = StrSql4 & "          rl.est_merc_id" & vbNewLine
            StrSql4 = StrSql4 & " ) T1" & vbNewLine
            StrSql4 = StrSql4 & " GROUP BY cliente_id," & vbNewLine
            StrSql4 = StrSql4 & "          producto_id," & vbNewLine
            StrSql4 = StrSql4 & "          cat_log_id_final," & vbNewLine
            StrSql4 = StrSql4 & "          categ_stock_id," & vbNewLine
            StrSql4 = StrSql4 & "          est_merc_id" & vbNewLine
            StrSql4 = StrSql4 & " )" & vbNewLine

            If Not IsNothing(PCLIENTE_ID) Then
                strSQLW = strSQLW & "      AND dd.cliente_id = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
                P_CLIENTE_ID = PCLIENTE_ID
            Else
                'ORIGINAL strSQL1 = strSQL1 & "      AND funciones_generales_api.ClienteEnUsuario(dd.cliente_id, " & fg.GetUsuarioActivo & ") = 1" & vbNewLine
                StrSql1 = StrSql1 & "     AND "
                StrSql1 = StrSql1 & "        (SELECT CASE WHEN (Count(cliente_id)) > 0 THEN 1 ELSE 0 END" & vbNewLine
                StrSql1 = StrSql1 & "         FROM   rl_sys_cliente_usuario" & vbNewLine
                StrSql1 = StrSql1 & "         WHERE  cliente_id = dd.cliente_id" & vbNewLine
                StrSql1 = StrSql1 & "         And    usuario_id = '" & UCase(Trim(vUsr.CodUsuario)) & "') = 1"

                'ORIGINAL strSQL2 = strSQL2 & "      AND funciones_generales_api.ClienteEnUsuario(dd.cliente_id, " & fg.GetUsuarioActivo & ") = 1" & vbNewLine
                StrSql2 = StrSql2 & "     AND "
                StrSql2 = StrSql2 & "        (SELECT CASE WHEN (Count(cliente_id)) > 0 THEN 1 ELSE 0 END" & vbNewLine
                StrSql2 = StrSql2 & "         FROM   rl_sys_cliente_usuario" & vbNewLine
                StrSql2 = StrSql2 & "         WHERE  cliente_id = dd.cliente_id" & vbNewLine
                StrSql2 = StrSql2 & "         And    usuario_id = '" & UCase(Trim(vUsr.CodUsuario)) & "') = 1"

                'ORIGINAL strSQL3b = strSQL3b & "      AND funciones_generales_api.ClienteEnUsuario(dd.cliente_id, " & fg.GetUsuarioActivo & ") = 1" & vbNewLine
                strSQLW = strSQLW & "     AND "
                strSQLW = strSQLW & "        (SELECT CASE WHEN (Count(cliente_id)) > 0 THEN 1 ELSE 0 END" & vbNewLine
                strSQLW = strSQLW & "         FROM   rl_sys_cliente_usuario" & vbNewLine
                strSQLW = strSQLW & "         WHERE  cliente_id = dd.cliente_id" & vbNewLine
                strSQLW = strSQLW & "         And    usuario_id = '" & UCase(Trim(vUsr.CodUsuario)) & "') = '1'"

                P_CLIENTE_ID = "1"
                strSQLW = strSQLW & "      AND '1' = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine

            End If

            If Not IsNothing(PPRODUCTO_ID) Then
                P_PRODUCTO_ID = PPRODUCTO_ID
                strSQLW = strSQLW & "      AND dd.producto_id = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine

            Else
                P_PRODUCTO_ID = "1"
                strSQLW = strSQLW & "      AND '1' = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine

            End If

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = StrSql1 & strSQLW & StrSql2 & strSQLW & StrSql3 & strSQL3b & strSQLW & StrSql4
            Cmd.ExecuteNonQuery()
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Insertar_Lista_ProdCatLogEGR: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Private Function Actualizar_Saldos_CatLog1() As Boolean
        Dim Da As SqlDataAdapter
        Dim pcur As New DataSet
        Try


            Dim strsql As String
            Dim PCLIENTE_ID As String = ""
            Dim PPRODUCTO_ID As String = ""

            strsql = ""
            strsql = strsql & "SELECT t2.cliente_id," & vbNewLine
            strsql = strsql & "       t2.producto_id," & vbNewLine
            strsql = strsql & "       (t2.cantidad - IsNull(t1.cantidad, 0)) AS saldo," & vbNewLine
            strsql = strsql & "       t2.cat_log_id," & vbNewLine
            strsql = strsql & "       t2.est_merc_id" & vbNewLine
            strsql = strsql & " FROM #TEMP_SALDOS_CATLOG t1," & vbNewLine
            strsql = strsql & "       #TEMP_SALDOS_CATLOG t2" & vbNewLine
            strsql = strsql & " WHERE t1.categ_stock_id = 'TRAN_EGR'" & vbNewLine
            strsql = strsql & "       And t2.categ_stock_id = 'STOCK'" & vbNewLine
            strsql = strsql & "       And t2.cat_log_id = t1.cat_log_id" & vbNewLine
            strsql = strsql & "       And t2.EST_MERC_ID = t1.EST_MERC_ID" & vbNewLine
            strsql = strsql & "       And t2.CLIENTE_ID = t1.CLIENTE_ID" & vbNewLine
            strsql = strsql & "       And t2.PRODUCTO_ID = t1.PRODUCTO_ID"
            'pcur = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Da = New SqlDataAdapter(Cmd)
            Da.Fill(pcur, "PCUR")
            Dim i As Long
            For i = 0 To pcur.Tables("PCUR").Rows.Count - 1
                strsql = ""
                strsql = strsql & "UPDATE #TEMP_SALDOS_CATLOG SET cantidad = " & pcur.Tables("pcur").Rows(0)("saldo") & vbNewLine
                strsql = strsql & " WHERE cliente_id = '" & pcur.Tables("pcur").Rows(0)("cliente_id") & "'" & vbNewLine
                strsql = strsql & "       And producto_id = '" & pcur.Tables("pcur").Rows(0)("producto_id") & "'" & vbNewLine
                strsql = strsql & "       And categ_stock_id = 'STOCK'" & vbNewLine
                strsql = strsql & "       And cat_log_id = '" & pcur.Tables("pcur").Rows(0)("cat_log_id") & "'" & vbNewLine
                strsql = strsql & "       And est_merc_id = '" & pcur.Tables("pcur").Rows(0)("est_merc_id") & "'"

                Cmd.CommandType = CommandType.Text
                Cmd.CommandText = strsql
                Cmd.ExecuteNonQuery()

            Next
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Actualizar_Saldos_CatLog1: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            pcur = Nothing
            Da = Nothing
        End Try
    End Function

    Private Function Actualizar_Saldos_CatLog2(ByVal PCLIENTE_ID As Object, ByVal PPRODUCTO_ID As Object) As Boolean

        Dim Da As SqlDataAdapter
        Dim pcur As New DataSet
        Try


            Dim PCANTIDAD As Double = 0
            Dim strsql As String = ""

            Dim P_CLIENTE_ID As Object
            Dim P_PRODUCTO_ID As Object
            Dim p_Est_Merc_Id As Object

            P_CLIENTE_ID = PCLIENTE_ID
            P_PRODUCTO_ID = PPRODUCTO_ID

            strsql = "SELECT dd.cliente_id," & vbNewLine
            strsql = strsql & "        dd.producto_id," & vbNewLine
            strsql = strsql & "        Sum(rl.cantidad) AS Cantidad," & vbNewLine
            strsql = strsql & "        rl.est_merc_id" & vbNewLine
            strsql = strsql & " FROM det_documento_transaccion ddt," & vbNewLine
            strsql = strsql & "        det_documento dd," & vbNewLine
            strsql = strsql & "        documento d," & vbNewLine
            strsql = strsql & "        categoria_logica cl," & vbNewLine
            strsql = strsql & "        rl_det_doc_trans_posicion rl" & vbNewLine
            strsql = strsql & " WHERE dd.documento_id  = d.documento_id" & vbNewLine
            strsql = strsql & "        And d.status = 'D30'" & vbNewLine
            strsql = strsql & "        And rl.cliente_id = cl.cliente_id" & vbNewLine
            strsql = strsql & "        And rl.cat_log_id = cl.cat_log_id" & vbNewLine
            strsql = strsql & "        And cl.categ_stock_id = 'TRAN_EGR'" & vbNewLine
            strsql = strsql & "        And ddt.documento_id = dd.documento_id" & vbNewLine
            strsql = strsql & "        And ddt.nro_linea_doc = dd.nro_linea" & vbNewLine
            strsql = strsql & "        And rl.doc_trans_id_egr = ddt.doc_trans_id" & vbNewLine
            strsql = strsql & "        And rl.nro_linea_trans_egr = ddt.nro_linea_trans" & vbNewLine

            If Not IsNothing(P_CLIENTE_ID) Then
                strsql = strsql & "        And dd.cliente_id = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine
            Else
                P_CLIENTE_ID = "1"
                strsql = strsql & "        And '1' = '" & UCase(Trim(P_CLIENTE_ID)) & "'" & vbNewLine

            End If
            If Not IsNothing(P_PRODUCTO_ID) Then
                strsql = strsql & "        And dd.producto_id = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine
            Else
                P_PRODUCTO_ID = "1"
                strsql = strsql & "        And '1' = '" & UCase(Trim(P_PRODUCTO_ID)) & "'" & vbNewLine

            End If
            strsql = strsql & " GROUP BY dd.cliente_id," & vbNewLine
            strsql = strsql & "        dd.producto_id," & vbNewLine
            strsql = strsql & "        cl.cat_log_id," & vbNewLine
            strsql = strsql & "        cl.categ_stock_id," & vbNewLine
            strsql = strsql & "        rl.est_merc_id"
            'pcur = bd.Execute(strsql)
            Cmd.CommandText = strsql
            Da = New SqlDataAdapter(Cmd)
            Da.Fill(pcur, "PCUR")

            Dim i As Long
            For i = 0 To pcur.Tables("PCUR").Rows.Count - 1
                strsql = "UPDATE #TEMP_SALDOS_CATLOG SET cantidad = cantidad + " & CDbl(pcur.Tables("PCUR").Rows(0)("Cantidad")) & vbNewLine
                strsql = strsql & " WHERE cliente_id ='" & pcur.Tables("PCUR").Rows(0)("cliente_id") & "'" & vbNewLine
                strsql = strsql & "        And producto_id ='" & pcur.Tables("PCUR").Rows(0)("producto_id") & "'" & vbNewLine
                strsql = strsql & "        And categ_stock_id = 'STOCK'" & vbNewLine
                strsql = strsql & "        And cat_log_id = 'DISPONIBLE'"

                Cmd.CommandType = CommandType.Text
                Cmd.CommandText = strsql
                Cmd.ExecuteNonQuery()
            Next
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("Actualizar_Saldos_CatLog1: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            pcur = Nothing
            Da = Nothing
        End Try
    End Function

    Private Function hscInsertRecord(ByVal P_HIST_CATLOG_ID As Object, ByVal P_FECHA As Object, _
                                     ByVal P_CLIENTE_ID As Object, ByVal P_PRODUCTO_ID As Object, _
                                     ByVal P_CAT_LOG_ID As Object, ByVal P_CANTIDAD As Object, _
                                     ByVal P_CATEG_STOCK_ID As Object, ByVal P_DOCUMENTO_ID As Object, _
                                     ByVal P_NRO_LINEA As Object, ByVal P_DOC_STATUS As Object, _
                                     ByVal p_Est_Merc_Id As Object) As Boolean
        Try

            Dim strsql As String
            strsql = ""
            strsql = strsql & "Insert Into HISTORICO_SALDOS_CATLOG (" & vbNewLine
            strsql = strsql & "        FECHA," & vbNewLine
            strsql = strsql & "        CLIENTE_ID," & vbNewLine
            strsql = strsql & "        PRODUCTO_ID," & vbNewLine
            strsql = strsql & "        CAT_LOG_ID," & vbNewLine
            strsql = strsql & "        CANTIDAD," & vbNewLine
            strsql = strsql & "        CATEG_STOCK_ID," & vbNewLine
            strsql = strsql & "        DOCUMENTO_ID," & vbNewLine
            strsql = strsql & "        NRO_LINEA," & vbNewLine
            strsql = strsql & "        DOC_STATUS," & vbNewLine
            strsql = strsql & "        EST_MERC_ID)" & vbNewLine
            strsql = strsql & " Values (" & vbNewLine

            If IsNothing(P_FECHA) Then
                strsql = strsql & "        Null" & vbNewLine
            Else
                strsql = strsql & "        getdate()" & vbNewLine '& Format(P_FECHA, "dd/MM/yyyy hh:nn:ss") & "'" & vbNewLine
            End If
            strsql = strsql & "        ," & IIf(IsNothing(P_CLIENTE_ID), "Null", "'" & UCase(Trim(P_CLIENTE_ID)) & "'") & vbNewLine
            strsql = strsql & "        ," & IIf(IsNothing(P_PRODUCTO_ID), "Null", "'" & UCase(Trim(P_PRODUCTO_ID)) & "'") & vbNewLine
            strsql = strsql & "        ," & IIf(IsNothing(P_CAT_LOG_ID), "Null", "'" & UCase(Trim(P_CAT_LOG_ID)) & "'") & vbNewLine
            strsql = strsql & "        ," & IIf(IsNothing(P_CANTIDAD), "Null", UCase(Trim(P_CANTIDAD))) & vbNewLine
            strsql = strsql & "        ," & IIf(IsNothing(P_CATEG_STOCK_ID), "Null", "'" & UCase(Trim(P_CATEG_STOCK_ID)) & "'") & vbNewLine
            strsql = strsql & "        ," & IIf(IsNothing(P_DOCUMENTO_ID), "Null", UCase(Trim(P_DOCUMENTO_ID))) & vbNewLine
            strsql = strsql & "        ," & IIf(IsNothing(P_NRO_LINEA), "Null", UCase(Trim(P_NRO_LINEA))) & vbNewLine
            strsql = strsql & "        ," & IIf(IsNothing(P_DOC_STATUS), "Null", "'" & UCase(Trim(P_DOC_STATUS)) & "'") & vbNewLine
            strsql = strsql & "        ," & IIf(IsNothing(p_Est_Merc_Id), "Null", "'" & UCase(Trim(p_Est_Merc_Id)) & "'") & vbNewLine
            strsql = strsql & "      )"


            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strsql
            Cmd.ExecuteNonQuery()

            P_HIST_CATLOG_ID = OBTENER_SECUENCIA()
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            'MsgBox("BorrarDocTREgreso: " & ex.Message & MsgBoxStyle.OkOnly, ClsName)
            Return False

        End Try
    End Function

End Class
