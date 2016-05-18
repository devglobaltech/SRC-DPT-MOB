<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmCambEstMerc
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
        Me.BtnSalir = New System.Windows.Forms.Button
        Me.BtnModificar = New System.Windows.Forms.Button
        Me.BtnCancelar = New System.Windows.Forms.Button
        Me.BtnConfirmar = New System.Windows.Forms.Button
        Me.CmbEstMerc = New System.Windows.Forms.ComboBox
        Me.LblCatLog = New System.Windows.Forms.Label
        Me.TxtCantidad = New System.Windows.Forms.TextBox
        Me.LblCantidad = New System.Windows.Forms.Label
        Me.DGEstMerc = New System.Windows.Forms.DataGrid
        Me.TxtProducto = New System.Windows.Forms.TextBox
        Me.LblCodProd = New System.Windows.Forms.Label
        Me.txtUbicacion = New System.Windows.Forms.TextBox
        Me.LblUbicacion = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'BtnSalir
        '
        Me.BtnSalir.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BtnSalir.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.BtnSalir.ForeColor = System.Drawing.Color.DarkBlue
        Me.BtnSalir.Location = New System.Drawing.Point(123, 266)
        Me.BtnSalir.Name = "BtnSalir"
        Me.BtnSalir.Size = New System.Drawing.Size(114, 15)
        Me.BtnSalir.TabIndex = 25
        Me.BtnSalir.Text = "F4) Salir"
        '
        'BtnModificar
        '
        Me.BtnModificar.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BtnModificar.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.BtnModificar.ForeColor = System.Drawing.Color.DarkBlue
        Me.BtnModificar.Location = New System.Drawing.Point(4, 266)
        Me.BtnModificar.Name = "BtnModificar"
        Me.BtnModificar.Size = New System.Drawing.Size(114, 15)
        Me.BtnModificar.TabIndex = 24
        Me.BtnModificar.Text = "F3) Modificar"
        '
        'BtnCancelar
        '
        Me.BtnCancelar.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BtnCancelar.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCancelar.ForeColor = System.Drawing.Color.DarkBlue
        Me.BtnCancelar.Location = New System.Drawing.Point(124, 245)
        Me.BtnCancelar.Name = "BtnCancelar"
        Me.BtnCancelar.Size = New System.Drawing.Size(114, 15)
        Me.BtnCancelar.TabIndex = 23
        Me.BtnCancelar.Text = "F2) Cancelar"
        '
        'BtnConfirmar
        '
        Me.BtnConfirmar.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BtnConfirmar.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.BtnConfirmar.ForeColor = System.Drawing.Color.DarkBlue
        Me.BtnConfirmar.Location = New System.Drawing.Point(4, 245)
        Me.BtnConfirmar.Name = "BtnConfirmar"
        Me.BtnConfirmar.Size = New System.Drawing.Size(114, 15)
        Me.BtnConfirmar.TabIndex = 22
        Me.BtnConfirmar.Text = "F1) Confirmar"
        '
        'CmbEstMerc
        '
        Me.CmbEstMerc.Location = New System.Drawing.Point(111, 217)
        Me.CmbEstMerc.Name = "CmbEstMerc"
        Me.CmbEstMerc.Size = New System.Drawing.Size(126, 22)
        Me.CmbEstMerc.TabIndex = 21
        '
        'LblCatLog
        '
        Me.LblCatLog.Location = New System.Drawing.Point(4, 217)
        Me.LblCatLog.Name = "LblCatLog"
        Me.LblCatLog.Size = New System.Drawing.Size(114, 20)
        Me.LblCatLog.Text = "Estado Mercaderia:"
        '
        'TxtCantidad
        '
        Me.TxtCantidad.Location = New System.Drawing.Point(111, 193)
        Me.TxtCantidad.Name = "TxtCantidad"
        Me.TxtCantidad.Size = New System.Drawing.Size(126, 21)
        Me.TxtCantidad.TabIndex = 20
        '
        'LblCantidad
        '
        Me.LblCantidad.Location = New System.Drawing.Point(4, 193)
        Me.LblCantidad.Name = "LblCantidad"
        Me.LblCantidad.Size = New System.Drawing.Size(100, 20)
        Me.LblCantidad.Text = "Cantidad:"
        '
        'DGEstMerc
        '
        Me.DGEstMerc.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.DGEstMerc.Location = New System.Drawing.Point(4, 80)
        Me.DGEstMerc.Name = "DGEstMerc"
        Me.DGEstMerc.Size = New System.Drawing.Size(233, 106)
        Me.DGEstMerc.TabIndex = 19
        '
        'TxtProducto
        '
        Me.TxtProducto.Location = New System.Drawing.Point(111, 56)
        Me.TxtProducto.Name = "TxtProducto"
        Me.TxtProducto.Size = New System.Drawing.Size(126, 21)
        Me.TxtProducto.TabIndex = 18
        '
        'LblCodProd
        '
        Me.LblCodProd.Location = New System.Drawing.Point(4, 56)
        Me.LblCodProd.Name = "LblCodProd"
        Me.LblCodProd.Size = New System.Drawing.Size(100, 20)
        Me.LblCodProd.Text = "Codigo Producto:"
        '
        'txtUbicacion
        '
        Me.txtUbicacion.Location = New System.Drawing.Point(4, 28)
        Me.txtUbicacion.Name = "txtUbicacion"
        Me.txtUbicacion.Size = New System.Drawing.Size(233, 21)
        Me.txtUbicacion.TabIndex = 17
        '
        'LblUbicacion
        '
        Me.LblUbicacion.Location = New System.Drawing.Point(4, 4)
        Me.LblUbicacion.Name = "LblUbicacion"
        Me.LblUbicacion.Size = New System.Drawing.Size(233, 20)
        Me.LblUbicacion.Text = "Ubicacion:"
        '
        'frmCambEstMerc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.CmbEstMerc)
        Me.Controls.Add(Me.BtnSalir)
        Me.Controls.Add(Me.BtnModificar)
        Me.Controls.Add(Me.BtnCancelar)
        Me.Controls.Add(Me.BtnConfirmar)
        Me.Controls.Add(Me.LblCatLog)
        Me.Controls.Add(Me.TxtCantidad)
        Me.Controls.Add(Me.LblCantidad)
        Me.Controls.Add(Me.DGEstMerc)
        Me.Controls.Add(Me.TxtProducto)
        Me.Controls.Add(Me.LblCodProd)
        Me.Controls.Add(Me.txtUbicacion)
        Me.Controls.Add(Me.LblUbicacion)
        Me.KeyPreview = True
        Me.Name = "frmCambEstMerc"
        Me.Text = "Cambio de Estado de Mercaderia"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnSalir As System.Windows.Forms.Button
    Friend WithEvents BtnModificar As System.Windows.Forms.Button
    Friend WithEvents BtnCancelar As System.Windows.Forms.Button
    Friend WithEvents BtnConfirmar As System.Windows.Forms.Button
    Friend WithEvents CmbEstMerc As System.Windows.Forms.ComboBox
    Friend WithEvents LblCatLog As System.Windows.Forms.Label
    Friend WithEvents TxtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents LblCantidad As System.Windows.Forms.Label
    Friend WithEvents DGEstMerc As System.Windows.Forms.DataGrid
    Friend WithEvents TxtProducto As System.Windows.Forms.TextBox
    Friend WithEvents LblCodProd As System.Windows.Forms.Label
    Friend WithEvents txtUbicacion As System.Windows.Forms.TextBox
    Friend WithEvents LblUbicacion As System.Windows.Forms.Label
End Class
