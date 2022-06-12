using Data.ValueObject;
using Entity.Collision;
using Entity.Input.Controller;
using Entity.Logger;
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
        public PlayerVO vo;

        [SerializeField] private CollisionTransmitter _collisionTransmitter;
        [SerializeField] private PlayerUI _playerUI;

        private Tank.Tank _tank;
        private Camera _camera;
        private InputController _inputController;

        private void OnEnable()
        {
            _collisionTransmitter.OnTriggerEnter2DCallback += OnTriggerEnter2DListener;
        }

        private void OnDisable()
        {
            _collisionTransmitter.OnTriggerEnter2DCallback -= OnTriggerEnter2DListener;
        }

        private void Start()
        {
            vo.steamId = SteamUser.GetSteamID().m_SteamID;
            vo.playerName = SteamFriends.GetPersonaName();
            
            _camera = Camera.main;
            _tank = GetComponentInChildren<StandardTank>();
            _inputController = GetComponent<KeyboardMouseController>();

            _playerUI.Initialize(vo);

            _tank.Initialize(vo.tank);
            _tank.SetVisualization(vo.team);
            _tank.OnAttackCallback += CmdFire;
        }

        public override void OnStartAuthority()
        {
            gameObject.name = "LocalGamePlayer";
        }

        public override void OnStopClient()
        {
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

        [ClientRpc]
        private void Die()
        {
            _tank.gameObject.SetActive(false);
        }
        
        // this is called on the server
        [Command]
        private void CmdFire()
        {
            var prefab = _tank.GetProjectilePrefab();
            var gunEndPoint = _tank.GetGunEndPoint();
            
            var newProjectile = Instantiate(prefab, gunEndPoint.position, gunEndPoint.rotation);
            var projectile = newProjectile.GetComponent<Projectile>();
            projectile.ownerSteamId = vo.steamId;
            projectile.team = vo.team;
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
        private void OnTriggerEnter2DListener(Collider2D other)
        {
            var projectile = other.GetComponent<Projectile>();
            if (vo.isDead == false && projectile != null && projectile.team != vo.team)
            {
                vo.currentHealth -= 10f;
                if (vo.currentHealth == 0)
                    Die();
                
                NetworkServer.Destroy(other.gameObject);
            }
        }
    }
}