Imports System.Data
Imports System.Data.SqlClient

Public Class FrmTransferenciaBultos_Series

    Private Const FrmName As String = "Transferencias"
    Private Const SQLConErr As String = "Fallo al intentar conectar con la base de datos."
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private Data As DataSet
    Private vOrigen As String = ""
    Private vCliente_ID As String = ""
    Private vProducto_ID As String = ""
    Private vCancelado As Boolean = False

    Public ReadOnly Property InformacionSeries() As DataSet
        Get
            Return Data
        End Get
    End Property

    Public Property Cancelado() As Boolean
        Get
            Return Me.vCancelado
        End Get
        Set(ByVal value As Boolean)
            Me.vCancelado = value
        End Set
    End Property

    Public Property Origen() As String
        Get
            Return vOrigen
        End Get
        Set(ByVal value As String)
            vOrigen = value
        End Set
    End Property

    Public Property Cliente_ID() As String
        Get
            Return vCliente_ID
        End Get
        Set(ByVal value As String)
            vCliente_ID = value
        End Set
    End Property

    Public Property Producto_ID() As String
        Get
            Return vProducto_ID
        End Get
        Set(ByVal value As String)
            vProducto_ID = value
        End Set
    End Property

    Private Sub inicializarPantalla()
        Dim DT As New DataTable

        Data = New DataSet
        DT.Columns.Add("NRO_SERIE", GetType(String))
        Data.Tables.Add(DT)

        Me.lblOrigen.Text = "Ubicacion Origen: " & Me.Origen
        Me.lblCliente.Text = "Cod. Cliente: " & Me.Cliente_ID
        Me.lblProducto.Text = "Cod.Producto: " & Me.Producto_ID
        Me.txtNroSerie.Focus()
    End Sub

    Private Sub FrmTransferenciaBultos_Series_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyData
            Case Keys.F2
                Me.Cancelar()
            Case Keys.F1
                Me.Close()
        End Select
    End Sub

    Private Sub FrmTransferenciaBultos_Series_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.inicializarPantalla()
    End Sub

    Private Sub txtNroSerie_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNroSerie.KeyUp
        Dim Serie2D() As String, Serie As String = "", i As Integer = 0

        If e.KeyValue = 13 Then
            If Trim(Me.txtNroSerie.Text) <> "" Then

                o2D.CLIENTE_ID = Me.Cliente_ID
                o2D.Decode(Trim(Me.txtNroSerie.Text))

                If o2D.QtySeries = 0 Then
                    If Ingresar_Serie(Trim(UCase(Me.txtNroSerie.Text))) Then
                        Me.txtNroSerie.Text = ""
                        Me.txtNroSerie.Focus()
                    Else
                        Me.txtNroSerie.Text = ""
                        Me.txtNroSerie.Focus()
                    End If
                Else
                    If Me.Producto_ID <> o2D.PRODUCTO_ID Then
                        MsgBox("La serie escaneada no corresponde al producto que se desea transferir.", MsgBoxStyle.Information, FrmName)
                        Me.txtNroSerie.Text = ""
                        Me.txtNroSerie.Focus()
                        Exit Sub
                    End If
                    Serie2D = Split(o2D.Get_Series(), ";")
                    For i = 0 To Serie2D.Length - 1
                        If (Serie2D(i).ToString <> "") Then
                            If Ingresar_Serie(Serie2D(i)) Then
                                Me.txtNroSerie.Text = ""
                                Me.txtNroSerie.Focus()
                            Else
                                Me.txtNroSerie.Text = ""
                                Me.txtNroSerie.Focus()
                            End If
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Function Ingresar_Serie(ByVal NroSerie As String) As Boolean
        Dim vSerie As String, i As Integer, Row As DataRow, DT As New DataTable
        Try
            '---------------------------------------------------------------------------------------------
            '1. Controlo que no la haya tomado.
            '---------------------------------------------------------------------------------------------
            For i = 0 To Data.Tables(0).Rows.Count - 1
                vSerie = Data.Tables(0).Rows(i)(0).ToString
                If (vSerie = NroSerie) Then
                    If MsgBox("La serie se encuentra seleccionada, ¿desea quitarla de la lista?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                        Me.Data.Tables(0).Rows.RemoveAt(i)
                        Return True
                    End If
                End If
            Next
            '---------------------------------------------------------------------------------------------
            '2. Valido que la serie existe para la posición.
            '---------------------------------------------------------------------------------------------
            If Not ExisteSerie(Me.Origen, Me.Cliente_ID, Me.Producto_ID, NroSerie) Then
                MsgBox("El numero de serie no se encuentra en la posicion indicada", MsgBoxStyle.Information, FrmName)
                Me.txtNroSerie.Text = ""
                Me.txtNroSerie.Focus()
                Exit Try
            End If
            '---------------------------------------------------------------------------------------------
            '3. Si llego aqui es porque no existe la serie, hay que agregarla.
            '---------------------------------------------------------------------------------------------
            Row = Data.Tables(0).NewRow
            Row("NRO_SERIE") = NroSerie
            Data.Tables(0).Rows.Add(Row)

            '---------------------------------------------------------------------------------------------
            '4. Muestro los valores en la pantalla.
            '---------------------------------------------------------------------------------------------
            Me.DG.DataSource = Nothing
            Me.DG.DataSource = Me.Data.Tables(0)
            AutoSizeGrid(Me.DG, FrmName)

            Return True
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            DT.Dispose()
        End Try
    End Function

    Private Function ExisteSerie(ByVal Origen As String, ByVal Cliente As String, ByVal Producto As String, ByVal Serie As String) As Boolean
        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As New DataSet
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandText = "SELECT DBO.MOB_TR_BULTO_VALIDA_SERIE('" & Origen & "', '" & Cliente & "', '" & Producto & "', '" & Serie & "')"
                Cmd.CommandType = CommandType.Text

                DA.Fill(DS, "EXISTE")
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        If DS.Tables(0).Rows(0)(0).ToString = "0" Then
                            Return False
                        Else
                            Return True
                        End If
                    End If
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Information, FrmName)
                Return False
            End If
            Return True
        Catch ex As Exception
            MsgBox("Excepción: " & ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            Cmd.Dispose()
            DA.Dispose()
            DS.Dispose()
        End Try
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Cancelar()
    End Sub

    Private Sub Cancelar()
        If MsgBox("¿Desea cancelar la carga de series?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
            Me.Cancelado = True
            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub txtNroSerie_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNroSerie.TextChanged

    End Sub
End Class