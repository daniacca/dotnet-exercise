using System;
using Tcp.Server.Interaces;

namespace Tcp.Server.Tests
{
    public abstract class TestBase<TServer> : IDisposable where TServer : IServer
    {
        protected IServer Server { get; }

        public TestBase()
        {
            Server = InitServer();
        }

        protected abstract TServer InitServer();

        public void Dispose()
        {
            Server.Shutdown();
            GC.SuppressFinalize(this);
        }
    }
}
