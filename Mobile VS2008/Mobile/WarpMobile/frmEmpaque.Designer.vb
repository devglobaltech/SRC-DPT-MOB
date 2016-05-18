<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEmpaque
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
        Me.btnComenzar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.lblCodigoOla = New System.Windows.Forms.Label
        Me.txtCodigoOla = New System.Windows.Forms.TextBox
        Me.lblContenedoras = New System.Windows.Forms.Label
        Me.lblProducto = New System.Windows.Forms.Label
        Me.txtProducto = New System.Windows.Forms.TextBox
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.btnSalir = New System.Windows.Forms.Button
        Me.lblCantidad = New System.Windows.Forms.Label
        Me.txtCantidad = New System.Windows.Forms.TextBox
        Me.txtPartida = New System.Windows.Forms.TextBox
        Me.lblNroPartida = New System.Windows.Forms.Label
        Me.txtNroLote = New System.Windows.Forms.TextBox
        Me.lblNroLote = New System.Windows.Forms.Label
        Me.txtSerie = New System.Windows.Forms.TextBox
        Me.lblNroSerie = New System.Windows.Forms.Label
        Me.btnContenedoras = New System.Windows.Forms.Button
        Me.btnPendientes = New System.Windows.Forms.Button
        Me.btnFinalizar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnComenzar
        '
        Me.btnComenzar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnComenzar.Location = New System.Drawing.Point(3, 230)
        Me.btnComenzar.Name = "btnComenzar"
        Me.btnComenzar.Size = New System.Drawing.Size(111, 18)
        Me.btnComenzar.TabIndex = 3
        Me.btnComenzar.Text = "F1) Comenzar"
        '
        'btnCancelar
        '
        Me.btnCancelar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancelar.Location = New System.Drawing.Point(126, 230)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(111, 18)
        Me.btnCancelar.TabIndex = 4
        Me.btnCancelar.Text = "F2) Cancelar"
        '
        'lblCodigoOla
        '
        Me.lblCodigoOla.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblCodigoOla.Location = New System.Drawing.Point(0, 2)
        Me.lblCodigoOla.Name = "lblCodigoOla"
        Me.lblCodigoOla.Size = New System.Drawing.Size(71, 19)
        Me.lblCodigoOla.Text = "Codigo Ola:"
        '
        'txtCodigoOla
        '
        Me.txtCodigoOla.Enabled = False
        Me.txtCodigoOla.Location = New System.Drawing.Point(68, 0)
        Me.txtCodigoOla.Name = "txtCodigoOla"
        Me.txtCodigoOla.Size = New System.Drawing.Size(157, 21)
        Me.txtCodigoOla.TabIndex = 7
        '
        'lblContenedoras
        '
        Me.lblContenedoras.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblContenedoras.Location = New System.Drawing.Point(0, 22)
        Me.lblContenedoras.Name = "lblContenedoras"
        Me.lblContenedoras.Size = New System.Drawing.Size(240, 18)
        Me.lblContenedoras.Text = "Contenedoras:"
        '
        'lblProducto
        '
        Me.lblProducto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblProducto.Location = New System.Drawing.Point(0, 43)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(85, 19)
        Me.lblProducto.Text = "Cod.Producto:"
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(82, 42)
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(143, 21)
        Me.txtProducto.TabIndex = 10
        '
        'lblDescripcion
        '
        Me.lblDescripcion.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescripcion.Location = New System.Drawing.Point(0, 64)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(240, 81)
        '
        'btnSalir
        '
        Me.btnSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnSalir.Location = New System.Drawing.Point(3, 250)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(111, 18)
        Me.btnSalir.TabIndex = 15
        Me.btnSalir.Text = "F3) Salir"
        '
        'lblCantidad
        '
        Me.lblCantidad.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblCantidad.Location = New System.Drawing.Point(0, 213)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(114, 13)
        Me.lblCantidad.Text = "Confirme Cantidad:"
        '
        'txtCantidad
        '
        Me.txtCantidad.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtCantidad.Location = New System.Drawing.Point(108, 210)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(117, 19)
        Me.txtCantidad.TabIndex = 22
        '
        'txtPartida
        '
        Me.txtPartida.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtPartida.Location = New System.Drawing.Point(87, 168)
        Me.txtPartida.Name = "txtPartida"
        Me.txtPartida.Size = New System.Drawing.Size(138, 19)
        Me.txtPartida.TabIndex = 26
        '
        'lblNroPartida
        '
        Me.lblNroPartida.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblNroPartida.Location = New System.Drawing.Point(0, 169)
        Me.lblNroPartida.Name = "lblNroPartida"
        Me.lblNroPartida.Size = New System.Drawing.Size(92, 18)
        Me.lblNroPartida.Text = "Nro. Partida:"
        '
        'txtNroLote
        '
        Me.txtNroLote.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtNroLote.Location = New System.Drawing.Point(87, 147)
        Me.txtNroLote.Name = "txtNroLote"
        Me.txtNroLote.Size = New System.Drawing.Size(138, 19)
        Me.txtNroLote.TabIndex = 25
        '
        'lblNroLote
        '
        Me.lblNroLote.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblNroLote.Location = New System.Drawing.Point(0, 147)
        Me.lblNroLote.Name = "lblNroLote"
        Me.lblNroLote.Size = New System.Drawing.Size(81, 19)
        Me.lblNroLote.Text = "Nro. de Lote:"
        '
        'txtSerie
        '
        Me.txtSerie.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtSerie.Location = New System.Drawing.Point(87, 189)
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.Size = New System.Drawing.Size(138, 19)
        Me.txtSerie.TabIndex = 30
        '
        'lblNroSerie
        '
        Me.lblNroSerie.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblNroSerie.Location = New System.Drawing.Point(0, 190)
        Me.lblNroSerie.Name = "lblNroSerie"
        Me.lblNroSerie.Size = New System.Drawing.Size(92, 18)
        Me.lblNroSerie.Text = "Nro. Serie"
        '
        'btnContenedoras
        '
        Me.btnContenedoras.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnContenedoras.Location = New System.Drawing.Point(126, 250)
        Me.btnContenedoras.Name = "btnContenedoras"
        Me.btnContenedoras.Size = New System.Drawing.Size(111, 18)
        Me.btnContenedoras.TabIndex = 48
        Me.btnContenedoras.Text = "F4) Cont.Empaque"
        '
        'btnPendientes
        '
        Me.btnPendientes.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnPendientes.Location = New System.Drawing.Point(3, 270)
        Me.btnPendientes.Name = "btnPendientes"
        Me.btnPendientes.Size = New System.Drawing.Size(111, 18)
        Me.btnPendientes.TabIndex = 57
        Me.btnPendientes.Text = "F5) Pendientes"
        '
        'btnFinalizar
        '
        Me.btnFinalizar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnFinalizar.Location = New System.Drawing.Point(126, 270)
        Me.btnFinalizar.Name = "btnFinalizar"
        Me.btnFinalizar.Size = New System.Drawing.Size(111, 18)
        Me.btnFinalizar.TabIndex = 66
        Me.btnFinalizar.Text = "F6) Finalizar"
        '
        'frmEmpaque
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnFinalizar)
        Me.Controls.Add(Me.btnPendientes)
        Me.Controls.Add(Me.btnContenedoras)
        Me.Controls.Add(Me.txtSerie)
        Me.Controls.Add(Me.lblNroSerie)
        Me.Controls.Add(Me.txtPartida)
        Me.Controls.Add(Me.lblNroPartida)
        Me.Controls.Add(Me.txtNroLote)
        Me.Controls.Add(Me.lblNroLote)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.lblCantidad)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.txtProducto)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.lblContenedoras)
        Me.Controls.Add(Me.txtCodigoOla)
        Me.Controls.Add(Me.lblCodigoOla)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnComenzar)
        Me.Controls.Add(Me.lblDescripcion)
        Me.KeyPreview = True
        Me.Name = "frmEmpaque"
        Me.Text = "Empaquetado"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnComenzar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents lblCodigoOla As System.Windows.Forms.Label
    Friend WithEvents txtCodigoOla As System.Windows.Forms.TextBox
    Friend WithEvents lblContenedoras As System.Windows.Forms.Label
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents lblCantidad As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents txtPartida As System.Windows.Forms.TextBox
    Friend WithEvents lblNroPartida As System.Windows.Forms.Label
    Friend WithEvents txtNroLote As System.Windows.Forms.TextBox
    Friend WithEvents lblNroLote As System.Windows.Forms.Label
    Friend WithEvents txtSerie As System.Windows.Forms.TextBox
    Friend WithEvents lblNroSerie As System.Windows.Forms.Label
    Friend WithEvents btnContenedoras As System.Windows.Forms.Button
    Friend WithEvents btnPendientes As System.Windows.Forms.Button
    Friend WithEvents btnFinalizar As System.Windows.Forms.Button
End Class
