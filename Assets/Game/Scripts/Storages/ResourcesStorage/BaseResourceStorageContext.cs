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
        private ResourceStorage Storage;

        [ShowInInspector, ReadOnly]
        public float Value => Storage.Value;

        private void OnEnable()
        {
            Storage.Init();
            Storage.OnResourceStorageAdded += ValueAdded;
            Storage.OnResourceStorageChanged += ValueChanged;
            Storage.OnResourceStorageEmpty += OnEmpty;
            Storage.OnResourceStorageFull += OnFull;
            Storage.OnResourceStorageValueSpent += OnSpent;
        }

        private void OnDisable()
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

        private void ValueAdded(float value)
        {
            Debug.Log($"{Name} storage added by {value}");
        }

        private void ValueChanged(float value)
        {
        Debug.Log($"{Name} storage value changed to {value}");
        }

        private void OnSpent(float value)
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

        [Button]
        public void Add(float value)
        {
            bool result = Storage.Add(value);

            Debug.Log($"Add to {Name} storage value {value}: result - {result}");
        }

        [Button]
        public void Spend(float value)
        {
            bool result = Storage.Spend(value);

            Debug.Log($"Spend from {Name} storage value {value}: result - {result}");
        }

        [Button]
        public void ResetStorage()
        {
            Storage.Reset();

            Debug.Log($"Reset {Name} storage");
        }     
    }
}