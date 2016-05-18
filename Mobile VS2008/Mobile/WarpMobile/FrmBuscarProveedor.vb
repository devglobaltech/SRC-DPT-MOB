Imports System.Data.SqlClient
Imports System.Data

Public Class FrmBuscarProveedor
    Private Const FrmName As String = "Busqueda de proveedores"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private CodProveedor As String
    Private DesProveedor As String
    Private CodClienteID As String

    Public WriteOnly Property Cliente_id() As String
        Set(ByVal value As String)
            Me.CodClienteID = value
        End Set
    End Property

    Public ReadOnly Property Proveedor_ID() As String
        Get
            Return CodProveedor
        End Get
    End Property

    Public ReadOnly Property Proveedor_Des() As String
        Get
            Return DesProveedor
        End Get
    End Property

    Private Sub FrmBuscarProveedor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If GetCargar() Then
            ResizeGrillaIngresados()
            Me.dtgProveedor.Focus()
        End If
    End Sub

    Private Function ResizeGrillaIngresados() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "SUCURSAL"
            dtgProveedor.TableStyles.Clear()

            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "SUCURSAL_ID"
            TextCol1.HeaderText = "Codigo"
            TextCol1.Width = 60
            Style.GridColumnStyles.Add(TextCol1)
            TextCol1 = Nothing

            Dim TextCol2 As New DataGridTextBoxColumn
            TextCol2.MappingName = "NOMBRE"
            TextCol2.HeaderText = "NOMBRE"
            TextCol2.Width = 150
            Style.GridColumnStyles.Add(TextCol2)
            TextCol2 = Nothing

            dtgProveedor.TableStyles.Add(Style)
            'dtgProveedor.Refresh()


        Catch ex As Exception
            MsgBox("ResizeGrillaIngresados: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Function

    Private Function GetCargar() As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = "MOB_GET_SUCURSAL"

                Pa = New SqlParameter("@cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Me.CodClienteID
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                'Pa = New SqlParameter("@ORDEN_COMPRA", SqlDbType.VarChar, 100)
                'Pa.Value = vOC
                'xCmd.Parameters.Add(Pa)

                Da.Fill(Ds, "SUCURSAL")

                dtgProveedor.DataSource = Ds.Tables("sucursal")
                'If dtgSucursal.VisibleRowCount < 1 Then
                'btnEliminar.Enabled = False
                'btnAjustar.Enabled = False
                'Else
                'btnEliminar.Enabled = True
                'btnAjustar.Enabled = True
                'End If
            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            Pa = Nothing
            Ds = Nothing
            xCmd = Nothing
            Da = Nothing
        End Try

    End Function

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeleccionar.Click
        seleccionar()
    End Sub

    Sub seleccionar()
        Try
            Me.CodProveedor = Me.dtgProveedor.Item(dtgProveedor.CurrentRowIndex, 0).ToString()
            Me.DesProveedor = Me.dtgProveedor.Item(dtgProveedor.CurrentRowIndex, 1).ToString()
            Me.Close()
        Catch ex As Exception
        Finally
            FrmOrdenCompra = Nothing
        End Try
    End Sub

    Private Sub dtgProveedor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgProveedor.Click
        Try
            Me.dtgProveedor.Select(dtgProveedor.CurrentRowIndex)
            'seleccionar()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dtgProveedor_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgProveedor.DoubleClick
        Try
            Me.dtgProveedor.Select(dtgProveedor.CurrentRowIndex)
            seleccionar()
        Catch ex As Exception
        End Try
    End Sub
End Class