Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class frmContenedorasPicking

#Region "Declaracion de Variables"
    Private Const FrmName As String = "Configuración de Contenedoras"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private Const ErrCon As String = "No se pudo conectar con la base de datos."
    Private CodProducto As String
    Private DescProd As String
    Private Qty_Solcitada As Double
    Private PickingId As Long
    Private NewRL_Id As Long
    Private Qty_Pickeada As Double
    Private Qty_SumCant As Double
    Private UnidadDesc As String
    Private ViajeId As String
    Private Inicial As Boolean = False
    Public Nfrm As New FrmEditContenedoras
    Private pUbicacion As String = ""
    Private pContenedora As String = ""
    Private pCantidadMaxCont As Long
    Private pRespetaLotePartida As Integer = 0
    Private blnCambio As Boolean = False
#End Region

#Region "Property's"

    Public ReadOnly Property RealizoCambio() As Boolean
        Get
            Return Me.blnCambio
        End Get
    End Property

    Public Property RespetaLotePartida() As Integer
        Get
            Return pRespetaLotePartida
        End Get
        Set(ByVal value As Integer)
            Me.pRespetaLotePartida = value
        End Set
    End Property

    Public Property RL_Id() As Long
        Get
            Return NewRL_Id
        End Get
        Set(ByVal value As Long)
            NewRL_Id = value
        End Set
    End Property

    Public Property Picking_ID() As Long
        Get
            Return PickingId
        End Get
        Set(ByVal value As Long)
            PickingId = value
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

    Public Property CantidadSolicitada() As Double
        Get
            Return Qty_Solcitada
        End Get
        Set(ByVal value As Double)
            Qty_Solcitada = value
        End Set
    End Property

    Public Property CantidadPickeada() As Double
        Get
            Return Qty_Pickeada
        End Get
        Set(ByVal value As Double)
            Qty_Pickeada = value
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

    Public Property ContenedoraUbicacion() As String
        Get
            Return pUbicacion
        End Get
        Set(ByVal value As String)
            pUbicacion = value
        End Set
    End Property

    Public Property CantidadMaxCont() As Long
        Get
            Return pCantidadMaxCont
        End Get
        Set(ByVal value As Long)
            pCantidadMaxCont = value
        End Set
    End Property

    Public Property Viaje_ID() As String
        Get
            Return pContenedora
        End Get
        Set(ByVal value As String)
            pContenedora = value
        End Set
    End Property

    Public Property Nro_Contenedora() As String
        Get
            Return ViajeId
        End Get
        Set(ByVal value As String)
            ViajeId = value
        End Set
    End Property

