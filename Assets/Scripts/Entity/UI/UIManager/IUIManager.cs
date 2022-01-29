using System.Collections.Generic;
using Entity.UI.Screen;
using Enums;

namespace Entity.UI.UIManager
{
    public interface IUIManager
    {
        void Setup();
        Dictionary<ScreenType, IScreen> ScreensDict { get; }
        
        void ShowScreen(ScreenType screenType);
        void HideScreen(ScreenType screenType);
    }
}