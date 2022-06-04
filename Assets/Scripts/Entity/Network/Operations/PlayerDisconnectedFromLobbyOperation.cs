using Entity.Logger;
using Entity.Player;
using Enums;
using Events;
using Screens;

namespace Entity.Network.Operations
{
    public class PlayerDisconnectedFromLobbyOperation
    {
        public void OnPlayerDisconnectedFromLobby(PlayerObjectController playerObjectController)
        {
            this.LogWarning("OnPlayerDisconnectedFromLobby");
            
            var lobbyScreen = (LobbyScreen) UIEvents.GetScreen?.Invoke(ScreenType.Lobby);
            lobbyScreen.OnPlayerDisconnectedFromLobby(playerObjectController.connectionId);
        }
    }
}