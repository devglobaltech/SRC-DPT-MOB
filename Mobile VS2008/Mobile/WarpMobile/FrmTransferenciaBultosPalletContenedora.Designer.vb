<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmTransferenciaBultosPalletContenedora
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
        Me.lblOrigen = New System.Windows.Forms.Label
        Me.lblCompañia = New System.Windows.Forms.Label
        Me.lblProducto = New System.Windows.Forms.Label
        Me.lblDestino = New System.Windows.Forms.Label
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.lblBultoPallet = New System.Windows.Forms.Label
        Me.txtBultoPallet = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'lblOrigen
        '
        Me.lblOrigen.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblOrigen.Location = New System.Drawing.Point(0, 3)
        Me.lblOrigen.Name = "lblOrigen"
        Me.lblOrigen.Size = New System.Drawing.Size(240, 20)
        Me.lblOrigen.Text = "Origen: "
        '
        'lblCompañia
        '
        Me.lblCompañia.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblCompañia.Location = New System.Drawing.Point(0, 47)
        Me.lblCompañia.Name = "lblCompañia"
        Me.lblCompañia.Size = New System.Drawing.Size(240, 20)
        Me.lblCompañia.Text = "Compañia:"
        '
        'lblProducto
        '
        Me.lblProducto.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblProducto.Location = New System.Drawing.Point(0, 69)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(240, 20)
        Me.lblProducto.Text = "Producto"
        '
        'lblDestino
        '
        Me.lblDestino.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblDestino.Location = New System.Drawing.Point(0, 25)
        Me.lblDestino.Name = "lblDestino"
        Me.lblDestino.Size = New System.Drawing.Size(240, 20)
        Me.lblDestino.Text = "Destino: "
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(0, 271)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(240, 20)
        Me.btnCancelar.TabIndex = 5
        Me.btnCancelar.Text = "F 1 )  C a n c e l a r"
        '
        'lblBultoPallet
        '
        Me.lblBultoPallet.Location = New System.Drawing.Point(0, 116)
        Me.lblBultoPallet.Name = "lblBultoPallet"
        Me.lblBultoPallet.Size = New System.Drawing.Size(240, 20)
        Me.lblBultoPallet.Text = "Nro. de Bulto / Pallet"
        '
        'txtBultoPallet
        '
        Me.txtBultoPallet.Location = New System.Drawing.Point(0, 138)
        Me.txtBultoPallet.Name = "txtBultoPallet"
        Me.txtBultoPallet.Size = New System.Drawing.Size(240, 21)
        Me.txtBultoPallet.TabIndex = 10
        '
        'FrmTransferenciaBultosPalletContenedora
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtBultoPallet)
        Me.Controls.Add(Me.lblBultoPallet)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.lblDestino)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.lblCompañia)
        Me.Controls.Add(Me.lblOrigen)
        Me.KeyPreview = True
        Me.Name = "FrmTransferenciaBultosPalletContenedora"
        Me.Text = "Transferencia Bultos"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblOrigen As System.Windows.Forms.Label
    Friend WithEvents lblCompañia As System.Windows.Forms.Label
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents lblDestino As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents lblBultoPallet As System.Windows.Forms.Label
    Friend WithEvents txtBultoPallet As System.Windows.Forms.TextBox
End Class
