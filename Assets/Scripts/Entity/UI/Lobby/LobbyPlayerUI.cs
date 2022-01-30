using System;
using Enums;
using Events;
using Mirror;
using TMPro;
using UnityEngine;

namespace Entity.UI.Lobby
{
    public class LobbyPlayerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _txtPlayerName;
        [SerializeField] private TMP_Text _txtStatus;

        private LobbyPlayer _lobbyPlayer;

        public void Initialize(LobbyPlayer lobbyPlayer)
        {
            _lobbyPlayer = lobbyPlayer;
            
            SetPlayerName(lobbyPlayer.Name);
            SetStatus(lobbyPlayer.Status);
        }

        private void OnEnable()
        {
            NetworkEvents.OnServer_ClientConnectedToServer += OnClientConnectedToServerListener;
            NetworkEvents.OnServer_ClientDisconnectedFromServer += OnClientDisconnectedFromServerListener;
        }
        
        private void OnDisable()
        {
            NetworkEvents.OnServer_ClientConnectedToServer -= OnClientConnectedToServerListener;
            NetworkEvents.OnServer_ClientDisconnectedFromServer -= OnClientDisconnectedFromServerListener;
        }
        
        private void OnClientConnectedToServerListener(NetworkConnection connection)
        {
            Debug.LogWarning("ClientConnected to server test.");
        }

        private void OnClientDisconnectedFromServerListener(NetworkConnection connection)
        {
            Debug.LogWarning("ClientDisconnected from server test.");
        }

        private void SetPlayerName(string playerName)
        {
            _txtPlayerName.text = $"{playerName}";
        }

        private void SetStatus(LobbyPlayerStatus status)
        {
            if (status == LobbyPlayerStatus.READY)
                _txtStatus.text = "READY";
            else
                _txtStatus.text = "NOT READY";
        }

        public bool IsReady()
        {
            return _lobbyPlayer.Status == LobbyPlayerStatus.READY;
        }
    }
}