using Constants;
using Mirror;
using UnityEngine;

namespace Entity.Player
{
    public class PlayerObjectController : NetworkBehaviour
    {
        public ulong lobbySteamId;
        
        [SyncVar] public int connectionId;
        [SyncVar] public int mirrorIdNumber;
        [SyncVar] public ulong steamId;
        [SyncVar(hook = nameof(PlayerNameUpdate))] public string playerName;

        private void Awake()
        {
            lobbySteamId = ulong.Parse(PlayerPrefs.GetString(NetworkConstants.LOBBY_STEAM_ID));
        }

        public void PlayerNameUpdate(string oldValue, string newValue)
        {
            
        }
    }
}