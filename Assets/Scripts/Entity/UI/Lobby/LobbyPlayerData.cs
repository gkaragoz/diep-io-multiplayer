using Enums;

namespace Entity.UI.Lobby
{
    public class LobbyPlayerData
    {
        public ulong SteamId { get; set; }
        public string Name { get; set; }
        public LobbyPlayerStatus Status { get; set; }
    }
}