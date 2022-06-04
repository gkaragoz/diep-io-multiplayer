using UnityEngine;

namespace Input.Keyboard
{
    public abstract class KeyboardInputBase : InputReceiver.InputReceiver
    {
        protected override Vector2 GetCurrentInput()
        {
            return new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
        }
    }
}