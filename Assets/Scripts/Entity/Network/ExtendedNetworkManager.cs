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
            NetworkEvents.StartHost += StartHost;
            NetworkEvents.StartClient += StartClient;
            NetworkEvents.StartServer += StartServer;

            NetworkEvents.StopHost += StopServer;
            NetworkEvents.StopClient += StopClient;
            NetworkEvents.StopServer += StopServer;
            
            NetworkEvents.ChangeNetworkAddress += OnChangeNetworkAddressListener;

            _createLobbyOperation = new();
            _joinLobbyOperation = new();
            _listLobbiesOperation = new();
            _leaveLobbyOperation = new();

            _playerConnectedToLobbyOperation = new();
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

        

        #endregion
        
    }
}