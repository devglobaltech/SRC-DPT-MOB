Imports System.Data.SqlClient
Public Class frmConsultaStock
    Private Const SQLConErr As String = "No se pudo conectar a la base de datos."
    Private Const FrmName As String = "Consulta de stock"
    Private Const vMenu As String = "F1) Aceptar." & vbTab & vbTab & "F2) Cancelar." & vbNewLine & "F3) Salir."
    Private strBuscando As String = "Buscando..."
    Private sTipoConsulta As String
    Private Const strNoSeEncontraronDatos As String = "No se encontraron datos"
    Private bGrilla As Boolean = False
    'Si TipoConsulta=1 es Nro Pallet
    'Si TipoConsulta=2 es Ubicación
    'Si TipoConsulta=3 es Código Artículo
    Private blnCancelar As Boolean = True
    Public Property TipoConsulta() As Integer
        Get
            Return sTipoConsulta
        End Get
        Set(ByVal value As Integer)
            sTipoConsulta = value
        End Set
    End Property
    Private Sub Cancelar()
        Try
            Me.txtCodigo.Text = ""
            dgStock.DataSource = Nothing
            lblMsg.Text = ""
            txtCodigo.Focus()
            bGrilla = False
        Catch ex As Exception
            MsgBox("Cancelar: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
        
    End Sub
    Private Sub Salir()
        Try
            'frmPrincipal.Show()
            Me.Close()
        Catch ex As Exception
            MsgBox("Salir: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

    End Sub

    Private Sub frmConsultaStock_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                AddItem()
            Case Keys.F2
                Cancelar()
                'Case 112
                'VerificacionFin()
            Case 114 'F3
                Salir()
        End Select
    End Sub

    Private Sub ConsultaStock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = FrmName
            Me.txtCodigo.Focus()
            lblMsg.Text = ""
            Me.lblMenu.Text = vMenu
            Select Case Me.TipoConsulta
                Case TipoForm.FormStock.Pallet
                    'TipoConsulta=1 es Nro Pallet
                    lblCodigo.Text = "Nro. de Pallet"
                Case TipoForm.FormStock.Ubicacion
                    'TipoConsulta=2 es Ubicación
                    lblCodigo.Text = "Ubicación"
                Case TipoForm.FormStock.Producto
                    'TipoConsulta=3 es Código Producto
                    lblCodigo.Text = "Código de Producto"
            End Select
        Catch ex As Exception
            MsgBox("ConsultaStock_Load: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Function ResizeGrillaUbicacion() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "Consulta"
            dgStock.TableStyles.Clear()
            Style.MappingName = "Consulta"

            Dim TextCol1 As New DataGridTextBoxColumn
            With TextCol1
                .MappingName = "CLIENTE_ID"
                .HeaderText = "Cod. Cliente"
                .Width = 130
            End With
            Style.GridColumnStyles.Add(TextCol1)

            Dim TextCol2 As New DataGridTextBoxColumn
            With TextCol2
                .MappingName = "PRODUCTO_ID"
                .HeaderText = "Prod."
                .Width = 80
            End With
            Style.GridColumnStyles.Add(TextCol2)

            Dim TextCol7 As New DataGridTextBoxColumn
            With TextCol7
                .MappingName = "DESCRIPCION"
                .HeaderText = "Descr."
                .Width = 100
            End With
            Style.GridColumnStyles.Add(TextCol7)

            Dim TextCol8 As New DataGridTextBoxColumn
            With TextCol8
                .MappingName = "Unidad_id"
                .HeaderText = "Unidad"
                .Width = 50
            End With
            Style.GridColumnStyles.Add(TextCol8)

            Dim TextCol3 As New DataGridTextBoxColumn
            With TextCol3
                .MappingName = "CANTIDAD_TOTAL"
                .HeaderText = "Cant."
                .Width = 40
            End With
            Style.GridColumnStyles.Add(TextCol3)

            Dim TextCol4 As New DataGridTextBoxColumn
            With TextCol4
                .MappingName = "FECHA_VENCIMIENTO"
                .HeaderText = "Fecha Vto."
                .Width = 80
            End With
            Style.GridColumnStyles.Add(TextCol4)


            Dim TextCol6 As New DataGridTextBoxColumn
            With TextCol6
                .MappingName = "NRO_LOTE"
                .HeaderText = "Lote"
                .Width = 50
            End With
            Style.GridColumnStyles.Add(TextCol6)


            Dim TextCol5 As New DataGridTextBoxColumn
            With TextCol5
                .MappingName = "PROP1"
                .HeaderText = "Pallet"
                .Width = 70
            End With
            Style.GridColumnStyles.Add(TextCol5)

            dgStock.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("ResizeGrillaUbicacion: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Function
    
    Private Function ResizeGrillaCodigoProductoPallet() As Boolean
        Try

            Dim Style As New DataGridTableStyle
            Style.MappingName = "Consulta"
            dgStock.TableStyles.Clear()
            Style.MappingName = "Consulta"
            Dim TextCol0 As New DataGridTextBoxColumn
            With TextCol0
                .MappingName = "CLIENTE_ID"
                .HeaderText = "Cod. Cliente"
                .Width = 130
            End With
            Style.GridColumnStyles.Add(TextCol0)

            Dim TextCol1 As New DataGridTextBoxColumn
            With TextCol1
                .MappingName = "ProductoID"
                .HeaderText = "Prod."
                .Width = 80
            End With
            Style.GridColumnStyles.Add(TextCol1)

            Dim TextCol9 As New DataGridTextBoxColumn
            With TextCol9
                .MappingName = "DESCRIPCION"
                .HeaderText = "Desc."
                .Width = 100
            End With
            Style.GridColumnStyles.Add(TextCol9)

            Dim TextCol10 As New DataGridTextBoxColumn
            With TextCol10
                .MappingName = "UNIDAD_ID"
                .HeaderText = "Unidad"
                .Width = 50
            End With
            Style.GridColumnStyles.Add(TextCol10)


            Dim TextCol2 As New DataGridTextBoxColumn
            With TextCol2
                .MappingName = "Cantidad"
                .HeaderText = "Cant."
                .Width = 40
            End With
            Style.GridColumnStyles.Add(TextCol2)

            Dim TextCol3 As New DataGridTextBoxColumn
            With TextCol3
                .MappingName = "EST_MERC_ID"
                .HeaderText = "Est. Merc."
                .Width = 70
            End With
            Style.GridColumnStyles.Add(TextCol3)


            Dim TextCol3o As New DataGridTextBoxColumn
            With TextCol3o
                .MappingName = "POSICION"
                .HeaderText = "Posición"
                .Width = 120
            End With
            Style.GridColumnStyles.Add(TextCol3o)



            Dim TextCol4 As New DataGridTextBoxColumn
            With TextCol4
                .MappingName = "CategLogID"
                .HeaderText = "Cat. Lógica"
                .Width = 70
            End With
            Style.GridColumnStyles.Add(TextCol4)

            Dim TextCol5 As New DataGridTextBoxColumn
            With TextCol5
                .MappingName = "Nro_Lote"
                .HeaderText = "Nro. Lote"
                .Width = 70
            End With
            Style.GridColumnStyles.Add(TextCol5)

            Dim TextCol6 As New DataGridTextBoxColumn
            With TextCol6
                .MappingName = "Property_1"
                .HeaderText = "Pallet"
                .Width = 70
            End With
            Style.GridColumnStyles.Add(TextCol6)


            Dim TextCol7 As New DataGridTextBoxColumn
            With TextCol7
                .MappingName = "Fecha_Vencimiento"
                .HeaderText = "Fecha Vto."
                .Width = 80
            End With
            Style.GridColumnStyles.Add(TextCol7)



            Dim TextCol8 As New DataGridTextBoxColumn
            With TextCol8
                .MappingName = "PRODUCTO"
                .HeaderText = "Descripción"
                .Width = 90
            End With
            Style.GridColumnStyles.Add(TextCol8)

            dgStock.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("ResizeGrillaCodigoProducto: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Function

    Private Sub txtCodigo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigo.GotFocus
        txtCodigo.SelectAll()
    End Sub

    Private Sub txtCodigo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCodigo.KeyUp
        Try
            If e.KeyCode = 13 Then
                AddItem()
            End If
        Catch ex As Exception
            MsgBox("txtCodigo_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            lblMsg.Text = ""
        End Try
    End Sub

    Private Sub AddItem()
        Dim pa As SqlParameter
        Try
            If Me.txtCodigo.Text.Trim <> "" Then
                If VerifyConnection(SQLc) Then
                    lblMsg.Text = strBuscando
                    lblMsg.Refresh()
                    Dim Cmd As SqlCommand
                    Dim Da As SqlDataAdapter
                    Dim Ds As New Data.DataSet

                    Cmd = SQLc.CreateCommand
                    Da = New SqlDataAdapter(Cmd)
                    If Me.TipoConsulta <> TipoForm.FormStock.Producto Then
                        Cmd.Parameters.Add("@Codigo", Data.SqlDbType.VarChar, 100).Value = UCase(txtCodigo.Text.Trim) & ""
                    Else
                        o2D.Decode(txtCodigo.Text.Trim)
                        pa = New SqlParameter("@Codigo", Data.SqlDbType.VarChar, 100)
                        pa.Value = o2D.PRODUCTO_ID
                        pa.Direction = Data.ParameterDirection.InputOutput
                        'Cmd.Parameters.Add("@Codigo", Data.SqlDbType.VarChar, 100).Value = o2D.PRODUCTO_ID & ""
                        Cmd.Parameters.Add(pa)
                        pa = Nothing
                        Me.txtCodigo.Text = o2D.PRODUCTO_ID
                    End If
                    Cmd.Parameters.Add("@TipoOperacion", Data.SqlDbType.Int).Value = Me.TipoConsulta
                    'Cmd.Parameters.Add("@Cliente", Data.SqlDbType.VarChar, 15).Value = "PRU"
                    Cmd.CommandType = Data.CommandType.StoredProcedure
                    Cmd.CommandText = "Mob_ConsultaStock2"
                    Da.Fill(Ds, "Consulta")
                    If Ds.Tables("Consulta").Rows.Count = 0 Then
                        dgStock.DataSource = Nothing
                        lblMsg.Text = strNoSeEncontraronDatos
                        txtCodigo.Focus()
                        txtCodigo.SelectAll()
                    Else
                        bGrilla = True
                        Me.txtCodigo.Text = Cmd.Parameters("@Codigo").Value
                        dgStock.DataSource = Ds.Tables("Consulta")
                        Select Case Me.TipoConsulta
                            Case TipoForm.FormStock.Pallet
                                ResizeGrillaCodigoProductoPallet()
                            Case TipoForm.FormStock.Ubicacion
                                ResizeGrillaUbicacion()
                            Case TipoForm.FormStock.Producto
                                ResizeGrillaCodigoProductoPallet()
                        End Select
                        dgStock.Focus()
                        lblMsg.Text = ""
                    End If
                Else
                    MsgBox(SQLConErr, MsgBoxStyle.OkOnly, FrmName)
                    lblMsg.Text = ""
                    txtCodigo.Focus()
                End If

            Else

                Select Case Me.TipoConsulta
                    Case TipoForm.FormStock.Pallet
                        'TipoConsulta=1 es Nro Pallet
                        lblMsg.Text = "Debe ingresar el Nro. de Pallet"
                        txtCodigo.Focus()
                    Case TipoForm.FormStock.Ubicacion
                        'TipoConsulta=2 es Ubicación
                        lblMsg.Text = "Debe ingresar la Ubicación"
                        txtCodigo.Focus()
                    Case TipoForm.FormStock.Producto
                        'TipoConsulta=3 es Código Producto
                        lblMsg.Text = "Debe ingresar el Código del Producto"
                        txtCodigo.Focus()
                End Select


                dgStock.DataSource = Nothing
                'lblMsg.Text = ""
            End If
        Catch SQLExc As SqlException
            'MsgBox("AddItem_SQLExc: " & SQLExc.Message, MsgBoxStyle.OkOnly, FrmName)
            Me.txtCodigo.SelectAll()
            lblMsg.Text = SQLExc.Message
            Exit Sub
        Catch ex As Exception
            MsgBox("AddItem: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            lblMsg.Text = ""

        End Try
    End Sub

    Private Sub txtCodigo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigo.TextChanged
        dgStock.DataSource = Nothing
        lblMsg.Text = ""
        bGrilla = False
    End Sub

    Private Sub dgStock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgStock.Click
        Try
            dgStock.Select(dgStock.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgStock_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgStock.GotFocus
        Try
            If bGrilla = False Then
                txtCodigo.Focus()
                Exit Sub
            End If

            If lblMsg.Text <> strNoSeEncontraronDatos And txtCodigo.Text <> "" Then
                dgStock.Select(dgStock.CurrentRowIndex)
            Else
                txtCodigo.Focus()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgStock_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgStock.KeyUp
        Try
            dgStock.Select(dgStock.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmdAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
        AddItem()
    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Cancelar()
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Salir()
    End Sub

End Class