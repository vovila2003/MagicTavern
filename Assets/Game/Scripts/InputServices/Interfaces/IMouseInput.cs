using System;
using UnityEngine;

namespace Tavern.InputServices.Interfaces
{
    public interface IMouseInput
    {
        event Action<Vector2> OnMouse;
    }
}