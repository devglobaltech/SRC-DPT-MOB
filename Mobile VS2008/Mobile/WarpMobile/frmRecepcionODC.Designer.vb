<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmRecepcionODC
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
        Me.lblODC = New System.Windows.Forms.Label
        Me.lblCliente = New System.Windows.Forms.Label
        Me.txtODC = New System.Windows.Forms.TextBox
        Me.cmbClientes = New System.Windows.Forms.ComboBox
        Me.lblProducto = New System.Windows.Forms.Label
        Me.txtProducto = New System.Windows.Forms.TextBox
        Me.lblInformacion = New System.Windows.Forms.Label
        Me.lblCantidad = New System.Windows.Forms.Label
        Me.txtCantidad = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtLoteProveedor = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtFechaVto = New System.Windows.Forms.TextBox
        Me.lblPallet = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmdAceptar = New System.Windows.Forms.Button
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbTipoImpresion = New System.Windows.Forms.ComboBox
        Me.TxtCantContenedoras = New System.Windows.Forms.TextBox
        Me.LblCantContenedoras = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblODC
        '
        Me.lblODC.BackColor = System.Drawing.SystemColors.Control
        Me.lblODC.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblODC.Location = New System.Drawing.Point(3, 46)
        Me.lblODC.Name = "lblODC"
        Me.lblODC.Size = New System.Drawing.Size(101, 20)
        Me.lblODC.Text = "Orden de Compra"
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.Control
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblCliente.Location = New System.Drawing.Point(3, 3)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(93, 22)
        Me.lblCliente.Text = "Cliente"
        '
        'txtODC
        '
        Me.txtODC.Location = New System.Drawing.Point(110, 45)
        Me.txtODC.MaxLength = 50
        Me.txtODC.Name = "txtODC"
        Me.txtODC.Size = New System.Drawing.Size(126, 21)
        Me.txtODC.TabIndex = 2
        '
        'cmbClientes
        '
        Me.cmbClientes.Location = New System.Drawing.Point(91, 3)
        Me.cmbClientes.Name = "cmbClientes"
        Me.cmbClientes.Size = New System.Drawing.Size(145, 22)
        Me.cmbClientes.TabIndex = 3
        '
        'lblProducto
        '
        Me.lblProducto.BackColor = System.Drawing.SystemColors.Control
        Me.lblProducto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblProducto.Location = New System.Drawing.Point(3, 69)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(101, 21)
        Me.lblProducto.Text = "Producto"
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(110, 69)
        Me.txtProducto.MaxLength = 55
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(126, 21)
        Me.txtProducto.TabIndex = 5
        '
        'lblInformacion
        '
        Me.lblInformacion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblInformacion.ForeColor = System.Drawing.Color.Blue
        Me.lblInformacion.Location = New System.Drawing.Point(3, 226)
        Me.lblInformacion.Name = "lblInformacion"
        Me.lblInformacion.Size = New System.Drawing.Size(234, 23)
        '
        'lblCantidad
        '
        Me.lblCantidad.BackColor = System.Drawing.SystemColors.Control
        Me.lblCantidad.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblCantidad.Location = New System.Drawing.Point(3, 96)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(121, 18)
        Me.lblCantidad.Text = "Cantidad:"
        Me.lblCantidad.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCantidad
        '
        Me.txtCantidad.Location = New System.Drawing.Point(124, 96)
        Me.txtCantidad.MaxLength = 10
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(112, 21)
        Me.txtCantidad.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(3, 144)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 20)
        Me.Label1.Text = "Lote Proveedor:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtLoteProveedor
        '
        Me.txtLoteProveedor.Location = New System.Drawing.Point(124, 143)
        Me.txtLoteProveedor.MaxLength = 50
        Me.txtLoteProveedor.Name = "txtLoteProveedor"
        Me.txtLoteProveedor.Size = New System.Drawing.Size(112, 21)
        Me.txtLoteProveedor.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(3, 167)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(121, 20)
        Me.Label2.Text = "Fecha Vencimiento:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtFechaVto
        '
        Me.txtFechaVto.Location = New System.Drawing.Point(124, 166)
        Me.txtFechaVto.MaxLength = 10
        Me.txtFechaVto.Name = "txtFechaVto"
        Me.txtFechaVto.Size = New System.Drawing.Size(112, 21)
        Me.txtFechaVto.TabIndex = 12
        Me.txtFechaVto.TabStop = False
        Me.txtFechaVto.Text = "__/__/____"
        '
        'lblPallet
        '
        Me.lblPallet.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblPallet.ForeColor = System.Drawing.Color.Blue
        Me.lblPallet.Location = New System.Drawing.Point(3, 27)
        Me.lblPallet.Name = "lblPallet"
        Me.lblPallet.Size = New System.Drawing.Size(233, 15)
        Me.lblPallet.Text = "Pallet:"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(124, 187)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 10)
        Me.Label3.Text = "Ej.: 01/01/2020"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmdAceptar
        '
        Me.cmdAceptar.Location = New System.Drawing.Point(3, 257)
        Me.cmdAceptar.Name = "cmdAceptar"
        Me.cmdAceptar.Size = New System.Drawing.Size(113, 15)
        Me.cmdAceptar.TabIndex = 20
        Me.cmdAceptar.Text = "F1) Aceptar"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.Location = New System.Drawing.Point(124, 257)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(113, 15)
        Me.cmdCancelar.TabIndex = 21
        Me.cmdCancelar.Text = "F2) Cancelar"
        '
        'cmdSalir
        '
        Me.cmdSalir.Location = New System.Drawing.Point(3, 274)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(113, 15)
        Me.cmdSalir.TabIndex = 22
        Me.cmdSalir.Text = "F3) Salir"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(3, 199)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(121, 19)
        Me.Label4.Text = "Tipo Etiqueta:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbTipoImpresion
        '
        Me.cmbTipoImpresion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.cmbTipoImpresion.Location = New System.Drawing.Point(124, 199)
        Me.cmbTipoImpresion.Name = "cmbTipoImpresion"
        Me.cmbTipoImpresion.Size = New System.Drawing.Size(112, 19)
        Me.cmbTipoImpresion.TabIndex = 33
        '
        'TxtCantContenedoras
        '
        Me.TxtCantContenedoras.Location = New System.Drawing.Point(124, 119)
        Me.TxtCantContenedoras.Name = "TxtCantContenedoras"
        Me.TxtCantContenedoras.Size = New System.Drawing.Size(112, 21)
        Me.TxtCantContenedoras.TabIndex = 57
        Me.TxtCantContenedoras.Visible = False
        '
        'LblCantContenedoras
        '
        Me.LblCantContenedoras.BackColor = System.Drawing.SystemColors.Control
        Me.LblCantContenedoras.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.LblCantContenedoras.Location = New System.Drawing.Point(3, 119)
        Me.LblCantContenedoras.Name = "LblCantContenedoras"
        Me.LblCantContenedoras.Size = New System.Drawing.Size(121, 21)
        Me.LblCantContenedoras.Text = "Cant. Contenedoras:"
        Me.LblCantContenedoras.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.LblCantContenedoras.Visible = False
        '
        'frmRecepcionODC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.LblCantContenedoras)
        Me.Controls.Add(Me.TxtCantContenedoras)
        Me.Controls.Add(Me.cmbTipoImpresion)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdAceptar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblPallet)
        Me.Controls.Add(Me.txtFechaVto)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtLoteProveedor)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.lblCantidad)
        Me.Controls.Add(Me.lblInformacion)
        Me.Controls.Add(Me.txtProducto)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.cmbClientes)
        Me.Controls.Add(Me.txtODC)
        Me.Controls.Add(Me.lblCliente)
        Me.Controls.Add(Me.lblODC)
        Me.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.KeyPreview = True
        Me.Name = "frmRecepcionODC"
        Me.Text = "Recepcion Orden de Compra"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblODC As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents txtODC As System.Windows.Forms.TextBox
    Friend WithEvents cmbClientes As System.Windows.Forms.ComboBox
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents lblInformacion As System.Windows.Forms.Label
    Friend WithEvents lblCantidad As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtLoteProveedor As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFechaVto As System.Windows.Forms.TextBox
    Friend WithEvents lblPallet As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdAceptar As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbTipoImpresion As System.Windows.Forms.ComboBox
    Friend WithEvents TxtCantContenedoras As System.Windows.Forms.TextBox
    Friend WithEvents LblCantContenedoras As System.Windows.Forms.Label
End Class
