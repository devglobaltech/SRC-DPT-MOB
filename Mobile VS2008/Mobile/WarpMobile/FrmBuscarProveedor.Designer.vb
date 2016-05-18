<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmBuscarProveedor
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
        Me.dtgProveedor = New System.Windows.Forms.DataGrid
        Me.btnSeleccionar = New System.Windows.Forms.Button
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'dtgProveedor
        '
        Me.dtgProveedor.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dtgProveedor.Location = New System.Drawing.Point(2, 3)
        Me.dtgProveedor.Name = "dtgProveedor"
        Me.dtgProveedor.Size = New System.Drawing.Size(235, 217)
        Me.dtgProveedor.TabIndex = 7
        '
        'btnSeleccionar
        '
        Me.btnSeleccionar.Location = New System.Drawing.Point(11, 238)
        Me.btnSeleccionar.Name = "btnSeleccionar"
        Me.btnSeleccionar.Size = New System.Drawing.Size(101, 20)
        Me.btnSeleccionar.TabIndex = 9
        Me.btnSeleccionar.Text = "F1) Seleccionar"
        '
        'btnCerrar
        '
        Me.btnCerrar.Location = New System.Drawing.Point(143, 238)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(72, 20)
        Me.btnCerrar.TabIndex = 8
        Me.btnCerrar.Text = "F2) Cerrar"
        '
        'FrmBuscarProveedor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnSeleccionar)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.dtgProveedor)
        Me.Name = "FrmBuscarProveedor"
        Me.Text = "Proveedores"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtgProveedor As System.Windows.Forms.DataGrid
    Friend WithEvents btnSeleccionar As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
End Class
