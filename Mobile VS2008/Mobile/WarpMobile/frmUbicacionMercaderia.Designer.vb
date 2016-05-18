<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmUbicacionMercaderia
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
        Me.txtDestino = New System.Windows.Forms.TextBox
        Me.lblDestino = New System.Windows.Forms.Label
        Me.lblMenu = New System.Windows.Forms.Label
        Me.lblMsg = New System.Windows.Forms.Label
        Me.lblNewMenu = New System.Windows.Forms.Label
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.lblProducto = New System.Windows.Forms.Label
        Me.cmdProducto = New System.Windows.Forms.Button
        Me.txtCantidad = New System.Windows.Forms.TextBox
        Me.lblCantidad = New System.Windows.Forms.Label
        Me.cmdRechazar = New System.Windows.Forms.Button
        Me.cmdLockPosition = New System.Windows.Forms.Button
        Me.TxtCamada = New System.Windows.Forms.TextBox
        Me.lblCamada = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblPallet
        '
        Me.lblPallet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblPallet.Location = New System.Drawing.Point(3, 5)
        Me.lblPallet.Name = "lblPallet"
        Me.lblPallet.Size = New System.Drawing.Size(223, 21)
        Me.lblPallet.Text = "Pallet:"
        '
        'txtPallet
        '
        Me.txtPallet.Location = New System.Drawing.Point(3, 29)
        Me.txtPallet.MaxLength = 100
        Me.txtPallet.Name = "txtPallet"
        Me.txtPallet.Size = New System.Drawing.Size(225, 21)
        Me.txtPallet.TabIndex = 12
        '
        'txtDestino
        '
        Me.txtDestino.Location = New System.Drawing.Point(3, 196)
        Me.txtDestino.MaxLength = 45
        Me.txtDestino.Name = "txtDestino"
        Me.txtDestino.Size = New System.Drawing.Size(225, 21)
        Me.txtDestino.TabIndex = 14
        '
        'lblDestino
        '
        Me.lblDestino.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDestino.Location = New System.Drawing.Point(3, 173)
        Me.lblDestino.Name = "lblDestino"
        Me.lblDestino.Size = New System.Drawing.Size(223, 20)
        Me.lblDestino.Text = "Ubicación: "
        '
        'lblMenu
        '
        Me.lblMenu.Location = New System.Drawing.Point(183, 274)
        Me.lblMenu.Name = "lblMenu"
        Me.lblMenu.Size = New System.Drawing.Size(43, 23)
        Me.lblMenu.Text = "Menu"
        '
        'lblMsg
        '
        Me.lblMsg.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(5, 266)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(223, 31)
        Me.lblMsg.Text = "lblMsg"
        '
        'lblNewMenu
        '
        Me.lblNewMenu.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblNewMenu.Location = New System.Drawing.Point(0, 279)
        Me.lblNewMenu.Name = "lblNewMenu"
        Me.lblNewMenu.Size = New System.Drawing.Size(115, 23)
        Me.lblNewMenu.Text = "NewMenu"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.BackColor = System.Drawing.Color.White
        Me.cmdCancelar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCancelar.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdCancelar.Location = New System.Drawing.Point(3, 220)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(112, 20)
        Me.cmdCancelar.TabIndex = 17
        Me.cmdCancelar.Text = "Cancelar   F2"
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.White
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdSalir.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdSalir.Location = New System.Drawing.Point(116, 220)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(112, 20)
        Me.cmdSalir.TabIndex = 18
        Me.cmdSalir.Text = "Salir          F3"
        '
        'lblProducto
        '
        Me.lblProducto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblProducto.Location = New System.Drawing.Point(3, 54)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(223, 55)
        Me.lblProducto.Text = "Producto:"
        Me.lblProducto.Visible = False
        '
        'cmdProducto
        '
        Me.cmdProducto.BackColor = System.Drawing.Color.White
        Me.cmdProducto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdProducto.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdProducto.Location = New System.Drawing.Point(149, 146)
        Me.cmdProducto.Name = "cmdProducto"
        Me.cmdProducto.Size = New System.Drawing.Size(79, 20)
        Me.cmdProducto.TabIndex = 26
        Me.cmdProducto.Text = "Producto  F4"
        Me.cmdProducto.Visible = False
        '
        'txtCantidad
        '
        Me.txtCantidad.Location = New System.Drawing.Point(82, 146)
        Me.txtCantidad.MaxLength = 5
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(59, 21)
        Me.txtCantidad.TabIndex = 43
        Me.txtCantidad.Visible = False
        '
        'lblCantidad
        '
        Me.lblCantidad.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCantidad.Location = New System.Drawing.Point(3, 146)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(73, 21)
        Me.lblCantidad.Text = "Cantidad"
        Me.lblCantidad.Visible = False
        '
        'cmdRechazar
        '
        Me.cmdRechazar.BackColor = System.Drawing.Color.White
        Me.cmdRechazar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdRechazar.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdRechazar.Location = New System.Drawing.Point(3, 243)
        Me.cmdRechazar.Name = "cmdRechazar"
        Me.cmdRechazar.Size = New System.Drawing.Size(112, 20)
        Me.cmdRechazar.TabIndex = 51
        Me.cmdRechazar.Text = "Rechazar  F5"
        '
        'cmdLockPosition
        '
        Me.cmdLockPosition.BackColor = System.Drawing.Color.White
        Me.cmdLockPosition.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdLockPosition.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdLockPosition.Location = New System.Drawing.Point(116, 243)
        Me.cmdLockPosition.Name = "cmdLockPosition"
        Me.cmdLockPosition.Size = New System.Drawing.Size(112, 20)
        Me.cmdLockPosition.TabIndex = 59
        Me.cmdLockPosition.Text = "LCK Pos. F6"
        '
        'TxtCamada
        '
        Me.TxtCamada.Location = New System.Drawing.Point(82, 119)
        Me.TxtCamada.MaxLength = 5
        Me.TxtCamada.Name = "TxtCamada"
        Me.TxtCamada.Size = New System.Drawing.Size(59, 21)
        Me.TxtCamada.TabIndex = 68
        Me.TxtCamada.Visible = False
        '
        'lblCamada
        '
        Me.lblCamada.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCamada.Location = New System.Drawing.Point(3, 119)
        Me.lblCamada.Name = "lblCamada"
        Me.lblCamada.Size = New System.Drawing.Size(73, 21)
        Me.lblCamada.Text = "Camada"
        Me.lblCamada.Visible = False
        '
        'frmUbicacionMercaderia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(240, 320)
        Me.ControlBox = False
        Me.Controls.Add(Me.TxtCamada)
        Me.Controls.Add(Me.lblCamada)
        Me.Controls.Add(Me.cmdLockPosition)
        Me.Controls.Add(Me.cmdRechazar)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.lblCantidad)
        Me.Controls.Add(Me.cmdProducto)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.lblNewMenu)
        Me.Controls.Add(Me.lblMenu)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.txtDestino)
        Me.Controls.Add(Me.lblDestino)
        Me.Controls.Add(Me.lblPallet)
        Me.Controls.Add(Me.txtPallet)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frmUbicacionMercaderia"
        Me.Text = "Ubicación de Mercadería"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblPallet As System.Windows.Forms.Label
    Friend WithEvents txtPallet As System.Windows.Forms.TextBox
    Friend WithEvents txtDestino As System.Windows.Forms.TextBox
    Friend WithEvents lblDestino As System.Windows.Forms.Label
    Friend WithEvents lblMenu As System.Windows.Forms.Label
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents lblNewMenu As System.Windows.Forms.Label
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents cmdProducto As System.Windows.Forms.Button
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents lblCantidad As System.Windows.Forms.Label
    Friend WithEvents cmdRechazar As System.Windows.Forms.Button
    Friend WithEvents cmdLockPosition As System.Windows.Forms.Button
    Friend WithEvents TxtCamada As System.Windows.Forms.TextBox
    Friend WithEvents lblCamada As System.Windows.Forms.Label
End Class
