using strange.extensions.signal.impl;

namespace Signals
{
    public class NetworkSignals
    {
        public Signal StartHost = new Signal();
        public Signal StartClient = new Signal();
        public Signal StartServer = new Signal();
    }
}