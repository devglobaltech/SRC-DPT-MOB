<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmBultosPendientes
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
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtgIngresados = New System.Windows.Forms.DataGrid
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.dtgPendientes = New System.Windows.Forms.DataGrid
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label2.Location = New System.Drawing.Point(4, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(233, 14)
        Me.Label2.Text = "Ingresando"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label1.Location = New System.Drawing.Point(4, 128)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(233, 14)
        Me.Label1.Text = "Pendientes"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'dtgIngresados
        '
        Me.dtgIngresados.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dtgIngresados.Location = New System.Drawing.Point(4, 19)
        Me.dtgIngresados.Name = "dtgIngresados"
        Me.dtgIngresados.Size = New System.Drawing.Size(233, 100)
        Me.dtgIngresados.TabIndex = 21
        '
        'btnCerrar
        '
        Me.btnCerrar.Location = New System.Drawing.Point(76, 259)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(96, 20)
        Me.btnCerrar.TabIndex = 20
        Me.btnCerrar.Text = "Cerrar"
        '
        'dtgPendientes
        '
        Me.dtgPendientes.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dtgPendientes.Location = New System.Drawing.Point(4, 142)
        Me.dtgPendientes.Name = "dtgPendientes"
        Me.dtgPendientes.Size = New System.Drawing.Size(233, 100)
        Me.dtgPendientes.TabIndex = 18
        '
        'frmBultosPendientes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtgIngresados)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.dtgPendientes)
        Me.Name = "frmBultosPendientes"
        Me.Text = "Bultos Pendientes"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtgIngresados As System.Windows.Forms.DataGrid
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents dtgPendientes As System.Windows.Forms.DataGrid
End Class
