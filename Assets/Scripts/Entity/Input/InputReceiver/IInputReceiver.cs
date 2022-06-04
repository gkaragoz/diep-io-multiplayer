using UnityEngine;

namespace Entity.Input.InputReceiver
{
    public interface IInputReceiver
    {
        Vector2 CurrentInput { get; }
        bool HasInput();
    }
}