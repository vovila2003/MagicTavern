using Modules.Storages;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Storages
{
    public abstract class BaseResourceStorageContext : MonoBehaviour
    {
        [SerializeField] 
        private string Name;

        [SerializeField] 
        private bool DebugMode;

        [SerializeField, ShowIf("DebugMode")] 
        private int StartValueInStorageInDebugMode;
        
        [SerializeField]
        private LimitType Limit = LimitType.Unlimited;

        [SerializeField, ShowIf("Limit", LimitType.Limited)]
        private int MaxValue;

        private ResourceStorage _storage;

        [ShowInInspector, ReadOnly] 
        protected int Value => _storage?.Value ?? 0;

        private void Awake()
        {
            _storage = new ResourceStorage(0, Limit, MaxValue);
        }

        private void OnEnable()
        {
            _storage.OnResourceStorageAdded += ValueAdded;
            _storage.OnResourceStorageChanged += ValueChanged;
            _storage.OnResourceStorageEmpty += OnEmpty;
            _storage.OnResourceStorageFull += OnFull;
            _storage.OnResourceStorageValueSpent += OnSpent;
        }

        private void OnDisable()
        {
            _storage.Dispose();
            _storage.OnResourceStorageAdded -= ValueAdded;
            _storage.OnResourceStorageChanged -= ValueChanged;
            _storage.OnResourceStorageEmpty -= OnEmpty;
            _storage.OnResourceStorageFull -= OnFull;
            _storage.OnResourceStorageValueSpent -= OnSpent;
        }

        private void Start()
        {
            if (DebugMode)
            {
                _storage.Add(StartValueInStorageInDebugMode);
            }
        }
        
        [Button]
        protected void Add(int value)
        {
            bool result = _storage.Add(value);

            Debug.Log($"Add to {Name} storage value {value}: result - {result}");
        }

        protected bool CanSpend(int value)
        {
            return _storage.CanSpend(value);
        }
        
        [Button]
        protected void Spend(int value)
        {
            bool result = _storage.Spend(value);

            Debug.Log($"Spend from {Name} storage value {value}: result - {result}");
        }

        [Button]
        protected void ResetStorage()
        {
            _storage.Reset();

            Debug.Log($"Reset {Name} storage");
        }

        private void ValueAdded(int value)
        {
            Debug.Log($"{Name} storage added by {value}");
        }

        private void ValueChanged(int value)
        {
            Debug.Log($"{Name} storage value changed to {value}");
        }

        private void OnSpent(int value)
        {
            Debug.Log($"{Name} storage spent by {value}");
        }

        private void OnFull()
        {
            Debug.Log($"{Name} storage is full");
        }

        private void OnEmpty()
        {
            Debug.Log($"{Name} storage is empty");
        }
    }
}