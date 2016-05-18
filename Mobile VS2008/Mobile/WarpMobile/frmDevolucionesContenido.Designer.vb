<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmDevolucionesContenido
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
        Me.dgResult = New System.Windows.Forms.DataGrid
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.btnVolver = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'dgResult
        '
        Me.dgResult.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgResult.Location = New System.Drawing.Point(0, 24)
        Me.dgResult.Name = "dgResult"
        Me.dgResult.Size = New System.Drawing.Size(240, 241)
        Me.dgResult.TabIndex = 0
        '
        'lblTitulo
        '
        Me.lblTitulo.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblTitulo.Location = New System.Drawing.Point(0, 4)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(239, 17)
        Me.lblTitulo.Text = "Contenido Pallet: "
        '
        'btnVolver
        '
        Me.btnVolver.Location = New System.Drawing.Point(0, 271)
        Me.btnVolver.Name = "btnVolver"
        Me.btnVolver.Size = New System.Drawing.Size(240, 20)
        Me.btnVolver.TabIndex = 2
        Me.btnVolver.Text = "F1) Volver"
        '
        'frmDevolucionesContenido
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnVolver)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.dgResult)
        Me.KeyPreview = True
        Me.Name = "frmDevolucionesContenido"
        Me.Text = "Contenido Pallet"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgResult As System.Windows.Forms.DataGrid
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents btnVolver As System.Windows.Forms.Button
End Class
