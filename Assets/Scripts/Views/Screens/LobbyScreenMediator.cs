using Mirror;
using Models;
using Signals;
using strange.extensions.mediation.impl;

namespace Views.Screens
{
    public class LobbyScreenMediator : Mediator
    {
        [Inject] private LobbyScreenView _view { get; set; }
        [Inject] private NetworkSignals _networkSignals { get; set; }
        
        [Inject] private INetworkModel _networkModel { get; set; }
        
        public override void OnRegister()
        {
            base.OnRegister();

            _view.UpdateUI(false, _networkModel.SelfNetworkPlayer);
            
            _view.OnStartHostClicked += OnStartHostClickedListener;
            _view.OnStartClientClicked += OnStartClientClickedListener;
            _view.OnStartServerClicked += OnStartServerClickedListener;

            _view.OnStopHostClicked += OnStopHostClickedListener;
            _view.OnStopClientClicked += OnStopClientClickedListener;
            _view.OnStopServerClicked += OnStopServerClickedListener;

            _networkSignals.ClientConnected.AddListener(OnClientConnectedListener);
            _networkSignals.ClientDisconnected.AddListener(OnClientDisconnectedListener);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            
            _view.OnStartHostClicked += OnStartHostClickedListener;
            _view.OnStartClientClicked += OnStartClientClickedListener;
            _view.OnStartServerClicked += OnStartServerClickedListener;

            _view.OnStopHostClicked += OnStopHostClickedListener;
            _view.OnStopClientClicked += OnStopClientClickedListener;
            _view.OnStopServerClicked += OnStopServerClickedListener;
            
            _networkSignals.ClientConnected.RemoveListener(OnClientConnectedListener);
            _networkSignals.ClientDisconnected.RemoveListener(OnClientDisconnectedListener);
        }

        private void OnStartHostClickedListener()
        {
            _networkSignals.StartHost.Dispatch();
        }

        private void OnStartClientClickedListener()
        {
            _networkSignals.StartClient.Dispatch();
        }

        private void OnStartServerClickedListener()
        {
            _networkSignals.StartServer.Dispatch();
        }

        private void OnStopHostClickedListener()
        {
            _networkSignals.StopHost.Dispatch();
        }

        private void OnStopClientClickedListener()
        {
            _networkSignals.StopClient.Dispatch();
        }

        private void OnStopServerClickedListener()
        {
            _networkSignals.StopServer.Dispatch();
        }

        private void OnClientConnectedListener(NetworkConnection connection)
        {
            _networkSignals.ClientConnected.RemoveListener(OnClientConnectedListener);
            _networkSignals.ClientDisconnected.AddListener(OnClientConnectedListener);
            
            _view.UpdateUI(true, _networkModel.SelfNetworkPlayer);   
        }

        private void OnClientDisconnectedListener(NetworkConnection connection)
        {
            _networkSignals.ClientDisconnected.RemoveListener(OnClientDisconnectedListener);
            _networkSignals.ClientConnected.AddListener(OnClientConnectedListener);
            
            _view.UpdateUI(false, _networkModel.SelfNetworkPlayer);   
        }
    }
}