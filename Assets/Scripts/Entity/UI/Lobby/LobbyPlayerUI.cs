using Enums;
using TMPro;
using UnityEngine;

namespace Entity.UI.Lobby
{
    public class LobbyPlayerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _txtPlayerName;
        [SerializeField] private TMP_Text _txtStatus;

        private LobbyPlayerData _lobbyPlayerData;

        public void Initialize(LobbyPlayerData lobbyPlayerData)
        {
            _lobbyPlayerData = lobbyPlayerData;
            
            SetPlayerName(lobbyPlayerData.Name);
            SetStatus(lobbyPlayerData.Status);
        }

        private void SetPlayerName(string playerName)
        {
            _txtPlayerName.text = $"{playerName}";
        }

        private void SetStatus(LobbyPlayerStatus status)
        {
            if (status == LobbyPlayerStatus.READY)
                _txtStatus.text = "READY";
            else
                _txtStatus.text = "NOT READY";
        }

        public bool IsReady()
        {
            return _lobbyPlayerData.Status == LobbyPlayerStatus.READY;
        }
    }
}