using System;
using Data.ValueObject.Tank;
using Entity.Player.Tank.Motor;
using Entity.Player.Tank.TankBase;
using Entity.Player.Tank.Visualizer;
using Enums;
using UnityEngine;

namespace Entity.Player.Tank
{
    public abstract class Tank : MonoBehaviour, ITank
    {
        public virtual TankVO VO { get; set; }

        protected ITankMotor Motor { get; set; }
        protected ITankVisualizer Visualizer { get; set; }

        public Action OnAttackCallback { get; set; }
        
        public virtual void Initialize(TankVO vo)
        {
            Motor = GetComponent<ITankMotor>();
            Visualizer = GetComponent<ITankVisualizer>();
            
            Motor.Initialize(VO);
            Visualizer.Initialize(VO);
        }

        public virtual void SetVisualization(TeamType team)
        {
            Visualizer.SetVisualization(team);
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