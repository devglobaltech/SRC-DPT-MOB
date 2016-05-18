Imports System.Data.SqlClient
Imports System.Data
Public Class FrmProductosCargados
    Private Nrodocumento As String
    Private Const FrmName As String = "Productos Cargados"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."


    Public Property Documento_Id() As String
        Get
            Return Nrodocumento
        End Get
        Set(ByVal value As String)
            Nrodocumento = value
        End Set
    End Property

    Private Sub FrmProductosCargados_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F18

            Case Keys.F9

            Case Keys.F10


        End Select
    End Sub

    Private Sub FrmProductosCargados_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim val As New clsProcesoIngreso
        Dim vError As String = ""
        Try
            Me.lblNroDocumento.Text = Documento_Id
            DgProductosCargados.DataSource = val.GetProductosCargados(Documento_Id, vError)
            If vError = "" Then
                ResizeGrillaIngresados()
                DgProductosCargados.Focus()
            Else
                btnDescontarTodo.Enabled = False
                btnDescontarUno.Enabled = False
                Throw New Exception(vError)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
        
    End Sub

    Private Function ResizeGrillaIngresados() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "ProductosIngresados"
            DgProductosCargados.TableStyles.Clear()

            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "PRODUCTO_ID"
            TextCol1.HeaderText = "Cod.Producto"
            TextCol1.Width = 100
            Style.GridColumnStyles.Add(TextCol1)
            TextCol1 = Nothing

            Dim TextCol4 As New DataGridTextBoxColumn
            TextCol4.MappingName = "DESCRIPCION"
            TextCol4.HeaderText = "Unidad"
            TextCol4.Width = 80
            Style.GridColumnStyles.Add(TextCol4)
            TextCol4 = Nothing

            Dim TextCol2 As New DataGridTextBoxColumn
            TextCol2.MappingName = "CANTIDAD"
            TextCol2.HeaderText = "Cantidad"
            TextCol2.Width = 60
            Style.GridColumnStyles.Add(TextCol2)
            TextCol2 = Nothing

            Dim TextCol3 As New DataGridTextBoxColumn
            TextCol3.MappingName = "CAT_LOG_ID_FINAL"
            TextCol3.HeaderText = "Cat. Lógica"
            TextCol3.Width = 100
            Style.GridColumnStyles.Add(TextCol3)
            TextCol3 = Nothing

            DgProductosCargados.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("ResizeGrillaIngresados: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Function

    Private Sub DgProductosCargados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DgProductosCargados.Click
        Try
            If Me.DgProductosCargados.VisibleRowCount > 0 Then
                Me.DgProductosCargados.Select(Me.DgProductosCargados.CurrentRowIndex)
            End If
        Catch ex As Exception
            MsgBox("DgProductosCargados_GotFocus: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub DgProductosCargados_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DgProductosCargados.GotFocus
        Try
            If Me.DgProductosCargados.VisibleRowCount > 0 Then
                Me.DgProductosCargados.Select(Me.DgProductosCargados.CurrentRowIndex)
            End If
        Catch ex As Exception
            MsgBox("DgProductosCargados_GotFocus: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub DgProductosCargados_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DgProductosCargados.KeyUp
        Select Case e.KeyCode
            Case Keys.F8
                F8()
            Case Keys.F9
                F9()
            Case Keys.F10
                Me.Close()
        End Select
    End Sub
    Private Sub F8()
        Dim val As New clsProcesoIngreso
        Dim vError As String = ""
        Try
            If MsgBox("Desea descontar todas las existencias de este producto?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                If Not val.DescontarDetalle("S", Me.DgProductosCargados.Item(DgProductosCargados.CurrentRowIndex, 0).ToString(), Documento_Id, vError) Then
                    Throw New Exception(vError)
                Else
                    DgProductosCargados.DataSource = val.GetProductosCargados(Documento_Id, vError)
                    DgProductosCargados.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox("F8 Descontar Todo: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub
    Private Sub F9()
        Dim val As New clsProcesoIngreso
        Dim vError As String = ""
        Try
            If Not val.DescontarDetalle("N", Me.DgProductosCargados.Item(DgProductosCargados.CurrentRowIndex, 0).ToString(), Documento_Id, vError) Then
                Throw New Exception(vError)
            Else
                DgProductosCargados.DataSource = val.GetProductosCargados(Documento_Id, vError)
                DgProductosCargados.Focus()
            End If
        Catch ex As Exception
            MsgBox("F9 Descontar Uno: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Sub
    Private Sub F10()
        Me.Close()
    End Sub

    Private Sub btnDescontarTodo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDescontarTodo.Click
        F8()
    End Sub

    Private Sub btnVolver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVolver.Click
        Me.Close()
    End Sub

    Private Sub btnDescontarUno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDescontarUno.Click
        F9()
    End Sub
End Class