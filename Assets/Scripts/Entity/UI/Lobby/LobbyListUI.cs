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

        private string _id;

        public void Initialize(LobbyListItem item)
        {
            _id = item.Id;
            
            SetId(item.Id);
            SetLobbyName(item.LobbyName);
            SetOwnerName(item.OwnerName);
            SetPlayersCount(item.CurrentPlayersCount, item.MaxPlayersCount);
            SetIsPrivate(item.IsPrivate);
            SetIsPlaying(item.IsPlaying, item.IsFull);
            SetJoinButton(item.IsPlaying, item.IsFull);
            
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

        private void SetIsPlaying(bool isPlaying, bool isRoomFull)
        {
            if (isPlaying)
                _txtStatus.text = "PLAYING";
            else if (isRoomFull)
                _txtStatus.text = "FULL";
            else
                _txtStatus.text = "AVAILABLE";
        }

        private void SetJoinButton(bool isPlaying, bool isRoomFull)
        {
            _btnJoin.interactable = !isPlaying || !isRoomFull;
        }

        private void OnClick_Join()
        {
            NetworkEvents.JoinLobby?.Invoke(_id);
        }
    }
}