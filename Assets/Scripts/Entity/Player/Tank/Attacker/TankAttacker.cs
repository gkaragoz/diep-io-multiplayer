using System;
using UnityEngine;

namespace Entity.Player.Tank.Attacker
{
    public class TankAttacker : MonoBehaviour, ITankAttacker
    {
        [SerializeField] private Transform _gunEndPoint;
        [SerializeField] private GameObject _projectilePrefab;

        [SerializeField] private float _cooldown = 0.25f;

        private float _lastAttackedTime = 0;

        public Action OnAttackCallback { get; set; }

        private bool IsReadyToNextAttack()
        {
            return Time.time >= _lastAttackedTime + _cooldown;
        }
        
        public bool Attack()
        {
            if (IsReadyToNextAttack())
            {
                _lastAttackedTime = Time.time;
                return true;
            }
            
            return false;
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