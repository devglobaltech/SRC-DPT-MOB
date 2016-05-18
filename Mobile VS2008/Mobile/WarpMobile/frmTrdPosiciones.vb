Imports System.Data.SqlClient
Imports System.Data

Public Class frmTrdPosiciones

#Region "Declaraciones"
    Private blnPermisoGrid As Boolean = True
    Private blnPanelGrid As Boolean = True
    Private strProducto As String = ""
    Private strDescripcion As String = ""
    Private Const SqlConnErr As String = "No se pudo conectar a la Base de Datos."
    Private FrmName As String = "Seleccione Posiciones."
    Private Const xTable As String = "CONSULTA"
    Private xDS As DataSet
    Private xDestino As String = ""
    Private blnSalir As Boolean = False
    Private xQTY As Double
    Private PalletD As String = ""
    Private U_Original As String = ""
    Private Const StrConnErr As String = "Fallo al intentar conectar con la base de datos."
    Private blnOtherPos As Boolean = False


    Public Property QTY() As Double
        Get
            Return xQTY
        End Get
        Set(ByVal value As Double)
            xQTY = value
        End Set
    End Property
    Public Property Producto() As String
        Get
            Return strProducto
        End Get
        Set(ByVal value As String)
            strProducto = value
        End Set
    End Property
    Public Property Descripcion() As String
        Get
            Return strDescripcion
        End Get
        Set(ByVal value As String)
            strDescripcion = value
        End Set
    End Property
    Public Property Pallet() As String
        Get
            Return PalletD
        End Get
        Set(ByVal value As String)
            PalletD = value
        End Set
    End Property
    Public Property UbicacionOriginal() As String
        Get
            Return U_Original
        End Get
        Set(ByVal value As String)
            U_Original = value
        End Set
    End Property
    Public ReadOnly Property Destino() As String
        Get
            Return xDestino
        End Get
    End Property
    Public ReadOnly Property Salir() As Boolean
        Get
            Return blnSalir
        End Get
    End Property


#End Region

