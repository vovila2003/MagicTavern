using System;
using UnityEngine;

namespace InputServices
{
    public interface IDodgeInput
    {
        event Action<Vector2> OnDodge;
    }
}