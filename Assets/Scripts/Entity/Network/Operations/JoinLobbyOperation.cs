using Constants;
using Entity.Logger;
using Entity.Player;
using Events;
using Mirror;
using Steamworks;

namespace Entity.Network.Operations
{
    public class JoinLobbyOperation
    {
        private Callback<GameLobbyJoinRequested_t> _joinRequest;
        private Callback<LobbyEnter_t> _lobbyEntered;

        public JoinLobbyOperation()
        {
            NetworkEvents.JoinLobbyOperation += JoinLobbyListener;

            if (!SteamManager.Initialized)
                return;

            _joinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
            _lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        }

        ~JoinLobbyOperation()
        {
            NetworkEvents.JoinLobbyOperation -= JoinLobbyListener;
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