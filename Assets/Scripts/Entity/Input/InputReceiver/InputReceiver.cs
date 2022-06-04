using UnityEngine;

namespace Input.InputReceiver
{
    public abstract class InputReceiver : MonoBehaviour, IInputReceiver
    {
        public Vector2 CurrentInput => GetCurrentInput();

        protected abstract Vector2 GetCurrentInput();

        public virtual bool HasInput()
        {
            return CurrentInput != Vector2.zero;
        }
    }
}