Imports System.Data
Imports System.Data.SqlClient

Public Class clsDecode2D

#Region "Declaracion de Variables"
    Private UtilizaCeros As String = ""
    Private strPRODUCTO_ID As String
    Private strCLIENTE_ID As String
    Private blnSerieIng As Boolean = False
    Private blnSerieEgr As Boolean = False
    Private ArraySeries As New Collection
    Private lngCantSeries As Long = 0
    Private strContenedora As String
    Private Const ClsName As String = "Decodificacion"
    Private Const SQLConErr As String = "No se pudo conectar a la base de datos."
    Private SerieDesde As Long = 0
    Private SerieHasta As Long = 0
    Private SerieUnica As Boolean = False
    Private vNroSerie As String = ""
    Private blnAuditoria As Boolean = False
    Private strCodigoVIaje As String = ""
    Private vProductoSolicitado As String = ""
    Private vmaxseries As Integer = 0
    Private ErrorMax As Boolean = False

#End Region

#Region "Declaracion de Propiedades"

    Public Property ProductoSolicitado() As String
        Get
            Return Me.vProductoSolicitado
        End Get
        Set(ByVal value As String)
            Me.vProductoSolicitado = value
        End Set
    End Property

    Public Property CodigoViaje() As String
        Get
            Return Me.strCodigoVIaje
        End Get
        Set(ByVal value As String)
            Me.strCodigoVIaje = value
        End Set
    End Property

    Public Property GuardarAuditoria() As Boolean
        Get
            Return blnAuditoria
        End Get
        Set(ByVal value As Boolean)
            blnAuditoria = value
        End Set
    End Property

    Public ReadOnly Property aSeries() As Collection
        Get
            Return ArraySeries
        End Get
    End Property

    Public ReadOnly Property NroSerie() As String
        Get
            Return vNroSerie
        End Get
    End Property

    Public ReadOnly Property SERIE_INICIO() As Long
        Get
            Return SerieDesde
        End Get
    End Property

    Public ReadOnly Property SERIE_FIN() As Long
        Get
            Return SerieHasta
        End Get
    End Property

    Public ReadOnly Property UnicaSerie() As Boolean
        Get
            Return SerieUnica
        End Get
    End Property

    Public Property SerializacionEgreso() As Boolean
        Get
            Return blnSerieEgr
        End Get
        Set(ByVal value As Boolean)
            blnSerieEgr = value
        End Set
    End Property

    Public ReadOnly Property ErrorMaxSeries() As Long
        Get
            Return ErrorMax
        End Get
    End Property

    Public Property CLIENTE_ID() As String
        Get
            Return strCLIENTE_ID
        End Get
        Set(ByVal value As String)
            strCLIENTE_ID = value
        End Set
    End Property

    Public ReadOnly Property PRODUCTO_ID() As String
        Get
            Return strPRODUCTO_ID
        End Get
    End Property

    Public ReadOnly Property QtySeries() As Long
        Get
            Return lngCantSeries
        End Get
    End Property

    Public ReadOnly Property SerializacionIngreso() As Boolean
        Get
            Return blnSerieIng
        End Get
    End Property

    Public Property Contenedora() As String
        Get
            Return strContenedora
        End Get
        Set(ByVal value As String)
            strContenedora = value
        End Set
    End Property

    Public ReadOnly Property Get_Series() As String
        Get
            Return GetSeries()
        End Get
    End Property

#End Region

