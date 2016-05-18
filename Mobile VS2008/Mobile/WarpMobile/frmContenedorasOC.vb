Imports System.Data.SqlClient
Imports System.Data
Imports System.Math

Public Class FrmContenedorasOC

    Private Const FrmName As String = "Configuración de Contenedoras"
    Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."
    Private Const ErrCon As String = "No se pudo conectar con la base de datos."
    Private CodProducto As String
    Private DescProd As String
    Private Qty As Double
    Private Qty_Cont As Double
    Private Qty_SumCant As Double
    Private Cliente As String
    Private OC As String
    Private lngContenedora As Long
    Private vLoteProveedor As String
    Private vPartida As String
    Private UnidadDesc As String
    Private Inicial As Boolean = False
    Private vDoc_ext As String
    Public Nfrm As New FrmEditContenedoras
    Public Confirmado As Boolean = False
    Private vContenedoraEspecifica As String = ""
    Private vFechaVto As String

    Public Property ContenedoraEspecifica() As String
        Get
            Return vContenedoraEspecifica
        End Get
        Set(ByVal value As String)
            vContenedoraEspecifica = value
        End Set
    End Property

    Public Property doc_ext() As String
        Get
            Return vDoc_ext
        End Get
        Set(ByVal value As String)
            vDoc_ext = value
        End Set
    End Property

    Public Property loteProveedor() As String
        Get
            Return vLoteProveedor
        End Get
        Set(ByVal value As String)
            vLoteProveedor = value
        End Set
    End Property

    Public Property partida() As String
        Get
            Return vPartida
        End Get
        Set(ByVal value As String)
            vPartida = value
        End Set
    End Property

    Property fecha() As String
        Get
            Return vFechaVto
        End Get
        Set(ByVal value As String)
            vFechaVto = value
        End Set
    End Property

    Public Property vCliente() As String
        Get
            Return Cliente
        End Get
        Set(ByVal value As String)
            Cliente = value
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
    Public Property Cant_Cont() As Double
        Get
            Return Qty_Cont
        End Get
        Set(ByVal value As Double)
            Qty_Cont = value
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

    Private Sub FrmContenedorasOC_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Finalizar()
            Case Keys.F2
                Salir()
        End Select
    End Sub

    Private Sub FrmContenedorasOC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If GetContenedoras() Then
            lblProductoId.Text = Producto_ID
            lblDescripcion.Text = DescripcionProducto
            lblUnidad.Text = Unidad
            lblCantidadSolicitada.Text = Cantidad
            Me.lblPartida.Text = Me.partida
            Me.lblLoteProveedor.Text = Me.loteProveedor
            Me.dtgContenedoras.Focus()
        End If
    End Sub
    Private Function GetContenedoras() As Boolean
        Dim i As Integer
        Dim contenedora As Integer = 1
        Dim cantidadTotal As Double, SumaQty As Double
        Dim Table1 As DataTable
        Table1 = New DataTable("tabContenedora")

        Dim ds As New DataSet
        Dim columnas As DataColumnCollection = Table1.Columns
        Dim _filaTemp As DataRow

        Try
            cantidadTotal = Round(Cantidad / Cant_Cont, 5)
            With Table1
                .Columns.Add("Contenedora", Type.GetType("System.String"))
                .Columns.Add("Cantidad", Type.GetType("System.Double"))
                .PrimaryKey = New DataColumn() {Table1.Columns("Contenedora")}
            End With


            For i = 1 To Cant_Cont

                If i = Cant_Cont Then
                    cantidadTotal = Cantidad - SumaQty
                End If
                _filaTemp = Table1.NewRow()
                If GetNumberofContenedora(Me.lngContenedora) Then
                    contenedora = Me.lngContenedora
                    o2D.Contenedora = Me.lngContenedora
                End If
                _filaTemp(columnas(0)) = contenedora
                _filaTemp(columnas(1)) = CDbl(cantidadTotal)
                SumaQty = SumaQty + CDbl(cantidadTotal)
                Table1.Rows.Add(_filaTemp)
            Next
            lblCantidadContenedoras.Text = SumaQty
            lblCantidadRestante.Text = Cantidad - SumaQty

            ds.Tables.Add(Table1)
            Nfrm.TablaContenedoras = ds
            dtgContenedoras.DataSource = ds.Tables(0)
            ResizeGrillaContenedoras(ds.Tables(0))

            If dtgContenedoras.VisibleRowCount < 1 Then
                btnFinalizar.Enabled = False
            Else
                btnFinalizar.Enabled = True
            End If

            Return True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
            Return False
        Finally
            Table1 = Nothing
        End Try
    End Function

    Private Function ResizeGrillaContenedoras(ByVal ds As DataTable) As Boolean
        Try
            Dim Style As New DataGridTableStyle()
            Style.MappingName = ds.TableName
            dtgContenedoras.TableStyles.Add(Style)

            dtgContenedoras.TableStyles(ds.TableName).GridColumnStyles(0).Width = 85
            dtgContenedoras.TableStyles(ds.TableName).GridColumnStyles(0).MappingName = "Contenedora"
            dtgContenedoras.TableStyles(ds.TableName).GridColumnStyles(0).HeaderText = "Contenedora"

            dtgContenedoras.TableStyles(ds.TableName).GridColumnStyles(1).Width = 85
            dtgContenedoras.TableStyles(ds.TableName).GridColumnStyles(1).MappingName = "Cantidad"
            dtgContenedoras.TableStyles(ds.TableName).GridColumnStyles(1).HeaderText = "Cantidad"

        Catch ex As Exception
            MsgBox("ResizeGrillaContenedoras: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Function

    Private Sub dtgContenedoras_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgContenedoras.GotFocus
        Try
            Me.dtgContenedoras.Select(Me.dtgContenedoras.CurrentRowIndex)
        Catch ex As Exception
            MsgBox("dtgContenedoras_GotFocus: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub dtgContenedoras_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtgContenedoras.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Finalizar()
            Case Keys.F2
                Salir()
            Case Keys.Enter
                EditarContenedoras()
                recargarCantidades()
        End Select
    End Sub
    Public Sub recargarCantidades()
        Dim i As Integer
        Dim SumaQty As Double = 0
        Dim ContDataTable As DataTable = CType(dtgContenedoras.DataSource, DataTable)
        Try
            For i = 0 To ContDataTable.Rows.Count - 1
                SumaQty = SumaQty + CDbl(dtgContenedoras.Item(i, 1))
            Next
            lblCantidadContenedoras.Text = SumaQty
            lblCantidadRestante.Text = (CDbl(lblCantidadSolicitada.Text) - SumaQty).ToString
        Catch ex As Exception
            MsgBox("recargarCantidades: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Sub

    Private Sub EditarContenedoras()
        Try
            Nfrm.Producto_ID = lblProductoId.Text
            Nfrm.DescripcionProducto = lblDescripcion.Text
            Nfrm.Cantidad = Me.dtgContenedoras.Item(dtgContenedoras.CurrentRowIndex, 1).ToString()
            Nfrm.Row = dtgContenedoras.CurrentRowIndex
            Nfrm.Contenedora = CInt(Me.dtgContenedoras.Item(dtgContenedoras.CurrentRowIndex, 0).ToString())
            Nfrm.Unidad = lblUnidad.Text
            Nfrm.CantidadSolicitada = lblCantidadSolicitada.Text

            Nfrm.ShowDialog()

            If Nfrm.Producto_ID <> Nothing Then
                lblProductoId.Visible = True
                lblDescripcion.Visible = True
                lblUnidad.Visible = True
                lblCantidadSolicitada.Visible = True
                lblCantidadContenedoras.Visible = True
                lblCantidadRestante.Visible = True
                lblProductoId.Text = Producto_ID
                lblDescripcion.Text = DescripcionProducto
                lblUnidad.Text = Unidad
                lblCantidadSolicitada.Text = Cantidad
                dtgContenedoras.Focus()
            Else
                lblProductoId.Text = ""
                lblDescripcion.Text = ""
                lblUnidad.Text = ""
                lblCantidadSolicitada.Text = ""
                lblCantidadContenedoras.Text = ""
                lblCantidadRestante.Text = ""
            End If

        Catch ex As Exception
            MsgBox("EditarContenedoras: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally

        End Try
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Salir()
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
    Private Sub btnFinalizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinalizar.Click
        Try
            Finalizar()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub
    Private Sub Finalizar()
        If Me.lblCantidadRestante.Text <> "0" Then
            If CLng(Me.lblCantidadRestante.Text) < 0 Then
                MsgBox("No se puede finalizar porque existe una diferencia de " & Me.lblCantidadRestante.Text & " " & Me.lblUnidad.Text & " sobrantes", MsgBoxStyle.Critical, FrmName)
            Else
                MsgBox("No se puede finalizar porque existe una diferencia de " & Me.lblCantidadRestante.Text & " " & Me.lblUnidad.Text & " por ubicar", MsgBoxStyle.Critical, FrmName)
            End If
        Else
            If IngresaOC() = True Then
                'MessageBox.Show("Paso el IngresaOC()")
                Confirmado = True
                InsercionContenedoras()
                Producto_ID = Nothing
            End If
            Me.Close()
        End If
    End Sub

    Private Function IngresaOC() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Dim Respuesta As Boolean = False
        Try
            If GetYaExisteProductoCargado() = True Then
                If Val(Cantidad) > 0 Then
                    Dim Rta As Object = MsgBox("¿Confirma el Ingreso de la nueva Orden de Compra?", MsgBoxStyle.YesNo, FrmName)
                    If Rta = vbYes Then
                        Respuesta = True
                        If VerifyConnection(SQLc) Then
                            Cmd = SQLc.CreateCommand
                            Cmd.CommandText = "Dbo.INGRESO_OC_ALTA"
                            Cmd.CommandType = Data.CommandType.StoredProcedure
                            Cmd.Parameters.Clear()

                            Cmd.Transaction = SQLc.BeginTransaction()
                            Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                            Pa.Value = vCliente
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                            Pa.Value = Producto_ID
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@ORDEN_COMPRA", SqlDbType.VarChar, 100)
                            Pa.Value = vOC
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@CANTIDAD", SqlDbType.Float)
                            Pa.Value = Cantidad
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@CANT_CONTENEDORAS", SqlDbType.Float)
                            Pa.Value = CDbl(Replace(Cant_Cont, ".", ","))
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@FECHA", SqlDbType.DateTime)
                            Pa.Value = DateTime.Today
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@PROCESADO", SqlDbType.Char, 1)
                            Pa.Value = "0"
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@LOTEPROVEEDOR", SqlDbType.VarChar, 100)
                            Pa.Value = Me.LoteProveedor
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@PARTIDA", SqlDbType.VarChar, 100)
                            Pa.Value = Me.Partida
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@DOC_EXT", SqlDbType.VarChar, 100)
                            Pa.Value = Me.doc_ext
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@FECHA_VTO", SqlDbType.VarChar, 20)
                            If Me.fecha = "__/__/____" Then
                                Pa.Value = DBNull.Value
                            Else
                                Pa.Value = Me.fecha
                            End If

                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Cmd.ExecuteNonQuery()
                            Cmd.Transaction.Commit()

                        Else
                            MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
                        End If
                    End If
                End If
            Else
                If Val(Cantidad) > 0 Then
                    Dim Rta As Object = MsgBox("¿Confirma la modificacion de la Orden de Compra?", MsgBoxStyle.YesNo, FrmName)
                    If Rta = vbYes Then
                        Respuesta = True
                        If VerifyConnection(SQLc) Then
                            Cmd = SQLc.CreateCommand
                            Cmd.CommandText = "Dbo.INGRESO_OC_ACTUALIZA"
                            Cmd.CommandType = Data.CommandType.StoredProcedure
                            Cmd.Parameters.Clear()

                            Cmd.Transaction = SQLc.BeginTransaction()
                            Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                            Pa.Value = vCliente
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 30)
                            Pa.Value = Producto_ID
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@ORDEN_COMPRA", SqlDbType.VarChar, 100)
                            Pa.Value = vOC
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@CANTIDAD", SqlDbType.Float, 20)
                            Pa.Value = Cantidad
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@CANT_CONTENEDORAS", SqlDbType.Float)
                            Pa.Value = CDbl(Replace(Cant_Cont, ".", ","))
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@LOTEPROVEEDOR", SqlDbType.VarChar, 100)
                            Pa.Value = Me.loteProveedor
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@PARTIDA", SqlDbType.VarChar, 100)
                            Pa.Value = Me.partida
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Pa = New SqlParameter("@DOC_EXT", SqlDbType.VarChar, 100)
                            Pa.Value = Me.doc_ext
                            Cmd.Parameters.Add(Pa)
                            Pa = Nothing

                            Cmd.ExecuteNonQuery()
                            Cmd.Transaction.Commit()
                            If GetYaExistenContenedoras() = False Then
                                EliminacionContenedoras()
                            End If
                        Else
                            MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
                        End If
                    End If
                End If
            End If
        Catch sqlex As SqlException
            Cmd.Transaction.Rollback()
            MsgBox(sqlex.Message, MsgBoxStyle.Critical, FrmName)
        Catch ex As Exception
            Cmd.Transaction.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
        Return Respuesta
    End Function

    Private Function GetYaExisteProductoCargado() As Boolean
        Dim Da As SqlDataAdapter
        Dim Cmd As SqlCommand
        Dim Ds As New Data.DataSet
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.Parameters.Add("@CLIENTE_ID", Data.SqlDbType.VarChar, 15).Value = UCase(vCliente) & ""
                Cmd.Parameters.Add("@PRODUCTO_ID", Data.SqlDbType.VarChar, 30).Value = Producto_ID & ""
                Cmd.Parameters.Add("@ORDEN_COMPRA", Data.SqlDbType.VarChar, 100).Value = vOC
                Cmd.Parameters.Add("@LOTE_PROVEEDOR", Data.SqlDbType.VarChar, 100).Value = Me.loteProveedor
                Cmd.Parameters.Add("@PARTIDA", Data.SqlDbType.VarChar, 100).Value = Me.partida

                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.CommandText = "INGRESO_OC_EXISTE_PROD"
                Da.Fill(Ds, "Consulta")
                If Not (Ds Is Nothing) And Ds.Tables.Count > 0 And Ds.Tables("Consulta").Rows.Count > 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Function
    Public Function GetNumberofContenedora(ByRef Contenedora_id As Long) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "Get_Value_For_Sequence"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                xCmd.Parameters.Clear()
                Pa = New SqlParameter("@SECUENCIA", Data.SqlDbType.VarChar, 50)
                Pa.Value = "CONTENEDORA"
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VALUE", Data.SqlDbType.Int)
                Pa.Direction = Data.ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                Contenedora_id = IIf(IsDBNull(xCmd.Parameters("@VALUE").Value), 0, xCmd.Parameters("@VALUE").Value)
                Return True
            Else
                MsgBox(ErrCon, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("GetNumberofContenedora SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("GetNumberofContenedora: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function
    Private Function GetYaExistenContenedoras() As Boolean
        Dim Da As SqlDataAdapter
        Dim Cmd As SqlCommand
        Dim Ds As New Data.DataSet
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(Cmd)
                Cmd.Parameters.Add("@CLIENTE_ID", Data.SqlDbType.VarChar, 15).Value = UCase(vCliente) & ""
                Cmd.Parameters.Add("@PRODUCTO_ID", Data.SqlDbType.VarChar, 30).Value = Producto_ID & ""
                Cmd.Parameters.Add("@ORDEN_COMPRA", Data.SqlDbType.VarChar, 100).Value = vOC

                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.CommandText = "INGRESO_OC_EXISTE_CONTEN"
                Da.Fill(Ds, "Consulta")
                If Not (Ds Is Nothing) And Ds.Tables.Count > 0 And Ds.Tables("Consulta").Rows.Count > 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Function

    Private Function InsercionContenedoras() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Dim Ds As New Data.DataSet
        Dim i As Integer
        Dim ContDataTable As DataTable = CType(dtgContenedoras.DataSource, DataTable)

        Try
            If VerifyConnection(SQLc) Then
                'PartidaAT(UCase(vCliente), Producto_ID, partida)
                For i = 0 To ContDataTable.Rows.Count - 1
                    Cmd = SQLc.CreateCommand
                    Cmd.CommandText = "INGRESO_CONTENEDORAS"
                    Cmd.CommandType = Data.CommandType.StoredProcedure
                    Cmd.Parameters.Clear()

                    Cmd.Transaction = SQLc.BeginTransaction()
                    Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                    Pa.Value = UCase(vCliente)
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 15)
                    Pa.Value = Producto_ID
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@ORDEN_COMPRA", SqlDbType.VarChar, 15)
                    Pa.Value = vOC
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@CANTIDAD", SqlDbType.Float, 15)
                    Pa.Value = CDbl(Me.dtgContenedoras.Item(i, 1))
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@CONTENEDORA", SqlDbType.Int, 15)
                    Pa.Value = CInt(Me.dtgContenedoras.Item(i, 0))
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@LOTEPROVEEDOR", SqlDbType.VarChar, 100)
                    Pa.Value = Me.loteProveedor
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Pa = New SqlParameter("@PARTIDA", SqlDbType.VarChar, 100)
                    Pa.Value = Me.partida
                    Cmd.Parameters.Add(Pa)
                    Pa = Nothing

                    Cmd.ExecuteNonQuery()
                    Cmd.Transaction.Commit()

                Next

            Else
                MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Function

    Private Function PartidaAT(ByVal Cliente As String, ByVal Producto As String, ByRef Partida As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Ds As New Data.DataSet
        Dim DA As System.Data.SqlClient.SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "SELECT ISNULL(NRO_PARTIDA_AUTOMATICO,'0') AS NRO_PARTIDA_AUTOMATICO FROM PRODUCTO WHERE CLIENTE_ID='" & Cliente & "' AND PRODUCTO_ID='" & Producto & "'"
                Cmd.CommandType = Data.CommandType.Text

                DA = New SqlDataAdapter(Cmd)
                DA.Fill(Ds, "PARTIDAAT")

                If Ds.Tables("PARTIDAAT").Rows(0)(0).ToString = "1" Then
                    'GENERA LA PARTIDA.
                    If Trim(Partida).ToString = "" Then
                        'TENGO QUE GENERAR LA PARTIDA.
                        GetNroPartidaAT(Partida)
                    End If
                Else
                    Return True
                End If
            Else
                MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        Finally
            Cmd = Nothing
            Ds = Nothing
            DA = Nothing
        End Try
    End Function

    Public Function GetNroPartidaAT(ByRef Partida As String) As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.CommandText = "Get_Value_For_Sequence"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                xCmd.Parameters.Clear()
                Pa = New SqlParameter("@SECUENCIA", Data.SqlDbType.VarChar, 50)
                Pa.Value = "NRO_PARTIDA"
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VALUE", Data.SqlDbType.Int)
                Pa.Direction = Data.ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                Partida = IIf(IsDBNull(xCmd.Parameters("@VALUE").Value), 0, xCmd.Parameters("@VALUE").Value)
                Return True
            Else
                MsgBox(ErrCon, MsgBoxStyle.OkOnly)
            End If
        Catch SQLEx As SqlException
            MsgBox("GetNumberofContenedora SQL: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("GetNumberofContenedora: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function EliminacionContenedoras() As Boolean
        Dim Pa As SqlParameter
        Dim Cmd As SqlCommand
        Dim Ds As New Data.DataSet
        Dim i As Integer

        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.CommandText = "ELIMINA_CONTENEDORAS"
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Cmd.Parameters.Clear()

                Cmd.Transaction = SQLc.BeginTransaction()
                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = UCase(vCliente)
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 15)
                Pa.Value = Producto_ID
                Cmd.Parameters.Add(Pa)
                Pa = Nothing


                Pa = New SqlParameter("@ORDEN_COMPRA", SqlDbType.VarChar, 15)
                Pa.Value = vOC
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Cmd.Transaction.Commit()
            Else
                MsgBox(ErrCon, MsgBoxStyle.Exclamation, FrmName)
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Function
End Class