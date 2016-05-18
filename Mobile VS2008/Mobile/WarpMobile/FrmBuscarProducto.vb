Imports System.Data.SqlClient
Imports System.Data

Public Class FrmBuscarProducto
    Private Const FrmName As String = "Busqueda de Producto"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private VProducto As String
    Private VProductoDes As String
    Private VPROVEEDORID As String
    'Private DesProveedor As String
    Private idSecuencia As String
    Private CANTIDAD As Integer
    Public Property SecuenciaId() As String
        Get
            Return idSecuencia
        End Get
        Set(ByVal value As String)
            idSecuencia = value
        End Set
    End Property

    Public Property Producto_ID() As String
        Get
            Return VProducto
        End Get
        Set(ByVal value As String)
            VProducto = value
        End Set
    End Property

    Public Property Producto_Des() As String
        Get
            Return VProductoDes
        End Get
        Set(ByVal value As String)
            VProductoDes = value
        End Set
    End Property

    Public Property Proveedor_ID() As String
        Get
            Return VPROVEEDORID
        End Get
        Set(ByVal value As String)
            VPROVEEDORID = value
        End Set
    End Property


    Private Sub FrmBuscarProducto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetCargarPendiente()
        ResizeGrillaPendiente()
        If GetCargar() Then
            ResizeGrillaIngresados()
            Me.dtgPendientes.Focus()
        End If
    End Sub

    Private Function ResizeGrillaIngresados() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "Producto"
            dtgPendientes.TableStyles.Clear()

            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "CODPRODUCTO"
            TextCol1.HeaderText = "CODPRODUCTO"
            TextCol1.Width = 95
            Style.GridColumnStyles.Add(TextCol1)
            TextCol1 = Nothing

            Dim TextCol2 As New DataGridTextBoxColumn
            TextCol2.MappingName = "PRODUCTO"
            TextCol2.HeaderText = "PRODUCTO"
            TextCol2.Width = 140
            Style.GridColumnStyles.Add(TextCol2)
            TextCol2 = Nothing

            Dim TextCol3 As New DataGridTextBoxColumn
            TextCol3.MappingName = "OC"
            TextCol3.HeaderText = "OC"
            TextCol3.Width = 80
            Style.GridColumnStyles.Add(TextCol3)
            TextCol3 = Nothing

            Dim TextCol4 As New DataGridTextBoxColumn
            TextCol4.MappingName = "Cant"
            TextCol4.HeaderText = "Cant"
            TextCol4.Width = 80
            Style.GridColumnStyles.Add(TextCol4)
            TextCol4 = Nothing

            dtgPendientes.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("ResizeGrillaIngresados: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Function

    Private Function ResizeGrillaPendiente() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "Productos"
            dtgPendiente.TableStyles.Clear()

            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "codigo"
            TextCol1.HeaderText = "codigo"
            TextCol1.Width = 95
            Style.GridColumnStyles.Add(TextCol1)
            TextCol1 = Nothing

            Dim TextCol2 As New DataGridTextBoxColumn
            TextCol2.MappingName = "producto"
            TextCol2.HeaderText = "Producto"
            TextCol2.Width = 140
            Style.GridColumnStyles.Add(TextCol2)
            TextCol2 = Nothing

            Dim TextCol3 As New DataGridTextBoxColumn
            TextCol3.MappingName = "can"
            TextCol3.HeaderText = "can"
            TextCol3.Width = 30
            Style.GridColumnStyles.Add(TextCol3)
            TextCol3 = Nothing

            dtgPendiente.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("ResizeGrillaIngresados: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Function
    Private Function GetCargarPendiente() As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = "Mob_Get_Prod_OC_Pend"

                Pa = New SqlParameter("@PROVEEDOR_ID", SqlDbType.VarChar, 20)
                Pa.Value = Proveedor_ID
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@ID", SqlDbType.VarChar, 20)
                Pa.Value = Me.SecuenciaId ' Vsecuencia 'Nro_OC
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                'Pa = New SqlParameter("@ORDEN_COMPRA", SqlDbType.VarChar, 100)
                'Pa.Value = vOC
                'xCmd.Parameters.Add(Pa)

                Da.Fill(Ds, "Productos")

                'dtgProveedor.DataSource = Ds.Tables("sucursal")
                dtgPendiente.DataSource = Ds.Tables("Productos")
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
                xCmd.CommandText = "Mob_Get_Producto_OC"

                Pa = New SqlParameter("@PROVEEDOR_ID", SqlDbType.VarChar, 20)
                Pa.Value = Proveedor_ID
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                'Pa = New SqlParameter("@ORDEN_COMPRA", SqlDbType.VarChar, 100)
                'Pa.Value = vOC
                'xCmd.Parameters.Add(Pa)

                Da.Fill(Ds, "Producto")

                'dtgProveedor.DataSource = Ds.Tables("sucursal")
                dtgPendientes.DataSource = Ds.Tables("Producto")
                'If dtgSucursal.VisibleRowCount < 1 Then
                'btnEliminar.Enabled = False
                'btnAjustar.Enabled = False
                'Else
                'btnEliminar.Enabled = True
                'btnAjustar.Enabled = True
                'End If
            Else : MsgBox(SqlError, MsgBoxStyle.Critical, FrmName)
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
        VProducto = ""
        VProductoDes = ""
        Me.Close()
    End Sub

    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeleccionar.Click
        seleccionar()
    End Sub
    Sub seleccionar()
        Try
            Me.Producto_ID = Me.dtgPendientes.Item(dtgPendientes.CurrentRowIndex, 0).ToString
            Me.Producto_Des = Me.dtgPendientes.Item(dtgPendientes.CurrentRowIndex, 1).ToString
            'Me.Orden_ID = Me.dtgOc.Item(dtgOc.CurrentRowIndex, 0).ToString
            Me.Close()
        Catch ex As Exception
        Finally
            FrmOrdenCompra = Nothing
        End Try
    End Sub

    Private Sub dtgProducto_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgPendientes.DoubleClick
        Try
            Me.dtgPendientes.Select(dtgPendientes.CurrentRowIndex)
            seleccionar()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dtgProducto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgPendientes.Click
        Try
            Me.dtgPendientes.Select(dtgPendientes.CurrentRowIndex)
            'seleccionar()
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub dtgPendiente_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgPendiente.DoubleClick
    '    'Try
    '    '    Me.dtgPendiente.Select(dtgPendiente.CurrentRowIndex)
    '    '    Me.btnEliminar.Enabled = True
    '    '    BorrarSeleccion()
    '    'Catch ex As Exception
    '    'End Try
    'End Sub
    Sub BorrarSeleccion()
        Try
            Me.Producto_ID = Me.dtgPendiente.Item(dtgPendiente.CurrentRowIndex, 0).ToString
            If Producto_ID <> "" Then
                Me.btnEliminar.Enabled = True
            End If
            'Me.Producto_Des = Me.dtgPendientes.Item(dtgPendientes.CurrentRowIndex, 1).ToString
            'Me.Orden_ID = Me.dtgOc.Item(dtgOc.CurrentRowIndex, 0).ToString
            Me.Close()
        Catch ex As Exception
        Finally
            FrmOrdenCompra = Nothing
        End Try
    End Sub

    Private Sub dtgPendiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgPendiente.Click
        Try
            Me.dtgPendiente.Select(dtgPendiente.CurrentRowIndex)
            Producto_ID = Me.dtgPendiente.Item(dtgPendiente.CurrentRowIndex, 0).ToString
            CANTIDAD = Me.dtgPendiente.Item(dtgPendiente.CurrentRowIndex, 2).ToString
            If Producto_ID <> "" Then
                Me.btnEliminar.Enabled = True
            Else
                Me.btnEliminar.Enabled = False
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        eliminar()
    End Sub

    Sub eliminar()
        'Me.btneliminar.Enabled = False
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim Cmd As SqlCommand
        Try
            'Me.CodProducto = dtgIngresados.Item(dtgIngresados.CurrentRowIndex, 1).ToString()
            Dim Rta As Object = MsgBox("¿Confirma que va a eliminar el Producto " & Producto_ID & "?", MsgBoxStyle.YesNo, FrmName)
            If Rta = vbYes Then
                If VerifyConnection(SQLc) Then
                    Cmd = SQLc.CreateCommand
                    'Cmd.Transaction = SQLc.BeginTransaction()
                    Da = New SqlDataAdapter(Cmd)

                    Cmd.CommandText = "MOB_DELETE_PRODUCTO_TMP"
                    Cmd.CommandType = Data.CommandType.StoredProcedure

                    Pa = New SqlParameter("@ID", SqlDbType.VarChar, 20)
                    Pa.Value = idSecuencia
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                    Pa.Value = Producto_ID
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@cantidad", SqlDbType.Float)
                    Pa.Value = CANTIDAD
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Cmd.ExecuteNonQuery()
                    'Cmd.Transaction.Commit()
                    Me.btnEliminar.Enabled = False
                    GetCargar()
                    GetCargarPendiente()
                    'CodProducto = ""
                    'GetIngresados()

                End If
            End If
        Catch ex As Exception
            MsgBox("Error al Borrar: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub FrmBuscarProducto_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F2
                Me.Close()
            Case Keys.F3 And Me.btnEliminar.Enabled = True
                eliminar()
            Case Keys.F1
                seleccionar()
        End Select
    End Sub
End Class