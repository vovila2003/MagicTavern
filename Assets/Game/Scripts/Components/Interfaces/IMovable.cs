using System;
using UnityEngine;

namespace Tavern.Components.Interfaces
{
    public interface IMovable
    {
        event Action<Vector3> OnPositionChanged;
        
        void Init(Rigidbody rigidbody, ISpeedable speedable);
        void Move(Vector3 direction);
        void OnFixedUpdate(float fixedDeltaTime);
        ISpeedable Speedable { get; }
    }
}