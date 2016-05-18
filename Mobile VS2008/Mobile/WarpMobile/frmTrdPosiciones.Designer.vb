<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmTrdPosiciones
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
        Me.PanelGrid = New System.Windows.Forms.Panel
        Me.dgPosiciones = New System.Windows.Forms.DataGrid
        Me.lblProd = New System.Windows.Forms.Label
        Me.lblDesc = New System.Windows.Forms.Label
        Me.cmdConfirmar = New System.Windows.Forms.Button
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.cmdOtherPos = New System.Windows.Forms.Button
        Me.Panelpallet = New System.Windows.Forms.Panel
        Me.txtPallet = New System.Windows.Forms.TextBox
        Me.lblPallet = New System.Windows.Forms.Label
        Me.PanelConfirmacion = New System.Windows.Forms.Panel
        Me.lblDestino = New System.Windows.Forms.Label
        Me.txtConfDestino = New System.Windows.Forms.TextBox
        Me.lblMsg = New System.Windows.Forms.Label
        Me.PanelPosicion = New System.Windows.Forms.Panel
        Me.lblPosicion = New System.Windows.Forms.Label
        Me.txtDestino = New System.Windows.Forms.TextBox
        Me.PanelGrid.SuspendLayout()
        Me.Panelpallet.SuspendLayout()
        Me.PanelConfirmacion.SuspendLayout()
        Me.PanelPosicion.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelGrid
        '
        Me.PanelGrid.Controls.Add(Me.dgPosiciones)
        Me.PanelGrid.Location = New System.Drawing.Point(5, 47)
        Me.PanelGrid.Name = "PanelGrid"
        Me.PanelGrid.Size = New System.Drawing.Size(226, 166)
        '
        'dgPosiciones
        '
        Me.dgPosiciones.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgPosiciones.Location = New System.Drawing.Point(3, 1)
        Me.dgPosiciones.Name = "dgPosiciones"
        Me.dgPosiciones.RowHeadersVisible = False
        Me.dgPosiciones.Size = New System.Drawing.Size(220, 161)
        Me.dgPosiciones.TabIndex = 1
        '
        'lblProd
        '
        Me.lblProd.Location = New System.Drawing.Point(5, 3)
        Me.lblProd.Name = "lblProd"
        Me.lblProd.Size = New System.Drawing.Size(226, 15)
        Me.lblProd.Text = "Producto"
        '
        'lblDesc
        '
        Me.lblDesc.Location = New System.Drawing.Point(5, 21)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(226, 15)
        Me.lblDesc.Text = "Descripcion"
        '
        'cmdConfirmar
        '
        Me.cmdConfirmar.BackColor = System.Drawing.Color.White
        Me.cmdConfirmar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdConfirmar.Location = New System.Drawing.Point(5, 215)
        Me.cmdConfirmar.Name = "cmdConfirmar"
        Me.cmdConfirmar.Size = New System.Drawing.Size(103, 15)
        Me.cmdConfirmar.TabIndex = 3
        Me.cmdConfirmar.Text = "F1) Confirmar"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.BackColor = System.Drawing.Color.White
        Me.cmdCancelar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCancelar.Location = New System.Drawing.Point(5, 232)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(103, 15)
        Me.cmdCancelar.TabIndex = 4
        Me.cmdCancelar.Text = "F3) Cancelar"
        '
        'cmdOtherPos
        '
        Me.cmdOtherPos.BackColor = System.Drawing.Color.White
        Me.cmdOtherPos.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.cmdOtherPos.Location = New System.Drawing.Point(128, 215)
        Me.cmdOtherPos.Name = "cmdOtherPos"
        Me.cmdOtherPos.Size = New System.Drawing.Size(103, 15)
        Me.cmdOtherPos.TabIndex = 8
        Me.cmdOtherPos.Text = "F2) Otra Pos."
        '
        'Panelpallet
        '
        Me.Panelpallet.Controls.Add(Me.txtPallet)
        Me.Panelpallet.Controls.Add(Me.lblPallet)
        Me.Panelpallet.Location = New System.Drawing.Point(0, 52)
        Me.Panelpallet.Name = "Panelpallet"
        Me.Panelpallet.Size = New System.Drawing.Size(225, 52)
        Me.Panelpallet.Visible = False
        '
        'txtPallet
        '
        Me.txtPallet.Location = New System.Drawing.Point(3, 25)
        Me.txtPallet.Name = "txtPallet"
        Me.txtPallet.Size = New System.Drawing.Size(208, 21)
        Me.txtPallet.TabIndex = 1
        '
        'lblPallet
        '
        Me.lblPallet.Location = New System.Drawing.Point(3, 5)
        Me.lblPallet.Name = "lblPallet"
        Me.lblPallet.Size = New System.Drawing.Size(208, 17)
        Me.lblPallet.Text = "Escannee el pallet"
        '
        'PanelConfirmacion
        '
        Me.PanelConfirmacion.Controls.Add(Me.lblDestino)
        Me.PanelConfirmacion.Controls.Add(Me.txtConfDestino)
        Me.PanelConfirmacion.Location = New System.Drawing.Point(0, 50)
        Me.PanelConfirmacion.Name = "PanelConfirmacion"
        Me.PanelConfirmacion.Size = New System.Drawing.Size(225, 51)
        Me.PanelConfirmacion.Visible = False
        '
        'lblDestino
        '
        Me.lblDestino.Location = New System.Drawing.Point(3, 5)
        Me.lblDestino.Name = "lblDestino"
        Me.lblDestino.Size = New System.Drawing.Size(208, 17)
        Me.lblDestino.Text = "Ubicacion D:"
        '
        'txtConfDestino
        '
        Me.txtConfDestino.Location = New System.Drawing.Point(3, 24)
        Me.txtConfDestino.Name = "txtConfDestino"
        Me.txtConfDestino.Size = New System.Drawing.Size(208, 21)
        Me.txtConfDestino.TabIndex = 1
        '
        'lblMsg
        '
        Me.lblMsg.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(3, 249)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(228, 39)
        Me.lblMsg.Text = "Msg"
        '
        'PanelPosicion
        '
        Me.PanelPosicion.Controls.Add(Me.lblPosicion)
        Me.PanelPosicion.Controls.Add(Me.txtDestino)
        Me.PanelPosicion.Location = New System.Drawing.Point(0, 52)
        Me.PanelPosicion.Name = "PanelPosicion"
        Me.PanelPosicion.Size = New System.Drawing.Size(225, 50)
        Me.PanelPosicion.Visible = False
        '
        'lblPosicion
        '
        Me.lblPosicion.Location = New System.Drawing.Point(3, 6)
        Me.lblPosicion.Name = "lblPosicion"
        Me.lblPosicion.Size = New System.Drawing.Size(208, 17)
        Me.lblPosicion.Text = "Ingrese Ubicacion Destino"
        '
        'txtDestino
        '
        Me.txtDestino.Location = New System.Drawing.Point(5, 24)
        Me.txtDestino.Name = "txtDestino"
        Me.txtDestino.Size = New System.Drawing.Size(206, 21)
        Me.txtDestino.TabIndex = 1
        '
        'frmTrdPosiciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.PanelPosicion)
        Me.Controls.Add(Me.Panelpallet)
        Me.Controls.Add(Me.PanelConfirmacion)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.cmdOtherPos)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdConfirmar)
        Me.Controls.Add(Me.lblDesc)
        Me.Controls.Add(Me.lblProd)
        Me.Controls.Add(Me.PanelGrid)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frmTrdPosiciones"
        Me.Text = "Seleccione Ubicacion"
        Me.PanelGrid.ResumeLayout(False)
        Me.Panelpallet.ResumeLayout(False)
        Me.PanelConfirmacion.ResumeLayout(False)
        Me.PanelPosicion.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelGrid As System.Windows.Forms.Panel
    Friend WithEvents lblProd As System.Windows.Forms.Label
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents cmdConfirmar As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents dgPosiciones As System.Windows.Forms.DataGrid
    Friend WithEvents cmdOtherPos As System.Windows.Forms.Button
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents Panelpallet As System.Windows.Forms.Panel
    Friend WithEvents txtPallet As System.Windows.Forms.TextBox
    Friend WithEvents lblPallet As System.Windows.Forms.Label
    Friend WithEvents PanelPosicion As System.Windows.Forms.Panel
    Friend WithEvents txtDestino As System.Windows.Forms.TextBox
    Friend WithEvents lblPosicion As System.Windows.Forms.Label
    Friend WithEvents PanelConfirmacion As System.Windows.Forms.Panel
    Friend WithEvents lblDestino As System.Windows.Forms.Label
    Friend WithEvents txtConfDestino As System.Windows.Forms.TextBox
End Class
