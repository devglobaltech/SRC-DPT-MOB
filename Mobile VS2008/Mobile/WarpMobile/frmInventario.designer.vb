<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmInventario
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
        Me.LblInventario = New System.Windows.Forms.Label
        Me.LblPosicion = New System.Windows.Forms.Label
        Me.TxtPosicion = New System.Windows.Forms.TextBox
        Me.LblTitCliente = New System.Windows.Forms.Label
        Me.LblTitProd = New System.Windows.Forms.Label
        Me.LblTitUnidad = New System.Windows.Forms.Label
        Me.LblCant = New System.Windows.Forms.Label
        Me.TxtProducto = New System.Windows.Forms.TextBox
        Me.TxtCantidad = New System.Windows.Forms.TextBox
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.cmdComenzar = New System.Windows.Forms.Button
        Me.cmdNuevoMarbete = New System.Windows.Forms.Button
        Me.LblCliente = New System.Windows.Forms.Label
        Me.LblUnidad = New System.Windows.Forms.Label
        Me.LblProd = New System.Windows.Forms.Label
        Me.LblDescripcion = New System.Windows.Forms.Label
        Me.Lbl_prop_adic = New System.Windows.Forms.Label
        Me.cmdObservaciones = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'LblInventario
        '
        Me.LblInventario.BackColor = System.Drawing.SystemColors.Control
        Me.LblInventario.Location = New System.Drawing.Point(3, 2)
        Me.LblInventario.Name = "LblInventario"
        Me.LblInventario.Size = New System.Drawing.Size(232, 21)
        Me.LblInventario.Text = "Inventario:"
        Me.LblInventario.Visible = False
        '
        'LblPosicion
        '
        Me.LblPosicion.BackColor = System.Drawing.SystemColors.Control
        Me.LblPosicion.Location = New System.Drawing.Point(3, 25)
        Me.LblPosicion.Name = "LblPosicion"
        Me.LblPosicion.Size = New System.Drawing.Size(232, 22)
        Me.LblPosicion.Text = "Posicion:"
        Me.LblPosicion.Visible = False
        '
        'TxtPosicion
        '
        Me.TxtPosicion.Location = New System.Drawing.Point(3, 49)
        Me.TxtPosicion.Name = "TxtPosicion"
        Me.TxtPosicion.Size = New System.Drawing.Size(234, 21)
        Me.TxtPosicion.TabIndex = 2
        Me.TxtPosicion.Visible = False
        '
        'LblTitCliente
        '
        Me.LblTitCliente.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblTitCliente.Location = New System.Drawing.Point(3, 73)
        Me.LblTitCliente.Name = "LblTitCliente"
        Me.LblTitCliente.Size = New System.Drawing.Size(71, 17)
        Me.LblTitCliente.Text = "Cliente:"
        Me.LblTitCliente.Visible = False
        '
        'LblTitProd
        '
        Me.LblTitProd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblTitProd.Location = New System.Drawing.Point(3, 95)
        Me.LblTitProd.Name = "LblTitProd"
        Me.LblTitProd.Size = New System.Drawing.Size(69, 20)
        Me.LblTitProd.Text = "Producto:"
        Me.LblTitProd.Visible = False
        '
        'LblTitUnidad
        '
        Me.LblTitUnidad.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblTitUnidad.Location = New System.Drawing.Point(3, 199)
        Me.LblTitUnidad.Name = "LblTitUnidad"
        Me.LblTitUnidad.Size = New System.Drawing.Size(55, 17)
        Me.LblTitUnidad.Text = "Unidad:"
        Me.LblTitUnidad.Visible = False
        '
        'LblCant
        '
        Me.LblCant.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblCant.Location = New System.Drawing.Point(3, 223)
        Me.LblCant.Name = "LblCant"
        Me.LblCant.Size = New System.Drawing.Size(71, 17)
        Me.LblCant.Text = "Cantidad:"
        Me.LblCant.Visible = False
        '
        'TxtProducto
        '
        Me.TxtProducto.Location = New System.Drawing.Point(78, 93)
        Me.TxtProducto.Name = "TxtProducto"
        Me.TxtProducto.Size = New System.Drawing.Size(159, 21)
        Me.TxtProducto.TabIndex = 13
        Me.TxtProducto.Visible = False
        '
        'TxtCantidad
        '
        Me.TxtCantidad.Location = New System.Drawing.Point(90, 220)
        Me.TxtCantidad.MaxLength = 10
        Me.TxtCantidad.Name = "TxtCantidad"
        Me.TxtCantidad.Size = New System.Drawing.Size(147, 21)
        Me.TxtCantidad.TabIndex = 16
        Me.TxtCantidad.Visible = False
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.White
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdSalir.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdSalir.Location = New System.Drawing.Point(126, 259)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(109, 14)
        Me.cmdSalir.TabIndex = 24
        Me.cmdSalir.Text = "F4) Salir"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.BackColor = System.Drawing.Color.White
        Me.cmdCancelar.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCancelar.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdCancelar.Location = New System.Drawing.Point(126, 244)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(109, 14)
        Me.cmdCancelar.TabIndex = 22
        Me.cmdCancelar.Text = "F2) Cancelar"
        '
        'cmdComenzar
        '
        Me.cmdComenzar.BackColor = System.Drawing.Color.White
        Me.cmdComenzar.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdComenzar.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdComenzar.Location = New System.Drawing.Point(4, 244)
        Me.cmdComenzar.Name = "cmdComenzar"
        Me.cmdComenzar.Size = New System.Drawing.Size(109, 14)
        Me.cmdComenzar.TabIndex = 21
        Me.cmdComenzar.Text = "F1) Comenzar Inv."
        '
        'cmdNuevoMarbete
        '
        Me.cmdNuevoMarbete.BackColor = System.Drawing.Color.White
        Me.cmdNuevoMarbete.Enabled = False
        Me.cmdNuevoMarbete.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdNuevoMarbete.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdNuevoMarbete.Location = New System.Drawing.Point(4, 259)
        Me.cmdNuevoMarbete.Name = "cmdNuevoMarbete"
        Me.cmdNuevoMarbete.Size = New System.Drawing.Size(109, 14)
        Me.cmdNuevoMarbete.TabIndex = 23
        Me.cmdNuevoMarbete.Text = "F3) Nuevo Marb."
        '
        'LblCliente
        '
        Me.LblCliente.Location = New System.Drawing.Point(78, 73)
        Me.LblCliente.Name = "LblCliente"
        Me.LblCliente.Size = New System.Drawing.Size(157, 17)
        Me.LblCliente.Visible = False
        '
        'LblUnidad
        '
        Me.LblUnidad.Location = New System.Drawing.Point(90, 199)
        Me.LblUnidad.Name = "LblUnidad"
        Me.LblUnidad.Size = New System.Drawing.Size(145, 17)
        Me.LblUnidad.Visible = False
        '
        'LblProd
        '
        Me.LblProd.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.LblProd.Location = New System.Drawing.Point(3, 117)
        Me.LblProd.Name = "LblProd"
        Me.LblProd.Size = New System.Drawing.Size(70, 30)
        Me.LblProd.Visible = False
        '
        'LblDescripcion
        '
        Me.LblDescripcion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.LblDescripcion.Location = New System.Drawing.Point(78, 117)
        Me.LblDescripcion.Name = "LblDescripcion"
        Me.LblDescripcion.Size = New System.Drawing.Size(157, 30)
        '
        'Lbl_prop_adic
        '
        Me.Lbl_prop_adic.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Lbl_prop_adic.Location = New System.Drawing.Point(3, 153)
        Me.Lbl_prop_adic.Name = "Lbl_prop_adic"
        Me.Lbl_prop_adic.Size = New System.Drawing.Size(234, 45)
        '
        'cmdObservaciones
        '
        Me.cmdObservaciones.BackColor = System.Drawing.Color.White
        Me.cmdObservaciones.Enabled = False
        Me.cmdObservaciones.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdObservaciones.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdObservaciones.Location = New System.Drawing.Point(3, 274)
        Me.cmdObservaciones.Name = "cmdObservaciones"
        Me.cmdObservaciones.Size = New System.Drawing.Size(109, 14)
        Me.cmdObservaciones.TabIndex = 32
        Me.cmdObservaciones.Text = "F5) Observaciones"
        '
        'frmInventario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdObservaciones)
        Me.Controls.Add(Me.Lbl_prop_adic)
        Me.Controls.Add(Me.LblUnidad)
        Me.Controls.Add(Me.LblDescripcion)
        Me.Controls.Add(Me.LblProd)
        Me.Controls.Add(Me.LblCliente)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdComenzar)
        Me.Controls.Add(Me.cmdNuevoMarbete)
        Me.Controls.Add(Me.TxtCantidad)
        Me.Controls.Add(Me.TxtProducto)
        Me.Controls.Add(Me.LblCant)
        Me.Controls.Add(Me.LblTitUnidad)
        Me.Controls.Add(Me.LblTitProd)
        Me.Controls.Add(Me.LblTitCliente)
        Me.Controls.Add(Me.TxtPosicion)
        Me.Controls.Add(Me.LblPosicion)
        Me.Controls.Add(Me.LblInventario)
        Me.KeyPreview = True
        Me.Name = "frmInventario"
        Me.Text = "Inventario"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblInventario As System.Windows.Forms.Label
    Friend WithEvents LblPosicion As System.Windows.Forms.Label
    Friend WithEvents TxtPosicion As System.Windows.Forms.TextBox
    Friend WithEvents LblTitCliente As System.Windows.Forms.Label
    Friend WithEvents LblTitProd As System.Windows.Forms.Label
    Friend WithEvents LblTitUnidad As System.Windows.Forms.Label
    Friend WithEvents LblCant As System.Windows.Forms.Label
    Friend WithEvents TxtProducto As System.Windows.Forms.TextBox
    Friend WithEvents TxtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents cmdComenzar As System.Windows.Forms.Button
    Friend WithEvents cmdNuevoMarbete As System.Windows.Forms.Button
    Friend WithEvents LblCliente As System.Windows.Forms.Label
    Friend WithEvents LblUnidad As System.Windows.Forms.Label
    Friend WithEvents LblProd As System.Windows.Forms.Label
    Friend WithEvents LblDescripcion As System.Windows.Forms.Label
    Friend WithEvents Lbl_prop_adic As System.Windows.Forms.Label
    Friend WithEvents cmdObservaciones As System.Windows.Forms.Button
End Class
