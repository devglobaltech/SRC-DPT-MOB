Imports System.Data.SqlClient
Imports System.Data


Public Class FrmAPFPendientes

    Private Const FrmName As String = "Armado Pallet Final - Pendientes"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."

    Private Cliente As String = ""
    Public Property pCliente() As String
        Get
            Return Cliente
        End Get
        Set(ByVal value As String)
            Cliente = value
        End Set
    End Property

    Private Viaje As String = ""
    Public Property pViaje() As String
        Get
            Return Viaje
        End Get
        Set(ByVal value As String)
            Viaje = value
        End Set
    End Property

    Private Function GetPendientes() As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = "DBO.MOB_APF_PENDIENTE"

                Pa = New SqlParameter("@Cliente_ID", SqlDbType.VarChar, 20)
                Pa.Value = pCliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Viaje_ID", SqlDbType.VarChar, 100)
                Pa.Value = pViaje
                xCmd.Parameters.Add(Pa)

                Da.Fill(Ds, "Pendientes")

                DgPendientes.DataSource = Ds.Tables("Pendientes")

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

    Private Function ResizeGrillaUbicacion() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "Pendientes"
            DgPendientes.TableStyles.Clear()

            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "PRODUCTO"
            TextCol1.HeaderText = "Cod. Producto"
            TextCol1.Width = 120
            Style.GridColumnStyles.Add(TextCol1)
            TextCol1 = Nothing

            Dim TextCol2 As New DataGridTextBoxColumn
            TextCol2.MappingName = "PENDIENTES"
            TextCol2.HeaderText = "Pendientes"
            TextCol2.Width = 120
            Style.GridColumnStyles.Add(TextCol2)
            TextCol2 = Nothing

            DgPendientes.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("ResizeGrillaUbicacion: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Function

    Private Sub FrmAPFPendientes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F2
                Salir()
        End Select
    End Sub

    Private Sub Salir()
        Me.Close()
    End Sub

    Private Sub FrmAPFPendientes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If GetPendientes() Then
            Me.lblCliente.Text = "Cliente: " & pCliente
            Me.lblCodigoViaje.Text = "Codigo Viaje: " & pViaje
            ResizeGrillaUbicacion()
            Me.DgPendientes.Focus()
        End If
    End Sub

    Private Sub DgPendientes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DgPendientes.KeyUp
        Try
            DgPendientes.Select(DgPendientes.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmdSalir_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Salir()
    End Sub

    Private Sub chkActiva_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActiva.CheckStateChanged
        If Me.chkActiva.CheckState = CheckState.Checked Then
            tmr.Enabled = True
        Else
            tmr.Enabled = False
        End If
    End Sub

    Private Sub tmr_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmr.Tick
        If GetPendientes() Then
            ResizeGrillaUbicacion()
            Me.DgPendientes.Focus()
            Application.DoEvents()
            If Me.DgPendientes.VisibleRowCount = 0 Then
                tmr.Enabled = False
                Me.chkActiva.CheckState = CheckState.Unchecked
                MsgBox("No quedan productos pendientes para el viaje " & pViaje & ".", MsgBoxStyle.Information, FrmName)
            End If
        End If
    End Sub
End Class