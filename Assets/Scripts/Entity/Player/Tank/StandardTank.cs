using Data.ValueObject.Tank;
using Entity.Player.Tank.Attacker;
using UnityEngine;

namespace Entity.Player.Tank
{
    public class StandardTank : Tank
    {
        private StandardTankVO _standardTankVO;

        public override TankVO VO
        {
            get => _standardTankVO;
            set => _standardTankVO = value as StandardTankVO;
        }

        protected ITankAttacker Attacker { get; set; }

        public override void Initialize(TankVO vo)
        {
            _standardTankVO = vo as StandardTankVO;
            
            base.Initialize(vo);
            
            Attacker = GetComponent<ITankAttacker>();
            Attacker.Initialize(VO);
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