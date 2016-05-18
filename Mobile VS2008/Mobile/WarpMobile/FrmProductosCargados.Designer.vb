<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmProductosCargados
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblNroDocumento = New System.Windows.Forms.Label
        Me.DgProductosCargados = New System.Windows.Forms.DataGrid
        Me.btnDescontarTodo = New System.Windows.Forms.Button
        Me.btnDescontarUno = New System.Windows.Forms.Button
        Me.btnVolver = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(4, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(123, 20)
        Me.Label1.Text = "Nro. de Documento:"
        '
        'lblNroDocumento
        '
        Me.lblNroDocumento.Location = New System.Drawing.Point(134, 13)
        Me.lblNroDocumento.Name = "lblNroDocumento"
        Me.lblNroDocumento.Size = New System.Drawing.Size(100, 20)
        '
        'DgProductosCargados
        '
        Me.DgProductosCargados.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.DgProductosCargados.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.DgProductosCargados.Location = New System.Drawing.Point(7, 46)
        Me.DgProductosCargados.Name = "DgProductosCargados"
        Me.DgProductosCargados.Size = New System.Drawing.Size(230, 183)
        Me.DgProductosCargados.TabIndex = 2
        '
        'btnDescontarTodo
        '
        Me.btnDescontarTodo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnDescontarTodo.Location = New System.Drawing.Point(7, 236)
        Me.btnDescontarTodo.Name = "btnDescontarTodo"
        Me.btnDescontarTodo.Size = New System.Drawing.Size(120, 20)
        Me.btnDescontarTodo.TabIndex = 3
        Me.btnDescontarTodo.Text = "F8) Descontar Todo"
        '
        'btnDescontarUno
        '
        Me.btnDescontarUno.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnDescontarUno.Location = New System.Drawing.Point(7, 262)
        Me.btnDescontarUno.Name = "btnDescontarUno"
        Me.btnDescontarUno.Size = New System.Drawing.Size(120, 20)
        Me.btnDescontarUno.TabIndex = 4
        Me.btnDescontarUno.Text = "F9) Descontar Uno"
        '
        'btnVolver
        '
        Me.btnVolver.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnVolver.Location = New System.Drawing.Point(144, 262)
        Me.btnVolver.Name = "btnVolver"
        Me.btnVolver.Size = New System.Drawing.Size(90, 20)
        Me.btnVolver.TabIndex = 5
        Me.btnVolver.Text = "F10) Volver"
        '
        'FrmProductosCargados
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.btnVolver)
        Me.Controls.Add(Me.btnDescontarUno)
        Me.Controls.Add(Me.btnDescontarTodo)
        Me.Controls.Add(Me.DgProductosCargados)
        Me.Controls.Add(Me.lblNroDocumento)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "FrmProductosCargados"
        Me.Text = "Productos Cargados"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblNroDocumento As System.Windows.Forms.Label
    Friend WithEvents DgProductosCargados As System.Windows.Forms.DataGrid
    Friend WithEvents btnDescontarTodo As System.Windows.Forms.Button
    Friend WithEvents btnDescontarUno As System.Windows.Forms.Button
    Friend WithEvents btnVolver As System.Windows.Forms.Button
End Class
