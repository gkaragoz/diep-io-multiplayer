using Entity.Logger;
using Events;
using Mirror;

namespace Entity
{
    public class ExtendedNetworkManager : NetworkManager
    {
        private void OnEnable()
        {
            NetworkEvents.StartHost += StartHost;
            NetworkEvents.StartClient += StartClient;
            NetworkEvents.StartServer += StartServer;

            NetworkEvents.StopHost += StopServer;
            NetworkEvents.StopClient += StopClient;
            NetworkEvents.StopServer += StopServer;
            
            NetworkEvents.ChangeNetworkAddress += OnChangeNetworkAddressListener;
        }

        private void OnDisable()
        {
            NetworkEvents.ChangeNetworkAddress -= OnChangeNetworkAddressListener;
        }
        
        private void OnChangeNetworkAddressListener(string networkAddress)
        {
            this.networkAddress = networkAddress;
        }

        #region Callbacks
        
        /// <summary>Called on the server when a new client connects.</summary>
        public override void OnServerConnect(NetworkConnection conn)
        {
            base.OnServerConnect(conn);
            
            this.LogWarning($"OnServer_ClientConnectedToServer, ConnectionId: {conn.connectionId}");
            
            NetworkEvents.OnServer_ClientConnectedToServer?.Invoke(conn);
        }
        
        /// <summary>Called on the server when a client disconnects.</summary>
        public override void OnServerDisconnect(NetworkConnection conn)
        {
            base.OnServerDisconnect(conn);

            this.LogWarning($"OnServer_ClientDisconnectedFromServer, ConnectionId: {conn.connectionId}");

            NetworkEvents.OnServer_ClientDisconnectedFromServer?.Invoke(conn);
        }

        /// <summary>Called on the client when connected to a server. By default it sets client as ready and adds a player.</summary>
        public override void OnClientConnect()
        {
            base.OnClientConnect();
            
            this.LogWarning($"OnClient_ClientConnectedToServer, ConnectionId: {NetworkClient.connection.connectionId}");

            NetworkEvents.OnClient_ClientConnectedToServer?.Invoke(NetworkClient.connection);
        }

        /// <summary>Called on clients when disconnected from a server.</summary>
        public override void OnClientDisconnect()
        {
            base.OnClientDisconnect();
            
            this.LogWarning($"OnClient_ClientDisconnectedFromServer, ConnectionId: {NetworkClient.connection.connectionId}");

            NetworkEvents.OnClient_ClientDisconnectedFromServer?.Invoke(NetworkClient.connection);
        }

        /// <summary>This is invoked when a host is started.</summary>
        public override void OnStartHost()
        {
            this.LogWarning($"OnHostStarted");

            NetworkEvents.OnHostStarted?.Invoke();
        }

        /// <summary>This is invoked when the client is started.</summary>
        public override void OnStartClient()
        {
            this.LogWarning($"OnClientStarted");

            NetworkEvents.OnClientStarted?.Invoke();
        }

        /// <summary>This is invoked when a server is started - including when a host is started.</summary>
        public override void OnStartServer()
        {
            this.LogWarning($"OnServerStarted");

            NetworkEvents.OnServerStarted?.Invoke();
        }

        /// <summary>This is called when a host is stopped.</summary>
        public override void OnStopHost()
        {
            this.LogWarning($"OnHostStopped");

            NetworkEvents.OnHostStopped?.Invoke();
        }

        /// <summary>This is called when a client is stopped.</summary>
        public override void OnStopClient()
        {
            this.LogWarning($"OnClientStopped");

            NetworkEvents.OnClientStopped?.Invoke();   
        }
        
        /// <summary>This is called when a server is stopped - including when a host is stopped.</summary>
        public override void OnStopServer()
        {
            this.LogWarning($"OnServerStopped");
            
            NetworkEvents.OnServerStopped?.Invoke();
        }
        #endregion
        
    }
}