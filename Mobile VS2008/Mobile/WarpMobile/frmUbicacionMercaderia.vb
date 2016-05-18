Imports System.Data.SqlClient
Imports System.Data

Public Class frmUbicacionMercaderia

#Region "Declaraciones"
    Private CrossDock As Boolean = False
    Private bFallo As Boolean = False
    Private vMenu As String = "" '  "F1) Terminar Tarea." & vbNewLine & "F2) Cancelar." & vbNewLine & "F3) Salir."
    Private vSubMenu As String = "" '"La ubicación es incorrecta." & vbNewLine & "F1) Confirmar la ubicación." & vbNewLine & "F2) Cancelar la ubicación." & vbNewLine & "F3) Salir."
    Private vSubMenuPublico As String = "La ubicación es incorrecta."
    Public btnPublico As Boolean '= False 'para saber si es publico o supervisor
    Private slblUbicacion As String = "Ubicación: "
    Private sProcesando As String = "Procesando..."
    Private Const FrmName As String = "Ubicación de Mercadería"
    Private blnCancelar As Boolean = True
    Private sUbicacion As String
    Private sUbicacionId As Long
    Private btnSubmenu As Boolean = False
    Private sFinalizado As String = "Proceso finalizado..."
    Private sBuscando As String = "Buscando..."
    Private sCancelado As String = "Proceso cancelado..."
    Private sCompletarCampos As String = "Debe ingresar todos los campos..."
    Private DataGetPallet As New DataSet
    Private Documento_Id As Long = 0
    Private Nro_linea As Long = 0
    Private strProducto As String = "Producto: "
    Private strMultiproducto As String = "Pallet Multiproducto"
    Private Const SQLConErr As String = "No se pudo conectar a la base de datos."
    Private IntCant As Integer = 0 'intcant se va a utilizar para el control de cantidades (Flag)
    Private dblCantidad As Double = 0
    Private instaciado As Boolean = False
    Private xEsFraccionable As Boolean = False
    Private Monoproducto As Boolean = False
    Private Producto_Id As String = ""
    Private strUbicSug As String = ""
    Private blnPedirCodigo As Boolean
    Private blnCodigoValido As Boolean
    Dim DsP As New DataSet
