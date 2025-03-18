using System;
using Modules.Shopping;
using Tavern.Shopping;

namespace Tavern.UI.Presenters
{
    public class ShoppingPanelPresenter : BasePresenter
    {
        private const string Title = "Торговля";

        private readonly IPanelView _view;
        private readonly ShoppingPresentersFactory _shoppingPresentersFactory;
        private CategoriesPresenter _categoriesPresenter;
        private ShopItemsPresenter _shopItemsPresenter;
        private ShopCharacterItemsPresenter _shopCharacterItemsPresenter;
        private VendorInfoPresenter _vendorInfoPresenter; 
        private CharacterItemsPresenter _characterItemsPresenter; 
        private CharacterInfoPresenter _characterInfoPresenter;

        private Action _onExit;
        private Shop _shop;

        public ShoppingPanelPresenter(
            IPanelView view,
            ShoppingPresentersFactory shoppingPresentersFactory
            ) : base(view)
        {
            _view = view;
            _shoppingPresentersFactory = shoppingPresentersFactory;
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
            SetupShopItems();
            SetupVendorInfo();
            SetupCharacterItems();
            SetupCharacterInfo();

            _shop.OnUpdated += OnShopUpdated;
        }

        protected override void OnHide()
        {
            _view.OnCloseClicked -= Hide;
            
            _categoriesPresenter.Hide();
            _shopItemsPresenter.Hide();
            _shopCharacterItemsPresenter?.Hide();
            _vendorInfoPresenter.Hide(); 
            _characterItemsPresenter.Hide(); 
            _characterInfoPresenter.Hide();

            _categoriesPresenter.OnShowAllGoods -= OnShowAllGoods;
            _categoriesPresenter.OnShowGroup -= OnShowGroup;
            _categoriesPresenter.OnShowBuyOut -= OnShowBuyOut;

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
            _categoriesPresenter ??= _shoppingPresentersFactory.CreateCategoriesPresenter(_view.Container);
            
            _categoriesPresenter.OnShowAllGoods += OnShowAllGoods;
            _categoriesPresenter.OnShowGroup += OnShowGroup;
            _categoriesPresenter.OnShowBuyOut += OnShowBuyOut;
            _categoriesPresenter.Show(_shop.NpcSeller.ItemPrices);
        }

        private void OnShopUpdated()
        {
            _categoriesPresenter.Hide();
            _categoriesPresenter.Show(_shop.NpcSeller.ItemPrices);
        }

        private void OnShowAllGoods() => SetShopItemPresenterFilter(null);

        private void OnShowGroup(ComponentGroupConfig config) => SetShopItemPresenterFilter(config);

        private void SetShopItemPresenterFilter(ComponentGroupConfig config)
        {
            if (!_shopItemsPresenter.IsShown)
            {
                _shopCharacterItemsPresenter?.Hide();
                _shopItemsPresenter.Show(_shop);
            }
            
            _shopItemsPresenter.SetFilter(config);
        }

        private void SetupShopItems()
        {
            _shopCharacterItemsPresenter?.Hide();
            _shopItemsPresenter ??= _shoppingPresentersFactory.CreateShopItemsPresenter(_view.Container);
            _shopItemsPresenter.Show(_shop);
        }

        private void SetupVendorInfo()
        {
            _vendorInfoPresenter ??= _shoppingPresentersFactory.CreateVendorInfoPresenter(_view.Container);
            _vendorInfoPresenter.Show(_shop.NpcSeller);
        }

        private void SetupCharacterItems()
        {
            _characterItemsPresenter ??= _shoppingPresentersFactory.CreateCharacterItemsPresenter(_view.Container);
            _characterItemsPresenter.Show(_shop);
        }

        private void SetupCharacterInfo()
        {
            _characterInfoPresenter ??= _shoppingPresentersFactory.CreateCharacterInfoPresenter(_view.Container);
            _characterInfoPresenter.Show();
        }

        private void OnShowBuyOut()
        {
            _shopItemsPresenter.Hide();
            _shopCharacterItemsPresenter ??= _shoppingPresentersFactory.CreateShopCharacterItemsPresenter(
                _view.Container);
            
            _shopCharacterItemsPresenter.Show(_shop);
        }
    }
}