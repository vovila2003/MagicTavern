using Modules.Gardening;
using Modules.Gardening.Enums;
using Modules.Gardening.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gardening
{
    public class SeedbedTest : MonoBehaviour
    {
        private ISeedbed _seedbed;

        private void Start()
        {
            _seedbed = new Seedbed();
            _seedbed.OnStateChanged += OnStateChanged;
            _seedbed.OnHarvestStateChanged += OnHarvestStateChanged;
            _seedbed.OnCareNeeded += OnCareNeed;
        }

        private void OnDisable()
        {
            _seedbed.OnStateChanged -= OnStateChanged;
            _seedbed.OnHarvestStateChanged -= OnHarvestStateChanged;
            _seedbed.OnCareNeeded -= OnCareNeed;
        }

        private void Update()
        {
            _seedbed?.Tick(Time.deltaTime);
        }

        [Button]
        public void Prepare()
        {
            bool result = _seedbed.Prepare();
            Debug.Log($"Prepare seedbed: {result}");
        }

        [Button]
        public void Seed(SeedConfig seedConfig)
        {
            if (seedConfig is null) return;
            
            bool result = _seedbed.Seed(seedConfig);
            Debug.Log($"Seeded: {result}");
        }
        
        [Button]
        public void Gather()
        {
            bool result = _seedbed.Gather(out HarvestResult harvestResult);
            Debug.Log($"Gather: {result}.");
            if (result)
            {
                Debug.Log($"HarvestResult: {harvestResult.IsCorrect}, {harvestResult.Value}, {harvestResult.Type}");
            }
        }

        [Button]
        public void Care(AttributeType attributeType)
        {
            _seedbed.Care(attributeType);
        }

        private void OnStateChanged(SeedbedState state)
        {
            Debug.Log($"Seedbed state changed to {state}");
        }

        private void OnHarvestStateChanged(HarvestState state)
        {
            Debug.Log($"Harvest state changed to {state}");
        }

        private void OnCareNeed(AttributeType type)
        {
            Debug.Log($"Care {type} needed!");
        }
    }
}