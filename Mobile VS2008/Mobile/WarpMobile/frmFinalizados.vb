Imports System.Data
Imports System.Data.SqlClient

Public Class frmFinalizados
    Private dsClone As New DataSet
    Private Ds As New DataSet
    Private Const FrmName As String = "Finalizados Egreso."
    Private Const vMenu As String = "F1) Salir"
    Private oCmd As SqlCommand
    Private vViajeId As String

    Public Property ViajeId() As String
        Get
            Return vViajeId
        End Get
        Set(ByVal value As String)
            vViajeId = value
        End Set
    End Property

    Public Property Cmd() As SqlCommand
        Get
            Return oCmd
        End Get
        Set(ByVal value As SqlCommand)
            oCmd = value
        End Set
    End Property

    Enum DsE
        Documento_id = 0
        Nro_linea = 1
        Producto_id
        Descripcion
        Cantidad
        Nave_Cod
        Posicion
        Ruta
        Prop1
        Terminado
    End Enum

    Enum DseClone
        Cod_Producto = 0
        Cantidad = 1
        Posicion = 2
        Pallet = 3
    End Enum

    Private Sub frmFinalizados_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Me.Close()
        End Select
    End Sub

    Private Sub frmFinalizados_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If GET_FINALIZADOS(Ds, "PCUR") Then
            Me.Text = "Picking Finalizado. Viaje: " & vViajeId
            If CloneDs(Ds, "PCUR") Then
                Me.dgFinalizados.DataSource = dsClone.Tables("PCUR")
                ResizeGrilla()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Function CloneDs(ByVal Ds As DataSet, ByVal Table As String) As Boolean
        Try
            Dim i As Integer = 0
            Dim j As Integer = 0
            dsClone.Tables.Add(Table)
            'Para las columnas.
            For i = 0 To Ds.Tables(Table).Columns.Count - 1
                dsClone.Tables(Table).Columns.Add(Ds.Tables(Table).Columns(i).Caption)
            Next i
            'Para los Rows.
            For j = 0 To Ds.Tables(Table).Rows.Count - 1
                dsClone.Tables(Table).Rows.Add(Ds.Tables(Table).Rows(j)(0), _
                                               CInt(Ds.Tables(Table).Rows(j)(1)), _
                                               Ds.Tables(Table).Rows(j)(2), _
                                               Ds.Tables(Table).Rows(j)(3), _
                                               Ds.Tables(Table).Rows(j)(4))

            Next j
            Return True
        Catch ex As Exception
            MsgBox("CloneDs. " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return True
        End Try
    End Function

    Private Function GET_FINALIZADOS(ByRef Ds As DataSet, ByVal Table As String) As Boolean
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Try
            Cmd.Parameters.Clear()
            Cmd.CommandText = "Picking_Completado"
            Cmd.CommandType = CommandType.StoredProcedure
            Da = New SqlDataAdapter(Cmd)

            Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 20)
            Pa.Value = vUsr.CodUsuario
            Cmd.Parameters.Add(Pa)
            Pa = Nothing

            Pa = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 30)
            Pa.Value = ViajeId
            Cmd.Parameters.Add(Pa)

            Da.Fill(Ds, Table)
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message & " Get_Finalizados", MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message & " Get_Finalizados", MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Pa = Nothing
            Da = Nothing
        End Try
    End Function

    Private Function ResizeGrilla() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "PCUR"
            dgFinalizados.TableStyles.Clear()
            Style.MappingName = "PCUR"

            Dim TextCol1 As New DataGridTextBoxColumn
            With TextCol1
                .MappingName = "Cod_Producto"
                .HeaderText = "Cod. Prod."
                .Width = 60
            End With
            Style.GridColumnStyles.Add(TextCol1)

            Dim TextCol2 As New DataGridTextBoxColumn
            With TextCol2
                .MappingName = "Cantidad"
                .HeaderText = "Cantidad"
                .Width = 60
            End With
            Style.GridColumnStyles.Add(TextCol2)

            Dim TextCol3 As New DataGridTextBoxColumn
            With TextCol3
                .MappingName = "Posicion"
                .HeaderText = "Posicion."
                .Width = 100
            End With
            Style.GridColumnStyles.Add(TextCol3)

            Dim TextCol4 As New DataGridTextBoxColumn
            With TextCol4
                .MappingName = "Pallet"
                .HeaderText = "Pallet"
                .Width = 70
            End With
            Style.GridColumnStyles.Add(TextCol4)

            Dim TextCol5 As New DataGridTextBoxColumn
            With TextCol5
                .MappingName = "PALLET_PICKING"
                .HeaderText = "Pallet Pick."
                .Width = 70
            End With
            Style.GridColumnStyles.Add(TextCol5)

            dgFinalizados.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("ResizeGrilla: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Function

    Private Sub dgFinalizados_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgFinalizados.GotFocus
        Try
            dgFinalizados.Select(dgFinalizados.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgFinalizados_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgFinalizados.KeyUp
        Try
            dgFinalizados.Select(dgFinalizados.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Me.Close()
    End Sub

End Class