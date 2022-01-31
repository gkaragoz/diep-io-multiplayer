using System;
using Mirror;
using Steamworks;

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
        
        public static Action<ELobbyType> CreateLobbyCommand { get; set; }
        public static Action ListLobbiesCommand { get; set; }
        public static Action<ulong> JoinLobbyCommand { get; set; }
        public static Action LeaveLobbyCommand { get; set; }

        #endregion

        #region Callbacks

        public  static Action<NetworkConnection> OnServerAddPlayer { get; set; }
        
        #endregion
    }
}