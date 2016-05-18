Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class clsStockInicial

#Region "Declaracion Variables"
    Private ClienteID As String = ""
    Private ClienteRZ As String = ""
    Private ConteoID As TipoConteo
    Private ValSKU As String = ""
    Private Const SQLConErr As String = "Se perdio la conexion con la base de datos."
    Private Const FrmName As String = "Toma de Stock Inicial."

    Private TomaLote As Boolean = False
    Private TomaPartida As Boolean = False
#End Region

#Region "Declaracion de Propertys"

    Enum TipoConteo
        'Define que tipo de conteo se va a realizar para evitar errores
        'de input.
        Cantidad = 1
        Etiqueta = 2
    End Enum

    Public Property RazonSocial() As String
        Get
            Return ClienteRZ
        End Get
        Set(ByVal value As String)
            ClienteRZ = value
        End Set
    End Property

    Public Property ValidaSku() As String
        Get
            Return ValSKU
        End Get
        Set(ByVal value As String)
            ValSKU = value
        End Set
    End Property

    Public Property ModoConteo() As TipoConteo
        Get
            Return ConteoID
        End Get
        Set(ByVal value As TipoConteo)
            ConteoID = value
        End Set
    End Property

    Public Property CodigoCliente() As String
        Get
            Return ClienteID
        End Get
        Set(ByVal value As String)
            ClienteID = value
        End Set
    End Property

    Public Property Lote() As Boolean
        Get
            Return TomaLote
        End Get
        Set(ByVal value As Boolean)
            TomaLote = value
        End Set
    End Property

    Public Property Partida() As Boolean
        Get
            Return TomaPartida
        End Get
        Set(ByVal value As Boolean)
            TomaPartida = value
        End Set
    End Property

#End Region

