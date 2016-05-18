Imports System.Data.SqlClient
Imports System.Data
Public Class frmRepaletizado

    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private Const FrmName As String = "Repaletizado"
    Dim PalletDestino As String = ""
    Private Ubicacion As String

    Private Sub frmTransferenciaAutomatica_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                If Me.CmdConfirmar.Enabled Then Confirmar()
            Case Keys.F2
                Cancelar()
            Case Keys.F3
                Salir()
        End Select
    End Sub

    Private Sub Cancelar()
        Dim Rta As Object
        Rta = MsgBox("Desea cancelar la operacion en curso?", MsgBoxStyle.YesNo, FrmName)
        If Rta = vbYes Then
            InicializarFrm()
        End If
    End Sub

    Private Sub Confirmar()
        Try
            Dim Rta As Object
            Rta = MsgBox("Finalizo la carga de contenedoras?", MsgBoxStyle.YesNo, FrmName)
            If Rta = vbNo Then
                Exit Sub
            End If
            Me.TxtContenedora.ReadOnly = True
            Me.CmdConfirmar.Enabled = False
            Me.LstContenedoras.Enabled = False
            Rta = MsgBox("Desea utilizar un pallet existente?", MsgBoxStyle.YesNo, FrmName)
            If Rta = vbYes Then
                Me.LblPalletDestino.Visible = True
                Me.TxtPalletDest.Visible = True
                Me.TxtPalletDest.Focus()
            Else
                'Aca tengo que realizar la generacion del nuevo pallet.
                GetNuevoPallet(PalletDestino)
                If Not GetUbicacionLocator(Me.TxtUbicacionOri.Text, Me.TxtPalletOri.Text) Then
                    Exit Sub
                End If
                Me.LblUbicacionDest.Visible = True
                Me.TxtUbicacionDest.Visible = True
                Me.TxtUbicacionDest.Focus()
            End If
        Catch ex As Exception
            MsgBox("Confirmar: " & ex.Message)
        End Try
    End Sub

    Private Sub Salir()
        Dim Rta As Object
        If Me.TxtPalletOri.Visible = True Then
            Rta = MsgBox("Desea Cancelar la operacion en curso y salir?", MsgBoxStyle.YesNo, FrmName)
        Else
            Rta = MsgBox("Desea salir de Repaletizado?", MsgBoxStyle.YesNo, FrmName)
        End If
        If Rta = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Sub CmdConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdConfirmar.Click
        Confirmar()
    End Sub

    Private Sub CmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCancelar.Click
        Cancelar()
    End Sub

    Private Sub CmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSalir.Click
        Salir()
    End Sub

    Private Sub TxtUbicacionOri_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtUbicacionOri.KeyUp
        If e.KeyValue = 13 Then
            If Me.TxtUbicacionOri.Text <> "" Then
                If ExisteNavePosicion(Me.TxtUbicacionOri.Text) Then
                    If Not VerificaNavePRE(Me.TxtUbicacionOri.Text) Then
                        MsgBox("La Nave/Posicion se encuentra inhabilitada para operaciones de transferencias", MsgBoxStyle.Information, FrmName)
                        Me.TxtUbicacionOri.Text = ""
                        Me.TxtUbicacionOri.Focus()
                        Exit Sub
                    End If
                Else
                    Me.TxtUbicacionOri.Text = ""
                    Me.TxtUbicacionOri.Focus()
                    Exit Sub
                End If
                If VerificaPosLockeada(Me.TxtUbicacionOri.Text) Then
                    Me.TxtUbicacionOri.Text = UCase(Me.TxtUbicacionOri.Text)
                    Me.TxtUbicacionOri.ReadOnly = True
                    Me.CmdCancelar.Enabled = True
                    Me.TxtUbicacionOri.Visible = True
                    Me.LblPalletOrigen.Visible = True
                    Me.TxtPalletOri.Visible = True
                    Me.TxtPalletOri.Focus()
                Else
                    Me.TxtUbicacionOri.Text = ""
                    Me.TxtUbicacionOri.Focus()
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

    Private Sub TxtPalletOri_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPalletOri.KeyUp
        Try
            Select Case e.KeyCode
                Case 13
                    If TxtPalletOri.Text.Trim <> "" Then
                        If Verifica_Pallet_Pos(Me.TxtUbicacionOri.Text, Me.TxtPalletOri.Text) Then
                            Me.TxtPalletOri.ReadOnly = True
                            Me.LblContenedora.Visible = True
                            Me.TxtContenedora.Visible = True
                            Me.LstContenedoras.Visible = True
                            Me.TxtContenedora.Focus()
                        Else
                            Me.TxtPalletOri.Text = ""
                            Me.TxtPalletOri.Focus()
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox("txtPalletOri_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub
    Private Function Verifica_Pallet_Pos(ByVal Pos_O As String, ByVal Pallet As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim ValErr As String = ""
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "VERIFICA_PALLET_POS_TR"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc

                Pa = New SqlParameter("@Posicion_O", SqlDbType.VarChar, 45)
                Pa.Value = Pos_O
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Pallet", SqlDbType.VarChar, 100)
                Pa.Value = Pallet

                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("Transferir SQL. " & SQLEx.Message)
            ValErr = Mid(SQLEx.Message.ToString, 1, 1)
            If ValErr = "1" Then
                Me.InicializarFrm()
                Me.TxtUbicacionOri.Focus()
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
    Private Sub InicializarFrm()
        Me.TxtUbicacionOri.ReadOnly = False
        Me.TxtUbicacionOri.Text = ""
        Me.TxtUbicacionOri.Focus()
        Me.LblPalletOrigen.Visible = False
        Me.TxtPalletOri.ReadOnly = False
        Me.TxtPalletOri.Visible = False
        Me.TxtPalletOri.Text = ""
        Me.LblContenedora.Visible = False
        Me.TxtContenedora.Text = ""
        Me.TxtContenedora.ReadOnly = False
        Me.TxtContenedora.Visible = False
        Me.LstContenedoras.Items.Clear()
        Me.LstContenedoras.Enabled = True
        Me.LstContenedoras.Visible = False
        Me.LblPalletDestino.Visible = False
        Me.TxtPalletDest.ReadOnly = False
        Me.TxtPalletDest.Text = ""
        Me.TxtPalletDest.Visible = False
        Me.LblUbicacionDest.Visible = False
        Me.LblUbicacionDest.Text = ""
        Me.TxtUbicacionDest.ReadOnly = False
        Me.TxtUbicacionDest.Visible = False
        Me.TxtUbicacionDest.Text = ""
        Me.CmdConfirmar.Enabled = False
        Me.CmdCancelar.Enabled = False
    End Sub

    Private Sub TxtContenedora_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtContenedora.KeyUp
        Try
            Select Case e.KeyCode
                Case 13
                    If TxtContenedora.Text.Trim <> "" Then
                        If Verifica_Contenedora_Pallet(Me.TxtPalletOri.Text, Me.TxtContenedora.Text) Then
                            Dim Contenedora As Object
                            Contenedora = Me.TxtContenedora.Text
                            If Existe(Me.TxtContenedora.Text) Then
                                Dim Rta As Object
                                Rta = MsgBox("Desea borrar la conedora :" & Me.TxtContenedora.Text & " de la lista?", MsgBoxStyle.YesNo, FrmName)
                                If Rta = vbYes Then
                                    Me.LstContenedoras.Items.Remove(Contenedora)
                                End If
                            Else
                                Me.LstContenedoras.Items.Add(Contenedora)
                            End If
                        End If
                    End If
                    Me.TxtContenedora.Text = ""
                    Me.TxtContenedora.Focus()
                    If Me.LstContenedoras.Items.Count > 0 Then
                        Me.CmdConfirmar.Enabled = True
                    Else
                        Me.CmdConfirmar.Enabled = False
                    End If
            End Select
        Catch ex As Exception
            MsgBox("txtContenedora_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub
    Private Function Verifica_Contenedora_Pallet(ByVal Pallet As String, ByVal Contenedora As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim ValErr As String = ""
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "VERIFICA_CONTENEDORA_PALLET"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc

                Pa = New SqlParameter("@Pallet", SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Contenedora", SqlDbType.VarChar, 100)
                Pa.Value = Contenedora
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("Transferir SQL. " & SQLEx.Message)
            ValErr = Mid(SQLEx.Message.ToString, 1, 1)
            If ValErr = "1" Then
                Me.TxtContenedora.Text = ""
                Me.TxtContenedora.Focus()
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
    Private Function GetUbicacionLocator(ByVal UbicacionOrigen As String, ByVal Pallet As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "Locator_Transf"
                xCmd.CommandType = CommandType.StoredProcedure
                Da = New SqlDataAdapter(xCmd)

                Pa = New SqlParameter("@UbicacionOrigen", SqlDbType.VarChar, 50)
                Pa.Value = UbicacionOrigen
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@NroPallet", SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                xCmd.Parameters.Add(Pa)

                Da.Fill(Ds, "Ubicacion")
                If Ds.Tables("Ubicacion").Rows.Count > 0 Then
                    Ubicacion = Ds.Tables("Ubicacion").Rows(0)(1).ToString
                    Me.LblUbicacionDest.Text = "Ubicacion Destino: " + Ds.Tables("Ubicacion").Rows(0)(1).ToString
                End If
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
            Ds = Nothing
            Da = Nothing
        End Try
    End Function

    Private Sub TxtPalletDest_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPalletDest.KeyUp
        Try
            Select Case e.KeyCode
                Case 13
                    If TxtPalletDest.Text.Trim <> "" Then
                        If ExistePallet(Me.TxtPalletDest.Text) Then
                            If GetPosicionPalletDestino(Me.TxtUbicacionOri.Text, Me.TxtPalletDest.Text) Then
                                Me.LblUbicacionDest.Visible = True
                                Me.TxtPalletDest.Text = UCase(Me.TxtPalletDest.Text)
                                Me.PalletDestino = Me.TxtPalletDest.Text
                                Me.TxtPalletDest.ReadOnly = True
                                Me.TxtUbicacionDest.Visible = True
                                Me.TxtUbicacionDest.Focus()
                            End If
                        Else
                            Me.TxtPalletDest.Text = ""
                            Me.TxtPalletDest.Focus()
                        End If
                    Else
                        Me.TxtPalletDest.Focus()
                    End If
            End Select
        Catch ex As Exception
            MsgBox("txtPalletOri_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub TxtUbicacionDest_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtUbicacionDest.KeyUp
        Try
            If e.KeyValue = 13 Then
                If Me.TxtUbicacionDest.Text <> "" Then
                    If UCase(Me.TxtUbicacionDest.Text) = UCase(Me.Ubicacion) Then
                        Dim Rta As Object = MsgBox("¿Confirma el Repaletizado?", MsgBoxStyle.YesNo, FrmName)
                        If Rta = vbYes Then
                            Transferir()
                            MsgBox("El repaletizado se realizo Correctamente", MsgBoxStyle.OkOnly, FrmName)
                        End If
                        InicializarFrm()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("TxtUbicacionDest_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub
    Private Function GetNuevoPallet(ByRef NroPallet As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "GET_VALUE_FOR_SEQUENCE"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc

                Pa = New SqlParameter("@SECUENCIA", SqlDbType.VarChar, 40)
                Pa.Value = "NROPALLET_SEQ"
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VALUE", SqlDbType.Decimal, 38)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

                NroPallet = IIf(IsDBNull(Cmd.Parameters("@VALUE").Value), "0", Cmd.Parameters("@VALUE").Value.ToString)
                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If

        Catch SQLEx As SqlException
            MsgBox("GET_VALUE_FOR_SEQUENCE SQL. " & SQLEx.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("GET_VALUE_FOR_SEQUENCE. " & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function
    Private Function Existe(ByRef contenedora As String) As Boolean
        Dim item As Object
        For Each item In Me.LstContenedoras.Items
            If UCase(item.ToString) = UCase(contenedora) Then
                Existe = True
                Exit Function
            End If
        Next
        Return False
    End Function
    Private Function ExistePallet(ByVal pallet As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim ValErr As String = ""
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "VERIFICA_PALLET"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc

                Pa = New SqlParameter("@PALLET_O", SqlDbType.VarChar, 100)
                Pa.Value = Me.TxtPalletOri.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PALLET_D", SqlDbType.VarChar, 100)
                Pa.Value = pallet
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If

        Catch SQLEx As SqlException
            MsgBox("VERIFICA_PALLET. " & SQLEx.Message)
            ValErr = Mid(SQLEx.Message.ToString, 1, 1)
            If ValErr = "1" Then
                Me.TxtPalletDest.Text = ""
                Me.TxtPalletDest.Focus()
            End If
            Return False
        Catch ex As Exception
            MsgBox("VERIFICA_PALLET. " & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function
    Private Function GetPosicionPallet(ByVal UbicacionOrigen As String, ByVal pallet As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "Locator_Transf"
                xCmd.CommandType = CommandType.StoredProcedure
                Da = New SqlDataAdapter(xCmd)

                Pa = New SqlParameter("@UbicacionOrigen", SqlDbType.VarChar, 50)
                Pa.Value = UbicacionOrigen
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@NroPallet", SqlDbType.VarChar, 100)
                Pa.Value = pallet
                xCmd.Parameters.Add(Pa)

                Da.Fill(Ds, "Ubicacion")
                If Ds.Tables("Ubicacion").Rows.Count > 0 Then
                    Ubicacion = Ds.Tables("Ubicacion").Rows(0)(1).ToString
                    Me.LblUbicacionDest.Text = "Ubicacion Destino: " + Ds.Tables("Ubicacion").Rows(0)(1).ToString
                End If
            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("GetPosicionPallet SQL. " & SQLEx.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox("GetPosicionPallet. " & ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
            Ds = Nothing
            Da = Nothing
        End Try
    End Function
    Private Function Transferir() As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim item As Object
        Dim trans As SqlClient.SqlTransaction
        trans = SQLc.BeginTransaction()
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "REPALETIZAR"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Cmd.Transaction = trans

                For Each item In Me.LstContenedoras.Items
                    'Aca transfiero
                    'imprimo una etiqueta por cada contenedora
                    'Genero la auditoria de correspondiente
                    Cmd.Parameters.Clear()

                    Pa = New SqlParameter("@POSICION_O", SqlDbType.VarChar, 100)
                    Pa.Value = Me.TxtUbicacionOri.Text
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@POSICION_D", SqlDbType.VarChar, 100)
                    Pa.Value = Me.TxtUbicacionDest.Text
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@PALLET_O", SqlDbType.VarChar, 100)
                    Pa.Value = Me.TxtPalletOri.Text
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@PALLET_D", SqlDbType.VarChar, 100)
                    Pa.Value = Me.PalletDestino
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                    Pa.Value = vUsr.CodUsuario
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@CONTENEDORA", SqlDbType.VarChar, 100)
                    Pa.Value = item.ToString
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Cmd.ExecuteNonQuery()
                Next
                trans.Commit()
                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If

        Catch SQLEx As SqlException
            MsgBox("REPALETIZAR SQL. " & SQLEx.Message, MsgBoxStyle.Critical, FrmName)
            trans.Rollback()
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
    Private Function GetPosicionPalletDestino(ByVal UbicacionOrigen As String, ByVal pallet As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "GET_UBICACION_PALLET"
                xCmd.CommandType = CommandType.StoredProcedure
                Da = New SqlDataAdapter(xCmd)

                Pa = New SqlParameter("@NroPallet", SqlDbType.VarChar, 100)
                Pa.Value = pallet
                xCmd.Parameters.Add(Pa)

                Da.Fill(Ds, "Ubicacion")
                If Ds.Tables("Ubicacion").Rows.Count > 0 Then
                    Ubicacion = Ds.Tables("Ubicacion").Rows(0)(0).ToString
                    Me.LblUbicacionDest.Text = "Ubicacion Destino: " + Ds.Tables("Ubicacion").Rows(0)(0).ToString
                End If
            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("GetPosicionPallet SQL. " & SQLEx.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox("GetPosicionPallet. " & ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
            Ds = Nothing
            Da = Nothing
        End Try
    End Function
End Class