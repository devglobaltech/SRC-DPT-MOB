<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmOrdenCompra
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
        Me.btnRemito = New System.Windows.Forms.Button
        Me.btnProveedor = New System.Windows.Forms.Button
        Me.txtproveedor = New System.Windows.Forms.TextBox
        Me.txtRemitoNro = New System.Windows.Forms.TextBox
        Me.lblremito = New System.Windows.Forms.Label
        Me.btnInicio = New System.Windows.Forms.Button
        Me.txtProducto = New System.Windows.Forms.TextBox
        Me.lblProducto = New System.Windows.Forms.Label
        Me.lblcodproducto = New System.Windows.Forms.Label
        Me.lblcodproveedor = New System.Windows.Forms.Label
        Me.btnSalir = New System.Windows.Forms.Button
        Me.lblproveedor = New System.Windows.Forms.Label
        Me.btnProducto = New System.Windows.Forms.Button
        Me.txtcantidad = New System.Windows.Forms.TextBox
        Me.lblcantidad = New System.Windows.Forms.Label
        Me.btnFin = New System.Windows.Forms.Button
        Me.txtRemitoPrefijo = New System.Windows.Forms.TextBox
        Me.btnPrecargar = New System.Windows.Forms.Button
        Me.lblsms = New System.Windows.Forms.Label
        Me.lbldoc = New System.Windows.Forms.Label
        Me.txtdocumento = New System.Windows.Forms.TextBox
        Me.lblClienteId = New System.Windows.Forms.Label
        Me.cmbClienteId = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'btnRemito
        '
        Me.btnRemito.Location = New System.Drawing.Point(206, 98)
        Me.btnRemito.Name = "btnRemito"
        Me.btnRemito.Size = New System.Drawing.Size(27, 20)
        Me.btnRemito.TabIndex = 11
        Me.btnRemito.Text = "F4"
        '
        'btnProveedor
        '
        Me.btnProveedor.Location = New System.Drawing.Point(206, 27)
        Me.btnProveedor.Name = "btnProveedor"
        Me.btnProveedor.Size = New System.Drawing.Size(27, 20)
        Me.btnProveedor.TabIndex = 6
        Me.btnProveedor.Text = "F2"
        '
        'txtproveedor
        '
        Me.txtproveedor.Location = New System.Drawing.Point(89, 27)
        Me.txtproveedor.Name = "txtproveedor"
        Me.txtproveedor.Size = New System.Drawing.Size(111, 21)
        Me.txtproveedor.TabIndex = 5
        '
        'txtRemitoNro
        '
        Me.txtRemitoNro.Location = New System.Drawing.Point(138, 98)
        Me.txtRemitoNro.MaxLength = 8
        Me.txtRemitoNro.Name = "txtRemitoNro"
        Me.txtRemitoNro.Size = New System.Drawing.Size(62, 21)
        Me.txtRemitoNro.TabIndex = 10
        '
        'lblremito
        '
        Me.lblremito.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblremito.Location = New System.Drawing.Point(9, 98)
        Me.lblremito.Name = "lblremito"
        Me.lblremito.Size = New System.Drawing.Size(63, 20)
        Me.lblremito.Text = "Remito"
        '
        'btnInicio
        '
        Me.btnInicio.Location = New System.Drawing.Point(11, 196)
        Me.btnInicio.Name = "btnInicio"
        Me.btnInicio.Size = New System.Drawing.Size(104, 20)
        Me.btnInicio.TabIndex = 1
        Me.btnInicio.Text = "F1)Inicio"
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(89, 121)
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(111, 21)
        Me.txtProducto.TabIndex = 12
        '
        'lblProducto
        '
        Me.lblProducto.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblProducto.Location = New System.Drawing.Point(6, 146)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(220, 22)
        '
        'lblcodproducto
        '
        Me.lblcodproducto.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblcodproducto.Location = New System.Drawing.Point(9, 121)
        Me.lblcodproducto.Name = "lblcodproducto"
        Me.lblcodproducto.Size = New System.Drawing.Size(63, 20)
        Me.lblcodproducto.Text = "Producto"
        '
        'lblcodproveedor
        '
        Me.lblcodproveedor.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblcodproveedor.Location = New System.Drawing.Point(9, 27)
        Me.lblcodproveedor.Name = "lblcodproveedor"
        Me.lblcodproveedor.Size = New System.Drawing.Size(72, 20)
        Me.lblcodproveedor.Text = "Proveedor"
        '
        'btnSalir
        '
        Me.btnSalir.Location = New System.Drawing.Point(121, 219)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(108, 20)
        Me.btnSalir.TabIndex = 4
        Me.btnSalir.Text = "Salir"
        '
        'lblproveedor
        '
        Me.lblproveedor.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblproveedor.Location = New System.Drawing.Point(6, 51)
        Me.lblproveedor.Name = "lblproveedor"
        Me.lblproveedor.Size = New System.Drawing.Size(228, 44)
        '
        'btnProducto
        '
        Me.btnProducto.Location = New System.Drawing.Point(206, 121)
        Me.btnProducto.Name = "btnProducto"
        Me.btnProducto.Size = New System.Drawing.Size(27, 20)
        Me.btnProducto.TabIndex = 13
        Me.btnProducto.Text = "F5"
        '
        'txtcantidad
        '
        Me.txtcantidad.Location = New System.Drawing.Point(90, 169)
        Me.txtcantidad.MaxLength = 12
        Me.txtcantidad.Name = "txtcantidad"
        Me.txtcantidad.Size = New System.Drawing.Size(110, 21)
        Me.txtcantidad.TabIndex = 14
        '
        'lblcantidad
        '
        Me.lblcantidad.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblcantidad.Location = New System.Drawing.Point(9, 172)
        Me.lblcantidad.Name = "lblcantidad"
        Me.lblcantidad.Size = New System.Drawing.Size(63, 20)
        Me.lblcantidad.Text = "Cantidad"
        '
        'btnFin
        '
        Me.btnFin.Location = New System.Drawing.Point(11, 219)
        Me.btnFin.Name = "btnFin"
        Me.btnFin.Size = New System.Drawing.Size(104, 20)
        Me.btnFin.TabIndex = 3
        Me.btnFin.Text = "Fin"
        '
        'txtRemitoPrefijo
        '
        Me.txtRemitoPrefijo.Location = New System.Drawing.Point(89, 98)
        Me.txtRemitoPrefijo.MaxLength = 4
        Me.txtRemitoPrefijo.Name = "txtRemitoPrefijo"
        Me.txtRemitoPrefijo.Size = New System.Drawing.Size(46, 21)
        Me.txtRemitoPrefijo.TabIndex = 9
        '
        'btnPrecargar
        '
        Me.btnPrecargar.Location = New System.Drawing.Point(121, 196)
        Me.btnPrecargar.Name = "btnPrecargar"
        Me.btnPrecargar.Size = New System.Drawing.Size(108, 20)
        Me.btnPrecargar.TabIndex = 2
        Me.btnPrecargar.Text = "F2) Precargar"
        '
        'lblsms
        '
        Me.lblsms.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblsms.Location = New System.Drawing.Point(5, 242)
        Me.lblsms.Name = "lblsms"
        Me.lblsms.Size = New System.Drawing.Size(235, 39)
        '
        'lbldoc
        '
        Me.lbldoc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lbldoc.Location = New System.Drawing.Point(9, 4)
        Me.lbldoc.Name = "lbldoc"
        Me.lbldoc.Size = New System.Drawing.Size(84, 20)
        Me.lbldoc.Text = "Documento"
        Me.lbldoc.Visible = False
        '
        'txtdocumento
        '
        Me.txtdocumento.Location = New System.Drawing.Point(99, 3)
        Me.txtdocumento.Name = "txtdocumento"
        Me.txtdocumento.Size = New System.Drawing.Size(102, 21)
        Me.txtdocumento.TabIndex = 90
        Me.txtdocumento.Visible = False
        '
        'lblClienteId
        '
        Me.lblClienteId.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblClienteId.Location = New System.Drawing.Point(9, 4)
        Me.lblClienteId.Name = "lblClienteId"
        Me.lblClienteId.Size = New System.Drawing.Size(74, 20)
        Me.lblClienteId.Text = "Cliente"
        '
        'cmbClienteId
        '
        Me.cmbClienteId.Location = New System.Drawing.Point(89, 2)
        Me.cmbClienteId.Name = "cmbClienteId"
        Me.cmbClienteId.Size = New System.Drawing.Size(112, 22)
        Me.cmbClienteId.TabIndex = 100
        '
        'FrmOrdenCompra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmbClienteId)
        Me.Controls.Add(Me.lblClienteId)
        Me.Controls.Add(Me.lblsms)
        Me.Controls.Add(Me.btnPrecargar)
        Me.Controls.Add(Me.lbldoc)
        Me.Controls.Add(Me.txtRemitoPrefijo)
        Me.Controls.Add(Me.btnFin)
        Me.Controls.Add(Me.txtcantidad)
        Me.Controls.Add(Me.lblcantidad)
        Me.Controls.Add(Me.btnProducto)
        Me.Controls.Add(Me.lblproveedor)
        Me.Controls.Add(Me.btnRemito)
        Me.Controls.Add(Me.btnProveedor)
        Me.Controls.Add(Me.txtproveedor)
        Me.Controls.Add(Me.txtRemitoNro)
        Me.Controls.Add(Me.lblremito)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.btnInicio)
        Me.Controls.Add(Me.txtProducto)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.lblcodproducto)
        Me.Controls.Add(Me.lblcodproveedor)
        Me.Controls.Add(Me.txtdocumento)
        Me.KeyPreview = True
        Me.Name = "FrmOrdenCompra"
        Me.Text = "Orden de Compra"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnRemito As System.Windows.Forms.Button
    Friend WithEvents btnProveedor As System.Windows.Forms.Button
    Friend WithEvents txtproveedor As System.Windows.Forms.TextBox
    Friend WithEvents txtRemitoNro As System.Windows.Forms.TextBox
    Friend WithEvents lblremito As System.Windows.Forms.Label
    Friend WithEvents btnInicio As System.Windows.Forms.Button
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents lblcodproducto As System.Windows.Forms.Label
    Friend WithEvents lblcodproveedor As System.Windows.Forms.Label
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents lblproveedor As System.Windows.Forms.Label
    Friend WithEvents btnProducto As System.Windows.Forms.Button
    Friend WithEvents txtcantidad As System.Windows.Forms.TextBox
    Friend WithEvents lblcantidad As System.Windows.Forms.Label
    Friend WithEvents btnFin As System.Windows.Forms.Button
    Friend WithEvents txtRemitoPrefijo As System.Windows.Forms.TextBox
    Friend WithEvents btnPrecargar As System.Windows.Forms.Button
    Friend WithEvents lblsms As System.Windows.Forms.Label
    Friend WithEvents lbldoc As System.Windows.Forms.Label
    Friend WithEvents txtdocumento As System.Windows.Forms.TextBox
    Friend WithEvents lblClienteId As System.Windows.Forms.Label
    Friend WithEvents cmbClienteId As System.Windows.Forms.ComboBox
End Class
