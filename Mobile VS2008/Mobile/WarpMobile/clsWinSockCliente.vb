Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.IO

Public Class clsWinSockCliente

#Region "VARIABLES"
    Private Stm As Stream 'Utilizado para enviar datos al Servidor y recibir datos del mismo
    Private m_IPDelHost As String 'Direccion del objeto de la clase Servidor
    Private m_PuertoDelHost As String 'Puerto donde escucha el objeto de la clase Servidor

    Private tcpClnt As TcpClient
    Private tcpThd As Thread 'Se encarga de escuchar mensajes enviados por el Servidor
    Private s As Socket

#End Region

#Region "EVENTOS"
    Public Event ConexionTerminada()
    Public Event DatosRecibidos(ByVal datos As String)
#End Region

#Region "PROPIEDADES"
    Public Property IPDelHost() As String
        Get
            IPDelHost = m_IPDelHost
        End Get

        Set(ByVal Value As String)
            m_IPDelHost = Value
        End Set
    End Property

    Public Property PuertoDelHost() As String
        Get
            PuertoDelHost = m_PuertoDelHost
        End Get
        Set(ByVal Value As String)
            m_PuertoDelHost = Value
        End Set
    End Property
#End Region

#Region "METODOS/FUNCIONES PUBLIC@S"

    Public Function SocketSendReceive(ByVal Server As String, ByVal Port As Integer) As String
        Dim ascii As Encoding = Encoding.ASCII
        Dim request As String = ""
        Dim bytesSent As [Byte]()
        Dim bytesReceived(255) As [Byte]
        Dim page As [String] = ""
        Dim bytes As Int32

        Try
            bytesSent = ascii.GetBytes(request)
            s = ConnectSocket(Server, Port)
            ' Create a socket connection with the specified server and port.
            If s Is Nothing Then
                Return "No se pudo conectar"
            End If
            ' Send request to the server.
            s.Send(bytesSent, bytesSent.Length, 0)

            ' Receive the server  home page content.

            bytes = s.Receive(bytesReceived, bytesReceived.Length, 0)
            page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes)


        Catch ex As Exception
            page = "No se pudo conectar"
            s.Shutdown(SocketShutdown.Both)
            s.Close()
        End Try
        Return page
    End Function

    'Procedimiento para cerrar la conexión con el servidor
    Public Sub Desconectar()
        Try
            'desconectamos del servidor
            s.Shutdown(SocketShutdown.Both)
            s.Close()
            tcpClnt.Close()

            'abortamos el hilo (thread)
            tcpThd.Abort()

        Catch sEx As SocketException
            MsgBox(sEx.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub EnviarDatos(ByVal Datos As String)
        Dim BufferDeEscritura() As Byte

        BufferDeEscritura = Encoding.ASCII.GetBytes(Datos)

        If Not (Stm Is Nothing) Then
            'Envio los datos al Servidor
            Stm.Write(BufferDeEscritura, 0, BufferDeEscritura.Length)
        End If
    End Sub

#End Region

#Region "FUNCIONES PRIVADAS"
    Private Sub LeerSocket()
        Dim BufferDeLectura() As Byte

        While True
            Try
                BufferDeLectura = New Byte(100) {}
                'Me quedo esperando a que llegue algun mensaje
                Stm.Read(BufferDeLectura, 0, BufferDeLectura.Length)

                'Genero el evento DatosRecibidos, ya que se han recibido datos desde el Servidor
                RaiseEvent DatosRecibidos(Encoding.ASCII.GetString(BufferDeLectura, 0, BufferDeLectura.Length))
            Catch e As Exception
                Exit While
            End Try
        End While

        'Finalizo la conexion, por lo tanto genero el evento correspondiente
        RaiseEvent ConexionTerminada()
    End Sub

    Private Function ConnectSocket(ByVal Server As String, ByVal Port As Integer) As Socket
        Dim Sock As Socket = Nothing
        Dim hostEntry As IPHostEntry = Nothing
        Dim address As IPAddress

        Try
            ' Get host related information.
            hostEntry = Dns.GetHostEntry(Server)

            ' Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
            ' an exception that occurs when the host host IP Address is not compatible with the address 
            ' family() (typical in the IPv6 case).

            For Each address In hostEntry.AddressList
                Dim endPoint As New IPEndPoint(address, Port)
                Dim tempSocket As New Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)

                tempSocket.Connect(endPoint)

                If tempSocket.Connected Then
                    Sock = tempSocket
                    Exit For
                End If

            Next address

            tcpClnt = New TcpClient(Sock.AddressFamily)
            Stm = New NetworkStream(Sock)
            tcpThd = New Thread(AddressOf LeerSocket)
            tcpThd.Start()

        Catch sEx As SocketException
            MsgBox(sEx.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return Sock
    End Function

#End Region

End Class
