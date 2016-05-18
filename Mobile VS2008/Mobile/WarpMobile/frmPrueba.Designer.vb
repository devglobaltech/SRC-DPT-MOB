<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmPrueba
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
    Private mainMenu1 As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.LBMenu = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'LBMenu
        '
        Me.LBMenu.BackColor = System.Drawing.Color.AliceBlue
        Me.LBMenu.Font = New System.Drawing.Font("Book Antiqua", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LBMenu.ForeColor = System.Drawing.Color.DarkBlue
        Me.LBMenu.Location = New System.Drawing.Point(3, 0)
        Me.LBMenu.Name = "LBMenu"
        Me.LBMenu.Size = New System.Drawing.Size(234, 290)
        Me.LBMenu.TabIndex = 0
        '
        'frmPrueba
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.LBMenu)
        Me.Name = "frmPrueba"
        Me.Text = "frmPrueba"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LBMenu As System.Windows.Forms.ListBox
End Class
