<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmFinalizados
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
        Me.dgFinalizados = New System.Windows.Forms.DataGrid
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'dgFinalizados
        '
        Me.dgFinalizados.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgFinalizados.HeaderBackColor = System.Drawing.Color.White
        Me.dgFinalizados.Location = New System.Drawing.Point(0, 10)
        Me.dgFinalizados.Name = "dgFinalizados"
        Me.dgFinalizados.RowHeadersVisible = False
        Me.dgFinalizados.Size = New System.Drawing.Size(240, 227)
        Me.dgFinalizados.TabIndex = 0
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.White
        Me.cmdSalir.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.cmdSalir.Location = New System.Drawing.Point(5, 249)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(232, 19)
        Me.cmdSalir.TabIndex = 1
        Me.cmdSalir.Text = "F1) Salir"
        '
        'frmFinalizados
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.dgFinalizados)
        Me.KeyPreview = True
        Me.Name = "frmFinalizados"
        Me.Text = "frmFinalizados"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgFinalizados As System.Windows.Forms.DataGrid
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
End Class
