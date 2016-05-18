Imports System.Data.SqlClient
Imports System.Data

Public Class frmTransfSobrantesPick
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private Const FrmName As String = "Transferencias Sobrantes de Picking"
    Private blnTransCurso As Boolean = False
    Private EsContenedora As Boolean
    Private Pallet As String = ""
    Private TSeleccion As Integer = 0

    Public Property Seleccion() As Integer
        Get
            Return TSeleccion
        End Get
        Set(ByVal value As Integer)
            TSeleccion = value
        End Set
    End Property

    Private Sub frmTransfSobrantesPick_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Try
            Select Case e.KeyCode
                Case Keys.F1
                    StartTransF()
                Case Keys.F2
                    CancelTransf()
                Case Keys.F3
                    NuevaPosicion()
                Case Keys.F4
                    ExitTransf()
            End Select
        Catch ex As Exception
            MsgBox("frmTransfSobrantesPick_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub ExitTransf()
        Dim Rta As Object
        If Me.blnTransCurso = True Then
            Rta = MsgBox("Desea Cancelar la Transf. en curso y salir?", MsgBoxStyle.YesNo, FrmName)
        Else
            Rta = MsgBox("Desea salir de Transferencias?", MsgBoxStyle.YesNo, FrmName)
        End If
        If Rta = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Sub StartTransF()
        Me.blnTransCurso = True
        Me.lblUbicacionOri.Visible = True
        Me.txtUbiacionOri.Visible = True
        Me.txtUbiacionOri.Text = ""
        Me.txtUbiacionOri.Focus()
        Me.cmdStartTransf.Enabled = False
        Me.cmdCancelar.Enabled = True
        Me.cmdNuevaPos.Enabled = False
    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        CancelTransf()
    End Sub

    Private Sub CancelTransf()
        InicializarFrm()
        Me.blnTransCurso = False
    End Sub

    Private Sub InicializarFrm()
        Try
            Me.Pallet = ""
            Me.cmdStartTransf.Enabled = True
            Me.cmdCancelar.Enabled = False
            Me.cmdNuevaPos.Enabled = False
            Me.blnTransCurso = False
            Me.lblUbicacionOri.Visible = False
            Me.txtUbiacionOri.Visible = False
            Me.txtUbiacionOri.Text = ""
            Me.txtUbiacionOri.ReadOnly = False
            Me.lblContendora.Visible = False
            Me.txtContenedora.Visible = False
            Me.txtContenedora.ReadOnly = False
            Me.txtContenedora.Text = ""
            Me.lblUbicacionDest.Visible = False
            Me.LblUbicacionSug.Visible = False
            Me.LblUbicacionSug.Text = ""
            Me.txtUbicacionDest.Visible = False
            Me.txtUbicacionDest.Text = ""
            Me.txtUbicacionDest.ReadOnly = False

            If Me.TSeleccion = 1 Then
                Me.lblTipo.Text = "Transf. por contenedoras..."
                Me.lblContendora.Text = "Nro. Contenedora: "
            ElseIf TSeleccion = 2 Then
                Me.lblTipo.Text = "Transf. por pallets..."
                Me.lblContendora.Text = "Nro. Pallet: "
            End If

        Catch ex As Exception
            MsgBox("InicializarFrm: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub cmdStartTransf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartTransf.Click
        StartTransF()
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        ExitTransf()
    End Sub

    Private Sub frmTransfSobrantesPick_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InicializarFrm()
    End Sub

    Private Sub txtUbiacionOri_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUbiacionOri.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtUbiacionOri.Text <> "" Then
                If ExisteNavePosicion(Me.txtUbiacionOri.Text) Then
                    If Not VerificaNavePRE(Me.txtUbiacionOri.Text) Then
                        MsgBox("La Nave/Posicion se encuentra inhabilitada para operaciones de transferencias", MsgBoxStyle.Information, FrmName)
                        Me.txtUbiacionOri.Text = ""
                        Exit Sub
                    End If
                    If VerificaPosLockeada(Me.txtUbiacionOri.Text) Then
                        Me.txtUbiacionOri.Text = UCase(Me.txtUbiacionOri.Text)
                        Me.txtUbiacionOri.ReadOnly = True
                        Me.lblContendora.Visible = True
                        Me.txtContenedora.Visible = True
                        Me.txtContenedora.Focus()
                    Else
                        Me.txtUbiacionOri.Text = ""
                        Me.txtUbiacionOri.Focus()
                    End If
                Else
                    Me.txtUbiacionOri.Text = ""
                    Me.txtUbiacionOri.Focus()
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Function ExisteNavePosicion(ByVal xUbicacion As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim xValue As String
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "ExisteNavePosicion"
                Cmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Ubicacion", SqlDbType.VarChar, 45)
                Pa.Value = xUbicacion
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Retorno", SqlDbType.Char, 1, ParameterDirection.Output)
                Pa.Value = DBNull.Value
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
                xValue = IIf(IsDBNull(Cmd.Parameters("@Retorno").Value), "", Cmd.Parameters("@Retorno").Value)
            Else
                MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("ExisteNavePosicion SQL: " & SQLEx.Message)
            Return False
        Catch ex As Exception
            MsgBox("ExisteNavePosicion: " & ex.Message)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub txtContenedora_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtContenedora.KeyUp
        Try
            Select Case e.KeyCode
                Case 13
                    If Me.txtContenedora.Text.Trim <> "" Then
                        If Me.TSeleccion = 2 Then
                            Pallet = Me.txtContenedora.Text
                        End If
                        If Verifica_Contenedora_Pos(Me.txtUbiacionOri.Text, Me.txtContenedora.Text, Pallet) Then
                            If Not GetUbicacionPrevia(Me.txtUbiacionOri.Text, Me.txtContenedora.Text) Then
                                Exit Sub
                            End If
                            Me.txtContenedora.Text = UCase(Me.txtContenedora.Text)
                            Me.txtContenedora.ReadOnly = True
                            Me.lblUbicacionDest.Visible = True
                            Me.txtUbicacionDest.Visible = True
                            Me.cmdNuevaPos.Enabled = True
                            Me.txtUbicacionDest.Focus()
                        Else
                            Me.txtContenedora.Text = ""
                            Me.txtContenedora.Focus()
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox("txtContenedora_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub
    Private Function Verifica_Contenedora_Pos(ByVal Origen As String, ByVal Contenedora As String, ByRef xPallet As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim ValErr As String = ""
        Try
            If VerifyConnection(SQLc) Then
                'VERIFICA_CONTENEDORA_POS_TR
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "VERIFICA_CONTENEDORA_POS_TR"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc

                Pa = New SqlParameter("@POSICION_O", SqlDbType.VarChar, 45)
                Pa.Value = Origen
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CONTENEDORA", SqlDbType.VarChar, 100)
                If Me.TSeleccion = 1 Then
                    Pa.Value = Contenedora
                Else
                    Pa.Value = DBNull.Value
                End If
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PALLET", SqlDbType.VarChar, 100)
                If Me.TSeleccion = 2 Then
                    Pa.Value = xPallet
                Else
                    Pa.Value = DBNull.Value
                End If
                Pa.Direction = ParameterDirection.InputOutput
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                xPallet = IIf(IsDBNull(Cmd.Parameters("@PALLET").Value), "", Cmd.Parameters("@PALLET").Value)
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("Transferir SQL. " & SQLEx.Message, MsgBoxStyle.Critical, FrmName)
            ValErr = Mid(SQLEx.Message.ToString, 1, 1)
            If ValErr = "1" Then
                Me.InicializarFrm()
                Me.cmdStartTransf.Enabled = False
                Me.lblUbicacionOri.Visible = True
                Me.txtUbiacionOri.Visible = True
                Me.txtUbiacionOri.Focus()
            End If
            Return False
        Catch ex As Exception
            MsgBox("Transferir SQL. " & ex.Message)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function
    Private Function GetUbicacionPrevia(ByVal UbicacionOrigen As String, ByVal Contenedora As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "GET_UBICACION_ANTERIOR"
                xCmd.CommandType = CommandType.StoredProcedure
                Da = New SqlDataAdapter(xCmd)

                Pa = New SqlParameter("@POSICION_O", SqlDbType.VarChar, 45)
                Pa.Value = UbicacionOrigen
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CONTENEDORA", SqlDbType.VarChar, 100)
                Pa.Value = Contenedora
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@TIPO", SqlDbType.Int)
                Pa.Value = Me.TSeleccion
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_D", SqlDbType.VarChar, 45)
                Pa.Value = DBNull.Value
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()
                Me.LblUbicacionSug.Visible = True
                Me.LblUbicacionSug.Text = xCmd.Parameters("@POSICION_D").Value.ToString
            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("GetUbicacionLocator SQL. " & SQLEx.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox("GetUbicacionLocator. " & ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
            Da = Nothing
        End Try
    End Function
    Private Sub NuevaPosicion()
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand

                If Me.TSeleccion = 1 Then
                    'xCmd.CommandText = "Locator_Transf_Contenedora"
                    xCmd.CommandText = "[dbo].[MOB_REUBICACION_CONTENEDORA]"
                Else
                    xCmd.CommandText = "[DBO].[MOB_REUBICACION_PALLET]"
                End If
                xCmd.CommandType = CommandType.StoredProcedure
                Da = New SqlDataAdapter(xCmd)

                Pa = Nothing
                If Me.TSeleccion = 1 Then
                    Pa = New SqlParameter("@CONTENEDORA", SqlDbType.VarChar, 100)
                Else
                    Pa = New SqlParameter("@NROPALLET", SqlDbType.VarChar, 100)
                End If
                Pa.Value = Me.txtContenedora.Text
                xCmd.Parameters.Add(Pa)

                Pa = Nothing
                Pa = New SqlParameter("@POS_COD", SqlDbType.VarChar, 45)
                Pa.Value = DBNull.Value
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@POS_OR", SqlDbType.VarChar, 45)
                Pa.Value = Me.txtUbiacionOri.Text
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                Me.LblUbicacionSug.Visible = True
                Me.LblUbicacionSug.Text = xCmd.Parameters("@POS_COD").Value.ToString
                Me.cmdNuevaPos.Enabled = False
            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Exit Sub
            End If
            Exit Sub
        Catch SQLEx As SqlException
            MsgBox("GetUbicacionLocator SQL. " & SQLEx.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox("GetUbicacionLocator. " & ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
            Da = Nothing
        End Try
    End Sub
    Private Function VerificaPosLockeada(ByVal Pos_O As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Value As String
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "VERIFICA_LOCKEO_POS"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc

                Pa = New SqlParameter("@Posicion_O", SqlDbType.VarChar, 45)
                Pa.Value = Pos_O
                Pa.Direction = ParameterDirection.Input
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@out", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

                Value = IIf(IsDBNull(Cmd.Parameters("@out").Value), "0", Cmd.Parameters("@out").Value)
                If Value = "1" Then
                    Return False
                ElseIf Value = "0" Then
                    Return True
                End If
            Else
                MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
        Catch SQLEx As SqlException
            MsgBox("VerificaPosLockeada SQL. " & SQLEx.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("VerificaPosLockeada. " & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub txtUbicacionDest_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUbicacionDest.KeyUp
        Try
            Select Case e.KeyCode
                Case 13
                    If Me.txtUbicacionDest.Text.Trim <> "" Then
                        If UCase(Me.txtUbicacionDest.Text.Trim) = UCase(Me.LblUbicacionSug.Text.Trim) Then
                            Dim Rta As Object = MsgBox("¿Confirma la Transferencia?", MsgBoxStyle.YesNo, FrmName)
                            If Rta = vbYes Then
                                If Transferir() Then
                                    MsgBox("La transferencia se realizo correctamente ", MsgBoxStyle.OkOnly, FrmName)
                                End If
                            End If
                            Me.InicializarFrm()
                        Else
                            If MsgBox("Selecciono una ubicación diferente, ¿desea continuar?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.No Then
                                Me.txtUbicacionDest.Text = ""
                                Me.txtUbicacionDest.Focus()
                            Else
                                If TransferirForzado() Then
                                    MsgBox("La transferencia se realizo correctamente ", MsgBoxStyle.OkOnly, FrmName)
                                End If
                                Me.InicializarFrm()
                            End If
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox("txtUbicacionDest_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Function Transferir() As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim item As Object
        Dim trans As SqlClient.SqlTransaction
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "SOBRANTES_PICKING"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@POSICION_O", SqlDbType.VarChar, 100)
                Pa.Value = Me.txtUbiacionOri.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_D", SqlDbType.VarChar, 100)
                Pa.Value = Me.txtUbicacionDest.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CONTENEDORA", SqlDbType.VarChar, 100)

                If (Trim(Pallet) <> Trim(Me.txtContenedora.Text)) And (Me.TSeleccion = 1) Then
                    Pa.Value = Trim(UCase(Me.txtContenedora.Text))
                Else
                    Pa.Value = DBNull.Value
                End If
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PALLET", SqlDbType.VarChar, 100)
                If Me.TSeleccion = 2 Then
                    Pa.Value = IIf(Trim(UCase(Me.txtContenedora.Text)) = "", DBNull.Value, Me.txtContenedora.Text)
                Else
                    Pa.Value = DBNull.Value
                End If
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                Pa.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@TIPO", SqlDbType.Int)
                Pa.Value = Me.TSeleccion
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If

        Catch SQLEx As SqlException
            MsgBox("REPALETIZAR SQL. " & SQLEx.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("REPALETIZAR. " & ex.Message, MsgBoxStyle.Critical, FrmName)
            trans.Rollback()
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function TransferirForzado() As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim item As Object
        Dim trans As SqlClient.SqlTransaction
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "SOBRANTES_PICKING_FORZADO"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@POSICION_O", SqlDbType.VarChar, 100)
                Pa.Value = Me.txtUbiacionOri.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_D", SqlDbType.VarChar, 100)
                Pa.Value = Me.txtUbicacionDest.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CONTENEDORA", SqlDbType.VarChar, 100)
                If Trim(Pallet) <> Trim(Me.txtContenedora.Text) Then
                    Pa.Value = Trim(UCase(Me.txtContenedora.Text))
                Else
                    Pa.Value = DBNull.Value
                End If
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PALLET", SqlDbType.VarChar, 100)
                Pa.Value = IIf(Trim(UCase(Pallet)) = "", DBNull.Value, Pallet)
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                Pa.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@TIPO", SqlDbType.VarChar, 100)
                Pa.Value = Me.TSeleccion
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If

        Catch SQLEx As SqlException
            MsgBox("TRANSFERENCIA FORZADA SQL. " & SQLEx.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("TRANSFERENCIA FORZADA. " & ex.Message, MsgBoxStyle.Critical, FrmName)
            trans.Rollback()
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub cmdNuevaPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNuevaPos.Click
        NuevaPosicion()
    End Sub
    Public Function VerificaNavePRE(ByVal Posicion As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Value As String
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Mob_Verifica_Nave_Pre"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc

                Pa = New SqlParameter("@Pos_Nave_cod", SqlDbType.VarChar, 40)
                Pa.Value = Posicion
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Flag", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

                Value = IIf(IsDBNull(Cmd.Parameters("@Flag").Value), "0", Cmd.Parameters("@Flag").Value)
                If Value = "1" Then
                    Return True
                ElseIf Value = "0" Then
                    Return False
                End If
            Else
                MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If

        Catch SQLEx As SqlException
            MsgBox("Mob_Verifica_Nave_Pre SQL. " & SQLEx.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("Mob_Verifica_Nave_Pre. " & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub txtContenedora_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtContenedora.LostFocus

    End Sub

    Private Sub txtContenedora_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContenedora.TextChanged

    End Sub

    Private Sub txtUbicacionDest_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUbicacionDest.TextChanged

    End Sub

    Private Sub LblUbicacionSug_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblUbicacionSug.ParentChanged

    End Sub
End Class