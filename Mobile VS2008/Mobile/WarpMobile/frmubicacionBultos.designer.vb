<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmUbicacionBultos
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
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtDock = New System.Windows.Forms.TextBox
        Me.txtBulto = New System.Windows.Forms.TextBox
        Me.btnSalir = New System.Windows.Forms.Button
        Me.lblMensaje = New System.Windows.Forms.Label
        Me.lblGuia = New System.Windows.Forms.Label
        Me.txtGuia = New System.Windows.Forms.TextBox
        Me.btnPendientes = New System.Windows.Forms.Button
        Me.btnFinalizar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(1, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 24)
        Me.Label1.Text = "Codigo de Dock"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(2, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(108, 24)
        Me.Label2.Text = "Codigo de Bulto"
        '
        'txtDock
        '
        Me.txtDock.Enabled = False
        Me.txtDock.Location = New System.Drawing.Point(117, 69)
        Me.txtDock.Name = "txtDock"
        Me.txtDock.Size = New System.Drawing.Size(119, 21)
        Me.txtDock.TabIndex = 3
        '
        'txtBulto
        '
        Me.txtBulto.Enabled = False
        Me.txtBulto.Location = New System.Drawing.Point(117, 42)
        Me.txtBulto.Name = "txtBulto"
        Me.txtBulto.Size = New System.Drawing.Size(119, 21)
        Me.txtBulto.TabIndex = 2
        '
        'btnSalir
        '
        Me.btnSalir.Location = New System.Drawing.Point(124, 245)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(112, 20)
        Me.btnSalir.TabIndex = 6
        Me.btnSalir.Text = "Salir"
        '
        'lblMensaje
        '
        Me.lblMensaje.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblMensaje.ForeColor = System.Drawing.Color.Red
        Me.lblMensaje.Location = New System.Drawing.Point(4, 167)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(233, 48)
        Me.lblMensaje.Text = "Lea el codigo de Guia."
        '
        'lblGuia
        '
        Me.lblGuia.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblGuia.Location = New System.Drawing.Point(2, 17)
        Me.lblGuia.Name = "lblGuia"
        Me.lblGuia.Size = New System.Drawing.Size(100, 24)
        Me.lblGuia.Text = "Nro de Guia"
        '
        'txtGuia
        '
        Me.txtGuia.Location = New System.Drawing.Point(117, 15)
        Me.txtGuia.Name = "txtGuia"
        Me.txtGuia.Size = New System.Drawing.Size(119, 21)
        Me.txtGuia.TabIndex = 1
        '
        'btnPendientes
        '
        Me.btnPendientes.Enabled = False
        Me.btnPendientes.Location = New System.Drawing.Point(6, 218)
        Me.btnPendientes.Name = "btnPendientes"
        Me.btnPendientes.Size = New System.Drawing.Size(112, 20)
        Me.btnPendientes.TabIndex = 4
        Me.btnPendientes.Text = "Ver Pendientes"
        '
        'btnFinalizar
        '
        Me.btnFinalizar.Enabled = False
        Me.btnFinalizar.Location = New System.Drawing.Point(124, 218)
        Me.btnFinalizar.Name = "btnFinalizar"
        Me.btnFinalizar.Size = New System.Drawing.Size(112, 20)
        Me.btnFinalizar.TabIndex = 5
        Me.btnFinalizar.Text = "Cancelar"
        '
        'frmUbicacionBultos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnPendientes)
        Me.Controls.Add(Me.lblMensaje)
        Me.Controls.Add(Me.btnFinalizar)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.txtBulto)
        Me.Controls.Add(Me.txtGuia)
        Me.Controls.Add(Me.txtDock)
        Me.Controls.Add(Me.lblGuia)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmUbicacionBultos"
        Me.Text = "Ubicacion de Bultos"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDock As System.Windows.Forms.TextBox
    Friend WithEvents txtBulto As System.Windows.Forms.TextBox
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents lblMensaje As System.Windows.Forms.Label
    Friend WithEvents lblGuia As System.Windows.Forms.Label
    Friend WithEvents txtGuia As System.Windows.Forms.TextBox
    Friend WithEvents btnPendientes As System.Windows.Forms.Button
    Friend WithEvents btnFinalizar As System.Windows.Forms.Button
End Class
