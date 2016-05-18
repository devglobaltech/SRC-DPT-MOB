Imports System.Data.SqlClient
Imports System.Data
Public Class frmPrincipal

    Private PickVehiculo As Boolean = False
    Private BlnSolic_Conf As Boolean = True
    Private Const FrmName As String = "Menú"
    Public DsMenu As Data.DataSet

    Private Sub frmPrincipal_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

    End Sub
    Private Sub frmPrincipal_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Try
            Select Case e.KeyCode
                Case 112 'Keys.F1
                    If bPermiso.Stock_Pallet = True Then
                        Dim ofrmConsulta As New frmConsultaStock
                        ofrmConsulta.TipoConsulta = TipoForm.FormStock.Pallet
                        ofrmConsulta.ShowDialog()
                        ofrmConsulta = Nothing
                    End If
                Case 113 'Keys.F2
                    If bPermiso.Stock_Ubicacion = True Then
                        Dim ofrmConsulta As New frmConsultaStock
                        ofrmConsulta.TipoConsulta = TipoForm.FormStock.Ubicacion
                        ofrmConsulta.ShowDialog()
                        ofrmConsulta = Nothing
                    End If
                Case 114 'Keys.F3
                    If bPermiso.Stock_Producto = True Then
                        Dim ofrmConsulta As New frmConsultaStock
                        ofrmConsulta.TipoConsulta = TipoForm.FormStock.Producto
                        ofrmConsulta.ShowDialog()
                        ofrmConsulta = Nothing
                    End If

                Case 115 'Keys.F4
                    If bPermiso.Ubicacion_Mercaderia = True Then
                        Dim frmUbicacion As New frmUbicacionMercaderia
                        frmUbicacion.btnPublico = True
                        frmUbicacion.ShowDialog()
                        frmUbicacion = Nothing
                    End If

                Case 116 'Keys.F5
                    If bPermiso.Ubicacion_Supervisor = True Then
                        Dim frmUbicacion As New frmUbicacionMercaderia
                        frmUbicacion.btnPublico = False
                        frmUbicacion.ShowDialog()
                        frmUbicacion = Nothing
                    End If

                Case 117 'Keys.F6
                    Try
                        If bPermiso.Picking = True Then
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
                                            End If
                                            vUsr.Vehiculo = ""
                                            vUsr.NaveCalle = ""
                                            vUsr.ClienteActivo = ""
                                            VerPickVehiculo(vCalleNave)
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
                                            frmEgreso.SolCalleNave = IIf(vCalleNave = "1", True, False)
                                            frmEgreso.ShowDialog()
                                            frmEgreso = Nothing
                                        Else
                                            MsgBox("Su usuario no tiene clientes asignados ", MsgBoxStyle.OkOnly, "Advertencia")
                                        End If
                                    End If
                                End If
                            Catch ex As Exception
                                MsgBox("cmdPicking_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
                            End Try
                        End If
                    Catch ex As Exception
                        MsgBox("Picking Pallet Completo: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
                    End Try
                Case 118 'Keys.F7   
                    If bPermiso.Ingreso_Viajes = True Then
                        Dim frmIngreso As New frmIngresoViajes
                        frmIngreso.ShowDialog()
                        frmIngreso = Nothing
                    End If

                Case 119  'F8
                    If bPermiso.control_picking = True Then
                        'Dim frmTransferenciaAutomatica As New frmTransferenciaAutomatica
                        'frmTransferenciaAutomatica.ShowDialog()
                        'frmTransferenciaAutomatica = Nothing
                        Dim CP As New frmControlPicking
                        CP.ShowDialog()
                        CP = Nothing
                    End If

                Case 120 'F9
                    If bPermiso.Transferencia_Manual = True Then
                        Dim frmTransferenciaManual As New frmTransferenciaManual
                        frmTransferenciaManual.ShowDialog()
                        frmTransferenciaManual = Nothing
                    End If

                Case 121 'F10
                    If bPermiso.Trans_Desconsolidada Then
                        Dim frmTransferenciaAutomatica As New frmTransferenciaAutomatica
                        frmTransferenciaAutomatica.ShowDialog()
                        frmTransferenciaAutomatica = Nothing
                    End If
                Case Keys.F11
                    Try
                        If bPermiso.PickingPalletCompleto = True Then
                            'Valido si el usuario tiene clientes asignados
                            Dim Fg As New FuncionesGenerales
                            Dim Cmd As SqlCommand
                            Dim QtyCliente As Integer
                            Dim vCalleNave As String = "" 'lo uso para saber si solicito la calle.
                            Cmd = SQLc.CreateCommand
                            Fg.Cmd = Cmd
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
                                End If
                                VerPickVehiculo(vCalleNave)
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
                                frmEgreso.SolCalleNave = IIf(vCalleNave = "1", True, False)
                                frmEgreso.ShowDialog()
                                frmEgreso = Nothing
                            Else
                                MsgBox("Su usuario no tiene clientes asignados ", MsgBoxStyle.OkOnly, "Advertencia")
                            End If
                        End If
                    Catch ex As Exception
                        MsgBox("Picking Pallet Completo: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
                    End Try
                Case Keys.F12
                    RecepcionODC()
                Case Keys.F13
                    TransferenciaGuiada()
                Case Keys.F14
                    Try
                        Dim APF As New frmAPF
                        APF.ShowDialog()
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
                    End Try
                Case 48  'Salir
                    Dim Msg As Object
                    Msg = MsgBox("Desea Cerrar WarpMobile? ", MsgBoxStyle.YesNo)
                    If Msg = vbYes Then
                        Application.Exit()
                    End If
            End Select
        Catch ex As Exception
            MsgBox("frmPrincipal_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub
    Private Sub frmPrincipal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Menú v" & vVersionHH & ". Usuario: " & vUsr.CodUsuario
        lblMenu.Text = sMenu
        If bPermiso.Stock_Pallet = True Then
            Me.cmdConsultaStockPallet.Enabled = True
        Else : Me.cmdConsultaStockPallet.Enabled = False
        End If

        If bPermiso.Stock_Ubicacion = True Then
            Me.cmdConsultaStockUbicacion.Enabled = True
        Else : Me.cmdConsultaStockUbicacion.Enabled = False
        End If

        If bPermiso.Stock_Producto = True Then
            Me.cmdConsultaStockProducto.Enabled = True
        Else : Me.cmdConsultaStockProducto.Enabled = False
        End If

        If bPermiso.Ubicacion_Mercaderia = True Then
            Me.cmdUbicacionMercaderia.Enabled = True
        Else : Me.cmdUbicacionMercaderia.Enabled = False
        End If

        If bPermiso.Ubicacion_Supervisor = True Then
            Me.cmdUbicacionForzada.Enabled = True
        Else : Me.cmdUbicacionForzada.Enabled = False
        End If

        If bPermiso.Ingreso_Viajes = True Then
            Me.cmdIngresoViajes.Enabled = True
        Else : Me.cmdIngresoViajes.Enabled = False
        End If

        If bPermiso.control_picking = True Then
            Me.cmdTransferenciaAutomatica.Enabled = True
        Else : Me.cmdTransferenciaAutomatica.Enabled = False
        End If

        If bPermiso.Transferencia_Manual = True Then
            Me.cmdTransferenciaManual.Enabled = True
        Else : Me.cmdTransferenciaManual.Enabled = False
        End If

        If bPermiso.Picking = True Then
            Me.cmdPicking.Enabled = True
        Else : Me.cmdPicking.Enabled = False
        End If

        If bPermiso.Trans_Desconsolidada Then
            Me.cmdTransDesconsolidada.Enabled = True
        Else : Me.cmdTransDesconsolidada.Enabled = False
        End If

        If bPermiso.PickingPalletCompleto Then
            Me.cmdPickingPalletCompleto.Enabled = True
        Else : Me.cmdPickingPalletCompleto.Enabled = False
        End If

        If bPermiso.RecepcionODC Then
            cmdRecepcionODC.Enabled = True
        Else : Me.cmdRecepcionODC.Enabled = False
        End If

        If bPermiso.TransGuiada Then
            cmdTransferenciaGuiada.Enabled = True
        Else : Me.cmdTransferenciaGuiada.Enabled = False
        End If

        If bPermiso.APF Then
            cmdAPF.Enabled = True
        Else : Me.cmdAPF.Enabled = False
        End If

        cmdSalir.Enabled = True
        Me.Focus()
    End Sub


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdConsultaStockPallet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConsultaStockPallet.Click
        Try
            If bPermiso.Stock_Pallet = True Then
                Dim ofrmConsulta As New frmConsultaStock
                ofrmConsulta.TipoConsulta = TipoForm.FormStock.Pallet
                ofrmConsulta.ShowDialog()
                ofrmConsulta = Nothing
            End If
        Catch ex As Exception
            MsgBox("cmdConsultaStockPallet_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub cmdConsultaStockUbicacion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConsultaStockUbicacion.Click
        Try
            If bPermiso.Stock_Ubicacion = True Then
                Dim ofrmConsulta As New frmConsultaStock
                ofrmConsulta.TipoConsulta = TipoForm.FormStock.Ubicacion
                ofrmConsulta.ShowDialog()
                ofrmConsulta = Nothing
            End If
        Catch ex As Exception
            MsgBox("cmdConsultaStockUbicacion_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Sub

    Private Sub cmdConsultaStockProducto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConsultaStockProducto.Click
        Try
            If bPermiso.Stock_Producto = True Then
                Dim ofrmConsulta As New frmConsultaStock
                ofrmConsulta.TipoConsulta = TipoForm.FormStock.Producto
                ofrmConsulta.ShowDialog()
                ofrmConsulta = Nothing
            End If
        Catch ex As Exception
            MsgBox("cmdConsultaStockProducto_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Sub

    Private Sub cmdUbicacionMercaderia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUbicacionMercaderia.Click
        Try
            If bPermiso.Ubicacion_Mercaderia = True Then
                Dim frmUbicacion As New frmUbicacionMercaderia
                frmUbicacion.btnPublico = True
                frmUbicacion.ShowDialog()
                frmUbicacion = Nothing
            End If
        Catch ex As Exception
            MsgBox("cmdUbicacionMercaderia_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Sub

    Private Sub cmdIngresoViajes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIngresoViajes.Click
        Try
            If bPermiso.Ingreso_Viajes = True Then
                Dim frmIngreso As New frmIngresoViajes
                frmIngreso.ShowDialog()
                frmIngreso = Nothing
            End If
        Catch ex As Exception
            MsgBox("cmdIngresoViajes_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub cmdTransferenciaAutomatica_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTransferenciaAutomatica.Click
        Dim CP As New frmControlPicking
        Try
            If bPermiso.control_picking = True Then
                'Dim frmTransferenciaAutomatica As New frmTransferenciaAutomatica
                'frmTransferenciaAutomatica.ShowDialog()
                'frmTransferenciaAutomatica = Nothing
                CP.ShowDialog()
            End If
        Catch ex As Exception
            MsgBox("Control Picking: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            CP = Nothing
        End Try
    End Sub

    Private Sub cmdTransferenciaManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTransferenciaManual.Click
        Try
            If bPermiso.Transferencia_Manual = True Then
                Dim frmTransferenciaManual As New frmTransferenciaManual
                frmTransferenciaManual.ShowDialog()
                frmTransferenciaManual = Nothing
            End If
        Catch ex As Exception
            MsgBox("cmdTransferenciaManual_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Try
            Dim Msg As Object
            Msg = MsgBox("Desea Cerrar WarpMobile? ", MsgBoxStyle.YesNo)
            If Msg = vbYes Then
                Application.Exit()
            End If

        Catch ex As Exception
            MsgBox("cmdSalir_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Sub

    Private Sub cmdUbicacionForzada_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUbicacionForzada.Click
        Try
            If bPermiso.Ubicacion_Supervisor = True Then
                Dim frmUbicacion As New frmUbicacionMercaderia
                frmUbicacion.btnPublico = False
                frmUbicacion.ShowDialog()
                frmUbicacion = Nothing
            End If
        Catch ex As Exception
            MsgBox("cmdUbicacionForzada_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub cmdPicking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPicking.Click
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
                        End If
                        VerPickVehiculo(vCalleNave)
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
                        frmEgreso.SolicitaConfirmacion = BlnSolic_Conf
                        frmEgreso.SolCalleNave = IIf(vCalleNave = "1", True, False)
                        frmEgreso.ShowDialog()
                        frmEgreso = Nothing
                    Else
                        MsgBox("Su usuario no tiene clientes asignados ", MsgBoxStyle.OkOnly, "Advertencia")
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("cmdPicking_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Function VerPickVehiculo(Optional ByRef S_CALLENAV As String = "") As Boolean
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
            Pa = Nothing
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
            If Ret = 0 Then
                BlnSolic_Conf = True
            Else
                BlnSolic_Conf = False
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

    Private Sub cmdTransDesconsolidada_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTransDesconsolidada.Click
        Try
            If bPermiso.Trans_Desconsolidada Then
                Dim frmTransferenciaAutomatica As New frmTransferenciaAutomatica
                frmTransferenciaAutomatica.ShowDialog()
                frmTransferenciaAutomatica = Nothing
            End If
        Catch ex As Exception
            MsgBox("cmdTransDesconsolidada_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub cmdPickingPalletCompleto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPickingPalletCompleto.Click
        Try
            If bPermiso.PickingPalletCompleto = True Then
                'Valido si el usuario tiene clientes asignados
                Dim Fg As New FuncionesGenerales
                Dim Cmd As SqlCommand
                Dim QtyCliente As Integer
                Dim vCalleNave As String = "" 'lo uso para saber si solicito la calle.
                Cmd = SQLc.CreateCommand
                Fg.Cmd = Cmd
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
                    End If

                    VerPickVehiculo(vCalleNave)
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
                    frmEgreso.SolCalleNave = IIf(vCalleNave = "1", True, False)
                    frmEgreso.ShowDialog()
                    frmEgreso = Nothing
                Else
                    MsgBox("Su usuario no tiene clientes asignados ", MsgBoxStyle.OkOnly, "Advertencia")
                End If
                'vUsr.Vehiculo = ""
                'vUsr.NaveCalle = ""
                'VerPickVehiculo()
                'If PickVehiculo Then
                '    Dim Vh As New frmVehiculo
                '    Dim NC As New frmCalleNave
                '    Vh.ShowDialog()
                '    If Vh.Cancelo Then
                '        Exit Try
                '    End If
                '    NC.ShowDialog()
                'End If
                'Dim frmEgreso As New frmEgreso
                'frmEgreso.PickingPalletCompleto = True
                'frmEgreso.PickingVehiculos = PickVehiculo
                'frmEgreso.ShowDialog()
                'frmEgreso = Nothing
            End If
        Catch ex As Exception
            MsgBox("Picking Pallet Completo: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub RecepcionODC()
        Dim Odc As frmRecepcionODC
        Try
            If bPermiso.RecepcionODC Then
                Odc = New frmRecepcionODC
                Odc.ShowDialog()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Recepcion de Orden de Compra")
        Finally
            Odc = Nothing
        End Try
    End Sub

    Private Sub cmdRecepcionODC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRecepcionODC.Click
        RecepcionODC()
    End Sub

    Private Sub cmdTransferenciaGuiada_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTransferenciaGuiada.Click
        TransferenciaGuiada()
    End Sub

    Private Sub TransferenciaGuiada()
        Try
            If bPermiso.TransGuiada Then
                Dim Tr As New frmTransferenciaManual
                Tr.TransferenciaGuiada = True
                Tr.ShowDialog()
                Tr = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        End Try
    End Sub


    Private Sub APF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmdAPF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAPF.Click
        Dim APF As New frmAPF
        Try
            APF.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            APF = Nothing
        End Try
    End Sub

    Private Sub btnTransfereciasPicking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransfereciasPicking.Click
        Try
            'If bPermiso.TransfPicking = True Then
            Dim frmTransfPicking As New frmTransfPicking
            frmTransfPicking.ShowDialog()
            frmTransfPicking = Nothing
            'End If
        Catch ex As Exception
            MsgBox("cmdTransferenciaManual_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub CmdCambCatLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCambCatLog.Click
        Try
            Dim frmCambCatLog As New frmCambCatLog
            frmCambCatLog.ShowDialog()
            frmCambCatLog = Nothing
        Catch ex As Exception
            MsgBox("CmdCambCatLog_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub CmdCambEstMerc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCambEstMerc.Click
        Try
            Dim frmCambEstMerc As New frmCambEstMerc
            frmCambEstMerc.ShowDialog()
            frmCambEstMerc = Nothing
        Catch ex As Exception
            MsgBox("CmdCambEstMerc_Click: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

End Class