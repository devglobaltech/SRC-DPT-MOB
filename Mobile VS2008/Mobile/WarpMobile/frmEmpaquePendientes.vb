Public Class frmEmpaquePendientes

    Dim oEMP As clsEmpaque
    Private Const frmName As String = "Empaque"

    Public Property oEMPAQUE() As clsEmpaque
        Get
            Return oEMP
        End Get
        Set(ByVal value As clsEmpaque)
            oEMP = value
        End Set
    End Property

    Private Sub Salir()
        Me.Close()
    End Sub

    Private Sub frmEmpaquePendientes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyData
            Case Keys.F1
                Salir()
        End Select
    End Sub

    Private Sub frmEmpaquePendientes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ObtenerDatos()
    End Sub

    Private Sub ObtenerDatos()
        If oEMP.GetPendientesDeEmpaquetar(Me.dg) Then
            AutoSizeGrid(Me.dg, frmname)
        End If
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Salir()
    End Sub

End Class