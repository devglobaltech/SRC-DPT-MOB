Imports System.Data
Imports System.Data.SqlClient

Public Class frmCargaSeries

    Private Const SQLConErr As String = "No se pudo conectar a la base de datos."
    Private Const FrmName As String = "Seleccion de Clientes"
    Private oSeries As New clsCargaSeries

    Private Sub frmCargaSeries_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyData
            Case Keys.F1
                Comenzar()
            Case Keys.F2
                Cancelar()
            Case Keys.F3
                Confirmar()
            Case Keys.F4
                Salir()
        End Select
    End Sub



    Private Sub frmCargaSeries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InicializarFormulario()
    End Sub

    Private Sub InicializarFormulario()
        Try
            Dim Ctl As Control
            For Each Ctl In Me.Controls
                If TypeOf Ctl Is Button Then
                    Ctl.Visible = True
                Else
                    Ctl.Visible = False
                End If
                If TypeOf Ctl Is TextBox Then
                    Ctl.Text = ""
                End If
            Next
            Me.DgSeries.Visible = False
            Me.btnSEspecifica.Visible = False
            Me.btnSEspecifica.Text = "Serie Esp: On"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        End Try
    End Sub

    Private Sub cmdComenzar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdComenzar.Click
        Comenzar()
    End Sub

    Private Sub Comenzar()
        If Me.cmbClientes.Visible = False Then
            Me.cmbClientes.DataSource = Nothing
            oSeries.FillCmb(Me.cmbClientes)
            Dim Ctl As Control
            For Each Ctl In Me.Controls
                Ctl.Enabled = True

                If TypeOf Ctl Is Button Then
                    Ctl.Visible = True
                Else
                    Ctl.Visible = False
                End If

                If TypeOf Ctl Is TextBox Then
                    Ctl.Text = ""
                End If
            Next
            Me.btnSEspecifica.Visible = False
            If oSeries.SeriesPendientes(Me.cmbClientes, Me.txtNroContenedora) Then
                Me.lblCliente.Visible = True
                Me.cmbClientes.Visible = True
                Me.lblIngresoSeries.Visible = True
                Me.cmbClientes.Enabled = False
                Me.lblNroContenedora.Visible = True
                Me.txtNroContenedora.Visible = True
                Me.txtNroContenedora.Enabled = False
                Me.lblDescripcionProducto.Visible = True
                oSeries.DescripcionSKU(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.txtProducto)
                Me.txtProducto.Visible = True
                Me.lblNroSerie.Visible = True
                Me.txtSerie.Visible = True
                oSeries.GetSeries(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.DgSeries)
                Me.txtSerie.Focus()
                oSeries.IndicadorSeries(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.lblCantSeries)
                If oSeries.MuestraBtnSerieEspecifica(Me.txtNroContenedora.Text) Then
                    Me.btnSEspecifica.Visible = True
                Else
                    Me.btnSEspecifica.Visible = False
                End If
                If Not oSeries.VerificacionSeriesCargadas(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text) Then
                    'Me.txtSerie.Enabled = False
                    If MsgBox("Se completo la carga de series para la contenedora." & vbNewLine & "¿Desea Confirmar la carga?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                        'Cierro de una todo...
                        If oSeries.ConfirmarSeries Then
                            MsgBox("Las series se confirmaron correctamente.", MsgBoxStyle.Information, FrmName)
                            Me.InicializarFormulario()
                        End If
                    End If
                End If
            Else
                oSeries.IniciarProcesoSeries()
                Me.lblCliente.Visible = True
                Me.cmbClientes.Visible = True
                Me.lblIngresoSeries.Visible = True
                Me.cmbClientes.Focus()
            End If
        End If
    End Sub

    Private Sub cmbClientes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbClientes.KeyUp
        If e.KeyValue = 13 Then
            Me.cmbClientes.Enabled = False
            Me.lblNroContenedora.Visible = True
            Me.txtNroContenedora.Visible = True
            Me.txtNroContenedora.Focus()
        End If
    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Cancelar()
    End Sub

    Private Sub Cancelar()
        Try
            If Me.cmbClientes.Visible Then
                If (MsgBox("Desea cancelar la operacion en curso?", MsgBoxStyle.YesNo, FrmName)) = MsgBoxResult.Yes Then
                    'Limpiar la tabla con los datos temporales.
                    'If Me.DgSeries.VisibleRowCount > 0 Then
                    oSeries.EliminarSerie(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text)
                    Me.InicializarFormulario()
                    'End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        End Try
    End Sub

    Private Sub Salir()
        Try
            If Me.cmbClientes.Visible Then
                If (MsgBox("Desea cancelar la operacion en curso?", MsgBoxStyle.YesNo, FrmName)) = MsgBoxResult.Yes Then
                    'Limpiar la tabla con los datos temporales.
                    If Me.DgSeries.VisibleRowCount > 0 Then
                        oSeries.EliminarSerie(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text)
                    End If
                    Me.Close()
                End If
            Else
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        End Try
    End Sub

    Private Sub txtNroContenedora_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNroContenedora.KeyUp
        If (e.KeyValue = 13) And Trim(Me.txtNroContenedora.Text) <> "" Then
            If oSeries.ValidarContenedora(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text) Then
                If oSeries.MuestraBtnSerieEspecifica(Me.txtNroContenedora.Text) Then
                    Me.btnSEspecifica.Visible = True
                Else
                    Me.btnSEspecifica.Visible = False
                End If
                Me.lblDescripcionProducto.Visible = True
                oSeries.DescripcionSKU(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.txtProducto)
                Me.txtProducto.Visible = True
                Me.lblNroSerie.Visible = True
                Me.txtSerie.Visible = True
                Me.txtNroContenedora.Enabled = False
                Me.txtSerie.Focus()
            Else
                Me.txtNroContenedora.Text = ""
                Me.txtNroContenedora.Focus()
            End If
        End If
    End Sub

    Private Sub txtNroContenedora_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNroContenedora.TextChanged

    End Sub

    Private Sub cmbClientes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClientes.SelectedIndexChanged

    End Sub

    Private Sub txtSerie_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSerie.KeyUp
        Dim blnSerieEsp As Boolean, DSerieError As New DataSet, vCancel As Boolean = False
        If (e.KeyValue = 13) And (Trim(Me.txtSerie.Text) <> "") Then
            oSeries.IndicadorSeries(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.lblCantSeries)
            If Me.btnSEspecifica.Visible Then
                If Me.btnSEspecifica.Text = "Serie Esp: Off" Then
                    blnSerieEsp = False
                Else
                    blnSerieEsp = True
                End If
            Else
                blnSerieEsp = False
            End If
            If oSeries.ExisteSerieDS(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.txtSerie, blnSerieEsp, Me.txtProducto.Text, vCancel) Then
                'Si existe la serie pregunto si desea eliminarla.
                If MsgBox("La serie se encuentra ingresada." & vbNewLine & "¿Desea eliminar la serie?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                    'elimino la serie.
                    If oSeries.EliminarSerieDS(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.txtSerie.Text, blnSerieEsp) Then
                        MsgBox("La serie fue borrada correctamente.", MsgBoxStyle.Information, FrmName)
                        Me.txtSerie.Text = ""
                        Me.txtSerie.Focus()
                    Else
                        Me.txtSerie.Text = ""
                        Me.txtSerie.Focus()
                    End If
                Else
                    Me.txtSerie.Text = ""
                    Me.txtSerie.Focus()
                End If
                oSeries.GetSeriesDS(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.DgSeries)
                oSeries.IndicadorSeries(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.lblCantSeries)
            Else
                If vCancel Then
                    Me.txtSerie.Text = ""
                    Me.txtSerie.Focus()
                    Exit Sub
                End If
                If Not oSeries.VerificacionSeriesCargadasDS(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text) Then
                    MsgBox("La carga de series para la contenedora ya se encuentra completa. La serie escaneada no sera agregada.", MsgBoxStyle.OkOnly, FrmName)
                    Me.txtSerie.Text = ""
                    Return
                End If
                'ACA
                If Me.btnSEspecifica.Visible Then
                    If Me.btnSEspecifica.Text = "Serie Esp: Off" Then
                        blnSerieEsp = False
                    Else
                        blnSerieEsp = True
                    End If
                Else
                    blnSerieEsp = False
                End If
                If oSeries.GuardarSerieDS(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.txtSerie.Text, blnSerieEsp) Then
                    oSeries.IndicadorSeries(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.lblCantSeries)
                    Me.txtSerie.Text = ""
                    Me.txtSerie.Focus()
                    If Not oSeries.VerificacionSeriesCargadasDS(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text) Then
                        'Me.txtSerie.Enabled = False
                        If MsgBox("Se completo la carga de series para la contenedora." & vbNewLine & "¿Desea Confirmar la carga?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                            'Cierro de una todo...
                            If oSeries.ConfirmarSeriesDS(DSerieError) Then
                                MsgBox("Las series se confirmaron correctamente.", MsgBoxStyle.Information, FrmName)
                                Me.InicializarFormulario()
                                Exit Sub
                            Else
                                MostrarSeriesInvalidas(DSerieError)
                            End If
                        End If
                    End If
                    oSeries.GetSeriesDS(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.DgSeries)
                Else
                    oSeries.GetSeriesDS(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.DgSeries)
                    Me.txtSerie.Text = ""
                    Me.txtSerie.Focus()
                End If
            End If

        End If
    End Sub

    Private Sub cmdConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfirmar.Click
        Confirmar()
    End Sub

    Private Sub Confirmar()
        If Me.DgSeries.Visible Then
            Dim Cantidad As String = "", DSerieError As New DataSet
            If Me.DgSeries.VisibleRowCount > 0 Then
                If oSeries.VerificacionSeriesCargadasDS(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Cantidad) Then
                    MsgBox("No es posible finalizar la carga ya que aun quedan series por tomar. Cantidad de series restantes: " & Cantidad, MsgBoxStyle.OkOnly, FrmName)
                    Exit Sub
                End If
                If MsgBox("¿Desea Confirmar el ingreso de las series?", MsgBoxStyle.YesNo, FrmName) = MsgBoxResult.Yes Then
                    'Cierro de una todo...
                    If oSeries.ConfirmarSeriesDS(DSerieError) Then
                        MsgBox("Las series se confirmaron correctamente.", MsgBoxStyle.Information, FrmName)
                        Me.InicializarFormulario()
                    Else
                        MostrarSeriesInvalidas(DSerieError)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub MostrarSeriesInvalidas(ByRef DsErr As DataSet)
        Try
            '1. las muestro.
            MsgBox("Se detectaron series duplicadas en la carga. A continuación seran listadas.", MsgBoxStyle.Exclamation, FrmName)
            Dim f As New frmCargaSeriesInvalidas
            f.SeriesError = DsErr
            f.ShowDialog()
            '2. las mando a eliminar del dataset principal.
            For Each row As DataRow In DsErr.Tables(0).Rows
                oSeries.EliminarSerieDS(Me.cmbClientes.Text, Me.txtNroContenedora.Text, row("SERIE").ToString, False)
            Next
            '3. posiciono todo para tomar las series restantes.
            oSeries.GetSeriesDS(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.DgSeries)
            oSeries.IndicadorSeries(Me.cmbClientes.SelectedValue, Me.txtNroContenedora.Text, Me.lblCantSeries)
            Me.txtSerie.Text = ""
            Me.txtSerie.Focus()
            f = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, FrmName)
        End Try
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Salir()
    End Sub

    Private Sub txtSerie_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSerie.TextChanged
        Dim L_nOldPos As Integer = Me.txtSerie.SelectionStart
        Me.txtSerie.Text = UCase(Me.txtSerie.Text)
        Me.txtSerie.SelectionStart = L_nOldPos
    End Sub

    Private Sub btnSEspecifica_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSEspecifica.Click
        If Me.btnSEspecifica.Text = "Serie Esp: Off" Then
            Me.btnSEspecifica.Text = "Serie Esp: On"
            Me.txtSerie.Focus()
        Else
            Me.btnSEspecifica.Text = "Serie Esp: Off"
            Me.txtSerie.Focus()
        End If
    End Sub
End Class