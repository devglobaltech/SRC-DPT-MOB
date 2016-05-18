Option Explicit On

Imports System.Data.SqlClient
Imports System.Data

Public Class frmLockPosicion

    Private Posicion_ID As Long
    Private Posicion_Cod As String
    Private Lock As Boolean = False

    Private Const SQLConErr As String = "No se pudo conectar a la base de datos."
    Private Const FrmName As String = "Lockeo de Posiciones"

    Public ReadOnly Property vLock() As Boolean
        Get
            Return Lock
        End Get
    End Property

    Public Property PosicionCod() As String
        Get
            Return Posicion_Cod
        End Get
        Set(ByVal value As String)
            Posicion_Cod = value
        End Set
    End Property

    Public Property PosicionLCK() As Long
        Get
            Return Posicion_ID
        End Get
        Set(ByVal value As Long)
            Posicion_ID = value
        End Set
    End Property

    Private Sub frmLockPosicion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                If LockearPosicion() Then
                    MsgBox("La posicion se lockeo Correctamente", MsgBoxStyle.OkOnly, FrmName)
                    Lock = True
                    Me.Close()
                End If
            Case Keys.F2
                Cancelar()
            Case Keys.F3
                Salir()
        End Select
    End Sub

    Private Sub frmLockPosicion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetMotivos()
        Me.lblUbicacion.Text = "Ubicacion: " & Me.Posicion_Cod
    End Sub

    Private Function GetMotivos() As Boolean
        Dim Cmd As SqlClient.SqlCommand
        Dim Da As SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim Ds As New DataSet
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow
        Dim xStore As String = "SELECT * FROM MOTIVO_LOCKEO"
        Cmd = SQLc.CreateCommand
        Da = New SqlClient.SqlDataAdapter(Cmd)
        Try
            If VerifyConnection(SQLc) Then
                Cmd.CommandType = CommandType.Text
                Cmd.CommandText = xStore
                Da.Fill(Ds, "tabla")
                dt.Columns.Add("Descripcion", GetType(System.String))
                dt.Columns.Add("Motivo_id", GetType(System.String))
                For Each drDSRow In Ds.Tables("tabla").Rows()
                    drNewRow = dt.NewRow()
                    drNewRow("Descripcion") = drDSRow("Descripcion")
                    drNewRow("Motivo_id") = drDSRow("Motivo_id")
                    dt.Rows.Add(drNewRow)
                Next
                Me.cmbMotivos.DropDownStyle = ComboBoxStyle.DropDownList
                With cmbMotivos
                    .DataSource = dt
                    .DisplayMember = "Descripcion"
                    .ValueMember = "Motivo_id"
                    .SelectedIndex = 0
                End With
            Else : MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlClient.SqlException
            MsgBox("GetMotivos SQLEx" & SQLEx.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("GetMotivos" & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            dt = Nothing
            Cmd = Nothing
            Da = Nothing
            Ds = Nothing
        End Try
    End Function

    Private Sub cmdSalir_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Salir()
    End Sub

    Private Sub Salir()
        Me.Close()
    End Sub

    Private Sub txtLibre_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLibre.GotFocus
        Me.txtLibre.Text = ""
    End Sub

    Private Sub cmbMotivos_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbMotivos.KeyUp
        If e.KeyValue = 13 Then
            Me.txtLibre.Focus()
        End If
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If LockearPosicion() Then
            MsgBox("La posicion se lockeo Correctamente", MsgBoxStyle.OkOnly, FrmName)
            Lock = True
            Me.Close()
        End If
    End Sub

    Private Function LockearPosicion() As Boolean
        Dim Cmd As SqlClient.SqlCommand
        Dim xStore As String = "DBO.LOCK_POSITION"
        Dim Pa As SqlClient.SqlParameter
        Cmd = SQLc.CreateCommand

        Try
            If VerifyConnection(SQLc) Then
                Cmd.CommandText = xStore
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@Posicion_id", SqlDbType.BigInt)
                Pa.Value = Me.PosicionLCK
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@MOTIVO_ID", SqlDbType.VarChar, 5)
                Pa.Value = Me.cmbMotivos.SelectedValue
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 20)
                Pa.Value = Trim(UCase(vUsr.CodUsuario))
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OBS", SqlDbType.VarChar, 100)
                If Me.txtLibre.Text <> "Ingrese aqui mas informacion" Then
                    Pa.Value = Trim(UCase(Me.txtLibre.Text))
                Else
                    Pa.Value = DBNull.Value
                End If
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()

            Else : MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlClient.SqlException
            MsgBox("LockearPosicion SQLEx" & SQLEx.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("LockearPosicion" & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            Cmd = Nothing
        End Try

    End Function

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Cancelar()
    End Sub

    Private Sub Cancelar()
        Me.txtLibre.Text = "Ingrese aqui mas informacion"
        Me.cmbMotivos.SelectedIndex = 0
        Me.cmbMotivos.Focus()
    End Sub

    Private Sub txtLibre_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLibre.TextChanged

    End Sub

    Private Sub cmbMotivos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMotivos.SelectedIndexChanged

    End Sub
End Class