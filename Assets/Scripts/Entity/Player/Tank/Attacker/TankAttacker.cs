using System;
using Data.ValueObject.Tank;
using UnityEngine;

namespace Entity.Player.Tank.Attacker
{
    public class TankAttacker : MonoBehaviour, ITankAttacker
    {
        public Action OnAttackCallback { get; set; }

        [SerializeField] private Transform _gunEndPoint;
        [SerializeField] private GameObject _projectilePrefab;

        [SerializeField] private float _cooldown = 0.25f;

        private TankVO _vo;
        private float _lastAttackedTime = 0;

        public void Initialize(TankVO vo)
        {
            _vo = vo;
        }

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