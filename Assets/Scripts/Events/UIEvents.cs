using System;
using Entity.UI.Screen;
using Enums;

namespace Events
{
    public static class UIEvents
    {
        #region Actions

        public static Func<ScreenType, IScreen> ShowScreen { get; set; }
        public static Func<ScreenType, IScreen> HideScreen { get; set; }
        public static Func<ScreenType, IScreen> GetScreen { get; set; }

        #endregion
    }
}