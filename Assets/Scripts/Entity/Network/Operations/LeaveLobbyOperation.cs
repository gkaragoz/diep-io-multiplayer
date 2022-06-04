using Entity.Logger;
using Enums;
using Events;
using Steamworks;

namespace Entity.Network.Operations
{
    public class LeaveLobbyOperation
    {
        public void LeaveLobbyListener(ulong lobbySteamId)
        {
            if (lobbySteamId == 0)
                return;
         
            this.Log("LeaveLobby");
            
            SteamMatchmaking.LeaveLobby(new CSteamID(lobbySteamId));
            NetworkEvents.StopClient?.Invoke();
            
            UIEvents.ShowScreen?.Invoke(ScreenType.MainMenu);
        }
    }
}