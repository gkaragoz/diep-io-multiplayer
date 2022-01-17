using Entity;
using MVC.Base.Runtime.Abstract.View;
using UnityEngine;

namespace Views
{
    public class NetworkManager : MVCView
    {
        [SerializeField] private ExtendedNetworkManager _extendedNetworkManager;

        public void StartHost()
        {
            _extendedNetworkManager.StartHost();
        }

        public void StartClient()
        {
            _extendedNetworkManager.StartClient();
        }

        public void StartServer()
        {
            _extendedNetworkManager.StartServer();
        }
    }
}