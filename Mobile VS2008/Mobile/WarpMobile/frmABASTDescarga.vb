Imports System.Data
Imports System.Data.SqlClient

Public Class frmABASTDescarga

    Private Const FrmName As String = "Abastecimiento"
    Private oAbast As clsABAST

    Public Property ObjAbastecimiento() As clsABAST
        Get
            Return oAbast
        End Get
        Set(ByVal value As clsABAST)
            oAbast = value
        End Set
    End Property

    Private Sub frmABASTDescarga_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                ComenzarOperacion()
            Case Keys.F2
                MostrarPendientesPorContenedora()
            Case Keys.F3
                Salir()
        End Select
    End Sub

    Private Sub frmABASTDescarga_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Inicializacion()
    End Sub

    Private Sub Inicializacion()
        Dim Ctrl As Control
        Try
            For Each Ctrl In Me.Controls
                'Textbox
                If (Ctrl.GetType() Is GetType(TextBox)) Then
                    Dim txt As TextBox = CType(Ctrl, TextBox)
                    txt.Enabled = True
                    txt.Text = ""
                    txt.Visible = False
                End If

                If (Ctrl.GetType() Is GetType(Label)) Then
                    Dim lbl As Label = CType(Ctrl, Label)
                    lbl.Visible = False
                End If

                If (Ctrl.GetType() Is GetType(ListBox)) Then
                    Dim LstF As ListBox = CType(Ctrl, ListBox)
                    LstF.Items.Clear()
                    LstF.Visible = False
                End If

                If (Ctrl.GetType() Is GetType(Button)) Then
                    Dim btn As Button = CType(Ctrl, Button)
                    btn.Enabled = True
                    btn.Visible = True
                End If
            Next

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub


    Private Sub MostrarPendientesPorContenedora()
        Dim frm As New frmABASTPENDIENTES, Ds As New DataSet
        Try
            If Me.txtNroContenedora.Visible = True Then
                If Trim(Me.txtNroContenedora.Text) <> "" Then
                    If oAbast.PendientesPorContenedora(Me.txtNroContenedora.Text, Ds) Then
                        If Ds.Tables(0).Rows.Count > 0 Then
                            frm.lblContenedora.Text = "Contenedora: " & Me.txtNroContenedora.Text
                            frm.DataGrid1.DataSource = Ds.Tables(0)
                            frm.ShowDialog()
                        Else
                            MsgBox("La contenedora no posee productos pendientes de ubicacion.", MsgBoxStyle.Information, FrmName)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
        Finally
            frm.Dispose()
        End Try
    End Sub

    Private Sub cmdComenzar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdComenzar.Click
        ComenzarOperacion()
    End Sub

    Private Sub ComenzarOperacion()
        Me.lblNroCarro.Visible = True
        Me.txtNroContenedora.Visible = True
        Me.txtNroContenedora.Focus()
    End Sub

    Private Sub txtNroContenedora_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNroContenedora.KeyPress
        ValidarCaracterNumerico(e)
    End Sub

    Private Sub txtNroContenedora_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNroContenedora.KeyUp
        If e.KeyValue = 13 Then
            If Trim(Me.txtNroContenedora.Text) <> "" Then
                If oAbast.ValidarContenedoraCargada(Me.txtNroContenedora.Text) Then
                    Me.txtNroContenedora.Enabled = False
                    Me.txtCodProducto.Text = ""
                    Me.lblCodProducto.Visible = True
                    Me.txtCodProducto.Visible = True
                    Me.txtCodProducto.Focus()
                Else
                    MsgBox("La contenedora " & Me.txtNroContenedora.Text & " no posee contenido.", MsgBoxStyle.Information, FrmName)
                    Me.txtNroContenedora.Text = ""
                    Me.txtNroContenedora.Focus()
                End If
            Else
                Me.txtNroContenedora.Text = ""
                Me.txtNroContenedora.Focus()
            End If
        End If
    End Sub

    Private Sub txtNroContenedora_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNroContenedora.TextChanged

    End Sub

    Private Sub txtCodProducto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCodProducto.KeyUp
        If e.KeyValue = 13 Then
            If Trim(Me.txtCodProducto.Text) <> "" Then
                'voy a buscar la informacion del producto para descargar todo en la posicion.
                If oAbast.ValidarProductoContenedoraCargada(Me.txtNroContenedora.Text, Me.txtCodProducto.Text) Then
                    'obtengo los datos de la tarea a Ubicar.
                    Me.txtCodProducto.Enabled = False
                    If oAbast.LlenarListboxDescarga(Me.txtNroContenedora.Text, Me.txtCodProducto.Text, Me.lst, Me.lblUbicacion) Then
                        Me.lst.Visible = True
                        Me.lblUbicacion.Visible = True
                        Me.txtUbicacion.Visible = True
                        Me.txtUbicacion.Focus()
                    Else
                        Inicializacion()
                        Me.lblNroCarro.Visible = True
                        Me.txtNroContenedora.Visible = True
                        Me.txtNroContenedora.Focus()
                    End If
                Else
                    MsgBox("El producto indicado no se encuentra en la contenedora.", MsgBoxStyle.Information, FrmName)
                    Me.txtCodProducto.Text = ""
                    Me.txtCodProducto.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub txtCodProducto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodProducto.TextChanged

    End Sub

    Private Sub txtUbicacion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUbicacion.KeyUp
        If e.KeyValue = 13 Then
            If Trim(Me.txtUbicacion.Text) <> "" Then
                If Trim(UCase(Me.txtUbicacion.Text)) = oAbast.Posicion_A_Abastecer Then
                    'desde aca confirmo la posicion.
                    If oAbast.ConfirmarDescarga(Me.txtNroContenedora.Text, Me.txtCodProducto.Text) Then
                        If oAbast.ValidarContenedoraCargada(Me.txtNroContenedora.Text) Then
                            Me.txtUbicacion.Text = ""
                            Me.lblUbicacion.Visible = False
                            Me.txtUbicacion.Visible = False
                            Me.lst.Items.Clear()
                            Me.lst.Visible = False
                            Me.txtCodProducto.Text = ""
                            Me.txtCodProducto.Enabled = True
                            Me.txtCodProducto.Focus()
                        Else
                            MsgBox("Se completo la descarga de la contenedora.", MsgBoxStyle.Information, FrmName)
                            Inicializacion()
                        End If
                    Else
                        MsgBox("Ocurrio un error al confirmar la descarga.", MsgBoxStyle.Information, FrmName)
                        Me.txtUbicacion.Text = ""
                        Me.txtUbicacion.Focus()
                    End If
                Else
                    MsgBox("La posicion escaneada es incorrecta.", MsgBoxStyle.Information, FrmName)
                    Me.txtUbicacion.Text = ""
                    Me.txtUbicacion.Focus()
                End If
            Else
                Me.txtUbicacion.Text = ""
                Me.txtUbicacion.Focus()
            End If
        End If
    End Sub

    Private Sub txtUbicacion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUbicacion.TextChanged

    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Salir()
    End Sub

    Private Sub Salir()
        If Me.txtNroContenedora.Visible = True Then
            If MsgBox("¿Desea cancelar la operacion en curso?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub cmdPendientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPendientes.Click
        MostrarPendientesPorContenedora()
    End Sub

End Class