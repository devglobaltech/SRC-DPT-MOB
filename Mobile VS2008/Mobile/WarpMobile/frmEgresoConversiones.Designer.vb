<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEgresoConversiones
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.dgConversiones = New System.Windows.Forms.DataGrid
        Me.btnVolver = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label1.Location = New System.Drawing.Point(0, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(240, 20)
        Me.Label1.Text = "Conversiones"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'dgConversiones
        '
        Me.dgConversiones.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgConversiones.Location = New System.Drawing.Point(0, 26)
        Me.dgConversiones.Name = "dgConversiones"
        Me.dgConversiones.Size = New System.Drawing.Size(240, 219)
        Me.dgConversiones.TabIndex = 1
        '
        'btnVolver
        '
        Me.btnVolver.Location = New System.Drawing.Point(0, 271)
        Me.btnVolver.Name = "btnVolver"
        Me.btnVolver.Size = New System.Drawing.Size(240, 20)
        Me.btnVolver.TabIndex = 2
        Me.btnVolver.Text = "F1) Volver"
        '
        'frmEgresoConversiones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnVolver)
        Me.Controls.Add(Me.dgConversiones)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "frmEgresoConversiones"
        Me.Text = "Conversion"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgConversiones As System.Windows.Forms.DataGrid
    Friend WithEvents btnVolver As System.Windows.Forms.Button
End Class
