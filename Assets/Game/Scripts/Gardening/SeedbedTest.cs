using Modules.Gardening;
using Modules.Gardening.Enums;
using Modules.Gardening.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Gardening
{
    public class SeedbedTest : MonoBehaviour
    {
        private ISeedbed _seedbed;

        private void Start()
        {
            _seedbed = new Seedbed();
            _seedbed.OnStateChanged += OnStateChanged;
            _seedbed.OnHarvestStateChanged += OnHarvestStateChanged;
            _seedbed.OnCaringChanged += OnCaringChanged;
        }
        
        private void OnDisable()
        {
            _seedbed.OnStateChanged -= OnStateChanged;
            _seedbed.OnHarvestStateChanged -= OnHarvestStateChanged;
            _seedbed.OnCaringChanged -= OnCaringChanged;
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
                Debug.Log($"HarvestResult: {harvestResult.IsCollected}, {harvestResult.Value}, {harvestResult.Type}");
            }
        }

        [Button]
        public void Care(CaringType caringType)
        {
            _seedbed.Care(caringType);
        }

        private void OnStateChanged(SeedbedState state)
        {
            Debug.Log($"Seedbed state changed to {state}");
        }

        private void OnHarvestStateChanged(HarvestState state)
        {
            Debug.Log($"Harvest state changed to {state}");
            if (state == HarvestState.Lost)
            {
                Debug.Log($"Lost by reason {_seedbed.LostReason}");
            }
        }

        private void OnCaringChanged(CaringType type, CaringState caringState)
        {
            Debug.Log($"Care {type} state changed to {caringState}!");
        }
    }
}