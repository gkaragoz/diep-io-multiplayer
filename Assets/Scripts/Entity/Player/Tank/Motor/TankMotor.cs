using Data.ValueObject.Tank;
using UnityEngine;

namespace Entity.Player.Tank.Motor
{
    public class TankMotor : MonoBehaviour, ITankMotor
    {
        [SerializeField]
        private float _movementSpeed = 5f;
        [SerializeField] 
        private bool _hasBlocked = false;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _gunHolderTransform;

        private TankVO _vo;
        private Vector2 _desiredDirection;
        private Quaternion _desiredRotation;

        public void Initialize(TankVO vo)
        {
            _vo = vo;
        }
        
        private void FixedUpdate()
        {
            if (_hasBlocked)
                return;
            
            _rigidbody.velocity = _desiredDirection * _movementSpeed;
            _gunHolderTransform.localRotation = _desiredRotation;
        }

        public void MoveTo(Vector2 direction)
        {
            if (_hasBlocked)
                return;
            
            _desiredDirection = direction;
        }

        public void StopMovement()
        {
            if (_hasBlocked)
                return;
            
            _desiredDirection = Vector2.zero;
        }

        public void RotateTo(Vector2 input, Camera camera)
        {
            if (_hasBlocked)
                return;
            
            var direction = new Vector3(input.x, input.y, 0f) - camera.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            var desiredRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            _desiredRotation = desiredRotation;
        }

        public void Block()
        {
            _hasBlocked = true;
        }
    }
}