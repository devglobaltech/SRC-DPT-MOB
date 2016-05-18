<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmGestionPalletViaje
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
        Me.dgPallet = New System.Windows.Forms.DataGrid
        Me.lblMenu = New System.Windows.Forms.Label
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'dgPallet
        '
        Me.dgPallet.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgPallet.Location = New System.Drawing.Point(3, 3)
        Me.dgPallet.Name = "dgPallet"
        Me.dgPallet.RowHeadersVisible = False
        Me.dgPallet.Size = New System.Drawing.Size(234, 243)
        Me.dgPallet.TabIndex = 1
        '
        'lblMenu
        '
        Me.lblMenu.ForeColor = System.Drawing.Color.Black
        Me.lblMenu.Location = New System.Drawing.Point(3, 260)
        Me.lblMenu.Name = "lblMenu"
        Me.lblMenu.Size = New System.Drawing.Size(17, 16)
        Me.lblMenu.Text = "vMenu"
        Me.lblMenu.Visible = False
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.White
        Me.cmdSalir.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.cmdSalir.Location = New System.Drawing.Point(26, 252)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(183, 15)
        Me.cmdSalir.TabIndex = 2
        Me.cmdSalir.Text = "F1) Volver."
        '
        'frmGestionPalletViaje
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 320)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.lblMenu)
        Me.Controls.Add(Me.dgPallet)
        Me.KeyPreview = True
        Me.Name = "frmGestionPalletViaje"
        Me.Text = "frmGestionPalletViaje"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgPallet As System.Windows.Forms.DataGrid
    Friend WithEvents lblMenu As System.Windows.Forms.Label
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
End Class
