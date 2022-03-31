using System;
using System.Collections.Generic;
using Assets.Scripts.Entity.UI.Screen;
using Assets.Scripts.Enums;
using Assets.Scripts.Events;
using UnityEngine;

namespace Assets.Scripts.Entity.UI.UIManager
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        public Dictionary<ScreenType, IScreen> ScreensDict { get; } = new Dictionary<ScreenType, IScreen>();

        public void Setup()
        {
            SetupScreensDict();
            HideAllScreens();
            SetupEventListeners();
        }

        private void SetupScreensDict()
        {
            var allScreens = FindAllScreenReferences();
            foreach (var screen in allScreens)
                ScreensDict.Add(screen.ScreenType, screen);
        }

        private void HideAllScreens()
        {
            foreach (var screen in ScreensDict)
                HideScreen(screen.Key);
        }

        private void SetupEventListeners()
        {
            UIEvents.ShowScreen += ShowScreen;
            UIEvents.HideScreen += HideScreen;
        }

        private void OnDisable()
        {
            DeleteEventListener();
        }

        private void DeleteEventListener()
        {
            UIEvents.ShowScreen -= ShowScreen;
            UIEvents.HideScreen -= HideScreen;
        }

        private Screen.Screen[] FindAllScreenReferences(bool includeInactive = true)
        {
            return FindObjectsOfType<Screen.Screen>(includeInactive);
        }

        private IScreen GetScreen(ScreenType screenType)
        {
            if (!ScreensDict.ContainsKey(screenType))
                throw new KeyNotFoundException($"{screenType} key not found.");

            return ScreensDict[screenType];
        }

        public IScreen ShowScreen(ScreenType screenType)
        {
            var screen = GetScreen(screenType);
        
            if (screen == null)
                throw new NullReferenceException(message: $"{screenType} value cannot be null.");
            
            screen.OnShowScreen();
        
            return screen;
        }
        
        public IScreen HideScreen(ScreenType screenType)
        {
            var screen = GetScreen(screenType);
        
            if (screen == null)
                throw new NullReferenceException($"{screenType} value cannot be null.");
            
            screen.OnHideScreen();
        
            return screen;
        }
    }
}