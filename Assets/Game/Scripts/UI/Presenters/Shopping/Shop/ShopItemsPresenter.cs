using System.Collections.Generic;
using Modules.Items;
using Modules.Shopping;
using Tavern.Shopping;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class ShopItemsPresenter : BasePresenter
    {
        private readonly IContainerView _view;
        private Shop _shop;
        private readonly ShoppingPresentersFactory _factory;
        private readonly Transform _canvas;
        private readonly Dictionary<string, ItemConfigCardPresenter> _presenters = new();
        private ComponentGroupConfig _filter;
        private DealInfoPresenter _dealInfoPresenter;
        private readonly Dictionary<ItemConfig, int> _itemCount = new();

        public ShopItemsPresenter(
            IContainerView view, 
            ShoppingPresentersFactory factory,
            Transform canvas
            ) : base(view)
        {
            _view = view;
            _factory = factory;
            _canvas = canvas;
        }

        public void Show(Shop shop)
        {
            _shop = shop;
            Show();
        }

        public void SetFilter(ComponentGroupConfig filter)
        {
            _filter = filter;
            ClearItems();
            SetupCards(GetItemsByFilter(filter));
        }

        protected override void OnShow()
        {
            SetupCards(_shop.NpcSeller.ItemPrices);

            _shop.OnUpdated += OnShopUpdated;
            _shop.OnNpcSellerItemsChanged += OnItemsChanged;
        }

        protected override void OnHide()
        {
            ClearItems();

            _shop.OnUpdated -= OnShopUpdated;
            _shop.OnNpcSellerItemsChanged -= OnItemsChanged;
        }

        private IReadOnlyCollection<ItemInfoByConfig> GetItemsByFilter(ComponentGroupConfig filter)
        {
            if (filter is null) return _shop.NpcSeller.ItemPrices;
            
            var items = new List<ItemInfoByConfig>();
            foreach (ItemInfoByConfig info in _shop.NpcSeller.ItemPrices)
            {
                if (!info.Item.TryGet(out ComponentGroup componentGroup)) continue;
                
                if (componentGroup.Config != filter) continue;
                
                items.Add(info);
            }

            return items;
        }

        private void SetupCards(IReadOnlyCollection<ItemInfoByConfig> items)
        {
            foreach (ItemInfoByConfig itemInfo in items)
            {
                AddPresenter(itemInfo.Item, itemInfo.Count);
                _itemCount.Add(itemInfo.Item, itemInfo.Count);
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
            _shop.BuyByConfig(config);
        }

        private void OnIngredientLeftClick(ItemConfig config)
        {
            _dealInfoPresenter ??= _factory.CreateDealInfoPresenter(_canvas);
            
            if (!_dealInfoPresenter.Show(config, _itemCount[config])) return;
            
            _dealInfoPresenter.OnConfigAccepted += OnDeal;
            _dealInfoPresenter.OnRejected += OnCancelled;
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
            _itemCount.Clear();
        }

        private void OnItemsChanged()
        {
            SetFilter(_filter);
        }

        private void OnDeal(ItemConfig config, int count)
        {
            UnsubscribeDealInfo();
            _shop.BuyByConfig(config, count);
        }

        private void OnCancelled() => UnsubscribeDealInfo();

        private void UnsubscribeDealInfo()
        {
            _dealInfoPresenter.OnConfigAccepted -= OnDeal;
            _dealInfoPresenter.OnRejected -= OnCancelled;
        }
    }
}