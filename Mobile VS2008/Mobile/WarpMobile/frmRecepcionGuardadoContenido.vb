Imports System.Data

Public Class frmRecepcionGuardadoContenido
    Dim Obj As clsRecepcionGuardado

    Private vPallet As String
    Private Const FrmName As String = "Recepción y Guardado."

    Public Property NroPallet() As String
        Get
            Return vPallet
        End Get
        Set(ByVal value As String)
            vPallet = value
        End Set
    End Property

    Public Property ObjRecepcion() As clsRecepcionGuardado
        Get
            Return Obj
        End Get
        Set(ByVal value As clsRecepcionGuardado)
            Obj = value
        End Set
    End Property

    Private Sub frmRecepcionGuardadoContenido_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Volver()
            Case Keys.F2
                Quitar()
        End Select
    End Sub

    Private Sub Quitar()
        Dim Id As String
        Try

            If MsgBox("¿Desea quitar el material seleccionado del pallet?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.No Then
                Exit Sub
            End If

            Id = Me.dgRes.Item(dgRes.CurrentRowIndex, 5).ToString()

            If Obj.QuitarContenido(Id) Then
                MsgBox("El material se retiro correctamente...", MsgBoxStyle.Information, FrmName)
                BuscarContenido()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub Volver()
        Me.Close()
    End Sub

    Private Sub frmRecepcionGuardadoContenido_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InicializarPantalla()
    End Sub

    Private Sub InicializarPantalla()
        Try
            Me.lblNroContenedora.Text = "Contenido del Pallet: " & Me.vPallet
            BuscarContenido()
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub BuscarContenido()
        Dim Data As New DataSet
        Try
            If Obj.ContenidoPallet(Me.vPallet, Data) Then

                If Data.Tables.Count > 0 Then

                    If Data.Tables(0).Rows.Count > 0 Then

                        Me.dgRes.DataSource = Data.Tables(0)
                        AutoSizeGrid(Me.dgRes, FrmName)
                        Me.dgRes.Focus()
                    Else

                        MsgBox("No se encontraron datos.", MsgBoxStyle.Information, FrmName)
                        Me.Close()
                    End If
                Else

                    MsgBox("No se encontraron datos.", MsgBoxStyle.Information, FrmName)
                    Me.Close()
                End If
            Else

                MsgBox("No se encontraron datos.", MsgBoxStyle.Information, FrmName)
                Me.Close()
            End If
        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            Data = Nothing
        End Try
    End Sub

    Private Sub dgRes_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgRes.CurrentCellChanged
        Try
            dgRes.Select(dgRes.CurrentRowIndex)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub dgRes_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgRes.GotFocus
        Try
            dgRes.Select(dgRes.CurrentRowIndex)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub dgRes_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgRes.KeyPress
        Try
            dgRes.Select(dgRes.CurrentRowIndex)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub BtnQuitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnQuitar.Click
        Quitar()
    End Sub

    Private Sub btnVolver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVolver.Click
        Volver()
    End Sub
End Class