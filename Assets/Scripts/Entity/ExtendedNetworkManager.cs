using System;
using Mirror;

namespace Entity
{
    public class ExtendedNetworkManager : NetworkManager
    {
        public Action<NetworkConnection> OnServerConnected { get; set; }
        public Action<NetworkConnection> OnServerDisconnected { get; set; }
        
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
            
        }

        public override void OnServerError(NetworkConnection conn, Exception exception)
        {
            base.OnServerError(conn, exception);
        }

        public override void OnServerChangeScene(string newSceneName)
        {
            base.OnServerChangeScene(newSceneName);
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
        }
        
        public override void OnClientDisconnect()
        {
            base.OnClientDisconnect();
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            base.OnClientDisconnect(conn);
        }

        public override void OnClientError(Exception exception)
        {
            base.OnClientError(exception);
        }
        
        public override void OnClientNotReady()
        {
            base.OnClientNotReady();
        }

        public override void OnClientNotReady(NetworkConnection conn)
        {
            base.OnClientNotReady(conn);
        }

        
    }
}