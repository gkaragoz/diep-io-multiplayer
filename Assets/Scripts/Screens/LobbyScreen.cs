using System.Collections.Generic;
using System.Linq;
using Entity.Logger;
using Entity.Player;
using Entity.UI.Lobby;
using Enums;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class LobbyScreen : Entity.UI.Screen.Screen
    {
        public override ScreenType ScreenType => ScreenType.Lobby;
        
        #region Serialized References

        [SerializeField] private LobbyPlayerUI _lobbyPlayerUIPrefab;
        [SerializeField] private RectTransform _contentParent;

        [SerializeField] private Button _btnReady;
        [SerializeField] private Button _btnStartGame;
        
        #endregion

        private PlayerObjectController _localPlayerObjectController;
        private readonly List<LobbyPlayerUI> _lobbyPlayerList = new();
        private ulong _lobbySteamId;

        public void Initialize(ulong lobbySteamId, bool isOwner, List<LobbyPlayerData> lobbyPlayerList)
        {
            _lobbySteamId = lobbySteamId;
            
            foreach (var lobbyPlayer in lobbyPlayerList)
                CreateLobbyPlayer(lobbyPlayer);
            
            SetButtonVisibilities(isOwner);
            UpdateReadyButton();
        }

        private void SetButtonVisibilities(bool isOwner)
        {
            _btnReady.gameObject.SetActive(!isOwner);
            _btnStartGame.gameObject.SetActive(isOwner);
        }

        private void UpdateReadyButton()
        {
            var isReadyToPlay = _lobbyPlayerList.Count > 1 && _lobbyPlayerList.All(x => x.Data.IsReady);
            _btnStartGame.interactable = isReadyToPlay;
        }

        private void CreateLobbyPlayer(LobbyPlayerData lobbyPlayerData)
        {
            var lobbyPlayerUI = Instantiate(_lobbyPlayerUIPrefab, _contentParent);
            lobbyPlayerUI.Initialize(lobbyPlayerData);

            if (lobbyPlayerData.PlayerObjectController.isLocalPlayer)
            {
                _localPlayerObjectController = lobbyPlayerData.PlayerObjectController;
            }
            
            _lobbyPlayerList.Add(lobbyPlayerUI);
        }

        private void RemoveLobbyPlayer(LobbyPlayerUI lobbyPlayerUI)
        {
            _lobbyPlayerList.Remove(lobbyPlayerUI);
            
            Destroy(lobbyPlayerUI.gameObject);
        }

        public void OnPlayerChangeReadyStatus(bool newStatus, int connectionId)
        {
            var disconnectedLobbyPlayerUI = _lobbyPlayerList.FirstOrDefault(x => x.Data.PlayerObjectController.connectionId == connectionId);

            if (disconnectedLobbyPlayerUI == null)
            {
                Debug.LogError("Player not found in lobby.");
                return;
            }
            
            disconnectedLobbyPlayerUI.SetReadyStatus(newStatus);
        }

        public void OnPlayerDisconnectedFromLobby(int connectionId)
        {
            var disconnectedLobbyPlayerUI = _lobbyPlayerList.FirstOrDefault(x => x.Data.PlayerObjectController.connectionId == connectionId);

            if (disconnectedLobbyPlayerUI == null)
            {
                Debug.LogError("Disconnected player not found in lobby.");
                return;
            }
            
            RemoveLobbyPlayer(disconnectedLobbyPlayerUI);
        }

        private void ClearList()
        {
            if (_lobbyPlayerList == null)
                return;
            
            foreach (var lobbyPlayerUI in _lobbyPlayerList)
                Destroy(lobbyPlayerUI.gameObject);
            
            _lobbyPlayerList.Clear();
        }

        #region Editor Callbacks

        public void OnClick_ChangeReadyStatus()
        {
            _localPlayerObjectController.ChangePlayerReady();
        }

        public void OnClick_BackToMainMenu()
        {
            ClearList();
            
            // this.Log("OnClick_BackToMainMenu");
            // this.Log("LeaveLobbyOperation Invoked");
            
            UIEvents.HideScreen?.Invoke(ScreenType);
            
            NetworkEvents.LeaveLobbyOperation?.Invoke(_lobbySteamId);
        }
        
        #endregion
    }
}