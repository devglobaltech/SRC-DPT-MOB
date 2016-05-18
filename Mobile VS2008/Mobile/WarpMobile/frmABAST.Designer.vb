<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmABAST
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
        Me.bComenzar = New System.Windows.Forms.Button
        Me.bCerrarCont = New System.Windows.Forms.Button
        Me.bSalir = New System.Windows.Forms.Button
        Me.lblNroContenedora = New System.Windows.Forms.Label
        Me.txtContenedora = New System.Windows.Forms.TextBox
        Me.lstInformacion = New System.Windows.Forms.ListBox
        Me.lblUbicacionOrigen = New System.Windows.Forms.Label
        Me.txtUbicacion = New System.Windows.Forms.TextBox
        Me.lblConf = New System.Windows.Forms.Label
        Me.txtConf = New System.Windows.Forms.TextBox
        Me.lblTareaCompleta = New System.Windows.Forms.Label
        Me.frmDescargar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'bComenzar
        '
        Me.bComenzar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.bComenzar.Location = New System.Drawing.Point(1, 248)
        Me.bComenzar.Name = "bComenzar"
        Me.bComenzar.Size = New System.Drawing.Size(115, 20)
        Me.bComenzar.TabIndex = 0
        Me.bComenzar.Text = "F1) Comenzar"
        '
        'bCerrarCont
        '
        Me.bCerrarCont.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.bCerrarCont.Location = New System.Drawing.Point(122, 248)
        Me.bCerrarCont.Name = "bCerrarCont"
        Me.bCerrarCont.Size = New System.Drawing.Size(116, 20)
        Me.bCerrarCont.TabIndex = 1
        Me.bCerrarCont.Text = "F2) Cerrar Cont."
        '
        'bSalir
        '
        Me.bSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.bSalir.Location = New System.Drawing.Point(1, 271)
        Me.bSalir.Name = "bSalir"
        Me.bSalir.Size = New System.Drawing.Size(115, 20)
        Me.bSalir.TabIndex = 2
        Me.bSalir.Text = "F3) Salir"
        '
        'lblNroContenedora
        '
        Me.lblNroContenedora.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNroContenedora.Location = New System.Drawing.Point(0, 3)
        Me.lblNroContenedora.Name = "lblNroContenedora"
        Me.lblNroContenedora.Size = New System.Drawing.Size(129, 20)
        Me.lblNroContenedora.Text = "Nro. Contenedora:"
        '
        'txtContenedora
        '
        Me.txtContenedora.Location = New System.Drawing.Point(122, 2)
        Me.txtContenedora.MaxLength = 10
        Me.txtContenedora.Name = "txtContenedora"
        Me.txtContenedora.Size = New System.Drawing.Size(115, 21)
        Me.txtContenedora.TabIndex = 4
        '
        'lstInformacion
        '
        Me.lstInformacion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstInformacion.Location = New System.Drawing.Point(1, 30)
        Me.lstInformacion.Name = "lstInformacion"
        Me.lstInformacion.Size = New System.Drawing.Size(237, 106)
        Me.lstInformacion.TabIndex = 5
        '
        'lblUbicacionOrigen
        '
        Me.lblUbicacionOrigen.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblUbicacionOrigen.Location = New System.Drawing.Point(1, 144)
        Me.lblUbicacionOrigen.Name = "lblUbicacionOrigen"
        Me.lblUbicacionOrigen.Size = New System.Drawing.Size(237, 20)
        Me.lblUbicacionOrigen.Text = "Ub. NP1-2-3-40-1 | Nro.Serie: 1s00000000"
        '
        'txtUbicacion
        '
        Me.txtUbicacion.Location = New System.Drawing.Point(1, 166)
        Me.txtUbicacion.MaxLength = 55
        Me.txtUbicacion.Name = "txtUbicacion"
        Me.txtUbicacion.Size = New System.Drawing.Size(237, 21)
        Me.txtUbicacion.TabIndex = 7
        '
        'lblConf
        '
        Me.lblConf.Location = New System.Drawing.Point(0, 192)
        Me.lblConf.Name = "lblConf"
        Me.lblConf.Size = New System.Drawing.Size(116, 20)
        Me.lblConf.Text = "Cantidad/Serie/MP"
        '
        'txtConf
        '
        Me.txtConf.Location = New System.Drawing.Point(111, 190)
        Me.txtConf.MaxLength = 10
        Me.txtConf.Name = "txtConf"
        Me.txtConf.Size = New System.Drawing.Size(127, 21)
        Me.txtConf.TabIndex = 9
        '
        'lblTareaCompleta
        '
        Me.lblTareaCompleta.Location = New System.Drawing.Point(0, 228)
        Me.lblTareaCompleta.Name = "lblTareaCompleta"
        Me.lblTareaCompleta.Size = New System.Drawing.Size(236, 18)
        '
        'frmDescargar
        '
        Me.frmDescargar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.frmDescargar.Location = New System.Drawing.Point(122, 272)
        Me.frmDescargar.Name = "frmDescargar"
        Me.frmDescargar.Size = New System.Drawing.Size(115, 18)
        Me.frmDescargar.TabIndex = 13
        Me.frmDescargar.Text = "F4) Descargar"
        '
        'frmABAST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.frmDescargar)
        Me.Controls.Add(Me.lblTareaCompleta)
        Me.Controls.Add(Me.txtConf)
        Me.Controls.Add(Me.lblConf)
        Me.Controls.Add(Me.txtUbicacion)
        Me.Controls.Add(Me.lblUbicacionOrigen)
        Me.Controls.Add(Me.lstInformacion)
        Me.Controls.Add(Me.txtContenedora)
        Me.Controls.Add(Me.lblNroContenedora)
        Me.Controls.Add(Me.bSalir)
        Me.Controls.Add(Me.bCerrarCont)
        Me.Controls.Add(Me.bComenzar)
        Me.KeyPreview = True
        Me.Name = "frmABAST"
        Me.Text = "Abastecimiento"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents bComenzar As System.Windows.Forms.Button
    Friend WithEvents bCerrarCont As System.Windows.Forms.Button
    Friend WithEvents bSalir As System.Windows.Forms.Button
    Friend WithEvents lblNroContenedora As System.Windows.Forms.Label
    Friend WithEvents txtContenedora As System.Windows.Forms.TextBox
    Friend WithEvents lstInformacion As System.Windows.Forms.ListBox
    Friend WithEvents lblUbicacionOrigen As System.Windows.Forms.Label
    Friend WithEvents txtUbicacion As System.Windows.Forms.TextBox
    Friend WithEvents lblConf As System.Windows.Forms.Label
    Friend WithEvents txtConf As System.Windows.Forms.TextBox
    Friend WithEvents lblTareaCompleta As System.Windows.Forms.Label
    Friend WithEvents frmDescargar As System.Windows.Forms.Button
End Class
