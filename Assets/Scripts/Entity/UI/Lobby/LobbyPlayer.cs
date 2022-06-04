using Enums;

namespace Entity.UI.Lobby
{
    public class LobbyPlayer
    {
        public ulong SteamId { get; set; }
        public string Name { get; set; }
        public LobbyPlayerStatus Status { get; set; }
    }
}