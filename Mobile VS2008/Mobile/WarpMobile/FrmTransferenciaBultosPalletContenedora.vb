Imports System.Data
Imports System.Data.SqlClient

Public Class FrmTransferenciaBultosPalletContenedora

    Private Const SQLConErr As String = "Fallo al intentar conectar con la base de datos."
    Private Const FrmName As String = "Transferencia por Bultos"
    Private vPosicionOrigen As String
    Private vCompañia As String
    Private vProducto As String
    Private vPosicionDestino As String
    Private vCancelado As Boolean = False
    Private TieneContenedora As String
    Private TienePallet As String
    Private SolicitaPallet As String
    Private SolicitaContenedora As String
    Private GeneraInfo As Boolean
    Public TipoValidacion As String
    Public Lectura As String = ""

#Region "Propiedades"

    Public ReadOnly Property GeneracionDeInformacionAdicional() As Boolean
        Get
            Return GeneraInfo
        End Get
    End Property

    Public Property PosicionOrigen() As String
        Get
            Return vPosicionOrigen
        End Get
        Set(ByVal value As String)
            vPosicionOrigen = value
        End Set
    End Property

    Public Property Compañia() As String
        Get
            Return Me.vCompañia
        End Get
        Set(ByVal value As String)
            Me.vCompañia = value
        End Set
    End Property

    Public Property Producto() As String
        Get
            Return Me.vProducto
        End Get
        Set(ByVal value As String)
            Me.vProducto = value
        End Set
    End Property

    Public Property PosicionDestino() As String
        Get
            Return Me.vPosicionDestino
        End Get
        Set(ByVal value As String)
            Me.vPosicionDestino = value
        End Set
    End Property

    Public ReadOnly Property Cancelado() As Boolean
        Get
            Return Me.vCancelado
        End Get
    End Property

