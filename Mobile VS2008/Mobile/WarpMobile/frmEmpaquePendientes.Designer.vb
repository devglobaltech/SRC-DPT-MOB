<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEmpaquePendientes
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
        Me.btnSalir = New System.Windows.Forms.Button
        Me.dg = New System.Windows.Forms.DataGrid
        Me.lblContenedores = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnSalir
        '
        Me.btnSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnSalir.Location = New System.Drawing.Point(0, 264)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(240, 18)
        Me.btnSalir.TabIndex = 19
        Me.btnSalir.Text = "F1) Salir"
        '
        'dg
        '
        Me.dg.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dg.Location = New System.Drawing.Point(0, 24)
        Me.dg.Name = "dg"
        Me.dg.Size = New System.Drawing.Size(240, 234)
        Me.dg.TabIndex = 18
        '
        'lblContenedores
        '
        Me.lblContenedores.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.lblContenedores.Location = New System.Drawing.Point(0, 2)
        Me.lblContenedores.Name = "lblContenedores"
        Me.lblContenedores.Size = New System.Drawing.Size(240, 20)
        Me.lblContenedores.Text = "Pendientes"
        '
        'frmEmpaquePendientes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.dg)
        Me.Controls.Add(Me.lblContenedores)
        Me.KeyPreview = True
        Me.Name = "frmEmpaquePendientes"
        Me.Text = "Pendientes de empaquetar"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents dg As System.Windows.Forms.DataGrid
    Friend WithEvents lblContenedores As System.Windows.Forms.Label
End Class
