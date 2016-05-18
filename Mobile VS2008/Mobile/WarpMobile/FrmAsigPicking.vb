Imports System.Data.SqlClient
Imports System.Data

Public Class FrmAsigPicking

    Private Cliente As String
    Private CodProducto As String
    Private Qty As Double

    Public Property vCliente() As String
        Get
            Return Cliente
        End Get
        Set(ByVal value As String)
            Cliente = value
        End Set
    End Property

    Public ReadOnly Property Producto_ID() As String
        Get
            Return CodProducto
        End Get
    End Property

    Public ReadOnly Property Cantidad() As Double
        Get
            Return Qty
        End Get
    End Property



    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUbicacion.KeyUp
        If e.KeyValue = 13 Then
            If Me.txtUbicacion.Text <> "" Then
                If Not VerificaProductoPosicion(Me.txtUbicacion.Text) Then
                    MsgBox("La Nave/Posicion se encuentra inhabilitada para operaciones de transferencias", MsgBoxStyle.Information, FrmName)
                    Me.txtUbicacion.Text = ""
                    'Me.txtOrigen.Text = ""
                    Exit Sub

                End If

                'If Not VerificaNavePRE(Me.txtOrigen.Text) Then
                '    MsgBox("La Nave/Posicion se encuentra inhabilitada para operaciones de transferencias", MsgBoxStyle.Information, FrmName)
                '    Me.txtOrigen.Text = ""
                '    Exit Sub
                'End If
                'If VerificaPosLockeada(Me.txtOrigen.Text) Then
                '    Me.txtOrigen.Text = UCase(Me.txtOrigen.Text)
                '    Me.txtOrigen.ReadOnly = True
                '    Me.lblProducto.Visible = True
                '    Me.txtProducto.Visible = True
                '    lblDescripcion.Visible = True
                '    lblCantDisponible.Visible = True
                '    lblMensaje.Text = "Ingrese el Producto"
                '    Me.txtProducto.Focus()
                'Else
                '    Me.txtOrigen.Text = ""
                '    Me.txtOrigen.ReadOnly = False
                'End If
            End If
        End If
    End Sub

    Public Function VerificaProductoPosicion(ByVal ubicacion As String)
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Da = New SqlDataAdapter(Cmd)
                Cmd.Parameters.Clear()
                Cmd.CommandText = "VerificaExistenciaProductoPosicion"
                Cmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@producto", SqlDbType.VarChar, 30)
                Pa.Value = producto
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@ubicacion", SqlDbType.Int)
                Pa.Value = IIf(TipoPick = True, 1, 0)
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Cliente", SqlDbType.VarChar, 20)
                Pa.Value = IIf(Trim(vUsr.ClienteActivo) = "", DBNull.Value, Trim(UCase(vUsr.ClienteActivo)))
                Cmd.Parameters.Add(Pa)
                Da.Fill(Dst, Table)
                Return True
            Else
                MsgBox(SQLConErr, MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " Picking_Pendiente", MsgBoxStyle.OkOnly, ClsName)
            Return False
        Finally
            Pa = Nothing
            Da = Nothing
        End Try
    End Function


End Class