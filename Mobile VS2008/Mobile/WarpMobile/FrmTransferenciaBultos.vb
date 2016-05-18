Imports System.Data.SqlClient
Imports System.Data

Public Class FrmTransferenciaBultos
    Private Const FrmName As String = "Transferencias"
    Private Const SQLConErr As String = "Fallo al intentar conectar con la base de datos."
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private TrLocator As Boolean
    Private Ubicacion As String
    Private xEsFraccionable As Boolean = False
    Private Producto As String
    Dim cliente_id As String
    Dim xCant As Double
    Dim cantidad As Double
    Dim vCat_Log_Id As String
    Private Const ErrCon As String = "No se pudo conectar con la base de datos."
    Private DataSerie As New DataSet
    Private Serializa As Boolean

    Private Sub FrmTransferenciaBultos_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Try
            Select Case e.KeyCode
                Case Keys.F1
                    Iniciar()
                Case Keys.F2
                    SalirForm()
            End Select
        Catch ex As Exception
            MsgBox("frmTransferencia_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub txtOrigen_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOrigen.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtOrigen.Text <> "" Then
                If Not VerificaNavePRE(Me.txtOrigen.Text) Then
                    MsgBox("La Nave/Posicion se encuentra inhabilitada para operaciones de transferencias", MsgBoxStyle.Information, FrmName)
                    Me.txtOrigen.Text = ""
                    Exit Sub
                End If
                If VerificaPosLockeada(Me.txtOrigen.Text) Then
                    Me.txtOrigen.Text = UCase(Me.txtOrigen.Text)
                    Me.txtOrigen.ReadOnly = True
                    Me.Label1.Visible = True
                    Me.cmbClienteId.Visible = True
                    Me.lblProducto.Visible = True
                    Me.txtProducto.Visible = True
                    lblDescripcion.Visible = True
                    lblCantDisponible.Visible = True
                    lblMensaje.Text = "Ingrese el Compañia y Codigo de Producto"
                    Me.cmbClienteId.Focus()
                Else
                    Me.txtOrigen.Text = ""
                    Me.txtOrigen.ReadOnly = False
                End If
            End If
        End If
    End Sub

    Private Function VerificaNavePRE(ByVal xUbicacion As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim xValue As String
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Mob_Verifica_Nave_Pre"
                Cmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Pos_Nave_cod", SqlDbType.VarChar, 40)
                Pa.Value = xUbicacion
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Flag", SqlDbType.Char, 1, ParameterDirection.Output)
                Pa.Value = DBNull.Value
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
                xValue = IIf(IsDBNull(Cmd.Parameters("@Flag").Value), "", Cmd.Parameters("@Flag").Value)
            Else
                lblMensaje.Text = SQLConErr
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

    Private Function VerificaPosLockeada(ByVal Pos_O As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Value As String
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "VERIFICA_LOCKEO_POS"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc

                Pa = New SqlParameter("@Posicion_O", SqlDbType.VarChar, 45)
                Pa.Value = Pos_O
                Pa.Direction = ParameterDirection.Input
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@out", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

                Value = IIf(IsDBNull(Cmd.Parameters("@out").Value), "0", Cmd.Parameters("@out").Value)
                If Value = "1" Then
                    Return False
                ElseIf Value = "0" Then
                    Return True
                End If
            Else
                Me.lblMensaje.Text = SQLConErr
                Return False
            End If
        Catch SQLEx As SqlException
            Me.lblMensaje.Text = "VerificaPosLockeada SQL. " & SQLEx.Message
            Return False
        Catch ex As Exception
            Me.lblMensaje.Text = "VerificaPosLockeada. " & ex.Message
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub FrmTransferenciaBultos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Iniciar()
        'ObtenerClientes()
        IniciarForm()
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
                        cliente_id = drDSRow("Cliente_id")
                        dt.Rows.Add(drNewRow)
                    Next
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

    Private Sub IniciarForm()
        lblMensaje.Text = "Ingrese Ubicacion de Origen"
        txtOrigen.Focus()
    End Sub

    Private Sub SalirForm()
        Try
            If MsgBox("Desea salir?" & vbNewLine & "La operacion en curso sera cancelada.", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub txtProducto_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProducto.KeyDown

    End Sub

    Private Function ValidarIngreso() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.Val_Prod_TR_Bulto"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                'Pa = New SqlParameter("@CLIENTE_ID", Data.SqlDbType.VarChar, 50)
                'Pa.Value = cliente_id   'Me.DtTareas.Rows(Indice)("CLIENTE").ToString
                'Cmd.Parameters.Add(Pa)
                'Pa = Nothing

                Pa = New SqlParameter("@CODIGO", SqlDbType.VarChar, 50)
                Pa.Value = Me.TxtProducto.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Producto = IIf(IsDBNull(Cmd.Parameters("@PRODUCTO_ID").Value), "", Cmd.Parameters("@PRODUCTO_ID").Value)
                Me.cliente_id = IIf(IsDBNull(Cmd.Parameters("@CLIENTE_ID").Value), "", Cmd.Parameters("@CLIENTE_ID").Value)
                Me.txtProducto.Text = Trim(UCase(Producto))
                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.OkOnly)
                Me.txtProducto.Text = ""
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Me.txtProducto.Text = ""
            Return False
        Catch ex As Exception
            MsgBox("ValidarIngreso: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Me.txtProducto.Text = ""
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function
    Private Function GetFlgFraccionable(ByVal Cliente_id As String, ByVal Producto_id As String, ByRef FlgFraccionable As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        GetFlgFraccionable = False
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "GET_FLG_FRACCIONABLE"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Cliente_id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 30)
                Pa.Value = Producto_id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Flg_Fraccionable", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                xCmd.ExecuteNonQuery()
                FlgFraccionable = (xCmd.Parameters("@Flg_Fraccionable").Value.ToString)
                xEsFraccionable = FlgFraccionable
                Return True
            Else : MsgBox(SQLConErr, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function
    Private Function ExistenciaProdUbicacion(ByVal xDestino As String, ByVal xProducto As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim DESC As String = ""

        'Dim xdesc As String
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "dbo.Mob_Verifica_Prod_Nave"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 30)
                Pa.Value = xProducto
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Pos_Nave_cod", SqlDbType.VarChar, 40)
                Pa.Value = xDestino
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Me.cmbClienteId.SelectedValue.ToString
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Desc", SqlDbType.VarChar, 200)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CanDisponible", SqlDbType.Float)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                DESC = IIf(IsDBNull(Cmd.Parameters("@Desc").Value), "", Cmd.Parameters("@Desc").Value)
                xCant = CDbl(IIf(IsDBNull(Cmd.Parameters("@CanDisponible").Value), 0, Cmd.Parameters("@CanDisponible").Value))

                If DESC <> "" And xCant > 0 Then
                    lblDescripcion.Text = DESC
                    lblCantDisponible.Text = xCant.ToString
                    Return True
                Else
                    Return False
                End If
            Else
                lblMensaje.Text = SQLConErr
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

    Private Sub btnVolver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVolver.Click
        SalirForm()
    End Sub


    Private Sub txtCantTransferir_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantTransferir.KeyUp
        If e.KeyValue = 13 And Me.txtCantTransferir.Text <> "" And Val(Me.txtCantTransferir.Text) > 0 Then
            If txtCantTransferir.Text <> "" Then
                cantidad = CDbl(txtCantTransferir.Text)
                If cantidad > xCant Then
                    MsgBox("Cantidad no puede ser mayor a la existencia", MsgBoxStyle.Information, FrmName)
                    txtCantTransferir.Text = ""
                    'lblMensaje.Text = "Cantidad a transferir"
                    txtCantTransferir.Focus()
                Else
                    If Not GetUbicacionLocator(Me.txtOrigen.Text) Then
                        Exit Sub
                    End If
                    lblDestino.Visible = True
                    txtDestino.Visible = True
                    lblDestinoSugerida.Visible = True
                    txtCantTransferir.ReadOnly = True
                    'txtDestino.ReadOnly = True
                    lblMensaje.Text = "Ingrese Destino"
                    txtDestino.Focus()
                End If
            End If
        End If
    End Sub

    Private Function GetUbicacionLocator(ByVal UbicacionOrigen As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "Locator_Transferencia_Bulto"
                xCmd.CommandType = CommandType.StoredProcedure
                Da = New SqlDataAdapter(xCmd)

                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 50)
                Pa.Value = Me.cmbClienteId.SelectedValue.ToString
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 100)
                Pa.Value = Me.txtProducto.Text
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@UbicacionOrigenCod", SqlDbType.VarChar, 50)
                Pa.Value = UbicacionOrigen
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Da.Fill(Ds, "Ubicacion")
                If Ds.Tables("Ubicacion").Rows.Count > 0 Then
                    Ubicacion = Ds.Tables("Ubicacion").Rows(0)(0).ToString
                    Me.lblDestinoSugerida.Text = Ds.Tables("Ubicacion").Rows(0)(0).ToString
                End If

            End If
            Return True
        Catch SQLEx As SqlException
            Me.lblMensaje.Text = "GetUbicacionLocator SQL. " & SQLEx.Message
        Catch ex As Exception
            Me.lblMensaje.Text = "GetUbicacionLocator. " & ex.Message
        Finally
            xCmd = Nothing
            Pa = Nothing
            Ds = Nothing
            Da = Nothing
        End Try
    End Function

    Private Sub txtDestino_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDestino.KeyUp
        Dim frmBultoPallet As New FrmTransferenciaBultosPalletContenedora, VBULTO As String, VPALLET As String, gInfo As Boolean, lectura As String
        Try
            Select Case e.KeyCode
                Case 13
                    If Me.txtDestino.Text <> "" Then
                        If Me.txtOrigen.Text.Trim.ToUpper <> Me.txtDestino.Text.Trim.ToUpper Then
                            If ExisteNavePosicion(Me.txtDestino.Text) Then
                                If Not VerificaNavePRE(Me.txtDestino.Text) Then
                                    MsgBox("La Nave/Posicion se encuentra inhabilitada para operaciones de transferencias", MsgBoxStyle.Information, FrmName)
                                    Me.txtDestino.Text = ""
                                    Exit Sub
                                End If
                            Else
                                Me.txtDestino.Text = ""
                                Me.txtDestino.Focus()
                                Exit Sub
                            End If
                            If TrLocator Then
                                If Trim(UCase(Me.txtDestino.Text)) <> Trim(UCase(Ubicacion)) Then
                                    MsgBox("Posicion Invalida.", MsgBoxStyle.Exclamation, FrmName)
                                    Me.txtDestino.Text = ""
                                    Me.txtDestino.Focus()
                                    Exit Try
                                End If
                            End If
                            Dim Rta As Object = MsgBox("¿Confirma la Transferencia?", MsgBoxStyle.YesNo, FrmName)
                            If Rta = vbNo Then
                                Me.IniciarForm()
                                Me.txtOrigen.Focus()
                                Exit Sub
                            End If
                            If txtDestino.Text.Trim <> "" Then
                                txtDestino.Text = UCase(txtDestino.Text)
                                frmBultoPallet.PosicionOrigen = Me.txtOrigen.Text
                                frmBultoPallet.PosicionDestino = Me.txtDestino.Text
                                frmBultoPallet.Compañia = Me.cmbClienteId.SelectedValue
                                frmBultoPallet.Producto = Me.txtProducto.Text
                                frmBultoPallet.ShowDialog()
                                lectura = frmBultoPallet.Lectura
                                If frmBultoPallet.Cancelado Then
                                    Me.txtDestino.Text = ""
                                    Me.txtDestino.Focus()
                                    Exit Sub
                                Else
                                    'saber si es pallet o contenedora.
                                    If frmBultoPallet.TipoValidacion = "BULTO" Then
                                        VPALLET = ""
                                        VBULTO = lectura
                                    Else
                                        VPALLET = lectura
                                        VBULTO = ""
                                    End If
                                    gInfo = frmBultoPallet.GeneracionDeInformacionAdicional
                                End If
                                'cmbClienteId
                                If Not Me.Serializa Then

                                    If Transferir(Me.cmbClienteId.SelectedValue.ToString, Me.txtOrigen.Text, Me.txtDestino.Text, Me.txtProducto.Text, vUsr.CodUsuario, txtCantTransferir.Text, vCat_Log_Id, VBULTO, VPALLET, gInfo) Then
                                        Iniciar()
                                        Me.lblMensaje.Text = "Ingrese Origen"
                                    Else
                                        Me.lblMensaje.Text = "No se pudo Completar la Operacion."
                                        Me.txtDestino.Text = ""
                                    End If

                                Else

                                    If TransferirPorSeries(Me.cmbClienteId.SelectedValue.ToString, Me.txtOrigen.Text, Me.txtDestino.Text, Me.txtProducto.Text, vUsr.CodUsuario, 1, vCat_Log_Id, VBULTO, VPALLET, gInfo, Me.DataSerie) Then
                                        Iniciar()
                                        Me.lblMensaje.Text = "Ingrese Origen"
                                    Else
                                        Me.lblMensaje.Text = "No se pudo Completar la Operacion."
                                        Me.txtDestino.Text = ""
                                    End If

                                End If
                            End If

                        Else
                            Me.txtDestino.SelectAll()
                            Me.lblMensaje.Text = "La posicion Origen y destino son iguales."
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox("txtDestino_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            frmBultoPallet.Dispose()
        End Try
    End Sub

    Private Function RequiereRemito(ByVal destino As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Value As String
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "GET_IMPRIME_REMITO"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc

                Pa = New SqlParameter("@POSICION_ORIGEN", SqlDbType.VarChar, 40)
                Pa.Value = Me.txtOrigen.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_DESTINO", SqlDbType.VarChar, 40)
                Pa.Value = destino
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@IMPRIME_REMITO", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

                Value = IIf(IsDBNull(Cmd.Parameters("@IMPRIME_REMITO").Value), "0", Cmd.Parameters("@IMPRIME_REMITO").Value)
                If Value = "1" Then
                    Return True
                ElseIf Value = "0" Then
                    Return False
                End If
            Else
                Me.lblMensaje.Text = SQLConErr
                Return False
            End If

        Catch SQLEx As SqlException
            Me.lblMensaje.Text = "Mob_Verifica_Nave_Pre SQL. " & SQLEx.Message
            Return False
        Catch ex As Exception
            Me.lblMensaje.Text = "Mob_Verifica_Nave_Pre. " & ex.Message
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function GenerarSecuencias(ByVal Secuencia As String) As String
        '--------------------------------------------------------------------------------------------------------------------------------
        'GENERA LOS NUMEROS DE CONTENEDORAS.
        '--------------------------------------------------------------------------------------------------------------------------------
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter, Ret As String = ""
        Try

            Cmd = SQLc.CreateCommand
            DA = New SqlDataAdapter(Cmd)
            DS = New DataSet
            Cmd.CommandText = "DBO.GET_VALUE_FOR_SEQUENCE"
            Cmd.CommandType = CommandType.StoredProcedure

            PA = New SqlParameter("@SECUENCIA", SqlDbType.VarChar, 100)
            PA.Value = Secuencia
            Cmd.Parameters.Add(PA)
            PA = Nothing

            PA = New SqlParameter("@VALUE", SqlDbType.BigInt)
            PA.Direction = ParameterDirection.Output
            Cmd.Parameters.Add(PA)

            Cmd.ExecuteNonQuery()

            Ret = Cmd.Parameters("@VALUE").Value

            Return Ret
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            DA.Dispose()
            Cmd.Dispose()
            DS.Dispose()
            PA = Nothing
        End Try
    End Function

    Private Function GENERA_PALLET_AT(ByVal Cliente As String, ByVal Producto As String) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As New DataSet
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandText = "SELECT ISNULL(PALLET_AUTOMATICO,'0') FROM PRODUCTO WHERE CLIENTE_ID='" & Cliente & "' AND PRODUCTO_ID='" & Producto & "'"
                Cmd.CommandType = CommandType.Text

                DA.Fill(DS, "EXISTE")
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        If DS.Tables(0).Rows(0)(0).ToString = "0" Then
                            Return False
                        Else
                            Return True
                        End If
                    End If
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Information, FrmName)
                Return False
            End If
            Return True
        Catch sqlex As SqlException
            MsgBox("Excepción: " & sqlex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            Cmd.Dispose()
            DA.Dispose()
            DS.Dispose()
        End Try
    End Function

    Private Function TransferirPorSeries(ByVal cliente_id As String, ByVal Pos_O As String, ByVal Pos_D As String, ByVal Producto As String, ByVal Usr As String, ByVal cantidad As Double, ByVal CAT_LOG_ID As String, ByVal Contenedora As String, ByVal Pallet As String, ByVal GeneraInfo As Boolean, ByVal DsSeries As DataSet) As Boolean
        Dim ValErr As String = "", i As Integer, NroSerie As String = "", GeneraPallet As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Trans As SqlTransaction

        Try
            If VerifyConnection(SQLc) Then
                '1. Determino si usa pallet o no.


                '2. La contenedora, existe o hay que generarla? (por definicion, si tiene serie, se genera)
                If Trim(Contenedora) = "" Then
                    Contenedora = GenerarSecuencias("CONTENEDORA")
                End If

                '3. El pallet existe o hay que generarlo.
                GeneraPallet = GENERA_PALLET_AT(cliente_id, Producto)
                If Trim(Pallet) = "" And GeneraPallet Then
                    Pallet = GenerarSecuencias("NROPALLET_SEQ")
                End If

                Trans = SQLc.BeginTransaction
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "MOB_TRANSFERENCIA_PROD_SERIES"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Cmd.Transaction = Trans


                For i = 0 To DsSeries.Tables(0).Rows.Count - 1

                    NroSerie = DsSeries.Tables(0).Rows(i)(0).ToString

                    Pa = New SqlParameter("@cliente_id", SqlDbType.VarChar, 15)
                    Pa.Value = cliente_id
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@POSICION_O", SqlDbType.VarChar, 45)
                    Pa.Value = Pos_O
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@POSICION_D", SqlDbType.VarChar, 45)
                    Pa.Value = Pos_D
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 30)
                    Pa.Value = Producto
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 30)
                    Pa.Value = Usr
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@Cantidad", SqlDbType.Float)
                    Pa.Value = cantidad
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@CAT_LOG_ID", SqlDbType.VarChar, 50)
                    Pa.Value = IIf(Trim(CAT_LOG_ID) = "", DBNull.Value, CAT_LOG_ID)
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@PALLET_DEST", SqlDbType.VarChar, 100)
                    Pa.Value = IIf(Trim(Pallet) = "", DBNull.Value, Trim(Pallet))
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@CONTENEDOR", SqlDbType.VarChar, 50)
                    Pa.Value = IIf(Trim(Contenedora) = "", DBNull.Value, Trim(Contenedora))
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@GENERA_INFO", SqlDbType.VarChar, 1)
                    Pa.Value = "0"
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 100)
                    Pa.Value = NroSerie
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Cmd.ExecuteNonQuery()
                    Cmd.Parameters.Clear()

                Next
                Trans.Commit()

            Else
                lblMensaje.Text = SQLConErr
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            Try
                Trans.Rollback()
            Catch ex As Exception
            End Try
            ValErr = Mid(SQLEx.Message.ToString, 1, 1)
            MsgBox("Transferir SQL. " & SQLEx.Message)
            Me.lblMensaje.Text = "Transferir SQL. " & SQLEx.Message
            If ValErr = "1" Then
                txtDestino.Focus()
            End If
            Return False
        Catch ex As Exception
            Trans.Rollback()
            MsgBox("Transferir SQL. " & ex.Message)
            Me.lblMensaje.Text = "Transferir. " & ex.Message
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
            Trans = Nothing
        End Try
    End Function


    Private Function Transferir(ByVal cliente_id As String, ByVal Pos_O As String, ByVal Pos_D As String, ByVal Producto As String, ByVal Usr As String, ByVal cantidad As Double, ByVal CAT_LOG_ID As String, ByVal Contenedora As String, ByVal Pallet As String, ByVal GeneraInfo As Boolean) As Boolean
        Dim ValErr As String = ""
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Trans As SqlTransaction
        Trans = SQLc.BeginTransaction
        Try
            If VerifyConnection(SQLc) Then

                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "MOB_TRANSFERENCIA_PROD"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Cmd.Transaction = Trans

                Pa = New SqlParameter("@cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = cliente_id
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_O", SqlDbType.VarChar, 45)
                Pa.Value = Pos_O
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_D", SqlDbType.VarChar, 45)
                Pa.Value = Pos_D
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 30)
                Pa.Value = Producto
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 30)
                Pa.Value = Usr
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Cantidad", SqlDbType.Float)
                Pa.Value = cantidad
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CAT_LOG_ID", SqlDbType.VarChar, 50)
                Pa.Value = CAT_LOG_ID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PALLET_DEST", SqlDbType.VarChar, 100)
                Pa.Value = IIf(Trim(Pallet) = "", DBNull.Value, Trim(Pallet))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CONTENEDOR", SqlDbType.VarChar, 50)
                Pa.Value = IIf(Trim(Contenedora) = "", DBNull.Value, Trim(Contenedora))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@GENERA_INFO", SqlDbType.VarChar, 1)
                Pa.Value = IIf(GeneraInfo, "1", "0")
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

            Else
                lblMensaje.Text = SQLConErr
                Return False
            End If
            Trans.Commit()
            Return True
        Catch SQLEx As SqlException
            Try
                Trans.Rollback()
            Catch ex As Exception
            End Try
            ValErr = Mid(SQLEx.Message.ToString, 1, 1)
            MsgBox("Transferir SQL. " & SQLEx.Message)
            Me.lblMensaje.Text = "Transferir SQL. " & SQLEx.Message
            If ValErr = "1" Then
                txtDestino.Focus()
            End If
            Return False
        Catch ex As Exception
            Trans.Rollback()
            MsgBox("Transferir SQL. " & ex.Message)
            Me.lblMensaje.Text = "Transferir. " & ex.Message
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
            Trans = Nothing
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
                Me.lblMensaje.Text = SQLConErr
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

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Iniciar()
        Me.lblMensaje.Text = "Ingrese Origen"
        txtOrigen.Focus()
    End Sub
    Private Sub Iniciar()
        Dim vError As String = ""
        Try
            Me.PanelCatLog.Visible = False
            txtOrigen.Text = ""
            txtCantTransferir.Text = ""
            txtProducto.Text = ""
            lblDescripcion.Text = ""
            lblCantDisponible.Text = ""
            txtDestino.Text = ""
            lblDestinoSugerida.Text = ""
            lblProducto.Visible = False
            txtProducto.Visible = False
            lblDescripcion.Visible = False
            lblCantDisponible.Visible = False
            lblCantidad.Visible = False
            Me.Label2.Visible = False
            txtCantTransferir.Visible = False
            lblDestino.Visible = False
            lblDestinoSugerida.Visible = False
            txtDestino.Visible = False
            Me.Label1.Visible = False
            Me.cmbClienteId.Visible = False

            Me.txtOrigen.ReadOnly = False
            txtDestino.ReadOnly = False
            txtProducto.ReadOnly = False
            txtCantTransferir.ReadOnly = False
            txtDestino.ReadOnly = False

            txtCantTransferir.Text = ""
            txtProducto.Text = ""
            lblDescripcion.Text = ""
            lblCantDisponible.Text = ""
            lblDestinoSugerida.Text = ""
            PanelCatLog.Visible = False

            If Not GetClientes(vError) Then
                MsgBox(vError, MsgBoxStyle.Critical, FrmName)
                Me.Close()
            End If

        Catch ex As Exception
            MsgBox("InicializarFrm: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub txtCantTransferir_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantTransferir.KeyPress
        Dim Search As String
        Dim Pos As Integer
        Search = "."
        If Not xEsFraccionable Then
            ValidarCaracterNumerico(e)
        Else
            Pos = InStr(1, Me.txtCantTransferir.Text, Search)
            If Pos > 0 And Asc(e.KeyChar) <> 46 Then
                If Len(Mid(Me.txtCantTransferir.Text, Pos + 1, Len(Me.txtCantTransferir.Text))) >= 5 And Asc(e.KeyChar) <> 8 Then
                    e.Handled = True
                    Me.txtCantTransferir.Focus()
                End If
            Else
                If Pos <> 0 And (Asc(e.KeyChar) = 46) Then
                    e.Handled = True
                ElseIf Pos = 0 And (Asc(e.KeyChar) = 44) Then
                    e.Handled = True
                ElseIf Pos = 0 And (Asc(e.KeyChar) = 46) Then
                    e.Handled = False
                Else
                    ValidarCaracterNumerico(e)
                End If
            End If
        End If
    End Sub
    Public Sub ValidarCaracterNumerico(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        Try
            'Valida que el caracter ingreado sea un nro
            If (Asc(e.KeyChar) >= 32 And Asc(e.KeyChar) <= 47) Or Asc(e.KeyChar) >= 58 Then
                e.Handled = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function SerializaIngreso() As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As New DataSet
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Select ISNULL(Serie_Ing,'0') from producto where cliente_id='" & Me.cmbClienteId.SelectedValue & "' and producto_id='" & Me.txtProducto.Text & "'"
                Cmd.CommandType = CommandType.Text
                DA = New SqlDataAdapter(Cmd)

                DA.Fill(DS, "PRODUCTO")

                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        If DS.Tables(0).Rows(0)(0).ToString = "0" Then
                            Return False
                        Else
                            Return True
                        End If
                    End If
                End If
            Else : MsgBox(Me.SQLConErr, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            DA.Dispose()
            Cmd.Dispose()
            DS.Dispose()
        End Try
    End Function

    Private Sub txtProducto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProducto.KeyUp
        Dim verror As String = "", frm As New FrmTransferenciaBultos_Series
        Try
            If e.KeyValue = 13 And Me.txtProducto.Text <> "" Then

                o2D.Decode(Me.txtProducto.Text)
                Me.txtProducto.Text = o2D.PRODUCTO_ID

                Me.Serializa = Me.SerializaIngreso()

                If ValidarIngreso() = True Then
                    If txtProducto.Text <> "" Then
                        If ExistenciaProdUbicacion(Me.txtOrigen.Text, Me.txtProducto.Text) Then
                            GetFlgFraccionable(cliente_id, Me.txtProducto.Text, xEsFraccionable)
                            If Not SelecCatLog(verror) Then
                                Throw New System.Exception(verror)
                            End If

                            Me.txtOrigen.ReadOnly = True
                            Me.lblMensaje.Text = "Cantidad a Transferir de Cat. Lógica : " & vCat_Log_Id
                            Me.Label2.Visible = True
                            Me.lblCantidad.Visible = True
                            Me.txtCantTransferir.Visible = True
                            Me.txtProducto.ReadOnly = True

                            If Serializa Then
                                Me.PanelCatLog.Visible = False
                            End If

                            If (Not Me.PanelCatLog.Visible) Then
                                If Not Serializa Then
                                    Me.txtCantTransferir.Focus()
                                Else
                                    frm.Cliente_ID = Me.cmbClienteId.SelectedValue
                                    frm.Producto_ID = Me.txtProducto.Text
                                    frm.Origen = Me.txtOrigen.Text
                                    frm.ShowDialog()
                                    If frm.Cancelado Then
                                        Me.txtProducto.Text = ""
                                        Me.txtProducto.Enabled = True
                                        Me.txtProducto.ReadOnly = False
                                        Me.lblDescripcion.Text = ""
                                        Me.txtProducto.Focus()
                                        Application.DoEvents()
                                        Me.lblCantDisponible.Visible = False
                                        Me.lblCantidad.Visible = False
                                        Me.Label2.Visible = False
                                        Me.txtCantTransferir.Visible = False
                                        Exit Sub
                                    Else
                                        Me.DataSerie = frm.InformacionSeries
                                        lblCantDisponible.Text = Me.DataSerie.Tables(0).Rows.Count
                                        Me.lblCantDisponible.Visible = True
                                        Me.lblCantidad.Visible = False
                                        Me.txtCantTransferir.Visible = False
                                        Me.lblDestino.Visible = True
                                        Me.txtDestino.Visible = True
                                        If Not GetUbicacionLocator(Me.txtOrigen.Text) Then
                                            Exit Sub
                                        End If
                                        Me.txtDestino.Text = ""
                                        Me.txtDestino.Focus()
                                    End If
                                End If
                            End If
                        Else
                            Me.txtProducto.Text = ""
                            Me.lblMensaje.Text = "Producto Incorrecto para el Cliente"
                            Me.txtProducto.Focus()
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en Transferecia")
        Finally
            frm.Dispose()
        End Try
    End Sub

    Private Function GetClientes(ByRef verror As String) As Boolean
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
                    Me.cmbClienteId.DropDownStyle = ComboBoxStyle.DropDownList
                    With cmbClienteId
                        .DataSource = dt
                        .DisplayMember = "RazonSocial"
                        .ValueMember = "Cliente_id"
                        .SelectedIndex = 0
                    End With
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, FrmName)
            End If
            Return True
        Catch ex As Exception
            verror = "No se puedieron cargar los clientes"
            Return False
        Finally
            Da = Nothing
            Ds = Nothing
            Pa = Nothing
        End Try
    End Function
    Private Function SelecCatLog(ByRef verror As String) As Boolean
        Dim Selecciona As Boolean

        Try

            If Not GetCatLogXUbic(Selecciona, verror) Then
                Throw New System.Exception("Error el buscar las categorías lógicas")
            End If

            If Selecciona Then
                Me.PanelCatLog.Visible = True
                Me.ListCatLog.SelectedIndex = 0
                Me.ListCatLog.Focus()



            End If

            Return True
        Catch ex As Exception
            If verror.Length = 0 Then

                verror = "Error en SelectCatLog : " + ex.Message.ToString


            End If

            Return False

        End Try
    End Function
    Private Function GetCatLogXUbic(ByRef Seleccionar As Boolean, ByRef verror As String) As Boolean
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
                xCmd.CommandText = "DBO.MOB_GET_CATLOG_XUBIC"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@POS_COD", SqlDbType.VarChar, 100)
                Pa.Value = Me.txtOrigen.Text
                xCmd.Parameters.Add(Pa)
                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 100)
                Pa.Value = Me.cmbClienteId.SelectedValue.ToString
                xCmd.Parameters.Add(Pa)
                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 100)
                Pa.Value = Me.txtProducto.Text
                xCmd.Parameters.Add(Pa)

                xCmd.Connection = SQLc
                Da.Fill(Ds, "CATLOG")
                dt.Columns.Add("CAT_LOG_ID", GetType(System.String))
                dt.Columns.Add("CANTIDAD_TOTAL", GetType(System.String))
                If Ds.Tables("CATLOG").Rows.Count > 1 Then
                    'HAY MAS DE UNA CATEGORIA LOGICA
                    Seleccionar = True

                    For Each drDSRow In Ds.Tables("CATLOG").Rows()
                        drNewRow = dt.NewRow()
                        drNewRow("CAT_LOG_ID") = drDSRow("CAT_LOG_ID")
                        drNewRow("CANTIDAD_TOTAL") = drDSRow("CAT_LOG_ID") & " : " & drDSRow("CANTIDAD_TOTAL").ToString

                        dt.Rows.Add(drNewRow)
                    Next
                    With Me.ListCatLog
                        .DataSource = Nothing
                        .DataSource = dt
                        .ValueMember = "CAT_LOG_ID"
                        .DisplayMember = "CANTIDAD_TOTAL"
                    End With

                ElseIf Ds.Tables("CATLOG").Rows.Count = 1 Then
                    Seleccionar = False
                    drNewRow = Ds.Tables("CATLOG").Rows(0)
                    xCant = Val(drNewRow("CANTIDAD_TOTAL"))
                    vCat_Log_Id = drNewRow("CAT_LOG_ID")

                    lblCantDisponible.Text = xCant.ToString

                Else
                    Seleccionar = False
                End If

            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, FrmName)
            End If
            Return True

        Catch SQLEx As SqlException
            verror = SQLEx.Message
            Return False

        Catch ex As Exception
            verror = "No se puedieron cargar las categorías lógicas: " + ex.Message.ToString
            Return False
        Finally
            Da = Nothing
            Ds = Nothing
            Pa = Nothing
        End Try
    End Function



    Private Sub cmbClienteId_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbClienteId.KeyUp
        If e.KeyValue = 13 Then
            Me.txtProducto.Focus()
        End If
    End Sub


    Private Sub cmbClienteId_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbClienteId.SelectedValueChanged
        Me.cliente_id = Me.cmbClienteId.SelectedValue.ToString
    End Sub



    Private Sub btnCancelar_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.GotFocus
        Me.txtOrigen.Focus()
    End Sub

    Private Sub ListCatLog_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListCatLog.KeyDown
        Dim verror As String = ""
        Try
            If e.KeyValue = 13 Then


                Me.txtOrigen.ReadOnly = True

                Me.Label2.Visible = True
                Me.lblCantidad.Visible = True
                Me.txtCantTransferir.Visible = True
                Me.txtProducto.ReadOnly = True
                xCant = Val(Mid(ListCatLog.Text, InStr(ListCatLog.Text, " : ") + 2))
                vCat_Log_Id = Trim(Mid(ListCatLog.Text, 1, InStr(ListCatLog.Text, " : ")))
                lblCantDisponible.Text = xCant.ToString
                Me.lblMensaje.Text = "Cantidad a Transferir de Cat. Lógica : " & vCat_Log_Id
                Me.txtCantTransferir.Focus()
                Me.PanelCatLog.Visible = False


            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, "Error en Transferecia")
        End Try
    End Sub

    Private Sub txtDestino_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDestino.TextChanged

    End Sub

    Private Sub txtOrigen_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOrigen.TextChanged

    End Sub

    Private Sub txtProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProducto.TextChanged

    End Sub
End Class