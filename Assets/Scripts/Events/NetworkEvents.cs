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
        
        public static Action<ELobbyType> CreateLobbyOperation { get; set; }
        public static Action ListLobbiesOperation { get; set; }
        public static Action<ulong> JoinLobbyOperation { get; set; }
        public static Action<ulong> LeaveLobbyOperation { get; set; }

        #endregion

        #region Callbacks
        public static Action<bool, int> OnPlayerConnectedToLobby { get; set; }
        public static Action<bool, int> OnPlayerDisconnectedFromLobby { get; set; }
        #endregion
    }
}