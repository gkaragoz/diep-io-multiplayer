using Entity.Logger;
using Entity.Player;
using Enums;
using Events;
using Steamworks;

namespace Entity.Network.Operations
{
    public class LeaveLobbyOperation
    {
        public LeaveLobbyOperation()
        {
            NetworkEvents.LeaveLobbyOperation += LeaveLobbyListener;
        }

        ~LeaveLobbyOperation()
        {
            NetworkEvents.LeaveLobbyOperation -= LeaveLobbyListener;
        }

        private void LeaveLobbyListener()
        {
            var lobbySteamId = PlayerDataController.Instance.GetLobbySteamId();
            if (lobbySteamId == 0)
                return;
         
            this.Log("LeaveLobby");
            
            SteamMatchmaking.LeaveLobby(new CSteamID(lobbySteamId));
            NetworkEvents.StopClient?.Invoke();
            
            PlayerDataController.Instance.SetLobbySteamId(0);

            UIEvents.ShowScreen?.Invoke(ScreenType.MainMenu);
        }
    }
}