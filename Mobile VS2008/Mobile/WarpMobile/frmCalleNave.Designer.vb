<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmCalleNave
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
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.cmdAcepter = New System.Windows.Forms.Button
        Me.lblNaveCalle = New System.Windows.Forms.Label
        Me.txtCalleNave = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'cmdSalir
        '
        Me.cmdSalir.Location = New System.Drawing.Point(8, 122)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(111, 18)
        Me.cmdSalir.TabIndex = 9
        Me.cmdSalir.Text = "Salir F3"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.Location = New System.Drawing.Point(124, 98)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(111, 18)
        Me.cmdCancelar.TabIndex = 8
        Me.cmdCancelar.Text = "Cancelar F2"
        '
        'cmdAcepter
        '
        Me.cmdAcepter.Location = New System.Drawing.Point(8, 98)
        Me.cmdAcepter.Name = "cmdAcepter"
        Me.cmdAcepter.Size = New System.Drawing.Size(111, 18)
        Me.cmdAcepter.TabIndex = 7
        Me.cmdAcepter.Text = "Aceptar F1"
        '
        'lblNaveCalle
        '
        Me.lblNaveCalle.Location = New System.Drawing.Point(8, 12)
        Me.lblNaveCalle.Name = "lblNaveCalle"
        Me.lblNaveCalle.Size = New System.Drawing.Size(228, 22)
        Me.lblNaveCalle.Text = "Escanee la Nave / Calle"
        '
        'txtCalleNave
        '
        Me.txtCalleNave.Location = New System.Drawing.Point(7, 37)
        Me.txtCalleNave.MaxLength = 50
        Me.txtCalleNave.Name = "txtCalleNave"
        Me.txtCalleNave.Size = New System.Drawing.Size(228, 21)
        Me.txtCalleNave.TabIndex = 6
        '
        'frmCalleNave
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdAcepter)
        Me.Controls.Add(Me.lblNaveCalle)
        Me.Controls.Add(Me.txtCalleNave)
        Me.KeyPreview = True
        Me.Name = "frmCalleNave"
        Me.Text = "Nave-Calle"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents cmdAcepter As System.Windows.Forms.Button
    Friend WithEvents lblNaveCalle As System.Windows.Forms.Label
    Friend WithEvents txtCalleNave As System.Windows.Forms.TextBox
End Class
