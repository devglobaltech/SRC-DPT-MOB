<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmDevolucionesPedidos
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
        Me.cmbCliente = New System.Windows.Forms.ComboBox
        Me.lblProducto = New System.Windows.Forms.Label
        Me.txtProducto = New System.Windows.Forms.TextBox
        Me.lblPallet = New System.Windows.Forms.Label
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.btnComenzar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.btnFinalizar = New System.Windows.Forms.Button
        Me.btnVerContenido = New System.Windows.Forms.Button
        Me.lblMotivo = New System.Windows.Forms.Label
        Me.cmbMotivos = New System.Windows.Forms.ComboBox
        Me.txtObservaciones = New System.Windows.Forms.TextBox
        Me.lblObservaciones = New System.Windows.Forms.Label
        Me.btnSalir = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblCliente
        '
        Me.lblCliente.Location = New System.Drawing.Point(3, 29)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(51, 20)
        Me.lblCliente.Text = "Cliente:"
        '
        'cmbCliente
        '
        Me.cmbCliente.Location = New System.Drawing.Point(60, 27)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(176, 22)
        Me.cmbCliente.TabIndex = 1
        '
        'lblProducto
        '
        Me.lblProducto.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblProducto.Location = New System.Drawing.Point(3, 51)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(233, 20)
        Me.lblProducto.Text = "Producto"
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(3, 73)
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(233, 21)
        Me.txtProducto.TabIndex = 3
        '
        'lblPallet
        '
        Me.lblPallet.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblPallet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblPallet.Location = New System.Drawing.Point(3, 3)
        Me.lblPallet.Name = "lblPallet"
        Me.lblPallet.Size = New System.Drawing.Size(237, 22)
        Me.lblPallet.Text = "Pallet Devolucion"
        Me.lblPallet.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblDescripcion
        '
        Me.lblDescripcion.Location = New System.Drawing.Point(3, 96)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(233, 35)
        Me.lblDescripcion.Text = "Producto_ID - Descripcion"
        '
        'btnComenzar
        '
        Me.btnComenzar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnComenzar.Location = New System.Drawing.Point(3, 238)
        Me.btnComenzar.Name = "btnComenzar"
        Me.btnComenzar.Size = New System.Drawing.Size(113, 18)
        Me.btnComenzar.TabIndex = 6
        Me.btnComenzar.Text = "F1) Comenzar"
        '
        'btnCancelar
        '
        Me.btnCancelar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancelar.Location = New System.Drawing.Point(127, 238)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(113, 18)
        Me.btnCancelar.TabIndex = 7
        Me.btnCancelar.Text = "F2) Cancelar"
        '
        'btnFinalizar
        '
        Me.btnFinalizar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnFinalizar.Location = New System.Drawing.Point(3, 257)
        Me.btnFinalizar.Name = "btnFinalizar"
        Me.btnFinalizar.Size = New System.Drawing.Size(113, 18)
        Me.btnFinalizar.TabIndex = 8
        Me.btnFinalizar.Text = "F3) Finalizar Pallet"
        '
        'btnVerContenido
        '
        Me.btnVerContenido.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnVerContenido.Location = New System.Drawing.Point(127, 257)
        Me.btnVerContenido.Name = "btnVerContenido"
        Me.btnVerContenido.Size = New System.Drawing.Size(113, 18)
        Me.btnVerContenido.TabIndex = 9
        Me.btnVerContenido.Text = "F4) Ver Contenido"
        '
        'lblMotivo
        '
        Me.lblMotivo.Location = New System.Drawing.Point(3, 133)
        Me.lblMotivo.Name = "lblMotivo"
        Me.lblMotivo.Size = New System.Drawing.Size(51, 20)
        Me.lblMotivo.Text = "Motivo:"
        '
        'cmbMotivos
        '
        Me.cmbMotivos.Location = New System.Drawing.Point(48, 133)
        Me.cmbMotivos.Name = "cmbMotivos"
        Me.cmbMotivos.Size = New System.Drawing.Size(188, 22)
        Me.cmbMotivos.TabIndex = 11
        '
        'txtObservaciones
        '
        Me.txtObservaciones.Location = New System.Drawing.Point(3, 180)
        Me.txtObservaciones.Multiline = True
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.Size = New System.Drawing.Size(234, 56)
        Me.txtObservaciones.TabIndex = 12
        '
        'lblObservaciones
        '
        Me.lblObservaciones.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblObservaciones.Location = New System.Drawing.Point(3, 158)
        Me.lblObservaciones.Name = "lblObservaciones"
        Me.lblObservaciones.Size = New System.Drawing.Size(234, 20)
        Me.lblObservaciones.Text = "Observaciones"
        '
        'btnSalir
        '
        Me.btnSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnSalir.Location = New System.Drawing.Point(3, 276)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(113, 18)
        Me.btnSalir.TabIndex = 18
        Me.btnSalir.Text = "F5) Salir"
        '
        'frmDevolucionesPedidos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.lblObservaciones)
        Me.Controls.Add(Me.txtObservaciones)
        Me.Controls.Add(Me.cmbMotivos)
        Me.Controls.Add(Me.lblMotivo)
        Me.Controls.Add(Me.btnVerContenido)
        Me.Controls.Add(Me.btnFinalizar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnComenzar)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.lblPallet)
        Me.Controls.Add(Me.txtProducto)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.cmbCliente)
        Me.Controls.Add(Me.lblCliente)
        Me.KeyPreview = True
        Me.Name = "frmDevolucionesPedidos"
        Me.Text = "Devoluciones"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents cmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents lblPallet As System.Windows.Forms.Label
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents btnComenzar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnFinalizar As System.Windows.Forms.Button
    Friend WithEvents btnVerContenido As System.Windows.Forms.Button
    Friend WithEvents lblMotivo As System.Windows.Forms.Label
    Friend WithEvents cmbMotivos As System.Windows.Forms.ComboBox
    Friend WithEvents txtObservaciones As System.Windows.Forms.TextBox
    Friend WithEvents lblObservaciones As System.Windows.Forms.Label
    Friend WithEvents btnSalir As System.Windows.Forms.Button
End Class
