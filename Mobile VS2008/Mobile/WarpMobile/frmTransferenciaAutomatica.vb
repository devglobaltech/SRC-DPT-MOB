Imports System.Data.SqlClient
Imports System.Data

Public Class frmTransferenciaAutomatica

#Region "Declaraciones"
    Private Const FrmName As String = "Transf. Desconsolidadas."
    Private Const StrConnErr As String = "Fallo al intentar conectar con la base de datos."
    Private strOrigen As String
    Private Ds As DataSet

#End Region

#Region "Acceso a Datos"

    Private Function ConsultaArticulos(ByVal NroPallet As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.Connection = SQLc
                Cmd.CommandText = "TRD_GetProdByPallet"
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Pallet", SqlDbType.VarChar, 100)
                Pa.Value = NroPallet
                Cmd.Parameters.Add(Pa)

                Da.Fill(Ds, "Consulta")

            Else : MsgBox(StrConnErr, MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("Cons_Art SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("Cons_Art: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
            Da = Nothing
        End Try
    End Function

    Private Function GetPosByPallet(ByVal NroPallet As String, ByRef Origen As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.Connection = SQLc
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = "GetPosByPallet"

                Pa = New SqlParameter("@Pallet", SqlDbType.VarChar, 100)
                Pa.Value = NroPallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Pos", SqlDbType.VarChar, 45)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteScalar()

                Origen = IIf(IsDBNull(Cmd.Parameters("@pos").Value), "", Cmd.Parameters("@pos").Value)
                If Origen = "" Then Return False

            Else : MsgBox(StrConnErr, MsgBoxStyle.OkCancel, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("GetPosByPallet SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("GetPosByPallet: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function Transferir(ByVal Origen As String, ByVal Destino As String, ByVal Usuario As String, _
                                ByVal Pallet As String, ByVal ProductoId As String, ByVal Qty As Double, _
                                ByVal PalletD As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Trans As SqlTransaction
        Trans = SQLc.BeginTransaction
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "MOB_TRANSFERENCIA_D"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Cmd.Transaction = Trans


                Pa = New SqlParameter("@POSICION_O", SqlDbType.VarChar, 45)
                Pa.Value = Origen
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_D", SqlDbType.VarChar, 45)
                Pa.Value = Destino
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Usuario", SqlDbType.VarChar, 30)
                Pa.Value = Usuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Pallet", SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = ProductoId
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@QTY", SqlDbType.Float)
                Pa.Value = Qty
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PALLET_D", SqlDbType.VarChar, 100)
                Pa.Value = IIf(Trim(PalletD) = "", DBNull.Value, Trim(PalletD))
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
            Else : MsgBox(StrConnErr, MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Trans.Commit()
            Return True
        Catch SQLEx As SqlException
            Trans.Rollback()
            MsgBox("Transferir SQL: " & SQLEx.Message)
            Return False
        Catch ex As Exception
            Trans.Rollback()
            MsgBox("Transferir: " & ex.Message)
            Return False
        Finally
            Trans = Nothing
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function


#End Region

#Region "Funciones / procedimientos Varios"

    Private Function ResizeGrillaCodigoProductoPallet() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "Consulta"
            Me.dgProductos.TableStyles.Clear()
            Style.MappingName = "Consulta"

            Dim TextCol1 As New DataGridTextBoxColumn
            With TextCol1
                .MappingName = "PRODUCTO_ID"
                .HeaderText = "Prod."
                .Width = 70
            End With
            Style.GridColumnStyles.Add(TextCol1)

            Dim TextCol2 As New DataGridTextBoxColumn
            With TextCol2
                .MappingName = "DESCRIPCION"
                .HeaderText = "Desc."
                .Width = 100
            End With
            Style.GridColumnStyles.Add(TextCol2)

            Dim TextCol3 As New DataGridTextBoxColumn
            With TextCol3
                .MappingName = "UNIDAD_ID"
                .HeaderText = "Unidad"
                .Width = 50
            End With
            Style.GridColumnStyles.Add(TextCol3)

            Dim TextCol4 As New DataGridTextBoxColumn
            With TextCol4
                .MappingName = "QTY"
                .HeaderText = "Cant."
                .Width = 40
            End With
            Style.GridColumnStyles.Add(TextCol4)

            Dim TextCol5 As New DataGridTextBoxColumn
            With TextCol5
                .MappingName = "NRO_LOTE"
                .HeaderText = "Nro. Lote"
                .Width = 70
            End With
            Style.GridColumnStyles.Add(TextCol5)

            Dim TextCol6 As New DataGridTextBoxColumn
            With TextCol6
                .MappingName = "UBICACION"
                .HeaderText = "Ubicacion"
                .Width = 120
            End With
            Style.GridColumnStyles.Add(TextCol6)

            Me.dgProductos.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("ResizeGrillaCodigoProducto: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Function

    Private Sub Salir()
        If MsgBox("¿Desea Salir?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub Cancelar()
        Ds = Nothing
        Ds = New DataSet
        Me.dgProductos.DataSource = Nothing
        Me.dgProductos.Visible = False
        Me.txtPallet.ReadOnly = False
        Me.txtPallet.Text = ""
        Me.txtPallet.Focus()
        Me.CmdTrans.Enabled = False
    End Sub

    Private Sub Procesar()
        Try
            Dim Producto As String = ""
            Dim Descripcion As String = ""
            Dim Trd As New frmTrdPosiciones

            Dim Pallet As String
            Dim Ubicacion As String

            Producto = dgProductos.Item(dgProductos.CurrentRowIndex, 0).ToString
            Descripcion = dgProductos.Item(dgProductos.CurrentRowIndex, 1).ToString
            Pallet = Trim(UCase(Me.txtPallet.Text))
            Ubicacion = dgProductos.Item(dgProductos.CurrentRowIndex, 5).ToString

            Trd.Producto = Producto
            Trd.Descripcion = Descripcion
            Trd.Pallet = Pallet
            Trd.UbicacionOriginal = Ubicacion
            Trd.QTY = CDbl(dgProductos.Item(dgProductos.CurrentRowIndex, 3))
            Trd.ShowDialog()
            If Trd.Salir = False Then

                '------------------------------------
                'MsgBox("Destino: " & Trd.Destino)
                'MsgBox("Pallet: " & Trd.Pallet)
                '------------------------------------
                If MsgBox("¿Confirma la Transferencia?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.No Then
                    Exit Sub
                End If

                'DEBUG
                'MsgBox("Origen: " & strOrigen & ", Trd.Destino: " & Trd.Destino & ", vUsr.CodUsuario: " & vUsr.CodUsuario & ", Me.txtPallet.Text: " & Me.txtPallet.Text & ", Trd.Producto: " & Trd.Producto & ", Trd.QTY: " & Trd.QTY & ", Trd.Pallet: " & Trd.Pallet)
                Dim VINT As Integer = 900000
                If Me.Transferir(strOrigen, Trd.Destino, vUsr.CodUsuario, Me.txtPallet.Text, Trd.Producto, Trd.QTY, Trd.Pallet) Then
                    'Verifico si es el ultimo Item.
                    Me.dgProductos.DataSource = Nothing
                    Me.Ds = Nothing
                    Me.Ds = New DataSet
                    If Me.ConsultaArticulos(Me.txtPallet.Text) And GetPosByPallet(Me.txtPallet.Text, strOrigen) Then
                        Me.dgProductos.Visible = True
                        Me.dgProductos.DataSource = Ds.Tables("Consulta")
                        ResizeGrillaCodigoProductoPallet()
                        Me.dgProductos.Focus()
                        Me.txtPallet.ReadOnly = True
                        Me.CmdTrans.Enabled = True
                    Else : Me.txtPallet.Text = ""
                        Me.txtPallet.ReadOnly = False
                        Me.dgProductos.Visible = False
                    End If

                End If
            Else
                Me.dgProductos.Focus()
            End If
            Trd.Close()
            Trd = Nothing
        Catch ex As Exception
            MsgBox("Procesar: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

#End Region

#Region "Constructor / Destructor"

    Public Sub New()
        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()
        'Constructor.
        Ds = New DataSet
        strOrigen = ""
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        'Destructor.
        Ds = Nothing
    End Sub

#End Region

#Region "Eventos Controles"

    Private Sub txtPallet_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPallet.KeyUp

        If e.KeyValue = 13 And Me.txtPallet.Text <> "" Then
            If Me.ConsultaArticulos(Me.txtPallet.Text) And GetPosByPallet(Me.txtPallet.Text, strOrigen) Then
                'MsgBox("Origen: " & strOrigen)
                Me.dgProductos.Visible = True
                Me.dgProductos.DataSource = Ds.Tables("Consulta")
                ResizeGrillaCodigoProductoPallet()
                Me.dgProductos.Focus()
                Me.txtPallet.ReadOnly = True
                Me.CmdTrans.Enabled = True
            Else : Me.txtPallet.Text = ""
                Me.txtPallet.ReadOnly = False
            End If
        End If

    End Sub
    Private Sub dgProductos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgProductos.Click

        Try
            Me.dgProductos.Select(dgProductos.CurrentRowIndex)
        Catch ex As Exception
        End Try

    End Sub
    Private Sub dgProductos_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgProductos.GotFocus

        Try
            Me.dgProductos.Select(dgProductos.CurrentRowIndex)
        Catch ex As Exception
        End Try

    End Sub
    Private Sub dgProductos_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgProductos.KeyPress

        Try
            Me.dgProductos.Select(dgProductos.CurrentRowIndex)
        Catch ex As Exception
        End Try

    End Sub
    Private Sub dgProductos_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgProductos.KeyUp

        Try
            Me.dgProductos.Select(dgProductos.CurrentRowIndex)
        Catch ex As Exception
        End Try

    End Sub
    Private Sub dgProductos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgProductos.KeyDown

        Try
            Me.dgProductos.Select(dgProductos.CurrentRowIndex)
        Catch ex As Exception
        End Try

    End Sub
    Private Sub CmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSalir.Click

        Salir()

    End Sub
    Private Sub frmTransferenciaAutomatica_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp

        Select Case e.KeyCode
            Case Keys.F1
                If CmdTrans.Enabled Then Procesar()
            Case Keys.F2
                Cancelar()
            Case Keys.F3
                Salir()
        End Select

    End Sub
    Private Sub frmTransferenciaAutomatica_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.txtPallet.Focus()
        Me.dgProductos.Visible = False
        Me.lblMsg.Text = ""
        Me.CmdTrans.Enabled = False

    End Sub
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Cancelar()

    End Sub
    Private Sub CmdTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdTrans.Click

        Procesar()

    End Sub

#End Region


    Private Sub txtPallet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPallet.TextChanged

    End Sub
End Class