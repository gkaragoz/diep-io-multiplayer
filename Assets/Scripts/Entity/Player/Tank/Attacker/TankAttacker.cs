using System;
using UnityEngine;

namespace Entity.Player.Tank.Attacker
{
    public class TankAttacker : MonoBehaviour, ITankAttacker
    {
        [SerializeField] private Transform _gunEndPoint;
        [SerializeField] private GameObject _projectilePrefab;

        public Action OnAttackCallback { get; set; }
        public bool Attack()
        {
            return true;
        }

        public GameObject GetProjectilePrefab()
        {
            return _projectilePrefab;
        }

        public Transform GetGunEndPoint()
        {
            return _gunEndPoint;
        }
    }
}