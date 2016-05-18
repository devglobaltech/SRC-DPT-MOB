<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEnvase
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
        Me.dgArt = New System.Windows.Forms.DataGrid
        Me.lblQTY = New System.Windows.Forms.Label
        Me.txtQty = New System.Windows.Forms.TextBox
        Me.cmdConfirmacion = New System.Windows.Forms.Button
        Me.cmdIngresar = New System.Windows.Forms.Button
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.CmdSalir = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'dgArt
        '
        Me.dgArt.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgArt.Location = New System.Drawing.Point(3, 3)
        Me.dgArt.Name = "dgArt"
        Me.dgArt.RowHeadersVisible = False
        Me.dgArt.Size = New System.Drawing.Size(234, 194)
        Me.dgArt.TabIndex = 0
        '
        'lblQTY
        '
        Me.lblQTY.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.lblQTY.Location = New System.Drawing.Point(3, 200)
        Me.lblQTY.Name = "lblQTY"
        Me.lblQTY.Size = New System.Drawing.Size(98, 19)
        Me.lblQTY.Text = "Cantidad:"
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(107, 200)
        Me.txtQty.MaxLength = 4
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(130, 21)
        Me.txtQty.TabIndex = 2
        '
        'cmdConfirmacion
        '
        Me.cmdConfirmacion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdConfirmacion.Location = New System.Drawing.Point(3, 245)
        Me.cmdConfirmacion.Name = "cmdConfirmacion"
        Me.cmdConfirmacion.Size = New System.Drawing.Size(116, 16)
        Me.cmdConfirmacion.TabIndex = 3
        Me.cmdConfirmacion.Text = "F3) Terminar"
        '
        'cmdIngresar
        '
        Me.cmdIngresar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdIngresar.Location = New System.Drawing.Point(3, 227)
        Me.cmdIngresar.Name = "cmdIngresar"
        Me.cmdIngresar.Size = New System.Drawing.Size(116, 16)
        Me.cmdIngresar.TabIndex = 5
        Me.cmdIngresar.Text = "F1) Ingresar"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCancelar.Location = New System.Drawing.Point(121, 227)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(116, 16)
        Me.cmdCancelar.TabIndex = 6
        Me.cmdCancelar.Text = "F2) Cancelar"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.Location = New System.Drawing.Point(121, 245)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(116, 16)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "F4) Limpiar Todo"
        '
        'CmdSalir
        '
        Me.CmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.CmdSalir.Location = New System.Drawing.Point(3, 263)
        Me.CmdSalir.Name = "CmdSalir"
        Me.CmdSalir.Size = New System.Drawing.Size(116, 16)
        Me.CmdSalir.TabIndex = 9
        Me.CmdSalir.Text = "F5) Salir"
        '
        'frmEnvase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 300)
        Me.ControlBox = False
        Me.Controls.Add(Me.CmdSalir)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdIngresar)
        Me.Controls.Add(Me.cmdConfirmacion)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.lblQTY)
        Me.Controls.Add(Me.dgArt)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frmEnvase"
        Me.Text = "Carga de Envases"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgArt As System.Windows.Forms.DataGrid
    Friend WithEvents lblQTY As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents cmdConfirmacion As System.Windows.Forms.Button
    Friend WithEvents cmdIngresar As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents CmdSalir As System.Windows.Forms.Button
End Class
