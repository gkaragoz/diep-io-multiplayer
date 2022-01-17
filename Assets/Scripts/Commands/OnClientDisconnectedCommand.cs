using Mirror;
using Models;
using MVC.Base.Runtime.Concrete.Controller;
using UnityEngine;

namespace Commands
{
    public class OnServerDisconnectedCommand : MVCCommand
    {
        [Inject] private NetworkConnection _connection { get; set; }
        
        [Inject] private INetworkModel _networkModel { get; set; }
        
        public override void Execute()
        {
            Debug.LogWarning("ClientDisconnectedListener: " + _connection);
        }
    }
}