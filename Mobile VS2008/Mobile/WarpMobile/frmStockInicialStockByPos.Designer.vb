<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmStockInicialStockByPos
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
        Me.dgStock = New System.Windows.Forms.DataGrid
        Me.btnVolver = New System.Windows.Forms.Button
        Me.btnBorrar = New System.Windows.Forms.Button
        Me.lblUbicacion = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.BackColor = System.Drawing.SystemColors.Control
        Me.lblTitulo.Location = New System.Drawing.Point(0, 3)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(240, 20)
        Me.lblTitulo.Text = "Consulta de Stock por Posicion"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'dgStock
        '
        Me.dgStock.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgStock.Location = New System.Drawing.Point(0, 48)
        Me.dgStock.Name = "dgStock"
        Me.dgStock.Size = New System.Drawing.Size(240, 196)
        Me.dgStock.TabIndex = 1
        '
        'btnVolver
        '
        Me.btnVolver.Location = New System.Drawing.Point(0, 271)
        Me.btnVolver.Name = "btnVolver"
        Me.btnVolver.Size = New System.Drawing.Size(240, 20)
        Me.btnVolver.TabIndex = 2
        Me.btnVolver.Text = "F2) Volver"
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(0, 249)
        Me.btnBorrar.Name = "btnBorrar"
        Me.btnBorrar.Size = New System.Drawing.Size(240, 20)
        Me.btnBorrar.TabIndex = 3
        Me.btnBorrar.Text = "F1) Borrar Registro"
        '
        'lblUbicacion
        '
        Me.lblUbicacion.Location = New System.Drawing.Point(0, 25)
        Me.lblUbicacion.Name = "lblUbicacion"
        Me.lblUbicacion.Size = New System.Drawing.Size(240, 20)
        Me.lblUbicacion.Text = "Ubicacion: "
        '
        'frmStockInicialStockByPos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblUbicacion)
        Me.Controls.Add(Me.btnBorrar)
        Me.Controls.Add(Me.btnVolver)
        Me.Controls.Add(Me.dgStock)
        Me.Controls.Add(Me.lblTitulo)
        Me.KeyPreview = True
        Me.Name = "frmStockInicialStockByPos"
        Me.Text = "Conteo por Ubicación"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents dgStock As System.Windows.Forms.DataGrid
    Friend WithEvents btnVolver As System.Windows.Forms.Button
    Friend WithEvents btnBorrar As System.Windows.Forms.Button
    Friend WithEvents lblUbicacion As System.Windows.Forms.Label
End Class
