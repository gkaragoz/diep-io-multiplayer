using System.Collections.Generic;
using Controllers;
using Entity.Controllers;
using Entity.UI.UIManager;
using Enums;
using UnityEngine;

namespace Entity.Game
{
    public class GameManager : MonoBehaviour
    {
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
            _uiManager.Setup();

            _commandsList = new List<ICommand>
            {
                new CreateLobbyCommand(),
                new ListLobbiesCommand()
            };
        }

        private void StartGame()
        {
            _uiManager.ShowScreen(ScreenType.MainMenu);
        }
    }
}