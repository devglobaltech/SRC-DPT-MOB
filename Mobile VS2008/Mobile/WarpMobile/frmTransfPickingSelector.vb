Option Explicit On

Public Class frmTransfPickingSelector

    Private blnCancelar As Boolean = False
    Private Seleccion As Integer = 0

    Public ReadOnly Property Cancelo() As Boolean
        Get
            Return Me.blnCancelar
        End Get
    End Property

    Public ReadOnly Property TSeleccion() As Integer
        Get
            Return Me.Seleccion
        End Get
    End Property

    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyValue = 13 Then
            If Me.ComboBox1.Text <> "" Then
                If Me.ComboBox1.Text = "Contenedor" Then
                    Seleccion = 1
                Else
                    Seleccion = 2
                End If
                Me.Close()
            End If
        End If
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub btn_aceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_aceptar.Click
        Aceptar()

    End Sub

    Private Sub Aceptar()
        If Me.ComboBox1.Text <> "" Then
            If Me.ComboBox1.Text = "Contenedor" Then
                Seleccion = 1
            Else
                Seleccion = 2
            End If
            Me.Close()
        End If
    End Sub

    Private Sub frmTransfPickingSelector_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyData
            Case Keys.F1
                Aceptar()
            Case Keys.F2
                Cancelar()
        End Select
    End Sub

    Private Sub Cancelar()
        Me.blnCancelar = True
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Cancelar()
    End Sub
End Class