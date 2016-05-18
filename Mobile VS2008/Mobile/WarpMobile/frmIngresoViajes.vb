Imports System.Data.SqlClient
Imports System.Data

Public Class frmIngresoViajes

    Private vMenu As String = "F1) Agregar pallet." & vbTab & "F2) Cancelar viaje." & vbNewLine & "F3) Pallet ingresados." & vbTab & "F4) Pallet faltantes." & vbNewLine & "F5) Finalizar." & vbTab & "F6)Control x Pedido "
    
    Private Const FrmName As String = "Control Expedicion."
    Private sPalletNoEncontrado As String = "El pallet no pertenece al Nro. de Viaje o Pedido: "
    Private sElviajeNoExiste As String = "No existe el Nro. de viaje: "
    Private sElpedidoNoExiste As String = "No existe el Nro. de pedido: "
    Private sPalletAgregado As String = "Pallet ingresado."
    Private sViajeCompleto As String = "El viaje se encuentra completo."
    Private strBuscando As String = "Buscando..."
    Private sPalletYaIngresado As String = "El pallet ya fue ingresado."
    Private sNohayViajeActivo As String = "No existe ningún viaje activo."
    Private sNohayPedidoActivo As String = "No existe ningún pedido activo."
    Private sIngreseTodo As String = "Debe ingresar todos los campos."
    Private Const SQLConErr As String = "No se pudo conectar a la base de datos."
    Dim sViajeIngresado As String = "Debe confirmar el viaje."
    Private sIngresarPallet As String = "Debe ingresar un Pallet"
    Dim sViajeIngresadoPallet As String = "No puede ingresar el pallet porque no confirmo la seleccion del viaje"
    Dim Cmd As SqlCommand
    Dim Da As SqlDataAdapter
    Public Ds As Data.DataSet
    Public DsSeleccionados As Data.DataSet
    Dim oDataTable As New DataTable
    Dim bExisteViaje As Boolean = False
    Dim bCanceloViaje As Boolean = False
    Dim bCanceloViajePallet As Boolean = False
    Dim Cliente_ID As String

    Private Sub CreateDataset()
        Try
            DsSeleccionados = New Data.DataSet
            'DsSeleccionados.Tables.Add("Clone")
            'DsSeleccionados.Tables("Clone").Columns.Add("NRO_PALLET", Type.GetType("System.String"))

            If txtNroViaje.Text.Trim <> "" And Me.lblNroViaje.Text = "Nro. de viaje:" Then
                GetValues(Me.txtNroViaje.Text, DsSeleccionados)
            ElseIf txtNroViaje.Text.Trim <> "" And Me.lblNroViaje.Text = "Nro. de pedido:" Then
                GetValues_Pedidos(Me.txtNroViaje.Text, DsSeleccionados)
            End If
        Catch ex As Exception
            MsgBox("CreateDataset: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub
    Private Sub F3()
        If Me.txtNroViaje.Text <> "" Then
            lblMsg.Text = ""
            Dim frmGestion As New frmGestionPalletViaje
            frmGestion.TipoConsulta = TipoForm.GestionPallet.Ingresados
            frmGestion.DataSet = DsSeleccionados
            If Me.lblNroViaje.Text = "Nro. de viaje:" Then
                frmGestion.ViajeId = Me.txtNroViaje.Text
            Else
                frmGestion.PedidoId = Me.txtNroViaje.Text
            End If
            frmGestion.ShowDialog()
            frmGestion = Nothing
            If Me.txtNroPallet.Visible = True Then
                Me.txtNroPallet.Focus()
            Else
                Me.txtNroViaje.Focus()
            End If
        Else
            Me.txtNroViaje.Focus()
        End If
    End Sub
    Private Sub F6()
        If Me.lblNroViaje.Text = "Nro. de viaje:" Then
            lblNroViaje.Text = "Nro. de pedido:"
            Me.cmdCambiar.Text = "F6)Control x Viaje"
            Me.txtNroViaje.Focus()
        Else
            lblNroViaje.Text = "Nro. de viaje:"
            Me.cmdCambiar.Text = "F6)Control x Pedido"
            Me.txtNroViaje.Focus()
        End If

    End Sub
    Private Sub F4()
        If Me.txtNroViaje.Text <> "" Then
            lblMsg.Text = ""
            Dim frmGestion As New frmGestionPalletViaje
            frmGestion.TipoConsulta = TipoForm.GestionPallet.Faltantes
            frmGestion.DataSet = Ds
            If Me.lblNroViaje.Text = "Nro. de viaje:" Then
                frmGestion.ViajeId = Me.txtNroViaje.Text
            Else
                frmGestion.PedidoId = Me.txtNroViaje.Text
            End If
            frmGestion.ShowDialog()
            frmGestion = Nothing
            If Me.txtNroPallet.Visible = True Then
                Me.txtNroPallet.Focus()
            Else
                Me.txtNroViaje.Focus()
            End If
        Else
            Me.txtNroViaje.Focus()
        End If
    End Sub

    Private Sub F2()
        Dim controlx As String


        If Me.lblNroViaje.Text = "Nro. de viaje:" Then
            controlx = "viaje"
        Else
            controlx = "pedido"
        End If

        If txtNroViaje.ReadOnly = False Then
            'no tiene ningun viaje activo
            txtNroViaje.Text = ""
            txtNroPallet.Text = ""
            txtNroViaje.Focus()
            lblMsg.Text = sNohayViajeActivo
            Exit Sub
        End If
        If MsgBox("Va a cancelar el " & controlx & "." & vbNewLine & "¿Confirma dicha operación?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'If Not SetStatus(Me.txtNroViaje.Text, 0) Then
            txtNroViaje.ReadOnly = False
            txtNroViaje.Text = ""
            txtNroPallet.Text = ""
            Ds = Nothing
            DsSeleccionados = Nothing
            lblMsg.Text = ""
            bExisteViaje = False
            bCanceloViaje = True
            bCanceloViajePallet = False
            txtNroViaje.Focus()
            Inicializarfrm()
            'End If
        Else
            If Me.txtNroPallet.Visible = True Then
                Me.txtNroPallet.Focus()
            Else
                Me.txtNroViaje.Focus()
            End If
            bCanceloViajePallet = True
            bCanceloViaje = False
        End If


    End Sub

    Private Sub frmIngresoViajes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Try
            Select Case e.KeyCode
                Case Keys.F1
                    AddPallet(txtNroViaje)
                Case Keys.F3
                    F3()
                Case Keys.F4
                    F4()
                Case Keys.F2
                    F2()
                Case Keys.F5
                    frmPrincipal.Show()
                    Me.Close()
                Case Keys.F6
                    F6()
            End Select
        Catch ex As Exception
            MsgBox("frmIngresoViajes_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Sub

    Private Sub frmIngresoViajes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Inicializarfrm()
        lblMsg.Text = ""
        lblMenu.Text = vMenu
        Me.Text = FrmName
    End Sub

    Private Sub txtNroViaje_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNroViaje.KeyUp
        Try
            If e.KeyCode = 13 Then
                Me.lblMsg.Text = ""
                If bCanceloViaje = True Then
                    bCanceloViaje = False
                    Exit Sub
                End If
                'acá debo buscar los pallet y cargar el dataset
                If txtNroViaje.Text.Trim <> "" And Me.lblNroViaje.Text = "Nro. de viaje:" Then
                    If VerifyConnection(SQLc) Then
                        Ds = New Data.DataSet
                        lblMsg.Text = strBuscando
                        lblMsg.Refresh()
                        Cmd = SQLc.CreateCommand
                        Da = New SqlDataAdapter(Cmd)
                        'Catalina Castillo.Tracker 4717. Se aumento el tamaño de la variable codigo a 100
                        Cmd.Parameters.Add("@Codigo", Data.SqlDbType.NVarChar, 100).Value = txtNroViaje.Text.Trim.ToUpper & ""
                        Cmd.CommandType = Data.CommandType.StoredProcedure
                        Cmd.CommandText = "Mob_IngresarViajes"
                        Da.Fill(Ds, "Consulta")
                        If Ds.Tables("Consulta").Rows.Count = 0 Then
                            If Ds.Tables(1) IsNot Nothing Then
                                If Ds.Tables(1).Rows(0)(0) = 1 Then
                                    If MsgBox("El viaje ya fue controlado. Desea cargar envases?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                                        Dim frmEnv As New frmEnvase
                                        Cliente_ID = Me.GetCliente_ID(txtNroViaje.Text, 0)
                                        frmEnv.Cliente = Cliente_ID
                                        frmEnv.Viaje_Id = Me.txtNroViaje.Text
                                        frmEnv.ShowDialog()
                                        frmEnv = Nothing
                                        Inicializarfrm()
                                        lblMsg.Text = ""
                                        lblMenu.Text = vMenu
                                        Me.Text = FrmName
                                        txtNroViaje.Text = ""
                                        txtNroViaje.Focus()
                                        Exit Sub
                                    Else
                                        Me.txtNroViaje.ReadOnly = False
                                        Me.txtNroViaje.Text = ""
                                        Me.txtNroViaje.Focus()
                                        Exit Sub
                                    End If
                                End If
                            Else
                                lblMsg.Text = sElviajeNoExiste & txtNroViaje.Text
                                txtNroViaje.SelectAll()
                                Ds = Nothing
                                Inicializarfrm()
                                Exit Sub
                            End If
                        Else
                            MostrarPallet()
                        End If
                        bExisteViaje = True
                        CreateDataset()
                        SetStatus(Me.txtNroViaje.Text, 1)
                        lblMsg.Text = ""
                        txtNroViaje.ReadOnly = True
                        txtNroPallet.Text = ""
                        txtNroPallet.Focus()
                    Else
                        MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
                        Inicializarfrm()
                    End If
                    lblMsg.Text = ""
                Else
                    If txtNroViaje.Text.Trim <> "" And Me.lblNroViaje.Text = "Nro. de pedido:" Then
                        If VerifyConnection(SQLc) Then
                            Ds = New Data.DataSet
                            lblMsg.Text = strBuscando
                            lblMsg.Refresh()
                            Cmd = SQLc.CreateCommand
                            Da = New SqlDataAdapter(Cmd)
                            Cmd.Parameters.Add("@Codigo", Data.SqlDbType.NVarChar, 50).Value = Me.txtNroViaje.Text.Trim.ToUpper & ""
                            Cmd.CommandType = Data.CommandType.StoredProcedure
                            Cmd.CommandText = "Mob_IngresarPedidos"
                            Da.Fill(Ds, "Consulta")
                            If Ds.Tables("Consulta").Rows.Count = 0 Then
                                If Ds.Tables(1) IsNot Nothing Then
                                    If Ds.Tables(1).Rows(0)(0) = 1 Then
                                        Exit Sub
                                    End If
                                Else
                                    lblMsg.Text = sElpedidoNoExiste & txtNroViaje.Text
                                    txtNroViaje.SelectAll()
                                    Ds = Nothing
                                    Inicializarfrm()
                                    Exit Sub
                                End If
                            Else
                                MostrarPallet()
                            End If
                            bExisteViaje = True
                            CreateDataset()
                            SetStatus(Me.txtNroViaje.Text, 1)
                            lblMsg.Text = ""
                            txtNroViaje.ReadOnly = True
                            txtNroPallet.Text = ""
                            txtNroPallet.Focus()
                        Else
                            MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
                            Inicializarfrm()
                        End If
                        lblMsg.Text = ""
                    End If
                End If


            End If

        Catch SQLExc As SqlException
            MsgBox("SQLExc: " & SQLExc.Message, MsgBoxStyle.OkOnly, FrmName)
            'Inicializarfrm()
            Me.txtNroViaje.Text = ""
            lblMsg.Text = ""
            Exit Sub
        Catch ex As Exception
            MsgBox("Ex : " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            'Inicializarfrm()
            Me.txtNroViaje.Text = ""
            lblMsg.Text = ""
        End Try
    End Sub

    Private Sub Inicializarfrm()
        Try
            lblNroPallet.Visible = False
            txtNroPallet.Visible = False
            txtNroViaje.ReadOnly = False
            lblMsg.Text = ""
        Catch ex As Exception
            MsgBox("Inicializarfrm: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            lblMsg.Text = ""
        End Try
    End Sub
    Private Sub MostrarPallet()
        Try
            lblNroPallet.Visible = True
            txtNroPallet.Visible = True
            txtNroPallet.Focus()
        Catch ex As Exception
            MsgBox("MostrarPallet: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            lblMsg.Text = ""
        End Try
    End Sub
    Private Sub txtNroPallet_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNroPallet.KeyUp
        If e.KeyCode = Keys.Enter And Me.txtNroPallet.Text <> "" Then
            If bCanceloViajePallet = True Then
                bCanceloViajePallet = False
                Exit Sub
            End If
            If Len(txtNroViaje.Text) > 0 Then
                AddPallet(txtNroViaje)
            End If
        End If
    End Sub

    ' MGR 20120301 para viaje y pedido
    Private Sub AddPallet(ByRef sender As TextBox)
        Try
            Dim myTextBox As TextBox = sender
            If txtNroPallet.Text.Trim <> "" Then
                If bExisteViaje = True Then
                    Dim dr As DataRow
                    lblMsg.Text = ""
                    Debug.WriteLine(myTextBox.Name)
                    For Each dr In Ds.Tables("Consulta").Rows
                        If dr(0).ToString.ToUpper = txtNroPallet.Text.Trim.ToUpper Then
                            'MGR 20120301
                            If txtNroViaje.Text.Trim <> "" And Me.lblNroViaje.Text = "Nro. de viaje:" Then
                                '--Lo Nuevo para el Control de Expedicion...
                                If Not Mob_IngresarViajes_Pallet(myTextBox.Text, Me.txtNroPallet.Text) Then
                                    Exit Try
                                End If
                            ElseIf txtNroViaje.Text.Trim <> "" And Me.lblNroViaje.Text = "Nro. de pedido:" Then
                                If Not Mob_IngresarPedidos_Pallet(myTextBox.Text, Me.txtNroPallet.Text) Then
                                    Exit Try
                                End If
                            End If
                            'Esto lo muevo de Lugar porque antes primero lo mandaba al dataset en memoria
                            'el pallet y despues lo mandaba a la base de datos. ESTABA MAL
                            Ds.Tables(0).Rows.Remove(dr)
                            dr = DsSeleccionados.Tables("Clone").NewRow()
                            dr("NRO_PALLET") = txtNroPallet.Text.Trim.ToUpper & ""
                            DsSeleccionados.Tables(0).Rows.Add(dr)

                            Cliente_ID = GetCliente_ID(myTextBox.Text, Me.txtNroPallet.Text)
                            '-------------------------------------------------------------------
                            Me.txtNroPallet.Text = ""
                            lblMsg.Text = sPalletAgregado
                            If Ds.Tables("Consulta").Rows.Count = 0 Then
                                lblMsg.Text = sViajeCompleto
                                'MsgBox(sViajeCompleto, MsgBoxStyle.OkOnly)
                                If GetCantEnvase(Cliente_ID) Then
                                    Dim frmEnv As New frmEnvase
                                    frmEnv.Cliente = Cliente_ID
                                    frmEnv.Viaje_Id = myTextBox.Text
                                    frmEnv.ShowDialog()
                                    frmEnv = Nothing
                                    Inicializarfrm()
                                    lblMsg.Text = ""
                                    lblMenu.Text = vMenu
                                    Me.Text = FrmName
                                    txtNroViaje.Text = ""
                                    txtNroViaje.Focus()
                                    Exit Sub
                                Else : Inicializarfrm()
                                    myTextBox.Text = ""
                                    lblMsg.Text = sViajeCompleto
                                    Me.txtNroViaje.Focus()
                                    Exit Sub
                                End If

                                'PROBAR ESTA RUTINA. SGG.
                                If SetStatus(myTextBox.Text, 2) Then
                                    Me.txtNroPallet.Text = ""
                                    myTextBox.Text = ""
                                    Inicializarfrm()
                                    lblMenu.Text = vMenu
                                    Me.Text = FrmName
                                    lblMsg.Text = ""
                                End If
                            End If
                            Exit For
                        End If
                    Next
                    'si llego acá es porque no existe el pallet en el viaje
                    If lblMsg.Text <> sPalletAgregado And lblMsg.Text <> sViajeCompleto Then
                        'acá debería validar si el pallet no fue previamente ingresados
                        For Each dr In DsSeleccionados.Tables("Clone").Rows
                            If dr(0).ToString.ToUpper = txtNroPallet.Text.Trim.ToUpper Then
                                lblMsg.Text = sPalletYaIngresado
                                'MsgBox(sPalletYaIngresado, MsgBoxStyle.OkOnly, FrmName)
                                txtNroPallet.SelectAll()
                                txtNroPallet.Focus()
                                Exit For
                            End If
                        Next
                        If lblMsg.Text <> sPalletAgregado And lblMsg.Text <> sViajeCompleto And lblMsg.Text <> sPalletYaIngresado Then
                            If Not VerificaPalletVacio(myTextBox.Text, Me.txtNroPallet.Text) Then
                                lblMsg.Text = sPalletNoEncontrado & txtNroViaje.Text
                                txtNroPallet.SelectAll()
                                txtNroPallet.Focus()
                            Else
                                lblMsg.Text = "El pallet Ingresado no tiene ningun producto."
                                txtNroPallet.SelectAll()
                                txtNroPallet.Focus()
                            End If
                        End If
                    End If
                    If lblMsg.Text = sPalletAgregado Then
                        lblMsg.Text = ""
                    End If
                Else
                    If txtNroViaje.ReadOnly = False And myTextBox.Text.Trim <> "" Then
                        txtNroPallet.Text = ""
                        lblMsg.Text = sViajeIngresadoPallet
                        myTextBox.Focus()
                    Else
                        If myTextBox.Text.Trim = "" Then
                            txtNroPallet.Text = ""
                            lblMsg.Text = sNohayViajeActivo
                            myTextBox.Focus()
                        End If
                    End If
                End If
            Else
                If txtNroViaje.Text.Trim = "" Then
                    lblMsg.Text = sIngreseTodo
                    myTextBox.Focus()
                Else
                    If txtNroViaje.ReadOnly = True Then
                        lblMsg.Text = sIngresarPallet
                        txtNroPallet.Focus()
                    Else
                        txtNroPallet.Text = ""
                        lblMsg.Text = sNohayViajeActivo
                        myTextBox.Focus()
                    End If
                End If

            End If
        Catch ex As Exception
            MsgBox("AddPallet: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Function VerificaPalletVacio(ByVal Viaje, ByVal Pallet) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "Mob_CE_VerificaPallet"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Pa = New SqlParameter("@viaje_id", SqlDbType.VarChar, 50)
                Pa.Value = Viaje
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Pallet", SqlDbType.VarChar, 50)
                Pa.Value = Pallet
                Cmd.Parameters.Add(Pa)

                Da.Fill(Ds, "PT")

                If IsDBNull(Ds.Tables("PT").Rows(0)(0)) Then
                    Return False
                Else
                    Return True
                End If
            Else
                Me.lblMsg.Text = SQLConErr
                Return False
            End If

        Catch SQLEx As SqlException
            Me.lblMsg.Text = "VerificaPalletVacio SQL: " & SQLEx.Message
            Return False
        Catch ex As Exception
            Me.lblMsg.Text = "VerificaPalletVacio: " & ex.Message
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
            Da = Nothing
            Ds = Nothing
        End Try
    End Function
    Private Sub txtNroPallet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNroPallet.TextChanged
        lblMsg.Text = ""
    End Sub

    Private Sub txtNroViaje_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNroViaje.TextChanged
        lblMsg.Text = ""
    End Sub
    ' MGR 20120301 para Pedido
    Private Sub txtNroPedido_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        lblMsg.Text = ""
    End Sub

    Private Sub cmdFinalizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFinalizar.Click
        'frmPrincipal.Show()
        Me.Close()
    End Sub

    Private Sub cmdAgregarPallet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAgregarPallet.Click
        If txtNroViaje.Text.Trim <> "" Then
            AddPallet(txtNroViaje)
        End If
    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        F2()
    End Sub

    Private Sub cmdPalletIng_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPalletIng.Click
        F3()
    End Sub

    Private Sub cmdPalletRest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPalletRest.Click
        F4()
    End Sub

    Private Function SetStatus(ByVal ViajeID As String, ByVal Status As String) As Boolean 'SGG
        Dim cmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                cmd = SQLc.CreateCommand
                cmd.CommandText = "CONTROL_VIAJE"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = SQLc
                Pa = New SqlParameter("@viaje_id", SqlDbType.VarChar, 50)
                Pa.Value = ViajeID
                cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Value", SqlDbType.Char, 1)
                Pa.Value = Status
                cmd.Parameters.Add(Pa)
                cmd.ExecuteNonQuery()
            Else
                Me.lblMsg.Text = SQLConErr
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            Me.lblMsg.Text = "SetStatus SQL: " & SQLEx.Message
            Return False
        Catch ex As Exception
            Me.lblMsg.Text = "SetStatus: " & ex.Message
            Return False
        Finally
            cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function Mob_IngresarViajes_Pallet(ByVal ViajeId As String, ByVal Pallet As Long) As Boolean
        Dim xCmd As SqlCommand
        Dim xPa As SqlParameter
        Dim Trans As SqlTransaction
        xCmd = SQLc.CreateCommand
        Trans = SQLc.BeginTransaction
        xCmd.Transaction = Trans
        xCmd.Connection = SQLc
        Try
            If VerifyConnection(SQLc) Then
                xCmd.CommandText = "Mob_IngresarViajes_Pallet"
                xCmd.CommandType = CommandType.StoredProcedure

                xPa = New SqlParameter("@ViajeId", SqlDbType.VarChar, 100)
                xPa.Value = ViajeId
                xCmd.Parameters.Add(xPa)
                xPa = Nothing

                xPa = New SqlParameter("@Pallet", SqlDbType.Int)
                xPa.Value = Pallet
                xCmd.Parameters.Add(xPa)

                xCmd.ExecuteNonQuery()
                Trans.Commit()
            Else
                Me.lblMsg.Text = "No se pudo conectar a la base de datos."
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            Trans.Rollback()
            Me.lblMsg.Text = "Mob_IngV_Pallet SQL.: " & SQLEx.Message
            Return False
        Catch ex As Exception
            Trans.Rollback()
            Me.lblMsg.Text = "Mob_IngV_Pallet: " & ex.Message
            Return False
        Finally
            xCmd = Nothing
            xPa = Nothing
        End Try
    End Function
    ' MGR 20120301 para Pedido
    Private Function Mob_IngresarPedidos_Pallet(ByVal PedidoId As String, ByVal Pallet As Long) As Boolean
        Dim xCmd As SqlCommand
        Dim xPa As SqlParameter
        Dim Trans As SqlTransaction
        xCmd = SQLc.CreateCommand
        Trans = SQLc.BeginTransaction
        xCmd.Transaction = Trans
        xCmd.Connection = SQLc
        Try
            If VerifyConnection(SQLc) Then
                xCmd.CommandText = "Mob_IngresarPedidos_Pallet"
                xCmd.CommandType = CommandType.StoredProcedure

                xPa = New SqlParameter("@PedidoId", SqlDbType.VarChar, 100)
                xPa.Value = PedidoId
                xCmd.Parameters.Add(xPa)
                xPa = Nothing

                xPa = New SqlParameter("@Pallet", SqlDbType.Int)
                xPa.Value = Pallet
                xCmd.Parameters.Add(xPa)

                xCmd.ExecuteNonQuery()
                Trans.Commit()
            Else
                Me.lblMsg.Text = "No se pudo conectar a la base de datos."
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            Trans.Rollback()
            Me.lblMsg.Text = "Mob_IngV_Pallet SQL.: " & SQLEx.Message
            Return False
        Catch ex As Exception
            Trans.Rollback()
            Me.lblMsg.Text = "Mob_IngV_Pallet: " & ex.Message
            Return False
        Finally
            xCmd = Nothing
            xPa = Nothing
        End Try
    End Function
    Private Function GetValues(ByVal ViajeId As String, ByRef xDs As DataSet) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.Connection = SQLc
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "Mob_IngresarViajes_Controlado"
                Pa = New SqlParameter("@ViajeId", Data.SqlDbType.VarChar, 100)
                Pa.Value = ViajeId
                Cmd.Parameters.Add(Pa)
                Da.Fill(xDs, "Clone")
            Else
                MsgBox("No se pudo conectar a la base de datos.", MsgBoxStyle.OkOnly, FrmName)
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("GetValues SQL: " & SQLEx.Message)
            Return False
        Catch ex As Exception
            MsgBox("GetValues : " & ex.Message)
            Return False
        Finally
            Da = Nothing
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function GetValues_Pedidos(ByVal PedidoId As String, ByRef xDs As DataSet) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.Connection = SQLc
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "Mob_IngresarPedidos_Controlado"
                Pa = New SqlParameter("@PedidoId", Data.SqlDbType.VarChar, 100)
                Pa.Value = PedidoId
                Cmd.Parameters.Add(Pa)
                Da.Fill(xDs, "Clone")
            Else
                MsgBox("No se pudo conectar a la base de datos.", MsgBoxStyle.OkOnly, FrmName)
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("GetValues SQL: " & SQLEx.Message)
            Return False
        Catch ex As Exception
            MsgBox("GetValues : " & ex.Message)
            Return False
        Finally
            Da = Nothing
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function
    Private Function GetCantEnvase(ByVal Cliente_Id As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim x As Integer
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.Connection = SQLc
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "Mob_GetCantEnvase"

                Pa = New SqlParameter("@Cliente_Id", Data.SqlDbType.VarChar, 15)
                Pa.Value = Cliente_Id
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Variable", Data.SqlDbType.Int)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

                x = Cmd.Parameters("@Variable").Value
                If x > 0 Then
                    Return True
                Else
                    Return False
                End If

            Else
                MsgBox("No se pudo conectar a la base de datos.", MsgBoxStyle.OkOnly, FrmName)
            End If

        Catch SQLEx As SqlException
            MsgBox("GetValues SQL: " & SQLEx.Message)
            Return False
        Catch ex As Exception
            MsgBox("GetValues : " & ex.Message)
            Return False
        Finally
            Da = Nothing
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function
    Private Function GetCliente_ID(ByVal Viaje_id As String, ByVal Nro_Pallet As Long) As String
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim ClienteR As String = ""
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.Connection = SQLc
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "Mob_GetClient"

                Pa = New SqlParameter("@Viaje_Id", Data.SqlDbType.VarChar, 50)
                Pa.Value = Viaje_id
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Pallet_Picking", Data.SqlDbType.BigInt)
                Pa.Value = Nro_Pallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Cliente_ID", Data.SqlDbType.VarChar, 15)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

                ClienteR = IIf(IsDBNull(Cmd.Parameters("@Cliente_Id").Value), "", Cmd.Parameters("@Cliente_Id").Value)

            Else
                MsgBox("No se pudo conectar a la base de datos.", MsgBoxStyle.OkOnly, FrmName)
            End If
            Return ClienteR
        Catch SQLEx As SqlException
            MsgBox("GetValues SQL: " & SQLEx.Message)
            Return ""
        Catch ex As Exception
            MsgBox("GetValues : " & ex.Message)
            Return ""
        Finally
            Da = Nothing
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub lblNroViaje_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblNroViaje.ParentChanged

    End Sub

    Private Sub cmdCambiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCambiar.Click
        F6()
    End Sub
End Class