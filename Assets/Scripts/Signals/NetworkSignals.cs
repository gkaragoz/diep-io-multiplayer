using Mirror;
using strange.extensions.signal.impl;

namespace Signals
{
    public class NetworkSignals
    {
        public Signal StartHost = new Signal();
        public Signal StartClient = new Signal();
        public Signal StartServer = new Signal();

        public Signal StopHost = new Signal();
        public Signal StopClient = new Signal();
        public Signal StopServer = new Signal();

        public Signal<NetworkConnection> ClientConnected = new Signal<NetworkConnection>();
        public Signal<NetworkConnection> ClientDisconnected = new Signal<NetworkConnection>();
        
        // public Signal HostStopped = new Signal();
        // public Signal ClientDisconnected = new Signal();
        // public Signal ServerStopped = new Signal();
    }
}