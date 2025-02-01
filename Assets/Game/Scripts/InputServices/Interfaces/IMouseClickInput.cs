using System;
using UnityEngine;

namespace Tavern.InputServices.Interfaces
{
    public interface IMouseClickInput
    {
        event Action<Vector2> OnMouseClicked;
    }
}