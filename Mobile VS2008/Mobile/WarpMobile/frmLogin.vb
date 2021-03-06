Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Net.Sockets
Imports System.Text

Public Class frmLoggin

    Private Const FrmName As String = "Login"
    Dim BlnValidado As Boolean = False
    'Dim sfile As String = "\Flash Disk\Warp\Config.dat" '"c:\temp\Config.dat"
    Dim Path As String = AppPath(True)
    'Dim Ruta As String = "\Archivos de Programa\warpmobile\Configt.dat"
    Dim sfile As String = Path & "Config.dat"
    Dim iSock As Boolean = False
    Dim sUserBBDD As String
    Dim sBBDD As String
    Dim sIp As String
    Dim Timeout As String
    Dim IpServer As String
    Dim PortServer As String

    'Variables de interaccion HK
    Public WithEvents WinSockCliente As New clsWinSockCliente
    Private dsConexion As Data.DataSet

    Private Const vMenu As String = "F1) Ingresar." & vbNewLine & "F2) Cancelar." & vbNewLine & _
                                    "F3) Cerrar Aplicación."

    Private DataReceived As Boolean = False

    Public Delegate Sub cambiandoLabeldelegate(ByVal value As Integer)
    Private cambio As New cambiandoLabeldelegate(AddressOf SetStatus)


    Private Sub frmLoggin_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Try

            Select Case e.KeyCode
                Case Keys.F1 '112
                    Try
                        Conectar()
                        PreValidate()
                    Catch ex As Exception
                    End Try
                Case Keys.F2 '113
                    Me.txtCodUsr.Text = ""
                    Me.txtPass.Text = ""
                    Me.txtCodUsr.Focus()
                Case Keys.F3 '114
                    CloseApp()
            End Select
        Catch ex As Exception
            MsgBox("frmLogin_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Function PreValidate() As Boolean
        Try
            Dim output As String = ""
            If Me.txtCodUsr.Text <> "" And Me.txtPass.Text <> "" Then
                Me.PB.Visible = True
                Me.PB.Minimum = 0
                Me.PB.Maximum = 100
                Me.PB.Value = 15
                SetStatus(1)
                Me.PB.Value = 30
                If Me.ValidateUsr(Me.txtCodUsr.Text, Me.txtPass.Text) Then
                    'If VerificarServidor() Then
                    '    TrdLic = New System.Threading.Thread(AddressOf ScA.Main)
                    '    TrdLic.Start()
                    '    System.Threading.Thread.Sleep(2000)
                    Me.PB.Value = 50
                    Application.DoEvents()
                    Me.PB.Value = 75
                    Application.DoEvents()
                    Me.PB.Value = 100
                    Application.DoEvents()
                    ShowForm()
                    'Else
                    'Application.Exit()
                    'End If
                Else
                    'ScA = Nothing
                    Me.PB.Visible = False
                End If
            ElseIf Me.txtCodUsr.Text = "" And Me.txtPass.Text <> "" Then
                Me.txtCodUsr.Focus()
            ElseIf Me.txtCodUsr.Text <> "" And Me.txtPass.Text = "" Then
                Me.txtPass.Focus()
            Else
                lblStatus.Text = "Debe ingresar el usuario y la contraseña "
                'MsgBox("Debe ingresar el usuario y la contraseña ", MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            MsgBox("PreValidate: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Function

    Function VerificarServidor() As Boolean
        Dim clientSocket As New System.Net.Sockets.TcpClient(), Output As String = ""
        Try
            clientSocket.Connect(IpServer, PortServer)
            ' Translate the passed message into ASCII and store it as a byte array.
            Dim data(255) As [Byte]
            data = System.Text.Encoding.ASCII.GetBytes("T")
            ' Get a client stream for reading and writing. Stream stream = client.GetStream();
            Dim stream As NetworkStream = clientSocket.GetStream()
            ' Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length)
            ' Buffer to store the response bytes.
            data = New [Byte](255) {}
            ' String to store the response ASCII representation.
            Dim responseData As String = String.Empty
            ' Read the first batch of the TcpServer response bytes.
            Dim bytes As Int32 = stream.Read(data, 0, data.Length)
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes)

            If responseData <> "" Then
                Return True
            End If
        Catch ex As Exception
            Output = "No se encontro el servidor de Conexiones Mobiles en la direccion " & IpServer & ", puerto: " & PortServer & vbNewLine & "La aplicacion sera cerrada." '& vbNewLine & "SocketException: " + es.ToString()
            MsgBox(Output, MsgBoxStyle.Exclamation, "Servidor de Licencias Mobiles")
        Finally
            clientSocket.Close()
            clientSocket = Nothing
        End Try
    End Function

    Private Sub CloseApp()
        Try
            Dim Msg As Object
            Msg = MsgBox("¿Desea Cerrar la aplicación? ", MsgBoxStyle.YesNo)
            If Msg = vbYes Then
                Application.Exit()
            End If
        Catch ex As Exception
            MsgBox("Fallo al cerrar la aplicacion" & ex.Message.ToString)
        End Try
    End Sub

    Private Sub ShowForm()
        Try
            SetStatus(6)
            AsingarPermisos()
            'Dim Fp As New frmPrincipal
            Dim fp As New frmMenuPrincipal
            fp.Show()
            fp.Win_Sock_Cliente = WinSockCliente
            fp = Nothing
            Me.Hide()
        Catch ex As Exception
            MsgBox("ShowForm: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub Conectar()
        Try
            If IsNothing(ScA) Then
                ScA = New AsynchronousClient
            End If
            ScA.IPAdd = Me.IpServer
            ScA.nPort = Me.PortServer
            iSock = False
            SQLc.Close()
            SQLc.ConnectionString = "Password=DOBLEFALTA;Persist Security Info=True;User ID=" & sUserBBDD & ";Initial Catalog=" & sBBDD & ";Data Source=" & sIp & ";Connect Timeout=" & Timeout '192.168.1.54" '//DESARROLLO
            SetStatus(0)
            Dim ServerStr As String = ""
            Dim Err As String = ""
            '------------------------------------------
            SQLc.Open()
            Me.txtCodUsr.Focus()
            '---------------------- sebas --------------------
            Dim Cmd As SqlCommand
            Cmd = SQLc.CreateCommand
            SetConnection(Cmd)
            CreateTemporales(Cmd)
            If Not VerificaVersionHH(vVersionHH) Then
                Application.Exit()
            End If
        Catch SQLExc As SqlException
            MsgBox("Error al conectar con la base de datos. La aplicacion se cerrara. " & SQLExc.Message)
            Application.Exit()
        Catch ex As Exception
            MsgBox("frmLogin_Load: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub frmLoggin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Me.Text = FrmName
            Me.Text = "Loggin. v" & vVersionHH
            Me.lblMenu.Text = vMenu

            Dim dtConexion As DataTable = New DataTable("Conexion")
            Dim sr As New StreamReader(sfile)
            Dim valor(3) As String

            dtConexion.Columns.Add("Fantasia")
            dtConexion.Columns.Add("Esquema")
            dtConexion.Columns.Add("UserBBDD")
            dtConexion.Columns.Add("Ip")
            dtConexion.Columns.Add("Timeout")
            '======================================================================================================================
            dtConexion.Columns.Add("IpServer")
            dtConexion.Columns.Add("PortServer")
            '======================================================================================================================
            dsConexion = New DataSet
            Do Until sr.Peek = -1
                valor = sr.ReadLine().Split(",")
                dtConexion.Rows.Add(valor(0), valor(1), valor(2), valor(3), valor(4), valor(5), valor(6))
            Loop
            sr.Close()

            dsConexion.Tables.Add(dtConexion)
            cboServer.DataSource = dsConexion.Tables("conexion")
            cboServer.DisplayMember = "Fantasia"
            cboServer.ValueMember = "Esquema"
            cboServer.SelectedIndex = 0
            sBBDD = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(1) 'BBDD
            sUserBBDD = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(2) 'User BBDD
            sIp = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(3) 'IP
            Timeout = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(4) 'TimeOut
            '======================================================================================================================
            IpServer = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(5) 'Servidor de Validacion Llaves.
            PortServer = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(6) 'Puerto en que escucha el servidor.
            '======================================================================================================================
            Me.SetStatus(0)
            'Conectar()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al crear dataset")
        End Try

    End Sub

    Private Function SetConnection(ByVal Cmd As SqlCommand) As Boolean
        Try
            Cmd.Parameters.Clear()
            Dim xSQL As String = "set language Español"
            Cmd.CommandText = xSQL
            Cmd.CommandType = Data.CommandType.Text
            Cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("SetConnection: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Function

    Public Function CreateTemporales(ByVal Cmd As SqlCommand) As Boolean
        Dim xSQL As String = ""
        Dim strCollate As String = ""
        Cmd.CommandType = Data.CommandType.Text
        strCollate = "COLLATE SQL_Latin1_General_CP1_CI_AS"

        xSQL = "Begin "
        xSQL = xSQL & " CREATE TABLE #temp_saldos_catlog (" & vbNewLine
        xSQL = xSQL & " cliente_id     VARCHAR(15)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " producto_id    VARCHAR(30)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " cat_log_id     VARCHAR(50)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " cantidad       NUMERIC(20,5) NOT NULL," & vbNewLine
        xSQL = xSQL & " categ_stock_id VARCHAR(15)    " & strCollate & " NULL," & vbNewLine
        xSQL = xSQL & " est_merc_id    VARCHAR(15)    " & strCollate & " NULL" & vbNewLine
        xSQL = xSQL & " )"
        xSQL = xSQL & vbNewLine

        xSQL = xSQL & " CREATE TABLE #temp_saldos_stock (" & vbNewLine
        xSQL = xSQL & " cliente_id  VARCHAR(15)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " producto_id VARCHAR(30)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " cant_tr_ing NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " cant_stock  NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " cant_tr_egr NUMERIC(20,5) NULL" & vbNewLine
        xSQL = xSQL & " )" & vbNewLine


        xSQL = xSQL & "CREATE TABLE #temp_usuario_loggin (" & vbNewLine
        xSQL = xSQL & " usuario_id            VARCHAR(20)  " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " terminal              VARCHAR(100)  " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " fecha_loggin          DATETIME     ," & vbNewLine
        xSQL = xSQL & " session_id            VARCHAR(60)  " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " rol_id                VARCHAR(5)  " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " emplazamiento_default VARCHAR(15)  " & strCollate & " NULL," & vbNewLine
        xSQL = xSQL & " deposito_default      VARCHAR(15)  " & strCollate & " NULL " & vbNewLine
        xSQL = xSQL & " )" & vbNewLine

        ''desde aca
        xSQL = xSQL & " CREATE TABLE #temp_rl_existencia_doc ( " & vbNewLine
        xSQL = xSQL & " rl_id NUMERIC(20,5) NULL " & vbNewLine
        xSQL = xSQL & " ) " & vbNewLine

        xSQL = xSQL & " CREATE TABLE #temp_existencia_locator_rl (" & vbNewLine
        xSQL = xSQL & " rl_id             NUMERIC(20,0)  NULL," & vbNewLine
        xSQL = xSQL & " clienteid         VARCHAR(15)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " productoid        VARCHAR(30)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " cantidad          NUMERIC(20,5)  NULL," & vbNewLine
        xSQL = xSQL & " nro_serie         VARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_lote          VARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " fecha_vencimiento DATETIME       NULL," & vbNewLine
        xSQL = xSQL & " nro_despacho      VARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_bulto         VARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_partida       VARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " peso              NUMERIC(20,5)  NULL," & vbNewLine
        xSQL = xSQL & " volumen           NUMERIC(20,5)  NULL," & vbNewLine
        xSQL = xSQL & " cat_log_id        VARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop1             VARCHAR(100)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop2             VARCHAR(100)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop3             VARCHAR(100)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " fecha_cpte        DATETIME       NULL," & vbNewLine
        xSQL = xSQL & " fecha_alta_gtw    DATETIME       NULL," & vbNewLine
        xSQL = xSQL & " unidad_id         VARCHAR(5)      COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " unidad_peso       VARCHAR(5)      COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " unidad_volumen    VARCHAR(5)      COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " est_merc_id       VARCHAR(15)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " moneda_id         VARCHAR(20)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " costo             NUMERIC(10,3)  NULL" & vbNewLine
        xSQL = xSQL & " )" & vbNewLine

        xSQL = xSQL & " CREATE TABLE #temp_existencia_doc (" & vbNewLine
        xSQL = xSQL & " clienteid         VARCHAR(15)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " productoid        VARCHAR(30)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " cantidad          NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " nro_serie         VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_lote          VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " fecha_vencimiento DATETIME      NULL," & vbNewLine
        xSQL = xSQL & " nro_despacho      VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_bulto         VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_partida       VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " peso              NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " volumen           NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " tie_in            CHAR(1)        COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " cantidad_disp     NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " code              CHAR(1)        COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " description       VARCHAR(100)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " cat_log_id        VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop1             VARCHAR(100)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop2             VARCHAR(100)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop3             VARCHAR(100)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " unidad_id         VARCHAR(5)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " unidad_peso       VARCHAR(5)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " unidad_volumen    VARCHAR(5)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " est_merc_id       VARCHAR(15)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " moneda_id         VARCHAR(20)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " costo             NUMERIC(10,3) NULL," & vbNewLine
        xSQL = xSQL & " orden             NUMERIC(20,0) NULL" & vbNewLine
        xSQL = xSQL & " )" & vbNewLine

        xSQL = xSQL & " CREATE TABLE #temp_existencia_locator (" & vbNewLine
        xSQL = xSQL & " clienteid         VARCHAR(15)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " productoid        VARCHAR(30)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " cantidad          NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " nro_serie         VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_lote          VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " fecha_vencimiento DATETIME      NULL," & vbNewLine
        xSQL = xSQL & " nro_despacho      VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_bulto         VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_partida       VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " peso              NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " volumen           NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " cat_log_id        VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop1             VARCHAR(100)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop2             VARCHAR(100)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop3             VARCHAR(100)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " fecha_cpte        DATETIME      NULL," & vbNewLine
        xSQL = xSQL & " fecha_alta_gtw    DATETIME      NULL," & vbNewLine
        xSQL = xSQL & " unidad_id         VARCHAR(5)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " unidad_peso       VARCHAR(5)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " unidad_volumen    VARCHAR(5)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " est_merc_id       VARCHAR(15)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " moneda_id         VARCHAR(20)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " costo             NUMERIC(10,3) NULL" & vbNewLine
        xSQL = xSQL & " )" & vbNewLine


        'xSQL = xSQL & "Exec Funciones_loggin_Api#Registra_usuario_loggin '" & vUsr.CodUsuario & "'" & vbNewLine
        xSQL = xSQL & "End"
        Cmd.CommandText = xSQL
        Cmd.ExecuteNonQuery()


        Return True
    End Function

    Private Sub SetStatus(ByVal Value As Integer)
        Try
            Select Case Value
                Case 0

                    Me.lblStatus.Text = ""
                Case 1
                    Me.lblStatus.Text = "Validando..."
                Case 2
                    Me.lblStatus.Text = "Validado..."
                Case 3
                    Me.lblStatus.Text = "Usuario y contraseña inválida"
                Case 4
                    Me.lblStatus.Text = "Usuario inválido"
                Case 5
                    Me.lblStatus.Text = "Contraseña inválida"
                Case 6
                    Me.lblStatus.Text = "Asignando permisos..."
                Case 7
                    Me.lblStatus.Text = "No hay conexion con la HARDKey"
            End Select
            Application.DoEvents()
        Catch ex As Exception
            MsgBox("SetStatus: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub AsingarPermisos()
        Try
            If GetPermiso(MenuWarp.Codigo.Stock_Pallet) = True Then
                bPermiso.Stock_Pallet = True
                'sMenu = "F1) Consulta de stock por pallet." & vbNewLine & vbNewLine
            Else
                frmPrincipal.cmdConsultaStockPallet.Enabled = False
                bPermiso.Stock_Pallet = False
            End If

            If GetPermiso(MenuWarp.Codigo.Stock_Ubicacion) = True Then
                bPermiso.Stock_Ubicacion = True

                'sMenu = sMenu & "F2) Consulta de stock por ubicación." & vbNewLine & vbNewLine
            Else
                frmPrincipal.cmdConsultaStockUbicacion.Enabled = False
                bPermiso.Stock_Ubicacion = False
            End If

            If GetPermiso(MenuWarp.Codigo.Stock_Producto) = True Then
                bPermiso.Stock_Producto = True

                'sMenu = sMenu & "F3) Consulta de stock por producto." & vbNewLine & vbNewLine
            Else
                frmPrincipal.cmdConsultaStockProducto.Enabled = False
                bPermiso.Stock_Producto = False
            End If
            If GetPermiso(MenuWarp.Codigo.Ubicacion_Mercaderia) = True Then
                bPermiso.Ubicacion_Mercaderia = True

                'sMenu = sMenu & "F4) Ubicación de mercaderías." & vbNewLine & vbNewLine
            Else
                frmPrincipal.cmdUbicacionMercaderia.Enabled = False
                bPermiso.Ubicacion_Mercaderia = False
            End If

            If GetPermiso(MenuWarp.Codigo.Ubicacion_Supervisor) = True Then
                bPermiso.Ubicacion_Supervisor = True

            Else
                frmPrincipal.cmdUbicacionForzada.Enabled = False
                bPermiso.Ubicacion_Supervisor = False
            End If
            If GetPermiso(MenuWarp.Codigo.Ingreso_Viajes) = True Then
                bPermiso.Ingreso_Viajes = True
                'sMenu = sMenu & "F5) Ingreso de viajes." & vbNewLine & vbNewLine
            Else
                bPermiso.Ingreso_Viajes = False
                frmPrincipal.cmdIngresoViajes.Enabled = False
            End If
            If GetPermiso(MenuWarp.Codigo.Control_Picking) = True Then
                bPermiso.control_picking = True
                'sMenu = sMenu & "F6) Transferencia Automática." & vbNewLine & vbNewLine
            Else
                frmPrincipal.cmdTransferenciaAutomatica.Enabled = False
                bPermiso.control_picking = False
            End If

            If GetPermiso(MenuWarp.Codigo.Transferencia_Manual) = True Then
                bPermiso.Transferencia_Manual = True
                'sMenu = sMenu & "F7) Transferencia Manual." & vbNewLine & vbNewLine
            Else
                frmPrincipal.cmdTransferenciaManual.Enabled = False
                bPermiso.Transferencia_Manual = False
            End If

            If GetPermiso(MenuWarp.Codigo.Picking) = True Then
                bPermiso.Picking = True
            Else
                frmPrincipal.cmdPicking.Enabled = False
                bPermiso.Picking = False
            End If


            If GetPermiso(MenuWarp.Codigo.Trans_Desconsolidada) = True Then
                bPermiso.Trans_Desconsolidada = True
            Else
                frmPrincipal.cmdTransDesconsolidada.Enabled = False
                bPermiso.Trans_Desconsolidada = False
            End If

            If GetPermiso(MenuWarp.Codigo.PickingPalletCompleto) = True Then
                bPermiso.PickingPalletCompleto = True
            Else
                frmPrincipal.cmdPickingPalletCompleto.Enabled = False
                bPermiso.PickingPalletCompleto = False
            End If

            If GetPermiso(MenuWarp.Codigo.RecepcionODC) = True Then
                bPermiso.RecepcionODC = True
            Else
                frmPrincipal.cmdRecepcionODC.Enabled = False
                bPermiso.RecepcionODC = False
            End If

            If GetPermiso(MenuWarp.Codigo.TransGuiada) = True Then
                bPermiso.TransGuiada = True
            Else
                frmPrincipal.cmdTransferenciaGuiada.Enabled = False
                bPermiso.TransGuiada = False
            End If

            If GetPermiso(MenuWarp.Codigo.APF) = True Then
                bPermiso.APF = True
            Else
                frmPrincipal.cmdAPF.Enabled = False
                bPermiso.APF = False
            End If

            'sMenu = sMenu & "F8)  Salir."
        Catch ex As Exception
            MsgBox("AsingarPermisos: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Function ValidateUsr(ByVal Usr As String, ByVal Pass As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Ds As New Data.DataSet
        Try
            Me.Focus()
            If VerifyConnection(SQLc) Then
                'ingreso ok el usuario y la pwd
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.Parameters.Add("@usuario_id", Data.SqlDbType.NVarChar, 20).Value = Usr.Trim
                Cmd.Parameters.Add("@password_handheld", Data.SqlDbType.NVarChar, 50).Value = Pass.Trim
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.CommandText = "Mob_Busca_Usuario"
                Da.Fill(Ds, "Usr")
                If Ds.Tables("Usr").Rows.Count > 0 Then
                    Try
                        vUsr.CodUsuario = Trim(UCase(Usr))
                        vUsr.Nombre = Ds.Tables("Usr").Rows(0)(0).ToString
                        BlnValidado = True
                        Cmd.Parameters.Clear()
                        Cmd.CommandType = Data.CommandType.StoredProcedure
                        Cmd.CommandText = "Funciones_loggin_api#registra_usuario_loggin"
                        Cmd.Parameters.Add("@Usuario", Data.SqlDbType.VarChar, 30).Value = vUsr.CodUsuario
                        Cmd.ExecuteNonQuery()
                        Return True
                    Catch ex As Exception
                        Return False
                    End Try
                Else
                    'validar que ingreso mal si el usuario o la contraseña
                    Dim CmdUsuario As SqlCommand
                    Dim DaUsuario As SqlDataAdapter
                    Dim DsUsuario As New Data.DataSet
                    CmdUsuario = SQLc.CreateCommand
                    DaUsuario = New SqlDataAdapter(CmdUsuario)
                    CmdUsuario.Parameters.Add("@usuario_id", Data.SqlDbType.NVarChar, 20).Value = Usr.Trim
                    CmdUsuario.CommandType = Data.CommandType.StoredProcedure
                    CmdUsuario.CommandText = "Mob_Usuario_Correcto"
                    DaUsuario.Fill(DsUsuario, "Usr")
                    If DsUsuario.Tables("Usr").Rows.Count > 0 Then
                        'ingreso bien el usuario y mal la pwd
                        SetStatus(5)
                        Me.txtPass.SelectAll()
                        Me.txtPass.Focus()
                    Else
                        'ingreso bien el usuario y mal la pwd
                        Dim CmdPwd As SqlCommand
                        Dim DaPwd As SqlDataAdapter
                        Dim DsPwd As New Data.DataSet
                        CmdPwd = SQLc.CreateCommand
                        DaPwd = New SqlDataAdapter(CmdPwd)
                        CmdPwd.Parameters.Add("@password_handheld", Data.SqlDbType.NVarChar, 50).Value = Pass.Trim
                        CmdPwd.CommandType = Data.CommandType.StoredProcedure
                        CmdPwd.CommandText = "Mob_Pwd_Correcto"
                        DaPwd.Fill(DsPwd, "Usr")
                        If DsPwd.Tables("Usr").Rows.Count > 0 Then
                            SetStatus(4)
                            Me.txtCodUsr.SelectAll()
                            Me.txtCodUsr.Focus()
                        Else
                            SetStatus(3)
                            Me.txtCodUsr.Text = ""
                            Me.txtPass.Text = ""
                            Me.txtCodUsr.Focus()
                        End If
                        CmdPwd = Nothing
                        DaPwd = Nothing
                        DsPwd = Nothing
                    End If
                    CmdUsuario = Nothing
                    DaUsuario = Nothing
                    DsUsuario = Nothing
                    Return False
                End If

            Else
                Return False
            End If
        Catch SQLExc As SqlException
            Me.lblStatus.Text = (SQLExc.Message)
            Exit Function
        Catch ex As Exception
            Me.lblStatus.Text = (ex.Message.ToString)
        Finally
            Cmd = Nothing
            Da = Nothing
            Ds = Nothing
        End Try
    End Function

    Private Sub txtCodUsr_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCodUsr.KeyUp
        Try
            If e.KeyCode = 13 Then
                Conectar()
                '--------------------------------------------------------------------------------
                'LRojas 06/02/2012: Se comenta el If y se reemplaza por el método ConectarSocket, 
                '                   que se encarga de llamar a la función PreValidate
                PreValidate()
                'ConectarSocket()
                '-----------------------------------------------------------------------------

            End If
        Catch ex As Exception
            MsgBox("txtCodUsr_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub txtPass_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPass.KeyUp
        Try
            If e.KeyCode = 13 Then
                e.Handled = True
                Conectar()
                '--------------------------------------------------------------------------------
                'LRojas 06/02/2012: Se comenta el If y se reemplaza por el método ConectarSocket, 
                '                   que se encarga de llamar a la función PreValidate
                PreValidate()
                'ConectarSocket()
                '-----------------------------------------------------------------------------
            End If
        Catch ex As Exception
            MsgBox("txtPass_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub frmLoggin_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.KeyCode = System.Windows.Forms.Keys.Up) Then
                'Rocker Up
                'Up
            End If
            If (e.KeyCode = System.Windows.Forms.Keys.Down) Then
                'Rocker Down
                'Down
            End If
            If (e.KeyCode = System.Windows.Forms.Keys.Left) Then
                'Left
            End If
            If (e.KeyCode = System.Windows.Forms.Keys.Right) Then
                'Right
            End If
            If (e.KeyCode = System.Windows.Forms.Keys.Enter) Then
                'Enter
            End If
        Catch ex As Exception
            MsgBox("frmLogin_KeyDown: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Function GetPermiso(ByVal iPermiso As Integer) As Boolean

        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Ds As New Data.DataSet
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.Parameters.Add("@usuario_id", Data.SqlDbType.NVarChar, 20).Value = vUsr.CodUsuario.Trim & ""
                Cmd.Parameters.Add("@codigo_id", Data.SqlDbType.Int).Value = iPermiso
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.CommandText = "Mob_Permisos_Menu"
                Da.Fill(Ds, "Consulta")
                If Ds.Tables("Consulta").Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch SQLExc As SqlException
            MsgBox("GetPermiso_SQLExc: " & SQLExc.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
            Exit Function
        Catch ex As Exception
            MsgBox("GetPermiso: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        End Try
    End Function

    Private Sub cmdIngresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIngresar.Click
        Try
            Conectar()
            'Si se conecta correctamente al HARDKey, continúo el proceso normalmente
            PreValidate()
            'ConectarSocket()
        Catch ex As Exception
            MsgBox("cmdIngresar_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Me.txtCodUsr.Text = ""
        Me.txtPass.Text = ""
        Me.txtCodUsr.Focus()
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        CloseApp()
    End Sub

    Private Sub txtCodUsr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodUsr.TextChanged
        lblStatus.Text = ""
    End Sub

    Private Sub txtPass_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPass.TextChanged
        lblStatus.Text = ""
    End Sub

    Private Function VerificaVersionHH(ByVal Version As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim ValueSP As String
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.Connection = SQLc
                Cmd.CommandText = "VERIFICA_VERSION_HH"
                Cmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@VER_HH", Data.SqlDbType.VarChar, 10)
                Pa.Direction = Data.ParameterDirection.Input
                Pa.Value = Version
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VERIFICA", Data.SqlDbType.Char, 1)
                Pa.Direction = Data.ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

                ValueSP = IIf(IsDBNull(Cmd.Parameters("@VERIFICA").Value), 0, Cmd.Parameters("@VERIFICA").Value)
                If ValueSP = "1" Then
                    Return True
                End If
            Else
                MsgBox("Error al conectar con la base de datos. La aplicacion se cerrara. ", MsgBoxStyle.OkOnly, FrmName)
            End If
        Catch SQLEx As SqlException
            MsgBox("VerificaVersionHH SQL.: " & SQLEx.Message, MsgBoxStyle.OkOnly)
            Return False
        Catch ex As Exception
            MsgBox("VerificaVersionHH SQL.: " & ex.Message, MsgBoxStyle.OkOnly)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function RegistraSession(ByVal Cmd As SqlCommand, ByVal xUsuario As String) As Boolean
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd.Parameters.Clear()
                Cmd.CommandText = "Mob_RegistraSession"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@Usuario", Data.SqlDbType.VarChar, 50)
                Pa.Value = Trim(UCase(xUsuario))
                Cmd.Parameters.Add(Pa)
                Cmd.ExecuteNonQuery()
            Else
                MsgBox("Error al conectar con la base de datos. La aplicacion se cerrara. ", MsgBoxStyle.OkOnly, FrmName)
            End If
        Catch SQLEx As SqlException
            MsgBox("RegistraSession SQL.: " & SQLEx.Message, MsgBoxStyle.OkOnly)
            Return False
        Catch ex As Exception
            MsgBox("RegistraSession SQL.: " & ex.Message, MsgBoxStyle.OkOnly)
            Return False
        Finally
            Pa = Nothing
        End Try
    End Function

    Private Sub cboServer_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboServer.KeyUp
        Try
            If e.KeyValue = 13 Then
                sBBDD = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(1)
                sUserBBDD = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(2)
                sIp = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(3)
                Timeout = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(4)
                'Me.IpServer = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(5)
                'Me.PortServer = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(6)
                'SQLc.ConnectionString = "Password=DOBLEFALTA;Persist Security Info=True;User ID=" & sServer & ";Initial Catalog=MICROSULES;Data Source=" & sIp '192.168.1.54" '//DESARROLLO
                txtCodUsr.Focus()
                Conectar()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al conectar al Servidor")
        End Try

    End Sub

    Private Sub PB_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PB.ParentChanged

    End Sub

    Private Sub lblLink_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblLink.ParentChanged

    End Sub

    Private Sub lblGlobalTech_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblGlobalTech.ParentChanged

    End Sub

    Private Sub lblStatus_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblStatus.ParentChanged

    End Sub

    'Private Sub cboServer_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboServer.SelectedValueChanged
    '    Try
    '        'If e.KeyValue = 13 Then

    '        sServer = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(1)
    '        sIp = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(2)
    '        sFantasia = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(0)
    '        Conectar()
    '        'SQLc.ConnectionString = "Password=DOBLEFALTA;Persist Security Info=True;User ID=" & sServer & ";Initial Catalog=MICROSULES;Data Source=" & sIp '192.168.1.54" '//DESARROLLO
    '        txtCodUsr.Focus()
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al conectar al Servidor")
    '    End Try
    'End Sub

    Private Sub cboServer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboServer.SelectedIndexChanged
        sBBDD = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(1)
        sUserBBDD = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(2)
        sIp = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(3)
        Timeout = dsConexion.Tables("conexion").Rows(Me.cboServer.SelectedIndex)(4)

    End Sub

    Private Sub WinSockCliente_ConexionTerminada() Handles WinSockCliente.ConexionTerminada
        Try
            frmMenuPrincipal.Close()
            'MsgBox("No hay conexion con la HARDKey", MsgBoxStyle.Exclamation)
            Dim hilo1 As Threading.Thread = New Threading.Thread(AddressOf Me.MetodoHilo)
            hilo1.Start()

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub WinSockCliente_DatosRecibidos(ByVal datos As String) Handles WinSockCliente.DatosRecibidos
        'MsgBox(datos, MsgBoxStyle.Information)
    End Sub

    Private Sub ConectarSocket()
        Dim result As String
        Try
            With WinSockCliente
                If sIp.ToString.IndexOf("\") > 0 Then
                    .IPDelHost = sIp.Substring(0, sIp.ToString.IndexOf("\"))
                Else
                    .IPDelHost = sIp
                End If

                .PuertoDelHost = GetKey(3)
                result = .SocketSendReceive(.IPDelHost, CInt(.PuertoDelHost))
            End With

            If (result <> "No se pudo conectar") And (result <> "") Then
                PreValidate()
            Else
                MsgBox("No se pudo conectar con el servidor para validar la licencia", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub MetodoHilo()
        Me.Invoke(Me.cambio, New Object() {7})
    End Sub

    Private Function GetKey(ByVal xKey As Integer) As String
        Dim sSql As String
        Dim Da As SqlDataAdapter
        Dim Cmd As SqlCommand
        Dim Ds As New Data.DataSet

        sSql = "dbo.sp_valor_sys_parametro_proceso"

        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.Parameters.Add("@num_key", Data.SqlDbType.Int).Value = xKey
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = sSql
                Da.Fill(Ds, "valores")
                If Not (Ds Is Nothing) And Ds.Tables.Count > 0 And Ds.Tables("valores").Rows.Count > 0 Then
                    GetKey = Ds.Tables("valores").Rows(0)(3)
                Else
                    GetKey = ""
                End If
            Else
                GetKey = ""
            End If
        Catch ex As Exception
            GetKey = 0
        End Try
    End Function


End Class
