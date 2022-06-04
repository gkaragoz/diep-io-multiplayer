using System;
using Entity.Network;
using Enums;
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
            
            SetPlayerName(lobbyPlayerData.Name);
            SetStatus(lobbyPlayerData.Status);
            
            if (Data.AvatarReceived == false)
                PlayerIconRequest();
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

        private void PlayerIconRequest()
        {
            int imageId = SteamFriends.GetLargeFriendAvatar((CSteamID) Data.SteamId);
            if (imageId == -1)
                return;

            _imgIcon.texture = SteamHelpers.GetSteamImageAsTexture(imageId);
        }
        
        private void OnImageLoaded(AvatarImageLoaded_t callback)
        {
            if (callback.m_steamID.m_SteamID == Data.SteamId)
                _imgIcon.texture = SteamHelpers.GetSteamImageAsTexture(callback.m_iImage);
            else //another player
                return;

            Data.AvatarReceived = true;
        }

        public bool IsReady()
        {
            return Data.Status == LobbyPlayerStatus.READY;
        }
    }
}