#End Region

    Public Property Ubicacion() As String
        Get
            Return sUbicacion
        End Get
        Set(ByVal value As String)
            sUbicacion = value
        End Set
    End Property

    Public Property UbicacionId() As Long
        Get
            Return sUbicacionId
        End Get
        Set(ByVal value As Long)
            sUbicacionId = value
        End Set
    End Property

    Private lNave As Long
    Public Property NaveId() As Long
        Get
            Return lNave
        End Get
        Set(ByVal value As Long)
            lNave = value
        End Set
    End Property

    Private lPosicionId As Long
    Public Property PosicionId() As Long
        Get
            Return lPosicionId
        End Get
        Set(ByVal value As Long)
            lPosicionId = value
        End Set
    End Property

    Private Sub InicializarFrm()
        Try
            Dim i As Integer = 0
            Dim Pp As String = ""
            blnCodigoValido = False
            blnPedirCodigo = False
            cmdRechazar.Visible = False
            txtDestino.Text = ""
            txtPallet.Text = ""
            txtPallet.Enabled = True
            '            txtPallet.ReadOnly = False
            For i = 0 To DsP.Tables.Count - 1
                Pp = DsP.Tables(i).TableName
                DsP.Tables.RemoveAt(i)
            Next
            Me.txtCantidad.ReadOnly = False
            Me.txtCantidad.Text = ""
            Me.txtCantidad.Visible = False
            Me.lblCantidad.Visible = False
            Ubicacion = ""
            lblDestino.Visible = False
            txtDestino.Visible = False
            UbicacionId = Nothing
            PosicionId = Nothing
            NaveId = Nothing
            lblMenu.Text = vMenu
            'lblMsg.Text = ""
            lblNewMenu.Text = ""
            lblNewMenu.Visible = False
            bFallo = False
            Me.txtPallet.Focus()
            lblProducto.Visible = False
            cmdProducto.Visible = False
            cmdLockPosition.Visible = False
            TxtCamada.Text = ""
            TxtCamada.Visible = False
            lblCamada.Visible = False
            TxtCamada.Enabled = True

            lblProducto.Text = strProducto
            IntCant = 0
            dblCantidad = 0
            'Documento_Id = 0
            'Nro_linea = 0
            'Me.UbicacionId = Nothing
            'Me.Ubicacion = ""
        Catch ex As Exception
            MsgBox("InicializarFrm: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub InicializarFrm2()
        Try

            txtDestino.Text = ""
            txtPallet.Enabled = True
            'txtPallet.ReadOnly = False
            Ubicacion = ""
            lblDestino.Visible = False
            txtDestino.Visible = False
            UbicacionId = Nothing
            PosicionId = Nothing
            NaveId = Nothing
            lblMenu.Text = vMenu
            lblMsg.Text = ""
            lblNewMenu.Text = ""
            lblNewMenu.Visible = False
            Me.txtPallet.SelectAll()
            Me.txtPallet.Focus()

        Catch ex As Exception
            MsgBox("InicializarFrm2: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Function BuscarProducto(ByRef Ds As DataSet) As Boolean
        Dim Search As String = "-"
        Dim MyPos As Integer = 0
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "MobBuscaProducto"
                Cmd.Connection = SQLc
                Cmd.CommandType = CommandType.StoredProcedure


                Pa = New SqlParameter("@NroPallet", SqlDbType.VarChar, 100)
                Pa.Value = Me.txtPallet.Text.Trim
                Cmd.Parameters.Add(Pa)
                Da.Fill(Ds, "MobBuscaProducto")
                If Ds.Tables("MobBuscaProducto").Rows.Count = 1 Then
                    lblProducto.Text = strProducto & Ds.Tables("MobBuscaProducto").Rows(0)("Producto").ToString
                    MyPos = InStr(Ds.Tables("MobBuscaProducto").Rows(0)("Producto").ToString, Search)
                    Producto_Id = Trim(Mid(Ds.Tables("MobBuscaProducto").Rows(0)("Producto").ToString, 1, MyPos - 1))
                    cmdProducto.Visible = False
                    lblProducto.Visible = True
                    Monoproducto = True
                Else
                    lblProducto.Text = strProducto & strMultiproducto
                    cmdProducto.Visible = True
                    lblProducto.Visible = True
                    Monoproducto = False
                End If
            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            txtPallet.Enabled = True
            txtPallet.Focus()
            lblMsg.Text = SQLEx.Message
            Return False
        Catch ex As Exception
            txtPallet.Enabled = True
            txtPallet.Focus()
            MsgBox("BuscarProducto: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Da = Nothing
            Pa = Nothing

        End Try
    End Function

    Private Sub BuscarUbicacion()
        Dim FG As New FuncionesGenerales
        Dim Cmd As SqlCommand
        Dim vInt As Integer
        Dim OpCrossDock As Integer = 0
        Dim NavCrossDock As Long = 0
        Dim NaveCod As String = ""
        Dim vRem As Long
        Try
            Cmd = SQLc.CreateCommand
            lblMsg.Text = ""
            If txtPallet.Text.Trim <> "" And bFallo = False Then
                lblMsg.Text = ""
                txtPallet.Text = UCase(txtPallet.Text.Trim)
                txtPallet.Enabled = False
                TxtCamada.Enabled = False
                lblMsg.Text = sBuscando
                Me.lblDestino.Text = slblUbicacion
                Me.Ubicacion = ""
                txtDestino.Text = ""
                'verificar que el pallet existe y buscar los productos
                If BuscarProducto(DsP) = True Then
                    'el pallet existe
                    If Monoproducto Then
                        'EsFraccionable("10202", Producto_Id, xEsFraccionable, SQLc)
                    End If
                    If xEsFraccionable Then
                        Me.txtCantidad.MaxLength = 9
                    Else : Me.txtCantidad.MaxLength = 5
                    End If
                    If VerificaPalletAll(DataGetPallet) = True Then
                        If Not Me.VerificaCrossDock(Documento_Id, Nro_linea, NavCrossDock, OpCrossDock, NaveCod) Then
                            MsgBox("Ocurrio un error inesperado al verificar Si es una operacion de Cross Dock.")
                            Exit Sub
                        Else
                            If OpCrossDock = 1 Then
                                If MsgBox("¿Desea Ubicar el Pallet en la nave " & NaveCod & "?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                    'Ubico el pallet Manualmente
                                    Me.UbicacionId = NavCrossDock
                                    Me.PosicionId = 0
                                    Me.NaveId = NavCrossDock
                                    If Procesar(Me.UbicacionId) = True Then
                                        IngresoxCrossDock(Documento_Id, Nro_linea)
                                        'Verifico si hay mas pallets para ubicar con ese documento...
                                        If Verifica_Rem_CrossDock(Documento_Id, vRem) Then
                                            If vRem > 0 Then
                                                If MsgBox("Quedan pallets pendientes, ¿desea ubicarlos automaticamente en la posicion Default?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                                    'Ubico los remanentes.
                                                    If UbicarRemanenteCrossDock(Documento_Id, NavCrossDock) Then
                                                        InicializarFrm()
                                                        txtPallet.Focus()
                                                        'Fuerzo la salida para que no ejecute nada mas que esto.
                                                        Exit Try
                                                    End If
                                                Else
                                                    InicializarFrm()
                                                    txtPallet.Focus()
                                                    Exit Try
                                                End If
                                            Else : InicializarFrm()
                                                txtPallet.Focus()
                                                Exit Try
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        FG.Cmd = Cmd
                        FG.VerificaCantidad(Me.txtPallet.Text, IntCant)
                        'ESTA EN INTERMEDIA?
                        If Me.Intermedia(Me.txtPallet.Text, vInt) Then
                            If vInt = 1 Then
                                IntCant = 0
                            End If
                        End If

                        'If IntCant = 1 Then
                        '    GetCantidad()
                        '    Me.txtCantidad.Visible = True
                        '    Me.lblCantidad.Visible = True
                        '    lblMsg.Text = ""
                        '    Me.txtCantidad.Focus()
                        '    If Me.PosicionId <> 0 Then
                        '        Me.cmdLockPosition.Visible = True
                        '    End If
                        '    Exit Try
                        'Else
                        If cmdProducto.Visible = False Then
                            lblProducto.Text = lblProducto.Text & vbNewLine & GetCantidadSolicitada(Documento_Id, Nro_linea)
                        End If
                        'End If
                        If Not blnPedirCodigo Then
                            lblMsg.Text = ""
                            txtDestino.Visible = True
                            lblDestino.Visible = True
                            Me.txtDestino.Focus()
                            If Me.PosicionId <> 0 Then
                                Me.cmdLockPosition.Visible = True
                            End If
                        ElseIf (blnPedirCodigo) And (Not blnCodigoValido) And (Monoproducto) Then
                            Dim Control As frmCODIGOS
                            Control = New frmCODIGOS
                            Control.Producto = Me.lblProducto.Text
                            Control.DocumentoId = Documento_Id
                            Control.NroLinea = Nro_linea
                            Control.ShowDialog()
                            If Control.Cancel Then
                                'reload de todo
                                Control = Nothing
                                InicializarFrm()
                                Exit Try
                            Else
                                blnCodigoValido = True
                                lblMsg.Text = ""
                                txtDestino.Visible = True
                                lblDestino.Visible = True
                                Me.txtCantidad.ReadOnly = True
                                Me.cmdRechazar.Visible = False
                                Me.txtDestino.Focus()
                                Control = Nothing
                            End If
                        Else
                            blnCodigoValido = True
                            lblMsg.Text = ""
                            txtDestino.Visible = True
                            lblDestino.Visible = True
                            Me.txtCantidad.ReadOnly = True
                            Me.cmdRechazar.Visible = False
                            Me.txtDestino.Focus()
                        End If
                    Else
                        Me.NaveId = Nothing
                        Me.UbicacionId = Nothing
                        Me.Ubicacion = ""
                        txtPallet.Enabled = True
                        txtPallet.SelectAll()
                        txtDestino.Visible = False
                        lblDestino.Visible = False
                        lblProducto.Visible = False
                        cmdProducto.Visible = False
                        TxtCamada.Enabled = True
                        TxtCamada.Visible = False
                        TxtCamada.Text = ""
                        lblCamada.Visible = False
                    End If

                Else
                    lblProducto.Visible = False
                    cmdProducto.Visible = False
                    txtPallet.SelectAll()
                    Me.Ubicacion = ""
                    Me.UbicacionId = Nothing
                    Me.NaveId = Nothing
                    TxtCamada.Enabled = True
                    TxtCamada.Visible = False
                    TxtCamada.Text = ""
                    lblCamada.Visible = False

                End If
            End If
            'ESTOY CORTO DE TIEMPO.
            If Me.PosicionId <> 0 Then
                Me.cmdLockPosition.Visible = True
            Else
                Me.cmdLockPosition.Visible = False
            End If
        Catch ex As Exception
            MsgBox("BuscarUbicacion: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            FG = Nothing
            Cmd = Nothing
        End Try
    End Sub

    Private Function UbicarRemanenteCrossDock(ByVal vDocumento_Id As Long, ByVal NaveDef As Long) As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        Cmd = SQLc.CreateCommand
        Da = New SqlDataAdapter(Cmd)
        Dim i As Integer = 0
        Try
            If VerifyConnection(SQLc) Then

                Cmd.CommandText = "Get_Remanente_CrossDock"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Documento_id", SqlDbType.BigInt)
                Pa.Value = vDocumento_Id
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Da.Fill(Ds, "Pendientes")
                For i = 0 To Ds.Tables("Pendientes").Rows.Count - 1
                    Nro_linea = Ds.Tables("Pendientes").Rows(i)(1)
                    Me.txtPallet.Text = Ds.Tables("Pendientes").Rows(i)(2)
                    Me.UbicacionId = NaveDef
                    NaveId=navedef
                    Procesar(Me.UbicacionId)
                Next
            Else : MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("IngresoxCrossDock SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("IngresoxCrossDock: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Ds = Nothing
            Da = Nothing
            Cmd = Nothing
            Pa = Nothing
        End Try


    End Function

    Private Function Verifica_Rem_CrossDock(ByVal Documentoid As Long, ByRef Remanente As Long) As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Cmd = SQLc.CreateCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd.CommandText = "Verifica_Rem_CrossDock"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Documento_id", SqlDbType.BigInt)
                Pa.Value = Documentoid
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Remanente", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

                Remanente = Cmd.Parameters("@Remanente").Value
            Else : MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("IngresoxCrossDock SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("IngresoxCrossDock: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try

    End Function

    Private Function IngresoxCrossDock(ByVal vDocumentoId As Long, ByVal vNroLinea As Long) As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Cmd = SQLc.CreateCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd.CommandText = "Ingreso_CrossDock"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Documento_id", SqlDbType.BigInt)
                Pa.Value = vDocumentoId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Nro_Linea", SqlDbType.BigInt)
                Pa.Value = vNroLinea
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

            Else : MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("IngresoxCrossDock SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("IngresoxCrossDock: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function VerificaCrossDock(ByVal vDocumentoId As Long, ByVal vNroLinea As Long, _
                                       ByRef NavCrossDock As Long, ByRef OpCrossDock As String, ByRef NaveCod As String) As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Cmd = SQLc.CreateCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd.CommandText = "VerificaCrossDock"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Documento_id", SqlDbType.BigInt)
                Pa.Value = vDocumentoId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Nro_Linea", SqlDbType.BigInt)
                Pa.Value = vNroLinea
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Nav_CrossDock", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@UsaCrossDock", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NaveCod", SqlDbType.VarChar, 45)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)



                Cmd.ExecuteNonQuery()

                NavCrossDock = IIf(IsDBNull(Cmd.Parameters("@Nav_CrossDock").Value), 0, Cmd.Parameters("@Nav_CrossDock").Value)
                OpCrossDock = IIf(IsDBNull(Cmd.Parameters("@UsaCrossDock").Value), 0, Cmd.Parameters("@UsaCrossDock").Value)
                NaveCod = IIf(IsDBNull(Cmd.Parameters("@NaveCod").Value), "", Cmd.Parameters("@NaveCod").Value)
                If OpCrossDock = "1" Then
                    CrossDock = True
                Else
                    CrossDock = False
                End If
            Else : MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("VerificaCrossDock SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("VerificaCrossDock: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub txtPallet_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPallet.GotFocus

        If txtPallet.Enabled = False Then
            txtDestino.Focus()
        End If

    End Sub

    Private Sub txtPallet_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPallet.KeyUp
        Try
            Select Case e.KeyCode
                Case 13
                    Dim Ubica As String = ""
                    If Trim(Me.txtPallet.Text) <> "" Then
                        If Not ValidarSerializacion(Me.txtPallet.Text) Then
                            Me.txtPallet.Text = ""
                            Me.txtPallet.Focus()
                            Exit Sub
                        End If
                        UbicaPorCamada(Me.txtPallet.Text, Ubica)
                        If Ubica = "1" Then
                            txtPallet.Enabled = False
                            lblCamada.Visible = True
                            TxtCamada.Visible = True
                            TxtCamada.Focus()
                        ElseIf Ubica = "0" Then
                            BuscarUbicacion()
                        End If
                    End If
            End Select

        Catch ex As Exception
            MsgBox("txtPallet_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Function ValidarSerializacion(ByVal Pallet As String) As Boolean
        Dim xCMD As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCMD = SQLc.CreateCommand
                xCMD.CommandText = "[dbo].[VALIDA_TOMA_SERIES_PALLET]"
                xCMD.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@NRO_PALLET", SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                xCMD.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@RETORNO", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xCMD.Parameters.Add(Pa)

                xCMD.ExecuteNonQuery()

                If xCMD.Parameters("@RETORNO").Value = "1" Then
                    MsgBox("No es posible realizar el guardado del pallet sin haber cargado todas las series", MsgBoxStyle.Information, FrmName)
                    Return False
                Else
                    Return True
                End If

            Else : MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
            End If
        Catch SQLEx As SqlException
            MsgBox("ValidarSerializacion: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
        Catch ex As Exception
            MsgBox("ValidarSerializacion: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            xCMD = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub UbicaPorCamada(ByVal Pallet As String, ByRef UbicaCamada As String)
        Dim xCMD As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCMD = SQLc.CreateCommand
                xCMD.CommandText = "dbo.GetValuesForIngreso"
                xCMD.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Pallet", SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                xCMD.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Camada", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xCMD.Parameters.Add(Pa)
                xCMD.ExecuteNonQuery()

                UbicaCamada = IIf(IsDBNull(xCMD.Parameters("@Camada").Value), "0", xCMD.Parameters("@Camada").Value)
            Else : MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
            End If
        Catch SQLEx As SqlException
            MsgBox("UbicaPorCamada: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
        Catch ex As Exception
            MsgBox("UbicaPorCamada: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            xCMD = Nothing
            Pa = Nothing
        End Try
    End Sub


    Private Function Procesar(ByVal Ubicacion As Long) As Boolean
        Dim CI As New ClsIngreso
        Dim CA As New clsAceptar
        Dim Trans As SqlTransaction
        Dim Ds As New DataSet
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim Linea As Long = 0
        Dim Envase As Integer = 0
        Dim NavId As Integer = 0
        Dim PosId As Integer = 0
        Dim dSTMP As New DataSet
        Dim vError As String = ""
        Trans = SQLc.BeginTransaction
        Dim Cmd As SqlCommand
        Try
            lblMsg.Text = "Registrando ingreso..."
            lblMsg.Refresh()
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.Connection = SQLc
                Cmd.Transaction = Trans
                CI.objConnection = SQLc
                CI.Cmd = Cmd
                GC.Collect()
                GetValueForPalletMP(Me.txtPallet.Text, Documento_Id, Cmd, dSTMP)
                Cmd.Parameters.Clear()
                For I = 0 To dSTMP.Tables("TABLE").Rows.Count - 1
                    Nro_linea = dSTMP.Tables("TABLE").Rows(I)(0)
                    Envase = dSTMP.Tables("TABLE").Rows(I)(1)
                    If Envase = 0 Then
                        If Not CI.ExecuteAll(Documento_Id, Nro_linea, IIf(Me.PosicionId = 0, Nothing, Me.PosicionId), Me.NaveId) Then
                            Throw New Exception("Error en modulo ExecuteAll.")
                        End If

                    ElseIf Envase = 1 Then
                        If Not GetNaveOrPosition(Documento_Id, Nro_linea, dSTMP, Cmd) Then
                            Throw New Exception("Error en modulo ExecuteAll.")
                        End If
                        NavId = IIf(IsDBNull(dSTMP.Tables("Position").Rows(0)(0)), 0, dSTMP.Tables("Position").Rows(0)(0))
                        PosId = IIf(IsDBNull(dSTMP.Tables("Position").Rows(0)(1)), 0, dSTMP.Tables("Position").Rows(0)(1))
                        If Not CI.ExecuteAll(Documento_Id, Nro_linea, IIf(PosId = 0, Nothing, PosId), NavId) Then
                            Throw New Exception("Error en modulo ExecuteAll.")
                        End If
                    End If

                    If Not IngresaAuditoria(Documento_Id, Nro_linea, Cmd, strUbicSug, vError) Then
                        Throw New Exception(vError)
                    End If
                Next
                CA.DocumentoID = Documento_Id
                CA.Cmd = Cmd
                CA.NroLinea = Nro_linea
                CA.objConnection = SQLc
                CA.OperacionID = "ING"
                CA.UsuarioID = vUsr.CodUsuario
                If Not CA.Aceptar() Then
                    Throw New Exception("Error en modulo Aceptar.")
                End If
                If Not Sys_Dev(Documento_Id, 1, Cmd) Then
                    Throw New Exception("No se pudo completar la operacion Sys_Dev")
                End If
            Else
                'AGREGAR EL ERROR DE CONEXION
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
                Procesar = False
            End If
            Trans.Commit()
            txtDestino.Text = ""
            txtPallet.Text = ""
            lblDestino.Text = slblUbicacion
            lblMsg.Text = ""
            Me.Ubicacion = ""
            Me.UbicacionId = Nothing
            bFallo = False
            InicializarFrm()
            txtPallet.Focus()
            Procesar = True
        Catch ex As Exception
            Trans.Rollback()
            Procesar = False
            MsgBox("Procesar: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            CI = Nothing
            CA = Nothing
        End Try
    End Function

    Private Function IngresaAuditoria(ByVal Documento_Id As Long, ByVal Nro_Linea As Long, _
                                      ByRef xCmd As SqlCommand, ByVal Ubicacion As String, _
                                      ByRef vError As String) As Boolean
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd.Parameters.Clear()
                xCmd.CommandText = "AUDITORIA_HIST_INSERT_UBIC"
                xCmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@DOC", SqlDbType.BigInt)
                Pa.Value = Documento_Id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_LINEA", SqlDbType.BigInt)
                Pa.Value = Nro_Linea
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@UBIC", SqlDbType.VarChar, 45)
                Pa.Value = Trim(UCase(Ubicacion))
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@SWITCH", SqlDbType.Char, 1)
                Pa.Value = "1"
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                Return True
            Else : MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
        Catch SqlEx As SqlException
            vError = SqlEx.Message
            Return False
        Catch ex As Exception
            vError = ex.Message
            Return False
        Finally
            Pa = Nothing
        End Try
    End Function

    Private Function GetNaveOrPosition(ByVal Documento_id As Long, ByVal Nro_Linea As Integer, ByRef Ds As DataSet, _
                                       ByRef xCmd As SqlCommand) As Boolean
        Dim Da As New SqlDataAdapter(xCmd)
        Try
            xCmd.Parameters.Clear()
            xCmd.CommandText = "Get_Default_Envase"
            xCmd.CommandType = CommandType.StoredProcedure
            xCmd.Parameters.Add("@Documento_id", SqlDbType.Float).Value = Documento_id
            xCmd.Parameters.Add("@Nro_Linea", SqlDbType.VarChar, 30).Value = Nro_Linea
            Da.Fill(Ds, "Position")
            Return True
        Catch SQLEx As SqlException
            MsgBox("GetNaveOrPosition SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("GetNaveOrPosition: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Da = Nothing
        End Try
    End Function

    Private Sub FinalizarTarea()
        Try
            'Esta es la solucion mas rapida que encontre... 
            If (btnPublico = False) And (Me.Ubicacion = "") Then
                'Si puede ubicar forzadamente y si ubicacion no hay; entonces...
                Me.Ubicacion = Me.txtPallet.Text.Trim
            End If

            If txtPallet.Text.Trim <> "" And txtDestino.Text.Trim <> "" And Me.Ubicacion <> "" Then
                lblMsg.Text = ""
                If Trim(UCase(Me.txtDestino.Text)) = Trim(UCase(sUbicacion)) Then
                    'se deberá registrar el ingreso
                    If Procesar(Me.UbicacionId) = True Then
                        txtPallet.Focus()
                        If Me.CrossDock = "1" Then
                            IngresoxCrossDock(Documento_Id, Nro_linea)
                        End If
                    End If
                    InicializarFrm()
                Else
                    If btnPublico = True Then
                        PerfilPublicoIncorrecto()
                    Else
                        PerfilSupervisorIncorrecto()
                    End If
                End If
            Else

                If bFallo = False Then
                    lblMsg.Text = sCompletarCampos
                Else
                    bFallo = False
                End If

            End If
            'End If
        Catch ex As Exception
            MsgBox("FinalizarTarea: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub frmUbicacionMercaderia_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Try
            Select Case e.KeyCode
                Case Keys.F2
                    If lblNewMenu.Visible = False Then
                        'submenu
                        bFallo = False
                        Me.lblDestino.Text = slblUbicacion
                        Me.Ubicacion = ""
                        txtDestino.Text = ""
                        Me.lblMsg.Text = ""
                        Me.txtPallet.Text = ""
                        'txtPallet.ReadOnly = False
                        txtPallet.Enabled = True
                        InicializarFrm()
                        txtPallet.Focus()
                        Del_sys_Locator(Documento_Id, Nro_linea)
                        lblProducto.Visible = False
                        lblProducto.Text = strProducto
                    Else
                        'submenu cancelar
                        lblMsg.Text = sCancelado
                        lblMsg.Refresh()
                        txtDestino.Text = ""
                        Me.lblMsg.Text = ""
                        lblMenu.Visible = True
                        lblMenu.Refresh()
                        btnSubmenu = False
                        lblNewMenu.Visible = False
                        txtDestino.Enabled = True
                        txtDestino.Focus()
                        lblProducto.Visible = False
                        lblProducto.Text = strProducto
                        Del_sys_Locator(Documento_Id, Nro_linea)
                    End If
                Case Keys.F3
                    Del_sys_Locator(Documento_Id, Nro_linea)
                    frmPrincipal.Show()
                    Me.Close()
                Case Keys.F4
                    If Me.cmdRechazar.Visible Then
                        Instaciado_Proc()
                    End If
                Case Keys.F5
                    If Me.cmdRechazar.Visible Then
                        Rechazar()
                        Del_sys_Locator(Documento_Id, Nro_linea)
                    End If
                Case Keys.F6
                    Lockear()
            End Select

        Catch ex As Exception
            MsgBox("frmUbicacionMercaderia_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Sub
    Private Sub Instaciado_Proc()

        If Not instaciado Then
            instaciado = True
            Dim frmProducto As New frmProducto
            frmProducto.DataSet = DsP
            frmProducto.Pallet = txtPallet.Text
            frmProducto.ShowDialog()
            frmProducto = Nothing
            instaciado = False
            If Me.txtDestino.Visible Then
                txtDestino.Focus()
            ElseIf Me.txtCantidad.Visible Then
                Me.txtCantidad.Focus()
            End If
        End If

    End Sub
    Private Sub frmUbicacionMercaderia_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InicializarFrm()
        lblMsg.Text = ""
        Me.Text = FrmName
        txtPallet.Focus()
        'If bPermiso.Ubicacion_Supervisor = True Then
        '    Me.btnPublico = False
        'Else
        '    Me.btnPublico = True
        'End If
    End Sub

    Private Function VerificaPalletAll(ByRef Ds As DataSet) As Boolean
        Try
            Dim BlnTMP As Boolean = False
            'primero llamar a Mob_Verifica_Pallet
            lblMsg.Text = sBuscando
            lblMsg.Refresh()
            If VerificarExistenciaPallets(Me.txtPallet.Text.Trim, Ds) = True Then
                If Ds.Tables("GetDataForPallet").Rows.Count > 0 Then
                    'una vez q tengo valores llamo a  Mob_Verifica_Pallet
                    Me.Ubicacion = ""
                    Me.UbicacionId = 0
                    'Dim Documento_Id As Long
                    'Dim Nro_linea As Long
                    Documento_Id = Ds.Tables("GetDataForPallet").Rows(0)("DOCUMENTO_ID").ToString
                    Nro_linea = Ds.Tables("GetDataForPallet").Rows(0)("NRO_LINEA").ToString
                    If Not blnCodigoValido Then
                        VerificaCodigosIngreso(Documento_Id, Nro_linea)
                    End If
                    Ds = New DataSet
                    If Mob_Verifica_Pallet(Ds, Documento_Id, Nro_linea, Me.txtPallet.Text) = True Then
                        If Ds.Tables("Mob_Verifica_Pallet").Rows.Count = 0 Then
                            'si es =0 entonces llamar a Locator_Ing                
                            strUbicSug = ""
                            If Locator_Ing(Ds, Documento_Id, Nro_linea, txtPallet.Text, IIf(TxtCamada.Text = "", 0, TxtCamada.Text)) = True Then
                                'If Locator_Ing(Ds, Documento_Id, Nro_linea, txtPallet.Text) = True Then
                                If Not IsNothing(Ds.Tables("Locator_Ing")) Then
                                    If Ds.Tables("Locator_Ing").Rows.Count > 0 Then
                                        Me.PosicionId = Nothing
                                        Me.NaveId = Nothing
                                        If IsDBNull(Ds.Tables("Locator_Ing").Rows(0)("POSICION_ID")) Then
                                            Me.NaveId = IIf(IsDBNull(Ds.Tables("Locator_Ing").Rows(0)("NAVE_ID")), 0, Ds.Tables("Locator_Ing").Rows(0)("NAVE_ID"))
                                            Me.UbicacionId = IIf(IsDBNull(Ds.Tables("Locator_Ing").Rows(0)("NAVE_ID")), 0, Ds.Tables("Locator_Ing").Rows(0)("NAVE_ID"))
                                            Me.PosicionId = Nothing
                                        Else
                                            Me.UbicacionId = IIf(IsDBNull(Ds.Tables("Locator_Ing").Rows(0)("POSICION_ID")), 0, Ds.Tables("Locator_Ing").Rows(0)("POSICION_ID"))
                                            Me.PosicionId = IIf(IsDBNull(Ds.Tables("Locator_Ing").Rows(0)("POSICION_ID")), 0, Ds.Tables("Locator_Ing").Rows(0)("POSICION_ID"))
                                            Me.NaveId = Nothing
                                        End If
                                        strUbicSug = Ds.Tables("Locator_Ing").Rows(0)("POSICION_COD").ToString
                                        Me.Ubicacion = Ds.Tables("Locator_Ing").Rows(0)("POSICION_COD").ToString
                                        lblDestino.Text = slblUbicacion & Me.Ubicacion
                                    Else
                                        If btnPublico = True Then
                                            lblMsg.Text = "No existen posiciones asignadas"
                                            txtPallet.Enabled = True
                                            Me.txtPallet.SelectAll()
                                            Me.txtPallet.Focus()
                                            bFallo = True
                                            Return False
                                        Else
                                            lblDestino.Text = "Escanee la Ubicacion." 'slblUbicacion & ""
                                        End If
                                    End If
                                Else
                                    If btnPublico = True Then
                                        'MsgBox("No existen posiciones asignadas")
                                        lblMsg.Text = "No existen posiciones asignadas"
                                        txtPallet.Enabled = True
                                        Me.txtPallet.SelectAll()
                                        Me.txtPallet.Focus()
                                        bFallo = True
                                        Return False
                                    Else
                                        lblDestino.Text = "Escanee la Ubicacion." 'slblUbicacion & "N/A"
                                    End If
                                End If
                            Else
                                txtPallet.Enabled = True
                                '          Me.txtPallet.ReadOnly = False
                                Me.txtPallet.SelectAll()
                                Me.txtPallet.Focus()
                                bFallo = True
                                Return False
                            End If
                        Else
                            'si es <>0 entonces recupero el valor
                            If IsDBNull(Ds.Tables("Mob_Verifica_Pallet").Rows(0)("POSICION_ID")) Then
                                Me.NaveId = IIf(IsDBNull(Ds.Tables("Mob_Verifica_Pallet").Rows(0)("NAVE_ID")), 0, Ds.Tables("Mob_Verifica_Pallet").Rows(0)("NAVE_ID"))
                                Me.UbicacionId = IIf(IsDBNull(Ds.Tables("Mob_Verifica_Pallet").Rows(0)("NAVE_ID")), 0, Ds.Tables("Mob_Verifica_Pallet").Rows(0)("NAVE_ID"))
                                Me.PosicionId = Nothing
                            Else
                                Me.UbicacionId = IIf(IsDBNull(Ds.Tables("Mob_Verifica_Pallet").Rows(0)("POSICION_ID")), 0, Ds.Tables("Mob_Verifica_Pallet").Rows(0)("POSICION_ID"))
                                Me.PosicionId = IIf(IsDBNull(Ds.Tables("Mob_Verifica_Pallet").Rows(0)("POSICION_ID")), 0, Ds.Tables("Mob_Verifica_Pallet").Rows(0)("POSICION_ID"))
                                Me.NaveId = Nothing
                            End If

                            Me.Ubicacion = Ds.Tables("Mob_Verifica_Pallet").Rows(0)("POSICION_COD")
                            lblDestino.Text = slblUbicacion & Me.Ubicacion
                        End If
                    Else
                        txtPallet.Enabled = True
                        Me.txtPallet.SelectAll()
                        Me.txtPallet.Focus()
                        bFallo = True
                        Return False
                    End If
                Else
                    txtPallet.Enabled = True
                    Me.txtPallet.SelectAll()
                    Me.txtPallet.Focus()
                    bFallo = True
                    Return False
                End If
            Else
                DsP = Nothing
                DataGetPallet = Nothing
                DsP = New DataSet
                DataGetPallet = New DataSet
                txtPallet.Enabled = True
                Me.txtPallet.ReadOnly = False
                Me.txtPallet.SelectAll()
                Me.txtPallet.Focus()
                bFallo = True
                Return False
            End If
            'lblMsg.Text = ""
            bFallo = False
            Return True
        Catch ex As Exception
            bFallo = True
            MsgBox("VerificaPalletAll: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
            txtPallet.Enabled = True
            'txtPallet.ReadOnly = False
            Me.txtPallet.Focus()
        End Try
    End Function

    Private Function VerificaCodigosIngreso(ByVal DocId As Long, ByVal NroLinea As Long) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Dim xProcedure As String = ""
        Dim Ret As Integer = 0
        xCmd = SQLc.CreateCommand
        Try
            If VerifyConnection(SQLc) Then
                xProcedure = "Dbo.SolicitaCodigos"
                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = xProcedure
                Pa = New SqlParameter("@Documento_id", SqlDbType.BigInt)
                Pa.Value = DocId
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@nro_linea", SqlDbType.BigInt)
                Pa.Value = NroLinea
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@pOut", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()

                Ret = xCmd.Parameters("@pOut").Value
                If Ret = 1 Then
                    blnPedirCodigo = True
                    blnCodigoValido = False
                Else
                    blnPedirCodigo = False
                    blnCodigoValido = False
                End If
            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SqlEx As SqlException
            lblMsg.Text = SqlEx.Message
            bFallo = True
            Return False
        Catch ex As Exception
            MsgBox("VerificarExistenciaPallets: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function VerificarExistenciaPallets(ByVal NroPallet As String, ByRef Ds As DataSet) As Boolean
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Try

            If VerifyConnection(SQLc) Then
                If Ds Is Nothing Then
                    Ds = New DataSet
                End If
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "GetDataForPallet"
                Cmd.CommandType = CommandType.StoredProcedure
                Da = New SqlDataAdapter(Cmd)
                Pa = New SqlParameter("@NroPallet", SqlDbType.VarChar, 100)
                Pa.Value = NroPallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Usuario", SqlDbType.VarChar, 20)
                Pa.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(Pa)
                Da.Fill(Ds, "GetDataForPallet")
            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            lblMsg.Text = SQLEx.Message
            bFallo = True
            Return False
        Catch ex As Exception
            MsgBox("VerificarExistenciaPallets: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function Mob_Verifica_Pallet(ByRef Ds As DataSet, ByVal Documento_Id As Long, ByVal Nro_linea As Long, ByVal NroPallet As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Try

            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "Mob_Verifica_Pallet"
                Cmd.Connection = SQLc
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Documento_id", SqlDbType.Int)
                Pa.Value = Documento_Id
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Nro_linea", SqlDbType.Int)
                Pa.Value = Nro_linea
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NroPallet", SqlDbType.VarChar, 100)
                Pa.Value = NroPallet
                Cmd.Parameters.Add(Pa)

                Da.Fill(Ds, "Mob_Verifica_Pallet")
            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            bFallo = True
            lblMsg.Text = SQLEx.Message
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)


            Return False
        Catch ex As Exception

            MsgBox("Mob_Verifica_Pallet: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)

            Return False
        Finally
            Cmd = Nothing
            Da = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function Locator_Ing(ByRef Ds As DataSet, ByVal Documento_id As Long, ByVal Nro_linea As Long, ByVal Nro_Pallet As String, ByVal camada As Integer) As Boolean
        'Private Function Locator_Ing(ByRef Ds As DataSet, ByVal Documento_id As Long, ByVal Nro_linea As Long, ByVal Nro_Pallet As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Try

            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                'Cmd.CommandText = "Locator_Ing"
                Cmd.CommandText = "LOCATOR_ING_X_ALTURA"
                Cmd.Connection = SQLc
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Documento_id", SqlDbType.Int)
                Pa.Value = Documento_id
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Nro_linea", SqlDbType.Int)
                Pa.Value = Nro_linea
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NroPallet", SqlDbType.VarChar, 100)
                Pa.Value = Nro_Pallet
                Cmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@CANT", SqlDbType.Int)
                Pa.Value = camada
                Cmd.Parameters.Add(Pa)

                Da.Fill(Ds, "Locator_ing")
            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            lblMsg.Text = SQLEx.Message
            If btnPublico = False Then
                bFallo = False
                Return True
            Else
                bFallo = True
                Return False
            End If
        Catch ex As Exception
            MsgBox("Locator_Ing: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Da = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub PerfilPublicoIncorrecto()
        lblMsg.Text = vSubMenuPublico
        lblMsg.Visible = True
        lblMsg.Refresh()
        lblMenu.Visible = True
        btnSubmenu = False
        txtDestino.SelectAll()
        txtDestino.Focus()

    End Sub

    Private Function SupervisorConfirma() As Boolean
        Try
            Me.UbicacionId = BuscarPosicionId(UCase(txtDestino.Text.Trim))
            If Me.UbicacionId <> Nothing Then
                If Procesar(Me.UbicacionId) = True Then
                    'InicializarFrm()
                    Return True
                Else
                    'deberia borrar igual la posicion asignada
                    Del_sys_Locator(Documento_Id, Nro_linea)
                    Return False
                End If
            Else
                'deberia borrar igual la posicion asignada
                Del_sys_Locator(Documento_Id, Nro_linea)
                Return False
            End If
        Catch ex As Exception
            Return False
            MsgBox("SupervisorConfirma: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Function

    Private Sub PerfilSupervisorIncorrecto()
        Try
            Me.lblNewMenu.Text = vSubMenu
            Me.lblNewMenu.Visible = True
            txtPallet.Enabled = False
            txtDestino.Enabled = False
            btnSubmenu = True
            lblMenu.Visible = False
            'aca va el msgbox para que confirme o no
            If MsgBox("¿Confirma Ubicacion?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If SupervisorConfirma() = True Then
                    If Me.CrossDock = "1" Then
                        IngresoxCrossDock(Documento_Id, Nro_linea)
                    End If
                    txtDestino.Text = ""
                    txtPallet.Text = ""
                    lblMsg.Text = sFinalizado
                    lblMsg.Refresh()
                    lblMenu.Visible = True
                    lblMenu.Refresh()
                    btnSubmenu = False
                    Me.lblMsg.Text = ""
                    lblNewMenu.Visible = False
                    lblDestino.Text = slblUbicacion
                    txtPallet.Enabled = True
                    'txtPallet.ReadOnly = False
                    txtDestino.Enabled = True
                    InicializarFrm()
                    bFallo = False
                    txtPallet.Focus()
                Else
                    'txtDestino.Text = ""
                    lblMenu.Visible = True
                    lblMenu.Refresh()
                    'Me.lblMsg.Text = ""
                    lblNewMenu.Visible = False
                    txtDestino.Enabled = True
                    'txtDestino.SelectAll()
                    txtDestino.Focus()
                End If
            Else
                lblMsg.Text = sCancelado
                lblMsg.Refresh()
                txtDestino.Text = ""
                Me.lblMsg.Text = ""
                lblMenu.Visible = True
                lblMenu.Refresh()
                btnSubmenu = False
                lblNewMenu.Visible = False
                txtDestino.Enabled = True
                txtDestino.Focus()
            End If

        Catch ex As Exception
            MsgBox("PerfilSupervisorIncorrecto: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub txtDestino_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDestino.GotFocus
        txtDestino.SelectAll()
    End Sub

    Private Sub txtDestino_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDestino.KeyPress

    End Sub

    Private Sub txtDestino_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDestino.KeyUp
        Try
            Select Case e.KeyCode
                Case 13
                    If Trim(Me.txtDestino.Text) <> "" Then
                        FinalizarTarea()
                    End If
                Case Keys.F4
                    If cmdProducto.Visible = True Then
                        Dim frmProducto As New frmProducto
                        frmProducto.DataSet = DsP
                        frmProducto.Pallet = txtPallet.Text
                        frmProducto.ShowDialog()
                        frmProducto = Nothing
                        txtDestino.Focus()
                    End If
                Case Keys.F2

                    If lblNewMenu.Visible = False Then
                        'submenu
                        bFallo = False
                        Me.lblDestino.Text = slblUbicacion
                        Me.Ubicacion = ""
                        txtDestino.Text = ""
                        Me.lblMsg.Text = ""
                        Me.txtPallet.Text = ""
                        'txtPallet.ReadOnly = False
                        txtPallet.Enabled = True
                        InicializarFrm()
                        txtPallet.Focus()
                        Del_sys_Locator(Documento_Id, Nro_linea)
                        lblProducto.Visible = False
                        lblProducto.Text = strProducto
                    Else
                        'submenu cancelar
                        lblMsg.Text = sCancelado
                        lblMsg.Refresh()
                        txtDestino.Text = ""
                        Me.lblMsg.Text = ""
                        lblMenu.Visible = True
                        lblMenu.Refresh()
                        btnSubmenu = False
                        lblNewMenu.Visible = False
                        txtDestino.Enabled = True
                        txtDestino.Focus()
                        lblProducto.Visible = False
                        lblProducto.Text = strProducto
                    End If
                Case Keys.F3
                    Del_sys_Locator(Documento_Id, Nro_linea)
                    frmPrincipal.Show()
                    Me.Close()
            End Select
        Catch ex As Exception
            MsgBox("txtDestino_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub
    Private Sub GetCantidad()
        Dim Cmd As SqlCommand
        Dim Fg As New FuncionesGenerales

        Cmd = SQLc.CreateCommand
        Fg.Cmd = Cmd
        Fg.GetCantidadSolicitada(Documento_Id, Nro_linea, dblCantidad)
        Cmd = Nothing
        Fg = Nothing

    End Sub
    Private Function GetCantidadSolicitada(ByVal Documento_Id, ByVal Nro_linea) As String
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim sCadena As String

        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "GET_CANTIDAD_SOLICITADA_DEV"
                Cmd.CommandType = CommandType.StoredProcedure

                Cmd.Parameters.Add("@DOCUMENTOID", SqlDbType.BigInt).Value = Documento_Id
                Cmd.Parameters.Add("@NROLINEA", SqlDbType.BigInt).Value = Nro_linea

                Pa = New SqlParameter("@CANT", SqlDbType.Int)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

                sCadena = ""
                sCadena = "Cantidad: " & IIf(IsDBNull(Cmd.Parameters("@CANT").Value), 0, Cmd.Parameters("@CANT").Value)

                Return sCadena

            Else : MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
                sCadena = ""
                Return sCadena
            End If

        Catch SQLEx As SqlException
            'MsgBox("Intermedia SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            sCadena = ""
            Return sCadena

        Catch ex As Exception
            'MsgBox("Intermedia : " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            sCadena = ""
            Return sCadena

        Finally
            Pa = Nothing
            Cmd = Nothing

        End Try

    End Function


    Private Sub txtPallet_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPallet.TextChanged
        bFallo = False
        Me.Ubicacion = ""
        lblDestino.Text = slblUbicacion
        txtDestino.Text = ""
        lblMsg.Text = ""
        cmdProducto.Visible = False
    End Sub

    Private Sub txtDestino_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDestino.TextChanged
        lblMsg.Text = ""

    End Sub

    Private Function BuscarPosicionId(ByVal Posicion_Codigo As String) As Long
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim Posicion_Id As Long
        Try

            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "MobBuscarPosicion"
                Cmd.Connection = SQLc
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@POSICION_COD", SqlDbType.VarChar, 45)
                Pa.Value = Posicion_Codigo
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Da.Fill(Ds, "MobBuscarPosicion")
                Posicion_Id = Ds.Tables("MobBuscarPosicion").Rows(0)("POSICION_ID").ToString()

                If Ds.Tables("MobBuscarPosicion").Rows(0)("TIPO").ToString() = "POS" Then
                    Me.PosicionId = Ds.Tables("MobBuscarPosicion").Rows(0)("POSICION_ID").ToString()
                    Me.NaveId = Nothing
                Else
                    If Ds.Tables("MobBuscarPosicion").Rows(0)("TIPO").ToString() = "NAVE" Then
                        Me.NaveId = Ds.Tables("MobBuscarPosicion").Rows(0)("POSICION_ID").ToString()
                        Me.PosicionId = Nothing
                    End If
                End If
                Return Posicion_Id
            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                bFallo = True
                Return Nothing
            End If

        Catch SQLEx As SqlException
            txtDestino.Text = ""
            lblMsg.Text = SQLEx.Message
            'MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            bFallo = True
            Return Nothing
        Catch ex As Exception
            bFallo = True
            MsgBox("BuscarPosicionId: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return Nothing
        Finally
            Cmd = Nothing
            Da = Nothing
            Pa = Nothing
        End Try

    End Function

    Private Function Del_sys_Locator(ByVal DocumentoID As Long, ByVal NroLinea As Long) As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand

        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.Connection = SQLc
                Cmd.CommandText = "MOB_ELIMINAR_LOCATOR_ING"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Documento_Id", SqlDbType.Int)
                Pa.Value = DocumentoID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Nro_linea", SqlDbType.Int)
                Pa.Value = NroLinea
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
                Return True
            End If

        Catch SQLEx As SqlException

            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception

            MsgBox("Del_sys_Locator: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try

    End Function
    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Try
            If lblNewMenu.Visible = False Then
                'submenu
                bFallo = False
                Me.lblDestino.Text = slblUbicacion
                Me.Ubicacion = ""
                txtDestino.Text = ""
                Me.lblMsg.Text = ""
                Me.txtPallet.Text = ""
                txtPallet.Enabled = True
                'txtPallet.ReadOnly = False
                InicializarFrm()
                txtPallet.Focus()
                Del_sys_Locator(Documento_Id, Nro_linea)

            Else
                'submenu cancelar
                lblMsg.Text = sCancelado
                lblMsg.Refresh()
                txtDestino.Text = ""
                Me.lblMsg.Text = ""
                lblMenu.Visible = True
                lblMenu.Refresh()
                btnSubmenu = False
                lblNewMenu.Visible = False
                txtDestino.Enabled = True
                txtDestino.Focus()

            End If
            lblProducto.Visible = False
            lblProducto.Text = strProducto

        Catch ex As Exception
            MsgBox("cmdCancelar_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click

        Try
            Del_sys_Locator(Documento_Id, Nro_linea)
            'frmPrincipal.Show()
            Me.Close()
        Catch ex As Exception
            MsgBox("cmdCancelar_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Sub
    Private Sub cmdProducto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProducto.Click

        If Me.cmdProducto.Visible Then Instaciado_Proc()
        'If Not instaciado Then
        '    instaciado = True
        '    Dim frmProducto As New frmProducto
        '    frmProducto.DataSet = DsP
        '    frmProducto.Pallet = txtPallet.Text
        '    frmProducto.ShowDialog()
        '    frmProducto = Nothing
        '    instaciado = False
        '    If Me.txtDestino.Visible Then
        '        txtDestino.Focus()
        '    ElseIf Me.txtCantidad.Visible Then
        '        Me.txtCantidad.Focus()
        '    End If
        'End If

    End Sub
    Private Sub txtPallet_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPallet.Validating

        txtPallet.SelectAll()

    End Sub
    Private Sub txtDestino_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtDestino.Validating
        txtDestino.SelectAll()
    End Sub
    Private Sub txtCantidad_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCantidad.GotFocus
        If Me.txtCantidad.ReadOnly = True Then
            Me.txtDestino.Focus()
        End If
    End Sub

    Private Sub txtCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress
        Dim Search As String
        Dim Pos As Integer
        Search = "."
        If Not xEsFraccionable Then
            ValidarCaracterNumerico(e)
        Else
            Pos = InStr(1, Me.txtCantidad.Text, Search)
            If Pos > 0 And Asc(e.KeyChar) <> 46 Then
                If Len(Mid(Me.txtCantidad.Text, Pos + 1, Len(Me.txtCantidad.Text))) >= 5 And Asc(e.KeyChar) <> 8 Then
                    e.Handled = True
                    Me.txtCantidad.Focus()
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

    Private Sub txtCantidad_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyUp
        Dim Cod As String = ""
        Dim Control As frmCODIGOS
        If Me.txtCantidad.Visible = True And Me.txtCantidad.Text <> "" Then
            If e.KeyCode = 13 Then
                If CDbl(Me.txtCantidad.Text) = dblCantidad Then
                    If Not blnPedirCodigo Then
                        lblMsg.Text = ""
                        txtDestino.Visible = True
                        lblDestino.Visible = True
                        Me.txtCantidad.ReadOnly = True
                        Me.cmdRechazar.Visible = False
                        Me.txtDestino.Focus()
                    ElseIf Not blnCodigoValido Then
                        Control = New frmCODIGOS
                        Control.DocumentoId = Documento_Id
                        Control.NroLinea = Nro_linea
                        Control.Producto = Me.lblProducto.Text
                        Control.ShowDialog()
                        If Control.Cancel Then
                            'reload de todo
                            InicializarFrm()
                            Control = Nothing
                        Else
                            blnCodigoValido = True
                            lblMsg.Text = ""
                            txtDestino.Visible = True
                            lblDestino.Visible = True
                            Me.txtCantidad.ReadOnly = True
                            Me.cmdRechazar.Visible = False
                            Me.txtDestino.Focus()
                            Control = Nothing
                        End If
                    End If
                Else
                    Me.cmdRechazar.Visible = True
                    Me.txtCantidad.SelectAll()
                    lblMsg.Text = "La cantidad ingresada es incorrecta."
                End If
            End If
        End If

    End Sub
    Private Sub txtCantidad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.TextChanged

    End Sub
    Private Function Sys_Dev(ByVal DocumentoId As Long, ByVal Estado As Integer, ByRef oCmd As SqlCommand) As Boolean

        Try
            If VerifyConnection(SQLc) Then
                oCmd.Parameters.Clear()
                oCmd.CommandText = "Sys_dev"
                oCmd.CommandType = CommandType.StoredProcedure
                oCmd.Parameters.Add("@Documento_ID", SqlDbType.Int).Value = DocumentoId
                oCmd.Parameters.Add("@Estado", SqlDbType.Int).Value = Estado
                oCmd.ExecuteNonQuery()
            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True

        Catch SQLEx As SqlException
            'MsgBox("Sys_Dev SQL - " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            'MsgBox("Sys_Dev - " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        End Try

    End Function

    Private Function Rechazar() As Boolean
        Dim Cmd As SqlCommand
        Dim Rta As Object

        Try
            Rta = MsgBox("Confirma el rechazo?", MsgBoxStyle.YesNo, FrmName)
            If Rta = vbNo Then
                Return True
            End If
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                If Me.cmdRechazar.Visible = True Then
                    Sys_Dev(Documento_Id, 2, Cmd)
                    InicializarFrm()
                End If
            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("Sys_Dev SQL - " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("Sys_Dev - " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Cmd = Nothing
        End Try

    End Function
    Private Sub cmdRechazar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRechazar.Click
        If Me.cmdRechazar.Visible Then
            Rechazar()
            Del_sys_Locator(Documento_Id, Nro_linea)
        End If
    End Sub
    Private Sub GetValueForPalletMP(ByVal Pallet As String, ByVal DocumentoID As Long, ByVal xCmd As SqlCommand, ByVal dS As DataSet)
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim vInt As Integer = 0

        Try
            If VerifyConnection(SQLc) Then
                Da = New SqlDataAdapter(xCmd)

                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = "GETCANTPROD"

                Pa = New SqlParameter("@documento_id", SqlDbType.Int)
                Pa.Value = DocumentoID
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@pallet", SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Out", SqlDbType.Int)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                Da.Fill(dS, "table")
                'xCmd.ExecuteNonQuery()
                'vInt = IIf(IsDBNull(xCmd.Parameters("@out").Value), 1, xCmd.Parameters("@out").Value)
                'Return vInt
            End If

        Catch ex As Exception
            Me.lblMsg.Text = "GetValueForPalletMP: " & ex.Message
        Finally
            Pa = Nothing
        End Try
    End Sub

    Private Function Intermedia(ByVal Pallet As String, ByRef vInter As Integer) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter

        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Mob_VerificaIntermedia"
                Cmd.CommandType = CommandType.StoredProcedure

                Cmd.Parameters.Add("@pallet", SqlDbType.VarChar, 100).Value = Pallet
                Pa = New SqlParameter("@out", SqlDbType.Int)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

                vInter = IIf(IsDBNull(Cmd.Parameters("@Out").Value), 0, Cmd.Parameters("@Out").Value)

            Else : MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("Intermedia SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("Intermedia : " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLockPosition.Click
        Lockear()
    End Sub

    Private Sub Lockear()
        Dim p As New frmLockPosicion
        Dim blnTMp As Boolean
        p.PosicionCod = Me.Ubicacion
        p.PosicionLCK = Me.PosicionId
        If Me.PosicionId <> 0 Then
            p.ShowDialog()
            If p.vLock Then
                DsP = Nothing
                DsP = New DataSet
                blnTMp = blnCodigoValido
                BuscarUbicacion()
                blnCodigoValido = blnTMp
            Else
                Me.txtDestino.Focus()
            End If
        Else
            Me.lblMsg.Text = "No es posible lockear una nave."
        End If
        p = Nothing
    End Sub
    Private Function ValidarNumero(ByVal caracter As String) As Boolean
        Dim expReg As String = "[0-9]" 'valida numero
        Dim val As New System.Text.RegularExpressions.Regex(expReg)
        Return val.IsMatch(caracter)
    End Function

    Private Sub TxtCamada_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtCamada.KeyPress
        'e.Handled = Not ValidarNumero(e.KeyChar)
        If Not (ValidarNumero(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtCamada_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCamada.KeyUp
        If e.KeyCode = Keys.Enter Then
            If TxtCamada.Text.Length > 0 Then
                lblMsg.Text = ""
                BuscarUbicacion()
            Else
                lblMsg.Text = "Debe ingresar camanda"
                lblMsg.Visible = True
                TxtCamada.Focus()
            End If
        End If
    End Sub

    Private Sub lblMenu_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMenu.ParentChanged

    End Sub

    Private Sub lblDestino_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblDestino.ParentChanged

    End Sub
End Class

