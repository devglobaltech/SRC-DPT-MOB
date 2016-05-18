<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmStockInicialConf
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbClientes = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdTipoConteo = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbValidarSKU = New System.Windows.Forms.ComboBox
        Me.cmdContinuar = New System.Windows.Forms.Button
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblTitulo.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.Location = New System.Drawing.Point(0, 4)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(240, 36)
        Me.lblTitulo.Text = "Configuración para la toma de Stock inicial."
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 20)
        Me.Label1.Text = "Cliente:"
        '
        'cmbClientes
        '
        Me.cmbClientes.Location = New System.Drawing.Point(77, 58)
        Me.cmbClientes.Name = "cmbClientes"
        Me.cmbClientes.Size = New System.Drawing.Size(160, 22)
        Me.cmbClientes.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 20)
        Me.Label2.Text = "Tipo Conteo"
        '
        'cmdTipoConteo
        '
        Me.cmdTipoConteo.Items.Add("Codigo de Barras")
        Me.cmdTipoConteo.Items.Add("Conteo de Cantidades")
        Me.cmdTipoConteo.Location = New System.Drawing.Point(77, 97)
        Me.cmdTipoConteo.Name = "cmdTipoConteo"
        Me.cmdTipoConteo.Size = New System.Drawing.Size(160, 22)
        Me.cmdTipoConteo.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 138)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 20)
        Me.Label3.Text = "Validar SKU"
        '
        'cmbValidarSKU
        '
        Me.cmbValidarSKU.Items.Add("Si")
        Me.cmbValidarSKU.Items.Add("No")
        Me.cmbValidarSKU.Location = New System.Drawing.Point(77, 136)
        Me.cmbValidarSKU.Name = "cmbValidarSKU"
        Me.cmbValidarSKU.Size = New System.Drawing.Size(160, 22)
        Me.cmbValidarSKU.TabIndex = 8
        '
        'cmdContinuar
        '
        Me.cmdContinuar.Location = New System.Drawing.Point(0, 246)
        Me.cmdContinuar.Name = "cmdContinuar"
        Me.cmdContinuar.Size = New System.Drawing.Size(240, 20)
        Me.cmdContinuar.TabIndex = 9
        Me.cmdContinuar.Text = "Comenzar"
        '
        'cmdSalir
        '
        Me.cmdSalir.Location = New System.Drawing.Point(0, 270)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(240, 20)
        Me.cmdSalir.TabIndex = 10
        Me.cmdSalir.Text = "Salir"
        '
        'frmStockInicialConf
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdContinuar)
        Me.Controls.Add(Me.cmbValidarSKU)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmdTipoConteo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbClientes)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblTitulo)
        Me.Name = "frmStockInicialConf"
        Me.Text = "Configuración"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbClientes As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdTipoConteo As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbValidarSKU As System.Windows.Forms.ComboBox
    Friend WithEvents cmdContinuar As System.Windows.Forms.Button
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
End Class
