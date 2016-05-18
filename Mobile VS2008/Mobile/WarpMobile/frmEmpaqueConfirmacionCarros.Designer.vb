<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEmpaqueConfirmacionCarros
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
        Me.pnlCarros = New System.Windows.Forms.Panel
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.lblCodigoViaje = New System.Windows.Forms.Label
        Me.lblValidos = New System.Windows.Forms.Label
        Me.lblNoValidos = New System.Windows.Forms.Label
        Me.btnContinuar = New System.Windows.Forms.Button
        Me.btnQuitarCarros = New System.Windows.Forms.Button
        Me.btnAgregarCarro = New System.Windows.Forms.Button
        Me.txtCarros = New System.Windows.Forms.TextBox
        Me.lblTituloCarro = New System.Windows.Forms.Label
        Me.pnlCarros.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlCarros
        '
        Me.pnlCarros.Controls.Add(Me.btnCancelar)
        Me.pnlCarros.Controls.Add(Me.lblCodigoViaje)
        Me.pnlCarros.Controls.Add(Me.lblValidos)
        Me.pnlCarros.Controls.Add(Me.lblNoValidos)
        Me.pnlCarros.Controls.Add(Me.btnContinuar)
        Me.pnlCarros.Controls.Add(Me.btnQuitarCarros)
        Me.pnlCarros.Controls.Add(Me.btnAgregarCarro)
        Me.pnlCarros.Controls.Add(Me.txtCarros)
        Me.pnlCarros.Controls.Add(Me.lblTituloCarro)
        Me.pnlCarros.Location = New System.Drawing.Point(0, 3)
        Me.pnlCarros.Name = "pnlCarros"
        Me.pnlCarros.Size = New System.Drawing.Size(240, 257)
        '
        'btnCancelar
        '
        Me.btnCancelar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancelar.Location = New System.Drawing.Point(126, 185)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(111, 18)
        Me.btnCancelar.TabIndex = 10
        Me.btnCancelar.Text = "F4) Cancelar"
        '
        'lblCodigoViaje
        '
        Me.lblCodigoViaje.Location = New System.Drawing.Point(3, 48)
        Me.lblCodigoViaje.Name = "lblCodigoViaje"
        Me.lblCodigoViaje.Size = New System.Drawing.Size(234, 20)
        '
        'lblValidos
        '
        Me.lblValidos.Location = New System.Drawing.Point(3, 119)
        Me.lblValidos.Name = "lblValidos"
        Me.lblValidos.Size = New System.Drawing.Size(170, 37)
        Me.lblValidos.Text = "Validos"
        '
        'lblNoValidos
        '
        Me.lblNoValidos.ForeColor = System.Drawing.Color.Red
        Me.lblNoValidos.Location = New System.Drawing.Point(3, 81)
        Me.lblNoValidos.Name = "lblNoValidos"
        Me.lblNoValidos.Size = New System.Drawing.Size(234, 38)
        Me.lblNoValidos.Text = "No Validos"
        '
        'btnContinuar
        '
        Me.btnContinuar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnContinuar.Location = New System.Drawing.Point(3, 185)
        Me.btnContinuar.Name = "btnContinuar"
        Me.btnContinuar.Size = New System.Drawing.Size(111, 20)
        Me.btnContinuar.TabIndex = 8
        Me.btnContinuar.Text = "F3) Continuar"
        '
        'btnQuitarCarros
        '
        Me.btnQuitarCarros.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnQuitarCarros.Location = New System.Drawing.Point(126, 162)
        Me.btnQuitarCarros.Name = "btnQuitarCarros"
        Me.btnQuitarCarros.Size = New System.Drawing.Size(111, 20)
        Me.btnQuitarCarros.TabIndex = 6
        Me.btnQuitarCarros.Text = "F2) Quitar Carros"
        '
        'btnAgregarCarro
        '
        Me.btnAgregarCarro.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnAgregarCarro.Location = New System.Drawing.Point(3, 162)
        Me.btnAgregarCarro.Name = "btnAgregarCarro"
        Me.btnAgregarCarro.Size = New System.Drawing.Size(111, 20)
        Me.btnAgregarCarro.TabIndex = 5
        Me.btnAgregarCarro.Text = "F1) Agregar Carro"
        '
        'txtCarros
        '
        Me.txtCarros.Location = New System.Drawing.Point(3, 24)
        Me.txtCarros.Name = "txtCarros"
        Me.txtCarros.Size = New System.Drawing.Size(234, 21)
        Me.txtCarros.TabIndex = 3
        '
        'lblTituloCarro
        '
        Me.lblTituloCarro.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblTituloCarro.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTituloCarro.Location = New System.Drawing.Point(3, 1)
        Me.lblTituloCarro.Name = "lblTituloCarro"
        Me.lblTituloCarro.Size = New System.Drawing.Size(234, 20)
        Me.lblTituloCarro.Text = "Ingrese el / los carros de picking"
        '
        'frmEmpaqueConfirmacionCarros
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlCarros)
        Me.KeyPreview = True
        Me.Name = "frmEmpaqueConfirmacionCarros"
        Me.Text = "Confirmacion de carros"
        Me.pnlCarros.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlCarros As System.Windows.Forms.Panel
    Friend WithEvents lblCodigoViaje As System.Windows.Forms.Label
    Friend WithEvents lblValidos As System.Windows.Forms.Label
    Friend WithEvents lblNoValidos As System.Windows.Forms.Label
    Friend WithEvents btnContinuar As System.Windows.Forms.Button
    Friend WithEvents btnQuitarCarros As System.Windows.Forms.Button
    Friend WithEvents btnAgregarCarro As System.Windows.Forms.Button
    Friend WithEvents txtCarros As System.Windows.Forms.TextBox
    Friend WithEvents lblTituloCarro As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
End Class
