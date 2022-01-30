using System.Collections.Generic;
using Entity.UI.Lobby;
using Enums;
using Events;
using UnityEngine;
using Screen = Entity.UI.Screen.Screen;

namespace Screens
{
    public class LobbiesScreen : Screen
    {
        public override ScreenType ScreenType => ScreenType.Lobbies;
        
        #region Serialized References

        [SerializeField] private LobbyListUI lobbyListPrefab;
        [SerializeField] private RectTransform _contentParent;
        
        #endregion

        private List<LobbyListUI> _lobbyList;

        public void Initialize(List<LobbyListItem> lobbyListItems)
        {
            ClearList();
            _lobbyList = new List<LobbyListUI>();
            
            foreach (var lobbyListItem in lobbyListItems)
                CreateLobbyListItem(lobbyListItem);
        }

        private void CreateLobbyListItem(LobbyListItem lobbyListItem)
        {
            var lobbyListItemUI = Instantiate(lobbyListPrefab, _contentParent);
            lobbyListItemUI.Initialize(lobbyListItem);
            
            _lobbyList.Add(lobbyListItemUI);
        }

        private void ClearList()
        {
            if (_lobbyList == null)
                return;
            
            foreach (var lobbyListUI in _lobbyList)
                Destroy(lobbyListUI.gameObject);
        }

        #region Editor Callbacks

        public void OnClick_BackToMainMenu()
        {
            UIEvents.HideScreen?.Invoke(ScreenType);
            UIEvents.ShowScreen?.Invoke(ScreenType.MainMenu);
        }

        public void OnClick_RefreshLobbies()
        {
            NetworkEvents.ListLobbiesCommand?.Invoke();
        }
        
        #endregion
        
    }
}