using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IShoppingViewsFactory
    {
        IDealInfoViewProvider DealInfoViewProvider { get; }
        ICategoriesView CreateCategoriesView(Transform viewContainer);
        IContainerView CreateShopItemsView(Transform viewContainer);
        IVendorInfoView CreateVendorInfoView(Transform viewContainer);
        IContainerView CreateCharacterItemsView(Transform viewContainer);
        ICharacterInfoView CreateCharacterInfoView(Transform viewContainer);
        IFilterView CreateFilterView(Transform viewContainer);
    }
}