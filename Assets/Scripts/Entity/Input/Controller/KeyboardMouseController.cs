﻿using Entity.Input.Keyboard;
using Entity.Input.Mouse;
using UnityEngine;

namespace Entity.Input.Controller
{
    public class KeyboardMouseController : InputController
    {
        public KeyboardInputBase KeyboardInput { get; private set; }
        public MouseInputBase MouseInputBase { get; private set; }

        private void Awake()
        {
            KeyboardInput = gameObject.AddComponent<KeyboardInput>();
            MouseInputBase = gameObject.AddComponent<MouseInput>();
        }

        public override bool HasMovementInput()
        {
            return KeyboardInput.HasInput();
        }

        public override bool HasRotationInput()
        {
            return MouseInputBase.HasInput();
        }

        public override Vector2 GetMovementInput()
        {
            return KeyboardInput.CurrentInput;
        }

        public override Vector2 GetRotationInput()
        {
            return MouseInputBase.CurrentInput;
        }
        
        public override bool HasAttackButtonPressed()
        {
            return UnityEngine.Input.GetMouseButton(0);
        }
    }
}