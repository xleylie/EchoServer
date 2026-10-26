using System.Net;
using System.Net.Sockets;
using System.Text;

int port = 5001;
using Socket socket = new Socket(
    AddressFamily.InterNetwork,
    SocketType.Dgram,
    ProtocolType.Udp
    );

socket.Bind(new IPEndPoint(IPAddress.Loopback, port));

var messageBytes = new byte[1024];
EndPoint remote=new IPEndPoint(IPAddress.Any, 0);

int received = socket.ReceiveFrom(messageBytes, ref remote);

string text = Encoding.UTF8.GetString(messageBytes, 0, received);
Console.WriteLine($"Получено: {text}");

socket.SendTo(messageBytes.AsSpan(0, received), SocketFlags.None, remote);