<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmValidaLote
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
        Me.lblViaje = New System.Windows.Forms.Label
        Me.lblProducto = New System.Windows.Forms.Label
        Me.lblLote = New System.Windows.Forms.Label
        Me.txtLote = New System.Windows.Forms.TextBox
        Me.cmdAceptar = New System.Windows.Forms.Button
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.lblNroLote = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblViaje
        '
        Me.lblViaje.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblViaje.Location = New System.Drawing.Point(6, 11)
        Me.lblViaje.Name = "lblViaje"
        Me.lblViaje.Size = New System.Drawing.Size(231, 17)
        Me.lblViaje.Text = "Picking / Viaje: "
        '
        'lblProducto
        '
        Me.lblProducto.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblProducto.Location = New System.Drawing.Point(6, 28)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(231, 50)
        Me.lblProducto.Text = "Producto: "
        '
        'lblLote
        '
        Me.lblLote.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblLote.Location = New System.Drawing.Point(6, 125)
        Me.lblLote.Name = "lblLote"
        Me.lblLote.Size = New System.Drawing.Size(231, 14)
        Me.lblLote.Text = "Ingrese el Numero de Lote:"
        '
        'txtLote
        '
        Me.txtLote.Location = New System.Drawing.Point(6, 141)
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Size = New System.Drawing.Size(231, 21)
        Me.txtLote.TabIndex = 3
        '
        'cmdAceptar
        '
        Me.cmdAceptar.Location = New System.Drawing.Point(6, 223)
        Me.cmdAceptar.Name = "cmdAceptar"
        Me.cmdAceptar.Size = New System.Drawing.Size(112, 14)
        Me.cmdAceptar.TabIndex = 4
        Me.cmdAceptar.Text = "F1) Aceptar"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.Location = New System.Drawing.Point(124, 223)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(112, 14)
        Me.cmdCancelar.TabIndex = 5
        Me.cmdCancelar.Text = "F2) Cancelar"
        '
        'cmdSalir
        '
        Me.cmdSalir.Location = New System.Drawing.Point(6, 243)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(112, 14)
        Me.cmdSalir.TabIndex = 6
        Me.cmdSalir.Text = "F3) Salir"
        '
        'lblNroLote
        '
        Me.lblNroLote.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNroLote.ForeColor = System.Drawing.Color.Black
        Me.lblNroLote.Location = New System.Drawing.Point(6, 82)
        Me.lblNroLote.Name = "lblNroLote"
        Me.lblNroLote.Size = New System.Drawing.Size(230, 39)
        Me.lblNroLote.Text = "Lote Proveedor:"
        '
        'FrmValidaLote
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.lblNroLote)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdAceptar)
        Me.Controls.Add(Me.txtLote)
        Me.Controls.Add(Me.lblLote)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.lblViaje)
        Me.KeyPreview = True
        Me.Name = "FrmValidaLote"
        Me.Text = "Validacion Lote"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblViaje As System.Windows.Forms.Label
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents lblLote As System.Windows.Forms.Label
    Friend WithEvents txtLote As System.Windows.Forms.TextBox
    Friend WithEvents cmdAceptar As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents lblNroLote As System.Windows.Forms.Label
End Class
