using Enums;
using Mirror;
using UnityEngine;

namespace Entity.Player.Tank.Projectile
{
    public class Projectile : NetworkBehaviour
    {
        public float destroyAfter = 2;
        public Rigidbody2D rigidbody;
        public float force = 1000;

        public TeamType team;
        public ulong ownerSteamId = 0;

        public override void OnStartServer()
        {
            Invoke(nameof(DestroySelf), destroyAfter);
        }

        // set velocity for server and client. this way we don't have to sync the
        // position, because both the server and the client simulate it.
        private void Start()
        {
            rigidbody.AddForce(transform.right * force);
        }

        // destroy for everyone on the server
        [Server]
        private void DestroySelf()
        {
            NetworkServer.Destroy(gameObject);
        }

        // ServerCallback because we don't want a warning
        // if OnTriggerEnter is called on the client
        // [ServerCallback]
        // private void OnTriggerEnter2D(Collider2D other) => DestroySelf();
    }
}