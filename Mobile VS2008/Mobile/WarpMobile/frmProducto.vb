Public Class frmProducto
    Private FrmName As String = "Productos del pallet"
    Private Const vMenu As String = "F1) Volver."
    Private sPallet As String
    Public Property Pallet() As String
        Get
            Return sPallet
        End Get
        Set(ByVal value As String)
            sPallet = value
        End Set
    End Property
    Private oDataSet As Data.DataSet
    Public Property DataSet() As Data.DataSet
        Get
            Return oDataSet
        End Get
        Set(ByVal value As Data.DataSet)
            oDataSet = value
        End Set
    End Property


    Private Sub FormatGrilla()
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "MobBuscaProducto"
            dgProducto.TableStyles.Clear()
            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "Producto"
            TextCol1.HeaderText = "Producto"
            TextCol1.Width = dgProducto.Width
            Style.GridColumnStyles.Add(TextCol1)
            dgProducto.TableStyles.Add(Style)
        Catch ex As Exception
            MsgBox("FormatGrilla: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub frmProducto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Try
            Select Case e.KeyCode
                'Volver
                Case Keys.F3
                    Me.Close()

            End Select
        Catch ex As Exception
            MsgBox("frmProducto_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub frmProducto_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.dgProducto.DataSource = Me.DataSet.Tables(0)
        FormatGrilla()
        dgProducto.Focus()
        Me.Text = "Productos del Pallet " & Me.Pallet
    End Sub

 
    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Me.Close()
    End Sub


    Private Sub dgProducto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgProducto.KeyUp
        Select Case e.KeyCode
            Case Keys.F3
                Me.Close()
        End Select
    End Sub
End Class