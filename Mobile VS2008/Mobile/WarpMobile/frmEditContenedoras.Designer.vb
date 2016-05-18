<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmEditContenedoras
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCantContenedora = New System.Windows.Forms.TextBox
        Me.lblContenedora = New System.Windows.Forms.Label
        Me.btnActualizar = New System.Windows.Forms.Button
        Me.btnAtrasVolver = New System.Windows.Forms.Button
        Me.lblProductoId = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.lblProducto = New System.Windows.Forms.Label
        Me.lblUnidad = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(6, 147)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 20)
        Me.Label1.Text = "Contenedora:"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(6, 174)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 20)
        Me.Label2.Text = "Cantidad:"
        '
        'txtCantContenedora
        '
        Me.txtCantContenedora.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtCantContenedora.Location = New System.Drawing.Point(119, 175)
        Me.txtCantContenedora.Name = "txtCantContenedora"
        Me.txtCantContenedora.Size = New System.Drawing.Size(81, 19)
        Me.txtCantContenedora.TabIndex = 4
        '
        'lblContenedora
        '
        Me.lblContenedora.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblContenedora.Location = New System.Drawing.Point(119, 147)
        Me.lblContenedora.Name = "lblContenedora"
        Me.lblContenedora.Size = New System.Drawing.Size(81, 20)
        '
        'btnActualizar
        '
        Me.btnActualizar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnActualizar.Location = New System.Drawing.Point(6, 207)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(104, 20)
        Me.btnActualizar.TabIndex = 9
        Me.btnActualizar.Text = "F1) Actualizar"
        '
        'btnAtrasVolver
        '
        Me.btnAtrasVolver.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnAtrasVolver.Location = New System.Drawing.Point(119, 207)
        Me.btnAtrasVolver.Name = "btnAtrasVolver"
        Me.btnAtrasVolver.Size = New System.Drawing.Size(107, 20)
        Me.btnAtrasVolver.TabIndex = 10
        Me.btnAtrasVolver.Text = "F2) Salir"
        '
        'lblProductoId
        '
        Me.lblProductoId.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblProductoId.Location = New System.Drawing.Point(6, 25)
        Me.lblProductoId.Name = "lblProductoId"
        Me.lblProductoId.Size = New System.Drawing.Size(220, 20)
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(6, 83)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 20)
        Me.Label4.Text = "Descripción:"
        '
        'lblDescripcion
        '
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescripcion.Location = New System.Drawing.Point(6, 103)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(220, 42)
        '
        'lblProducto
        '
        Me.lblProducto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblProducto.Location = New System.Drawing.Point(6, 6)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(104, 20)
        Me.lblProducto.Text = "Producto:"
        '
        'lblUnidad
        '
        Me.lblUnidad.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblUnidad.Location = New System.Drawing.Point(119, 58)
        Me.lblUnidad.Name = "lblUnidad"
        Me.lblUnidad.Size = New System.Drawing.Size(118, 22)
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(6, 54)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 22)
        Me.Label5.Text = "Unidad:"
        '
        'FrmEditContenedoras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 296)
        Me.Controls.Add(Me.lblProductoId)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblUnidad)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.btnAtrasVolver)
        Me.Controls.Add(Me.btnActualizar)
        Me.Controls.Add(Me.lblContenedora)
        Me.Controls.Add(Me.txtCantContenedora)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmEditContenedoras"
        Me.Text = "Modifcar Cantidad Contenedora"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCantContenedora As System.Windows.Forms.TextBox
    Friend WithEvents lblContenedora As System.Windows.Forms.Label
    Friend WithEvents btnActualizar As System.Windows.Forms.Button
    Friend WithEvents btnAtrasVolver As System.Windows.Forms.Button
    Friend WithEvents lblProductoId As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents lblUnidad As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
