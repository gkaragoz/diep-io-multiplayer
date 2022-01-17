using Contexts;
using MVC.Base.Runtime.Concrete.Root;

namespace Roots
{
    public class UIRoot : MVCContextRoot<UIContext>
    {
        protected override void BeforeCreateContext()
        {
            base.BeforeCreateContext();
        
            DontDestroyOnLoad(this);
        }
    }
}
