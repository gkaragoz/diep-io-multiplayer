using TMPro;
using UnityEngine;

namespace Entity.UI.Lobby
{
    public class LobbyPlayerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _txtPlayerName;
        [SerializeField] private TMP_Text _txtStatus;

        private LobbyPlayer _lobbyPlayer;

        public void Initialize(LobbyPlayer lobbyPlayer)
        {
            _lobbyPlayer = lobbyPlayer;
            
            SetPlayerName(lobbyPlayer.Name);
            SetStatus(lobbyPlayer.IsReady);
        }

        private void SetPlayerName(string playerName)
        {
            _txtPlayerName.text = $"{playerName}";
        }

        private void SetStatus(bool isReady)
        {
            if (isReady)
                _txtStatus.text = "READY";
            else
                _txtStatus.text = "NOT READY";
        }

        public bool IsReady()
        {
            return _lobbyPlayer.IsReady;
        }
    }
}