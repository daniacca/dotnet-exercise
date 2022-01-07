using System.Net;
using Xunit;
using Tcp.Server.Servers;
using Tcp.Server.Tests.Helpers;
using System.Threading;
using System.Threading.Tasks;

namespace Tcp.Server.Tests
{
    public class BroadcastChatServerTest : TestBase<BroadcastChatServer>
    {
        public BroadcastChatServerTest()
        {
            Task.Run(() => Server.StartListening());
        }

        protected override BroadcastChatServer InitServer()
        {
            return new BroadcastChatServer(IPAddress.Any);
        }

        [Fact]
        public void Server_Should_Accept_Connection()
        {
            Assert.Equal(0, Server.ConnectedClient);

            var client = new TestClient(IPAddress.Loopback, 10000);
            Assert.True(client.Connected);
            Thread.Sleep(500);
            Assert.Equal(1, Server.ConnectedClient);

            client.Disconnect();
            Thread.Sleep(500);
        }

        [Fact]
        public async Task Two_Client_Sent_Read()
        {
            // Create clients and connections
            var obiWan = new TestClient(IPAddress.Loopback, 10000);
            Thread.Sleep(500);
            Assert.True(obiWan.Connected);
            Assert.Equal(1, Server.ConnectedClient);
            
            var grievius = new TestClient(IPAddress.Loopback, 10000);
            Thread.Sleep(500);
            Assert.True(grievius.Connected);
            Assert.Equal(2, Server.ConnectedClient);

            var obiWanRead = await obiWan.Read();
            Assert.Equal("Client 1 has connected to server \r\n", obiWanRead);

            await obiWan.Send("Hello There!");
            var grieviusRead = await grievius.Read();
            Assert.Equal("Hello There!", grieviusRead);

            await grievius.Send("General Kenobi!");
            obiWanRead = await obiWan.Read();
            Assert.Equal("General Kenobi!", obiWanRead);

            obiWan.Disconnect();
            Thread.Sleep(500);

            grievius.Disconnect();
            Thread.Sleep(500);
        }

        [Fact]
        public async Task Three_Client_Sent_Read()
        {
            var luke = new TestClient(IPAddress.Loopback, 10000);
            Thread.Sleep(500);
            Assert.True(luke.Connected);
            Assert.Equal(1, Server.ConnectedClient);

            var vader = new TestClient(IPAddress.Loopback, 10000);
            Thread.Sleep(500);
            Assert.True(vader.Connected);
            Assert.Equal(2, Server.ConnectedClient);

            var lukeRead = await luke.Read();
            Assert.Equal("Client 1 has connected to server \r\n", lukeRead);

            var palpatine = new TestClient(IPAddress.Loopback, 10000);
            Thread.Sleep(500);
            Assert.True(palpatine.Connected);
            Assert.Equal(3, Server.ConnectedClient);

            lukeRead = await luke.Read();
            Assert.Equal("Client 2 has connected to server \r\n", lukeRead);

            var vaderRead = await vader.Read();
            Assert.Equal("Client 2 has connected to server \r\n", vaderRead);

            await palpatine.Send("Welcome, young Skywalker, I have been expecting you.");
            lukeRead = await luke.Read();
            vaderRead = await vader.Read();
            Assert.Equal(lukeRead, vaderRead);

            await luke.Send("You're gravely mistaken. You won't convert me as you did my father.");
            vaderRead = await vader.Read();
            var palpatineRead = await palpatine.Read();
            Assert.Equal(vaderRead, palpatineRead);

            await vader.Send("It is pointless to resist, my son.");
            lukeRead = await luke.Read();
            palpatineRead = await palpatine.Read();
            Assert.Equal(lukeRead, palpatineRead);

            luke.Disconnect();
            Thread.Sleep(500);

            vader.Disconnect();
            Thread.Sleep(500);

            palpatine.Disconnect();
            Thread.Sleep(500);
        }
    }
}
