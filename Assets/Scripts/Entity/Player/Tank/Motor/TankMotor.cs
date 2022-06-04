using UnityEngine;

namespace Entity.Player.Tank.Motor
{
    public class TankMotor : MonoBehaviour, ITankMotor
    {
        [SerializeField]
        private float _movementSpeed = 5f;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _gunHolderTransform;
        
        private Vector2 _desiredDirection;
        private Quaternion _desiredRotation;

        private void FixedUpdate()
        {
            _rigidbody.velocity = _desiredDirection * _movementSpeed;
            _gunHolderTransform.localRotation = _desiredRotation;
        }

        public void MoveTo(Vector2 direction)
        {
            _desiredDirection = direction;
        }

        public void StopMovement()
        {
            _desiredDirection = Vector2.zero;
        }

        public void RotateTo(Vector2 input, Camera camera)
        {
            var direction = new Vector3(input.x, input.y, 0f) - camera.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            var desiredRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            _desiredRotation = desiredRotation;
        }
    }
}