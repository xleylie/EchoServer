using System.Net;
using System.Net.Sockets;
using System.Text;

int port = 5000;
using Socket socket = new Socket(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp
    );
IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, port);

socket.Bind(endPoint);
socket.Listen(10);

var messageBytes = new byte[1024];
List<byte> receivedData = new List<byte>();
int received;

var connectedClient=socket.Accept();

while ((received = connectedClient.Receive(messageBytes)) > 0)
{
    receivedData.AddRange(messageBytes.Take(received));
}

var messageBytesArray=receivedData.ToArray();

connectedClient.Send(messageBytesArray);
connectedClient.Close();