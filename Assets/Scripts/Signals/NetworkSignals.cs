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
    }
}