<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmRecepcionOC
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
        Me.cboCliente = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtOC = New System.Windows.Forms.TextBox
        Me.lblNOC = New System.Windows.Forms.Label
        Me.lblProducto = New System.Windows.Forms.Label
        Me.txtProducto = New System.Windows.Forms.TextBox
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.lblMsg = New System.Windows.Forms.Label
        Me.btnInicio = New System.Windows.Forms.Button
        Me.lblcantidad = New System.Windows.Forms.Label
        Me.txtCantidad = New System.Windows.Forms.TextBox
        Me.btnFin = New System.Windows.Forms.Button
        Me.btnIngresados = New System.Windows.Forms.Button
        Me.btnAtrasVolver = New System.Windows.Forms.Button
        Me.LblTitDescripcion = New System.Windows.Forms.Label
        Me.lblRemito = New System.Windows.Forms.Label
        Me.txtRemito = New System.Windows.Forms.TextBox
        Me.LblUnidCont = New System.Windows.Forms.Label
        Me.TxtUnidCont = New System.Windows.Forms.TextBox
        Me.lblLotePartida = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cboCliente
        '
        Me.cboCliente.Location = New System.Drawing.Point(93, 1)
        Me.cboCliente.Name = "cboCliente"
        Me.cboCliente.Size = New System.Drawing.Size(144, 22)
        Me.cboCliente.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(4, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 20)
        Me.Label1.Text = "Compañia"
        '
        'txtOC
        '
        Me.txtOC.Location = New System.Drawing.Point(93, 24)
        Me.txtOC.Name = "txtOC"
        Me.txtOC.Size = New System.Drawing.Size(144, 21)
        Me.txtOC.TabIndex = 1
        '
        'lblNOC
        '
        Me.lblNOC.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNOC.Location = New System.Drawing.Point(4, 25)
        Me.lblNOC.Name = "lblNOC"
        Me.lblNOC.Size = New System.Drawing.Size(74, 20)
        Me.lblNOC.Text = "Nº de OC"
        '
        'lblProducto
        '
        Me.lblProducto.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblProducto.Location = New System.Drawing.Point(4, 70)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(74, 20)
        Me.lblProducto.Text = "Producto"
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(93, 69)
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(144, 21)
        Me.txtProducto.TabIndex = 3
        '
        'lblDescripcion
        '
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescripcion.Location = New System.Drawing.Point(4, 110)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(229, 37)
        Me.lblDescripcion.Text = "Descripcion del producto"
        '
        'lblMsg
        '
        Me.lblMsg.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(4, 228)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(229, 25)
        Me.lblMsg.Text = "lblMsg"
        '
        'btnInicio
        '
        Me.btnInicio.Location = New System.Drawing.Point(2, 256)
        Me.btnInicio.Name = "btnInicio"
        Me.btnInicio.Size = New System.Drawing.Size(120, 17)
        Me.btnInicio.TabIndex = 6
        Me.btnInicio.Text = "F1) Inicio"
        '
        'lblcantidad
        '
        Me.lblcantidad.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblcantidad.Location = New System.Drawing.Point(4, 180)
        Me.lblcantidad.Name = "lblcantidad"
        Me.lblcantidad.Size = New System.Drawing.Size(146, 20)
        Me.lblcantidad.Text = "Cantidad a ingresar:"
        '
        'txtCantidad
        '
        Me.txtCantidad.Location = New System.Drawing.Point(156, 180)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(81, 21)
        Me.txtCantidad.TabIndex = 4
        '
        'btnFin
        '
        Me.btnFin.Location = New System.Drawing.Point(2, 275)
        Me.btnFin.Name = "btnFin"
        Me.btnFin.Size = New System.Drawing.Size(120, 17)
        Me.btnFin.TabIndex = 7
        Me.btnFin.Text = "F2) Fin Recepción"
        '
        'btnIngresados
        '
        Me.btnIngresados.Location = New System.Drawing.Point(128, 256)
        Me.btnIngresados.Name = "btnIngresados"
        Me.btnIngresados.Size = New System.Drawing.Size(107, 17)
        Me.btnIngresados.TabIndex = 8
        Me.btnIngresados.Text = "F3) Ingresados"
        '
        'btnAtrasVolver
        '
        Me.btnAtrasVolver.Location = New System.Drawing.Point(128, 275)
        Me.btnAtrasVolver.Name = "btnAtrasVolver"
        Me.btnAtrasVolver.Size = New System.Drawing.Size(107, 17)
        Me.btnAtrasVolver.TabIndex = 9
        Me.btnAtrasVolver.Text = "F4) Salir"
        '
        'LblTitDescripcion
        '
        Me.LblTitDescripcion.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblTitDescripcion.Location = New System.Drawing.Point(4, 93)
        Me.LblTitDescripcion.Name = "LblTitDescripcion"
        Me.LblTitDescripcion.Size = New System.Drawing.Size(80, 20)
        Me.LblTitDescripcion.Text = "Descripcion"
        '
        'lblRemito
        '
        Me.lblRemito.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblRemito.Location = New System.Drawing.Point(4, 47)
        Me.lblRemito.Name = "lblRemito"
        Me.lblRemito.Size = New System.Drawing.Size(74, 20)
        Me.lblRemito.Text = "Remito Nº"
        '
        'txtRemito
        '
        Me.txtRemito.Location = New System.Drawing.Point(93, 46)
        Me.txtRemito.Name = "txtRemito"
        Me.txtRemito.Size = New System.Drawing.Size(144, 21)
        Me.txtRemito.TabIndex = 2
        '
        'LblUnidCont
        '
        Me.LblUnidCont.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblUnidCont.Location = New System.Drawing.Point(4, 204)
        Me.LblUnidCont.Name = "LblUnidCont"
        Me.LblUnidCont.Size = New System.Drawing.Size(158, 20)
        Me.LblUnidCont.Text = "Cantidad Contenedoras"
        '
        'TxtUnidCont
        '
        Me.TxtUnidCont.Location = New System.Drawing.Point(156, 203)
        Me.TxtUnidCont.Name = "TxtUnidCont"
        Me.TxtUnidCont.Size = New System.Drawing.Size(81, 21)
        Me.TxtUnidCont.TabIndex = 5
        '
        'lblLotePartida
        '
        Me.lblLotePartida.Location = New System.Drawing.Point(4, 153)
        Me.lblLotePartida.Name = "lblLotePartida"
        Me.lblLotePartida.Size = New System.Drawing.Size(233, 20)
        Me.lblLotePartida.Text = "Lote/Partida"
        '
        'FrmRecepcionOC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.lblLotePartida)
        Me.Controls.Add(Me.TxtUnidCont)
        Me.Controls.Add(Me.LblUnidCont)
        Me.Controls.Add(Me.btnAtrasVolver)
        Me.Controls.Add(Me.btnIngresados)
        Me.Controls.Add(Me.btnFin)
        Me.Controls.Add(Me.btnInicio)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.txtProducto)
        Me.Controls.Add(Me.lblcantidad)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.LblTitDescripcion)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.txtRemito)
        Me.Controls.Add(Me.lblRemito)
        Me.Controls.Add(Me.txtOC)
        Me.Controls.Add(Me.lblNOC)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboCliente)
        Me.KeyPreview = True
        Me.Name = "FrmRecepcionOC"
        Me.Text = "Recepcion Orden de Compra"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cboCliente As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtOC As System.Windows.Forms.TextBox
    Friend WithEvents lblNOC As System.Windows.Forms.Label
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents btnInicio As System.Windows.Forms.Button
    Friend WithEvents lblcantidad As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents btnFin As System.Windows.Forms.Button
    Friend WithEvents btnIngresados As System.Windows.Forms.Button
    Friend WithEvents btnAtrasVolver As System.Windows.Forms.Button
    Friend WithEvents LblTitDescripcion As System.Windows.Forms.Label
    Friend WithEvents lblRemito As System.Windows.Forms.Label
    Friend WithEvents txtRemito As System.Windows.Forms.TextBox
    Friend WithEvents LblUnidCont As System.Windows.Forms.Label
    Friend WithEvents TxtUnidCont As System.Windows.Forms.TextBox
    Private WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents lblLotePartida As System.Windows.Forms.Label
End Class
