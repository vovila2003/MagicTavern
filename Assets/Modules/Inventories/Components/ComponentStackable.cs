using System;
using Modules.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Inventories
{
    [Serializable]
    public class ComponentStackable : IItemComponent
    {
        public event Action<int> OnValueChanged;

        [SerializeField] 
        private bool IsLimited;

        [SerializeField, ShowIf("IsLimited")]
        private int MaxSize;

        private int _value;

        public bool IsFull => IsLimited && _value >= MaxSize;
        public bool Limited => IsLimited;

        public ComponentStackable()
        {
            Value = 1;
        }

        private ComponentStackable(bool isLimited = false, int maxSize = 0)
        {
            IsLimited = isLimited;
            MaxSize = maxSize;
            Value = 1;
        }

        [ShowInInspector, ReadOnly]
        public int Value
        {
            get => _value;
            set => SetValue(value);
        }

        public int Size
        {
            get => MaxSize;
            set => MaxSize = value;
        }

        public IItemComponent Clone()
        {
            return new ComponentStackable
            {
                _value = _value,
                MaxSize = MaxSize
            };
        }

        private void SetValue(int value)
        {
            if (IsLimited)
            {
                value = Mathf.Clamp(value, 0, MaxSize);
            }
            
            _value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}