#Region "Declaracion de Metodos"

    Private Function Auditar(ByVal Lectura As String, ByVal Producto As String) As Boolean
        '@CLIENTE_ID  VARCHAR(15),
        '@CODIGO_VIAJE  VARCHAR(100),
        '@PRODUCTO_ID  VARCHAR(30),
        '@LECTURA_2D  VARCHAR(100),
        '@USUARIO   VARCHAR(100)
        Dim Cmd As SqlCommand, PA As SqlParameter
        Try
            If Me.GuardarAuditoria Then

                If VerifyConnection(SQLc) Then
                    Cmd = SQLc.CreateCommand
                    Cmd.Parameters.Clear()
                    Cmd.CommandText = "MOB_INS_AUDITORIA_2D"
                    Cmd.CommandType = CommandType.StoredProcedure

                    PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                    PA.Value = Me.strCLIENTE_ID
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@CODIGO_VIAJE", SqlDbType.VarChar, 100)
                    PA.Value = Me.CodigoViaje
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                    PA.Value = Me.vProductoSolicitado
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@LECTURA_2D", SqlDbType.VarChar, 100)
                    PA.Value = Lectura
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    PA = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                    PA.Value = vUsr.CodUsuario
                    Cmd.Parameters.Add(PA)
                    PA = Nothing

                    Cmd.ExecuteNonQuery()

                End If
            End If
            Return True
        Catch exs As SqlException
            MsgBox("Excepcion al guardar auditoria: " + exs.Message, MsgBoxStyle.Exclamation, ClsName)
        Catch ex As Exception
            MsgBox("Excepcion al guardar auditoria: " + ex.Message, MsgBoxStyle.Exclamation, ClsName)
        End Try

    End Function

    Public Sub Decode(ByVal Value As String)
        Try
            '-------------------------------------------------------------------------------------------
            '0. Guardo en auditoria la lectura. El ponja nos tiene inflados.
            '-------------------------------------------------------------------------------------------
            Auditar(Value, Trim(UCase(Mid(Value, 1, 20))))
            '-------------------------------------------------------------------------------------------
            '1. Limpio las variables al entrar en el metodo, esto me garantiza que no queden residuales.
            '-------------------------------------------------------------------------------------------
            LimpiarVariables()
            '-------------------------------------------------------------------------------------------
            '2. Dependiendo de la longitud de la cadena completo las variables.
            '-------------------------------------------------------------------------------------------
            Select Case Len(Trim(Value))
                Case 52
                    strPRODUCTO_ID = Trim(UCase(Mid(Value, 1, 20)))
                    GenerarArraySeries(Value)
                Case 55
                    strPRODUCTO_ID = Trim(UCase(Mid(Value, 1, 20)))
                    GenerarArraySeries(Value)
                Case Else
                    strPRODUCTO_ID = Trim(UCase(Value))
            End Select
            '-------------------------------------------------------------------------------------------
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, ClsName)
        End Try
    End Sub

    Private Sub LimpiarVariables()
        '-------------------------------------------------------------------------------------------
        'Limpieza de variable para evitar valores residuales.
        '-------------------------------------------------------------------------------------------
        Me.GuardarAuditoria = False
        UtilizaCeros = ""
        Me.CodigoViaje = ""
        strContenedora = ""
        strPRODUCTO_ID = ""
        lngCantSeries = 0
        SerieDesde = 0
        SerieHasta = 0
        ArraySeries = Nothing
        ArraySeries = New Collection
        blnSerieIng = False
        SerieUnica = False
        SerializacionEgreso = False
        vNroSerie = ""
        ErrorMax = False
        vmaxseries = 0
    End Sub

    Private Function GetSeries() As String
        Dim Ret As String = "", vSerie As String
        Try
            For Each vSerie In ArraySeries
                Ret = Ret & vSerie & ";"
            Next
            Return Ret
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, ClsName)
        End Try
    End Function

    Private Sub GenerarArraySeries(ByVal Value As String)
        'Se obtiene el valor desde Hasta, luego obtengo la diferencia entre ambos y eso me
        'da la longitud del ciclo for. Posteriormente voy generando los numeros de serie
        'incrementado
        Dim iFrom As Long = 0, iTo As Long = 0, i As Long = 0, SerialNum As Long = 0
        Dim pcar As String, b As Boolean, strSerieI As String = "", strSerieF As String = ""

        b = False

        Try
            If SerializaIngreso() Or SerializacionEgreso Then

                strSerieI = Mid(Value, 21, 9)
                strSerieF = Mid(Value, 30, 9)

                If Not IsNumeric(strSerieI) Then
                    Throw New Exception("La serie no es numerica.")
                End If


                If Not IsNumeric(strSerieF) Then
                    Throw New Exception("La serie no es numerica.")
                End If

                iFrom = Mid(Value, 21, 9)
                iTo = Mid(Value, 30, 9)
                pcar = Mid(Value, 21, 1)
                If pcar = " " Then
                    Me.UtilizaCeros = "0"
                    b = True
                Else
                    Me.UtilizaCeros = "1"
                End If
                SerialNum = iFrom
                SerieDesde = iFrom
                SerieHasta = iTo
                If iTo = iFrom Then
                    If Not b Then
                        vNroSerie = Mid(Value, 21, 9)
                    Else
                        vNroSerie = LTrim(Mid(Value, 21, 9))
                    End If
                    SerieUnica = True
                End If

                'BUSCO LA CANTIDAD MAXIMA DE SERIES PARA LEER
                GetMaxSeries()

                If vmaxseries > (iTo - iFrom) And (iTo - iFrom) > 0 Then

                    For i = 0 To iTo - iFrom
                        If Not b Then
                            ArraySeries.Add(SerialNum.ToString().PadLeft(9, "0"))
                        Else
                            ArraySeries.Add(SerialNum)
                        End If
                        SerialNum = SerialNum + 1
                    Next

                    lngCantSeries = ArraySeries.Count

                Else

                    lngCantSeries = 0

                    ErrorMax = True

                    'MsgBox("Se superó el máximo de series a cargar", MsgBoxStyle.Information, ClsName)

                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, ClsName)
        End Try
    End Sub

    Private Function SerializaIngreso() As Boolean
        Dim DA As SqlDataAdapter, MyCmd As SqlCommand, Ds As New DataSet

        Try
            If VerifyConnection(SQLc) Then
                MyCmd = SQLc.CreateCommand
                MyCmd.CommandText = "SELECT ISNULL(SERIE_ING,'0') AS SERIE_ING ,ISNULL(SERIE_EGR,'0') AS SERIE_EGR FROM PRODUCTO WHERE CLIENTE_ID='" & Me.CLIENTE_ID & "' AND PRODUCTO_ID='" & Me.PRODUCTO_ID & "'"
                MyCmd.CommandType = CommandType.Text
                DA = New SqlDataAdapter(MyCmd)
                DA.Fill(Ds, "PRODUCTO")
                If Ds.Tables(0).Rows.Count > 0 Then
                    Me.SerializacionEgreso = IIf(Ds.Tables(0).Rows(0)(1).ToString = "1", True, False)
                    If Ds.Tables("PRODUCTO").Rows(0)(0).ToString = "0" Then
                        Return False
                        blnSerieIng = False
                    Else
                        blnSerieIng = True
                        Return True
                    End If
                Else
                    MsgBox("No se encontro el producto " & Me.PRODUCTO_ID & " para el codigo de cliente " & Me.CLIENTE_ID, MsgBoxStyle.Information, ClsName)
                    Return False
                End If
            Else : Return False
            End If
        Catch ExSQL As SqlException

        Catch ex As Exception

        Finally
            MyCmd = Nothing
            DA = Nothing
            Ds = Nothing
        End Try
    End Function

    Public Function GuardarSeries(ByRef Trans As SqlTransaction) As Boolean
        Dim oSerie As New clsCargaSeries, DsErr As New DataSet
        Try
            If SerializacionIngreso Then
                oSerie.IniciarProcesoSeries(Trans)
                oSerie.Producto = Me.PRODUCTO_ID
                IngresarSeries(oSerie)
                If oSerie.ConfirmarSeriesDS(DsErr, Trans) Then
                    Return True
                Else
                    Trans.Rollback()
                    MostrarSeriesInvalidas(DsErr)
                    Return False
                End If
            Else : Return True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, ClsName)
            Return False
        Finally
            DsErr = Nothing
        End Try
    End Function

    Private Sub MostrarSeriesInvalidas(ByRef DsErr As DataSet)
        Try
            '1. las muestro.
            MsgBox("Se detectaron series duplicadas en la carga. A continuación seran listadas.", MsgBoxStyle.Exclamation, ClsName)
            Dim f As New frmCargaSeriesInvalidas
            f.SeriesError = DsErr
            f.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, ClsName)
        End Try
    End Sub

    Private Sub IngresarSeries(ByRef oSeries As clsCargaSeries)
        Dim Valor As String

        For Each Valor In ArraySeries
            oSeries.GuardarSerieDS(CLIENTE_ID, Contenedora, Valor, False)
        Next
    End Sub

    Public Function GuardarSeriesEgreso(ByVal ClienteID As String, ByVal ViajeID As String, ByVal NroContenedora As String, ByRef Confirmo As Boolean) As Boolean
        Dim Pa As SqlParameter
        Dim myCmd As SqlCommand
        Dim Trans As SqlTransaction
        Trans = SQLc.BeginTransaction
        Try
            If VerifyConnection(SQLc) Then
                myCmd = SQLc.CreateCommand
                myCmd.Transaction = Trans
                myCmd.CommandType = CommandType.StoredProcedure
                myCmd.CommandText = "DBO.CONFIRMAR_SERIES_2D_CONTENEDORAS"
                '@CLIENTE_ID		VARCHAR(100),
                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 100)
                Pa.Value = ClienteID
                myCmd.Parameters.Add(Pa)
                Pa = Nothing
                '@VIAJE_ID		VARCHAR(100),
                Pa = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                Pa.Value = ViajeID
                myCmd.Parameters.Add(Pa)
                Pa = Nothing
                '@PRODUCTO_ID	VARCHAR(50),
                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 100)
                Pa.Value = Me.PRODUCTO_ID
                myCmd.Parameters.Add(Pa)
                Pa = Nothing

                '@CONTENEDORA	VARCHAR(50),
                Pa = New SqlParameter("@CONTENEDORA", SqlDbType.VarChar, 100)
                Pa.Value = NroContenedora
                myCmd.Parameters.Add(Pa)
                Pa = Nothing

                '@SERIE_DESDE	NUMERIC(20,0),
                Pa = New SqlParameter("@SERIE_DESDE", SqlDbType.BigInt)
                Pa.Value = SerieDesde
                myCmd.Parameters.Add(Pa)
                Pa = Nothing

                '@SERIE_HASTA	NUMERIC(20,0),
                Pa = New SqlParameter("@SERIE_HASTA", SqlDbType.BigInt)
                Pa.Value = SerieHasta
                myCmd.Parameters.Add(Pa)
                Pa = Nothing

                '@CANT_SERIES	NUMERIC(20,0),
                Pa = New SqlParameter("@CANT_SERIES", SqlDbType.VarChar, 100)
                Pa.Value = Me.QtySeries
                myCmd.Parameters.Add(Pa)
                Pa = Nothing

                '@USUARIO		VARCHAR(100),
                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                Pa.Value = vUsr.CodUsuario
                myCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USA_CERO", SqlDbType.VarChar, 1)
                Pa.Value = Me.UtilizaCeros
                myCmd.Parameters.Add(Pa)
                Pa = Nothing

                '@CONFIRMO		VARCHAR(1)OUT
                Pa = New SqlParameter("@CONFIRMO", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                myCmd.Parameters.Add(Pa)

                myCmd.ExecuteNonQuery()

                Confirmo = myCmd.Parameters("@CONFIRMO").Value
                Trans.Commit()
                Return True
            Else : MsgBox(SQLConErr, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
        Catch ExSQL As SqlException
            Trans.Rollback()
            MsgBox(ExSQL.Message & " GuardarSeriesEgreso", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            Trans.Rollback()
            MsgBox(ex.Message & " GuardarSeriesEgreso", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Pa = Nothing
            myCmd = Nothing
            Trans = Nothing
        End Try

    End Function

    Private Function GetMaxSeries() As Boolean
        Dim DA As SqlDataAdapter, MyCmd As SqlCommand, Ds As New DataSet

        Try
            If VerifyConnection(SQLc) Then
                MyCmd = SQLc.CreateCommand
                MyCmd.CommandText = "SELECT ISNULL(VALOR,'0') FROM SYS_PARAMETRO_PROCESO WHERE PROCESO_ID='WMOV' AND SUBPROCESO_ID='EGR_SERIE' AND PARAMETRO_ID = 'MAX_SERIES';"
                MyCmd.CommandType = CommandType.Text
                DA = New SqlDataAdapter(MyCmd)
                DA.Fill(Ds, "MAX_SERIES")
                If Ds.Tables(0).Rows.Count > 0 Then
                    Me.vmaxseries = Ds.Tables(0).Rows(0)(0)
                End If
            Else : Return False
            End If
        Catch ExSQL As SqlException

        Catch ex As Exception

        Finally
            MyCmd = Nothing
            DA = Nothing
            Ds = Nothing
        End Try
    End Function

#End Region

End Class
