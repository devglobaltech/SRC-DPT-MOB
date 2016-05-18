Imports System.Data.SqlClient
Imports System.Data
Public Class FrmEditContenedoras

    Private Const FrmName As String = "Armado Pallet Final - Pendientes"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."

    Private OC As String
    Private xEsFraccionable As Boolean = False
    Private CodProducto As String
    Private DescProd As String
    Private Qty As Double
    Dim Fila As Integer
    Dim Col As Integer
    Private Cont As Integer
    Private Qty_Cont As Double
    Private Qty_Solicitada As Double
    Private UnidadDesc As String
    Private Table As DataSet
    Public Property TablaContenedoras() As DataSet
        Get
            Return Table
        End Get
        Set(ByVal value As DataSet)
            Table = value
        End Set
    End Property
    Public Property Row() As Integer
        Get
            Return Fila
        End Get
        Set(ByVal value As Integer)
            Fila = value
        End Set
    End Property
    Public Property Producto_ID() As String
        Get
            Return CodProducto
        End Get
        Set(ByVal value As String)
            CodProducto = value
        End Set
    End Property

    Public Property DescripcionProducto() As String
        Get
            Return DescProd
        End Get
        Set(ByVal value As String)
            DescProd = value
        End Set
    End Property

    Public Property Cantidad() As Double
        Get
            Return Qty
        End Get
        Set(ByVal value As Double)
            Qty = value
        End Set
    End Property
    Public Property CantidadSolicitada() As Double
        Get
            Return Qty_Solicitada
        End Get
        Set(ByVal value As Double)
            Qty_Solicitada = value
        End Set
    End Property

    Public Property Cant_Cont() As Double
        Get
            Return Qty_Cont
        End Get
        Set(ByVal value As Double)
            Qty_Cont = value
        End Set
    End Property

    Public Property vOC() As String
        Get
            Return OC
        End Get
        Set(ByVal value As String)
            OC = value
        End Set
    End Property
    Public Property Unidad() As String
        Get
            Return UnidadDesc
        End Get
        Set(ByVal value As String)
            UnidadDesc = value
        End Set
    End Property
    Public Property Contenedora() As Integer
        Get
            Return Cont
        End Get
        Set(ByVal value As Integer)
            Cont = value
        End Set
    End Property

    Private Sub FrmEditContenedoras_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                'finalizar
            Case Keys.F2
                Salir()
        End Select
    End Sub

    Private Sub FrmEditContenedoras_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblProductoId.Text = Producto_ID
        lblDescripcion.Text = DescripcionProducto
        lblUnidad.Text = Unidad
        lblContenedora.Text = Contenedora
        txtCantContenedora.Text = Cantidad

    End Sub
    Private Sub Salir()
        Try
            If MsgBox("Desea salir?" & vbNewLine & "La operacion en curso sera cancelada.", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub btnAtrasVolver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAtrasVolver.Click
        Salir()
    End Sub

    Private Sub txtCantContenedora_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantContenedora.KeyPress
        Dim Search As String
        Dim Pos As Integer
        Search = "."
        If Not xEsFraccionable Then
            ValidarCaracterNumerico(e)
        Else
            Pos = InStr(1, Me.txtCantContenedora.Text, Search)
            If Pos > 0 And Asc(e.KeyChar) <> 46 Then
                If Len(Mid(Me.txtCantContenedora.Text, Pos + 1, Len(Me.txtCantContenedora.Text))) >= 5 And Asc(e.KeyChar) <> 8 Then
                    e.Handled = True
                    Me.txtCantContenedora.Focus()
                End If
            Else
                If Pos <> 0 And (Asc(e.KeyChar) = 46) Then
                    e.Handled = True
                ElseIf Pos = 0 And (Asc(e.KeyChar) = 44) Then
                    e.Handled = True
                ElseIf Pos = 0 And (Asc(e.KeyChar) = 46) Then
                    e.Handled = False
                Else
                    ValidarCaracterNumerico(e)
                End If
            End If
        End If
    End Sub

    Private Sub txtCantContenedora_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantContenedora.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                asignarNuevaCantidad()
                Close()
            Case Keys.F2
                Salir()
        End Select
    End Sub
    Private Sub asignarNuevaCantidad()
        Dim NfrmOC As New FrmContenedorasOC
        TablaContenedoras.Tables(0).Rows(Row)(1) = txtCantContenedora.Text
        NfrmOC.dtgContenedoras.DataSource = TablaContenedoras.Tables(0)
    End Sub

    Private Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
        asignarNuevaCantidad()
        Close()
    End Sub
    Private Function GetFlgFraccionable(ByVal Cliente_id As String, ByVal Producto_id As String, ByRef FlgFraccionable As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        GetFlgFraccionable = False
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "GET_FLG_FRACCIONABLE"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Cliente_id", SqlDbType.VarChar, 15)
                Pa.Value = Cliente_id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Producto_id", SqlDbType.VarChar, 30)
                Pa.Value = Producto_id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Flg_Fraccionable", SqlDbType.VarChar, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                xCmd.ExecuteNonQuery()
                FlgFraccionable = (xCmd.Parameters("@Flg_Fraccionable").Value.ToString)
                xEsFraccionable = FlgFraccionable
                Return True
            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.Information, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function
End Class