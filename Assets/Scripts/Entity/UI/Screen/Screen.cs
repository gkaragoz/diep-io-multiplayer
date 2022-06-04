using Enums;
using UnityEngine;

namespace Entity.UI.Screen
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