Imports System.Data
Imports System.Data.SqlClient

Public Class clsGuardadoManual

#Region "Declaraciones"
    '----------------------------------------------------
    'Variables.
    '----------------------------------------------------
    Private Cnx As SqlConnection
    Private Const StrConexion As String = "Fallo al conectar con la base de datos."
    Private Const strModule As String = ""
    '----------------------------------------------------
    'Property's
    '----------------------------------------------------
    Public Property Conexion() As SqlConnection
        Get
            Return Cnx
        End Get
        Set(ByVal value As SqlConnection)
            Cnx = value
        End Set
    End Property


#End Region

#Region "Metodos"

    Public Function ExistsDocument(ByVal Documento_ID As Long, ByRef Cliente As String, Optional ByRef MsgErr As String = "") As Boolean
        Dim Cmd As SqlCommand
        Dim Param As SqlParameter
        Try
            If VerifyConnection(Cnx) Then
                'Comienzo con la logica de validaciones.
                Cmd = New SqlCommand("MOB_GUARDADO_VALDOC", Cnx)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = Cnx

                Param = New SqlParameter("@Documento_ID", SqlDbType.BigInt)
                Param.Value = Documento_ID
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Cliente", SqlDbType.VarChar, 30)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)

                Cmd.ExecuteNonQuery()
                Cliente = Cmd.Parameters("@Cliente").Value

            Else : MsgBox(StrConexion, MsgBoxStyle.Critical, strModule)
                Return False
            End If
            Return True
        Catch SqlEx As SqlException
            MsgErr = SqlEx.Number.ToString & "-" & SqlEx.Message.ToString
            Return False
        Catch ex As Exception
            MsgErr = ex.InnerException.ToString & "-" & ex.Message.ToString
            Return False
        Finally
            Param = Nothing
            Cmd = Nothing
        End Try
    End Function

    Public Function ValidarSeries(ByVal Documento_ID As Long, ByVal ProductoID As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Param As SqlParameter, MsgErr As String = "", Ret As String = ""
        Try
            If VerifyConnection(Cnx) Then
                'Comienzo con la logica de validaciones.
                Cmd = New SqlCommand("dbo.VALIDA_TOMA_SERIES", Cnx)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = Cnx

                Param = New SqlParameter("@Documento_ID", SqlDbType.BigInt)
                Param.Value = Documento_ID
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Producto_id", SqlDbType.VarChar)
                Param.Value = ProductoID
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Retorno", SqlDbType.VarChar, 30)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)

                Cmd.ExecuteNonQuery()
                Ret = Cmd.Parameters("@Retorno").Value
                If Ret = "1" Then
                    Return True
                Else
                    Return False
                End If
            Else : MsgBox(StrConexion, MsgBoxStyle.Critical, strModule)
                Return False
            End If
        Catch SqlEx As SqlException
            MsgErr = SqlEx.Number.ToString & "-" & SqlEx.Message.ToString
            MsgBox(MsgErr)
            Return False
        Catch ex As Exception
            MsgErr = ex.InnerException.ToString & "-" & ex.Message.ToString
            MsgBox(MsgErr)
            Return False
        Finally
            Param = Nothing
            Cmd = Nothing
        End Try
    End Function

    Public Function GetDescriptionByProd(ByVal Documento_ID As Long, _
                                         ByVal Cliente As String, _
                                         ByRef Producto As String, _
                                         ByRef Description As String, _
                                         ByRef Linea As Integer, _
                                         ByRef Qty As Double, _
                                         ByRef Fracc As String, _
                                         Optional ByRef vError As String = "") As Boolean
        Dim Cmd As SqlCommand
        Dim Param As SqlParameter
        Try
            If VerifyConnection(Cnx) Then
                Cmd = New SqlCommand("DBO.Mob_Guardado_Prod", Cnx)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = Cnx
                '------------------------------------------------------------
                'Genero y cargo los parametros.
                '------------------------------------------------------------
                Param = New SqlParameter("@Documento_ID", SqlDbType.BigInt)
                Param.Value = Documento_ID
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Cliente", SqlDbType.VarChar, 30)
                Param.Value = Cliente
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Producto", SqlDbType.VarChar, 30)
                Param.Value = Producto
                Param.Direction = ParameterDirection.InputOutput
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@DESCRIPCION", SqlDbType.VarChar, 50)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Linea", SqlDbType.SmallInt)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Qty", SqlDbType.Float)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Fracc", SqlDbType.Char, 1)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Cmd.ExecuteNonQuery()
                '------------------------------------------------------------
                'Saco los valores que necesito del Sp.
                '------------------------------------------------------------
                Producto = Cmd.Parameters("@Producto").Value
                Description = Cmd.Parameters("@Descripcion").Value
                Linea = Cmd.Parameters("@Linea").Value
                Qty = Cmd.Parameters("@Qty").Value
                Fracc = Cmd.Parameters("@Fracc").Value
                '------------------------------------------------------------
            Else : MsgBox(StrConexion, MsgBoxStyle.Critical, strModule)
                Return False
            End If
            Return True
        Catch SqlEx As SqlException
            vError = SqlEx.Number.ToString & "-" & SqlEx.Message.ToString
            Return False
        Catch ex As Exception
            vError = ex.InnerException.ToString & "-" & ex.Message.ToString
            Return False
        Finally
            Param = Nothing
            Cmd = Nothing
        End Try
    End Function
    Public Function GetDescriptionByProdCont(ByVal Documento_ID As Long, _
                                         ByVal Cliente As String, _
                                         ByRef Producto As String, _
                                         ByRef Description As String, _
                                         ByRef QtyLineas As Double, _
                                         ByRef Fracc As String, _
                                         Optional ByRef vError As String = "") As Boolean
        Dim Cmd As SqlCommand
        Dim Param As SqlParameter
        Try
            If VerifyConnection(Cnx) Then
                Cmd = New SqlCommand("DBO.Mob_Guardado_Prod_Cont", Cnx)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = Cnx
                '------------------------------------------------------------
                'Genero y cargo los parametros.
                '------------------------------------------------------------
                Param = New SqlParameter("@Documento_ID", SqlDbType.BigInt)
                Param.Value = Documento_ID
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Cliente", SqlDbType.VarChar, 30)
                Param.Value = Cliente
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Producto", SqlDbType.VarChar, 30)
                Param.Value = Producto
                Param.Direction = ParameterDirection.InputOutput
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@DESCRIPCION", SqlDbType.VarChar, 50)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@COUNT_LINEAS", SqlDbType.SmallInt)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Fracc", SqlDbType.Char, 1)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Cmd.ExecuteNonQuery()
                '------------------------------------------------------------
                'Saco los valores que necesito del Sp.
                '------------------------------------------------------------
                Producto = Cmd.Parameters("@Producto").Value
                Description = Cmd.Parameters("@Descripcion").Value
                QtyLineas = Cmd.Parameters("@COUNT_LINEAS").Value
                Fracc = Cmd.Parameters("@Fracc").Value
                '------------------------------------------------------------
            Else : MsgBox(StrConexion, MsgBoxStyle.Critical, strModule)
                Return False
            End If
            Return True
        Catch SqlEx As SqlException
            vError = SqlEx.Number.ToString & "-" & SqlEx.Message.ToString
            Return False
        Catch ex As Exception
            vError = ex.InnerException.ToString & "-" & ex.Message.ToString
            Return False
        Finally
            Param = Nothing
            Cmd = Nothing
        End Try
    End Function

    Public Function ExisteNavePosicion(ByVal xUbicacion As String, ByRef vError As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim xValue As String
        Try
            If VerifyConnection(Cnx) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "ExisteNavePosicion"
                Cmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Ubicacion", SqlDbType.VarChar, 45)
                Pa.Value = xUbicacion
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Retorno", SqlDbType.Char, 1, ParameterDirection.Output)
                Pa.Value = DBNull.Value
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
                xValue = IIf(IsDBNull(Cmd.Parameters("@Retorno").Value), "", Cmd.Parameters("@Retorno").Value)
            Else : MsgBox(StrConexion, MsgBoxStyle.Critical, strModule)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            vError = SQLEx.Message
            Return False
        Catch ex As Exception
            vError = ex.Message
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Public Function Locator_Ing(ByVal Documento_id As Long, _
                                 ByVal Nro_linea As Long, _
                                 ByRef NaveCod As String, _
                                 ByRef NaveId As Long, _
                                 ByRef PosCod As String, _
                                 ByRef PosID As Long, _
                                 ByRef Qty_Pos As Double, _
                                 Optional ByRef vError As String = "") As Boolean

        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(Cnx) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "Mob_Guardado_Loc"
                Cmd.Connection = Cnx
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Documento_id", SqlDbType.Int)
                Pa.Value = Documento_id
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Nro_linea", SqlDbType.Int)
                Pa.Value = Nro_linea
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POS_ID", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POS_COD", SqlDbType.VarChar, 45)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NAV_COD", SqlDbType.VarChar, 45)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NAV_ID", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@QTY_UBICACION", SqlDbType.Float)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

                PosID = IIf(IsDBNull(Cmd.Parameters("@POS_ID").Value), 0, Cmd.Parameters("@POS_ID").Value)
                PosCod = IIf(IsDBNull(Cmd.Parameters("@POS_COD").Value), "", Cmd.Parameters("@POS_COD").Value)
                NaveCod = IIf(IsDBNull(Cmd.Parameters("@NAV_COD").Value), "", Cmd.Parameters("@NAV_COD").Value)
                NaveId = IIf(IsDBNull(Cmd.Parameters("@NAV_ID").Value), 0, Cmd.Parameters("@NAV_ID").Value)
                Qty_Pos = IIf(IsDBNull(Cmd.Parameters("@QTY_UBICACION").Value), 0, Cmd.Parameters("@QTY_UBICACION").Value)
            Else : MsgBox(StrConexion, MsgBoxStyle.OkOnly, strModule)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            vError = SQLEx.Message
            Return False
        Catch ex As Exception
            vError = ex.Message
            Return False
        Finally
            Cmd = Nothing
            Da = Nothing
            Pa = Nothing
        End Try
    End Function

    Public Function GuardarItem(ByVal Documento_ID As Long, _
                                ByVal Nro_Linea As Integer, _
                                ByVal Cantidad As Double, _
                                ByVal Posicion_Cod As String, _
                                ByVal Producto_Id As String, _
                                ByRef vError As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(Cnx) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "Mob_Guardado_Items"
                Cmd.Connection = Cnx
                Cmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Documento_id", SqlDbType.Int)
                Pa.Value = Documento_ID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Nro_linea", SqlDbType.Int)
                Pa.Value = Nro_Linea
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Cantidad", SqlDbType.Decimal)
                Pa.Value = Cantidad
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Posicion_Cod", SqlDbType.VarChar, 45)
                Pa.Value = Posicion_Cod
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 30)
                Pa.Value = Producto_Id
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
            Else : MsgBox(StrConexion, MsgBoxStyle.OkOnly, strModule)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            vError = SQLEx.Message
            Return False
        Catch ex As Exception
            vError = ex.Message
            Return False
        Finally
            Cmd = Nothing
            Da = Nothing
            Pa = Nothing
        End Try
    End Function
    Public Function validarSiContenedoraUbicada(ByVal Documento_ID As Long, _
                               ByVal Nro_Contenedora As String, _
                               ByVal Producto_Id As String, _
                               ByRef vError As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(Cnx) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "Mob_Validacion_Cont_Ubic"
                Cmd.Connection = Cnx
                Cmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Documento_id", SqlDbType.Int)
                Pa.Value = Documento_ID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Nro_Contenedora", SqlDbType.VarChar, 50)
                Pa.Value = Nro_Contenedora
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 30)
                Pa.Value = Producto_Id
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
            Else : MsgBox(StrConexion, MsgBoxStyle.OkOnly, strModule)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            vError = SQLEx.Message
            Return False
        Catch ex As Exception
            vError = ex.Message
            Return False
        Finally
            Cmd = Nothing
            Da = Nothing
            Pa = Nothing
        End Try
    End Function
    Public Function GuardarItemContenedoras(ByVal Documento_ID As Long, _
                               ByVal Cantidad As Double, _
                               ByVal Posicion_Cod As String, _
                               ByVal Producto_Id As String, _
                               ByRef vError As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(Cnx) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "Mob_Guardado_Items_Cont"
                Cmd.Connection = Cnx
                Cmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Documento_id", SqlDbType.Int)
                Pa.Value = Documento_ID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Cantidad", SqlDbType.Float)
                Pa.Value = Cantidad
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Posicion_Cod", SqlDbType.VarChar, 45)
                Pa.Value = Posicion_Cod
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 30)
                Pa.Value = Producto_Id
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
            Else : MsgBox(StrConexion, MsgBoxStyle.OkOnly, strModule)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            vError = SQLEx.Message
            Return False
        Catch ex As Exception
            vError = ex.Message
            Return False
        Finally
            Cmd = Nothing
            Da = Nothing
            Pa = Nothing
        End Try
    End Function

    Public Function CloseDocumento(ByVal Documento_ID As Long, ByVal Linea As Integer, ByRef vError As String, ByRef Cierre As Boolean) As Boolean
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Dim Qty As Integer = 0
        Dim Ca As New clsAceptar
        Dim Trans As SqlTransaction
        Try
            If VerifyConnection(Cnx) Then
                Trans = SQLc.BeginTransaction
                Cmd = SQLc.CreateCommand

                Cmd.Transaction = Trans
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "Mob_Guardado_All"
                Cmd.Connection = Cnx
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Documento_id", SqlDbType.BigInt)
                Pa.Value = Documento_ID
                Cmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@Qty", SqlDbType.Int)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
                Qty = Cmd.Parameters("@Qty").Value
                If Qty = 0 Then
                    Cmd.Parameters.Clear()
                    Ca.DocumentoID = Documento_ID
                    Ca.NroLinea = Linea
                    Ca.Cmd = Cmd
                    Ca.objConnection = Cnx
                    Ca.OperacionID = "ING"
                    Ca.UsuarioID = vUsr.CodUsuario
                    If Not Ca.Aceptar() Then
                        Throw New Exception("Error en modulo Aceptar.")
                    End If
                    Cierre = True
                Else
                    Cierre = False
                End If
            Else : MsgBox(StrConexion, MsgBoxStyle.OkOnly, strModule)
                Return False
            End If
            Trans.Commit()
            Return True
        Catch SQLEx As SqlException
            Trans.Rollback()
            vError = SQLEx.Message
            Return False
        Catch ex As Exception
            Trans.Rollback()
            vError = ex.Message
            Return False
        Finally
            Cmd = Nothing
            Da = Nothing
            Pa = Nothing
            Ca = Nothing
            Trans = Nothing
        End Try
    End Function
    Public Function GetFlgContenedora(ByVal Cliente_id As String, ByVal Producto_id As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Return_Contenedora As Boolean = False
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

                Pa = New SqlParameter("@FLG_CONTENEDORA", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()
                If Not xCmd.Parameters("@FLG_CONTENEDORA").Value Is DBNull.Value Then

                    Return_Contenedora = CBool(xCmd.Parameters("@FLG_CONTENEDORA").Value.ToString)
                Else
                    Return_Contenedora = False
                End If

                Return Return_Contenedora
            Else : MsgBox("No se pudo conectar con la base de datos.", MsgBoxStyle.Critical)
                Return False
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Public Function Locator_Ing_Cont(ByVal Documento_id As Long, _
                                 ByRef NaveCod As String, _
                                 ByRef NaveId As Long, _
                                 ByRef PosCod As String, _
                                 ByRef PosID As Long, _
                                 ByRef ProductoId As String, _
                                 ByRef Contenedora As String, _
                                 Optional ByRef vError As String = "") As Boolean

        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(Cnx) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "Mob_Guardado_Loc_Cont"
                Cmd.Connection = Cnx
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Documento_id", SqlDbType.Int)
                Pa.Value = Documento_id
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POS_ID", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POS_COD", SqlDbType.VarChar, 45)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NAV_COD", SqlDbType.VarChar, 45)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NAV_ID", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar)
                Pa.Value = ProductoId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CONTENEDORA", SqlDbType.VarChar)
                Pa.Value = Contenedora
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

                PosID = IIf(IsDBNull(Cmd.Parameters("@POS_ID").Value), 0, Cmd.Parameters("@POS_ID").Value)
                PosCod = IIf(IsDBNull(Cmd.Parameters("@POS_COD").Value), "", Cmd.Parameters("@POS_COD").Value)
                NaveCod = IIf(IsDBNull(Cmd.Parameters("@NAV_COD").Value), "", Cmd.Parameters("@NAV_COD").Value)
                NaveId = IIf(IsDBNull(Cmd.Parameters("@NAV_ID").Value), 0, Cmd.Parameters("@NAV_ID").Value)
            Else : MsgBox(StrConexion, MsgBoxStyle.OkOnly, strModule)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            vError = SQLEx.Message
            Return False
        Catch ex As Exception
            vError = ex.Message
            Return False
        Finally
            Cmd = Nothing
            Da = Nothing
            Pa = Nothing
        End Try
    End Function

    Public Function Delete_sys_lock_pallet(ByVal Documento_ID As Long) As Boolean
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Dim vError As String
        Try
            If VerifyConnection(Cnx) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "delete from sys_lock_pallet where documento_id=" & Documento_ID
                Cmd.Connection = Cnx
                Cmd.CommandType = CommandType.Text

                Cmd.ExecuteNonQuery()
            Else : MsgBox(StrConexion, MsgBoxStyle.OkOnly, strModule)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            vError = SQLEx.Message
            Return False
        Catch ex As Exception
            vError = ex.Message
            Return False
        Finally
            Cmd = Nothing
            Da = Nothing
            Pa = Nothing
        End Try
    End Function

#End Region

End Class
