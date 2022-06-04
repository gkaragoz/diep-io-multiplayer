using Entity.Player.Tank.Attacker;
using UnityEngine;

namespace Entity.Player.Tank
{
    public class StandardTank : Tank
    {
        protected ITankAttacker Attacker { get; set; }

        protected override void Awake()
        {
            base.Awake();

            Attacker = GetComponent<ITankAttacker>();

            Attacker.OnAttackCallback = OnAttackCallback;
        }

        public override void Attack()
        {
            if (Attacker.Attack()) 
                OnAttackCallback?.Invoke();
        }

        public override GameObject GetProjectilePrefab()
        {
            return Attacker.GetProjectilePrefab();
        }

        public override Transform GetGunEndPoint()
        {
            return Attacker.GetGunEndPoint();
        }
    }
}