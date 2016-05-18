Imports System.Data.SqlClient
Imports System.Data

Public Class frmEgreso

    Enum DsE
        ViajeID = 0
        ProductoId = 1
        Descripcion = 2
        Qty = 3
        PosicionCod = 4
    End Enum

#Region "Declaraciones"
    Private blnConversion As Boolean = False
    Private intFactorConversion As Integer
    Private Const FrmName As String = "Picking."
    Private Const SQLConErr As String = "No se pudo conectar a la base de datos."
    Private Const vMenu As String = "F1) Completados." & vbTab & "F2) Salir."
    Private blnSalida As Boolean = True
    Private Ubicacion As String = ""
    Private UbicacionID As Long = 0
    Private DsEgreso As Data.DataSet
    Private PalletId As Long = 0
    Private ClienteId As String = "0"
    Private ViajeId As String = "0"
    Private Rl_Id As Long = 0
    Private PalletProp1 As String = ""
    Private vContCantInf As Integer = 0
    Private vRuta As String = ""
    Private xEsFraccionable As Boolean = False
    Private PickVehiculo As Boolean = False
    Private BlnValCod As Boolean = False
    Private BlnValidaCodigoPick As Boolean = False
    Private strCodigoPick As String = ""
    Private PalletCompleto As Boolean = False
    Private SolicitaCalleNave As Boolean = False
    Private DS_Series As DataSet
    Private ProdConSerieEgr As Boolean
    Private BlnSolicitaConfirmacion As Boolean
    Private SolicitaLote As Boolean = False
    Private NroLote As String = ""
    Private flagContenedora As Boolean
    Private pCantidadMaxCont As Double
    Private DocumentoId As Double = 0
    Private vLote_proveedor As String
    Private vPartida As String
    Private vSerie As String
    Private vPickingID As String
    Private vNroSerieStock As String
    Private vSerieObligatoria As String
    Private vNroBulto As String
    Private flg_serie_egreso As String
    Private CambioContenedora As Boolean = False
    Private blnSerializadoBestFit As Boolean = False
    Private blnConfirmoPalletOK As Boolean = False
