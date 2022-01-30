using System;
using Enums;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Entity.UI.Lobby
{
    public class LobbyListUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _txtId;
        [SerializeField] private TMP_Text _txtLobbyName;
        [SerializeField] private TMP_Text _txtOwnerName;
        [SerializeField] private TMP_Text _txtPlayersCount;

        [SerializeField] private Image _imgIsPrivate;
        [SerializeField] private TMP_Text _txtIsPrivate;

        [SerializeField] private TMP_Text _txtStatus;

        [SerializeField] private Button _btnJoin;

        private ulong _id;

        public void Initialize(LobbyListItem item)
        {
            _id = item.LobbySteamId;

            SetId(item.LobbySteamId.ToString());
            SetLobbyName(item.LobbyName);
            SetOwnerName(item.OwnerName);
            SetPlayersCount(item.CurrentPlayersCount, item.MaxPlayersCount);
            SetIsPrivate(item.IsPrivate);
            SetStatus(item.Status);
            SetJoinButton(item.Status);

            _btnJoin.onClick.AddListener(OnClick_Join);
        }

        private void OnDisable()
        {
            _btnJoin.onClick.RemoveAllListeners();
        }

        private void SetId(string id)
        {
            _txtId.text = $"#{id}";
        }

        private void SetLobbyName(string lobbyName)
        {
            _txtLobbyName.text = $"{lobbyName}";
        }

        private void SetOwnerName(string ownerName)
        {
            _txtOwnerName.text = $"{ownerName}";
        }

        private void SetPlayersCount(int currentPlayersCount, int maxPlayersCount)
        {
            _txtPlayersCount.text = $"{currentPlayersCount}/{maxPlayersCount}";
        }

        private void SetIsPrivate(bool isPrivate)
        {
            if (isPrivate)
            {
                _imgIsPrivate.color = Color.red;
                _txtIsPrivate.text = "PRIVATE";
            }
            else
            {
                _imgIsPrivate.color = Color.green;
                _txtIsPrivate.text = "PUBLIC";
            }
        }

        private void SetStatus(LobbyStatus lobbyStatus)
        {
            switch (lobbyStatus)
            {
                case LobbyStatus.AVAILABLE:
                    _txtStatus.text = "AVAILABLE";
                    break;
                case LobbyStatus.PLAYING:
                    _txtStatus.text = "PLAYING";
                    break;
                case LobbyStatus.FULL:
                    _txtStatus.text = "FULL";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lobbyStatus), lobbyStatus, null);
            }
        }

        private void SetJoinButton(LobbyStatus lobbyStatus)
        {
            _btnJoin.interactable = lobbyStatus == LobbyStatus.AVAILABLE;
        }

        private void OnClick_Join()
        {
            NetworkEvents.JoinLobbyCommand?.Invoke(_id);
        }
    }
}