<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmSeries
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
        Me.dg1 = New System.Windows.Forms.DataGrid
        Me.TxtNumSeries = New System.Windows.Forms.TextBox
        Me.lblNumSeries = New System.Windows.Forms.Label
        Me.cmdAceptar = New System.Windows.Forms.Button
        Me.cmdModificar = New System.Windows.Forms.Button
        Me.lblMsg = New System.Windows.Forms.Label
        Me.txtNumMod = New System.Windows.Forms.TextBox
        Me.LblNumMod = New System.Windows.Forms.Label
        Me.cmdCerrar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'dg1
        '
        Me.dg1.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dg1.Location = New System.Drawing.Point(0, 41)
        Me.dg1.Name = "dg1"
        Me.dg1.Size = New System.Drawing.Size(237, 172)
        Me.dg1.TabIndex = 2
        '
        'TxtNumSeries
        '
        Me.TxtNumSeries.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.TxtNumSeries.Location = New System.Drawing.Point(108, 16)
        Me.TxtNumSeries.Name = "TxtNumSeries"
        Me.TxtNumSeries.Size = New System.Drawing.Size(119, 19)
        Me.TxtNumSeries.TabIndex = 1
        '
        'lblNumSeries
        '
        Me.lblNumSeries.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblNumSeries.Location = New System.Drawing.Point(22, 16)
        Me.lblNumSeries.Name = "lblNumSeries"
        Me.lblNumSeries.Size = New System.Drawing.Size(80, 20)
        Me.lblNumSeries.Text = "Número de Serie:"
        '
        'cmdAceptar
        '
        Me.cmdAceptar.BackColor = System.Drawing.Color.White
        Me.cmdAceptar.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdAceptar.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdAceptar.Location = New System.Drawing.Point(3, 244)
        Me.cmdAceptar.Name = "cmdAceptar"
        Me.cmdAceptar.Size = New System.Drawing.Size(109, 13)
        Me.cmdAceptar.TabIndex = 4
        Me.cmdAceptar.Text = "F1) Aceptar"
        '
        'cmdModificar
        '
        Me.cmdModificar.BackColor = System.Drawing.Color.White
        Me.cmdModificar.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdModificar.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdModificar.Location = New System.Drawing.Point(118, 260)
        Me.cmdModificar.Name = "cmdModificar"
        Me.cmdModificar.Size = New System.Drawing.Size(109, 13)
        Me.cmdModificar.TabIndex = 6
        Me.cmdModificar.Text = "F2) Modifica"
        Me.cmdModificar.Visible = False
        '
        'lblMsg
        '
        Me.lblMsg.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(3, 260)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(231, 23)
        '
        'txtNumMod
        '
        Me.txtNumMod.Enabled = False
        Me.txtNumMod.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtNumMod.Location = New System.Drawing.Point(164, 219)
        Me.txtNumMod.Name = "txtNumMod"
        Me.txtNumMod.Size = New System.Drawing.Size(63, 19)
        Me.txtNumMod.TabIndex = 10
        Me.txtNumMod.Visible = False
        '
        'LblNumMod
        '
        Me.LblNumMod.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.LblNumMod.Location = New System.Drawing.Point(22, 218)
        Me.LblNumMod.Name = "LblNumMod"
        Me.LblNumMod.Size = New System.Drawing.Size(136, 20)
        Me.LblNumMod.Text = "Pos. Núm Serie a Modificar:"
        Me.LblNumMod.Visible = False
        '
        'cmdCerrar
        '
        Me.cmdCerrar.BackColor = System.Drawing.Color.White
        Me.cmdCerrar.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCerrar.ForeColor = System.Drawing.Color.DarkBlue
        Me.cmdCerrar.Location = New System.Drawing.Point(118, 244)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(109, 13)
        Me.cmdCerrar.TabIndex = 13
        Me.cmdCerrar.Text = "F2) Salir"
        '
        'frmSeries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdCerrar)
        Me.Controls.Add(Me.LblNumMod)
        Me.Controls.Add(Me.txtNumMod)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.cmdModificar)
        Me.Controls.Add(Me.cmdAceptar)
        Me.Controls.Add(Me.lblNumSeries)
        Me.Controls.Add(Me.TxtNumSeries)
        Me.Controls.Add(Me.dg1)
        Me.MinimizeBox = False
        Me.Name = "frmSeries"
        Me.Text = "Series."
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dg1 As System.Windows.Forms.DataGrid
    Friend WithEvents TxtNumSeries As System.Windows.Forms.TextBox
    Friend WithEvents lblNumSeries As System.Windows.Forms.Label
    Friend WithEvents cmdAceptar As System.Windows.Forms.Button
    Friend WithEvents cmdModificar As System.Windows.Forms.Button
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents txtNumMod As System.Windows.Forms.TextBox
    Friend WithEvents LblNumMod As System.Windows.Forms.Label
    Friend WithEvents cmdCerrar As System.Windows.Forms.Button
End Class
