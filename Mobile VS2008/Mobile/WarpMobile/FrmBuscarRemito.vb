Imports System.Data.SqlClient
Imports System.Data

Public Class FrmBuscarRemito
    Private Const FrmName As String = "Busqueda de proveedores"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private CodProveedor As String
    Private remito As String
    Private idSecuencia As String

    Public Property SecuenciaId() As String
        Get
            Return idSecuencia
        End Get
        Set(ByVal value As String)
            idSecuencia = value
        End Set
    End Property

    'Public ReadOnly Property Proveedor_ID() As String
    '    Get
    '        Return CodProveedor
    '    End Get
    'End Property
    Public Property Proveedor_ID() As String
        Get
            Return CodProveedor
        End Get
        Set(ByVal value As String)
            CodProveedor = value
        End Set
    End Property


    Private Sub FrmBuscarProveedor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If GetCargar() Then
            ResizeGrillaIngresados()
            Me.dtgRemito.Focus()
        End If
    End Sub

    Private Function ResizeGrillaIngresados() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "Remito"
            dtgRemito.TableStyles.Clear()

            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "REMITO"
            TextCol1.HeaderText = "Remito"
            TextCol1.Width = 140
            Style.GridColumnStyles.Add(TextCol1)
            TextCol1 = Nothing

            'Dim TextCol2 As New DataGridTextBoxColumn
            'TextCol2.MappingName = "Sucursal"
            'TextCol2.HeaderText = "Sucursal"
            'TextCol2.Width = 120
            'Style.GridColumnStyles.Add(TextCol2)
            'TextCol2 = Nothing

            Me.dtgRemito.TableStyles.Add(Style)

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
                xCmd.CommandText = "MobGetRemitoTmp"

                Pa = New SqlParameter("@IDPROVEEDOR", SqlDbType.VarChar, 20)
                Pa.Value = Me.Proveedor_ID
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Id_remito", SqlDbType.VarChar, 20)
                Pa.Value = Me.SecuenciaId ' Vsecuencia 'Nro_OC
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                'Pa = New SqlParameter("@ORDEN_COMPRA", SqlDbType.VarChar, 100)
                'Pa.Value = vOC
                'xCmd.Parameters.Add(Pa)

                Da.Fill(Ds, "Remito")

                dtgRemito.DataSource = Ds.Tables("Remito")

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

    'Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btneliminar.Click

    '    'Try
    '    '    Me.CodProveedor = Me.dtgRemito.Item(dtgRemito.CurrentRowIndex, 0).ToString
    '    '    Me.Close()
    '    'Catch ex As Exception
    '    'Finally
    '    '    FrmOrdenCompra = Nothing
    '    'End Try
    'End Sub

    Private Sub dtgSucursal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgRemito.Click
        Try
            Me.dtgRemito.Select(dtgRemito.CurrentRowIndex)
            remito = Me.dtgRemito.Item(dtgRemito.CurrentRowIndex, 0).ToString
            If remito <> "" Then
                Me.btneliminar.Enabled = True
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btneliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btneliminar.Click
        eliminar()
    End Sub

    Sub eliminar()
        'Me.btneliminar.Enabled = False
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim Cmd As SqlCommand
        Try
            'Me.CodProducto = dtgIngresados.Item(dtgIngresados.CurrentRowIndex, 1).ToString()
            Dim Rta As Object = MsgBox("¿Confirma que va a eliminar el remito " & remito & "?", MsgBoxStyle.YesNo, FrmName)
            If Rta = vbYes Then
                If VerifyConnection(SQLc) Then
                    Cmd = SQLc.CreateCommand
                    Cmd.Transaction = SQLc.BeginTransaction()
                    Da = New SqlDataAdapter(Cmd)

                    Cmd.CommandText = "MOB_DELETE_REMITO_TMP"
                    Cmd.CommandType = Data.CommandType.StoredProcedure
                    Pa = New SqlParameter("@ID_PROVEEDOR", SqlDbType.VarChar, 20)
                    Pa.Value = CodProveedor
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@REMITO", SqlDbType.VarChar, 20)
                    Pa.Value = remito
                    'dtgIngresados.Item(dtgIngresados.CurrentRowIndex, 1).ToString()
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Cmd.ExecuteNonQuery()
                    Cmd.Transaction.Commit()
                    Me.btneliminar.Enabled = False
                    GetCargar()
                    'CodProducto = ""
                    'GetIngresados()

                End If
            End If
        Catch SQLEx As SqlException
            Cmd.Transaction.Rollback()
            MsgBox("Error al Borrar: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
        Catch ex As Exception
            Cmd.Transaction.Rollback()
            MsgBox("Error al Borrar: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Cmd = Nothing
        End Try
    End Sub

    Private Sub FrmBuscarRemito_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1 And Me.btneliminar.Enabled = True
                eliminar()
                Me.btneliminar.Enabled = False
            Case Keys.F2
                Me.Close()
        End Select
    End Sub
End Class