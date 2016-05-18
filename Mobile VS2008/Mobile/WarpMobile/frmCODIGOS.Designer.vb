<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmCODIGOS
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
        Me.lblTit = New System.Windows.Forms.Label
        Me.txtCod = New System.Windows.Forms.TextBox
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.bntSalir = New System.Windows.Forms.Button
        Me.lblProducto_ID = New System.Windows.Forms.Label
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblTit
        '
        Me.lblTit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTit.Location = New System.Drawing.Point(3, 22)
        Me.lblTit.Name = "lblTit"
        Me.lblTit.Size = New System.Drawing.Size(236, 36)
        Me.lblTit.Text = "Escanee el codigo EAN13 o DUN14"
        '
        'txtCod
        '
        Me.txtCod.Location = New System.Drawing.Point(4, 120)
        Me.txtCod.Name = "txtCod"
        Me.txtCod.Size = New System.Drawing.Size(234, 21)
        Me.txtCod.TabIndex = 1
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(3, 168)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(106, 17)
        Me.btnAceptar.TabIndex = 2
        Me.btnAceptar.Text = "Aceptar F1"
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(131, 168)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(106, 17)
        Me.btnCancelar.TabIndex = 3
        Me.btnCancelar.Text = "Cancelar F2"
        '
        'bntSalir
        '
        Me.bntSalir.Location = New System.Drawing.Point(3, 191)
        Me.bntSalir.Name = "bntSalir"
        Me.bntSalir.Size = New System.Drawing.Size(106, 17)
        Me.bntSalir.TabIndex = 5
        Me.bntSalir.Text = "Salir F3"
        '
        'lblProducto_ID
        '
        Me.lblProducto_ID.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblProducto_ID.Location = New System.Drawing.Point(3, 48)
        Me.lblProducto_ID.Name = "lblProducto_ID"
        Me.lblProducto_ID.Size = New System.Drawing.Size(234, 29)
        Me.lblProducto_ID.Text = "Label1"
        '
        'lblDescripcion
        '
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescripcion.Location = New System.Drawing.Point(4, 81)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(233, 36)
        Me.lblDescripcion.Text = "Label1"
        '
        'frmCODIGOS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.lblProducto_ID)
        Me.Controls.Add(Me.bntSalir)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.txtCod)
        Me.Controls.Add(Me.lblTit)
        Me.KeyPreview = True
        Me.Name = "frmCODIGOS"
        Me.Text = "Control de Codigos."
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTit As System.Windows.Forms.Label
    Friend WithEvents txtCod As System.Windows.Forms.TextBox
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents bntSalir As System.Windows.Forms.Button
    Friend WithEvents lblProducto_ID As System.Windows.Forms.Label
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
End Class