#Region "Acceso a Datos"

    Private Function GetPosiciones(ByVal Producto As String, ByRef Ds As DataSet, ByVal Table As String, ByVal Pallet As String, ByVal UbicacionOriginal As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Da As SqlDataAdapter
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "Trd_GetProductosByProd"
                Cmd.Connection = SQLc
                Cmd.CommandType = CommandType.StoredProcedure

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Value = Producto
                Cmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@Pallet", SqlDbType.VarChar, 100)
                Pa.Value = Pallet
                Cmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@U_Orig", SqlDbType.VarChar, 45)
                Pa.Value = UbicacionOriginal
                Cmd.Parameters.Add(Pa)

                Da.Fill(Ds, "CONSULTA")
            Else : MsgBox(SqlConnErr, MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("Trd_GetProductosByProd SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly)
            Return False
        Catch ex As Exception
            MsgBox("Trd_GetProductosByProd: " & ex.Message, MsgBoxStyle.OkOnly)
            Return False
        Finally
            Cmd = Nothing
            Da = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function GetCantPallets(ByVal Posicion As String, ByVal Prod As String, ByRef Elementos As Integer) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "TRD_GET_PALLETS_BY_POS"
                Cmd.CommandType = CommandType.StoredProcedure

                Cmd.Parameters.Add("@posicion", SqlDbType.VarChar, 45).Value = Posicion
                Cmd.Parameters.Add("@Producto", SqlDbType.VarChar, 30).Value = prod
                Da.Fill(xDS, "PALLETS")

                Elementos = xDS.Tables("PALLETS").Rows.Count

            Else : MsgBox(SqlConnErr, MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("GetCantPallet SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Me.txtDestino.Text = ""
            Return False
        Catch ex As Exception
            MsgBox("GetCantPallet: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
            Da = Nothing
        End Try
    End Function

#End Region

#Region "Procedimientos / Funciones"

    Private Sub InicializarForm()
        Dim intVar As Integer = 0
        Me.lblMsg.Text = ""
        Me.lblProd.Text = "Producto Id: " & Producto
        Me.lblDesc.Text = "Descripcion: " & Descripcion
        Me.PanelConfirmacion.Visible = False
        Me.Panelpallet.Visible = False
        Me.PanelPosicion.Visible = False
        If Me.GetPosiciones(Producto, xDS, xTable, Pallet, UbicacionOriginal) Then
            intVar = xDS.Tables(xTable).Rows.Count
            If intVar > 0 Then
                Me.PanelGrid.Visible = True
                Me.PanelPosicion.Visible = False
                Me.dgPosiciones.DataSource = xDS.Tables("CONSULTA")
                ResizeGrid()
                Me.dgPosiciones.Focus()
            ElseIf intVar = 0 Then
                'Aca es donde le pido al usuario una posicion Destino.
                blnPermisoGrid = False
                Me.PanelPosicion.Visible = True
                Me.PanelGrid.Visible = False
                Me.txtDestino.Focus()
            End If
        Else
            MsgBox("Fallo al obtener las posiciones")
        End If
    End Sub

    Private Function ResizeGrid() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "CONSULTA"
            Me.dgPosiciones.TableStyles.Clear()
            Style.MappingName = "CONSULTA"

            Dim TextCol1 As New DataGridTextBoxColumn
            With TextCol1
                .MappingName = "POSICION_COD"
                .HeaderText = "Posicion."
                .Width = 145
            End With
            Style.GridColumnStyles.Add(TextCol1)

            Dim TextCol9 As New DataGridTextBoxColumn
            With TextCol9
                .MappingName = "PALLET"
                .HeaderText = "Nro. Pallet"
                .Width = 70
            End With
            Style.GridColumnStyles.Add(TextCol9)

            Dim TextCol10 As New DataGridTextBoxColumn
            With TextCol10
                .MappingName = "QTY"
                .HeaderText = "Cantidad"
                .Width = 60
            End With
            Style.GridColumnStyles.Add(TextCol10)

            Dim TextCol11 As New DataGridTextBoxColumn
            With TextCol11
                .MappingName = "NRO_LOTE"
                .HeaderText = "Nro. Lote"
                .Width = 60
            End With
            Style.GridColumnStyles.Add(TextCol11)

            Me.dgPosiciones.TableStyles.Add(Style)

            Return True
        Catch ex As Exception
            MsgBox("ResizeGrid: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        End Try
    End Function

    Private Sub Confirmar()
        If Me.cmdConfirmar.Enabled Then
            If Me.PanelGrid.Visible Then
                xDestino = Me.dgPosiciones.Item(Me.dgPosiciones.CurrentRowIndex, 0)
                PalletD = Me.dgPosiciones.Item(Me.dgPosiciones.CurrentRowIndex, 1)
                Me.PanelGrid.Visible = False
                Application.DoEvents()
                Me.PanelConfirmacion.Visible = True
                Me.lblDestino.Text = "Destino: " & xDestino
                Me.txtConfDestino.Focus()
                Me.cmdConfirmar.Enabled = False
                Me.cmdOtherPos.Enabled = False
            Else
                xDestino = Me.txtDestino.Text
                Me.Close()
            End If
        End If
    End Sub

    Private Sub xSalir()
        If Not Me.PanelConfirmacion.Visible Then
            blnSalir = True
            Me.Close()
        Else
            Me.cmdConfirmar.Enabled = True
            Me.cmdOtherPos.Enabled = True
            Me.txtConfDestino.Text = ""
            Me.PanelConfirmacion.Visible = False
            Me.PanelGrid.Visible = True
            Me.dgPosiciones.Focus()
        End If
    End Sub

    Private Sub OtherPosition()
        If Me.cmdOtherPos.Enabled Then
            If Me.blnPanelGrid Then
                blnOtherPos = True
                Me.PanelGrid.Visible = False
                Me.PanelPosicion.Visible = True
                blnPanelGrid = False
                Me.cmdConfirmar.Enabled = False
                Me.txtConfDestino.Text = ""
                Me.txtDestino.Text = ""
                Me.txtDestino.Focus()
            Else
                If Me.blnPermisoGrid Then
                    If Me.Panelpallet.Visible Then
                        Me.txtPallet.Text = ""
                        Me.Panelpallet.Visible = False
                    End If
                    Application.DoEvents()
                    blnOtherPos = False
                    Me.PanelGrid.Visible = True
                    Me.PanelPosicion.Visible = False
                    Me.cmdConfirmar.Enabled = True
                    Me.dgPosiciones.Focus()
                    Me.blnPanelGrid = True
                    Me.txtConfDestino.Text = ""
                    Me.txtDestino.Text = ""
                Else
                    MsgBox("No hay existencias del articulo en el Warehouse", MsgBoxStyle.OkOnly, FrmName)
                    Me.txtDestino.Focus()
                End If
            End If
        End If
    End Sub

    Private Function VerificaPallet(ByVal NroPallet As String, ByRef Verifica As Integer) As Boolean
        Try
            Dim i As Integer = 0
            Verifica = 0
            For i = 0 To xDS.Tables("Pallets").Rows.Count - 1
                If UCase(Trim(xDS.Tables("Pallets").Rows(i)(0))) = UCase(Trim(NroPallet)) Then
                    Verifica = 1
                    Exit For
                End If
            Next i
            Return True
        Catch ex As Exception
            MsgBox("VerificaPallet: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        End Try
    End Function

#End Region

#Region "Contructor / Destructor"

    Public Sub New()
        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()
        xDS = New DataSet
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        xDS = Nothing
    End Sub

#End Region

    Private Sub frmTrdPosiciones_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Confirmar()
            Case Keys.F2
                OtherPosition()
            Case Keys.F3
                xSalir()
        End Select
    End Sub

    Private Sub frmTrdPosiciones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InicializarForm()
    End Sub

    Private Sub cmdConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfirmar.Click
        Confirmar()
    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        xSalir()
    End Sub

    Private Sub txtDestino_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDestino.KeyPress

    End Sub

    Private Sub txtDestino_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDestino.KeyUp
        Dim fTM As frmTransferenciaManual
        If e.KeyCode = Keys.Enter Then
            If (Me.txtDestino.Text <> "") Then
                Dim Pallets As Integer
                If Me.GetCantPallets(Me.txtDestino.Text, strProducto, Pallets) Then
                    fTM = frmTransferenciaManual
                    If Not fTM.VerificaNavePRE(Me.txtDestino.Text) Then
                        MsgBox("La Nave/Posicion se encuentra inhabilitada para operaciones de transferencias", MsgBoxStyle.Information, FrmName)
                        Me.txtDestino.Text = ""
                        Exit Sub
                    End If
                    fTM = Nothing
                    If (Pallets > 1) And (blnOtherPos) Then
                        Me.PanelPosicion.Visible = False
                        Me.Panelpallet.Visible = True
                        Me.txtPallet.Text = ""
                        Me.txtPallet.Focus()
                        xDestino = Me.txtDestino.Text
                    Else
                        If (Pallets = 1) And (blnOtherPos = True) Then
                            PalletD = "" 'LIMPIO EL PALLET
                            GetPalletByPos(Me.txtDestino.Text, strProducto, PalletD) 'RECUPERO EL PALLET DE LA POSICION
                        ElseIf (Pallets = 0) And (blnOtherPos = True) Then
                            PalletD = ""
                        End If
                        xDestino = Me.txtDestino.Text
                        Me.Close()
                    End If
                End If
            End If
        End If
    End Sub

    Private Function GetPalletByPos(ByVal Destino As String, ByVal Producto As String, ByRef Pallet As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "TRD_GET_PALLETS_BY_POS"
                Cmd.CommandType = CommandType.StoredProcedure

                Cmd.Parameters.Add("@posicion", SqlDbType.VarChar, 45).Value = Destino
                Cmd.Parameters.Add("@Producto", SqlDbType.VarChar, 30).Value = Producto
                Da.Fill(xDS, "PALLETS")

                Pallet = xDS.Tables("PALLETS").Rows(0)(0).ToString

            Else : MsgBox(SqlConnErr, MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("GetCantPallet SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Me.txtDestino.Text = ""
            Return False
        Catch ex As Exception
            MsgBox("GetCantPallet: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
            Da = Nothing
        End Try
    End Function

    Private Sub txtConfDestino_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtConfDestino.KeyUp
        Try
            If e.KeyValue = 13 Then
                If Me.txtConfDestino.Text <> "" Then
                    Me.lblMsg.Text = ""
                    If Trim(UCase(Me.txtConfDestino.Text)) = Trim(UCase(xDestino)) Then
                        Dim Pallets As Integer
                        If Me.GetCantPallets(xDestino, strProducto, Pallets) Then
                            'MsgBox("Pallets: " & Pallets)
                            If (Pallets > 1) And (blnOtherPos) Then
                                Me.PanelConfirmacion.Visible = False
                                Me.Panelpallet.Visible = True
                                Me.txtPallet.Text = ""
                                Me.txtPallet.Focus()
                            Else
                                PalletD = Me.dgPosiciones.Item(Me.dgPosiciones.CurrentRowIndex, 1)
                                Me.Close()
                            End If
                            'Else : MsgBox("Error")
                        End If
                    Else : Me.lblMsg.Text = "Posicion Destino erronea."
                        Me.txtConfDestino.Focus()
                        Me.txtConfDestino.SelectAll()
                    End If
                Else
                    Me.txtConfDestino.Focus()
                    Me.txtConfDestino.SelectAll()
                End If
            End If
        Catch ex As Exception
            MsgBox("txtConfDestino_KeyUp. " & ex.Message)
        End Try
    End Sub

    Private Sub cmdOtherPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOtherPos.Click
        OtherPosition()
    End Sub

    Private Sub dgPosiciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgPosiciones.Click
        Me.dgPosiciones.Select(Me.dgPosiciones.CurrentRowIndex)
    End Sub

    Private Sub dgPosiciones_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgPosiciones.GotFocus
        Me.dgPosiciones.Select(Me.dgPosiciones.CurrentRowIndex)
    End Sub

    Private Sub dgPosiciones_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgPosiciones.KeyUp
        Me.dgPosiciones.Select(Me.dgPosiciones.CurrentRowIndex)
    End Sub

    Private Sub txtPallet_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPallet.KeyUp
        If e.KeyValue = 13 Then
            If txtPallet.Text <> "" Then
                Dim Verifica As Integer
                If Me.VerificaPallet(Me.txtPallet.Text, Verifica) Then
                    If Verifica = 1 Then
                        PalletD = Me.txtPallet.Text
                        Me.Close()
                    Else : lblMsg.Text = "El pallet escaneado no se encuentra en la posicion."
                        Me.txtPallet.SelectAll()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtDestino_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDestino.TextChanged

    End Sub
End Class