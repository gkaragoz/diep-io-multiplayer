using MVC.Base.Runtime.Concrete.Root;

public class GameRoot : MVCContextRoot<GameContext>
{
    protected override void BeforeCreateContext()
    {
        base.BeforeCreateContext();
        
        DontDestroyOnLoad(this);
    }
}
