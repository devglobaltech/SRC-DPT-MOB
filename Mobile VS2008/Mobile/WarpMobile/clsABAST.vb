Option Explicit On

Imports System.Data.SqlClient
Imports System.Data

Public Class clsABAST

#Region "Declaraciones"

    Private Const ClsName As String = "Abastecimiento"
    Private Const SQLConErr As String = "No se pudo conectar a la base de datos."
    Private CLIENTE As String = ""
    Private PRODUCTO As String = ""
    Private DESCRIPCION As String = ""
    Private POSICION_ABAST As String = ""
    Private PRIORIDAD As String = ""
    Private ABAST_ID As String = ""
    Private POSICION_ORIGEN As String = ""
    Private NRO_LOTE As String = ""
    Private NRO_PARTIDA As String = ""
    Private NRO_SERIE As String = ""
    Private NRO_BULTO As String = ""
    Public CANT_POS_OR As Double = 0
    Public SERIALIZADO As Boolean = False
    Private VCIERREFORZADO As Boolean = False
    Private vConfContenedora As Boolean = False
    Public VCANT_CONTENEDORA As Double = 0

    Private oBase As SqlConnection

    Private DABAST_ID As String = ""
    Private DCLIENTE As String = ""
    Private DPRODUCTO As String = ""
    Private DDESCRIPCION As String = ""
    Private DPOSICION_ABAST As String = ""
    Private DCANTIDAD As Double = 0
    Private DNRO_LOTE As String = ""
    Private DNRO_PARTIDA As String = ""
    Private DNRO_SERIE As String = ""
    Private DNRO_BULTO As String = ""

#End Region

#Region "Property's"

    Public ReadOnly Property Posicion_A_Abastecer() As String
        Get
            Return DPOSICION_ABAST
        End Get
    End Property

    Public ReadOnly Property ConfirmaPorContenedora() As Boolean
        Get
            Return vConfContenedora
        End Get
    End Property

    Public Property CierreForzado() As Boolean
        Get
            Return VCIERREFORZADO
        End Get
        Set(ByVal value As Boolean)
            VCIERREFORZADO = value
        End Set
    End Property

    Public ReadOnly Property EsSerializado() As Boolean
        Get
            Return SERIALIZADO
        End Get
    End Property

    Public ReadOnly Property ClienteID() As String
        Get
            Return CLIENTE
        End Get
    End Property

    Public ReadOnly Property ProductoID() As String
        Get
            Return PRODUCTO
        End Get
    End Property

    Public ReadOnly Property Posicion_Abastecida() As String
        Get
            Return POSICION_ABAST
        End Get
    End Property

    Public ReadOnly Property PrioridadAbast() As String
        Get
            Return PRIORIDAD
        End Get
    End Property

    Public ReadOnly Property NroAbastecimiento() As String
        Get
            Return ABAST_ID
        End Get
    End Property

    Public Property Conexion() As SqlConnection
        Get
            Return oBase
        End Get
        Set(ByVal value As SqlConnection)
            oBase = value
        End Set
    End Property


#End Region

