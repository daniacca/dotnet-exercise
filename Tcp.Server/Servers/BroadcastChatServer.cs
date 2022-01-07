using System.Text;
using System.Net;
using System.Net.Sockets;
using System;
using System.IO;
using Tcp.Server.Abstracts;
using System.Threading.Tasks;

namespace Tcp.Server.Servers
{
    public class BroadcastChatServer : AbsServer
    {
        private readonly object _lock = new();

        public BroadcastChatServer(IPAddress address, int port = 10000) : base(address, port)
        { }

        #region public methods
        public override void StartListening()
        {
            if (ListeningTask is null)
                ListeningTask = ListeningHandler();
        }

        public override void Shutdown()
        {
            if (ListeningTask is not null)
                ServerSocket.Stop();
        }
        #endregion

        #region Private methods
        private void CleanReadBuffer(TcpClient client)
        {
            var buffer = new byte[64];
            var stream = client.GetStream();
            while (stream.DataAvailable)
                stream.Read(buffer, 0, buffer.Length);
        }

        private void ManageNewClient(TcpClient client)
        {
            lock (_lock) ConnectedClients.Add(client);

            CleanReadBuffer(client);

            if (ConnectedClients.Count > 1)
                Broadcast($"Client {ConnectedClients.Count - 1} has connected to server \r\n", client);

            Task.Run(() => ClientTask(ConnectedClients.Count - 1));
        }

        private async Task ListeningHandler()
        {
            try
            {
                ServerSocket.Start();
                Console.WriteLine("Server's up!!");

                while (true)
                {
                    var client = await ServerSocket.AcceptTcpClientAsync();
                    ManageNewClient(client);
                }
            }
            catch
            {
                Console.WriteLine("Listening interrupted");
            }
        }

        private async Task ClientTask(int id)
        {
            TcpClient client;
            lock (_lock) client = ConnectedClients[id];

            try
            {
                while (true)
                {
                    var buffer = new byte[1024];
                    var stream = client.GetStream();
                    if (stream.DataAvailable)
                    {
                        var byte_count = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (byte_count != 0)
                        {
                            string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
                            Broadcast(data, client);
                        }
                    }
                }
            }
            catch (IOException)
            {
            }
            catch (SocketException)
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            lock (_lock)
                ConnectedClients.RemoveAt(id);

            try
            {
                client.Client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch { }

            Broadcast($"Client {id} has disconnected! \r\n");
        }

        private void Broadcast(string data, TcpClient sender = null)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            lock (_lock)
                ConnectedClients.ForEach(c =>
                {
                    if (c is not null && c != sender)
                        c.GetStream().Write(buffer, 0, buffer.Length);
                });
        }
        #endregion
    }
}