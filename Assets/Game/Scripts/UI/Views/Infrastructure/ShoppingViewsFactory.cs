using JetBrains.Annotations;
using Tavern.Settings;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    [UsedImplicitly]
    public class ShoppingViewsFactory : IShoppingViewsFactory
    {
        private readonly UISettings _uiSettings;
        private readonly UISceneSettings _uiSceneSettings;

        public IDealInfoViewProvider DealInfoViewProvider { get; }

        public ShoppingViewsFactory(GameSettings gameSettings, SceneSettings sceneSettings)
        {
            _uiSettings = gameSettings.UISettings;
            _uiSceneSettings = sceneSettings.UISceneSettings;
            DealInfoViewProvider = new DealInfoViewProvider(CreateDealInfoPanelView, _uiSceneSettings.Pool);
        }
        
        public ICategoriesView CreateCategoriesView(Transform viewContainer) => 
            Object.Instantiate(_uiSettings.Shopping.CategoriesView, viewContainer);
        
        public IContainerView CreateShopItemsView(Transform viewContainer) => 
            Object.Instantiate(_uiSettings.Shopping.ShopItemView, viewContainer);
        
        public IVendorInfoView CreateVendorInfoView(Transform viewContainer) =>
            Object.Instantiate(_uiSettings.Shopping.VendorInfoView, viewContainer);
        
        public IContainerView CreateCharacterItemsView(Transform viewContainer) => 
            Object.Instantiate(_uiSettings.Shopping.CharacterItemView, viewContainer);
        
        public ICharacterInfoView CreateCharacterInfoView(Transform viewContainer) =>
            Object.Instantiate(_uiSettings.Shopping.CharacterInfoView, viewContainer);

        public IFilterView CreateFilterView(Transform viewContainer) => 
            Object.Instantiate(_uiSettings.Shopping.FilterView, viewContainer);
        
        //private
        private DealInfoView CreateDealInfoPanelView() =>
            Object.Instantiate(_uiSettings.Shopping.DealInfoView, _uiSceneSettings.Pool);
    }
}