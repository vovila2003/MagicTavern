using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Components
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnDeath;

        [ShowInInspector, ReadOnly]
        private int _currentHp;
        private int _initialHp;

        public void Init(int hitPoints)
        {
            _initialHp = hitPoints;
        }
        
        public void Reset()
        {
            _currentHp = _initialHp;
        }
        
        public void TakeDamage(int damage)
        {
            _currentHp -= damage;
            if (_currentHp <= 0)
            {
                OnDeath?.Invoke(gameObject);
            }
        }
    }
}