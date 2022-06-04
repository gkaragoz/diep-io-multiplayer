using System;
using Entity.Logger;
using Entity.Player.Tank;
using Entity.UI.Lobby;
using Input;
using Input.Controller;
using Mirror;
using Steamworks;
using UnityEngine;

namespace Entity.Player
{
    public class PlayerController : NetworkBehaviour
    {
        [SyncVar] public ulong lobbySteamId;
        [SyncVar] public int connectionId;
        [SyncVar] public ulong steamId;

        [SerializeField] private PlayerUI _playerUI;

        private Tank.Tank _tank;
        private Camera _camera;
        private InputController _inputController;

        private void Awake()
        {
            steamId = SteamUser.GetSteamID().m_SteamID;
            
            _camera = Camera.main;
            _tank = GetComponentInChildren<StandardTank>();
            _inputController = GetComponent<KeyboardMouseController>();

            _playerUI.Initialize();
        }

        private void Update()
        {
            if (!isLocalPlayer)
                return;
            
            if (_inputController.HasMovementInput())
                _tank.MoveTo(_inputController.GetMovementInput());
            else
                _tank.StopMovement();

            if (_inputController.HasRotationInput())
                _tank.RotateTo(_inputController.GetRotationInput(), _camera);
        }

        public override void OnStartAuthority()
        {
            gameObject.name = "LocalGamePlayer";
        }

        public override void OnStartClient()
        {
        }

        public override void OnStopClient()
        {
        }
    }
}