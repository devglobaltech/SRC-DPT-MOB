
Public Class frmGuardadoxDoc

    Private Const FrmName As String = "Ubicacion Manual."
    Private Cliente As String
    Private PosCod As String = ""
    Private PosId As Long = 0
    Private NaveCod As String = ""
    Private NaveId As Long = 0
    Private xFrac As Boolean = False
    Private Linea As Integer = 0
    Private CantLineas As Integer = 0
    Private IsContenedora As Boolean = False
    Private QtyLocator As Long

    Private Property FlagContenedora() As Boolean
        Get
            Return IsContenedora
        End Get
        Set(ByVal value As Boolean)
            IsContenedora = value
        End Set
    End Property

    Private Sub frmGuardadoxDoc_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                F1()
            Case Keys.F2
                F2()
            Case Keys.F3
                F3()
            Case Keys.F4
                F4()
        End Select
    End Sub

    Private Sub F1()
        Me.lblDoc.Visible = True
        Me.txtDOC.Visible = True
        Me.txtDOC.Focus()
        Me.cmdFin.Enabled = True
    End Sub

    Private Sub F2()
        'para ver los pendientes.
        If Trim(Me.txtDOC.Text) <> "" Then
            Dim vForm As New frmGManPendientes
            vForm.vDocumento = CLng(Me.txtDOC.Text)
            vForm.FlagContenedora = FlagContenedora
            vForm.ShowDialog()
            vForm = Nothing
        End If
    End Sub

    Private Sub F3()
        Dim CG As New clsGuardadoManual
        If (Me.txtDOC.Enabled = False) And (Me.txtDOC.Text <> "") Then
            CG.Conexion = SQLc
            CG.Delete_sys_lock_pallet(CLng(Me.txtDOC.Text))
            If MsgBox("Desea cancelar la operacion en curso?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                Me.IniForm()
            End If
        Else
            Me.IniForm()
        End If
    End Sub

    Private Sub F4()
        If MsgBox("¿Desea Salir?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
            If (Me.txtDOC.Enabled = False) And (Me.txtDOC.Text <> "") Then
                Dim CG As New clsGuardadoManual
                CG.Conexion = SQLc
                CG.Delete_sys_lock_pallet(CLng(Me.txtDOC.Text))
            End If
            Me.Close()
        End If
    End Sub

    Private Sub frmGuardadoxDoc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IniForm()
        Me.cmdComenzar.Focus()
    End Sub

    Private Sub IniForm()
        Try
            Me.txtProd.Text = ""
            Me.txtProd.Enabled = True
            Me.txtDOC.Text = ""
            Me.txtDOC.Enabled = True
            Me.lblDoc.Visible = False
            Me.txtDOC.Visible = False
            Me.lblProd.Visible = False
            Me.txtProd.Visible = False
            Me.lblDescripcion.Visible = False
            Me.lblQty.Visible = False
            Me.lblDescUbic.Visible = False
            Me.lblUbicacion.Visible = False
            Me.txtUbicacion.Visible = False
            Me.lblQtyIng.Visible = False
            Me.txtQty.Visible = False
            Me.txtQty.Text = ""
            Me.cmdPendientes.Enabled = False
            Me.cmdFin.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "IniForm")
        End Try
    End Sub

    Private Sub NextProd()
        Me.txtProd.Text = ""
        Me.txtProd.Enabled = True
        Me.lblQty.Visible = False
        Me.lblDescUbic.Visible = False
        Me.lblUbicacion.Visible = False
        Me.txtUbicacion.Text = ""
        Me.txtUbicacion.Visible = False
        Me.lblQtyIng.Visible = False
        Me.txtQty.Text = ""
        Me.txtQty.Visible = False
        Me.lblDescripcion.Text = ""
        Me.lblDescripcion.Visible = False
        Me.txtUbicacion.Enabled = True
        Me.txtProd.Focus()
    End Sub

    Private Sub txtDOC_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDOC.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub txtDOC_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDOC.KeyUp
        Dim val As clsGuardadoManual
        Dim vError As String = ""
        Try
            If e.KeyValue = 13 And (Trim(Me.txtDOC.Text) <> "") Then
                val = New clsGuardadoManual
                val.Conexion = SQLc
                If Not val.ExistsDocument(CLng(Me.txtDOC.Text), Cliente, vError) Then
                    Me.txtDOC.Text = ""
                    Me.txtDOC.Focus()
                    Throw New Exception(vError)
                Else
                    Me.cmdPendientes.Enabled = True
                    Me.cmdFin.Enabled = True
                    Me.txtDOC.Enabled = False
                    Me.lblProd.Visible = True
                    Me.txtProd.Visible = True
                    Me.txtProd.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        Finally
            val = Nothing
        End Try
    End Sub

    Private Sub txtProd_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProd.KeyUp
        Dim Val As New clsGuardadoManual
        Dim Desc As String = "", Fracc As String = "", verror As String = "", Posicion As String = "", msg As String = ""
        Dim Line As Integer = 0, CantLines As Double = 0
        Dim Qty As Double = 0
        Try
            If (e.KeyValue = 13) And (Trim(Me.txtProd.Text) <> "") Then
                o2D.CLIENTE_ID = Me.Cliente
                o2D.Decode(Me.txtProd.Text)

                Me.txtProd.Text = o2D.PRODUCTO_ID
                Val.Conexion = SQLc

                If Val.ValidarSeries(CLng(Me.txtDOC.Text), Trim(UCase(Me.txtProd.Text))) Then
                    msg = "Para ubicar el producto " & Trim(UCase(Me.txtProd.Text)) & " es necesario cargar todos sus números de serie." & vbNewLine & "No sera posible continuar con el guardado de este producto."
                    Me.txtProd.Text = ""
                    Me.txtProd.Focus()
                    Throw New Exception(msg)
                End If
                'Catalina Castillo.25/01/2012.Agrego el llamado a la funcion para saber si el producto 
                'esta marcado como tiene contenedora.
                If Val.GetFlgContenedora(Cliente, Me.txtProd.Text) = False Then
                    FlagContenedora = False
                    If Not Val.GetDescriptionByProd(CLng(Me.txtDOC.Text), Cliente, Me.txtProd.Text, Desc, Line, Qty, Fracc, verror) Then
                        Me.txtProd.Text = ""
                        Me.txtProd.Focus()
                        Throw New Exception(verror)
                    Else
                        xFrac = IIf(Fracc = 1, True, False)
                        Linea = Line

                        '-----------------------------------------------------------------------
                        'Aca debo traerme la posicion  donde voy a guardar los productos.
                        '-----------------------------------------------------------------------
                        If Not Val.Locator_Ing(CLng(Me.txtDOC.Text), Linea, NaveCod, NaveId, PosCod, PosId, Qty, verror) Then
                            Throw New Exception(verror)
                        Else
                            'Me.txtQty.Visible = True
                            Me.txtQty.Text = Qty
                            QtyLocator = Qty
                            Me.lblQtyIng.Text = "Cant. de Unidades a Guardar:"
                            'Me.lblQtyIng.Visible = True
                            Me.txtUbicacion.Text = ""
                            Me.txtUbicacion.Enabled = True
                            Me.txtProd.Enabled = False
                            Me.lblDescripcion.Visible = True
                            Me.lblDescripcion.Text = Desc

                            Me.lblQty.Visible = True
                            Me.lblQty.Text = "Cantidad Pendiente de Guardar: " & Qty
                            Me.lblDescUbic.Visible = True
                            Me.lblUbicacion.Visible = True
                            Me.txtUbicacion.Visible = True
                            If (PosId <> 0) And (PosCod <> "") Then
                                'es una posicion.
                                Me.lblUbicacion.Text = PosCod
                            ElseIf (NaveCod <> "") And (NaveId <> 0) Then
                                'es una nave.
                                Me.lblUbicacion.Text = PosCod
                            End If
                            Me.txtUbicacion.Focus()
                        End If
                    End If
                Else
                    FlagContenedora = True
                    'UbicacionProductoContenedoras()
                    If Not Val.GetDescriptionByProdCont(CLng(Me.txtDOC.Text), Cliente, Me.txtProd.Text, Desc, CantLines, Fracc, verror) Then
                        Me.txtProd.Text = ""
                        Me.txtProd.Focus()
                        Throw New Exception(verror)
                    Else
                        Me.lblDescripcion.Visible = True
                        Me.lblDescripcion.Text = Desc
                    End If
                    Me.PanCantidad.Visible = True
                    If FlagContenedora Then
                        lblQtyIng.Text = "Nro.Contenedora a Guardar:"
                    Else
                        lblQtyIng.Text = "Cant. a Guardar:"
                    End If
                    Me.lblQtyIng.Visible = True
                    Me.txtQty.Visible = True
                    Me.txtQty.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        Finally
            Val = Nothing
        End Try
    End Sub
    Private Sub UbicacionProductoContenedoras()
        Dim Val As New clsGuardadoManual
        Dim Desc As String = "", Fracc As String = "", verror As String = "", Posicion As String = ""
        Dim CantLines As Integer = 0
        Dim Qty As Double = 0
        Val.Conexion = SQLc
        Try
            If Not Val.GetDescriptionByProdCont(CLng(Me.txtDOC.Text), Cliente, Me.txtProd.Text, Desc, CantLines, Fracc, verror) Then
                Me.txtProd.Text = ""
                Me.txtProd.Focus()
                Throw New Exception(verror)
            Else
                xFrac = IIf(Fracc = 1, True, False)
                CantLineas = CantLines
                Linea = CantLines

                If Not Val.validarSiContenedoraUbicada(CLng(Me.txtDOC.Text), CDbl(Me.txtQty.Text), Trim(Me.txtProd.Text), verror) Then
                    Me.txtQty.Text = ""
                    Me.txtQty.Focus()
                    Throw New Exception(verror)
                End If
                '-----------------------------------------------------------------------
                'Aca debo traerme la posicion  donde voy a guardar los productos.
                '-----------------------------------------------------------------------
                If Not Val.Locator_Ing_Cont(CLng(Me.txtDOC.Text), NaveCod, NaveId, PosCod, PosId, Me.txtProd.Text, Me.txtQty.Text, verror) Then
                    Throw New Exception(verror)
                Else
                    Me.txtUbicacion.Text = ""
                    Me.txtUbicacion.Enabled = True
                    Me.txtProd.Enabled = False
                    Me.lblDescripcion.Visible = True
                    Me.lblDescripcion.Text = Desc
                    Me.lblQty.Visible = True
                    Me.lblQty.Text = "Cantidad Contenedoras Pendientes de Guardar: " & CantLines
                    Me.lblDescUbic.Visible = True
                    Me.lblUbicacion.Visible = True
                    Me.txtUbicacion.Visible = True
                    If (PosId <> 0) And (PosCod <> "") Then
                        'es una posicion.
                        Me.lblUbicacion.Text = PosCod
                    ElseIf (NaveCod <> "") And (NaveId <> 0) Then
                        'es una nave.
                        Me.lblUbicacion.Text = PosCod
                    End If
                    Me.txtUbicacion.Focus()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, FrmName)
        End Try
    End Sub

    Private Sub txtUbicacion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUbicacion.KeyUp
        Dim val As New clsGuardadoManual
        Dim vError As String = "", cierre As Boolean = False
        Try
            If (e.KeyValue = 13) And (Trim(Me.txtUbicacion.Text) <> "") Then
                If (UCase(Me.lblUbicacion.Text) <> UCase(Trim(Me.txtUbicacion.Text))) Then
                    val.Conexion = SQLc
                    If Not val.ExisteNavePosicion(Me.txtUbicacion.Text, vError) Then
                        Me.txtUbicacion.Text = ""
                        Me.txtUbicacion.Focus()
                        Throw New Exception(vError)
                    Else
                        If MsgBox("Selecciono una ubicación diferente, ¿desea continuar?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.No Then
                            Me.txtUbicacion.Text = ""
                            Me.txtUbicacion.Focus()
                        Else
                            GoTo Guardado
                        End If
                    End If
                Else
Guardado:
                    If FlagContenedora Then
                        'Guardado de Mercderia.
                        val.Conexion = SQLc
                        If val.validarSiContenedoraUbicada(CLng(Me.txtDOC.Text), CDbl(Me.txtQty.Text), Trim(Me.txtProd.Text), vError) Then
                            If Not val.GuardarItemContenedoras(CLng(Me.txtDOC.Text), CDbl(Me.txtQty.Text), Me.txtUbicacion.Text, Trim(Me.txtProd.Text), vError) Then
                                Me.txtQty.Text = ""
                                Me.txtQty.Focus()
                                Throw New Exception(vError)
                            Else
                                If Not val.CloseDocumento(CLng(Me.txtDOC.Text), Linea, vError, cierre) Then
                                    Throw New Exception(vError)
                                Else
                                    If cierre = False Then
                                        NextProd()
                                    Else
                                        MsgBox("Se completo el guardado del documento de ingreso " & Me.txtDOC.Text & ".", MsgBoxStyle.Information, FrmName)
                                        IniForm()
                                    End If
                                End If
                            End If
                        Else
                            Me.txtQty.Text = ""
                            Me.txtQty.Focus()
                            Throw New Exception(vError)
                        End If
                    Else
                        If Not FlagContenedora Then
                            lblQtyIng.Visible = True
                            Me.txtQty.Text = ""
                            Me.txtQty.Visible = True
                            Me.txtQty.Focus()
                            Exit Try
                        End If
                        val.Conexion = SQLc
                        If Not val.GuardarItem(CLng(Me.txtDOC.Text), Linea, CDbl(Me.txtQty.Text), Me.txtUbicacion.Text, Trim(Me.txtProd.Text), vError) Then
                            Me.txtQty.Text = ""
                            Me.txtQty.Focus()
                            Throw New Exception(vError)
                        Else
                            If Not val.CloseDocumento(CLng(Me.txtDOC.Text), Linea, vError, cierre) Then
                                Throw New Exception(vError)
                            Else
                                If cierre = False Then
                                    NextProd()
                                Else
                                    MsgBox("Se completo el guardado del documento de ingreso " & Me.txtDOC.Text & ".", MsgBoxStyle.Information, FrmName)
                                    IniForm()
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        Finally
            val = Nothing
        End Try
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        Dim Search As String
        Dim Pos As Integer
        Search = "."
        If Not xFrac Then
            ValidarCaracterNumerico(e)
        Else
            Pos = InStr(1, Me.txtQty.Text, Search)
            If Pos > 0 And Asc(e.KeyChar) <> 46 Then
                If Len(Mid(Me.txtQty.Text, Pos + 1, Len(Me.txtQty.Text))) >= 5 And Asc(e.KeyChar) <> 8 Then
                    e.Handled = True
                    Me.txtQty.Focus()
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

    Private Sub txtQty_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyUp
        Dim val As New clsGuardadoManual
        Dim vError As String = ""
        Dim Cierre As Boolean = False
        If e.KeyValue = 13 Then

            If Trim(Me.txtQty.Text) = "" Then
                Me.txtQty.Text = ""
                Me.txtQty.Focus()
                Exit Sub
            End If
            If Not FlagContenedora Then
                If QtyLocator > 0 Then
                    If CDbl(Me.txtQty.Text) > Me.QtyLocator Then
                        MsgBox("No es posible ubicar la cantidad indicada. Maximo posible " & Me.QtyLocator & ".", MsgBoxStyle.Information, FrmName)
                        Me.txtQty.Text = ""
                        Me.txtQty.Focus()
                        Exit Sub
                    ElseIf CDbl(Me.txtQty.Text) < Me.QtyLocator Then
                        If CDbl(Me.txtQty.Text) = 0 Then
                            MsgBox("La cantidad a guardar debe ser mayor a cero.", MsgBoxStyle.Information, FrmName)
                            Me.txtQty.Text = ""
                            Me.txtQty.Focus()
                            Exit Sub
                        End If
                        If MsgBox("La cantidad a guardar es menor a la especificada. ¿Desea continuar?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.No Then
                            Me.txtQty.Text = ""
                            Me.txtQty.Focus()
                            Exit Sub
                        End If
                    End If
                End If
            End If
            '------------------------------------------
            'impacto con los cambios de guardado.
            '------------------------------------------
            Try
                If FlagContenedora Then
                    UbicacionProductoContenedoras()
                    Exit Sub
                End If

                If (IsNumeric(Me.txtQty.Text)) And (Trim(Me.txtQty.Text) <> "") Then
                    If (CDbl(Me.txtQty.Text) > 0) Then
                        val.Conexion = SQLc
                        If FlagContenedora = False Then
                            If Not val.GuardarItem(CLng(Me.txtDOC.Text), Linea, CDbl(Me.txtQty.Text), Me.txtUbicacion.Text, Trim(Me.txtProd.Text), vError) Then
                                Me.txtQty.Text = ""
                                Me.txtQty.Focus()
                                Throw New Exception(vError)
                            Else
                                If Not val.CloseDocumento(CLng(Me.txtDOC.Text), Linea, vError, Cierre) Then
                                    Throw New Exception(vError)
                                Else
                                    If Cierre = False Then
                                        NextProd()
                                    Else
                                        MsgBox("Se completo el guardado del documento de ingreso " & Me.txtDOC.Text & ".", MsgBoxStyle.Information, FrmName)
                                        IniForm()
                                    End If
                                End If
                            End If
                        Else
                            If val.validarSiContenedoraUbicada(CLng(Me.txtDOC.Text), CDbl(Me.txtQty.Text), Trim(Me.txtProd.Text), vError) Then
                                If Not val.GuardarItemContenedoras(CLng(Me.txtDOC.Text), CDbl(Me.txtQty.Text), Me.txtUbicacion.Text, Trim(Me.txtProd.Text), vError) Then
                                    Me.txtQty.Text = ""
                                    Me.txtQty.Focus()
                                    Throw New Exception(vError)
                                Else
                                    If Not val.CloseDocumento(CLng(Me.txtDOC.Text), Linea, vError, Cierre) Then
                                        Throw New Exception(vError)
                                    Else
                                        If Cierre = False Then
                                            NextProd()
                                        Else
                                            MsgBox("Se completo el guardado del documento de ingreso " & Me.txtDOC.Text & ".", MsgBoxStyle.Information, FrmName)
                                            IniForm()
                                        End If
                                    End If
                                End If
                            Else
                                Me.txtQty.Text = ""
                                Me.txtQty.Focus()
                                Throw New Exception(vError)
                            End If
                        End If
                    Else
                        'LRojas Tracker ID 3626 13/03/2012: Validaciones sobre cantidad
                        Me.txtQty.Text = ""
                        Me.txtQty.Focus()
                        Throw New Exception("Cantidad debe ser mayor a cero.")
                    End If
                Else
                    Me.txtQty.Focus()
                    'Throw New Exception("Cantidad no válida.")
                End If
                'Fin Tracker ID 3626
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
            End Try
        End If
    End Sub


    Private Sub cmdComenzar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdComenzar.Click
        F1()
    End Sub

    Private Sub cmdPendientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPendientes.Click
        F2()
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        F4()
    End Sub

    Private Sub cmdFin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFin.Click
        F3()
    End Sub

    Private Sub TxtUnidCont_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtUnidCont.TextChanged

    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged

    End Sub

    Private Sub txtProd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProd.TextChanged

    End Sub

    Private Sub txtDOC_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDOC.TextChanged

    End Sub

    Private Sub txtUbicacion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUbicacion.TextChanged

    End Sub
End Class