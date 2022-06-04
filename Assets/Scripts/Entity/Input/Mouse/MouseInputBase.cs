using UnityEngine;

namespace Input.Mouse
{
    public abstract class MouseInputBase : InputReceiver.InputReceiver
    {
        protected override Vector2 GetCurrentInput()
        {
            var mousePosition = UnityEngine.Input.mousePosition;
            return new Vector2(mousePosition.x, mousePosition.y);
        }
    }
}