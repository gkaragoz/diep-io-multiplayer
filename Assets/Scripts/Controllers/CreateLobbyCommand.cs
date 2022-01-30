using System.Collections.Generic;
using Constants;
using Entity.Controllers;
using Entity.Logger;
using Entity.UI.Lobby;
using Enums;
using Events;
using Mirror;
using Screens;
using Steamworks;

namespace Controllers
{
    public class CreateLobbyCommand : Command
    {
        protected Callback<LobbyCreated_t> LobbyCreated;
        protected Callback<GameLobbyJoinRequested_t> JoinRequest;
        protected Callback<LobbyEnter_t> LobbyEntered;

        public ulong CurrentLobbyId { get; protected set; }

        public CreateLobbyCommand()
        {
            NetworkEvents.CreateLobbyCommand += CreateLobbyListener;

            if (!SteamManager.Initialized)
                return;
            
            LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
            JoinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
            LobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        }

        private void OnLobbyCreated(LobbyCreated_t callback)
        {
            this.LogWarning($"OnLobbyCreated {callback.m_eResult}");
            
            if (callback.m_eResult != EResult.k_EResultOK)
                return;
            
            NetworkEvents.StartHost?.Invoke();
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), NetworkConstants.HOST_ADDRESS_KEY, SteamUser.GetSteamID().ToString());
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), NetworkConstants.LOBBY_NAME_KEY,$"{SteamFriends.GetPersonaName()} 's LOBBY" );
        }
        
        private void OnJoinRequest(GameLobbyJoinRequested_t callback)
        {
            this.LogWarning("OnJoinRequest");
            
            SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
        }

        private void OnLobbyEntered(LobbyEnter_t callback)
        {
            this.LogWarning("OnLobbyEntered");

            // Everyone
            var lobbyScreen = UIEvents.ShowScreen?.Invoke(ScreenType.Lobby);

            CurrentLobbyId = callback.m_ulSteamIDLobby;
            
            if (lobbyScreen is LobbyScreen screen)
                screen.Initialize(true, new List<LobbyPlayer>
                {
                    new LobbyPlayer()
                    {
                        SteamId = SteamUser.GetSteamID().m_SteamID,
                        Name = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), NetworkConstants.LOBBY_NAME_KEY),
                        IsReady = true
                    }
                });
            
            // Clients
            if (NetworkServer.active)
                return;

            NetworkEvents.ChangeNetworkAddress?.Invoke(SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), NetworkConstants.HOST_ADDRESS_KEY));
            NetworkEvents.StartClient?.Invoke();
        }

        private void CreateLobbyListener()
        {
            this.Log("Trying to create lobby.");

            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, NetworkManager.singleton.maxConnections);
        }
    }
}