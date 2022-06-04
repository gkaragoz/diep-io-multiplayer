using Entity.Logger;
using Events;
using Mirror;
using Steamworks;

namespace Entity.Player
{
    public class PlayerObjectController : NetworkBehaviour
    {
        [SyncVar] public ulong lobbySteamId;
        [SyncVar] public int connectionId;
        [SyncVar] public ulong steamId;

        public void Initialize(ulong lobbySteamId, int connectionId)
        {
            this.lobbySteamId = lobbySteamId;
            this.connectionId = connectionId;
            this.steamId = SteamUser.GetSteamID().m_SteamID;
            
            this.LogWarning( "steamId" + steamId);
        }

        public override void OnStartAuthority()
        {
            gameObject.name = "LocalGamePlayer";
        }

        public override void OnStartClient()
        {
            NetworkEvents.OnPlayerConnectedToLobby?.Invoke(isLocalPlayer, connectionId);
        }

        public override void OnStopClient()
        {
            NetworkEvents.OnPlayerDisconnectedFromLobby?.Invoke(isLocalPlayer, connectionId);
        }
    }
}