Imports System.Data
Imports System.Data.SqlClient
Public Class clsProcesoIngreso

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



    Public Function ExistsDocument(ByVal Documento_ID As Long, ByRef Cpte_Prefijo As String, ByRef Cpte_Numero As String, ByRef Cod_Origen As String, ByRef Nombre_Origen As String, ByRef Cliente As String, Optional ByRef MsgErr As String = "") As Boolean
        Dim Cmd As SqlCommand
        Dim Param As SqlParameter
        Try
            If VerifyConnection(Cnx) Then
                'Comienzo con la logica de validaciones.
                Cmd = New SqlCommand("MOB_GUARDADO_VALDOCING", Cnx)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = Cnx

                Param = New SqlParameter("@Documento_ID", SqlDbType.BigInt)
                Param.Value = Documento_ID
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Cpte_Prefijo", SqlDbType.VarChar, 6)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)

                Param = New SqlParameter("@Cpte_Numero", SqlDbType.VarChar, 20)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)

                Param = New SqlParameter("@Cod_Origen", SqlDbType.VarChar, 20)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)

                Param = New SqlParameter("@Nombre_Origen", SqlDbType.VarChar, 50)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)

                Param = New SqlParameter("@Cliente_Id", SqlDbType.VarChar, 15)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)

                Cmd.ExecuteNonQuery()
                Cpte_Prefijo = Cmd.Parameters("@Cpte_Prefijo").Value
                Cpte_Numero = Cmd.Parameters("@Cpte_Numero").Value
                Cod_Origen = Cmd.Parameters("@Cod_Origen").Value
                Nombre_Origen = Cmd.Parameters("@Nombre_Origen").Value
                Cliente = Cmd.Parameters("@Cliente_Id").Value

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

    Public Function ExistsProduct(ByVal Cod_Producto As String, ByRef Descripcion As String, ByRef Unidad As String, ByRef Cat_Logica As String, ByRef Producto_Id As String, ByRef Cliente As String, ByRef EsFraccionable As Char, Optional ByRef MsgErr As String = "") As Boolean
        Dim Cmd As SqlCommand
        Dim Param As SqlParameter
        Try
            If VerifyConnection(Cnx) Then
                'Comienzo con la logica de validaciones.
                Cmd = New SqlCommand("MOB_GUARDADO_VALPRODUCTO", Cnx)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = Cnx

                Param = New SqlParameter("@Cod_Producto", SqlDbType.VarChar, 50)
                Param.Value = Cod_Producto
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Cliente_Id", SqlDbType.VarChar, 15)
                Param.Value = Cliente
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Descripcion", SqlDbType.VarChar, 200)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)

                Param = New SqlParameter("@Unidad", SqlDbType.VarChar, 50)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)

                Param = New SqlParameter("@Cat_Logica", SqlDbType.VarChar, 50)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)

                Param = New SqlParameter("@Producto_Id", SqlDbType.VarChar, 30)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)

                Param = New SqlParameter("@EsFraccionable", SqlDbType.Char, 1)
                Param.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Param)



                Cmd.ExecuteNonQuery()
                Descripcion = Cmd.Parameters("@Descripcion").Value
                Unidad = Cmd.Parameters("@Unidad").Value
                Cat_Logica = Cmd.Parameters("@Cat_Logica").Value
                Producto_Id = Cmd.Parameters("@Producto_Id").Value
                EsFraccionable = Cmd.Parameters("@EsFraccionable").Value


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

    Public Function GuardarDetalle(ByVal Cod_Producto As String, ByVal Documento_Id As Double, ByVal Cantidad As Double, ByVal Cliente As String, Optional ByRef MsgErr As String = "") As Boolean
        Dim Cmd As SqlCommand
        Dim Param As SqlParameter
        Try
            If VerifyConnection(Cnx) Then
                Cmd = New SqlCommand("Mob_Guardado_Ing_Detalle", Cnx)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = Cnx

                Param = New SqlParameter("@Cod_Producto", SqlDbType.VarChar, 30)
                Param.Value = Cod_Producto
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Cliente_Id", SqlDbType.VarChar, 15)
                Param.Value = Cliente
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Documento_Id", SqlDbType.BigInt)
                Param.Value = Documento_Id
                Cmd.Parameters.Add(Param)

                Param = New SqlParameter("@Cantidad", SqlDbType.Float)
                Param.Value = Cantidad
                Cmd.Parameters.Add(Param)


                Cmd.ExecuteNonQuery()

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
    Public Function GetProductosCargados(ByVal Documento_Id As String, Optional ByRef MsgErr As String = "") As DataTable
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Dt As New DataTable
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = "Mob_Consulta_Ing_Detalle"

                Pa = New SqlParameter("@Documento_Id", SqlDbType.BigInt)
                Pa.Value = CLng(Documento_Id)
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Da.Fill(Ds, "ProductosIngresados")
                Dt = Ds.Tables("ProductosIngresados")
            Else : MsgBox(StrConexion, MsgBoxStyle.Critical, strModule)
                Return Dt
            End If
            Return Dt
        Catch sqlex As SqlException
            MsgErr = sqlex.Number.ToString & "-" & sqlex.Message.ToString
            Return Dt
        Catch ex As Exception
            MsgErr = ex.InnerException.ToString & "-" & ex.Message.ToString
            Return Dt
        Finally
            Pa = Nothing
            Ds = Nothing
            xCmd = Nothing
            Da = Nothing
        End Try
    End Function

    Public Function DescontarDetalle(ByVal DescontarTodo As Char, ByVal Cod_Producto As String, ByVal Documento_Id As Double, Optional ByRef MsgErr As String = "") As Boolean
        Dim Cmd As SqlCommand
        Dim Param As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = New SqlCommand("Mob_Descontar_Detalle", SQLc)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc

                Param = New SqlParameter("@DescontarTodo", SqlDbType.Char, 1)
                Param.Value = DescontarTodo
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Producto_Id", SqlDbType.VarChar, 30)
                Param.Value = Cod_Producto
                Cmd.Parameters.Add(Param)
                Param = Nothing

                Param = New SqlParameter("@Documento_Id", SqlDbType.BigInt)
                Param.Value = Documento_Id
                Cmd.Parameters.Add(Param)

                Cmd.ExecuteNonQuery()

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

End Class
