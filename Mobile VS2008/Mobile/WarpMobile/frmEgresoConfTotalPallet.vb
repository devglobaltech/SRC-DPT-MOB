Public Class frmEgresoConfTotalPallet

    Private vOla As String = ""
    Private vCliente As String = ""
    Private vProducto As String = ""
    Private vDescripcion As String = ""
    Private vUbicacion As String = ""
    Private vNroPallet As String = ""
    Private FrmName As String = "Confirmación"

    Public Property OlaPicking() As String
        Get
            Return vOla
        End Get
        Set(ByVal value As String)
            vOla = value
        End Set
    End Property

    Public Property Producto() As String
        Get
            Return vProducto
        End Get
        Set(ByVal value As String)
            vProducto = value
        End Set
    End Property

    Public Property Cliente() As String
        Get
            Return vCliente
        End Get
        Set(ByVal value As String)
            vCliente = value
        End Set
    End Property

    Public Property DescripcionProducto() As String
        Get
            Return vDescripcion
        End Get
        Set(ByVal value As String)
            vDescripcion = value
        End Set
    End Property

    Public Property UbicacionPallet() As String
        Get
            Return vUbicacion
        End Get
        Set(ByVal value As String)
            vUbicacion = value
        End Set
    End Property

    Public Property NumeroPallet() As String
        Get
            Return vNroPallet
        End Get
        Set(ByVal value As String)
            vNroPallet = value
        End Set
    End Property

    Private Sub frmEgresoConfTotalPallet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Inicializar()
    End Sub

    Private Sub Inicializar()
        Me.txtOla.Text = vOla
        Me.txtOla.Enabled = False

        Me.txtProducto.Text = vProducto & " / " & vDescripcion
        Me.txtProducto.Enabled = False

        Me.txtUbicacion.Text = Me.vUbicacion
        Me.txtUbicacion.Enabled = False

        Me.lblConfPallet.Text = "Confirme el pallet Nro: " & vNroPallet

        Me.txtPallet.Enabled = True
        Me.txtPallet.Text = ""
        Me.txtPallet.Focus()

    End Sub

    Private Sub txtPallet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPallet.TextChanged
        Confirmacion()
    End Sub

    Private Sub Confirmacion()
        Try
            If Trim(Me.txtPallet.Text) <> Me.NumeroPallet Then
                MsgBox("El pallet escaneado no es el correcto.", MsgBoxStyle.OkOnly, FrmName)
                Me.txtPallet.Text = ""
                Me.txtPallet.Focus()
                Return
            Else
                MsgBox("El Pallet es correcto", MsgBoxStyle.OkOnly, FrmName)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

End Class