using Mirror;
using Signals;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Views
{
    public class NetworkMediator : Mediator
    {
        [Inject] private NetworkView _view { get; set; }

        [Inject] private NetworkSignals _networkSignals { get; set; }
        
        public override void OnRegister()
        {
            base.OnRegister();

            _view._.OnServerConnected += OnServerConnectedListener;
            _view._.OnServerDisconnected += OnServerDisconnectedListener;
            
            _networkSignals.StartHost.AddListener(OnStartHost);
            _networkSignals.StartClient.AddListener(OnStartClient);
            _networkSignals.StartServer.AddListener(OnStartServer);
            
            _networkSignals.StopHost.AddListener(OnStopHost);
            _networkSignals.StopClient.AddListener(OnStopClient);
            _networkSignals.StopServer.AddListener(OnStopServer);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            
            _view._.OnServerConnected -= OnServerConnectedListener;
            _view._.OnServerDisconnected -= OnServerDisconnectedListener;
            
            _networkSignals.StartHost.RemoveListener(OnStartHost);
            _networkSignals.StartClient.RemoveListener(OnStartClient);
            _networkSignals.StartServer.RemoveListener(OnStartServer);
            
            _networkSignals.StopHost.RemoveListener(OnStopHost);
            _networkSignals.StopClient.RemoveListener(OnStopClient);
            _networkSignals.StopServer.RemoveListener(OnStopServer);
        }

        private void OnServerConnectedListener(NetworkConnection connection)
        {
            Debug.LogWarning("ServerConnectedListener: " + connection);
        }

        private void OnServerDisconnectedListener(NetworkConnection connection)
        {
            Debug.LogWarning("ServerDisconnectedListener: " + connection);
        }

        private void OnStartHost()
        {
            _view._.StartHost();
        }

        private void OnStartClient()
        {
            _view._.StartClient();
        }

        private void OnStartServer()
        {
            _view._.StartServer();
        }

        private void OnStopHost()
        {
            _view._.StopHost();
        }

        private void OnStopClient()
        {
            _view._.StopClient();
        }

        private void OnStopServer()
        {
            _view._.StopServer();
        }
    }
}