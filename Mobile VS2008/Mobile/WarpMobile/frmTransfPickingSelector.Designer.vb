<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmTransfPickingSelector
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.btn_aceptar = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Label1.Location = New System.Drawing.Point(0, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(240, 34)
        Me.Label1.Text = "Indique que tipo de transferencias desea realizar"
        '
        'ComboBox1
        '
        Me.ComboBox1.Items.Add("Contenedor")
        Me.ComboBox1.Items.Add("Pallet")
        Me.ComboBox1.Location = New System.Drawing.Point(0, 60)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(240, 22)
        Me.ComboBox1.TabIndex = 1
        '
        'btn_aceptar
        '
        Me.btn_aceptar.Location = New System.Drawing.Point(0, 101)
        Me.btn_aceptar.Name = "btn_aceptar"
        Me.btn_aceptar.Size = New System.Drawing.Size(109, 20)
        Me.btn_aceptar.TabIndex = 2
        Me.btn_aceptar.Text = "F1) Aceptar"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(125, 101)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(115, 20)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "F2) Cancelar"
        '
        'frmTransfPickingSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btn_aceptar)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "frmTransfPickingSelector"
        Me.Text = "Transf. Pre-Picking"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents btn_aceptar As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
