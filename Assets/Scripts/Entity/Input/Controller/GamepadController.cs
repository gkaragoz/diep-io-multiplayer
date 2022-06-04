using Input.Gamepad;
using UnityEngine;

namespace Input.Controller
{
    public class GamepadController : InputController
    {
        private GamepadInputBase _gamepadInputBase;

        private void Awake()
        {
            _gamepadInputBase = gameObject.AddComponent<GamepadInput>();
        }

        public override bool HasMovementInput()
        {
            return _gamepadInputBase.HasInput();
        }

        public override bool HasRotationInput()
        {
            return _gamepadInputBase.HasInput();
        }

        public override Vector2 GetMovementInput()
        {
            return _gamepadInputBase.CurrentInput;
        }

        public override Vector2 GetRotationInput()
        {
            return _gamepadInputBase.CurrentInput;
        }
    }
}