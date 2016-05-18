Imports System.Data.SqlClient
Imports System.Data
Public Class frmPrueba
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private Const FrmName As String = "Prueba"

    Private Sub frmPrueba_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Da As SqlDataAdapter
        Dim Ds As New System.Data.DataSet
        Dim drDSRow As Data.DataRow
        Dim drNewRow As Data.DataRow
        Dim dt As New Data.DataTable
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.CARGA_MENU"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                xCmd.Connection = SQLc
                Da.Fill(Ds, "MENUS")
                dt.Columns.Add("CODIGO_ID", GetType(System.String))
                dt.Columns.Add("DESCRIPCION", GetType(System.String))
                If Ds.Tables("MENUS").Rows.Count > 0 Then
                    'Hay mas de un cliente, los cargo en el combo.
                    For Each drDSRow In Ds.Tables("MENUS").Rows()
                        drNewRow = dt.NewRow()
                        drNewRow("CODIGO_ID") = drDSRow("CODIGO_ID")
                        drNewRow("DESCRIPCION") = drDSRow("DESCRIPCION")
                        dt.Rows.Add(drNewRow)
                    Next
                    drNewRow = dt.NewRow()
                    drNewRow("CODIGO_ID") = "999"
                    drNewRow("DESCRIPCION") = "SALIR"
                    dt.Rows.Add(drNewRow)
                    With LBMenu
                        .DataSource = Nothing
                        .DataSource = dt
                        .DisplayMember = "DESCRIPCION"
                        .ValueMember = "CODIGO_ID"
                        .SelectedIndex = 0
                    End With
                Else
                    Me.Close()
                    Exit Sub
                End If
            Else : MsgBox(SqlError, MsgBoxStyle.Exclamation, FrmName)
            End If
        Catch SQLEx As SqlException
            MsgBox("ExisteNavePosicion SQL: " & SQLEx.Message)
        Catch ex As Exception
            MsgBox("ExisteNavePosicion: " & ex.Message)
        Finally
            Da = Nothing
            Ds = Nothing
            Pa = Nothing
        End Try
    End Sub
End Class