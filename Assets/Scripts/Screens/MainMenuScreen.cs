using Entity.Logger;
using Enums;
using Events;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Screen = Entity.UI.Screen.Screen;

namespace Screens
{
    public class MainMenuScreen : Screen
    {
        public override ScreenType ScreenType => ScreenType.MainMenu;
        
        #region Serialized References

        [Header("References")]
        [SerializeField] private TMP_Text _txtStatus;

        [SerializeField] private Button _btnStartGame;
        [SerializeField] private Button _btnJoinGame;

        #endregion

        private void OnEnable()
        {
            NetworkEvents.OnClientConnectedToServer += OnClientConnectedToServerListener;
            NetworkEvents.OnClientDisconnectedFromServer += OnClientDisconnectedFromServerListener;
        }

        private void OnDisable()
        {
            NetworkEvents.OnClientConnectedToServer -= OnClientConnectedToServerListener;
            NetworkEvents.OnClientDisconnectedFromServer -= OnClientDisconnectedFromServerListener;
        }
        
        private void OnClientConnectedToServerListener(NetworkConnection connection)
        {
        }

        private void OnClientDisconnectedFromServerListener(NetworkConnection connection)
        {
        }

        #region Editor Callbacks

        public void OnClick_StartGame()
        {
            this.Log("Trying to start game.");
            
            NetworkEvents.StartHost?.Invoke();
        }
        
        public void OnClick_JoinGame()
        {
            this.Log("RoomListScreen is going to be opened.");

            UIEvents.HideScreen?.Invoke(ScreenType);
            UIEvents.ShowScreen?.Invoke(ScreenType.RoomList);
        }

        #endregion
        
    }
}