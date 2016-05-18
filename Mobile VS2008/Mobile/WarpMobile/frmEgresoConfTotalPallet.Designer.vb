<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEgresoConfTotalPallet
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
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.lblCodOla = New System.Windows.Forms.Label
        Me.txtOla = New System.Windows.Forms.TextBox
        Me.lblSKU = New System.Windows.Forms.Label
        Me.txtProducto = New System.Windows.Forms.TextBox
        Me.txtUbicacion = New System.Windows.Forms.TextBox
        Me.lblUbicacion = New System.Windows.Forms.Label
        Me.txtPallet = New System.Windows.Forms.TextBox
        Me.lblConfPallet = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblTitulo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.Location = New System.Drawing.Point(0, 2)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(240, 20)
        Me.lblTitulo.Text = "Confirmación de Series por Pallet"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblCodOla
        '
        Me.lblCodOla.Location = New System.Drawing.Point(0, 26)
        Me.lblCodOla.Name = "lblCodOla"
        Me.lblCodOla.Size = New System.Drawing.Size(119, 20)
        Me.lblCodOla.Text = "Cod. Ola de Picking:"
        '
        'txtOla
        '
        Me.txtOla.Enabled = False
        Me.txtOla.Location = New System.Drawing.Point(114, 26)
        Me.txtOla.Name = "txtOla"
        Me.txtOla.Size = New System.Drawing.Size(126, 21)
        Me.txtOla.TabIndex = 2
        '
        'lblSKU
        '
        Me.lblSKU.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblSKU.Location = New System.Drawing.Point(0, 50)
        Me.lblSKU.Name = "lblSKU"
        Me.lblSKU.Size = New System.Drawing.Size(106, 20)
        Me.lblSKU.Text = "Cod. Producto"
        '
        'txtProducto
        '
        Me.txtProducto.Enabled = False
        Me.txtProducto.Location = New System.Drawing.Point(114, 49)
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(126, 21)
        Me.txtProducto.TabIndex = 5
        '
        'txtUbicacion
        '
        Me.txtUbicacion.Enabled = False
        Me.txtUbicacion.Location = New System.Drawing.Point(0, 106)
        Me.txtUbicacion.Name = "txtUbicacion"
        Me.txtUbicacion.Size = New System.Drawing.Size(240, 21)
        Me.txtUbicacion.TabIndex = 7
        '
        'lblUbicacion
        '
        Me.lblUbicacion.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblUbicacion.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblUbicacion.Location = New System.Drawing.Point(0, 83)
        Me.lblUbicacion.Name = "lblUbicacion"
        Me.lblUbicacion.Size = New System.Drawing.Size(240, 20)
        Me.lblUbicacion.Text = "Ubicación del Pallet"
        Me.lblUbicacion.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtPallet
        '
        Me.txtPallet.Enabled = False
        Me.txtPallet.Location = New System.Drawing.Point(0, 166)
        Me.txtPallet.Name = "txtPallet"
        Me.txtPallet.Size = New System.Drawing.Size(240, 21)
        Me.txtPallet.TabIndex = 10
        '
        'lblConfPallet
        '
        Me.lblConfPallet.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblConfPallet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblConfPallet.Location = New System.Drawing.Point(0, 143)
        Me.lblConfPallet.Name = "lblConfPallet"
        Me.lblConfPallet.Size = New System.Drawing.Size(240, 20)
        Me.lblConfPallet.Text = "Confirme el pallet Nro: "
        Me.lblConfPallet.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmEgresoConfTotalPallet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtPallet)
        Me.Controls.Add(Me.lblConfPallet)
        Me.Controls.Add(Me.txtUbicacion)
        Me.Controls.Add(Me.lblUbicacion)
        Me.Controls.Add(Me.txtProducto)
        Me.Controls.Add(Me.lblSKU)
        Me.Controls.Add(Me.txtOla)
        Me.Controls.Add(Me.lblCodOla)
        Me.Controls.Add(Me.lblTitulo)
        Me.Name = "frmEgresoConfTotalPallet"
        Me.Text = "Confirmación por Pallet"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lblCodOla As System.Windows.Forms.Label
    Friend WithEvents txtOla As System.Windows.Forms.TextBox
    Friend WithEvents lblSKU As System.Windows.Forms.Label
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents txtUbicacion As System.Windows.Forms.TextBox
    Friend WithEvents lblUbicacion As System.Windows.Forms.Label
    Friend WithEvents txtPallet As System.Windows.Forms.TextBox
    Friend WithEvents lblConfPallet As System.Windows.Forms.Label
End Class
