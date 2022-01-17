using System;
using Mirror;

namespace Entity
{
    public class ExtendedNetworkManager : NetworkManager
    {
        public Action<NetworkConnection> OnServerConnected { get; set; }
        public Action<NetworkConnection> OnServerDisconnected { get; set; }
        public Action<NetworkConnection> OnServerAddedPlayer { get; set; }
        
        public override void OnServerConnect(NetworkConnection conn)
        {
            base.OnServerConnect(conn);
            
            OnServerConnected?.Invoke(conn);
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            base.OnServerDisconnect(conn);
            
            OnServerDisconnected?.Invoke(conn);
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            base.OnServerAddPlayer(conn);
            
            OnServerAddedPlayer?.Invoke(conn);
        }
    }
}