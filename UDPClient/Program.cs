using System.Net;
using System.Net.Sockets;
using System.Text;

int port = 5001;
using Socket clientSocket = new Socket(
    AddressFamily.InterNetwork,
    SocketType.Dgram,
    ProtocolType.Udp
    );

IPEndPoint endPoint=new IPEndPoint(IPAddress.Loopback,port);

Console.WriteLine("Введите ваше сообщение: ");
var message = Console.ReadLine();
byte[] data = Encoding.UTF8.GetBytes(message);
clientSocket.SendTo(data, endPoint);


clientSocket.ReceiveTimeout = 5000;
var buffer = new byte[65535];
EndPoint from = new IPEndPoint(IPAddress.Any, 0);

try
{
    int received = clientSocket.ReceiveFrom(buffer, ref from);
    string response = Encoding.UTF8.GetString(buffer, 0, received);
    Console.WriteLine("Ответ сервера: ");
    Console.WriteLine(response);
}
catch (SocketException)
{
    Console.WriteLine("Ответ не получен");
}

clientSocket.Close();