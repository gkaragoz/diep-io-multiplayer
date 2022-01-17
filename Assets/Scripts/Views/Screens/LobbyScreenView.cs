using System;
using Mirror;
using MVC.Base.Runtime.Abstract.View;
using UnityEngine;
using UnityEngine.UI;
using NetworkPlayer = Data.ValueObject.NetworkPlayer;

namespace Views.Screens
{
    public class LobbyScreenView : MVCScreenView
    {
        public Action OnStartHostClicked { get; set; }
        public Action OnStartClientClicked { get; set; }
        public Action OnStartServerClicked { get; set; }

        public Action OnStopHostClicked { get; set; }
        public Action OnStopClientClicked { get; set; }
        public Action OnStopServerClicked { get; set; }

        [Header("Parents")]
        [SerializeField] private GameObject _onlineButtonsParent;
        [SerializeField] private GameObject _offlineButtonsParent;

        [Header("Online Buttons")] 
        [SerializeField] private Button _btnStopHost;
        [SerializeField] private Button _btnStopClient;
        [SerializeField] private Button _btnStopServer;

        public void UpdateUI(bool isConnected, NetworkPlayer networkPlayer)
        {
            _onlineButtonsParent.SetActive(isConnected);
            _offlineButtonsParent.SetActive(!isConnected);
         
            if (networkPlayer != null && networkPlayer.connection.identity != null)
            {
                _btnStopHost.gameObject.SetActive(!networkPlayer.connection.identity.isClient && !networkPlayer.connection.identity.isServerOnly);
                _btnStopClient.gameObject.SetActive(networkPlayer.connection.identity.isClientOnly);
                _btnStopServer.gameObject.SetActive(networkPlayer.connection.identity.isServerOnly);
            }
        }

        public void OnClick_StartHost()
        {
            OnStartHostClicked?.Invoke();
        }
        
        public void OnClick_StartClient()
        {
            OnStartClientClicked?.Invoke();
        }
        
        public void OnClick_StartServer()
        {
            OnStartServerClicked?.Invoke();
        }
        
        public void OnClick_StopHost()
        {
            OnStopHostClicked?.Invoke();
        }
        
        public void OnClick_StopClient()
        {
            OnStopClientClicked?.Invoke();
        }
        
        public void OnClick_StopServer()
        {
            OnStopServerClicked?.Invoke();
        }
    }
}