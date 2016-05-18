Option Explicit On
Imports System.Data
Imports System.Data.SqlClient

Public Class frmVehiculo
    Private Const FrmName As String = "Seleccion de vehículos"
    Private Const StrConnErr As String = "Fallo al intentar conectar con la base de datos."
    Private BlnCancelo As Boolean = False
    Private TieneVehiculo As Boolean = False
    Private BlnCambioOk As Boolean = False

    Public ReadOnly Property CambioOk() As Boolean
        Get
            Return BlnCambioOk
        End Get
    End Property


    Public ReadOnly Property Cancelo() As Boolean
        Get
            Return BlnCancelo
        End Get
    End Property

    Private Sub frmVehiculo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Aceptar()
            Case Keys.F2
                Cancelar()
            Case Keys.F3
                Salir()
        End Select
    End Sub

    Private Sub Aceptar()
        Dim Valido As Integer
        If Trim(Me.txtVehiculo.Text) <> "" Then
            If Not ValidarVehiculo(Me.txtVehiculo.Text, Valido) Then
                MsgBox("No se pudo validar el vehiculo.", MsgBoxStyle.Critical, FrmName)
            Else
                If Valido = 0 Then
                    MsgBox("El Vehiculo seleccionado no es valido.", MsgBoxStyle.OkOnly, FrmName)
                    Me.txtVehiculo.Text = ""
                    Me.txtVehiculo.Focus()
                Else
                    If TomarVehiculo(Me.txtVehiculo.Text) Then
                        BlnCambioOk = True
                        vUsr.Vehiculo = Trim(UCase(Me.txtVehiculo.Text))
                        Me.Close()
                    Else : MsgBox("Ocurrio un error al reservar el Vehiculo.", MsgBoxStyle.Information, FrmName)
                    End If
                End If
            End If
        Else
            Me.txtVehiculo.Focus()
        End If
    End Sub

    Private Sub Cancelar()
        Me.txtVehiculo.Text = ""
        Me.txtVehiculo.Focus()
    End Sub

    Private Sub Salir()
        If Not TieneVehiculo Then
            If MsgBox("Si no selecciona un vehiculo no podra continuar, ¿desea salir?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                BlnCancelo = True
                Me.Close()
            End If
        Else
            If MsgBox("Si selecciona no otro vehiculo continuara con el vehiculo " & vUsr.Vehiculo & ", ¿desea salir?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                Me.Close()
            End If
        End If
    End Sub

    Private Sub frmVehiculo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtVehiculo.Text = ""
        Me.txtVehiculo.Focus()
        If Trim(vUsr.Vehiculo) <> "" Then
            TieneVehiculo = True
        End If
    End Sub

    Private Function ValidarVehiculo(ByVal VH As String, ByRef Ret As Integer) As Boolean
        Dim xCMD As SqlCommand
        Dim PA As SqlParameter
        xCMD = SQLc.CreateCommand
        Try
            If VerifyConnection(SQLc) Then
                xCMD.CommandText = "DBO.VALIDAR_VEHICULO"
                xCMD.CommandType = CommandType.StoredProcedure
                PA = New SqlParameter("@Vehiculo_id", SqlDbType.VarChar, 50)
                PA.Value = VH
                xCMD.Parameters.Add(PA)
                PA = Nothing
                PA = New SqlParameter("@Valido", SqlDbType.SmallInt)
                PA.Direction = ParameterDirection.Output
                xCMD.Parameters.Add(PA)
                xCMD.ExecuteNonQuery()
                Ret = IIf(IsDBNull(xCMD.Parameters("@Valido").Value), 0, xCMD.Parameters("@Valido").Value)
            Else
                MsgBox(StrConnErr, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SqlEx As SqlException
            MsgBox(SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            PA = Nothing
            xCMD = Nothing
        End Try
    End Function

    Private Sub cmdAcepter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAcepter.Click
        Aceptar()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Cancelar()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Salir()
    End Sub

    Private Sub txtVehiculo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtVehiculo.KeyUp
        If e.KeyValue = 13 Then
            Aceptar()
        End If
    End Sub
    Private Function TomarVehiculo(ByVal VH As String) As Boolean
        Dim xCMD As SqlCommand
        Dim PA As SqlParameter
        xCMD = SQLc.CreateCommand
        Try
            If VerifyConnection(SQLc) Then
                xCMD.CommandText = "DBO.TOMA_VH"
                xCMD.CommandType = CommandType.StoredProcedure
                PA = New SqlParameter("@Vehiculo_id", SqlDbType.VarChar, 50)
                PA.Value = VH
                xCMD.Parameters.Add(PA)
                xCMD.ExecuteNonQuery()
            Else
                MsgBox(StrConnErr, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch SqlEx As SqlException
            MsgBox(SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            PA = Nothing
            xCMD = Nothing
        End Try
    End Function

End Class