using System;
using Entity.Logger;
using Events;
using Mirror;
using Steamworks;
using UnityEngine;

namespace Entity.Player
{
    public class PlayerObjectController : NetworkBehaviour
    {
        [SyncVar] public ulong lobbySteamId;
        [SyncVar] public int connectionId;
        [SyncVar] public ulong steamId;

        [SyncVar(hook = nameof(PlayerReadyUpdate))] public bool isReady;

        public void Initialize(ulong lobbySteamId, int connectionId)
        {
            this.lobbySteamId = lobbySteamId;
            this.connectionId = connectionId;
            this.steamId = SteamUser.GetSteamID().m_SteamID;
            
            this.LogWarning( "steamId" + steamId);
        }

        private void PlayerReadyUpdate(bool oldValue, bool newValue)
        {
            if (isServer)
                isReady = newValue;
            
            NetworkEvents.OnPlayerReadyStatusChangedLobby?.Invoke(newValue, connectionId);
        }

        [Command]
        private void CmdChangePlayerReady()
        {
            PlayerReadyUpdate(isReady, !isReady);
        }

        public void ChangePlayerReady()
        {
            if (hasAuthority)
                CmdChangePlayerReady();
        }

        public override void OnStartAuthority()
        {
            gameObject.name = "LocalGamePlayer";
        }

        public override void OnStartClient()
        {
            NetworkEvents.OnPlayerConnectedToLobby?.Invoke(this);
        }

        public override void OnStopClient()
        {
            NetworkEvents.OnPlayerDisconnectedFromLobby?.Invoke(this);
        }
    }
}