Option Explicit On

Imports System.Data
Imports System.Data.SqlClient


Public Class clsEmpaque

    'Private o2D As New clsDecode2D
    Private Conn As SqlClient.SqlConnection
    Private Const ClsName As String = "Empaquetado."
    Private Const SQLConnection As String = "No se pudo conectar a la base de datos."
    Private cCarrosEmpaque As New Collection
    Private cContenedoresNoValidos As New Collection
    Private ClienteID As String = ""
    Private CodigoViaje As String = ""
    Private ProductoID As String = ""
    Private CANTIDADPENDIENTE As Double
    Private UC_EMPAQUE As String
    Private NRO_LOTE As String
    Private NRO_PARTIDA As String
    Private vSOLICITA_LOTE_PARTIDA As Boolean
    Private PEDIDO As String
    Private SERIE As String
    Private ULTIMA_UC_EMPAQUE As String
    Private vULTIMO_PEDIDO As String

#Region "Property's"

    Public ReadOnly Property ULTIMO_UC_EMPAQUE() As String
        Get
            Return Me.ULTIMA_UC_EMPAQUE
        End Get
    End Property

    Public ReadOnly Property ULTIMO_PEDIDO() As String
        Get
            Return vULTIMO_PEDIDO
        End Get
    End Property

    Public ReadOnly Property NRO_SERIE() As String
        Get
            Return SERIE
        End Get
    End Property

    Public ReadOnly Property NRO_PEDIDO() As String
        Get
            Return PEDIDO
        End Get
    End Property

    Public ReadOnly Property CodigoDeOla() As String
        Get
            Return CodigoViaje
        End Get
    End Property

    Public ReadOnly Property CantidadContenedores() As Integer
        Get
            Return Me.cCarrosEmpaque.Count
        End Get
    End Property

    Public Property Conexion() As SqlConnection
        Get
            Return Conn
        End Get
        Set(ByVal value As SqlConnection)
            Conn = value
        End Set
    End Property

    Public ReadOnly Property ContenedorasDesconsolidacion()
        Get
            Return GetContenedoras()
        End Get
    End Property

    Public ReadOnly Property ContenedorasNoValidadas()
        Get
            Return GetContenedoresNoValidados()
        End Get
    End Property

    Public ReadOnly Property SolicitaLotePartida() As Boolean
        Get
            Return vSOLICITA_LOTE_PARTIDA
        End Get
    End Property

    Public ReadOnly Property NumeroLote() As String
        Get
            Return Me.NRO_LOTE
        End Get
    End Property

    Public ReadOnly Property NumeroPartida() As String
        Get
            Return Me.NRO_PARTIDA
        End Get
    End Property

    Public ReadOnly Property U_EMPAQUE() As String
        Get
            Return Me.UC_EMPAQUE
        End Get
    End Property

