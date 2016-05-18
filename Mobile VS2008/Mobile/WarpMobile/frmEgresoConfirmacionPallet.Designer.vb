<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEgresoConfirmacionPallet
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
        Me.txtCliente = New System.Windows.Forms.TextBox
        Me.lblProducto = New System.Windows.Forms.Label
        Me.txtProducto = New System.Windows.Forms.TextBox
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.lblNro_Lote = New System.Windows.Forms.Label
        Me.lblNroPartida = New System.Windows.Forms.Label
        Me.lblNroPallet = New System.Windows.Forms.Label
        Me.txtPallet = New System.Windows.Forms.TextBox
        Me.btnACEPTAR = New System.Windows.Forms.Button
        Me.btnSALIR = New System.Windows.Forms.Button
        Me.LblUbicacion = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblCliente
        '
        Me.lblCliente.Location = New System.Drawing.Point(0, 4)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(78, 20)
        Me.lblCliente.Text = "Cod. Cliente:"
        '
        'txtCliente
        '
        Me.txtCliente.Location = New System.Drawing.Point(87, 4)
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.Size = New System.Drawing.Size(153, 21)
        Me.txtCliente.TabIndex = 1
        '
        'lblProducto
        '
        Me.lblProducto.Location = New System.Drawing.Point(0, 28)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(86, 20)
        Me.lblProducto.Text = "Cod.Producto:"
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(87, 27)
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(153, 21)
        Me.txtProducto.TabIndex = 2
        '
        'lblDescripcion
        '
        Me.lblDescripcion.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblDescripcion.Location = New System.Drawing.Point(0, 51)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(240, 19)
        Me.lblDescripcion.Text = "Descripcion"
        Me.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblNro_Lote
        '
        Me.lblNro_Lote.Location = New System.Drawing.Point(0, 95)
        Me.lblNro_Lote.Name = "lblNro_Lote"
        Me.lblNro_Lote.Size = New System.Drawing.Size(240, 20)
        Me.lblNro_Lote.Text = "Nro.Lote"
        '
        'lblNroPartida
        '
        Me.lblNroPartida.Location = New System.Drawing.Point(0, 119)
        Me.lblNroPartida.Name = "lblNroPartida"
        Me.lblNroPartida.Size = New System.Drawing.Size(240, 20)
        Me.lblNroPartida.Text = "Nro.Partida"
        '
        'lblNroPallet
        '
        Me.lblNroPallet.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblNroPallet.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblNroPallet.Location = New System.Drawing.Point(0, 139)
        Me.lblNroPallet.Name = "lblNroPallet"
        Me.lblNroPallet.Size = New System.Drawing.Size(240, 20)
        Me.lblNroPallet.Text = "Nro.Pallet"
        Me.lblNroPallet.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPallet
        '
        Me.txtPallet.Location = New System.Drawing.Point(0, 161)
        Me.txtPallet.MaxLength = 50
        Me.txtPallet.Name = "txtPallet"
        Me.txtPallet.Size = New System.Drawing.Size(240, 21)
        Me.txtPallet.TabIndex = 0
        '
        'btnACEPTAR
        '
        Me.btnACEPTAR.Location = New System.Drawing.Point(0, 204)
        Me.btnACEPTAR.Name = "btnACEPTAR"
        Me.btnACEPTAR.Size = New System.Drawing.Size(112, 20)
        Me.btnACEPTAR.TabIndex = 10
        Me.btnACEPTAR.Text = "Aceptar"
        '
        'btnSALIR
        '
        Me.btnSALIR.Location = New System.Drawing.Point(128, 204)
        Me.btnSALIR.Name = "btnSALIR"
        Me.btnSALIR.Size = New System.Drawing.Size(112, 20)
        Me.btnSALIR.TabIndex = 11
        Me.btnSALIR.Text = "Salir"
        '
        'LblUbicacion
        '
        Me.LblUbicacion.Location = New System.Drawing.Point(0, 73)
        Me.LblUbicacion.Name = "LblUbicacion"
        Me.LblUbicacion.Size = New System.Drawing.Size(239, 20)
        Me.LblUbicacion.Text = "Ubicacion:"
        '
        'frmEgresoConfirmacionPallet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.LblUbicacion)
        Me.Controls.Add(Me.btnSALIR)
        Me.Controls.Add(Me.btnACEPTAR)
        Me.Controls.Add(Me.txtPallet)
        Me.Controls.Add(Me.lblNroPallet)
        Me.Controls.Add(Me.lblNroPartida)
        Me.Controls.Add(Me.lblNro_Lote)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.txtProducto)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.txtCliente)
        Me.Controls.Add(Me.lblCliente)
        Me.KeyPreview = True
        Me.Name = "frmEgresoConfirmacionPallet"
        Me.Text = "Confirmacion Pallet"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents txtCliente As System.Windows.Forms.TextBox
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents lblNro_Lote As System.Windows.Forms.Label
    Friend WithEvents lblNroPartida As System.Windows.Forms.Label
    Friend WithEvents lblNroPallet As System.Windows.Forms.Label
    Friend WithEvents txtPallet As System.Windows.Forms.TextBox
    Friend WithEvents btnACEPTAR As System.Windows.Forms.Button
    Friend WithEvents btnSALIR As System.Windows.Forms.Button
    Friend WithEvents LblUbicacion As System.Windows.Forms.Label
End Class
