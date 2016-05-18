Imports System.Data.SqlClient
Imports System.Data


Public Class FrmConsultaRecepOC

  Private Const FrmName As String = "Armado Pallet Final - Pendientes"
  Private Const SQLError As String = "Fallo al intentar conectar con la base de datos."

  Private Cliente As String
  Private OC As String

  Private CodProducto As String
  Private DescProd As String
  Private Qty As Double
  Private Qty_Cont As Double

  Public ReadOnly Property Producto_ID() As String
    Get
      Return CodProducto
    End Get
  End Property

  Public ReadOnly Property DescripcionProducto() As String
    Get
      Return DescProd
    End Get
  End Property

  Public ReadOnly Property Cantidad() As Double
    Get
      Return Qty
    End Get
  End Property

  Public ReadOnly Property Cant_Cont() As Double
    Get
      Return Qty_Cont
    End Get
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

  Private Sub FrmConsultaRecepOC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If GetIngresados() Then
            lblCompañia.Text = Cliente
            lblNOC.Text = OC
            ResizeGrillaIngresados()
            Me.dtgIngresados.Focus()
        End If
  End Sub

  Private Function GetIngresados() As Boolean
        Dim Pa As SqlParameter
        Dim Ds As New DataSet
        Dim xCmd As SqlCommand
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandType = CommandType.StoredProcedure
                xCmd.CommandText = "MOB_INGRESO_OC_SEL"

                Pa = New SqlParameter("@Cliente_ID", SqlDbType.VarChar, 20)
                Pa.Value = vCliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@ORDEN_COMPRA", SqlDbType.VarChar, 100)
                Pa.Value = vOC
                xCmd.Parameters.Add(Pa)

                Da.Fill(Ds, "Ingresados")

                dtgIngresados.DataSource = Ds.Tables("Ingresados")

                If dtgIngresados.VisibleRowCount < 1 Then
                    btnEliminar.Enabled = False
                    btnAjustar.Enabled = False
                Else
                    btnEliminar.Enabled = True
                    btnAjustar.Enabled = True
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

  Private Function ResizeGrillaIngresados() As Boolean
        Try
            Dim Style As New DataGridTableStyle
            Style.MappingName = "Ingresados"
            dtgIngresados.TableStyles.Clear()

            Dim TextCol1 As New DataGridTextBoxColumn
            TextCol1.MappingName = "CANTIDAD"
            TextCol1.HeaderText = "Cantidad"
            TextCol1.Width = 60
            Style.GridColumnStyles.Add(TextCol1)
            TextCol1 = Nothing

            Dim TextCol4 As New DataGridTextBoxColumn
            TextCol4.MappingName = "CANT_CONTENEDORAS"
            TextCol4.HeaderText = "Contenedoras"
            TextCol4.Width = 85
            Style.GridColumnStyles.Add(TextCol4)
            TextCol4 = Nothing

            Dim TextCol2 As New DataGridTextBoxColumn
            TextCol2.MappingName = "PRODUCTO"
            TextCol2.HeaderText = "Producto"
            TextCol2.Width = 80
            Style.GridColumnStyles.Add(TextCol2)
            TextCol2 = Nothing

            Dim TextCol3 As New DataGridTextBoxColumn
            TextCol3.MappingName = "DESCRIPCION"
            TextCol3.HeaderText = "Descripcion"
            TextCol3.Width = 220
            Style.GridColumnStyles.Add(TextCol3)
            TextCol3 = Nothing

            Dim TextCol5 As New DataGridTextBoxColumn
            TextCol5.MappingName = "NRO_LOTE"
            TextCol5.HeaderText = "Lote Proveedor"
            TextCol5.Width = 100
            Style.GridColumnStyles.Add(TextCol5)
            TextCol5 = Nothing

            Dim TextCol6 As New DataGridTextBoxColumn
            TextCol6.MappingName = "NRO_PARTIDA"
            TextCol6.HeaderText = "Nro Partida"
            TextCol6.Width = 100
            Style.GridColumnStyles.Add(TextCol6)
            TextCol6 = Nothing

            Dim TextCol7 As New DataGridTextBoxColumn
            TextCol7.MappingName = "NRO_SERIE"
            TextCol7.HeaderText = "Nro Serie"
            TextCol7.Width = 100
            Style.GridColumnStyles.Add(TextCol7)
            TextCol7 = Nothing

            Dim TextCol8 As New DataGridTextBoxColumn
            TextCol8.MappingName = "ING_ID"
            TextCol8.HeaderText = "ING_ID"
            TextCol8.Width = 0
            Style.GridColumnStyles.Add(TextCol8)
            TextCol8 = Nothing

            dtgIngresados.TableStyles.Add(Style)
        Catch ex As Exception
            MsgBox("ResizeGrillaIngresados: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try

  End Function

  Private Sub Salir()
    Me.Close()
  End Sub
  Private Sub btnAjustar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAjustar.Click

    Try
      Me.CodProducto = dtgIngresados.Item(dtgIngresados.CurrentRowIndex, 2).ToString()
      Me.DescProd = dtgIngresados.Item(dtgIngresados.CurrentRowIndex, 3).ToString()
      Me.Qty = dtgIngresados.Item(dtgIngresados.CurrentRowIndex, 0).ToString()
      Me.Qty_Cont = Val(dtgIngresados.Item(dtgIngresados.CurrentRowIndex, 1).ToString())
      Me.Close()
    Catch ex As Exception
    Finally
      FrmRecepcionOC = Nothing
    End Try
  End Sub

  Private Sub dtgIngresados_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgIngresados.Click
    Try
      Me.dtgIngresados.Select(dtgIngresados.CurrentRowIndex)
    Catch ex As Exception
    End Try
  End Sub

  Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
    Salir()
  End Sub

  Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Me.EliminarSeleccion()
    End Sub

    Private Sub EliminarSeleccion()
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Dim Cmd As SqlCommand
        Try

            Me.CodProducto = dtgIngresados.Item(dtgIngresados.CurrentRowIndex, 2).ToString()

            Dim Rta As Object = MsgBox("¿Confirma que va a eliminar el producto " & Me.Producto_ID & "?", MsgBoxStyle.YesNo, FrmName)
            If Rta = vbYes Then
                Cmd = SQLc.CreateCommand
                Cmd.Transaction = SQLc.BeginTransaction()
                Da = New SqlDataAdapter(Cmd)
                Cmd.CommandText = "INGRESO_OC_BORRAR"
                Cmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@ING_ID", SqlDbType.BigInt)
                Pa.Value = dtgIngresados.Item(dtgIngresados.CurrentRowIndex, 7).ToString()
                Cmd.Parameters.Add(Pa)
                Pa = Nothing

                Cmd.ExecuteNonQuery()
                Cmd.Transaction.Commit()
                CodProducto = ""
                GetIngresados()
            End If
        Catch ex As Exception
            MsgBox("Error al Borrar: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub FrmConsultaRecepOC_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Me.Salir()
            Case Keys.F2
                Me.EliminarSeleccion()
        End Select
    End Sub

    Private Sub dtgIngresados_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgIngresados.GotFocus
        Try
            dtgIngresados.Select(dtgIngresados.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dtgIngresados_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtgIngresados.KeyUp
        Try
            dtgIngresados.Select(dtgIngresados.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub
End Class