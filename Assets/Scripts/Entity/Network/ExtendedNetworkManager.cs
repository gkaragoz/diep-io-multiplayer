using Entity.Network.Operations;
using Events;
using Mirror;

namespace Entity.Network
{
    public class ExtendedNetworkManager : NetworkManager
    {
        private CreateLobbyOperation _createLobbyOperation;
        private JoinLobbyOperation _joinLobbyOperation;
        private ListLobbiesOperation _listLobbiesOperation;
        private LeaveLobbyOperation _leaveLobbyOperation;

        private PlayerConnectedToLobbyOperation _playerConnectedToLobbyOperation;
        
        private void OnEnable()
        {
            _createLobbyOperation = new();
            _joinLobbyOperation = new();
            _listLobbiesOperation = new();
            _leaveLobbyOperation = new();

            _playerConnectedToLobbyOperation = new();
            
            NetworkEvents.StartHost += StartHost;
            NetworkEvents.StartClient += StartClient;
            NetworkEvents.StartServer += StartServer;

            NetworkEvents.StopHost += StopServer;
            NetworkEvents.StopClient += StopClient;
            NetworkEvents.StopServer += StopServer;
            
            NetworkEvents.ChangeNetworkAddress += OnChangeNetworkAddressListener;

            //Operations
            NetworkEvents.CreateLobbyOperation += _createLobbyOperation.CreateLobbyListener;
            NetworkEvents.JoinLobbyOperation += _joinLobbyOperation.JoinLobbyListener;
            NetworkEvents.LeaveLobbyOperation += _leaveLobbyOperation.LeaveLobbyListener;
            NetworkEvents.ListLobbiesOperation += _listLobbiesOperation.ListLobbiesListener;

            NetworkEvents.OnServerAddPlayer += _playerConnectedToLobbyOperation.OnServerAddPlayer;
        }

        private void OnDisable()
        {
            NetworkEvents.StartHost -= StartHost;
            NetworkEvents.StartClient -= StartClient;
            NetworkEvents.StartServer -= StartServer;

            NetworkEvents.StopHost -= StopServer;
            NetworkEvents.StopClient -= StopClient;
            NetworkEvents.StopServer -= StopServer;
            
            NetworkEvents.ChangeNetworkAddress -= OnChangeNetworkAddressListener;
            
            //Operations
            NetworkEvents.CreateLobbyOperation -= _createLobbyOperation.CreateLobbyListener;
            NetworkEvents.JoinLobbyOperation -= _joinLobbyOperation.JoinLobbyListener;
            NetworkEvents.LeaveLobbyOperation -= _leaveLobbyOperation.LeaveLobbyListener;
            NetworkEvents.ListLobbiesOperation -= _listLobbiesOperation.ListLobbiesListener;

            NetworkEvents.OnServerAddPlayer -= _playerConnectedToLobbyOperation.OnServerAddPlayer;
        }
        
        private void OnChangeNetworkAddressListener(string networkAddress)
        {
            this.networkAddress = networkAddress;
        }

        #region Callbacks

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);
            
            NetworkEvents.OnServerAddPlayer?.Invoke(conn);
        }

        #endregion
        
    }
}