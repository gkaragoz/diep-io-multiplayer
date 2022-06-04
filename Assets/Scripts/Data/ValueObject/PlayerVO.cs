using System;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerVO
    {
        public ulong steamLobbyId;

        public int connectionId;
        public int mirrorId;
        public ulong steamId;
        public string playerName;
    }
}