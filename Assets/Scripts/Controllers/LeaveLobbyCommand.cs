using Assets.Scripts.Entity.Controllers;
using Assets.Scripts.Entity.Logger;
using Assets.Scripts.Entity.Player;
using Assets.Scripts.Enums;
using Assets.Scripts.Events;
using Steamworks;

namespace Assets.Scripts.Controllers
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
            NetworkEvents.StopClient?.Invoke();
            
            PlayerDataController.Instance.SetLobbySteamId(0);

            UIEvents.ShowScreen?.Invoke(ScreenType.MainMenu);
        }
    }
}