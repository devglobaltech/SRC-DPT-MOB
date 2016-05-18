<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmAPF_Abiertos
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
        Me.lblCodigoViaje = New System.Windows.Forms.Label
        Me.lblCliente = New System.Windows.Forms.Label
        Me.DgPallet = New System.Windows.Forms.DataGrid
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.cmdAbrir = New System.Windows.Forms.Button
        Me.cmdReImpresion = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblCodigoViaje
        '
        Me.lblCodigoViaje.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblCodigoViaje.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCodigoViaje.Location = New System.Drawing.Point(4, 36)
        Me.lblCodigoViaje.Name = "lblCodigoViaje"
        Me.lblCodigoViaje.Size = New System.Drawing.Size(233, 15)
        Me.lblCodigoViaje.Text = "Codigo de Viaje:"
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCliente.Location = New System.Drawing.Point(4, 11)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(233, 16)
        Me.lblCliente.Text = "Cliente:"
        '
        'DgPallet
        '
        Me.DgPallet.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.DgPallet.Location = New System.Drawing.Point(4, 64)
        Me.DgPallet.Name = "DgPallet"
        Me.DgPallet.SelectionBackColor = System.Drawing.Color.LightSalmon
        Me.DgPallet.Size = New System.Drawing.Size(233, 174)
        Me.DgPallet.TabIndex = 4
        '
        'cmdSalir
        '
        Me.cmdSalir.BackColor = System.Drawing.Color.LightGray
        Me.cmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmdSalir.Location = New System.Drawing.Point(125, 244)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(112, 15)
        Me.cmdSalir.TabIndex = 14
        Me.cmdSalir.Text = "F2) Salir"
        '
        'cmdAbrir
        '
        Me.cmdAbrir.BackColor = System.Drawing.Color.LightGray
        Me.cmdAbrir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmdAbrir.Location = New System.Drawing.Point(4, 244)
        Me.cmdAbrir.Name = "cmdAbrir"
        Me.cmdAbrir.Size = New System.Drawing.Size(115, 15)
        Me.cmdAbrir.TabIndex = 13
        Me.cmdAbrir.Text = "F1) Abrir Pallet"
        '
        'cmdReImpresion
        '
        Me.cmdReImpresion.BackColor = System.Drawing.Color.LightGray
        Me.cmdReImpresion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.cmdReImpresion.Location = New System.Drawing.Point(4, 265)
        Me.cmdReImpresion.Name = "cmdReImpresion"
        Me.cmdReImpresion.Size = New System.Drawing.Size(115, 15)
        Me.cmdReImpresion.TabIndex = 17
        Me.cmdReImpresion.Text = "F3) Imprimir Etiqueta"
        '
        'FrmAPF_Abiertos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdReImpresion)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdAbrir)
        Me.Controls.Add(Me.DgPallet)
        Me.Controls.Add(Me.lblCliente)
        Me.Controls.Add(Me.lblCodigoViaje)
        Me.KeyPreview = True
        Me.Name = "FrmAPF_Abiertos"
        Me.Text = "Pallet's Abiertos/Cerrados."
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCodigoViaje As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents DgPallet As System.Windows.Forms.DataGrid
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdAbrir As System.Windows.Forms.Button
    Friend WithEvents cmdReImpresion As System.Windows.Forms.Button
End Class
