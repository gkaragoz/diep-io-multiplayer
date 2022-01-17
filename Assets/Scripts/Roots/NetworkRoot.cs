using Contexts;
using MVC.Base.Runtime.Concrete.Root;

namespace Roots
{
    public class NetworkRoot : MVCContextRoot<NetworkContext>
    {
        protected override void BeforeCreateContext()
        {
            base.BeforeCreateContext();
        
            DontDestroyOnLoad(this);
        }
    }
}
