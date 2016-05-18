<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmTansfPickingCompletadas
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
        Me.lblCodigoViaje = New System.Windows.Forms.Label
        Me.chkActiva = New System.Windows.Forms.CheckBox
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.tmr = New System.Windows.Forms.Timer
        Me.DgTranfCompletadas = New System.Windows.Forms.DataGrid
        Me.SuspendLayout()
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCliente.Location = New System.Drawing.Point(3, 11)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(233, 16)
        Me.lblCliente.Text = "Cliente:"
        '
        'lblCodigoViaje
        '
        Me.lblCodigoViaje.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblCodigoViaje.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCodigoViaje.Location = New System.Drawing.Point(3, 36)
        Me.lblCodigoViaje.Name = "lblCodigoViaje"
        Me.lblCodigoViaje.Size = New System.Drawing.Size(233, 15)
        Me.lblCodigoViaje.Text = "Codigo de Viaje:"
        '
        'chkActiva
        '
        Me.chkActiva.Location = New System.Drawing.Point(3, 248)
        Me.chkActiva.Name = "chkActiva"
        Me.chkActiva.Size = New System.Drawing.Size(230, 15)
        Me.chkActiva.TabIndex = 20
        Me.chkActiva.Text = "Auto Refrescar (10 seg)"
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.LightGray
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmdSalir.Location = New System.Drawing.Point(124, 269)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(112, 15)
        Me.cmdSalir.TabIndex = 19
        Me.cmdSalir.Text = "F2) Salir"
        '
        'tmr
        '
        Me.tmr.Interval = 10000
        '
        'DgTranfCompletadas
        '
        Me.DgTranfCompletadas.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.DgTranfCompletadas.Location = New System.Drawing.Point(3, 54)
        Me.DgTranfCompletadas.Name = "DgTranfCompletadas"
        Me.DgTranfCompletadas.Size = New System.Drawing.Size(233, 188)
        Me.DgTranfCompletadas.TabIndex = 23
        '
        'frmTansfPickingCompletadas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.DgTranfCompletadas)
        Me.Controls.Add(Me.chkActiva)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.lblCliente)
        Me.Controls.Add(Me.lblCodigoViaje)
        Me.KeyPreview = True
        Me.Name = "frmTansfPickingCompletadas"
        Me.Text = "Transferencias Completadas"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblCodigoViaje As System.Windows.Forms.Label
    Friend WithEvents chkActiva As System.Windows.Forms.CheckBox
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents tmr As System.Windows.Forms.Timer
    Friend WithEvents DgTranfCompletadas As System.Windows.Forms.DataGrid
End Class
