using System.Collections.Generic;
using Data.ValueObject;
using Mirror;

namespace Models
{
    public interface INetworkModel
    {
        NetworkPlayer SelfNetworkPlayer { get; }
        List<NetworkPlayer> ConnectedNetworkPlayerList { get; }
        
        void SetSelfNetworkPlayer(NetworkConnection connection);
        void AddNetworkPlayer(NetworkConnection connection);
        void RemoveNetworkPlayer(NetworkConnection connection);
    }
}