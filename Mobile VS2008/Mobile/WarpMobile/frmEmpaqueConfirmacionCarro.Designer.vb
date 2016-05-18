<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEmpaqueConfirmacionCarro
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
        Me.lblCodigoViaje = New System.Windows.Forms.Label
        Me.lblContenedora = New System.Windows.Forms.Label
        Me.txtContenedora = New System.Windows.Forms.TextBox
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.lblMsg = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblCodigoViaje
        '
        Me.lblCodigoViaje.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblCodigoViaje.Location = New System.Drawing.Point(0, 3)
        Me.lblCodigoViaje.Name = "lblCodigoViaje"
        Me.lblCodigoViaje.Size = New System.Drawing.Size(240, 20)
        Me.lblCodigoViaje.Text = "Codigo de Ola "
        Me.lblCodigoViaje.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblContenedora
        '
        Me.lblContenedora.Location = New System.Drawing.Point(0, 48)
        Me.lblContenedora.Name = "lblContenedora"
        Me.lblContenedora.Size = New System.Drawing.Size(240, 20)
        Me.lblContenedora.Text = "Confirme Contenedora"
        '
        'txtContenedora
        '
        Me.txtContenedora.Location = New System.Drawing.Point(0, 71)
        Me.txtContenedora.Name = "txtContenedora"
        Me.txtContenedora.Size = New System.Drawing.Size(240, 21)
        Me.txtContenedora.TabIndex = 2
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(0, 129)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(119, 20)
        Me.btnAceptar.TabIndex = 3
        Me.btnAceptar.Text = "Aceptar"
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(121, 129)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(119, 20)
        Me.btnCancelar.TabIndex = 4
        Me.btnCancelar.Text = "Cancelar"
        '
        'lblMsg
        '
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(0, 184)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(240, 30)
        '
        'frmEmpaqueConfirmacionCarro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.txtContenedora)
        Me.Controls.Add(Me.lblContenedora)
        Me.Controls.Add(Me.lblCodigoViaje)
        Me.KeyPreview = True
        Me.Name = "frmEmpaqueConfirmacionCarro"
        Me.Text = "Confirmacion Contenedor"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCodigoViaje As System.Windows.Forms.Label
    Friend WithEvents lblContenedora As System.Windows.Forms.Label
    Friend WithEvents txtContenedora As System.Windows.Forms.TextBox
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents lblMsg As System.Windows.Forms.Label
End Class
