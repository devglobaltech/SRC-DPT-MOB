<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmCargaSeries
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
        Me.lblIngresoSeries = New System.Windows.Forms.Label
        Me.lblCliente = New System.Windows.Forms.Label
        Me.cmbClientes = New System.Windows.Forms.ComboBox
        Me.lblNroContenedora = New System.Windows.Forms.Label
        Me.txtNroContenedora = New System.Windows.Forms.TextBox
        Me.lblDescripcionProducto = New System.Windows.Forms.Label
        Me.txtProducto = New System.Windows.Forms.TextBox
        Me.txtSerie = New System.Windows.Forms.TextBox
        Me.lblNroSerie = New System.Windows.Forms.Label
        Me.DgSeries = New System.Windows.Forms.DataGrid
        Me.cmdComenzar = New System.Windows.Forms.Button
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.cmdConfirmar = New System.Windows.Forms.Button
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.lblCantSeries = New System.Windows.Forms.Label
        Me.btnSEspecifica = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblIngresoSeries
        '
        Me.lblIngresoSeries.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblIngresoSeries.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblIngresoSeries.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblIngresoSeries.Location = New System.Drawing.Point(0, 0)
        Me.lblIngresoSeries.Name = "lblIngresoSeries"
        Me.lblIngresoSeries.Size = New System.Drawing.Size(240, 20)
        Me.lblIngresoSeries.Text = "Ingreso de Series"
        Me.lblIngresoSeries.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblCliente
        '
        Me.lblCliente.Location = New System.Drawing.Point(1, 22)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(77, 20)
        Me.lblCliente.Text = "Cod. Cliente:"
        '
        'cmbClientes
        '
        Me.cmbClientes.Location = New System.Drawing.Point(84, 21)
        Me.cmbClientes.Name = "cmbClientes"
        Me.cmbClientes.Size = New System.Drawing.Size(156, 22)
        Me.cmbClientes.TabIndex = 2
        '
        'lblNroContenedora
        '
        Me.lblNroContenedora.Location = New System.Drawing.Point(1, 45)
        Me.lblNroContenedora.Name = "lblNroContenedora"
        Me.lblNroContenedora.Size = New System.Drawing.Size(116, 20)
        Me.lblNroContenedora.Text = "Nro. Contenedora:"
        '
        'txtNroContenedora
        '
        Me.txtNroContenedora.Location = New System.Drawing.Point(112, 45)
        Me.txtNroContenedora.Name = "txtNroContenedora"
        Me.txtNroContenedora.Size = New System.Drawing.Size(128, 21)
        Me.txtNroContenedora.TabIndex = 4
        '
        'lblDescripcionProducto
        '
        Me.lblDescripcionProducto.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblDescripcionProducto.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblDescripcionProducto.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblDescripcionProducto.Location = New System.Drawing.Point(0, 68)
        Me.lblDescripcionProducto.Name = "lblDescripcionProducto"
        Me.lblDescripcionProducto.Size = New System.Drawing.Size(240, 20)
        Me.lblDescripcionProducto.Text = "Cod. Producto / Descripcion"
        Me.lblDescripcionProducto.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtProducto
        '
        Me.txtProducto.Enabled = False
        Me.txtProducto.Location = New System.Drawing.Point(0, 89)
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(240, 21)
        Me.txtProducto.TabIndex = 7
        '
        'txtSerie
        '
        Me.txtSerie.Location = New System.Drawing.Point(67, 132)
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.Size = New System.Drawing.Size(173, 21)
        Me.txtSerie.TabIndex = 9
        '
        'lblNroSerie
        '
        Me.lblNroSerie.Location = New System.Drawing.Point(0, 133)
        Me.lblNroSerie.Name = "lblNroSerie"
        Me.lblNroSerie.Size = New System.Drawing.Size(78, 20)
        Me.lblNroSerie.Text = "Nro. Serie:"
        '
        'DgSeries
        '
        Me.DgSeries.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.DgSeries.Location = New System.Drawing.Point(0, 154)
        Me.DgSeries.Name = "DgSeries"
        Me.DgSeries.Size = New System.Drawing.Size(240, 98)
        Me.DgSeries.TabIndex = 11
        '
        'cmdComenzar
        '
        Me.cmdComenzar.Location = New System.Drawing.Point(1, 255)
        Me.cmdComenzar.Name = "cmdComenzar"
        Me.cmdComenzar.Size = New System.Drawing.Size(116, 17)
        Me.cmdComenzar.TabIndex = 12
        Me.cmdComenzar.Text = "F1) Comenzar"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.Location = New System.Drawing.Point(123, 255)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(116, 17)
        Me.cmdCancelar.TabIndex = 13
        Me.cmdCancelar.Text = "F2) Cancelar"
        '
        'cmdConfirmar
        '
        Me.cmdConfirmar.Location = New System.Drawing.Point(1, 274)
        Me.cmdConfirmar.Name = "cmdConfirmar"
        Me.cmdConfirmar.Size = New System.Drawing.Size(116, 17)
        Me.cmdConfirmar.TabIndex = 14
        Me.cmdConfirmar.Text = "F3) Confirmar"
        '
        'cmdSalir
        '
        Me.cmdSalir.Location = New System.Drawing.Point(123, 274)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(116, 17)
        Me.cmdSalir.TabIndex = 15
        Me.cmdSalir.Text = "F4) Salir"
        '
        'lblCantSeries
        '
        Me.lblCantSeries.Location = New System.Drawing.Point(0, 113)
        Me.lblCantSeries.Name = "lblCantSeries"
        Me.lblCantSeries.Size = New System.Drawing.Size(117, 17)
        Me.lblCantSeries.Text = "Series "
        '
        'btnSEspecifica
        '
        Me.btnSEspecifica.Location = New System.Drawing.Point(112, 111)
        Me.btnSEspecifica.Name = "btnSEspecifica"
        Me.btnSEspecifica.Size = New System.Drawing.Size(128, 20)
        Me.btnSEspecifica.TabIndex = 21
        Me.btnSEspecifica.Text = "Serie Esp: Off"
        '
        'frmCargaSeries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnSEspecifica)
        Me.Controls.Add(Me.lblCantSeries)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdConfirmar)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdComenzar)
        Me.Controls.Add(Me.DgSeries)
        Me.Controls.Add(Me.txtSerie)
        Me.Controls.Add(Me.lblNroSerie)
        Me.Controls.Add(Me.txtProducto)
        Me.Controls.Add(Me.lblDescripcionProducto)
        Me.Controls.Add(Me.txtNroContenedora)
        Me.Controls.Add(Me.lblNroContenedora)
        Me.Controls.Add(Me.cmbClientes)
        Me.Controls.Add(Me.lblCliente)
        Me.Controls.Add(Me.lblIngresoSeries)
        Me.KeyPreview = True
        Me.Name = "frmCargaSeries"
        Me.Text = "Carga de Series"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblIngresoSeries As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents cmbClientes As System.Windows.Forms.ComboBox
    Friend WithEvents lblNroContenedora As System.Windows.Forms.Label
    Friend WithEvents txtNroContenedora As System.Windows.Forms.TextBox
    Friend WithEvents lblDescripcionProducto As System.Windows.Forms.Label
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents txtSerie As System.Windows.Forms.TextBox
    Friend WithEvents lblNroSerie As System.Windows.Forms.Label
    Friend WithEvents DgSeries As System.Windows.Forms.DataGrid
    Friend WithEvents cmdComenzar As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents cmdConfirmar As System.Windows.Forms.Button
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents lblCantSeries As System.Windows.Forms.Label
    Friend WithEvents btnSEspecifica As System.Windows.Forms.Button
End Class
