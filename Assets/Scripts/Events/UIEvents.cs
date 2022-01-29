using System;
using Enums;
using Mirror;

namespace Events
{
    public static class UIEvents
    {
        #region Actions

        public static Action<ScreenType> ShowScreen { get; set; }
        public static Action<ScreenType> HideScreen { get; set; }

        #endregion
    }
}