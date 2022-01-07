namespace Tcp.Server.Interaces
{
    public interface IServer
    {
        /// <summary>
        /// Keep tracks of active connections to the current server
        /// </summary>
        /// <value>the number of connected clients</value>
        int ConnectedClient { get; }

        /// <summary>
        /// Return true if listening task started on the selected port
        /// </summary>
        bool ListeningStarted { get; }

        /// <summary>
        /// Start a background thread that's actively
        /// listening to the specified port and accept
        /// client connection's
        /// </summary>
        void StartListening();

        /// <summary>
        /// Stop Server execution, disconnect all connected
        /// clients and stops all thread execution
        /// </summary>
        void Shutdown();
    }
}