Imports System.Data.SqlClient
Imports System.Data


Public Class frmControlPicking
    Private Const StrConnErr As String = "Fallo al intentar conectar con la base de datos."
    Private Const FrmName As String = "Control Picking."
    Private CantBultos As Integer
    Private Ds As Data.DataSet
    Private PalletControlado As String
    Private DsForm As New Data.DataSet

    Private Sub frmControlPicking_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Buscar()
            Case Keys.F2
                Cancelar()
            Case Keys.F3
                Salir()
        End Select
    End Sub

    Private Sub Salir()
        Me.Close()
    End Sub

    Private Sub frmControlPicking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IniFrm()
    End Sub

    Private Sub IniFrm()
        Me.lblMsg.Text = ""
        Me.txtPalletPicking.Text = ""
        Me.dgPicking.Visible = False
    End Sub

    Private Function GetDataPallet(ByVal PalletPicking As Long, ByRef DsRef As DataSet) As Boolean
        Dim cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                cmd = SQLc.CreateCommand
                da = New SqlDataAdapter(cmd)
                cmd.CommandText = "CONTROL_PICKING_PALLET"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = SQLc
                cmd.Parameters.Clear()

                Pa = New SqlParameter("@PALLET_PIC", SqlDbType.BigInt)
                Pa.Value = CLng(PalletPicking)
                cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 30)
                Pa.Value = vUsr.CodUsuario
                cmd.Parameters.Add(Pa)
                da.Fill(DsRef, "CONTROL_PICKING_PALLET")
            Else
                lblMsg.Text = StrConnErr
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            lblMsg.Text = "GetDataPallet SQL." & SQLEx.Message
            Return False
        Catch ex As Exception
            lblMsg.Text = "GetDataPallet." & ex.Message
            Return False
        Finally
            cmd = Nothing
            Pa = Nothing
            da = Nothing
        End Try
    End Function

    Private Sub txtPalletPicking_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPalletPicking.KeyPress
        Try
            ValidarCaracterNumerico(e)
        Catch ex As Exception
            lblMsg.Text = "Número de Pallet Incorrecto"
        End Try
    End Sub

    Private Sub txtPalletPicking_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPalletPicking.KeyUp
        Try
            If e.KeyValue = 13 Then
                Me.lblMsg.Text = ""
                If Me.txtPalletPicking.Text <> "" Then
                    'BuscarPallet(Me.txtPalletPicking.Text)
                    GetCantBultos(Me.txtPalletPicking.Text)
                    If CantBultos <> 0 Then
                        If PalletControlado = "1" Then
                            BuscarPallet(Me.txtPalletPicking.Text)
                        Else
                            Me.LblCantBultos.Visible = True
                            Me.txtCantBultos.Visible = True
                            Me.txtCantBultos.Focus()
                        End If
                    Else
                        Me.lblMsg.Text = "No se encontro el Pallet de Picking " & Me.txtPalletPicking.Text
                        Me.txtPalletPicking.Text = ""
                        Me.txtPalletPicking.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            lblMsg.Text = "GetDataPallet." & ex.Message
            Exit Sub
        End Try
    End Sub

    Private Sub BuscarPallet(ByVal Pallet As String)
        Try
            If DsForm.Tables("CONTROL_PICKING_PALLET") IsNot Nothing Then
                DsForm.Tables.Remove("CONTROL_PICKING_PALLET")
            End If
            If DsForm.Tables("CONTROL_PICKING_PALLET1") IsNot Nothing Then
                DsForm.Tables.Remove("CONTROL_PICKING_PALLET1")
            End If
            If GetDataPallet(CLng(Pallet), DsForm) Then
                If DsForm.Tables("CONTROL_PICKING_PALLET").Rows.Count = 0 Then
                    Me.txtPalletPicking.SelectAll()
                    Me.lblMsg.Text = "No se encontro el Pallet de Picking " & Me.txtPalletPicking.Text
                    Exit Sub
                Else
                    Me.dgPicking.DataSource = Nothing
                    Me.dgPicking.Visible = True
                    Me.dgPicking.DataSource = DsForm.Tables("CONTROL_PICKING_PALLET")
                    ResizeGrid()
                    Me.dgPicking.Focus()

                End If
            End If
        Catch ex As Exception
            lblMsg.Text = "GetDataPallet." & ex.Message
            Exit Sub
        End Try
    End Sub

    Private Sub ResizeGrid()
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "CONTROL_PICKING_PALLET"
            Me.dgPicking.TableStyles.Clear()
            Style.MappingName = "CONTROL_PICKING_PALLET"

            Dim TextCol2 As New DataGridTextBoxColumn
            With TextCol2
                .MappingName = "PRODUCTO_ID"
                .HeaderText = "Prod. ID"
                .Width = 50
            End With
            Style.GridColumnStyles.Add(TextCol2)

            Dim TextCol3 As New DataGridTextBoxColumn
            With TextCol3
                .MappingName = "DESCRIPCION"
                .HeaderText = "Descripcion"
                .Width = 140
            End With
            Style.GridColumnStyles.Add(TextCol3)

            Dim TextCol4 As New DataGridTextBoxColumn
            With TextCol4
                .MappingName = "CANTIDAD"
                .HeaderText = "CANTIDAD"
                .Width = 50
            End With
            Style.GridColumnStyles.Add(TextCol4)

            Me.dgPicking.TableStyles.Add(Style)
        Catch ex As Exception
            Me.lblMsg.Text = "ResizeGrid: " & ex.Message
        End Try
    End Sub

    Private Sub Cancelar()
        If DsForm.Tables("CONTROL_PICKING_PALLET") IsNot Nothing Then
            DsForm.Tables.Remove("CONTROL_PICKING_PALLET")
        End If
        If DsForm.Tables("CONTROL_PICKING_PALLET1") IsNot Nothing Then
            DsForm.Tables.Remove("CONTROL_PICKING_PALLET1")
        End If
        Me.LblCantBultos.Visible = False
        Me.txtCantBultos.Visible = False
        Me.txtCantBultos.Text = ""
        Me.txtPalletPicking.Text = ""
        Me.dgPicking.DataSource = Nothing
        Me.dgPicking.Visible = False
        Me.lblMsg.Text = ""
        Me.txtPalletPicking.Focus()
    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Cancelar()
    End Sub

    Private Sub dgPicking_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgPicking.GotFocus
        Try
            Me.dgPicking.Select(Me.dgPicking.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgPicking_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgPicking.KeyUp
        Try
            Me.dgPicking.Select(Me.dgPicking.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ValidarCaracterNumerico(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        Try
            'Valida que el caracter ingreado sea un nro
            If (Asc(e.KeyChar) >= 32 And Asc(e.KeyChar) <= 47) Or Asc(e.KeyChar) >= 58 Then
                e.Handled = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Salir()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Buscar()
    End Sub
    Private Sub Buscar()
        Me.lblMsg.Text = ""
        If Me.txtPalletPicking.Text <> "" Then
            BuscarPallet(Me.txtPalletPicking.Text)
        End If
    End Sub


    Private Function SetStatusPick(ByVal Usuario As String, ByVal PalletPick As String, ByVal Status As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "CONTROL_PICKING_STATUS"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc

                Pa = New SqlParameter("@PALLET", SqlDbType.BigInt)
                Pa.Value = PalletPick
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 30)
                Pa.Value = Usuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@STATUS", SqlDbType.Char, 1)
                Pa.Value = Status
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

            Else
                lblMsg.Text = StrConnErr
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            Me.lblMsg.Text = "SetStatusPick SQL.: " & SQLEx.Message
            Return False
        Catch ex As Exception
            Me.lblMsg.Text = "SetStatusPick : " & ex.Message
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub txtCantBultos_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantBultos.KeyUp
        Try
            If e.KeyValue = 13 Then
                Me.lblMsg.Text = ""
                If Me.txtCantBultos.Text <> "" Then
                    If Val(Me.txtCantBultos.Text) > 0 Then
                        If Val(Trim(Me.txtCantBultos.Text)) = CantBultos Then
                            BuscarPallet(Me.txtPalletPicking.Text)
                        Else
                            Me.lblMsg.Text = "La cantidad de bultos ingresada es incorrecta"
                            If DsForm.Tables("CONTROL_PICKING_PALLET") IsNot Nothing Then
                                DsForm.Tables.Remove("CONTROL_PICKING_PALLET")
                            End If
                            If DsForm.Tables("CONTROL_PICKING_PALLET1") IsNot Nothing Then
                                DsForm.Tables.Remove("CONTROL_PICKING_PALLET1")
                            End If
                            Me.txtCantBultos.Text = ""
                            Me.txtCantBultos.Focus()
                        End If
                    Else
                        lblMsg.Text = "La cantidad debe ser mayor a cero."
                        Me.txtCantBultos.Text = ""
                        Me.txtCantBultos.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            lblMsg.Text = "GetDataPallet." & ex.Message
            Exit Sub
        End Try
    End Sub

    Private Sub txtCantBultos_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantBultos.KeyPress
        ValidarCaracterNumerico(e)
    End Sub
    Private Function GetCantBultos(ByVal PalletPicking As Long) As Boolean
        Dim cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                cmd = SQLc.CreateCommand
                da = New SqlDataAdapter(cmd)
                cmd.CommandText = "CONTROL_PICKING_CANT_BULTOS"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = SQLc
                cmd.Parameters.Clear()

                Pa = New SqlParameter("@PALLET_PIC", SqlDbType.BigInt)
                Pa.Value = CLng(PalletPicking)
                cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CANTIDAD", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PALLET_CONTROLADO", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                cmd.Parameters.Add(Pa)
                Pa = Nothing

                cmd.ExecuteNonQuery()

                Me.CantBultos = IIf(IsDBNull(cmd.Parameters("@CANTIDAD").Value), "0", cmd.Parameters("@CANTIDAD").Value)
                Me.PalletControlado = IIf(IsDBNull(cmd.Parameters("@PALLET_CONTROLADO").Value), "0", cmd.Parameters("@PALLET_CONTROLADO").Value)
            Else
                lblMsg.Text = StrConnErr
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            lblMsg.Text = "GetDataPallet SQL." & SQLEx.Message
            Return False
        Catch ex As Exception
            lblMsg.Text = "GetDataPallet." & ex.Message
            Return False
        Finally
            cmd = Nothing
            Pa = Nothing
            da = Nothing
        End Try
    End Function

    Private Sub txtPalletPicking_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPalletPicking.TextChanged

    End Sub
End Class