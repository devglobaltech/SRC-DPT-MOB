<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmRecepcionGuardadoContenido
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
        Me.lblNroContenedora = New System.Windows.Forms.Label
        Me.dgRes = New System.Windows.Forms.DataGrid
        Me.btnVolver = New System.Windows.Forms.Button
        Me.BtnQuitar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblNroContenedora
        '
        Me.lblNroContenedora.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.lblNroContenedora.Location = New System.Drawing.Point(0, 3)
        Me.lblNroContenedora.Name = "lblNroContenedora"
        Me.lblNroContenedora.Size = New System.Drawing.Size(240, 20)
        Me.lblNroContenedora.Text = "Contenido del Pallet:"
        '
        'dgRes
        '
        Me.dgRes.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgRes.Location = New System.Drawing.Point(0, 26)
        Me.dgRes.Name = "dgRes"
        Me.dgRes.Size = New System.Drawing.Size(239, 200)
        Me.dgRes.TabIndex = 1
        '
        'btnVolver
        '
        Me.btnVolver.Location = New System.Drawing.Point(0, 232)
        Me.btnVolver.Name = "btnVolver"
        Me.btnVolver.Size = New System.Drawing.Size(237, 20)
        Me.btnVolver.TabIndex = 2
        Me.btnVolver.Text = "F1) Volver"
        '
        'BtnQuitar
        '
        Me.BtnQuitar.Location = New System.Drawing.Point(0, 255)
        Me.BtnQuitar.Name = "BtnQuitar"
        Me.BtnQuitar.Size = New System.Drawing.Size(237, 20)
        Me.BtnQuitar.TabIndex = 3
        Me.BtnQuitar.Text = "F2) Quitar"
        '
        'frmRecepcionGuardadoContenido
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnQuitar)
        Me.Controls.Add(Me.btnVolver)
        Me.Controls.Add(Me.dgRes)
        Me.Controls.Add(Me.lblNroContenedora)
        Me.KeyPreview = True
        Me.Name = "frmRecepcionGuardadoContenido"
        Me.Text = "Contenido"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblNroContenedora As System.Windows.Forms.Label
    Friend WithEvents dgRes As System.Windows.Forms.DataGrid
    Friend WithEvents btnVolver As System.Windows.Forms.Button
    Friend WithEvents BtnQuitar As System.Windows.Forms.Button
End Class
