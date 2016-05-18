Imports System.Data.SqlClient
Imports System.Data

Public Class frmAPF

#Region "Declaraciones"
    '------------------------------------------------------------------------------------
    'Constantes.
    '------------------------------------------------------------------------------------
    Private Const FrmName As String = "Armado Pallet Final"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    '------------------------------------------------------------------------------------
    'Estructuras
    '------------------------------------------------------------------------------------
    Private Structure UltDatos
        Dim Clientes As String
    End Structure
    '------------------------------------------------------------------------------------
    'Variables.
    '------------------------------------------------------------------------------------
    Private vDatos As UltDatos
    Private vTipoAPF As Boolean = False
    Private vInicializando As Boolean = True
    Private Pallet As Long = 0

#End Region

#Region "A la Base de datos."

    Private Function ImpresionPallet(ByVal Tipo As String) As Boolean
        Dim xSQL As String
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandType = Data.CommandType.Text
                'LRojas Tracker ID 3806 05/03/2012: Inserción de Usuario para Demonio de Impresion
                xSQL = "insert into IMPRESION_APF values(" & Pallet & ",'" & Tipo & "','0','" & Me.cmbCodigoViaje.SelectedValue & "', '" & vUsr.CodUsuario & "')"
                Cmd.CommandText = xSQL
                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            Cmd = Nothing
        End Try
    End Function

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
                xCmd.CommandText = "DBO.Get_Viajes_by_Cliente"
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
                    Me.cmbCodigoViaje.DropDownStyle = ComboBoxStyle.DropDownList
                    With cmbCodigoViaje
                        .DataSource = Nothing
                        .DataSource = dt
                        .DisplayMember = "Viaje_ID"
                        .ValueMember = "Viaje_ID"
                        .SelectedIndex = 0
                    End With
                End If
                If Me.cmbCodigoViaje.Items.Count = 1 Then
                    Me.cmbCodigoViaje.Enabled = False
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

    Public Function GetNumberofPallet(ByRef Pallet_id As Long) As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
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
                MsgBox(SQLError, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("SQL GetPallet: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("GetPallet: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Pa = Nothing
        End Try
    End Function

    Private Function ValidarIngreso() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Dim Producto As String = ""
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.APF_Val_Prod"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@Cliente", Data.SqlDbType.VarChar, 50)
                Pa.Value = Me.cmbClientes.SelectedValue
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Viaje_Id", SqlDbType.VarChar, 30)
                Pa.Value = Me.cmbCodigoViaje.SelectedValue
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

    Private Function ConfirmacionNormal() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.MOB_AFP"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@Cliente", Data.SqlDbType.VarChar, 50)
                Pa.Value = Me.cmbClientes.SelectedValue
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Producto", Data.SqlDbType.VarChar, 30)
                Pa.Value = Me.txtProducto.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Viaje_id", Data.SqlDbType.VarChar, 50)
                Pa.Value = Me.cmbCodigoViaje.SelectedValue
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Tipo", Data.SqlDbType.VarChar, 50)
                Pa.Value = "0"
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PF", Data.SqlDbType.VarChar, 50)
                Pa.Value = Me.Pallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Usuario", Data.SqlDbType.VarChar, 50)
                Pa.Value = Trim(UCase(vUsr.CodUsuario))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("SQL ConfirmacionNormal: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("ConfirmacionNormal: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function

    Private Function CerrarPFinal(ByVal Pallet As Long) As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "CERRAR_PALLET_FINAL"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()
                Pa = New SqlParameter("@PALLET", SqlDbType.BigInt)
                Pa.Value = Pallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("SQL CerrarPFinal: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("CerrarPFinal: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Pa = Nothing
        End Try
    End Function

    Private Function ConfirmacionPorCama() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.MOB_AFP"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@Cliente", Data.SqlDbType.VarChar, 50)
                Pa.Value = Me.cmbClientes.SelectedValue
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Producto", Data.SqlDbType.VarChar, 30)
                Pa.Value = Me.txtProducto.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Viaje_Id", Data.SqlDbType.VarChar, 50)
                Pa.Value = Me.cmbCodigoViaje.SelectedValue
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Tipo", Data.SqlDbType.VarChar, 50)
                Pa.Value = "1"
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PF", Data.SqlDbType.VarChar, 50)
                Pa.Value = Me.Pallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Usuario", Data.SqlDbType.VarChar, 50)
                Pa.Value = Trim(UCase(vUsr.CodUsuario))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@TOTAL", SqlDbType.BigInt)
                Pa.Value = Me.txtCantidad.Text
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("SQL ConfirmacionPorCama: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("ConfirmacionPorCama: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function

#End Region

#Region "Eventos y Funciones"

    Private Sub frmAPF_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case 112 'Keys.F1
                SetUltCliente()
            Case 113 'Keys.F2

            Case 114 'Keys.F3
                SetTipoPallet()
            Case 115 'Keys.F4
                newPallet()
            Case 116 'Keys.F5
                CerrarPallet()
            Case 117 'Keys.F6
                AbrirPallet()
            Case 118 'Keys.F7   
                PalletStandBy()
            Case 119  'F8
                Cancelar()
            Case 120 'F9
                pendientes()
            Case 121 'F10
                Me.Close()
        End Select
    End Sub

    Private Sub pendientes()
        Dim F As New FrmAPFPendientes
        Try
            If (Me.cmbClientes.SelectedValue <> "") And (Me.cmbCodigoViaje.SelectedValue <> "") And (Me.cmbCodigoViaje.Visible = True) Then
                F.pCliente = Me.cmbClientes.SelectedValue
                F.pViaje = Me.cmbCodigoViaje.SelectedValue
                F.ShowDialog()
            Else : MsgBox("Debe seleccionar cliente y pedido", MsgBoxStyle.Information, FrmName)
                If Me.cmbClientes.Enabled Then
                    Me.cmbClientes.Focus()
                    Exit Try
                End If
                If Me.cmbCodigoViaje.Enabled Then
                    Me.cmbCodigoViaje.Focus()
                    Exit Try
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            F = Nothing
        End Try
    End Sub
    Private Sub frmAPF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IniForm()
    End Sub

    Private Sub IniForm()
        Try
            ObtenerClientes()
            Me.cmdNewPallet.Enabled = True
            Me.cmdAbrir.Enabled = True

            Me.cmbCodigoViaje.DataSource = Nothing
            Me.cmbCodigoViaje.Visible = False
            Me.lblCliente.Visible = True
            Me.lblPallet.Visible = True
            Me.cmbClientes.Enabled = True
            Me.cmbCodigoViaje.Enabled = True
            Me.cmbClientes.Visible = True
            Pallet = 0
            Me.txtQTYCama.Enabled = True
            Me.txtQtyLineasCama.Enabled = True
            Me.txtQtyBultosSuelto.Enabled = True
            Me.txtCantidad.Enabled = True
            Me.lblConfirmacion.Enabled = True
            Me.lblConfirmacion.Text = "¿Confirma Cantidad?" & vbNewLine & "1=Si, 0=No"
            vInicializando = False
            Me.CmdUltCliente.Visible = False
            Me.cmdPalletxCama.Visible = False
            Me.txtProducto.Visible = False
            Me.txtProducto.Text = ""
            Me.txtProducto.ReadOnly = False
            Me.lblQTYCama.Visible = False
            Me.txtQTYCama.Visible = False
            Me.txtQTYCama.Text = ""
            Me.lblQtyLineasCama.Visible = False
            Me.txtQtyLineasCama.Visible = False
            Me.txtQtyLineasCama.Text = ""
            Me.lblQtyBultosSuelto.Visible = False
            Me.txtQtyBultosSuelto.Visible = False
            Me.txtQtyBultosSuelto.Text = ""
            Me.lblCantidad.Visible = False
            Me.txtCantidad.Visible = False
            Me.txtCantidad.Text = ""
            Me.lblConfirmacion.Visible = False
            Me.TxtConfirmacion.Visible = False
            Me.TxtConfirmacion.Text = ""
            Me.lblPallet.Text = ""
            Me.vTipoAPF = False
            Me.cmdPalletxCama.Text = "F3) Pallet por Producto."
            Me.cmdPalletxCama.BackColor = Color.LightGray
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub RefreshForm()
        Try
            Me.txtQTYCama.Enabled = True
            Me.txtQtyLineasCama.Enabled = True
            Me.txtQtyBultosSuelto.Enabled = True
            Me.txtCantidad.Enabled = True
            Me.lblConfirmacion.Enabled = True
            Me.lblConfirmacion.Text = "¿Confirma Cantidad?" & vbNewLine & "1=Si, 0=No"
            Me.txtProducto.Enabled = True
            Me.lblQTYCama.Visible = False
            Me.txtQTYCama.Visible = False
            Me.txtQTYCama.Text = ""
            Me.lblQtyLineasCama.Visible = False
            Me.txtQtyLineasCama.Visible = False
            Me.txtQtyLineasCama.Text = ""
            Me.lblQtyBultosSuelto.Visible = False
            Me.txtQtyBultosSuelto.Visible = False
            Me.txtQtyBultosSuelto.Text = ""
            Me.lblCantidad.Visible = False
            Me.txtCantidad.Visible = False
            Me.txtCantidad.Text = ""
            Me.lblConfirmacion.Visible = False
            Me.TxtConfirmacion.Visible = False
            Me.TxtConfirmacion.Text = ""
            Me.vTipoAPF = False
            Me.cmdPalletxCama.Text = "F3) Pallet por Producto."
            Me.cmdPalletxCama.BackColor = Color.LightGray
            Me.txtProducto.Text = ""
            Me.txtProducto.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub cmdPalletxCama_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPalletxCama.Click
        SetTipoPallet()
    End Sub

    Private Sub SetTipoPallet()
        Me.vTipoAPF = Not Me.vTipoAPF
        If Me.vTipoAPF = False Then
            Me.cmdPalletxCama.Text = "F3) Pallet por Producto."
            Me.cmdPalletxCama.BackColor = Color.LightGray
        Else
            Me.cmdPalletxCama.Text = "F3) Pallet por Cama."
            Me.cmdPalletxCama.BackColor = Color.Red
        End If
        Me.txtProducto.Focus()
    End Sub

    Private Sub cmbClientes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbClientes.KeyUp
        If e.KeyValue = 13 Then
            ObtenerViajes(Me.cmbClientes.SelectedValue)

            Me.CmdUltCliente.Visible = True
            Me.cmbCodigoViaje.Visible = True
            If Me.cmbCodigoViaje.Items.Count = 0 Then
                msgbox("No quedan pedidos pendientes para controlar del cliente " & me.cmbClientes.Text ,MsgBoxStyle.Information,frmname)
                IniForm()
                Me.cmbClientes.Focus()
                Exit Sub
            End If
            vDatos.Clientes = Me.cmbClientes.SelectedValue
            Me.cmbClientes.Enabled = False
            Me.cmbCodigoViaje.Focus()
        End If
    End Sub

    Private Sub cmbSucursal_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCodigoViaje.KeyUp
        If e.KeyValue = 13 Then
            Me.cmbCodigoViaje.Enabled = False
        End If
    End Sub

    Private Sub txtProducto_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProducto.GotFocus
        If txtProducto.Enabled = False Then
            txtProducto.Enabled = True
            Me.txtProducto.Focus()
        End If
    End Sub

    Private Sub txtProducto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProducto.KeyUp
        If (e.KeyValue = 13) And Trim(Me.txtProducto.Text) <> "" Then
            If ValidarIngreso() = True Then
                If vTipoAPF Then
                    Me.txtProducto.Enabled = False
                    Me.lblQTYCama.Visible = True
                    Me.txtQTYCama.Visible = True
                    Me.txtQTYCama.Focus()
                Else
                    If ConfirmacionNormal() Then
                        Me.txtProducto.Text = ""
                        Me.txtProducto.Focus()
                    Else : Me.txtProducto.Text = ""
                        Me.txtProducto.Focus()
                    End If
                End If
            Else : Me.txtProducto.Text = ""
                Me.txtProducto.Focus()
            End If
        End If
    End Sub

    Private Sub cmdNewPallet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewPallet.Click
        newPallet()
    End Sub

    Private Sub Cancelar()
        If MsgBox("Desea cancelar la operacion en curso?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
            IniForm()
            Me.CmdUltCliente.Visible = True
            Me.cmbClientes.Focus()
        End If
    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Cancelar()
    End Sub

    Private Sub ValidarProdCod()
        ValidarIngreso()
    End Sub

    Private Sub cmbClientes_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbClientes.LostFocus
        '
        'Try
        '    ObtenerViajes(Me.cmbClientes.SelectedValue)
        '    vDatos.Clientes = Me.cmbClientes.SelectedValue
        '    Me.cmbClientes.Enabled = False
        '    Me.cmbCodigoViaje.Visible = True
        '    Me.cmbCodigoViaje.Enabled = True
        '    Me.cmbCodigoViaje.Focus()
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        'End Try
    End Sub

    Private Sub cmbSucursal_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCodigoViaje.LostFocus
        'Try
        '    Me.cmbCodigoViaje.Enabled = False
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        'End Try
    End Sub

    Private Sub CerrarPallet()
        If Me.Pallet <> 0 Then
            If CerrarPFinal(Pallet) Then
                ImpresionPallet(1)
                IniForm()
                Me.CmdUltCliente.Visible = True
                Me.CmdUltCliente.Focus()
                Me.cmbClientes.Focus()
            End If
        End If
    End Sub

    Private Sub cmdClosePallet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClosePallet.Click
        CerrarPallet()
    End Sub

    Private Sub CmdUltCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdUltCliente.Click
        SetUltCliente()
    End Sub

    Private Sub SetUltCliente()
        Try
            If vDatos.Clientes <> "" Then
                ObtenerViajes(Me.cmbClientes.SelectedValue)
                Me.cmbClientes.SelectedValue = vDatos.Clientes
                Me.cmbClientes.Enabled = False

                Me.cmbCodigoViaje.Visible = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub newPallet()
        Try
            If (Me.cmbCodigoViaje.SelectedValue <> "") And (Me.cmbClientes.SelectedValue <> "") Then
                If GetNumberofPallet(Pallet) Then
                    Me.cmdNewPallet.Enabled = False
                    Me.cmdAbrir.Enabled = False
                    Me.cmdPalletxCama.Visible = True
                    Me.lblPallet.Text = "Pallet: " & Pallet
                    Me.txtProducto.Text = ""
                    Me.txtProducto.Visible = True
                    Me.txtProducto.Focus()
                End If
            Else
                MsgBox("Debe seleccionar cliente y pedido", MsgBoxStyle.Information, FrmName)
                If Me.cmbClientes.Enabled Then
                    Me.cmbClientes.Focus()
                    Exit Try
                End If
                If Me.cmbCodigoViaje.Enabled Then
                    Me.cmbCodigoViaje.Focus()
                    Exit Try
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub cmdPalletStBy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPalletStBy.Click
        PalletStandBy()
    End Sub

    Private Sub PalletStandBy()
        If Me.Pallet <> 0 Then
            ImpresionPallet(0)
            IniForm()
            Me.CmdUltCliente.Visible = True
        End If
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Me.Close()
    End Sub

    Private Sub cmdAbrir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAbrir.Click
        AbrirPallet()
    End Sub

    Private Sub AbrirPallet()
        Dim Frm As FrmAPF_Abiertos
        Try
            If (Me.cmbClientes.SelectedValue <> "") And (Me.cmbCodigoViaje.SelectedValue <> "") Then
                Frm = New FrmAPF_Abiertos
                Frm.pCliente = Me.cmbClientes.SelectedValue
                Frm.pCodigoViaje = Me.cmbCodigoViaje.SelectedValue
                Frm.ShowDialog()
                If Frm.GetPallet <> 0 Then
                    Me.vTipoAPF = False
                    Me.cmdPalletxCama.Text = "F3) Pallet por Producto."
                    Me.cmdPalletxCama.BackColor = Color.LightGray
                    Me.cmdPalletxCama.Visible = True
                    Me.Pallet = Frm.GetPallet
                    Me.lblPallet.Text = "Pallet: " & Pallet
                    Me.cmdNewPallet.Enabled = False
                    Me.cmdAbrir.Enabled = False
                    Me.txtProducto.Text = ""
                    Me.txtProducto.Visible = True
                    Me.txtProducto.Focus()
                End If
            Else
                MsgBox("Debe seleccionar cliente y pedido", MsgBoxStyle.Information, FrmName)
                If Me.cmbClientes.Enabled Then
                    Me.cmbClientes.Focus()
                    Exit Try
                End If
                If Me.cmbCodigoViaje.Enabled Then
                    Me.cmbCodigoViaje.Focus()
                    Exit Try
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            Frm = Nothing
        End Try
    End Sub

    Private Sub txtQTYCama_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQTYCama.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub txtQTYCama_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQTYCama.KeyUp
        If (e.KeyValue = 13) And (Val(Me.txtQTYCama.Text) > 0) Then
            Me.lblQtyLineasCama.Visible = True
            Me.txtQtyLineasCama.Visible = True
            Me.txtQTYCama.Enabled = False
        End If
    End Sub

    Private Sub txtQtyLineasCama_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQtyLineasCama.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub txtQtyLineasCama_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQtyLineasCama.KeyUp
        If (e.KeyValue = 13) And (Val(txtQtyLineasCama.Text) > 0) Then
            Me.txtQtyLineasCama.Enabled = False
            Me.lblQtyBultosSuelto.Visible = True
            Me.txtQtyBultosSuelto.Visible = True
            Me.txtQtyBultosSuelto.Focus()
        End If
    End Sub

    Private Sub txtQtyBultosSuelto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQtyBultosSuelto.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub txtQtyBultosSuelto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQtyBultosSuelto.KeyUp
        If e.KeyValue = 13 Then
            Me.txtCantidad.Visible = True
            Me.lblCantidad.Visible = True
            Me.txtQtyBultosSuelto.Enabled = False
            Me.txtCantidad.Text = (Me.txtQTYCama.Text * Me.txtQtyLineasCama.Text) + Val(Me.txtQtyBultosSuelto.Text)
            Me.txtCantidad.Enabled = False
            Me.TxtConfirmacion.Visible = True
            Me.lblConfirmacion.Visible = True
            Me.TxtConfirmacion.Focus()
        End If
    End Sub

    Private Sub TxtConfirmacion_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtConfirmacion.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub TxtConfirmacion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtConfirmacion.KeyUp
        If (e.KeyValue = 13) And (Trim(Me.TxtConfirmacion.Text) <> "") Then
            If (Me.TxtConfirmacion.Text = 0) Then
                Me.txtCantidad.Enabled = True
                Me.txtCantidad.Text = ""
                Me.txtCantidad.Focus()
            Else
                If ConfirmacionPorCama() Then
                    RefreshForm()
                Else
                    Me.TxtConfirmacion.Text = ""
                    Me.TxtConfirmacion.Focus()
                End If
            End If
        Else : Me.TxtConfirmacion.Focus()
        End If
    End Sub

    Private Sub txtCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub txtCantidad_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyUp
        If (e.KeyValue = 13) And (Val(Me.txtCantidad.Text) > 0) Then
            Me.txtCantidad.Enabled = False
            Me.TxtConfirmacion.Text = ""
            Me.TxtConfirmacion.Focus()
        End If
    End Sub

    Private Sub cmdPendientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPendientes.Click
        pendientes()
    End Sub

#End Region

    Private Sub cmbCodigoViaje_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCodigoViaje.SelectedIndexChanged

    End Sub

    Private Sub txtProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProducto.TextChanged

    End Sub
End Class
