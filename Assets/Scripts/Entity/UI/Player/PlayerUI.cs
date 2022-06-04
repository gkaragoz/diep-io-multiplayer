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

        private bool _avatarReceived = false;

        private Callback<AvatarImageLoaded_t> _imageLoadedCallback;

        public void Initialize()
        {
            _imageLoadedCallback = Callback<AvatarImageLoaded_t>.Create(OnImageLoaded);
            
            SetPlayerName();
            
            if (_avatarReceived == false)
                PlayerIconRequest();
        }

        private void SetPlayerName()
        {
            _txtPlayerName.text = $"{SteamFriends.GetPersonaName()}";
        }
        
        private void PlayerIconRequest()
        {
            int imageId = SteamFriends.GetLargeFriendAvatar(SteamUser.GetSteamID());
            if (imageId == -1)
                return;

            _imgIcon.texture = SteamHelpers.GetSteamImageAsTexture(imageId);
        }
        
        private void OnImageLoaded(AvatarImageLoaded_t callback)
        {
            if (callback.m_steamID == SteamUser.GetSteamID())
                _imgIcon.texture = SteamHelpers.GetSteamImageAsTexture(callback.m_iImage);
            else //another player
                return;

            _avatarReceived = true;
        }
    }
}