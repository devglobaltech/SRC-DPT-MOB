Imports System.IO
Imports System.Data.SqlClient
Imports System
Imports System.Reflection
Imports System.Reflection.Assembly
Imports System.Threading
Imports System.Diagnostics.Process
Imports System.Data

Module ModuloGral

    Public Const vVersionHH As String = "9.9.0.0"
    Public sMenu As String = ""
    Public WithEvents ScA As New AsynchronousClient
    Public SistemaOperativo As String = ""
    Public o2D As New clsDecode2D
    Public ScreenSize As Integer = 0

    Public Structure Usuario
        Dim CodUsuario As String
        Dim Nombre As String
        Dim Vehiculo As String
        Dim NaveCalle As String
        Dim ClienteActivo As String
    End Structure

    Sub Main()
        Dim callingDomainName As String = Thread.GetDomain().FriendlyName
        Dim exeAssembly As String = Assembly.GetExecutingAssembly().FullName
    End Sub

    Public Sub ProductoInhabilidato(ByVal Cliente As String, ByVal Producto As String, ByRef Inactivo As Boolean)
        Dim SQL As String = "", CMD As SqlCommand, DA As SqlDataAdapter, DS As New DataSet
        Try
            If VerifyConnection(SQLc) Then
                CMD = SQLc.CreateCommand
                CMD.CommandType = CommandType.Text
                DA = New SqlDataAdapter(CMD)
                SQL = "SELECT ISNULL(INACTIVO,'0') AS RESULT FROM PRODUCTO WHERE CLIENTE_ID='" & Cliente & "' AND PRODUCTO_ID='" & Producto & "'"
                CMD.CommandText = SQL

                DA.Fill(DS, "PRODUCTO")
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        Inactivo = IIf(DS.Tables(0).Rows(0)(0) = "1", True, False)
                        Return
                    End If
                End If
            Else : MsgBox("No fue posible restablecer la conexion con la base de datos.", MsgBoxStyle.Exclamation, "Depot WMS")
                Return
            End If
        Catch SQLEx As SqlException
            MsgBox(SQLEx.Message, MsgBoxStyle.OkOnly, "Depot WMS")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Depot WMS")
        Finally
            CMD.Dispose()
            DA.Dispose()
            DS.Dispose()
        End Try
    End Sub

    Public Sub AutoSizeGrid(ByRef Grid As DataGrid, ByVal frmName As String)
        Dim Style As New DataGridTableStyle
        Dim Ds As New DataSet
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0

        Dim Obj As DataTable, valueStr As String = ""
        Dim LongAct As Integer = 0, Obj2 As DataTable
        Dim LongMax As Integer = 0, MaxData As Integer = 0
        Dim CstMult As Integer = 8 'Multiplico el Length por este valor para q quede todo bien.
        Try
            If ScreenSize = 480 Then
                CstMult = 16
            End If
            Obj = Grid.DataSource
            '==============================================
            'Seteo el mismo style del tablename
            '==============================================
            Style.MappingName = Obj.TableName
            i = 0
            For i = 0 To Obj.Columns.Count - 1
                LongAct = 0
                LongMax = 0
                For j = 0 To Obj.Rows.Count - 1
                    LongAct = Len(Trim(Obj.Rows(j)(i).ToString))
                    If LongAct > LongMax Then LongMax = LongAct
                Next
                '==============================================
                'desde aca tengo el maximo para una columna i.
                '==============================================
                Dim TextCol As New DataGridTextBoxColumn
                With TextCol
                    .MappingName = Obj.Columns(i).ColumnName
                    .HeaderText = Obj.Columns(i).ColumnName
                    '============================================================================================
                    'Esto esta porque si el dato es mas chico que el nombre de la columna me corta la columna
                    '============================================================================================
                    If Len(Obj.Columns(i).ColumnName) > LongMax Then LongMax = Len(Obj.Columns(i).ColumnName)
                    '============================================================================================
                    .Width = LongMax * CstMult
                End With
                Style.GridColumnStyles.Add(TextCol)
                TextCol = Nothing
                '==============================================
            Next
            '==============================================
            'para terminar le pongo el style a la grilla.
            '==============================================
            Grid.TableStyles.Clear() 'Le saco el estilo para que no reviente.
            Grid.TableStyles.Add(Style)
            '==============================================
        Catch ex As Exception
        End Try
    End Sub

    Public CerrarAplicacion As Boolean = False
    Public vUsr As Usuario
    Public SQLc As New Data.SqlClient.SqlConnection
    Public IDControl As Long
    Public Server As String
    Public bPermiso As Permisos
    Public TrdLic As Thread

    Public Structure Permisos
        Dim Stock_Pallet As Boolean
        Dim Stock_Ubicacion As Boolean
        Dim Stock_Producto As Boolean
        Dim Ubicacion_Mercaderia As Boolean
        Dim Ubicacion_Supervisor As Boolean
        Dim Ingreso_Viajes As Boolean
        Dim control_picking As Boolean
        Dim Transferencia_Manual As Boolean
        Dim Picking As Boolean
        Dim Trans_Desconsolidada As Boolean
        Dim PickingPalletCompleto As Boolean
        Dim RecepcionODC As Boolean
        Dim TransGuiada As Boolean
        Dim APF As Boolean
        Dim TransfPicking As Boolean
    End Structure

    Public Function GetConfig(ByRef Server As String, ByRef Err As String) As Boolean
        Try
            Dim oFile As New StreamReader("\Flash Disk\Warp\Config.dat")
            'Dim oFile As New StreamReader("\Flash Disk\Warp\Config.dat")
            Dim Line As String
            Line = oFile.ReadLine
            oFile.Close()
            If Line.Length > 0 Then
                Server = Line
                Return True
            Else
                Err = "No hay parametros en el archivo de Configuracion."
                Return False
            End If
        Catch ex As Exception
            Err = ex.Message
            Return False
        End Try
    End Function

    Public Function VerifyConnection(ByRef Cnx As Data.SqlClient.SqlConnection) As Boolean
        Dim Cmd As SqlCommand
        Try
            Dim Rta As Object
            Dim Cont As Integer = 0
            'Try
            '    If (ScA.SocketConnected) Or (Not ScA.SocketConnected) Then
            '        ScA.Actividad(False)
            '    End If
            'Catch ex As Exception
            'End Try

            'If CerrarAplicacion Then
            '    Try
            '        TrdLic.Abort()
            '    Catch ex As Exception
            '    End Try
            '    Application.Exit()
            'End If
            If Cnx.State = Data.ConnectionState.Broken Then
                Rta = MsgBox("La conexion no esta activa. Se intentara reconectar." & vbNewLine & "Desea Continuar? ")
                If Rta Then
                    'Try
                    '    If Not ScA.SocketConnected Then
                    '        ScA.Actividad()
                    '    End If
                    'Catch ex As Exception
                    'End Try

                    Cnx.Close()
                    Do Until Cnx.State = Data.ConnectionState.Open Or Cont = 3
                        Cnx.Open()
                        Cont = Cont + 1
                    Loop
                    If (Cnx.State = Data.ConnectionState.Closed Or Cnx.State = Data.ConnectionState.Broken) And Cont = 3 Then
                        Return False
                    End If
                    Cmd = Cnx.CreateCommand
                    SetConnection(Cmd)
                    CreateTemporales(Cmd)
                End If
            ElseIf Cnx.State = Data.ConnectionState.Closed Then
                Rta = MsgBox("La conexion no esta activa. Se intentara reconectar." & vbNewLine & "Desea Continuar? ", MsgBoxStyle.YesNo)
                If Rta Then
                    'Try
                    '    If Not ScA.SocketConnected Then
                    '        ScA.Actividad(False)
                    '    End If
                    'Catch ex As Exception
                    'End Try

                    Cnx.Close()
                    Do Until Cnx.State = Data.ConnectionState.Open Or Cont = 3
                        Cnx.Open()
                        Cont = Cont + 1
                    Loop
                    If (Cnx.State = Data.ConnectionState.Closed Or Cnx.State = Data.ConnectionState.Broken) And Cont = 3 Then
                        Return False
                    End If
                    Cmd = Cnx.CreateCommand
                    SetConnection(Cmd)
                    CreateTemporales(Cmd)
                End If
            ElseIf Cnx.State = Data.ConnectionState.Open Then
                Return True
            End If
            Return True
        Catch ex As Exception
            MsgBox("Fallo al verificar la conexion. " & ex.Message)
            Cmd = Nothing
            Return False
        End Try
    End Function

    Public Function KillForm(ByVal iForm As Integer) As Boolean
        'Este metodo es el encargado de hacer el Kill definitivo de los formularios.
        Try
            Select Case iForm
                Case 1
                    Dim Fl As New frmLoggin
                    Fl.Close()
                    Fl = Nothing

            End Select
        Catch ex As Exception
            MsgBox("Fallo al cerrar Form(KillForm) " & ex.Message)
        End Try
    End Function

    Public Sub ValidarCaracterNumerico(ByRef e As System.Windows.Forms.KeyPressEventArgs)
        Try
            'Valida que el caracter ingreado sea un nro
            If (Asc(e.KeyChar) >= 32 And Asc(e.KeyChar) <= 47) Or Asc(e.KeyChar) >= 58 Then
                e.Handled = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function SetConnection(ByVal Cmd As SqlCommand) As Boolean
        Try
            Cmd.Parameters.Clear()
            Dim xSQL As String = "set language Español"
            Cmd.CommandText = xSQL
            Cmd.CommandType = Data.CommandType.Text
            Cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("SetConnection: " & ex.Message, MsgBoxStyle.OkOnly, "Modulo General")
        End Try
    End Function

    Public Function CreateTemporales(ByVal Cmd As SqlCommand) As Boolean
        Dim xSQL As String = ""
        Dim strCollate As String = ""
        Cmd.CommandType = Data.CommandType.Text
        strCollate = "COLLATE SQL_Latin1_General_CP1_CI_AS"

        xSQL = "Begin "
        xSQL = xSQL & " CREATE TABLE #temp_saldos_catlog (" & vbNewLine
        xSQL = xSQL & " cliente_id     VARCHAR(15)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " producto_id    VARCHAR(30)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " cat_log_id     VARCHAR(50)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " cantidad       NUMERIC(20,5) NOT NULL," & vbNewLine
        xSQL = xSQL & " categ_stock_id VARCHAR(15)    " & strCollate & " NULL," & vbNewLine
        xSQL = xSQL & " est_merc_id    VARCHAR(15)    " & strCollate & " NULL" & vbNewLine
        xSQL = xSQL & " )"
        xSQL = xSQL & vbNewLine
        'Cmd.CommandText = xSQL
        'Cmd.ExecuteNonQuery()

        xSQL = xSQL & " CREATE TABLE #temp_saldos_stock (" & vbNewLine
        xSQL = xSQL & " cliente_id  VARCHAR(15)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " producto_id VARCHAR(30)    " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " cant_tr_ing NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " cant_stock  NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " cant_tr_egr NUMERIC(20,5) NULL" & vbNewLine
        xSQL = xSQL & " )" & vbNewLine
        'xSQL = xSQL & vbNewLine
        'Cmd.CommandText = xSQL
        'Cmd.ExecuteNonQuery()

        xSQL = xSQL & "CREATE TABLE #temp_usuario_loggin (" & vbNewLine
        xSQL = xSQL & " usuario_id            VARCHAR(20)  " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " terminal              VARCHAR(100)  " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " fecha_loggin          DATETIME     ," & vbNewLine
        xSQL = xSQL & " session_id            VARCHAR(60)  " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " rol_id                VARCHAR(5)  " & strCollate & " NOT NULL," & vbNewLine
        xSQL = xSQL & " emplazamiento_default VARCHAR(15)  " & strCollate & " NULL," & vbNewLine
        xSQL = xSQL & " deposito_default      VARCHAR(15)  " & strCollate & " NULL " & vbNewLine
        xSQL = xSQL & " )" & vbNewLine
        xSQL = xSQL & " CREATE TABLE #temp_rl_existencia_doc ( " & vbNewLine
        xSQL = xSQL & " rl_id NUMERIC(20,5) NULL " & vbNewLine
        xSQL = xSQL & " ) " & vbNewLine

        xSQL = xSQL & " CREATE TABLE #temp_existencia_locator_rl (" & vbNewLine
        xSQL = xSQL & " rl_id             NUMERIC(20,0)  NULL," & vbNewLine
        xSQL = xSQL & " clienteid         VARCHAR(15)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " productoid        VARCHAR(30)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " cantidad          NUMERIC(20,5)  NULL," & vbNewLine
        xSQL = xSQL & " nro_serie         VARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_lote          VARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " fecha_vencimiento DATETIME       NULL," & vbNewLine
        xSQL = xSQL & " nro_despacho      VARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_bulto         VARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_partida       VARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " peso              NUMERIC(20,5)  NULL," & vbNewLine
        xSQL = xSQL & " volumen           NUMERIC(20,5)  NULL," & vbNewLine
        xSQL = xSQL & " cat_log_id        VARCHAR(50)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop1             VARCHAR(100)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop2             VARCHAR(100)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop3             VARCHAR(100)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " fecha_cpte        DATETIME       NULL," & vbNewLine
        xSQL = xSQL & " fecha_alta_gtw    DATETIME       NULL," & vbNewLine
        xSQL = xSQL & " unidad_id         VARCHAR(5)      COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " unidad_peso       VARCHAR(5)      COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " unidad_volumen    VARCHAR(5)      COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " est_merc_id       VARCHAR(15)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " moneda_id         VARCHAR(20)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " costo             NUMERIC(10,3)  NULL" & vbNewLine
        xSQL = xSQL & " )" & vbNewLine

        xSQL = xSQL & " CREATE TABLE #temp_existencia_doc (" & vbNewLine
        xSQL = xSQL & " clienteid         VARCHAR(15)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " productoid        VARCHAR(30)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " cantidad          NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " nro_serie         VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_lote          VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " fecha_vencimiento DATETIME      NULL," & vbNewLine
        xSQL = xSQL & " nro_despacho      VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_bulto         VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_partida       VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " peso              NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " volumen           NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " tie_in            CHAR(1)        COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " cantidad_disp     NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " code              CHAR(1)        COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " description       VARCHAR(100)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " cat_log_id        VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop1             VARCHAR(100)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop2             VARCHAR(100)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop3             VARCHAR(100)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " unidad_id         VARCHAR(5)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " unidad_peso       VARCHAR(5)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " unidad_volumen    VARCHAR(5)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " est_merc_id       VARCHAR(15)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " moneda_id         VARCHAR(20)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & "  costo             NUMERIC(10,3) NULL," & vbNewLine
        xSQL = xSQL & " orden             NUMERIC(20,0) NULL" & vbNewLine
        xSQL = xSQL & " )" & vbNewLine

        xSQL = xSQL & " CREATE TABLE #temp_existencia_locator (" & vbNewLine
        xSQL = xSQL & " clienteid         VARCHAR(15)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " productoid        VARCHAR(30)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " cantidad          NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " nro_serie         VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_lote          VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " fecha_vencimiento DATETIME      NULL," & vbNewLine
        xSQL = xSQL & " nro_despacho      VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_bulto         VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " nro_partida       VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " peso              NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " volumen           NUMERIC(20,5) NULL," & vbNewLine
        xSQL = xSQL & " cat_log_id        VARCHAR(50)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop1             VARCHAR(100)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop2             VARCHAR(100)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " prop3             VARCHAR(100)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " fecha_cpte        DATETIME      NULL," & vbNewLine
        xSQL = xSQL & " fecha_alta_gtw    DATETIME      NULL," & vbNewLine
        xSQL = xSQL & " unidad_id         VARCHAR(5)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " unidad_peso       VARCHAR(5)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " unidad_volumen    VARCHAR(5)     COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " est_merc_id       VARCHAR(15)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " moneda_id         VARCHAR(20)    COLLATE SQL_Latin1_General_CP1_CI_AS NULL," & vbNewLine
        xSQL = xSQL & " costo             NUMERIC(10,3) NULL" & vbNewLine
        xSQL = xSQL & " )" & vbNewLine


        xSQL = xSQL & "Exec Funciones_loggin_Api#Registra_usuario_loggin '" & vUsr.CodUsuario & "'" & vbNewLine

        xSQL = xSQL & "End"
        Cmd.CommandText = xSQL
        Cmd.ExecuteNonQuery()
        Return True
    End Function

    Public Function EsFraccionable(ByVal Cliente_ID As String, ByVal Producto_Id As String, ByRef xBool As Boolean, ByRef Cnx As SqlConnection) As Boolean
        Dim xDs As New Data.DataSet
        Dim xDa As SqlDataAdapter
        Dim xCmd As SqlCommand
        Dim xSql As String = ""
        Try
            xCmd = Cnx.CreateCommand
            xDa = New SqlDataAdapter(xCmd)
            xSql = "Select isnull(Fraccionable,'0') from producto where cliente_id='" & Cliente_ID & "' and producto_id='" & Producto_Id & "'"
            xCmd.CommandText = xSql
            xCmd.CommandType = Data.CommandType.Text
            xDa.Fill(xDs, "Temp")
            xBool = IIf(xDs.Tables("Temp").Rows(0)(0) = "1", True, False)
            Return True
        Catch SqlEx As SqlException
            MsgBox("Fallo en funcion EsFraccionable. SQL - " & SqlEx.Message, MsgBoxStyle.OkOnly, "EsFraccionable")
            Return False
        Catch ex As Exception
            MsgBox("Fallo en funcion EsFraccionable. - " & ex.Message, MsgBoxStyle.OkOnly, "EsFraccionable")
            Return False
        Finally
            xCmd = Nothing
            xDs = Nothing
            xDa = Nothing
        End Try
    End Function

    Public Function AppPath(Optional ByVal backSlash As Boolean = False) As String
        Dim path As String = ""
        path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        path = Replace(path, "file:\", "")
        If backSlash Then
            path &= "\"
        End If
        Return path
    End Function

    Private Sub ScA_Validado(ByVal value As Boolean) Handles ScA.Validado
        If Not value Then
            Thread.Sleep(2000)
            frmMenuPrincipal.Hide()
            CerrarAplicacion = True
        End If
    End Sub

    Function CerrarSistemaOperativo() As Boolean
        Try
            Dim SearchSO As String = "CE"
            Dim MyPos As Integer = 0
            SistemaOperativo = Environment.OSVersion.ToString
            'MsgBox("SO: " & SistemaOperativo)
            MyPos = InStr(SistemaOperativo, SearchSO)
            If MyPos = 0 Then
                SearchSO = "Mobile"
                MyPos = InStr(SistemaOperativo, SearchSO)
                If MyPos = 0 Then
                    Return True
                End If
            End If
        Catch ex As Exception
        End Try
    End Function

    Public Function ProductoFraccionable(ByVal ClienteCOD As String, ByVal ProductoCOD As String) As Boolean

        Dim DA As SqlDataAdapter, Cmd As SqlCommand, DS As DataSet, RET As Boolean = False
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                DA = New SqlDataAdapter(Cmd)
                DS = New DataSet
                Cmd.CommandText = "SELECT ISNULL(FRACCIONABLE,'0') AS FRC FROM PRODUCTO WHERE CLIENTE_ID='" & Trim(ClienteCOD) & "' AND PRODUCTO_ID='" & Trim(ProductoCOD) & "'"
                Cmd.CommandType = CommandType.Text

                DA.Fill(DS)
                If (DS.Tables.Count > 0) Then
                    If (DS.Tables(0).Rows.Count > 0) Then
                        RET = IIf(DS.Tables(0).Rows(0)(0).ToString = "1", True, False)
                    End If
                End If
            End If
            Return RET
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Critical, "Producto Fraccionable")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Producto Fraccionable")
        Finally
            DS.Dispose()
            Cmd.Dispose()
            DA.Dispose()
        End Try
    End Function

End Module
