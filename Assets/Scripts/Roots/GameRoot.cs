using Contexts;
using MVC.Base.Runtime.Concrete.Root;

namespace Roots
{
    public class GameRoot : MVCContextRoot<GameContext>
    {
        protected override void BeforeCreateContext()
        {
            base.BeforeCreateContext();
        
            DontDestroyOnLoad(this);
        }
    }
}
