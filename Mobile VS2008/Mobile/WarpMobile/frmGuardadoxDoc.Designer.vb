<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmGuardadoxDoc
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
        Me.lblDoc = New System.Windows.Forms.Label
        Me.txtDOC = New System.Windows.Forms.TextBox
        Me.txtProd = New System.Windows.Forms.TextBox
        Me.lblProd = New System.Windows.Forms.Label
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.lblQty = New System.Windows.Forms.Label
        Me.lblDescUbic = New System.Windows.Forms.Label
        Me.lblUbicacion = New System.Windows.Forms.Label
        Me.txtUbicacion = New System.Windows.Forms.TextBox
        Me.lblQtyIng = New System.Windows.Forms.Label
        Me.txtQty = New System.Windows.Forms.TextBox
        Me.cmdComenzar = New System.Windows.Forms.Button
        Me.cmdPendientes = New System.Windows.Forms.Button
        Me.cmdFin = New System.Windows.Forms.Button
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.lblMensajes = New System.Windows.Forms.Label
        Me.PanCantidad = New System.Windows.Forms.Panel
        Me.PanCableBolsa = New System.Windows.Forms.Panel
        Me.TxtUnidCont = New System.Windows.Forms.TextBox
        Me.LblUnidCont = New System.Windows.Forms.Label
        Me.PanCantidad.SuspendLayout()
        Me.PanCableBolsa.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDoc
        '
        Me.lblDoc.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDoc.Location = New System.Drawing.Point(5, 8)
        Me.lblDoc.Name = "lblDoc"
        Me.lblDoc.Size = New System.Drawing.Size(108, 18)
        Me.lblDoc.Text = "Nro. Doc. Ingreso:"
        '
        'txtDOC
        '
        Me.txtDOC.Location = New System.Drawing.Point(117, 5)
        Me.txtDOC.MaxLength = 8
        Me.txtDOC.Name = "txtDOC"
        Me.txtDOC.Size = New System.Drawing.Size(118, 21)
        Me.txtDOC.TabIndex = 1
        '
        'txtProd
        '
        Me.txtProd.Location = New System.Drawing.Point(71, 31)
        Me.txtProd.MaxLength = 55
        Me.txtProd.Name = "txtProd"
        Me.txtProd.Size = New System.Drawing.Size(164, 21)
        Me.txtProd.TabIndex = 3
        '
        'lblProd
        '
        Me.lblProd.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblProd.Location = New System.Drawing.Point(5, 34)
        Me.lblProd.Name = "lblProd"
        Me.lblProd.Size = New System.Drawing.Size(60, 18)
        Me.lblProd.Text = "Producto:"
        '
        'lblDescripcion
        '
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescripcion.Location = New System.Drawing.Point(5, 57)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(229, 38)
        Me.lblDescripcion.Text = "Descripcion del Producto"
        '
        'lblQty
        '
        Me.lblQty.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblQty.Location = New System.Drawing.Point(5, 133)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(228, 22)
        Me.lblQty.Text = "Cantidad Pend. de Guardar: "
        '
        'lblDescUbic
        '
        Me.lblDescUbic.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDescUbic.Location = New System.Drawing.Point(5, 161)
        Me.lblDescUbic.Name = "lblDescUbic"
        Me.lblDescUbic.Size = New System.Drawing.Size(117, 16)
        Me.lblDescUbic.Text = "Ubicacion Sugerida:"
        '
        'lblUbicacion
        '
        Me.lblUbicacion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblUbicacion.Location = New System.Drawing.Point(120, 161)
        Me.lblUbicacion.Name = "lblUbicacion"
        Me.lblUbicacion.Size = New System.Drawing.Size(113, 16)
        Me.lblUbicacion.Text = "01AS - 58 - 1A"
        '
        'txtUbicacion
        '
        Me.txtUbicacion.Location = New System.Drawing.Point(5, 182)
        Me.txtUbicacion.Name = "txtUbicacion"
        Me.txtUbicacion.Size = New System.Drawing.Size(228, 21)
        Me.txtUbicacion.TabIndex = 9
        '
        'lblQtyIng
        '
        Me.lblQtyIng.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblQtyIng.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblQtyIng.Location = New System.Drawing.Point(3, 3)
        Me.lblQtyIng.Name = "lblQtyIng"
        Me.lblQtyIng.Size = New System.Drawing.Size(158, 19)
        Me.lblQtyIng.Text = "Cant. de Unidades a Guardar:"
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(169, 3)
        Me.txtQty.MaxLength = 15
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(65, 21)
        Me.txtQty.TabIndex = 11
        '
        'cmdComenzar
        '
        Me.cmdComenzar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdComenzar.Location = New System.Drawing.Point(5, 252)
        Me.cmdComenzar.Name = "cmdComenzar"
        Me.cmdComenzar.Size = New System.Drawing.Size(104, 15)
        Me.cmdComenzar.TabIndex = 12
        Me.cmdComenzar.Text = "F1) Comenzar"
        '
        'cmdPendientes
        '
        Me.cmdPendientes.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdPendientes.Location = New System.Drawing.Point(129, 252)
        Me.cmdPendientes.Name = "cmdPendientes"
        Me.cmdPendientes.Size = New System.Drawing.Size(104, 15)
        Me.cmdPendientes.TabIndex = 13
        Me.cmdPendientes.Text = "F2) Ver Pend."
        '
        'cmdFin
        '
        Me.cmdFin.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdFin.Location = New System.Drawing.Point(5, 273)
        Me.cmdFin.Name = "cmdFin"
        Me.cmdFin.Size = New System.Drawing.Size(104, 15)
        Me.cmdFin.TabIndex = 14
        Me.cmdFin.Text = "F3) Cancelar"
        '
        'cmdSalir
        '
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdSalir.Location = New System.Drawing.Point(130, 273)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(104, 15)
        Me.cmdSalir.TabIndex = 15
        Me.cmdSalir.Text = "F4) Salir"
        '
        'lblMensajes
        '
        Me.lblMensajes.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblMensajes.ForeColor = System.Drawing.Color.Red
        Me.lblMensajes.Location = New System.Drawing.Point(8, 205)
        Me.lblMensajes.Name = "lblMensajes"
        Me.lblMensajes.Size = New System.Drawing.Size(224, 36)
        '
        'PanCantidad
        '
        Me.PanCantidad.Controls.Add(Me.txtQty)
        Me.PanCantidad.Controls.Add(Me.lblQtyIng)
        Me.PanCantidad.Location = New System.Drawing.Point(0, 99)
        Me.PanCantidad.Name = "PanCantidad"
        Me.PanCantidad.Size = New System.Drawing.Size(237, 26)
        '
        'PanCableBolsa
        '
        Me.PanCableBolsa.Controls.Add(Me.TxtUnidCont)
        Me.PanCableBolsa.Controls.Add(Me.LblUnidCont)
        Me.PanCableBolsa.Location = New System.Drawing.Point(0, 262)
        Me.PanCableBolsa.Name = "PanCableBolsa"
        Me.PanCableBolsa.Size = New System.Drawing.Size(240, 26)
        Me.PanCableBolsa.Visible = False
        '
        'TxtUnidCont
        '
        Me.TxtUnidCont.Location = New System.Drawing.Point(179, 2)
        Me.TxtUnidCont.Name = "TxtUnidCont"
        Me.TxtUnidCont.Size = New System.Drawing.Size(55, 21)
        Me.TxtUnidCont.TabIndex = 11
        Me.TxtUnidCont.Visible = False
        '
        'LblUnidCont
        '
        Me.LblUnidCont.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblUnidCont.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.LblUnidCont.Location = New System.Drawing.Point(5, 5)
        Me.LblUnidCont.Name = "LblUnidCont"
        Me.LblUnidCont.Size = New System.Drawing.Size(175, 18)
        Me.LblUnidCont.Text = "Cant de Contenedoras a Guardar:"
        Me.LblUnidCont.Visible = False
        '
        'frmGuardadoxDoc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.PanCantidad)
        Me.Controls.Add(Me.lblMensajes)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdFin)
        Me.Controls.Add(Me.cmdPendientes)
        Me.Controls.Add(Me.cmdComenzar)
        Me.Controls.Add(Me.txtUbicacion)
        Me.Controls.Add(Me.lblUbicacion)
        Me.Controls.Add(Me.lblDescUbic)
        Me.Controls.Add(Me.lblQty)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.txtProd)
        Me.Controls.Add(Me.lblProd)
        Me.Controls.Add(Me.txtDOC)
        Me.Controls.Add(Me.lblDoc)
        Me.Controls.Add(Me.PanCableBolsa)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frmGuardadoxDoc"
        Me.Text = "Guardado Manual"
        Me.PanCantidad.ResumeLayout(False)
        Me.PanCableBolsa.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
  Friend WithEvents lblDoc As System.Windows.Forms.Label
  Friend WithEvents txtDOC As System.Windows.Forms.TextBox
  Friend WithEvents txtProd As System.Windows.Forms.TextBox
  Friend WithEvents lblProd As System.Windows.Forms.Label
  Friend WithEvents lblDescripcion As System.Windows.Forms.Label
  Friend WithEvents lblQty As System.Windows.Forms.Label
  Friend WithEvents lblDescUbic As System.Windows.Forms.Label
  Friend WithEvents lblUbicacion As System.Windows.Forms.Label
  Friend WithEvents txtUbicacion As System.Windows.Forms.TextBox
  Friend WithEvents lblQtyIng As System.Windows.Forms.Label
  Friend WithEvents txtQty As System.Windows.Forms.TextBox
  Friend WithEvents cmdComenzar As System.Windows.Forms.Button
  Friend WithEvents cmdPendientes As System.Windows.Forms.Button
  Friend WithEvents cmdFin As System.Windows.Forms.Button
  Friend WithEvents cmdSalir As System.Windows.Forms.Button
  Friend WithEvents lblMensajes As System.Windows.Forms.Label
  Friend WithEvents PanCantidad As System.Windows.Forms.Panel
  Friend WithEvents PanCableBolsa As System.Windows.Forms.Panel
  Friend WithEvents TxtUnidCont As System.Windows.Forms.TextBox
  Friend WithEvents LblUnidCont As System.Windows.Forms.Label
End Class
