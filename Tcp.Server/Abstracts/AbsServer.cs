using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Tcp.Server.Interaces;

namespace Tcp.Server.Abstracts
{
    public abstract class AbsServer : IServer
    {
        protected TcpListener ServerSocket { get; }

        protected Task ListeningTask { get; set; } = null;

        protected List<TcpClient> ConnectedClients { get; } = new List<TcpClient>();

        public AbsServer(IPAddress address, int port = 10000)
        {
            ServerSocket = new TcpListener(address, port);
        }

        public int ConnectedClient => ConnectedClients.Count;

        public bool ListeningStarted => ListeningTask is not null;

        public abstract void Shutdown();

        public abstract void StartListening();
    }
}