#End Region

    Private Sub frmContenedorasPicking_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If GetProductosEgresos() And GetValoresPicking() Then
            If dtgUbicContenedoras.VisibleRowCount > 0 Then
                Me.lblProductoId.Text = "Producto: " & Producto_ID
                Me.lblDescripcion.Text = "Descripción: " & DescripcionProducto
                Me.lblCantidadSolicitada.Text = CantidadSolicitada
                Me.lblCantidadRestante.Text = CLng(Me.lblCantidadSolicitada.Text) - CLng(lblCantidadPickeada.Text)
                CantidadMaxCont = CantidadSolicitada
                Me.lblUnidad.Text = Unidad
            End If
        End If
    End Sub

    Private Function GetProductosEgresos() As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = "Estacion_GetProductos_Egr"

                Pa = New SqlParameter("@Usr", SqlDbType.VarChar, 20)
                Pa.Value = vUsr.CodUsuario
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Rpar", SqlDbType.VarChar, 1)
                Pa.Value = Me.RespetaLotePartida
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Tipo", SqlDbType.Float, 20)
                Pa.Value = 1
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Da.Fill(Ds, "ProductosEgrCont")

                dtgUbicContenedoras.DataSource = Ds.Tables("ProductosEgrCont")
                If (Ds.Tables(0).Rows.Count > 0) Then
                    ResizeGrillaContenedoras(Ds.Tables("ProductosEgrCont"))
                    If dtgUbicContenedoras.VisibleRowCount > 0 Then
                        dtgUbicContenedoras.Focus()
                    End If
                Else
                    MsgBox("No se encontraron contenedoras disponibles", MsgBoxStyle.Information, FrmName)
                    Me.Close()
                End If
            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            Pa = Nothing
            Ds = Nothing
            xCmd = Nothing
            Da = Nothing
        End Try
    End Function
    Private Function GetValoresPicking() As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = "Frontera_PickingProceso_Cont"

                Pa = New SqlParameter("@Usr", SqlDbType.VarChar, 20)
                Pa.Value = vUsr.CodUsuario
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Da.Fill(Ds, "ValoresPicking")
                Try
                    Me.lblCantidadPickeada.Text = Ds.Tables("ValoresPicking").Rows(0)(3).ToString
                    Me.lblContenedora.Text = "Contenedora: " & Ds.Tables("ValoresPicking").Rows(0)(8).ToString
                Catch ex As Exception
                End Try
            Else : MsgBox(SQLError, MsgBoxStyle.Critical, FrmName)
                Return False
            End If
            Return True
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            Pa = Nothing
            Ds = Nothing
            xCmd = Nothing
            Da = Nothing
        End Try
    End Function

    Private Function ResizeGrillaContenedoras(ByVal tb As DataTable) As Boolean
        Try
            Dim Style As New DataGridTableStyle()
            Style.MappingName = tb.TableName
            dtgUbicContenedoras.TableStyles.Clear()

            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "POSICION"
            TextCol1.HeaderText = "Ubicación"
            TextCol1.Width = 70
            Style.GridColumnStyles.Add(TextCol1)
            TextCol1 = Nothing

            Dim TextCol2 As New DataGridTextBoxColumn
            TextCol2.MappingName = "NRO_BULTO"
            TextCol2.HeaderText = "Contenedora"
            TextCol2.Width = 80
            Style.GridColumnStyles.Add(TextCol2)
            TextCol2 = Nothing

            Dim TextCol4 As New DataGridTextBoxColumn
            TextCol4.MappingName = "CANTIDAD"
            TextCol4.HeaderText = "Stk. No Reservado"
            TextCol4.Width = 100
            Style.GridColumnStyles.Add(TextCol4)
            TextCol4 = Nothing

            Dim TextCol3 As New DataGridTextBoxColumn
            TextCol3.MappingName = "RL_ID"
            TextCol3.HeaderText = "RL_ID"
            TextCol3.Width = 0
            Style.GridColumnStyles.Add(TextCol3)
            TextCol3 = Nothing
            dtgUbicContenedoras.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("ResizeGrillaContenedoras: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Function

    Private Sub btnVolver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVolver.Click
        Me.Close()
    End Sub

    Private Sub dtgUbicContenedoras_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtgUbicContenedoras.KeyUp
        Try
            dtgUbicContenedoras.Select(dtgUbicContenedoras.CurrentRowIndex)
        Catch ex As Exception
        End Try
        Select Case e.KeyCode
            Case Keys.F1
                Me.CambiarUbicacion()
            Case Keys.F2
                Me.Close()
            Case Keys.Enter
                Me.CambiarUbicacion()
        End Select
    End Sub

    Private Sub dtgUbicContenedoras_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgUbicContenedoras.GotFocus
        Try
            If Me.dtgUbicContenedoras.CurrentRowIndex >= 0 Then
                Me.dtgUbicContenedoras.Select(Me.dtgUbicContenedoras.CurrentRowIndex)
            End If
        Catch ex As Exception
            MsgBox("dtgUbicContenedoras_GotFocus: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub
    Private Sub CambiarUbicacion()
        Try
            Dim QtyRL As Double = 0

            QtyRL = CDbl(Me.dtgUbicContenedoras.Item(dtgUbicContenedoras.CurrentRowIndex, 2).ToString())
            If (Me.Qty_Solcitada <= QtyRL) Then
                Me.blnCambio = True
                ContenedoraUbicacion = (Me.dtgUbicContenedoras.Item(dtgUbicContenedoras.CurrentRowIndex, 0).ToString())
                CantidadMaxCont = CLng(Me.dtgUbicContenedoras.Item(dtgUbicContenedoras.CurrentRowIndex, 2).ToString())
                RL_Id = CLng(Me.dtgUbicContenedoras.Item(dtgUbicContenedoras.CurrentRowIndex, 3).ToString())
                Nro_Contenedora = Me.dtgUbicContenedoras.Item(dtgUbicContenedoras.CurrentRowIndex, 1).ToString()
                Me.Close()
            Else
                MsgBox("No es posible realizar el cambio por una contenedora con menor cantidad Disponible", MsgBoxStyle.Information, Me.FrmName)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub dtgUbicContenedoras_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgUbicContenedoras.CurrentCellChanged
        Try
            If Me.dtgUbicContenedoras.CurrentRowIndex >= 0 Then
                Me.dtgUbicContenedoras.Select(Me.dtgUbicContenedoras.CurrentRowIndex)
            End If
        Catch ex As Exception
            MsgBox("dtgUbicContenedoras_GotFocus: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub btn_cambio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cambio.Click
        Me.CambiarUbicacion()
    End Sub
End Class