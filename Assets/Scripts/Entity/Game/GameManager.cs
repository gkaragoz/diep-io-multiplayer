using System.Collections.Generic;
using Assets.Scripts.Controllers;
using Assets.Scripts.Entity.Controllers;
using Assets.Scripts.Entity.Player;
using Assets.Scripts.Entity.UI.UIManager;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Entity.Game
{
    public class GameManager : MonoBehaviour
    {
        private PlayerDataController _playerDataController;
        private UIManager _uiManager;
        private List<ICommand> _commandsList;

        private void Start()
        {
            SetReferences();

            SetupAll();

            StartGame();
        }

        private void SetReferences()
        {
            _uiManager = FindObjectOfType<UIManager>();
        }

        private void SetupAll()
        {
            _playerDataController = new PlayerDataController();

            _uiManager.Setup();

            _commandsList = new List<ICommand>
            {
                new CreateLobbyCommand(),
                new ListLobbiesCommand(),
                new LeaveLobbyCommand(),
                new JoinLobbyCommand(),
                new PlayerConnectedToLobbyCommand()
            };
        }

        private void StartGame()
        {
            _uiManager.ShowScreen(ScreenType.MainMenu);
        }
    }
}