Imports System.Data.SqlClient
Imports System.Data

Public Class FuncionesEgreso

    Private Const SQLConErr As String = "Fallo al intentar conectar con la base de datos."
    Private Const ClsName As String = "FuncionesEgreso"
    Private oCmd As SqlCommand
    Private vCierrePallet As Boolean = False
    Private vH As Boolean 'La uso para saber si pickea con vehiculos.

    Public Property Vehiculo() As Boolean
        Get
            Return vH
        End Get
        Set(ByVal value As Boolean)
            vH = value
        End Set
    End Property

    Public Property Cmd() As SqlCommand
        Get
            Return oCmd
        End Get
        Set(ByVal value As SqlCommand)
            oCmd = value
        End Set
    End Property

    Public Property CerrarPallet() As Boolean
        Get
            Return vCierrePallet
        End Get
        Set(ByVal value As Boolean)
            vCierrePallet = value
        End Set
    End Property

    Public Function GetFlgGenNewPicking(ByVal ClienteId As String) As Boolean
        Dim Pa As SqlParameter
        Dim Flag As String

        Cmd.Parameters.Clear()
        Try
            If VerifyConnection(SQLc) Then
                Cmd.CommandText = "GET_FLG_GEN_NEWPICKING"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = ClienteId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@FLAG", SqlDbType.VarChar, 1)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Cmd.ExecuteNonQuery()
                Flag = IIf(IsDBNull(Cmd.Parameters("@Flag").Value), "0", Cmd.Parameters("@Flag").Value).ToString
                Return (Flag = "1")
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("GetFlgGenNewPicking: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
        Catch ex As Exception
            'Tran.Rollback()
            MsgBox("GetFlgGenNewPicking: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            Pa = Nothing
        End Try
    End Function
    'Traer el valor total de la cantidad a pickear para un producto en un mismo documento_id
    Public Function GetCantidadxProducto(ByVal ViajeID as String, ByVal Producto_Id As String) As Double
        Dim Pa As SqlParameter
        Dim ValorTotal As Double = 0

        Cmd.Parameters.Clear()
        Try
            If VerifyConnection(SQLc) Then
                Cmd.CommandText = "GET_CANTIDAD_TOTAL_PICKING_PRODUCTO"
                Cmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@ViajeID", SqlDbType.VarChar, 100)
                Pa.Value = ViajeID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTOID", SqlDbType.VarChar, 30)
                Pa.Value = Producto_Id
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VALORTOTAL", SqlDbType.Float)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Cmd.ExecuteNonQuery()

                ValorTotal = IIf(IsDBNull(Cmd.Parameters("@VALORTOTAL").Value), 0, Cmd.Parameters("@VALORTOTAL").Value)
                Return ValorTotal
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("GET_CANTIDAD_TOTAL_PICKING_PRODUCTO: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
        Catch ex As Exception
            MsgBox("GET_CANTIDAD_TOTAL_PICKING_PRODUCTO: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            Pa = Nothing
        End Try
    End Function
    Public Function GetTareasPicking(ByVal Usuario As String, ByRef Viaje_ID As String, _
                                     ByRef DsT As Data.DataSet, ByVal Table As String, _
                                     ByRef PalletId As Long, ByRef Ruta As String, _
                                     ByVal PalletCompleto As Boolean, ByVal NaveCalle As String) As Boolean
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim ContLock As Integer = 0
ReRun:
        Cmd.Parameters.Clear()
        Try
            If VerifyConnection(SQLc) Then
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "TAREAS_PICKING_D"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                '@USUARIO 			AS VARCHAR(30),
                Pa = New SqlParameter("@Usuario", SqlDbType.VarChar, 30)
                Pa.Value = Usuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                '@VIAJE_ID 			AS VARCHAR(100),
                Pa = New SqlParameter("@Viaje_ID", SqlDbType.VarChar, 100)
                Pa.Value = Viaje_ID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                '@PALLET_I			AS VARCHAR(30),
                Pa = New SqlParameter("@Pallet_I", SqlDbType.Int)
                Pa.Value = PalletId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                '--agregado para la ruta o documento.
                '@RUTA_I				AS VARCHAR(50),
                Pa = New SqlParameter("@Ruta_I", SqlDbType.VarChar, 100)
                Pa.Value = IIf(Ruta = "", System.DBNull.Value, Ruta)
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                '@CLIENTE			AS VARCHAR(30)=NULL,
                Pa = New SqlParameter("@CLIENTE", SqlDbType.VarChar, 20)
                Pa.Value = IIf(Trim(vUsr.ClienteActivo) = "", DBNull.Value, Trim(UCase(vUsr.ClienteActivo)))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                '@VH					AS VARCHAR(40)=NULL,
                If Trim(vUsr.Vehiculo) <> "" Then
                    Pa = New SqlParameter("@VH", SqlDbType.VarChar, 30)
                    Pa.Value = Trim(UCase(vUsr.Vehiculo))
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                End If
                '@PALLETCOMPLETO		AS NUMERIC(10),
                Pa = New SqlParameter("@PalletCompleto", SqlDbType.VarChar, 50)
                Pa.Value = IIf(PalletCompleto = True, 1, 0)
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                '@NAVECALLE			AS VARCHAR(50)=NULL
                Pa = New SqlParameter("@NAVECALLE", SqlDbType.VarChar, 50)
                Pa.Value = IIf(Trim(NaveCalle) = "", DBNull.Value, Trim(UCase(NaveCalle)))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Da.Fill(DsT, Table)
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            If SQLEx.Number <> 1205 Then
                Select Case SQLEx.Message
                    Case "1"
                        MsgBox("El viaje " & Viaje_ID & " no tiene mas tareas de Picking." & _
                                vbNewLine & "El pallet sera cerrado.", MsgBoxStyle.OkOnly, ClsName)
                        Me.CerrarPallet = True
                        Viaje_ID = "0"
                    Case "2"
                        If Not vH Then
                            MsgBox("La ruta " & Ruta & " ha finalizado." & _
                                    vbNewLine & "El pallet sera cerrado.", MsgBoxStyle.OkOnly, ClsName)
                            Me.CerrarPallet = True
                            Ruta = ""
                        Else
                            MsgBox("No quedan mas tareas pendientes para la Nave-Calle seleccionada." & _
                                    vbNewLine & "El pallet sera cerrado.", MsgBoxStyle.OkOnly, ClsName)
                            Me.CerrarPallet = True
                            Ruta = ""
                        End If
                    Case Else
                        MsgBox("GetTareasPicking SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
                End Select
                Return False
            End If
        Catch ex As Exception
            'Tran.Rollback()
            MsgBox("GetTareasPicking: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            Pa = Nothing
            Da = Nothing
        End Try
    End Function

    Public Function GetPendientes(ByVal Ruta As String, ByRef Cantidad As Integer) As Boolean
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "Mob_Pendientes"
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@Ruta", Data.SqlDbType.VarChar, 100)
                Pa.Value = Ruta
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Cant", Data.SqlDbType.Int)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

                Cantidad = IIf(IsDBNull(Cmd.Parameters("@Cant")), 0, Cmd.Parameters("@Cant"))
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("GetPendientes: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("GetPendientes: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            Pa = Nothing
        End Try
    End Function

    Public Function CompletadosUsuario(ByVal Usuario As String, ByVal Ruta As String, ByVal DsRef As Data.DataSet, _
                                       ByVal Table As String) As Boolean

        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "COMPLETADOS_USUARIO"
                Cmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@Usuario", Data.SqlDbType.VarChar, 20)
                Pa.Value = Usuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Ruta", Data.SqlDbType.VarChar, 100)
                Pa.Value = Ruta
                Cmd.Parameters.Add(Pa)

                Da.Fill(DsRef, Table)
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("GetPendientes: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("GetPendientes: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            Da = Nothing
            Pa = Nothing
        End Try
    End Function

    Public Function GetNumberofPallet(ByRef Pallet_id As Long) As Boolean
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Get_Value_For_Sequence"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()
                Pa = New SqlParameter("@SECUENCIA", Data.SqlDbType.VarChar, 50)
                Pa.Value = "PALLET_PICKING"
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VALUE", Data.SqlDbType.Int)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
                Pallet_id = IIf(IsDBNull(Cmd.Parameters("@VALUE").Value), 0, Cmd.Parameters("@VALUE").Value)
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("GetPendientes: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("GetPendientes: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            Pa = Nothing
        End Try
    End Function

    Public Function Fin_Picking(ByVal Usuario As String, ByVal ViajeId As String, ByVal ProductoID As String, _
                              ByVal PosicionCod As String, ByVal CantConf As Double, ByVal PalletPicking As Long, _
                              ByVal Pallet As String, ByVal Ruta As String, ByVal Lote As String, ByVal pSerie As String, Optional ByRef trans As SqlTransaction = Nothing) As Boolean
        'Fin_Picking()
        '@USUARIO 			AS VARCHAR(30),--
        '@VIAJEID 			AS VARCHAR(30),--
        '@PRODUCTO_ID	AS VARCHAR(50),--
        '@POSICION_COD	AS VARCHAR(45), --
        '@CANT_CONF		AS NUMERIC(20)--
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd.Parameters.Clear()
                Cmd.CommandText = "Fin_Picking"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 30)
                Pa.Value = Usuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VIAJEID", SqlDbType.VarChar, 100)
                Pa.Value = ViajeId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar)
                Pa.Value = ProductoID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_COD", SqlDbType.VarChar, 45)
                Pa.Value = PosicionCod
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CANT_CONF", SqlDbType.Float)
                Pa.Value = CantConf
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Pallet_Picking", SqlDbType.BigInt)
                Pa.Value = PalletPicking
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Pallet", SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@ruta", SqlDbType.VarChar, 100)
                Pa.Value = Ruta
                Cmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@LOTE", SqlDbType.VarChar, 50)
                Pa.Value = IIf(Trim(Lote) = "", DBNull.Value, Trim(Lote))
                Cmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@LOTE_PROVEEDOR", SqlDbType.VarChar, 100)
                Pa.Value = ""
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 100)
                Pa.Value = ""
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 50)
                Pa.Value = pSerie
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                If Not IsNothing(trans) Then
                    Cmd.Transaction = trans
                End If
                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            Dim xErr As String = Mid(SQLEx.Message, 1, 1)
            If xErr = "3" Then
                MsgBox(Mid(SQLEx.Message, 3, SQLEx.Message.Length), MsgBoxStyle.OkOnly, ClsName)
                vCierrePallet = True
            Else
                MsgBox(SQLEx.Message & " Fin_Picking", MsgBoxStyle.OkOnly, ClsName)
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message & " Fin_Picking", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Pa = Nothing
        End Try
    End Function

    Public Function Fin_Picking_Contenedoras(ByVal Usuario As String, ByVal ViajeId As String, ByVal ProductoID As String, _
                                ByVal PosicionCod As String, ByVal CantConf As Double, ByVal PalletPicking As Long, _
                                ByVal Pallet As String, ByVal Ruta As String, ByVal Lote As String, ByVal pSerie As String, Optional ByRef trans As SqlTransaction = Nothing) As Boolean
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd.Parameters.Clear()
                Cmd.CommandText = "Fin_Picking_Contenedora"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 30)
                Pa.Value = Usuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VIAJEID", SqlDbType.VarChar, 100)
                Pa.Value = ViajeId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar)
                Pa.Value = ProductoID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_COD", SqlDbType.VarChar, 45)
                Pa.Value = PosicionCod
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CANT_CONF", SqlDbType.Float)
                Pa.Value = CantConf
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Pallet_Picking", SqlDbType.BigInt)
                Pa.Value = PalletPicking
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Pallet", SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@ruta", SqlDbType.VarChar, 100)
                Pa.Value = Ruta
                Cmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@LOTE", SqlDbType.VarChar, 50)
                Pa.Value = IIf(Trim(Lote) = "", DBNull.Value, Trim(Lote))
                Cmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@LOTE_PROVEEDOR", SqlDbType.VarChar, 100)
                Pa.Value = ""
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 100)
                Pa.Value = ""
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 50)
                Pa.Value = pSerie
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                If Not IsNothing(trans) Then
                    Cmd.Transaction = trans
                End If
                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            Dim xErr As String = Mid(SQLEx.Message, 1, 1)
            If xErr = "3" Then
                MsgBox(Mid(SQLEx.Message, 3, SQLEx.Message.Length), MsgBoxStyle.OkOnly, ClsName)
                vCierrePallet = True
            Else
                MsgBox(SQLEx.Message & " Fin_Picking", MsgBoxStyle.OkOnly, ClsName)
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message & " Fin_Picking", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Pa = Nothing
        End Try
    End Function


    Public Function Picking_Pendiente(ByVal Usuario As String, ByVal TipoPick As Boolean, ByRef Dst As DataSet, ByVal Table As String) As Boolean
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                Da = New SqlDataAdapter(Cmd)
                Cmd.Parameters.Clear()
                Cmd.CommandText = "Picking_Pendiente"
                Cmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@usuario", SqlDbType.VarChar, 30)
                Pa.Value = Usuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@TipoPicking", SqlDbType.Int)
                Pa.Value = IIf(TipoPick = True, 1, 0)
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Cliente", SqlDbType.VarChar, 20)
                Pa.Value = IIf(Trim(vUsr.ClienteActivo) = "", DBNull.Value, Trim(UCase(vUsr.ClienteActivo)))
                Cmd.Parameters.Add(Pa)
                Da.Fill(Dst, Table)
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " Picking_Pendiente", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Pa = Nothing
            Da = Nothing
        End Try
    End Function

    Public Function Cerrar_Pallet(ByVal ViajeId As String, ByVal Producto_Id As String, ByVal Posicion_Cod As String, _
                                  ByVal Pallet As String, ByVal PalletPicking As Long, ByVal Usuario As String, _
                                  ByVal Ruta As String) As Boolean
        Try
            If VerifyConnection(SQLc) Then
                Cmd.Parameters.Clear()
                Cmd.CommandText = "CERRAR_PALLET"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Parameters.Add("@VIAJEID", SqlDbType.VarChar, 30).Value = ViajeId
                Cmd.Parameters.Add("@PRODUCTO_ID", SqlDbType.VarChar, 50).Value = Producto_Id
                Cmd.Parameters.Add("@POSICION_COD", SqlDbType.VarChar, 45).Value = Posicion_Cod
                Cmd.Parameters.Add("@PALLET", SqlDbType.VarChar, 100).Value = Pallet
                Cmd.Parameters.Add("@PALLET_PICKING", SqlDbType.Int).Value = PalletPicking
                Cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 30).Value = Usuario
                Cmd.Parameters.Add("@Ruta", SqlDbType.VarChar, 100).Value = Ruta
                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message & " Cerrar_Pallet", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message & " Cerrar_Pallet", MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Public Function JumpPicking(ByVal Usr As String, ByVal Viaje As String, ByVal Prod As String, _
                                ByVal Pos As String, ByVal Pallet As String, ByVal Ruta As String) As Boolean

        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd.Parameters.Clear()
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = "SALTO_PICKING"

                Pa = New SqlParameter("@usuario", SqlDbType.VarChar, 30)
                Pa.Value = Usr
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@viajeid", SqlDbType.VarChar, 100)
                Pa.Value = Viaje
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 50)
                Pa.Value = Prod
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@posicion_cod", SqlDbType.VarChar, 45)
                Pa.Value = Pos
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Pallet", SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Ruta", SqlDbType.VarChar, 100)
                Pa.Value = Ruta
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("Salto Picking SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Salto Picking: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function
    Public Function Fin_Picking_Split(ByVal Usuario As String, ByVal ViajeId As String, ByVal ProductoID As String, _
                            ByVal PosicionCod As String, ByVal CantConf As Double, ByVal PalletPicking As Long, _
                            ByVal Pallet As String, ByVal Ruta As String, ByVal Lote As String, ByVal pSerie As String, Optional ByRef trans As SqlTransaction = Nothing) As Boolean

        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd.Parameters.Clear()
                Cmd.CommandText = "Fin_Picking_Split"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 30)
                Pa.Value = Usuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VIAJEID", SqlDbType.VarChar, 30)
                Pa.Value = ViajeId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar)
                Pa.Value = ProductoID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_COD", SqlDbType.VarChar, 45)
                Pa.Value = PosicionCod
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CANT_CONF", SqlDbType.Float)
                Pa.Value = CantConf
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Pallet_Picking", SqlDbType.BigInt)
                Pa.Value = PalletPicking
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Pallet", SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Ruta", SqlDbType.VarChar, 100)
                Pa.Value = Ruta
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Lote", SqlDbType.VarChar, 50)
                Pa.Value = IIf(Trim(Lote) = "", DBNull.Value, Lote)
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@LOTE_PROVEEDOR", SqlDbType.VarChar, 100)
                Pa.Value = ""
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 100)
                Pa.Value = ""
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 50)
                Pa.Value = pSerie
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                If Not IsNothing(trans) Then
                    Cmd.Transaction = trans
                End If

                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            Dim xErr As String = Mid(SQLEx.Message, 1, 1)
            If xErr = "3" Then
                MsgBox(Mid(SQLEx.Message, 3, SQLEx.Message.Length), MsgBoxStyle.OkOnly, ClsName)
                vCierrePallet = True
            Else
                MsgBox(SQLEx.Message & " Fin_Picking", MsgBoxStyle.OkOnly, ClsName)
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message & " Fin_Picking", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Pa = Nothing
        End Try
    End Function

    Public Function CambioUbicacionContenedora(ByVal NewRL_Id As Double, ByVal Picking_Id As Double, ByVal Cantidad As Double) As Boolean
        Dim Pa As SqlParameter
        Dim xCmd As SqlCommand
        Dim trans As SqlClient.SqlTransaction
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.Parameters.Clear()
                xCmd.CommandText = "Estacion_Picking_ActNroLinea_Cont"
                xCmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@NewRl_Id", SqlDbType.Float, 20)
                Pa.Value = NewRL_Id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Picking_Id", SqlDbType.Float, 20)
                Pa.Value = Picking_Id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Cantidad", SqlDbType.Float)
                Pa.Value = Cantidad
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                If Not IsNothing(trans) Then
                    xCmd.Transaction = trans
                End If

                xCmd.ExecuteNonQuery()
                Return True

            Else : MsgBox(SQLConErr, MsgBoxStyle.Critical, ClsName)
                Return False
            End If
            Return True
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, ClsName)
            Return False
        Finally
            Pa = Nothing
            xCmd = Nothing
        End Try
    End Function
   

    '    Public Function GetTareasPickingVH(ByVal Usuario As String, ByRef Viaje_ID As String, _
    '                                       ByRef DsT As Data.DataSet, ByVal Table As String, _
    '                                       ByRef PalletId As Long, ByRef Ruta As String, _
    '                                       ByVal Vehiculo As String, ByVal NaveCalle As String, _
    '                                       ByVal PalletCompleto As Boolean) As Boolean
    '        Dim Pa As SqlParameter
    '        Dim Da As SqlDataAdapter
    '        Dim ContLock As Integer = 0

    'ReRun:
    '        Cmd.Parameters.Clear()
    '        Try
    '            If VerifyConnection(SQLc) Then
    '                Da = New SqlDataAdapter(Cmd)
    '                Cmd.CommandText = "TAREAS_PICKINGNEW"
    '                Cmd.CommandType = Data.CommandType.StoredProcedure
    '                Pa = New SqlParameter("@Usuario", SqlDbType.VarChar, 30)
    '                Pa.Value = Usuario
    '                Cmd.Parameters.Add(Pa)
    '                Pa = Nothing
    '                Pa = New SqlParameter("@Viaje_ID", SqlDbType.VarChar, 100)
    '                Pa.Value = Viaje_ID
    '                Cmd.Parameters.Add(Pa)
    '                Pa = Nothing
    '                Pa = New SqlParameter("@Pallet_I", SqlDbType.Int)
    '                Pa.Value = PalletId
    '                Cmd.Parameters.Add(Pa)
    '                Pa = Nothing
    '                '--agregado para la ruta o documento.
    '                Pa = New SqlParameter("@Ruta_I", SqlDbType.VarChar, 50)
    '                Pa.Value = IIf(Ruta = "", System.DBNull.Value, Ruta)
    '                Cmd.Parameters.Add(Pa)
    '                Pa = Nothing
    '                Pa = New SqlParameter("@vehiculo_id", SqlDbType.VarChar, 50)
    '                Pa.Value = Vehiculo
    '                Cmd.Parameters.Add(Pa)
    '                Pa = Nothing
    '                Pa = New SqlParameter("@NaveCalle", SqlDbType.VarChar, 50)
    '                Pa.Value = NaveCalle
    '                Cmd.Parameters.Add(Pa)
    '                Pa = New SqlParameter("@PalletCompleto", SqlDbType.VarChar, 50)
    '                Pa.Value = IIf(PalletCompleto = True, 1, 0)
    '                Cmd.Parameters.Add(Pa)
    '                Pa = Nothing

    '                Pa = New SqlParameter("@CLIENTE", SqlDbType.VarChar, 20)
    '                Pa.Value = IIf(Trim(vUsr.ClienteActivo) = "", DBNull.Value, Trim(UCase(vUsr.ClienteActivo)))
    '                Cmd.Parameters.Add(Pa)
    '                Pa = Nothing
    '                Da.Fill(DsT, Table)
    '                Return True
    '            Else
    '                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
    '            End If
    '        Catch SQLEx As SqlException
    '            If SQLEx.Number <> 1205 Then
    '                Select Case SQLEx.Message
    '                    Case "1"
    '                        MsgBox("El viaje " & Viaje_ID & " no tiene mas tareas de Picking." & _
    '                                vbNewLine & "El pallet sera cerrado.", MsgBoxStyle.OkOnly, ClsName)
    '                        Me.CerrarPallet = True
    '                        Viaje_ID = "0"
    '                    Case "2"
    '                        MsgBox("No quedan tareas de picking pendientes para " & vbNewLine & "la Nave-Calle: " & vUsr.NaveCalle & _
    '                        " y el vehiculo: " & vUsr.Vehiculo, MsgBoxStyle.OkOnly, ClsName)
    '                        Me.CerrarPallet = True
    '                        Viaje_ID = "0"
    '                        Ruta = ""
    '                    Case Else
    '                        MsgBox("GetTareasPicking SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
    '                End Select
    '                Return False
    '            End If
    '        Catch ex As Exception
    '            'Tran.Rollback()
    '            MsgBox("GetTareasPicking: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
    '        Finally
    '            Pa = Nothing
    '            Da = Nothing
    '        End Try
    '    End Function

    Public Function JumpPickingVH(ByVal Usr As String, ByVal Viaje As String, ByVal Prod As String, _
                                ByVal Pos As String, ByVal Pallet As String, ByVal Ruta As String) As Boolean

        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd.Parameters.Clear()
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = "SALTO_PICKINGVH"

                Pa = New SqlParameter("@usuario", SqlDbType.VarChar, 30)
                Pa.Value = Usr
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@viajeid", SqlDbType.VarChar, 100)
                Pa.Value = Viaje
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 50)
                Pa.Value = Prod
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@posicion_cod", SqlDbType.VarChar, 45)
                Pa.Value = Pos
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Pallet", SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Ruta", SqlDbType.VarChar, 100)
                Pa.Value = Ruta
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("Salto Picking SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("Salto Picking: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Public Function Picking_Pendiente_VH(ByVal Usuario As String, ByVal TipoPick As Boolean, _
                                         ByVal NaveCalle As String, ByVal VH As String, _
                                         ByRef Dst As DataSet, ByVal Table As String) As Boolean
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                Da = New SqlDataAdapter(Cmd)
                Cmd.Parameters.Clear()
                Cmd.CommandText = "PICKING_PENDIENTEVH"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@usuario", SqlDbType.VarChar, 30)
                Pa.Value = Usuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@TipoPicking", SqlDbType.Int)
                Pa.Value = IIf(TipoPick = True, 1, 0)
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NAVECALLE", SqlDbType.VarChar, 45)
                Pa.Value = NaveCalle
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VEHICULO_ID", SqlDbType.VarChar, 50)
                Pa.Value = VH
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE", SqlDbType.VarChar, 20)
                Pa.Value = IIf(Trim(vUsr.ClienteActivo) = "", DBNull.Value, Trim(UCase(vUsr.ClienteActivo)))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Da.Fill(Dst, Table)
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message & " Picking_PendienteVH SQL: ", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message & " Picking_PendienteVH", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Pa = Nothing
            Da = Nothing
        End Try

    End Function
    Public Function IsSeriesEgreso(ByVal prod As String, ByVal cliente As String) As Boolean
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                Cmd.Parameters.Clear()
                Da = New SqlDataAdapter(Cmd)
                Cmd.Parameters.Clear()
                Cmd.CommandText = "MOB_ES_SERIES_EGRESO"
                Cmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@producto_id", SqlDbType.VarChar, 50)
                Pa.Value = prod
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@cliente_id", SqlDbType.VarChar, 50)
                Pa.Value = cliente
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Dim Dst As New Data.DataSet
                Da.Fill(Dst)
                If Dst.Tables(0).Rows(0)("serie_egr") = "1" Then
                    Return True
                Else
                    Return False
                End If

            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " IsSeriesEgreso", MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function
    Public Function GetPickingIdsXPickingPorducto(ByVal Usuario As String, ByVal TipoPick As Boolean, _
    ByVal cliente_id As String, ByVal viaje_id As String, ByVal producto_id As String, _
    ByVal ruta As String, ByVal pos_cod As String, ByVal pallet As String, ByRef Dst As DataSet) As Boolean
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand()
                Da = New SqlDataAdapter(xCmd)
                xCmd.Parameters.Clear()
                xCmd.CommandText = "MOB_GET_PICKING_IDS_X_PICKING_DE_PRODUCTO"
                xCmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 30)
                Pa.Value = Usuario
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@TIPOPICKING", SqlDbType.Int)
                Pa.Value = IIf(TipoPick = True, 1, 0)
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE", SqlDbType.VarChar, 30)
                Pa.Value = cliente_id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VIAJEID", SqlDbType.VarChar, 30)
                Pa.Value = viaje_id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = producto_id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_COD", SqlDbType.VarChar, 45)
                Pa.Value = pos_cod
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PALLET", SqlDbType.VarChar, 100)
                Pa.Value = pallet
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@RUTA", SqlDbType.VarChar, 100)
                Pa.Value = ruta
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Dst = New Data.DataSet
                Da.Fill(Dst)
                If Dst.Tables(0).Rows.Count = 0 Then
                    Err.Raise(513, "GetPickingIdsXPickingPorducto", "No se pudo recuperar los picking_id ZZZZ")
                End If
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message & " GetPickingIdsXPickingPorducto SQL: ", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message & " GetPickingIdsXPickingPorducto", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try

    End Function
    Public Function GrabarSeries(ByVal picking_id As Long, ByVal series As String, ByRef trans As SqlTransaction) As Boolean
        Try
            Dim xCmd As SqlCommand
            Dim Pa As SqlParameter
            xCmd = SQLc.CreateCommand()
            If VerifyConnection(SQLc) Then
                xCmd.Parameters.Clear()
                xCmd.CommandText = "MOB_INSERT_SERIE_PICKING"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Picking_id", SqlDbType.VarChar, 50)
                Pa.Value = picking_id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Nro_Serie", SqlDbType.VarChar, 50)
                Pa.Value = series
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                xCmd.Transaction = trans
                xCmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " GrabarSeries", MsgBoxStyle.OkOnly, ClsName)
            Return False
        End Try
    End Function

    Public Function CambioContenedoraPicking(ByVal NewRl As Long, ByVal Viaje As String, ByVal Producto As String, ByVal CantConf As Double, ByVal Usr As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            xCmd = SQLc.CreateCommand()
            If VerifyConnection(SQLc) Then
                xCmd.Parameters.Clear()
                xCmd.CommandText = "DBO.MOB_CAMBIO_CONTENEDORA"
                xCmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@NEWRL_ID", SqlDbType.BigInt, 20)
                Pa.Value = NewRl
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                Pa.Value = Viaje
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = Producto
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CANTIDAD", SqlDbType.Float, 20.5)
                Pa.Value = CantConf
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USR", SqlDbType.VarChar, 20)
                Pa.Value = vUsr.CodUsuario
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()

                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEX As SqlException
            MsgBox(SQLEX.Message & " CambioContenedoraPicking", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message & " CambioContenedoraPicking", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            xCmd.Dispose()
        End Try
    End Function

    Public Function CarroenUso(ByVal NroCarro As String) As Boolean
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "CarroenUso"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                'parametro de entrada
                Cmd.Parameters.Clear()
                Cmd.Parameters.Add("@carro", SqlDbType.VarChar, 20).Value = NroCarro
                Cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 20).Value = vUsr.CodUsuario
                'parametro de salida
                Cmd.Parameters.Add("@value", SqlDbType.Int, 1).Direction = ParameterDirection.Output

                Cmd.ExecuteNonQuery()
                CarroenUso = Cmd.Parameters("@VALUE").Value
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("CarroenUso: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("CarroenUso: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            Cmd = Nothing
        End Try
    End Function

    Public Function esCarroenViaje(ByVal nrocarro As String, ByVal Viajeid As String) As Boolean
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "carroenviaje"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                'parametro de entrada
                Cmd.Parameters.Clear()
                Cmd.Parameters.Add("@viaje_id", SqlDbType.VarChar, 100).Value = Viajeid
                Cmd.Parameters.Add("@nrocarro", SqlDbType.VarChar, 20).Value = nrocarro

                'parametro de salida
                Cmd.Parameters.Add("@value", SqlDbType.Int, 1).Direction = ParameterDirection.Output

                Cmd.ExecuteNonQuery()
                esCarroenViaje = Cmd.Parameters("@VALUE").Value
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("esCarroenViaje: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("esCarroenViaje: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            Cmd = Nothing
        End Try

    End Function

    Public Function PickingUsaCarro() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "PickingUsaCarro"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                'parametro de entrada
                Pa = New SqlParameter("@CLIENTE", Data.SqlDbType.VarChar, 15)
                Pa.Value = vUsr.ClienteActivo
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                'parametro de salida
                Pa = New SqlParameter("@VALUE", Data.SqlDbType.Int)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
                PickingUsaCarro = IIf(IsDBNull(Cmd.Parameters("@VALUE").Value), 0, Cmd.Parameters("@VALUE").Value)
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If


        Catch SQLEx As SqlException
            MsgBox("PickingUsaCarro: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("PickingUsaCarro: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try

    End Function

    Public Function ConfirmacionPorPallet(ByVal Cliente As String, ByVal Producto As String, ByVal Pallet As String) As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "PickingUsaCarro"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                'parametro de entrada
                Pa = New SqlParameter("@CLIENTE", Data.SqlDbType.VarChar, 15)
                Pa.Value = vUsr.ClienteActivo
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                'parametro de salida
                Pa = New SqlParameter("@VALUE", Data.SqlDbType.Int)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If


        Catch SQLEx As SqlException
            MsgBox("PickingUsaCarro: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("PickingUsaCarro: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function

    Public Function SerializadoBestFit(ByVal PosicionCod As String, ByVal Pallet As String) As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "[dbo].[FX_SERIALIZADO_BEST_FIT]"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@POSICION_COD", Data.SqlDbType.VarChar, 45)
                Pa.Value = PosicionCod
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PALLET", Data.SqlDbType.VarChar, 50)
                Pa.Value = Pallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@RETURN_VALUE", SqlDbType.Int)
                Pa.Direction = ParameterDirection.ReturnValue
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
                If (Cmd.Parameters("@RETURN_VALUE").Value = 1) Then
                    Return True
                Else
                    Return False
                End If
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If


        Catch SQLEx As SqlException
            MsgBox("SerializadoBestFit SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("SerializadoBestFit: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function

    Public Function ConfirmarSerializacionPallet(ByVal ViajeID As String, ByVal Posicion As String, ByVal Pallet As String, ByVal PalletPick As String, ByVal Usuario As String) As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "DBO.MOB_CONFIRMA_SERIES_PALLET"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@VIAJE_ID", Data.SqlDbType.VarChar, 100)
                Pa.Value = ViajeID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_COD", Data.SqlDbType.VarChar, 50)
                Pa.Value = Posicion
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PALLET", Data.SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PALLET_PICK", Data.SqlDbType.VarChar, 100)
                Pa.Value = PalletPick
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO", Data.SqlDbType.VarChar, 100)
                Pa.Value = Usuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("ConfirmarSerializacionPallet SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, ClsName)
            Return False
        Catch ex As Exception
            MsgBox("ConfirmarSerializacionPallet: " & ex.Message, MsgBoxStyle.OkOnly, ClsName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function

    Public Function MuestraBtnSerieEspecifica(ByVal Contenedora As String) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Dim Ret As String
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.GET_SERIE_ESPECIFICA"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@CONTENEDORA", SqlDbType.VarChar, 100)
                Pa.Value = Contenedora
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OUT", SqlDbType.VarChar, 15)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()

                Ret = xCmd.Parameters("@OUT").Value
                If Ret = "1" Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, ClsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, ClsName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function DescomponerSerieEspecifica(ByVal ViajeID As String, ByVal NroPallet As String, ByRef TSerie As TextBox) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Dim Ret As String
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.MOB_PICKING_DESCOMPONER_SERIE_ESPECIFICA"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                Pa.Value = ViajeID
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_PALLET", SqlDbType.VarChar, 100)
                Pa.Value = NroPallet
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 100)
                Pa.Direction = ParameterDirection.InputOutput
                Pa.Value = Trim(UCase(TSerie.Text))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@ERROR", SqlDbType.VarChar, 15)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()

                Ret = xCmd.Parameters("@ERROR").Value

                Select Case Ret
                    Case "0"
                        TSerie.Text = xCmd.Parameters("@NRO_SERIE").Value
                        Return True
                    Case "1"
                        MsgBox("La serie escaneada no es correcta.", MsgBoxStyle.Information, ClsName)
                        Return False
                    Case "2"
                        MsgBox("La longitud de la serie tomada es incorrecta. ", MsgBoxStyle.Information, ClsName)
                        Return False
                    Case "3"
                        MsgBox("La serie escaneada ya fue ingresada. ", MsgBoxStyle.Information, ClsName)
                        Return False
                    Case "4"
                        MsgBox("La serie escaneada no se corresponde con el Codigo de producto de la contendora.", MsgBoxStyle.Information, ClsName)
                        Return False
                End Select
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, ClsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, ClsName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

End Class
