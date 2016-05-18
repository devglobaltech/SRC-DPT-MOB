<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmProcesoIngreso
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
        Me.lblDocumento = New System.Windows.Forms.Label
        Me.txtNroDocumento = New System.Windows.Forms.TextBox
        Me.lblNroRemito = New System.Windows.Forms.Label
        Me.lblRemito = New System.Windows.Forms.Label
        Me.lblProveedor = New System.Windows.Forms.Label
        Me.lblProveedorOrigen = New System.Windows.Forms.Label
        Me.lblProducto = New System.Windows.Forms.Label
        Me.txtCodProducto = New System.Windows.Forms.TextBox
        Me.lblDescripcionProd = New System.Windows.Forms.Label
        Me.lblCategoria = New System.Windows.Forms.Label
        Me.lblCatLogica = New System.Windows.Forms.Label
        Me.lblCantidad = New System.Windows.Forms.Label
        Me.lbltxtcantidad = New System.Windows.Forms.Label
        Me.txtCantidad = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblDescUnidad = New System.Windows.Forms.Label
        Me.lblUnidad = New System.Windows.Forms.Label
        Me.btnNuevaCarga = New System.Windows.Forms.Button
        Me.btnEspCantidad = New System.Windows.Forms.Button
        Me.btnVerCargados = New System.Windows.Forms.Button
        Me.btnFinalizar = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDocumento
        '
        Me.lblDocumento.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblDocumento.Location = New System.Drawing.Point(5, 4)
        Me.lblDocumento.Name = "lblDocumento"
        Me.lblDocumento.Size = New System.Drawing.Size(106, 20)
        Me.lblDocumento.Text = "Nro. de Documento:"
        '
        'txtNroDocumento
        '
        Me.txtNroDocumento.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtNroDocumento.Location = New System.Drawing.Point(117, 4)
        Me.txtNroDocumento.Name = "txtNroDocumento"
        Me.txtNroDocumento.Size = New System.Drawing.Size(114, 18)
        Me.txtNroDocumento.TabIndex = 1
        '
        'lblNroRemito
        '
        Me.lblNroRemito.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblNroRemito.Location = New System.Drawing.Point(5, 28)
        Me.lblNroRemito.Name = "lblNroRemito"
        Me.lblNroRemito.Size = New System.Drawing.Size(106, 20)
        Me.lblNroRemito.Text = "Nro. de Remito:"
        Me.lblNroRemito.Visible = False
        '
        'lblRemito
        '
        Me.lblRemito.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblRemito.Location = New System.Drawing.Point(117, 27)
        Me.lblRemito.Name = "lblRemito"
        Me.lblRemito.Size = New System.Drawing.Size(114, 21)
        Me.lblRemito.Visible = False
        '
        'lblProveedor
        '
        Me.lblProveedor.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblProveedor.Location = New System.Drawing.Point(5, 55)
        Me.lblProveedor.Name = "lblProveedor"
        Me.lblProveedor.Size = New System.Drawing.Size(106, 20)
        Me.lblProveedor.Text = "Proveedor/Origen:"
        Me.lblProveedor.Visible = False
        '
        'lblProveedorOrigen
        '
        Me.lblProveedorOrigen.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblProveedorOrigen.Location = New System.Drawing.Point(117, 54)
        Me.lblProveedorOrigen.Name = "lblProveedorOrigen"
        Me.lblProveedorOrigen.Size = New System.Drawing.Size(114, 21)
        Me.lblProveedorOrigen.Visible = False
        '
        'lblProducto
        '
        Me.lblProducto.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblProducto.Location = New System.Drawing.Point(5, 94)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(106, 20)
        Me.lblProducto.Text = "Cod. de Producto:"
        Me.lblProducto.Visible = False
        '
        'txtCodProducto
        '
        Me.txtCodProducto.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtCodProducto.Location = New System.Drawing.Point(116, 91)
        Me.txtCodProducto.Name = "txtCodProducto"
        Me.txtCodProducto.Size = New System.Drawing.Size(115, 18)
        Me.txtCodProducto.TabIndex = 10
        Me.txtCodProducto.Visible = False
        '
        'lblDescripcionProd
        '
        Me.lblDescripcionProd.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescripcionProd.Location = New System.Drawing.Point(5, 5)
        Me.lblDescripcionProd.Name = "lblDescripcionProd"
        Me.lblDescripcionProd.Size = New System.Drawing.Size(226, 36)
        '
        'lblCategoria
        '
        Me.lblCategoria.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblCategoria.Location = New System.Drawing.Point(5, 67)
        Me.lblCategoria.Name = "lblCategoria"
        Me.lblCategoria.Size = New System.Drawing.Size(106, 20)
        Me.lblCategoria.Text = "Cat. Logica:"
        '
        'lblCatLogica
        '
        Me.lblCatLogica.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblCatLogica.Location = New System.Drawing.Point(116, 69)
        Me.lblCatLogica.Name = "lblCatLogica"
        Me.lblCatLogica.Size = New System.Drawing.Size(114, 21)
        '
        'lblCantidad
        '
        Me.lblCantidad.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblCantidad.Location = New System.Drawing.Point(5, 91)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(106, 20)
        Me.lblCantidad.Text = "Cantidad:"
        '
        'lbltxtcantidad
        '
        Me.lbltxtcantidad.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lbltxtcantidad.Location = New System.Drawing.Point(117, 91)
        Me.lbltxtcantidad.Name = "lbltxtcantidad"
        Me.lbltxtcantidad.Size = New System.Drawing.Size(114, 21)
        Me.lbltxtcantidad.Text = "1"
        '
        'txtCantidad
        '
        Me.txtCantidad.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtCantidad.Location = New System.Drawing.Point(116, 93)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(114, 18)
        Me.txtCantidad.TabIndex = 20
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblDescUnidad)
        Me.Panel1.Controls.Add(Me.lblUnidad)
        Me.Panel1.Controls.Add(Me.lblCatLogica)
        Me.Panel1.Controls.Add(Me.txtCantidad)
        Me.Panel1.Controls.Add(Me.lbltxtcantidad)
        Me.Panel1.Controls.Add(Me.lblDescripcionProd)
        Me.Panel1.Controls.Add(Me.lblCategoria)
        Me.Panel1.Controls.Add(Me.lblCantidad)
        Me.Panel1.Location = New System.Drawing.Point(0, 118)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(237, 119)
        Me.Panel1.Visible = False
        '
        'lblDescUnidad
        '
        Me.lblDescUnidad.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescUnidad.Location = New System.Drawing.Point(116, 41)
        Me.lblDescUnidad.Name = "lblDescUnidad"
        Me.lblDescUnidad.Size = New System.Drawing.Size(114, 21)
        '
        'lblUnidad
        '
        Me.lblUnidad.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblUnidad.Location = New System.Drawing.Point(5, 44)
        Me.lblUnidad.Name = "lblUnidad"
        Me.lblUnidad.Size = New System.Drawing.Size(106, 20)
        Me.lblUnidad.Text = "Unidad:"
        '
        'btnNuevaCarga
        '
        Me.btnNuevaCarga.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnNuevaCarga.Location = New System.Drawing.Point(5, 243)
        Me.btnNuevaCarga.Name = "btnNuevaCarga"
        Me.btnNuevaCarga.Size = New System.Drawing.Size(110, 20)
        Me.btnNuevaCarga.TabIndex = 22
        Me.btnNuevaCarga.Text = "F1) Nueva Carga"
        '
        'btnEspCantidad
        '
        Me.btnEspCantidad.BackColor = System.Drawing.Color.Gainsboro
        Me.btnEspCantidad.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnEspCantidad.Location = New System.Drawing.Point(121, 243)
        Me.btnEspCantidad.Name = "btnEspCantidad"
        Me.btnEspCantidad.Size = New System.Drawing.Size(110, 20)
        Me.btnEspCantidad.TabIndex = 23
        Me.btnEspCantidad.Text = "F2) Esp. Cantidad"
        '
        'btnVerCargados
        '
        Me.btnVerCargados.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnVerCargados.Location = New System.Drawing.Point(5, 269)
        Me.btnVerCargados.Name = "btnVerCargados"
        Me.btnVerCargados.Size = New System.Drawing.Size(110, 20)
        Me.btnVerCargados.TabIndex = 24
        Me.btnVerCargados.Text = "F3) Ver Cargados"
        '
        'btnFinalizar
        '
        Me.btnFinalizar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnFinalizar.Location = New System.Drawing.Point(121, 269)
        Me.btnFinalizar.Name = "btnFinalizar"
        Me.btnFinalizar.Size = New System.Drawing.Size(110, 20)
        Me.btnFinalizar.TabIndex = 25
        Me.btnFinalizar.Text = "F4) Finalizar"
        '
        'FrmProcesoIngreso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.btnFinalizar)
        Me.Controls.Add(Me.btnVerCargados)
        Me.Controls.Add(Me.btnEspCantidad)
        Me.Controls.Add(Me.btnNuevaCarga)
        Me.Controls.Add(Me.txtCodProducto)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.lblProveedorOrigen)
        Me.Controls.Add(Me.lblProveedor)
        Me.Controls.Add(Me.lblRemito)
        Me.Controls.Add(Me.lblNroRemito)
        Me.Controls.Add(Me.txtNroDocumento)
        Me.Controls.Add(Me.lblDocumento)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FrmProcesoIngreso"
        Me.Text = "Proceso de Ingreso"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblDocumento As System.Windows.Forms.Label
    Friend WithEvents txtNroDocumento As System.Windows.Forms.TextBox
    Friend WithEvents lblNroRemito As System.Windows.Forms.Label
    Friend WithEvents lblRemito As System.Windows.Forms.Label
    Friend WithEvents lblProveedor As System.Windows.Forms.Label
    Friend WithEvents lblProveedorOrigen As System.Windows.Forms.Label
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents txtCodProducto As System.Windows.Forms.TextBox
    Friend WithEvents lblDescripcionProd As System.Windows.Forms.Label
    Friend WithEvents lblCategoria As System.Windows.Forms.Label
    Friend WithEvents lblCatLogica As System.Windows.Forms.Label
    Friend WithEvents lblCantidad As System.Windows.Forms.Label
    Friend WithEvents lbltxtcantidad As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnNuevaCarga As System.Windows.Forms.Button
    Friend WithEvents btnEspCantidad As System.Windows.Forms.Button
    Friend WithEvents btnVerCargados As System.Windows.Forms.Button
    Friend WithEvents btnFinalizar As System.Windows.Forms.Button
    Friend WithEvents lblDescUnidad As System.Windows.Forms.Label
    Friend WithEvents lblUnidad As System.Windows.Forms.Label
End Class
