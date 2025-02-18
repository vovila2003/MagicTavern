using System;
using Tavern.Shopping;

namespace Tavern.UI.Presenters
{
    public class ShoppingPanelPresenter : BasePresenter
    {
        private const string Title = "Торговля";

        private readonly IPanelView _view;

        private InfoPresenter _infoPresenter;
        private Action _onExit;
        private SellerConfig _shopConfig;

        public ShoppingPanelPresenter(
            IPanelView view
            ) : base(view)
        {
            _view = view;
        }
        
        public void Show(SellerConfig shopConfig, Action onExit)
        {
            _shopConfig = shopConfig;
            _onExit = onExit;
            Show();
        }

        protected override void OnShow()
        {
            SetupView();
        }

        protected override void OnHide()
        {
            _view.OnCloseClicked -= Hide;
            
            _onExit?.Invoke();
        }

        private void SetupView()
        {
            _view.SetTitle($"{Title}: {_shopConfig.ShopMetadata.Title}");
            _view.OnCloseClicked += Hide;
        }
    }
}