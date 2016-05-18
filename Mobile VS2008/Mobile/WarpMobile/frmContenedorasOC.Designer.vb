<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmContenedorasOC
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
        Me.lblProducto = New System.Windows.Forms.Label
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.dtgContenedoras = New System.Windows.Forms.DataGrid
        Me.btnSalir = New System.Windows.Forms.Button
        Me.btnFinalizar = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblUnidad = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblProductoId = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblCantidadSolicitada = New System.Windows.Forms.Label
        Me.lblCantidadContenedoras = New System.Windows.Forms.Label
        Me.lblCantidadRestante = New System.Windows.Forms.Label
        Me.lblLote = New System.Windows.Forms.Label
        Me.lblPart = New System.Windows.Forms.Label
        Me.lblPartida = New System.Windows.Forms.Label
        Me.lblLoteProveedor = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblProducto
        '
        Me.lblProducto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblProducto.Location = New System.Drawing.Point(3, 2)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(74, 15)
        Me.lblProducto.Text = "Producto:"
        '
        'lblDescripcion
        '
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescripcion.Location = New System.Drawing.Point(3, 59)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(234, 20)
        '
        'dtgContenedoras
        '
        Me.dtgContenedoras.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dtgContenedoras.Location = New System.Drawing.Point(3, 177)
        Me.dtgContenedoras.Name = "dtgContenedoras"
        Me.dtgContenedoras.Size = New System.Drawing.Size(234, 88)
        Me.dtgContenedoras.TabIndex = 1
        '
        'btnSalir
        '
        Me.btnSalir.Location = New System.Drawing.Point(141, 271)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(64, 20)
        Me.btnSalir.TabIndex = 3
        Me.btnSalir.Text = "F2) Salir"
        '
        'btnFinalizar
        '
        Me.btnFinalizar.Location = New System.Drawing.Point(38, 271)
        Me.btnFinalizar.Name = "btnFinalizar"
        Me.btnFinalizar.Size = New System.Drawing.Size(84, 20)
        Me.btnFinalizar.TabIndex = 2
        Me.btnFinalizar.Text = "F1) Finalizar"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(109, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 15)
        Me.Label1.Text = "Unidad:"
        '
        'lblUnidad
        '
        Me.lblUnidad.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblUnidad.Location = New System.Drawing.Point(156, 2)
        Me.lblUnidad.Name = "lblUnidad"
        Me.lblUnidad.Size = New System.Drawing.Size(81, 15)
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(3, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 15)
        Me.Label2.Text = "Descripción:"
        '
        'lblProductoId
        '
        Me.lblProductoId.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblProductoId.Location = New System.Drawing.Point(3, 17)
        Me.lblProductoId.Name = "lblProductoId"
        Me.lblProductoId.Size = New System.Drawing.Size(234, 20)
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(3, 122)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 15)
        Me.Label3.Text = "Cantidad Solicitada:"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(3, 140)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(166, 15)
        Me.Label4.Text = "Cantidad en Contenedoras:"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(3, 158)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(121, 15)
        Me.Label5.Text = "Diferencia:"
        '
        'lblCantidadSolicitada
        '
        Me.lblCantidadSolicitada.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblCantidadSolicitada.Location = New System.Drawing.Point(187, 122)
        Me.lblCantidadSolicitada.Name = "lblCantidadSolicitada"
        Me.lblCantidadSolicitada.Size = New System.Drawing.Size(50, 15)
        '
        'lblCantidadContenedoras
        '
        Me.lblCantidadContenedoras.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblCantidadContenedoras.Location = New System.Drawing.Point(187, 140)
        Me.lblCantidadContenedoras.Name = "lblCantidadContenedoras"
        Me.lblCantidadContenedoras.Size = New System.Drawing.Size(50, 15)
        '
        'lblCantidadRestante
        '
        Me.lblCantidadRestante.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblCantidadRestante.Location = New System.Drawing.Point(187, 158)
        Me.lblCantidadRestante.Name = "lblCantidadRestante"
        Me.lblCantidadRestante.Size = New System.Drawing.Size(50, 15)
        '
        'lblLote
        '
        Me.lblLote.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblLote.Location = New System.Drawing.Point(3, 103)
        Me.lblLote.Name = "lblLote"
        Me.lblLote.Size = New System.Drawing.Size(100, 15)
        Me.lblLote.Text = "Lote Proveedor:"
        '
        'lblPart
        '
        Me.lblPart.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblPart.Location = New System.Drawing.Point(3, 84)
        Me.lblPart.Name = "lblPart"
        Me.lblPart.Size = New System.Drawing.Size(100, 15)
        Me.lblPart.Text = "Partida:"
        '
        'lblPartida
        '
        Me.lblPartida.Location = New System.Drawing.Point(105, 84)
        Me.lblPartida.Name = "lblPartida"
        Me.lblPartida.Size = New System.Drawing.Size(100, 15)
        '
        'lblLoteProveedor
        '
        Me.lblLoteProveedor.Location = New System.Drawing.Point(105, 103)
        Me.lblLoteProveedor.Name = "lblLoteProveedor"
        Me.lblLoteProveedor.Size = New System.Drawing.Size(100, 15)
        '
        'FrmContenedorasOC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.lblLoteProveedor)
        Me.Controls.Add(Me.lblPartida)
        Me.Controls.Add(Me.lblPart)
        Me.Controls.Add(Me.lblLote)
        Me.Controls.Add(Me.lblCantidadRestante)
        Me.Controls.Add(Me.lblCantidadContenedoras)
        Me.Controls.Add(Me.lblCantidadSolicitada)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblProductoId)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblUnidad)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnFinalizar)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.dtgContenedoras)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.lblProducto)
        Me.Name = "FrmContenedorasOC"
        Me.Text = "Configuración de Contenedoras"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Private WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents dtgContenedoras As System.Windows.Forms.DataGrid
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents btnFinalizar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblUnidad As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblProductoId As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblCantidadSolicitada As System.Windows.Forms.Label
    Friend WithEvents lblCantidadContenedoras As System.Windows.Forms.Label
    Friend WithEvents lblCantidadRestante As System.Windows.Forms.Label
    Friend WithEvents lblLote As System.Windows.Forms.Label
    Friend WithEvents lblPart As System.Windows.Forms.Label
    Friend WithEvents lblPartida As System.Windows.Forms.Label
    Friend WithEvents lblLoteProveedor As System.Windows.Forms.Label
End Class
