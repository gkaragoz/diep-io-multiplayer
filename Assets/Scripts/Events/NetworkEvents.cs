using System;
using Mirror;

namespace Events
{
    public static class NetworkEvents
    {
        #region Actions

        public static Action<string> ChangeNetworkAddress { get; set; }

        public static Action StartHost { get; set; }
        public static Action StartServer{ get; set; }
        public static Action StartClient { get; set; }

        public static Action StopHost { get; set; }
        public static Action StopServer{ get; set; }
        public static Action StopClient { get; set; }

        #endregion

        #region Callbacks

        public static Action OnHostStarted { get; set; }
        public static Action OnServerStarted{ get; set; }
        public static Action OnClientStarted { get; set; }
        
        public static Action OnHostStopped { get; set; }
        public static Action OnClientStopped { get; set; }
        public static Action OnServerStopped { get; set; }
        
        public static Action<NetworkConnection> OnClientConnectedToServer { get; set; }
        public static Action<NetworkConnection> OnClientDisconnectedFromServer { get; set; }
        
        #endregion
    }
}