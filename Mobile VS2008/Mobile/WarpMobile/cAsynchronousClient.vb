Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text


' State object for receiving data from remote device.

Public Class StateObject
    ' Client socket.
    Public workSocket As Socket = Nothing
    ' Size of receive buffer.
    Public Const BufferSize As Integer = 256
    ' Receive buffer.
    Public buffer(BufferSize) As Byte
    ' Received data string.
    Public sb As New StringBuilder
End Class 'StateObject


Public Class AsynchronousClient

    ' The port number for the remote device.
    Private Shared port As Integer
    Private Shared IpDirection As String = ""
    ' ManualResetEvent instances signal completion.
    Private Shared connectDone As New ManualResetEvent(False)
    Private Shared sendDone As New ManualResetEvent(False)
    Private Shared receiveDone As New ManualResetEvent(False)
    Public Shared blnCerrar As Boolean = False
    Public Shared Event Validado(ByVal value As Boolean)
    ' The response from the remote device.
    Private Shared response As String = String.Empty

    ' Create a TCP/IP socket.
    Public client As Socket
    Public Shared Connected As Boolean = False

    Public ReadOnly Property SocketConnected() As Boolean
        Get
            Return client.Connected
        End Get
    End Property


    Public Property IsConnected() As Boolean
        Get
            Return Connected
        End Get
        Set(ByVal value As Boolean)
            Connected = value
        End Set
    End Property



    Public Property IPAdd() As String
        Get
            Return IpDirection
        End Get
        Set(ByVal value As String)
            IpDirection = value
        End Set
    End Property

    Public Property nPort() As Integer
        Get
            Return port
        End Get
        Set(ByVal value As Integer)
            port = value
        End Set
    End Property

    Public Sub Cerrar()
        blnCerrar = True
        SendMessage("CERRAR")
    End Sub

    Private Function VerifyServer() As Boolean
        Dim clientSocket As New System.Net.Sockets.TcpClient(), Output As String = ""
        Try
            'Este artilugio lo necesito para saber si puedo conectarme al server. Como la conexion es asincronica me contesta en
            'en una rutina posterior a la que vuelvo a reconectar. De esta forma sincronicamente me entero si el server esta activo
            'o si puedo llegar a el.
            clientSocket.Connect(IpDirection, port)
            Dim data(255) As [Byte]
            data = System.Text.Encoding.ASCII.GetBytes("T")
            Dim stream As NetworkStream = clientSocket.GetStream()
            stream.Write(data, 0, data.Length)
            data = New [Byte](255) {}
            Dim responseData As String = String.Empty
            Dim bytes As Int32 = stream.Read(data, 0, data.Length)
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes)
            If responseData <> "" Then
                Return True
            End If
        Catch ex As Exception
            Return False
        Finally
            clientSocket.Close()
            clientSocket = Nothing
        End Try
    End Function

    Private Function Connect2() As Boolean
        Dim vReturn As Boolean = False
        If client.Poll(0, SelectMode.SelectRead) = True Then
            Dim byteArray As Byte() = New Byte(1) {}
            If (client.Receive(byteArray, SocketFlags.Peek)) = 0 Then
                vReturn = True
            End If
        Else
            Return True
        End If
        Return vReturn
    End Function


    Public Function Actividad(Optional ByVal Msg As Boolean = True) As Boolean
        Dim cont As Integer = 0, vConnected As Boolean = False
        Try
            vConnected = Connected
            If vConnected And client.Connected Then
                If SendMessage("#ACT#") Then
                    Return True
                End If
            Else
                If CerrarSistemaOperativo() Then
                    MsgBox("Se perdio la conexion con el Servidor de Licencias mobiles. La aplicacion sera cerrada. ", MsgBoxStyle.Information)
                    Try

                        TrdLic.Abort()
                    Catch ex As Exception
                    End Try
                    Application.Exit()
                End If
                If Not client.Connected Then vConnected = False
                Do Until (vConnected = True) Or (cont = 3)
                    cont = cont + 1
                    If (VerifyServer()) Then
                        Reconect()
                        vConnected = True
                        If SendMessage("#ACT#") Then
                            Return True
                        End If
                    Else
                        vConnected = False
                    End If
                    Thread.Sleep(1000)
                Loop
                If Not vConnected Then
                    If Msg Then
                        MsgBox("Se perdio la conexion con el servidor de licencias mobiles. Intentelo nuevamente en unos segundos.", MsgBoxStyle.Information, "Informacion")
                    End If
                    Return False
                Else
                    Return True
                End If
                End If
        Catch ex As Exception
            'Si Falla tendre q revisar la conexion
            Reconect()
            Return False
        End Try
    End Function

    Public Function SendMessage(ByVal Message As String) As Boolean
        Try
            Send(client, Message)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Reconect() As Boolean
        Try
            Dim ipAddr As System.Net.IPAddress
            ipAddr = Net.IPAddress.Parse(IpDirection)
            Dim remoteEP As New IPEndPoint(ipAddr, port)
            client = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            ' Connect to the remote endpoint.
            client.BeginConnect(remoteEP, New AsyncCallback(AddressOf ConnectCallback), client)
            sendDone.WaitOne()
            'Receive the response from the remote device.
            Receive(client)
            'receiveDone.WaitOne()
            Connected = True
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub Main()
        ' Establish the remote endpoint for the socket.
        ' For this example use local machine.

        Dim ipAddr As System.Net.IPAddress
        ipAddr = Net.IPAddress.Parse(IpDirection)
        Dim remoteEP As New IPEndPoint(ipAddr, port)

        client = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        Connected = True
        ' Connect to the remote endpoint.
        client.BeginConnect(remoteEP, New AsyncCallback(AddressOf ConnectCallback), client)

        ' Wait for connect.
        connectDone.WaitOne()

        ' Send test data to the remote device.
        Send(client, "INICIALIZAR")
        Thread.Sleep(1000)
        sendDone.WaitOne()

        ' Receive the response from the remote device.
        Receive(client)
        receiveDone.WaitOne()

        ' Write the response to the console.
        'Debug.Print("Response received : {0}", response)
        ' Release the socket.
        client.Shutdown(SocketShutdown.Both)
        client.Close()

        If client.Connected Then
            Reconect()
        End If

    End Sub 'Main


    Private Shared Sub ConnectCallback(ByVal ar As IAsyncResult)
        ' Retrieve the socket from the state object.
        Dim client As Socket = CType(ar.AsyncState, Socket)
        ' Complete the connection.
        client.EndConnect(ar)
        ' Signal that the connection has been made.
        connectDone.Set()
    End Sub 'ConnectCallback


    Private Shared Sub Receive(ByVal client As Socket)

        ' Create the state object.
        Dim state As New StateObject
        state.workSocket = client

        ' Begin receiving the data from the remote device.
        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReceiveCallback), state)
    End Sub 'Receive


    Private Shared Sub ReceiveCallback(ByVal ar As IAsyncResult)
        Try
            ' Retrieve the state object and the client socket 
            ' from the asynchronous state object.
            Dim state As StateObject = CType(ar.AsyncState, StateObject)
            Dim client As Socket = state.workSocket

            ' Read data from the remote device.
            Dim bytesRead As Integer = client.EndReceive(ar)

            If bytesRead > 0 Then
                ' There might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead))

                Select Case Encoding.ASCII.GetString(state.buffer, 0, bytesRead)
                    Case "#LICENCIA#1"
                        RaiseEvent Validado(True)
                    Case "#LICENCIA#0"
                        RaiseEvent Validado(False)
                        MsgBox("No quedan licencias concurrentes disponibles. La aplicacion sera cerrada.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Depot Mobile")
                        Try
                            TrdLic.Abort()
                        Catch ex As Exception
                        End Try
                        Application.Exit()
                    Case "#ST#"
                        Send(client, "#OK#")
                End Select

                ' Get the rest of the data.
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReceiveCallback), state)
            Else
                ' All the data has arrived; put it in response.
                If state.sb.Length > 1 Then
                    response = state.sb.ToString()
                End If
                ' Signal that all bytes have been received.
                receiveDone.Set()
            End If
        Catch Exs As SocketException
            Connected = False
        End Try
    End Sub 'ReceiveCallback


    Private Shared Sub Send(ByVal client As Socket, ByVal data As String)
        ' Convert the string data to byte data using ASCII encoding.
        Dim byteData As Byte() = Encoding.ASCII.GetBytes(data)

        ' Begin sending the data to the remote device.
        client.BeginSend(byteData, 0, byteData.Length, 0, New AsyncCallback(AddressOf SendCallback), client)
    End Sub 'Send

    Private Shared Sub SendCallback(ByVal ar As IAsyncResult)
        Try
            ' Retrieve the socket from the state object.
            Dim client As Socket = CType(ar.AsyncState, Socket)

            ' Complete sending the data to the remote device.
            Dim bytesSent As Integer = client.EndSend(ar)
            Console.WriteLine("Sent {0} bytes to server.", bytesSent)

            ' Signal that all bytes have been sent.
            sendDone.Set()
        Catch ex As Exception
            If Not blnCerrar Then
                MsgBox("No se encontro el servidor de licencias mobiles en la direccion " & IpDirection)
                RaiseEvent Validado(False)
            End If
        End Try

    End Sub 'SendCallback

    Function StateConnection() As Boolean
        Dim blockingState As Boolean = client.Blocking
        IsConnected = False
        Try
            Dim tmp(0) As Byte
            client.Blocking = False
            client.Send(tmp, 0, 0)
            Return True
        Catch e As SocketException
            If e.NativeErrorCode.Equals(10035) Then
                Return True
            Else : Return False
            End If
            'Throw New Exception("State Connection")
        Finally
            client.Blocking = blockingState
        End Try
    End Function
End Class 'AsynchronousClient