Option Explicit On

Imports System.Data.SqlClient
Imports System.Data

Public Class frmEgresoConfirmacionPallet

#Region "VARIABLES"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private FrmName As String = "Confirmacion de Pallet."
    Private strPallet As String = ""
    Private strProducto_ID As String = ""
    Private strDescripcion As String = ""
    Private strViaje_ID As String = ""
    Private strCliente As String = ""
    Private strNro_Lote As String = ""
    Private strNro_Partida As String = ""
    Private blnConfirmo As Boolean = False
    Private dblCantidad As Double = 0
    Private strUBICACION As String = ""
#End Region

#Region "PROPIEDADES"

    Public Property CANTIDAD() As Double
        Get
            Return Me.dblCantidad
        End Get
        Set(ByVal value As Double)
            Me.dblCantidad = value
        End Set
    End Property

    Public ReadOnly Property CONFIRMO() As Boolean
        Get
            Return blnConfirmo
        End Get
    End Property

    Public Property PALLET() As String
        Get
            Return strPallet
        End Get
        Set(ByVal value As String)
            Me.strPallet = value
        End Set
    End Property

    Public Property PRODUCTO_ID() As String
        Get
            Return Me.strProducto_ID
        End Get
        Set(ByVal value As String)
            Me.strProducto_ID = value
        End Set
    End Property

    Public Property DESCRIPCION() As String
        Get
            Return Me.strDescripcion
        End Get
        Set(ByVal value As String)
            Me.strDescripcion = value
        End Set
    End Property

    Public Property VIAJE_ID() As String
        Get
            Return Me.strViaje_ID
        End Get
        Set(ByVal value As String)
            Me.strViaje_ID = value
        End Set
    End Property

    Public Property CLIENTE() As String
        Get
            Return strCliente
        End Get
        Set(ByVal value As String)
            strCliente = value
        End Set
    End Property

    Public Property NRO_LOTE() As String
        Get
            Return strNro_Lote
        End Get
        Set(ByVal value As String)
            strNro_Lote = value
        End Set
    End Property

    Public Property NRO_PARTIDA() As String
        Get
            Return strNro_Partida
        End Get
        Set(ByVal value As String)
            strNro_Partida = value
        End Set
    End Property

    Public Property UBICACION() As String
        Get
            Return Me.strUBICACION
        End Get
        Set(ByVal value As String)
            Me.strUBICACION = value
        End Set
    End Property

#End Region

