Imports System.Data.SqlClient
Imports System.Data

Public Class frmUbicacionBultos
    Private esReubicacion As Boolean
    Private sDockReubicacion As String
    Private Const SQLConErr As String = "Fallo al intentar conectar con la base de datos."


    Private Sub btnIniciar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CrearTablaTemporal() Then
            lblMensaje.Text = "Lea el codigo de Guia."
            'btnIniciar.Enabled = False
            'btnFinalizar.Enabled = True
            btnPendientes.Enabled = True
            txtGuia.Enabled = True
            txtGuia.Focus()
        End If
    End Sub
    Private Function CrearTablaTemporal() As Boolean
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "CrearTMPBulto_Dock"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()
                Cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = vUsr.CodUsuario


                Cmd.ExecuteNonQuery()
                CrearTablaTemporal = True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If


        Catch SQLEx As SqlException
            MsgBox("CrearTablaTemporal: " & SQLEx.Message, MsgBoxStyle.OkOnly, "")
            Return False
        Catch ex As Exception
            MsgBox("CrearTablaTemporal: " & ex.Message, MsgBoxStyle.OkOnly, "")
        Finally
            Cmd = Nothing
        End Try
    End Function
    Private Function EliminarTablaTemporal() As Boolean
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "eliminarTMPBulto_Dock"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure

                Cmd.ExecuteNonQuery()
                EliminarTablaTemporal = True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If


        Catch SQLEx As SqlException
            MsgBox("EliminarTablaTemporal: " & SQLEx.Message, MsgBoxStyle.OkOnly, "")
            Return False
        Catch ex As Exception
            MsgBox("EliminarTablaTemporal: " & ex.Message, MsgBoxStyle.OkOnly, "")
        Finally
            Cmd = Nothing
        End Try
    End Function
    Private Sub txtDock_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDock.KeyUp
        'si apreto enter
        If e.KeyCode = Keys.Enter And Len(Trim(txtDock.Text)) > 0 Then
            'valido contra la base si existe el dock y esta activo
            If ValidarDock(txtDock.Text) Then
                'If esReubicacion Then
                'valido el dock
                If Len(sDockReubicacion) = 0 Then
                    sDockReubicacion = txtDock.Text
                End If
                If txtDock.Text = sDockReubicacion Then
                    'insert into tmp bultos-dock
                    If InsertBultoTMP(txtBulto.Text) Then
                        'valido pendientes
                        If hayBultosPendientes(txtGuia.Text) Then
                            'Call EliminarTablaTemporal()
                            txtDock.Text = ""
                            txtBulto.Text = ""
                            txtBulto.Focus()
                            lblMensaje.Text = "Ingrese el bulto que desea ubicar."
                        Else
                            If UbicarBultoenDock(txtDock.Text) Then
                                sDockReubicacion = ""
                                lblMensaje.Text = "Todos los bultos de la guia fueron correctamente ubicadados."
                                txtGuia.Text = ""
                                txtGuia.Enabled = True
                                txtBulto.Text = ""
                                txtBulto.Enabled = False
                                txtDock.Text = ""
                                txtDock.Enabled = False
                                'btnIniciar.Enabled = True
                                btnFinalizar.Enabled = False
                                btnPendientes.Enabled = False
                                txtGuia.Focus()
                            End If
                        End If
                    End If
                    Else
                    lblMensaje.Text = "El dock ingresado no corresponde a la guia " + txtGuia.Text + "."
                    End If
                Else
                    lblMensaje.Text = "El Dock ingresado no es valido."
                End If
            End If
    End Sub
    Private Function InsertBultoTMP(ByVal sBulto As String) As Boolean
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "InsertBultoTMP"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                'parametro de entrada
                Cmd.Parameters.Clear()
                Cmd.Parameters.Add("@bulto", SqlDbType.VarChar, 50).Value = sBulto
                Cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 10).Value = vUsr.CodUsuario

                Cmd.ExecuteNonQuery()
                InsertBultoTMP = True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If


        Catch SQLEx As SqlException
            MsgBox("InsertBultoTMP: " & SQLEx.Message, MsgBoxStyle.OkOnly, "")
        Catch ex As Exception
            MsgBox("InsertBultoTMP: " & ex.Message, MsgBoxStyle.OkOnly, "")
        Finally
            Cmd = Nothing
        End Try
    End Function
    Private Function hayBultosPendientes(ByVal sGuia) As Boolean
        Dim Cmd As New SqlCommand

        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "VALIDARBULTOSPENDIENTES"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                'parametro de entrada
                Cmd.Parameters.Clear()
                Cmd.Parameters.Add("@GUIA", SqlDbType.VarChar, 50).Value = sGuia
                Cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = vUsr.CodUsuario
                'parametro de salida
                Cmd.Parameters.Add("@value", SqlDbType.Int, 1).Direction = ParameterDirection.Output

                Cmd.ExecuteNonQuery()
                hayBultosPendientes = Cmd.Parameters("@VALUE").Value
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If


        Catch SQLEx As SqlException
            MsgBox("hayBultosPendientes: " & SQLEx.Message, MsgBoxStyle.OkOnly, "")
            Return False
        Catch ex As Exception
            MsgBox("hayBultosPendientes: " & ex.Message, MsgBoxStyle.OkOnly, "")
        Finally
            Cmd = Nothing
        End Try
    End Function
    Private Function ValidarDock(ByVal sDock As String) As Boolean
        Dim Cmd As New SqlCommand

        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "ValidaDock"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                'parametro de entrada
                Cmd.Parameters.Clear()
                Cmd.Parameters.Add("@dock", SqlDbType.VarChar, 50).Value = sDock
                'parametro de salida
                Cmd.Parameters.Add("@value", SqlDbType.Int, 1).Direction = ParameterDirection.Output

                Cmd.ExecuteNonQuery()
                ValidarDock = Cmd.Parameters("@VALUE").Value
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If


        Catch SQLEx As SqlException
            MsgBox("ValidarDock: " & SQLEx.Message, MsgBoxStyle.OkOnly, "")
            Return False
        Catch ex As Exception
            MsgBox("ValidarDock: " & ex.Message, MsgBoxStyle.OkOnly, "")
        Finally
            Cmd = Nothing
        End Try
    End Function

    Private Sub txtBulto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBulto.KeyUp
        If e.KeyCode = Keys.Enter And Len(Trim(txtBulto.Text)) > 0 Then
            If esReubicacion Then
                If ValidarBultoenGuia(txtBulto.Text, txtGuia.Text) Then
                    lblMensaje.Text = "Ingrese el Dock de destino."
                    txtDock.Enabled = True
                    txtDock.Text = ""
                    txtDock.Focus()
                Else
                    lblMensaje.Text = "El bulto seleccionado no pertenece a la guia."
                    txtBulto.Text = ""
                    txtBulto.Focus()
                End If
            Else
                Select Case ValidarBulto(txtBulto.Text, txtGuia.Text)

                    Case 0
                        lblMensaje.Text = "El Bulto ingresado no pertenece a la guia."

                    Case 1

                        lblMensaje.Text = "Ingrese el Dock destino."
                        txtDock.Enabled = True
                        txtDock.Text = ""
                        txtDock.Focus()

                    Case 2
                        lblMensaje.Text = "La guia del bulto se encuentra ubicada en otro Dock."
                End Select
            End If
        End If

    End Sub
    Private Function UbicarBultoenDock(ByVal sDock As String) As Boolean
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "ubicarBultoenDock"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                'parametro de entrada
                Cmd.Parameters.Clear()
                Cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 10).Value = vUsr.CodUsuario
                Cmd.Parameters.Add("@DOCK", SqlDbType.VarChar, 50).Value = sDock
                Cmd.ExecuteNonQuery()
                UbicarBultoenDock = True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If

        Catch SQLEx As SqlException
            MsgBox("ubicarBultoenDock: " & SQLEx.Message, MsgBoxStyle.OkOnly, "Ubicacion de Bultos")
        Catch ex As Exception
            MsgBox("ubicarBultoenDock: " & ex.Message, MsgBoxStyle.OkOnly, "Ubicacion de Bultos")
        Finally
            Cmd = Nothing
        End Try
    End Function
    Private Function ValidarBultoenGuia(ByVal sBulto As String, ByVal sGuia As String) As Boolean
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "ValidarBultoenGuia"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                'parametro de entrada
                Cmd.Parameters.Clear()
                Cmd.Parameters.Add("@BULTO", SqlDbType.VarChar, 100).Value = sBulto
                Cmd.Parameters.Add("@GUIA", SqlDbType.VarChar, 20).Value = sGuia
                'parametro de salida
                Cmd.Parameters.Add("@value", SqlDbType.Int, 1).Direction = ParameterDirection.Output

                Cmd.ExecuteNonQuery()
                ValidarBultoenGuia = Cmd.Parameters("@VALUE").Value
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If


        Catch SQLEx As SqlException
            MsgBox("ValidarBultoenGuia: " & SQLEx.Message, MsgBoxStyle.OkOnly, "Ubicacion de Bultos")
            Return False
        Catch ex As Exception
            MsgBox("ValidarBultoenGuia: " & ex.Message, MsgBoxStyle.OkOnly, "Ubicacion de Bultos")
        Finally
            Cmd = Nothing
        End Try
    End Function
    Private Function ValidarBulto(ByVal sBulto As String, ByVal sGuia As String) As Integer
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "ValidarBulto"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                'parametro de entrada
                Cmd.Parameters.Clear()
                Cmd.Parameters.Add("@BULTO", SqlDbType.VarChar, 100).Value = sBulto
                Cmd.Parameters.Add("@GUIA", SqlDbType.VarChar, 20).Value = sGuia
                'parametro de salida
                Cmd.Parameters.Add("@value", SqlDbType.Int, 1).Direction = ParameterDirection.Output

                Cmd.ExecuteNonQuery()
                ValidarBulto = Cmd.Parameters("@VALUE").Value
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If


        Catch SQLEx As SqlException
            MsgBox("ValidarBulto: " & SQLEx.Message, MsgBoxStyle.OkOnly, "Ubicacion de Bultos")
            Return False
        Catch ex As Exception
            MsgBox("ValidarBulto: " & ex.Message, MsgBoxStyle.OkOnly, "Ubicacion de Bultos")
        Finally
            Cmd = Nothing
        End Try
    End Function

    Private Sub btnFinalizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinalizar.Click
        If Len(txtGuia.Text) > 0 Then
            Dim msg = "¿Desea cancelar la operación?" & vbNewLine & "Toda la operación en curso sera cancelada."
            If MsgBox(msg, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Ubicacion de Bultos") = MsgBoxResult.Yes Then
                btnPendientes.Enabled = False
                btnFinalizar.Enabled = False
                txtGuia.Enabled = True
                txtGuia.Text = ""
                lblMensaje.Text = "Lea el codigo de Guia."
                txtDock.Text = ""
                txtDock.Enabled = False
                txtBulto.Text = ""
                txtBulto.Enabled = False
                txtGuia.Focus()
            End If
        End If
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        If btnPendientes.Enabled = False Then
            Me.Close()
        Else
            lblMensaje.Text = "Debe concluir la ubicacion de todos los bultos antes de salir."
        End If
    End Sub

    

    Private Sub txtGuia_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtGuia.KeyUp
        If e.KeyCode = Keys.Enter And Len(Trim(txtGuia.Text)) > 0 Then
            CrearTablaTemporal()
            '            lblMensaje.Text = "Lea el codigo de Guia."
            'btnIniciar.Enabled = False
            btnFinalizar.Enabled = True
            btnPendientes.Enabled = True
            'valido existencia de guia
            If ValidarGuia(txtGuia.Text) Then
                txtGuia.Enabled = False
                txtBulto.Text = ""
                txtBulto.Enabled = True
                lblMensaje.Text = "Ingrese el codigo de Bulto."
                txtBulto.Focus()
            Else
                lblMensaje.Text = "La guia ingresada no es valida."
            End If

        End If
    End Sub
    Private Function ReubicarGuia(ByVal sGuia As String, ByVal sDock As String) As Boolean
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "ReubicarGuia"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                'parametro de entrada
                Cmd.Parameters.Clear()
                Cmd.Parameters.Add("@DOCK", SqlDbType.VarChar, 50).Value = sDock
                Cmd.Parameters.Add("@GUIA", SqlDbType.VarChar, 20).Value = sGuia

                Cmd.ExecuteNonQuery()
                ReubicarGuia = True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If


        Catch SQLEx As SqlException
            MsgBox("ReubicarGuia: " & SQLEx.Message, MsgBoxStyle.OkOnly, "Ubicacion de Bultos")
            Return False
        Catch ex As Exception
            MsgBox("ReubicarGuia: " & ex.Message, MsgBoxStyle.OkOnly, "Ubicacion de Bultos")
        Finally
            Cmd = Nothing
        End Try
    End Function
    Private Function ValidarGuia(ByVal sGuia As String) As Boolean
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "ValidarGuia"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                'parametro de entrada
                Cmd.Parameters.Clear()
                Cmd.Parameters.Add("@GUIA", SqlDbType.VarChar, 100).Value = sGuia
                'parametro de salida
                Cmd.Parameters.Add("@value", SqlDbType.Int, 1).Direction = ParameterDirection.Output

                Cmd.ExecuteNonQuery()
                ValidarGuia = Cmd.Parameters("@VALUE").Value
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If


        Catch SQLEx As SqlException
            MsgBox("ValidarGuia: " & SQLEx.Message, MsgBoxStyle.OkOnly, "Ubicacion de Bultos")
            Return False
        Catch ex As Exception
            MsgBox("ValidarGuia: " & ex.Message, MsgBoxStyle.OkOnly, "Ubicacion de Bultos")
        Finally
            Cmd = Nothing
        End Try
    End Function
    Private Function ValidarTotaldeBultos(ByVal sGuia As String) As Boolean
        Dim Cmd As New SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Dim xSQL As String = "ValidarTotaldeBultos"
                Cmd.Connection = SQLc
                Cmd.CommandText = xSQL
                Cmd.CommandType = Data.CommandType.StoredProcedure
                'parametro de entrada
                Cmd.Parameters.Clear()
                Cmd.Parameters.Add("@GUIA", SqlDbType.VarChar, 100).Value = sGuia
                'parametro de salida
                Cmd.Parameters.Add("@value", SqlDbType.Int, 1).Direction = ParameterDirection.Output

                Cmd.ExecuteNonQuery()
                ValidarTotaldeBultos = Cmd.Parameters("@VALUE").Value
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If


        Catch SQLEx As SqlException
            MsgBox("ValidarTotaldeBultos: " & SQLEx.Message, MsgBoxStyle.OkOnly, "Ubicacion de Bultos")
            Return False
        Catch ex As Exception
            MsgBox("ValidarTotaldeBultos: " & ex.Message, MsgBoxStyle.OkOnly, "Ubicacion de Bultos")
        Finally
            Cmd = Nothing
        End Try
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPendientes.Click
        If Len(txtGuia.Text) > 0 Then
            Dim frmPendientes As New frmBultosPendientes()
            frmPendientes.sGuia = txtGuia.Text
            frmPendientes.Show()
        End If
    End Sub
End Class