using JetBrains.Annotations;
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
            new(_shoppingViewsFactory.CreateCategoriesView(viewContainer));
    }
}