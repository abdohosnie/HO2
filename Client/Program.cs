using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        // Setting
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Connect
        IPAddress host = IPAddress.Parse("127.0.0.1");
        IPEndPoint hostEndPoint = new IPEndPoint(host, 9090);
        clientSocket.Connect(hostEndPoint);
        Console.WriteLine("Connected to server!");

        string clientMessage = "";
        string serverMessage = "";

        while (serverMessage.ToLower() != "bye" && clientMessage.ToLower() != "bye")
        {
            // Receive
            byte[] recievedData = new byte[1024];
            int receivedBytesLen = clientSocket.Receive(recievedData);

            serverMessage = Encoding.UTF8.GetString(recievedData, 0, receivedBytesLen);
            Console.WriteLine("Server Said: {0}", serverMessage);

            Console.WriteLine("Write you message: ");
            clientMessage = Console.ReadLine();

            byte[] sendData = Encoding.UTF8.GetBytes(clientMessage);
            clientSocket.Send(sendData);

            Console.WriteLine("Message has been sent.");
        }
        Console.WriteLine("Convo Ended");
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }
}