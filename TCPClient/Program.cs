using System.Net;
using System.Net.Sockets;
using System.Text;

int port = 5000;
using Socket clientSocket = new Socket(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp
    );
IPEndPoint IpendPoint = new IPEndPoint(IPAddress.Loopback, port);

clientSocket.Connect( IpendPoint );

while (true)
{
    Console.WriteLine("Введите ваше сообщение: (0-Exit) ");
    var message = Console.ReadLine();
    if(message == null || message == "0" || message == "exit")
    {
        break;
    }

    byte[] data = Encoding.UTF8.GetBytes(message);
    clientSocket.Send(data);
    
}
clientSocket.Shutdown(SocketShutdown.Send);

var responseBytes = new byte[1024];
List<byte> receivedData = new List<byte>();
int received;

while ((received = clientSocket.Receive(responseBytes)) > 0)
{
    receivedData.AddRange(responseBytes.Take(received));
}

var responseBytesArray = receivedData.ToArray();
var response = Encoding.UTF8.GetString(responseBytesArray, 0, responseBytesArray.Length);
Console.WriteLine("Ответ сервера:");
Console.WriteLine(response);

clientSocket.Close();