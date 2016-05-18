Imports System.Data
Imports System.Data.SqlClient

Public Class FrmAPF_Abiertos

    Private Const FrmName As String = "Armado Pallet Final"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private Cliente As String = ""
    Private CodigoViaje As String = ""
    Private Pallet As Long = 0

    Public Property pCliente() As String
        Get
            Return Cliente
        End Get
        Set(ByVal value As String)
            Cliente = value
        End Set
    End Property

    Public Property pCodigoViaje() As String
        Get
            Return CodigoViaje
        End Get
        Set(ByVal value As String)
            CodigoViaje = value
        End Set
    End Property

    Public ReadOnly Property GetPallet() As Long
        Get
            Return Pallet
        End Get
    End Property

    Private Sub FrmAPF_Abiertos_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case 112 'Keys.F1
                Aceptar()
            Case 113 'Keys.F2
                Me.Close()
            Case 114 'Keys.F3
                ReImpresion(1)
        End Select
    End Sub

    Private Function ReImpresion(ByVal Tipo As String) As Boolean
        Dim xSQL As String
        Dim Cmd As SqlCommand
        Dim Pallet As String
        Try
            If VerifyConnection(SQLc) Then
                Try

                    Cmd = SQLc.CreateCommand
                    Cmd.CommandType = Data.CommandType.Text
                    Pallet = Me.DgPallet.Item(DgPallet.CurrentRowIndex, 0)
                    'LRojas Tracker ID 3806 05/03/2012: Inserción de Usuario para Demonio de Impresion
                    xSQL = "insert into IMPRESION_APF values(" & Pallet & ",'" & Tipo & "','0','" & pCodigoViaje & "', '" & vUsr.CodUsuario & "')"
                    Cmd.CommandText = xSQL
                    Cmd.ExecuteNonQuery()
                Catch ex As Exception 'Esto complicado de tiempo.
                End Try
                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            Cmd = Nothing
        End Try
    End Function

    Private Sub FrmAPF_Abiertos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblCliente.Text = "Cliente: " & Cliente
        Me.lblCodigoViaje.Text = "Codigo de Viaje: " & CodigoViaje
        ObtenerPallet()
        ResizeGrillaUbicacion()
        Me.DgPallet.Focus()
        If Me.DgPallet.VisibleRowCount = 0 Then
            MsgBox("No hay pallets abiertos", MsgBoxStyle.Information, FrmName)
            Me.Close()
        End If
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Me.Close()
    End Sub

    Private Function ObtenerPallet() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "dbo.Mob_Get_Pallet_Final"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()
                Da = New SqlDataAdapter(Cmd)

                Pa = New SqlParameter("@Cliente_Id", SqlDbType.VarChar, 20)
                Pa.Value = Trim(UCase(Cliente))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@viaje_id", SqlDbType.VarChar, 20)
                Pa.Value = Trim(UCase(CodigoViaje))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Da.Fill(Ds, "Pallets")
                Me.DgPallet.DataSource = Ds.Tables("Pallets")
                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("SQL CerrarPFinal: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("CerrarPFinal: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Da = Nothing
            Ds = Nothing
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function ResizeGrillaUbicacion() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "Pallets"
            DgPallet.TableStyles.Clear()

            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "PALLET_FINAL"
            TextCol1.HeaderText = "Pallet Final"
            TextCol1.Width = 120
            Style.GridColumnStyles.Add(TextCol1)
            TextCol1 = Nothing

            Dim TextCol2 As New DataGridTextBoxColumn
            TextCol2.MappingName = "STATUS"
            TextCol2.HeaderText = "Status"
            TextCol2.Width = 120
            Style.GridColumnStyles.Add(TextCol2)
            TextCol2 = Nothing

            DgPallet.TableStyles.Add(Style)
        Catch ex As Exception
            MsgBox("ResizeGrillaUbicacion: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Function

    Private Sub DgPallet_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DgPallet.KeyUp
        Try
            DgPallet.Select(DgPallet.CurrentRowIndex)
            If e.KeyValue = 13 Then
                Pallet = Me.DgPallet.Item(DgPallet.CurrentRowIndex, 0)
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub cmdAbrir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAbrir.Click
        Aceptar()
    End Sub

    Private Sub DgPallet_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DgPallet.CurrentCellChanged

    End Sub

    Private Sub Aceptar()
        Try
            If Me.DgPallet.Item(DgPallet.CurrentRowIndex, 1) = "CERRADO" Then
                If MsgBox("El pallet se encuentra cerrado, " & vbNewLine & "¿Desea abrirlo?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                    ReAbrirPallet(Me.DgPallet.Item(DgPallet.CurrentRowIndex, 0))
                    DgPallet.Select(DgPallet.CurrentRowIndex)
                    Pallet = Me.DgPallet.Item(DgPallet.CurrentRowIndex, 0)
                    Me.Close()
                Else
                    Exit Sub
                End If
            Else
                DgPallet.Select(DgPallet.CurrentRowIndex)
                Pallet = Me.DgPallet.Item(DgPallet.CurrentRowIndex, 0)
                Me.Close()
            End If
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Function ReAbrirPallet(ByVal Pallet As String) As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "DBO.Mob_AbrirPalletCerrado"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@Pallet", SqlDbType.BigInt)
                Pa.Value = Trim(UCase(Pallet))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Cmd.ExecuteNonQuery()
            Else
                MsgBox(SQLError, MsgBoxStyle.OkOnly)
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("SQL AbrirPalletFinal: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("AbrirPalletFinal: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try

    End Function

    Private Sub cmdReImpresion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReImpresion.Click
        ReImpresion(1)
    End Sub
End Class