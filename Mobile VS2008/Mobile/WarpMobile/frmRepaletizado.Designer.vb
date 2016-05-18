<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmRepaletizado
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
        Me.LblUbicacionOri = New System.Windows.Forms.Label
        Me.LblPalletOrigen = New System.Windows.Forms.Label
        Me.LblContenedora = New System.Windows.Forms.Label
        Me.LstContenedoras = New System.Windows.Forms.ListBox
        Me.LblPalletDestino = New System.Windows.Forms.Label
        Me.LblUbicacionDest = New System.Windows.Forms.Label
        Me.TxtUbicacionOri = New System.Windows.Forms.TextBox
        Me.TxtPalletOri = New System.Windows.Forms.TextBox
        Me.TxtContenedora = New System.Windows.Forms.TextBox
        Me.TxtPalletDest = New System.Windows.Forms.TextBox
        Me.TxtUbicacionDest = New System.Windows.Forms.TextBox
        Me.CmdConfirmar = New System.Windows.Forms.Button
        Me.CmdCancelar = New System.Windows.Forms.Button
        Me.CmdSalir = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'LblUbicacionOri
        '
        Me.LblUbicacionOri.Location = New System.Drawing.Point(4, 4)
        Me.LblUbicacionOri.Name = "LblUbicacionOri"
        Me.LblUbicacionOri.Size = New System.Drawing.Size(233, 20)
        Me.LblUbicacionOri.Text = "Ubicacion Origen:"
        '
        'LblPalletOrigen
        '
        Me.LblPalletOrigen.Location = New System.Drawing.Point(4, 52)
        Me.LblPalletOrigen.Name = "LblPalletOrigen"
        Me.LblPalletOrigen.Size = New System.Drawing.Size(100, 20)
        Me.LblPalletOrigen.Text = "Pallet Origen:"
        Me.LblPalletOrigen.Visible = False
        '
        'LblContenedora
        '
        Me.LblContenedora.Location = New System.Drawing.Point(4, 76)
        Me.LblContenedora.Name = "LblContenedora"
        Me.LblContenedora.Size = New System.Drawing.Size(100, 20)
        Me.LblContenedora.Text = "Contenedora:"
        Me.LblContenedora.Visible = False
        '
        'LstContenedoras
        '
        Me.LstContenedoras.Location = New System.Drawing.Point(4, 101)
        Me.LstContenedoras.Name = "LstContenedoras"
        Me.LstContenedoras.Size = New System.Drawing.Size(233, 72)
        Me.LstContenedoras.TabIndex = 3
        Me.LstContenedoras.Visible = False
        '
        'LblPalletDestino
        '
        Me.LblPalletDestino.Location = New System.Drawing.Point(4, 177)
        Me.LblPalletDestino.Name = "LblPalletDestino"
        Me.LblPalletDestino.Size = New System.Drawing.Size(100, 20)
        Me.LblPalletDestino.Text = "Pallet Destino:"
        Me.LblPalletDestino.Visible = False
        '
        'LblUbicacionDest
        '
        Me.LblUbicacionDest.Location = New System.Drawing.Point(4, 203)
        Me.LblUbicacionDest.Name = "LblUbicacionDest"
        Me.LblUbicacionDest.Size = New System.Drawing.Size(233, 20)
        Me.LblUbicacionDest.Text = "Ubicacion Destino: "
        Me.LblUbicacionDest.Visible = False
        '
        'TxtUbicacionOri
        '
        Me.TxtUbicacionOri.Location = New System.Drawing.Point(4, 28)
        Me.TxtUbicacionOri.MaxLength = 45
        Me.TxtUbicacionOri.Name = "TxtUbicacionOri"
        Me.TxtUbicacionOri.Size = New System.Drawing.Size(233, 21)
        Me.TxtUbicacionOri.TabIndex = 6
        '
        'TxtPalletOri
        '
        Me.TxtPalletOri.Location = New System.Drawing.Point(110, 52)
        Me.TxtPalletOri.MaxLength = 20
        Me.TxtPalletOri.Name = "TxtPalletOri"
        Me.TxtPalletOri.Size = New System.Drawing.Size(127, 21)
        Me.TxtPalletOri.TabIndex = 7
        Me.TxtPalletOri.Visible = False
        '
        'TxtContenedora
        '
        Me.TxtContenedora.Location = New System.Drawing.Point(110, 76)
        Me.TxtContenedora.MaxLength = 15
        Me.TxtContenedora.Name = "TxtContenedora"
        Me.TxtContenedora.Size = New System.Drawing.Size(127, 21)
        Me.TxtContenedora.TabIndex = 8
        Me.TxtContenedora.Visible = False
        '
        'TxtPalletDest
        '
        Me.TxtPalletDest.Location = New System.Drawing.Point(110, 177)
        Me.TxtPalletDest.MaxLength = 15
        Me.TxtPalletDest.Name = "TxtPalletDest"
        Me.TxtPalletDest.Size = New System.Drawing.Size(127, 21)
        Me.TxtPalletDest.TabIndex = 9
        Me.TxtPalletDest.Visible = False
        '
        'TxtUbicacionDest
        '
        Me.TxtUbicacionDest.Location = New System.Drawing.Point(4, 225)
        Me.TxtUbicacionDest.MaxLength = 45
        Me.TxtUbicacionDest.Name = "TxtUbicacionDest"
        Me.TxtUbicacionDest.Size = New System.Drawing.Size(233, 21)
        Me.TxtUbicacionDest.TabIndex = 10
        Me.TxtUbicacionDest.Visible = False
        '
        'CmdConfirmar
        '
        Me.CmdConfirmar.BackColor = System.Drawing.Color.White
        Me.CmdConfirmar.Enabled = False
        Me.CmdConfirmar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.CmdConfirmar.Location = New System.Drawing.Point(4, 253)
        Me.CmdConfirmar.Name = "CmdConfirmar"
        Me.CmdConfirmar.Size = New System.Drawing.Size(111, 15)
        Me.CmdConfirmar.TabIndex = 11
        Me.CmdConfirmar.Text = "F1) Confirmar"
        '
        'CmdCancelar
        '
        Me.CmdCancelar.BackColor = System.Drawing.Color.White
        Me.CmdCancelar.Enabled = False
        Me.CmdCancelar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.CmdCancelar.Location = New System.Drawing.Point(126, 253)
        Me.CmdCancelar.Name = "CmdCancelar"
        Me.CmdCancelar.Size = New System.Drawing.Size(111, 15)
        Me.CmdCancelar.TabIndex = 12
        Me.CmdCancelar.Text = "F2) Cancelar"
        '
        'CmdSalir
        '
        Me.CmdSalir.BackColor = System.Drawing.Color.White
        Me.CmdSalir.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.CmdSalir.Location = New System.Drawing.Point(4, 275)
        Me.CmdSalir.Name = "CmdSalir"
        Me.CmdSalir.Size = New System.Drawing.Size(111, 15)
        Me.CmdSalir.TabIndex = 13
        Me.CmdSalir.Text = "F3) Salir"
        '
        'frmRepaletizado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.CmdSalir)
        Me.Controls.Add(Me.CmdCancelar)
        Me.Controls.Add(Me.CmdConfirmar)
        Me.Controls.Add(Me.TxtUbicacionDest)
        Me.Controls.Add(Me.TxtPalletDest)
        Me.Controls.Add(Me.TxtContenedora)
        Me.Controls.Add(Me.TxtPalletOri)
        Me.Controls.Add(Me.TxtUbicacionOri)
        Me.Controls.Add(Me.LblUbicacionDest)
        Me.Controls.Add(Me.LblPalletDestino)
        Me.Controls.Add(Me.LstContenedoras)
        Me.Controls.Add(Me.LblContenedora)
        Me.Controls.Add(Me.LblPalletOrigen)
        Me.Controls.Add(Me.LblUbicacionOri)
        Me.KeyPreview = True
        Me.Name = "frmRepaletizado"
        Me.Text = "Repaletizado"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblUbicacionOri As System.Windows.Forms.Label
    Friend WithEvents LblPalletOrigen As System.Windows.Forms.Label
    Friend WithEvents LblContenedora As System.Windows.Forms.Label
    Friend WithEvents LstContenedoras As System.Windows.Forms.ListBox
    Friend WithEvents LblPalletDestino As System.Windows.Forms.Label
    Friend WithEvents LblUbicacionDest As System.Windows.Forms.Label
    Friend WithEvents TxtUbicacionOri As System.Windows.Forms.TextBox
    Friend WithEvents TxtPalletOri As System.Windows.Forms.TextBox
    Friend WithEvents TxtContenedora As System.Windows.Forms.TextBox
    Friend WithEvents TxtPalletDest As System.Windows.Forms.TextBox
    Friend WithEvents TxtUbicacionDest As System.Windows.Forms.TextBox
    Friend WithEvents CmdConfirmar As System.Windows.Forms.Button
    Friend WithEvents CmdCancelar As System.Windows.Forms.Button
    Friend WithEvents CmdSalir As System.Windows.Forms.Button
End Class
