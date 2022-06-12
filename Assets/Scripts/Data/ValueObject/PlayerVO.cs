using Data.ValueObject.Tank;
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

        [SyncVar] 
        public float currentHealth = 100;
        [SyncVar] 
        public float maxHealth = 100;

        public bool isDead => currentHealth <= 0;
        
        public TankVO tank;
    }
}