#Region "FUNCIONES Y METODOS"

    Private Sub frmEgresoConfirmacionPallet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetearValoresEnPantalla()
        Application.DoEvents()
        Me.txtPallet.Text = ""
        Me.txtPallet.Focus()
    End Sub

    Private Sub SetearValoresEnPantalla()
        Try

            Me.txtCliente.Text = Me.CLIENTE
            Me.txtCliente.ReadOnly = True

            Me.txtProducto.Text = Me.PRODUCTO_ID
            Me.txtProducto.ReadOnly = True

            Me.lblDescripcion.Text = Me.DESCRIPCION

            If Me.NRO_LOTE.Trim <> "" Then
                Me.lblNro_Lote.Text = "Nro. Lote: " & Me.NRO_LOTE
            Else
                Me.lblNro_Lote.Text = ""
            End If

            If Me.NRO_PARTIDA.Trim <> "" Then
                Me.lblNroPartida.Text = "Nro. Partida: " & Me.NRO_PARTIDA.Trim
            Else
                Me.lblNroPartida.Text = ""
            End If

            If Me.UBICACION <> "" Then
                Me.LblUbicacion.Text = "Ubicacion: " & Me.UBICACION
            Else
                Me.LblUbicacion.Text = ""
            End If

            Me.lblNroPallet.Text = "Nro. Pallet: " & Me.PALLET

            Me.txtPallet.Text = ""
            Me.txtPallet.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSALIR.Click
        Salir()
    End Sub

    Private Sub Salir()
        If MsgBox("No podra continuar hasta que confirme el numero de pallet." & vbNewLine & "¿Desea cancelar de todas formas?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
            blnConfirmo = False
            Me.Close()
        Else
            Me.txtPallet.Text = ""
            Me.txtPallet.Focus()
        End If
    End Sub

    Private Sub txtPallet_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPallet.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtPallet.Text.Trim <> "" Then
                Me.VerificarPallet()
            Else
                Me.txtPallet.Text = ""
                Me.txtPallet.Focus()
            End If
        End If
    End Sub

    Private Sub VerificarPallet()
        Dim blnCambio As Boolean = False
        Try
            If Me.txtPallet.Text.Trim = Me.PALLET Then
                Me.blnConfirmo = True
                Me.Close()
            Else
                If MsgBox("El pallet escaneado no es el solicitado." & vbNewLine & "¿Desea realizar un cambio de pallet?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                    If VALIDAR_CAMBIO(blnCambio) Then
                        If blnCambio Then
                            If REALIZAR_CAMBIO() Then
                                'TENDRIA QUE RECUPERAR LOS DATOS DEL NUEVO PALLET.
                                'DESCUBRI Q SOLO CAMBIA EL PALLET.
                                strPallet = Me.txtPallet.Text.Trim
                                Me.Close()
                            End If
                        Else
                            MsgBox("El pallet de reemplazo no posee las mismas caracteristicas que el pallet asignado.", MsgBoxStyle.Information, FrmName)
                            Me.txtPallet.Text = ""
                            Me.txtPallet.Focus()
                        End If
                    Else
                        Me.txtPallet.Text = ""
                        Me.txtPallet.Focus()
                    End If
                    Me.blnConfirmo = True
                Else
                    Me.txtPallet.Text = ""
                    Me.txtPallet.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        End Try
    End Sub

    Private Function REALIZAR_CAMBIO() As Boolean
        Dim Cmd As SqlCommand, PA As SqlParameter, oTRANS As SqlTransaction, vError As String = ""
        Try
            ' [dbo].[MOB_CAMBIO_PALLET]()
            '@CLIENTE_ID    VARCHAR(15),
            '@VIAJE_ID		VARCHAR(100),
            '@PRODUCTO_ID	VARCHAR(30),
            '@CANTIDAD		NUMERIC(20,5),
            '@PALLET			VARCHAR(100),
            '@USR			VARCHAR(20),
            '@PALLET_DESTINO	VARCHAR(100)
            '@QTY   
            If VerifyConnection(SQLc) Then
                oTRANS = SQLc.BeginTransaction
                Cmd = SQLc.CreateCommand
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = "[dbo].[MOB_CAMBIO_PALLET]"
                Cmd.Transaction = oTRANS

                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.CLIENTE
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@VIAJE_ID", SqlDbType.VarChar, 100)
                PA.Value = Me.VIAJE_ID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                PA.Value = Me.PRODUCTO_ID
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CANTIDAD", SqlDbType.Float)
                PA.Value = Me.CANTIDAD
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PALLET", SqlDbType.VarChar, 100)
                PA.Value = Me.PALLET
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@USR", SqlDbType.VarChar, 20)
                PA.Value = vUsr.CodUsuario
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PALLET_DESTINO", SqlDbType.VarChar, 100)
                PA.Value = Me.txtPallet.Text.Trim
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@QTY_PALLET", SqlDbType.Float)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@UBICACION", SqlDbType.VarChar, 45)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()

                'recuperar la cantidad que devuelve el sp.
                Me.CANTIDAD = Cmd.Parameters("@QTY_PALLET").Value
                Me.UBICACION = Cmd.Parameters("@UBICACION").Value

                oTRANS.Commit()
            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SQLEX As SqlException
            vError = SQLEX.Number & "-" & SQLEX.Message
            oTRANS.Rollback()
            MsgBox(vError, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            vError = ex.Message
            oTRANS.Rollback()
            MsgBox(vError, MsgBoxStyle.Exclamation, FrmName)
        Finally
            oTRANS.Dispose()
            Cmd.Dispose()
        End Try
    End Function

    Private Function VALIDAR_CAMBIO(ByRef CAMBIO_OK As Boolean) As Boolean
        Dim Cmd As SqlCommand, PA As SqlParameter
        Dim Ret As String = ""
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = "DBO.MOB_CAMBIO_PALLET_VALIDACION"
                PA = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                PA.Value = Me.CLIENTE
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PALLET_ORIGEN", SqlDbType.VarChar, 100)
                PA.Value = Me.PALLET
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@PALLET_DESTINO", SqlDbType.VarChar, 100)
                PA.Value = Me.txtPallet.Text.Trim
                Cmd.Parameters.Add(PA)
                PA = Nothing

                PA = New SqlParameter("@CAMBIO_OK", SqlDbType.VarChar, 1)
                PA.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(PA)
                PA = Nothing

                Cmd.ExecuteNonQuery()

                Ret = Cmd.Parameters("@CAMBIO_OK").Value
                If Ret = "1" Then
                    CAMBIO_OK = True
                Else
                    CAMBIO_OK = False
                End If

            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SQLEX As SqlException
            MsgBox(SQLEX.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Cmd.Dispose()
        End Try
    End Function

#End Region

    Private Sub txtPallet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPallet.TextChanged

    End Sub
End Class