
Imports System.Data.SqlClient
Public Class frmGestionPalletViaje
    Private FrmName As String = "Consulta"
    Private Const vMenu As String = "F1) Volver."
    Private sTipoConsulta As Integer
    Private Const SQLConnErr As String = "Fallo al conectar con la base de datos."
    Private strViaje As String = ""
    Private strPedido As String = ""
    Private Ds As New Data.DataSet

    Public Property TipoConsulta() As Integer
        Get
            Return sTipoConsulta
        End Get
        Set(ByVal value As Integer)
            sTipoConsulta = value
        End Set
    End Property
  
    Private oDataSet As Data.DataSet
    Public Property DataSet() As Data.DataSet
        Get
            Return oDataSet
        End Get
        Set(ByVal value As Data.DataSet)
            oDataSet = value
        End Set
    End Property

    Public Property ViajeId() As String
        Get
            Return strViaje
        End Get
        Set(ByVal value As String)
            strViaje = value
        End Set
    End Property
    Public Property PedidoId() As String
        Get
            Return strPedido
        End Get
        Set(ByVal value As String)
            strPedido = value
        End Set
    End Property
  
    Private Sub FormatGrilla()
        Try

            'Instanciamos y creamos una nueva tabla de estilos
            Dim Style As New DataGridTableStyle
            Style.MappingName = "Consulta"
            dgPallet.TableStyles.Clear()
            'Le indicamos el nombre de la tabla de nuestro DataSet
            'If Me.TipoConsulta = TipoForm.GestionPallet.Faltantes Then
            'Style.MappingName = "Consulta"
            'Else
            'Style.MappingName = "Consulta"
            'End If

            'Instanciamos y creamos nuestro primer formato de columna
            Dim TextCol1 As New DataGridTextBoxColumn
            'Le indicamos el nombre de la columna 
            TextCol1.MappingName = "NRO_PALLET"
            'Le indicamos el nombre que deseamos aparezca en el encabezado de la columna
            TextCol1.HeaderText = "Nro. Pallet"
            'Le indicamos el ancho de la columna
            TextCol1.Width = 150
            'Agregamos nuestro formato de columna a la colección de nuestra Tabla de estilos
            Style.GridColumnStyles.Add(TextCol1)

            dgPallet.TableStyles.Add(Style)

        Catch ex As Exception
            MsgBox("FormatGrilla: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub frmGestionPalletViaje_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Try
            Select Case e.KeyCode
                'Volver
                Case Keys.F1
                    Me.Close()

            End Select
        Catch ex As Exception
            MsgBox("frmGestionPalletViaje_KeyUp: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Private Sub frmGestionPalletViaje_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            If strViaje = "" Then
                If GetValues(strPedido, "Pedido") Then
                    Me.dgPallet.DataSource = Ds.Tables("Consulta")
                    Me.Text = FrmName
                    FormatGrilla()
                Else
                    Me.Close()
                End If
            Else

                If GetValues(strViaje, "Viaje") Then
                    Me.dgPallet.DataSource = Ds.Tables("Consulta")
                    Me.Text = FrmName
                    FormatGrilla()
                Else
                    Me.Close()
                End If
            End If
            
        Catch ex As Exception
            MsgBox("frmGestionPalletViaje_Load: " & ex.Message, MsgBoxStyle.OkOnly, FrmName)
        End Try
    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Me.Close()
    End Sub

    Private Function GetValues(ByVal filtroBusqueda As String, ByVal tipoBusqueda As String) As Boolean
        Dim Cmd As SqlCommand
        Dim Pa As SqlParameter
        Dim Da As SqlDataAdapter
        Try
            If VerifyConnection(SQLc) Then
                Cmd = SQLc.CreateCommand
                Cmd.Connection = SQLc
                Cmd.CommandType = Data.CommandType.StoredProcedure
                Da = New SqlDataAdapter(Cmd)

                If Me.TipoConsulta = TipoForm.GestionPallet.Faltantes Then
                    If tipoBusqueda = "Pedido" Then
                        Cmd.CommandText = "Mob_IngresarPedidos_Pendiente"
                        Pa = New SqlParameter("@PedidoId", Data.SqlDbType.VarChar, 100)
                        Pa.Value = filtroBusqueda
                        Cmd.Parameters.Add(Pa)
                    Else
                        Cmd.CommandText = "Mob_IngresarViajes_Pendiente"
                        Pa = New SqlParameter("@ViajeId", Data.SqlDbType.VarChar, 100)
                        Pa.Value = filtroBusqueda
                        Cmd.Parameters.Add(Pa)
                    End If

                ElseIf Me.TipoConsulta = TipoForm.GestionPallet.Ingresados Then
                    If tipoBusqueda = "Pedido" Then
                        Cmd.CommandText = "Mob_IngresarPedidos_Controlado"
                        Pa = New SqlParameter("@PedidoId", Data.SqlDbType.VarChar, 100)
                        Pa.Value = filtroBusqueda
                        Cmd.Parameters.Add(Pa)
                    Else
                        Cmd.CommandText = "Mob_IngresarViajes_Controlado"
                        Pa = New SqlParameter("@ViajeId", Data.SqlDbType.VarChar, 100)
                        Pa.Value = filtroBusqueda
                        Cmd.Parameters.Add(Pa)
                    End If

                End If
               
                Da.Fill(Ds, "Consulta")
            Else
                MsgBox(SQLConnErr, MsgBoxStyle.OkOnly, FrmName)
            End If
            Return True
        Catch SQLEx As SqlException
            MsgBox("GetValues SQL: " & SQLEx.Message)
            Return False
        Catch ex As Exception
            MsgBox("GetValues : " & ex.Message)
            Return False
        Finally
            Da = Nothing
            Cmd = Nothing
            Pa = Nothing
        End Try
    End Function

End Class