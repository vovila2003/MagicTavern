using JetBrains.Annotations;
using Tavern.Settings;
using Tavern.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    [UsedImplicitly]
    public class GardeningViewsFactory : IGardeningViewsFactory
    {
        private readonly UISettings _uiSettings;

        public GardeningViewsFactory(GameSettings settings)
        {
            _uiSettings = settings.UISettings;
        }
        
        public IContainerView CreateSeedItemsView(Transform viewContainer) => 
            Object.Instantiate(_uiSettings.Gardening.SeedItemView, viewContainer);
        
        public IContainerView CreateFertilizerItemsView(Transform viewContainer) => 
            Object.Instantiate(_uiSettings.Gardening.FertilizerItemView, viewContainer);
        
        public IContainerView CreateMedicineItemsView(Transform viewContainer) => 
            Object.Instantiate(_uiSettings.Gardening.MedicineItemView, viewContainer);
        
        public Button CreateMakeSeedsButton(Transform viewContainer) => 
            Object.Instantiate(_uiSettings.Gardening.MakeSeedsButton, viewContainer);
        
        public IPotInfoView CreatePotInfoView(Transform viewContainer) =>
            Object.Instantiate(_uiSettings.Gardening.PotInfoView, viewContainer);
        
    }
}