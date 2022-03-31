using Assets.Scripts.Entity.Logger;
using Assets.Scripts.Enums;
using Assets.Scripts.Events;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Screens
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