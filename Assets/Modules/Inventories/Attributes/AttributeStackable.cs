using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Inventories
{
    [Serializable]
    public class AttributeStackable : ICloneable
    {
        public event Action<int> OnValueChanged;

        [SerializeField] 
        private bool IsLimited;

        [SerializeField, ShowIf("IsLimited")]
        private int MaxSize;

        private int _value;

        public bool IsFull => IsLimited && _value >= MaxSize;

        public AttributeStackable(bool isLimited = false, int maxSize = 0)
        {
            IsLimited = isLimited;
            MaxSize = maxSize;
        }

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

        object ICloneable.Clone()
        {
            return new AttributeStackable
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