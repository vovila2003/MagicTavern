using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Components
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnDeath;

        [ShowInInspector, ReadOnly]
        private int _currentHp;
        private int _initialHp;

        public int CurrentHp => _currentHp;

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

        public void Set(int value)
        {
            _currentHp = Mathf.Clamp(value ,0, _initialHp);;
            TakeDamage(0);
        }
    }
}