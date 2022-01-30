using System;
using System.Collections.Generic;
using Constants;
using Entity.Controllers;
using Entity.Logger;
using Entity.Player;
using Entity.UI.Lobby;
using Enums;
using Events;
using Mirror;
using Screens;
using Steamworks;

namespace Controllers
{
    public class JoinLobbyCommand : Command
    {
        protected Callback<GameLobbyJoinRequested_t> JoinRequest;
        protected Callback<LobbyEnter_t> LobbyEntered;

        public JoinLobbyCommand()
        {
            NetworkEvents.JoinLobbyCommand += JoinLobbyListener;

            if (!SteamManager.Initialized)
                return;

            JoinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
            LobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        }

        private void JoinLobbyListener(ulong lobbySteamId)
        {
            this.Log("JoinLobby");
            SteamMatchmaking.JoinLobby(new CSteamID(lobbySteamId));
        }

        private void OnJoinRequest(GameLobbyJoinRequested_t callback)
        {
            this.LogWarning("OnJoinRequest");

            SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
        }

        private void OnLobbyEntered(LobbyEnter_t callback)
        {
            this.LogWarning("OnLobbyEntered");
            
            PlayerDataController.Instance.SetLobbySteamId(callback.m_ulSteamIDLobby);
            
            // Everyone
            // Do nothing.
            
            // Clients
            if (NetworkServer.active)
                return;

            NetworkEvents.ChangeNetworkAddress?.Invoke(SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), NetworkConstants.HOST_ADDRESS_KEY));
            NetworkEvents.StartClient?.Invoke();
        }
    }
}