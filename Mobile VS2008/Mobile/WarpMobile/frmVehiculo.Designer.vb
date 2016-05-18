<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmVehiculo
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
        Me.txtVehiculo = New System.Windows.Forms.TextBox
        Me.lblVehiculo = New System.Windows.Forms.Label
        Me.cmdAcepter = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'txtVehiculo
        '
        Me.txtVehiculo.Location = New System.Drawing.Point(3, 37)
        Me.txtVehiculo.MaxLength = 50
        Me.txtVehiculo.Name = "txtVehiculo"
        Me.txtVehiculo.Size = New System.Drawing.Size(228, 21)
        Me.txtVehiculo.TabIndex = 0
        '
        'lblVehiculo
        '
        Me.lblVehiculo.Location = New System.Drawing.Point(3, 12)
        Me.lblVehiculo.Name = "lblVehiculo"
        Me.lblVehiculo.Size = New System.Drawing.Size(228, 22)
        Me.lblVehiculo.Text = "Escanee el codigo de Vehiculo"
        '
        'cmdAcepter
        '
        Me.cmdAcepter.Location = New System.Drawing.Point(3, 98)
        Me.cmdAcepter.Name = "cmdAcepter"
        Me.cmdAcepter.Size = New System.Drawing.Size(111, 18)
        Me.cmdAcepter.TabIndex = 2
        Me.cmdAcepter.Text = "Aceptar F1"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(120, 98)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(111, 18)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Cancelar F2"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(3, 122)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(111, 18)
        Me.Button3.TabIndex = 4
        Me.Button3.Text = "Salir F3"
        '
        'frmVehiculo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.cmdAcepter)
        Me.Controls.Add(Me.lblVehiculo)
        Me.Controls.Add(Me.txtVehiculo)
        Me.KeyPreview = True
        Me.Name = "frmVehiculo"
        Me.Text = "Escanne el Vehiculo"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtVehiculo As System.Windows.Forms.TextBox
    Friend WithEvents lblVehiculo As System.Windows.Forms.Label
    Friend WithEvents cmdAcepter As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
End Class
