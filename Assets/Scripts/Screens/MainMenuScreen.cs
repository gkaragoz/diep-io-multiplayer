using Entity.Logger;
using Enums;
using Events;
using Steamworks;
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

        #region Editor Callbacks

        public void OnClick_StartGame()
        {
            UIEvents.HideScreen?.Invoke(ScreenType);

            NetworkEvents.CreateLobbyCommand?.Invoke(ELobbyType.k_ELobbyTypePublic);
        }

        public void OnClick_JoinGame()
        {
            this.Log($"{ScreenType.Lobbies} is going to be opened.");

            UIEvents.HideScreen?.Invoke(ScreenType);

            NetworkEvents.ListLobbiesCommand?.Invoke();
        }

        #endregion

    }
}