Imports System.Data.SqlClient
Imports System.Data
Public Class frmMarbete
    Private Const FrmName As String = "Nuevo Marbete"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private strUbicacion As String
    Private NroInventario As Integer
    Private Producto As String
    Private UsaNroLote As String
    Private UsaNroPartida As String


    Public Property NumeroInventario()
        Get
            Return NroInventario
        End Get
        Set(ByVal value)
            NroInventario = value
        End Set
    End Property
    Public Property Ubicacion() As String
        Get
            Return strUbicacion
        End Get
        Set(ByVal value As String)
            strUbicacion = value
        End Set
    End Property
    Private Sub frmMarbete_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IniForm()
    End Sub
    Private Sub IniForm()
        ObtenerClientes()
        Me.CmbClientes.Focus()
    End Sub
    Private Function ObtenerClientes() As Boolean
        Dim Da As SqlDataAdapter
        Dim Ds As New System.Data.DataSet
        Dim drDSRow As Data.DataRow
        Dim drNewRow As Data.DataRow
        Dim dt As New Data.DataTable
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter
        Try
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
                    Me.CmbClientes.DropDownStyle = ComboBoxStyle.DropDownList
                    With CmbClientes
                        .DataSource = Nothing
                        .DataSource = dt
                        .DisplayMember = "RazonSocial"
                        .ValueMember = "Cliente_id"
                        .SelectedIndex = 0
                    End With
                End If
                If Me.CmbClientes.Items.Count = 1 Then
                    Me.CmbClientes.Enabled = False
                    Me.LblProducto.Visible = True
                    Me.TxtProducto.Visible = True
                    Me.TxtProducto.Focus()
                End If
            Else : MsgBox(SQLError, MsgBoxStyle.Exclamation, FrmName)
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

    Private Sub CmbClientes_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbClientes.KeyUp
        If (e.KeyValue = 13) Then
            Me.CmbClientes.Enabled = False
            Me.LblProducto.Visible = True
            Me.TxtProducto.Visible = True
            Me.TxtProducto.Focus()
        End If
    End Sub

    Private Sub TxtProducto_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtProducto.KeyUp
        If (e.KeyValue = 13) And Trim(Me.TxtProducto.Text) <> "" Then
            o2D.Decode(Me.TxtProducto.Text)
            Me.TxtProducto.Text = o2D.PRODUCTO_ID
            If ValidarIngreso() = True Then
                If Producto = TxtProducto.Text Then
                    BuscarDescripcionYUnidad()
                    Me.TxtProducto.ReadOnly = True
                    If UsaNroLote Then
                        lbl_nro_lote.Visible = True
                        Txt_nro_lote.Visible = True
                        Txt_nro_lote.Text = ""
                        Txt_nro_lote.Focus()
                    ElseIf UsaNroPartida Then
                        lbl_nro_partida.Visible = True
                        Txt_nro_partida.Visible = True
                        Txt_nro_partida.Text = ""
                        Txt_nro_partida.Focus()
                    Else
                        Me.LblUbicacion.Visible = True
                        Me.TxtUbicacion.Visible = True
                        Me.TxtUbicacion.Focus()
                    End If
                Else
                    TxtProducto.Text = ""
                    TxtProducto.Focus()
                End If
            Else
                TxtProducto.Text = ""
                TxtProducto.Focus()
            End If
        End If
    End Sub
    Private Function ValidarIngreso() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.Val_Prod"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@CLIENTE_ID", Data.SqlDbType.VarChar, 50)
                Pa.Value = Me.CmbClientes.SelectedValue
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CODIGO", SqlDbType.VarChar, 50)
                Pa.Value = Me.TxtProducto.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Producto = IIf(IsDBNull(Cmd.Parameters("@PRODUCTO_ID").Value), "", Cmd.Parameters("@PRODUCTO_ID").Value)
                Me.LblProducto.Visible = True
                Me.TxtProducto.Visible = True
                Me.txtProducto.Text = Trim(UCase(Producto))
                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("SQL ValidarIngreso: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("ValidarIngreso: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function
    Private Function BuscarDescripcionYUnidad() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.GET_DESCRIPCION_UNIDAD_PRODUCTO"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@CLIENTE_ID", Data.SqlDbType.VarChar, 50)
                Pa.Value = Me.CmbClientes.SelectedValue
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 50)
                Pa.Value = Producto
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@DESCRIPCION", SqlDbType.VarChar, 30)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@UNIDAD", SqlDbType.VarChar, 30)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USA_NROLOTE", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USA_NROPARTIDA", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

                Me.LblTitDescripcion.Visible = True
                Me.LblDescripcion.Visible = True
                Me.LblDescripcion.Text = IIf(IsDBNull(Cmd.Parameters("@DESCRIPCION").Value), "", Cmd.Parameters("@DESCRIPCION").Value)
                If IsDBNull(Cmd.Parameters("@UNIDAD").Value) Then
                    LblCant.Text = "Cantidad"
                Else
                    LblCant.Text = "Cantidad en " + Cmd.Parameters("@UNIDAD").Value
                End If

                UsaNroLote = IIf(IsDBNull(Cmd.Parameters("@USA_NROLOTE").Value), "0", Cmd.Parameters("@USA_NROLOTE").Value)
                UsaNroPartida = IIf(IsDBNull(Cmd.Parameters("@USA_NROPARTIDA").Value), "0", Cmd.Parameters("@USA_NROPARTIDA").Value)



                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("SQL BuscarDescripcionYUnidad: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("BuscarDescripcionYUnidad: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function

    Private Sub TxtUbicacion_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtUbicacion.KeyUp
        If e.KeyValue = 13 Then
            If Me.TxtUbicacion.Text <> "" Then
                If ExisteNavePosicion(Me.TxtUbicacion.Text) Then
                    If ValidaPosicion(Me.TxtUbicacion.Text) Then
                        Me.TxtUbicacion.ReadOnly = True
                        Me.LblTitObser.Visible = True
                        Me.TxtObservaciones.Visible = True
                        Me.TxtObservaciones.Focus()
                    Else
                        Me.TxtUbicacion.Text = ""
                        Me.TxtUbicacion.Focus()
                    End If
                Else
                    Me.TxtUbicacion.Text = ""
                    Me.TxtUbicacion.Focus()
                End If
            Else
                Me.TxtUbicacion.Text = ""
                Me.TxtUbicacion.Focus()
            End If
        End If
    End Sub

    Private Function ExisteNavePosicion(ByVal xUbicacion As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim xValue As String
        Try
            If VerifyConnection(SQLc) Then
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
            Else
                MsgBox(SQLError, MsgBoxStyle.Exclamation, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("ExisteNavePosicion SQL: " & SQLEx.Message)
            Return False
        Catch ex As Exception
            MsgBox("ExisteNavePosicion: " & ex.Message)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub TxtObservaciones_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtObservaciones.KeyUp
        If e.KeyValue = 13 Then
            Me.TxtObservaciones.ReadOnly = True
            Me.LblCant.Visible = True
            Me.TxtCant.Visible = True
            Me.TxtCant.Focus()
        End If
    End Sub

    Private Sub cmdCerrarPallet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCerrarPallet.Click
        salir()
    End Sub
    Private Sub salir()
        Dim Rta As Object
        Rta = MsgBox("Desea salir de la creacion de marbete?", MsgBoxStyle.YesNo, FrmName)
        If Rta = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Sub TxtCant_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCant.KeyUp
        Dim Rta As Object
        If e.KeyValue = 13 Then
            If Val(TxtCant.Text) >= 0 Then
                Rta = MsgBox("Desea Confirmar la cantidad?", MsgBoxStyle.YesNo, FrmName)
                If Rta = vbYes Then
                    GrabaMarbete()
                    Me.Close()
                Else
                    TxtCant.Text = ""
                    TxtCant.Focus()
                End If
            Else
                TxtCant.Text = ""
                TxtCant.Focus()
            End If
        End If
    End Sub
    Private Function GrabaMarbete() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.FUNCIONES_INVENTARIO_API#CREA_MARBETE"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@INVENTARIO_ID", Data.SqlDbType.Int, 20)
                Pa.Value = NroInventario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION", SqlDbType.VarChar, 45)
                Pa.Value = Me.TxtUbicacion.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 20)
                Pa.Value = Me.CmbClientes.SelectedValue
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = Me.TxtProducto.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CANTIDAD", SqlDbType.Decimal, 10)
                Pa.Value = Me.TxtCant.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_LOTE", SqlDbType.VarChar, 100)
                Pa.Value = IIf(Me.Txt_nro_lote.Text.Length > 0, Me.Txt_nro_lote.Text.ToUpper, DBNull.Value)
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 100)
                Pa.Value = IIf(Me.Txt_nro_partida.Text.Length > 0, Me.Txt_nro_partida.Text.ToUpper, DBNull.Value)
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@OBSERVACION", SqlDbType.VarChar, 2000)
                Pa.Value = Me.TxtObservaciones.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("SQL GRABA_CANT_CONTEO: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("GRABA_CANT_CONTEO: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function

    Private Sub TxtCant_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtCant.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub cmdApertura_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApertura.Click
        cancelar()
    End Sub
    Private Sub Cancelar()
        If Me.CmbClientes.Items.Count > 1 Then
            Me.CmbClientes.Enabled = True
            Me.CmbClientes.Focus()
            Me.LblProducto.Visible = False
            Me.TxtProducto.Visible = False
            Me.TxtProducto.Text = ""
        Else
            Me.TxtProducto.Text = ""
            Me.TxtProducto.Focus()
        End If
        Me.TxtProducto.ReadOnly = False
        Me.LblUbicacion.Visible = False
        Me.TxtUbicacion.ReadOnly = False
        Me.TxtUbicacion.Text = ""
        Me.TxtUbicacion.Visible = False
        Me.LblTitDescripcion.Visible = False
        Me.LblDescripcion.Visible = False
        Me.LblDescripcion.Text = ""
        Me.LblTitObser.Visible = False
        Me.TxtObservaciones.ReadOnly = False
        Me.TxtObservaciones.Text = ""
        Me.TxtObservaciones.Visible = False
        Me.LblCant.Visible = False
        Me.TxtCant.Text = ""
        Me.LblCant.Text = "Cantidad"
        Me.Txt_nro_lote.Text = ""
        Me.Txt_nro_partida.Text = ""
        Me.lbl_nro_lote.Visible = False
        Me.Txt_nro_lote.Visible = False
        Me.Txt_nro_lote.ReadOnly = False
        Me.lbl_nro_partida.Visible = False
        Me.Txt_nro_partida.Visible = False
        Me.Txt_nro_partida.ReadOnly = False



    End Sub
    Private Function ValidaPosicion(ByVal xUbicacion As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim xValue As String
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "ExisteNavePosicionInventario"
                Cmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Ubicacion", SqlDbType.VarChar, 45)
                Pa.Value = xUbicacion
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@INVENTARIO_ID", SqlDbType.Int, 10)
                Pa.Value = NroInventario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Retorno", SqlDbType.Char, 1, ParameterDirection.Output)
                Pa.Value = DBNull.Value
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
                xValue = IIf(IsDBNull(Cmd.Parameters("@Retorno").Value), "", Cmd.Parameters("@Retorno").Value)
            Else
                MsgBox(SQLError, MsgBoxStyle.Exclamation, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("ValudaPosicion SQL: " & SQLEx.Message)
            Return False
        Catch ex As Exception
            MsgBox("ValidaPosicion: " & ex.Message)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub TxtCant_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCant.TextChanged

    End Sub

    Private Sub TxtProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtProducto.TextChanged

    End Sub

    Private Sub lbl_partida_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbl_nro_partida.ParentChanged

    End Sub


    Private Sub Txt_nro_lote_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_nro_lote.KeyUp
        If (e.KeyValue = 13) And Trim(Me.Txt_nro_lote.Text) <> "" Then
            Txt_nro_lote.ReadOnly = True
            If UsaNroPartida Then
                lbl_nro_partida.Visible = True
                Txt_nro_partida.Visible = True
                Txt_nro_partida.Text = ""
                Txt_nro_partida.Focus()
            Else
                Me.LblUbicacion.Visible = True
                Me.TxtUbicacion.Visible = True
                Me.TxtUbicacion.Focus()
            End If
        End If
    End Sub

    Private Sub TxtUbicacion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtUbicacion.TextChanged

    End Sub

    

    Private Sub Txt_nro_partida_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_nro_partida.KeyUp
        If (e.KeyValue = 13) And Trim(Me.Txt_nro_partida.Text) <> "" Then
            Txt_nro_partida.ReadOnly = True
            Me.LblUbicacion.Visible = True
            Me.TxtUbicacion.Visible = True
            Me.TxtUbicacion.Focus()
        End If
    End Sub

    
End Class