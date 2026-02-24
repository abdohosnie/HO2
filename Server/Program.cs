using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        // Setting
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 9090);
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Bind
        serverSocket.Bind(localEndPoint);

        // Listen
        serverSocket.Listen(5);
        Console.WriteLine("Server is listening on port 9090...");

        //Accept
        Socket clientSocket = serverSocket.Accept();
        Console.WriteLine("client connected!");

        string serverMessage = "";
        string clientMessage = "";

        while (serverMessage.ToLower() != "bye" && clientMessage.ToLower() != "bye")
        {
            Console.WriteLine("Server: ");
            serverMessage = Console.ReadLine();

            byte[] sendData = Encoding.UTF8.GetBytes(serverMessage);
            clientSocket.Send(sendData);
            Console.WriteLine("Message has been sent.");

            byte[] receiveData = new byte[1024];
            int receivedBytesLen = clientSocket.Receive(receiveData);

            clientMessage = Encoding.UTF8.GetString(receiveData, 0, receivedBytesLen);
            Console.WriteLine("Client: " + clientMessage);
        }
        Console.WriteLine("Convo Ended");
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
        serverSocket.Close();
    }
}