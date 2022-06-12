using Data.ValueObject;
using Entity.Network;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Entity.UI.Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _txtPlayerName;
        [SerializeField] private RawImage _imgIcon;
        [SerializeField] private GameObject _holder;

        private bool _avatarReceived = false;

        private Callback<AvatarImageLoaded_t> _imageLoadedCallback;
        private PlayerVO _playerVO;
        
        public void Initialize(PlayerVO vo)
        {
            _playerVO = vo;
            
            _imageLoadedCallback = Callback<AvatarImageLoaded_t>.Create(OnImageLoaded);
            
            SetPlayerName();
            
            if (_avatarReceived == false)
                PlayerIconRequest();
        }

        private void SetPlayerName()
        {
            _txtPlayerName.text = $"{_playerVO.playerName}";
        }
        
        private void PlayerIconRequest()
        {
            int imageId = SteamFriends.GetLargeFriendAvatar(new CSteamID(_playerVO.steamId));
            if (imageId == -1)
                return;

            _imgIcon.texture = SteamHelpers.GetSteamImageAsTexture(imageId);
        }
        
        private void OnImageLoaded(AvatarImageLoaded_t callback)
        {
            if (callback.m_steamID.m_SteamID == _playerVO.steamId)
                _imgIcon.texture = SteamHelpers.GetSteamImageAsTexture(callback.m_iImage);
            else //another player
                return;

            _avatarReceived = true;
        }
    }
}