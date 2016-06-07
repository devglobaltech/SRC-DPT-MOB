Option Explicit On
Imports System.Data
Imports System.Data.SqlClient

Public Class cModuloProduccion

    Private Const SQLConErr As String = "Fallo al intentar conectar con la base de datos."
    Private Const clsName As String = "Modulo de ensamble"

    Private Conn As SqlConnection
    Private ClienteID As String = ""
    Private User As String = ""
    Private vCliente_ID As String = ""
    Private vProducto_ID As String = ""
    Private vProducto_DESC As String = ""
    Private vNro_Bulto As String = ""
    Private vNro_Pallet As String = ""
    Private vViaje_ID As String = ""
    Private vPosicion_Cod As String = ""
    Private vTipoMovimiento As String = ""
    Private vPosicionDestino As String = ""

    Public ReadOnly Property GeneroTareas()
        Get
            If Me.vViaje_ID.Trim = "" Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Public ReadOnly Property UbicacionOrigen() As String
        Get
            Return Me.vPosicion_Cod
        End Get
    End Property

    Public ReadOnly Property Nro_Bulto() As String
        Get
            Return vNro_Bulto
        End Get
    End Property

    Public ReadOnly Property Nro_pallet() As String
        Get
            Return vNro_Pallet
        End Get
    End Property

    Public Property TipoMovimiento() As String
        Get
            Return Me.vTipoMovimiento
        End Get
        Set(ByVal value As String)
            Me.vTipoMovimiento = value
        End Set
    End Property

    Public Property Usuario() As String
        Get
            Return User
        End Get
        Set(ByVal value As String)
            User = value
        End Set
    End Property

    Public Property Cliente() As String
        Get
            Return Me.ClienteID
        End Get
        Set(ByVal value As String)
            Me.ClienteID = value
        End Set
    End Property

    Public Property Conexion() As SqlConnection
        Get
            Return Conn
        End Get
        Set(ByVal value As SqlConnection)
            Conn = value
        End Set
    End Property

    Public Function GetClientesByUser(ByRef CMB As ComboBox) As Boolean
        Dim Da As SqlDataAdapter
        Dim Ds As New System.Data.DataSet
        Dim drDSRow As Data.DataRow
        Dim drNewRow As Data.DataRow
        Dim dt As New Data.DataTable
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter

        Try
            If VerifyConnection(Conn) Then
                CMB.DataSource = Nothing
                xCmd = Conn.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.GET_CLIENTES_BY_USER"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@USER", SqlDbType.VarChar, 30)
                Pa.Value = Me.Usuario
                xCmd.Parameters.Add(Pa)
                xCmd.Connection = SQLc
                Da.Fill(Ds, "CLIENTES")
                dt.Columns.Add("RazonSocial", GetType(System.String))
                dt.Columns.Add("Cliente_id", GetType(System.String))
                If Ds.Tables("CLIENTES").Rows.Count > 0 Then

                    For Each drDSRow In Ds.Tables("CLIENTES").Rows()
                        drNewRow = dt.NewRow()
                        drNewRow("RazonSocial") = drDSRow("RazonSocial")
                        drNewRow("Cliente_id") = drDSRow("Cliente_id")
                        dt.Rows.Add(drNewRow)
                    Next
                    CMB.DropDownStyle = ComboBoxStyle.DropDownList
                    With CMB
                        .DataSource = dt
                        .DisplayMember = "RazonSocial"
                        .ValueMember = "Cliente_id"
                        .SelectedIndex = 0
                    End With
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, clsName)
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Critical, clsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
            Return False
        Finally
            xCmd = Nothing
            Da = Nothing
            Ds = Nothing
            Pa = Nothing
        End Try
    End Function

    Public Function Get_Tareas_Transferencia(ByRef Cierre_Forzado As Boolean) As Boolean
        Dim DA As SqlDataAdapter, CMD As SqlCommand, PA As SqlParameter, DS As New DataSet
        Try
            If VerifyConnection(Conn) Then
                '-------------------------------------------------------------
                CMD = Conn.CreateCommand
                DA = New SqlDataAdapter(CMD)
                CMD.CommandType = CommandType.StoredProcedure
                CMD.CommandText = "[dbo].[MOD_PRODUCCION_GET_TAREAS]"
                '-------------------------------------------------------------
                PA = New SqlParameter("@TIPO_OPERACION", SqlDbType.VarChar, 1)
                PA.Value = Me.TipoMovimiento
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                PA = New SqlParameter("@CODIGO_VIAJE", SqlDbType.VarChar, 100)
                If Trim(Me.vViaje_ID) = "" Then
                    PA.Value = DBNull.Value
                Else
                    PA.Value = Me.vViaje_ID
                End If
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                PA = New SqlParameter("@USUARIO_ID", SqlDbType.VarChar, 100)
                If Trim(Me.Usuario) = "" Then
                    PA.Value = DBNull.Value
                Else
                    PA.Value = Me.Usuario
                End If
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                DA.Fill(DS, "REG")
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        Cierre_Forzado = False
                        Me.vCliente_ID = DS.Tables(0).Rows(0)(0).ToString
                        Me.vProducto_ID = DS.Tables(0).Rows(0)(1).ToString
                        Me.vProducto_DESC = DS.Tables(0).Rows(0)(2).ToString
                        Me.vNro_Bulto = DS.Tables(0).Rows(0)(3).ToString
                        Me.vNro_Pallet = DS.Tables(0).Rows(0)(4).ToString
                        Me.vViaje_ID = DS.Tables(0).Rows(0)(5).ToString
                        Me.vPosicion_Cod = DS.Tables(0).Rows(0)(6).ToString
                        'Me.vPosicionDestino = DS.Tables(0).Rows(0)(7).ToString
                        Return True
                    Else
                        Cierre_Forzado = True
                    End If
                Else
                    Cierre_Forzado = True
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, clsName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Critical, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        Finally
            DA = Nothing
            CMD = Nothing
            PA = Nothing
            DS = Nothing
        End Try
    End Function

    Public Function LlenarFormulario(ByRef Form As frmModuloProduccion)
        Try
            Form.cmbClientes.Enabled = False
            Form.lblOperacion.Text = "Operacion: " & Me.vViaje_ID
            Form.lblOperacion.Visible = True
            Form.lblProducto.Visible = True
            Form.lblDescripcion.Text = Me.vProducto_DESC
            Form.lblDescripcion.Visible = True
            Form.txtProducto.Text = Me.vProducto_ID
            Form.txtProducto.Enabled = False
            Form.txtProducto.Visible = True
            If Me.TipoMovimiento = "2" Then
                Form.lblPalletContenedora.Text = "Nro.Pallet " & Me.vNro_Pallet & ":"
            Else
                Form.lblPalletContenedora.Text = "Nro.Cont. " & Me.vNro_Bulto & ":"
            End If
            Form.lblUbicacionOrigen.Visible = False
            Form.lblUbicacionOrigen.Text = "Ubicacion: " & Me.vPosicion_Cod
            Form.txtUbicacionOrigen.Text = ""
            Form.txtUbicacionOrigen.Visible = False

            Form.lblPalletContenedora.Visible = True
            Form.txtPalletContenedora.Visible = True
            Form.txtPalletContenedora.Enabled = True
            Form.txtPalletContenedora.Text = ""
            Form.txtPalletContenedora.Focus()


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        End Try
    End Function

    Public Function InsertarMovimiento() As Boolean
        Dim DA As SqlDataAdapter, CMD As SqlCommand, PA As SqlParameter, DS As New DataSet
        Try
            If VerifyConnection(Conn) Then
                '-------------------------------------------------------------
                CMD = Conn.CreateCommand
                DA = New SqlDataAdapter(CMD)
                CMD.CommandType = CommandType.StoredProcedure
                CMD.CommandText = "[dbo].[MOB_PRODUCCION_INSERT_MOVIMIENTOS]"
                '-------------------------------------------------------------
                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.vCliente_ID
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.vViaje_ID
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                PA = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.vProducto_ID
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                PA = New SqlParameter("@NRO_BULTO", SqlDbType.VarChar, 100)
                PA.Value = Me.vNro_Bulto
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                PA = New SqlParameter("@NRO_PALLET", SqlDbType.VarChar, 100)
                PA.Value = Me.vNro_Pallet
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                PA = New SqlParameter("@POSICION", SqlDbType.VarChar, 100)
                PA.Value = Me.vPosicion_Cod
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                PA = New SqlParameter("@TIPO_MOVIMIENTO", SqlDbType.VarChar, 1)
                PA.Value = Me.TipoMovimiento
                CMD.Parameters.Add(PA)
                PA = Nothing

                CMD.ExecuteNonQuery()

            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, clsName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Critical, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        Finally
            DA = Nothing
            CMD = Nothing
            PA = Nothing
            DS = Nothing
        End Try
    End Function

    Public Function CancelarMovimientos() As Boolean
        Dim DA As SqlDataAdapter, CMD As SqlCommand, PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                CMD = Conn.CreateCommand
                CMD.CommandType = CommandType.StoredProcedure
                CMD.CommandText = "[dbo].[MOD_PRODUCCION_DEL_TAREAS]"
                '-------------------------------------------------------------
                PA = New SqlParameter("@TIPO_OPERACION", SqlDbType.VarChar, 1)
                PA.Value = Me.TipoMovimiento
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                PA = New SqlParameter("@CODIGO_VIAJE", SqlDbType.VarChar, 100)
                PA.Value = Me.vViaje_ID
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                PA = New SqlParameter("@USUARIO_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.Usuario
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                CMD.ExecuteNonQuery()
                '-------------------------------------------------------------
            Else : MsgBox(Me.SQLConErr, MsgBoxStyle.Exclamation, clsName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Critical, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        Finally
            DA = Nothing
            CMD = Nothing
            PA = Nothing
        End Try
    End Function

    Public Function EliminarMovimientos() As Boolean
        Dim DA As SqlDataAdapter, CMD As SqlCommand, PA As SqlParameter
        Try
            If VerifyConnection(Conn) Then
                CMD = Conn.CreateCommand
                CMD.CommandType = CommandType.StoredProcedure
                CMD.CommandText = "[dbo].[MOD_PRODUCCION_DEL_TAREAS]"
                '-------------------------------------------------------------
                PA = New SqlParameter("@TIPO_OPERACION", SqlDbType.VarChar, 1)
                PA.Value = Me.TipoMovimiento
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                PA = New SqlParameter("@CODIGO_VIAJE", SqlDbType.VarChar, 100)
                PA.Value = Me.vViaje_ID
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                PA = New SqlParameter("@USUARIO_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.Usuario
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                CMD.ExecuteNonQuery()
                '-------------------------------------------------------------
            Else : MsgBox(Me.SQLConErr, MsgBoxStyle.Exclamation, clsName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Critical, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        Finally
            DA = Nothing
            CMD = Nothing
            PA = Nothing
        End Try
    End Function

    Public Function FinalizarTransferencias(ByVal Destino As String) As Boolean
        Dim DA As SqlDataAdapter, CMD As SqlCommand, PA As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                'Realizo las Transferencias de la tabla 
                CMD = SQLc.CreateCommand
                CMD.CommandText = "TRANSFERENCIA_PREPICKING"
                CMD.CommandType = CommandType.StoredProcedure
                CMD.Connection = SQLc
                '-------------------------------------------------------------
                PA = New SqlParameter("@UBICACION_DESTINO", SqlDbType.VarChar, 45)
                PA.Value = Destino
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 50)
                PA.Value = Me.vViaje_ID
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                PA = New SqlParameter("@TIPO", SqlDbType.Int)
                PA.Value = Me.vTipoMovimiento
                CMD.Parameters.Add(PA)
                PA = Nothing
                '-------------------------------------------------------------
                CMD.ExecuteNonQuery()
                '-------------------------------------------------------------
                MsgBox("La transferencia se realizo correctamente ", MsgBoxStyle.OkOnly, clsName)

            Else : MsgBox(SQLConErr, MsgBoxStyle.Critical, clsName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Critical, clsName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
        Finally
            DA = Nothing
            CMD = Nothing
            PA = Nothing
        End Try
    End Function

End Class
