using Enums;

namespace Entity.UI.Screen
{
    public interface IScreen
    {
        ScreenType ScreenType { get; }
        void OnShowScreen();
        void OnHideScreen();
    }
}