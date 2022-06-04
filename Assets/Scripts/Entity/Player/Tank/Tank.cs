using System;
using Entity.Player.Tank.Motor;
using UnityEngine;

namespace Entity.Player.Tank
{
    public abstract class Tank : MonoBehaviour
    {
        protected ITankMotor Motor { get; set; }

        public Action OnAttackCallback { get; set; }
        
        protected virtual void Awake()
        {
            Motor = GetComponent<ITankMotor>();
        }

        public virtual void Attack()
        {
            
        }

        public virtual void MoveTo(Vector2 input)
        {
            Motor.MoveTo(input);
        }

        public virtual void StopMovement()
        {
            Motor.StopMovement();
        }

        public virtual void RotateTo(Vector2 input, Camera camera)
        {
            Motor.RotateTo(input, camera);
        }

        public virtual GameObject GetProjectilePrefab()
        {
            return null;
        }

        public virtual Transform GetGunEndPoint()
        {
            return null;
        }
    }
}