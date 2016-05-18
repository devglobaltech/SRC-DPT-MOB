<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmInventarioObservaciones
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
        Me.LblInventario = New System.Windows.Forms.Label
        Me.txtObservaciones = New System.Windows.Forms.TextBox
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.cmdComenzar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'LblInventario
        '
        Me.LblInventario.BackColor = System.Drawing.SystemColors.Control
        Me.LblInventario.Location = New System.Drawing.Point(3, 2)
        Me.LblInventario.Name = "LblInventario"
        Me.LblInventario.Size = New System.Drawing.Size(232, 21)
        Me.LblInventario.Text = "Inventario:"
        '
        'txtObservaciones
        '
        Me.txtObservaciones.Location = New System.Drawing.Point(3, 26)
        Me.txtObservaciones.MaxLength = 2000
        Me.txtObservaciones.Multiline = True
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.Size = New System.Drawing.Size(232, 233)
        Me.txtObservaciones.TabIndex = 2
        '
        'cmdCancelar
        '
        Me.cmdCancelar.BackColor = System.Drawing.Color.White
        Me.cmdCancelar.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCancelar.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdCancelar.Location = New System.Drawing.Point(126, 262)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(109, 26)
        Me.cmdCancelar.TabIndex = 24
        Me.cmdCancelar.Text = "F2) Volver"
        '
        'cmdComenzar
        '
        Me.cmdComenzar.BackColor = System.Drawing.Color.White
        Me.cmdComenzar.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdComenzar.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdComenzar.Location = New System.Drawing.Point(3, 262)
        Me.cmdComenzar.Name = "cmdComenzar"
        Me.cmdComenzar.Size = New System.Drawing.Size(109, 26)
        Me.cmdComenzar.TabIndex = 23
        Me.cmdComenzar.Text = "F1) Confirmar"
        '
        'frmInventarioObservaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdComenzar)
        Me.Controls.Add(Me.txtObservaciones)
        Me.Controls.Add(Me.LblInventario)
        Me.KeyPreview = True
        Me.Name = "frmInventarioObservaciones"
        Me.Text = "Observaciones"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblInventario As System.Windows.Forms.Label
    Friend WithEvents txtObservaciones As System.Windows.Forms.TextBox
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents cmdComenzar As System.Windows.Forms.Button
End Class
