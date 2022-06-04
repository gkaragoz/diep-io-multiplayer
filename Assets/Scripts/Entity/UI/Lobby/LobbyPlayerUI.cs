using System;
using Entity.Network;
using Enums;
using Events;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Entity.UI.Lobby
{
    public class LobbyPlayerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _txtPlayerName;
        [SerializeField] private TMP_Text _txtStatus;
        [SerializeField] private RawImage _imgIcon;

        public LobbyPlayerData Data { get; private set; }

        private Callback<AvatarImageLoaded_t> _imageLoadedCallback;

        public void Initialize(LobbyPlayerData lobbyPlayerData)
        {
            Data = lobbyPlayerData;
            _imageLoadedCallback = Callback<AvatarImageLoaded_t>.Create(OnImageLoaded);
            
            SetPlayerName();
            SetReadyStatus(Data.IsReady);
            
            if (Data.AvatarReceived == false)
                PlayerIconRequest();
        }

        private void SetPlayerName()
        {
            _txtPlayerName.text = $"{Data.Name}";
        }
        private void PlayerIconRequest()
        {
            int imageId = SteamFriends.GetLargeFriendAvatar((CSteamID) Data.PlayerObjectController.steamId);
            if (imageId == -1)
                return;

            _imgIcon.texture = SteamHelpers.GetSteamImageAsTexture(imageId);
        }
        
        private void OnImageLoaded(AvatarImageLoaded_t callback)
        {
            if (callback.m_steamID.m_SteamID == Data.PlayerObjectController.steamId)
                _imgIcon.texture = SteamHelpers.GetSteamImageAsTexture(callback.m_iImage);
            else //another player
                return;

            Data.AvatarReceived = true;
        }

        public void SetReadyStatus(bool isReady)
        {
            Data.IsReady = isReady;
            
            if (Data.IsReady)
                _txtStatus.text = "READY";
            else
                _txtStatus.text = "NOT READY";
        }
    }
}