#Region "Metodos y Funciones"

    Public Function ObtenerClientes(ByRef Combo As ComboBox) As Boolean
        Dim Da As SqlDataAdapter
        Dim Ds As New System.Data.DataSet
        Dim drDSRow As Data.DataRow
        Dim drNewRow As Data.DataRow
        Dim dt As New Data.DataTable
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter
        Try
            Combo.DataSource = Nothing
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.GET_CLIENTES_BY_USER"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@USER", SqlDbType.VarChar, 30)
                Pa.Value = vUsr.CodUsuario
                xCmd.Parameters.Add(Pa)
                xCmd.Connection = SQLc
                Da.Fill(Ds, "CLIENTES")
                dt.Columns.Add("RazonSocial", GetType(System.String))
                dt.Columns.Add("Cliente_id", GetType(System.String))
                If Ds.Tables("CLIENTES").Rows.Count > 0 Then
                    'Hay mas de un cliente, los cargo en el combo.
                    For Each drDSRow In Ds.Tables("CLIENTES").Rows()
                        drNewRow = dt.NewRow()
                        drNewRow("RazonSocial") = drDSRow("RazonSocial")
                        drNewRow("Cliente_id") = drDSRow("Cliente_id")
                        dt.Rows.Add(drNewRow)
                    Next
                    Combo.DropDownStyle = ComboBoxStyle.DropDownList
                    With Combo
                        .DataSource = dt
                        .DisplayMember = "RazonSocial"
                        .ValueMember = "Cliente_id"
                        .SelectedIndex = 0
                    End With
                Else
                    MsgBox("Su usuario no posee clientes asignados.", MsgBoxStyle.Exclamation, FrmName)
                    Return False
                End If
                Return True
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, FrmName)
            End If
        Catch SqlEx As SqlException
            MsgBox("SQL. " & SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            Da = Nothing
            Ds = Nothing
            Pa = Nothing
        End Try
    End Function

    Public Function PuedoComenzar(ByRef Err As Integer) As Boolean
        Err = 0
        If Trim(ClienteID) = "" Then
            Err = 1
        End If
        If ValSKU = "" Then
            Err = 3
        End If
        If Me.ModoConteo = 0 Then
            Err = 2
        End If
        If Err > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function ValidarPosicion(ByVal NavePosicion As String)
        Dim Da As SqlDataAdapter
        Dim Ds As New System.Data.DataSet
        Dim dt As New Data.DataTable
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter
        Dim retorno As Boolean
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.fx_TomaInicial_ValNavePosicion"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@NavePosicion", SqlDbType.VarChar, 45)
                Pa.Value = Trim(UCase(NavePosicion))
                xCmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@Return_Value", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.ReturnValue
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                If (xCmd.Parameters("@Return_Value").Value = 0) Then
                    retorno = False
                Else
                    retorno = True
                End If
                Return retorno
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, FrmName)
            End If
        Catch SqlEx As SqlException
            MsgBox("SQL. " & SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            Da = Nothing
            Ds = Nothing
            Pa = Nothing
        End Try
    End Function

    Public Function ValidarProducto(ByRef Producto As TextBox, ByRef LabelProd As Label) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter
        Dim retorno As Boolean
        Try
            If Me.ValidaSku = "S" Then
                If VerifyConnection(SQLc) Then
                    xCmd = SQLc.CreateCommand
                    xCmd.CommandText = "DBO.TomaInicial_ValProducto"
                    xCmd.CommandType = Data.CommandType.StoredProcedure

                    Pa = New SqlParameter("@Cliente_ID", SqlDbType.VarChar, 15)
                    Pa.Value = Trim(UCase(Me.ClienteID))
                    xCmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@Codigo", SqlDbType.VarChar, 50)
                    Pa.Value = Trim(UCase(Producto.Text))
                    xCmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 30)
                    Pa.Direction = ParameterDirection.Output
                    xCmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@Ret", SqlDbType.BigInt)
                    Pa.Direction = ParameterDirection.Output
                    xCmd.Parameters.Add(Pa)

                    xCmd.ExecuteNonQuery()
                    If (xCmd.Parameters("@Ret").Value = 0) Then
                        retorno = False
                    Else
                        retorno = True
                        Producto.Text = xCmd.Parameters("@Producto_id").Value
                        LabelProd.Text = "Desc.:" & GetDescripcion(Producto.Text)
                        LabelProd.Visible = True
                        TomaLotePartida(Producto.Text)
                    End If
                    Return retorno
                Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, FrmName)
                End If
            Else
                LabelProd.Text = "Desc.:" & GetDescripcion(Producto.Text)
                LabelProd.Visible = True
                Lote = False
                Partida = False
                Return True
            End If
        Catch SqlEx As SqlException
            MsgBox("SQL. " & SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            Pa = Nothing
        End Try
    End Function

    Public Function GetDescripcion(ByVal Producto As String) As String
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Ds As New DataSet
        Try
            'If Me.ValidaSku = "S" Then
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "select isnull(descripcion,'') from producto where cliente_id='" & ClienteID & "' and producto_id='" & Producto & "'"
                xCmd.CommandType = Data.CommandType.Text

                Da = New SqlDataAdapter(xCmd)

                Da.Fill(Ds, "Desc")
                If Ds.Tables("Desc").Rows.Count > 0 Then
                    GetDescripcion = IIf(IsDBNull(Ds.Tables("Desc").Rows(0)(0).ToString), "", Ds.Tables("Desc").Rows(0)(0).ToString)
                Else
                    GetDescripcion = "PROD.NO ENCONTRADO"
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, FrmName)
            End If
            'End If
        Catch SqlEx As SqlException
            MsgBox("SQL. " & SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            Da = Nothing
            Ds = Nothing
        End Try
    End Function

    Public Sub TomaLotePartida(ByVal Producto As String)
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter
        Try
            If Me.ValidaSku = "S" Then
                If VerifyConnection(SQLc) Then
                    xCmd = SQLc.CreateCommand
                    xCmd.CommandText = "DBO.TomaInicial_LotePartida"
                    xCmd.CommandType = Data.CommandType.StoredProcedure

                    Pa = New SqlParameter("@Cliente_ID", SqlDbType.VarChar, 15)
                    Pa.Value = Trim(UCase(Me.ClienteID))
                    xCmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@Producto_ID", SqlDbType.VarChar, 30)
                    Pa.Value = Trim(UCase(Producto))
                    xCmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@Lote", SqlDbType.VarChar, 1)
                    Pa.Direction = ParameterDirection.Output
                    xCmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@Partida", SqlDbType.VarChar, 1)
                    Pa.Direction = ParameterDirection.Output
                    xCmd.Parameters.Add(Pa)
                    Pa = Nothing

                    xCmd.ExecuteNonQuery()

                    TomaLote = IIf(xCmd.Parameters("@Lote").Value = "1", True, False)
                    TomaPartida = IIf(xCmd.Parameters("@Partida").Value = "1", True, False)

                Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, FrmName)
                End If
            Else
                TomaLote = False
                TomaPartida = False
            End If
        Catch SqlEx As SqlException
            MsgBox("SQL. " & SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            Pa = Nothing
        End Try
    End Sub

    Public Function Guardar(ByVal Ubicacion As String, ByVal Producto As String, ByVal Cantidad As String, ByVal NroLote As String, ByVal NroPartida As String) As Boolean
        '@UBICACION		VARCHAR(45),
        '@CLIENTE_ID		VARCHAR(15),
        '@PRODUCTO_ID	VARCHAR(30),
        '@CANTIDAD		NUMERIC(20,5),
        '@NRO_LOTE		VARCHAR(50),
        '@NRO_PARTIDA	VARCHAR(50),
        '@VALIDA_SKU		CHAR(1),
        '@USUARIO_ID		VARCHAR(50)
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter
        Try

            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "DBO.REGISTRA_TOMA_STOCK_INICIAL"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@UBICACION", SqlDbType.VarChar, 45)
                Pa.Value = Trim(UCase(Ubicacion))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Trim(UCase(Me.CodigoCliente))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = Trim(UCase(Producto.Trim.ToUpper))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CANTIDAD", SqlDbType.Float)
                If Cantidad.Trim = "" Then
                    Pa.Value = 1
                Else
                    Pa.Value = CDbl(Cantidad)
                End If
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_LOTE", SqlDbType.VarChar, 50)
                If Not Me.Lote Then
                    Pa.Value = DBNull.Value
                Else
                    Pa.Value = NroLote.Trim.ToUpper
                End If
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 50)

                If Not Me.Partida Then
                    Pa.Value = DBNull.Value
                Else
                    Pa.Value = NroPartida.Trim.ToUpper
                End If
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VALIDA_SKU", SqlDbType.Char, 1)
                Pa.Value = Me.ValidaSku.Trim.ToUpper
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO_ID", SqlDbType.VarChar, 50)
                Pa.Value = vUsr.CodUsuario.Trim.ToUpper
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()
                Return True
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, FrmName)
            End If

        Catch SqlEx As SqlException
            MsgBox("SQL. " & SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            Pa = Nothing
        End Try

    End Function

    Public Function GetStockByPosicion(ByVal Ubicacion As String, ByRef Ds As DataSet)
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim PA As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "DBO.TOMA_INICIAL_STOCK_POR_POSICION"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                PA = New SqlParameter("@UBICACION", SqlDbType.VarChar, 45)
                PA.Value = Ubicacion
                xCmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.ClienteID
                xCmd.Parameters.Add(PA)
                PA = Nothing

                Da = New SqlDataAdapter(xCmd)
                Da.Fill(Ds, "STOCK")

            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, FrmName)
            End If
            Return True
        Catch SqlEx As SqlException
            MsgBox("SQL. " & SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            Da = Nothing
        End Try
    End Function

    Public Function BorrarStock(ByVal Id As Long) As Boolean
        Dim xCmd As SqlCommand
        Dim PA As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "DBO.TOMA_INICIAL_STOCK_BORRAR_REGISTRO"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                PA = New SqlParameter("@ID", SqlDbType.BigInt)
                PA.Value = Id
                xCmd.Parameters.Add(PA)

                xCmd.ExecuteNonQuery()
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, FrmName)
            End If
            Return True
        Catch SqlEx As SqlException
            MsgBox("SQL. " & SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            PA = Nothing
            xCmd = Nothing
        End Try
    End Function
#End Region

End Class
