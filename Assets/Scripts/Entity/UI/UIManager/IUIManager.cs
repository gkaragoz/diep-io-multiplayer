using System.Collections.Generic;
using Entity.UI.Screen;
using Enums;

namespace Entity.UI.UIManager
{
    public interface IUIManager
    {
        void Setup();
        Dictionary<ScreenType, IScreen> ScreensDict { get; }
        
        IScreen ShowScreen(ScreenType screenType);
        IScreen HideScreen(ScreenType screenType);
        IScreen GetScreen(ScreenType screenType);
    }
}