#Region "Metodos Para la Carga"

    Public Function ValidarContenedora(ByVal Contenedora As String) As Boolean
        Dim Pa As SqlParameter, Cmd As SqlCommand
        Try
            If VerifyConnection(oBase) Then

                Cmd = oBase.CreateCommand
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.CommandText = "[DBO].[ABAST_MOB_VALIDAR_CONTENEDORA]"

                Pa = New SqlParameter("@USUARIO", Data.SqlDbType.VarChar, 100)
                Pa.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_CONTENEDORA", Data.SqlDbType.BigInt)
                Pa.Value = Contenedora
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@RESULT", Data.SqlDbType.VarChar, 1)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
                If Cmd.Parameters("@RESULT").Value = "1" Then
                    Return True
                ElseIf Cmd.Parameters("@RESULT").Value = "0" Then
                    Return False
                End If
            Else
                MsgBox(SQLConErr, MsgBoxStyle.Exclamation, ClsName)
            End If
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Message, MsgBoxStyle.Information, ClsName)
        Catch ex As Exception
            Mensaje(ex)
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub Mensaje(ByVal ex As Exception)
        MsgBox(ex.Message, MsgBoxStyle.Critical, ClsName)
    End Sub

    Public Function GenerarTareaAbastecimiento(ByVal List As ListBox) As Boolean
        Dim Cmd As SqlCommand, Pa As SqlParameter, Da As SqlDataAdapter, Ds As New DataSet
        Try
            If VerifyConnection(oBase) Then
                Cmd = oBase.CreateCommand
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.CommandText = "[DBO].[ABAST_MOB_TAREAS]"
                Da = New SqlDataAdapter(Cmd)

                Pa = New SqlParameter("@USUARIO", Data.SqlDbType.VarChar, 100)
                Pa.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(Pa)

                Da.Fill(Ds, "ABASTECIMIENTO")

                CLIENTE = Ds.Tables(0).Rows(0)(0).ToString
                PRODUCTO = Ds.Tables(0).Rows(0)(1).ToString
                DESCRIPCION = Ds.Tables(0).Rows(0)(2).ToString
                POSICION_ABAST = Ds.Tables(0).Rows(0)(3).ToString
                PRIORIDAD = Ds.Tables(0).Rows(0)(4).ToString
                ABAST_ID = Ds.Tables(0).Rows(0)(5).ToString

                List.Items.Clear()
                List.Items.Add("-Cod. Cliente: " & CLIENTE)
                List.Items.Add("-Cod. Producto: " & PRODUCTO & "-" & DESCRIPCION)
                List.Items.Add("-Pos. Abastecible: " & POSICION_ABAST)
                List.Items.Add("-Prioridad: " & PRIORIDAD)

                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.Exclamation, ClsName)
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Information, ClsName)
        Catch ex As Exception
            Mensaje(ex)
        Finally
            Da.Dispose()
            Ds.Dispose()
            Cmd.Dispose()
            Pa = Nothing
        End Try

    End Function

    Public Function DetalleTareaAbastecimiento(ByVal List As ListBox, ByVal Contenedora As String, ByRef LblUbicacion As Label) As Boolean
        Dim Cmd As SqlCommand, Pa As SqlParameter, Da As SqlDataAdapter, Ds As New DataSet
        Try
            If VerifyConnection(oBase) Then
                Cmd = oBase.CreateCommand
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.CommandText = "[DBO].[ABAST_MOB_TAREAS_DETALLE]"
                Da = New SqlDataAdapter(Cmd)

                Pa = New SqlParameter("@ABAST_ID", Data.SqlDbType.BigInt)
                Pa.Value = ABAST_ID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_CONTENEDORA", Data.SqlDbType.BigInt)
                Pa.Value = Contenedora
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Da.Fill(Ds, "DET_ABASTECIMIENTO")

                If Ds.Tables.Count > 0 Then
                    If Ds.Tables(0).Rows.Count > 0 Then
                        POSICION_ORIGEN = Ds.Tables(0).Rows(0)(0).ToString
                        NRO_LOTE = IIf(Ds.Tables(0).Rows(0)(1).ToString = "", "", Ds.Tables(0).Rows(0)(1).ToString)
                        NRO_PARTIDA = IIf(Ds.Tables(0).Rows(0)(2).ToString = "", "", Ds.Tables(0).Rows(0)(2).ToString)
                        NRO_SERIE = IIf(Ds.Tables(0).Rows(0)(3).ToString = "", "", Ds.Tables(0).Rows(0)(3).ToString)
                        NRO_BULTO = IIf(Ds.Tables(0).Rows(0)(4).ToString = "", "", Ds.Tables(0).Rows(0)(4).ToString)
                        CANT_POS_OR = CDbl(Ds.Tables(0).Rows(0)(5))
                        EsMasterPack()

                        List.Items.Clear()
                        List.Items.Add("-Cod. Cliente: " & CLIENTE)
                        List.Items.Add("-Cod. Producto: " & PRODUCTO & "-" & DESCRIPCION)
                        List.Items.Add("-Pos. Abastecible: " & POSICION_ABAST)
                        List.Items.Add("-Prioridad: " & PRIORIDAD)
                        List.Items.Add("-Nro. Partida: " & NRO_PARTIDA)
                        List.Items.Add("-Nro. Lote: " & NRO_LOTE)
                        List.Items.Add("-Nro. Contenedora: " & NRO_BULTO)


                        If NRO_SERIE <> "" Then
                            If Not Me.vConfContenedora Then
                                List.Items.Add("-Cantidad: " & CANT_POS_OR)
                                LblUbicacion.Text = "Pos.: " & POSICION_ORIGEN & " | Nro. Serie: " & NRO_SERIE
                                SERIALIZADO = True
                            Else
                                LblUbicacion.Text = "Pos.: " & POSICION_ORIGEN & " | Confirme Cont.: " & NRO_BULTO
                            End If
                        Else
                            LblUbicacion.Text = "Pos.: " & POSICION_ORIGEN
                            List.Items.Add("-Cantidad: " & CANT_POS_OR)
                        End If
                    End If
                    CierreForzado = False
                    Return True
                Else
                    CLIENTE = ""
                    PRODUCTO = ""
                    DESCRIPCION = ""
                    POSICION_ABAST = ""
                    PRIORIDAD = ""
                    ABAST_ID = ""
                    POSICION_ORIGEN = ""
                    NRO_LOTE = ""
                    NRO_PARTIDA = ""
                    NRO_SERIE = ""
                    NRO_BULTO = ""
                    CANT_POS_OR = 0
                    VCANT_CONTENEDORA = 0
                    SERIALIZADO = False
                    VCIERREFORZADO = False
                    vConfContenedora = False
                    CierreForzado = True
                End If
            Else
                MsgBox(SQLConErr, MsgBoxStyle.Exclamation, ClsName)
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Information, ClsName)
        Catch ex As Exception
            Mensaje(ex)
        Finally
            Da.Dispose()
            Ds.Dispose()
            Cmd.Dispose()
            Pa = Nothing
        End Try

    End Function

    Public Function ValidarPosicion_Serie(ByVal valor As String, ByVal Contenedora As String) As Boolean
        Try
            If Me.vConfContenedora Then
                If Trim(UCase(valor)) = Me.NRO_BULTO Then
                    Return True
                Else
                    Return False
                End If
            ElseIf SERIALIZADO Then
                If Trim(UCase(valor)) = Trim(UCase(NRO_SERIE)) Then
                    Return True
                Else
                    If PermitePermutacionSeries(Contenedora, valor) Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                If Trim(UCase(valor)) = Trim(UCase(POSICION_ORIGEN)) Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Mensaje(ex)
        End Try
    End Function

    Private Function PermitePermutacionSeries(ByVal Contenedora As String, ByVal SerieDestino As String) As Boolean
        Dim Pa As SqlParameter, Cmd As SqlCommand
        Try
            If VerifyConnection(oBase) Then

                Cmd = oBase.CreateCommand
                Cmd.CommandText = "[DBO].[ABAST_MOB_TAREAS_CAMBIASERIE]"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@ABAST_ID", SqlDbType.BigInt)
                Pa.Value = Me.ABAST_ID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Me.CLIENTE
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = Me.PRODUCTO
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_COD", SqlDbType.VarChar, 45)
                Pa.Value = Me.POSICION_ORIGEN
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_LOTE", SqlDbType.VarChar, 100)
                If Me.NRO_LOTE = "" Then
                    Pa.Value = DBNull.Value
                Else : Pa.Value = Me.NRO_LOTE
                End If
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 100)
                If Me.NRO_PARTIDA = "" Then
                    Pa.Value = DBNull.Value
                Else : Pa.Value = Me.NRO_PARTIDA
                End If
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 100)
                If Me.NRO_SERIE = "" Then
                    Pa.Value = DBNull.Value
                Else : Pa.Value = Me.NRO_SERIE
                End If
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_SERIE_DEST", SqlDbType.VarChar, 100)
                If SerieDestino = "" Then
                    Pa.Value = DBNull.Value
                Else : Pa.Value = SerieDestino
                End If

                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_BULTO", SqlDbType.VarChar, 100)
                Pa.Value = Me.NRO_BULTO
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CONTENEDORA", SqlDbType.BigInt)
                Pa.Value = Contenedora
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@RETORNO", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

                If Cmd.Parameters("@RETORNO").Value = "1" Then
                    Me.NRO_SERIE = SerieDestino
                    Return True
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, ClsName)
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Information, ClsName)
        Catch ex As Exception
            Mensaje(ex)
        Finally
            Cmd.Dispose()
            Pa = Nothing
        End Try
    End Function

    Public Function ConfirmarRetiroOrigen(ByVal CantConfirmada As Double, ByVal ContenedoraTR As String, ByRef TareaEliminada As Boolean) As Boolean
        Dim Pa As SqlParameter, Cmd As SqlCommand
        Try
            If VerifyConnection(oBase) Then

                Cmd = oBase.CreateCommand
                Cmd.CommandText = "[DBO].[ABAST_MOB_TAREAS_INGRESACARRO]"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@ABAST_ID", SqlDbType.BigInt)
                Pa.Value = Me.ABAST_ID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_COD", SqlDbType.VarChar, 45)
                Pa.Value = Me.POSICION_ORIGEN
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_LOTE", SqlDbType.VarChar, 100)
                If Me.NRO_LOTE = "" Then
                    Pa.Value = DBNull.Value
                Else : Pa.Value = Me.NRO_LOTE
                End If
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 100)
                If Me.NRO_PARTIDA = "" Then
                    Pa.Value = DBNull.Value
                Else : Pa.Value = Me.NRO_PARTIDA
                End If
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 100)
                If Me.NRO_SERIE = "" Then
                    Pa.Value = DBNull.Value
                Else
                    If Not Me.ConfirmaPorContenedora Then
                        Pa.Value = Me.NRO_SERIE
                    Else
                        Pa.Value = DBNull.Value
                    End If
                End If
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_BULTO", SqlDbType.VarChar, 100)
                If Me.NRO_BULTO = "" Then
                    Pa.Value = DBNull.Value
                Else : Pa.Value = Me.NRO_BULTO
                End If
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CANT_CONFIRMADA", SqlDbType.Float)
                If Me.ConfirmaPorContenedora Then
                    Pa.Value = Me.VCANT_CONTENEDORA
                Else
                    Pa.Value = CantConfirmada
                End If
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CONTENEDOR", SqlDbType.BigInt)
                Pa.Value = ContenedoraTR
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@TAREA_ELIMINADA", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                TareaEliminada = Cmd.Parameters("@TAREA_ELIMINADA").Value

                Return True
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, ClsName)
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Information, ClsName)
        Catch ex As Exception
            Mensaje(ex)
        Finally
            Cmd.Dispose()
            Pa = Nothing
        End Try
    End Function

    Private Function EsMasterPack() As Boolean
        Dim Ds As New DataSet, Cmd As SqlCommand, Pa As SqlParameter, Ret As String
        Try
            If VerifyConnection(oBase) Then
                Cmd = oBase.CreateCommand

                If Me.NRO_BULTO <> "" Then

                    Cmd.CommandText = "[DBO].[ABAST_MOB_CONFIRMAPORCONTENEDORA]"
                    Cmd.CommandType = CommandType.StoredProcedure

                    Pa = New SqlParameter("@ABAST_ID", SqlDbType.BigInt)
                    Pa.Value = Me.ABAST_ID
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                    Pa.Value = Me.CLIENTE
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                    Pa.Value = Me.PRODUCTO
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@NRO_BULTO", SqlDbType.VarChar, 100)
                    Pa.Value = Me.NRO_BULTO
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@RETORNO", SqlDbType.VarChar, 1)
                    Pa.Direction = ParameterDirection.Output
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@CANT_CONT", SqlDbType.Float)
                    Pa.Direction = ParameterDirection.Output
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Cmd.ExecuteNonQuery()

                    Ret = Cmd.Parameters("@RETORNO").Value
                    If Ret = "1" Then
                        VCANT_CONTENEDORA = Cmd.Parameters("@CANT_CONT").Value
                        vConfContenedora = True
                        Return True
                    Else
                        vConfContenedora = False
                        Return False
                    End If
                Else : Return False
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, ClsName)
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Information, ClsName)
        Catch ex As Exception
            Mensaje(ex)
        Finally
            Cmd.Dispose()
            Ds.Dispose()
        End Try
    End Function

