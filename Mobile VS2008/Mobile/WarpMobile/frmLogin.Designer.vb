<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmLoggin
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
        Me.lblGlobalTech = New System.Windows.Forms.Label
        Me.lblStatus = New System.Windows.Forms.Label
        Me.lblMenu = New System.Windows.Forms.Label
        Me.txtPass = New System.Windows.Forms.TextBox
        Me.lblPassword = New System.Windows.Forms.Label
        Me.txtCodUsr = New System.Windows.Forms.TextBox
        Me.lblCod_Usr = New System.Windows.Forms.Label
        Me.lblLink = New System.Windows.Forms.Label
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.cmdIngresar = New System.Windows.Forms.Button
        Me.PB = New System.Windows.Forms.ProgressBar
        Me.cboServer = New System.Windows.Forms.ComboBox
        Me.lblServer = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblGlobalTech
        '
        Me.lblGlobalTech.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.lblGlobalTech.Location = New System.Drawing.Point(28, 255)
        Me.lblGlobalTech.Name = "lblGlobalTech"
        Me.lblGlobalTech.Size = New System.Drawing.Size(171, 19)
        Me.lblGlobalTech.Text = "Powered by GlobalTech."
        Me.lblGlobalTech.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatus.ForeColor = System.Drawing.Color.Red
        Me.lblStatus.Location = New System.Drawing.Point(12, 213)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(212, 39)
        Me.lblStatus.Text = "Status"
        '
        'lblMenu
        '
        Me.lblMenu.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.lblMenu.ForeColor = System.Drawing.Color.Black
        Me.lblMenu.Location = New System.Drawing.Point(12, 315)
        Me.lblMenu.Name = "lblMenu"
        Me.lblMenu.Size = New System.Drawing.Size(11, 12)
        Me.lblMenu.Text = "Menu"
        Me.lblMenu.Visible = False
        '
        'txtPass
        '
        Me.txtPass.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.txtPass.Location = New System.Drawing.Point(12, 107)
        Me.txtPass.MaxLength = 50
        Me.txtPass.Name = "txtPass"
        Me.txtPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPass.Size = New System.Drawing.Size(212, 23)
        Me.txtPass.TabIndex = 2
        '
        'lblPassword
        '
        Me.lblPassword.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPassword.Location = New System.Drawing.Point(12, 89)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(212, 16)
        Me.lblPassword.Text = "Password"
        '
        'txtCodUsr
        '
        Me.txtCodUsr.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.txtCodUsr.Location = New System.Drawing.Point(12, 63)
        Me.txtCodUsr.MaxLength = 20
        Me.txtCodUsr.Name = "txtCodUsr"
        Me.txtCodUsr.Size = New System.Drawing.Size(212, 23)
        Me.txtCodUsr.TabIndex = 1
        '
        'lblCod_Usr
        '
        Me.lblCod_Usr.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblCod_Usr.Location = New System.Drawing.Point(12, 45)
        Me.lblCod_Usr.Name = "lblCod_Usr"
        Me.lblCod_Usr.Size = New System.Drawing.Size(212, 16)
        Me.lblCod_Usr.Text = "Código de Usuario"
        '
        'lblLink
        '
        Me.lblLink.Location = New System.Drawing.Point(28, 275)
        Me.lblLink.Name = "lblLink"
        Me.lblLink.Size = New System.Drawing.Size(171, 19)
        Me.lblLink.Text = "www.globaltechsa.com.ar"
        Me.lblLink.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.Transparent
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdSalir.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdSalir.Location = New System.Drawing.Point(12, 189)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(212, 20)
        Me.cmdSalir.TabIndex = 5
        Me.cmdSalir.Text = "Salir               F3"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.BackColor = System.Drawing.Color.Transparent
        Me.cmdCancelar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCancelar.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdCancelar.Location = New System.Drawing.Point(12, 165)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(212, 20)
        Me.cmdCancelar.TabIndex = 4
        Me.cmdCancelar.Text = "Cancelar       F2"
        '
        'cmdIngresar
        '
        Me.cmdIngresar.BackColor = System.Drawing.Color.Transparent
        Me.cmdIngresar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdIngresar.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdIngresar.Location = New System.Drawing.Point(12, 140)
        Me.cmdIngresar.Name = "cmdIngresar"
        Me.cmdIngresar.Size = New System.Drawing.Size(212, 20)
        Me.cmdIngresar.TabIndex = 3
        Me.cmdIngresar.Text = "Aceptar         F1"
        '
        'PB
        '
        Me.PB.Location = New System.Drawing.Point(12, 302)
        Me.PB.Name = "PB"
        Me.PB.Size = New System.Drawing.Size(212, 11)
        Me.PB.Visible = False
        '
        'cboServer
        '
        Me.cboServer.Location = New System.Drawing.Point(12, 21)
        Me.cboServer.Name = "cboServer"
        Me.cboServer.Size = New System.Drawing.Size(212, 22)
        Me.cboServer.TabIndex = 20
        '
        'lblServer
        '
        Me.lblServer.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblServer.Location = New System.Drawing.Point(12, 2)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(212, 16)
        Me.lblServer.Text = "Servidor"
        '
        'frmLoggin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 333)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblServer)
        Me.Controls.Add(Me.cboServer)
        Me.Controls.Add(Me.PB)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdIngresar)
        Me.Controls.Add(Me.lblLink)
        Me.Controls.Add(Me.lblGlobalTech)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblMenu)
        Me.Controls.Add(Me.txtPass)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.txtCodUsr)
        Me.Controls.Add(Me.lblCod_Usr)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frmLoggin"
        Me.Text = "WarpMobile."
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblGlobalTech As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblMenu As System.Windows.Forms.Label
    Friend WithEvents txtPass As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtCodUsr As System.Windows.Forms.TextBox
    Friend WithEvents lblCod_Usr As System.Windows.Forms.Label
    Friend WithEvents lblLink As System.Windows.Forms.Label
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents cmdIngresar As System.Windows.Forms.Button
    Friend WithEvents PB As System.Windows.Forms.ProgressBar
    Friend WithEvents cboServer As System.Windows.Forms.ComboBox
    Friend WithEvents lblServer As System.Windows.Forms.Label

End Class
