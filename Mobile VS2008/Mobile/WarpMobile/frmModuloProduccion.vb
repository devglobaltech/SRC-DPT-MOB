Imports System.Data

Public Class frmModuloProduccion

    Private OBJ As cModuloProduccion
    Private Const SQLConErr As String = "Fallo al intentar conectar con la base de datos."
    Private Const frmName As String = "Modulo de ensamble"
    Private OperacionEnCurso As Boolean = False

    Private Sub frmModuloProduccion_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode
            Case Keys.F1
                Me.Comenzar()
            Case Keys.F2

            Case Keys.F3

            Case Keys.F4

        End Select
    End Sub

    Private Sub frmModuloProduccion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InicializarFormulario()
    End Sub

    Private Sub InicializarFormulario()

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
            Me.cmbClientes.DataSource = Nothing
            If Not OBJ.GetClientesByUser(Me.cmbClientes) Then Exit Try

        Catch ex As Exception
            MsgBox(ex, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

    Public Sub New()
        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        OBJ = New cModuloProduccion
        OBJ.Conexion = SQLc
    End Sub

    Protected Overrides Sub Finalize()
        OBJ = Nothing
        MyBase.Finalize()
    End Sub

    Private Sub cmbClientes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbClientes.KeyUp
        If e.KeyValue = 13 Then
            OBJ.Cliente = Trim(Me.cmbClientes.SelectedValue)
            Me.cmbClientes.Enabled = False
            Me.Close()
        End If
    End Sub

    Private Sub btnComenzar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComenzar.Click
        Comenzar()
    End Sub

    Private Sub Comenzar()
        Try
            Me.lblTitulo.Visible = True
            Me.cmbClientes.Visible = True
            Me.cmbClientes.Focus()
            OperacionEnCurso = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, frmName)
        End Try
    End Sub

End Class