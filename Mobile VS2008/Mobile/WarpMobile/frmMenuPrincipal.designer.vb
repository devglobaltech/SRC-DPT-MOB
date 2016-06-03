<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmMenuPrincipal
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
        Me.LBMenu = New System.Windows.Forms.ListBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'LBMenu
        '
        Me.LBMenu.BackColor = System.Drawing.Color.White
        Me.LBMenu.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.LBMenu.ForeColor = System.Drawing.SystemColors.Highlight
        Me.LBMenu.Location = New System.Drawing.Point(3, 4)
        Me.LBMenu.Name = "LBMenu"
        Me.LBMenu.Size = New System.Drawing.Size(236, 290)
        Me.LBMenu.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(88, 270)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(68, 24)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Button1"
        '
        'frmMenuPrincipal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.LBMenu)
        Me.KeyPreview = True
        Me.Name = "frmMenuPrincipal"
        Me.Text = "Menú Principal"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LBMenu As System.Windows.Forms.ListBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
