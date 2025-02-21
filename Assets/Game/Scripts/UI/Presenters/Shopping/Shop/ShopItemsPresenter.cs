using System.Collections.Generic;
using Modules.Items;
using Tavern.Shopping;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class ShopItemsPresenter : BasePresenter
    {
        private readonly IContainerView _view;
        private Shop _shop;
        private readonly ShoppingPresentersFactory _factory;
        private readonly Dictionary<string, ItemConfigCardPresenter> _presenters = new();

        public ShopItemsPresenter(
            IContainerView view, 
            ShoppingPresentersFactory factory
            ) : base(view)
        {
            _view = view;
            _factory = factory;
        }

        public void Show(Shop shop)
        {
            _shop = shop;
            Show();
        }

        public void SetItems(IReadOnlyCollection<ItemInfoByConfig> items)
        {
            ClearItems();
            SetupCards(items);
        }

        protected override void OnShow()
        {
            SetupCards(_shop.NpcSeller.ItemPrices);

            _shop.OnUpdated += OnShopUpdated;
        }

        protected override void OnHide()
        {
            ClearItems();

            _shop.OnUpdated -= OnShopUpdated;
        }

        private void SetupCards(IReadOnlyCollection<ItemInfoByConfig> items)
        {
            foreach (ItemInfoByConfig itemInfo in items)
            {
                AddPresenter(itemInfo.Item, itemInfo.Count);
            }
        }

        private void AddPresenter(ItemConfig itemConfig, int itemCount)
        {
            if (itemCount <= 0) return;
            
            if (_presenters.TryGetValue(itemConfig.Name, out ItemConfigCardPresenter presenter))
            {
                presenter.ChangeCount(itemCount);
                return;
            }
            
            presenter = _factory.CreateItemConfigCardPresenter(_view.ContentTransform);
            _presenters.Add(itemConfig.Name, presenter);
            presenter.OnRightClick += OnIngredientRightClick;
            presenter.OnLeftClick += OnIngredientLeftClick;
            presenter.Show(itemConfig, itemCount);
        }

        private void OnIngredientRightClick(ItemConfig config)
        {
            Debug.Log($"{config.Name} right button clicked");
        }

        private void OnIngredientLeftClick(ItemConfig config)
        {
            Debug.Log($"{config.Name} left button clicked");
        }

        private void UnsubscribeItemCard(ItemConfigCardPresenter presenter)
        {
            presenter.OnRightClick -= OnIngredientRightClick;
            presenter.OnLeftClick -= OnIngredientLeftClick;
        }

        private void OnShopUpdated()
        {
            ClearItems();
            SetupCards(_shop.NpcSeller.ItemPrices);
        }

        private void ClearItems()
        {
            foreach (ItemConfigCardPresenter presenter in _presenters.Values)
            {
                UnsubscribeItemCard(presenter);
                presenter.Hide();
            }

            _presenters.Clear();
        }
    }
}