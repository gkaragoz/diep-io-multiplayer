using System.Collections.Generic;
using Controllers;
using Entity.Controllers;
using Entity.Player;
using Entity.UI.UIManager;
using Enums;
using UnityEngine;

namespace Entity.Game
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
                new UpdateLobbyCommand()
            };
        }

        private void StartGame()
        {
            _uiManager.ShowScreen(ScreenType.MainMenu);
        }
    }
}