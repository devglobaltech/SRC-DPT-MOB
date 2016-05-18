<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmContenedorasPicking
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
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.lblUnidad = New System.Windows.Forms.Label
        Me.lblProductoId = New System.Windows.Forms.Label
        Me.dtgUbicContenedoras = New System.Windows.Forms.DataGrid
        Me.btnVolver = New System.Windows.Forms.Button
        Me.lblContenedora = New System.Windows.Forms.Label
        Me.lblCantidadRestante = New System.Windows.Forms.Label
        Me.lblCantidadSolicitada = New System.Windows.Forms.Label
        Me.lblCantidadPickeada = New System.Windows.Forms.Label
        Me.btn_cambio = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(3, 111)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 20)
        Me.Label5.Text = "Diferencia:"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(3, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(129, 20)
        Me.Label3.Text = "Cantidad Pickeada:"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(3, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 20)
        Me.Label2.Text = "Cantidad Solicitada:"
        '
        'lblDescripcion
        '
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDescripcion.Location = New System.Drawing.Point(3, 45)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(237, 26)
        Me.lblDescripcion.Text = "Descripción:"
        '
        'lblUnidad
        '
        Me.lblUnidad.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblUnidad.Location = New System.Drawing.Point(3, 25)
        Me.lblUnidad.Name = "lblUnidad"
        Me.lblUnidad.Size = New System.Drawing.Size(233, 20)
        Me.lblUnidad.Text = "Unidad:"
        '
        'lblProductoId
        '
        Me.lblProductoId.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblProductoId.Location = New System.Drawing.Point(3, 3)
        Me.lblProductoId.Name = "lblProductoId"
        Me.lblProductoId.Size = New System.Drawing.Size(233, 20)
        Me.lblProductoId.Text = "Producto:"
        '
        'dtgUbicContenedoras
        '
        Me.dtgUbicContenedoras.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dtgUbicContenedoras.Location = New System.Drawing.Point(4, 134)
        Me.dtgUbicContenedoras.Name = "dtgUbicContenedoras"
        Me.dtgUbicContenedoras.Size = New System.Drawing.Size(234, 132)
        Me.dtgUbicContenedoras.TabIndex = 24
        '
        'btnVolver
        '
        Me.btnVolver.Location = New System.Drawing.Point(122, 271)
        Me.btnVolver.Name = "btnVolver"
        Me.btnVolver.Size = New System.Drawing.Size(115, 20)
        Me.btnVolver.TabIndex = 25
        Me.btnVolver.Text = "F2) Volver"
        '
        'lblContenedora
        '
        Me.lblContenedora.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblContenedora.Location = New System.Drawing.Point(116, 111)
        Me.lblContenedora.Name = "lblContenedora"
        Me.lblContenedora.Size = New System.Drawing.Size(120, 20)
        Me.lblContenedora.Text = "Contenedora:"
        '
        'lblCantidadRestante
        '
        Me.lblCantidadRestante.Location = New System.Drawing.Point(71, 111)
        Me.lblCantidadRestante.Name = "lblCantidadRestante"
        Me.lblCantidadRestante.Size = New System.Drawing.Size(45, 20)
        '
        'lblCantidadSolicitada
        '
        Me.lblCantidadSolicitada.Location = New System.Drawing.Point(128, 71)
        Me.lblCantidadSolicitada.Name = "lblCantidadSolicitada"
        Me.lblCantidadSolicitada.Size = New System.Drawing.Size(73, 20)
        '
        'lblCantidadPickeada
        '
        Me.lblCantidadPickeada.Location = New System.Drawing.Point(128, 89)
        Me.lblCantidadPickeada.Name = "lblCantidadPickeada"
        Me.lblCantidadPickeada.Size = New System.Drawing.Size(73, 22)
        '
        'btn_cambio
        '
        Me.btn_cambio.Location = New System.Drawing.Point(4, 271)
        Me.btn_cambio.Name = "btn_cambio"
        Me.btn_cambio.Size = New System.Drawing.Size(112, 20)
        Me.btn_cambio.TabIndex = 32
        Me.btn_cambio.Text = "F1) Cambio"
        '
        'frmContenedorasPicking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btn_cambio)
        Me.Controls.Add(Me.lblCantidadPickeada)
        Me.Controls.Add(Me.lblCantidadSolicitada)
        Me.Controls.Add(Me.lblCantidadRestante)
        Me.Controls.Add(Me.lblContenedora)
        Me.Controls.Add(Me.btnVolver)
        Me.Controls.Add(Me.dtgUbicContenedoras)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.lblUnidad)
        Me.Controls.Add(Me.lblProductoId)
        Me.KeyPreview = True
        Me.Name = "frmContenedorasPicking"
        Me.Text = "Ubicación de Contenedoras"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents lblUnidad As System.Windows.Forms.Label
    Friend WithEvents lblProductoId As System.Windows.Forms.Label
    Friend WithEvents dtgUbicContenedoras As System.Windows.Forms.DataGrid
    Friend WithEvents btnVolver As System.Windows.Forms.Button
    Friend WithEvents lblContenedora As System.Windows.Forms.Label
    Friend WithEvents lblCantidadRestante As System.Windows.Forms.Label
    Friend WithEvents lblCantidadSolicitada As System.Windows.Forms.Label
    Friend WithEvents lblCantidadPickeada As System.Windows.Forms.Label
    Friend WithEvents btn_cambio As System.Windows.Forms.Button
End Class
