using Entity.Controllers;
using Entity.Logger;
using Steamworks;

namespace Controllers
{
    public class UpdateLobbyCommand : Command
    {
        protected Callback<LobbyDataUpdate_t> LobbyDataUpdated;
        
        public UpdateLobbyCommand()
        {
            if (!SteamManager.Initialized)
                return;
            
            LobbyDataUpdated = Callback<LobbyDataUpdate_t>.Create(OnLobbyDataUpdated);
        }

        private void OnLobbyDataUpdated(LobbyDataUpdate_t callback)
        {
            this.LogWarning($"OnLobbyDataUpdated SteamLobbyId: {callback.m_ulSteamIDLobby} SteamUserId: {callback.m_ulSteamIDMember}");
            this.LogError("member count: " + SteamMatchmaking.GetNumLobbyMembers(new CSteamID(callback.m_ulSteamIDLobby)));
        }
    }
}