#End Region

#Region "Metodos Para la Descarga"

    Public Function ValidarContenedoraCargada(ByVal NroContenedora As String) As Boolean
        Dim Ds As New DataSet, Cmd As SqlCommand, Pa As SqlParameter, Ret As String
        Try
            If VerifyConnection(oBase) Then
                Cmd = oBase.CreateCommand
                Cmd.CommandText = "[DBO].[ABAST_MOB_VAL_CONTENEDORA_DESCARGA]"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@NRO_CONTENEDORA", SqlDbType.BigInt)
                Pa.Value = NroContenedora
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OUTPUT", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

                If Cmd.Parameters("@OUTPUT").Value = "0" Then
                    Return False
                Else
                    Return True
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, ClsName)
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Information, ClsName)
        Catch ex As Exception
            Mensaje(ex)
        Finally
            Cmd.Dispose()
            Ds.Dispose()
        End Try
    End Function

    Public Function ValidarProductoContenedoraCargada(ByVal NroContenedora As String, ByVal ProductoID As String) As Boolean
        Dim Ds As New DataSet, Cmd As SqlCommand, Pa As SqlParameter, Ret As String
        Try
            If VerifyConnection(oBase) Then
                Cmd = oBase.CreateCommand
                Cmd.CommandText = "[DBO].[ABAST_MOB_VAL_PROD_CONTENEDORA_DESCARGA]"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@NRO_CONTENEDORA", SqlDbType.BigInt)
                Pa.Value = NroContenedora
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = ProductoID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OUTPUT", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

                If Cmd.Parameters("@OUTPUT").Value = "0" Then
                    Return False
                Else
                    Return True
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, ClsName)
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Information, ClsName)
        Catch ex As Exception
            Mensaje(ex)
        Finally
            Cmd.Dispose()
            Ds.Dispose()
        End Try
    End Function


    Public Function LlenarListboxDescarga(ByVal NroContenedora As String, ByVal ProductoID As String, ByRef Lst As ListBox, ByRef lblUbicDestino As Label) As Boolean
        Dim Ds As New DataSet, Cmd As SqlCommand, Pa As SqlParameter, Da As SqlDataAdapter

        Try
            If VerifyConnection(oBase) Then
                Cmd = oBase.CreateCommand
                Cmd.CommandText = "[DBO].[ABAST_MOB_VAL_PRODUCTO_CONTENEDORA]"
                Cmd.CommandType = CommandType.StoredProcedure
                Da = New SqlDataAdapter(Cmd)

                Pa = New SqlParameter("@NRO_CONTENEDORA", SqlDbType.BigInt)
                Pa.Value = NroContenedora
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = ProductoID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Da.Fill(Ds, "DETALLE")

                DABAST_ID = Ds.Tables(0).Rows(0)(0)
                DCLIENTE = Ds.Tables(0).Rows(0)(1)
                DPRODUCTO = Ds.Tables(0).Rows(0)(2)
                DDESCRIPCION = Ds.Tables(0).Rows(0)(3)
                DCANTIDAD = Ds.Tables(0).Rows(0)(4)
                DPOSICION_ABAST = Ds.Tables(0).Rows(0)(5)
                DNRO_BULTO = IIf(IsDBNull(Ds.Tables(0).Rows(0)(6)), "", Ds.Tables(0).Rows(0)(6).ToString)
                DNRO_LOTE = IIf(IsDBNull(Ds.Tables(0).Rows(0)(7)), "", Ds.Tables(0).Rows(0)(7).ToString)
                DNRO_PARTIDA = IIf(IsDBNull(Ds.Tables(0).Rows(0)(8)), "", Ds.Tables(0).Rows(0)(8).ToString)

                lblUbicDestino.Text = "Ubicacion: " & DPOSICION_ABAST
                Lst.Items.Clear()
                Lst.Items.Add("- Cod.Cliente: " & DCLIENTE)
                Lst.Items.Add("- Cod.Producto: " & DPRODUCTO)
                Lst.Items.Add("- " & DDESCRIPCION)
                Lst.Items.Add("- Cantidad: " & DCANTIDAD)
                Lst.Items.Add("- Nro.Contenedora: " & DNRO_BULTO)
                Lst.Items.Add("- Nro.Lote: " & DNRO_LOTE)
                Lst.Items.Add("- Nro.Partida: " & DNRO_PARTIDA)
                Return True
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, ClsName)
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Information, ClsName)
        Catch ex As Exception
            Mensaje(ex)
        Finally
            Cmd.Dispose()
            Ds.Dispose()
        End Try
    End Function

    Public Function ConfirmarDescarga(ByVal NroContenedora As String, ByVal ProductoID As String) As Boolean
        Dim Ds As New DataSet, Cmd As SqlCommand, Pa As SqlParameter
        Try
            If VerifyConnection(oBase) Then
                Cmd = oBase.CreateCommand
                Cmd.CommandText = "[DBO].[ABAST_MOB_CONFIRMAR]"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@ABAST_ID", SqlDbType.BigInt)
                Pa.Value = DABAST_ID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_LOTE", SqlDbType.VarChar, 50)
                Pa.Value = IIf(DNRO_LOTE = "", DBNull.Value, DNRO_LOTE)
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 50)
                Pa.Value = IIf(DNRO_PARTIDA = "", DBNull.Value, DNRO_PARTIDA)
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                '@CONTENEDOR			NUMERIC(20,0)
                Pa = New SqlParameter("@NRO_BULTO", SqlDbType.VarChar, 50)
                Pa.Value = IIf(DNRO_BULTO = "", DBNull.Value, DNRO_BULTO)
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CANT_CONFIRMADA", SqlDbType.Float, 50)
                Pa.Value = DCANTIDAD
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CONTENEDOR", SqlDbType.BigInt)
                Pa.Value = IIf(NroContenedora = "", DBNull.Value, NroContenedora)
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                Pa.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

                Return True
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, ClsName)
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Information, ClsName)
        Catch ex As Exception
            Mensaje(ex)
        Finally
            Cmd.Dispose()
            Ds.Dispose()
        End Try
    End Function

    Public Function PendientesPorContenedora(ByVal Contenedora As String, ByRef DS As DataSet) As Boolean
        Dim Cmd As SqlCommand, Pa As SqlParameter, Da As SqlDataAdapter
        Try
            If VerifyConnection(oBase) Then

                Cmd = oBase.CreateCommand
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = "[DBO].[ABAST_MOB_CONTENIDO]"
                Da = New SqlDataAdapter(Cmd)

                Pa = New SqlParameter("@CONTENEDORA", SqlDbType.BigInt)
                Pa.Value = Contenedora
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Da.Fill(DS, "RES")

                Return True
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, ClsName)
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Information, ClsName)
        Catch ex As Exception
            Mensaje(ex)
        Finally
            Cmd.Dispose()
        End Try
    End Function
#End Region

End Class
