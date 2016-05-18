<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmTransfSobrantesPick
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
        Me.txtUbicacionDest = New System.Windows.Forms.TextBox
        Me.lblUbicacionDest = New System.Windows.Forms.Label
        Me.txtContenedora = New System.Windows.Forms.TextBox
        Me.lblContendora = New System.Windows.Forms.Label
        Me.txtUbiacionOri = New System.Windows.Forms.TextBox
        Me.lblUbicacionOri = New System.Windows.Forms.Label
        Me.cmdNuevaPos = New System.Windows.Forms.Button
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.cmdStartTransf = New System.Windows.Forms.Button
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.LblUbicacionSug = New System.Windows.Forms.Label
        Me.lblTipo = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtUbicacionDest
        '
        Me.txtUbicacionDest.Location = New System.Drawing.Point(3, 178)
        Me.txtUbicacionDest.MaxLength = 45
        Me.txtUbicacionDest.Name = "txtUbicacionDest"
        Me.txtUbicacionDest.Size = New System.Drawing.Size(233, 21)
        Me.txtUbicacionDest.TabIndex = 54
        Me.txtUbicacionDest.Visible = False
        '
        'lblUbicacionDest
        '
        Me.lblUbicacionDest.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblUbicacionDest.Location = New System.Drawing.Point(3, 160)
        Me.lblUbicacionDest.Name = "lblUbicacionDest"
        Me.lblUbicacionDest.Size = New System.Drawing.Size(111, 15)
        Me.lblUbicacionDest.Text = "Ubicacion Destino :"
        Me.lblUbicacionDest.Visible = False
        '
        'txtContenedora
        '
        Me.txtContenedora.Location = New System.Drawing.Point(3, 110)
        Me.txtContenedora.MaxLength = 15
        Me.txtContenedora.Name = "txtContenedora"
        Me.txtContenedora.Size = New System.Drawing.Size(233, 21)
        Me.txtContenedora.TabIndex = 53
        Me.txtContenedora.Visible = False
        '
        'lblContendora
        '
        Me.lblContendora.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblContendora.Location = New System.Drawing.Point(3, 90)
        Me.lblContendora.Name = "lblContendora"
        Me.lblContendora.Size = New System.Drawing.Size(234, 17)
        Me.lblContendora.Text = "Contenedora :"
        Me.lblContendora.Visible = False
        '
        'txtUbiacionOri
        '
        Me.txtUbiacionOri.Location = New System.Drawing.Point(3, 45)
        Me.txtUbiacionOri.MaxLength = 45
        Me.txtUbiacionOri.Name = "txtUbiacionOri"
        Me.txtUbiacionOri.Size = New System.Drawing.Size(233, 21)
        Me.txtUbiacionOri.TabIndex = 52
        Me.txtUbiacionOri.Visible = False
        '
        'lblUbicacionOri
        '
        Me.lblUbicacionOri.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblUbicacionOri.Location = New System.Drawing.Point(3, 26)
        Me.lblUbicacionOri.Name = "lblUbicacionOri"
        Me.lblUbicacionOri.Size = New System.Drawing.Size(234, 16)
        Me.lblUbicacionOri.Text = "Ubicacion Origen :"
        Me.lblUbicacionOri.Visible = False
        '
        'cmdNuevaPos
        '
        Me.cmdNuevaPos.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.cmdNuevaPos.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdNuevaPos.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdNuevaPos.Location = New System.Drawing.Point(3, 253)
        Me.cmdNuevaPos.Name = "cmdNuevaPos"
        Me.cmdNuevaPos.Size = New System.Drawing.Size(233, 16)
        Me.cmdNuevaPos.TabIndex = 62
        Me.cmdNuevaPos.Text = "F3) Nueva Posicion"
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.White
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdSalir.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdSalir.Location = New System.Drawing.Point(3, 275)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(233, 16)
        Me.cmdSalir.TabIndex = 61
        Me.cmdSalir.Text = "F4) Salir"
        '
        'cmdStartTransf
        '
        Me.cmdStartTransf.BackColor = System.Drawing.Color.White
        Me.cmdStartTransf.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdStartTransf.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdStartTransf.Location = New System.Drawing.Point(3, 208)
        Me.cmdStartTransf.Name = "cmdStartTransf"
        Me.cmdStartTransf.Size = New System.Drawing.Size(233, 17)
        Me.cmdStartTransf.TabIndex = 60
        Me.cmdStartTransf.Text = "F1) Comenzar Transferencia"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.BackColor = System.Drawing.Color.White
        Me.cmdCancelar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCancelar.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdCancelar.Location = New System.Drawing.Point(3, 231)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(233, 17)
        Me.cmdCancelar.TabIndex = 63
        Me.cmdCancelar.Text = "F2) Cancelar Transferencia"
        '
        'LblUbicacionSug
        '
        Me.LblUbicacionSug.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.LblUbicacionSug.Location = New System.Drawing.Point(116, 161)
        Me.LblUbicacionSug.Name = "LblUbicacionSug"
        Me.LblUbicacionSug.Size = New System.Drawing.Size(111, 15)
        Me.LblUbicacionSug.Visible = False
        '
        'lblTipo
        '
        Me.lblTipo.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblTipo.Location = New System.Drawing.Point(0, 2)
        Me.lblTipo.Name = "lblTipo"
        Me.lblTipo.Size = New System.Drawing.Size(240, 16)
        Me.lblTipo.Text = "Label1"
        Me.lblTipo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmTransfSobrantesPick
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblTipo)
        Me.Controls.Add(Me.LblUbicacionSug)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdNuevaPos)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdStartTransf)
        Me.Controls.Add(Me.txtUbicacionDest)
        Me.Controls.Add(Me.lblUbicacionDest)
        Me.Controls.Add(Me.txtContenedora)
        Me.Controls.Add(Me.lblContendora)
        Me.Controls.Add(Me.txtUbiacionOri)
        Me.Controls.Add(Me.lblUbicacionOri)
        Me.KeyPreview = True
        Me.Name = "frmTransfSobrantesPick"
        Me.Text = "Transf. de Sobrantes de Picking"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtUbicacionDest As System.Windows.Forms.TextBox
    Friend WithEvents lblUbicacionDest As System.Windows.Forms.Label
    Friend WithEvents txtContenedora As System.Windows.Forms.TextBox
    Friend WithEvents lblContendora As System.Windows.Forms.Label
    Friend WithEvents txtUbiacionOri As System.Windows.Forms.TextBox
    Friend WithEvents lblUbicacionOri As System.Windows.Forms.Label
    Friend WithEvents cmdNuevaPos As System.Windows.Forms.Button
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdStartTransf As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents LblUbicacionSug As System.Windows.Forms.Label
    Friend WithEvents lblTipo As System.Windows.Forms.Label
End Class
