<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmTransferenciaAutomatica
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
        Me.txtPallet = New System.Windows.Forms.TextBox
        Me.lblMsg = New System.Windows.Forms.Label
        Me.dgProductos = New System.Windows.Forms.DataGrid
        Me.CmdTrans = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.CmdSalir = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblPallet
        '
        Me.lblPallet.Location = New System.Drawing.Point(3, 4)
        Me.lblPallet.Name = "lblPallet"
        Me.lblPallet.Size = New System.Drawing.Size(50, 21)
        Me.lblPallet.Text = "Pallet:"
        '
        'txtPallet
        '
        Me.txtPallet.Location = New System.Drawing.Point(59, 4)
        Me.txtPallet.MaxLength = 10
        Me.txtPallet.Name = "txtPallet"
        Me.txtPallet.Size = New System.Drawing.Size(162, 21)
        Me.txtPallet.TabIndex = 20
        '
        'lblMsg
        '
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(3, 256)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(234, 38)
        Me.lblMsg.Text = "lblMsg"
        '
        'dgProductos
        '
        Me.dgProductos.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgProductos.Location = New System.Drawing.Point(0, 31)
        Me.dgProductos.Name = "dgProductos"
        Me.dgProductos.RowHeadersVisible = False
        Me.dgProductos.Size = New System.Drawing.Size(240, 181)
        Me.dgProductos.TabIndex = 24
        '
        'CmdTrans
        '
        Me.CmdTrans.BackColor = System.Drawing.Color.White
        Me.CmdTrans.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.CmdTrans.Location = New System.Drawing.Point(6, 217)
        Me.CmdTrans.Name = "CmdTrans"
        Me.CmdTrans.Size = New System.Drawing.Size(111, 15)
        Me.CmdTrans.TabIndex = 27
        Me.CmdTrans.Text = "F1) Transferir"
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.White
        Me.cmdCancel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCancel.Location = New System.Drawing.Point(123, 217)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(111, 15)
        Me.cmdCancel.TabIndex = 28
        Me.cmdCancel.Text = "F2) Cancelar"
        '
        'CmdSalir
        '
        Me.CmdSalir.BackColor = System.Drawing.Color.White
        Me.CmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.CmdSalir.Location = New System.Drawing.Point(6, 236)
        Me.CmdSalir.Name = "CmdSalir"
        Me.CmdSalir.Size = New System.Drawing.Size(111, 15)
        Me.CmdSalir.TabIndex = 29
        Me.CmdSalir.Text = "F3) Salir"
        '
        'frmTransferenciaAutomatica
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.CmdSalir)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.CmdTrans)
        Me.Controls.Add(Me.dgProductos)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.lblPallet)
        Me.Controls.Add(Me.txtPallet)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frmTransferenciaAutomatica"
        Me.Text = "Procesar Devolucion"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblPallet As System.Windows.Forms.Label
    Friend WithEvents txtPallet As System.Windows.Forms.TextBox
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents dgProductos As System.Windows.Forms.DataGrid
    Friend WithEvents CmdTrans As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents CmdSalir As System.Windows.Forms.Button
End Class
