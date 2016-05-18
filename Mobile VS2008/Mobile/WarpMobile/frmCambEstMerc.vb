Imports System.Data.SqlClient
Imports System.Data
Public Class frmCambEstMerc
    Private Producto As String
    Private Indice As Integer
    Private Cliente As String
    Private Descripcion As String
    Private Cantidad As String
    Private Lote As String
    Private Partida As String
    Private Contenedora As String
    Private NroSerie As String
    Private CatLog As String
    Private EstMerc As String
    Private Pallet As String
    Private LoteProveedor As String
    Private NroAnalisis As String
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private Const FrmName As String = "Cambio de Estado de Mercaderia"

    Private Sub frmCambEstMerc_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case 112 'Keys.F1
                If Me.BtnConfirmar.Enabled Then
                    ConfirmarCambio(Me.CmbEstMerc.SelectedValue)
                    Cancelar()
                End If
            Case 113 'Keys.F2
                Cancelar()
            Case 114 'Keys.F3
                If Me.BtnModificar.Enabled Then
                    Me.TxtCantidad.Text = ""
                    Me.TxtCantidad.Visible = False
                    Me.CmbEstMerc.Visible = False
                    Me.LblCatLog.Visible = False
                    Me.LblCantidad.Visible = False
                End If
            Case 115 'Keys.F4
                ExitCambEstMerc()
        End Select
    End Sub
    Private Sub ExitCambEstMerc()
        Dim Rta As Object
        If Me.DGEstMerc.Visible = True Then
            Rta = MsgBox("Desea Cancelar la operacion en curso y salir?", MsgBoxStyle.YesNo, FrmName)
        Else
            Rta = MsgBox("Desea salir de Cambio de Estado de Mercaderia?", MsgBoxStyle.YesNo, FrmName)
        End If
        If Rta = vbYes Then
            Me.Close()
        End If
    End Sub
    Private Sub Cancelar()
        Me.txtUbicacion.Text = ""
        Me.txtUbicacion.ReadOnly = False
        Me.txtUbicacion.Focus()
        Me.TxtProducto.Text = ""
        Me.txtUbicacion.ReadOnly = False
        Me.TxtProducto.Visible = False
        Me.TxtProducto.ReadOnly = False
        Me.LblCodProd.Visible = False
        Me.DGEstMerc.Visible = False
        Me.LblCantidad.Visible = False
        Me.TxtCantidad.Visible = False
        Me.TxtCantidad.ReadOnly = False
        Me.LblCatLog.Visible = False
        Me.CmbEstMerc.Visible = False
        Me.CmbEstMerc.Enabled = True
        Me.BtnConfirmar.Enabled = False
        Me.BtnModificar.Enabled = False
    End Sub
    Private Sub IniciarForm()
        Cancelar()
        Me.BtnConfirmar.Enabled = False
        Me.BtnCancelar.Enabled = False
        Me.BtnModificar.Enabled = False
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

    Private Sub BtnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelar.Click
        Cancelar()
    End Sub

    Private Sub BtnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSalir.Click
        ExitCambEstMerc()
    End Sub

    Private Sub BtnModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnModificar.Click
        Me.TxtCantidad.Text = ""
        Me.TxtCantidad.Visible = False
        Me.TxtCantidad.ReadOnly = False
        Me.CmbEstMerc.Visible = False
        Me.CmbEstMerc.Enabled = True
        Me.LblCatLog.Visible = False
        Me.LblCantidad.Visible = False
        Me.BtnConfirmar.Enabled = False
        Me.BtnModificar.Enabled = False
        Me.DGEstMerc.Focus()
    End Sub

    Private Sub txtUbicacion_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUbicacion.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtUbicacion.Text <> "" Then
                If Not ExisteNavePosicion(UCase(Me.txtUbicacion.Text)) Then
                    Me.txtUbicacion.Text = ""
                    Me.txtUbicacion.Focus()
                    Exit Sub
                End If
                Me.txtUbicacion.ReadOnly = True
                Me.TxtProducto.Visible = True
                Me.LblCodProd.Visible = True
                Me.BtnCancelar.Enabled = True
                Me.TxtProducto.Focus()
            End If
        End If
    End Sub

    Private Sub TxtProducto_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtProducto.KeyUp
        If e.KeyValue = 13 Then
            If Me.TxtProducto.Text <> "" Then
                o2D.Decode(Me.TxtProducto.Text)
                Me.TxtProducto.Text = o2D.PRODUCTO_ID
                If Not ValidarIngreso() Then
                    Me.TxtProducto.Text = ""
                    Me.TxtProducto.Focus()
                    Exit Sub
                End If
                Me.TxtProducto.ReadOnly = True
                CargarGrilla()

            End If
        End If
    End Sub

    Private Sub TxtCantidad_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCantidad.KeyUp
        If e.KeyValue = 13 Then
            If (Val(TxtCantidad.Text) <= Me.Cantidad) And (Val(TxtCantidad.Text) > 0) Then
                Me.LblCatLog.Visible = True
                Me.TxtCantidad.ReadOnly = True
                Me.CmbEstMerc.Visible = True
                Me.TxtCantidad.ReadOnly = True
                Me.CmbEstMerc.Focus()
            Else
                If Val(TxtCantidad.Text) <> 0 Then
                    MsgBox("La cantidad maxima a cambiar es :" + Me.Cantidad, MsgBoxStyle.OkOnly, FrmName)
                Else
                    MsgBox("La cantidad no puede ser menor ni igual a Cero", MsgBoxStyle.OkOnly, FrmName)
                End If
                Me.TxtCantidad.Text = ""
                Me.DGEstMerc.Focus()
            End If
        End If
    End Sub

    Private Sub DGEstMerc_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGEstMerc.KeyUp
        If e.KeyValue = 13 Then
            Me.LblCantidad.Visible = True
            Me.TxtCantidad.Text = ""
            Me.TxtCantidad.Visible = True
            Me.TxtCantidad.Focus()
            Indice = Me.DGEstMerc.CurrentRowIndex
            Cliente = Me.DGEstMerc.Item(Indice, 0).ToString
            Descripcion = Me.DGEstMerc.Item(Indice, 1).ToString
            Cantidad = Me.DGEstMerc.Item(Indice, 2)
            Lote = Me.DGEstMerc.Item(Indice, 4).ToString
            Partida = Me.DGEstMerc.Item(Indice, 5).ToString
            Contenedora = Me.DGEstMerc.Item(Indice, 6).ToString
            NroSerie = Me.DGEstMerc.Item(Indice, 7).ToString
            CatLog = Me.DGEstMerc.Item(Indice, 8).ToString
            EstMerc = Me.DGEstMerc.Item(Indice, 9).ToString
            Pallet = Me.DGEstMerc.Item(Indice, 10).ToString
            LoteProveedor = Me.DGEstMerc.Item(Indice, 11).ToString
            NroAnalisis = Me.DGEstMerc.Item(Indice, 12).ToString
            CargarComboEstMerc(Cliente)
        End If
    End Sub

    Private Sub CargarComboEstMerc(ByVal Cliente As String)
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
                xCmd.CommandText = "DBO.GET_ESTMERC"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@CLIENTE", SqlDbType.VarChar, 30)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                xCmd.Connection = SQLc
                Da.Fill(Ds, "ESTMERC")
                dt.Columns.Add("EST_MERC_ID", GetType(System.String))
                dt.Columns.Add("Descripcion", GetType(System.String))
                If Ds.Tables("ESTMERC").Rows.Count > 0 Then
                    'Hay mas de un cliente, los cargo en el combo.
                    For Each drDSRow In Ds.Tables("ESTMERC").Rows()
                        drNewRow = dt.NewRow()
                        drNewRow("EST_MERC_ID") = drDSRow("EST_MERC_ID")
                        drNewRow("Descripcion") = drDSRow("EST_MERC_ID")
                        dt.Rows.Add(drNewRow)
                    Next
                    Me.CmbEstMerc.DropDownStyle = ComboBoxStyle.DropDownList
                    With CmbEstMerc
                        .DataSource = Nothing
                        .DataSource = dt
                        .DisplayMember = "EST_MERC_ID"
                        .ValueMember = "EST_MERC_ID"
                        .SelectedIndex = 0
                    End With
                Else
                    MsgBox("El usuario " + vUsr.CodUsuario + " no posee clientes asignados", MsgBoxStyle.OkOnly, FrmName)
                    Me.Close()
                    Exit Sub
                End If
                If Me.CmbEstMerc.Items.Count = 1 Then
                    Me.CmbEstMerc.Enabled = False
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
    End Sub

    Private Sub frmCambEstMerc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IniciarForm()
    End Sub

    Private Sub CmbEstMerc_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbEstMerc.KeyUp
        If e.KeyValue = 13 Then
            If Me.CmbEstMerc.SelectedValue <> Me.EstMerc Then
                Me.BtnConfirmar.Enabled = True
                Me.BtnModificar.Enabled = True
                Me.BtnConfirmar.Focus()
            Else
                MsgBox("No es posible cambiar el estado de mercaderia del producto con codigo " + Me.Producto + ", en la posicion " + Me.txtUbicacion.Text, MsgBoxStyle.Critical, FrmName)
                Me.TxtCantidad.Focus()
            End If
        End If
    End Sub
    Private Sub ConfirmarCambio(ByVal NuevoEstMerc As String)
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim Cmd As SqlCommand

        Try
            Dim Rta As Object = MsgBox("¿Confirma el Cambio de Estado de Mercaderia?", MsgBoxStyle.YesNo, FrmName)
            If Rta = vbYes Then
                If VerifyConnection(SQLc) Then
                    'Cmd.Transaction = SQLc.BeginTransaction()
                    Cmd = SQLc.CreateCommand
                    Da = New SqlDataAdapter(Cmd)
                    Cmd.CommandText = "DBO.CAMB_ESTMERC"
                    Cmd.CommandType = Data.CommandType.StoredProcedure
                    Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                    Pa.Value = Cliente
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                    Pa.Value = Producto
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Pa = New SqlParameter("@CANTIDAD", SqlDbType.Float)
                    Pa.Value = Val(Cantidad)
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Pa = New SqlParameter("@NRO_LOTE", SqlDbType.VarChar, 50)
                    If Trim(Lote) = "" Then
                        Pa.Value = DBNull.Value
                    Else
                        Pa.Value = Lote
                    End If
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Pa = New SqlParameter("@NRO_PARTIDA", SqlDbType.VarChar, 50)
                    If Trim(Partida) = "" Then
                        Pa.Value = DBNull.Value
                    Else
                        Pa.Value = Partida
                    End If
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Pa = New SqlParameter("@NRO_BULTO", SqlDbType.VarChar, 50)
                    If Trim(Contenedora) = "" Then
                        Pa.Value = DBNull.Value
                    Else
                        Pa.Value = Contenedora
                    End If
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Pa = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 50)
                    If Trim(NroSerie) = "" Then
                        Pa.Value = DBNull.Value
                    Else
                        Pa.Value = NroSerie
                    End If
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Pa = New SqlParameter("@CAT_LOG_ID", SqlDbType.VarChar, 50)
                    Pa.Value = CatLog
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Pa = New SqlParameter("@EST_MERC_ID", SqlDbType.VarChar, 50)
                    If Trim(EstMerc) = "" Then
                        Pa.Value = DBNull.Value
                    Else
                        Pa.Value = EstMerc
                    End If
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Pa = New SqlParameter("@PROP1", SqlDbType.VarChar, 100)
                    If Trim(Pallet) = "" Then
                        Pa.Value = DBNull.Value
                    Else
                        Pa.Value = Pallet
                    End If
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Pa = New SqlParameter("@PROP2", SqlDbType.VarChar, 100)
                    If Trim(LoteProveedor) = "" Then
                        Pa.Value = DBNull.Value
                    Else
                        Pa.Value = LoteProveedor
                    End If
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Pa = New SqlParameter("@PROP3", SqlDbType.VarChar, 100)
                    If Trim(NroAnalisis) = "" Then
                        Pa.Value = DBNull.Value
                    Else
                        Pa.Value = NroAnalisis
                    End If
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Pa = New SqlParameter("@NEW_EST_MERC_ID", SqlDbType.VarChar, 100)
                    Pa.Value = NuevoEstMerc
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Pa = New SqlParameter("@TOTAL_A_CAMBIAR", SqlDbType.Float)
                    Pa.Value = Val(Me.TxtCantidad.Text)
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Pa = New SqlParameter("@UBICACION", SqlDbType.VarChar, 45)
                    Pa.Value = Me.txtUbicacion.Text
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing
                    Cmd.ExecuteNonQuery()
                    'Cmd.Transaction.Commit()
                Else : MsgBox(SQLError, MsgBoxStyle.Exclamation, FrmName)
                End If
                MsgBox("El Cambio se realizo Correctamente", MsgBoxStyle.Information, FrmName)
            End If
            Cancelar()
        Catch SqlEx As SqlException
            'Cmd.Transaction.Rollback()
            MsgBox("SQL. " & SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            Pa = Nothing
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Sub

    Private Sub BtnConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConfirmar.Click
        If CDbl(Me.Cantidad) >= CDbl(Me.TxtCantidad.Text) Then
            ConfirmarCambio(Me.CmbEstMerc.SelectedValue)
            Cancelar()
        Else
            MsgBox("La cantidad indicada no es correcta", MsgBoxStyle.YesNo, FrmName)
        End If
    End Sub
    Private Function ValidarIngreso() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "CAMBCATLOG_VAL_PROD"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@Posicion", Data.SqlDbType.VarChar, 50)
                Pa.Value = Me.txtUbicacion.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Codigo", SqlDbType.VarChar, 50)
                Pa.Value = Trim(UCase(Me.txtProducto.Text))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@ProductoId", SqlDbType.VarChar, 30)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Producto = IIf(IsDBNull(Cmd.Parameters("@ProductoId").Value), "", Cmd.Parameters("@ProductoId").Value)
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
    Private Sub CargarGrilla()
        If GetDatosGrilla() Then
            Me.ResizeGrillaUbicacion()
            Me.DGEstMerc.Visible = True
            Me.DGEstMerc.Focus()
        Else
            MsgBox("No hay existencia del producto codigo " + Me.Producto + ", en la posicion " + Me.txtUbicacion.Text, MsgBoxStyle.YesNo, FrmName)
            Me.Cancelar()
        End If
    End Sub
    Private Function GetDatosGrilla() As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = "GET_DATA_FOR_CAMBCATLOG"

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 50)
                Pa.Value = Me.TxtProducto.Text
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@POSICION_COD", SqlDbType.VarChar, 50)
                Pa.Value = Me.txtUbicacion.Text
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Da.Fill(Ds, "DATOS")
                If Ds.Tables("DATOS").Rows.Count > 0 Then
                    Me.DGEstMerc.DataSource = Ds.Tables("DATOS")
                Else
                    Return False
                    Me.Cancelar()
                End If
            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
                Return True
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            Pa = Nothing
            Ds = Nothing
            xCmd = Nothing
            Da = Nothing
        End Try
    End Function
    Private Function ResizeGrillaUbicacion() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "DATOS"
            Me.DGEstMerc.TableStyles.Clear()

            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "CLIENTE_ID"
            TextCol1.HeaderText = "Cliente"
            TextCol1.Width = 100
            Style.GridColumnStyles.Add(TextCol1)
            TextCol1 = Nothing

            Dim TextCol2 As New DataGridTextBoxColumn
            TextCol2.MappingName = "DESCRIPCION"
            TextCol2.HeaderText = "Descripcion"
            TextCol2.Width = 150
            Style.GridColumnStyles.Add(TextCol2)
            TextCol2 = Nothing

            Dim TextCol3 As New DataGridTextBoxColumn
            TextCol3.MappingName = "CANTIDAD"
            TextCol3.HeaderText = "Cantidad"
            TextCol3.Width = 80
            Style.GridColumnStyles.Add(TextCol3)
            TextCol3 = Nothing

            Dim TextCol4 As New DataGridTextBoxColumn
            TextCol4.MappingName = "FECHA_VENCIMIENTO"
            TextCol4.HeaderText = "Fecha Vto"
            TextCol4.Width = 80
            Style.GridColumnStyles.Add(TextCol4)
            TextCol4 = Nothing

            Dim TextCol5 As New DataGridTextBoxColumn
            TextCol5.MappingName = "NRO_LOTE"
            TextCol5.HeaderText = "Nro Lote"
            TextCol5.Width = 80
            Style.GridColumnStyles.Add(TextCol5)
            TextCol5 = Nothing

            Dim TextCol6 As New DataGridTextBoxColumn
            TextCol6.MappingName = "NRO_PARTIDA"
            TextCol6.HeaderText = "Nro Partida"
            TextCol6.Width = 80
            Style.GridColumnStyles.Add(TextCol6)
            TextCol6 = Nothing

            Dim TextCol7 As New DataGridTextBoxColumn
            TextCol7.MappingName = "NRO_BULTO"
            TextCol7.HeaderText = "Contenedora"
            TextCol7.Width = 80
            Style.GridColumnStyles.Add(TextCol7)
            TextCol7 = Nothing

            Dim TextCol8 As New DataGridTextBoxColumn
            TextCol8.MappingName = "NRO_SERIE"
            TextCol8.HeaderText = "Nro Serie"
            TextCol8.Width = 80
            Style.GridColumnStyles.Add(TextCol8)
            TextCol8 = Nothing

            Dim TextCol9 As New DataGridTextBoxColumn
            TextCol9.MappingName = "CAT_LOG_ID"
            TextCol9.HeaderText = "Cat. Logica"
            TextCol9.Width = 80
            Style.GridColumnStyles.Add(TextCol9)
            TextCol9 = Nothing

            Dim TextCol10 As New DataGridTextBoxColumn
            TextCol10.MappingName = "EST_MERC_ID"
            TextCol10.HeaderText = "Est. Merc."
            TextCol10.Width = 80
            Style.GridColumnStyles.Add(TextCol10)
            TextCol10 = Nothing

            Dim TextCol11 As New DataGridTextBoxColumn
            TextCol11.MappingName = "PROP1"
            TextCol11.HeaderText = "Pallet"
            TextCol11.Width = 80
            Style.GridColumnStyles.Add(TextCol11)
            TextCol11 = Nothing

            Dim TextCol12 As New DataGridTextBoxColumn
            TextCol12.MappingName = "PROP2"
            TextCol12.HeaderText = "Lote Prov."
            TextCol12.Width = 80
            Style.GridColumnStyles.Add(TextCol12)
            TextCol12 = Nothing

            Dim TextCol13 As New DataGridTextBoxColumn
            TextCol13.MappingName = "PROP3"
            TextCol13.HeaderText = "Nro. Analisis"
            TextCol13.Width = 80
            Style.GridColumnStyles.Add(TextCol13)
            TextCol13 = Nothing


            Me.DGEstMerc.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("ResizeGrillaUbicacion: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Function

    Private Sub TxtProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtProducto.TextChanged

    End Sub
End Class