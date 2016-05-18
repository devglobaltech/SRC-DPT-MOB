#Region "Comentarios de Clase"
'===============================================================================================
'Para desactivar el control de licencias en Mobile, se debe comentar las siguientes llamadas:
'===============================================================================================
'1. frmMenuPrincipal.LBMenu_KeyUp 
'	SC.Actividad()
'	SC.CerrarConexion()

'2. frmLogin.Conectar()
'	SC = New ClsSocket
'        SC.Host = Me.IpServer
'        SC.Port = Me.PortServer
'	iSock

'3. ModuloGral.VerifyConnection
'	SC.Actividad()

'4. ModuloGral
'   Public SC As New ClsSocket

#End Region


Option Explicit On

Imports System.Net.Sockets
Imports System.Text

Public Class ClsSocket

    Private vHost As String
    Private vPort As String
    Private clientSocket As New System.Net.Sockets.TcpClient()
    Private Const ClsName As String = "Verificacion de Licencias."
    Private Connected As Boolean = False

    Public Property Host() As String
        Get
            Return vHost
        End Get
        Set(ByVal value As String)
            vHost = value
        End Set
    End Property

    Public Property Port() As String
        Get
            Return vPort
        End Get
        Set(ByVal value As String)
            vPort = value
        End Set
    End Property

    Public Function IniciarConexion() As Boolean
        Dim output As String = ""
        Try
            If Connected Then
                Return True
            End If
            'clientSocket = New TcpClient(Host, port)
            'clientSocket.SendTimeout = 2000
            'clientSocket.ReceiveTimeout = 2000

            clientSocket.Connect(Host, Port)
            Me.Connected = True
            ' Translate the passed message into ASCII and store it as a byte array.
            Dim data(255) As [Byte]
            data = System.Text.Encoding.ASCII.GetBytes("INICIALIZAR")
            ' Get a client stream for reading and writing. Stream stream = client.GetStream();
            Dim stream As NetworkStream = clientSocket.GetStream()
            ' Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length)
            ' Buffer to store the response bytes.
            data = New [Byte](255) {}
            ' String to store the response ASCII representation.
            Dim responseData As String = String.Empty
            ' Read the first batch of the TcpServer response bytes.
            Dim bytes As Int32 = stream.Read(data, 0, data.Length)
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes)

            If responseData = "1" Then
                Return True
            Else
                MsgBox("No quedan licencias concurrentes disponibles. La aplicacion sera cerrada.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, ClsName)
                Return False
            End If

        Catch es As System.Net.Sockets.SocketException
            output = "No se encontro el servidor de Conexiones Mobiles en la direccion " & Host & ", puerto: " & Port & vbNewLine & "La aplicacion sera cerrada." '& vbNewLine & "SocketException: " + es.ToString()
            MsgBox(output, MsgBoxStyle.Exclamation, "Servidor de Licencias Mobiles")
        Catch ex As Exception
            output = "No se encontro el servidor de Conexiones Mobiles en la direccion " & Host & ", puerto: " & Port & vbNewLine & "La aplicacion sera cerrada." '& vbNewLine & "SocketException: " + ex.ToString()
            MessageBox.Show(output)
        End Try
    End Function

    Public Function CerrarConexion() As Boolean
        Dim output As String = ""
        Try
            ' Translate the passed message into ASCII and store it as a byte array.
            Dim data(255) As [Byte]
            data = System.Text.Encoding.ASCII.GetBytes("CERRAR")
            ' Get a client stream for reading and writing. Stream stream = client.GetStream();
            Dim stream As NetworkStream = clientSocket.GetStream()
            ' Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length)
            output = "Sent: " + "CERRAR"
            ' Buffer to store the response bytes.
            data = New [Byte](255) {}
            ' String to store the response ASCII representation.
            Dim responseData As String = String.Empty
            ' Read the first batch of the TcpServer response bytes.
            Dim bytes As Int32 = stream.Read(data, 0, data.Length)
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes)
            output = "Received: " + responseData
            ' Close everything.
            stream.Close()
            clientSocket.Close()
            Return True
        Catch io As IO.IOException
            'retorno true porque es el mensaje de cierre.
            Return True
        Catch ex As Exception
            'retorno true porque es el mensaje de cierre.
            Return True
        End Try
    End Function

    Public Function Actividad() As Boolean
        Dim output As String = ""
        Dim recon As Integer = 0
        Try
            Dim data(255) As [Byte]
            Dim responseData As String = String.Empty
            data = System.Text.Encoding.ASCII.GetBytes("ACT")
            Try
                If Not IsNothing(clientSocket) Then
                    Dim stream As NetworkStream = clientSocket.GetStream()
                    stream.Write(data, 0, data.Length)
                    data = New [Byte](255) {}
                    Dim bytes As Int32 = stream.Read(data, 0, data.Length)
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes)
                Else
                    Throw New IO.IOException("Forzado")
                End If
            Catch io As IO.IOException
                '======================================================================================================================
                'Esta rutina se ocupa de reintentar conectar hasta 3 veces antes de devolver un mensaje al usuario.
                '======================================================================================================================
                Connected = False
                While recon <= 3
                    System.Threading.Thread.Sleep(1000)
                    recon = recon + 1
                    Try
                        If ReConnect() Then
                            Dim stream As NetworkStream = clientSocket.GetStream()
                            stream.Write(data, 0, data.Length)
                            data = New [Byte](255) {}
                            Dim bytes As Int32 = stream.Read(data, 0, data.Length)
                            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes)
                        End If
                        If Connected Then Exit While
                    Catch ex As Exception
                    End Try
                End While
                If Not Connected Then
                    MsgBox("Se perdio la conexion con el servidor de licencias mobiles. Intentelo nuevamente en unos segundos.", MsgBoxStyle.Information, "Informacion")
                    Return False
                End If
            Catch ex As Exception
                '======================================================================================================================
                'Esta rutina esta de esta manera porque cuando se ejecuta desde un entorno win32 en ocaciones sale por error general
                'y no por un IO.Exception. Entonces hace que se devuelva un error de sockets pero desde la rutina del menu donde posee
                'un catch de una excepcion generica. Para que no salga por esa excepcion trabajo esta incidente de esta forma.
                '======================================================================================================================
                Connected = False
                While recon <= 3
                    System.Threading.Thread.Sleep(1000)
                    recon = recon + 1
                    Try
                        If ReConnect() Then
                            Dim stream As NetworkStream = clientSocket.GetStream()
                            stream.Write(data, 0, data.Length)
                            data = New [Byte](255) {}
                            Dim bytes As Int32 = stream.Read(data, 0, data.Length)
                            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes)
                        End If
                        If Connected Then Exit While
                    Catch exs As Exception
                        'lo dejo ciego.
                    End Try
                End While
                If Not Connected Then
                    MsgBox("Se perdio la conexion con el servidor de licencias mobiles. Intentelo nuevamente en unos segundos.", MsgBoxStyle.Information, "Informacion")
                    Return False
                End If
            End Try
            If responseData = "1" Then
                Return True
            Else
                MsgBox("Se perdio la conexion con el servidor de licencias mobiles. Intentelo nuevamente en unos segundos.", MsgBoxStyle.Information, "Informacion")
                Return False
            End If
        Catch e As ArgumentNullException
            output = "ArgumentNullException: " + e.ToString()
            MessageBox.Show(output)
        Catch ex As SocketException
            output = "SocketException: " + ex.ToString()
            MessageBox.Show(output)
        End Try
    End Function

    Private Function ReConnect() As Boolean
        Dim output As String = ""
        Try
            clientSocket.Close()
            clientSocket = Nothing
            clientSocket = New System.Net.Sockets.TcpClient()
            clientSocket.Connect(Host, Port)
            Dim data(255) As [Byte]
            data = System.Text.Encoding.ASCII.GetBytes("T")
            Dim stream As NetworkStream = clientSocket.GetStream()
            stream.Write(data, 0, data.Length)
            data = New [Byte](255) {}
            Dim responseData As String = String.Empty
            Dim bytes As Int32 = stream.Read(data, 0, data.Length)
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes)
            If responseData = "1" Then
                Connected = True
                Return True
            Else
                Return False
            End If
        Catch e As ArgumentNullException
            Return False
        Catch e As SocketException
            output = "No se encontro el servidor de Conexiones Mobiles en la direccion " & Host & ", puerto: " & Port
            Return False
        End Try
    End Function

End Class
