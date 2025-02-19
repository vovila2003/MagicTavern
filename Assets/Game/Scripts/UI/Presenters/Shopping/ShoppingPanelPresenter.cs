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
        private SellerConfig _sellerConfig;

        public ShoppingPanelPresenter(
            IPanelView view,
            ShoppingPresentersFactory factory
            ) : base(view)
        {
            _view = view;
            _factory = factory;
            _categoriesPresenter = _factory.CreateCategoriesPresenter(_view.Container);
        }
        
        public void Show(SellerConfig sellerConfig, Action onExit)
        {
            _sellerConfig = sellerConfig;
            _onExit = onExit;
            Show();
        }

        protected override void OnShow()
        {
            SetupView();
            SetupCategories();
        }

        protected override void OnHide()
        {
            _view.OnCloseClicked -= Hide;
            
            _onExit?.Invoke();
        }

        private void SetupView()
        {
            _view.SetTitle($"{Title}: {_sellerConfig.ShopMetadata.Title}");
            _view.OnCloseClicked += Hide;
        }

        private void SetupCategories()
        {
            _categoriesPresenter.Show(_sellerConfig);
        }
    }
}