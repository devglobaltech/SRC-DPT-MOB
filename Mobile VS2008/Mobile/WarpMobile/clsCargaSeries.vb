Imports System.Data
Imports System.Data.SqlClient

Public Class clsCargaSeries

    Private Const SQLConErr As String = "No se pudo conectar a la base de datos."
    Private Const FrmName As String = "Carga de Series"
    Private IdProceso As String = ""
    Private ProductoID As String = ""
    Private DSeries As New DataSet
    Private CantSeriesCont As Long = 0

    Public Property Producto() As String
        Get
            Return ProductoID
        End Get
        Set(ByVal value As String)
            ProductoID = value
        End Set
    End Property

    Public Sub FillCmb(ByRef cmbClientes As ComboBox)
        Dim Da As SqlDataAdapter
        Dim Ds As New System.Data.DataSet
        Dim drDSRow As Data.DataRow
        Dim drNewRow As Data.DataRow
        Dim dt As New Data.DataTable
        Dim xCmd As SqlCommand
        Dim Pa As New SqlParameter
        Try
            cmbClientes.DataSource = Nothing
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.GET_CLIENTES_BY_USER"
                xCmd.CommandType = Data.CommandType.StoredProcedure
                Pa = New SqlParameter("@USER", SqlDbType.VarChar, 30)
                Pa.Value = vUsr.CodUsuario
                xCmd.Parameters.Add(Pa)
                xCmd.Connection = SQLc
                Da.Fill(Ds, "CLIENTES")
                dt.Columns.Add("RazonSocial", GetType(System.String))
                dt.Columns.Add("Cliente_id", GetType(System.String))
                If Ds.Tables("CLIENTES").Rows.Count > 0 Then
                    'Hay mas de un cliente, los cargo en el combo.
                    For Each drDSRow In Ds.Tables("CLIENTES").Rows()
                        drNewRow = dt.NewRow()
                        drNewRow("RazonSocial") = drDSRow("RazonSocial")
                        drNewRow("Cliente_id") = drDSRow("Cliente_id")
                        dt.Rows.Add(drNewRow)
                    Next
                    cmbClientes.DropDownStyle = ComboBoxStyle.DropDownList
                    With cmbClientes
                        .DataSource = dt
                        .DisplayMember = "RazonSocial"
                        .ValueMember = "Cliente_id"
                        .SelectedIndex = 0
                    End With
                End If
            Else : MsgBox(SQLConErr, MsgBoxStyle.Exclamation, FrmName)
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
    End Sub

    Public Sub IniciarProcesoSeries()
        Try
            GetIDProceso(IdProceso)
            CreateDs()
            CantSeriesCont = 0
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        End Try
    End Sub

    Public Sub IniciarProcesoSeries(ByRef Trans As SqlTransaction)
        Try
            GetIDProceso(IdProceso, Trans)
            CreateDs()
            CantSeriesCont = 0
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        End Try
    End Sub

    Private Function GetIDProceso(ByRef ID As String, ByRef Trans As SqlTransaction) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.Transaction = Trans
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "dbo.GET_VALUE_FOR_SEQUENCE"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@SECUENCIA", SqlDbType.VarChar, 50)
                Pa.Value = "SEQ_CARGASERIES"
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VALUE", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                ID = xCmd.Parameters("@VALUE").Value.ToString
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Private Function GetIDProceso(ByRef ID As String) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "dbo.GET_VALUE_FOR_SEQUENCE"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@SECUENCIA", SqlDbType.VarChar, 50)
                Pa.Value = "SEQ_CARGASERIES"
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@VALUE", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                ID = xCmd.Parameters("@VALUE").Value.ToString
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function ValidarContenedora(ByVal Cliente As String, ByVal Contenedora As String) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Dim Ret As String
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.VALIDA_CONTENEDORA_SERIE"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@CLIENTE", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CONTENEDORA", SqlDbType.VarChar, 50)
                Pa.Value = Contenedora
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@RETORNO", SqlDbType.VarChar, 2)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()

                Ret = xCmd.Parameters("@RETORNO").Value
                Select Case (Ret)
                    Case "0"
                        MsgBox("La contenedora ingresada no existe.", MsgBoxStyle.Information, FrmName)
                        Return False
                    Case "1"
                        MsgBox("La contenedora ingresada ya se encuentra ubicada.", MsgBoxStyle.Information, FrmName)
                        Return False
                    Case "2"
                        MsgBox("Las series ya fueron cargadas para la contenedora.", MsgBoxStyle.Information, FrmName)
                        Return False
                    Case "3"
                        MsgBox("El producto de la contenedora no requiere serializacion al ingreso.", MsgBoxStyle.Information, FrmName)
                        Return False
                    Case "4"
                        Return True
                End Select
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function DescripcionSKU(ByVal Cliente As String, ByVal Contenedora As String, ByRef oText As TextBox) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Dim Ret As String
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.SKU_CONTENEDORA_SERIE"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@CLIENTE", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CONTENEDORA", SqlDbType.VarChar, 50)
                Pa.Value = Contenedora
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@RETORNO", SqlDbType.VarChar, 100)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 100)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()

                Ret = xCmd.Parameters("@RETORNO").Value
                Me.Producto = xCmd.Parameters("@PRODUCTO_ID").Value
                oText.Text = Ret
                oText.Enabled = False
                Return True
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function ExisteSerieDS(ByVal Cliente As String, ByVal Contenedora As String, ByVal NroSerie As TextBox, ByVal ValSerieEspecifica As Boolean, ByVal Producto As String, ByRef Cancel As Boolean) As Boolean
        Dim Row As DataRow, existe As Boolean = False, Serie As String
        Try
            Serie = NroSerie.Text
            If Not VerificarSerieMob(Cliente, Me.Producto, Contenedora, Serie, ValSerieEspecifica, True) Then
                Cancel = True
                Return False
            Else
                NroSerie.Text = Serie
            End If

            For Each Row In DSeries.Tables(0).Rows
                If NroSerie.Text = Row("SERIE").ToString Then
                    existe = True
                End If
            Next
            Return existe
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        End Try
    End Function

    Public Function ExisteSerie(ByVal Cliente As String, ByVal Contenedora As String, ByVal NroSerie As TextBox, ByVal ValSerieEspecifica As Boolean, ByVal Producto As String) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Dim Ret As String
        Dim Serie As String
        Try
            If VerifyConnection(SQLc) Then

                'If Not VerificarNroSerie(Cliente, Contenedora, NroSerie, ValSerieEspecifica, False) Then
                'Return False
                'End If
                Serie = NroSerie.Text
                If Not VerificarSerieMob(Cliente, Me.Producto, Contenedora, Serie, ValSerieEspecifica, False) Then
                    Return False
                Else
                    NroSerie.Text = Serie
                End If

                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "dbo.ValidaSerieTomada"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@IDPROCESO", SqlDbType.BigInt)
                Pa.Value = IdProceso
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_CONTENEDORA", SqlDbType.VarChar, 100)
                Pa.Value = Contenedora
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@SERIE", SqlDbType.VarChar, 50)
                Pa.Value = Serie
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@RETORNO", SqlDbType.VarChar, 100)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()

                Ret = xCmd.Parameters("@RETORNO").Value

                If Ret = "1" Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function EliminarSerieDS(ByVal Cliente As String, ByVal Contenedora As String, ByVal NroSerie As String, ByVal ValSerieEspecifica As Boolean) As Boolean
        Dim i As Long = 0
        Try
            For i = 0 To DSeries.Tables(0).Rows.Count - 1
                If DSeries.Tables(0).Rows(i)(4).ToString = NroSerie Then
                    DSeries.Tables(0).Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        End Try
    End Function

    Public Function EliminarSerie(ByVal Cliente As String, ByVal Contenedora As String, ByVal NroSerie As String, ByVal ValSerieEspecifica As Boolean) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then

                If Not VerificarNroSerie(Cliente, Contenedora, NroSerie, ValSerieEspecifica, False) Then
                    Return False
                End If
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "dbo.EliminarSerieTomada"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@IDPROCESO", SqlDbType.BigInt)
                Pa.Value = IdProceso
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_CONTENEDORA", SqlDbType.VarChar, 100)
                Pa.Value = Contenedora
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@SERIE", SqlDbType.VarChar, 50)
                Pa.Value = NroSerie
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()

                Return True
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function EliminarSerie(ByVal Cliente As String, ByVal Contenedora As String) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "dbo.EliminarSeriesTomadas"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@IDPROCESO", SqlDbType.BigInt)
                Pa.Value = IdProceso
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_CONTENEDORA", SqlDbType.VarChar, 100)
                Pa.Value = Contenedora
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()

                Return True
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function CreateDs() As Boolean
        Dim Tbl As New DataTable
        Try
            DSeries = Nothing
            DSeries = New DataSet
            Tbl.TableName = "SERIES"
            Tbl.Columns.Add("IDPROCESO", Type.GetType("System.String"))
            Tbl.Columns.Add("CLIENTE", Type.GetType("System.String"))
            Tbl.Columns.Add("NRO_BULTO", Type.GetType("System.String"))
            Tbl.Columns.Add("PRODUCTO_ID", Type.GetType("System.String"))
            Tbl.Columns.Add("SERIE", Type.GetType("System.String"))
            DSeries.Tables.Add(Tbl)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        End Try
    End Function

    Public Function GuardarSerieDS(ByVal Cliente As String, ByVal Contenedora As String, ByVal NroSerie As String, ByVal ValSerieEspecifica As Boolean) As Boolean
        Try
            Dim newSerieRow As DataRow = DSeries.Tables("SERIES").NewRow()
            'Doy balance a la fuerza completando los campos
            newSerieRow("IDPROCESO") = Me.IdProceso
            newSerieRow("CLIENTE") = Cliente
            newSerieRow("NRO_BULTO") = Contenedora
            newSerieRow("PRODUCTO_ID") = Me.Producto
            newSerieRow("SERIE") = NroSerie
            'agrego el registro al dataset.
            DSeries.Tables("SERIES").Rows.Add(newSerieRow)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        End Try
    End Function

    Private Function GuardarSerie(ByVal Cliente As String, ByVal Contenedora As String, ByVal NroSerie As String, ByVal ValSerieEspecifica As Boolean, ByRef Trans As SqlTransaction) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then

                'If Not VerificarNroSerie(Cliente, Contenedora, NroSerie, ValSerieEspecifica) Then
                'Return False
                'End If

                xCmd = SQLc.CreateCommand
                xCmd.Transaction = Trans
                Da = New SqlDataAdapter(xCmd)

                xCmd.CommandText = "DBO.MOB_INSERTARSERIELOG"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@IDPROCESO", SqlDbType.BigInt)
                Pa.Value = IdProceso
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_BULTO", SqlDbType.VarChar, 100)
                Pa.Value = Contenedora
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 50)
                Pa.Value = Me.Producto
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@SERIE", SqlDbType.VarChar, 50)
                Pa.Value = NroSerie
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@TERMINAL", SqlDbType.VarChar, 50)
                Pa.Value = "TERMINAL"
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 50)
                Pa.Value = vUsr.CodUsuario
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@ARCHIVO", SqlDbType.VarChar, 50)
                Pa.Value = "MANUAL"
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()

                Return True
            End If
        Catch sqlex As SqlException
            Return False
        Catch ex As Exception
            Return False
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Private Function GuardarSerie(ByVal Cliente As String, ByVal Contenedora As String, ByVal NroSerie As String, ByVal ValSerieEspecifica As Boolean) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then

                'If Not VerificarNroSerie(Cliente, Contenedora, NroSerie, ValSerieEspecifica) Then
                'Return False
                'End If

                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.MOB_INSERTARSERIELOG"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@IDPROCESO", SqlDbType.BigInt)
                Pa.Value = IdProceso
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_BULTO", SqlDbType.VarChar, 100)
                Pa.Value = Contenedora
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@PRODUCTO_ID", SqlDbType.VarChar, 50)
                Pa.Value = Me.Producto
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@SERIE", SqlDbType.VarChar, 50)
                Pa.Value = NroSerie
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@TERMINAL", SqlDbType.VarChar, 50)
                Pa.Value = "TERMINAL"
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 50)
                Pa.Value = vUsr.CodUsuario
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@ARCHIVO", SqlDbType.VarChar, 50)
                Pa.Value = "MANUAL"
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()

                Return True
            End If
        Catch sqlex As SqlException
            Return False
        Catch ex As Exception
            Return False
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function GetSeries(ByVal Cliente As String, ByVal Contenedora As String, ByRef Grid As DataGrid) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter, Ds As New DataSet
        Dim xCmd As SqlCommand
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.GET_SERIES_TOMADAS"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@IDPROCESO", SqlDbType.BigInt)
                Pa.Value = IdProceso
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_CONTENEDORA", SqlDbType.VarChar, 100)
                Pa.Value = Contenedora
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Da.Fill(Ds, "SERIES")
                Grid.DataSource = Ds.Tables("SERIES")
                Grid.Visible = True
                ResizeGrilla(Grid)
                Return True
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function GetSeriesDS(ByVal Cliente As String, ByVal Contenedora As String, ByRef Grid As DataGrid) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter, Ds As New DataSet
        Dim xCmd As SqlCommand
        Try
            Grid.DataSource = Nothing

            BuildSeries(Ds)
            Grid.DataSource = Ds.Tables(0)
            ResizeGrilla(Grid)
            Grid.Visible = True
            Return True

        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Private Function BuildSeries(ByRef Ds As DataSet) As Boolean
        Dim Row As DataRow, Tbl As New DataTable, newSerieRow As DataRow

        Try
            Ds = Nothing
            Ds = New DataSet
            Tbl.TableName = "SERIES"
            Tbl.Columns.Add("SERIE", Type.GetType("System.String"))
            Ds.Tables.Add(Tbl)
            For Each Row In DSeries.Tables(0).Rows
                newSerieRow = Ds.Tables("SERIES").NewRow()
                newSerieRow("SERIE") = Row("SERIE").ToString

                Ds.Tables(0).Rows.Add(newSerieRow)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        End Try
    End Function

    Private Function ResizeGrilla(ByRef Dg As DataGrid) As Boolean
        Try

            Dim Style As New DataGridTableStyle
            Style.MappingName = "SERIES"
            Dg.TableStyles.Clear()
            Style.MappingName = "SERIES"

            Dim TextCol1 As New DataGridTextBoxColumn
            With TextCol1
                .MappingName = "SERIE"
                .HeaderText = "Nro. de Serie"
                .Width = 200
            End With
            Style.GridColumnStyles.Add(TextCol1)


            Dg.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("ResizeGrillaCodigoProducto: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Function

    Public Function SeriesPendientes(ByRef cmb As ComboBox, ByRef oTextCont As TextBox) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter, Ds As New DataSet
        Dim xCmd As SqlCommand, msg As String = ""
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.SeriesNoConfirmadas"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@USUARIO", SqlDbType.VarChar, 100)
                Pa.Value = vUsr.CodUsuario
                xCmd.Parameters.Add(Pa)

                Da.Fill(Ds, "SNOC")
                If Ds.Tables("SNOC").Rows.Count > 0 Then
                    msg = "El usuario posee series sin confirmar para la contenedora " & Ds.Tables("SNOC").Rows(0)(2).ToString & "." & vbNewLine & "¿Desea continuar con la carga de series para esta contenedora?"
                    If (MsgBox(msg, MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes) Then
                        IdProceso = Ds.Tables("SNOC").Rows(0)(0).ToString
                        cmb.Text = Ds.Tables("SNOC").Rows(0)(1).ToString
                        oTextCont.Text = Ds.Tables("SNOC").Rows(0)(2).ToString
                        Return True
                    Else
                        IdProceso = Ds.Tables("SNOC").Rows(0)(0).ToString
                        EliminarSerie(Ds.Tables("SNOC").Rows(0)(1).ToString, Ds.Tables("SNOC").Rows(0)(2).ToString)
                        Return False
                    End If
                End If
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Private Function VerificarSerieMob(ByVal Cliente As String, ByVal Producto As String, ByVal Contenedora As String, ByRef NroSerie As String, ByVal SerieEspecifica As Boolean, Optional ByVal ShowError As Boolean = True) As Boolean
        Dim LenSerie As Long = 0
        Dim DIGITO As String = "", SKUD As String = "", SERIED As String = "", SKU2 As String = ""
        Try
            If SerieEspecifica Then
                '----------------------------------------
                'Quito los guiones.
                '----------------------------------------
                Producto = Replace(Producto, "-", "")
                NroSerie = Replace(NroSerie, "-", "")
                LenSerie = Len(NroSerie)
                '----------------------------------------
                'Longitud de cadena.
                '----------------------------------------
                If LenSerie <> 16 And LenSerie <> 20 Then
                    Throw New Exception("2")
                End If
                '----------------------------------------
                'Digito Verificador.
                '----------------------------------------
                Digito = Mid(NroSerie, 1, 2)
                If (Digito <> "1S") Then
                    Throw New Exception("1")
                End If
                '----------------------------------------
                'Caso 2.
                '----------------------------------------
                If (LenSerie = 16) Then
                    SKUD = Mid(NroSerie, 3, 7)
                    SERIED = Mid(NroSerie, 10, 7)
                    If Producto <> SKUD Then
                        Throw New Exception("4")
                    End If
                End If

                If (LenSerie = 20) Then
                    SKU2 = Mid(NroSerie, 3, 2)
                    If ((SKU2 = "10") Or (SKU2 = "20") Or (SKU2 = "60") Or (SKU2 = "70")) Then
                        'Caso 4.
                        SKUD = Mid(NroSerie, 3, 10)
                        If (Producto <> SKUD) Then
                            Throw New Exception("4")
                        End If

                        SERIED = Mid(NroSerie, 13, LenSerie)
                    Else
                        'Caso 3.
                        SKUD = Mid(NroSerie, 3, 8)
                        If (Producto <> SKUD) Then
                            Throw New Exception("4")
                        End If
                        SERIED = Mid(NroSerie, 11, LenSerie)
                    End If
                End If
                NroSerie = SERIED
            End If
            return True 
        Catch ex As Exception
            Select Case ex.Message
                Case "1"
                    If ShowError Then
                        MsgBox("La serie escaneada no es correcta.", MsgBoxStyle.Information, FrmName)
                        Return False
                    End If
                Case "2"
                    If ShowError Then
                        MsgBox("La longitud de la serie tomada es incorrecta. ", MsgBoxStyle.Information, FrmName)
                        Return False
                    End If
                Case "3"
                    If ShowError Then
                        MsgBox("La serie escaneada ya fue ingresada. ", MsgBoxStyle.Information, FrmName)
                        Return False
                    End If
                Case "4"
                    If ShowError Then
                        MsgBox("La serie escaneada no se corresponde con el Codigo de producto de la contendora.", MsgBoxStyle.Information, FrmName)
                        Return False
                    End If
            End Select
            Return False
        End Try
    End Function

    Private Function VerificarNroSerie(ByVal Cliente As String, ByVal Contenedora As String, ByRef NroSerie As String, ByVal SerieEspecifica As Boolean, Optional ByVal ShowError As Boolean = True)
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Dim Ret As String
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.VALIDACION_SERIE"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@IDPROCESO", SqlDbType.BigInt)
                Pa.Value = IdProceso
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_BULTO", SqlDbType.VarChar, 100)
                Pa.Value = Contenedora
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_SERIE", SqlDbType.VarChar, 50)
                Pa.Value = NroSerie
                Pa.Direction = ParameterDirection.InputOutput
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@SERIE_ESP", SqlDbType.VarChar, 1)
                If SerieEspecifica Then
                    Pa.Value = "1"
                Else
                    Pa.Value = "0"
                End If
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@RETORNO", SqlDbType.VarChar, 2)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()

                Ret = xCmd.Parameters("@RETORNO").Value
                Select Case Ret
                    Case "0"
                        NroSerie = xCmd.Parameters("@NRO_SERIE").Value
                        Return True
                    Case "1"
                        If ShowError Then
                            MsgBox("La serie escaneada no es correcta.", MsgBoxStyle.Information, FrmName)
                            Return False
                        End If
                    Case "2"
                        If ShowError Then
                            MsgBox("La longitud de la serie tomada es incorrecta. ", MsgBoxStyle.Information, FrmName)
                            Return False
                        End If
                    Case "3"
                        If ShowError Then
                            MsgBox("La serie escaneada ya fue ingresada. ", MsgBoxStyle.Information, FrmName)
                            Return False
                        End If
                    Case "4"
                        If ShowError Then
                            MsgBox("La serie escaneada no se corresponde con el Codigo de producto de la contendora.", MsgBoxStyle.Information, FrmName)
                            Return False
                        End If
                End Select
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function VerificacionSeriesCargadasDS(ByVal Cliente As String, ByVal Contenedora As String, Optional ByRef Cantidad As String = "1") As Boolean
        Dim ret As Boolean = True
        Try
            If (DSeries.Tables(0).Rows.Count = Me.CantSeriesCont) Then
                ret = False
            Else
                ret = True
            End If
            Return ret
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        End Try
    End Function

    Public Function VerificacionSeriesCargadas(ByVal Cliente As String, ByVal Contenedora As String, Optional ByRef Cantidad As String = "1") As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Dim Ret As String
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.CANT_SERIES_PENDIENTES"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@IDPROCESO", SqlDbType.BigInt)
                Pa.Value = IdProceso
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_BULTO", SqlDbType.VarChar, 100)
                Pa.Value = Contenedora
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CANTIDAD", SqlDbType.BigInt)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()

                Ret = xCmd.Parameters("@CANTIDAD").Value
                CantSeriesCont = CLng(Ret)
                If Cantidad = "" Then
                    Cantidad = Ret
                End If

                If Ret = 0 Then
                    Return False
                Else
                    Return True
                End If
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Private Function CreateDsError(ByRef Ds As DataSet) As Boolean
        Dim Tbl As New DataTable
        Try
            Ds = Nothing
            Ds = New DataSet
            Tbl.TableName = "SERIES"
            Tbl.Columns.Add("SERIE", Type.GetType("System.String"))
            Ds.Tables.Add(Tbl)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        End Try
    End Function

    Public Function GuardarSerieDSError(ByRef DS As DataSet, ByVal NroSerie As String) As Boolean
        Try
            Dim newSerieRow As DataRow = DS.Tables("SERIES").NewRow()

            newSerieRow("SERIE") = NroSerie
            'agrego el registro al dataset.
            DS.Tables("SERIES").Rows.Add(newSerieRow)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, FrmName)
        End Try
    End Function


    Public Function ConfirmarSeriesDS(ByRef DSerieError As DataSet, ByRef Trans As SqlTransaction) As Boolean
        Try
            CreateDsError(DSerieError)
            '1. GUARDO TODO EL DATASET EN LA BASE DE DATOS.

            For Each ROW As DataRow In DSeries.Tables(0).Rows
                If Not Me.GuardarSerie(ROW("CLIENTE").ToString, ROW("NRO_BULTO").ToString, ROW("SERIE").ToString().PadLeft(9, "0"), False, Trans) Then
                    'ACA TENGO QUE METER EL CONTROL PARA QUE ME MUESTRE LAS SERIES ERRONEAS.
                    GuardarSerieDSError(DSerieError, ROW("SERIE").ToString().PadLeft(0, "0"))
                End If
            Next
            If (DSerieError.Tables(0).Rows.Count = 0) Then
                Me.ConfirmarSeries(Trans)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
            Return False
        End Try
    End Function

    Public Function ConfirmarSeriesDS(ByRef DSerieError As DataSet) As Boolean
        Try
            CreateDsError(DSerieError)
            '1. GUARDO TODO EL DATASET EN LA BASE DE DATOS.

            For Each ROW As DataRow In DSeries.Tables(0).Rows
                If Not Me.GuardarSerie(ROW("CLIENTE").ToString, ROW("NRO_BULTO").ToString, ROW("SERIE").ToString, False) Then
                    'ACA TENGO QUE METER EL CONTROL PARA QUE ME MUESTRE LAS SERIES ERRONEAS.
                    GuardarSerieDSError(DSerieError, ROW("SERIE").ToString)
                End If
            Next
            If (DSerieError.Tables(0).Rows.Count = 0) Then
                Me.ConfirmarSeries()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
            Return False
        End Try
    End Function

    Public Function ConfirmarSeries(ByRef Trans As SqlTransaction) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand

        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                xCmd.Transaction = Trans
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.RealizarCargaDeSeriesIng"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@IDPROCESO", SqlDbType.BigInt)
                Pa.Value = IdProceso
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()
                Return True
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function ConfirmarSeries() As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand

        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.RealizarCargaDeSeries"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@IDPROCESO", SqlDbType.BigInt)
                Pa.Value = IdProceso
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()
                Return True
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function IndicadorSeries(ByVal Cliente As String, ByVal Contenedora As String, ByRef oLabel As Label) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Dim Ret As String
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.CANT_SERIES_PENDIENTES_LEYENDA"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@IDPROCESO", SqlDbType.BigInt)
                Pa.Value = IdProceso
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CLIENTE_ID", SqlDbType.VarChar, 15)
                Pa.Value = Cliente
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@NRO_BULTO", SqlDbType.VarChar, 100)
                Pa.Value = Contenedora
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@CANTIDAD", SqlDbType.VarChar, 50)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                If CantSeriesCont = 0 Then
                    xCmd.ExecuteNonQuery()
                    Ret = xCmd.Parameters("@CANTIDAD").Value
                    CantSeriesCont = Ret
                Else
                    Ret = CantSeriesCont
                End If
                oLabel.Visible = True
                oLabel.Text = "Serie: " & Me.DSeries.Tables(0).Rows.Count & "/" & Ret
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function

    Public Function MuestraBtnSerieEspecifica(ByVal Contenedora As String) As Boolean
        Dim Pa As New SqlParameter
        Dim Da As SqlDataAdapter
        Dim xCmd As SqlCommand
        Dim Ret As String
        Try
            If VerifyConnection(SQLc) Then
                xCmd = SQLc.CreateCommand
                Da = New SqlDataAdapter(xCmd)
                xCmd.CommandText = "DBO.GET_SERIE_ESPECIFICA"
                xCmd.CommandType = Data.CommandType.StoredProcedure

                Pa = New SqlParameter("@CONTENEDORA", SqlDbType.VarChar, 100)
                Pa.Value = Contenedora
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@OUT", SqlDbType.VarChar, 15)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                xCmd.ExecuteNonQuery()

                Ret = xCmd.Parameters("@OUT").Value
                If Ret = "1" Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Exclamation, FrmName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, FrmName)
        Finally
            Pa = Nothing
            Da = Nothing
            xCmd = Nothing
        End Try
    End Function
End Class