#End Region

    Public Property SolCalleNave() As Boolean
        Get
            Return SolicitaCalleNave
        End Get
        Set(ByVal value As Boolean)
            SolicitaCalleNave = value
        End Set
    End Property

    Public Property PickingPalletCompleto() As Boolean
        Get
            Return PalletCompleto
        End Get
        Set(ByVal value As Boolean)
            PalletCompleto = value
        End Set
    End Property

    Public Property PickingVehiculos() As Boolean
        Get
            Return PickVehiculo
        End Get
        Set(ByVal value As Boolean)
            PickVehiculo = value
        End Set
    End Property

    Public Property SolicitaConfirmacion() As Boolean
        Get
            Return BlnSolicitaConfirmacion
        End Get
        Set(ByVal value As Boolean)
            BlnSolicitaConfirmacion = value
        End Set
    End Property

    Public Property IsContenedora() As Boolean
        Get
            Return FlagContenedora
        End Get
        Set(ByVal value As Boolean)
            flagContenedora = value
        End Set
    End Property

    Public Property CantidadMaxCont() As Double
        Get
            Return pCantidadMaxCont
        End Get
        Set(ByVal value As Double)
            pCantidadMaxCont = value
        End Set
    End Property

    Private Sub frmEgreso_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                If Me.cmdApertura.Enabled = True Then
                    AbrirPallet()
                End If
            Case Keys.F2 'cerrar pallet
                CerrarPallet()
            Case Keys.F3
                MostrarFinalizados()
            Case Keys.F4
                JumpPick()
            Case Keys.F5
                ValidacionSalida()
            Case Keys.F6
                CambioVehiculo()
            Case Keys.F7
                CambioCalle()
            Case Keys.F8
                AbrirPickingContenedoras()
            Case Keys.F9
                ActivaSerieEspecifica()
            Case Keys.F10
                BuscarConversiones()
        End Select
    End Sub

    Private Sub CerrarPallet()
        If Me.cmdApertura.Enabled = False Then
            If VerifyConnection(SQLc) Then
                Dim Cmd As SqlCommand
                Dim Fe As New FuncionesEgreso
                Cmd = SQLc.CreateCommand
                Fe.Cmd = Cmd
                'MsgBox("Viaje: " & ViajeId)
                'MsgBox("Producto: " & Me.txtProducto_ID.Text)
                'MsgBox("Ubicacion: " & Me.Ubicacion)
                'MsgBox("Pallet: " & Me.PalletProp1)
                'MsgBox("Usuario: " & vUsr.CodUsuario)
                'MsgBox("Ruta: " & vRuta)
                If Fe.Cerrar_Pallet(ViajeId, Me.txtProducto_ID.Text, Me.Ubicacion, Me.PalletProp1, Me.PalletId, vUsr.CodUsuario, vRuta) Then
                    Me.cmdCerrarPallet.Enabled = False
                    ImpresionPallet(2)
                    ClearAll(True)
                    Me.cmdSaltoPicking.Enabled = False
                End If
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
            End If
        End If
    End Sub

    Public Function GetPalletoCarro(ByRef Pallet_ID As String, ByRef vCmd As SqlCommand) As Boolean
        Try
            Dim fe As New FuncionesEgreso
            fe.Cmd = vCmd
            If fe.PickingUsaCarro Then
                Dim frmCarro As New frmNroCarro
                'nro de carro
                frmCarro.sViaje = ViajeId
                frmCarro.ShowDialog()
                If frmCarro.isCancel Then
                    CerrarPallet()
                    Me.Show()
                    Exit Function
                Else
                    Pallet_ID = frmCarro.txtNroCarro.Text
                    'guardo el carro en la base para reservarlo
                    'fe.SetCarro(ViajeId, Me.txtProducto_ID.Text, Me.Ubicacion, Me.PalletProp1, Me.PalletId, vUsr.CodUsuario, vRuta)
                    frmCarro.Close()
                    Return True
                End If
            Else
                fe.GetNumberofPallet(Pallet_ID)
                Return True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Private Sub AbrirPallet()
        ClearAll(True)
        Dim Fe As New FuncionesEgreso
        Dim Cmd As SqlCommand

        If VerifyConnection(SQLc) Then
            Me.btn_Series_Esp.Text = "F9) Series Esp.: On"
            Cmd = SQLc.CreateCommand
            Fe.Cmd = Cmd
            If Not PickVehiculo Then
                'Reviso que no tenga pendientes... picking comun.
                If Fe.Picking_Pendiente(vUsr.CodUsuario, PickingPalletCompleto, DsEgreso, "pcur") Then
                    If DsEgreso.Tables("PCUR") IsNot Nothing Then
                        Me.cmdCerrarPallet.Enabled = True
                        If DsEgreso.Tables("PCUR").Rows(0)("Pallet_picking") IsNot Nothing And Not IsDBNull(DsEgreso.Tables("PCUR").Rows(0)("Pallet_picking")) Then
                            PalletId = DsEgreso.Tables("PCUR").Rows(0)("Pallet_picking")
                            Me.lblPallet.Text = "Pallet Picking: " & PalletId
                        Else
                            Me.lblPallet.Text = "Pallet Picking: " & PalletId
                        End If
                        Me.lblPallet.Visible = True
                        Me.cmdApertura.Enabled = False
                        Me.cmdSaltoPicking.Enabled = True
                        SetValuesScreen()
                        Exit Sub
                    End If
                End If
                If GetPalletoCarro(PalletId, Cmd) Then
                    If DsEgreso.Tables("PCUR") Is Nothing Then
                        If Fe.GetTareasPicking(vUsr.CodUsuario, ViajeId, DsEgreso, "pcur", PalletId, vRuta, PickingPalletCompleto, vUsr.NaveCalle) Then
                            SetValuesScreen()
                            Me.cmdSaltoPicking.Enabled = True
                        End If
                    Else
                        Me.cmdCerrarPallet.Enabled = True
                        Me.lblPallet.Text = "Pallet: " & PalletId
                        Me.lblPallet.Visible = True
                        Me.cmdApertura.Enabled = False
                        SetValuesScreen()
                    End If
                End If
            ElseIf (PickVehiculo) And (SolCalleNave) = True Then
                '===============================================================================================
                'Reviso que no tenga nada pendiente picking con vh.
                '===============================================================================================
                If Fe.Picking_Pendiente_VH(vUsr.CodUsuario, PickingPalletCompleto, vUsr.NaveCalle, vUsr.Vehiculo, DsEgreso, "pcur") Then
                    If DsEgreso.Tables("PCUR") IsNot Nothing Then
                        Me.cmdCerrarPallet.Enabled = True
                        If DsEgreso.Tables("PCUR").Rows(0)("Pallet_picking") IsNot Nothing And Not IsDBNull(DsEgreso.Tables("PCUR").Rows(0)("Pallet_picking")) Then
                            PalletId = DsEgreso.Tables("PCUR").Rows(0)("Pallet_picking")
                            Me.lblPallet.Text = "Pallet Picking: " & PalletId
                        Else
                            GetPalletoCarro(PalletId, Cmd)
                            Me.lblPallet.Text = "Pallet Picking: " & PalletId
                        End If
                        Me.lblPallet.Visible = True
                        Me.cmdApertura.Enabled = False
                        Me.cmdSaltoPicking.Enabled = True
                        SetValuesScreen()
                        Exit Sub
                    End If
                End If
                If GetPalletoCarro(PalletId, Cmd) Then
                    If DsEgreso.Tables("PCUR") Is Nothing Then
                        If Fe.GetTareasPicking(vUsr.CodUsuario, ViajeId, DsEgreso, "pcur", PalletId, vRuta, PickingPalletCompleto, vUsr.NaveCalle) Then
                            SetValuesScreen()
                            Me.cmdSaltoPicking.Enabled = True
                        End If
                    Else
                        Me.cmdCerrarPallet.Enabled = True
                        Me.lblPallet.Text = "Pallet: " & PalletId
                        Me.lblPallet.Visible = True
                        Me.cmdApertura.Enabled = False
                        SetValuesScreen()
                    End If
                End If
            ElseIf (PickingVehiculos) And (SolCalleNave = False) Then
                'A partir de aca tengo q recuperar tareas.
                If Fe.Picking_Pendiente(vUsr.CodUsuario, PickingPalletCompleto, DsEgreso, "pcur") Then
                    If DsEgreso.Tables("PCUR") IsNot Nothing Then
                        Me.cmdCerrarPallet.Enabled = True
                        If DsEgreso.Tables("PCUR").Rows(0)("Pallet_picking") IsNot Nothing And Not IsDBNull(DsEgreso.Tables("PCUR").Rows(0)("Pallet_picking")) Then
                            PalletId = DsEgreso.Tables("PCUR").Rows(0)("Pallet_picking")
                            Me.lblPallet.Text = "Pallet Picking: " & PalletId
                        Else
                            GetPalletoCarro(PalletId, Cmd)
                            Me.lblPallet.Text = "Pallet Picking: " & PalletId
                        End If
                        Me.lblPallet.Visible = True
                        Me.cmdApertura.Enabled = False
                        Me.cmdSaltoPicking.Enabled = True
                        SetValuesScreen()
                        Exit Sub
                    End If
                End If
                If GetPalletoCarro(PalletId, Cmd) Then
                    If DsEgreso.Tables("PCUR") Is Nothing Then
                        If Fe.GetTareasPicking(vUsr.CodUsuario, ViajeId, DsEgreso, "pcur", PalletId, vRuta, PickingPalletCompleto, vUsr.NaveCalle) Then
                            SetValuesScreen()
                            Me.cmdSaltoPicking.Enabled = True
                        End If
                    Else
                        Me.cmdCerrarPallet.Enabled = True
                        Me.lblPallet.Text = "Pallet: " & PalletId
                        Me.lblPallet.Visible = True
                        Me.cmdApertura.Enabled = False
                        SetValuesScreen()
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub AbrirPickingContenedoras()
        Dim FrmPC As New frmContenedorasPicking
        Dim Fe As New FuncionesEgreso
        Dim Cmd As SqlCommand
        Try
            FrmPC.Producto_ID = Me.txtProducto_ID.Text
            FrmPC.DescripcionProducto = Me.txtDescripcion.Text
            FrmPC.CantidadSolicitada = Me.txtCantidad.Text
            If (MsgBox("¿Desea respetar el Nro.Lote/Nro.Partida?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes) Then
                FrmPC.RespetaLotePartida = "1"
            Else
                FrmPC.RespetaLotePartida = "0"
            End If
            FrmPC.Viaje_ID = ViajeId
            FrmPC.Unidad = Me.lblUnidad.Text
            FrmPC.ShowDialog()
            If Not FrmPC.ContenedoraUbicacion Is Nothing Or FrmPC.ContenedoraUbicacion <> "" Then
                CambioContenedora = FrmPC.RealizoCambio
                If (FrmPC.RealizoCambio) Then
                    Me.lblPosicion.Visible = True
                    Me.txtPosicion.Visible = True
                    Me.txtPosicion.Text = FrmPC.ContenedoraUbicacion
                    Me.lblPosicion.Text = "Ubicacion: " & FrmPC.ContenedoraUbicacion
                    Me.Ubicacion = FrmPC.ContenedoraUbicacion
                    CantidadMaxCont = FrmPC.CantidadMaxCont.ToString
                    Cmd = SQLc.CreateCommand
                    Fe.Cmd = Cmd
                    If Fe.GetCantidadxProducto(Me.ViajeId.ToString, Me.txtProducto_ID.Text.ToString) < CantidadMaxCont Then
                        Me.txtCantidad.Text = CStr(Fe.GetCantidadxProducto(Me.ViajeId.ToString, Me.txtProducto_ID.Text.ToString))
                    Else
                        Me.txtCantidad.Text = CantidadMaxCont
                    End If
                    Rl_Id = FrmPC.RL_Id
                    Me.lblContenedora.Text = "Contenedora: " & FrmPC.Nro_Contenedora
                    Me.txtPosicion.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox("Ocurrio un error en la rutina AbrirPickingContenedoras", MsgBoxStyle.Critical, FrmName)
        Finally
            Fe = Nothing
            Cmd = Nothing
            FrmPC.Dispose()
        End Try
    End Sub

    Private Sub MostrarFinalizados()
        Dim frmF As New frmFinalizados
        Dim Cmd As SqlCommand
        If VerifyConnection(SQLc) Then
            Cmd = SQLc.CreateCommand
            frmF.ViajeId = Me.ViajeId
            frmF.Cmd = Cmd
            frmF.ShowDialog()
            frmF = Nothing
            Cmd = Nothing
        End If
    End Sub

    Private Sub ValidacionSalida()
        lblMsg.Text = ""
        If blnSalida = True Then
            Dim Rta As Object = MsgBox("Desea salir de Picking?", MsgBoxStyle.YesNo, FrmName)
            If Rta = vbYes Then
                Me.Close()
            Else
                If Me.txtCCantidad.Visible = True Then
                    Me.txtCCantidad.Focus()
                ElseIf Me.txtCCantidad.Visible = False Then
                    Me.txtPosicion.Focus()
                End If
            End If
        Else
            lblMsg.Text = "Debe cerrar el pallet para salir."
        End If
    End Sub

    Private Sub frmEgreso_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IniForm()
    End Sub

    Private Sub SetValuesScreen()
        Dim val As New clsGuardadoManual
        Dim Cmd As SqlCommand
        Dim Fe As New FuncionesEgreso
        Dim BlnConfirmoPallet As Boolean = False
        Try
            Me.lblDatoLote.Text = "Label1"
            Me.lblDatoPartida.Text = "Label1"
            If DsEgreso.Tables("pcur") IsNot Nothing Then
                Try
                    If IsDBNull(DsEgreso.Tables("pcur").Rows(0)("NRO_LOTE")) Then
                        NroLote = ""
                        SolicitaLote = False
                    Else
                        SolicitaLote = True
                        NroLote = DsEgreso.Tables("pcur").Rows(0)("NRO_LOTE").ToString
                    End If
                Catch ex As Exception
                End Try

                blnConfirmoPalletOK = False
                blnSerializadoBestFit = False
                Me.lblPallet.Text = "Pallet Picking: " & PalletId
                Me.lblPallet.Visible = True
                Me.cmdApertura.Enabled = False
                Me.cmdCerrarPallet.Enabled = True
                blnSalida = False
                Me.lblPallet.Visible = True
                Me.lblPalletIng.Visible = True
                Me.lblProducto_ID.Visible = True
                Me.txtProducto_ID.Visible = True
                Me.lblDescripcion.Visible = True
                Me.txtDescripcion.Visible = True
                Me.lblCantidad.Visible = True
                Me.txtCantidad.Visible = True
                Me.lblPosicion.Visible = True
                Me.txtPosicion.Visible = True
                Me.txtPosicion.Text = ""
                Me.txtPosicion.ReadOnly = False
                Me.lblPosicion.Text = "Ubicacion : "
                Me.lblCCantidad.Visible = False
                Me.txtCCantidad.Text = ""

                'DocumentoId = CDbl(DsEgreso.Tables("PCUR").Rows(0)("DOCUMENTO_ID"))
                EsFraccionable(vUsr.ClienteActivo, DsEgreso.Tables("pcur").Rows(0)("PRODUCTO_ID").ToString, xEsFraccionable, SQLc)
                If xEsFraccionable Then
                    Me.txtCCantidad.MaxLength = 15
                Else : Me.txtCCantidad.MaxLength = 20
                End If
                Application.DoEvents()
                Try
                    If Not (DsEgreso.Tables("pcur").Rows(0)("STRCOD") Is Nothing) Then
                        If DsEgreso.Tables("pcur").Rows(0)("STRCOD").ToString <> "" Then
                            BlnValidaCodigoPick = True
                            strCodigoPick = DsEgreso.Tables("pcur").Rows(0)("STRCOD").ToString
                        Else
                            BlnValidaCodigoPick = False
                            strCodigoPick = ""
                        End If
                    End If
                Catch ex As Exception 'NO HAGO NADA PORQUE ES PICKING NORMAL
                End Try
                Me.lblPalletIng.Text = "Pallet : " & DsEgreso.Tables("pcur").Rows(0)("PALLET").ToString
                Me.PalletProp1 = DsEgreso.Tables("pcur").Rows(0)("PALLET").ToString
                Me.txtProducto_ID.Text = DsEgreso.Tables("pcur").Rows(0)("PRODUCTO_ID").ToString
                Me.lblPosicion.Text = "Ubicacion: " & DsEgreso.Tables("pcur").Rows(0)("POSICION_COD").ToString
                Me.txtDescripcion.Text = DsEgreso.Tables("pcur").Rows(0)("DESCRIPCION").ToString
                Me.Ubicacion = DsEgreso.Tables("pcur").Rows(0)("POSICION_COD").ToString
                Me.ViajeId = DsEgreso.Tables("pcur").Rows(0)("VIAJE_ID").ToString
                Me.ClienteId = DsEgreso.Tables("pcur").Rows(0)("CLIENTE_ID").ToString
                Me.txtCantidad.Text = CDbl(DsEgreso.Tables("pcur").Rows(0)("QTY")) 'QTY
                Me.vRuta = DsEgreso.Tables("PCUR").Rows(0)("RUTA")
                Me.lblUnidad.Text = "Unidad: " & DsEgreso.Tables("PCUR").Rows(0)("Unidad_ID")
                Me.BlnValCod = IIf(DsEgreso.Tables("PCUR").Rows(0)("VAL_COD_EGR") = "0", False, True)
                Me.vLote_proveedor = DsEgreso.Tables("pcur").Rows(0)("LOTE_PROVEEDOR").ToString 'Viene del doc de egreso.
                Me.vPartida = DsEgreso.Tables("pcur").Rows(0)("NRO_PARTIDA").ToString 'Viene del doc de egreso.
                Me.vSerieObligatoria = DsEgreso.Tables("pcur").Rows(0)("NRO_SERIE").ToString 'Viene del doc de egreso.
                Me.vNroSerieStock = DsEgreso.Tables("pcur").Rows(0)("NRO_SERIE_STOCK").ToString 'Viene del doc de egreso.
                Me.vNroBulto = DsEgreso.Tables("PCUR").Rows(0)("NRO_CONTENEDORA").ToString
                Me.vSerieObligatoria = Me.vSerieObligatoria.ToUpper

                'GetSerieExistencia(Me.vPickingID, Me.vSerie) 'Obtengo la serie de la existencia (RL).
                Me.vSerie = Me.vNroSerieStock.ToUpper

                Me.flg_serie_egreso = get_flg_serie_egreso_producto()

                If (vSerie <> "") Then 'El producto tiene serie en la existencia
                    If Fe.MuestraBtnSerieEspecifica(123) Then
                        Me.btn_Series_Esp.Visible = True
                    Else
                        Me.btn_Series_Esp.Visible = False
                    End If
                    '-------------------------------------------------------------------------------------
                    'Aca tengo que poner la validacion para saber si voy por confirmacion total del pallet.
                    '-------------------------------------------------------------------------------------
                    If Fe.SerializadoBestFit(Me.Ubicacion, Me.PalletProp1) Then
                        blnSerializadoBestFit = True

                        'Me.lblPosicion.Text = "Ubicacion: " & DsEgreso.Tables("pcur").Rows(0)("POSICION_COD").ToString & " | Confirme Pallet: " & PalletProp1

                        If Len("Ubicacion: " & DsEgreso.Tables("pcur").Rows(0)("POSICION_COD").ToString & " | Confirme Pallet: " & PalletProp1) > 35 Then
                            Me.txtPosicion.Location = New Point(6, 147)
                            Me.lblPosicion.Text = "Ubicacion: " & DsEgreso.Tables("pcur").Rows(0)("POSICION_COD").ToString & " |" + vbCrLf + "Confirme Pallet: " & PalletProp1
                        Else
                            Me.lblPosicion.Text = "Ubicacion: " & DsEgreso.Tables("pcur").Rows(0)("POSICION_COD").ToString & " | Confirme Pallet: " & PalletProp1
                        End If

                    Else
                        'El producto tiene serie cargada
                        If (Me.vSerieObligatoria <> "") Then 'El documento de egreso dice que serie sacar
                            'Además se pidió esta serie obligatoriamente.

                            'Me.lblPosicion.Text = "UBICACION: " + Me.Ubicacion + " | SERIE: " + Me.vSerieObligatoria

                            If Len("UBICACION: " + Me.Ubicacion + " | SERIE: " + Me.vSerieObligatoria) > 35 Then
                                Me.txtPosicion.Location = New Point(6, 147)
                                Me.lblPosicion.Text = "UBICACION: " + Me.Ubicacion + " |" + vbCrLf + "SERIE: " + Me.vSerieObligatoria
                            Else
                                Me.lblPosicion.Text = "UBICACION: " + Me.Ubicacion + " | SERIE: " + Me.vSerieObligatoria
                            End If

                        Else
                            'Puedo pickear cualquier serie
                            Me.lblPosicion.Text = "UBICACION: " + Me.Ubicacion + " | ING. SERIE:"
                        End If
                    End If
                Else
                    If Me.flg_serie_egreso = "1" Then
                        'El producto tiene tildado serie al egreso pero el stock no tiene cargadas series, esto hace que se pickee de a 1 y tenga que ingresar la serie.
                        'Me.lblPosicion.Text = "Ingrese un número de Serie:"
                        If Me.SolicitaConfirmacion Then
                            Me.lblPosicion.Text = "UBICACION: " + Me.Ubicacion '+ " | ING. SERIE:".
                        Else
                            Me.lblPosicion.Text = "UBICACION: " + Me.Ubicacion + " | ING. SERIE:"
                        End If

                    End If
                End If

                If Me.vLote_proveedor <> "" Then
                    Me.lblNroLote.Visible = True
                    Me.lblDatoLote.Visible = True
                    Me.lblDatoLote.Text = Me.vLote_proveedor
                Else
                    Me.lblNroLote.Visible = False
                    Me.lblDatoLote.Visible = False
                End If

                If Me.vPartida <> "" Then
                    Me.lblPartida.Visible = True
                    Me.lblDatoPartida.Visible = True
                    Me.lblDatoPartida.Text = Me.vPartida
                Else
                    Me.lblPartida.Visible = False
                    Me.lblDatoPartida.Visible = False
                End If

                If SolicitaLote Then
                    If Not Validarlote() Then Exit Sub
                End If

                If Me.vSerie = "" Then
                    If Me.BlnSolicitaConfirmacion Then
                        Me.txtPosicion.Focus()
                    Else
                        Me.txtPosicion.Text = Me.Ubicacion
                        If Not BlnValidaCodigoPick Then

                            Me.txtPosicion.Text = UCase(Me.txtPosicion.Text)

                            If Not Me.BlnSolicitaConfirmacion And Not blnConfirmoPalletOK Then
                                Me.ConfirmacionPallet(BlnConfirmoPallet)
                                If Not BlnConfirmoPallet Then
                                    blnConfirmoPalletOK = False
                                Else
                                    blnConfirmoPalletOK = True
                                End If
                            End If

                            If Not ProcesoIngresoSeries() Then
                                MsgBox("No se puede seguir si no se ingresan los nros. de series", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, FrmName)
                                Exit Sub
                            End If
                            If SolicitaLote Then
                                If Not Validarlote() Then Exit Sub
                            End If
                            Me.txtPosicion.ReadOnly = True
                            Me.txtCCantidad.Visible = True
                            Me.lblCCantidad.Visible = True
                            Me.txtCCantidad.Focus()
                        ElseIf BlnValidaCodigoPick Then
                            Me.lblCodigo.Visible = True
                            Me.txtCodigo.Visible = True
                            Me.txtCodigo.Text = ""
                            Me.txtCodigo.Focus()
                        End If
                    End If
                Else
                    Me.txtPosicion.Focus()
                End If

                If val.GetFlgContenedora(Trim(vUsr.ClienteActivo), DsEgreso.Tables("PCUR").Rows(0)(1).ToString) And Me.vSerie = "" Then
                    IsContenedora = True
                    Cmd = SQLc.CreateCommand
                    Fe.Cmd = Cmd
                    Me.lblContenedora.Visible = True
                    Me.lblContenedora.Text = "Contenedora: " & DsEgreso.Tables("PCUR").Rows(0)("NRO_CONTENEDORA")
                    Me.cmdUbicContenedora.Visible = True
                    Me.lblMsg.Text = "Seleccione F8 o la opción Cambiar Cont. si desea cambiar de contenedora "
                    Me.lblPalletIng.Text = "Total Picking : " & CStr(Fe.GetCantidadxProducto(DsEgreso.Tables("pcur").Rows(0)("VIAJE_ID").ToString, DsEgreso.Tables("pcur").Rows(0)("PRODUCTO_ID").ToString)) & ""
                Else
                    Me.cmdUbicContenedora.Visible = False
                End If
                Me.Text = "Picking. Viaje: " & ViajeId
                Me.btnConversiones.Visible = True
            Else
                MsgBox("Ud. no tiene más tareas de Picking.", MsgBoxStyle.OkOnly, FrmName)
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " SetValuesScreen.", MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Function get_flg_serie_egreso_producto() As String
        Dim Pa As SqlParameter
        Dim Cmd As New SqlCommand
        Dim resultado As String = ""

        Cmd.Parameters.Clear()
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "GET_FLG_SERIE_EGRESO_PRODUCTO"
                Cmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Me.ClienteId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = Me.txtProducto_ID.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OUT", SqlDbType.VarChar, 1)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                resultado = IIf(IsDBNull(Cmd.Parameters("@OUT").Value), "0", Cmd.Parameters("@OUT").Value).ToString

                If resultado <> "" Then
                    Return resultado
                Else
                    Return "0"
                End If
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
                Return False
            End If
        Catch SQLEx As SqlException
            MsgBox("get_flg_serie_egreso_producto: " & SQLEx.Message, MsgBoxStyle.OkOnly, "Picking")
            Return "0"
        Catch ex As Exception
            'Tran.Rollback()
            MsgBox("get_flg_serie_egreso_producto: " & ex.Message, MsgBoxStyle.OkOnly, "Picking")
            Return "0"
        Finally
            Pa = Nothing
        End Try
    End Function

    Private Sub GetSerieExistencia(ByVal pPickingId As String, ByRef pSerieObligatoria As String)
        Dim Pa As SqlParameter
        Dim Cmd As New SqlCommand


        Cmd.Parameters.Clear()
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "GET_SERIE_DE_EXISTENCIA"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@PICKING_ID", SqlDbType.BigInt)
                Pa.Value = pPickingId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OUT_SERIE", SqlDbType.VarChar, 50)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                pSerieObligatoria = IIf(IsDBNull(Cmd.Parameters("@OUT_SERIE").Value), "", Cmd.Parameters("@OUT_SERIE").Value).ToString
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("GetFlgGenNewPicking: " & SQLEx.Message, MsgBoxStyle.OkOnly, "Picking")
        Catch ex As Exception
            'Tran.Rollback()
            MsgBox("GetFlgGenNewPicking: " & ex.Message, MsgBoxStyle.OkOnly, "Picking")
        Finally
            Pa = Nothing
        End Try
    End Sub

    Private Sub IniForm()
        Dim Fe As New FuncionesEgreso
        Try
            Try
                If DsEgreso.Tables("PCUR") IsNot Nothing Then
                    DsEgreso.Tables.Remove("PCUR")
                End If
            Catch ex As Exception
            End Try
            If PickVehiculo Then
                Me.lblVehiculo.Visible = True
                If SolCalleNave Then
                    Me.lblVehiculo.Text = "Vehiculo: " & vUsr.Vehiculo & ", Nave-Calle: " & vUsr.NaveCalle
                    Me.cmdCambioCalle.Visible = True
                Else : Me.lblVehiculo.Text = "Vehiculo: " & vUsr.Vehiculo
                    Me.cmdCambioCalle.Visible = False
                End If
                cmdCambioVH.Visible = True
                'cmdCambioCalle.Visible = True
            Else
                Me.lblVehiculo.Visible = False
                cmdCambioVH.Visible = False
                cmdCambioCalle.Visible = False
            End If
            Me.btnConversiones.Visible = False
            Me.btn_Series_Esp.Visible = False
            Me.lblCodigo.Visible = False
            Me.txtCodigo.Visible = False
            Me.cmdUbicContenedora.Visible = False
            Me.cmdSaltoPicking.Enabled = False
            Me.cmdCerrarPallet.Enabled = False
            Me.vContCantInf = 0
            Me.lblPallet.Visible = False
            Me.lblPalletIng.Visible = False
            Me.lblProducto_ID.Visible = False
            Me.txtProducto_ID.Visible = False
            Me.txtProducto_ID.Text = ""
            Me.lblDescripcion.Visible = False
            Me.txtDescripcion.Visible = False
            Me.txtDescripcion.Text = ""
            Me.lblUnidad.Text = ""
            Me.lblCantidad.Visible = False
            Me.txtCantidad.Visible = False
            Me.txtCantidad.Text = ""
            Me.lblPosicion.Visible = False
            Me.txtPosicion.Visible = False
            Me.txtPosicion.Text = ""
            Me.lblCCantidad.Visible = False
            Me.txtCCantidad.Visible = False
            Me.txtCCantidad.Text = ""
            Me.lblMsg.Text = ""
            Me.lblCodigo.Visible = False
            Me.txtCodigo.Text = ""
            Me.txtCodigo.Visible = False
            Me.lblContenedora.Visible = False
            Me.lblDatoLote.Visible = False
            Me.lblDatoPartida.Visible = False
            Me.lblNroLote.Visible = False
            Me.lblPartida.Visible = False

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Public Sub New()
        InitializeComponent()
        DsEgreso = New DataSet

    End Sub

    Private Sub txtProducto_ID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProducto_ID.GotFocus
        If Me.txtCCantidad.Visible = True Then
            Me.txtCCantidad.Focus()
        ElseIf Me.txtCCantidad.Visible = False Then
            Me.txtPosicion.Focus()
        End If
    End Sub

    Private Sub txtCantidad_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCantidad.GotFocus
        If Me.txtCCantidad.Visible = True Then
            Me.txtCCantidad.Focus()
        ElseIf Me.txtCCantidad.Visible = False Then
            Me.txtPosicion.Focus()
        End If
    End Sub

    Private Sub PickSerieDeAUna()
        If VerifyConnection(SQLc) Then
            Dim Fe As New FuncionesEgreso
            Dim Cmd As SqlCommand
            Dim serieIngresada As String = ""
            Dim trans As SqlClient.SqlTransaction
            Try
                Cmd = SQLc.CreateCommand
                Fe.Cmd = Cmd

                serieIngresada = Me.vSerie

                trans = SQLc.BeginTransaction()
                If Not Fe.Fin_Picking_Split(vUsr.CodUsuario, Me.ViajeId, Me.txtProducto_ID.Text, Me.Ubicacion, 1, PalletId, PalletProp1, vRuta, NroLote, Me.vSerie, trans) Then
                    'cschenk
                    trans.Rollback()
                Else
                    'cschenk
                    'grabo series
                    If Me.vSerie <> "" Then
                        If Not graboSerieEgresada(trans) Then
                            trans.Rollback()
                        End If
                    End If
                    If Not IsNothing(trans) Then
                        trans.Commit()
                    End If
                End If

                Me.vSerie = serieIngresada

                trans = SQLc.BeginTransaction()
                If Not Fe.Fin_Picking(vUsr.CodUsuario, Me.ViajeId, Me.txtProducto_ID.Text, Me.Ubicacion, 1, PalletId, PalletProp1, vRuta, NroLote, Me.vSerie, trans) Then
                    trans.Rollback()
                Else
                    trans.Commit()
                End If

                ClearAll()

                NewTomaTarea()

                Me.vSerie = ""
                Exit Try

            Catch ex As Exception
                MsgBox(ex.Message + vbCrLf + vbCrLf + ex.StackTrace, MsgBoxStyle.OkOnly, FrmName)
                If Not IsNothing(trans) Then
                    trans.Rollback()
                End If
            Finally
                Fe = Nothing
                Cmd = Nothing

            End Try
        Else
            MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
        End If
    End Sub

    Private Function validaSerieIngresada() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As New SqlCommand
        Dim resultado As String = ""
        Cmd.Parameters.Clear()
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand

                Cmd.CommandText = "VALIDAR_SERIE_INGRESADA"
                Cmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Me.ClienteId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = Me.txtProducto_ID.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 50)
                Pa.Value = Me.txtPosicion.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                Pa.Value = Me.ViajeId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OUT", SqlDbType.VarChar, 1)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                resultado = IIf(IsDBNull(Cmd.Parameters("@OUT").Value), "0", Cmd.Parameters("@OUT").Value).ToString

                If (resultado = "1") Then
                    Me.vSerie = Me.txtPosicion.Text.ToUpper
                    Return True
                Else
                    Return False
                End If

            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("intercambiarSeries: " & SQLEx.Message, MsgBoxStyle.OkOnly, "Picking")
            Return False
        Catch ex As Exception
            'Tran.Rollback()
            MsgBox("intercambiarSeries: " & ex.Message, MsgBoxStyle.OkOnly, "Picking")
            Return False
        Finally
            Pa = Nothing
        End Try
    End Function

    Private Sub txtPosicion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPosicion.KeyUp
        Dim Confirmo As Boolean = False
        Dim DSConversion As New DataSet
        If e.KeyCode = 13 And Me.txtPosicion.Text <> "" Then
            Dim valPosicion As String = "", Confirmacion As Boolean = False
            Me.lblMsg.Text = ""
            Me.txtPosicion.Text = Me.txtPosicion.Text.ToUpper

            If (Me.vSerie = "") Then
                'SERIALIZACION AL EGRESO
                If Me.flg_serie_egreso = "1" Then
                    'VALIDA POSICION
                    If Me.BlnSolicitaConfirmacion Then
                        'SI TIENE LA POSICION SE VALIDA
                        If Me.lblPosicion.Text = "UBICACION: " + Me.Ubicacion Then
                            If UCase(Me.Ubicacion) <> (Me.txtPosicion.Text) Then
                                MsgBox("La posición ingresada es incorrecta")
                                Me.txtPosicion.Text = ""
                                Exit Sub
                            Else
                                Me.ConfirmacionPallet(Confirmo)
                                If Not Confirmo Then
                                    Me.txtPosicion.Enabled = True
                                    Me.txtPosicion.ReadOnly = False
                                    Me.txtPosicion.Text = ""
                                    Exit Sub
                                End If
                                Me.lblPosicion.Text = Me.lblPosicion.Text + " | ING. SERIE:"
                                Me.txtPosicion.Text = ""
                            End If
                        Else
                            'VALIDA LA SERIE 2D OBLIGATORIA
                            If Valida2D() Then
                                If (Len(Trim(Me.txtPosicion.Text)) = 55) Or (Len(Trim(Me.txtPosicion.Text)) = 52) Then
                                    If Not SerieEgreso2D(Confirmacion) Then
                                        Exit Sub
                                    End If
                                Else
                                    MsgBox("La serie ingresada no corresponde a una serie 2D")
                                    Exit Sub
                                End If
                            Else
                                'NO VALIDA LA SERIE 2D OBLIGATORIA
                                If Not SerieEgreso(Confirmacion) Then
                                    Exit Sub
                                End If
                            End If
                        End If
                    Else
                        'NO VALIDA POSICION                        
                        If Valida2D() Then
                            If (Len(Trim(Me.txtPosicion.Text)) = 55) Or (Len(Trim(Me.txtPosicion.Text)) = 52) Then
                                If Not SerieEgreso2D(Confirmacion) Then
                                    Exit Sub
                                End If
                            Else
                                MsgBox("La serie ingresada no corresponde a una serie 2D")
                                Exit Sub
                            End If
                        Else
                            If Not SerieEgreso(Confirmacion) Then
                                Exit Sub
                            End If
                        End If
                    End If
                Else
                    o2D.SerializacionEgreso = False
                    If Not IsContenedora Then
                        If UCase(Trim(Me.txtPosicion.Text.ToUpper)) = UCase(Trim(Ubicacion.ToUpper)) Then
                            'SGOMEZ.
                            valPosicion = validarPosicion()
                            Select Case valPosicion
                                Case "1"
                                    Me.ConfirmacionPallet(Confirmo)
                                    If Not Confirmo Then
                                        Me.txtPosicion.Enabled = True
                                        Me.txtPosicion.ReadOnly = False
                                        Me.txtPosicion.Text = ""
                                        Exit Sub
                                    End If
                                Case "2"
                                    'No existe
                                    MessageBox.Show("La posición ingresada no existe.", "Posición inexistente")
                                    Me.txtPosicion.Text = ""
                                    Exit Sub
                                Case "3"
                                    'No disponible para picking
                                    MessageBox.Show("La posición no está disponible para picking.", "Posición no pickeable")
                                    Me.txtPosicion.Text = ""
                                    Exit Sub
                                Case "4"
                                    'lockeada
                                    MessageBox.Show("La posición se encuentra lockeada.", "Posición loqueada")
                                    Me.txtPosicion.Text = ""
                                    Exit Sub
                                Case "5"
                                    'No se puede pickear de la posición
                                    MessageBox.Show("No se puede pickear de la posición.", "Posición inválida")
                                    Me.txtPosicion.Text = ""
                                    Exit Sub
                            End Select
                        Else
                            Me.lblMsg.Text = "La posición seleccionada no es correcta."
                            Me.txtPosicion.SelectAll()
                        End If
                    Else
                        If UCase(Trim(Me.txtPosicion.Text.ToUpper)) = UCase(Trim(Ubicacion.ToUpper)) Then
                            valPosicion = validarPosicion()
                            Select Case valPosicion
                                Case "1"
                                    Me.ConfirmacionPallet(Confirmo)
                                    If Not Confirmo Then
                                        Me.txtPosicion.Enabled = True
                                        Me.txtPosicion.ReadOnly = False
                                        Me.txtPosicion.Text = ""
                                        Exit Sub
                                    End If
                                Case "2"
                                    'No existe
                                    MessageBox.Show("La posición ingresada no existe.", "Posición inexistente")
                                    Me.txtPosicion.Text = ""
                                    Exit Sub
                                Case "3"
                                    'No disponible para picking
                                    MessageBox.Show("La posición no está disponible para picking.", "Posición no pickeable")
                                    Me.txtPosicion.Text = ""
                                    Exit Sub
                                Case "4"
                                    'lockeada
                                    MessageBox.Show("La posición se encuentra lockeada.", "Posición loqueada")
                                    Me.txtPosicion.Text = ""
                                    Exit Sub
                                Case "5"
                                    'No se puede pickear de la posición
                                    MessageBox.Show("No se puede pickear de la posición.", "Posición inválida")
                                    Me.txtPosicion.Text = ""
                                    Exit Sub
                            End Select
                        Else
                            Me.lblMsg.Text = "La posición seleccionada no es correcta."
                            Me.txtPosicion.SelectAll()
                        End If
                    End If
                End If
            Else

                If BlnValCod Then
                    If Not ValidaEAN_DUN() Then Exit Sub
                End If

                If Me.vSerieObligatoria = "" Then
                    o2D.CLIENTE_ID = Me.ClienteId
                    o2D.Decode(Me.txtPosicion.Text)
                    If o2D.UnicaSerie Then
                        Me.txtPosicion.Text = o2D.NroSerie
                        Application.DoEvents()
                        'Else
                        '    MsgBox("La lectura no corresponde a una serie individual.", MsgBoxStyle.Information, FrmName)
                        '    Me.txtPosicion.Text = ""
                        '    Me.txtPosicion.Focus()
                        '    Exit Sub
                    End If

                    If blnSerializadoBestFit Then
                        If Me.PalletProp1 = Me.txtPosicion.Text Then
                            ConfimaPalletSerializado()
                            ClearAll()
                            NewTomaTarea()
                            Return
                        Else
                            MsgBox("El pallet escaneado no es correcto.", MsgBoxStyle.Critical, FrmName)
                            Me.txtPosicion.Text = ""
                            Me.txtPosicion.Focus()
                            Exit Sub
                        End If
                    End If
                    '--------------------------------------------------------------------------
                    'VALIDO LA TOMA DE SERIES DE LENOVO.
                    '--------------------------------------------------------------------------
                    If Me.btn_Series_Esp.Visible = True And Me.btn_Series_Esp.Text = "F9) Series Esp.: On" Then
                        Dim fe As New FuncionesEgreso
                        If Not fe.DescomponerSerieEspecifica(Me.ViajeId, Me.PalletProp1, Me.txtPosicion) Then
                            Me.txtPosicion.Text = ""
                            Me.txtPosicion.Focus()
                            fe = Nothing
                            Exit Sub
                        End If
                    End If
                    '--------------------------------------------------------------------------
                    'Si la serie es la que se tiene en memoria prosigo con el picking
                    If (Me.vSerie.ToUpper = Me.txtPosicion.Text.ToUpper) Then
                        'Pickeo la serie
                        pickearPorSerie()
                    Else
                        'Busco la serie ingresada y si está en disponibilidad de egreso la cambio por la que esta en memoria.
                        Select Case permitePickSerieIntercambio()
                            Case "0"
                                If intercambiarSeries() Then
                                    'SE realizó correctamente el intercambio de series, pickeo.
                                    obtengoDatosNuevaSerie()
                                    pickearPorSerie()
                                Else
                                    MessageBox.Show("Ocurrio un error al intentar cambiar de serie. Por favor inténtelo nuevamente", "Error cambio de serie", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                                    Me.txtPosicion.Text = ""
                                End If
                            Case "1"
                                MessageBox.Show("La serie ingresada no existe.", "Serie inexistente")
                                Me.txtPosicion.Text = ""
                            Case "2"
                                MessageBox.Show("La serie ingresada no se encuentra disponible para egreso.", "Serie no egresable")
                                Me.txtPosicion.Text = ""
                            Case "3"
                                MessageBox.Show("La serie ya fue pickeada para este viaje.", "Serie no egresable")
                                Me.txtPosicion.Text = ""
                            Case "4"
                                MessageBox.Show("La serie fue especificada para otro viaje.", "Serie no egresable")
                                Me.txtPosicion.Text = ""
                            Case "5"
                                MessageBox.Show("La serie ingresada no se encuentra disponible para egreso.", "Serie no egresable")
                                Me.txtPosicion.Text = ""
                            Case "6"
                                MessageBox.Show("La serie ingresada no se encuentra disponible para egreso.", "Serie no egresable")
                                Me.txtPosicion.Text = ""
                        End Select
                    End If
                Else
                    If blnSerializadoBestFit Then
                        If Me.PalletProp1 = Me.txtPosicion.Text Then
                            ConfimaPalletSerializado()
                            ClearAll()
                            NewTomaTarea()
                            Return
                        Else
                            MsgBox("El pallet escaneado no es correcto.", MsgBoxStyle.Critical, FrmName)
                            Me.txtPosicion.Text = ""
                            Me.txtPosicion.Focus()
                            Exit Sub
                        End If
                    End If

                    '--------------------------------------------------------------------------
                    'VALIDO LA TOMA DE SERIES DE LENOVO.
                    '--------------------------------------------------------------------------
                    If Me.btn_Series_Esp.Visible = True And Me.btn_Series_Esp.Text = "F9) Series Esp.: On" Then
                        Dim fe As New FuncionesEgreso
                        If Not fe.DescomponerSerieEspecifica(Me.ViajeId, Me.PalletProp1, Me.txtPosicion) Then
                            Me.txtPosicion.Text = ""
                            Me.txtPosicion.Focus()
                            fe = Nothing
                            Exit Sub
                        End If
                    End If

                    o2D.CLIENTE_ID = Me.ClienteId
                    o2D.Decode(Me.txtPosicion.Text)

                    If o2D.UnicaSerie Then
                        Me.txtPosicion.Text = o2D.NroSerie
                        Application.DoEvents()
                        'Else
                        '   MsgBox("La lectura no corresponde a una serie individual.", MsgBoxStyle.Information, FrmName)
                        '    Me.txtPosicion.Text = ""
                        '   Me.txtPosicion.Focus()
                        '   Exit Sub
                    End If

                    If Not (Me.vSerie.ToUpper = Me.vSerieObligatoria.ToUpper And Me.vSerie.ToUpper = Me.txtPosicion.Text.ToUpper) Then
                        MessageBox.Show("La serie ingresada no es la indicada.", "Serie incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                        Me.txtPosicion.Text = ""
                        Exit Sub
                    Else
                        'Pickeo
                        pickearPorSerie()
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub ConfirmacionPallet(ByRef Confirmacion As Boolean)
        Dim xConf As Boolean = False
        Dim frm As New frmEgresoConfirmacionPallet
        Try
            If Me.ConfirmaPallet(xConf) Then
                If Not xConf Then
                    Confirmacion = True
                    Return
                End If
                If xConf And Me.PalletProp1 <> "" Then
                    'Comienzo con el pasaje de propiedades.
                    frm.CLIENTE = Me.ClienteId
                    frm.PRODUCTO_ID = Me.txtProducto_ID.Text
                    frm.DESCRIPCION = Me.txtDescripcion.Text
                    frm.VIAJE_ID = Me.ViajeId
                    frm.PALLET = Me.PalletProp1
                    frm.CANTIDAD = Me.txtCantidad.Text
                    frm.NRO_LOTE = ""
                    frm.NRO_PARTIDA = ""
                    If Me.lblDatoLote.Text <> "Label1" Then frm.NRO_LOTE = Me.lblDatoLote.Text
                    If Me.lblDatoPartida.Text <> "Label1" Then frm.NRO_PARTIDA = lblDatoPartida.Text
                    frm.UBICACION = Me.Ubicacion
                    'form a la pantalla.
                    frm.ShowDialog()
                    Confirmacion = frm.CONFIRMO
                    If Not Confirmacion Then
                        Me.txtCCantidad.Visible = False
                        Me.lblCCantidad.Visible = False
                    Else
                        '¿COMO PONGO EL PALLET DONDE VA?
                        Me.lblPalletIng.Text = frm.PALLET
                        Me.PalletProp1 = frm.PALLET
                        Me.txtCantidad.Text = frm.CANTIDAD
                        If frm.UBICACION <> "" Then
                            Me.Ubicacion = frm.UBICACION
                        End If
                        Me.txtPosicion.Text = Me.Ubicacion
                        If Mid(Me.lblPosicion.Text, 1, 10).ToUpper = "UBICACION:" Then
                            Me.lblPosicion.Text = "Ubicacion: " & Me.Ubicacion
                        End If
                    End If
                Else
                    If Me.PalletProp1 = "" Then
                        Confirmacion = True
                        Return
                    End If
                End If

            End If
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            frm = Nothing
            GC.Collect()
        End Try
    End Sub

    Private Function ConfirmaPallet(ByRef Confirma As Boolean) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandText = "SELECT ISNULL(C.FLG_PICK_CONFIRMA_PALLET,'0')RESULT FROM CLIENTE_PARAMETROS C WHERE C.CLIENTE_ID='" & Me.ClienteId & "'"
                Cmd.CommandType = CommandType.Text

                DA.Fill(DS, "TBL")
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        If DS.Tables(0).Rows(0)(0) = "0" Then
                            Confirma = False
                        Else
                            Confirma = True
                        End If
                    Else
                        Confirma = False
                    End If
                Else
                    Confirma = False
                End If

            Else : MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
                Return False
            End If
            Return True
        Catch SQLEX As SqlException
            MsgBox("Excepcion SQL: " & SQLEX.Message)
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            DA.Dispose()
            Cmd.Dispose()
            DS.Dispose()
        End Try
    End Function

    Private Function ConfimaPalletSerializado()
        Dim Fe As New FuncionesEgreso
        Try
            Fe.ConfirmarSerializacionPallet(Me.ViajeId, Me.Ubicacion, Me.PalletProp1, PalletId, vUsr.CodUsuario)

        Catch ex As Exception
            MsgBox(ex.Message & " obtengoDatosNuevaSerie", MsgBoxStyle.OkOnly, "frmEgreso")
        Finally
            Fe = Nothing
        End Try
    End Function

    Private Function obtengoDatosNuevaSerie() As Boolean
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim cmd As SqlCommand
        Dim DsSeriePickeada As New DataSet
        Try
            If VerifyConnection(SQLc) Then
                cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(cmd)
                cmd.Parameters.Clear()
                cmd.CommandText = "GET_DATOS_NUEVA_SERIE"
                cmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@P_USUARIO", SqlDbType.VarChar, 30)
                Pa.Value = vUsr.CodUsuario
                cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@P_CLIENTE_ID", SqlDbType.VarChar, 20)
                Pa.Value = IIf(Trim(vUsr.ClienteActivo) = "", DBNull.Value, Trim(UCase(vUsr.ClienteActivo)))
                cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@P_PRODUCTO_ID", SqlDbType.VarChar, 50)
                Pa.Value = Me.txtProducto_ID.Text
                cmd.Parameters.Add(Pa)
                Pa = New SqlParameter("@P_NRO_SERIE", SqlDbType.VarChar, 50)
                Pa.Value = Me.txtPosicion.Text
                cmd.Parameters.Add(Pa)
                Pa = New SqlParameter("@P_VIAJE_ID", SqlDbType.VarChar, 50)
                Pa.Value = Me.ViajeId
                cmd.Parameters.Add(Pa)
                Da.Fill(DsSeriePickeada, "NuevaSerie")

                If DsSeriePickeada.Tables.Count > 0 And DsSeriePickeada.Tables("NuevaSerie").Rows.Count = 1 Then
                    Me.PalletProp1 = DsSeriePickeada.Tables("NuevaSerie").Rows(0)("PALLET").ToString
                    Me.txtProducto_ID.Text = DsSeriePickeada.Tables("NuevaSerie").Rows(0)("PRODUCTO_ID").ToString
                    Me.Ubicacion = DsSeriePickeada.Tables("NuevaSerie").Rows(0)("POSICION_COD").ToString
                    Me.ViajeId = DsSeriePickeada.Tables("NuevaSerie").Rows(0)("VIAJE_ID").ToString
                    Me.txtCantidad.Text = CDbl(DsSeriePickeada.Tables("NuevaSerie").Rows(0)("QTY")) 'QTY
                    Me.vRuta = DsSeriePickeada.Tables("NuevaSerie").Rows(0)("RUTA")
                    Me.PalletId = DsSeriePickeada.Tables("NuevaSerie").Rows(0)("Pallet_picking")
                    Return True
                Else
                    Return False
                End If
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " obtengoDatosNuevaSerie", MsgBoxStyle.OkOnly, "frmEgreso")
        Finally
            Pa = Nothing
            Da = Nothing
        End Try
    End Function

    Private Function graboSerieEgresada(Optional ByRef trans As SqlTransaction = Nothing) As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As New SqlCommand
        Dim resultado As String = ""

        Cmd.Parameters.Clear()
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "GRABAR_SERIE_EGRESADA"
                Cmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Me.ClienteId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_BULTO", SqlDbType.VarChar, 100)
                Pa.Value = Me.vNroBulto
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = Me.txtProducto_ID.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 50)
                Pa.Value = Me.vSerie
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                Pa.Value = Me.ViajeId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO_ID", SqlDbType.VarChar, 30)
                Pa.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@TERMINAL", SqlDbType.VarChar, 100)
                Pa.Value = "hhtest"
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                If Not IsNothing(trans) Then
                    Cmd.Transaction = trans
                End If
                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("graboSerieEgresada: " & SQLEx.Message, MsgBoxStyle.OkOnly, "Picking")
            Return False
        Catch ex As Exception
            'Tran.Rollback()
            MsgBox("graboSerieEgresada: " & ex.Message, MsgBoxStyle.OkOnly, "Picking")
            Return False
        Finally
            Pa = Nothing
        End Try

    End Function

    Private Function pickearPorSerie() As Boolean
        If VerifyConnection(SQLc) Then
            Dim Fe As New FuncionesEgreso
            Dim Cmd As SqlCommand
            Dim trans As SqlClient.SqlTransaction
            Try
                Cmd = SQLc.CreateCommand
                Fe.Cmd = Cmd

                trans = SQLc.BeginTransaction()
                If Fe.Fin_Picking(vUsr.CodUsuario, Me.ViajeId, Me.txtProducto_ID.Text, Me.Ubicacion, CDbl(Me.txtCantidad.Text), PalletId, PalletProp1, vRuta, NroLote, Me.vSerie, trans) Then
                    'copia de la seccion de codigo al hacer "enter" en cantidad a confirmar cuando la cantidad pedida = confirmada.
                    'grabo series
                    If Me.vSerie <> "" Then
                        If Not graboSerieEgresada(trans) Then
                            trans.Rollback()
                            Return False
                        End If
                    End If
                    trans.Commit()
                    ClearAll()
                    NewTomaTarea()
                    Return True
                Else
                    'cschenk-----
                    trans.Rollback()
                    '------------
                    If Fe.CerrarPallet Then
                        ImpresionPallet(2)
                        ClearAll(True)
                        Me.ViajeId = "0"
                        Me.Text = "Picking."
                    End If
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message + vbCrLf + vbCrLf + ex.StackTrace, MsgBoxStyle.OkOnly, FrmName)
                If Not IsNothing(trans) Then
                    trans.Rollback()
                End If
                Return False
            Finally
                Fe = Nothing
                Cmd = Nothing
            End Try
        Else
            MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
            Return False
        End If
    End Function

    Private Function intercambiarSeries() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As New SqlCommand
        Dim resultado As String = ""
        Dim trans As SqlClient.SqlTransaction
        Cmd.Parameters.Clear()
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                trans = SQLc.BeginTransaction()

                Cmd.CommandText = "INTERCAMBIAR_SERIES_PICK"
                Cmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Me.ClienteId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = Me.txtProducto_ID.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                Pa.Value = Me.ViajeId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_SERIE_ACTUAL", SqlDbType.VarChar, 50)
                Pa.Value = Me.vSerie
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_SERIE_NUEVA", SqlDbType.VarChar, 50)
                Pa.Value = Me.txtPosicion.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OUT", SqlDbType.VarChar, 1)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                If Not IsNothing(trans) Then
                    Cmd.Transaction = trans
                End If

                Cmd.ExecuteNonQuery()
                resultado = IIf(IsDBNull(Cmd.Parameters("@OUT").Value), "0", Cmd.Parameters("@OUT").Value).ToString

                If (resultado = "1") Then
                    trans.Commit()
                    Me.vSerie = Me.txtPosicion.Text.ToUpper
                    Return True
                Else
                    If Not IsNothing(trans) Then
                        trans.Rollback()
                    End If
                    Return False
                End If

            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            If Not IsNothing(trans) Then
                trans.Rollback()
            End If
            MsgBox("intercambiarSeries: " & SQLEx.Message, MsgBoxStyle.OkOnly, "Picking")
            Return False
        Catch ex As Exception
            'Tran.Rollback()
            If Not IsNothing(trans) Then
                trans.Rollback()
            End If
            MsgBox("intercambiarSeries: " & ex.Message, MsgBoxStyle.OkOnly, "Picking")
            Return False
        Finally
            Pa = Nothing
        End Try

    End Function

    Private Function permitePickSerieIntercambio() As String
        Dim Pa As SqlParameter
        Dim Cmd As New SqlCommand
        Dim resultado As String = ""

        Cmd.Parameters.Clear()
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "PERMITE_PICK_DE_SERIE"
                Cmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Me.ClienteId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = Me.txtProducto_ID.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_SERIE_ANT", SqlDbType.VarChar, 50)
                Pa.Value = Me.vSerie
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 50)
                Pa.Value = Me.txtPosicion.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CODIGO_VIAJE", SqlDbType.VarChar, 100)
                Pa.Value = Me.ViajeId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO_ID", SqlDbType.VarChar, 30)
                Pa.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OUT", SqlDbType.VarChar, 1)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                resultado = IIf(IsDBNull(Cmd.Parameters("@OUT").Value), "0", Cmd.Parameters("@OUT").Value).ToString

                If resultado <> "" Then
                    Return resultado
                Else
                    Return "6"
                End If
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
                Return False
            End If
        Catch SQLEx As SqlException
            MsgBox("GetFlgGenNewPicking: " & SQLEx.Message, MsgBoxStyle.OkOnly, "Picking")
            Return "4"
        Catch ex As Exception
            'Tran.Rollback()
            MsgBox("GetFlgGenNewPicking: " & ex.Message, MsgBoxStyle.OkOnly, "Picking")
            Return "4"
        Finally
            Pa = Nothing
        End Try
    End Function

    Public Function validarPosicion() As String
        'If BlnValCod Then
        '    If Not ValidaEAN_DUN() Then Exit Function
        'End If
        'If SolicitaLote Then
        '    If Not Validarlote() Then Exit Function
        'End If
        ''grabo nros series
        ''para result=0 o result=1 se sigue

        ''Valido que la posicion permita picking y exista.


        ''---------
        'If Not BlnValidaCodigoPick Then
        '    Me.txtPosicion.Text = UCase(Me.txtPosicion.Text)
        '    If Not ProcesoIngresoSeries() Then
        '        MsgBox("No se puede seguir si no se ingresan los nros. de series", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, FrmName)
        '        Exit Function
        '    End If
        '    'Cargar el Flag de Disponible
        '    Me.txtPosicion.ReadOnly = True
        '    Me.txtCCantidad.Visible = True
        '    Me.lblCCantidad.Visible = True
        '    Me.txtCCantidad.Focus()
        'ElseIf BlnValidaCodigoPick Then
        '    Me.lblCodigo.Visible = True
        '    Me.txtCodigo.Visible = True
        '    Me.txtCodigo.Text = ""
        '    Me.txtCodigo.Focus()
        'End If

        Dim Pa As SqlParameter
        Dim Cmd As New SqlCommand
        Dim resultado As String = ""

        Cmd.Parameters.Clear()
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "VALIDAR_POSICION_PICKING"
                Cmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@POSICION_COD", SqlDbType.VarChar, 45)
                Pa.Value = Me.txtPosicion.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OUT", SqlDbType.VarChar, 1)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                resultado = IIf(IsDBNull(Cmd.Parameters("@OUT").Value), "0", Cmd.Parameters("@OUT").Value).ToString
                If resultado = "1" Then
                    If BlnValCod Then
                        If Not ValidaEAN_DUN() Then Exit Function
                    End If
                    Me.txtPosicion.ReadOnly = True
                    Me.txtCCantidad.Visible = True
                    Me.lblCCantidad.Visible = True
                    Me.txtCantidad.Focus()
                End If
                If resultado <> "" Then
                    Return resultado
                Else
                    Return "0"
                End If
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
                Return False
            End If
        Catch SQLEx As SqlException
            MsgBox("validarPosicion: " & SQLEx.Message, MsgBoxStyle.OkOnly, "Picking")
            Return "0"
        Catch ex As Exception
            'Tran.Rollback()
            MsgBox("validarPosicion: " & ex.Message, MsgBoxStyle.OkOnly, "Picking")
            Return "0"
        Finally
            Pa = Nothing
        End Try

    End Function

    Private Sub txtCCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCCantidad.KeyPress
        'ValidarCaracterNumerico(e)
        Dim Search As String
        Dim Pos As Integer
        Search = "."
        If Not xEsFraccionable Then
            ValidarCaracterNumerico(e)
        Else
            Pos = InStr(1, Me.txtCCantidad.Text, Search)
            If Pos > 0 And Asc(e.KeyChar) <> 46 Then
                If Len(Mid(Me.txtCCantidad.Text, Pos + 1, Len(Me.txtCCantidad.Text))) >= 5 And Asc(e.KeyChar) <> 8 Then
                    e.Handled = True
                    Me.txtCCantidad.Focus()
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

    Private Sub txtCCantidad_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCCantidad.KeyUp
        If e.KeyCode = 13 And IsNumeric(Me.txtCCantidad.Text) Then
            If VerifyConnection(SQLc) Then
                Dim Fe As New FuncionesEgreso, Confirmo As Boolean = False
                Dim Cmd As SqlCommand
                Dim Rta As Object
                Dim FlgNewPicking As Boolean
                Dim trans As SqlClient.SqlTransaction
                Try
                    Cmd = SQLc.CreateCommand
                    Fe.Cmd = Cmd
                    Me.lblMsg.Text = ""

                    If Not Me.BlnSolicitaConfirmacion And Not blnConfirmoPalletOK Then
                        Me.ConfirmacionPallet(Confirmo)
                        If Not Confirmo Then
                            Me.lblCCantidad.Visible = True
                            Me.txtCCantidad.Visible = True
                            Me.txtCCantidad.Text = ""
                            Me.txtCCantidad.Focus()
                            Exit Sub
                        End If
                    End If

                    If Not IsContenedora Then
                        If CDbl(Me.txtCantidad.Text) <> CDbl(Me.txtCCantidad.Text) And vContCantInf > 0 Then
                            If CDbl(Me.txtCantidad.Text) > CDbl(Me.txtCCantidad.Text) Then
                                'Maximiliano Privitera
                                FlgNewPicking = Fe.GetFlgGenNewPicking(ClienteId)
                                If FlgNewPicking Then
                                    Rta = MsgBox("¿Desea generar una nueva tarea de picking con la cantidad restante?", MsgBoxStyle.YesNo, FrmName)
                                    If Rta = vbYes Then
                                        trans = SQLc.BeginTransaction()
                                        If Not Fe.Fin_Picking_Split(vUsr.CodUsuario, Me.ViajeId, Me.txtProducto_ID.Text, Me.Ubicacion, CDbl(Me.GetCantidadConvertida), PalletId, PalletProp1, vRuta, NroLote, Me.vSerie, trans) Then
                                            'cschenk
                                            trans.Rollback()
                                        Else
                                            'cschenk
                                            'grabo series
                                            If ProdConSerieEgr Then
                                                If Not GrabarDSSeries(trans) Then
                                                    trans.Rollback()
                                                    Exit Sub
                                                End If
                                            End If
                                            trans.Commit()
                                        End If
                                        Rta = MsgBox("¿Desea cerrar el pallet y abrir otro?", MsgBoxStyle.YesNo, FrmName)
                                        If Rta = vbYes Then
                                            If Not GetPalletoCarro(PalletId, Cmd) Then
                                            End If
                                            ImpresionPallet(2)
                                            ClearAll()
                                            NewTomaTarea()
                                        Else
                                            ClearAll()
                                            NewTomaTarea()
                                        End If
                                        Exit Try
                                    Else
                                        Rta = MsgBox("Las Cantidades son diferentes" & vbNewLine & "¿Desea continuar?", MsgBoxStyle.YesNo, FrmName)
                                        If Rta = vbYes Then
                                            trans = SQLc.BeginTransaction()
                                            If Not Fe.Fin_Picking(vUsr.CodUsuario, Me.ViajeId, Me.txtProducto_ID.Text, Me.Ubicacion, CDbl(Me.txtCCantidad.Text), PalletId, PalletProp1, vRuta, NroLote, Me.vSerie, trans) Then
                                                'Q va aca?? Maximiliano Privitera
                                                'cschenk
                                                trans.Rollback()
                                            Else
                                                'cschenk
                                                'grabo series
                                                If ProdConSerieEgr Then
                                                    If Not GrabarDSSeries(trans) Then
                                                        trans.Rollback()
                                                        Exit Sub
                                                    End If
                                                End If
                                                trans.Commit()
                                            End If
                                        Else
                                            Me.txtCCantidad.Text = ""
                                            Me.txtCCantidad.Focus()
                                            Exit Try
                                        End If
                                    End If
                                End If
                            Else
                                lblMsg.Text = "La cantidad ingresada es" & vbNewLine & " mayor a la solicitada."
                                Me.txtCCantidad.SelectAll()
                                Exit Try
                            End If
                        End If
                        If CDbl(Me.txtCantidad.Text) = CDbl(Me.txtCCantidad.Text) Or vContCantInf > 0 Then
                            trans = SQLc.BeginTransaction()
                            If Fe.Fin_Picking(vUsr.CodUsuario, Me.ViajeId, Me.txtProducto_ID.Text, Me.Ubicacion, CDbl(Me.GetCantidadConvertida), PalletId, PalletProp1, vRuta, NroLote, Me.vSerie, trans) Then
                                'cschenk
                                'grabo series
                                If ProdConSerieEgr Then
                                    If Not GrabarDSSeries(trans) Then
                                        MsgBox("ERR EN GrabarDSSeries 3")
                                        trans.Rollback()
                                        Exit Sub
                                    End If
                                End If
                                trans.Commit()
                                ClearAll()
                                NewTomaTarea()
                            Else
                                'cschenk-----
                                trans.Rollback()
                                '------------
                                If Fe.CerrarPallet Then
                                    ImpresionPallet(2)
                                    ClearAll(True)
                                    Me.ViajeId = "0"
                                    Me.Text = "Picking."
                                End If

                            End If
                        Else
                            If CDbl(Me.txtCantidad.Text) > CDbl(Me.txtCCantidad.Text) Then
                                vContCantInf = vContCantInf + 1
                                Me.lblMsg.Text = "La cantidad ingresada es" & vbNewLine & "menor a la solicitada."
                                Me.txtCCantidad.SelectAll()
                            Else
                                lblMsg.Text = "La cantidad ingresada es" & vbNewLine & "mayor a la solicitada."
                                Me.txtCCantidad.SelectAll()
                            End If
                        End If
                    Else
                        ConfirmarCantidadCont()
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message + vbCrLf + vbCrLf + ex.StackTrace, MsgBoxStyle.OkOnly, FrmName)
                    If Not IsNothing(trans) Then
                        trans.Rollback()
                    End If
                Finally
                    Fe = Nothing
                    Cmd = Nothing
                End Try
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
            End If
        End If
    End Sub
    '--
    Public Sub ConfirmarCantidadCont()

        Dim Rta As Object
        Dim FlgNewPicking As Boolean
        Dim trans As SqlClient.SqlTransaction
        Dim vError As String = ""
        Dim Cmd As SqlCommand
        Dim Fe As New FuncionesEgreso
        Dim ProductoId As String = ""
        Dim ValorTotalxProducto As Double = 0
        Dim CantidadConvertida As Double = 0
        Try
            Cmd = SQLc.CreateCommand
            Fe.Cmd = Cmd
            If CDbl(Me.txtCCantidad.Text) > CantidadMaxCont And Rl_Id > 0 Then
                Throw New Exception("La cantidad a confirmar es mayor a la cantidad que tiene la contenedora seleccionada.")
            End If
            If (Not CambioContenedora) Then
                If CDbl(Me.txtCCantidad.Text) > CDbl(Me.txtCantidad.Text) Then
                    Throw New Exception("La cantidad a confirmar es mayor a la solicitada.")
                End If
            End If
            If CDbl(Me.txtCantidad.Text) <> CDbl(Me.txtCCantidad.Text) And vContCantInf > 0 Then
                If CDbl(Me.txtCantidad.Text) > CDbl(Me.txtCCantidad.Text) Then
                    'Maximiliano Privitera
                    FlgNewPicking = Fe.GetFlgGenNewPicking(ClienteId)
                    If FlgNewPicking Then
                        If CambioContenedora Then
                            If Not Fe.CambioContenedoraPicking(Rl_Id, Me.ViajeId, Me.txtProducto_ID.Text, CDbl(Me.txtCCantidad.Text), vUsr.CodUsuario) Then
                                Throw New Exception("El cambio de contenedora no fue exitoso.")
                            End If
                        End If
                        '-------------------------------------------------------------------------------------------------------------------------------------------------
                        'Comento esto porque en el cambio de contenedora esto carece de sentido porque si o si funcionalmente debe generar la diferencia primero
                        'Rta = MsgBox("¿Desea generar una nueva tarea de picking con la cantidad restante?", MsgBoxStyle.YesNo, FrmName)
                        '-------------------------------------------------------------------------------------------------------------------------------------------------
                        If Not CambioContenedora Then
                            Rta = MsgBox("¿Desea generar una nueva tarea de picking con la cantidad restante?", MsgBoxStyle.YesNo, FrmName)
                        Else
                            Rta = vbNo
                        End If
                        If Rta = vbYes Then
                            trans = SQLc.BeginTransaction()
                            If Me.blnConversion Then
                                CantidadConvertida = CDbl(Me.txtCCantidad.Text) * CDbl(Me.intFactorConversion)
                            Else
                                CantidadConvertida = CDbl(Me.txtCCantidad.Text)
                            End If
                            If Not Fe.Fin_Picking_Split(vUsr.CodUsuario, Me.ViajeId, Me.txtProducto_ID.Text, Me.Ubicacion, CDbl(CantidadConvertida), PalletId, PalletProp1, vRuta, NroLote, Me.vSerie, trans) Then
                                'cschenk
                                trans.Rollback()
                            Else
                                'cschenk
                                'grabo series
                                If ProdConSerieEgr Then
                                    If Not GrabarDSSeries(trans) Then
                                        trans.Rollback()
                                        Exit Sub
                                    End If
                                End If
                                trans.Commit()
                            End If
                            Rta = MsgBox("¿Desea cerrar el pallet y abrir otro?", MsgBoxStyle.YesNo, FrmName)
                            If Rta = vbYes Then
                                If Not GetPalletoCarro(PalletId, Cmd) Then
                                End If
                                ImpresionPallet(2)
                                ClearAll()
                                NewTomaTarea()
                            Else
                                ClearAll()
                                NewTomaTarea()
                            End If
                            Exit Try
                        Else
                            Rta = MsgBox("Las Cantidades son diferentes" & vbNewLine & "¿Desea continuar?", MsgBoxStyle.YesNo, FrmName)
                            If Rta = vbYes Then
                                If Rl_Id > 0 Then
                                    If Not Fe.CambioContenedoraPicking(Rl_Id, Me.ViajeId, Me.txtProducto_ID.Text, CDbl(Me.txtCCantidad.Text), vUsr.CodUsuario) Then
                                        Throw New Exception("El cambio de contenedora no fue exitoso.")
                                    End If
                                End If
                                trans = SQLc.BeginTransaction()
                                Me.Ubicacion = Me.txtPosicion.Text
                                If Not Fe.Fin_Picking_Contenedoras(vUsr.CodUsuario, Me.ViajeId, Me.txtProducto_ID.Text, Me.Ubicacion, CDbl(Me.txtCCantidad.Text), PalletId, PalletProp1, vRuta, NroLote, Me.vSerie, trans) Then
                                    'Q va aca?? Maximiliano Privitera
                                    'cschenk
                                    trans.Rollback()
                                Else
                                    'cschenk
                                    'grabo series
                                    If ProdConSerieEgr Then
                                        If Not GrabarDSSeries(trans) Then
                                            trans.Rollback()
                                            Exit Sub
                                        End If
                                    End If
                                    trans.Commit()
                                    ClearAll()
                                    If Fe.Picking_Pendiente(vUsr.CodUsuario, PickingPalletCompleto, DsEgreso, "pcur") Then
                                        If DsEgreso.Tables("PCUR") IsNot Nothing Then
                                            Me.cmdCerrarPallet.Enabled = True
                                            If DsEgreso.Tables("PCUR").Rows(0)("Pallet_picking") IsNot Nothing And Not IsDBNull(DsEgreso.Tables("PCUR").Rows(0)("Pallet_picking")) Then
                                                PalletId = DsEgreso.Tables("PCUR").Rows(0)("Pallet_picking")
                                                Me.lblPallet.Text = "Pallet Picking: " & PalletId
                                            Else
                                                GetPalletoCarro(PalletId, Cmd)
                                                Me.lblPallet.Text = "Pallet Picking: " & PalletId
                                            End If
                                            Me.lblPallet.Visible = True
                                            Me.cmdApertura.Enabled = False
                                            Me.cmdSaltoPicking.Enabled = True
                                            SetValuesScreen()
                                            Exit Try
                                        Else
                                            NewTomaTarea()
                                        End If
                                    End If
                                    Exit Try
                                End If
                            Else
                                Me.txtCCantidad.Text = ""
                                Me.txtCCantidad.Focus()
                                Exit Try
                            End If
                        End If
                    End If
                Else
                    'Cuando el valor a pickear es mayos que la cantidad de la tarea pero no excede el valor total a pickear del producto
                    ProductoId = DsEgreso.Tables("PCUR").Rows(0)("PRODUCTO_ID").ToString
                    ValorTotalxProducto = Fe.GetCantidadxProducto(DsEgreso.Tables("pcur").Rows(0)("VIAJE_ID").ToString, ProductoId)
                    If CDbl(Me.txtCCantidad.Text) > ValorTotalxProducto Then
                        lblMsg.Text = "La cantidad ingresada es" & vbNewLine & " mayor a la cantidad total a pickear para el producto."
                        Me.txtCCantidad.SelectAll()
                        Exit Try
                    Else
                        If Rl_Id > 0 Then
                            If Not Fe.CambioContenedoraPicking(Rl_Id, Me.ViajeId, Me.txtProducto_ID.Text, CDbl(Me.txtCCantidad.Text), vUsr.CodUsuario) Then
                                Throw New Exception("El cambio de contenedora no fue exitoso.")
                            End If
                        End If
                        trans = SQLc.BeginTransaction()
                        Me.Ubicacion = Me.txtPosicion.Text
                        If Fe.Fin_Picking_Contenedoras(vUsr.CodUsuario, Me.ViajeId, Me.txtProducto_ID.Text, Me.Ubicacion, CDbl(Me.txtCCantidad.Text), PalletId, PalletProp1, vRuta, NroLote, Me.vSerie, trans) Then
                            'cschenk
                            'grabo series
                            If ProdConSerieEgr Then
                                If Not GrabarDSSeries(trans) Then
                                    MsgBox("ERR EN GrabarDSSeries 3")
                                    trans.Rollback()
                                    Exit Sub
                                End If
                            End If
                            trans.Commit()
                            ClearAll()
                            NewTomaTarea()
                        Else
                            'cschenk-----
                            trans.Rollback()
                            '------------
                            If Fe.CerrarPallet Then
                                ImpresionPallet(2)
                                ClearAll(True)
                                Me.ViajeId = "0"
                                Me.Text = "Picking."
                            End If
                        End If
                        Exit Try
                    End If
                End If
            End If
            If CDbl(Me.txtCantidad.Text) = CDbl(Me.txtCCantidad.Text) Or vContCantInf > 0 Then
                If Rl_Id > 0 Then
                    If Not Fe.CambioContenedoraPicking(Rl_Id, Me.ViajeId, Me.txtProducto_ID.Text, CDbl(Me.txtCCantidad.Text), vUsr.CodUsuario) Then
                        Throw New Exception("El cambio de contenedora no fue exitoso.")
                    End If
                End If
                trans = SQLc.BeginTransaction()
                Me.Ubicacion = Me.txtPosicion.Text
                'If Fe.Fin_Picking_Contenedoras(vUsr.CodUsuario, Me.ViajeId, Me.txtProducto_ID.Text, Me.Ubicacion, CDbl(Me.txtCCantidad.Text), PalletId, PalletProp1, vRuta, NroLote, Me.vSerie, trans) Then
                If Fe.Fin_Picking_Contenedoras(vUsr.CodUsuario, Me.ViajeId, Me.txtProducto_ID.Text, Me.Ubicacion, GetCantidadConvertida(), PalletId, PalletProp1, vRuta, NroLote, Me.vSerie, trans) Then
                    'cschenk
                    'grabo series
                    If ProdConSerieEgr Then
                        If Not GrabarDSSeries(trans) Then
                            MsgBox("ERR EN GrabarDSSeries 3")
                            trans.Rollback()
                            Exit Sub
                        End If
                    End If
                    trans.Commit()
                    ClearAll()
                    NewTomaTarea()
                Else
                    'cschenk-----
                    trans.Rollback()
                    '------------
                    If Fe.CerrarPallet Then
                        ImpresionPallet(2)
                        ClearAll(True)
                        Me.ViajeId = "0"
                        Me.Text = "Picking."
                    End If

                End If
            Else
                If CDbl(Me.txtCantidad.Text) > CDbl(Me.txtCCantidad.Text) Then
                    vContCantInf = vContCantInf + 1
                    Me.lblMsg.Text = "La cantidad ingresada es" & vbNewLine & "menor a la solicitada."
                    Me.txtCCantidad.SelectAll()
                Else
                    vContCantInf = vContCantInf + 1
                    ConfirmarCantidadCont()
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, FrmName)
            If Not IsNothing(trans) Then
                trans.Rollback()
            End If
        End Try

    End Sub

    Private Function GetCantidadConvertida() As Double
        Dim Calc As Double
        Try
            If Me.blnConversion Then
                Calc = CDbl(Me.txtCCantidad.Text) * Me.intFactorConversion
            Else : Calc = CDbl(Me.txtCCantidad.Text)
            End If
            Return Calc
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Function

    '--
    Private Function ValidaEAN_DUN() As Boolean
        Dim vCodes As frmCODIGOS, DSConversion As DataSet, Calc As Integer
        Try
            vCodes = New frmCODIGOS
            vCodes.PalletEgr = DsEgreso.Tables("pcur").Rows(0)("PALLET").ToString
            vCodes.ValCodEgr = Me.BlnValCod
            vCodes.Producto = Me.txtProducto_ID.Text
            vCodes.lblProducto_ID.Text = "Codigo de Producto: " & Me.txtProducto_ID.Text
            vCodes.lblDescripcion.Text = txtDescripcion.Text
            vCodes.cliente_id = Me.ClienteId
            vCodes.CantidadEgreso = CDbl(Me.txtCantidad.Text)
            vCodes.ShowDialog()
            vCodes.lblProducto_ID.Text = ""

            If Not vCodes.Cancel Then
                If Not vCodes.ValidCode Then
                    Return False
                Else
                    DSConversion = vCodes.Conversion
                    If DSConversion.Tables.Count > 0 Then
                        blnConversion = True
                        Me.intFactorConversion = DSConversion.Tables(0).Rows(0)(3)
                        'Hacemos la conversion?
                        Calc = Int(CDbl(txtCantidad.Text) / CDbl(Me.intFactorConversion))
                        Me.txtCantidad.Text = Calc

                    Else : Me.blnConversion = False
                    End If
                End If
            Else : Return False
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            vCodes = Nothing
        End Try
    End Function

    Private Sub NewTomaTarea()
        Dim Fe As New FuncionesEgreso
        Dim Cmd As SqlCommand
        Cmd = SQLc.CreateCommand
        Fe.Cmd = Cmd
        If (PickingVehiculos) And (SolCalleNave = True) Then
            If Fe.GetTareasPicking(vUsr.CodUsuario, ViajeId, DsEgreso, "pcur", PalletId, vRuta, PickingPalletCompleto, vUsr.NaveCalle) Then
                SetValuesScreen()
            Else
                If Fe.CerrarPallet = True Then
                    ImpresionPallet(2)
                    ClearAll()
                End If
            End If
        ElseIf (PickingVehiculos) And (SolCalleNave = False) Then
            If Fe.GetTareasPicking(vUsr.CodUsuario, ViajeId, DsEgreso, "pcur", PalletId, vRuta, PickingPalletCompleto, vUsr.NaveCalle) Then
                SetValuesScreen()
            Else
                If Fe.CerrarPallet = True Then
                    ImpresionPallet(2)
                    ClearAll()
                End If
            End If
        Else
            If Fe.GetTareasPicking(vUsr.CodUsuario, ViajeId, DsEgreso, "pcur", PalletId, vRuta, PickingPalletCompleto, vUsr.NaveCalle) Then
                SetValuesScreen()
            Else
                If Fe.CerrarPallet = True Then
                    ImpresionPallet(2)
                    ClearAll()
                End If
            End If
        End If
        Fe = Nothing
        Cmd = Nothing
    End Sub

    Private Sub ClearAll(Optional ByVal PalletPicking As Boolean = False)
        blnSalida = True
        Ubicacion = ""
        UbicacionID = 0
        Me.blnConversion = False
        Me.intFactorConversion = 0
        Try
            If DsEgreso.Tables("PCUR") IsNot Nothing Then
                DsEgreso.Tables.Remove("PCUR")
            End If
        Catch ex As Exception
        End Try
        vContCantInf = 0
        If PalletPicking = True Then
            PalletId = 0
            'Me.ViajeId = "0"
            Me.vRuta = ""
            Me.lblPallet.Visible = False
            Me.cmdApertura.Enabled = True
            Me.blnSalida = True
            Ubicacion = ""
            'If DsEgreso.Tables("PCUR") IsNot Nothing Then
            '    DsEgreso.Tables.Remove("PCUR")
            'End If
            PalletProp1 = ""
            vContCantInf = 0
            Me.cmdCerrarPallet.Enabled = False
        End If
        Me.cmdUbicContenedora.Visible = False
        Me.btn_Series_Esp.Visible = False
        'Me.btn_Series_Esp.Text = "F9) Series Esp.: On"
        Me.CambioContenedora = False
        Me.lblCodigo.Visible = False
        Me.txtCodigo.Text = ""
        Me.txtCodigo.Visible = False
        Me.lblPallet.Visible = False
        Me.cmdCerrarPallet.Enabled = False
        Me.cmdApertura.Enabled = True
        Me.lblUnidad.Text = ""
        Me.lblPalletIng.Text = "Pallet :"
        Me.lblPalletIng.Visible = False
        Me.lblProducto_ID.Visible = False
        Me.txtProducto_ID.Visible = False
        Me.txtProducto_ID.Text = ""
        Me.lblDescripcion.Visible = False
        Me.txtDescripcion.Visible = False
        Me.txtDescripcion.Text = ""
        Me.lblCantidad.Visible = False
        Me.txtCantidad.Visible = False
        Me.txtCantidad.Text = ""
        Me.lblPosicion.Visible = False
        Me.txtPosicion.Visible = False
        Me.txtPosicion.Text = ""
        Me.lblCCantidad.Visible = False
        Me.txtCCantidad.Visible = False
        Me.txtCCantidad.Text = ""
        Me.lblMsg.Text = ""
        Me.lblContenedora.Text = ""
        Me.lblContenedora.Visible = False
        Me.lblNroLote.Visible = False
        Me.lblDatoLote.Visible = False
        Me.lblPartida.Visible = False
        Me.lblDatoPartida.Visible = False
        IsContenedora = False
        CantidadMaxCont = 0
        Rl_Id = 0
        Application.DoEvents()
    End Sub

    Private Function AddItem(ByRef Ds As DataSet, ByVal TableName As String, ByVal DocId As Object, _
                             ByVal NroLinea As Object, ByVal ProductoId As Object, ByVal Desc As Object, _
                             ByVal Cant As Object, ByVal NaveCod As Object, ByVal PosId As Object, _
                             ByVal Ruta As Object, ByVal Prop1 As Object) As Boolean
        Try
            Ds.Tables(TableName).Rows.Add(DocId, NroLinea, ProductoId, Desc, Cant, NaveCod, PosId, Ruta, Prop1, 0)
        Catch ex As Exception
            MsgBox("AddItem: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Function

    Private Sub txtDescripcion_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescripcion.GotFocus
        If Me.txtCCantidad.Visible = True Then
            Me.txtCCantidad.Focus()
        ElseIf Me.txtCCantidad.Visible = False Then
            Me.txtPosicion.Focus()
        End If
    End Sub

    Private Sub cmdCompletados_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCompletados.Click
        MostrarFinalizados()
        If Me.txtCCantidad.Visible = True Then
            Me.txtCCantidad.Focus()
        ElseIf Me.txtCCantidad.Visible = False Then
            Me.txtPosicion.Focus()
        End If
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        ValidacionSalida()
    End Sub

    Private Sub cmdApertura_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApertura.Click
        AbrirPallet()
    End Sub

    Private Sub cmdCerrarPallet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCerrarPallet.Click
        CerrarPallet()
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

    Public Sub ValidadCaracterNumerico(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Decimales As Boolean)
        If Asc(e.KeyChar) <> 8 Then
            If (Asc(e.KeyChar) < 46) Or (Asc(e.KeyChar) > 57) Then
                e.Handled = True
            End If
        End If
        If (Asc(e.KeyChar) = 13) Then
            e.Handled = False
        End If
        If Decimales = True Then
            Select Case Asc(e.KeyChar)
                Case 46, 44
                    e.Handled = True
            End Select
        End If
    End Sub

    Private Sub cmdSaltoPicking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaltoPicking.Click
        JumpPick()
    End Sub

    Private Sub JumpPick()
        If MsgBox("¿Confirma el Salto de Picking?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
            Dim Cmd As SqlCommand
            Dim Fe As New FuncionesEgreso
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Fe.Cmd = Cmd
                If Not PickingVehiculos Then
                    If Fe.JumpPicking(vUsr.CodUsuario, Me.ViajeId, Me.txtProducto_ID.Text, _
                                      Ubicacion, PalletProp1, vRuta) Then
                        ClearAll()
                        NewTomaTarea()
                    Else
                        If txtCCantidad.Visible Then
                            Me.txtCantidad.Focus()
                        Else
                            If Me.txtPosicion.Visible Then
                                Me.txtPosicion.Focus()
                            End If
                        End If
                    End If
                Else
                    If Fe.JumpPickingVH(vUsr.CodUsuario, Me.ViajeId, Me.txtProducto_ID.Text, _
                                        Ubicacion, PalletProp1, vRuta) Then
                        ClearAll()
                        NewTomaTarea()
                    Else
                        If txtCCantidad.Visible Then
                            Me.txtCantidad.Focus()
                        Else
                            If Me.txtPosicion.Visible Then
                                Me.txtPosicion.Focus()
                            End If
                        End If
                    End If
                End If
            End If
            Cmd = Nothing
            Fe = Nothing
        Else
            If txtCCantidad.Visible Then
                Me.txtCantidad.Focus()
            Else
                If Me.txtPosicion.Visible Then
                    Me.txtPosicion.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub CambioVehiculo()
        'Dim frmEgreso As New frmEgreso
        'frmEgreso.PickingVehiculos = PickVehiculo
        'frmEgreso.SolCalleNave = IIf(vCalleNave = "1", True, False)
        'frmEgreso.ShowDialog()
        'frmEgreso = Nothing

        If PickingVehiculos Then
            Dim vh As New frmVehiculo
            vh.ShowDialog()
            If vh.CambioOk Then
                liberarXCambios(Me.ViajeId, vUsr.CodUsuario)
                IniForm()
            End If
            Me.cmdApertura.Enabled = True
            vh = Nothing
            If Me.SolCalleNave = True Then
                Me.lblVehiculo.Text = "Vehiculo: " & vUsr.Vehiculo & " Nave-Calle: " & vUsr.NaveCalle
            Else
                Me.lblVehiculo.Text = "Vehiculo: " & vUsr.Vehiculo
            End If
        End If
    End Sub

    Private Sub cmdCambioVH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCambioVH.Click
        CambioVehiculo()
    End Sub

    Private Sub cmdCambioCalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCambioCalle.Click
        CambioCalle()
    End Sub

    Private Sub CambioCalle()
        Try
            If PickingVehiculos Then
                Dim NC As New frmCalleNave
                NC.ShowDialog()
                If NC.CambioOk Then
                    liberarXCambios(Me.ViajeId, vUsr.CodUsuario)
                    IniForm()
                End If
                NC = Nothing
                Me.lblVehiculo.Text = "Vehiculo: " & vUsr.Vehiculo & ", Nave-Calle: " & vUsr.NaveCalle
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub liberarXCambios(ByVal ViajeId As String, ByVal Usuario As String)
        Dim xCMD As SqlCommand
        Dim Pa As SqlParameter
        xCMD = SQLc.CreateCommand
        Try
            xCMD.CommandText = "DBO.Frontera_liberarTareaPicking"
            xCMD.CommandType = CommandType.StoredProcedure
            Pa = New SqlParameter("@pViaje_Id", SqlDbType.VarChar, 50)
            Pa.Value = ViajeId
            xCMD.Parameters.Add(Pa)
            Pa = Nothing

            Pa = New SqlParameter("@pUsuario_id", SqlDbType.VarChar, 50)
            Pa.Value = Trim(UCase(Usuario))
            xCMD.Parameters.Add(Pa)
            Pa = Nothing

            xCMD.ExecuteNonQuery()
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            xCMD = Nothing
            Pa = Nothing
        End Try
    End Sub

    Private Sub txtCodigo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCodigo.KeyUp
        Dim vCod As String = ""
        If e.KeyValue = 13 Then
            vCod = Replace(Trim(UCase(Me.txtCodigo.Text)), "COD:", "")
            If Trim(vCod) = Trim(Me.strCodigoPick) Then
                Me.lblMsg.Text = ""
                If Not ProcesoIngresoSeries() Then
                    MsgBox("No se puede seguir si no se ingresan los nros. de series", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, FrmName)
                    Exit Sub
                End If
                Me.lblCCantidad.Visible = True
                Me.txtCCantidad.Visible = True
                Me.txtCCantidad.Focus()
            Else
                Me.lblMsg.Text = "El codigo ingresado no es correcto."
                Me.txtCodigo.Text = ""
                Me.txtCodigo.Focus()
            End If
        End If
    End Sub

    Private Function ProcesoIngresosSeries(ByVal cliente_id As String, ByVal producto_id As String, ByVal viaje_id As String, _
                ByRef cantidad As Integer, ByVal ruta As String, ByVal pos_cod As String, ByVal pallet As String) As Int16
        Dim fe As New FuncionesEgreso
        Dim ing_series As New frmSeries
        Dim Cmd As SqlCommand

        Try
            Cmd = SQLc.CreateCommand
            fe.Cmd = Cmd

            If fe.IsSeriesEgreso(producto_id, cliente_id) Then
                ProdConSerieEgr = True
                DS_Series = New Data.DataSet
                If Not fe.GetPickingIdsXPickingPorducto(vUsr.CodUsuario, PickingPalletCompleto, cliente_id, _
                        viaje_id, producto_id, ruta, pos_cod, pallet, DS_Series) Then
                    Return 2 'ERROR AL BUSCAR LOS PICKING_IDS
                End If

                ing_series.DS = DS_Series 'LE PASO LOS PICKING_IDS
                ing_series.ShowDialog()

                If ing_series.Cantidad = 0 Then
                    Return 2 'POR ALGUNA RAZON NO ME PASO INGRESO LOS NROS DE SERIES
                End If
                DS_Series = Nothing
                DS_Series = ing_series.DS.Copy
                cantidad = ing_series.Cantidad
                Return 0 'SERIES INGRESADAS CORRECTAMENTE
            Else
                ProdConSerieEgr = False
                Return 1 'NO HACE FALTA SERIES
            End If
        Catch EX As Exception
            MsgBox(EX.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            fe = Nothing
            ing_series = Nothing

        End Try


    End Function

    Private Function ProcesoIngresoSeries() As Boolean
        Try

            Dim result As Int16
            Dim cant As Integer
            Dim Cliente As String = ""
            Dim Producto As String = ""
            Dim Viaje As String = ""
            Dim Ruta As String = ""
            Dim Posicion As String = ""
            Dim Pallet As String = ""
            Cliente = IIf(IsDBNull(DsEgreso.Tables("PCUR").Rows(0)("CLIENTE_ID")), "", DsEgreso.Tables("PCUR").Rows(0)("CLIENTE_ID"))
            Producto = IIf(IsDBNull(DsEgreso.Tables("PCUR").Rows(0)("PRODUCTO_ID")), "", DsEgreso.Tables("PCUR").Rows(0)("PRODUCTO_ID"))
            Viaje = IIf(IsDBNull(DsEgreso.Tables("PCUR").Rows(0)("VIAJE_ID")), "", DsEgreso.Tables("PCUR").Rows(0)("VIAJE_ID"))
            Ruta = IIf(IsDBNull(DsEgreso.Tables("PCUR").Rows(0)("RUTA")), "", DsEgreso.Tables("PCUR").Rows(0)("RUTA"))
            Posicion = IIf(IsDBNull(DsEgreso.Tables("PCUR").Rows(0)("POSICION_COD")), "", DsEgreso.Tables("PCUR").Rows(0)("POSICION_COD"))
            Pallet = IIf(IsDBNull(DsEgreso.Tables("PCUR").Rows(0)("PALLET")), "", DsEgreso.Tables("PCUR").Rows(0)("PALLET"))
            'llama al proceso de ingreso de los picking_ids
            result = ProcesoIngresosSeries(Cliente, Producto, Viaje, cant, Ruta, Posicion, Pallet)
            If result = 0 Then
                'completando la cantidad
                Me.txtCCantidad.Text = cant

            ElseIf result = 2 Then
                'retorno false, hubo error al buscar los picking_ids 
                'y no se puede seguir porque el producto esta marcado con series_egr
                Return False
            End If
            'para result=0 o result=1 es correcto, 0=se cargaron series, 1=no se precisan series
            Return True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ProcesoIngresoSeries")
            Return False
        End Try
    End Function

    Function GrabarDSSeries(ByRef trans As SqlTransaction) As Boolean
        Try
            Dim fe As New FuncionesEgreso
            Dim i As Long
            If Not IsNothing(DS_Series) Then
                For i = 0 To DS_Series.Tables(0).Rows.Count - 1
                    If Not IsDBNull(DS_Series.Tables(0).Rows(i)("series")) AndAlso (DS_Series.Tables(0).Rows(i)("series") <> "") Then
                        If Not fe.GrabarSeries(DS_Series.Tables(0).Rows(i)("picking_id"), DS_Series.Tables(0).Rows(i)("series"), trans) Then
                            Return False
                        End If
                    End If
                Next
                DS_Series = Nothing
                Return True
            Else
                Err.Raise(513, "GrabarDSSeries", "No se obtubieron los números de series, y son necesarios para grabar.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message + " - GrabarDSSeries", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "GrabarDSSeries")
            Return False

        End Try
    End Function

    Private Function ImpresionPallet(ByVal Tipo As String) As Boolean
        Dim xSQL As String
        Dim Cmd As SqlCommand
        Dim FE As New FuncionesEgreso
        Try
            If Not FE.SerializadoBestFit(Me.Ubicacion, Me.PalletProp1) Then
                If VerifyConnection(SQLc) Then
                    Cmd = SQLc.CreateCommand
                    Cmd.CommandType = Data.CommandType.Text
                    'LRojas Tracker ID 3806 05/03/2012: Inserción de Usuario para Demonio de Impresion
                    xSQL = "insert into IMPRESION_APF values(" & Me.PalletId & ",'" & Tipo & "','0','', '" & vUsr.CodUsuario & "')"
                    Cmd.CommandText = xSQL
                    Cmd.ExecuteNonQuery()
                    Return True
                Else
                    MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
                End If
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            Cmd = Nothing
            FE = Nothing
        End Try
    End Function

    Private Function Validarlote() As Boolean
        Dim vLote As FrmValidaLote
        Try
            vLote = New FrmValidaLote
            vLote.pNroLote = NroLote
            vLote.pViaje = ViajeId
            vLote.pProducto = txtProducto_ID.Text & " - " & txtDescripcion.Text
            vLote.ShowDialog()
            Return vLote.LoteValido
        Catch ex As Exception
            MsgBox("Error al validar lote: " & ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            vLote = Nothing
        End Try
    End Function

    Private Sub cmdUbicContenedora_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUbicContenedora.Click
        AbrirPickingContenedoras()
    End Sub

    Private Sub txtPosicion_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPosicion.KeyPress
        If Char.IsLower(e.KeyChar) Then
            Me.txtPosicion.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub txtCCantidad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCCantidad.TextChanged

    End Sub

    Private Sub txtPosicion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPosicion.TextChanged

    End Sub

    Private Sub txtCodigo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodigo.TextChanged

    End Sub

    Private Sub btn_Series_Esp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Series_Esp.Click
        ActivaSerieEspecifica()
    End Sub

    Private Sub ActivaSerieEspecifica()
        If Me.btn_Series_Esp.Text = "F9) Series Esp.: On" Then
            Me.btn_Series_Esp.Text = "F9) Series Esp.: Off"
        Else
            Me.btn_Series_Esp.Text = "F9) Series Esp.: On"
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConversiones.Click
        BuscarConversiones()
    End Sub

    Private Sub BuscarConversiones()
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        Dim frm As New frmEgresoConversiones
        Try
            If VerifyConnection(SQLc) Then
                If Me.txtCantidad.Visible = True Then
                    xCmd = SQLc.CreateCommand
                    xCmd.CommandText = "[dbo].[MOB_PICKING_CALCULADOR_POR_CODIGO]"
                    xCmd.CommandType = CommandType.StoredProcedure
                    Da = New SqlDataAdapter(xCmd)

                    Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                    Pa.Value = Me.ClienteId
                    xCmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                    Pa.Value = Trim(UCase(Me.txtProducto_ID.Text))
                    xCmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@CANTIDAD", SqlDbType.Float)
                    Pa.Value = CDbl(Me.txtCantidad.Text)
                    xCmd.Parameters.Add(Pa)

                    Da.Fill(Ds, "CONVERSION")
                    If Ds.Tables("CONVERSION").Rows.Count > 0 Then
                        frm.Datos = Ds
                        frm.ShowDialog()
                    Else
                        MsgBox("No se encontraron conversiones para el producto " & Me.txtProducto_ID.Text & ".")
                    End If
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Information, FrmName)
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
            Da = Nothing
            Ds = Nothing
        End Try
    End Sub

    Private Function Valida2D() As Boolean
        Dim DA As SqlDataAdapter, MyCmd As SqlCommand, Ds As New DataSet

        Try
            If VerifyConnection(SQLc) Then
                MyCmd = SQLc.CreateCommand
                MyCmd.CommandText = "SELECT ISNULL(VALOR,'0') FROM SYS_PARAMETRO_PROCESO WHERE PROCESO_ID = 'WMOV' AND SUBPROCESO_ID = 'EGR_SERIE' AND PARAMETRO_ID = 'SERIE_2D'"
                MyCmd.CommandType = CommandType.Text
                DA = New SqlDataAdapter(MyCmd)
                DA.Fill(Ds, "SERIE_2D")

                If Ds.Tables("SERIE_2D").Rows(0)(0).ToString = "0" Then
                    Return False
                Else
                    Return True
                End If
            Else : Return False
            End If
        Catch ExSQL As SqlException
            MsgBox("Valida2D SQL: " & ExSQL.Message)
            Return False
        Catch ex As Exception
            MsgBox("Valida2D: " & ex.Message)
            Return False
        Finally
            MyCmd = Nothing
            DA = Nothing
            Ds = Nothing
        End Try
    End Function

    Public Function SerieEgreso2D(ByRef Confirmacion As Boolean) As Boolean
        Try
            o2D.SerializacionEgreso = True
            o2D.CodigoViaje = Me.ViajeId
            o2D.GuardarAuditoria = True
            o2D.ProductoSolicitado = Me.txtProducto_ID.Text
            o2D.CLIENTE_ID = Me.ClienteId
            o2D.Decode(Me.txtPosicion.Text)

            If Me.txtProducto_ID.Text <> o2D.PRODUCTO_ID Then
                MsgBox("El producto escaneado no corresponde al codigo de producto solicitado.", MsgBoxStyle.Information, FrmName)
                Me.txtPosicion.Text = ""
                Me.txtPosicion.Focus()
                SerieEgreso2D = False
                Exit Function
            End If

            If o2D.ErrorMaxSeries = True Then
                MsgBox("Se superó el máximo permitido de lectura de series.", MsgBoxStyle.Information, FrmName)
                Me.txtPosicion.Text = ""
                Me.txtPosicion.Focus()
                Return False
            End If

            If o2D.QtySeries = 0 Then
                MsgBox("El codigo ingresado no genero ninguna serie.", MsgBoxStyle.Information, FrmName)
                Me.txtPosicion.Text = ""
                Me.txtPosicion.Focus()
                Return False
            End If

            If o2D.QtySeries > Me.txtCantidad.Text Then
                MsgBox("La cantidad de series en la lectura supera a la cantidad solicitada.", MsgBoxStyle.Information, FrmName)
                Me.txtPosicion.Text = ""
                Me.txtPosicion.Focus()
                SerieEgreso2D = False
                Exit Function
            End If

            If o2D.QtySeries > 0 Then
                'aqui puedo llegar a tener que confirmar mas de una serie Masivamente en caso de que la cantidad
                'de series a confirmar sea igual a la cantidad de series de la contenedora.
                If o2D.GuardarSeriesEgreso(Me.ClienteId, Me.ViajeId, Trim(Replace(Me.lblContenedora.Text, "Contenedora:", "")), Confirmacion) Then
                    If Confirmacion Then
                        ClearAll()
                        NewTomaTarea()
                        SerieEgreso2D = True
                        Exit Function
                    End If
                Else
                    Me.txtPosicion.Text = ""
                    Me.txtPosicion.Focus()
                    SerieEgreso2D = False
                    Exit Function
                End If
            End If
            If validaSerieIngresada() Then
                'Pick por serie pero solo voy descontando de a 1 en el stock reservado.
                Me.vSerie = Me.txtPosicion.Text
                PickSerieDeAUna()
                SerieEgreso2D = True
            Else
                Me.txtPosicion.Text = ""
                MessageBox.Show("La serie " + Me.txtPosicion.Text + " ya fue ingresada.", "Serie ya ingresada.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
                Me.txtPosicion.Focus()
                SerieEgreso2D = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Public Function SerieEgreso(ByRef Confirmacion As Boolean) As Boolean
        Try
            o2D.SerializacionEgreso = True
            o2D.Decode(Me.txtPosicion.Text)

            If o2D.QtySeries > 0 Then
                If Me.txtProducto_ID.Text <> o2D.PRODUCTO_ID Then
                    MsgBox("El producto escaneado no corresponde al codigo de producto solicitado.", MsgBoxStyle.Information, FrmName)
                    Me.txtPosicion.Text = ""
                    Me.txtPosicion.Focus()
                    SerieEgreso = False
                    Exit Function
                End If
                'aqui puedo llegar a tener que confirmar mas de una serie Masivamente en caso de que la cantidad
                'de series a confirmar sea igual a la cantidad de series de la contenedora.
                If o2D.GuardarSeriesEgreso(Me.ClienteId, Me.ViajeId, Trim(Replace(Me.lblContenedora.Text, "Contenedora:", "")), Confirmacion) Then
                    If Confirmacion Then
                        ClearAll()
                        NewTomaTarea()
                        SerieEgreso = True
                        Exit Function
                    End If
                Else
                    Me.txtPosicion.Text = ""
                    Me.txtPosicion.Focus()
                    SerieEgreso = False
                    Exit Function
                End If
            End If
            If validaSerieIngresada() Then
                'Pick por serie pero solo voy descontando de a 1 en el stock reservado.
                Me.vSerie = Me.txtPosicion.Text
                PickSerieDeAUna()
                SerieEgreso = True
            Else
                Me.txtPosicion.Text = ""
                MessageBox.Show("La serie " + Me.txtPosicion.Text + " ya fue ingresada.", "Serie ya ingresada.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
                Me.txtPosicion.Focus()
                SerieEgreso = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function
End Class