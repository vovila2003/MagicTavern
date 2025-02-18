using JetBrains.Annotations;

namespace Tavern.UI.Presenters
{
    [UsedImplicitly]
    public class ShoppingPresentersFactory
    {
        private readonly ICommonViewsFactory _commonViewsFactory;

        public ShoppingPresentersFactory(ICommonViewsFactory commonViewsFactory)
        {
            _commonViewsFactory = commonViewsFactory;
        }

        public ShoppingPanelPresenter CreateShoppingPanelPresenter() => 
            new(_commonViewsFactory.CreatePanelView());
    }
}