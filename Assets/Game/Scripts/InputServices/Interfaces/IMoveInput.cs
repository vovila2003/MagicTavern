using System;
using UnityEngine;

namespace InputServices
{
    public interface IMoveInput
    {
        event Action<Vector2> OnMove;
    }
}