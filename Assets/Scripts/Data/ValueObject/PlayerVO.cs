using Enums;
using Mirror;

namespace Data.ValueObject
{
    public class PlayerVO : NetworkBehaviour
    {
        [SyncVar]
        public ulong lobbySteamId;

        [SyncVar]
        public int connectionId;
        [SyncVar]
        public ulong steamId;
        [SyncVar]
        public string playerName;

        [SyncVar]
        public TeamType team;
    }
}