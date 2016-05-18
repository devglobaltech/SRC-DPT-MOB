<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEmpaqueContenido
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
        Me.dgContenido = New System.Windows.Forms.DataGrid
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.lblContenedor = New System.Windows.Forms.Label
        Me.btnSalir = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'dgContenido
        '
        Me.dgContenido.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgContenido.Location = New System.Drawing.Point(0, 51)
        Me.dgContenido.Name = "dgContenido"
        Me.dgContenido.Size = New System.Drawing.Size(240, 169)
        Me.dgContenido.TabIndex = 0
        '
        'lblTitulo
        '
        Me.lblTitulo.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblTitulo.Location = New System.Drawing.Point(0, 4)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(240, 20)
        Me.lblTitulo.Text = "Visor de Contenido"
        '
        'lblContenedor
        '
        Me.lblContenedor.Location = New System.Drawing.Point(0, 27)
        Me.lblContenedor.Name = "lblContenedor"
        Me.lblContenedor.Size = New System.Drawing.Size(237, 20)
        Me.lblContenedor.Text = "Cont. Empaque: "
        '
        'btnSalir
        '
        Me.btnSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnSalir.Location = New System.Drawing.Point(0, 253)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(117, 18)
        Me.btnSalir.TabIndex = 17
        Me.btnSalir.Text = "F1) Salir"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.Location = New System.Drawing.Point(123, 253)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(117, 18)
        Me.Button1.TabIndex = 20
        Me.Button1.Text = "F2) Quitar"
        '
        'frmEmpaqueContenido
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.lblContenedor)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.dgContenido)
        Me.KeyPreview = True
        Me.Name = "frmEmpaqueContenido"
        Me.Text = "Detalle Contenido"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgContenido As System.Windows.Forms.DataGrid
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lblContenedor As System.Windows.Forms.Label
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
