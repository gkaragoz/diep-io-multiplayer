using MVC.Base.Runtime.Concrete.Context;
using Views;

namespace Contexts
{
    public class GameContext : MVCContext
    {
        protected override void mapBindings()
        {
            base.mapBindings();

            mediationBinder.Bind<NetworkMediator>().To<NetworkView>();
        }

        public override void Launch()
        {
            base.Launch();
        }
    }
}
