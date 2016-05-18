<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmCargaSeriesInvalidas
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
        Me.lblIngresoSeries = New System.Windows.Forms.Label
        Me.DgSeries = New System.Windows.Forms.DataGrid
        Me.cmdSalir = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblIngresoSeries
        '
        Me.lblIngresoSeries.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblIngresoSeries.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblIngresoSeries.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblIngresoSeries.Location = New System.Drawing.Point(0, 2)
        Me.lblIngresoSeries.Name = "lblIngresoSeries"
        Me.lblIngresoSeries.Size = New System.Drawing.Size(240, 20)
        Me.lblIngresoSeries.Text = "Series Existentes"
        Me.lblIngresoSeries.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'DgSeries
        '
        Me.DgSeries.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.DgSeries.Location = New System.Drawing.Point(0, 25)
        Me.DgSeries.Name = "DgSeries"
        Me.DgSeries.Size = New System.Drawing.Size(240, 233)
        Me.DgSeries.TabIndex = 12
        '
        'cmdSalir
        '
        Me.cmdSalir.Location = New System.Drawing.Point(0, 264)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(240, 17)
        Me.cmdSalir.TabIndex = 16
        Me.cmdSalir.Text = "F1) Volver"
        '
        'frmCargaSeriesInvalidas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.DgSeries)
        Me.Controls.Add(Me.lblIngresoSeries)
        Me.KeyPreview = True
        Me.Name = "frmCargaSeriesInvalidas"
        Me.Text = "Series Invalidas"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblIngresoSeries As System.Windows.Forms.Label
    Friend WithEvents DgSeries As System.Windows.Forms.DataGrid
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
End Class
