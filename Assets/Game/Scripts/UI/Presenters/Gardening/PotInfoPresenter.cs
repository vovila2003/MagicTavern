using Modules.Gardening;
using Tavern.Gardening;
using Tavern.Settings;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class PotInfoPresenter : BasePresenter
    {
        private readonly IPotInfoView _view;
        private readonly GardeningUISettings _settings;
        private Pot _pot;

        public PotInfoPresenter(
            IPotInfoView view,
            GardeningUISettings settings
        ) : base(view)
        {
            _view = view;
            _settings = settings;
        }

        public void Show(Pot pot)
        {
            _pot = pot;
            Show();
        }
        
        protected override void OnShow()
        {
            if (_pot.IsSeeded)
            {
                SetupSeededPot();
            }
            else
            {
                SetupEmptyPot();
            }

            _view.OnGatherClicked += OnGatherClicked;
            _view.OnWateringClicked += OnWaterClicked;
        }

        protected override void OnHide()
        {
            _view.OnGatherClicked -= OnGatherClicked;
            _view.OnWateringClicked -= OnWaterClicked;
        }

        private void SetupSeededPot()
        {
            _view.SetTitle(_pot.CurrentSeedConfig.Metadata.Title);
            _view.SetIcon(_pot.CurrentSprite);
            _view.SetProgress(_pot.Progress);
            _view.SetIsFertilized(_pot.IsFertilized);
            bool isReady = _pot.Seedbed.Harvest.State != HarvestState.Growing;
            if (isReady)
            {
                _view.SetIsSick(false);
                _view.SetIsWaterNeed(false);
                _view.SetWateringActive(false);
            }
            else
            {
                _view.SetIsSick(_pot.IsSick);
                bool waterRequired = _pot.WaterRequired;
                _view.SetIsWaterNeed(waterRequired);
                _view.SetWateringActive(waterRequired);
            }
            
            _view.SetGatherActive(isReady);
        }

        private void SetupEmptyPot()
        {
            _view.SetTitle(string.Empty);
            _view.SetIcon(_settings.EmptyPotSprite);
            _view.SetProgress(0);
            _view.SetIsFertilized(false);
            _view.SetIsSick(false);
            _view.SetIsWaterNeed(false);
            _view.SetWateringActive(false);
            _view.SetGatherActive(false);
        }

        private void OnGatherClicked()
        {
            Debug.Log("Gather");
            _pot.Gather();
            SetupEmptyPot();
        }

        private void OnWaterClicked()
        {
            Debug.Log("Watering");
            _pot.Watering();
            _view.SetIsWaterNeed(false);
            _view.SetWateringActive(false);
        }
    }
}