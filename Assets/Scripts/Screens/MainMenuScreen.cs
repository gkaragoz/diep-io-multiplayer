using Entity.Logger;
using Enums;
using Events;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class MainMenuScreen : Entity.UI.Screen.Screen
    {
        public override ScreenType ScreenType => ScreenType.MainMenu;

        #region Serialized References

        [Header("References")]
        [SerializeField] private TMP_Text _txtStatus;

        [SerializeField] private Button _btnStartGame;
        [SerializeField] private Button _btnJoinGame;

        #endregion

        #region Editor Callbacks

        public void OnClick_StartGame()
        {
            UIEvents.HideScreen?.Invoke(ScreenType);

            // this.Log("OnClick_StartGame");
            // this.Log($"CreateLobbyOperation {ELobbyType.k_ELobbyTypePublic} Invoked");
            
            NetworkEvents.CreateLobbyOperation?.Invoke(ELobbyType.k_ELobbyTypePublic);
        }

        public void OnClick_JoinGame()
        {
            // this.Log("OnClick_JoinGame");
            // this.Log("ListLobbiesOperation Invoked");

            UIEvents.HideScreen?.Invoke(ScreenType);

            NetworkEvents.ListLobbiesOperation?.Invoke();
        }

        #endregion

    }
}