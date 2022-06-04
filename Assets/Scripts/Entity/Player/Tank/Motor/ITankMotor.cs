using UnityEngine;

namespace Entity.Player.Tank.Motor
{
    public interface ITankMotor
    {
        void MoveTo(Vector2 input);
        void StopMovement();
        void RotateTo(Vector2 input, Camera camera);
    }
}