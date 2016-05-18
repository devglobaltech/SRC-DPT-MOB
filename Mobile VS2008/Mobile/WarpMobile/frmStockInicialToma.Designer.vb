<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmStockInicialToma
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
        Me.lblCliente = New System.Windows.Forms.Label
        Me.lblUbicacion = New System.Windows.Forms.Label
        Me.txtUbicacion = New System.Windows.Forms.TextBox
        Me.lblProducto = New System.Windows.Forms.Label
        Me.txtProducto = New System.Windows.Forms.TextBox
        Me.btnComenzar = New System.Windows.Forms.Button
        Me.btnCambiarUbicacion = New System.Windows.Forms.Button
        Me.btnConf = New System.Windows.Forms.Button
        Me.btnSalir = New System.Windows.Forms.Button
        Me.lblCantidad = New System.Windows.Forms.Label
        Me.txtCantidad = New System.Windows.Forms.TextBox
        Me.lblAdicionales = New System.Windows.Forms.Label
        Me.lblLote = New System.Windows.Forms.Label
        Me.txtLote = New System.Windows.Forms.TextBox
        Me.txtPartida = New System.Windows.Forms.TextBox
        Me.lblPartida = New System.Windows.Forms.Label
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.btnStockPos = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.Control
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCliente.Location = New System.Drawing.Point(0, 4)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(240, 20)
        Me.lblCliente.Text = "Cliente: "
        '
        'lblUbicacion
        '
        Me.lblUbicacion.Location = New System.Drawing.Point(0, 28)
        Me.lblUbicacion.Name = "lblUbicacion"
        Me.lblUbicacion.Size = New System.Drawing.Size(67, 20)
        Me.lblUbicacion.Text = "Ubicación:"
        '
        'txtUbicacion
        '
        Me.txtUbicacion.Location = New System.Drawing.Point(66, 27)
        Me.txtUbicacion.MaxLength = 45
        Me.txtUbicacion.Name = "txtUbicacion"
        Me.txtUbicacion.Size = New System.Drawing.Size(148, 21)
        Me.txtUbicacion.TabIndex = 2
        '
        'lblProducto
        '
        Me.lblProducto.Location = New System.Drawing.Point(0, 51)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(67, 20)
        Me.lblProducto.Text = "Producto: "
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(66, 51)
        Me.txtProducto.MaxLength = 60
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(174, 21)
        Me.txtProducto.TabIndex = 6
        '
        'btnComenzar
        '
        Me.btnComenzar.Location = New System.Drawing.Point(0, 245)
        Me.btnComenzar.Name = "btnComenzar"
        Me.btnComenzar.Size = New System.Drawing.Size(117, 20)
        Me.btnComenzar.TabIndex = 10
        Me.btnComenzar.Text = "F1) Comenzar"
        '
        'btnCambiarUbicacion
        '
        Me.btnCambiarUbicacion.Location = New System.Drawing.Point(123, 245)
        Me.btnCambiarUbicacion.Name = "btnCambiarUbicacion"
        Me.btnCambiarUbicacion.Size = New System.Drawing.Size(117, 20)
        Me.btnCambiarUbicacion.TabIndex = 11
        Me.btnCambiarUbicacion.Text = "F2) Sel. Ubicacion"
        '
        'btnConf
        '
        Me.btnConf.Location = New System.Drawing.Point(0, 271)
        Me.btnConf.Name = "btnConf"
        Me.btnConf.Size = New System.Drawing.Size(117, 20)
        Me.btnConf.TabIndex = 12
        Me.btnConf.Text = "F3) Configurar"
        '
        'btnSalir
        '
        Me.btnSalir.Location = New System.Drawing.Point(123, 271)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(117, 20)
        Me.btnSalir.TabIndex = 13
        Me.btnSalir.Text = "F4) Salir"
        '
        'lblCantidad
        '
        Me.lblCantidad.Location = New System.Drawing.Point(0, 113)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(67, 20)
        Me.lblCantidad.Text = "Cantidad:"
        '
        'txtCantidad
        '
        Me.txtCantidad.Location = New System.Drawing.Point(66, 113)
        Me.txtCantidad.MaxLength = 10
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(100, 21)
        Me.txtCantidad.TabIndex = 15
        '
        'lblAdicionales
        '
        Me.lblAdicionales.BackColor = System.Drawing.SystemColors.Control
        Me.lblAdicionales.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblAdicionales.Location = New System.Drawing.Point(0, 147)
        Me.lblAdicionales.Name = "lblAdicionales"
        Me.lblAdicionales.Size = New System.Drawing.Size(240, 20)
        Me.lblAdicionales.Text = "Datos adicionales"
        Me.lblAdicionales.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblLote
        '
        Me.lblLote.Location = New System.Drawing.Point(0, 171)
        Me.lblLote.Name = "lblLote"
        Me.lblLote.Size = New System.Drawing.Size(67, 20)
        Me.lblLote.Text = "Lote:"
        '
        'txtLote
        '
        Me.txtLote.Location = New System.Drawing.Point(66, 171)
        Me.txtLote.MaxLength = 50
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Size = New System.Drawing.Size(174, 21)
        Me.txtLote.TabIndex = 18
        '
        'txtPartida
        '
        Me.txtPartida.Location = New System.Drawing.Point(66, 195)
        Me.txtPartida.MaxLength = 50
        Me.txtPartida.Name = "txtPartida"
        Me.txtPartida.Size = New System.Drawing.Size(174, 21)
        Me.txtPartida.TabIndex = 20
        '
        'lblPartida
        '
        Me.lblPartida.Location = New System.Drawing.Point(0, 195)
        Me.lblPartida.Name = "lblPartida"
        Me.lblPartida.Size = New System.Drawing.Size(67, 20)
        Me.lblPartida.Text = "Partida:"
        '
        'lblDescripcion
        '
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescripcion.Location = New System.Drawing.Point(0, 75)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(240, 34)
        Me.lblDescripcion.Text = "DesProducto..."
        '
        'btnStockPos
        '
        Me.btnStockPos.Location = New System.Drawing.Point(217, 27)
        Me.btnStockPos.Name = "btnStockPos"
        Me.btnStockPos.Size = New System.Drawing.Size(22, 20)
        Me.btnStockPos.TabIndex = 28
        Me.btnStockPos.Text = "..."
        '
        'frmStockInicialToma
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnStockPos)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.txtPartida)
        Me.Controls.Add(Me.lblPartida)
        Me.Controls.Add(Me.txtLote)
        Me.Controls.Add(Me.lblLote)
        Me.Controls.Add(Me.lblAdicionales)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.lblCantidad)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.btnConf)
        Me.Controls.Add(Me.btnCambiarUbicacion)
        Me.Controls.Add(Me.btnComenzar)
        Me.Controls.Add(Me.txtProducto)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.txtUbicacion)
        Me.Controls.Add(Me.lblUbicacion)
        Me.Controls.Add(Me.lblCliente)
        Me.KeyPreview = True
        Me.Name = "frmStockInicialToma"
        Me.Text = "Toma de Stock Inicial"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblUbicacion As System.Windows.Forms.Label
    Friend WithEvents txtUbicacion As System.Windows.Forms.TextBox
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents btnComenzar As System.Windows.Forms.Button
    Friend WithEvents btnCambiarUbicacion As System.Windows.Forms.Button
    Friend WithEvents btnConf As System.Windows.Forms.Button
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents lblCantidad As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents lblAdicionales As System.Windows.Forms.Label
    Friend WithEvents lblLote As System.Windows.Forms.Label
    Friend WithEvents txtLote As System.Windows.Forms.TextBox
    Friend WithEvents txtPartida As System.Windows.Forms.TextBox
    Friend WithEvents lblPartida As System.Windows.Forms.Label
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents btnStockPos As System.Windows.Forms.Button
End Class
