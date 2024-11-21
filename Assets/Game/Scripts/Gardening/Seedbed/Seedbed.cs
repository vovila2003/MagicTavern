using System;
using Modules.GameCycle.Interfaces;
using Modules.Gardening;
using Sirenix.OdinInspector;
using Tavern.Storages;
using UnityEngine;
using VContainer;

namespace Tavern.Gardening
{
    public class Seedbed :
        MonoBehaviour,
        IStartGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IFinishGameListener,
        IUpdateListener
    {
        [SerializeField] 
        private SpriteRenderer HarvestSpriteRenderer;

        [SerializeField] 
        private GameObject WaterIndicator;

        public event Action<Plant, int> OnHarvestReceived;
        public event Action<int> OnSlopsReceived;

        private ISeedbed _seedbed;
        private bool _isEnable;
        private HarvestViewController _harvestController;
        private SeedbedHarvestController _seedbedHarvestController;
        private IProductsStorage _productsStorage;
        
        [ShowInInspector, ReadOnly] 
        private float _progress;
        
        [ShowInInspector, ReadOnly] 
        private float _dryingTimerProgress;

        [Inject]
        private void Construct(IProductsStorage productsStorage)
        {
            _productsStorage = productsStorage;
        }

        private void Awake()
        {
            _isEnable = true;
            _seedbed = new SeedbedImpl();
            _harvestController = new HarvestViewController(_seedbed, HarvestSpriteRenderer, WaterIndicator);
            _seedbedHarvestController = new SeedbedHarvestController(this, _productsStorage);
        }

        private void OnEnable()
        {
            _seedbed.OnHarvestProgressChanged += OnHarvestProgressChanged;
            _seedbed.OnDryingTimerProgressChanged += OnDryingTimerChanged;
        }

        private void OnDisable()
        {
            _seedbed.OnHarvestProgressChanged -= OnHarvestProgressChanged;
            _seedbed.OnDryingTimerProgressChanged -= OnDryingTimerChanged;
            
            _harvestController.Dispose();
            _seedbedHarvestController.Dispose();
        }

        public bool Seed(PlantConfig plantConfig)
        {
            if (!_isEnable || plantConfig is null) return false;

            bool result = _seedbed.Seed(plantConfig);
            Debug.Log($"Seedbed seeded: {result}");

            return result;
        }

        public void Gather()
        {
            if (!_isEnable) return;
            
            bool gathered = _seedbed.Gather(out HarvestResult harvestResult);
            Debug.Log($"Seedbed gathered: {gathered}.");
            if (!gathered) return;

            if (harvestResult.IsNormal)
            {
                OnHarvestReceived?.Invoke(harvestResult.Plant, harvestResult.Value);
            }
            else
            {
                OnSlopsReceived?.Invoke(harvestResult.Value);
            }
        }

        public void Watering()
        {
            if (!_isEnable) return;
            
            _seedbed.Watering();
        }

        void IUpdateListener.OnUpdate(float deltaTime)
        {
            if (!_isEnable) return;

            _seedbed.Tick(deltaTime);
        }

        void IStartGameListener.OnStart()
        {
            _isEnable = true;
            _seedbed.Resume();
        }

        void IPauseGameListener.OnPause()
        {
            _isEnable = false;
            _seedbed.Pause();
        }

        void IResumeGameListener.OnResume()
        {
            _isEnable = true;
            _seedbed.Resume();
        }

        void IFinishGameListener.OnFinish()
        {
            _isEnable = false;
            _seedbed.Stop();
        }

        private void OnHarvestProgressChanged(float progress) => _progress = progress;

        private void OnDryingTimerChanged(float progress) => _dryingTimerProgress = progress;
    }
}