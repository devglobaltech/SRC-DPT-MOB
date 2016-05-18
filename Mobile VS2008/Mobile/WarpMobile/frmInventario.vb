Imports System.Data.SqlClient
Imports System.Data
Public Class frmInventario
    Private fObs As New frmInventarioObservaciones
    Private Const FrmName As String = "Toma de Inventario"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private DtTareas As New Data.DataTable
    Private Indice As Integer
    Private Producto As String
    Private BlnInvEnCurso As Boolean
    Private NroInventario As Integer
    Private NroConteo As Integer
    Private xEsFraccionable As Boolean = False
    Private Producto_id As String = ""
    Private Cliente_id As String

    Private Sub frmInventario_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Comenzar()
            Case Keys.F2
                Cancelar()
            Case Keys.F3
                NuevoMarbete()
            Case Keys.F4
                Salir()
            Case Keys.F5
                Me.Observaciones()
        End Select
    End Sub
    Private Sub Salir()
        Dim Rta As Object
        If Me.BlnInvEnCurso = True Then
            Rta = MsgBox("Desea Cancelar la toma de inventario en curso y salir?", MsgBoxStyle.YesNo, FrmName)
        Else
            Rta = MsgBox("Desea salir de la toma de inventario?", MsgBoxStyle.YesNo, FrmName)
        End If
        If Rta = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Function ObtenerTareas() As Boolean
        Dim Da As SqlDataAdapter
        Dim Ds As New System.Data.DataSet
        Dim drDSRow As Data.DataRow
        Dim dt As New Data.DataTable
        Dim drNewRow As Data.DataRow
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.GET_TAREAS_INVENTARIO"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@INVENTARIO_ID", SqlDbType.Int, 30)
                Pa.Value = Me.NroInventario
                xCmd.Parameters.Add(Pa)
                xCmd.Connection = SQLc
                Da.Fill(Ds, "TAREAS")
                If Ds.Tables("TAREAS").Rows.Count > 0 Then
                    'Hay mas de un cliente, los cargo en el combo.
                    For Each drDSRow In Ds.Tables("TAREAS").Rows()
                        drNewRow = DtTareas.NewRow()
                        drNewRow("MARBETE") = drDSRow("MARBETE")
                        drNewRow("POSICION") = drDSRow("POSICION")
                        drNewRow("CLIENTE") = drDSRow("CLIENTE")
                        drNewRow("PRODUCTO") = drDSRow("PRODUCTO")
                        drNewRow("DESCRIPCION") = drDSRow("DESCRIPCION")
                        drNewRow("UNIDAD") = drDSRow("UNIDAD")
                        drNewRow("NRO_LOTE") = drDSRow("NRO_LOTE")
                        drNewRow("NRO_PARTIDA") = drDSRow("NRO_PARTIDA")

                        Me.DtTareas.Rows.Add(drNewRow)
                    Next
                    Indice = 0
                Else
                    MsgBox("No quedan tareas pendientes para el inventario: " & Str(NroInventario), MsgBoxStyle.Information, FrmName)
                    Return False
                End If
            Else : MsgBox(SQLError, MsgBoxStyle.Exclamation, FrmName)
            End If
        Catch SqlEx As SqlException
            MsgBox("SQL. " & SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            Da = Nothing
            Ds = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub frmInventario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        NroInventario = 0
        BuscarInventario()
        If NroInventario = 0 Then
            MsgBox("No se encuentra asignado a ningun inventario", MsgBoxStyle.Exclamation, FrmName)
            Me.Close()
        Else
            DtTareas.Columns.Add("MARBETE", GetType(System.String))
            DtTareas.Columns.Add("POSICION", GetType(System.String))
            DtTareas.Columns.Add("CLIENTE", GetType(System.String))
            DtTareas.Columns.Add("PRODUCTO", GetType(System.String))
            'MGR 20120312 Se muestra la descripcion del producto
            DtTareas.Columns.Add("DESCRIPCION", GetType(System.String))
            DtTareas.Columns.Add("UNIDAD", GetType(System.String))
            DtTareas.Columns.Add("NRO_LOTE", GetType(System.String))
            DtTareas.Columns.Add("NRO_PARTIDA", GetType(System.String))
            Me.cmdComenzar.Enabled = True
        End If
    End Sub
    Private Sub BuscarInventario()
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.GET_INVENTARIO_ID"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@INVENTARIO_ID", SqlDbType.Int, 30)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@NRO_CONTEO", SqlDbType.Int, 30)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()
                NroInventario = IIf(IsDBNull(xCmd.Parameters("@INVENTARIO_ID").Value), 0, xCmd.Parameters("@INVENTARIO_ID").Value)
                NroConteo = IIf(IsDBNull(xCmd.Parameters("@NRO_CONTEO").Value), 0, xCmd.Parameters("@NRO_CONTEO").Value)

            Else : MsgBox(SQLError, MsgBoxStyle.Exclamation, FrmName)
            End If
        Catch SqlEx As SqlException
            MsgBox("SQL. " & SqlEx.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        Finally
            Da = Nothing
            Pa = Nothing
        End Try
    End Sub

    Private Sub TxtPosicion_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPosicion.KeyUp
        If e.KeyValue = 13 Then
            If Me.TxtPosicion.Text <> "" Then
                If Not ExisteNavePosicion(Me.TxtPosicion.Text) Then
                    Me.TxtPosicion.Text = ""
                    Me.TxtPosicion.Focus()
                    Exit Sub
                Else
                    If UCase(Trim(Me.TxtPosicion.Text)) = UCase(Trim(Me.DtTareas.Rows(Indice)("POSICION").ToString)) Then
                        Me.TxtPosicion.ReadOnly = True
                        Me.LblTitCliente.Visible = True
                        Me.LblCliente.Visible = True
                        Me.LblCliente.Text = Me.DtTareas.Rows(Indice)("CLIENTE").ToString
                        Me.Cliente_id = Me.DtTareas.Rows(Indice)("CLIENTE").ToString
                        Me.LblTitProd.Visible = True
                        Me.TxtProducto.Visible = True
                        Me.LblProd.Visible = True
                        'MGR 20120312 Se muestra la descripcion del producto
                        Me.LblDescripcion.Visible = True
                        Me.LblProd.Text = Me.DtTareas.Rows(Indice)("PRODUCTO").ToString
                        Me.LblDescripcion.Text = Me.DtTareas.Rows(Indice)("DESCRIPCION") ' MGR
                        Me.TxtProducto.Focus()
                    Else
                        MsgBox("La posicion ingresada es incorrecta", MsgBoxStyle.Exclamation, FrmName)
                        Me.TxtPosicion.Text = ""
                        Me.TxtPosicion.Focus()
                        Exit Sub
                    End If
                End If
            End If
        End If
    End Sub
    Private Function ExisteNavePosicion(ByVal xUbicacion As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim xValue As String
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "ExisteNavePosicion"
                Cmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Ubicacion", SqlDbType.VarChar, 45)
                Pa.Value = xUbicacion
                Cmd.Parameters.Add(Pa)
                Pa = Nothing
                Pa = New SqlParameter("@Retorno", SqlDbType.Char, 1, ParameterDirection.Output)
                Pa.Value = DBNull.Value
                Cmd.Parameters.Add(Pa)

                Cmd.ExecuteNonQuery()
                xValue = IIf(IsDBNull(Cmd.Parameters("@Retorno").Value), "", Cmd.Parameters("@Retorno").Value)
            Else
                MsgBox(SQLError, MsgBoxStyle.Exclamation, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("ExisteNavePosicion SQL: " & SQLEx.Message)
            Return False
        Catch ex As Exception
            MsgBox("ExisteNavePosicion: " & ex.Message)
            Return False
        Finally
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub TxtProducto_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtProducto.KeyUp
        If (e.KeyValue = 13) And Trim(Me.TxtProducto.Text) <> "" Then
            o2D.CLIENTE_ID = Me.Cliente_id
            o2D.Decode(Me.TxtProducto.Text)
            Me.TxtProducto.Text = o2D.PRODUCTO_ID
            If ValidarIngreso() = True Then
                If Producto.ToUpper = Me.DtTareas.Rows(Indice)("PRODUCTO").ToString.ToUpper Then
                    Me.TxtProducto.ReadOnly = True
                    Me.LblTitUnidad.Visible = True
                    Me.LblUnidad.Text = Me.DtTareas.Rows(Indice)("UNIDAD").ToString
                    Dim TxtAdic As String = ""

                    If Len(Me.DtTareas.Rows(Indice)("NRO_LOTE").ToString) > 0 Then
                        TxtAdic = "-Nro.Lote = " + Me.DtTareas.Rows(Indice)("NRO_LOTE").ToString
                    End If

                    If Len(Me.DtTareas.Rows(Indice)("NRO_PARTIDA").ToString) > 0 Then
                        If TxtAdic.Length > 0 Then
                            TxtAdic += Chr(10) + Chr(13)
                        End If
                        TxtAdic += "-Nro.Partida = " + Me.DtTareas.Rows(Indice)("NRO_PARTIDA").ToString
                    End If
                    Me.Lbl_prop_adic.Visible = True
                    If Len(TxtAdic) > 0 Then
                        Me.Lbl_prop_adic.Text = "Atributos: " + Chr(13) + TxtAdic
                    End If
                    Me.LblUnidad.Text = Me.DtTareas.Rows(Indice)("UNIDAD").ToString
                    Me.LblUnidad.Visible = True
                    Me.LblCant.Visible = True
                    Me.TxtCantidad.Visible = True
                    Me.cmdObservaciones.Enabled = True
                    Me.TxtCantidad.Focus()
                Else
                    Me.TxtProducto.Text = ""
                    Me.TxtProducto.Focus()
                End If
            Else : Me.TxtProducto.Text = ""
                Me.TxtProducto.Focus()
            End If
        End If
    End Sub
    Private Function ValidarIngreso() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.Val_Prod"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@CLIENTE_ID", Data.SqlDbType.VarChar, 50)
                Pa.Value = Me.DtTareas.Rows(Indice)("CLIENTE").ToString
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CODIGO", SqlDbType.VarChar, 50)
                Pa.Value = Me.TxtProducto.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                Pa.Direction = ParameterDirection.Output
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Producto = IIf(IsDBNull(Cmd.Parameters("@PRODUCTO_ID").Value), "", Cmd.Parameters("@PRODUCTO_ID").Value)
                Me.TxtProducto.Text = Trim(UCase(Producto))

                Dim flgfracc As String = ""
                GetFlgFraccionable(Cmd.Parameters("@CLIENTE_ID").Value, TxtProducto.Text.ToString, flgfracc)

                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("SQL ValidarIngreso: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("ValidarIngreso: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function


    Private Sub TxtCantidad_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCantidad.KeyUp
        Dim Rta As Object
        If (e.KeyValue = 13) Then
            'If Val(TxtCantidad.Text) >= 0 And TxtCantidad.Text <> "" Then
            If TxtCantidad.Text >= 0 And TxtCantidad.Text <> "" Then
                Rta = MsgBox("Desea Confirmar la cantidad?", MsgBoxStyle.YesNo, FrmName)
                If Rta = vbYes Then
                    If GrabaCantidad() Then
                        Indice = Indice + 1
                        Cancelar()
                        If Indice < DtTareas.Rows.Count Then
                            ProximaTarea()
                        Else
                            Me.LblInventario.Visible = False
                            Me.LblPosicion.Visible = False
                            Me.TxtPosicion.Visible = False
                            DtTareas.Clear()
                            Do While DtTareas.Rows.Count = 0
                                ObtenerTareas()
                                If DtTareas.Rows.Count > 0 Then
                                    ProximaTarea()
                                    Me.LblInventario.Visible = True
                                    Me.LblPosicion.Visible = True
                                    Me.TxtPosicion.Visible = True
                                Else
                                    NroInventario = 0
                                    BuscarInventario()
                                    If NroInventario = 0 Then
                                        BlnInvEnCurso = False
                                        Me.Close()
                                        Exit Sub
                                    End If
                                End If
                            Loop
                        End If
                    End If
                Else
                    TxtCantidad.Text = ""
                    TxtCantidad.Focus()
                End If
            Else
                TxtCantidad.Text = ""
                TxtCantidad.Focus()
            End If
        End If
    End Sub
    Private Sub Cancelar()
        Me.TxtPosicion.Focus()
        Me.TxtPosicion.ReadOnly = False
        Me.TxtPosicion.Text = ""
        Me.LblTitCliente.Visible = False
        Me.LblCliente.Text = ""
        Me.LblCliente.Visible = False
        Me.LblTitProd.Visible = False
        Me.LblProd.Visible = False
        'MGR 20120312 Se muestra la descripcion del producto
        Me.LblDescripcion.Visible = False
        Me.LblProd.Text = ""
        Me.TxtProducto.ReadOnly = False
        Me.TxtProducto.Text = ""
        Me.TxtProducto.Visible = False
        Me.LblUnidad.Visible = False
        Me.LblUnidad.Text = ""
        Me.LblTitUnidad.Visible = False
        Me.TxtCantidad.Visible = False
        Me.TxtCantidad.Text = ""
        Me.LblCant.Visible = False
        Me.Lbl_prop_adic.Text = ""
        Me.Lbl_prop_adic.Visible = False
        Me.LblUnidad.Visible = False
        Me.LblUnidad.Text = ""
        Me.fObs.IniForm()
        Me.cmdObservaciones.Enabled = False
    End Sub

    Private Sub inicializaForm()
        Me.LblInventario.Text = ""
        Me.LblPosicion.Text = ""
        Me.TxtPosicion.Visible = False
        Me.cmdComenzar.Enabled = True
        Me.cmdNuevoMarbete.Enabled = False
        Me.cmdCancelar.Enabled = False
        Cancelar()
    End Sub


    Private Sub ProximaTarea()
        Me.LblInventario.Visible = True
        Me.LblInventario.Text = "Inventario : " & Me.NroInventario & " Conteo : " & NroConteo.ToString
        Me.LblPosicion.Visible = True
        Me.LblPosicion.Text = "Posicion: " & Me.DtTareas.Rows(Indice)("POSICION").ToString
        Me.TxtPosicion.Visible = True
        Me.TxtPosicion.Focus()
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Salir()
    End Sub

    Private Sub cmdComenzar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdComenzar.Click
        Comenzar()
    End Sub
    Private Sub Comenzar()
        NroInventario = 0
        BuscarInventario()
        If NroInventario = 0 Then
            MsgBox("No se encuentra asignado a ningun inventario", MsgBoxStyle.Exclamation, FrmName)
        Else
            ObtenerTareas()
            If DtTareas.Rows.Count > 0 Then
                ProximaTarea()
                Me.cmdComenzar.Enabled = False
                Me.cmdCancelar.Enabled = True
                Me.cmdNuevoMarbete.Enabled = True
                Me.BlnInvEnCurso = True
            Else
                Me.Close()
            End If
        End If
    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Cancelar()
    End Sub
    Private Function GrabaCantidad() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "Dbo.GRABA_CANT_CONTEO"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Pa = New SqlParameter("@INVENTARIO_ID", Data.SqlDbType.Int, 20)
                Pa.Value = NroInventario
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@MARBETE", SqlDbType.Int, 20)
                Pa.Value = Me.DtTareas.Rows(Indice)("MARBETE")
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CANTIDAD", SqlDbType.Decimal, 10)
                Pa.Value = Me.TxtCantidad.Text
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OBSERVACIONES", SqlDbType.VarChar, 2000)
                If Trim(fObs.Observaciones = "") Then
                    Pa.Value = DBNull.Value
                Else
                    Pa.Value = fObs.Observaciones
                End If
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Return True
            Else
                MsgBox(SQLError, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            If Mid(SQLEx.Message, 1, 1) = "1" Then
                inicializaForm()
            End If
            MsgBox("SQL GRABA_CANT_CONTEO: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("GRABA_CANT_CONTEO: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            Pa = Nothing
            Cmd = Nothing
        End Try
    End Function

    Private Sub TxtCantidad_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtCantidad.KeyPress
        Dim Search As String
        Dim Pos As Integer
        Search = "."
        If Not xEsFraccionable Then
            ValidarCaracterNumerico(e)
        Else
            Pos = InStr(1, Me.TxtCantidad.Text, Search)
            If Pos > 0 And Asc(e.KeyChar) <> 46 Then
                If Len(Mid(Me.TxtCantidad.Text, Pos + 1, Len(Me.TxtCantidad.Text))) >= 5 And Asc(e.KeyChar) <> 8 Then
                    e.Handled = True
                    Me.TxtCantidad.Focus()
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

    Private Sub cmdNuevoMarbete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNuevoMarbete.Click
        NuevoMarbete()
    End Sub
    Private Sub NuevoMarbete()
        Dim FrmMarbete As New frmMarbete
        FrmMarbete.Ubicacion = DtTareas.Rows(Indice)("POSICION").ToString
        FrmMarbete.NumeroInventario = NroInventario
        FrmMarbete.ShowDialog()
        FrmMarbete = Nothing
    End Sub

    Private Sub TxtProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtProducto.TextChanged

    End Sub

    Private Sub Label1_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblDescripcion.ParentChanged

    End Sub

    Private Sub TxtObservaciones_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmdObservaciones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdObservaciones.Click

    End Sub

    Private Sub Observaciones()
        fObs.DataInventario = Me.LblInventario.Text
        fObs.ShowDialog()
    End Sub

    Private Sub TxtCantidad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCantidad.TextChanged

    End Sub

    Private Sub TxtPosicion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtPosicion.TextChanged

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
                If FlgFraccionable = "1" Then
                    xEsFraccionable = True
                Else
                    xEsFraccionable = False
                End If
                Return True
            Else : MsgBox("Error", MsgBoxStyle.Critical, FrmName)
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