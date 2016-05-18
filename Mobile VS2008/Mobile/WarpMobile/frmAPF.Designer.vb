<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmAPF
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmbClientes = New System.Windows.Forms.ComboBox
        Me.lblCliente = New System.Windows.Forms.Label
        Me.lblCodigoViaje = New System.Windows.Forms.Label
        Me.cmbCodigoViaje = New System.Windows.Forms.ComboBox
        Me.cmdPalletxCama = New System.Windows.Forms.Button
        Me.txtProducto = New System.Windows.Forms.TextBox
        Me.lblQTYCama = New System.Windows.Forms.Label
        Me.txtQTYCama = New System.Windows.Forms.TextBox
        Me.txtQtyLineasCama = New System.Windows.Forms.TextBox
        Me.lblQtyLineasCama = New System.Windows.Forms.Label
        Me.txtQtyBultosSuelto = New System.Windows.Forms.TextBox
        Me.lblQtyBultosSuelto = New System.Windows.Forms.Label
        Me.txtCantidad = New System.Windows.Forms.TextBox
        Me.lblCantidad = New System.Windows.Forms.Label
        Me.TxtConfirmacion = New System.Windows.Forms.TextBox
        Me.lblConfirmacion = New System.Windows.Forms.Label
        Me.cmdNewPallet = New System.Windows.Forms.Button
        Me.cmdClosePallet = New System.Windows.Forms.Button
        Me.cmdPalletStBy = New System.Windows.Forms.Button
        Me.cmdAbrir = New System.Windows.Forms.Button
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.CmdUltCliente = New System.Windows.Forms.Button
        Me.lblPallet = New System.Windows.Forms.Label
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.cmdPendientes = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cmbClientes
        '
        Me.cmbClientes.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmbClientes.Location = New System.Drawing.Point(3, 19)
        Me.cmbClientes.Name = "cmbClientes"
        Me.cmbClientes.Size = New System.Drawing.Size(147, 20)
        Me.cmbClientes.TabIndex = 0
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCliente.Location = New System.Drawing.Point(3, 1)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(126, 16)
        Me.lblCliente.Text = "Cliente"
        '
        'lblCodigoViaje
        '
        Me.lblCodigoViaje.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblCodigoViaje.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCodigoViaje.Location = New System.Drawing.Point(4, 40)
        Me.lblCodigoViaje.Name = "lblCodigoViaje"
        Me.lblCodigoViaje.Size = New System.Drawing.Size(233, 15)
        Me.lblCodigoViaje.Text = "Codigo de Viaje:"
        '
        'cmbCodigoViaje
        '
        Me.cmbCodigoViaje.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmbCodigoViaje.Location = New System.Drawing.Point(3, 56)
        Me.cmbCodigoViaje.Name = "cmbCodigoViaje"
        Me.cmbCodigoViaje.Size = New System.Drawing.Size(234, 20)
        Me.cmbCodigoViaje.TabIndex = 1
        '
        'cmdPalletxCama
        '
        Me.cmdPalletxCama.BackColor = System.Drawing.Color.LightGray
        Me.cmdPalletxCama.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmdPalletxCama.Location = New System.Drawing.Point(3, 77)
        Me.cmdPalletxCama.Name = "cmdPalletxCama"
        Me.cmdPalletxCama.Size = New System.Drawing.Size(234, 20)
        Me.cmdPalletxCama.TabIndex = 2
        Me.cmdPalletxCama.Text = "F3) Pallet por Producto"
        '
        'txtProducto
        '
        Me.txtProducto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtProducto.Location = New System.Drawing.Point(3, 98)
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(234, 19)
        Me.txtProducto.TabIndex = 3
        '
        'lblQTYCama
        '
        Me.lblQTYCama.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblQTYCama.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblQTYCama.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblQTYCama.Location = New System.Drawing.Point(3, 119)
        Me.lblQTYCama.Name = "lblQTYCama"
        Me.lblQTYCama.Size = New System.Drawing.Size(147, 18)
        Me.lblQTYCama.Text = "Cant. por Cama"
        '
        'txtQTYCama
        '
        Me.txtQTYCama.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtQTYCama.Location = New System.Drawing.Point(151, 118)
        Me.txtQTYCama.MaxLength = 3
        Me.txtQTYCama.Name = "txtQTYCama"
        Me.txtQTYCama.Size = New System.Drawing.Size(86, 19)
        Me.txtQTYCama.TabIndex = 4
        '
        'txtQtyLineasCama
        '
        Me.txtQtyLineasCama.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtQtyLineasCama.Location = New System.Drawing.Point(151, 138)
        Me.txtQtyLineasCama.MaxLength = 3
        Me.txtQtyLineasCama.Name = "txtQtyLineasCama"
        Me.txtQtyLineasCama.Size = New System.Drawing.Size(86, 19)
        Me.txtQtyLineasCama.TabIndex = 5
        '
        'lblQtyLineasCama
        '
        Me.lblQtyLineasCama.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblQtyLineasCama.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblQtyLineasCama.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblQtyLineasCama.Location = New System.Drawing.Point(3, 139)
        Me.lblQtyLineasCama.Name = "lblQtyLineasCama"
        Me.lblQtyLineasCama.Size = New System.Drawing.Size(147, 18)
        Me.lblQtyLineasCama.Text = "Cant. de Camas"
        '
        'txtQtyBultosSuelto
        '
        Me.txtQtyBultosSuelto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtQtyBultosSuelto.Location = New System.Drawing.Point(151, 158)
        Me.txtQtyBultosSuelto.MaxLength = 2
        Me.txtQtyBultosSuelto.Name = "txtQtyBultosSuelto"
        Me.txtQtyBultosSuelto.Size = New System.Drawing.Size(86, 19)
        Me.txtQtyBultosSuelto.TabIndex = 6
        '
        'lblQtyBultosSuelto
        '
        Me.lblQtyBultosSuelto.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblQtyBultosSuelto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblQtyBultosSuelto.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblQtyBultosSuelto.Location = New System.Drawing.Point(3, 159)
        Me.lblQtyBultosSuelto.Name = "lblQtyBultosSuelto"
        Me.lblQtyBultosSuelto.Size = New System.Drawing.Size(147, 18)
        Me.lblQtyBultosSuelto.Text = "Cant. Bultos Sueltos"
        '
        'txtCantidad
        '
        Me.txtCantidad.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtCantidad.Location = New System.Drawing.Point(151, 178)
        Me.txtCantidad.MaxLength = 4
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(86, 19)
        Me.txtCantidad.TabIndex = 7
        '
        'lblCantidad
        '
        Me.lblCantidad.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblCantidad.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblCantidad.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblCantidad.Location = New System.Drawing.Point(3, 179)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(147, 18)
        Me.lblCantidad.Text = "Cantidad"
        '
        'TxtConfirmacion
        '
        Me.TxtConfirmacion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.TxtConfirmacion.Location = New System.Drawing.Point(208, 200)
        Me.TxtConfirmacion.MaxLength = 1
        Me.TxtConfirmacion.Name = "TxtConfirmacion"
        Me.TxtConfirmacion.Size = New System.Drawing.Size(29, 19)
        Me.TxtConfirmacion.TabIndex = 8
        '
        'lblConfirmacion
        '
        Me.lblConfirmacion.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblConfirmacion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblConfirmacion.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblConfirmacion.Location = New System.Drawing.Point(3, 199)
        Me.lblConfirmacion.Name = "lblConfirmacion"
        Me.lblConfirmacion.Size = New System.Drawing.Size(204, 26)
        Me.lblConfirmacion.Text = "¿Confirma Cantidad? 1=Si, 0=No"
        '
        'cmdNewPallet
        '
        Me.cmdNewPallet.BackColor = System.Drawing.Color.LightGray
        Me.cmdNewPallet.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmdNewPallet.Location = New System.Drawing.Point(3, 227)
        Me.cmdNewPallet.Name = "cmdNewPallet"
        Me.cmdNewPallet.Size = New System.Drawing.Size(115, 15)
        Me.cmdNewPallet.TabIndex = 9
        Me.cmdNewPallet.Text = "F4) Nuevo Pallet"
        '
        'cmdClosePallet
        '
        Me.cmdClosePallet.BackColor = System.Drawing.Color.LightGray
        Me.cmdClosePallet.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmdClosePallet.Location = New System.Drawing.Point(125, 227)
        Me.cmdClosePallet.Name = "cmdClosePallet"
        Me.cmdClosePallet.Size = New System.Drawing.Size(112, 15)
        Me.cmdClosePallet.TabIndex = 10
        Me.cmdClosePallet.Text = "F5) Cerrar Pallet"
        '
        'cmdPalletStBy
        '
        Me.cmdPalletStBy.BackColor = System.Drawing.Color.LightGray
        Me.cmdPalletStBy.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmdPalletStBy.Location = New System.Drawing.Point(125, 243)
        Me.cmdPalletStBy.Name = "cmdPalletStBy"
        Me.cmdPalletStBy.Size = New System.Drawing.Size(112, 15)
        Me.cmdPalletStBy.TabIndex = 12
        Me.cmdPalletStBy.Text = "F7) Stand By"
        '
        'cmdAbrir
        '
        Me.cmdAbrir.BackColor = System.Drawing.Color.LightGray
        Me.cmdAbrir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmdAbrir.Location = New System.Drawing.Point(3, 243)
        Me.cmdAbrir.Name = "cmdAbrir"
        Me.cmdAbrir.Size = New System.Drawing.Size(115, 15)
        Me.cmdAbrir.TabIndex = 11
        Me.cmdAbrir.Text = "F6) Abrir Pallet"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.BackColor = System.Drawing.Color.LightGray
        Me.cmdCancelar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmdCancelar.Location = New System.Drawing.Point(3, 259)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(115, 15)
        Me.cmdCancelar.TabIndex = 13
        Me.cmdCancelar.Text = "F8) Cancelar"
        '
        'CmdUltCliente
        '
        Me.CmdUltCliente.BackColor = System.Drawing.Color.LightGray
        Me.CmdUltCliente.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.CmdUltCliente.Location = New System.Drawing.Point(151, 19)
        Me.CmdUltCliente.Name = "CmdUltCliente"
        Me.CmdUltCliente.Size = New System.Drawing.Size(86, 20)
        Me.CmdUltCliente.TabIndex = 25
        Me.CmdUltCliente.Text = "F1) Ult. Cliente"
        '
        'lblPallet
        '
        Me.lblPallet.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblPallet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblPallet.Location = New System.Drawing.Point(122, 1)
        Me.lblPallet.Name = "lblPallet"
        Me.lblPallet.Size = New System.Drawing.Size(115, 16)
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.LightGray
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmdSalir.Location = New System.Drawing.Point(3, 275)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(115, 15)
        Me.cmdSalir.TabIndex = 34
        Me.cmdSalir.Text = "F10) Salir"
        '
        'cmdPendientes
        '
        Me.cmdPendientes.BackColor = System.Drawing.Color.LightGray
        Me.cmdPendientes.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmdPendientes.Location = New System.Drawing.Point(125, 259)
        Me.cmdPendientes.Name = "cmdPendientes"
        Me.cmdPendientes.Size = New System.Drawing.Size(112, 15)
        Me.cmdPendientes.TabIndex = 43
        Me.cmdPendientes.Text = "F9) Pendientes"
        '
        'frmAPF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdPendientes)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.lblPallet)
        Me.Controls.Add(Me.CmdUltCliente)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdPalletStBy)
        Me.Controls.Add(Me.cmdAbrir)
        Me.Controls.Add(Me.cmdClosePallet)
        Me.Controls.Add(Me.cmdNewPallet)
        Me.Controls.Add(Me.TxtConfirmacion)
        Me.Controls.Add(Me.lblConfirmacion)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.lblCantidad)
        Me.Controls.Add(Me.txtQtyBultosSuelto)
        Me.Controls.Add(Me.lblQtyBultosSuelto)
        Me.Controls.Add(Me.txtQtyLineasCama)
        Me.Controls.Add(Me.lblQtyLineasCama)
        Me.Controls.Add(Me.txtQTYCama)
        Me.Controls.Add(Me.lblQTYCama)
        Me.Controls.Add(Me.txtProducto)
        Me.Controls.Add(Me.cmdPalletxCama)
        Me.Controls.Add(Me.cmbCodigoViaje)
        Me.Controls.Add(Me.lblCodigoViaje)
        Me.Controls.Add(Me.lblCliente)
        Me.Controls.Add(Me.cmbClientes)
        Me.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.KeyPreview = True
        Me.Name = "frmAPF"
        Me.Text = "Armado Pallet Final."
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbClientes As System.Windows.Forms.ComboBox
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblCodigoViaje As System.Windows.Forms.Label
    Friend WithEvents cmbCodigoViaje As System.Windows.Forms.ComboBox
    Friend WithEvents cmdPalletxCama As System.Windows.Forms.Button
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents lblQTYCama As System.Windows.Forms.Label
    Friend WithEvents txtQTYCama As System.Windows.Forms.TextBox
    Friend WithEvents txtQtyLineasCama As System.Windows.Forms.TextBox
    Friend WithEvents lblQtyLineasCama As System.Windows.Forms.Label
    Friend WithEvents txtQtyBultosSuelto As System.Windows.Forms.TextBox
    Friend WithEvents lblQtyBultosSuelto As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents lblCantidad As System.Windows.Forms.Label
    Friend WithEvents TxtConfirmacion As System.Windows.Forms.TextBox
    Friend WithEvents lblConfirmacion As System.Windows.Forms.Label
    Friend WithEvents cmdNewPallet As System.Windows.Forms.Button
    Friend WithEvents cmdClosePallet As System.Windows.Forms.Button
    Friend WithEvents cmdPalletStBy As System.Windows.Forms.Button
    Friend WithEvents cmdAbrir As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents CmdUltCliente As System.Windows.Forms.Button
    Friend WithEvents lblPallet As System.Windows.Forms.Label
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdPendientes As System.Windows.Forms.Button
End Class
