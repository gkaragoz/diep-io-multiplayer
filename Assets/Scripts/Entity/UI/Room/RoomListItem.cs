namespace Entity.UI.Room
{
    public class RoomListItem
    {
        public string Id { get; set; }
        public string RoomName { get; set; }
        public string OwnerName { get; set; }

        public int CurrentPlayersCount { get; set; }
        public int MaxPlayersCount { get; set; }
        
        public bool IsPrivate { get; set; }
        
        public bool IsPlaying { get; set; }

        public bool IsFull => CurrentPlayersCount == MaxPlayersCount;
    }
}