Imports System.Data
Imports System.Data.SqlClient

Public Class frmEnvase

#Region "Declaraciones"
    Private Const FrmName As String = "Envases por viaje."
    Private Const StrConnErr As String = "Fallo al intentar conectar con la base de datos."
    Private ClienteId As String = ""
    Private ViajeId As String = ""
    Private DsArticulos As New DataSet
#End Region

    Public Property Cliente() As String

        Get
            Return ClienteId
        End Get
        Set(ByVal value As String)
            ClienteId = value
        End Set

    End Property
    Public Property Viaje_Id() As String

        Get
            Return ViajeId
        End Get
        Set(ByVal value As String)
            ViajeId = value
        End Set

    End Property
    Private Sub frmEnvase_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp

        Select Case e.KeyCode
            Case Keys.F1
                ActivarEntrada()
            Case Keys.F2
                Cancelar()
            Case Keys.F3
                Terminar()
            Case Keys.F4
                ClearAll()
            Case Keys.F5
                Salir()
            Case Else
                Exit Sub
        End Select

    End Sub
    Private Sub frmEnvase_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If GetEnvases() Then 'Levanta los articulos.
            Me.dgArt.DataSource = DsArticulos.Tables("Art")
            FormatGrilla() 'formatea la grilla.
            Me.dgArt.Focus()
            HabilitarEntrada(False)
            Me.cmdCancelar.Enabled = False
        End If

    End Sub
    Private Function GetEnvases() As Boolean
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "Mob_GetProductoEnvase"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Parameters.Add("@Cliente_Id", SqlDbType.VarChar, 15).Value = ClienteId

                Da.Fill(DsArticulos, "Art")
            Else : MsgBox(StrConnErr, MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch ex As Exception
            MsgBox("GetEnvases: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return True
        Finally
            Cmd = Nothing
            Da = Nothing
        End Try

    End Function
    Private Sub FormatGrilla()

        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "Art"
            dgArt.TableStyles.Clear()

            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "PRODUCTO_ID"
            TextCol1.HeaderText = "Prod. Id."
            TextCol1.Width = 70
            Style.GridColumnStyles.Add(TextCol1)

            Dim TextCol2 As New DataGridTextBoxColumn
            TextCol2.MappingName = "DESCRIPCION"
            TextCol2.HeaderText = "Descrip."
            TextCol2.Width = 100
            Style.GridColumnStyles.Add(TextCol2)

            Dim TextCol3 As New DataGridTextBoxColumn
            TextCol3.MappingName = "QTY"
            TextCol3.HeaderText = "Cantidad"
            TextCol3.Width = 60
            Style.GridColumnStyles.Add(TextCol3)

            dgArt.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("FormatGrilla: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Sub
    Private Sub dgArt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgArt.Click

        Try
            dgArt.Select(dgArt.CurrentRowIndex)
        Catch ex As Exception
        End Try

    End Sub
    Private Sub dgArt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgArt.GotFocus

        Try
            dgArt.Select(dgArt.CurrentRowIndex)
        Catch ex As Exception
        End Try

    End Sub
    Private Sub dgArt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgArt.KeyPress

        Try
            dgArt.Select(dgArt.CurrentRowIndex)
        Catch ex As Exception
        End Try

    End Sub
    Private Sub dgArt_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgArt.KeyUp

        Try
            dgArt.Select(dgArt.CurrentRowIndex)
        Catch ex As Exception
        End Try

    End Sub
    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress

        'Llama a una funcion para que no ingresen caracteres no numericos
        ValidarCaracterNumerico(e)

    End Sub
    Private Sub cmdConfirmacion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfirmacion.Click
        Terminar()
    End Sub

    Private Sub Terminar()
        Dim Ce As New clsEnvases
        Dim Cmd As SqlCommand
        Dim Trans As SqlTransaction
        Dim i As Integer
        Dim Flag_Existencia As Boolean = False
        Trans = SQLc.BeginTransaction
        Try
            If Not ExistenElementos(DsArticulos) Then
                Trans.Commit()
                Exit Try
            End If
            If MsgBox("Confirma el egreso de estos productos?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                If VerifyConnection(SQLc) Then
                    Cmd = SQLc.CreateCommand
                    Cmd.Transaction = Trans
                    Ce.Cmd = Cmd
                    Ce.Viaje_Id = ViajeId
                    If Not Ce.CargaCabecera(ClienteId, "E04", ViajeId) Then Throw New Exception(Ce.GetError)

                    For i = 0 To DsArticulos.Tables("Art").Rows.Count - 1
                        If CInt(DsArticulos.Tables("Art").Rows(i)(2)) > 0 Then
                            If Not Mob_Verifica_Existencia(Cmd, _
                                    ClienteId, _
                                    DsArticulos.Tables("Art").Rows(i)(0).ToString, _
                                    DsArticulos.Tables("Art").Rows(i)(2).ToString, _
                                    Ce.DocumentoId.ToString) Then
                                Exit For
                            Else
                                Flag_Existencia = True
                                If Not Ce.CargaDetalle(DsArticulos.Tables("Art").Rows(i)(0).ToString, _
                                                       DsArticulos.Tables("Art").Rows(i)(2).ToString, _
                                                       ClienteId) Then Throw New Exception(Ce.GetError)
                            End If
                            Trans.Commit()
                        End If
                    Next

                    If Not Flag_Existencia Then
                        Trans.Rollback()
                        Exit Try
                    End If
                    If Not Ce.Procesar() Then Throw New Exception(Ce.GetError)
                Else : MsgBox(StrConnErr, MsgBoxStyle.OkOnly, FrmName)
                End If
            Else
                Trans.Commit()
            End If
            Me.Close()
        Catch ex As Exception
            Try 'Dejo este try sin mensaje ya que puede venir por aca y no estar activa la transaccion.
                Trans.Rollback()
            Catch Subex As Exception
            End Try
            MsgBox("Error al Confirmar: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Ce = Nothing
            Cmd = Nothing
            Trans = Nothing
        End Try
    End Sub
    Private Sub ActualizarItem(ByVal RowPos As Integer, ByVal Cantidad As Integer)

        Try
            DsArticulos.Tables("Art").Rows(RowPos)(2) = Cantidad
        Catch ex As Exception
            MsgBox("Fallo al Actualizar Item: " & ex.Message, FrmName)
        End Try

    End Sub
    Private Function ExistenElementos(ByVal ds As DataSet) As Boolean

        Dim i As Integer = 0
        Dim vFlag As Boolean

        vFlag = False
        For i = 0 To ds.Tables("Art").Rows.Count - 1
            If CInt(ds.Tables("Art").Rows(i)(2)) > 0 Then
                vFlag = True
            End If
        Next
        If vFlag = False Then
            MsgBox("No se ha ingresado ninguna cantidad de envases", MsgBoxStyle.Critical, FrmName)
        End If

        Return vFlag

    End Function
    Private Sub txtQty_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyUp
        If e.KeyCode = 13 Then
            If Me.txtQty.Text <> "" And IsNumeric(Me.txtQty.Text) Then
                ActualizarItem(dgArt.CurrentRowIndex, CInt(Me.txtQty.Text))
                Me.dgArt.DataSource = Nothing
                Me.dgArt.DataSource = DsArticulos.Tables("Art")
                FormatGrilla()
                Me.HabilitarEntrada(False)
                Me.cmdIngresar.Enabled = True
                Me.cmdCancelar.Enabled = False
                Me.dgArt.Focus()
            Else
                Me.txtQty.Text = ""
            End If
        End If

    End Sub
    Private Sub HabilitarEntrada(ByVal vBool As Boolean)

        Me.lblQTY.Visible = vBool
        Me.txtQty.Text = ""
        Me.txtQty.Visible = vBool

    End Sub
    Private Sub cmdIngresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIngresar.Click

        ActivarEntrada()

    End Sub
    Private Sub ActivarEntrada()

        If Me.cmdIngresar.Enabled Then
            Me.HabilitarEntrada(True)
            Me.cmdIngresar.Enabled = False
            Me.cmdCancelar.Enabled = True
            Me.txtQty.Focus()
        End If

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        ClearAll()

    End Sub
    Private Sub ClearAll()

        If MsgBox("Esta accion actualizara las cantidades a 0. Desea continuar?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
            Dim i As Integer
            For i = 0 To DsArticulos.Tables("Art").Rows.Count - 1
                DsArticulos.Tables("Art").Rows(i)(2) = 0
            Next
        End If
        Me.dgArt.Focus()
        HabilitarEntrada(False)
        Me.cmdIngresar.Enabled = True
        Me.cmdCancelar.Enabled = False

    End Sub
    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click

        Cancelar()

    End Sub

    Private Sub Cancelar()
        If Me.cmdCancelar.Enabled Then
            Me.HabilitarEntrada(False)
            Me.cmdIngresar.Enabled = True
            Me.cmdCancelar.Enabled = False
            Me.dgArt.Focus()
        End If
    End Sub

    Private Sub CmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSalir.Click
        Salir()
    End Sub

    Private Sub Salir()
        If MsgBox("¿Desea Salir?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Function Mob_Verifica_Existencia(ByRef xCmd As SqlCommand, ByVal Cliente_id As String, ByVal Producto_id As String, ByVal Solicitada As Long, ByVal Documento_id As String) As Boolean
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Dim vControl As Integer
        Dim xErr As String

        Try
            xCmd.Parameters.Clear()
            xCmd.Connection = SQLc
            xCmd.CommandType = Data.CommandType.StoredProcedure
            Da = New SqlDataAdapter(xCmd)
            xCmd.CommandText = "Mob_Verifica_Existencia"

            Pa = New SqlParameter("@Cliente_Id", Data.SqlDbType.VarChar, 15)
            Pa.Value = ClienteId
            xCmd.Parameters.Add(Pa)

            Pa = Nothing
            Pa = New SqlParameter("@Producto_id", Data.SqlDbType.VarChar, 30)
            Pa.Value = Producto_id
            xCmd.Parameters.Add(Pa)

            Pa = Nothing
            Pa = New SqlParameter("@Solicitada", Data.SqlDbType.Float)
            Pa.Value = Solicitada
            xCmd.Parameters.Add(Pa)

            Pa = Nothing
            Pa = New SqlParameter("@Documento_Id", Data.SqlDbType.BigInt)
            Pa.Value = Documento_id
            xCmd.Parameters.Add(Pa)

            Pa = Nothing
            Pa = New SqlParameter("@Control", Data.SqlDbType.Char, 1)
            Pa.Direction = ParameterDirection.Output
            xCmd.Parameters.Add(Pa)

            xCmd.ExecuteNonQuery()

            vControl = xCmd.Parameters("@Control").Value
            If vControl = 1 Then
                Return True
            Else
                Return False
            End If

        Catch SQLEx As SqlException
            xErr = "Mob_Verifica_Existencia SQL: " & SQLEx.Message
            MsgBox(xErr, MsgBoxStyle.Exclamation, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("Mob_Verifica_Existencia: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Da = Nothing
            Pa = Nothing
        End Try

    End Function

    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged

    End Sub
End Class