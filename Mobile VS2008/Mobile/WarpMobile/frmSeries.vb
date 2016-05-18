Public Class frmSeries


#Region "declaraciones"
    Private _ds As Data.DataSet
    Private _cantidad As Integer
    Private PosEspecifica As Boolean
    Private PosModif As Long
    Public Property DS() As Data.DataSet
        Get
            Return _ds
        End Get
        Set(ByVal value As Data.DataSet)
            _ds = value
        End Set
    End Property
    Public Property Cantidad()
        Get
            Return _cantidad
        End Get
        Set(ByVal value)
            _cantidad = value
        End Set
    End Property
#End Region



    Private Sub TxtNumSeries_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtNumSeries.KeyUp
        If e.KeyCode = Keys.Enter Then
            If TxtNumSeries.Text <> "" Then
                INGRESAR_SERIE(TxtNumSeries.Text)
                TxtNumSeries.Text = ""

            End If
        End If
    End Sub

    Private Sub INGRESAR_SERIE(ByVal serie As String)
        Try
            Dim ROW() As Data.DataRow
            ROW = DS.Tables(0).Select("SERIES = '" + serie + "'")
            If Not IsNothing(ROW) Then
                If ROW.GetLength(0) > 0 Then
                    Err.Raise(513, "INGRESAR_SERIE", "Nro de serie ya ingresado")
                    Exit Sub
                End If
            End If
            If PosEspecifica Then
                DS.Tables(0).Rows(PosModif)("series") = TxtNumSeries.Text
                MsgBox("Número de serie Modificado", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Ingreso de Números de Serie")
                PosEspecifica = False
                PosModif = 0
                Me.lblMsg.Text = ""
                Me.LblNumMod.Visible = False
                Me.txtNumMod.Visible = False
                Me.txtNumMod.Text = ""
            Else
                If Me.Cantidad < DS.Tables(0).Rows.Count Then
                    DS.Tables(0).Rows(Me.Cantidad)("series") = TxtNumSeries.Text
                    Me.Cantidad += 1
                Else
                    MsgBox("Se ha superado la cantidad a ingresar", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Ingreso de Números de Serie")
                End If
                If Me.Cantidad = DS.Tables(0).Rows.Count Then
                    'MsgBox("No hay más números de serie que tomar", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Ingreso de Números de Serie")
                    cmdModificar.Enabled = True
                    Dim res As Integer = MsgBox("Se ingresaron todos los Números de Series. " & vbCrLf & "¿Confirma la carga de de estos Números de Series?", MsgBoxStyle.Information + MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkCancel)
                    If res = vbOK Then
                        cerrar()
                    End If
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Ingreso de Números de Serie")
        End Try
    End Sub
    
    
    Private Sub cmdAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
        cerrar()
    End Sub

    Private Sub frmSeries_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Me.TxtNumSeries.Focus()
    End Sub

    Private Sub frmSeries_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                cerrar()
            Case Keys.F2
                'Modifica()
                cancelar()
        End Select
    End Sub

    Private Sub frmSeries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim cant As Integer
            cant = 1
            PosEspecifica = False
            ExpandirDs(DS)

            For Each row As Data.DataRow In DS.Tables(0).Rows
                row("Posición") = cant
                cant += 1
            Next

            DS.Tables(0).TableName = "Consulta"
            dg1.DataSource = DS.Tables(0)
            dg1.CurrentRowIndex = 0


            FormatGrilla()
            Me.Cantidad = 0
            Me.LblNumMod.Visible = False
            Me.txtNumMod.Visible = False
            'Me.Show()
            Me.lblMsg.Text = ""
            Me.TxtNumSeries.Focus()
        Catch EX As Exception
            MsgBox(EX.Message + " - MostrarForm", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "MostrarForm")
        End Try
    End Sub
    Private Sub ExpandirDs(ByRef ds As Data.DataSet)
        Try
            Dim dsAux As New Data.DataSet
            Dim i As Integer, e As Long

            dsAux.Tables.Add()
            dsAux.Tables(0).Columns.Add("picking_id")
            dsAux.Tables(0).Columns.Add("Posición")
            dsAux.Tables(0).Columns.Add("series")
            For i = 0 To ds.Tables(0).Rows.Count - 1
                For e = 0 To CLng(ds.Tables(0).Rows(i)("CANTIDAD")) - 1
                    dsAux.Tables(0).Rows.Add(ds.Tables(0).Rows(i)("picking_id"), 0, "")
                Next
            Next

            DS = Nothing
            DS = dsAux

        Catch ex As Exception
            MsgBox(ex.Message + " - ExpandirDs", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ExpandirDs")
        End Try
    End Sub

    Private Sub TxtNumSeries_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNumSeries.TextChanged

    End Sub

    Private Sub cmdModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdModificar.Click
        Modifica()
    End Sub

    Private Sub txtNumMod_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNumMod.KeyUp
        Select Case e.KeyCode
            Case Keys.Enter
                If IsNumeric(txtNumMod.Text) Then
                    If (CLng(txtNumMod.Text) > 0) And (CLng(txtNumMod.Text) <= Me.Cantidad) Then
                        PosModif = CLng(txtNumMod.Text) - 1
                        lblMsg.Text = "Ingrese el número de serie"
                        Me.TxtNumSeries.Focus()
                        Me.TxtNumSeries.SelectAll()
                    Else
                        MsgBox("El número debe estar entre 1 y " + CStr(Me.Cantidad), MsgBoxStyle.Information + MsgBoxStyle.ApplicationModal)
                        Me.txtNumMod.Focus()
                        Me.txtNumMod.SelectAll()
                    End If
                Else
                    MsgBox("Debe ingresar un valor válido", MsgBoxStyle.Information + MsgBoxStyle.ApplicationModal)
                    Me.txtNumMod.Focus()
                    Me.txtNumMod.SelectAll()
                End If

        End Select
    End Sub

    Sub Modifica()
        If Me.Cantidad > 0 Then
            'LblNumMod.Visible = True
            'txtNumMod.Visible = True
            lblMsg.Text = "seleccione la posición del número de serie a modificar e ingresela de nuevo"
            'txtNumMod.Focus()
            dg1.Focus()
            'PosEspecifica = True
        Else
            MsgBox("Nada para modificar", MsgBoxStyle.Information + MsgBoxStyle.ApplicationModal)
        End If
    End Sub

    Private Function FormatGrilla() As Boolean
        Try

            Dim Style As New DataGridTableStyle

            dg1.TableStyles.Clear()
            Style.MappingName = "Consulta"

            Dim TextCol1 As New DataGridTextBoxColumn
            With TextCol1
                .MappingName = "picking_id"
                .HeaderText = "picking_id"
                .Width = 0
            End With
            Style.GridColumnStyles.Add(TextCol1)

            Dim TextCol9 As New DataGridTextBoxColumn
            With TextCol9
                .MappingName = "Posición"
                .HeaderText = "Posición"
                .Width = 0
            End With
            Style.GridColumnStyles.Add(TextCol9)

            Dim TextCol10 As New DataGridTextBoxColumn
            With TextCol10
                .MappingName = "series"
                .HeaderText = "Número de Serie"
                .Width = 150
            End With
            Style.GridColumnStyles.Add(TextCol10)

            dg1.TableStyles.Add(Style)

            Return True

        Catch ex As Exception
            MsgBox("FormaGrilla: " & ex.Message, MsgBoxStyle.OkOnly, "frmSeries")
            Return False
        End Try
    End Function

    Private Sub dg1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dg1.Click
        Try
            dg1.Select(dg1.CurrentRowIndex)
        Catch ex As Exception
        End Try
    End Sub

    
    Private Sub dg1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dg1.KeyUp
        Try
            dg1.Select(dg1.CurrentRowIndex)
        Catch ex As Exception
        End Try
        Select Case e.KeyCode
            Case Keys.Enter
                Me.txtNumMod.Text = dg1.CurrentRowIndex
                Me.TxtNumSeries.Focus()
                Me.lblMsg.Text = "Ingrese el número de serie"
        End Select
    End Sub

  
    Private Sub dg1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dg1.Validating
        'Try
        '    If Me.Cantidad > 0 Then

        '        If DS.Tables(0).Rows.Count > Me.Cantidad Then
        '            Err.Raise(513, "INGRESAR_SERIE", "No puede haber más nros. de series que cantidad de bultos")
        '        End If
        '    End If
        '    e.Cancel = False
        'Catch ex As Exception
        '    MsgBox("dg1_Validating: " & ex.Message, MsgBoxStyle.OkOnly, "frmSeries")
        '    e.Cancel = True
        'End Try
    End Sub
    Function VALIDAR_SERIE_REPETIDA(ByVal SERIE As String) As Boolean
        Try
            Dim ROW() As Data.DataRow
            ROW = DS.Tables(0).Select("SERIES = '" + SERIE + "'")
            If Not IsNothing(ROW) Then
                If ROW.GetLength(0) > 1 Then
                    Return False
                End If
            End If

            Return True
        Catch EX As Exception
            MsgBox("VALIDAR_SERIE: " & EX.Message, MsgBoxStyle.OkOnly, "frmSeries")
            Return False
        End Try
    End Function
    Function VALIDAR_SERIES() As Boolean
        Try
            Dim I As Long
            For I = 0 To DS.Tables(0).Rows.Count - 1
                If Not IsDBNull(DS.Tables(0).Rows(I)("SERIES")) AndAlso DS.Tables(0).Rows(I)("SERIES") <> "" Then
                    If Not VALIDAR_SERIE_REPETIDA(DS.Tables(0).Rows(I)("SERIES")) Then
                        Err.Raise(513, "VALIDAR_SERIES", "Núm de Serie '" & CStr(DS.Tables(0).Rows(I)("SERIES")) & "' repetido.")
                    End If
                End If
            Next

            I = 0
            For Each myrow As Data.DataRow In DS.Tables(0).Rows
                If (IsDBNull(myrow("picking_id"))) OrElse (myrow("picking_id") = "") Then
                    'Err.Raise(513, "VALIDAR_SERIES", "Hay más seríes que bultos")
                    Dim res As Integer
                    res = MsgBox("Hay números de series de más, ¿Quiere eliminarlos?", MsgBoxStyle.OkCancel + MsgBoxStyle.ApplicationModal + MsgBoxStyle.Exclamation, "Números de Series")
                    If res = vbOK Then
                        EliminarRegDeMas()
                    End If
                    Return False
                End If
                If myrow("series") <> "" Then I += 1
            Next

            Me.Cantidad = I
            Return True

        Catch ex As Exception
            MsgBox("VALIDAR_SERIES: " & ex.Message, MsgBoxStyle.OkOnly, "frmSeries")
            Return False
        End Try
    End Function
    Sub EliminarRegDeMas()
        Try
            While EliminarRegDeMasF()

            End While

        Catch ex As Exception
            MsgBox("EliminarRegDeMas: " & ex.Message, MsgBoxStyle.OkOnly, "frmSeries")
        End Try
    End Sub
    Function EliminarRegDeMasF() As Boolean
        Try
            For Each myrow As Data.DataRow In DS.Tables(0).Rows
                If (IsDBNull(myrow("picking_id"))) OrElse (myrow("picking_id") = "") Then
                    myrow.Delete()
                    Return True
                End If
            Next
            Return False

        Catch ex As Exception
            MsgBox("EliminarRegDeMasF: " & ex.Message, MsgBoxStyle.OkOnly, "frmSeries")
            Return False
        End Try
    End Function
    Sub cerrar()
        If Not VALIDAR_SERIES() Then
            Exit Sub
        Else
            Me.Close()
        End If
    End Sub
    Sub cancelar()
        Me.DS.Tables(0).Rows.Clear()
        Me.Close()
    End Sub

    Private Sub dg1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dg1.CurrentCellChanged

    End Sub

    Private Sub cmdCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCerrar.Click
        cancelar()
    End Sub
End Class