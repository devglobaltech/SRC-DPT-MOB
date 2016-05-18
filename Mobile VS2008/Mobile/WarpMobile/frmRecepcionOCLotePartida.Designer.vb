<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmRecepcionOCLotePartida
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
        Me.lblMensajeTitulo = New System.Windows.Forms.Label
        Me.lblLoteProveedor = New System.Windows.Forms.Label
        Me.lblPartida = New System.Windows.Forms.Label
        Me.txtLoteProveedor = New System.Windows.Forms.TextBox
        Me.txtPartida = New System.Windows.Forms.TextBox
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.lblVencimiento = New System.Windows.Forms.Label
        Me.txtVencimiento = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblDebeCompletarFecha = New System.Windows.Forms.Label
        Me.lblDebeCompletarPartida = New System.Windows.Forms.Label
        Me.lblDebeCompletarLote = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblMensajeTitulo
        '
        Me.lblMensajeTitulo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblMensajeTitulo.Location = New System.Drawing.Point(3, 11)
        Me.lblMensajeTitulo.Name = "lblMensajeTitulo"
        Me.lblMensajeTitulo.Size = New System.Drawing.Size(234, 62)
        Me.lblMensajeTitulo.Text = "Ingrese Lote de Proveedor y/o número de Partida."
        '
        'lblLoteProveedor
        '
        Me.lblLoteProveedor.Location = New System.Drawing.Point(3, 76)
        Me.lblLoteProveedor.Name = "lblLoteProveedor"
        Me.lblLoteProveedor.Size = New System.Drawing.Size(117, 20)
        Me.lblLoteProveedor.Text = "Lote de Proveedor:"
        '
        'lblPartida
        '
        Me.lblPartida.Location = New System.Drawing.Point(3, 101)
        Me.lblPartida.Name = "lblPartida"
        Me.lblPartida.Size = New System.Drawing.Size(117, 20)
        Me.lblPartida.Text = "Número de Partida:"
        '
        'txtLoteProveedor
        '
        Me.txtLoteProveedor.Location = New System.Drawing.Point(126, 76)
        Me.txtLoteProveedor.Name = "txtLoteProveedor"
        Me.txtLoteProveedor.Size = New System.Drawing.Size(111, 21)
        Me.txtLoteProveedor.TabIndex = 3
        '
        'txtPartida
        '
        Me.txtPartida.Location = New System.Drawing.Point(126, 100)
        Me.txtPartida.Name = "txtPartida"
        Me.txtPartida.Size = New System.Drawing.Size(111, 21)
        Me.txtPartida.TabIndex = 4
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(6, 165)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(234, 20)
        Me.btnAceptar.TabIndex = 6
        Me.btnAceptar.Text = "Aceptar (F1)"
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(6, 191)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(234, 20)
        Me.btnCancelar.TabIndex = 7
        Me.btnCancelar.Text = "Cancelar (F2)"
        '
        'lblVencimiento
        '
        Me.lblVencimiento.Location = New System.Drawing.Point(3, 125)
        Me.lblVencimiento.Name = "lblVencimiento"
        Me.lblVencimiento.Size = New System.Drawing.Size(117, 20)
        Me.lblVencimiento.Text = "Fecha Vencimiento:"
        '
        'txtVencimiento
        '
        Me.txtVencimiento.Location = New System.Drawing.Point(126, 124)
        Me.txtVencimiento.MaxLength = 10
        Me.txtVencimiento.Name = "txtVencimiento"
        Me.txtVencimiento.Size = New System.Drawing.Size(111, 21)
        Me.txtVencimiento.TabIndex = 5
        Me.txtVencimiento.Text = "__/__/____"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(125, 148)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 10)
        Me.Label3.Text = "Ej.: 01/01/2020"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblDebeCompletarFecha
        '
        Me.lblDebeCompletarFecha.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDebeCompletarFecha.ForeColor = System.Drawing.Color.Red
        Me.lblDebeCompletarFecha.Location = New System.Drawing.Point(6, 260)
        Me.lblDebeCompletarFecha.Name = "lblDebeCompletarFecha"
        Me.lblDebeCompletarFecha.Size = New System.Drawing.Size(234, 16)
        Me.lblDebeCompletarFecha.Text = "* Debe completar la Fecha de Vencimiento"
        '
        'lblDebeCompletarPartida
        '
        Me.lblDebeCompletarPartida.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDebeCompletarPartida.ForeColor = System.Drawing.Color.Red
        Me.lblDebeCompletarPartida.Location = New System.Drawing.Point(6, 240)
        Me.lblDebeCompletarPartida.Name = "lblDebeCompletarPartida"
        Me.lblDebeCompletarPartida.Size = New System.Drawing.Size(234, 16)
        Me.lblDebeCompletarPartida.Text = "* Debe completar el campo número de Partida"
        '
        'lblDebeCompletarLote
        '
        Me.lblDebeCompletarLote.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDebeCompletarLote.ForeColor = System.Drawing.Color.Red
        Me.lblDebeCompletarLote.Location = New System.Drawing.Point(6, 220)
        Me.lblDebeCompletarLote.Name = "lblDebeCompletarLote"
        Me.lblDebeCompletarLote.Size = New System.Drawing.Size(234, 16)
        Me.lblDebeCompletarLote.Text = "* Debe completar el campo Lote de Proveedor"
        '
        'frmRecepcionOCLotePartida
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblDebeCompletarFecha)
        Me.Controls.Add(Me.lblDebeCompletarPartida)
        Me.Controls.Add(Me.lblDebeCompletarLote)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtVencimiento)
        Me.Controls.Add(Me.lblVencimiento)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.txtPartida)
        Me.Controls.Add(Me.txtLoteProveedor)
        Me.Controls.Add(Me.lblPartida)
        Me.Controls.Add(Me.lblLoteProveedor)
        Me.Controls.Add(Me.lblMensajeTitulo)
        Me.KeyPreview = True
        Me.Name = "frmRecepcionOCLotePartida"
        Me.Text = "Ingresar datos obligatorios"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblMensajeTitulo As System.Windows.Forms.Label
    Friend WithEvents lblLoteProveedor As System.Windows.Forms.Label
    Friend WithEvents lblPartida As System.Windows.Forms.Label
    Friend WithEvents txtLoteProveedor As System.Windows.Forms.TextBox
    Friend WithEvents txtPartida As System.Windows.Forms.TextBox
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents lblVencimiento As System.Windows.Forms.Label
    Friend WithEvents txtVencimiento As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblDebeCompletarFecha As System.Windows.Forms.Label
    Friend WithEvents lblDebeCompletarPartida As System.Windows.Forms.Label
    Friend WithEvents lblDebeCompletarLote As System.Windows.Forms.Label
End Class
