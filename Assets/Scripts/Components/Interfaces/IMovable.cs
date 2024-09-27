using UnityEngine;

namespace Components
{
    public interface IMovable
    {
        void Init(Transform transform, ISpeedable speedable);
        void Move(Vector2 direction);
        void OnUpdate(float deltaTime);
    }
}