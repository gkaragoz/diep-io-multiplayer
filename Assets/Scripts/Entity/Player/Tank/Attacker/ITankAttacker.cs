using System;
using Data.ValueObject.Tank;
using UnityEngine;

namespace Entity.Player.Tank.Attacker
{
    public interface ITankAttacker
    {
        void Initialize(TankVO vo);
        
        Action OnAttackCallback { get; set; }
        bool Attack();
        GameObject GetProjectilePrefab();
        Transform GetGunEndPoint();
    }
}