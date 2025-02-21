using JetBrains.Annotations;
using Modules.Shopping;
using Tavern.Shopping;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    [UsedImplicitly]
    public class ShoppingPresentersFactory
    {
        private readonly ICommonViewsFactory _commonViewsFactory;
        private readonly IShoppingViewsFactory _shoppingViewsFactory;

        public ShoppingPresentersFactory(
            ICommonViewsFactory commonViewsFactory,
            IShoppingViewsFactory shoppingViewsFactory)
        {
            _commonViewsFactory = commonViewsFactory;
            _shoppingViewsFactory = shoppingViewsFactory;
        }

        public ShoppingPanelPresenter CreateShoppingPanelPresenter() => 
            new(_commonViewsFactory.CreatePanelView(),
            this);

        public CategoriesPresenter CreateCategoriesPresenter(Transform viewContainer) => 
            new(_shoppingViewsFactory.CreateCategoriesView(viewContainer),
                this);

        public FilterPresenter CreateFilterPresenter(Transform viewContainer, string filterText) =>
            new(_shoppingViewsFactory.CreateFilterView(viewContainer), filterText);

        public ItemConfigCardPresenter CreateItemConfigCardPresenter(Transform viewContainer) =>
            new(_commonViewsFactory.GetItemCardView(viewContainer), 
                _commonViewsFactory.ItemCardViewPool);

        public ShopItemsPresenter CreateShopItemsPresenter(Transform viewContainer) =>
            new(_shoppingViewsFactory.CreateShopItemsView(viewContainer), this);
    }
}