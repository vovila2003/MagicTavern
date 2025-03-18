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
        private readonly UISceneSettings _uiSceneSettings;

        public IConvertInfoViewProvider ConvertInfoViewProvider { get; }
        

        public GardeningViewsFactory(GameSettings settings, SceneSettings sceneSettings)
        {
            _uiSettings = settings.UISettings;
            _uiSceneSettings = sceneSettings.UISceneSettings;
            
            ConvertInfoViewProvider = new ConvertInfoViewProvider(CreateConvertInfoPanelView, _uiSceneSettings.Pool);
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

        public IContainerView CreateSeedMakerProductItemsView(Transform viewContainer) => 
            Object.Instantiate(_uiSettings.Gardening.SeedMakerProductItemsView, viewContainer);
        
        public IContainerView CreateSeedMakerSeedsView(Transform viewContainer) => 
            Object.Instantiate(_uiSettings.Gardening.SeedMakerSeedsView, viewContainer);
        
        //private
        private ConvertInfoView CreateConvertInfoPanelView() =>
            Object.Instantiate(_uiSettings.Gardening.ConvertInfoView, _uiSceneSettings.Pool);
    }
}