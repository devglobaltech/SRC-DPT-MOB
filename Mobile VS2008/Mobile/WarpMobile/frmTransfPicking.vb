Imports System.Data.SqlClient
Imports System.Data
Public Class frmTransfPicking

    Private TSeleccion As Integer = 0 '1= Contenedor; 2= Pallet.
    Private blnTransCurso As Boolean = False
    Private TrLocator As Boolean
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private Const FrmName As String = "Transferencias Picking"
    Private Pallet As String
    Private Contenedora As String
    Private Producto As String
    Private ProductoIng As String
    Private Documento_id As Integer
    Private ProdDesc As String
    Private Nro_Linea As Integer
    Private Fin As Boolean
    Private Ubicacion_Origen As String

    Public Property Seleccion() As Integer
        Get
            Return TSeleccion
        End Get
        Set(ByVal value As Integer)
            TSeleccion = value
        End Set
    End Property

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        ExitTransf()
    End Sub
    Private Sub ExitTransf()
        Dim Rta As Object
        If Me.txtUbiacionOri.Visible = True Then
            Rta = MsgBox("Desea Cancelar la Transf. en curso y salir?", MsgBoxStyle.YesNo, FrmName)
            AsignaUsuario("")
        Else
            Rta = MsgBox("Desea salir de Transferencias?", MsgBoxStyle.YesNo, FrmName)
        End If
        If Rta = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Sub StartTransF()
        Me.blnTransCurso = True
        Me.lblCliente.Visible = True
        Me.cmbClientes.Visible = True
        Me.cmbClientes.Enabled = True
        Me.cmbClientes.Focus()
        Me.btnComenzarTareas.Enabled = False
    End Sub

    Private Sub CancelTransf()
        Me.txtUbiacionOri.Text = ""
        Me.txtUbiacionOri.ReadOnly = False
        Me.lblProducto.Text = ""
        Me.lblProducto.Visible = False
        Me.lblContendora.Text = ""
        Me.lblContendora.Visible = False
        Me.lblPallet.Text = ""
        Me.lblPallet.Visible = False
        Me.txtUbiacionOri.Enabled = True
        Me.txtContenedora.Text = ""
        Me.txtContenedora.Visible = False
        Me.txtContenedora.ReadOnly = False
        Me.lblUbicacionDest.Visible = False
        Me.txtUbicacionDest.Text = ""
        Me.txtUbicacionDest.Visible = False
        Me.txtUbicacionDest.ReadOnly = False
    End Sub
    Private Sub InicializarFrm()
        Try
            Me.Fin = False
            Me.btnComenzarTareas.Enabled = True
            Me.btnCerrarTransf.Enabled = False
            Me.btnSaltarTarea.Enabled = False
            Me.btnCompletados.Enabled = False
            Me.lblCliente.Visible = False
            Me.txtUbiacionOri.Enabled = True
            Me.txtUbicacionDest.Enabled = True
            Me.txtContenedora.Enabled = True
            Me.cmbClientes.Visible = False
            Me.cmbClientes.Enabled = True
            Me.lblCodViaje.Visible = False
            Me.CmbViajes.Visible = False
            Me.CmbViajes.Enabled = True
            CancelTransf()
        Catch ex As Exception
            MsgBox("InicializarFrm: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub frmTransPicking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = FrmName
        InicializarFrm()
        ObtenerClientes()
        If Me.TSeleccion = 1 Then
            Me.lblTipo.Text = "Transf. por contenedoras..."
            Me.lblContendora.Visible = True
            Me.lblPallet.Visible = False
        ElseIf Me.TSeleccion = 2 Then
            Me.lblTipo.Text = "Transf. por pallets..."
            Me.lblContendora.Visible = False
            Me.lblPallet.Visible = True
        End If

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
                    Me.cmbClientes.DropDownStyle = ComboBoxStyle.DropDownList
                    With cmbClientes
                        .DataSource = Nothing
                        .DataSource = dt
                        .DisplayMember = "RazonSocial"
                        .ValueMember = "Cliente_id"
                        .SelectedIndex = 0
                    End With
                Else
                    MsgBox("El usuario " + vUsr.CodUsuario + " no posee clientes asignados", MsgBoxStyle.OkOnly, FrmName)
                    Me.Close()
                    Exit Function
                End If
                If Me.cmbClientes.Items.Count = 1 Then
                    Me.cmbClientes.Enabled = False
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

    Private Function ObtenerViajes(ByVal Cliente As String) As Boolean
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
                xCmd.CommandText = "DBO.GET_VIAJES_BY_CLIENTE_TRANSFPICKING"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@Cliente", SqlDbType.VarChar, 30)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                xCmd.Connection = SQLc
                Da.Fill(Ds, "PEDIDOS")
                dt.Columns.Add("Viaje_ID", GetType(System.String))
                dt.Columns.Add("Descripcion", GetType(System.String))
                If Ds.Tables("PEDIDOS").Rows.Count > 0 Then
                    'Hay mas de un cliente, los cargo en el combo.
                    For Each drDSRow In Ds.Tables("PEDIDOS").Rows()
                        drNewRow = dt.NewRow()
                        drNewRow("Viaje_ID") = drDSRow("Viaje_ID")
                        drNewRow("Descripcion") = drDSRow("Viaje_ID")
                        dt.Rows.Add(drNewRow)
                    Next
                    Me.CmbViajes.DropDownStyle = ComboBoxStyle.DropDownList
                    With CmbViajes
                        .DataSource = Nothing
                        .DataSource = dt
                        .DisplayMember = "Viaje_ID"
                        .ValueMember = "Viaje_ID"
                        .SelectedIndex = 0
                    End With
                Else
                    MsgBox("El cliente " + Cliente + " no posee viajes con tareas pendientes", MsgBoxStyle.OkOnly, FrmName)
                    ObtenerViajes = False
                    Exit Function
                End If
                If Me.CmbViajes.Items.Count = 1 Then
                    Me.lblCodViaje.Visible = True
                    Me.CmbViajes.Visible = True
                    ActualizaTablaPicking()
                    ObtenerMovimientos()
                    If Not Fin Then
                        Me.CmbViajes.Enabled = False
                        Me.lblUbicacionOri.Visible = True
                        Me.lblUbicacionOri.Text = "Ubicacion Origen : " + Ubicacion_Origen
                        Me.txtUbiacionOri.Visible = True
                        Me.txtUbiacionOri.Focus()
                    Else
                        If hayCompletadas() Then
                            Me.CmbViajes.Visible = True
                            termino_tranfPrePicking()
                            Exit Function
                        End If
                        Me.lblCodViaje.Visible = False
                        Me.CmbViajes.Visible = False
                        Me.cmbClientes.Enabled = True
                        Me.cmdSalir.Focus()
                    End If
                End If
            Else : MsgBox(SQLError, MsgBoxStyle.Exclamation, FrmName)
            End If
            ObtenerViajes = True
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

    Private Sub txtUbicacionDest_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUbicacionDest.KeyUp
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            Select Case e.KeyCode
                Case 13
                    If Me.txtUbicacionDest.Text <> "" Then
                        If Me.txtUbicacionDest.Text.Trim.ToUpper <> Me.txtUbiacionOri.Text.Trim.ToUpper Then
                            If Not ExisteNavePosicion(Me.txtUbicacionDest.Text) Then
                                Me.txtUbicacionDest.Text = ""
                                Me.txtUbicacionDest.Focus()
                                Exit Sub
                            Else
                                If VerifyConnection(SQLc) Then
                                    'Realizo las Transferencias de la tabla 
                                    Cmd = SQLc.CreateCommand
                                    Cmd.CommandText = "TRANSFERENCIA_PREPICKING"
                                    Cmd.CommandType = CommandType.StoredProcedure
                                    Cmd.Connection = SQLc

                                    txtUbicacionDest.Text = UCase(txtUbicacionDest.Text)
                                    Pa = New SqlParameter("@UBICACION_DESTINO", SqlDbType.VarChar, 45)
                                    Pa.Value = txtUbicacionDest.Text
                                    Cmd.Parameters.Add(Pa)
                                    Pa = Nothing
                                    Pa = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 50)
                                    Pa.Value = Me.CmbViajes.SelectedValue
                                    Cmd.Parameters.Add(Pa)
                                    Pa = Nothing
                                    Pa = New SqlParameter("@TIPO", SqlDbType.Int)
                                    Pa.Value = Me.TSeleccion
                                    Cmd.Parameters.Add(Pa)
                                    Pa = Nothing
                                    Cmd.ExecuteNonQuery()
                                    Me.txtUbicacionDest.Visible = False
                                    Me.lblUbicacionDest.Visible = False
                                    Me.CmbViajes.Visible = False
                                    Me.lblCodViaje.Visible = False
                                    Me.CmbViajes.Enabled = True
                                    Me.cmbClientes.Enabled = True
                                    Me.cmbClientes.Focus()
                                    Me.btnComenzarTareas.Enabled = True
                                    Me.cmdSalir.Enabled = True
                                    MsgBox("La transferencia se realizo correctamente ", MsgBoxStyle.OkOnly, FrmName)
                                Else : MsgBox(SQLError, MsgBoxStyle.Exclamation, FrmName)
                                    Exit Sub
                                End If
                            End If
                        Else
                            MsgBox("La Ubicacion Destino es igual a la Ubicacion Origen ", MsgBoxStyle.OkOnly, FrmName)
                            Me.txtUbicacionDest.Text = ""
                            Me.txtUbicacionDest.Focus()
                        End If
                    End If
            End Select
        Catch SQLEX As SqlException
            MsgBox("Error SQL: " & SQLEX.Message, MsgBoxStyle.OkOnly, FrmName)
        Catch ex As Exception
            MsgBox("txtDestino_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Sub

    Private Sub txtUbiacionOri_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUbiacionOri.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtUbiacionOri.Text <> "" Then
                If Not ExisteNavePosicion(Me.txtUbiacionOri.Text) Then
                    Me.txtUbiacionOri.Text = ""
                    Me.txtUbiacionOri.Focus()
                    Exit Sub
                Else
                    If UCase(Me.txtUbiacionOri.Text) <> UCase(Me.Ubicacion_Origen) Then
                        MsgBox("La Ubicacion Origen es incorrecta ", MsgBoxStyle.OkOnly, FrmName)
                        Me.txtUbiacionOri.Text = ""
                        Me.txtUbiacionOri.Focus()
                        Exit Sub
                    End If
                End If
                Me.txtUbiacionOri.Text = UCase(Me.txtUbiacionOri.Text)
                Me.txtUbiacionOri.ReadOnly = True
                Me.lblProducto.Visible = True
                Me.lblProducto.Text = "Producto: " + Producto + " - " + ProdDesc
                If Me.TSeleccion = 1 Then
                    Me.lblContendora.Visible = True
                    Me.lblContendora.Text = "Transf. Contenedora: " + Contenedora
                ElseIf Me.TSeleccion = 2 Then
                    Me.lblPallet.Visible = True
                    Me.lblPallet.Text = "Pallet: " + Pallet
                End If
                Me.txtContenedora.Visible = True
                Me.txtContenedora.ReadOnly = False
                Me.txtContenedora.Focus()
            End If
            End If
    End Sub

    Private Sub txtContenedora_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtContenedora.KeyUp
        Dim Cmd As SqlCommand
        Dim Pa As New SqlParameter
        Dim Ubi_Ori_Ant As String
        Try
            If e.KeyValue = 13 Then
                If Me.txtContenedora.Text <> "" Then
                    If Me.TSeleccion = 1 Then
                        If UCase(Me.txtContenedora.Text) <> UCase(Contenedora) Then
                            MsgBox("La Contenedora es incorrecta ", MsgBoxStyle.OkOnly, FrmName)
                            Me.txtContenedora.Text = ""
                            Me.txtContenedora.Focus()
                            Exit Sub
                        End If
                        Me.txtContenedora.ReadOnly = True
                        Me.txtContenedora.Text = UCase(Me.txtContenedora.Text)
                    ElseIf Me.TSeleccion = 2 Then
                        If UCase(Me.txtContenedora.Text) <> UCase(Me.Pallet) Then
                            MsgBox("El pallet indicado no es correcto.", MsgBoxStyle.OkOnly, FrmName)
                            Me.txtContenedora.Text = ""
                            Me.txtContenedora.Focus()
                            Exit Sub
                        End If
                        Me.txtContenedora.ReadOnly = True
                    End If

                    Dim Rta As Object = MsgBox("¿Confirma la Transferencia?", MsgBoxStyle.YesNo, FrmName)

                    If Rta = vbYes Then
                        'Cargo la Transferencia en la tabla "MOVIMIENTOSPREPICKING"
                        If VerifyConnection(SQLc) Then
                            Cmd = SQLc.CreateCommand
                            Cmd.CommandText = "INSERT_MOVIMIENTOSPREPICKING"
                            Cmd.CommandType = CommandType.StoredProcedure
                            Cmd.Connection = SQLc

                            Pa = New SqlParameter("@PALLET", SqlDbType.VarChar, 100)
                            If Trim(Pallet) = "" Then
                                Pa.Value = DBNull.Value
                            Else
                                If Me.TSeleccion = 2 Then
                                    Pa.Value = Pallet
                                Else
                                    Pa.Value = DBNull.Value
                                End If
                            End If

                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@CONTENEDORA", SqlDbType.VarChar, 50)
                            If Me.TSeleccion = 1 Then
                                Pa.Value = IIf(Contenedora = "", DBNull.Value, Contenedora)
                            Else
                                Pa.Value = DBNull.Value
                            End If

                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing
                            Pa = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 50)
                            Pa.Value = Me.CmbViajes.SelectedValue
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing
                            Pa = New SqlParameter("@UBICACION_ORIGEN", SqlDbType.VarChar, 45)
                            Pa.Value = Ubicacion_Origen
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing
                            Pa = New SqlParameter("@DOCUMENTO_ID", SqlDbType.Int, 20)
                            Pa.Value = DBNull.Value
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing
                            Pa = New SqlParameter("@NRO_LINEA", SqlDbType.Int, 10)
                            Pa.Value = DBNull.Value
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing
                            Cmd.ExecuteNonQuery()
                            ObtenerMovimientos()
                        Else : MsgBox(SQLError, MsgBoxStyle.Exclamation, FrmName)
                            Exit Sub
                        End If
                        Else
                            'Salto de picking
                            salto_picking()
                        End If
                    Ubi_Ori_Ant = Me.txtUbiacionOri.Text
                    CancelTransf()
                    If Not Fin Then
                        Me.lblUbicacionOri.Visible = True
                        Me.txtUbiacionOri.Visible = True
                        If Ubi_Ori_Ant <> Ubicacion_Origen Then
                            Me.lblUbicacionOri.Text = "Ubicacion Origen : " + Ubicacion_Origen
                            Me.txtUbiacionOri.ReadOnly = False
                            Me.txtUbiacionOri.Focus()
                        Else
                            Me.txtUbiacionOri.Text = Ubi_Ori_Ant
                            Me.txtUbiacionOri.ReadOnly = True
                            Me.lblProducto.Visible = True
                            Me.lblProducto.Text = "Producto: " + Producto + " - " + ProdDesc
                            Me.lblContendora.Visible = True
                            Me.lblContendora.Text = "Transf. Contenedora: " + Contenedora
                            Me.lblPallet.Visible = True
                            Me.lblPallet.Text = "Pallet: " + Pallet
                            Me.txtContenedora.Visible = True
                            Me.txtContenedora.ReadOnly = False
                            Me.txtContenedora.Focus()
                        End If
                    Else
                        ' Aca termino las tareas pendientes

                        termino_tranfPrePicking()
                    End If
                End If
            End If
        Catch SqlEx As SqlException
            MsgBox("SQL. " & SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Sub

    Private Sub btnCerrarTransf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrarTransf.Click
        termino_tranfPrePicking()
    End Sub

    Private Sub btnSaltarTarea_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaltarTarea.Click
        salto_picking()
    End Sub

    Private Sub Completados()
        Dim F As New frmTansfPickingCompletadas
        Try
            If (Me.cmbClientes.SelectedValue <> "") And (Me.CmbViajes.SelectedValue <> "") And (Me.CmbViajes.Visible = True) Then
                F.pCliente = Me.cmbClientes.SelectedValue
                F.pViaje = Me.CmbViajes.SelectedValue
                F.ShowDialog()
            Else : MsgBox("Debe seleccionar cliente y pedido", MsgBoxStyle.Information, FrmName)
                If Me.cmbClientes.Enabled Then
                    Me.cmbClientes.Focus()
                    Exit Try
                End If
                If Me.CmbViajes.Enabled Then
                    Me.CmbViajes.Focus()
                    Exit Try
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            F = Nothing
        End Try
    End Sub

    Private Sub cmbClientes_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbClientes.KeyUp
        If e.KeyValue = 13 Then
            If (ObtenerViajes(Me.cmbClientes.SelectedValue) = True) Then
                If Not Fin Then
                    Me.btnSaltarTarea.Enabled = True
                    Me.btnCompletados.Enabled = True
                    Me.btnCerrarTransf.Enabled = True
                    Me.cmbClientes.Enabled = False
                    Me.lblCodViaje.Visible = True
                    Me.CmbViajes.Visible = True
                    Me.CmbViajes.Focus()
                Else
                    Fin = False
                    Me.btnCerrarTransf.Enabled = False
                End If
            Else
                Me.cmdSalir.Focus()
            End If
        End If
    End Sub

    Private Sub CmbViajes_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbViajes.KeyUp
        If e.KeyValue = 13 Then
            If CmbViajes.SelectedValue <> "" Then
                ActualizaTablaPicking()
                ObtenerMovimientos()

                If Not Fin Then
                    Me.CmbViajes.Enabled = False
                    Me.btnSaltarTarea.Enabled = True
                    Me.btnCompletados.Enabled = True
                    Me.lblUbicacionOri.Visible = True
                    Me.lblUbicacionOri.Text = "Ubicacion Origen : " + Ubicacion_Origen
                    Me.txtUbiacionOri.Visible = True
                    Me.txtUbiacionOri.Focus()
                Else
                    If hayCompletadas() Then
                        termino_tranfPrePicking()
                        Exit Sub
                    End If
                    Me.cmdSalir.Focus()
                    Me.InicializarFrm()
                End If
            End If
        End If
    End Sub
    Private Function ObtenerMovimientos() As Boolean
        Dim Da As SqlDataAdapter
        Dim Ds As New System.Data.DataSet
        Dim drDSRow As Data.DataRow
        Dim dt As New Data.DataTable
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.GET_MOVIMIENTOS_BY_VIAJE_TRANSFPICKING"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                xCmd.Connection = SQLc

                Pa = New SqlParameter("@Viaje_id", SqlDbType.VarChar, 30)
                Pa.Value = Me.CmbViajes.SelectedValue
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Tipo", SqlDbType.VarChar, 1)
                Pa.Value = Me.TSeleccion
                xCmd.Parameters.Add(Pa)

                Da.Fill(Ds, "MOVIMIENTOS")
                If Ds.Tables("MOVIMIENTOS").Rows.Count <> 0 Then
                    drDSRow = Ds.Tables("MOVIMIENTOS").Rows(0)
                    Pallet = drDSRow("PALLET").ToString
                    Producto = drDSRow("PRODUCTO_ID").ToString
                    ProdDesc = drDSRow("DESCRIPCION").ToString
                    Ubicacion_Origen = drDSRow("UBICACION_ORIGEN").ToString
                    Contenedora = drDSRow("CONTENEDORA").ToString
                    'Documento_id = drDSRow("DOCUMENTO_ID")
                    'Nro_Linea = drDSRow("NRO_LINEA")
                    AsignaUsuario(vUsr.CodUsuario)
                    Fin = False
                Else
                    Fin = True
                    MsgBox("No hay mas Tareas para este viaje", MsgBoxStyle.OkOnly, FrmName)
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

    Private Sub btnCompletados_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompletados.Click
        If Me.hayCompletadas() Then
            Completados()
        Else
            MsgBox("No hay tareas Completadas para este viaje ", MsgBoxStyle.OkOnly, FrmName)
        End If
        buscafoco()
    End Sub

    Private Sub btnComenzarTareas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComenzarTareas.Click
        StartTransF()
    End Sub
    Private Sub salto_picking()
        Dim Cmd As SqlCommand
        Dim Pa As New SqlParameter
        Dim Ubi_Ori_Ant As String
        Try
            If Me.txtUbiacionOri.Visible = True Then
                If VerifyConnection(SQLc) Then
                    Cmd = SQLc.CreateCommand
                    Cmd.CommandText = "SALTO_TRANFPREPIKING"
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Connection = SQLc

                    Pa = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 50)
                    Pa.Value = Me.CmbViajes.SelectedValue
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@PALLET", SqlDbType.Int, 100)
                    If Trim(Pallet) = "" Then
                        Pa.Value = DBNull.Value
                    Else
                        Pa.Value = Pallet
                    End If
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@CONTENEDORA", SqlDbType.Int, 100)
                    Pa.Value = IIf(Contenedora = "", DBNull.Value, Contenedora)
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Cmd.ExecuteNonQuery()
                    AsignaUsuario("")
                Else : MsgBox(SQLError, MsgBoxStyle.Exclamation, FrmName)
                    Exit Sub
                End If
                ObtenerMovimientos()
                Ubi_Ori_Ant = Me.lblUbicacionOri.Text
                CancelTransf()
                Me.lblUbicacionOri.Visible = True
                Me.txtUbiacionOri.Visible = True
                If Ubi_Ori_Ant <> Ubicacion_Origen Then
                    Me.lblUbicacionOri.Text = "Ubicacion Origen : " + Ubicacion_Origen
                    Me.txtUbiacionOri.ReadOnly = False
                    Me.txtUbiacionOri.Focus()
                Else
                    Me.txtUbiacionOri.ReadOnly = True
                    Me.lblProducto.Visible = True
                    Me.lblProducto.Text = "Producto: " + Producto + " - " + ProdDesc
                    Me.lblContendora.Text = "Transf. Contenedora: " + Contenedora
                    Me.lblPallet.Text = "Pallet: " + Pallet
                    If Me.TSeleccion = 1 Then
                        Me.lblContendora.Visible = True
                        Me.lblPallet.Visible = False
                    ElseIf Me.TSeleccion = 2 Then
                        Me.lblPallet.Visible = True
                        Me.lblContendora.Visible = False
                    End If
                    Me.txtContenedora.Visible = True
                    Me.txtContenedora.ReadOnly = False
                    Me.txtContenedora.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Sub
    Private Sub termino_tranfPrePicking()
        Try
            If Me.CmbViajes.Visible Then
                If Me.hayCompletadas Then
                    Dim Rta As Object = MsgBox("¿Desea Completar las transferencias?", MsgBoxStyle.YesNo, FrmName)

                    If Rta = vbYes Then
                        Fin = True
                        AsignaUsuario("")
                        Me.btnComenzarTareas.Enabled = False
                        Me.btnCerrarTransf.Enabled = False
                        Me.btnCompletados.Enabled = False
                        Me.btnSaltarTarea.Enabled = False
                        Me.lblUbicacionDest.Visible = True
                        Me.lblUbicacionOri.Visible = False
                        Me.txtUbiacionOri.Visible = False
                        Me.txtUbicacionDest.Visible = True
                        Me.cmdSalir.Enabled = False
                        Me.txtUbicacionDest.Text = ""
                        Me.txtUbicacionDest.Focus()
                    Else
                        'Rta = vbNo
                        Rta = MsgBox("¿Desea Anular las transferencias?", MsgBoxStyle.YesNo, FrmName)
                        If Rta = vbYes Then
                            AnulaTransferencias()
                        End If
                        Me.CancelTransf()
                        If Fin Then
                            Me.lblUbicacionOri.Visible = False
                            Me.txtUbiacionOri.Visible = False
                            Me.btnSaltarTarea.Enabled = False
                            Me.lblCliente.Visible = True
                            Me.CmbViajes.Visible = False
                            Me.CmbViajes.Enabled = True
                            Me.lblCodViaje.Visible = False
                            Me.cmbClientes.Visible = True
                            Me.cmbClientes.Enabled = True
                            Me.cmbClientes.Focus()
                        Else
                            buscafoco()
                        End If
                    End If
                Else
                    MsgBox("No hay tareas Completadas para este viaje ", MsgBoxStyle.OkOnly, FrmName)
                    buscafoco()
                End If
            Else
                MsgBox("Debe asignar un viaje ", MsgBoxStyle.OkOnly, FrmName)
                Me.cmbClientes.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        End Try
    End Sub

    Private Function hayCompletadas() As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = "TRANF_PREPICKING_COMPLETADAS"

                Pa = New SqlParameter("@Viaje_ID", SqlDbType.VarChar, 50)
                Pa.Value = Me.CmbViajes.SelectedValue
                xCmd.Parameters.Add(Pa)

                Da.Fill(Ds, "MOVIMIENTOS")

                If Ds.Tables("MOVIMIENTOS").Rows.Count > 0 Then
                    hayCompletadas = True
                    Exit Function
                Else
                    hayCompletadas = False
                    Exit Function
                End If

            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
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

    Private Function AsignaUsuario(ByVal usuario As String) As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then

                ' Asigno el usuario en la tabla de picking
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "SET_USUARIO_TRANSF_PREPICKING"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 30)
                Pa.Value = usuario
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@DOCUMENTO_ID", SqlDbType.Int, 20)
                Pa.Value = Documento_id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@NRO_LINEA", SqlDbType.Int, 10)
                Pa.Value = Nro_Linea
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                xCmd.Connection = SQLc
                xCmd.ExecuteNonQuery()
            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
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
    Private Sub buscafoco()
        If Me.cmbClientes.Enabled Then
            Me.cmbClientes.Focus()
        End If
        If Me.CmbViajes.Visible And Me.CmbViajes.Enabled Then
            Me.CmbViajes.Focus()
        End If
        If Me.txtUbiacionOri.Visible Then
            Me.txtUbiacionOri.Focus()
        End If
        If Me.txtContenedora.Visible Then
            Me.txtContenedora.Focus()
        End If
    End Sub

    Private Sub frmTransfPicking_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case 112 'Keys.F1
                If Me.btnComenzarTareas.Enabled Then
                    StartTransF()
                End If
            Case 113 'Keys.F2
                If Me.btnCerrarTransf.Enabled Then
                    termino_tranfPrePicking()
                End If
            Case 114 'Keys.F3
                If Me.btnCompletados.Enabled Then
                    If Me.hayCompletadas() Then
                        Completados()
                    Else
                        MsgBox("No hay tareas Completadas para este viaje ", MsgBoxStyle.OkOnly, FrmName)
                    End If
                End If
                buscafoco()
            Case 115 'Keys.F4
                If Me.btnSaltarTarea.Enabled Then
                    salto_picking()
                End If
            Case 116 'Keys.F5
                ExitTransf()
        End Select
    End Sub
    Private Function AnulaTransferencias() As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then

                ' Asigno el usuario en la tabla de picking
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "ANULA_TRANSF_PREPICKING"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 30)
                Pa.Value = Me.CmbViajes.SelectedValue
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                xCmd.Connection = SQLc
                xCmd.ExecuteNonQuery()
            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
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
    Private Function ActualizaTablaPicking() As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then

                ' Asigno el usuario en la tabla de picking
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "ACTUALIZA_PICKING_TRANSF_PREPICKING"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 30)
                Pa.Value = Me.CmbViajes.SelectedValue
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                xCmd.Connection = SQLc
                xCmd.ExecuteNonQuery()
            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
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

    Private Sub txtContenedora_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CmbViajes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbViajes.SelectedIndexChanged

    End Sub

    Private Sub cmbClientes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClientes.SelectedIndexChanged

    End Sub

    Private Sub txtUbicacionDest_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUbicacionDest.TextChanged

    End Sub

    Private Sub txtContenedora_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContenedora.TextChanged

    End Sub

    Private Sub txtUbiacionOri_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUbiacionOri.TextChanged

    End Sub
End Class

