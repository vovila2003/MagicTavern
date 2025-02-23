using JetBrains.Annotations;
using Tavern.Settings;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    [UsedImplicitly]
    public class ShoppingViewsFactory : IShoppingViewsFactory
    {
        private readonly UISettings _settings;
        private readonly UISceneSettings _sceneSettings;

        public IDealInfoViewProvider DealInfoViewProvider { get; }

        public ShoppingViewsFactory(UISettings settings, UISceneSettings sceneSettings)
        {
            _settings = settings;
            _sceneSettings = sceneSettings;
            DealInfoViewProvider = new DealInfoViewProvider(CreateDealInfoPanelView, _sceneSettings.Pool);
        }
        
        public ICategoriesView CreateCategoriesView(Transform viewContainer) => 
            Object.Instantiate(_settings.Shopping.CategoriesView, viewContainer);
        
        public IContainerView CreateShopItemsView(Transform viewContainer) => 
            Object.Instantiate(_settings.Shopping.ShopItemView, viewContainer);
        
        public IVendorInfoView CreateVendorInfoView(Transform viewContainer) =>
            Object.Instantiate(_settings.Shopping.VendorInfoView, viewContainer);
        
        public IContainerView CreateCharacterItemsView(Transform viewContainer) => 
            Object.Instantiate(_settings.Shopping.CharacterItemView, viewContainer);
        
        public ICharacterInfoView CreateCharacterInfoView(Transform viewContainer) =>
            Object.Instantiate(_settings.Shopping.CharacterInfoView, viewContainer);

        public IFilterView CreateFilterView(Transform viewContainer) => 
            Object.Instantiate(_settings.Shopping.FilterView, viewContainer);
        
        //private
        private DealInfoView CreateDealInfoPanelView() =>
            Object.Instantiate(_settings.Shopping.DealInfoView, _sceneSettings.Pool);
    }
}