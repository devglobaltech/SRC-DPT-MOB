<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmBuscarProducto
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
        Me.btnSeleccionar = New System.Windows.Forms.Button
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.dtgPendientes = New System.Windows.Forms.DataGrid
        Me.dtgPendiente = New System.Windows.Forms.DataGrid
        Me.btnEliminar = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnSeleccionar
        '
        Me.btnSeleccionar.Location = New System.Drawing.Point(13, 265)
        Me.btnSeleccionar.Name = "btnSeleccionar"
        Me.btnSeleccionar.Size = New System.Drawing.Size(103, 20)
        Me.btnSeleccionar.TabIndex = 12
        Me.btnSeleccionar.Text = "F1) Seleccionar"
        '
        'btnCerrar
        '
        Me.btnCerrar.Location = New System.Drawing.Point(145, 265)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(74, 20)
        Me.btnCerrar.TabIndex = 13
        Me.btnCerrar.Text = "F2) Cerrar"
        '
        'dtgPendientes
        '
        Me.dtgPendientes.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dtgPendientes.Location = New System.Drawing.Point(3, 162)
        Me.dtgPendientes.Name = "dtgPendientes"
        Me.dtgPendientes.Size = New System.Drawing.Size(234, 92)
        Me.dtgPendientes.TabIndex = 11
        '
        'dtgPendiente
        '
        Me.dtgPendiente.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dtgPendiente.Location = New System.Drawing.Point(3, 16)
        Me.dtgPendiente.Name = "dtgPendiente"
        Me.dtgPendiente.Size = New System.Drawing.Size(234, 94)
        Me.dtgPendiente.TabIndex = 14
        '
        'btnEliminar
        '
        Me.btnEliminar.Enabled = False
        Me.btnEliminar.Location = New System.Drawing.Point(3, 116)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(86, 20)
        Me.btnEliminar.TabIndex = 15
        Me.btnEliminar.Text = "F3) Eliminar"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label1.Location = New System.Drawing.Point(3, 148)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(234, 14)
        Me.Label1.Text = "Pendientes"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label2.Location = New System.Drawing.Point(3, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(234, 14)
        Me.Label2.Text = "Ingresando"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'FrmBuscarProducto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.dtgPendiente)
        Me.Controls.Add(Me.btnSeleccionar)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.dtgPendientes)
        Me.KeyPreview = True
        Me.Name = "FrmBuscarProducto"
        Me.Text = "FrmBuscarProducto"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSeleccionar As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents dtgPendientes As System.Windows.Forms.DataGrid
    Friend WithEvents dtgPendiente As System.Windows.Forms.DataGrid
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
