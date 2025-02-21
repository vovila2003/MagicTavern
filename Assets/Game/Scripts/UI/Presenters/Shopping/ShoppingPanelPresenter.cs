using System;
using System.Collections.Generic;
using Modules.Shopping;
using Tavern.Shopping;

namespace Tavern.UI.Presenters
{
    public class ShoppingPanelPresenter : BasePresenter
    {
        private const string Title = "Торговля";

        private readonly IPanelView _view;
        private readonly ShoppingPresentersFactory _factory;
        private CategoriesPresenter _categoriesPresenter;
        private ShopItemsPresenter _shopItemsPresenter;
        private VendorInfoPresenter _vendorInfoPresenter; 

        private Action _onExit;
        private Shop _shop;

        public ShoppingPanelPresenter(
            IPanelView view,
            ShoppingPresentersFactory factory
            ) : base(view)
        {
            _view = view;
            _factory = factory;
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

            _shop.OnUpdated += OnShopUpdated;
        }

        protected override void OnHide()
        {
            _view.OnCloseClicked -= Hide;

            _categoriesPresenter.OnShowAllGoods -= OnShowAllGoods;
            _categoriesPresenter.OnShowGroup -= OnShowGroup;

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
            _categoriesPresenter ??= _factory.CreateCategoriesPresenter(_view.Container);
            
            _categoriesPresenter.OnShowAllGoods += OnShowAllGoods;
            _categoriesPresenter.OnShowGroup += OnShowGroup;
            _categoriesPresenter.Show(_shop.NpcSeller.ItemPrices);
        }

        private void OnShopUpdated()
        {
            _categoriesPresenter.Hide();
            _categoriesPresenter.Show(_shop.NpcSeller.ItemPrices);
        }

        private void ShowGoods(IReadOnlyCollection<ItemInfoByConfig> items)
        {
            _shopItemsPresenter.SetItems(items);
        }

        private void OnShowAllGoods()
        {
            ShowGoods(_shop.NpcSeller.ItemPrices);
        }

        private void OnShowGroup(ComponentGroupConfig config)
        {
            var items = new List<ItemInfoByConfig>();
            foreach (ItemInfoByConfig info in _shop.NpcSeller.ItemPrices)
            {
                if (!info.Item.TryGet(out ComponentGroup componentGroup)) continue;
                
                if (componentGroup.Config != config) continue;
                
                items.Add(info);
            }
            
            ShowGoods(items);
        }

        private void SetupShopItems()
        {
            _shopItemsPresenter ??= _factory.CreateShopItemsPresenter(_view.Container);
            
            _shopItemsPresenter.Show(_shop);
        }

        private void SetupVendorInfo()
        {
            _vendorInfoPresenter ??= _factory.CreateVendorInfoPresenter(_view.Container);
            
            _vendorInfoPresenter.Show(_shop.NpcSeller);
        }
    }
}