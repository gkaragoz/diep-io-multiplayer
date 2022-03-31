using System;
using Assets.Scripts.Entity.UI.Screen;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Events
{
    public static class UIEvents
    {
        #region Actions

        public static Func<ScreenType, IScreen> ShowScreen { get; set; }
        public static Func<ScreenType, IScreen> HideScreen { get; set; }

        #endregion
    }
}