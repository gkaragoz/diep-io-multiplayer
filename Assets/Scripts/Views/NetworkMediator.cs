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
            _view._.OnServerAddedPlayer += OnServerAddedPlayerListener;
            
            _networkSignals.StartHost.AddListener(OnStartHostListener);
            _networkSignals.StartClient.AddListener(OnStartClientListener);
            _networkSignals.StartServer.AddListener(OnStartServerListener);
            
            _networkSignals.StopHost.AddListener(OnStopHostListener);
            _networkSignals.StopClient.AddListener(OnStopClientListener);
            _networkSignals.StopServer.AddListener(OnStopServerListener);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            
            _view._.OnServerConnected -= OnServerConnectedListener;
            _view._.OnServerDisconnected -= OnServerDisconnectedListener;
            _view._.OnServerAddedPlayer -= OnServerAddedPlayerListener;
            
            _networkSignals.StartHost.RemoveListener(OnStartHostListener);
            _networkSignals.StartClient.RemoveListener(OnStartClientListener);
            _networkSignals.StartServer.RemoveListener(OnStartServerListener);
            
            _networkSignals.StopHost.RemoveListener(OnStopHostListener);
            _networkSignals.StopClient.RemoveListener(OnStopClientListener);
            _networkSignals.StopServer.RemoveListener(OnStopServerListener);
        }

        private void OnServerConnectedListener(NetworkConnection connection)
        {
            _networkSignals.ClientConnected.Dispatch(connection);
        }

        private void OnServerDisconnectedListener(NetworkConnection connection)
        {
            _networkSignals.ClientDisconnected.Dispatch(connection);
        }
        
        private void OnServerAddedPlayerListener(NetworkConnection connection)
        {
            Debug.LogWarning("ServerAddedPlayerListener: " + connection);            
        }

        private void OnStartHostListener()
        {
            _view._.StartHost();
        }

        private void OnStartClientListener()
        {
            _view._.StartClient();
        }

        private void OnStartServerListener()
        {
            _view._.StartServer();
        }

        private void OnStopHostListener()
        {
            _view._.StopHost();
        }

        private void OnStopClientListener()
        {
            _view._.StopClient();
        }

        private void OnStopServerListener()
        {
            _view._.StopServer();
        }
    }
}