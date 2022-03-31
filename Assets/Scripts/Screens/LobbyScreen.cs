﻿using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Entity.UI.Lobby;
using Assets.Scripts.Enums;
using Assets.Scripts.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Screens
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
        
        private List<LobbyPlayerUI> _lobbyPlayerList;

        public void Initialize(bool isOwner, List<LobbyPlayer> lobbyPlayerList)
        {
            ClearList();
            _lobbyPlayerList = new List<LobbyPlayerUI>();
            
            foreach (var lobbyPlayer in lobbyPlayerList)
                CreateLobbyPlayer(lobbyPlayer);
            
            SetButtons(isOwner);
        }

        private void SetButtons(bool isOwner)
        {
            _btnReady.gameObject.SetActive(!isOwner);
            _btnStartGame.gameObject.SetActive(isOwner);

            var isReadyToPlay = _lobbyPlayerList.Count > 1 && _lobbyPlayerList.All(x => x.IsReady());
            _btnStartGame.interactable = isReadyToPlay;
        }

        private void CreateLobbyPlayer(LobbyPlayer lobbyPlayer)
        {
            var lobbyListItemUI = Instantiate(_lobbyPlayerUIPrefab, _contentParent);
            lobbyListItemUI.Initialize(lobbyPlayer);
            
            _lobbyPlayerList.Add(lobbyListItemUI);
        }

        private void ClearList()
        {
            if (_lobbyPlayerList == null)
                return;;
            
            foreach (var lobbyPlayerUI in _lobbyPlayerList)
                Destroy(lobbyPlayerUI.gameObject);
        }

        #region Editor Callbacks

        public void OnClick_BackToMainMenu()
        {
            UIEvents.HideScreen?.Invoke(ScreenType);
            
            NetworkEvents.LeaveLobbyCommand?.Invoke();
        }
        
        #endregion
        
    }
}