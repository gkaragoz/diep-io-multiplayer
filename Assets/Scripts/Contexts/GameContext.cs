using MVC.Base.Runtime.Concrete.Context;
using MVC.Base.Runtime.Extensions;
using Signals;
using Views;

namespace Contexts
{
    public class GameContext : MVCContext
    {
        private NetworkSignals _networkSignals;
        
        protected override void mapBindings()
        {
            base.mapBindings();

            _networkSignals = injectionBinder.BindCrossContextSingletonSafely<NetworkSignals>();
        }

        public override void Launch()
        {
            base.Launch();
        }
    }
}
