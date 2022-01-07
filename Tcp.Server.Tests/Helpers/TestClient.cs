using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tcp.Server.Tests.Helpers
{
    public class TestClient
    {
        private TcpClient Client { get; }

        public TestClient(IPAddress address, int port)
        {
            Client = new TcpClient(address.ToString(), port);
        }

        public bool Connected => Client?.Connected ?? false;

        public async Task Send(string message)
        {
            var stream = Client.GetStream();
            var data = Encoding.ASCII.GetBytes(message);
            await stream.WriteAsync(data, 0, data.Length);
        }

        public async Task<string> Read(int numberOfByteToRead = 1024)
        {
            var buffer = new byte[numberOfByteToRead];
            var stream = Client.GetStream();
            var byte_count = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (byte_count == 0) return "";
            return Encoding.ASCII.GetString(buffer, 0, byte_count);
        }

        public void Disconnect()
        {
            Client.Close();
        }
    }
}