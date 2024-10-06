using UnityEngine;

namespace Components
{
    public sealed class MovableByTransform : IMovable
    {
        private Vector3 _direction;
        private Transform _transform;
        private ISpeedable _speedable;

        public void Init(Transform transform, ISpeedable speedable)
        {
            _transform = transform;
            _speedable = speedable;
        }

        public void OnUpdate(float deltaTime)
        {
            Vector3 nextPosition = _transform.position + _direction * (_speedable.GetSpeed() * deltaTime);
            _transform.position = nextPosition;
            
        }

        public void Move(Vector3 direction)
        {
            _direction = direction;
        }
    }
}