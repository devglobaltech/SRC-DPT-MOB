<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmGManPendientes
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
        Me.lblIngreso = New System.Windows.Forms.Label
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.dgPendientes = New System.Windows.Forms.DataGrid
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblIngreso
        '
        Me.lblIngreso.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblIngreso.Location = New System.Drawing.Point(3, 5)
        Me.lblIngreso.Name = "lblIngreso"
        Me.lblIngreso.Size = New System.Drawing.Size(234, 19)
        Me.lblIngreso.Text = "Nro. de Ingreso: "
        '
        'lblTitulo
        '
        Me.lblTitulo.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.Location = New System.Drawing.Point(0, 30)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(240, 17)
        Me.lblTitulo.Text = "Pendientes de Ubicacion"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'dgPendientes
        '
        Me.dgPendientes.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgPendientes.Location = New System.Drawing.Point(0, 50)
        Me.dgPendientes.Name = "dgPendientes"
        Me.dgPendientes.Size = New System.Drawing.Size(240, 216)
        Me.dgPendientes.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(0, 272)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(86, 20)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "F1) Atras"
        '
        'frmGManPendientes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dgPendientes)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.lblIngreso)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frmGManPendientes"
        Me.Text = "Pendientes"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblIngreso As System.Windows.Forms.Label
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents dgPendientes As System.Windows.Forms.DataGrid
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
