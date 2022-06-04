using UnityEngine;

namespace Input.Controller
{
    public abstract class InputController : MonoBehaviour
    {
        public abstract bool HasMovementInput();
        public abstract bool HasRotationInput();
        
        public abstract Vector2 GetMovementInput();
        public abstract Vector2 GetRotationInput();
    }
}