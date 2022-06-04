using Entity.Logger;
using Enums;
using Events;
using Screens;

namespace Entity.Network.Operations
{
    public class PlayerReadyStatusChangedLobbyOperation
    {
        public void OnPlayerReadyStatusChangedLobbyOperation(bool newStatus, int connectionId)
        {
            this.LogWarning("OnPlayerReadyStatusChangedLobbyOperation");
            this.LogWarning($"newStatus {newStatus} : connectionId {connectionId}");
            
            var lobbyScreen = (LobbyScreen) UIEvents.GetScreen?.Invoke(ScreenType.Lobby);
            lobbyScreen.OnPlayerChangeReadyStatus(newStatus, connectionId);
        }
    }
}