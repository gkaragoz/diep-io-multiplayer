using Signals;
using strange.extensions.mediation.impl;

namespace Views
{
    public class NetworkMediator : Mediator
    {
        [Inject] private NetworkManager _view { get; set; }

        [Inject] private NetworkSignals _networkSignals { get; set; }
        
        public override void OnRegister()
        {
            base.OnRegister();
            
            _networkSignals.StartHost.AddListener(OnStartHost);
            _networkSignals.StartClient.AddListener(OnStartClient);
            _networkSignals.StartServer.AddListener(OnStartServer);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            
            _networkSignals.StartHost.RemoveListener(OnStartHost);
            _networkSignals.StartClient.RemoveListener(OnStartClient);
            _networkSignals.StartServer.RemoveListener(OnStartServer);
        }
        
        private void OnStartHost()
        {
            _view.StartHost();
        }

        private void OnStartClient()
        {
            _view.StartClient();
        }

        private void OnStartServer()
        {
            _view.StartServer();
        }
    }
}