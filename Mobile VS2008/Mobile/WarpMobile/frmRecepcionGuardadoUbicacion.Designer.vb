<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmRecepcionGuardadoUbicacion
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
        Me.txtDestino = New System.Windows.Forms.TextBox
        Me.lblDestino = New System.Windows.Forms.Label
        Me.lblPallet = New System.Windows.Forms.Label
        Me.lblOc = New System.Windows.Forms.Label
        Me.lblCliente = New System.Windows.Forms.Label
        Me.lblError = New System.Windows.Forms.Label
        Me.BtnContinuar = New System.Windows.Forms.Button
        Me.CmdCancelar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'txtDestino
        '
        Me.txtDestino.Location = New System.Drawing.Point(0, 108)
        Me.txtDestino.MaxLength = 45
        Me.txtDestino.Name = "txtDestino"
        Me.txtDestino.Size = New System.Drawing.Size(240, 21)
        Me.txtDestino.TabIndex = 16
        '
        'lblDestino
        '
        Me.lblDestino.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDestino.Location = New System.Drawing.Point(0, 85)
        Me.lblDestino.Name = "lblDestino"
        Me.lblDestino.Size = New System.Drawing.Size(240, 20)
        Me.lblDestino.Text = "Ubicación: "
        '
        'lblPallet
        '
        Me.lblPallet.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblPallet.Location = New System.Drawing.Point(0, 2)
        Me.lblPallet.Name = "lblPallet"
        Me.lblPallet.Size = New System.Drawing.Size(240, 22)
        Me.lblPallet.Text = "Numero de Pallet:"
        '
        'lblOc
        '
        Me.lblOc.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblOc.Location = New System.Drawing.Point(0, 52)
        Me.lblOc.Name = "lblOc"
        Me.lblOc.Size = New System.Drawing.Size(240, 22)
        Me.lblOc.Text = "Orden de Compra:"
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblCliente.Location = New System.Drawing.Point(0, 27)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(240, 22)
        Me.lblCliente.Text = "Cliente:"
        '
        'lblError
        '
        Me.lblError.ForeColor = System.Drawing.Color.Red
        Me.lblError.Location = New System.Drawing.Point(0, 143)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(240, 36)
        '
        'BtnContinuar
        '
        Me.BtnContinuar.Location = New System.Drawing.Point(0, 202)
        Me.BtnContinuar.Name = "BtnContinuar"
        Me.BtnContinuar.Size = New System.Drawing.Size(240, 20)
        Me.BtnContinuar.TabIndex = 18
        Me.BtnContinuar.Text = "F1) Continuar"
        '
        'CmdCancelar
        '
        Me.CmdCancelar.Location = New System.Drawing.Point(0, 225)
        Me.CmdCancelar.Name = "CmdCancelar"
        Me.CmdCancelar.Size = New System.Drawing.Size(240, 20)
        Me.CmdCancelar.TabIndex = 19
        Me.CmdCancelar.Text = "F2) Cancelar Guardado"
        '
        'frmRecepcionGuardadoUbicacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.CmdCancelar)
        Me.Controls.Add(Me.BtnContinuar)
        Me.Controls.Add(Me.lblError)
        Me.Controls.Add(Me.lblCliente)
        Me.Controls.Add(Me.lblOc)
        Me.Controls.Add(Me.lblPallet)
        Me.Controls.Add(Me.txtDestino)
        Me.Controls.Add(Me.lblDestino)
        Me.KeyPreview = True
        Me.Name = "frmRecepcionGuardadoUbicacion"
        Me.Text = "Guardado"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtDestino As System.Windows.Forms.TextBox
    Friend WithEvents lblDestino As System.Windows.Forms.Label
    Friend WithEvents lblPallet As System.Windows.Forms.Label
    Friend WithEvents lblOc As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents BtnContinuar As System.Windows.Forms.Button
    Friend WithEvents CmdCancelar As System.Windows.Forms.Button
End Class
