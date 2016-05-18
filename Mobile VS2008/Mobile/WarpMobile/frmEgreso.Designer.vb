<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEgreso
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
        Me.lblProducto_ID = New System.Windows.Forms.Label
        Me.txtProducto_ID = New System.Windows.Forms.TextBox
        Me.lblCantidad = New System.Windows.Forms.Label
        Me.txtCantidad = New System.Windows.Forms.TextBox
        Me.lblPosicion = New System.Windows.Forms.Label
        Me.txtPosicion = New System.Windows.Forms.TextBox
        Me.lblCCantidad = New System.Windows.Forms.Label
        Me.txtCCantidad = New System.Windows.Forms.TextBox
        Me.lblMsg = New System.Windows.Forms.Label
        Me.txtDescripcion = New System.Windows.Forms.TextBox
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.cmdCompletados = New System.Windows.Forms.Button
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.cmdApertura = New System.Windows.Forms.Button
        Me.lblPallet = New System.Windows.Forms.Label
        Me.lblPalletIng = New System.Windows.Forms.Label
        Me.cmdCerrarPallet = New System.Windows.Forms.Button
        Me.cmdSaltoPicking = New System.Windows.Forms.Button
        Me.lblUnidad = New System.Windows.Forms.Label
        Me.lblVehiculo = New System.Windows.Forms.Label
        Me.cmdCambioVH = New System.Windows.Forms.Button
        Me.cmdCambioCalle = New System.Windows.Forms.Button
        Me.lblCodigo = New System.Windows.Forms.Label
        Me.cmdUbicContenedora = New System.Windows.Forms.Button
        Me.lblContenedora = New System.Windows.Forms.Label
        Me.lblNroLote = New System.Windows.Forms.Label
        Me.lblDatoLote = New System.Windows.Forms.Label
        Me.lblPartida = New System.Windows.Forms.Label
        Me.lblDatoPartida = New System.Windows.Forms.Label
        Me.txtCodigo = New System.Windows.Forms.TextBox
        Me.btn_Series_Esp = New System.Windows.Forms.Button
        Me.btnConversiones = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblProducto_ID
        '
        Me.lblProducto_ID.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblProducto_ID.Location = New System.Drawing.Point(6, 21)
        Me.lblProducto_ID.Name = "lblProducto_ID"
        Me.lblProducto_ID.Size = New System.Drawing.Size(85, 14)
        Me.lblProducto_ID.Text = "Cod. Producto"
        '
        'txtProducto_ID
        '
        Me.txtProducto_ID.BackColor = System.Drawing.Color.White
        Me.txtProducto_ID.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.txtProducto_ID.Location = New System.Drawing.Point(96, 19)
        Me.txtProducto_ID.Name = "txtProducto_ID"
        Me.txtProducto_ID.ReadOnly = True
        Me.txtProducto_ID.Size = New System.Drawing.Size(134, 19)
        Me.txtProducto_ID.TabIndex = 1
        '
        'lblCantidad
        '
        Me.lblCantidad.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblCantidad.Location = New System.Drawing.Point(6, 98)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(60, 14)
        Me.lblCantidad.Text = "Cantidad"
        '
        'txtCantidad
        '
        Me.txtCantidad.BackColor = System.Drawing.Color.White
        Me.txtCantidad.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.txtCantidad.Location = New System.Drawing.Point(72, 95)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.ReadOnly = True
        Me.txtCantidad.Size = New System.Drawing.Size(64, 19)
        Me.txtCantidad.TabIndex = 3
        '
        'lblPosicion
        '
        Me.lblPosicion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblPosicion.Location = New System.Drawing.Point(6, 117)
        Me.lblPosicion.Name = "lblPosicion"
        Me.lblPosicion.Size = New System.Drawing.Size(224, 33)
        Me.lblPosicion.Text = "Posicion: "
        '
        'txtPosicion
        '
        Me.txtPosicion.BackColor = System.Drawing.Color.White
        Me.txtPosicion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.txtPosicion.Location = New System.Drawing.Point(6, 132)
        Me.txtPosicion.MaxLength = 55
        Me.txtPosicion.Name = "txtPosicion"
        Me.txtPosicion.Size = New System.Drawing.Size(224, 19)
        Me.txtPosicion.TabIndex = 5
        '
        'lblCCantidad
        '
        Me.lblCCantidad.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblCCantidad.Location = New System.Drawing.Point(7, 175)
        Me.lblCCantidad.Name = "lblCCantidad"
        Me.lblCCantidad.Size = New System.Drawing.Size(108, 14)
        Me.lblCCantidad.Text = "Confirme Cantidad"
        '
        'txtCCantidad
        '
        Me.txtCCantidad.BackColor = System.Drawing.Color.White
        Me.txtCCantidad.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.txtCCantidad.Location = New System.Drawing.Point(121, 174)
        Me.txtCCantidad.MaxLength = 20
        Me.txtCCantidad.Name = "txtCCantidad"
        Me.txtCCantidad.Size = New System.Drawing.Size(109, 19)
        Me.txtCCantidad.TabIndex = 7
        '
        'lblMsg
        '
        Me.lblMsg.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(6, 270)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(231, 23)
        Me.lblMsg.Text = "lblMsg"
        '
        'txtDescripcion
        '
        Me.txtDescripcion.BackColor = System.Drawing.Color.White
        Me.txtDescripcion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.txtDescripcion.Location = New System.Drawing.Point(72, 74)
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ReadOnly = True
        Me.txtDescripcion.Size = New System.Drawing.Size(158, 19)
        Me.txtDescripcion.TabIndex = 13
        '
        'lblDescripcion
        '
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescripcion.Location = New System.Drawing.Point(6, 76)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(62, 14)
        Me.lblDescripcion.Text = "Descripción"
        '
        'cmdCompletados
        '
        Me.cmdCompletados.BackColor = System.Drawing.Color.White
        Me.cmdCompletados.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCompletados.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdCompletados.Location = New System.Drawing.Point(6, 209)
        Me.cmdCompletados.Name = "cmdCompletados"
        Me.cmdCompletados.Size = New System.Drawing.Size(109, 14)
        Me.cmdCompletados.TabIndex = 3
        Me.cmdCompletados.Text = "F3) Completados"
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.White
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdSalir.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdSalir.Location = New System.Drawing.Point(6, 224)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(109, 14)
        Me.cmdSalir.TabIndex = 4
        Me.cmdSalir.Text = "F5) Salir"
        '
        'cmdApertura
        '
        Me.cmdApertura.BackColor = System.Drawing.Color.White
        Me.cmdApertura.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdApertura.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdApertura.Location = New System.Drawing.Point(6, 194)
        Me.cmdApertura.Name = "cmdApertura"
        Me.cmdApertura.Size = New System.Drawing.Size(109, 14)
        Me.cmdApertura.TabIndex = 1
        Me.cmdApertura.Text = "F1) Abrir Cont."
        '
        'lblPallet
        '
        Me.lblPallet.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblPallet.Location = New System.Drawing.Point(6, 3)
        Me.lblPallet.Name = "lblPallet"
        Me.lblPallet.Size = New System.Drawing.Size(224, 14)
        Me.lblPallet.Text = "Pallet Picking:"
        '
        'lblPalletIng
        '
        Me.lblPalletIng.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblPalletIng.Location = New System.Drawing.Point(6, 41)
        Me.lblPalletIng.Name = "lblPalletIng"
        Me.lblPalletIng.Size = New System.Drawing.Size(110, 14)
        Me.lblPalletIng.Text = "Pallet: "
        '
        'cmdCerrarPallet
        '
        Me.cmdCerrarPallet.BackColor = System.Drawing.Color.White
        Me.cmdCerrarPallet.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCerrarPallet.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdCerrarPallet.Location = New System.Drawing.Point(121, 194)
        Me.cmdCerrarPallet.Name = "cmdCerrarPallet"
        Me.cmdCerrarPallet.Size = New System.Drawing.Size(109, 14)
        Me.cmdCerrarPallet.TabIndex = 2
        Me.cmdCerrarPallet.Text = "F2) Cerrar Cont."
        '
        'cmdSaltoPicking
        '
        Me.cmdSaltoPicking.BackColor = System.Drawing.Color.White
        Me.cmdSaltoPicking.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdSaltoPicking.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdSaltoPicking.Location = New System.Drawing.Point(121, 209)
        Me.cmdSaltoPicking.Name = "cmdSaltoPicking"
        Me.cmdSaltoPicking.Size = New System.Drawing.Size(109, 14)
        Me.cmdSaltoPicking.TabIndex = 20
        Me.cmdSaltoPicking.Text = "F4) Saltar Pick."
        '
        'lblUnidad
        '
        Me.lblUnidad.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblUnidad.Location = New System.Drawing.Point(139, 97)
        Me.lblUnidad.Name = "lblUnidad"
        Me.lblUnidad.Size = New System.Drawing.Size(91, 14)
        '
        'lblVehiculo
        '
        Me.lblVehiculo.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblVehiculo.ForeColor = System.Drawing.Color.Navy
        Me.lblVehiculo.Location = New System.Drawing.Point(6, 255)
        Me.lblVehiculo.Name = "lblVehiculo"
        Me.lblVehiculo.Size = New System.Drawing.Size(110, 14)
        Me.lblVehiculo.Text = "Vehiculo"
        '
        'cmdCambioVH
        '
        Me.cmdCambioVH.BackColor = System.Drawing.Color.White
        Me.cmdCambioVH.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCambioVH.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdCambioVH.Location = New System.Drawing.Point(121, 224)
        Me.cmdCambioVH.Name = "cmdCambioVH"
        Me.cmdCambioVH.Size = New System.Drawing.Size(109, 14)
        Me.cmdCambioVH.TabIndex = 30
        Me.cmdCambioVH.Text = "F6) Cambio Veh."
        '
        'cmdCambioCalle
        '
        Me.cmdCambioCalle.BackColor = System.Drawing.Color.White
        Me.cmdCambioCalle.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCambioCalle.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdCambioCalle.Location = New System.Drawing.Point(6, 239)
        Me.cmdCambioCalle.Name = "cmdCambioCalle"
        Me.cmdCambioCalle.Size = New System.Drawing.Size(109, 14)
        Me.cmdCambioCalle.TabIndex = 41
        Me.cmdCambioCalle.Text = "F7) Cambio Calle"
        '
        'lblCodigo
        '
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblCodigo.Location = New System.Drawing.Point(7, 156)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(109, 14)
        Me.lblCodigo.Text = "Confirme Serie:"
        '
        'cmdUbicContenedora
        '
        Me.cmdUbicContenedora.BackColor = System.Drawing.Color.White
        Me.cmdUbicContenedora.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdUbicContenedora.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdUbicContenedora.Location = New System.Drawing.Point(121, 224)
        Me.cmdUbicContenedora.Name = "cmdUbicContenedora"
        Me.cmdUbicContenedora.Size = New System.Drawing.Size(109, 14)
        Me.cmdUbicContenedora.TabIndex = 64
        Me.cmdUbicContenedora.Text = "F8) Cambiar Cont."
        '
        'lblContenedora
        '
        Me.lblContenedora.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblContenedora.Location = New System.Drawing.Point(121, 41)
        Me.lblContenedora.Name = "lblContenedora"
        Me.lblContenedora.Size = New System.Drawing.Size(109, 14)
        '
        'lblNroLote
        '
        Me.lblNroLote.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblNroLote.Location = New System.Drawing.Point(7, 57)
        Me.lblNroLote.Name = "lblNroLote"
        Me.lblNroLote.Size = New System.Drawing.Size(30, 14)
        Me.lblNroLote.Text = "Lote:"
        '
        'lblDatoLote
        '
        Me.lblDatoLote.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblDatoLote.Location = New System.Drawing.Point(43, 57)
        Me.lblDatoLote.Name = "lblDatoLote"
        Me.lblDatoLote.Size = New System.Drawing.Size(70, 14)
        Me.lblDatoLote.Text = "Label1"
        '
        'lblPartida
        '
        Me.lblPartida.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblPartida.Location = New System.Drawing.Point(120, 57)
        Me.lblPartida.Name = "lblPartida"
        Me.lblPartida.Size = New System.Drawing.Size(40, 14)
        Me.lblPartida.Text = "Partida:"
        '
        'lblDatoPartida
        '
        Me.lblDatoPartida.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblDatoPartida.Location = New System.Drawing.Point(170, 57)
        Me.lblDatoPartida.Name = "lblDatoPartida"
        Me.lblDatoPartida.Size = New System.Drawing.Size(60, 14)
        Me.lblDatoPartida.Text = "Label1"
        '
        'txtCodigo
        '
        Me.txtCodigo.BackColor = System.Drawing.Color.White
        Me.txtCodigo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.txtCodigo.Location = New System.Drawing.Point(139, 153)
        Me.txtCodigo.MaxLength = 20
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(91, 19)
        Me.txtCodigo.TabIndex = 76
        '
        'btn_Series_Esp
        '
        Me.btn_Series_Esp.BackColor = System.Drawing.Color.White
        Me.btn_Series_Esp.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.btn_Series_Esp.ForeColor = System.Drawing.Color.DarkBlue
        Me.btn_Series_Esp.Location = New System.Drawing.Point(121, 239)
        Me.btn_Series_Esp.Name = "btn_Series_Esp"
        Me.btn_Series_Esp.Size = New System.Drawing.Size(109, 14)
        Me.btn_Series_Esp.TabIndex = 93
        Me.btn_Series_Esp.Text = "F9) Series Esp.: On"
        '
        'btnConversiones
        '
        Me.btnConversiones.BackColor = System.Drawing.Color.White
        Me.btnConversiones.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.btnConversiones.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnConversiones.Location = New System.Drawing.Point(121, 259)
        Me.btnConversiones.Name = "btnConversiones"
        Me.btnConversiones.Size = New System.Drawing.Size(109, 14)
        Me.btnConversiones.TabIndex = 110
        Me.btnConversiones.Text = "F10) Conversion"
        '
        'frmEgreso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnConversiones)
        Me.Controls.Add(Me.btn_Series_Esp)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.lblDatoPartida)
        Me.Controls.Add(Me.lblPartida)
        Me.Controls.Add(Me.lblDatoLote)
        Me.Controls.Add(Me.lblNroLote)
        Me.Controls.Add(Me.lblContenedora)
        Me.Controls.Add(Me.cmdUbicContenedora)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.lblCodigo)
        Me.Controls.Add(Me.cmdCambioCalle)
        Me.Controls.Add(Me.cmdCambioVH)
        Me.Controls.Add(Me.lblVehiculo)
        Me.Controls.Add(Me.lblUnidad)
        Me.Controls.Add(Me.cmdSaltoPicking)
        Me.Controls.Add(Me.cmdCerrarPallet)
        Me.Controls.Add(Me.lblPalletIng)
        Me.Controls.Add(Me.lblPallet)
        Me.Controls.Add(Me.cmdApertura)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdCompletados)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.txtCCantidad)
        Me.Controls.Add(Me.lblCCantidad)
        Me.Controls.Add(Me.txtPosicion)
        Me.Controls.Add(Me.lblPosicion)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.lblCantidad)
        Me.Controls.Add(Me.txtProducto_ID)
        Me.Controls.Add(Me.lblProducto_ID)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frmEgreso"
        Me.Text = "Picking."
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblProducto_ID As System.Windows.Forms.Label
    Friend WithEvents txtProducto_ID As System.Windows.Forms.TextBox
    Friend WithEvents lblCantidad As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents lblPosicion As System.Windows.Forms.Label
    Friend WithEvents txtPosicion As System.Windows.Forms.TextBox
    Friend WithEvents lblCCantidad As System.Windows.Forms.Label
    Friend WithEvents txtCCantidad As System.Windows.Forms.TextBox
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents cmdCompletados As System.Windows.Forms.Button
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdApertura As System.Windows.Forms.Button
    Friend WithEvents lblPallet As System.Windows.Forms.Label
    Friend WithEvents lblPalletIng As System.Windows.Forms.Label
    Friend WithEvents cmdCerrarPallet As System.Windows.Forms.Button
    Friend WithEvents cmdSaltoPicking As System.Windows.Forms.Button
    Friend WithEvents lblUnidad As System.Windows.Forms.Label
    Friend WithEvents lblVehiculo As System.Windows.Forms.Label
    Friend WithEvents cmdCambioVH As System.Windows.Forms.Button
    Friend WithEvents cmdCambioCalle As System.Windows.Forms.Button
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents cmdUbicContenedora As System.Windows.Forms.Button
    Friend WithEvents lblContenedora As System.Windows.Forms.Label
    Friend WithEvents lblNroLote As System.Windows.Forms.Label
    Friend WithEvents lblDatoLote As System.Windows.Forms.Label
    Friend WithEvents lblPartida As System.Windows.Forms.Label
    Friend WithEvents lblDatoPartida As System.Windows.Forms.Label
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents btn_Series_Esp As System.Windows.Forms.Button
    Friend WithEvents btnConversiones As System.Windows.Forms.Button
End Class
