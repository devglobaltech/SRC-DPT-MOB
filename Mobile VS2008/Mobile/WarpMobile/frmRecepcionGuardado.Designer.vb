<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmRecepcionGuardado
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
        Me.btnComenzar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.btnFinalizar = New System.Windows.Forms.Button
        Me.btnContenido = New System.Windows.Forms.Button
        Me.lblCliente = New System.Windows.Forms.Label
        Me.cmbCliente = New System.Windows.Forms.ComboBox
        Me.lblOC = New System.Windows.Forms.Label
        Me.txtOC = New System.Windows.Forms.TextBox
        Me.lblProducto = New System.Windows.Forms.Label
        Me.txtProducto = New System.Windows.Forms.TextBox
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.txtNroLote = New System.Windows.Forms.TextBox
        Me.lblNroLote = New System.Windows.Forms.Label
        Me.txtNroPartida = New System.Windows.Forms.TextBox
        Me.lblNroPartida = New System.Windows.Forms.Label
        Me.txtf_vto = New System.Windows.Forms.TextBox
        Me.lblF_Vto = New System.Windows.Forms.Label
        Me.txtCantidad = New System.Windows.Forms.TextBox
        Me.lblCantidad = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.txtNroPallet = New System.Windows.Forms.TextBox
        Me.lblNroPallet = New System.Windows.Forms.Label
        Me.lblEj = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnComenzar
        '
        Me.btnComenzar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnComenzar.Location = New System.Drawing.Point(2, 227)
        Me.btnComenzar.Name = "btnComenzar"
        Me.btnComenzar.Size = New System.Drawing.Size(108, 20)
        Me.btnComenzar.TabIndex = 0
        Me.btnComenzar.Text = "F1) Comenzar"
        '
        'btnCancelar
        '
        Me.btnCancelar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancelar.Location = New System.Drawing.Point(129, 249)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(108, 20)
        Me.btnCancelar.TabIndex = 1
        Me.btnCancelar.Text = "F4) Cancelar"
        '
        'btnFinalizar
        '
        Me.btnFinalizar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnFinalizar.Location = New System.Drawing.Point(129, 227)
        Me.btnFinalizar.Name = "btnFinalizar"
        Me.btnFinalizar.Size = New System.Drawing.Size(108, 20)
        Me.btnFinalizar.TabIndex = 2
        Me.btnFinalizar.Text = "F2) Finalizar"
        '
        'btnContenido
        '
        Me.btnContenido.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnContenido.Location = New System.Drawing.Point(2, 249)
        Me.btnContenido.Name = "btnContenido"
        Me.btnContenido.Size = New System.Drawing.Size(108, 20)
        Me.btnContenido.TabIndex = 3
        Me.btnContenido.Text = "F3) Ver Contenido"
        '
        'lblCliente
        '
        Me.lblCliente.Location = New System.Drawing.Point(0, 5)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(53, 20)
        Me.lblCliente.Text = "Cliente:"
        '
        'cmbCliente
        '
        Me.cmbCliente.Location = New System.Drawing.Point(51, 3)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(186, 22)
        Me.cmbCliente.TabIndex = 5
        '
        'lblOC
        '
        Me.lblOC.Location = New System.Drawing.Point(0, 28)
        Me.lblOC.Name = "lblOC"
        Me.lblOC.Size = New System.Drawing.Size(53, 20)
        Me.lblOC.Text = "O.C.:"
        '
        'txtOC
        '
        Me.txtOC.Location = New System.Drawing.Point(51, 27)
        Me.txtOC.Name = "txtOC"
        Me.txtOC.Size = New System.Drawing.Size(186, 21)
        Me.txtOC.TabIndex = 8
        '
        'lblProducto
        '
        Me.lblProducto.Location = New System.Drawing.Point(0, 73)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(86, 20)
        Me.lblProducto.Text = "Cod.Producto:"
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(86, 72)
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(151, 21)
        Me.txtProducto.TabIndex = 12
        '
        'lblDescripcion
        '
        Me.lblDescripcion.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescripcion.ForeColor = System.Drawing.Color.Black
        Me.lblDescripcion.Location = New System.Drawing.Point(0, 95)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(237, 20)
        Me.lblDescripcion.Text = "DescripcionProducto"
        '
        'txtNroLote
        '
        Me.txtNroLote.Location = New System.Drawing.Point(78, 117)
        Me.txtNroLote.Name = "txtNroLote"
        Me.txtNroLote.Size = New System.Drawing.Size(159, 21)
        Me.txtNroLote.TabIndex = 17
        '
        'lblNroLote
        '
        Me.lblNroLote.Location = New System.Drawing.Point(0, 118)
        Me.lblNroLote.Name = "lblNroLote"
        Me.lblNroLote.Size = New System.Drawing.Size(80, 20)
        Me.lblNroLote.Text = "Nro. Lote:"
        '
        'txtNroPartida
        '
        Me.txtNroPartida.Location = New System.Drawing.Point(78, 140)
        Me.txtNroPartida.Name = "txtNroPartida"
        Me.txtNroPartida.Size = New System.Drawing.Size(159, 21)
        Me.txtNroPartida.TabIndex = 20
        '
        'lblNroPartida
        '
        Me.lblNroPartida.Location = New System.Drawing.Point(0, 141)
        Me.lblNroPartida.Name = "lblNroPartida"
        Me.lblNroPartida.Size = New System.Drawing.Size(80, 20)
        Me.lblNroPartida.Text = "Nro. Partida:"
        '
        'txtf_vto
        '
        Me.txtf_vto.Location = New System.Drawing.Point(87, 162)
        Me.txtf_vto.MaxLength = 10
        Me.txtf_vto.Name = "txtf_vto"
        Me.txtf_vto.Size = New System.Drawing.Size(71, 21)
        Me.txtf_vto.TabIndex = 23
        Me.txtf_vto.Text = "__/__/____"
        '
        'lblF_Vto
        '
        Me.lblF_Vto.Location = New System.Drawing.Point(0, 163)
        Me.lblF_Vto.Name = "lblF_Vto"
        Me.lblF_Vto.Size = New System.Drawing.Size(97, 20)
        Me.lblF_Vto.Text = "F. Vencimiento:"
        '
        'txtCantidad
        '
        Me.txtCantidad.Location = New System.Drawing.Point(86, 185)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(104, 21)
        Me.txtCantidad.TabIndex = 26
        '
        'lblCantidad
        '
        Me.lblCantidad.Location = New System.Drawing.Point(0, 187)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(97, 20)
        Me.lblCantidad.Text = "Cantidad:"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.Location = New System.Drawing.Point(2, 271)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(108, 20)
        Me.Button1.TabIndex = 28
        Me.Button1.Text = "F5) Salir"
        '
        'txtNroPallet
        '
        Me.txtNroPallet.Location = New System.Drawing.Point(86, 50)
        Me.txtNroPallet.Name = "txtNroPallet"
        Me.txtNroPallet.Size = New System.Drawing.Size(151, 21)
        Me.txtNroPallet.TabIndex = 38
        '
        'lblNroPallet
        '
        Me.lblNroPallet.Location = New System.Drawing.Point(0, 51)
        Me.lblNroPallet.Name = "lblNroPallet"
        Me.lblNroPallet.Size = New System.Drawing.Size(86, 20)
        Me.lblNroPallet.Text = "Nro. Pallet:"
        '
        'lblEj
        '
        Me.lblEj.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblEj.ForeColor = System.Drawing.Color.Red
        Me.lblEj.Location = New System.Drawing.Point(159, 164)
        Me.lblEj.Name = "lblEj"
        Me.lblEj.Size = New System.Drawing.Size(78, 18)
        Me.lblEj.Text = "E.:01/01/2020"
        '
        'frmRecepcionGuardado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblEj)
        Me.Controls.Add(Me.txtNroPallet)
        Me.Controls.Add(Me.lblNroPallet)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.lblCantidad)
        Me.Controls.Add(Me.txtf_vto)
        Me.Controls.Add(Me.lblF_Vto)
        Me.Controls.Add(Me.txtNroPartida)
        Me.Controls.Add(Me.lblNroPartida)
        Me.Controls.Add(Me.txtNroLote)
        Me.Controls.Add(Me.lblNroLote)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.txtProducto)
        Me.Controls.Add(Me.lblProducto)
        Me.Controls.Add(Me.txtOC)
        Me.Controls.Add(Me.lblOC)
        Me.Controls.Add(Me.cmbCliente)
        Me.Controls.Add(Me.lblCliente)
        Me.Controls.Add(Me.btnContenido)
        Me.Controls.Add(Me.btnFinalizar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnComenzar)
        Me.KeyPreview = True
        Me.Name = "frmRecepcionGuardado"
        Me.Text = "Recepcion y Guardado"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnComenzar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnFinalizar As System.Windows.Forms.Button
    Friend WithEvents btnContenido As System.Windows.Forms.Button
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents cmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents lblOC As System.Windows.Forms.Label
    Friend WithEvents txtOC As System.Windows.Forms.TextBox
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents txtNroLote As System.Windows.Forms.TextBox
    Friend WithEvents lblNroLote As System.Windows.Forms.Label
    Friend WithEvents txtNroPartida As System.Windows.Forms.TextBox
    Friend WithEvents lblNroPartida As System.Windows.Forms.Label
    Friend WithEvents txtf_vto As System.Windows.Forms.TextBox
    Friend WithEvents lblF_Vto As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents lblCantidad As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtNroPallet As System.Windows.Forms.TextBox
    Friend WithEvents lblNroPallet As System.Windows.Forms.Label
    Friend WithEvents lblEj As System.Windows.Forms.Label
End Class
