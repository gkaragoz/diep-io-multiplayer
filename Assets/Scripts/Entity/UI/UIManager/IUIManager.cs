using System.Collections.Generic;
using Assets.Scripts.Entity.UI.Screen;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Entity.UI.UIManager
{
    public interface IUIManager
    {
        void Setup();
        Dictionary<ScreenType, IScreen> ScreensDict { get; }
        
        IScreen ShowScreen(ScreenType screenType);
        IScreen HideScreen(ScreenType screenType);
    }
}