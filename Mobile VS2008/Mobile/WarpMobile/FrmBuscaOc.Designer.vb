<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmBuscaOc
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
        Me.dtgOc = New System.Windows.Forms.DataGrid
        Me.btnBorrar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnSeleccionar
        '
        Me.btnSeleccionar.Location = New System.Drawing.Point(124, 263)
        Me.btnSeleccionar.Name = "btnSeleccionar"
        Me.btnSeleccionar.Size = New System.Drawing.Size(103, 20)
        Me.btnSeleccionar.TabIndex = 9
        Me.btnSeleccionar.Text = "F1) Seleccionar"
        Me.btnSeleccionar.Visible = False
        '
        'btnCerrar
        '
        Me.btnCerrar.Location = New System.Drawing.Point(3, 237)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(74, 20)
        Me.btnCerrar.TabIndex = 10
        Me.btnCerrar.Text = "F1) Cerrar"
        '
        'dtgOc
        '
        Me.dtgOc.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dtgOc.Location = New System.Drawing.Point(0, 3)
        Me.dtgOc.Name = "dtgOc"
        Me.dtgOc.Size = New System.Drawing.Size(237, 217)
        Me.dtgOc.TabIndex = 8
        '
        'btnBorrar
        '
        Me.btnBorrar.Enabled = False
        Me.btnBorrar.Location = New System.Drawing.Point(134, 237)
        Me.btnBorrar.Name = "btnBorrar"
        Me.btnBorrar.Size = New System.Drawing.Size(83, 20)
        Me.btnBorrar.TabIndex = 11
        Me.btnBorrar.Text = "F2) Borrar"
        '
        'FrmBuscaOc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.btnBorrar)
        Me.Controls.Add(Me.btnSeleccionar)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.dtgOc)
        Me.KeyPreview = True
        Me.Name = "FrmBuscaOc"
        Me.Text = "FrmBuscaOc"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSeleccionar As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents dtgOc As System.Windows.Forms.DataGrid
    Friend WithEvents btnBorrar As System.Windows.Forms.Button
End Class
