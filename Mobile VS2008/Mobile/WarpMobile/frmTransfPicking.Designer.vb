<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmTransfPicking
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
        Me.btnComenzarTareas = New System.Windows.Forms.Button
        Me.btnCompletados = New System.Windows.Forms.Button
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.lblCodViaje = New System.Windows.Forms.Label
        Me.CmbViajes = New System.Windows.Forms.ComboBox
        Me.lblUbicacionOri = New System.Windows.Forms.Label
        Me.txtUbiacionOri = New System.Windows.Forms.TextBox
        Me.lblContendora = New System.Windows.Forms.Label
        Me.txtContenedora = New System.Windows.Forms.TextBox
        Me.lblUbicacionDest = New System.Windows.Forms.Label
        Me.txtUbicacionDest = New System.Windows.Forms.TextBox
        Me.lblCliente = New System.Windows.Forms.Label
        Me.cmbClientes = New System.Windows.Forms.ComboBox
        Me.btnCerrarTransf = New System.Windows.Forms.Button
        Me.btnSaltarTarea = New System.Windows.Forms.Button
        Me.lblProducto = New System.Windows.Forms.Label
        Me.lblPallet = New System.Windows.Forms.Label
        Me.lblTipo = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnComenzarTareas
        '
        Me.btnComenzarTareas.BackColor = System.Drawing.Color.White
        Me.btnComenzarTareas.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.btnComenzarTareas.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnComenzarTareas.Location = New System.Drawing.Point(3, 229)
        Me.btnComenzarTareas.Name = "btnComenzarTareas"
        Me.btnComenzarTareas.Size = New System.Drawing.Size(110, 17)
        Me.btnComenzarTareas.TabIndex = 21
        Me.btnComenzarTareas.Text = "F1)  Comenzar Tareas"
        '
        'btnCompletados
        '
        Me.btnCompletados.BackColor = System.Drawing.Color.White
        Me.btnCompletados.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.btnCompletados.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnCompletados.Location = New System.Drawing.Point(3, 252)
        Me.btnCompletados.Name = "btnCompletados"
        Me.btnCompletados.Size = New System.Drawing.Size(110, 17)
        Me.btnCompletados.TabIndex = 22
        Me.btnCompletados.Text = "F3)  Completados"
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.White
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdSalir.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdSalir.Location = New System.Drawing.Point(3, 275)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(110, 16)
        Me.cmdSalir.TabIndex = 23
        Me.cmdSalir.Text = "F5) Salir"
        '
        'lblCodViaje
        '
        Me.lblCodViaje.Location = New System.Drawing.Point(3, 42)
        Me.lblCodViaje.Name = "lblCodViaje"
        Me.lblCodViaje.Size = New System.Drawing.Size(73, 20)
        Me.lblCodViaje.Text = "Codigo Viaje"
        Me.lblCodViaje.Visible = False
        '
        'CmbViajes
        '
        Me.CmbViajes.Location = New System.Drawing.Point(82, 42)
        Me.CmbViajes.Name = "CmbViajes"
        Me.CmbViajes.Size = New System.Drawing.Size(155, 22)
        Me.CmbViajes.TabIndex = 25
        Me.CmbViajes.Visible = False
        '
        'lblUbicacionOri
        '
        Me.lblUbicacionOri.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblUbicacionOri.Location = New System.Drawing.Point(3, 67)
        Me.lblUbicacionOri.Name = "lblUbicacionOri"
        Me.lblUbicacionOri.Size = New System.Drawing.Size(234, 20)
        Me.lblUbicacionOri.Text = "Ubicacion Origen :"
        Me.lblUbicacionOri.Visible = False
        '
        'txtUbiacionOri
        '
        Me.txtUbiacionOri.Location = New System.Drawing.Point(3, 83)
        Me.txtUbiacionOri.MaxLength = 45
        Me.txtUbiacionOri.Name = "txtUbiacionOri"
        Me.txtUbiacionOri.Size = New System.Drawing.Size(234, 21)
        Me.txtUbiacionOri.TabIndex = 27
        Me.txtUbiacionOri.Visible = False
        '
        'lblContendora
        '
        Me.lblContendora.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblContendora.Location = New System.Drawing.Point(2, 147)
        Me.lblContendora.Name = "lblContendora"
        Me.lblContendora.Size = New System.Drawing.Size(234, 20)
        Me.lblContendora.Text = "Transf Contenedora :"
        Me.lblContendora.Visible = False
        '
        'txtContenedora
        '
        Me.txtContenedora.Location = New System.Drawing.Point(3, 162)
        Me.txtContenedora.MaxLength = 15
        Me.txtContenedora.Name = "txtContenedora"
        Me.txtContenedora.Size = New System.Drawing.Size(233, 21)
        Me.txtContenedora.TabIndex = 29
        Me.txtContenedora.Visible = False
        '
        'lblUbicacionDest
        '
        Me.lblUbicacionDest.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblUbicacionDest.Location = New System.Drawing.Point(2, 188)
        Me.lblUbicacionDest.Name = "lblUbicacionDest"
        Me.lblUbicacionDest.Size = New System.Drawing.Size(234, 20)
        Me.lblUbicacionDest.Text = "Ubicacion Destino :"
        Me.lblUbicacionDest.Visible = False
        '
        'txtUbicacionDest
        '
        Me.txtUbicacionDest.Location = New System.Drawing.Point(3, 202)
        Me.txtUbicacionDest.MaxLength = 45
        Me.txtUbicacionDest.Name = "txtUbicacionDest"
        Me.txtUbicacionDest.Size = New System.Drawing.Size(234, 21)
        Me.txtUbicacionDest.TabIndex = 31
        Me.txtUbicacionDest.Visible = False
        '
        'lblCliente
        '
        Me.lblCliente.Location = New System.Drawing.Point(4, 19)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(72, 20)
        Me.lblCliente.Text = "Cliente"
        Me.lblCliente.Visible = False
        '
        'cmbClientes
        '
        Me.cmbClientes.Location = New System.Drawing.Point(82, 19)
        Me.cmbClientes.Name = "cmbClientes"
        Me.cmbClientes.Size = New System.Drawing.Size(155, 22)
        Me.cmbClientes.TabIndex = 33
        Me.cmbClientes.Visible = False
        '
        'btnCerrarTransf
        '
        Me.btnCerrarTransf.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.btnCerrarTransf.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.btnCerrarTransf.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnCerrarTransf.Location = New System.Drawing.Point(127, 229)
        Me.btnCerrarTransf.Name = "btnCerrarTransf"
        Me.btnCerrarTransf.Size = New System.Drawing.Size(109, 17)
        Me.btnCerrarTransf.TabIndex = 39
        Me.btnCerrarTransf.Text = "F2) Cerrar Transf."
        '
        'btnSaltarTarea
        '
        Me.btnSaltarTarea.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.btnSaltarTarea.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.btnSaltarTarea.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnSaltarTarea.Location = New System.Drawing.Point(127, 252)
        Me.btnSaltarTarea.Name = "btnSaltarTarea"
        Me.btnSaltarTarea.Size = New System.Drawing.Size(109, 17)
        Me.btnSaltarTarea.TabIndex = 40
        Me.btnSaltarTarea.Text = "F4) Saltar Tarea"
        '
        'lblProducto
        '
        Me.lblProducto.Location = New System.Drawing.Point(3, 107)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(233, 17)
        Me.lblProducto.Text = "Producto: "
        Me.lblProducto.Visible = False
        '
        'lblPallet
        '
        Me.lblPallet.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblPallet.Location = New System.Drawing.Point(2, 128)
        Me.lblPallet.Name = "lblPallet"
        Me.lblPallet.Size = New System.Drawing.Size(234, 20)
        Me.lblPallet.Text = "Pallet:"
        Me.lblPallet.Visible = False
        '
        'lblTipo
        '
        Me.lblTipo.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblTipo.Location = New System.Drawing.Point(0, 1)
        Me.lblTipo.Name = "lblTipo"
        Me.lblTipo.Size = New System.Drawing.Size(240, 16)
        Me.lblTipo.Text = "Label1"
        Me.lblTipo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmTransfPicking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblTipo)
        Me.Controls.Add(Me.lblPallet)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.btnSaltarTarea)
        Me.Controls.Add(Me.btnCerrarTransf)
        Me.Controls.Add(Me.cmbClientes)
        Me.Controls.Add(Me.lblCliente)
        Me.Controls.Add(Me.txtUbicacionDest)
        Me.Controls.Add(Me.lblUbicacionDest)
        Me.Controls.Add(Me.txtContenedora)
        Me.Controls.Add(Me.lblContendora)
        Me.Controls.Add(Me.txtUbiacionOri)
        Me.Controls.Add(Me.lblUbicacionOri)
        Me.Controls.Add(Me.CmbViajes)
        Me.Controls.Add(Me.lblCodViaje)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.btnCompletados)
        Me.Controls.Add(Me.btnComenzarTareas)
        Me.KeyPreview = True
        Me.Name = "frmTransfPicking"
        Me.Text = "TransferenciasPicking"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnComenzarTareas As System.Windows.Forms.Button
    Friend WithEvents btnCompletados As System.Windows.Forms.Button
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents lblCodViaje As System.Windows.Forms.Label
    Friend WithEvents CmbViajes As System.Windows.Forms.ComboBox
    Friend WithEvents lblUbicacionOri As System.Windows.Forms.Label
    Friend WithEvents txtUbiacionOri As System.Windows.Forms.TextBox
    Friend WithEvents lblContendora As System.Windows.Forms.Label
    Friend WithEvents txtContenedora As System.Windows.Forms.TextBox
    Friend WithEvents lblUbicacionDest As System.Windows.Forms.Label
    Friend WithEvents txtUbicacionDest As System.Windows.Forms.TextBox
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents cmbClientes As System.Windows.Forms.ComboBox
    Friend WithEvents btnCerrarTransf As System.Windows.Forms.Button
    Friend WithEvents btnSaltarTarea As System.Windows.Forms.Button
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents lblPallet As System.Windows.Forms.Label
    Friend WithEvents lblTipo As System.Windows.Forms.Label
End Class
