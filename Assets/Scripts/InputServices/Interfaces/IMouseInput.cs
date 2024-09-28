using System;
using UnityEngine;

namespace InputServices
{
    public interface IMouseInput
    {
        event Action<Vector2> OnMouse;
    }
}