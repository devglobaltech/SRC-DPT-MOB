Imports System.Data.SqlClient
Imports System.Data

Public Class FrmBuscaOc

    Private Const FrmName As String = "Busqueda de Orden de compra"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private VOrden As String
    Private idSecuencia As String
    Public Property SecuenciaId() As String
        Get
            Return idSecuencia
        End Get
        Set(ByVal value As String)
            idSecuencia = value
        End Set
    End Property

    Public Property Orden_ID() As String
        Get
            Return VOrden
        End Get
        Set(ByVal value As String)
            VOrden = value
        End Set
    End Property

    Private Sub FrmBuscaOc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If GetCargar() Then
            ResizeGrillaIngresados()
            Me.dtgOc.Focus()
        End If
    End Sub

    Private Function ResizeGrillaIngresados() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "OC"
            dtgOc.TableStyles.Clear()

            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "OC"
            TextCol1.HeaderText = "Orden de Compra"
            TextCol1.Width = 220
            Style.GridColumnStyles.Add(TextCol1)
            TextCol1 = Nothing

            Dim TextCol2 As New DataGridTextBoxColumn
            TextCol2.MappingName = "SELECCIONADO"
            TextCol2.HeaderText = "SELECCIONADO"
            TextCol2.Width = 1
            Style.GridColumnStyles.Add(TextCol2)
            TextCol2 = Nothing

            dtgOc.TableStyles.Add(Style)

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
                xCmd.CommandText = "MOB_GET_OC"

                Pa = New SqlParameter("@SUCURSAL", SqlDbType.VarChar, 20)
                Pa.Value = VOrden
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@ID_OC", SqlDbType.VarChar, 20)
                Pa.Value = Me.SecuenciaId ' Vsecuencia 'Nro_OC
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Da.Fill(Ds, "OC")

                'dtgProveedor.DataSource = Ds.Tables("sucursal")
                dtgOc.DataSource = Ds.Tables("OC")
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

    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeleccionar.Click
        seleccionar()
    End Sub

    Sub seleccionar()
        Try
            Me.Orden_ID = Me.dtgOc.Item(dtgOc.CurrentRowIndex, 0).ToString
            Me.Close()
        Catch ex As Exception
        Finally
            FrmOrdenCompra = Nothing
        End Try
    End Sub

    Private Sub dtgOc_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgOc.DoubleClick
        Try
            Me.dtgOc.Select(dtgOc.CurrentRowIndex)
            seleccionar()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dtgOc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgOc.Click
        Try
            Me.dtgOc.Select(dtgOc.CurrentRowIndex)
            'Me.dtgRemito.Select(dtgRemito.CurrentRowIndex)
            Me.VOrden = Me.dtgOc.Item(dtgOc.CurrentRowIndex, 0).ToString
            'remito = Me.dtgRemito.Item(dtgRemito.CurrentRowIndex, 0).ToString
            If VOrden <> "" Then
                Me.btnBorrar.Enabled = True
            Else
                Me.btnBorrar.Enabled = False
            End If
            'If remito <> "" Then
            '    Me.btneliminar.Enabled = True
            'End If

        Catch ex As Exception
        End Try

        'Try
        '    Me.dtgOc.Select(dtgOc.CurrentRowIndex)
        '    'seleccionar()
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        cerrar()
        'Me.Orden_ID = ""
        'Me.Close()
    End Sub
    Sub cerrar()
        Me.Orden_ID = ""
        Me.Close()
    End Sub
    Private Sub btnBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBorrar.Click
        eliminar()
    End Sub
    Sub eliminar()
        Try
            Me.Orden_ID = Me.dtgOc.Item(dtgOc.CurrentRowIndex, 0).ToString
            borrar()
            GetCargar()
            Me.btnBorrar.Enabled = False
            'Me.Close()
        Catch ex As Exception
        Finally
            FrmOrdenCompra = Nothing
        End Try
    End Sub
    Sub borrar()
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = "MOB_DELETE_OC"

                Pa = New SqlParameter("@OC", SqlDbType.VarChar, 20)
                Pa.Value = Me.Orden_ID 'VOrden
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                'Return False
            End If
            'Return True
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Critical, FrmName)
            'Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
            'Return False
        Finally
            Pa = Nothing
            Ds = Nothing
            Cmd = Nothing
            Da = Nothing
        End Try
    End Sub


    Private Sub FrmBuscaOc_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                cerrar()
            Case Keys.F2 And Me.btnBorrar.Enabled = True
                eliminar()
        End Select
    End Sub
End Class