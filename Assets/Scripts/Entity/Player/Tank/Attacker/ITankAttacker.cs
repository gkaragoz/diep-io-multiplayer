using System;
using UnityEngine;

namespace Entity.Player.Tank.Attacker
{
    public interface ITankAttacker
    {
        Action OnAttackCallback { get; set; }
        bool Attack();
        GameObject GetProjectilePrefab();
        Transform GetGunEndPoint();
    }
}