<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmABASTDescarga
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
        Me.lblNroCarro = New System.Windows.Forms.Label
        Me.txtNroContenedora = New System.Windows.Forms.TextBox
        Me.lst = New System.Windows.Forms.ListBox
        Me.lblCodProducto = New System.Windows.Forms.Label
        Me.txtCodProducto = New System.Windows.Forms.TextBox
        Me.lblUbicacion = New System.Windows.Forms.Label
        Me.txtUbicacion = New System.Windows.Forms.TextBox
        Me.cmdComenzar = New System.Windows.Forms.Button
        Me.cmdPendientes = New System.Windows.Forms.Button
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblNroCarro
        '
        Me.lblNroCarro.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNroCarro.Location = New System.Drawing.Point(0, 3)
        Me.lblNroCarro.Name = "lblNroCarro"
        Me.lblNroCarro.Size = New System.Drawing.Size(138, 20)
        Me.lblNroCarro.Text = "Nro. de Contenedora:"
        '
        'txtNroContenedora
        '
        Me.txtNroContenedora.Location = New System.Drawing.Point(137, 3)
        Me.txtNroContenedora.MaxLength = 10
        Me.txtNroContenedora.Name = "txtNroContenedora"
        Me.txtNroContenedora.Size = New System.Drawing.Size(100, 21)
        Me.txtNroContenedora.TabIndex = 1
        '
        'lst
        '
        Me.lst.Location = New System.Drawing.Point(0, 52)
        Me.lst.Name = "lst"
        Me.lst.Size = New System.Drawing.Size(237, 128)
        Me.lst.TabIndex = 2
        '
        'lblCodProducto
        '
        Me.lblCodProducto.Location = New System.Drawing.Point(0, 28)
        Me.lblCodProducto.Name = "lblCodProducto"
        Me.lblCodProducto.Size = New System.Drawing.Size(90, 20)
        Me.lblCodProducto.Text = "Cod. Producto:"
        '
        'txtCodProducto
        '
        Me.txtCodProducto.Location = New System.Drawing.Point(96, 27)
        Me.txtCodProducto.MaxLength = 55
        Me.txtCodProducto.Name = "txtCodProducto"
        Me.txtCodProducto.Size = New System.Drawing.Size(141, 21)
        Me.txtCodProducto.TabIndex = 4
        '
        'lblUbicacion
        '
        Me.lblUbicacion.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblUbicacion.Location = New System.Drawing.Point(0, 183)
        Me.lblUbicacion.Name = "lblUbicacion"
        Me.lblUbicacion.Size = New System.Drawing.Size(237, 20)
        Me.lblUbicacion.Text = "Ubicacion:"
        '
        'txtUbicacion
        '
        Me.txtUbicacion.Location = New System.Drawing.Point(0, 205)
        Me.txtUbicacion.MaxLength = 45
        Me.txtUbicacion.Name = "txtUbicacion"
        Me.txtUbicacion.Size = New System.Drawing.Size(237, 21)
        Me.txtUbicacion.TabIndex = 6
        '
        'cmdComenzar
        '
        Me.cmdComenzar.Location = New System.Drawing.Point(0, 245)
        Me.cmdComenzar.Name = "cmdComenzar"
        Me.cmdComenzar.Size = New System.Drawing.Size(118, 20)
        Me.cmdComenzar.TabIndex = 7
        Me.cmdComenzar.Text = "F1) Comenzar"
        '
        'cmdPendientes
        '
        Me.cmdPendientes.Location = New System.Drawing.Point(119, 245)
        Me.cmdPendientes.Name = "cmdPendientes"
        Me.cmdPendientes.Size = New System.Drawing.Size(118, 20)
        Me.cmdPendientes.TabIndex = 8
        Me.cmdPendientes.Text = "F2) Ver Pendientes"
        '
        'cmdSalir
        '
        Me.cmdSalir.Location = New System.Drawing.Point(0, 266)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(118, 20)
        Me.cmdSalir.TabIndex = 9
        Me.cmdSalir.Text = "F3) Salir"
        '
        'frmABASTDescarga
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdPendientes)
        Me.Controls.Add(Me.cmdComenzar)
        Me.Controls.Add(Me.txtUbicacion)
        Me.Controls.Add(Me.lblUbicacion)
        Me.Controls.Add(Me.txtCodProducto)
        Me.Controls.Add(Me.lblCodProducto)
        Me.Controls.Add(Me.lst)
        Me.Controls.Add(Me.txtNroContenedora)
        Me.Controls.Add(Me.lblNroCarro)
        Me.KeyPreview = True
        Me.Name = "frmABASTDescarga"
        Me.Text = "Descarga"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblNroCarro As System.Windows.Forms.Label
    Friend WithEvents txtNroContenedora As System.Windows.Forms.TextBox
    Friend WithEvents lst As System.Windows.Forms.ListBox
    Friend WithEvents lblCodProducto As System.Windows.Forms.Label
    Friend WithEvents txtCodProducto As System.Windows.Forms.TextBox
    Friend WithEvents lblUbicacion As System.Windows.Forms.Label
    Friend WithEvents txtUbicacion As System.Windows.Forms.TextBox
    Friend WithEvents cmdComenzar As System.Windows.Forms.Button
    Friend WithEvents cmdPendientes As System.Windows.Forms.Button
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
End Class
