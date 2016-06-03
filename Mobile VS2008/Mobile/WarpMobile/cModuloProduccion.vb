Option Explicit On
Imports System.Data
Imports System.Data.SqlClient

Public Class cModuloProduccion

    Private Const SQLConErr As String = "Fallo al intentar conectar con la base de datos."
    Private Const clsName As String = "Modulo de ensamble"

    Private Conn As SqlConnection
    Private ClienteID As String = ""
    Private DocExt As String = ""
    Private User As String = ""

    Public Property Usuario() As String
        Get
            Return User
        End Get
        Set(ByVal value As String)
            User = value
        End Set
    End Property

    Public Property DocumentoExterno() As String
        Get
            Return DocExt
        End Get
        Set(ByVal value As String)
            DocExt = value
        End Set
    End Property

    Public Property Cliente() As String
        Get
            Return Me.ClienteID
        End Get
        Set(ByVal value As String)
            Me.ClienteID = value
        End Set
    End Property

    Public Property Conexion() As SqlConnection
        Get
            Return Conn
        End Get
        Set(ByVal value As SqlConnection)
            Conn = value
        End Set
    End Property

    Public Function GetClientesByUser(ByRef CMB As ComboBox) As Boolean
        Dim Da As SqlDataAdapter
        Dim Ds As New System.Data.DataSet
        Dim drDSRow As Data.DataRow
        Dim drNewRow As Data.DataRow
        Dim dt As New Data.DataTable
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter

        Try
            If VerifyConnection(Conn) Then
                xCmd = Conn.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.GET_CLIENTES_BY_USER"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@USER", SqlDbType.VarChar, 30)
                Pa.Value = Me.Usuario
                xCmd.Parameters.Add(Pa)
                xCmd.Connection = SQLc
                Da.Fill(Ds, "CLIENTES")
                dt.Columns.Add("RazonSocial", GetType(System.String))
                dt.Columns.Add("Cliente_id", GetType(System.String))
                If Ds.Tables("CLIENTES").Rows.Count > 0 Then

                    For Each drDSRow In Ds.Tables("CLIENTES").Rows()
                        drNewRow = dt.NewRow()
                        drNewRow("RazonSocial") = drDSRow("RazonSocial")
                        drNewRow("Cliente_id") = drDSRow("Cliente_id")
                        dt.Rows.Add(drNewRow)
                    Next
                    CMB.DropDownStyle = ComboBoxStyle.DropDownList
                    With CMB
                        .DataSource = dt
                        .DisplayMember = "RazonSocial"
                        .ValueMember = "Cliente_id"
                        .SelectedIndex = 0
                    End With
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, clsName)
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Critical, clsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, clsName)
            Return False
        Finally
            xCmd = Nothing
            Da = Nothing
            Ds = Nothing
            Pa = Nothing
        End Try
    End Function

End Class
