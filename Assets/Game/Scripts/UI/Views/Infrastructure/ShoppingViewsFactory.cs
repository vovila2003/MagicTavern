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

        public ShoppingViewsFactory(UISettings settings)
        {
            _settings = settings;
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
    }
}