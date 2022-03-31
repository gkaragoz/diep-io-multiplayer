using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Entity.UI.Screen
{
    public abstract class Screen : MonoBehaviour, IScreen
    {
        public abstract ScreenType ScreenType { get; }
        
        public virtual void OnShowScreen()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnHideScreen()
        {
            gameObject.SetActive(false);
        }
    }
}