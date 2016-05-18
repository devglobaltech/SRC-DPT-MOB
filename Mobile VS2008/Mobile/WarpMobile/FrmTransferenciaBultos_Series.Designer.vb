<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmTransferenciaBultos_Series
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
        Me.lblOrigen = New System.Windows.Forms.Label
        Me.lblCliente = New System.Windows.Forms.Label
        Me.lblProducto = New System.Windows.Forms.Label
        Me.lblNroSerie = New System.Windows.Forms.Label
        Me.txtNroSerie = New System.Windows.Forms.TextBox
        Me.DG = New System.Windows.Forms.DataGrid
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblTitulo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblTitulo.Location = New System.Drawing.Point(0, 3)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(240, 23)
        Me.lblTitulo.Text = "Transferencia de Series"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblOrigen
        '
        Me.lblOrigen.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblOrigen.Location = New System.Drawing.Point(0, 28)
        Me.lblOrigen.Name = "lblOrigen"
        Me.lblOrigen.Size = New System.Drawing.Size(240, 20)
        Me.lblOrigen.Text = "Origen:"
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblCliente.Location = New System.Drawing.Point(0, 50)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(240, 20)
        Me.lblCliente.Text = "Cod. Cliente:"
        '
        'lblProducto
        '
        Me.lblProducto.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblProducto.Location = New System.Drawing.Point(0, 72)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(240, 20)
        Me.lblProducto.Text = "Cod. Producto"
        '
        'lblNroSerie
        '
        Me.lblNroSerie.Location = New System.Drawing.Point(0, 95)
        Me.lblNroSerie.Name = "lblNroSerie"
        Me.lblNroSerie.Size = New System.Drawing.Size(108, 20)
        Me.lblNroSerie.Text = "Ing. Nro. de Serie:"
        '
        'txtNroSerie
        '
        Me.txtNroSerie.Location = New System.Drawing.Point(114, 95)
        Me.txtNroSerie.Name = "txtNroSerie"
        Me.txtNroSerie.Size = New System.Drawing.Size(126, 21)
        Me.txtNroSerie.TabIndex = 7
        '
        'DG
        '
        Me.DG.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.DG.Location = New System.Drawing.Point(0, 143)
        Me.DG.Name = "DG"
        Me.DG.Size = New System.Drawing.Size(240, 121)
        Me.DG.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Label1.Location = New System.Drawing.Point(0, 121)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(240, 20)
        Me.Label1.Text = "Series Leidas"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(0, 270)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(119, 20)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "F1) Finalizar"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(121, 270)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(119, 20)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "F2) Cancelar"
        '
        'FrmTransferenciaBultos_Series
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DG)
        Me.Controls.Add(Me.txtNroSerie)
        Me.Controls.Add(Me.lblNroSerie)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.lblCliente)
        Me.Controls.Add(Me.lblOrigen)
        Me.Controls.Add(Me.lblTitulo)
        Me.KeyPreview = True
        Me.Name = "FrmTransferenciaBultos_Series"
        Me.Text = "Transf. de Series"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lblOrigen As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents lblNroSerie As System.Windows.Forms.Label
    Friend WithEvents txtNroSerie As System.Windows.Forms.TextBox
    Friend WithEvents DG As System.Windows.Forms.DataGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
