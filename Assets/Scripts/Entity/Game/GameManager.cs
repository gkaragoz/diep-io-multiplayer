using Entity.Player;
using Entity.UI.UIManager;
using Enums;
using UnityEngine;

namespace Entity.Game
{
    public class GameManager : MonoBehaviour
    {
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
            _uiManager.Setup();
        }

        private void StartGame()
        {
            _uiManager.ShowScreen(ScreenType.MainMenu);
        }
    }
}