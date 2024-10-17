using UnityEngine;

namespace Tavern.Components.Interfaces
{
    public interface IMovable
    {
        void Init(Rigidbody rigidbody, ISpeedable speedable);
        void Move(Vector3 direction);
        void OnFixedUpdate(float fixedDeltaTime);
    }
}