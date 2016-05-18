<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmConsultaRecepOC
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
        Me.lblCompañia = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblNOC = New System.Windows.Forms.Label
        Me.dtgIngresados = New System.Windows.Forms.DataGrid
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnSalir = New System.Windows.Forms.Button
        Me.btnEliminar = New System.Windows.Forms.Button
        Me.btnAjustar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(4, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 20)
        Me.Label1.Text = "Compañia"
        '
        'lblCompañia
        '
        Me.lblCompañia.BackColor = System.Drawing.SystemColors.Control
        Me.lblCompañia.Location = New System.Drawing.Point(84, 4)
        Me.lblCompañia.Name = "lblCompañia"
        Me.lblCompañia.Size = New System.Drawing.Size(153, 20)
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(4, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 20)
        Me.Label2.Text = "Nº de OC :"
        '
        'lblNOC
        '
        Me.lblNOC.BackColor = System.Drawing.SystemColors.Control
        Me.lblNOC.Location = New System.Drawing.Point(84, 27)
        Me.lblNOC.Name = "lblNOC"
        Me.lblNOC.Size = New System.Drawing.Size(153, 20)
        '
        'dtgIngresados
        '
        Me.dtgIngresados.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dtgIngresados.Location = New System.Drawing.Point(4, 73)
        Me.dtgIngresados.Name = "dtgIngresados"
        Me.dtgIngresados.Size = New System.Drawing.Size(233, 166)
        Me.dtgIngresados.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(4, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(233, 20)
        Me.Label3.Text = "Productos Ingresados"
        '
        'btnSalir
        '
        Me.btnSalir.Location = New System.Drawing.Point(4, 243)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(113, 20)
        Me.btnSalir.TabIndex = 7
        Me.btnSalir.Text = "F1) Salir"
        '
        'btnEliminar
        '
        Me.btnEliminar.Location = New System.Drawing.Point(125, 243)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(112, 20)
        Me.btnEliminar.TabIndex = 7
        Me.btnEliminar.Text = "F2) Eliminar"
        '
        'btnAjustar
        '
        Me.btnAjustar.Location = New System.Drawing.Point(125, 269)
        Me.btnAjustar.Name = "btnAjustar"
        Me.btnAjustar.Size = New System.Drawing.Size(20, 20)
        Me.btnAjustar.TabIndex = 7
        Me.btnAjustar.Text = "Ajustar Cantidad"
        Me.btnAjustar.Visible = False
        '
        'FrmConsultaRecepOC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnAjustar)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtgIngresados)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblNOC)
        Me.Controls.Add(Me.lblCompañia)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "FrmConsultaRecepOC"
        Me.Text = "Consulta Orden de Compra"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCompañia As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblNOC As System.Windows.Forms.Label
    Friend WithEvents dtgIngresados As System.Windows.Forms.DataGrid
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents btnAjustar As System.Windows.Forms.Button
End Class
