using System.Net;
using Tcp.Server.Servers;
using Tcp.Server.Interaces;

namespace telnet_chat_server
{
    class Program
    {
        static void Main(string[] args)
        {
            IServer server = new BroadcastChatServer(IPAddress.Any);
            server.StartListening();

            while (true) { }
        }
    }
}