#End Region

    Public Sub Limpiar()
        ClienteID = ""
        CodigoViaje = ""
        ProductoID = ""
        CantidadPendiente = 0
        cCarrosEmpaque.Clear()
        cContenedoresNoValidos.Clear()
        vSOLICITA_LOTE_PARTIDA = False
        UC_EMPAQUE = ""
        PEDIDO = ""
        SERIE = ""
        vULTIMO_PEDIDO = ""
        ULTIMA_UC_EMPAQUE = ""
    End Sub

    Private Function GetInfoByContenedor(ByVal Contenedora As String, ByRef vClienteID As String, ByRef vCodigoViaje As String) As Boolean
        '--------------------------------------------------------------------------------------------------------------------------------
        'CUANDO ME DAN EL PRIMER CARRO A EMPAQUETAR BUSCO LA INFORMACION.
        '--------------------------------------------------------------------------------------------------------------------------------
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandType = CommandType.Text
                Cmd.CommandText = "SELECT DISTINCT CLIENTE_ID, VIAJE_ID,DOCUMENTO_ID FROM PICKING WHERE PALLET_PICKING=" & Contenedora & " AND NRO_UCEMPAQUETADO IS NULL "
                DA.Fill(DS)
                If DS.Tables.Count > 0 Then
                    vClienteID = DS.Tables(0).Rows(0)(0).ToString
                    vCodigoViaje = DS.Tables(0).Rows(0)(1).ToString
                End If
            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
        Finally
            Cmd.Dispose()
            DA.Dispose()
            DS.Dispose()
        End Try
    End Function

    Private Function SolicitaLotePartidaBD(ByVal ClienteID As String) As Boolean
        '--------------------------------------------------------------------------------------------------------------------------------
        'ME DETERMINA SI VOY A USAR O NO CONFIRMACION PARA EL LOTE Y LA PARTIDA.
        '--------------------------------------------------------------------------------------------------------------------------------
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandType = CommandType.Text
                Cmd.CommandText = "SELECT ISNULL(FLG_EMP_LOTE_PARTIDA,'0')AS SOLICITA FROM CLIENTE_PARAMETROS WHERE CLIENTE_ID=" & ClienteID
                DA.Fill(DS)
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows(0)(0).ToString = "1" Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
        Finally
            Cmd.Dispose()
            DA.Dispose()
            DS.Dispose()
        End Try
    End Function

    Public Function QuitarCarro(ByVal CarroEmpaque As String) As Boolean
        '--------------------------------------------------------------------------------------------------------------------------------
        'QUITA UN CARRO DE LA LISTA A EMPAQUETAR.
        '--------------------------------------------------------------------------------------------------------------------------------
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter
        Dim vCarros As String, Encontro As Boolean = False, mCollection As New Collection
        Try

            Cmd = Conn.CreateCommand
            DA = New SqlDataAdapter(Cmd)
            DS = New DataSet

            Cmd.CommandText = "[dbo].[MOB_EMPAQUETADO_QUITAR_CONTENEDORA]"
            Cmd.CommandType = CommandType.StoredProcedure

            PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
            PA.Value = Me.CodigoViaje
            Cmd.Parameters.Add(PA)
            PA = Nothing

            PA = New SqlParameter("@CONTENEDOR", SqlDbType.VarChar, 100)
            PA.Value = CarroEmpaque
            Cmd.Parameters.Add(PA)
            PA = Nothing

            Cmd.ExecuteNonQuery()

            For Each vCarros In cCarrosEmpaque
                If vCarros <> CarroEmpaque Then
                    mCollection.Add(vCarros)
                    'cCarrosEmpaque.Remove(vCarros)
                Else
                    Encontro = True
                End If
            Next
            cCarrosEmpaque = mCollection
            If Encontro = False Then
                MsgBox("No se encontro el contenedor solicitado.", MsgBoxStyle.Information, ClsName)
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally

        End Try
    End Function

    Public Function AgregarCarroEmpaque(ByVal CarroEmpaque As String) As Boolean
        '--------------------------------------------------------------------------------------------------------------------------------
        'VALIDA Y GUARDA LOS CARROS DE PICKING / DESCONSOLIDACION QUE SE DESEAN EMPAQUETAR.
        '--------------------------------------------------------------------------------------------------------------------------------
        Dim vCarros As String = "", ContOK As Boolean = False, vClienteID As String = "", vCodigoViaje As String = ""
        Try
            For Each vCarros In cCarrosEmpaque
                If vCarros = CarroEmpaque Then
                    MsgBox("El Carro de empaque " & CarroEmpaque & " ya ha sido seleccionado.")
                    Return False
                End If
            Next

            'Valido el carro para saber si esta o no en condiciones.
            If ValidarContenedora(CarroEmpaque, ContOK) Then
                If ContOK = False Then
                    MsgBox("El contenedor " & CarroEmpaque & ", no se encuentra disponible.", MsgBoxStyle.OkOnly, ClsName)
                    Return False
                End If
            Else
                Return False
            End If

            'Levanto Informacion del Carro.
            If GetInfoByContenedor(CarroEmpaque, vClienteID, vCodigoViaje) Then
                If Me.ClienteID = "" And Me.CodigoViaje = "" Then
                    Me.ClienteID = vClienteID
                    Me.CodigoViaje = vCodigoViaje
                Else
                    If Me.ClienteID <> vClienteID Then
                        MsgBox("El contenedor seleccionado posee un cliente diferente. No es posible continuar.", MsgBoxStyle.Exclamation, ClsName)
                        Return False
                    End If
                    If Me.CodigoViaje <> vCodigoViaje Then
                        MsgBox("El contenedor seleccionado posee una ola diferente. No es posible continuar.", MsgBoxStyle.Exclamation, ClsName)
                        Return False
                    End If
                End If
            End If
            'agrego a la coleccion de carros.
            If GuardarContenedorAEmpacar(CarroEmpaque) Then
                Return True
            End If
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
        End Try
    End Function

    Private Function GuardarContenedorAEmpacar(ByVal CarroEmpaque As String) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter, I As Integer

        Try
            If VerifyConnection(Conn) Then

                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandText = "DBO.MOB_EMPAQUE_ADD"
                Cmd.CommandType = CommandType.StoredProcedure

                PA = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                PA.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.CodigoViaje
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CONTENEDOR", SqlDbType.VarChar, 100)
                PA.Value = CarroEmpaque
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@FLG_EN_CURSO", SqlDbType.VarChar, 1)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                DA.Fill(DS)

                'LIMPIO LA COLECCION PORQUE PUEDE HABER DOS PERSONAS O MAS CON ESTE VIAJE.
                Me.cCarrosEmpaque.Clear()

                If DS.Tables.Count > 0 Then
                    For I = 0 To DS.Tables(0).Rows.Count - 1

                        'CARGO EL LISTADO DE CONTENEDORAS VALIDADAS.
                        Me.cCarrosEmpaque.Add(DS.Tables(0).Rows(I)(0).ToString())

                    Next
                End If
            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DS.Dispose()
            Cmd.Dispose()
            DA.Dispose()
            PA = Nothing
        End Try
    End Function

    Private Function ValidarContenedora(ByVal Contenedora As String, ByRef ContOK As Boolean) As Boolean
        '--------------------------------------------------------------------------------------------------------------------------------
        'DETERMINA SI EXISTE O NO LA CONTENEDORA.
        '--------------------------------------------------------------------------------------------------------------------------------
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = "DBO.MOB_EMPAQUETADO_VERIFICA_CONTENEDORA"

                PA = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                PA.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CONTENEDOR", SqlDbType.VarChar, 100)
                PA.Value = Contenedora
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@EXISTE", SqlDbType.VarChar, 1)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()

                If Cmd.Parameters("@EXISTE").Value = "0" Then
                    ContOK = False
                Else
                    ContOK = True
                End If
            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DA.Dispose()
            Cmd.Dispose()
            DS.Dispose()
            PA = Nothing
        End Try
    End Function

    Private Function GetContenedoras() As String
        '--------------------------------------------------------------------------------------------------------------------------------
        'ACUMULO LAS CONTENEDORAS Y LAS DEVUELVO EN UN STRING
        '--------------------------------------------------------------------------------------------------------------------------------
        Dim Contenedora As String = "", Retorno As String = ""
        Try
            For Each Contenedora In cCarrosEmpaque
                Retorno = Retorno & Contenedora & ";"
            Next

            Return Retorno
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
        End Try
    End Function

    Public Function BuscarDescripcion2D(ByRef Descripcion As Label) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, vProductoID As String = ""
        Dim PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then

                Cmd = Conn.CreateCommand
                Cmd.CommandType = CommandType.StoredProcedure

                Cmd.CommandText = "DBO.MOB_EMP_GET_DESCRIPCION_CANTIDAD_2D"

                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.CodigoViaje
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.ClienteID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                PA.Value = Me.ProductoID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CONTENEDORAS", SqlDbType.VarChar, 4000)
                PA.Value = Me.GetContenedoras & "0"
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PEDIDO", SqlDbType.VarChar, 100)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@DESCRIPCION", SqlDbType.VarChar, 200)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CANTIDAD", SqlDbType.BigInt)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@SOLICITA_LP", SqlDbType.VarChar, 1)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@LOTE", SqlDbType.VarChar, 100)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PARTIDA", SqlDbType.VarChar, 100)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 100)
                PA.Value = o2D.Get_Series
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CONT_EMPAQUE", SqlDbType.VarChar, 100)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                PA.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()

                If Cmd.Parameters("@CANTIDAD").Value > 0 Then
                    Me.UC_EMPAQUE = Cmd.Parameters("@CONT_EMPAQUE").Value.ToString
                    Me.NRO_LOTE = Cmd.Parameters("@LOTE").Value.ToString
                    Me.NRO_PARTIDA = Cmd.Parameters("@PARTIDA").Value.ToString
                    Me.SERIE = Cmd.Parameters("@NRO_SERIE").Value.ToString
                    Me.PEDIDO = Cmd.Parameters("@PEDIDO").Value.ToString
                    Me.CANTIDADPENDIENTE = Cmd.Parameters("@CANTIDAD").Value

                    Descripcion.Text = Cmd.Parameters("@DESCRIPCION").Value & vbNewLine & "- Cantidad Pendiente: " & Cmd.Parameters("@CANTIDAD").Value & vbNewLine
                    Descripcion.Text = Descripcion.Text & "- Guardar en caja Empaque: " & Cmd.Parameters("@CONT_EMPAQUE").Value.ToString & vbNewLine
                    Descripcion.Text = Descripcion.Text & "- Nro. Lote: " & Cmd.Parameters("@LOTE").Value.ToString & vbNewLine
                    Descripcion.Text = Descripcion.Text & "- Nro. Partida: " & Cmd.Parameters("@PARTIDA").Value.ToString

                    If SERIE <> "" Then
                        Descripcion.Text = Descripcion.Text & vbNewLine & "- Nro. Serie: " & SERIE
                    End If

                    If Cmd.Parameters("@SOLICITA_LP").Value = "1" Then
                        vSOLICITA_LOTE_PARTIDA = True
                    Else
                        vSOLICITA_LOTE_PARTIDA = False
                    End If
                Else
                    MsgBox("El producto indicado no posee cantidades pendientes para empaquetar.", MsgBoxStyle.Exclamation, ClsName)
                    Return False
                End If

            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DA.Dispose()
            DS.Dispose()
            Cmd.Dispose()
        End Try
    End Function

    Public Function BuscarDescripcion(ByRef Descripcion As Label, ByRef ProductoID As TextBox) As Boolean
        '--------------------------------------------------------------------------------------------------------------------------------
        'DEVUELVE LA DESCRIPCION DEL PRODUCTO Y LA CANTIDAD PENDIENTE DE EMPAQUETAR.
        '@VIAJE_ID		    VARCHAR(100),
        '@CLIENTE_ID		VARCHAR(15),
        '@PRODUCTO_ID	VARCHAR(30),
        '@CONTENEDORAS	VARCHAR(4000),
        '@PEDIDO			    VARCHAR(100)OUTPUT,
        '@DESCRIPCION	    VARCHAR(200)OUTPUT,
        '@CANTIDAD		    BIGINT		OUTPUT,
        '@SOLICITA_LP	    VARCHAR(1)	OUTPUT,
        '@LOTE			        VARCHAR(100)OUTPUT,
        '@PARTIDA		    VARCHAR(100)OUTPUT
        '@NRO_SERIE         VARCHAR(100)OUTPUT
        '@USUARIO            VARCHAR(100)OUTPUT
        '--------------------------------------------------------------------------------------------------------------------------------
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, vProductoID As String = ""
        Dim PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then

                o2D.CLIENTE_ID = Me.ClienteID
                o2D.Decode(ProductoID.Text)
                vProductoID = o2D.PRODUCTO_ID

                If vProductoID <> ProductoID.Text Then
                    ProductoID.Text = vProductoID
                End If

                Me.ProductoID = vProductoID

                If o2D.QtySeries > 0 Then
                    If Me.BuscarDescripcion2D(Descripcion) Then
                        Return True
                    Else
                        Return False
                    End If
                End If
                Cmd = Conn.CreateCommand
                Cmd.CommandType = CommandType.StoredProcedure

                Cmd.CommandText = "DBO.MOB_EMP_GET_DESCRIPCION_CANTIDAD"

                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.CodigoViaje
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.ClienteID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                PA.Value = Me.ProductoID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CONTENEDORAS", SqlDbType.VarChar, 4000)
                PA.Value = Me.GetContenedoras & "0"
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PEDIDO", SqlDbType.VarChar, 100)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@DESCRIPCION", SqlDbType.VarChar, 200)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CANTIDAD", SqlDbType.BigInt)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@SOLICITA_LP", SqlDbType.VarChar, 1)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@LOTE", SqlDbType.VarChar, 100)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PARTIDA", SqlDbType.VarChar, 100)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 100)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CONT_EMPAQUE", SqlDbType.VarChar, 100)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                PA.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()

                If Cmd.Parameters("@CANTIDAD").Value > 0 Then
                    Me.UC_EMPAQUE = Cmd.Parameters("@CONT_EMPAQUE").Value.ToString
                    Me.NRO_LOTE = Cmd.Parameters("@LOTE").Value.ToString
                    Me.NRO_PARTIDA = Cmd.Parameters("@PARTIDA").Value.ToString
                    Me.SERIE = Cmd.Parameters("@NRO_SERIE").Value.ToString
                    Me.PEDIDO = Cmd.Parameters("@PEDIDO").Value.ToString
                    Me.CANTIDADPENDIENTE = Cmd.Parameters("@CANTIDAD").Value

                    Descripcion.Text = Cmd.Parameters("@DESCRIPCION").Value & vbNewLine & "- Cantidad Pendiente: " & Cmd.Parameters("@CANTIDAD").Value & vbNewLine
                    Descripcion.Text = Descripcion.Text & "- Guardar en caja Empaque: " & Cmd.Parameters("@CONT_EMPAQUE").Value.ToString & vbNewLine
                    Descripcion.Text = Descripcion.Text & "- Nro. Lote: " & Cmd.Parameters("@LOTE").Value.ToString & vbNewLine
                    Descripcion.Text = Descripcion.Text & "- Nro. Partida: " & Cmd.Parameters("@PARTIDA").Value.ToString

                    If SERIE <> "" Then
                        Descripcion.Text = Descripcion.Text & vbNewLine & "- Nro. Serie: " & SERIE
                    End If

                    If Cmd.Parameters("@SOLICITA_LP").Value = "1" Then
                        vSOLICITA_LOTE_PARTIDA = True
                    Else
                        vSOLICITA_LOTE_PARTIDA = False
                    End If
                Else
                    MsgBox("El producto indicado no posee cantidades pendientes para empaquetar.", MsgBoxStyle.Exclamation, ClsName)
                    Return False
                End If

            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            If Not IsNothing(DA) Then DA.Dispose()
            If Not IsNothing(DS) Then DS.Dispose()
            If Not IsNothing(Cmd) Then Cmd.Dispose()
        End Try
    End Function

    Private Function GenerarContenedorEmpaquetado() As String
        '--------------------------------------------------------------------------------------------------------------------------------
        'GENERA LOS NUMEROS DE CONTENEDORAS.
        '--------------------------------------------------------------------------------------------------------------------------------
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter
        Try

            Cmd = Conn.CreateCommand
            DA = New SqlDataAdapter(Cmd)
            DS = New DataSet
            Cmd.CommandText = "DBO.GET_VALUE_FOR_SEQUENCE"

            PA = New SqlParameter("@SECUENCIA", SqlDbType.VarChar, 100)
            PA.Value = "UC_EMPAQUE"
            Cmd.Parameters.Add(PA)
            PA = Nothing

            PA = New SqlParameter("@VALUE", SqlDbType.BigInt)
            PA.Direction = ParameterDirection.Output
            Cmd.Parameters.Add(PA)

            Cmd.ExecuteNonQuery()

            GenerarContenedorEmpaquetado = Cmd.Parameters("@VALUE").Value

        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
        Finally
            DA.Dispose()
            Cmd.Dispose()
            DS.Dispose()
            PA = Nothing
        End Try
    End Function

    Private Function GetContenedoresNoValidados() As String
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, I As Integer, PA As SqlParameter
        Dim RET As String = ""
        Try
            Cmd = Conn.CreateCommand
            DA = New SqlDataAdapter(Cmd)
            DS = New DataSet
            Cmd.CommandText = "[DBO].[MOB_EMPAQUE_CONTENEDORAS_NO_VALIDAS]"
            Cmd.CommandType = CommandType.StoredProcedure

            PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
            PA.Value = Me.ClienteID
            Cmd.Parameters.Add(PA)
            PA = Nothing

            PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
            PA.Value = Me.CodigoViaje
            Cmd.Parameters.Add(PA)

            DA.Fill(DS)
            Me.cContenedoresNoValidos.Clear()
            If DS.Tables.Count > 0 Then
                For I = 0 To DS.Tables(0).Rows.Count - 1
                    'CARGO EL LISTADO DE CONTENEDORAS NO VALIDADAS.
                    Me.cContenedoresNoValidos.Add(DS.Tables(0).Rows(I)(0).ToString())
                    RET = RET & DS.Tables(0).Rows(I)(0).ToString() & ";"
                Next
            End If
            Return RET
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
        Finally
            DA.Dispose()
            DS.Dispose()
            Cmd.Dispose()
            PA = Nothing
        End Try
    End Function

    Public Function ConfirmarCantidad(ByVal CANT_CONTROLADA As Double) As Boolean
        Try
            If CANT_CONTROLADA > Me.CantidadPendiente Then
                MsgBox("La cantidad Controlada supera a la cantidad pendiente del producto.", MsgBoxStyle.Information, ClsName)
                Return False
            Else
                If Not Me.RegistrarProductoEnContenedora(CANT_CONTROLADA) Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        End Try
    End Function

    Private Function RegistrarProductoEnContenedora(ByVal CANT_CONTROLADA As Double) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                Cmd.CommandText = "[DBO].[MOB_REGISTRA_TMP_PRODUCTO_EMPAQUE]"
                Cmd.CommandType = CommandType.StoredProcedure
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                '.Fields.Append("Cliente_Id", adVarChar, 15)
                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.ClienteID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                '.Fields.Append("Pedido_Id", adVarChar, 100)
                PA = New SqlParameter("@PEDIDO_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.PEDIDO
                Me.vULTIMO_PEDIDO = Me.PEDIDO
                Cmd.Parameters.Add(PA)
                PA = Nothing

                '.Fields.Append("Producto_Id", adVarChar, 30)
                PA = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                PA.Value = Me.ProductoID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                '.Fields.Append("Nro_Contenedora", adDouble)
                PA = New SqlParameter("@NRO_CONTENEDORA", SqlDbType.BigInt)
                PA.Value = Me.UC_EMPAQUE
                Me.ULTIMA_UC_EMPAQUE = Me.UC_EMPAQUE
                Cmd.Parameters.Add(PA)
                PA = Nothing

                '.Fields.Append("Cant_Controlada", adDouble)
                PA = New SqlParameter("@CANT_CONTROLADA", SqlDbType.Float)
                PA.Value = CANT_CONTROLADA
                Cmd.Parameters.Add(PA)
                PA = Nothing
                '.Fields.Append("Nro_Lote", adVarChar, 100)

                PA = New SqlParameter("@NRO_LOTE", SqlDbType.VarChar, 100)
                If Me.NRO_LOTE <> "" Then
                    PA.Value = Me.NRO_LOTE
                Else : PA.Value = DBNull.Value
                End If
                Cmd.Parameters.Add(PA)
                PA = Nothing

                '.Fields.Append("Nro_Partida", adVarChar, 100)
                PA = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 100)
                If Me.NRO_PARTIDA <> "" Then
                    PA.Value = Me.NRO_PARTIDA
                Else
                    PA.Value = DBNull.Value
                End If
                Cmd.Parameters.Add(PA)
                PA = Nothing

                '.Fields.Append("Nro_Serie", adVarChar, 50)
                PA = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 50)
                If Me.NRO_SERIE <> "" Then
                    PA.Value = Me.NRO_SERIE
                Else : PA.Value = DBNull.Value
                End If
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CONTROLA_LP", SqlDbType.VarChar, 1)
                PA.Value = IIf(Me.SolicitaLotePartida = True, "1", "0")
                Cmd.Parameters.Add(PA)
                PA = Nothing
                Cmd.ExecuteNonQuery()

            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DA = Nothing
            Cmd = Nothing
            DS = Nothing
            PA = Nothing

        End Try
    End Function

    Public Function CerrarContenedora(ByVal CONTENEDORA As String, ByVal PEDIDO As String) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandText = "[DBO].[MOB_CERRAR_TMP_PRODUCTO_EMPAQUE]"
                Cmd.CommandType = CommandType.StoredProcedure

                '.Fields.Append("Cliente_Id", adVarChar, 15)
                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.ClienteID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                '.Fields.Append("Pedido_Id", adVarChar, 100)
                PA = New SqlParameter("@PEDIDO_ID", SqlDbType.VarChar, 100)
                PA.Value = PEDIDO
                Cmd.Parameters.Add(PA)
                PA = Nothing

                '.Fields.Append("Nro_Contenedora", adDouble)
                PA = New SqlParameter("@NRO_CONTENEDORA", SqlDbType.BigInt)
                PA.Value = Contenedora
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()

            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DA.Dispose()
            DS.Dispose()
            Cmd.Dispose()
            PA = Nothing
        End Try
    End Function

    Public Function GetContenedoresGenerados(ByRef Grid As DataGrid) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandText = "DBO.MOB_EMPAQUE_GET_CONTENEDORAS"
                Cmd.CommandType = CommandType.StoredProcedure

                PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.CodigoViaje
                Cmd.Parameters.Add(PA)
                PA = Nothing

                DA.Fill(DS)
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        Grid.DataSource = DS.Tables(0)
                        AutoSizeGrid(Grid, ClsName)
                    Else
                        MsgBox("No hay contenedoras generadas para el Codigo de Ola " & Me.CodigoViaje & ".", MsgBoxStyle.OkCancel, ClsName)
                        Return False
                    End If
                End If
            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DA.Dispose()
            DS.Dispose()
            Cmd.Dispose()
            PA = Nothing
        End Try
    End Function

    Public Function ContenidoCajaEmpaque(ByVal Contenedor As String, ByVal Pedido As String, ByRef Grid As DataGrid) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter

        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandText = "[dbo].[mob_busca_caja_contenedora_empaque]"
                Cmd.CommandType = CommandType.StoredProcedure

                '@CLIENTE_ID         as varchar(15) OUTPUT,
                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.ClienteID
                Cmd.Parameters.Add(PA)
                PA = Nothing
                '@PEDIDO_ID          as varchar(100) OUTPUT,
                PA = New SqlParameter("@PEDIDO_ID", SqlDbType.VarChar, 100)
                PA.Value = Pedido
                Cmd.Parameters.Add(PA)
                PA = Nothing
                '@NRO_CONTENEDORA    as numeric(20) OUTPUT
                PA = New SqlParameter("@NRO_CONTENEDORA", SqlDbType.VarChar, 100)
                PA.Value = Contenedor
                Cmd.Parameters.Add(PA)
                PA = Nothing

                DA.Fill(DS)

                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        Grid.DataSource = DS.Tables(0)
                        AutoSizeGrid(Grid, ClsName)
                    Else
                        MsgBox("La contenedora se encuentra vacia.", MsgBoxStyle.Information, ClsName)
                        Return False
                    End If
                End If
            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DA.Dispose()
            DS.Dispose()
            Cmd.Dispose()
            PA = Nothing
        End Try
    End Function

    Public Function QuitarContenido(ByVal PEDIDO As String, ByVal LOTE As String, ByVal PARTIDA As String, ByVal SERIE As String, _
                                                ByVal PRODUCTO As String, ByVal CONTENEDOR As String, ByVal CANTIDAD As String) As Boolean

        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandText = "[dbo].[mob_quitar_producto_empaque]"
                Cmd.CommandType = CommandType.StoredProcedure
                '@CLIENTE_ID         as varchar(15) OUTPUT,
                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.ClienteID
                Cmd.Parameters.Add(PA)
                PA = Nothing
                '@PEDIDO_ID          as varchar(100) OUTPUT,
                PA = New SqlParameter("@PEDIDO_ID", SqlDbType.VarChar, 100)
                PA.Value = PEDIDO
                Cmd.Parameters.Add(PA)
                PA = Nothing
                '@NRO_LOTE			AS VARCHAR(100) OUTPUT,
                PA = New SqlParameter("@NRO_LOTE", SqlDbType.VarChar, 100)
                If Trim(LOTE) <> "" Then
                    PA.Value = LOTE
                Else : PA.Value = DBNull.Value
                End If
                Cmd.Parameters.Add(PA)
                PA = Nothing
                '@NRO_PARTIDA		AS VARCHAR(100) OUTPUT,
                PA = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 100)
                If Trim(PARTIDA) <> "" Then
                    PA.Value = PARTIDA
                Else : PA.Value = DBNull.Value
                End If
                Cmd.Parameters.Add(PA)
                PA = Nothing
                '@NRO_SERIE			AS VARCHAR(50) OUTPUT,
                PA = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 100)
                If Trim(SERIE) <> "" Then
                    PA.Value = SERIE
                Else : PA.Value = DBNull.Value
                End If

                Cmd.Parameters.Add(PA)
                PA = Nothing
                '@PRODUCTO_ID        as varchar(30) OUTPUT,
                PA = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 100)
                PA.Value = PRODUCTO
                Cmd.Parameters.Add(PA)
                PA = Nothing
                '@NRO_CONTENEDORA    as numeric(20) OUTPUT,
                PA = New SqlParameter("@NRO_CONTENEDORA", SqlDbType.VarChar, 100)
                PA.Value = CONTENEDOR
                Cmd.Parameters.Add(PA)
                PA = Nothing
                '@CANT_CONTROLADA    as numeric(20,5) OUTPUT
                PA = New SqlParameter("@CANT_CONTROLADA", SqlDbType.Float)
                PA.Value = CANTIDAD
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()

            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DA.Dispose()
            DS.Dispose()
            Cmd.Dispose()
        End Try
    End Function

    Public Function AbrirContenedora(ByVal PEDIDO As String, ByVal CONTENEDOR As String) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter
        Try

            If VerifyConnection(Conn) Then

                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandText = "[dbo].[MOB_ABRIR_EMPAQUE]"
                Cmd.CommandType = CommandType.StoredProcedure


                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.ClienteID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.CodigoViaje
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CONTENEDOR", SqlDbType.VarChar, 100)
                PA.Value = CONTENEDOR
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()

                MsgBox("La contenedora se abrio correctamente.", MsgBoxStyle.OkOnly, ClsName)

            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DA.Dispose()
            DS.Dispose()
            Cmd.Dispose()
            PA = Nothing
        End Try
    End Function

    Public Function EmpaquetadoCompleto() As Boolean
        Try

            '-------------------------------------------------------------------------------------
            '1. Verifico si se completo el empaquetado.
            '-------------------------------------------------------------------------------------
            If Not VerificarEmpaqueCompletado() Then
                MsgBox("El empaquetado no se encuentra completo. No es posible finalizar.", MsgBoxStyle.Information, ClsName)
                Return False
            End If
            '-------------------------------------------------------------------------------------
            '2. Envio a cerrar todas las contenedoras que estan abiertas.
            '-------------------------------------------------------------------------------------
            If Not Me.CerrarConenedorasMasivo Then
                Return False
            End If
            '-------------------------------------------------------------------------------------
            'Mando a Imprimir el Packing List
            '-------------------------------------------------------------------------------------
            ImpresionPackigList()
            '-------------------------------------------------------------------------------------
            '4. Envio la informacion al ERP para facturar.
            '-------------------------------------------------------------------------------------
            If Not Me.EnviarPedidoERP Then
                Return False
            End If

            MsgBox("Se ha finalizado con el empaquetado...", MsgBoxStyle.Information, ClsName)

            Return True
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
        End Try
    End Function

    Private Function ImpresionPackigList() As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, SQL As String = ""
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                SQL = ""
                SQL = SQL & "INSERT INTO IMPRESION_PACKING" & vbNewLine
                SQL = SQL & "SELECT	CLIENTE_ID, NRO_REMITO, '0' " & vbNewLine
                SQL = SQL & "FROM    DOCUMENTO" & vbNewLine
                SQL = SQL & "WHERE	CLIENTE_ID='" & Me.ClienteID & "'" & vbNewLine
                SQL = SQL & "            AND NRO_DESPACHO_IMPORTACION='" & Me.CodigoViaje & "'" & vbNewLine

                Cmd.CommandText = SQL
                Cmd.CommandType = CommandType.Text

                Cmd.ExecuteNonQuery()

            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally

        End Try
    End Function

    Private Function CerrarConenedorasMasivo() As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, SQL As String = "", CONTENEDOR As String = "", PEDIDO As String = ""
        Dim I As Integer = 0
        Try
            SQL = SQL & "SELECT	    DISTINCT M.UC_EMPAQUE, D.NRO_REMITO" & vbNewLine
            SQL = SQL & "FROM    	PICKING P INNER JOIN DOCUMENTO D	ON(P.DOCUMENTO_ID=D.DOCUMENTO_ID)" & vbNewLine
            SQL = SQL & "		        INNER JOIN MOB_EMPAQUE_UC_EMPAQUE M	ON(D.DOCUMENTO_ID=M.DOCUMENTO_ID)" & vbNewLine
            SQL = SQL & "WHERE  	P.CLIENTE_ID='" & Me.ClienteID & "'" & vbNewLine
            SQL = SQL & "		        AND P.VIAJE_ID='" & Me.CodigoViaje & "'" & vbNewLine

            Cmd = Conn.CreateCommand
            DA = New SqlDataAdapter(Cmd)
            DS = New DataSet
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = SQL

            DA.Fill(DS)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    For I = 0 To DS.Tables(0).Rows.Count - 1
                        CONTENEDOR = DS.Tables(0).Rows(I)(0).ToString
                        PEDIDO = DS.Tables(0).Rows(I)(1).ToString
                        If Not CerrarContenedora(CONTENEDOR, PEDIDO) Then
                            Return False
                        End If
                    Next
                End If
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DS.Dispose()
            DA.Dispose()
            Cmd.Dispose()
        End Try
    End Function

    Public Function VerificarEmpaqueCompletado() As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter
        Try

            If VerifyConnection(Conn) Then

                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandText = "[DBO].[MOB_VERIFICAR_EMPAQUE_COMPLETADO]"
                Cmd.CommandType = CommandType.StoredProcedure

                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.ClienteID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.CodigoViaje
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@RESULTADO", SqlDbType.VarChar, 100)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()
                If Cmd.Parameters("@RESULTADO").Value = "0" Then
                    Return False
                Else
                    Return True
                End If
            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DA.Dispose()
            DS.Dispose()
            Cmd.Dispose()
            PA = Nothing
        End Try
    End Function

    Private Function EnviarPedidoERP() As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter
        Try

            If VerifyConnection(Conn) Then

                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandText = "[dbo].[MOB_INFORMAR_EMPAQUE_ERP]"
                Cmd.CommandType = CommandType.StoredProcedure

                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.ClienteID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.CodigoViaje
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                PA.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()
    
            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DA.Dispose()
            DS.Dispose()
            Cmd.Dispose()
            PA = Nothing
        End Try
    End Function

    Public Function VALIDAR_SERIE(ByVal SERIE As String) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                '1. VALIDO SI LA SERIE ES IGUAL A LA ASIGNADA
                If Me.NRO_SERIE <> Trim(UCase(SERIE)) Then
                    MsgBox("El numero de serie indicado no es correcto.", MsgBoxStyle.OkOnly, ClsName)
                    Return False
                End If
            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally

        End Try
    End Function

    Public Function RegistrarProductoEnContenedora2D() As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter, TRANS As SqlTransaction
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                Cmd.CommandText = "DBO.MOB_REGISTRA_TMP_PRODUCTO_EMPAQUE_2D"
                Cmd.CommandType = CommandType.StoredProcedure

                TRANS = Conn.BeginTransaction

                Cmd.Transaction = TRANS

                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                '.Fields.Append("Cliente_Id", adVarChar, 15)
                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.ClienteID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                '.Fields.Append("Pedido_Id", adVarChar, 100)
                PA = New SqlParameter("@PEDIDO_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.PEDIDO
                Me.vULTIMO_PEDIDO = Me.PEDIDO
                Cmd.Parameters.Add(PA)
                PA = Nothing

                '.Fields.Append("Producto_Id", adVarChar, 30)
                PA = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                PA.Value = Me.ProductoID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                '.Fields.Append("Nro_Contenedora", adDouble)
                PA = New SqlParameter("@NRO_CONTENEDORA", SqlDbType.BigInt)
                PA.Value = Me.UC_EMPAQUE
                Me.ULTIMA_UC_EMPAQUE = Me.UC_EMPAQUE
                Cmd.Parameters.Add(PA)
                PA = Nothing

                '.Fields.Append("Cant_Controlada", adDouble)
                PA = New SqlParameter("@CANT_CONTROLADA", SqlDbType.Float)
                PA.Value = o2D.QtySeries
                Cmd.Parameters.Add(PA)
                PA = Nothing
                '.Fields.Append("Nro_Lote", adVarChar, 100)

                PA = New SqlParameter("@NRO_LOTE", SqlDbType.VarChar, 100)
                If Me.NRO_LOTE <> "" Then
                    PA.Value = Me.NRO_LOTE
                Else : PA.Value = DBNull.Value
                End If
                Cmd.Parameters.Add(PA)
                PA = Nothing

                '.Fields.Append("Nro_Partida", adVarChar, 100)
                PA = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 100)
                If Me.NRO_PARTIDA <> "" Then
                    PA.Value = Me.NRO_PARTIDA
                Else
                    PA.Value = DBNull.Value
                End If
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@DESDE", SqlDbType.BigInt)
                PA.Value = o2D.SERIE_INICIO
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@HASTA", SqlDbType.BigInt)
                PA.Value = o2D.SERIE_FIN
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CONTROLA_LP", SqlDbType.VarChar, 1)
                PA.Value = IIf(Me.SolicitaLotePartida = True, "1", "0")
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()
                TRANS.Commit()

            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            TRANS.Rollback()
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            TRANS.Rollback()
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DA.Dispose()
            Cmd.Dispose()
            DS.Dispose()
            PA = Nothing
            TRANS.Dispose()
        End Try
    End Function

    Public Function GetPendientesDeEmpaquetar(ByRef GRID As DataGrid) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                Cmd = Conn.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandText = "DBO.MOB_GET_PENDIENTES"
                Cmd.CommandType = CommandType.StoredProcedure

                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.ClienteID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.CodigoViaje
                Cmd.Parameters.Add(PA)
                PA = Nothing

                DA.Fill(DS)

                GRID.DataSource = DS.Tables(0)

            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DS.Dispose()
            DA.Dispose()
            Cmd.Dispose()
        End Try
    End Function

    Public Function LiberarTarea() As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, vProductoID As String = ""
        Dim PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then

                Cmd = Conn.CreateCommand
                Cmd.CommandType = CommandType.StoredProcedure

                Cmd.CommandText = "[dbo].[MOB_EMP_LIBERAR_TAREA_TOMADA]"

                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.CodigoViaje
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.ClienteID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                PA.Value = Me.ProductoID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CONTENEDORAS", SqlDbType.VarChar, 4000)
                PA.Value = Me.GetContenedoras & "0"
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PEDIDO", SqlDbType.VarChar, 100)
                PA.Value = Me.PEDIDO
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CANTIDAD", SqlDbType.BigInt)
                PA.Value = Me.CANTIDADPENDIENTE
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@SOLICITA_LP", SqlDbType.VarChar, 1)
                PA.Value = IIf(Me.SolicitaLotePartida = True, "1", "0")
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@LOTE", SqlDbType.VarChar, 100)
                PA.Value = IIf(Trim(Me.NRO_LOTE) <> "", Me.NRO_LOTE, DBNull.Value)
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PARTIDA", SqlDbType.VarChar, 100)
                PA.Value = IIf(Trim(Me.NRO_PARTIDA) <> "", Me.NRO_PARTIDA, DBNull.Value)
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 100)
                PA.Value = IIf(Trim(Me.NRO_SERIE) <> "", Me.NRO_SERIE, DBNull.Value)
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CONT_EMPAQUE", SqlDbType.VarChar, 100)
                PA.Value = Me.U_EMPAQUE
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                PA.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()

            Else : MsgBox(SQLConnection, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            DA.Dispose()
            DS.Dispose()
            Cmd.Dispose()
        End Try
    End Function

End Class