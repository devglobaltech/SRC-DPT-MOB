<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmMarbete
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
        Me.LblCliente = New System.Windows.Forms.Label
        Me.LblProducto = New System.Windows.Forms.Label
        Me.LblTitDescripcion = New System.Windows.Forms.Label
        Me.LblUbicacion = New System.Windows.Forms.Label
        Me.CmbClientes = New System.Windows.Forms.ComboBox
        Me.TxtProducto = New System.Windows.Forms.TextBox
        Me.TxtUbicacion = New System.Windows.Forms.TextBox
        Me.LblTitObser = New System.Windows.Forms.Label
        Me.cmdCerrarPallet = New System.Windows.Forms.Button
        Me.cmdApertura = New System.Windows.Forms.Button
        Me.LblCant = New System.Windows.Forms.Label
        Me.TxtCant = New System.Windows.Forms.TextBox
        Me.TxtObservaciones = New System.Windows.Forms.TextBox
        Me.LblDescripcion = New System.Windows.Forms.Label
        Me.Txt_nro_partida = New System.Windows.Forms.TextBox
        Me.lbl_nro_partida = New System.Windows.Forms.Label
        Me.Txt_nro_lote = New System.Windows.Forms.TextBox
        Me.lbl_nro_lote = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'LblCliente
        '
        Me.LblCliente.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblCliente.Location = New System.Drawing.Point(3, 10)
        Me.LblCliente.Name = "LblCliente"
        Me.LblCliente.Size = New System.Drawing.Size(103, 17)
        Me.LblCliente.Text = "Cod Cliente:"
        '
        'LblProducto
        '
        Me.LblProducto.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblProducto.Location = New System.Drawing.Point(3, 35)
        Me.LblProducto.Name = "LblProducto"
        Me.LblProducto.Size = New System.Drawing.Size(84, 17)
        Me.LblProducto.Text = "Producto:"
        Me.LblProducto.Visible = False
        '
        'LblTitDescripcion
        '
        Me.LblTitDescripcion.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblTitDescripcion.Location = New System.Drawing.Point(3, 59)
        Me.LblTitDescripcion.Name = "LblTitDescripcion"
        Me.LblTitDescripcion.Size = New System.Drawing.Size(84, 19)
        Me.LblTitDescripcion.Text = "Descripcion:"
        Me.LblTitDescripcion.Visible = False
        '
        'LblUbicacion
        '
        Me.LblUbicacion.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblUbicacion.Location = New System.Drawing.Point(3, 157)
        Me.LblUbicacion.Name = "LblUbicacion"
        Me.LblUbicacion.Size = New System.Drawing.Size(76, 19)
        Me.LblUbicacion.Text = "Ubicacion:"
        Me.LblUbicacion.Visible = False
        '
        'CmbClientes
        '
        Me.CmbClientes.Location = New System.Drawing.Point(93, 10)
        Me.CmbClientes.Name = "CmbClientes"
        Me.CmbClientes.Size = New System.Drawing.Size(143, 22)
        Me.CmbClientes.TabIndex = 1
        '
        'TxtProducto
        '
        Me.TxtProducto.Location = New System.Drawing.Point(93, 35)
        Me.TxtProducto.Name = "TxtProducto"
        Me.TxtProducto.Size = New System.Drawing.Size(143, 21)
        Me.TxtProducto.TabIndex = 2
        Me.TxtProducto.Visible = False
        '
        'TxtUbicacion
        '
        Me.TxtUbicacion.Location = New System.Drawing.Point(93, 157)
        Me.TxtUbicacion.Name = "TxtUbicacion"
        Me.TxtUbicacion.Size = New System.Drawing.Size(144, 21)
        Me.TxtUbicacion.TabIndex = 5
        Me.TxtUbicacion.Visible = False
        '
        'LblTitObser
        '
        Me.LblTitObser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblTitObser.Location = New System.Drawing.Point(0, 184)
        Me.LblTitObser.Name = "LblTitObser"
        Me.LblTitObser.Size = New System.Drawing.Size(106, 19)
        Me.LblTitObser.Text = "Observaciones:"
        Me.LblTitObser.Visible = False
        '
        'cmdCerrarPallet
        '
        Me.cmdCerrarPallet.BackColor = System.Drawing.Color.White
        Me.cmdCerrarPallet.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCerrarPallet.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdCerrarPallet.Location = New System.Drawing.Point(127, 264)
        Me.cmdCerrarPallet.Name = "cmdCerrarPallet"
        Me.cmdCerrarPallet.Size = New System.Drawing.Size(109, 14)
        Me.cmdCerrarPallet.TabIndex = 24
        Me.cmdCerrarPallet.Text = "F2) Volver"
        '
        'cmdApertura
        '
        Me.cmdApertura.BackColor = System.Drawing.Color.White
        Me.cmdApertura.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdApertura.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdApertura.Location = New System.Drawing.Point(3, 264)
        Me.cmdApertura.Name = "cmdApertura"
        Me.cmdApertura.Size = New System.Drawing.Size(109, 14)
        Me.cmdApertura.TabIndex = 23
        Me.cmdApertura.Text = "F1) Cancelar"
        '
        'LblCant
        '
        Me.LblCant.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblCant.Location = New System.Drawing.Point(3, 232)
        Me.LblCant.Name = "LblCant"
        Me.LblCant.Size = New System.Drawing.Size(118, 29)
        Me.LblCant.Text = "Cantidad en su Unidad:"
        Me.LblCant.Visible = False
        '
        'TxtCant
        '
        Me.TxtCant.Location = New System.Drawing.Point(121, 231)
        Me.TxtCant.MaxLength = 10
        Me.TxtCant.Name = "TxtCant"
        Me.TxtCant.Size = New System.Drawing.Size(115, 21)
        Me.TxtCant.TabIndex = 7
        Me.TxtCant.Visible = False
        '
        'TxtObservaciones
        '
        Me.TxtObservaciones.Location = New System.Drawing.Point(120, 182)
        Me.TxtObservaciones.Multiline = True
        Me.TxtObservaciones.Name = "TxtObservaciones"
        Me.TxtObservaciones.Size = New System.Drawing.Size(116, 43)
        Me.TxtObservaciones.TabIndex = 6
        Me.TxtObservaciones.Visible = False
        '
        'LblDescripcion
        '
        Me.LblDescripcion.Location = New System.Drawing.Point(93, 59)
        Me.LblDescripcion.Name = "LblDescripcion"
        Me.LblDescripcion.Size = New System.Drawing.Size(143, 41)
        Me.LblDescripcion.Visible = False
        '
        'Txt_nro_partida
        '
        Me.Txt_nro_partida.Location = New System.Drawing.Point(93, 130)
        Me.Txt_nro_partida.Name = "Txt_nro_partida"
        Me.Txt_nro_partida.Size = New System.Drawing.Size(144, 21)
        Me.Txt_nro_partida.TabIndex = 4
        Me.Txt_nro_partida.Visible = False
        '
        'lbl_nro_partida
        '
        Me.lbl_nro_partida.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lbl_nro_partida.Location = New System.Drawing.Point(3, 130)
        Me.lbl_nro_partida.Name = "lbl_nro_partida"
        Me.lbl_nro_partida.Size = New System.Drawing.Size(84, 19)
        Me.lbl_nro_partida.Text = "Nro. Partida:"
        Me.lbl_nro_partida.Visible = False
        '
        'Txt_nro_lote
        '
        Me.Txt_nro_lote.Location = New System.Drawing.Point(93, 103)
        Me.Txt_nro_lote.Name = "Txt_nro_lote"
        Me.Txt_nro_lote.Size = New System.Drawing.Size(144, 21)
        Me.Txt_nro_lote.TabIndex = 3
        Me.Txt_nro_lote.Visible = False
        '
        'lbl_nro_lote
        '
        Me.lbl_nro_lote.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lbl_nro_lote.Location = New System.Drawing.Point(3, 103)
        Me.lbl_nro_lote.Name = "lbl_nro_lote"
        Me.lbl_nro_lote.Size = New System.Drawing.Size(76, 19)
        Me.lbl_nro_lote.Text = "Nro. Lote:"
        Me.lbl_nro_lote.Visible = False
        '
        'frmMarbete
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.Txt_nro_partida)
        Me.Controls.Add(Me.lbl_nro_partida)
        Me.Controls.Add(Me.Txt_nro_lote)
        Me.Controls.Add(Me.lbl_nro_lote)
        Me.Controls.Add(Me.LblDescripcion)
        Me.Controls.Add(Me.TxtObservaciones)
        Me.Controls.Add(Me.TxtCant)
        Me.Controls.Add(Me.cmdCerrarPallet)
        Me.Controls.Add(Me.cmdApertura)
        Me.Controls.Add(Me.LblTitObser)
        Me.Controls.Add(Me.TxtUbicacion)
        Me.Controls.Add(Me.TxtProducto)
        Me.Controls.Add(Me.CmbClientes)
        Me.Controls.Add(Me.LblUbicacion)
        Me.Controls.Add(Me.LblTitDescripcion)
        Me.Controls.Add(Me.LblProducto)
        Me.Controls.Add(Me.LblCliente)
        Me.Controls.Add(Me.LblCant)
        Me.KeyPreview = True
        Me.Name = "frmMarbete"
        Me.Text = "frmMarbete"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblCliente As System.Windows.Forms.Label
    Friend WithEvents LblProducto As System.Windows.Forms.Label
    Friend WithEvents LblTitDescripcion As System.Windows.Forms.Label
    Friend WithEvents LblUbicacion As System.Windows.Forms.Label
    Friend WithEvents CmbClientes As System.Windows.Forms.ComboBox
    Friend WithEvents TxtProducto As System.Windows.Forms.TextBox
    Friend WithEvents TxtUbicacion As System.Windows.Forms.TextBox
    Friend WithEvents LblTitObser As System.Windows.Forms.Label
    Friend WithEvents cmdCerrarPallet As System.Windows.Forms.Button
    Friend WithEvents cmdApertura As System.Windows.Forms.Button
    Friend WithEvents LblCant As System.Windows.Forms.Label
    Friend WithEvents TxtCant As System.Windows.Forms.TextBox
    Friend WithEvents TxtObservaciones As System.Windows.Forms.TextBox
    Friend WithEvents LblDescripcion As System.Windows.Forms.Label
    Friend WithEvents Txt_nro_partida As System.Windows.Forms.TextBox
    Friend WithEvents lbl_nro_partida As System.Windows.Forms.Label
    Friend WithEvents Txt_nro_lote As System.Windows.Forms.TextBox
    Friend WithEvents lbl_nro_lote As System.Windows.Forms.Label
End Class
