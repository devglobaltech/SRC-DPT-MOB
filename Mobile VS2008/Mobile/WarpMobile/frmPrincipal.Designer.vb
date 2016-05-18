<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmPrincipal
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdPicking = New System.Windows.Forms.Button
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.cmdUbicacionForzada = New System.Windows.Forms.Button
        Me.cmdTransferenciaManual = New System.Windows.Forms.Button
        Me.cmdTransferenciaAutomatica = New System.Windows.Forms.Button
        Me.cmdIngresoViajes = New System.Windows.Forms.Button
        Me.cmdUbicacionMercaderia = New System.Windows.Forms.Button
        Me.cmdConsultaStockUbicacion = New System.Windows.Forms.Button
        Me.cmdConsultaStockProducto = New System.Windows.Forms.Button
        Me.cmdConsultaStockPallet = New System.Windows.Forms.Button
        Me.lblMenu = New System.Windows.Forms.Label
        Me.cmdTransDesconsolidada = New System.Windows.Forms.Button
        Me.cmdPickingPalletCompleto = New System.Windows.Forms.Button
        Me.cmdRecepcionODC = New System.Windows.Forms.Button
        Me.cmdTransferenciaGuiada = New System.Windows.Forms.Button
        Me.cmdAPF = New System.Windows.Forms.Button
        Me.btnTransfereciasPicking = New System.Windows.Forms.Button
        Me.CmdCambCatLog = New System.Windows.Forms.Button
        Me.CmdCambEstMerc = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cmdPicking
        '
        Me.cmdPicking.BackColor = System.Drawing.Color.White
        Me.cmdPicking.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdPicking.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdPicking.Location = New System.Drawing.Point(14, 98)
        Me.cmdPicking.Name = "cmdPicking"
        Me.cmdPicking.Size = New System.Drawing.Size(213, 17)
        Me.cmdPicking.TabIndex = 17
        Me.cmdPicking.Text = "F6) Picking"
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.PeachPuff
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdSalir.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.cmdSalir.Location = New System.Drawing.Point(14, 269)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(213, 17)
        Me.cmdSalir.TabIndex = 21
        Me.cmdSalir.Text = "0) Salir"
        '
        'cmdUbicacionForzada
        '
        Me.cmdUbicacionForzada.BackColor = System.Drawing.Color.White
        Me.cmdUbicacionForzada.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdUbicacionForzada.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdUbicacionForzada.Location = New System.Drawing.Point(14, 79)
        Me.cmdUbicacionForzada.Name = "cmdUbicacionForzada"
        Me.cmdUbicacionForzada.Size = New System.Drawing.Size(213, 17)
        Me.cmdUbicacionForzada.TabIndex = 16
        Me.cmdUbicacionForzada.Text = "F5) Ubicación mercadería forzada"
        '
        'cmdTransferenciaManual
        '
        Me.cmdTransferenciaManual.BackColor = System.Drawing.Color.White
        Me.cmdTransferenciaManual.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdTransferenciaManual.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdTransferenciaManual.Location = New System.Drawing.Point(14, 155)
        Me.cmdTransferenciaManual.Name = "cmdTransferenciaManual"
        Me.cmdTransferenciaManual.Size = New System.Drawing.Size(213, 17)
        Me.cmdTransferenciaManual.TabIndex = 20
        Me.cmdTransferenciaManual.Text = "F9) Transferencias"
        '
        'cmdTransferenciaAutomatica
        '
        Me.cmdTransferenciaAutomatica.BackColor = System.Drawing.Color.White
        Me.cmdTransferenciaAutomatica.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdTransferenciaAutomatica.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdTransferenciaAutomatica.Location = New System.Drawing.Point(14, 136)
        Me.cmdTransferenciaAutomatica.Name = "cmdTransferenciaAutomatica"
        Me.cmdTransferenciaAutomatica.Size = New System.Drawing.Size(213, 17)
        Me.cmdTransferenciaAutomatica.TabIndex = 19
        Me.cmdTransferenciaAutomatica.Text = "F8) Control Picking"
        '
        'cmdIngresoViajes
        '
        Me.cmdIngresoViajes.BackColor = System.Drawing.Color.White
        Me.cmdIngresoViajes.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdIngresoViajes.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdIngresoViajes.Location = New System.Drawing.Point(14, 117)
        Me.cmdIngresoViajes.Name = "cmdIngresoViajes"
        Me.cmdIngresoViajes.Size = New System.Drawing.Size(213, 17)
        Me.cmdIngresoViajes.TabIndex = 18
        Me.cmdIngresoViajes.Text = "F7) Control Expedicion"
        '
        'cmdUbicacionMercaderia
        '
        Me.cmdUbicacionMercaderia.BackColor = System.Drawing.Color.White
        Me.cmdUbicacionMercaderia.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdUbicacionMercaderia.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdUbicacionMercaderia.Location = New System.Drawing.Point(14, 60)
        Me.cmdUbicacionMercaderia.Name = "cmdUbicacionMercaderia"
        Me.cmdUbicacionMercaderia.Size = New System.Drawing.Size(213, 17)
        Me.cmdUbicacionMercaderia.TabIndex = 15
        Me.cmdUbicacionMercaderia.Text = "F4) Ubicación  mercadería"
        '
        'cmdConsultaStockUbicacion
        '
        Me.cmdConsultaStockUbicacion.BackColor = System.Drawing.Color.White
        Me.cmdConsultaStockUbicacion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdConsultaStockUbicacion.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdConsultaStockUbicacion.Location = New System.Drawing.Point(14, 22)
        Me.cmdConsultaStockUbicacion.Name = "cmdConsultaStockUbicacion"
        Me.cmdConsultaStockUbicacion.Size = New System.Drawing.Size(213, 17)
        Me.cmdConsultaStockUbicacion.TabIndex = 13
        Me.cmdConsultaStockUbicacion.Text = "F2) Consulta stock por ubicación"
        '
        'cmdConsultaStockProducto
        '
        Me.cmdConsultaStockProducto.BackColor = System.Drawing.Color.White
        Me.cmdConsultaStockProducto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdConsultaStockProducto.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdConsultaStockProducto.Location = New System.Drawing.Point(14, 41)
        Me.cmdConsultaStockProducto.Name = "cmdConsultaStockProducto"
        Me.cmdConsultaStockProducto.Size = New System.Drawing.Size(213, 17)
        Me.cmdConsultaStockProducto.TabIndex = 14
        Me.cmdConsultaStockProducto.Text = "F3) Consulta stock por producto"
        '
        'cmdConsultaStockPallet
        '
        Me.cmdConsultaStockPallet.BackColor = System.Drawing.Color.White
        Me.cmdConsultaStockPallet.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdConsultaStockPallet.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdConsultaStockPallet.Location = New System.Drawing.Point(14, 3)
        Me.cmdConsultaStockPallet.Name = "cmdConsultaStockPallet"
        Me.cmdConsultaStockPallet.Size = New System.Drawing.Size(213, 17)
        Me.cmdConsultaStockPallet.TabIndex = 12
        Me.cmdConsultaStockPallet.Text = "F1) Consulta stock por pallet"
        '
        'lblMenu
        '
        Me.lblMenu.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.lblMenu.ForeColor = System.Drawing.Color.Black
        Me.lblMenu.Location = New System.Drawing.Point(14, 269)
        Me.lblMenu.Name = "lblMenu"
        Me.lblMenu.Size = New System.Drawing.Size(213, 15)
        Me.lblMenu.Text = "vMenu"
        '
        'cmdTransDesconsolidada
        '
        Me.cmdTransDesconsolidada.BackColor = System.Drawing.Color.White
        Me.cmdTransDesconsolidada.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdTransDesconsolidada.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdTransDesconsolidada.Location = New System.Drawing.Point(14, 174)
        Me.cmdTransDesconsolidada.Name = "cmdTransDesconsolidada"
        Me.cmdTransDesconsolidada.Size = New System.Drawing.Size(213, 17)
        Me.cmdTransDesconsolidada.TabIndex = 23
        Me.cmdTransDesconsolidada.Text = "F10) Procesar Devolucion"
        '
        'cmdPickingPalletCompleto
        '
        Me.cmdPickingPalletCompleto.BackColor = System.Drawing.Color.White
        Me.cmdPickingPalletCompleto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdPickingPalletCompleto.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdPickingPalletCompleto.Location = New System.Drawing.Point(14, 193)
        Me.cmdPickingPalletCompleto.Name = "cmdPickingPalletCompleto"
        Me.cmdPickingPalletCompleto.Size = New System.Drawing.Size(213, 17)
        Me.cmdPickingPalletCompleto.TabIndex = 25
        Me.cmdPickingPalletCompleto.Text = "F11) Picking Pallet Completo"
        '
        'cmdRecepcionODC
        '
        Me.cmdRecepcionODC.BackColor = System.Drawing.Color.White
        Me.cmdRecepcionODC.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdRecepcionODC.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdRecepcionODC.Location = New System.Drawing.Point(14, 212)
        Me.cmdRecepcionODC.Name = "cmdRecepcionODC"
        Me.cmdRecepcionODC.Size = New System.Drawing.Size(213, 17)
        Me.cmdRecepcionODC.TabIndex = 29
        Me.cmdRecepcionODC.Text = "F12) Recepcion Orden de Compra"
        '
        'cmdTransferenciaGuiada
        '
        Me.cmdTransferenciaGuiada.BackColor = System.Drawing.Color.White
        Me.cmdTransferenciaGuiada.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdTransferenciaGuiada.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdTransferenciaGuiada.Location = New System.Drawing.Point(14, 231)
        Me.cmdTransferenciaGuiada.Name = "cmdTransferenciaGuiada"
        Me.cmdTransferenciaGuiada.Size = New System.Drawing.Size(213, 17)
        Me.cmdTransferenciaGuiada.TabIndex = 31
        Me.cmdTransferenciaGuiada.Text = "F13) Transferencia Guiada"
        '
        'cmdAPF
        '
        Me.cmdAPF.BackColor = System.Drawing.Color.White
        Me.cmdAPF.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdAPF.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdAPF.Location = New System.Drawing.Point(14, 250)
        Me.cmdAPF.Name = "cmdAPF"
        Me.cmdAPF.Size = New System.Drawing.Size(213, 17)
        Me.cmdAPF.TabIndex = 37
        Me.cmdAPF.Text = "F14) Armado Pallet Final"
        '
        'btnTransfereciasPicking
        '
        Me.btnTransfereciasPicking.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.btnTransfereciasPicking.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnTransfereciasPicking.Location = New System.Drawing.Point(123, 197)
        Me.btnTransfereciasPicking.Name = "btnTransfereciasPicking"
        Me.btnTransfereciasPicking.Size = New System.Drawing.Size(104, 24)
        Me.btnTransfereciasPicking.TabIndex = 39
        Me.btnTransfereciasPicking.Text = "Transf Picking"
        '
        'CmdCambCatLog
        '
        Me.CmdCambCatLog.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.CmdCambCatLog.ForeColor = System.Drawing.Color.DarkBlue
        Me.CmdCambCatLog.Location = New System.Drawing.Point(123, 216)
        Me.CmdCambCatLog.Name = "CmdCambCatLog"
        Me.CmdCambCatLog.Size = New System.Drawing.Size(104, 24)
        Me.CmdCambCatLog.TabIndex = 41
        Me.CmdCambCatLog.Text = "Cambio de Categoria Logica"
        '
        'CmdCambEstMerc
        '
        Me.CmdCambEstMerc.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.CmdCambEstMerc.ForeColor = System.Drawing.Color.DarkBlue
        Me.CmdCambEstMerc.Location = New System.Drawing.Point(123, 239)
        Me.CmdCambEstMerc.Name = "CmdCambEstMerc"
        Me.CmdCambEstMerc.Size = New System.Drawing.Size(104, 24)
        Me.CmdCambEstMerc.TabIndex = 43
        Me.CmdCambEstMerc.Text = "Cambio de Estado de Mercaderia"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(38, 198)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(79, 23)
        Me.Button1.TabIndex = 45
        Me.Button1.Text = "Prueba"
        Me.Button1.Visible = False
        '
        'frmPrincipal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CmdCambEstMerc)
        Me.Controls.Add(Me.CmdCambCatLog)
        Me.Controls.Add(Me.btnTransfereciasPicking)
        Me.Controls.Add(Me.cmdAPF)
        Me.Controls.Add(Me.cmdTransferenciaGuiada)
        Me.Controls.Add(Me.cmdRecepcionODC)
        Me.Controls.Add(Me.cmdPickingPalletCompleto)
        Me.Controls.Add(Me.cmdTransDesconsolidada)
        Me.Controls.Add(Me.cmdPicking)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdUbicacionForzada)
        Me.Controls.Add(Me.cmdTransferenciaManual)
        Me.Controls.Add(Me.cmdTransferenciaAutomatica)
        Me.Controls.Add(Me.cmdIngresoViajes)
        Me.Controls.Add(Me.cmdUbicacionMercaderia)
        Me.Controls.Add(Me.cmdConsultaStockUbicacion)
        Me.Controls.Add(Me.cmdConsultaStockProducto)
        Me.Controls.Add(Me.cmdConsultaStockPallet)
        Me.Controls.Add(Me.lblMenu)
        Me.KeyPreview = True
        Me.Name = "frmPrincipal"
        Me.Text = "Menú "
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdPicking As System.Windows.Forms.Button
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdUbicacionForzada As System.Windows.Forms.Button
    Friend WithEvents cmdTransferenciaManual As System.Windows.Forms.Button
    Friend WithEvents cmdTransferenciaAutomatica As System.Windows.Forms.Button
    Friend WithEvents cmdIngresoViajes As System.Windows.Forms.Button
    Friend WithEvents cmdUbicacionMercaderia As System.Windows.Forms.Button
    Friend WithEvents cmdConsultaStockUbicacion As System.Windows.Forms.Button
    Friend WithEvents cmdConsultaStockProducto As System.Windows.Forms.Button
    Friend WithEvents cmdConsultaStockPallet As System.Windows.Forms.Button
    Friend WithEvents lblMenu As System.Windows.Forms.Label
    Friend WithEvents cmdTransDesconsolidada As System.Windows.Forms.Button
    Friend WithEvents cmdPickingPalletCompleto As System.Windows.Forms.Button
    Friend WithEvents cmdRecepcionODC As System.Windows.Forms.Button
    Friend WithEvents cmdTransferenciaGuiada As System.Windows.Forms.Button
    Friend WithEvents cmdAPF As System.Windows.Forms.Button
    Friend WithEvents btnTransfereciasPicking As System.Windows.Forms.Button
    Friend WithEvents CmdCambCatLog As System.Windows.Forms.Button
    Friend WithEvents CmdCambEstMerc As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
