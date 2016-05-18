<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmBuscarRemito
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
        Me.dtgRemito = New System.Windows.Forms.DataGrid
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.btneliminar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'dtgRemito
        '
        Me.dtgRemito.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dtgRemito.Location = New System.Drawing.Point(19, 3)
        Me.dtgRemito.Name = "dtgRemito"
        Me.dtgRemito.Size = New System.Drawing.Size(183, 217)
        Me.dtgRemito.TabIndex = 6
        '
        'btnCerrar
        '
        Me.btnCerrar.Location = New System.Drawing.Point(130, 253)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(72, 20)
        Me.btnCerrar.TabIndex = 7
        Me.btnCerrar.Text = "F2) Cerrar"
        '
        'btneliminar
        '
        Me.btneliminar.Enabled = False
        Me.btneliminar.Location = New System.Drawing.Point(19, 253)
        Me.btneliminar.Name = "btneliminar"
        Me.btneliminar.Size = New System.Drawing.Size(101, 20)
        Me.btneliminar.TabIndex = 7
        Me.btneliminar.Text = "F1) Eliminar"
        '
        'FrmBuscarRemito
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btneliminar)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.dtgRemito)
        Me.KeyPreview = True
        Me.Name = "FrmBuscarRemito"
        Me.Text = "Buscar Remito"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtgRemito As System.Windows.Forms.DataGrid
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btneliminar As System.Windows.Forms.Button
End Class
