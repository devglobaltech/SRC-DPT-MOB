Imports System.Data
Imports System.Data.SqlClient
Public Class frmCalleNave

    Private Const frmname As String = "Validacion Nave-Calle"
    Private Const StrConnErr As String = "Fallo al intentar conectar con la base de datos."
    Private TieneCalle As Boolean = False
    Private BlnCancelo As Boolean = False
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

    Private Sub frmCalleNave_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Aceptar()
            Case Keys.F2
                Cancelar()
            Case Keys.F3
                Salir()
        End Select
    End Sub

    Private Sub frmCalleNave_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtCalleNave.Text = ""
        Me.txtCalleNave.Focus()
        If Trim(vUsr.NaveCalle) <> "" Then
            TieneCalle = True
        Else
            TieneCalle = False
        End If
    End Sub

    Private Function VerificaNaveCalle(ByVal NaveCalle As String, ByRef Ret As Integer) As Boolean
        Dim xCMD As SqlCommand
        Dim PA As SqlParameter
        xCMD = SQLc.CreateCommand
        Try
            If VerifyConnection(SQLc) Then
                xCMD.CommandText = "DBO.VERIFICA_NAVECALLE"
                xCMD.CommandType = CommandType.StoredProcedure
                PA = New SqlParameter("@NAVECALLE", SqlDbType.VarChar, 50)
                PA.Value = NaveCalle
                xCMD.Parameters.Add(PA)
                PA = Nothing
                PA = New SqlParameter("@CONTROL", SqlDbType.SmallInt)
                PA.Direction = ParameterDirection.Output
                xCMD.Parameters.Add(PA)
                xCMD.ExecuteNonQuery()
                Ret = IIf(IsDBNull(xCMD.Parameters("@CONTROL").Value), 0, xCMD.Parameters("@CONTROL").Value)
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

    Private Sub Aceptar()
        Dim Ret As Integer
        If Trim(Me.txtCalleNave.Text) <> "" Then
            If VerificaNaveCalle(Me.txtCalleNave.Text, ret) Then
                If Ret = 0 Then
                    MsgBox("La Nave-Calle especificada no existe", MsgBoxStyle.OkOnly, frmname)
                    Me.txtCalleNave.Text = ""
                    Me.txtCalleNave.Focus()
                Else
                    BlnCambioOk = True
                    vUsr.NaveCalle = Me.txtCalleNave.Text
                    Me.Close()
                End If
            End If
        Else : Me.txtCalleNave.Focus()
            Me.txtCalleNave.Text = ""
        End If
    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Cancelar()
    End Sub

    Private Sub Cancelar()
        Me.txtCalleNave.Text = ""
        Me.txtCalleNave.Focus()
    End Sub

    Private Sub Salir()
        If Not TieneCalle Then
            If MsgBox("Si no selecciona un vehiculo no podra continuar, ¿desea salir?", MsgBoxStyle.YesNo, frmname) = MsgBoxResult.Yes Then
                BlnCancelo = True
                Me.Close()
            End If
        Else
            If MsgBox("Si no selecciona  otra Nave - Calle continuara con " & vUsr.NaveCalle & ", ¿desea salir?", MsgBoxStyle.YesNo, frmname) = MsgBoxResult.Yes Then
                Me.Close()
            End If
        End If
    End Sub

    Private Sub txtCalleNave_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCalleNave.KeyUp
        If e.KeyValue = 13 Then
            Aceptar()
        End If
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Salir()
    End Sub

    Private Sub txtCalleNave_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCalleNave.TextChanged

    End Sub
End Class