using Entity.Controllers;
using Entity.Logger;
using Entity.Player;
using Enums;
using Events;
using Steamworks;

namespace Controllers
{
    public class LeaveLobbyCommand : Command
    {
        public LeaveLobbyCommand()
        {
            NetworkEvents.LeaveLobbyCommand += LeaveLobbyListener;
        }

        private void LeaveLobbyListener()
        {
            var lobbySteamId = PlayerDataController.Instance.GetLobbySteamId();
            if (lobbySteamId == 0)
                return;
         
            this.Log("LeaveLobby");
            
            SteamMatchmaking.LeaveLobby(new CSteamID(lobbySteamId));
            PlayerDataController.Instance.SetLobbySteamId(0);

            UIEvents.ShowScreen?.Invoke(ScreenType.MainMenu);
        }
    }
}