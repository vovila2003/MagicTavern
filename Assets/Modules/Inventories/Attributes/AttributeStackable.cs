using System;
using UnityEngine;

namespace Modules.Inventories
{
    [Serializable]
    public class AttributeStackable : ICloneable
    {
        public event Action<int> OnValueChanged;

        [SerializeField]
        private int MaxSize;
        
        private int _value;
        
        public bool IsFull => _value >= MaxSize;
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

        private void SetValue(int value)
        {
            value = Mathf.Clamp(value, 0, MaxSize);
            _value = value;
            OnValueChanged?.Invoke(value);
        }

        public object Clone()
        {
            return new AttributeStackable
            {
                _value = _value,
                MaxSize = MaxSize
            };
        }
    }
}