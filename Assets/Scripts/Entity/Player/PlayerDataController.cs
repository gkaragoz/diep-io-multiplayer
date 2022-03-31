using Assets.Scripts.Data.UnityObject;
using UnityEngine;

namespace Assets.Scripts.Entity.Player
{
    public class PlayerDataController
    {
        #region Singleton

        private static PlayerDataController _instance;

        public static PlayerDataController Instance => _instance;

        #endregion
        
        private RD_PlayerData _data;
        
        public PlayerDataController()
        {
            _instance = this;
            
            _data = Resources.Load<RD_PlayerData>("Data/RD_PlayerData");
        }

        public void SetLobbySteamId(ulong id)
        {
            _data.vo.steamLobbyId = id;
        }

        public ulong GetLobbySteamId()
        {
            return _data.vo.steamLobbyId;
        }
    }
}