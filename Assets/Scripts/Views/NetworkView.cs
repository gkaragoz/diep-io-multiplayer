using Entity;
using MVC.Base.Runtime.Abstract.View;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(ExtendedNetworkManager))]
    public class NetworkView : MVCView
    {
        [SerializeField] private ExtendedNetworkManager _extendedNetworkManager;

        public ExtendedNetworkManager _
        {
            get => _extendedNetworkManager;
        }
    }
}