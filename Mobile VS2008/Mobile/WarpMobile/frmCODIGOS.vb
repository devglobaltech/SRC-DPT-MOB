Imports System.Data.SqlClient
Imports System.Data


Public Class frmCODIGOS

    Private blnValidCode As Boolean = False
    Private blnCancel As Boolean = False
    Private vDocId As Long
    Private vCliente_id As String
    Private vNroLinea As Long
    Private BlnValPicking As Boolean = False
    Private Producto_Id As String = ""
    Private PalletEgreso As String = ""
    Private Const FrmName As String = "Control de Codigos."
    Private DSConversion As New DataSet
    Private EgrCantidad As Double

    Public Property CantidadEgreso() As Double
        Get
            Return EgrCantidad
        End Get
        Set(ByVal value As Double)
            EgrCantidad = value
        End Set
    End Property

    Public ReadOnly Property Conversion() As DataSet
        Get
            Return Me.DSConversion
        End Get
    End Property

    Public Property cliente_id() As String
        Set(ByVal value As String)
            vCliente_id = value
        End Set
        Get
            Return vCliente_id
        End Get
    End Property

    Public Property ValCodEgr() As Boolean
        Get
            Return BlnValPicking
        End Get
        Set(ByVal value As Boolean)
            BlnValPicking = value
        End Set
    End Property

    Public Property Producto() As String
        Set(ByVal value As String)
            Producto_Id = value
        End Set
        Get
            Return Producto_Id
        End Get
    End Property

    Public Property PalletEgr() As String
        Get
            Return PalletEgreso
        End Get
        Set(ByVal value As String)
            PalletEgreso = value
        End Set
    End Property

    Public Property DocumentoId() As Long
        Get
            Return vDocId
        End Get
        Set(ByVal value As Long)
            vDocId = value
        End Set
    End Property

    Public Property NroLinea() As Long
        Get
            Return vNroLinea
        End Get
        Set(ByVal value As Long)
            vNroLinea = value
        End Set
    End Property

    Public ReadOnly Property Cancel() As Boolean
        Get
            Return blnCancel
        End Get
    End Property

    Public ReadOnly Property ValidCode() As Boolean
        Get
            Return blnValidCode
        End Get
    End Property

    Private Sub frmCODIGOS_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                'aca Valido Codigos.
                PreValida()
            Case Keys.F2
                Cancelar()
            Case Keys.F3
                Salir()
        End Select
    End Sub

    Private Sub Cancelar()
        Me.txtCod.Text = ""
        Me.txtCod.Focus()
    End Sub

    Private Sub frmCODIGOS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtCod.Text = ""
        Me.txtCod.Focus()
        If Producto = "" Then
            Me.lblProducto_ID.Visible = False
        Else
            Me.lblProducto_ID.Visible = True
            If Not ValCodEgr Then
                Me.lblProducto_ID.Text = Producto
            Else
                Me.lblProducto_ID.Text = "Codigo de Producto: " & Producto
            End If
        End If
    End Sub

    Private Sub bntSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntSalir.Click
        Salir()
    End Sub

    Private Sub Salir()
        If Not ValCodEgr Then
            If MsgBox("No se ha validado el codigo, desea salir?" & vbNewLine & "No podra continuar con la ubicacion hasta que lo valide.", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                blnCancel = True
                Me.Close()
            Else
                Me.txtCod.Text = ""
                Me.txtCod.Focus()
            End If
        Else
            If MsgBox("No se ha validado el codigo, desea salir?" & vbNewLine & "No podra continuar con la tarea de picking hasta que lo valide.", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                blnCancel = True
                Me.Close()
            Else
                Me.txtCod.Text = ""
                Me.txtCod.Focus()
            End If
        End If
    End Sub

    Private Sub PreValida()
        Dim FConversion As Double = 0, Calc As Double = 0
        If Trim(Me.txtCod.Text <> "") Then

            If Not BlnValPicking Then 'Para saber si valido ingresos o Egresos.
                o2D.Decode(Me.txtCod.Text)
                Me.txtCod.Text = o2D.PRODUCTO_ID
                If Not ValidarCodigo() Then
                    Me.txtCod.Text = ""
                    Me.txtCod.Focus()
                Else
                    blnValidCode = True
                    Me.Close()
                End If
            Else
                '==============================================================================
                'Segun definicion, aca valido que el codigo que cargo el tipo
                'sea el mismo del pallet. Si no lo es mando a verificar a la base
                'de datos el codigo ingresado por el pickeador.
                '==============================================================================
                If UCase(Trim(Me.txtCod.Text)) = PalletEgreso Then
                    blnValidCode = True
                    Me.Close()
                    Exit Sub
                End If
                '==============================================================================
                o2D.Decode(Me.txtCod.Text)
                Me.txtCod.Text = o2D.PRODUCTO_ID
                If Not ValidarCodigoEGR() Then
                    Me.txtCod.Text = ""
                    Me.txtCod.Focus()
                Else
                    If Me.RecuperarInfoCodigo(Me.cliente_id, Me.Producto_Id, Me.txtCod.Text.Trim) Then
                        'Hay que validar si se puede hacer la conversion primero.
                        FConversion = CDbl(Me.DSConversion.Tables(0).Rows(0)(3))
                        Calc = Int(Me.EgrCantidad / FConversion)
                        If Calc <= 0 Then
                            MsgBox("La cantidad solicitada es menor a la cantidad del codigo indicado. No es posible continuar", MsgBoxStyle.Information, FrmName)
                            Me.DSConversion.Tables.Clear()
                            Me.txtCod.Text = ""
                            Me.txtCod.Focus()
                            Return
                        End If
                        blnValidCode = True
                        Me.Close()
                    End If
                End If
            End If
        Else
            Me.txtCod.Focus()
        End If
    End Sub

    Private Function ValidarCodigo() As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Ret As String = ""
        xCmd = SQLc.CreateCommand
        Try
            If VerifyConnection(SQLc) Then
                xCmd.CommandText = "dbo.ING_MATCH_CODE"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@Documento_id", SqlDbType.BigInt)
                Pa.Value = DocumentoId
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Nro_linea", SqlDbType.BigInt)
                Pa.Value = NroLinea
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Code", SqlDbType.VarChar, 50)
                Pa.Value = Me.txtCod.Text
                xCmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@Control", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                Ret = xCmd.Parameters("@Control").Value

            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SqlEx As SqlException
            MsgBox("Verificacion de Codigos: " & SqlEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("Verificacion de Codigos: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Function RecuperarInfoCodigo(ByVal Cliente As String, ByVal Producto As String, ByVal Code As String) As Boolean
        Dim XSQL As String, CMD As SqlCommand, DA As SqlDataAdapter, Dr As DataRow
        Try

            If VerifyConnection(SQLc) Then
                CMD = SQLc.CreateCommand
                DA = New SqlDataAdapter(CMD)
                XSQL = "SELECT CLIENTE_ID,PRODUCTO_ID,CODIGO, CANTIDAD FROM RL_PRODUCTO_CODIGOS WHERE CLIENTE_ID='" & Cliente & "' AND PRODUCTO_ID='" & Producto & "' AND CODIGO='" & Code & "'"
                CMD.CommandText = XSQL
                CMD.CommandType = CommandType.Text

                DA.Fill(Me.DSConversion, "TBL")
                If DSConversion.Tables(0).Rows.Count = 0 Then
                    Dr = Me.DSConversion.Tables(0).NewRow
                    Dr(0) = Me.cliente_id
                    Dr(1) = Me.Producto_Id
                    Dr(2) = "DUN14"
                    Dr(3) = 1
                    DSConversion.Tables(0).Rows.Add(Dr)

                End If
            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("Recuperar Info. Codigos: " & SQLEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("Recuperar Info. Codigos: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            CMD.Dispose()
            DA.Dispose()
        End Try
    End Function

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        PreValida()
    End Sub

    Private Sub txtCod_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCod.KeyUp
        If e.KeyValue = 13 Then
            PreValida()
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Cancelar()
    End Sub

    Private Function ValidarCodigoEGR() As Boolean
        Dim xCmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Ret As String = ""
        xCmd = SQLc.CreateCommand
        Try
            If VerifyConnection(SQLc) Then
                xCmd.CommandText = "dbo.EGR_MATCH_COD"
                xCmd.CommandType = CommandType.StoredProcedure
                Pa = New SqlParameter("@producto_id", SqlDbType.VarChar, 30)
                Pa.Value = Producto_Id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@cliente_id", SqlDbType.VarChar, 30)
                Pa.Value = Me.cliente_id
                xCmd.Parameters.Add(Pa)
                Pa = Nothing

                Pa = New SqlParameter("@Code", SqlDbType.VarChar, 50)
                Pa.Value = Me.txtCod.Text
                xCmd.Parameters.Add(Pa)

                Pa = New SqlParameter("@Valido", SqlDbType.Char, 1)
                Pa.Direction = ParameterDirection.Output
                xCmd.Parameters.Add(Pa)

                xCmd.ExecuteNonQuery()
                Ret = xCmd.Parameters("@Valido").Value

            Else
                MsgBox("Fallo al conectar con la base de datos.", MsgBoxStyle.OkOnly, FrmName)
                Return False
            End If
            Return True
        Catch SqlEx As SqlException
            MsgBox("Verificacion de Codigos: " & SqlEx.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Catch ex As Exception
            MsgBox("Verificacion de Codigos: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
            Return False
        Finally
            xCmd = Nothing
            Pa = Nothing
        End Try
    End Function

    Private Sub txtCod_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCod.TextChanged

    End Sub
End Class