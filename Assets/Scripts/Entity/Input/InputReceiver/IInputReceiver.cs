using UnityEngine;

namespace Input.InputReceiver
{
    public interface IInputReceiver
    {
        Vector2 CurrentInput { get; }
        bool HasInput();
    }
}