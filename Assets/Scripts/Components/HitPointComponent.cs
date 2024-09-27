using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Components
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnDeath;

        [SerializeField] 
        private int HitPoints;

        [ShowInInspector, ReadOnly]
        private int _currentHp;

        public void Reset()
        {
            _currentHp = HitPoints;
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