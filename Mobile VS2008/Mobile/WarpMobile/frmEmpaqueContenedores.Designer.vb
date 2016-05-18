<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEmpaqueContenedores
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
        Me.lblContenedores = New System.Windows.Forms.Label
        Me.dgContenedoresGenerados = New System.Windows.Forms.DataGrid
        Me.btnSalir = New System.Windows.Forms.Button
        Me.btnCerrarContenedora = New System.Windows.Forms.Button
        Me.btnVerContenido = New System.Windows.Forms.Button
        Me.btnAbrirContenedora = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblContenedores
        '
        Me.lblContenedores.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblContenedores.Location = New System.Drawing.Point(0, 0)
        Me.lblContenedores.Name = "lblContenedores"
        Me.lblContenedores.Size = New System.Drawing.Size(240, 20)
        Me.lblContenedores.Text = "Contenedores Generados"
        '
        'dgContenedoresGenerados
        '
        Me.dgContenedoresGenerados.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgContenedoresGenerados.Location = New System.Drawing.Point(0, 22)
        Me.dgContenedoresGenerados.Name = "dgContenedoresGenerados"
        Me.dgContenedoresGenerados.Size = New System.Drawing.Size(240, 200)
        Me.dgContenedoresGenerados.TabIndex = 1
        '
        'btnSalir
        '
        Me.btnSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnSalir.Location = New System.Drawing.Point(0, 224)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(117, 18)
        Me.btnSalir.TabIndex = 16
        Me.btnSalir.Text = "F1) Salir"
        '
        'btnCerrarContenedora
        '
        Me.btnCerrarContenedora.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnCerrarContenedora.Location = New System.Drawing.Point(123, 224)
        Me.btnCerrarContenedora.Name = "btnCerrarContenedora"
        Me.btnCerrarContenedora.Size = New System.Drawing.Size(117, 18)
        Me.btnCerrarContenedora.TabIndex = 40
        Me.btnCerrarContenedora.Text = "F2) Cerrar Cont."
        '
        'btnVerContenido
        '
        Me.btnVerContenido.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnVerContenido.Location = New System.Drawing.Point(0, 244)
        Me.btnVerContenido.Name = "btnVerContenido"
        Me.btnVerContenido.Size = New System.Drawing.Size(117, 18)
        Me.btnVerContenido.TabIndex = 42
        Me.btnVerContenido.Text = "F3) Ver Contenido."
        '
        'btnAbrirContenedora
        '
        Me.btnAbrirContenedora.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnAbrirContenedora.Location = New System.Drawing.Point(123, 244)
        Me.btnAbrirContenedora.Name = "btnAbrirContenedora"
        Me.btnAbrirContenedora.Size = New System.Drawing.Size(117, 18)
        Me.btnAbrirContenedora.TabIndex = 44
        Me.btnAbrirContenedora.Text = "F4) Abrir Cont."
        '
        'frmEmpaqueContenedores
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnAbrirContenedora)
        Me.Controls.Add(Me.btnVerContenido)
        Me.Controls.Add(Me.btnCerrarContenedora)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.dgContenedoresGenerados)
        Me.Controls.Add(Me.lblContenedores)
        Me.KeyPreview = True
        Me.Name = "frmEmpaqueContenedores"
        Me.Text = "Empaque - Contenedores"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblContenedores As System.Windows.Forms.Label
    Friend WithEvents dgContenedoresGenerados As System.Windows.Forms.DataGrid
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents btnCerrarContenedora As System.Windows.Forms.Button
    Friend WithEvents btnVerContenido As System.Windows.Forms.Button
    Friend WithEvents btnAbrirContenedora As System.Windows.Forms.Button
End Class
