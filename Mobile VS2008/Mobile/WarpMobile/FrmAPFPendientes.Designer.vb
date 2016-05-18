<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmAPFPendientes
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
        Me.DgPendientes = New System.Windows.Forms.DataGrid
        Me.lblCliente = New System.Windows.Forms.Label
        Me.lblCodigoViaje = New System.Windows.Forms.Label
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.tmr = New System.Windows.Forms.Timer
        Me.chkActiva = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'DgPendientes
        '
        Me.DgPendientes.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.DgPendientes.Location = New System.Drawing.Point(4, 57)
        Me.DgPendientes.Name = "DgPendientes"
        Me.DgPendientes.SelectionBackColor = System.Drawing.Color.LightSalmon
        Me.DgPendientes.Size = New System.Drawing.Size(233, 174)
        Me.DgPendientes.TabIndex = 7
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCliente.Location = New System.Drawing.Point(4, 6)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(233, 16)
        Me.lblCliente.Text = "Cliente:"
        '
        'lblCodigoViaje
        '
        Me.lblCodigoViaje.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblCodigoViaje.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCodigoViaje.Location = New System.Drawing.Point(4, 31)
        Me.lblCodigoViaje.Name = "lblCodigoViaje"
        Me.lblCodigoViaje.Size = New System.Drawing.Size(233, 15)
        Me.lblCodigoViaje.Text = "Codigo de Viaje:"
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.LightGray
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmdSalir.Location = New System.Drawing.Point(125, 259)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(112, 15)
        Me.cmdSalir.TabIndex = 15
        Me.cmdSalir.Text = "F2) Salir"
        '
        'tmr
        '
        Me.tmr.Interval = 10000
        '
        'chkActiva
        '
        Me.chkActiva.Location = New System.Drawing.Point(7, 238)
        Me.chkActiva.Name = "chkActiva"
        Me.chkActiva.Size = New System.Drawing.Size(230, 15)
        Me.chkActiva.TabIndex = 18
        Me.chkActiva.Text = "Auto Refrescar (10 seg)"
        '
        'FrmAPFPendientes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkActiva)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.DgPendientes)
        Me.Controls.Add(Me.lblCliente)
        Me.Controls.Add(Me.lblCodigoViaje)
        Me.KeyPreview = True
        Me.Name = "FrmAPFPendientes"
        Me.Text = "Pendientes"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DgPendientes As System.Windows.Forms.DataGrid
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblCodigoViaje As System.Windows.Forms.Label
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents tmr As System.Windows.Forms.Timer
    Friend WithEvents chkActiva As System.Windows.Forms.CheckBox
End Class
