Imports System.Data.SqlClient
Imports System.Data

Public Class frmTansfPickingCompletadas

    Private Const FrmName As String = "Transferencias Picking - Completadas"
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
    Private Sub cmdSalir_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub chkActiva_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frmTansfPickingCompletadas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblCliente.Text = "Cliente: " & pCliente
        Me.lblCodigoViaje.Text = "Codigo Viaje: " & pViaje
        Me.GetCompletados()
        ResizeGrillaUbicacion()
        Me.Focus()
    End Sub
    Private Function ResizeGrillaUbicacion() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "MOVIMIENTOS"
            DgTranfCompletadas.TableStyles.Clear()

            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "PALLET"
            TextCol1.HeaderText = "Pallet"
            TextCol1.Width = 50
            Style.GridColumnStyles.Add(TextCol1)
            TextCol1 = Nothing

            Dim TextCol2 As New DataGridTextBoxColumn
            TextCol2.MappingName = "CONTENEDORA"
            TextCol2.HeaderText = "Cont."
            TextCol2.Width = 50
            Style.GridColumnStyles.Add(TextCol2)
            TextCol2 = Nothing

            Dim TextCol3 As New DataGridTextBoxColumn
            TextCol3.MappingName = "UBICACION_ORIGEN"
            TextCol3.HeaderText = "Ubicacion Origen"
            TextCol3.Width = 110
            Style.GridColumnStyles.Add(TextCol3)
            TextCol3 = Nothing

            DgTranfCompletadas.TableStyles.Add(Style)


        Catch ex As Exception
            MsgBox("ResizeGrillaUbicacion: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Function

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Me.Close()
    End Sub
    Private Function GetCompletados() As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = "TRANF_PREPICKING_COMPLETADAS"

                Pa = New SqlParameter("@Viaje_ID", SqlDbType.VarChar, 50)
                Pa.Value = pViaje
                xCmd.Parameters.Add(Pa)

                Da.Fill(Ds, "MOVIMIENTOS")

                Me.DgTranfCompletadas.DataSource = Ds.Tables("MOVIMIENTOS")

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

    Private Sub tmr_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmr.Tick
        If Me.GetCompletados() Then
            ResizeGrillaUbicacion()
            Me.DgTranfCompletadas.Focus()
            Application.DoEvents()
            If Me.DgTranfCompletadas.VisibleRowCount = 0 Then
                tmr.Enabled = False
                Me.chkActiva.CheckState = CheckState.Unchecked
                MsgBox("No quedan productos pendientes para el viaje " & pViaje & ".", MsgBoxStyle.Information, FrmName)
            End If
        End If
    End Sub

    Private Sub chkActiva_CheckStateChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActiva.CheckStateChanged
        If Me.chkActiva.CheckState = CheckState.Checked Then
            tmr.Enabled = True
        Else
            tmr.Enabled = False
        End If
    End Sub

    Private Sub frmTansfPickingCompletadas_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case 113 'Keys.F2
                Me.Close()
        End Select
    End Sub

    Private Sub DGCatLog_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub
End Class