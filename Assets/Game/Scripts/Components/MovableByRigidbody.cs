using Tavern.Components.Interfaces;
using UnityEngine;

namespace Tavern.Components
{
    public sealed class MovableByRigidbody : IMovable
    {
        private Vector3 _direction;
        private Rigidbody _rigidbody;
        public ISpeedable Speedable { get; private set; }

        public void Init(Rigidbody rigidbody, ISpeedable speedable)
        {
            _rigidbody = rigidbody;
            Speedable = speedable;
        }

        void IMovable.OnFixedUpdate(float fixedDeltaTime)
        {
            Vector3 nextPosition = _rigidbody.position + _direction * (Speedable.GetSpeed() * fixedDeltaTime);
            _rigidbody.MovePosition(nextPosition);
        }

        public void Move(Vector3 direction)
        {
            _direction = direction;
        }
    }
}