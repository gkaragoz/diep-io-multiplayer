using MVC.Base.Runtime.Concrete.Root;

public class UIRoot : MVCContextRoot<UIContext>
{
    protected override void BeforeCreateContext()
    {
        base.BeforeCreateContext();
        
    }
}
