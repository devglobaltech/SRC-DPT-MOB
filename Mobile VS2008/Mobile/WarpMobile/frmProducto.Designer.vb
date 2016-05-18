<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmProducto
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
        Me.dgProducto = New System.Windows.Forms.DataGrid
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'dgProducto
        '
        Me.dgProducto.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgProducto.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgProducto.Location = New System.Drawing.Point(0, 0)
        Me.dgProducto.Name = "dgProducto"
        Me.dgProducto.RowHeadersVisible = False
        Me.dgProducto.Size = New System.Drawing.Size(240, 254)
        Me.dgProducto.TabIndex = 3
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.White
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdSalir.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdSalir.Location = New System.Drawing.Point(160, 258)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(77, 20)
        Me.cmdSalir.TabIndex = 19
        Me.cmdSalir.Text = "Salir          F3"
        '
        'frmProducto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.dgProducto)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frmProducto"
        Me.Text = "frmProducto"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgProducto As System.Windows.Forms.DataGrid
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
End Class
