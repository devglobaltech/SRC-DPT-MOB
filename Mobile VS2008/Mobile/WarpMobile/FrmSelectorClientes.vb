Imports System.Data.SqlClient
Imports System.Data

Public Class FrmSelectorClientes

#Region "Declaraciones"
    Private Const SQLConErr As String = "No se pudo conectar a la base de datos."
    Private Const FrmName As String = "Seleccion de Clientes"
    Private blnCancel As Boolean = False

    Public Property Cancel() As Boolean
        Get
            Return blnCancel
        End Get
        Set(ByVal value As Boolean)
            blnCancel = value
        End Set
    End Property

#End Region


    Private Sub FrmSelectorClientes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillCmb()
    End Sub

    Private Sub FillCmb()
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
                xCmd.CommandText = "DBO.GET_CLIENTES_BY_USER"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@USER", SqlDbType.VarChar, 30)
                Pa.Value = vUsr.CodUsuario
                xCmd.Parameters.Add(Pa)
                xCmd.Connection = SQLc
                Da.Fill(Ds, "CLIENTES")
                dt.Columns.Add("RazonSocial", GetType(System.String))
                dt.Columns.Add("Cliente_id", GetType(System.String))
                If Ds.Tables("CLIENTES").Rows.Count > 0 Then
                    'Hay mas de un cliente, los cargo en el combo.
                    For Each drDSRow In Ds.Tables("CLIENTES").Rows()
                        drNewRow = dt.NewRow()
                        drNewRow("RazonSocial") = drDSRow("RazonSocial")
                        drNewRow("Cliente_id") = drDSRow("Cliente_id")
                        dt.Rows.Add(drNewRow)
                    Next
                    Me.cmbClientes.DropDownStyle = ComboBoxStyle.DropDownList
                    With cmbClientes
                        .DataSource = dt
                        .DisplayMember = "RazonSocial"
                        .ValueMember = "Cliente_id"
                        .SelectedIndex = 0
                    End With
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, FrmName)
            End If
        Catch SqlEx As SqlException
            MsgBox("SQL. " & SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            Da = Nothing
            Ds = Nothing
            Pa = Nothing
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        blnCancel = True
        Me.Close()
    End Sub

    Private Sub cmbClientes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbClientes.KeyUp
        If e.KeyValue = 13 Then
            vUsr.ClienteActivo = Trim(Me.cmbClientes.SelectedValue)
            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        vUsr.ClienteActivo = Trim(Me.cmbClientes.SelectedValue)
        Me.Close()
    End Sub

    Private Sub cmbClientes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClientes.SelectedIndexChanged

    End Sub
End Class