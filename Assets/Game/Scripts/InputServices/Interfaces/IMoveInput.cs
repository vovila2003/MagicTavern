using System;
using UnityEngine;

namespace Tavern.InputServices.Interfaces
{
    public interface IMoveInput
    {
        event Action<Vector2> OnMove;
    }
}