using System;
using Tavern.Shopping;

namespace Tavern.UI.Presenters
{
    public class ShoppingPanelPresenter : BasePresenter
    {
        private const string Title = "Торговля";

        private readonly IPanelView _view;
        private readonly ShoppingPresentersFactory _factory;
        private readonly CategoriesPresenter _categoriesPresenter;

        private InfoPresenter _infoPresenter;
        private Action _onExit;
        private Shop _shop;

        public ShoppingPanelPresenter(
            IPanelView view,
            ShoppingPresentersFactory factory
            ) : base(view)
        {
            _view = view;
            _factory = factory;
            _categoriesPresenter = _factory.CreateCategoriesPresenter(_view.Container);
        }
        
        public void Show(Shop shop, Action onExit)
        {
            _shop = shop;
            _onExit = onExit;
            Show();
        }

        protected override void OnShow()
        {
            SetupView();
            SetupCategories();

            _shop.OnUpdated += OnShopUpdated;
        }

        protected override void OnHide()
        {
            _view.OnCloseClicked -= Hide;
            _shop.OnUpdated -= OnShopUpdated;
            
            _onExit?.Invoke();
        }

        private void SetupView()
        {
            _view.SetTitle($"{Title}: {_shop.SellerConfig.ShopMetadata.Title}");
            _view.OnCloseClicked += Hide;
        }

        private void SetupCategories()
        {
            _categoriesPresenter.Show(_shop.NpcSeller.ItemPrices);
        }

        private void OnShopUpdated()
        {
            _categoriesPresenter.Hide();
            SetupCategories();
        }
    }
}