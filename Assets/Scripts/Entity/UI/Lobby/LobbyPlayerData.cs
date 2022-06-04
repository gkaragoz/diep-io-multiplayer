using Entity.Player;
using Enums;

namespace Entity.UI.Lobby
{
    public class LobbyPlayerData
    {
        public PlayerObjectController PlayerObjectController { get; set; }
        public string Name { get; set; }
        public bool AvatarReceived { get; set; }
        public bool IsReady { get; set; }
    }
}