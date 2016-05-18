<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmIngresoViajes
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
        Me.lblMsg = New System.Windows.Forms.Label
        Me.lblMenu = New System.Windows.Forms.Label
        Me.txtNroPallet = New System.Windows.Forms.TextBox
        Me.lblNroPallet = New System.Windows.Forms.Label
        Me.lblNroViaje = New System.Windows.Forms.Label
        Me.txtNroViaje = New System.Windows.Forms.TextBox
        Me.cmdAgregarPallet = New System.Windows.Forms.Button
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.cmdPalletIng = New System.Windows.Forms.Button
        Me.cmdPalletRest = New System.Windows.Forms.Button
        Me.cmdFinalizar = New System.Windows.Forms.Button
        Me.cmdCambiar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblMsg
        '
        Me.lblMsg.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(3, 227)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(237, 51)
        Me.lblMsg.Text = "lblMsg"
        '
        'lblMenu
        '
        Me.lblMenu.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblMenu.ForeColor = System.Drawing.Color.Black
        Me.lblMenu.Location = New System.Drawing.Point(0, 0)
        Me.lblMenu.Name = "lblMenu"
        Me.lblMenu.Size = New System.Drawing.Size(30, 10)
        Me.lblMenu.Text = "vMenu"
        Me.lblMenu.Visible = False
        '
        'txtNroPallet
        '
        Me.txtNroPallet.Location = New System.Drawing.Point(13, 97)
        Me.txtNroPallet.MaxLength = 20
        Me.txtNroPallet.Name = "txtNroPallet"
        Me.txtNroPallet.Size = New System.Drawing.Size(214, 21)
        Me.txtNroPallet.TabIndex = 19
        '
        'lblNroPallet
        '
        Me.lblNroPallet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNroPallet.Location = New System.Drawing.Point(3, 74)
        Me.lblNroPallet.Name = "lblNroPallet"
        Me.lblNroPallet.Size = New System.Drawing.Size(226, 20)
        Me.lblNroPallet.Text = "Nro. Pallet:"
        '
        'lblNroViaje
        '
        Me.lblNroViaje.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNroViaje.Location = New System.Drawing.Point(3, 12)
        Me.lblNroViaje.Name = "lblNroViaje"
        Me.lblNroViaje.Size = New System.Drawing.Size(223, 21)
        Me.lblNroViaje.Text = "Nro. de viaje:"
        '
        'txtNroViaje
        '
        Me.txtNroViaje.Location = New System.Drawing.Point(13, 36)
        Me.txtNroViaje.MaxLength = 100
        Me.txtNroViaje.Name = "txtNroViaje"
        Me.txtNroViaje.Size = New System.Drawing.Size(214, 21)
        Me.txtNroViaje.TabIndex = 17
        '
        'cmdAgregarPallet
        '
        Me.cmdAgregarPallet.BackColor = System.Drawing.Color.White
        Me.cmdAgregarPallet.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdAgregarPallet.ForeColor = System.Drawing.Color.Navy
        Me.cmdAgregarPallet.Location = New System.Drawing.Point(3, 161)
        Me.cmdAgregarPallet.Name = "cmdAgregarPallet"
        Me.cmdAgregarPallet.Size = New System.Drawing.Size(110, 17)
        Me.cmdAgregarPallet.TabIndex = 23
        Me.cmdAgregarPallet.Text = "F1) Agregar Pallet"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.BackColor = System.Drawing.Color.White
        Me.cmdCancelar.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCancelar.ForeColor = System.Drawing.Color.Navy
        Me.cmdCancelar.Location = New System.Drawing.Point(117, 161)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(110, 17)
        Me.cmdCancelar.TabIndex = 24
        Me.cmdCancelar.Text = "F2) Cancelar"
        '
        'cmdPalletIng
        '
        Me.cmdPalletIng.BackColor = System.Drawing.Color.White
        Me.cmdPalletIng.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdPalletIng.ForeColor = System.Drawing.Color.Navy
        Me.cmdPalletIng.Location = New System.Drawing.Point(3, 184)
        Me.cmdPalletIng.Name = "cmdPalletIng"
        Me.cmdPalletIng.Size = New System.Drawing.Size(110, 17)
        Me.cmdPalletIng.TabIndex = 25
        Me.cmdPalletIng.Text = "F3) Pallet Ingresado"
        '
        'cmdPalletRest
        '
        Me.cmdPalletRest.BackColor = System.Drawing.Color.White
        Me.cmdPalletRest.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdPalletRest.ForeColor = System.Drawing.Color.Navy
        Me.cmdPalletRest.Location = New System.Drawing.Point(117, 184)
        Me.cmdPalletRest.Name = "cmdPalletRest"
        Me.cmdPalletRest.Size = New System.Drawing.Size(110, 17)
        Me.cmdPalletRest.TabIndex = 26
        Me.cmdPalletRest.Text = "F4) Pallet Restantes"
        '
        'cmdFinalizar
        '
        Me.cmdFinalizar.BackColor = System.Drawing.Color.White
        Me.cmdFinalizar.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdFinalizar.ForeColor = System.Drawing.Color.Navy
        Me.cmdFinalizar.Location = New System.Drawing.Point(3, 207)
        Me.cmdFinalizar.Name = "cmdFinalizar"
        Me.cmdFinalizar.Size = New System.Drawing.Size(110, 17)
        Me.cmdFinalizar.TabIndex = 27
        Me.cmdFinalizar.Text = "F5) Finalizar"
        '
        'cmdCambiar
        '
        Me.cmdCambiar.BackColor = System.Drawing.Color.White
        Me.cmdCambiar.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCambiar.ForeColor = System.Drawing.Color.Navy
        Me.cmdCambiar.Location = New System.Drawing.Point(117, 207)
        Me.cmdCambiar.Name = "cmdCambiar"
        Me.cmdCambiar.Size = New System.Drawing.Size(110, 17)
        Me.cmdCambiar.TabIndex = 32
        Me.cmdCambiar.Text = "F6) Control x Pedido"
        '
        'frmIngresoViajes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdCambiar)
        Me.Controls.Add(Me.cmdFinalizar)
        Me.Controls.Add(Me.cmdPalletRest)
        Me.Controls.Add(Me.cmdPalletIng)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdAgregarPallet)
        Me.Controls.Add(Me.txtNroPallet)
        Me.Controls.Add(Me.lblNroPallet)
        Me.Controls.Add(Me.lblNroViaje)
        Me.Controls.Add(Me.txtNroViaje)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.lblMenu)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frmIngresoViajes"
        Me.Text = "Control Expedicion"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents lblMenu As System.Windows.Forms.Label
    Friend WithEvents txtNroPallet As System.Windows.Forms.TextBox
    Friend WithEvents lblNroPallet As System.Windows.Forms.Label
    Friend WithEvents lblNroViaje As System.Windows.Forms.Label
    Friend WithEvents txtNroViaje As System.Windows.Forms.TextBox
    Friend WithEvents cmdAgregarPallet As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents cmdPalletIng As System.Windows.Forms.Button
    Friend WithEvents cmdPalletRest As System.Windows.Forms.Button
    Friend WithEvents cmdFinalizar As System.Windows.Forms.Button
    Friend WithEvents cmdCambiar As System.Windows.Forms.Button
End Class
