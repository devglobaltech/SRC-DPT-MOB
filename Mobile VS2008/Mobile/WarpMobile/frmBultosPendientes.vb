Imports System.Data.SqlClient
Imports System.Data

Public Class frmBultosPendientes
    Public sGuia As String
    Private Const FrmName As String = "Busqueda de Producto"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private Sub frmBultosPendientes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'traer ingresados
        If GetUbicadosGuia() And GetPendientesGuia() Then
            SetGrillas()
        End If
    End Sub
    Private Sub SetGrillas()
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "Bultos"

            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "UC_EMPAQUE"
            TextCol1.HeaderText = "BULTO"
            TextCol1.Width = 40
            Style.GridColumnStyles.Add(TextCol1)
            TextCol1 = Nothing

            Dim TextCol2 As New DataGridTextBoxColumn
            TextCol2.MappingName = "ALTO"
            TextCol2.HeaderText = "ALTO"
            TextCol2.Width = 43
            Style.GridColumnStyles.Add(TextCol2)
            TextCol2 = Nothing

            Dim TextCol3 As New DataGridTextBoxColumn
            TextCol3.MappingName = "ANCHO"
            TextCol3.HeaderText = "ANCHO"
            TextCol3.Width = 43
            Style.GridColumnStyles.Add(TextCol3)
            TextCol3 = Nothing

            Dim TextCol4 As New DataGridTextBoxColumn
            TextCol4.MappingName = "LARGO"
            TextCol4.HeaderText = "LARGO"
            TextCol4.Width = 43
            Style.GridColumnStyles.Add(TextCol4)
            TextCol4 = Nothing

            Dim TextCol5 As New DataGridTextBoxColumn
            TextCol5.MappingName = "NRO_GUIA"
            TextCol5.HeaderText = "GUIA"
            TextCol5.Width = 43
            Style.GridColumnStyles.Add(TextCol5)
            TextCol5 = Nothing

            dtgPendientes.TableStyles.Clear()
            dtgIngresados.TableStyles.Clear()

            'dtgPendientes.TableStyles.Add(Style)
            dtgIngresados.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("SetGrillas: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Sub

    Private Function GetUbicadosGuia() As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = "GetUbicadosGuia"

                xCmd.Parameters.Add("@GUIA", SqlDbType.VarChar, 20).Value = sGuia
                xCmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 10).Value = vUsr.CodUsuario
                Da.Fill(Ds, "Bultos")
                dtgIngresados.DataSource = Ds.Tables("Bultos")

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
    Private Function GetPendientesGuia() As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = "GetpendientesGuia"

                xCmd.Parameters.Add("@GUIA", SqlDbType.VarChar, 20).Value = sGuia
                xCmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 10).Value = vUsr.CodUsuario
                Da.Fill(Ds, "Bultos")
                dtgPendientes.DataSource = Ds.Tables("Bultos")

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
End Class