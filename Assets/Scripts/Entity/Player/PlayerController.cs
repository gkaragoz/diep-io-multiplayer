using Entity.Input.Controller;
using Entity.Player.Tank;
using Entity.Player.Tank.Projectile;
using Entity.UI.Player;
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

            _tank.OnAttackCallback += CmdFire;
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
            
            if (_inputController.HasAttackButtonPressed())
                _tank.Attack();
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
        
        // this is called on the server
        [Command]
        private void CmdFire()
        {
            var prefab = _tank.GetProjectilePrefab();
            var gunEndPoint = _tank.GetGunEndPoint();
            
            var newProjectile = Instantiate(prefab, gunEndPoint.position, gunEndPoint.rotation);
            NetworkServer.Spawn(newProjectile);
            // RpcOnFireAnimation();
        }
        
        // this is called on the tank that fired for all observers
        [ClientRpc]
        private void RpcOnFireAnimation()
        {
            // animator.SetTrigger("Shoot");
        }
        
        [ServerCallback]
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Projectile>() != null)
            {
                // --health;
                // if (health == 0)
                //     NetworkServer.Destroy(gameObject);
            }
        }
    }
}