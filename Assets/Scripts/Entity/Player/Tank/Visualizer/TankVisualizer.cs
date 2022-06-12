using System;
using Data.ValueObject.Tank;
using Enums;
using UnityEngine;

namespace Entity.Player.Tank.Visualizer
{
    public class TankVisualizer : MonoBehaviour, ITankVisualizer
    {
        [SerializeField] private SpriteRenderer _body;
        [SerializeField] private SpriteRenderer _gun;
        
        private TankVO _vo;
        
        public void Initialize(TankVO vo)
        {
            _vo = vo;
        }

        public void SetVisualization(TeamType team)
        {
            _body.sprite = _vo.bodySprite;
            _gun.sprite = _vo.gunSprite;
            
            switch (team)
            {
                case TeamType.TeamA:
                    _body.color = Color.white;
                    _gun.color = Color.white;
                    break;
                case TeamType.TeamB:
                    _body.color = Color.red;
                    _gun.color = Color.red;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}