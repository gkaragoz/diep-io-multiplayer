namespace Entity.UI.Lobby
{
    public class LobbyListItem
    {
        public string Id { get; set; }
        public string LobbyName { get; set; }
        public string OwnerName { get; set; }

        public int CurrentPlayersCount { get; set; }
        public int MaxPlayersCount { get; set; }
        
        public bool IsPrivate { get; set; }
        
        public bool IsPlaying { get; set; }

        public bool IsFull => CurrentPlayersCount == MaxPlayersCount;
    }
}