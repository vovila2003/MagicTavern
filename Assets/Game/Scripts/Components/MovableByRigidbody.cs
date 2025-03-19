using System;
using JetBrains.Annotations;
using Tavern.Components.Interfaces;
using UnityEngine;

namespace Tavern.Components
{
    [UsedImplicitly]
    public sealed class MovableByRigidbody : IMovable
    {
        public event Action<Vector3> OnPositionChanged;
        
        private Vector3 _direction;
        private Rigidbody _rigidbody;
        private Vector3 _previousPosition;
        
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

            if (_previousPosition == nextPosition) return;
            
            _previousPosition = nextPosition;
            OnPositionChanged?.Invoke(_previousPosition);
        }

        public void Move(Vector3 direction)
        {
            _direction = direction;
        }
    }
}