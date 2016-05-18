Imports System.Data.SqlClient
Imports System.Data

Public Class frmTransferenciaManual

    Private Const vMenu As String = "F1) Terminar Tarea." & vbNewLine & "F2) Cancelar." & vbNewLine & "F3) Salir."
    Private slblUbicacion As String = "Ubicación: "
    Private sProcesando As String = "Procesando..."
    Private Const FrmName As String = "Transferencias"
    Private blnCancelar As Boolean = True
    Private sIngresarPallet As String = "Debe ingresar el Nro. de Pallet"
    Private sIngresarDestino As String = "Debe ingresar la Ubicación"
    Private sIngresarPalletDestino As String = "Debe ingresar el Nro. de Pallet y la Ubicación"
    Private blnTransCurso As Boolean = False
    Private Const SQLConErr As String = "Fallo al intentar conectar con la base de datos."
    Private TrLocator As Boolean
    Private Ubicacion As String
    Private PorContenedora As Boolean

    Public Property TransferenciaGuiada() As Boolean
        Get
            Return TrLocator
        End Get
        Set(ByVal value As Boolean)
            TrLocator = value
        End Set
    End Property

    Public Property TrContenedora() As Boolean
        Get
            Return PorContenedora
        End Get
        Set(ByVal value As Boolean)
            PorContenedora = value
        End Set
    End Property

    Private Sub frmTransferenciaManual_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Try
            Select Case e.KeyCode
                Case Keys.F1
                    StartTransF()
                Case Keys.F2
                    CancelTransf()
                Case Keys.F3
                    ExitTransf()
            End Select
        Catch ex As Exception
            MsgBox("frmTransferencia_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub frmTransferenciaManual_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = FrmName
        InicializarFrm()
    End Sub

    Private Sub txtPallet_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPallet.KeyUp
        Try
            Select Case e.KeyCode
                Case 13
                    If txtPallet.Text.Trim <> "" Then
                        Me.lblMsg.Text = ""
                        If Verifica_Pallet_Pos(Me.txtOrigen.Text, Me.txtPallet.Text, Me.PorContenedora) Then
                            If TransferenciaGuiada Then
                                If Not GetUbicacionLocator(Me.txtOrigen.Text, Me.txtPallet.Text) Then
                                    Exit Sub
                                End If
                            End If
                            Me.txtPallet.Text = UCase(Me.txtPallet.Text)
                            Me.txtPallet.ReadOnly = True
                            Me.lblDestino.Visible = True
                            Me.txtDestino.Visible = True
                            Me.txtDestino.Focus()
                        Else
                            Me.txtPallet.Text = ""
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox("txtPallet_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Function GetUbicacionLocator(ByVal UbicacionOrigen As String, ByVal Pallet As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand

                If Not Me.PorContenedora Then
                    xCmd.CommandText = "[dbo].[Locator_Transf]"
                Else
                    xCmd.CommandText = "[dbo].[Locator_Transf_Contenedora]"
                End If

                xCmd.CommandType = CommandType.StoredProcedure
                Da = New SqlDataAdapter(xCmd)

                Pa = New SqlParameter("@UbicacionOrigen", SqlDbType.VarChar, 50)
                Pa.Value = UbicacionOrigen
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                If Not Me.PorContenedora Then
                    Pa = New SqlParameter("@NroPallet", SqlDbType.VarChar, 100)
                    Pa.Value = Pallet
                    xCmd.Parameters.Add(Pa)
                Else
                    Pa = New SqlParameter("@NroContenedora", SqlDbType.VarChar, 100)
                    Pa.Value = Pallet
                    xCmd.Parameters.Add(Pa)
                End If

                Da.Fill(Ds, "Ubicacion")
                If Ds.Tables("Ubicacion").Rows.Count > 0 Then
                    Ubicacion = Ds.Tables("Ubicacion").Rows(0)(1).ToString
                    Me.lblPosSug.Text = Ds.Tables("Ubicacion").Rows(0)(1).ToString
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            Me.lblMsg.Text = "GetUbicacionLocator SQL. " & SQLEx.Message
        Catch ex As Exception
            Me.lblMsg.Text = "GetUbicacionLocator. " & ex.Message
        Finally
            xCmd = Nothing
            Pa = Nothing
            Ds = Nothing
            Da = Nothing
        End Try
    End Function

    Private Sub txtDestino_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDestino.KeyUp
        Try
            Select Case e.KeyCode
                Case 13
                    If Me.txtDestino.Text <> "" Then
                        If Me.txtOrigen.Text.Trim.ToUpper <> Me.txtDestino.Text.Trim.ToUpper Then
                            If ExisteNavePosicion(Me.txtDestino.Text) Then
                                If Not VerificaNavePRE(Me.txtDestino.Text) Then
                                    MsgBox("La Nave/Posicion se encuentra inhabilitada para operaciones de transferencias", MsgBoxStyle.Information, FrmName)
                                    Me.txtDestino.Text = ""
                                    Exit Sub
                                End If
                            Else
                                Me.txtDestino.Text = ""
                                Me.txtDestino.Focus()
                                Exit Sub
                            End If
                            If TrLocator Then
                                If Trim(UCase(Me.txtDestino.Text)) <> Trim(UCase(Ubicacion)) Then
                                    MsgBox("Posicion Invalida.", MsgBoxStyle.Exclamation, FrmName)
                                    Me.txtDestino.Text = ""
                                    Me.txtDestino.Focus()
                                    Exit Try
                                End If
                            End If

                            If Not Me.PorContenedora Then
                                '-----------------------------------------------------------------------------------------------------------------------------
                                'Logica de transferencia de pallets.
                                '-----------------------------------------------------------------------------------------------------------------------------
                                Dim Rta As Object = MsgBox("¿Confirma la Transferencia?", MsgBoxStyle.YesNo, FrmName)
                                If Rta = vbNo Then
                                    Me.InicializarFrm()
                                    Exit Sub
                                End If
                                If txtDestino.Text.Trim <> "" Then
                                    txtDestino.Text = UCase(txtDestino.Text)
                                    If Transferir(Me.txtOrigen.Text, Me.txtDestino.Text, vUsr.CodUsuario, Me.txtPallet.Text) Then
                                        MsgBox("La Transferencia se realizo Correctamente", MsgBoxStyle.OkOnly, FrmName)
                                        If (RequiereRemito(Me.txtDestino.Text)) Then
                                            ImprimirRemito()
                                        End If
                                        Me.InicializarFrm()
                                    Else
                                        Me.lblMsg.Text = "No se pudo Completar la Operacion."
                                        Me.txtDestino.Text = ""
                                    End If
                                End If
                            Else
                                '-----------------------------------------------------------------------------------------------------------------------------
                                'Logica para poder poner el pallet si corresponde.
                                '-----------------------------------------------------------------------------------------------------------------------------
                                TransferenciaContenedora()
                            End If 'Fin por contenedora
                        Else
                            Me.txtDestino.SelectAll()
                            Me.lblMsg.Text = "La posicion Origen y destino son iguales."
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox("txtDestino_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Function TransferenciaContenedora() As Boolean
        Dim ePallet As Boolean
        Try
            'A. Tiene pallet la posicion con ese material?
            ePallet = Me.ContPalletPosicion(Me.txtPallet.Text, Me.txtDestino.Text)
            'B. Sino tiene... dejo el material en la posicion.
            If ePallet Then
                Me.lblPalletDestino.Visible = True
                Me.txtPalletDestino.Text = ""
                Me.txtPalletDestino.Visible = True
                Me.txtPalletDestino.Focus()
            Else
                
                If TransferirContenedora(Me.txtOrigen.Text, Me.txtDestino.Text, vUsr.CodUsuario, Me.txtPallet.Text, Me.txtPalletDestino.Text) Then
                    MsgBox("La Transferencia se realizo Correctamente", MsgBoxStyle.OkOnly, FrmName)
                    If (RequiereRemito(Me.txtDestino.Text)) Then
                        ImprimirRemito()
                    End If
                    Me.InicializarFrm()
                Else
                    Me.lblMsg.Text = "No se pudo Completar la Operacion."
                    Me.txtDestino.Text = ""
                End If

            End If
        Catch SqlEx As SqlException
            MsgBox("Excepción BD: " & SqlEx.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Function

    Private Function ContPalletPosicion(ByVal Contenedora As String, ByVal PosicionDestino As String) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, RETORNO As Boolean = False
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandText = "SELECT DBO.MOB_TR_VERFICA_PALLET_POSICION_DESTINO('" & Trim(PosicionDestino) & "','" & Trim(Contenedora) & "')"
                Cmd.CommandType = CommandType.Text

                DA.Fill(DS)

                If DS.Tables(0).Rows(0)(0) = 0 Then
                    RETORNO = False
                Else
                    RETORNO = True
                End If
            Else
                Me.lblMsg.Text = SQLConErr
                Return False
            End If
            Return RETORNO
        Catch sqlex As SqlException
            MsgBox("Excepción: " & sqlex.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            DA.Dispose()
            Cmd.Dispose()
            DS.Dispose()
        End Try
    End Function

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
                Me.lblMsg.Text = SQLConErr
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

    Private Sub lblPallet_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblPallet.TextChanged
        lblMsg.Text = ""
    End Sub

    Private Sub txtPallet_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPallet.TextChanged
        lblMsg.Text = ""
    End Sub

    Private Sub InicializarFrm()
        Try
            Me.lblPosSug.Text = ""
            Me.cmdStartTransf.Enabled = True
            Me.blnTransCurso = False
            lblMenu.Text = vMenu
            lblMsg.Text = ""
            Me.lblOrigen.Visible = False
            Me.txtOrigen.Visible = False
            Me.txtOrigen.Text = ""
            Me.txtOrigen.ReadOnly = False
            Me.lblPallet.Visible = False
            Me.txtPallet.Visible = False
            Me.txtPallet.Text = ""
            Me.txtPallet.ReadOnly = False
            Me.lblDestino.Visible = False
            Me.txtDestino.Visible = False
            Me.txtDestino.Text = ""
            Me.txtDestino.ReadOnly = False
            Me.lblPalletDestino.Visible = False
            Me.txtPalletDestino.Text = ""
            Me.txtPalletDestino.Visible = False
        Catch ex As Exception
            MsgBox("InicializarFrm: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub cmdStartTransf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartTransf.Click
        StartTransF()
    End Sub

    Private Sub StartTransF()
        Me.blnTransCurso = True
        Me.lblOrigen.Visible = True
        Me.txtOrigen.Visible = True
        Me.txtOrigen.Text = ""
        Me.txtOrigen.Focus()
        Me.lblMsg.Text = ""
        Me.cmdStartTransf.Enabled = False
    End Sub

    Private Sub cmdCancelTransf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelTransf.Click
        CancelTransf()
    End Sub

    Private Sub CancelTransf()
        Me.cmdStartTransf.Enabled = True
        Me.txtDestino.Text = ""
        Me.txtOrigen.Text = ""
        Me.txtPallet.Text = ""
        Me.blnTransCurso = False
        InicializarFrm()
    End Sub

    Private Sub cmdExitTransf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExitTransf.Click
        ExitTransf()
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
                Me.lblMsg.Text = SQLConErr
                Return False
            End If
        Catch SQLEx As SqlException
            Me.lblMsg.Text = "VerificaPosLockeada SQL. " & SQLEx.Message
            Return False
        Catch ex As Exception
            Me.lblMsg.Text = "VerificaPosLockeada. " & ex.Message
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub txtOrigen_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOrigen.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtOrigen.Text <> "" Then
                '------------------------------------------------------------------
                'Para soportar la transferencia por contenedora.
                '------------------------------------------------------------------
                If Me.PorContenedora = True Then
                    Me.lblPallet.Text = "Contenedora:"
                Else
                    Me.lblPallet.Text = "Pallet:"
                End If
                '------------------------------------------------------------------
                Me.lblMsg.Text = ""
                If ExisteNavePosicion(Me.txtOrigen.Text) Then
                    If Not VerificaNavePRE(Me.txtOrigen.Text) Then
                        MsgBox("La Nave/Posicion se encuentra inhabilitada para operaciones de transferencias", MsgBoxStyle.Information, FrmName)
                        Me.txtOrigen.Text = ""
                        Exit Sub
                    End If
                Else
                    Me.txtOrigen.Text = ""
                    Me.txtOrigen.Focus()
                    Exit Sub
                End If
                If VerificaPosLockeada(Me.txtOrigen.Text) Then
                    Me.txtOrigen.Text = UCase(Me.txtOrigen.Text)
                    Me.txtOrigen.ReadOnly = True
                    Me.lblPallet.Visible = True
                    Me.txtPallet.Visible = True
                    Me.txtPallet.Focus()
                Else
                    Me.txtOrigen.Text = ""
                End If
            End If
        End If
    End Sub

    Private Function TransferirContenedora(ByVal Pos_O As String, ByVal Pos_D As String, ByVal Usr As String, ByVal Contenedora As String, ByVal PalletDestino As String) As Boolean

        Dim ValErr As String = ""
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Trans As SqlTransaction
        Trans = SQLc.BeginTransaction
        Try
            If VerifyConnection(SQLc) Then

                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "[dbo].[MOB_TRANSFERENCIA_CONTENEDORA]"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Cmd.Transaction = Trans

                Pa = New SqlParameter("@POSICION_O", SqlDbType.VarChar, 45)
                Pa.Value = UCase(Trim(Pos_O))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_D", SqlDbType.VarChar, 45)
                Pa.Value = UCase(Trim(Pos_D))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 30)
                Pa.Value = UCase(Trim(Usr))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PALLET_DESTINO", SqlDbType.VarChar, 100)
                Pa.Value = IIf(PalletDestino = "", DBNull.Value, UCase(Trim(PalletDestino)))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CONTENEDORA", SqlDbType.VarChar, 50)
                Pa.Value = UCase(Trim(Contenedora))
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

            Else
                lblMsg.Text = SQLConErr
                Return False
            End If
            Trans.Commit()
            Return True
        Catch SQLEx As SqlException
            Try
                Trans.Rollback()
            Catch ex As Exception
            End Try
            ValErr = Mid(SQLEx.Message.ToString, 1, 1)
            MsgBox("Transferir SQL. " & SQLEx.Message)
            Me.lblMsg.Text = "Transferir SQL. " & SQLEx.Message
            If ValErr = "1" Then
                Me.InicializarFrm()
            End If
            Return False
        Catch ex As Exception
            Trans.Rollback()
            MsgBox("Transferir SQL. " & ex.Message)
            Me.lblMsg.Text = "Transferir. " & ex.Message
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
            Trans = Nothing
        End Try
    End Function

    Private Function Transferir(ByVal Pos_O As String, ByVal Pos_D As String, ByVal Usr As String, _
                                ByVal Pallet As String) As Boolean

        Dim ValErr As String = ""
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Trans As SqlTransaction
        Trans = SQLc.BeginTransaction
        Try
            If VerifyConnection(SQLc) Then

                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "MOB_TRANSFERENCIA"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc
                Cmd.Transaction = Trans

                Pa = New SqlParameter("@POSICION_O", SqlDbType.VarChar, 45)
                Pa.Value = Pos_O
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_D", SqlDbType.VarChar, 45)
                Pa.Value = Pos_D
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 30)
                Pa.Value = Usr
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PALLET", SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

            Else
                lblMsg.Text = SQLConErr
                Return False
            End If
            Trans.Commit()
            Return True
        Catch SQLEx As SqlException
            Try
                Trans.Rollback()
            Catch ex As Exception
            End Try
            ValErr = Mid(SQLEx.Message.ToString, 1, 1)
            MsgBox("Transferir SQL. " & SQLEx.Message)
            Me.lblMsg.Text = "Transferir SQL. " & SQLEx.Message
            If ValErr = "1" Then
                Me.InicializarFrm()
            End If
            Return False
        Catch ex As Exception
            Trans.Rollback()
            MsgBox("Transferir SQL. " & ex.Message)
            Me.lblMsg.Text = "Transferir. " & ex.Message
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
            Trans = Nothing
        End Try
    End Function

    Private Function Verifica_Pallet_Pos(ByVal Pos_O As String, ByVal Pallet As String, Optional ByVal TIPO As Boolean = False) As Boolean
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
                Pa = Nothing

                Pa = New SqlParameter("@tipo", SqlDbType.VarChar, 1)
                Pa.Value = IIf(TIPO = True, "1", "0")
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("Transferir SQL. " & SQLEx.Message)
            Me.lblMsg.Text = "Transferir SQL. " & SQLEx.Message
            ValErr = Mid(SQLEx.Message.ToString, 1, 1)
            If ValErr = "1" Then
                Me.InicializarFrm()
                Me.lblOrigen.Visible = True
                Me.txtOrigen.Visible = True
                Me.txtOrigen.Focus()
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
                Me.lblMsg.Text = SQLConErr
                Return False
            End If

        Catch SQLEx As SqlException
            Me.lblMsg.Text = "Mob_Verifica_Nave_Pre SQL. " & SQLEx.Message
            Return False
        Catch ex As Exception
            Me.lblMsg.Text = "Mob_Verifica_Nave_Pre. " & ex.Message
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function
    Private Function RequiereRemito(ByVal destino As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Value As String
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "GET_IMPRIME_REMITO"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc

                Pa = New SqlParameter("@POSICION_ORIGEN", SqlDbType.VarChar, 40)
                Pa.Value = Me.txtOrigen.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_DESTINO", SqlDbType.VarChar, 40)
                Pa.Value = destino
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@IMPRIME_REMITO", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()

                Value = IIf(IsDBNull(Cmd.Parameters("@IMPRIME_REMITO").Value), "0", Cmd.Parameters("@IMPRIME_REMITO").Value)
                If Value = "1" Then
                    Return True
                ElseIf Value = "0" Then
                    Return False
                End If
            Else
                Me.lblMsg.Text = SQLConErr
                Return False
            End If

        Catch SQLEx As SqlException
            Me.lblMsg.Text = "Mob_Verifica_Nave_Pre SQL. " & SQLEx.Message
            Return False
        Catch ex As Exception
            Me.lblMsg.Text = "Mob_Verifica_Nave_Pre. " & ex.Message
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function
    Private Function ImprimirRemito() As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "INSERT_IMPRESION_REMITOS"
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Connection = SQLc

                Pa = New SqlParameter("@POSICION_ORIGEN", SqlDbType.VarChar, 40)
                Pa.Value = Me.txtOrigen.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PALLET", SqlDbType.VarChar, 100)
                Pa.Value = Me.txtPallet.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@POSICION_DESTINO", SqlDbType.VarChar, 40)
                Pa.Value = Me.txtDestino.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

            Else
                Me.lblMsg.Text = SQLConErr
                Return False
            End If

        Catch SQLEx As SqlException
            Me.lblMsg.Text = "Mob_Verifica_Nave_Pre SQL. " & SQLEx.Message
            Return False
        Catch ex As Exception
            Me.lblMsg.Text = "Mob_Verifica_Nave_Pre. " & ex.Message
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub txtOrigen_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOrigen.TextChanged

    End Sub

    Private Sub txtDestino_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDestino.TextChanged

    End Sub

    Private Sub txtPalletDestino_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPalletDestino.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtPalletDestino.Text <> "" Then
                Try
                    '------------------------------------------------------------------------------------------------------------------------
                    '1. Valido el Pallet destino.
                    '------------------------------------------------------------------------------------------------------------------------
                    If (ValidarPalletDestino(UCase(Trim(Me.txtDestino.Text)), UCase(Trim(Me.txtPalletDestino.Text)))) Then
                        If TransferirContenedora(Me.txtOrigen.Text, Me.txtDestino.Text, vUsr.CodUsuario, Me.txtPallet.Text, Me.txtPalletDestino.Text) Then
                            MsgBox("La Transferencia se realizo Correctamente", MsgBoxStyle.OkOnly, FrmName)
                            If (RequiereRemito(Me.txtDestino.Text)) Then
                                ImprimirRemito()
                            End If
                            Me.InicializarFrm()
                        Else
                            Me.lblMsg.Text = "No se pudo Completar la Operacion."
                            Me.txtDestino.Text = ""
                        End If
                    Else : MsgBox("El pallet indicado no se encuentra en la posicion indicada", MsgBoxStyle.Information, FrmName)
                        Me.txtPalletDestino.Text = ""
                        Me.txtPalletDestino.Focus()
                    End If
                Catch SqlEx As SqlException
                    MsgBox("Excepción BD: " & SqlEx.Message, MsgBoxStyle.Critical, FrmName)
                Catch ex As Exception
                    MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
                End Try
            End If
        End If
    End Sub

    Private Function ValidarPalletDestino(ByVal PosicionDestino As String, ByVal PalletDestino As String) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, RETORNO As Boolean = False
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                Cmd.CommandText = "SELECT DBO.MOB_TR_VERFICA_PALLET_POSICION('" & PalletDestino & "','" & PosicionDestino & "')"
                Cmd.CommandType = CommandType.Text

                DA.Fill(DS)

                If DS.Tables(0).Rows(0)(0) = 0 Then
                    RETORNO = False
                Else
                    RETORNO = True
                End If

            Else : Me.lblMsg.Text = SQLConErr
                Return False
            End If
            Return RETORNO
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            DA.Dispose()
            Cmd.Dispose()
            DS.Dispose()
        End Try
    End Function

    Private Sub txtPalletDestino_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPalletDestino.TextChanged

    End Sub
End Class