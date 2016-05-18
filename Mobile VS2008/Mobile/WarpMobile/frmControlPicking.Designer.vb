<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmControlPicking
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
        Me.lblPallet = New System.Windows.Forms.Label
        Me.txtPalletPicking = New System.Windows.Forms.TextBox
        Me.dgPicking = New System.Windows.Forms.DataGrid
        Me.lblMsg = New System.Windows.Forms.Label
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.LblCantBultos = New System.Windows.Forms.Label
        Me.txtCantBultos = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'lblPallet
        '
        Me.lblPallet.Location = New System.Drawing.Point(5, 5)
        Me.lblPallet.Name = "lblPallet"
        Me.lblPallet.Size = New System.Drawing.Size(89, 22)
        Me.lblPallet.Text = "Pallet Picking:"
        '
        'txtPalletPicking
        '
        Me.txtPalletPicking.Location = New System.Drawing.Point(98, 4)
        Me.txtPalletPicking.MaxLength = 18
        Me.txtPalletPicking.Name = "txtPalletPicking"
        Me.txtPalletPicking.Size = New System.Drawing.Size(139, 21)
        Me.txtPalletPicking.TabIndex = 1
        '
        'dgPicking
        '
        Me.dgPicking.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgPicking.Location = New System.Drawing.Point(0, 57)
        Me.dgPicking.Name = "dgPicking"
        Me.dgPicking.RowHeadersVisible = False
        Me.dgPicking.Size = New System.Drawing.Size(240, 166)
        Me.dgPicking.TabIndex = 2
        '
        'lblMsg
        '
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(5, 270)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(235, 24)
        Me.lblMsg.Text = "msg"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.BackColor = System.Drawing.Color.White
        Me.cmdCancelar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCancelar.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.cmdCancelar.Location = New System.Drawing.Point(132, 229)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(104, 16)
        Me.cmdCancelar.TabIndex = 4
        Me.cmdCancelar.Text = "F2) Cancelar"
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.White
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdSalir.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.cmdSalir.Location = New System.Drawing.Point(5, 251)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(113, 16)
        Me.cmdSalir.TabIndex = 5
        Me.cmdSalir.Text = "F3) Salir"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Button1.Location = New System.Drawing.Point(5, 229)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(113, 16)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "F1) Buscar"
        '
        'LblCantBultos
        '
        Me.LblCantBultos.Location = New System.Drawing.Point(5, 29)
        Me.LblCantBultos.Name = "LblCantBultos"
        Me.LblCantBultos.Size = New System.Drawing.Size(89, 16)
        Me.LblCantBultos.Text = "Cant. Bultos:"
        Me.LblCantBultos.Visible = False
        '
        'txtCantBultos
        '
        Me.txtCantBultos.Location = New System.Drawing.Point(132, 26)
        Me.txtCantBultos.MaxLength = 10
        Me.txtCantBultos.Name = "txtCantBultos"
        Me.txtCantBultos.Size = New System.Drawing.Size(104, 21)
        Me.txtCantBultos.TabIndex = 11
        Me.txtCantBultos.Visible = False
        '
        'frmControlPicking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtCantBultos)
        Me.Controls.Add(Me.LblCantBultos)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.dgPicking)
        Me.Controls.Add(Me.txtPalletPicking)
        Me.Controls.Add(Me.lblPallet)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frmControlPicking"
        Me.Text = "Control Picking"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblPallet As System.Windows.Forms.Label
    Friend WithEvents txtPalletPicking As System.Windows.Forms.TextBox
    Friend WithEvents dgPicking As System.Windows.Forms.DataGrid
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents LblCantBultos As System.Windows.Forms.Label
    Friend WithEvents txtCantBultos As System.Windows.Forms.TextBox
End Class
