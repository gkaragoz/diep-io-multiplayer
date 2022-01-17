using Constants;
using MVC.Base.Runtime.Concrete.Context;
using MVC.Base.Runtime.Concrete.UI;
using MVC.Base.Runtime.Extensions;
using Signals;
using Views.Screens;

namespace Contexts
{
    public class UIContext : BaseUIContext
    {
        private NetworkSignals _networkSignals;
        
        protected override void mapBindings()
        {
            base.mapBindings();

            _networkSignals = injectionBinder.BindCrossContextSingletonSafely<NetworkSignals>();

            mediationBinder.Bind<LobbyScreenView>().To<LobbyScreenMediator>();
        }

        public override void Launch()
        {
            base.Launch();
            
            _uiModel.ShowScreen(ScreenType.Lobby, UILayerIndex.SafeLayer_1, 0);
        }
    }
}
