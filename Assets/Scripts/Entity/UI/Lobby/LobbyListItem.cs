using Assets.Scripts.Enums;

namespace Assets.Scripts.Entity.UI.Lobby
{
    public class LobbyListItem
    {
        public ulong LobbySteamId { get; set; }
        public string LobbyName { get; set; }
        public string OwnerName { get; set; }

        public int CurrentPlayersCount { get; set; }
        public int MaxPlayersCount { get; set; }
        
        public bool IsPrivate { get; set; }
        
        public LobbyStatus Status { get; set; }

        public bool IsFull => CurrentPlayersCount == MaxPlayersCount;
    }
}