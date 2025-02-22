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

        protected ResourceStorage Storage;

        [ShowInInspector, ReadOnly] 
        protected int Value => Storage?.Value ?? 0;

        protected void Awake()
        {
            Storage = new ResourceStorage(0, Limit, MaxValue);
        }

        protected virtual void OnEnable()
        {
            Storage.OnResourceStorageAdded += ValueAdded;
            Storage.OnResourceStorageChanged += ValueChanged;
            Storage.OnResourceStorageEmpty += OnEmpty;
            Storage.OnResourceStorageFull += OnFull;
            Storage.OnResourceStorageValueSpent += OnSpent;
        }

        protected virtual void OnDisable()
        {
            Storage.Dispose();
            Storage.OnResourceStorageAdded -= ValueAdded;
            Storage.OnResourceStorageChanged -= ValueChanged;
            Storage.OnResourceStorageEmpty -= OnEmpty;
            Storage.OnResourceStorageFull -= OnFull;
            Storage.OnResourceStorageValueSpent -= OnSpent;
        }

        private void Start()
        {
            if (DebugMode)
            {
                Storage.Add(StartValueInStorageInDebugMode);
            }
        }
        
        [Button]
        protected void Add(int value)
        {
            bool result = Storage.Add(value);

            Debug.Log($"Add to {Name} storage value {value}: result - {result}");
        }

        protected bool CanSpend(int value)
        {
            return Storage.CanSpend(value);
        }
        
        [Button]
        protected void Spend(int value)
        {
            bool result = Storage.Spend(value);

            Debug.Log($"Spend from {Name} storage value {value}: result - {result}");
        }

        [Button]
        protected void ResetStorage()
        {
            Storage.Reset();

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