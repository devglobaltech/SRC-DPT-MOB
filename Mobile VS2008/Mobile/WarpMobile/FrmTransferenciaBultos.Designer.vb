<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmTransferenciaBultos
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
        Me.lblorigen = New System.Windows.Forms.Label
        Me.txtOrigen = New System.Windows.Forms.TextBox
        Me.lblProducto = New System.Windows.Forms.Label
        Me.txtProducto = New System.Windows.Forms.TextBox
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.lblCantDisponible = New System.Windows.Forms.Label
        Me.lblCantidad = New System.Windows.Forms.Label
        Me.txtCantTransferir = New System.Windows.Forms.TextBox
        Me.lblDestinoSugerida = New System.Windows.Forms.Label
        Me.lblDestino = New System.Windows.Forms.Label
        Me.txtDestino = New System.Windows.Forms.TextBox
        Me.lblMensaje = New System.Windows.Forms.Label
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.btnVolver = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbClienteId = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.PanelCatLog = New System.Windows.Forms.Panel
        Me.ListCatLog = New System.Windows.Forms.ListBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.PanelCatLog.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblorigen
        '
        Me.lblorigen.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblorigen.Location = New System.Drawing.Point(4, 0)
        Me.lblorigen.Name = "lblorigen"
        Me.lblorigen.Size = New System.Drawing.Size(121, 20)
        Me.lblorigen.Text = "Ubicacion Origen"
        '
        'txtOrigen
        '
        Me.txtOrigen.Location = New System.Drawing.Point(4, 14)
        Me.txtOrigen.MaxLength = 45
        Me.txtOrigen.Name = "txtOrigen"
        Me.txtOrigen.Size = New System.Drawing.Size(233, 21)
        Me.txtOrigen.TabIndex = 1
        '
        'lblProducto
        '
        Me.lblProducto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblProducto.Location = New System.Drawing.Point(4, 68)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(100, 20)
        Me.lblProducto.Text = "Producto"
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(79, 66)
        Me.txtProducto.MaxLength = 55
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(158, 21)
        Me.txtProducto.TabIndex = 3
        '
        'lblDescripcion
        '
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescripcion.Location = New System.Drawing.Point(4, 92)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(227, 31)
        Me.lblDescripcion.Text = "x"
        '
        'lblCantDisponible
        '
        Me.lblCantDisponible.Location = New System.Drawing.Point(117, 127)
        Me.lblCantDisponible.Name = "lblCantDisponible"
        Me.lblCantDisponible.Size = New System.Drawing.Size(114, 20)
        Me.lblCantDisponible.Text = "x"
        '
        'lblCantidad
        '
        Me.lblCantidad.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblCantidad.Location = New System.Drawing.Point(4, 153)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(122, 17)
        Me.lblCantidad.Text = "Cant. a Transferir:"
        '
        'txtCantTransferir
        '
        Me.txtCantTransferir.Location = New System.Drawing.Point(117, 149)
        Me.txtCantTransferir.MaxLength = 10
        Me.txtCantTransferir.Name = "txtCantTransferir"
        Me.txtCantTransferir.Size = New System.Drawing.Size(120, 21)
        Me.txtCantTransferir.TabIndex = 4
        '
        'lblDestinoSugerida
        '
        Me.lblDestinoSugerida.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDestinoSugerida.Location = New System.Drawing.Point(122, 173)
        Me.lblDestinoSugerida.Name = "lblDestinoSugerida"
        Me.lblDestinoSugerida.Size = New System.Drawing.Size(110, 20)
        Me.lblDestinoSugerida.Text = "lblDestinoSugerido"
        '
        'lblDestino
        '
        Me.lblDestino.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDestino.Location = New System.Drawing.Point(4, 173)
        Me.lblDestino.Name = "lblDestino"
        Me.lblDestino.Size = New System.Drawing.Size(121, 20)
        Me.lblDestino.Text = "Ubicacion Destino "
        '
        'txtDestino
        '
        Me.txtDestino.Location = New System.Drawing.Point(4, 189)
        Me.txtDestino.MaxLength = 45
        Me.txtDestino.Name = "txtDestino"
        Me.txtDestino.Size = New System.Drawing.Size(213, 21)
        Me.txtDestino.TabIndex = 5
        '
        'lblMensaje
        '
        Me.lblMensaje.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblMensaje.ForeColor = System.Drawing.Color.Red
        Me.lblMensaje.Location = New System.Drawing.Point(4, 213)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(227, 47)
        Me.lblMensaje.Text = "lblMensaje"
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(4, 264)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(88, 20)
        Me.btnCancelar.TabIndex = 6
        Me.btnCancelar.Text = "F1 Cancelar"
        '
        'btnVolver
        '
        Me.btnVolver.Location = New System.Drawing.Point(131, 264)
        Me.btnVolver.Name = "btnVolver"
        Me.btnVolver.Size = New System.Drawing.Size(72, 20)
        Me.btnVolver.TabIndex = 7
        Me.btnVolver.Text = "F2 Salir"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(4, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 20)
        Me.Label1.Text = "Compañia"
        Me.Label1.Visible = False
        '
        'cmbClienteId
        '
        Me.cmbClienteId.Location = New System.Drawing.Point(79, 37)
        Me.cmbClienteId.Name = "cmbClienteId"
        Me.cmbClienteId.Size = New System.Drawing.Size(158, 22)
        Me.cmbClienteId.TabIndex = 2
        Me.cmbClienteId.Visible = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(4, 127)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(107, 20)
        Me.Label2.Text = "Cant. Transferible:"
        '
        'PanelCatLog
        '
        Me.PanelCatLog.Controls.Add(Me.ListCatLog)
        Me.PanelCatLog.Controls.Add(Me.Label3)
        Me.PanelCatLog.Location = New System.Drawing.Point(4, 68)
        Me.PanelCatLog.Name = "PanelCatLog"
        Me.PanelCatLog.Size = New System.Drawing.Size(236, 184)
        Me.PanelCatLog.Visible = False
        '
        'ListCatLog
        '
        Me.ListCatLog.Location = New System.Drawing.Point(9, 49)
        Me.ListCatLog.Name = "ListCatLog"
        Me.ListCatLog.Size = New System.Drawing.Size(219, 128)
        Me.ListCatLog.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.Label3.Location = New System.Drawing.Point(10, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(189, 25)
        Me.Label3.Text = "Seleccione Cat. Lógica"
        '
        'FrmTransferenciaBultos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.PanelCatLog)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbClienteId)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnVolver)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.txtDestino)
        Me.Controls.Add(Me.txtCantTransferir)
        Me.Controls.Add(Me.txtProducto)
        Me.Controls.Add(Me.lblDestinoSugerida)
        Me.Controls.Add(Me.lblMensaje)
        Me.Controls.Add(Me.lblDestino)
        Me.Controls.Add(Me.lblCantidad)
        Me.Controls.Add(Me.lblCantDisponible)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.txtOrigen)
        Me.Controls.Add(Me.lblorigen)
        Me.KeyPreview = True
        Me.Name = "FrmTransferenciaBultos"
        Me.Text = "Transferencia por Bultos"
        Me.PanelCatLog.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblorigen As System.Windows.Forms.Label
    Friend WithEvents txtOrigen As System.Windows.Forms.TextBox
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents lblCantDisponible As System.Windows.Forms.Label
    Friend WithEvents lblCantidad As System.Windows.Forms.Label
    Friend WithEvents txtCantTransferir As System.Windows.Forms.TextBox
    Friend WithEvents lblDestinoSugerida As System.Windows.Forms.Label
    Friend WithEvents lblDestino As System.Windows.Forms.Label
    Friend WithEvents txtDestino As System.Windows.Forms.TextBox
    Friend WithEvents lblMensaje As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnVolver As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbClienteId As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PanelCatLog As System.Windows.Forms.Panel
    Friend WithEvents ListCatLog As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
