using Entity.Logger;
using Enums;
using Events;
using Screens;

namespace Entity.Network.Operations
{
    public class PlayerDisconnectedFromLobbyOperation
    {
        public void OnPlayerDisconnectedFromLobby(bool isLocalPlayer, int connectionId)
        {
            this.LogWarning("OnPlayerDisconnectedFromLobby");
            
            var lobbyScreen = (LobbyScreen) UIEvents.GetScreen?.Invoke(ScreenType.Lobby);
            lobbyScreen.OnPlayerDisconnectedFromLobby(connectionId);
        }
    }
}