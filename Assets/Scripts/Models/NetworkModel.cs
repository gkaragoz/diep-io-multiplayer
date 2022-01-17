using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using NetworkPlayer = Data.ValueObject.NetworkPlayer;

namespace Models
{
    public class NetworkModel : INetworkModel
    {
        public NetworkPlayer SelfNetworkPlayer { get; private set; }
        public List<NetworkPlayer> ConnectedNetworkPlayerList { get; private set; }
        
        [PostConstruct]
        private void PostConstruct()
        {
            ConnectedNetworkPlayerList = new List<NetworkPlayer>();
        }

        public void SetSelfNetworkPlayer(NetworkConnection connection)
        {
            SelfNetworkPlayer = new NetworkPlayer
            {
                connection = connection,
            };
        }

        public void AddNetworkPlayer(NetworkConnection connection)
        {
            var newNetworkPlayer = new NetworkPlayer
            {
                connection = connection
            };
            
            ConnectedNetworkPlayerList.Add(newNetworkPlayer);
        }

        public void RemoveNetworkPlayer(NetworkConnection connection)
        {
            var removedPlayer = ConnectedNetworkPlayerList.FirstOrDefault(x => x.connection == connection);
            if (removedPlayer == null)
            {
                Debug.LogWarning($"NetworkPlayer not found {connection.connectionId}");
                return;
            }

            ConnectedNetworkPlayerList.Remove(removedPlayer);
        }
    }
}