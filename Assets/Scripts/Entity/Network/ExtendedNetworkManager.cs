using System.Collections.Generic;
using Entity.Network.Operations;
using Entity.Player;
using Events;
using Mirror;
using UnityEngine;

namespace Entity.Network
{
    public class ExtendedNetworkManager : NetworkManager
    {
        public List<PlayerObjectController> PlayerObjectControllers { get; } = new();
        
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

            //Operations
            NetworkEvents.CreateLobbyOperation += _createLobbyOperation.CreateLobbyListener;
            NetworkEvents.JoinLobbyOperation += _joinLobbyOperation.JoinLobbyListener;
            NetworkEvents.LeaveLobbyOperation += _leaveLobbyOperation.LeaveLobbyListener;
            NetworkEvents.ListLobbiesOperation += _listLobbiesOperation.ListLobbiesListener;

            NetworkEvents.OnPlayerConnectedToLobby += _playerConnectedToLobbyOperation.OnPlayerConnectedToLobby;
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

            NetworkEvents.OnPlayerConnectedToLobby -= _playerConnectedToLobbyOperation.OnPlayerConnectedToLobby;
        }
        
        private void OnChangeNetworkAddressListener(string networkAddress)
        {
            Debug.LogWarning("WARNING: Not using steam address.");
            return;
            this.networkAddress = networkAddress;
        }

        #region Callbacks

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            Transform startPos = GetStartPosition();
            GameObject player = startPos != null
                ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
                : Instantiate(playerPrefab);

            // instantiating a "Player" prefab gives it the name "Player(clone)"
            // => appending the connectionId is WAY more useful for debugging!
            player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
            NetworkServer.AddPlayerForConnection(conn, player);

            PlayerObjectControllers.Add(player.GetComponent<PlayerObjectController>());
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();
        }

        #endregion
        
    }
}