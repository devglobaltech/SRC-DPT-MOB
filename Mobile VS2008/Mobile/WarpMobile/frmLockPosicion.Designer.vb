<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmLockPosicion
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblMotivo = New System.Windows.Forms.Label
        Me.cmbMotivos = New System.Windows.Forms.ComboBox
        Me.lblUbicacion = New System.Windows.Forms.Label
        Me.txtLibre = New System.Windows.Forms.TextBox
        Me.lblObservaciones = New System.Windows.Forms.Label
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblMotivo
        '
        Me.lblMotivo.Location = New System.Drawing.Point(3, 35)
        Me.lblMotivo.Name = "lblMotivo"
        Me.lblMotivo.Size = New System.Drawing.Size(54, 19)
        Me.lblMotivo.Text = "Motivo"
        '
        'cmbMotivos
        '
        Me.cmbMotivos.Location = New System.Drawing.Point(63, 35)
        Me.cmbMotivos.Name = "cmbMotivos"
        Me.cmbMotivos.Size = New System.Drawing.Size(167, 22)
        Me.cmbMotivos.TabIndex = 1
        '
        'lblUbicacion
        '
        Me.lblUbicacion.Location = New System.Drawing.Point(3, 9)
        Me.lblUbicacion.Name = "lblUbicacion"
        Me.lblUbicacion.Size = New System.Drawing.Size(227, 23)
        Me.lblUbicacion.Text = "Ubicacion: "
        '
        'txtLibre
        '
        Me.txtLibre.Location = New System.Drawing.Point(3, 102)
        Me.txtLibre.MaxLength = 100
        Me.txtLibre.Multiline = True
        Me.txtLibre.Name = "txtLibre"
        Me.txtLibre.Size = New System.Drawing.Size(227, 84)
        Me.txtLibre.TabIndex = 3
        Me.txtLibre.Text = "Ingrese aqui mas informacion"
        '
        'lblObservaciones
        '
        Me.lblObservaciones.ForeColor = System.Drawing.Color.Black
        Me.lblObservaciones.Location = New System.Drawing.Point(3, 79)
        Me.lblObservaciones.Name = "lblObservaciones"
        Me.lblObservaciones.Size = New System.Drawing.Size(226, 20)
        Me.lblObservaciones.Text = "Observaciones"
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.White
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdSalir.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdSalir.Location = New System.Drawing.Point(160, 218)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(77, 20)
        Me.cmdSalir.TabIndex = 20
        Me.cmdSalir.Text = "Salir          F3"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.BackColor = System.Drawing.Color.White
        Me.cmdCancelar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCancelar.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdCancelar.Location = New System.Drawing.Point(81, 218)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(77, 20)
        Me.cmdCancelar.TabIndex = 19
        Me.cmdCancelar.Text = "Cancelar   F2"
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.Color.White
        Me.btnAceptar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnAceptar.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnAceptar.Location = New System.Drawing.Point(3, 218)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(77, 20)
        Me.btnAceptar.TabIndex = 21
        Me.btnAceptar.Text = "Aceptar F1"
        '
        'frmLockPosicion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.lblObservaciones)
        Me.Controls.Add(Me.txtLibre)
        Me.Controls.Add(Me.lblUbicacion)
        Me.Controls.Add(Me.cmbMotivos)
        Me.Controls.Add(Me.lblMotivo)
        Me.KeyPreview = True
        Me.Name = "frmLockPosicion"
        Me.Text = "frmLockPosicion"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblMotivo As System.Windows.Forms.Label
    Friend WithEvents cmbMotivos As System.Windows.Forms.ComboBox
    Friend WithEvents lblUbicacion As System.Windows.Forms.Label
    Friend WithEvents txtLibre As System.Windows.Forms.TextBox
    Friend WithEvents lblObservaciones As System.Windows.Forms.Label
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
End Class
