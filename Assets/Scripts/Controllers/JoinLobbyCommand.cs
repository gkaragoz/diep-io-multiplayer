using Assets.Scripts.Constants;
using Assets.Scripts.Entity;
using Assets.Scripts.Entity.Controllers;
using Assets.Scripts.Entity.Logger;
using Assets.Scripts.Entity.Player;
using Assets.Scripts.Events;
using Mirror;
using Steamworks;

namespace Assets.Scripts.Controllers
{
    public class JoinLobbyCommand : Command
    {
        private Callback<GameLobbyJoinRequested_t> _joinRequest;
        private Callback<LobbyEnter_t> _lobbyEntered;

        public JoinLobbyCommand()
        {
            NetworkEvents.JoinLobbyCommand += JoinLobbyListener;

            if (!SteamManager.Initialized)
                return;

            _joinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
            _lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
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