#End Region

    Private Sub FrmTransferenciaBultosPalletContenedora_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.LlenarInformacionPantalla()
    End Sub

    Private Sub LlenarInformacionPantalla()
        Try
            Me.lblCompañia.Text = "Compañia: " & Me.Compañia
            Me.lblOrigen.Text = "Ubic. Origen: " & Me.PosicionOrigen
            Me.lblProducto.Text = "Producto: " & Me.Producto
            Me.lblDestino.Text = "Ubic. Destino: " & Me.PosicionDestino
            GetParametrosDestino()
            Me.txtBultoPallet.Text = ""

            If (TieneContenedora = "0" And SolicitaContenedora = "1") Then
                'Validado con Martin, si no tiene contenedora y en el M.P. tiene el check se debe generar si o si.
                GeneraInfo = True
                Me.Close()
                Exit Try
            End If

            If (TieneContenedora = "1" And SolicitaContenedora = "0") Then
                Me.lblBultoPallet.Text = "Indique el numero de bulto"
                Me.txtBultoPallet.Focus()
                Me.txtBultoPallet.Visible = True
                TipoValidacion = "BULTO"
                Exit Sub
            End If

            If (TieneContenedora = "0" And SolicitaContenedora = "0") And (TienePallet = "0" And SolicitaPallet = "0") Then
                Me.Close()
                Exit Try
            End If

            If (TieneContenedora = "1" And SolicitaContenedora = "1") Then

                If (MsgBox("El producto lleva contenedora , ¿desea generar una o utilizar una existente?" & vbNewLine & "(Presione Si para generar una nueva)", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes) Then
                    GeneraInfo = True
                    Me.Close()
                    Exit Try
                End If

                Me.lblBultoPallet.Text = "Indique el numero de bulto"
                TipoValidacion = "BULTO"
                Me.txtBultoPallet.Focus()
                Me.txtBultoPallet.Visible = True
                Exit Sub

            End If

            If (TienePallet = "0" And SolicitaPallet = "1") Then
                If (MsgBox("El producto esta configurado para utilizar pallet, ¿desea generar uno?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.No) Then
                    GeneraInfo = False
                    Me.Close()
                Else
                    GeneraInfo = True
                    Me.Close()
                End If
            End If

            If (TienePallet = "1" And SolicitaPallet = "0") Then
                'tengo que generar el pallet.
                Me.Close()
                Exit Try
            End If

            If (TienePallet = "0" And SolicitaPallet = "0") Then
                'tengo que generar el pallet.
                Me.Close()
                Exit Try
            End If

            If (TienePallet = "1" And SolicitaPallet = "1") Then
                'tengo que generar el pallet.
                TipoValidacion = "PALLET"
                Me.lblBultoPallet.Text = "Indique el numero de pallet"
                Me.txtBultoPallet.Focus()
                Me.txtBultoPallet.Visible = True
                Exit Sub
            End If

        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub Cancelar()
        Me.vCancelado = True
        Me.Close()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Cancelar()
    End Sub

    Private Function GetParametrosDestino() As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter
        Try
            If (VerifyConnection(SQLc)) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "DBO.GET_CONF_POSICION"
                Cmd.CommandType = CommandType.StoredProcedure

                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                PA = New SqlParameter("@POSICION_DESTINO", SqlDbType.VarChar, 45)
                PA.Value = Me.PosicionDestino
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.Compañia
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                PA.Value = Me.Producto
                Cmd.Parameters.Add(PA)
                PA = Nothing

                DA.Fill(DS)
                If DS.Tables.Count > 0 Then
                    TienePallet = DS.Tables(0).Rows(0)(0).ToString
                    TieneContenedora = DS.Tables(0).Rows(0)(1).ToString
                    SolicitaPallet = DS.Tables(0).Rows(0)(2).ToString
                    SolicitaContenedora = DS.Tables(0).Rows(0)(3).ToString
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            Cmd.Dispose()
            DA.Dispose()
            DS.Dispose()
        End Try
    End Function

    Private Sub txtBultoPallet_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBultoPallet.KeyUp
        If e.KeyValue = 13 Then
            If Trim(Me.txtBultoPallet.Text) <> "" Then
                If Not ValidarIngreso() Then
                    Me.txtBultoPallet.Text = ""
                    Lectura = Trim(Me.txtBultoPallet.Text)
                    Me.txtBultoPallet.Focus()
                Else
                    Lectura = Trim(Me.txtBultoPallet.Text)
                    Me.Close()
                End If
            End If
        End If
    End Sub

    Private Function ValidarIngreso() As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, PA As SqlParameter, Retorno As Boolean = False
        Try
            If (VerifyConnection(SQLc)) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "DBO.TR_BULTO_VALIDA_PALLET_CONTENEDORA"
                Cmd.CommandType = CommandType.StoredProcedure

                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet

                PA = New SqlParameter("@POSICION_DESTINO", SqlDbType.VarChar, 45)
                PA.Value = Me.PosicionDestino
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@VALOR", SqlDbType.VarChar, 50)
                PA.Value = Trim(UCase(Me.txtBultoPallet.Text))
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@TIPO", SqlDbType.VarChar, 30)
                If Me.TipoValidacion = "BULTO" Then
                    PA.Value = "0"
                Else
                    PA.Value = "1"
                End If
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CONT", SqlDbType.BigInt)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()

                If Cmd.Parameters("@CONT").Value = 0 Then
                    If Me.TipoValidacion = "BULTO" Then
                        Retorno = False
                        MsgBox("El bulto " & Trim(UCase(Me.txtBultoPallet.Text)) & " no se encuentra en la ubicacion.", MsgBoxStyle.Information, FrmName)
                    Else
                        Retorno = False
                        MsgBox("El pallet " & Trim(UCase(Me.txtBultoPallet.Text)) & " no se encuentra en la ubicacion.", MsgBoxStyle.Information, FrmName)
                    End If
                Else
                    Retorno = True
                End If

            Else : MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return Retorno
        Catch sqlEx As SqlException
            MsgBox(sqlEx.Number & " - " & sqlEx.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            DA.Dispose()
            DS.Dispose()
            Cmd.Dispose()
        End Try
    End Function

    Private Sub txtBultoPallet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBultoPallet.TextChanged

    End Sub

End Class