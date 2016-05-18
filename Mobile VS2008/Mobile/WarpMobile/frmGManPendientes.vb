Imports System.Data
Imports System.Data.SqlClient

Public Class frmGManPendientes

    Private DocumentoID As Long
    Private IsContenedora As Boolean = False
    Private Const FrmName As String = "Pendientes de Ubicacion"
    Private Const StrConn As String = "No se pudo reestablecer la conexion con la base de datos"

    Public Property vDocumento() As Long
        Get
            Return DocumentoID
        End Get
        Set(ByVal value As Long)
            DocumentoID = value
        End Set
    End Property
    Public Property FlagContenedora() As Boolean
        Get
            Return IsContenedora
        End Get
        Set(ByVal value As Boolean)
            IsContenedora = value
        End Set
    End Property

    Private Sub frmGManPendientes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Me.Close()
        End Select
    End Sub

    Private Sub frmGManPendientes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblIngreso.Text = "Nro. de Ingreso: " & DocumentoID
        If GetValuesForGrid() Then
            Pendientes()
        End If
    End Sub

    Private Function Pendientes() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "PENDIENTES"
            DgPendientes.TableStyles.Clear()
            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "PRODUCTO_ID"
            TextCol1.HeaderText = "Cod. Producto"
            TextCol1.Width = 120
            Style.GridColumnStyles.Add(TextCol1)
            TextCol1 = Nothing

            Dim TextCol2 As New DataGridTextBoxColumn
            TextCol2.MappingName = "NRO_BULTO"
            TextCol2.HeaderText = "Contenedora"
            TextCol2.Width = 80
            Style.GridColumnStyles.Add(TextCol2)
            TextCol2 = Nothing

            Dim TextCol3 As New DataGridTextBoxColumn
            TextCol3.MappingName = "CANTIDAD"
            TextCol3.HeaderText = "Cantidad"
            TextCol3.Width = 120
            Style.GridColumnStyles.Add(TextCol3)
            TextCol3 = Nothing

            Dim TextCol4 As New DataGridTextBoxColumn
            TextCol4.MappingName = "DESCRIPCION"
            TextCol4.HeaderText = "Descripcion"
            TextCol4.Width = 120
            Style.GridColumnStyles.Add(TextCol4)
            TextCol4 = Nothing



            DgPendientes.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("ResizeGrillaUbicacion: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Function
    Private Function GetValuesForGrid() As Boolean
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = New SqlCommand()
                Da = New SqlDataAdapter(Cmd)
                Cmd.Connection = SQLc
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = "DBO.MOB_GUARDADO_PENDIENTES"

                Pa = New SqlParameter("@DOCUMENTO_ID", SqlDbType.BigInt)
                Pa.Value = DocumentoID
                Cmd.Parameters.Add(Pa)

                Da.Fill(Ds, "PENDIENTES")
                Me.dgPendientes.DataSource = Ds.Tables("PENDIENTES")

            Else : MsgBox(StrConn, MsgBoxStyle.Information, FrmName)
                Return True
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            Ds = Nothing
            Da = Nothing
            Cmd = Nothing
        End Try
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class