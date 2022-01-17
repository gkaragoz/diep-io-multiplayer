using Commands;
using Models;
using MVC.Base.Runtime.Concrete.Context;
using MVC.Base.Runtime.Extensions;
using Signals;
using Views;

namespace Contexts
{
    public class NetworkContext : MVCContext
    {
        private NetworkSignals _networkSignals;
        
        protected override void mapBindings()
        {
            base.mapBindings();
            
            BindSignals();

            BindModels();
            
            BindViewMediators();

            BindCommands();
        }

        private void BindSignals()
        {
            _networkSignals = injectionBinder.BindCrossContextSingletonSafely<NetworkSignals>();
        }

        private void BindModels()
        {
            injectionBinder.BindCrossContextSingletonSafely<INetworkModel, NetworkModel>();
        }

        private void BindViewMediators()
        {
            mediationBinder.Bind<NetworkView>().To<NetworkMediator>();
        }

        private void BindCommands()
        {
            commandBinder.Bind(_networkSignals.ClientConnected)
                .To<OnServerStartedCommand>();

            commandBinder.Bind(_networkSignals.ClientDisconnected)
                .To<OnServerDisconnectedCommand>();
        }

        public override void Launch()
        {
            base.Launch();
        }
    }
}
