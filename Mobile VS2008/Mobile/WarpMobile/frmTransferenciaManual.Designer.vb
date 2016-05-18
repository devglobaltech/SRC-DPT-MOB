<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmTransferenciaManual
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
        Me.lblMenu = New System.Windows.Forms.Label
        Me.lblMsg = New System.Windows.Forms.Label
        Me.txtDestino = New System.Windows.Forms.TextBox
        Me.lblDestino = New System.Windows.Forms.Label
        Me.lblPallet = New System.Windows.Forms.Label
        Me.txtPallet = New System.Windows.Forms.TextBox
        Me.txtOrigen = New System.Windows.Forms.TextBox
        Me.lblOrigen = New System.Windows.Forms.Label
        Me.cmdStartTransf = New System.Windows.Forms.Button
        Me.cmdCancelTransf = New System.Windows.Forms.Button
        Me.cmdExitTransf = New System.Windows.Forms.Button
        Me.lblPosSug = New System.Windows.Forms.Label
        Me.lblPalletDestino = New System.Windows.Forms.Label
        Me.txtPalletDestino = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'lblMenu
        '
        Me.lblMenu.ForeColor = System.Drawing.Color.Black
        Me.lblMenu.Location = New System.Drawing.Point(0, 0)
        Me.lblMenu.Name = "lblMenu"
        Me.lblMenu.Size = New System.Drawing.Size(10, 18)
        Me.lblMenu.Text = "Menu"
        Me.lblMenu.Visible = False
        '
        'lblMsg
        '
        Me.lblMsg.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(3, 235)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(234, 49)
        Me.lblMsg.Text = "lblMsg"
        '
        'txtDestino
        '
        Me.txtDestino.Location = New System.Drawing.Point(3, 105)
        Me.txtDestino.MaxLength = 45
        Me.txtDestino.Name = "txtDestino"
        Me.txtDestino.Size = New System.Drawing.Size(234, 21)
        Me.txtDestino.TabIndex = 21
        '
        'lblDestino
        '
        Me.lblDestino.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDestino.Location = New System.Drawing.Point(3, 87)
        Me.lblDestino.Name = "lblDestino"
        Me.lblDestino.Size = New System.Drawing.Size(109, 15)
        Me.lblDestino.Text = "Ubicación destino: "
        '
        'lblPallet
        '
        Me.lblPallet.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblPallet.Location = New System.Drawing.Point(3, 45)
        Me.lblPallet.Name = "lblPallet"
        Me.lblPallet.Size = New System.Drawing.Size(234, 15)
        Me.lblPallet.Text = "Pallet:"
        '
        'txtPallet
        '
        Me.txtPallet.Location = New System.Drawing.Point(3, 63)
        Me.txtPallet.MaxLength = 100
        Me.txtPallet.Name = "txtPallet"
        Me.txtPallet.Size = New System.Drawing.Size(234, 21)
        Me.txtPallet.TabIndex = 20
        '
        'txtOrigen
        '
        Me.txtOrigen.Location = New System.Drawing.Point(3, 21)
        Me.txtOrigen.MaxLength = 45
        Me.txtOrigen.Name = "txtOrigen"
        Me.txtOrigen.Size = New System.Drawing.Size(234, 21)
        Me.txtOrigen.TabIndex = 25
        '
        'lblOrigen
        '
        Me.lblOrigen.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblOrigen.Location = New System.Drawing.Point(3, 3)
        Me.lblOrigen.Name = "lblOrigen"
        Me.lblOrigen.Size = New System.Drawing.Size(234, 15)
        Me.lblOrigen.Text = "Ubicación origen:"
        '
        'cmdStartTransf
        '
        Me.cmdStartTransf.BackColor = System.Drawing.Color.White
        Me.cmdStartTransf.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdStartTransf.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.cmdStartTransf.Location = New System.Drawing.Point(3, 176)
        Me.cmdStartTransf.Name = "cmdStartTransf"
        Me.cmdStartTransf.Size = New System.Drawing.Size(234, 17)
        Me.cmdStartTransf.TabIndex = 27
        Me.cmdStartTransf.Text = "F1) Comenzar Transferencia"
        '
        'cmdCancelTransf
        '
        Me.cmdCancelTransf.BackColor = System.Drawing.Color.White
        Me.cmdCancelTransf.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCancelTransf.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.cmdCancelTransf.Location = New System.Drawing.Point(3, 195)
        Me.cmdCancelTransf.Name = "cmdCancelTransf"
        Me.cmdCancelTransf.Size = New System.Drawing.Size(234, 17)
        Me.cmdCancelTransf.TabIndex = 28
        Me.cmdCancelTransf.Text = "F2) Cancelar Transferencia"
        '
        'cmdExitTransf
        '
        Me.cmdExitTransf.BackColor = System.Drawing.Color.White
        Me.cmdExitTransf.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdExitTransf.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.cmdExitTransf.Location = New System.Drawing.Point(3, 214)
        Me.cmdExitTransf.Name = "cmdExitTransf"
        Me.cmdExitTransf.Size = New System.Drawing.Size(234, 17)
        Me.cmdExitTransf.TabIndex = 29
        Me.cmdExitTransf.Text = "F3) Salir"
        '
        'lblPosSug
        '
        Me.lblPosSug.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblPosSug.Location = New System.Drawing.Point(109, 88)
        Me.lblPosSug.Name = "lblPosSug"
        Me.lblPosSug.Size = New System.Drawing.Size(128, 14)
        '
        'lblPalletDestino
        '
        Me.lblPalletDestino.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblPalletDestino.Location = New System.Drawing.Point(3, 129)
        Me.lblPalletDestino.Name = "lblPalletDestino"
        Me.lblPalletDestino.Size = New System.Drawing.Size(234, 15)
        Me.lblPalletDestino.Text = "Pallet destino:"
        '
        'txtPalletDestino
        '
        Me.txtPalletDestino.Location = New System.Drawing.Point(3, 146)
        Me.txtPalletDestino.MaxLength = 100
        Me.txtPalletDestino.Name = "txtPalletDestino"
        Me.txtPalletDestino.Size = New System.Drawing.Size(234, 21)
        Me.txtPalletDestino.TabIndex = 36
        '
        'frmTransferenciaManual
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblPalletDestino)
        Me.Controls.Add(Me.txtPalletDestino)
        Me.Controls.Add(Me.lblPosSug)
        Me.Controls.Add(Me.cmdExitTransf)
        Me.Controls.Add(Me.cmdCancelTransf)
        Me.Controls.Add(Me.cmdStartTransf)
        Me.Controls.Add(Me.txtOrigen)
        Me.Controls.Add(Me.lblOrigen)
        Me.Controls.Add(Me.lblMenu)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.txtDestino)
        Me.Controls.Add(Me.lblDestino)
        Me.Controls.Add(Me.lblPallet)
        Me.Controls.Add(Me.txtPallet)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frmTransferenciaManual"
        Me.Text = "Transferencia"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblMenu As System.Windows.Forms.Label
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents txtDestino As System.Windows.Forms.TextBox
    Friend WithEvents lblDestino As System.Windows.Forms.Label
    Friend WithEvents lblPallet As System.Windows.Forms.Label
    Friend WithEvents txtPallet As System.Windows.Forms.TextBox
    Friend WithEvents txtOrigen As System.Windows.Forms.TextBox
    Friend WithEvents lblOrigen As System.Windows.Forms.Label
    Friend WithEvents cmdStartTransf As System.Windows.Forms.Button
    Friend WithEvents cmdCancelTransf As System.Windows.Forms.Button
    Friend WithEvents cmdExitTransf As System.Windows.Forms.Button
    Friend WithEvents lblPosSug As System.Windows.Forms.Label
    Friend WithEvents lblPalletDestino As System.Windows.Forms.Label
    Friend WithEvents txtPalletDestino As System.Windows.Forms.TextBox
End Class
