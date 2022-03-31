using Assets.Scripts.Events;
using Mirror;

namespace Assets.Scripts.Entity
{
    public class ExtendedNetworkManager : NetworkManager
    {
        private void OnEnable()
        {
            NetworkEvents.StartHost += StartHost;
            NetworkEvents.StartClient += StartClient;
            NetworkEvents.StartServer += StartServer;

            NetworkEvents.StopHost += StopServer;
            NetworkEvents.StopClient += StopClient;
            NetworkEvents.StopServer += StopServer;
            
            NetworkEvents.ChangeNetworkAddress += OnChangeNetworkAddressListener;
        }

        private void OnDisable()
        {
            NetworkEvents.ChangeNetworkAddress -= OnChangeNetworkAddressListener;
        }
        
        private void OnChangeNetworkAddressListener(string networkAddress)
        {
            this.networkAddress = networkAddress;
        }

        #region Callbacks

        

        #endregion
        
    }
}