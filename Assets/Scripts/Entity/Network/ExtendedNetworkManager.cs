using System.Collections.Generic;
using Constants;
using Data.ValueObject;
using Entity.Network.Operations;
using Entity.Player;
using Enums;
using Events;
using Mirror;
using UnityEngine;

namespace Entity.Network
{
    public class ExtendedNetworkManager : NetworkManager
    {
        private Dictionary<NetworkConnectionToClient, PlayerController> _players = new();

        private CreateLobbyOperation _createLobbyOperation;
        private JoinLobbyOperation _joinLobbyOperation;
        private ListLobbiesOperation _listLobbiesOperation;
        private LeaveLobbyOperation _leaveLobbyOperation;
        
        private void OnEnable()
        {
            NetworkEvents.StartHost += StartHost;
            NetworkEvents.StartClient += StartClient;
            NetworkEvents.StartServer += StartServer;

            NetworkEvents.StopHost += StopServer;
            NetworkEvents.StopClient += StopClient;
            NetworkEvents.StopServer += StopServer;
            
            NetworkEvents.ChangeNetworkAddress += OnChangeNetworkAddressListener;

            _players = new();
            
            _createLobbyOperation = new();
            _joinLobbyOperation = new();
            _listLobbiesOperation = new();
            _leaveLobbyOperation = new();

            //Operations
            NetworkEvents.CreateLobbyOperation += _createLobbyOperation.CreateLobbyListener;
            NetworkEvents.JoinLobbyOperation += _joinLobbyOperation.JoinLobbyListener;
            NetworkEvents.LeaveLobbyOperation += _leaveLobbyOperation.LeaveLobbyListener;
            NetworkEvents.ListLobbiesOperation += _listLobbiesOperation.ListLobbiesListener;
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
            PlayerController player = (startPos != null
                ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
                : Instantiate(playerPrefab)).GetComponent<PlayerController>();

            // instantiating a "Player" prefab gives it the name "Player(clone)"
            // => appending the connectionId is WAY more useful for debugging!
            player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";

            var lobbySteamId = ulong.Parse(PlayerPrefs.GetString(NetworkConstants.LOBBY_STEAM_ID));
            var connectionId = conn.connectionId;
            var team = (_players.Count + 1) % 2 == 0 ? TeamType.TeamB : TeamType.TeamA;
            
            player.vo.lobbySteamId = lobbySteamId;
            player.vo.connectionId = connectionId;
            player.vo.team = team;
            
            NetworkServer.AddPlayerForConnection(conn, player.gameObject);
            _players.Add(conn, player);
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            base.OnServerDisconnect(conn);

            Debug.LogWarning("OnServerDisconnect");
            _players.Remove(conn);
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();
        }

        public override void OnClientDisconnect()
        {
            base.OnClientDisconnect();
            
        }

        #endregion
        
    }
}