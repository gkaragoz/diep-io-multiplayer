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
        }

        private void StartGame()
        {
            _uiManager.ShowScreen(ScreenType.MainMenu);
        }
    }
}