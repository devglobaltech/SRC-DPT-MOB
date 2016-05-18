Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text


Public Class frmMenuPrincipal
    Private PickVehiculo As Boolean = False
    Private ConfPos As Boolean = False
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private Const FrmName As String = "Menu Principal"
    Private objWSC As clsWinSockCliente

    Public Property Win_Sock_Cliente() As clsWinSockCliente
        Get
            Win_Sock_Cliente = objWSC
        End Get
        Set(ByVal Value As clsWinSockCliente)
            objWSC = Value
        End Set
    End Property

    Private Sub frmMenuPrincipal_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        If CerrarAplicacion Then
            Try
                TrdLic.Abort()
            Catch ex As Exception
            End Try
            Application.Exit()
        End If
    End Sub


    Private Sub frmMenuPrincipal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Da As SqlDataAdapter
        Dim Ds As New System.Data.DataSet
        Dim drDSRow As Data.DataRow
        Dim drNewRow As Data.DataRow
        Dim dt As New Data.DataTable
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter
        Try
            If Me.Size.Width = 480 Then
                ScreenSize = 480
            End If
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.CARGA_MENU"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                xCmd.Connection = SQLc
                Da.Fill(Ds, "MENUS")
                dt.Columns.Add("CODIGO_ID", GetType(System.String))
                dt.Columns.Add("DESCRIPCION", GetType(System.String))
                If Ds.Tables("MENUS").Rows.Count > 0 Then
                    'Hay mas de un cliente, los cargo en el combo.
                    For Each drDSRow In Ds.Tables("MENUS").Rows()
                        drNewRow = dt.NewRow()
                        drNewRow("CODIGO_ID") = drDSRow("CODIGO_ID")
                        drNewRow("DESCRIPCION") = drDSRow("DESCRIPCION")
                        dt.Rows.Add(drNewRow)
                    Next
                    drNewRow = dt.NewRow()
                    drNewRow("CODIGO_ID") = "999"
                    drNewRow("DESCRIPCION") = "SALIR"
                    dt.Rows.Add(drNewRow)
                    With LBMenu
                        .DataSource = Nothing
                        .DataSource = dt
                        .DisplayMember = "DESCRIPCION"
                        .ValueMember = "CODIGO_ID"
                        .SelectedIndex = 0
                    End With
                Else
                    Me.Close()
                    Exit Sub
                End If
            Else : MsgBox(SQLError, MsgBoxStyle.Exclamation, FrmName)
            End If

        Catch SQLEx As SqlException
            MsgBox("ExisteNavePosicion SQL: " & SQLEx.Message)
        Catch ex As Exception
            MsgBox("ExisteNavePosicion: " & ex.Message)
        Finally
            Da = Nothing
            Ds = Nothing
            Pa = Nothing
            LBMenu.Focus()
            If CerrarAplicacion Then
                Try
                    TrdLic.Abort()
                Catch ex As Exception
                End Try
                Application.Exit()
            End If
        End Try
    End Sub
    Private Function VerifyServer() As Boolean
        Dim clientSocket As New System.Net.Sockets.TcpClient(), Output As String = ""
        Try
            'Este artilugio lo necesito para saber si puedo conectarme al server. Como la conexion es asincronica me contesta en
            'en una rutina posterior a la que vuelvo a reconectar. De esta forma sincronicamente me entero si el server esta activo
            'o si puedo llegar a el.
            clientSocket.Connect(ScA.IPAdd, ScA.nPort)
            Dim data(255) As [Byte]
            data = System.Text.Encoding.ASCII.GetBytes("T")
            Dim stream As NetworkStream = clientSocket.GetStream()
            stream.Write(data, 0, data.Length)
            data = New [Byte](255) {}
            Dim responseData As String = String.Empty
            Dim bytes As Int32 = stream.Read(data, 0, data.Length)
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes)
            If responseData <> "" Then
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function Actividad() As Boolean
        Dim cont As Integer = 0, vConnected As Boolean = False
        Try
            AsynchronousClient.blnCerrar = True
            If (Not VerifyServer() And ScA.IsConnected) Then
                ScA.IsConnected = False
            End If

            vConnected = ScA.IsConnected
            If vConnected Then
                If ScA.Actividad() Then
                    Return True
                Else
                    ScA.IsConnected = False
                    MsgBox("Se perdio la conexion con el servidor de licencias mobiles. Intentelo nuevamente en unos segundos.", MsgBoxStyle.Information, "Informacion")
                    Return False
                End If
            Else
                Do Until (vConnected = True) Or (cont = 3)
                    cont = cont + 1
                    If (VerifyServer()) Then
                        ScA.Reconect()
                        vConnected = True
                        If ScA.Actividad Then
                            Return True
                        End If
                    Else
                        vConnected = False
                    End If
                    Thread.Sleep(1000)
                Loop
                If Not vConnected Then
                    MsgBox("Se perdio la conexion con el servidor de licencias mobiles. Intentelo nuevamente en unos segundos.", MsgBoxStyle.Information, "Informacion")
                    Return False
                Else
                    Return True
                End If
            End If
        Catch ex As Exception
            MsgBox("Se perdio la conexion con el servidor de licencias mobiles. Intentelo nuevamente en unos segundos.", MsgBoxStyle.Information, "Informacion")
            Return False
        End Try
    End Function

    Private Sub TomaStockInicial()
        Dim Sti As New clsStockInicial
        Dim St As New frmStockInicialConf
        Dim StT As New frmStockInicialToma
        Try
            St.oStockInicial = Sti
            St.ShowDialog()
            If St.ComenzarOperacion Then
                StT.oStockInicial = Sti
                StT.ShowDialog()
            End If
        Catch ex As Exception
            MsgBox("Toma Stock Inicial: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Sti = Nothing
            St = Nothing
            StT = Nothing
        End Try
    End Sub

    Private Sub LBMenu_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles LBMenu.KeyUp
        Try
            If e.KeyCode = 13 Then
                'If CerrarAplicacion Then
                '    Try
                '        TrdLic.Abort()
                '    Catch ex As Exception
                '    End Try
                '    Application.Exit()
                'End If
                VerifyConnection(SQLc)
                Select Case LBMenu.SelectedValue.ToString
                    Case "1" 'CONSULTA STOCK POR PALLETs
                        'If (Not Actividad()) Then Exit Try
                        Dim ofrmConsulta As New frmConsultaStock
                        ofrmConsulta.TipoConsulta = TipoForm.FormStock.Pallet
                        ofrmConsulta.ShowDialog()
                        ofrmConsulta = Nothing
                    Case "2" 'CONSULTA STOCK POR UBICACION
                        'If Not Actividad() Then Exit Try
                        Dim ofrmConsulta As New frmConsultaStock
                        ofrmConsulta.TipoConsulta = TipoForm.FormStock.Ubicacion
                        ofrmConsulta.ShowDialog()
                        ofrmConsulta = Nothing
                    Case "3" 'CONSULTA STOCK POR PRODUCTO
                        'If Not Actividad() Then Exit Try
                        Dim ofrmConsulta As New frmConsultaStock
                        ofrmConsulta.TipoConsulta = TipoForm.FormStock.Producto
                        ofrmConsulta.ShowDialog()
                        ofrmConsulta = Nothing
                    Case "4" 'UBICACION MERCADERIA
                        'If Not Actividad() Then Exit Try
                        Dim frmUbicacion As New frmUbicacionMercaderia
                        frmUbicacion.btnPublico = True
                        frmUbicacion.ShowDialog()
                        frmUbicacion = Nothing
                    Case "5" 'UBICACION MERCADERIA FORZADA
                        'If Not Actividad() Then Exit Try
                        Dim frmUbicacion As New frmUbicacionMercaderia
                        frmUbicacion.btnPublico = False
                        frmUbicacion.ShowDialog()
                        frmUbicacion = Nothing
                    Case "6" 'CONTROL EXPEDICION
                        'If Not Actividad() Then Exit Try
                        Dim frmIngreso As New frmIngresoViajes
                        frmIngreso.ShowDialog()
                        frmIngreso = Nothing
                    Case "7" 'CONTROL PALLET PICKING
                        'If Not Actividad() Then Exit Try
                        Dim CP As New frmControlPicking
                        CP.ShowDialog()
                        CP = Nothing
                    Case "8" 'TRANSFERENCIAS
                        'If Not Actividad() Then Exit Try
                        Dim frmTransferenciaManual As New frmTransferenciaManual
                        frmTransferenciaManual.ShowDialog()
                        frmTransferenciaManual = Nothing
                    Case "9" 'PICKING
                        'If Not Actividad() Then Exit Try
                        Call Picking()
                    Case "10" 'PROCESAR DEVOLUCION
                        'If Not Actividad() Then Exit Try
                        Dim frmTransferenciaAutomatica As New frmTransferenciaAutomatica
                        frmTransferenciaAutomatica.ShowDialog()
                        frmTransferenciaAutomatica = Nothing
                    Case "11" 'PICKING PALLET COMPLETO
                        'If Not Actividad() Then Exit Try
                        Call PickingPalletCompleto()
                    Case "12" 'RECEPCION DE ORDEN DE COMPRA
                        'If Not Actividad() Then Exit Try
                        Dim Odc As New frmRecepcionODC
                        Odc.ShowDialog()
                        Odc = Nothing
                    Case "13" 'TRANSFERENCIA GUIADA
                        'If Not Actividad() Then Exit Try
                        Dim Tr As New frmTransferenciaManual
                        Tr.TransferenciaGuiada = True
                        Tr.ShowDialog()
                        Tr = Nothing
                    Case "14" 'ARMADO DE PALLET FINAL
                        'If Not Actividad() Then Exit Try
                        Dim APF As New frmAPF
                        APF.ShowDialog()
                        APF = Nothing
                    Case "15" 'Inventario
                        'If Not Actividad() Then Exit Try
                        Dim FrmInventario As New frmInventario
                        FrmInventario.ShowDialog()
                        FrmInventario = Nothing
                    Case "16"
                        Dim Sel As Integer = 0
                        Dim fsel As New frmTransfPickingSelector
                        Dim FrmTransfPicking As New frmTransfPicking
                        fsel.ShowDialog()
                        If fsel.Cancelo = True Then
                            Exit Try
                        End If
                        Sel = fsel.TSeleccion
                        fsel.Dispose()
                        FrmTransfPicking.Seleccion = Sel
                        'If Not Actividad() Then Exit Try
                        FrmTransfPicking.ShowDialog()
                        FrmTransfPicking = Nothing
                    Case "17"
                        'If Not Actividad() Then Exit Try
                        Dim FrmCambCatLog As New frmCambCatLog
                        FrmCambCatLog.ShowDialog()
                        FrmCambCatLog = Nothing
                    Case "18"
                        'If Not Actividad() Then Exit Try
                        Dim FrmCambEstMerc As New frmCambEstMerc
                        FrmCambEstMerc.ShowDialog()
                        FrmCambEstMerc = Nothing
                    Case "19"
                        'If Not Actividad() Then Exit Try
                        Dim FrmRepaletizado As New frmRepaletizado
                        FrmRepaletizado.ShowDialog()
                        FrmRepaletizado = Nothing
                    Case "20"
                        'If Not Actividad() Then Exit Try
                        Dim Sel As Integer = 0
                        Dim fsel2 As New frmTransfPickingSelector
                        Dim frmTransfSobrantesPick As New frmTransfSobrantesPick
                        fsel2.ShowDialog()
                        If fsel2.Cancelo = True Then
                            Exit Try
                        End If
                        Sel = fsel2.TSeleccion
                        frmTransfSobrantesPick.Seleccion = Sel
                        frmTransfSobrantesPick.ShowDialog()
                        frmTransfSobrantesPick = Nothing
                        fsel2.Dispose()
                    Case "21"
                        'If Not Actividad() Then Exit Try
                        Dim FrmRecepcionOC As New FrmRecepcionOC
                        FrmRecepcionOC.ShowDialog()
                        FrmRecepcionOC = Nothing
                    Case "22"
                        'If Not Actividad() Then Exit Try
                        Dim frmTransferenciaBultos As New FrmTransferenciaBultos
                        frmTransferenciaBultos.ShowDialog()
                        frmTransferenciaBultos = Nothing
                    Case "23"
                        'If Not Actividad() Then Exit Try
                        Dim frmGManual As New frmGuardadoxDoc
                        frmGManual.ShowDialog()
                        frmGManual = Nothing
                    Case "24"
                        'If Not Actividad() Then Exit Try
                        Dim frmordencompra As New FrmOrdenCompra
                        frmordencompra.ShowDialog()
                        frmordencompra = Nothing
                        'Catalina Castillo.Tracker 4856.12/04/2012.INI
                    Case "25"
                        'If Not Actividad() Then Exit Try
                        Dim frmprocesoingreso As New FrmProcesoIngreso
                        frmprocesoingreso.ShowDialog()
                        frmprocesoingreso = Nothing
                    Case "26"
                        'If Not Actividad() Then Exit Try
                        Dim frmHubicacionBultos As New frmUbicacionBultos
                        frmHubicacionBultos.ShowDialog()
                        frmHubicacionBultos = Nothing
                    Case "27"
                        'If Not Actividad() Then Exit Try
                        Dim oSeries As New frmCargaSeries
                        oSeries.ShowDialog()
                        oSeries = Nothing
                    Case "28"
                        TomaStockInicial()
                    Case "29"
                        'ABASTECIMIENTO.
                        Dim oAb As New frmABAST
                        oAb.ShowDialog()
                        oAb = Nothing
                    Case "30"
                        Dim fEmp As New frmEmpaque
                        fEmp.ShowDialog()
                        fEmp.Dispose()
                    Case "31" 'TRANSFERENCIA CONTENEDORA GUIADA
                        Dim Trcg As New frmTransferenciaManual
                        Trcg.TransferenciaGuiada = True
                        Trcg.TrContenedora = True
                        Trcg.ShowDialog()
                        Trcg.Dispose()
                    Case "32" 'TRANSFERENCIA CONTENEDORA
                        Dim Trc As New frmTransferenciaManual
                        Trc.TransferenciaGuiada = False
                        Trc.TrContenedora = True
                        Trc.ShowDialog()
                        Trc.Dispose()
                    Case "33" 'DEVOLUCION 2D.
                        Dim DEV As New frmDevolucionesPedidos
                        DEV.ShowDialog()
                        DEV.Dispose()
                    Case "34"
                        Dim RG As New frmRecepcionGuardado
                        RG.ShowDialog()
                        RG.Dispose()
                    Case "999" 'SALIR
                        Dim Msg As Object
                        Msg = MsgBox("Desea Cerrar WarpMobile? ", MsgBoxStyle.YesNo)
                        If Msg = vbYes Then
                            'ScA.Cerrar()
                            'System.Threading.Thread.Sleep(1500)
                            'Try
                            'TrdLic.Abort()
                            'Catch ex As Exception
                            'End Try
                            Application.Exit()
                        End If
                    Case Else
                        MsgBox("Menu Inexistente", MsgBoxStyle.OkOnly, FrmName)
                End Select
                'Me.Button1.Focus()
            End If
        Catch sc As System.Net.Sockets.SocketException
            MsgBox("Socket: ")
        Catch io As IO.IOException
            MsgBox("-Ver Conexion: ")
        Catch ex As Exception
            MsgBox("-Menu Principal: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub Picking()
        Dim Fg As New FuncionesGenerales
        Dim Cmd As SqlCommand
        Dim QtyCliente As Integer
        Dim vCalleNave As String = "" 'lo uso para saber si solicito la calle.
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Fg.Cmd = Cmd
                If bPermiso.Picking = True Then
                    'Privitera Maximiliano
                    'Valido si el usuario tiene clientes asignados
                    vUsr.Vehiculo = ""
                    vUsr.NaveCalle = ""
                    vUsr.ClienteActivo = ""
                    If Fg.GetClientesAsignados(vUsr.CodUsuario, QtyCliente) Then
                        If QtyCliente > 1 Then
                            Dim fCliente As New FrmSelectorClientes
                            Try
                                With fCliente
                                    .ShowDialog()
                                    If .Cancel Then
                                        Exit Sub
                                    End If
                                End With
                            Catch ex As Exception
                                MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
                            Finally
                                fCliente = Nothing
                            End Try
                        Else
                            If QtyCliente = 1 Then
                                Fg.GetClienteDelUsuario(vUsr.CodUsuario, vUsr.ClienteActivo)
                            End If
                        End If
                        VerPickVehiculo(vCalleNave, ConfPos)
                        If PickVehiculo Then
                            Dim Vh As New frmVehiculo
                            Dim NC As New frmCalleNave
                            Vh.ShowDialog()
                            If Vh.Cancelo Then
                                Exit Try
                            End If
                            If vCalleNave = "1" Then
                                NC.ShowDialog()
                            End If
                        End If
                        Dim frmEgreso As New frmEgreso
                        frmEgreso.PickingVehiculos = PickVehiculo
                        frmEgreso.SolicitaConfirmacion = ConfPos
                        frmEgreso.SolCalleNave = IIf(vCalleNave = "1", True, False)
                        frmEgreso.ShowDialog()
                        frmEgreso = Nothing
                    Else
                        MsgBox("Su usuario no tiene clientes asignados ", MsgBoxStyle.OkOnly, "Advertencia")
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Picking: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub
    Private Function VerPickVehiculo(Optional ByRef S_CALLENAV As String = "", Optional ByRef ConfPos As Boolean = False) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Ret As Integer = 0
        Dim Procedure As String = "DBO.VERIFICA_PICK_VEHICULO"
        xCmd = SQLc.CreateCommand
        Try
            xCmd.CommandText = Procedure
            xCmd.CommandType = CommandType.StoredProcedure
            Pa = New SqlParameter("@Usuario_Id", SqlDbType.VarChar, 30)
            Pa.Value = UCase(vUsr.CodUsuario)
            xCmd.Parameters.Add(Pa)
            Pa = Nothing
            Pa = New SqlParameter("@CONTROL", SqlDbType.SmallInt)
            Pa.Direction = ParameterDirection.Output
            xCmd.Parameters.Add(Pa)
            Pa = Nothing
            Pa = New SqlParameter("@CLIENTE", SqlDbType.VarChar, 15)
            Pa.Value = IIf(vUsr.ClienteActivo = "", DBNull.Value, vUsr.ClienteActivo)
            xCmd.Parameters.Add(Pa)
            Pa = Nothing
            Pa = New SqlParameter("@CALLE_NAVE", SqlDbType.VarChar, 1)
            Pa.Direction = ParameterDirection.Output
            Pa.Value = DBNull.Value
            xCmd.Parameters.Add(Pa)
            Pa = New SqlParameter("@SOLIC_CONF", SqlDbType.VarChar, 1)
            Pa.Direction = ParameterDirection.Output
            Pa.Value = DBNull.Value
            xCmd.Parameters.Add(Pa)
            Pa = Nothing
            xCmd.ExecuteNonQuery()
            Ret = IIf(IsDBNull(xCmd.Parameters("@CONTROL").Value), 0, xCmd.Parameters("@CONTROL").Value)
            If Ret = 1 Then
                PickVehiculo = True
            Else
                PickVehiculo = False
            End If
            Ret = IIf(IsDBNull(xCmd.Parameters("@SOLIC_CONF").Value), 0, xCmd.Parameters("@SOLIC_CONF").Value)
            If Ret = 1 Then
                ConfPos = True
            Else
                ConfPos = False
            End If
            S_CALLENAV = IIf(IsDBNull(xCmd.Parameters("@CALLE_NAVE").Value), "HOLA", xCmd.Parameters("@CALLE_NAVE").Value)
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Critical, "Menu Principal")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Menu Principal")
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function
    Private Sub PickingPalletCompleto()
        Dim Fg As New FuncionesGenerales
        Dim Cmd As SqlCommand
        Dim QtyCliente As Integer
        Dim vCalleNave As String = "" 'lo uso para saber si solicito la calle.
        Cmd = SQLc.CreateCommand
        Fg.Cmd = Cmd
        vUsr.Vehiculo = ""
        vUsr.NaveCalle = ""
        vUsr.ClienteActivo = ""
        Try
            If Fg.GetClientesAsignados(vUsr.CodUsuario, QtyCliente) Then
                If QtyCliente > 1 Then
                    Dim fCliente As New FrmSelectorClientes
                    Try
                        With fCliente
                            .ShowDialog()
                            If .Cancel Then
                                Exit Sub
                            End If
                        End With
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
                    Finally
                        fCliente = Nothing
                    End Try
                Else
                    If QtyCliente = 1 Then
                        Fg.GetClienteDelUsuario(vUsr.CodUsuario, vUsr.ClienteActivo)
                    End If
                End If
                VerPickVehiculo(vCalleNave, ConfPos)
                If PickVehiculo Then
                    Dim Vh As New frmVehiculo
                    Dim NC As New frmCalleNave
                    Vh.ShowDialog()
                    If Vh.Cancelo Then
                        Exit Try
                    End If
                    If vCalleNave = "1" Then
                        NC.ShowDialog()
                    End If
                End If
                Dim frmEgreso As New frmEgreso
                frmEgreso.PickingVehiculos = PickVehiculo
                frmEgreso.PickingPalletCompleto = True
                frmEgreso.SolicitaConfirmacion = ConfPos
                frmEgreso.SolCalleNave = IIf(vCalleNave = "1", True, False)
                frmEgreso.ShowDialog()
                frmEgreso = Nothing
            End If
        Catch ex As Exception
            MsgBox("Picking Pallet Completo: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub frmMenuPrincipal_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = 13 Then
            LBMenu.Focus()
        End If
    End Sub

    Private Sub LBMenu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LBMenu.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim frm As New frmRecepcionGuardado
        frm.ShowDialog()
        frm.Dispose()
    End Sub

End Class