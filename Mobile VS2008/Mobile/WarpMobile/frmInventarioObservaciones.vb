Public Class frmInventarioObservaciones

    Private tInventario As String = ""
    Private rObservaciones As String = ""

    Public Property DataInventario() As String
        Get
            Return Me.tInventario
        End Get
        Set(ByVal value As String)
            Me.tInventario = value
        End Set
    End Property

    Public ReadOnly Property Observaciones() As String
        Get
            Dim vtext = Me.txtObservaciones.Text
            vtext = Replace(vtext, "'", "")
            Return vtext
        End Get
    End Property

    Private Sub frmInventarioObservaciones_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Me.Close()
            Case Keys.F2
                Me.Close()
        End Select
    End Sub

    Private Sub frmInventarioObservaciones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.LblInventario.Text = DataInventario
        Me.txtObservaciones.Text = ""
        Me.txtObservaciones.Focus()
    End Sub

    Public Sub IniForm()
        Me.txtObservaciones.Text = ""
        Me.DataInventario = ""
    End Sub

    Private Sub cmdComenzar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdComenzar.Click
        Me.Close()
    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Me.Close()
    End Sub

End Class