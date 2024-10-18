using System;
using UnityEngine;

namespace Tavern.InputServices.Interfaces
{
    public interface IDodgeInput
    {
        event Action<Vector2> OnDodge;
    }
}