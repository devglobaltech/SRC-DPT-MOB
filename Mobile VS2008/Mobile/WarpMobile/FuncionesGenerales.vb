Imports System.Data.SqlClient
Imports System.Data

Public Class FuncionesGenerales

    Private oCMD As SqlCommand
    Private Const SQLConErr As String = "Fallo al intentar conectar con la base de datos."
    Private Const clsName As String = "Funciones generales."

    Public Property Cmd() As SqlCommand
        Get
            Return oCMD
        End Get
        Set(ByVal value As SqlCommand)
            oCMD = value
        End Set
    End Property

    Public Function VerificaCantidad(ByVal NroPallet As String, ByRef Verifica As Integer) As Boolean
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim Ds As New Data.DataSet
        Try
            Da = New SqlDataAdapter(Cmd)
            Cmd.CommandType = Data.CommandType.StoredProcedure
            Cmd.CommandText = "VerificaCantidad"

            Pa = New SqlParameter("@NroPallet", Data.SqlDbType.VarChar, 100)
            Pa.Value = Trim(UCase(NroPallet))
            Cmd.Parameters.Add(Pa)
            Pa = Nothing

            'Pa = New SqlParameter("@Verifica", Data.SqlDbType.Char, 1)
            'Pa.Direction = Data.ParameterDirection.Output
            'Cmd.Parameters.Add(Pa)
            'Cmd.ExecuteNonQuery()

            Da.Fill(Ds, "1")

            Verifica = CInt(IIf(IsDBNull(Ds.Tables("1").Rows(0)(0)), 0, Ds.Tables("1").Rows(0)(0)))
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, clsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, clsName)
            Return False
        Finally
            Pa = Nothing
        End Try
    End Function

    Public Function GetClientesAsignados(ByVal UsuarioId As String, Optional ByRef Cant As Integer = 0) As Boolean
        Dim Pa As SqlParameter
        Dim Cliente As Integer

        Cmd.Parameters.Clear()
        Try
            If VerifyConnection(SQLc) Then
                Cmd.CommandText = "GET_CLIENTES_DE_USUARIO"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@USUARIO_ID", SqlDbType.VarChar, 20)
                Pa.Value = Trim(UCase(UsuarioId))
                Cmd.Parameters.Add(Pa)

                Pa = Nothing
                Pa = New SqlParameter("@CLIENTES", SqlDbType.Int, 15)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Cmd.ExecuteNonQuery()
                Cliente = IIf(IsDBNull(Cmd.Parameters("@CLIENTES").Value), 0, Cmd.Parameters("@CLIENTES").Value)
                Cant = Cliente
                Return (Cliente > 0)
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("GetFlgGenNewPicking: " & SQLEx.Message, MsgBoxStyle.OkOnly, clsName)
        Catch ex As Exception
            'Tran.Rollback()
            MsgBox("GetFlgGenNewPicking: " & ex.Message, MsgBoxStyle.OkOnly, clsName)
        Finally
            Pa = Nothing
        End Try
    End Function
    Public Function GetCantidadSolicitada(ByVal DocumentoId As Long, ByVal NroLinea As Long, ByRef Cantidad As Object) As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New Data.DataSet
        Dim Da As SqlDataAdapter
        Try
            Cmd.CommandText = "Get_Cantidad_Solicitada"
            Cmd.CommandType = Data.CommandType.StoredProcedure
            Da = New SqlDataAdapter(Cmd)

            Pa = New SqlParameter("@DocumentoID", Data.SqlDbType.Int)
            Pa.Value = DocumentoId
            Cmd.Parameters.Add(Pa)
            Pa = Nothing

            Pa = New SqlParameter("@NroLinea", Data.SqlDbType.Int)
            Pa.Value = NroLinea
            Cmd.Parameters.Add(Pa)
            Pa = Nothing

            Da.Fill(Ds, "Cantidad")

            Cantidad = IIf(IsDBNull(Ds.Tables("Cantidad").Rows(0)(0)), 0, Ds.Tables("Cantidad").Rows(0)(0))
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, clsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, clsName)
            Return False
        Finally
            Ds = Nothing
            Pa = Nothing
        End Try
    End Function
    Public Function GetClienteDelUsuario(ByVal UsuarioId As String, ByRef Cliente As String) As Boolean
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim Ds As New Data.DataSet
        Try
            Cmd.Parameters.Clear()
            Da = New SqlDataAdapter(Cmd)
            Cmd.CommandType = Data.CommandType.StoredProcedure
            Cmd.CommandText = "GET_CLIENTES_BY_USER"

            Pa = New SqlParameter("@USER", Data.SqlDbType.VarChar, 30)
            Pa.Value = Trim(UCase(UsuarioId))
            Cmd.Parameters.Add(Pa)
            Pa = Nothing

            Da.Fill(Ds, "1")

            Cliente = IIf(IsDBNull(Ds.Tables("1").Rows(0)(0)), "", Ds.Tables("1").Rows(0)(0).ToString)
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, clsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, clsName)
            Return False
        Finally
            Pa = Nothing
        End Try
    End Function
End Class
