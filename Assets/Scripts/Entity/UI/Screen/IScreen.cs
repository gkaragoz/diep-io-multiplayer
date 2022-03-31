using Assets.Scripts.Enums;

namespace Assets.Scripts.Entity.UI.Screen
{
    public interface IScreen
    {
        ScreenType ScreenType { get; }
        void OnShowScreen();
        void OnHideScreen();
    }
}