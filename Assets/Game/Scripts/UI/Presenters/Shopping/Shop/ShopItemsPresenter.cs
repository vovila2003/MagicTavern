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
        private readonly ShoppingPresentersFactory _shoppingPresentersFactory;
        private readonly CommonPresentersFactory _commonPresentersFactory;
        private readonly Transform _canvas;
        private readonly Dictionary<string, ItemConfigCardPresenter> _presenters = new();
        private ComponentGroupConfig _filter;
        private DealInfoPresenter _dealInfoPresenter;
        private readonly Dictionary<ItemConfig, ItemInfoByConfig> _itemInfos = new();

        public ShopItemsPresenter(
            IContainerView view, 
            ShoppingPresentersFactory shoppingPresentersFactory,
            CommonPresentersFactory commonPresentersFactory,
            Transform canvas) : base(view)
        {
            _view = view;
            _shoppingPresentersFactory = shoppingPresentersFactory;
            _canvas = canvas;
            _commonPresentersFactory = commonPresentersFactory;
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
                AddPresenter(itemInfo);
                _itemInfos.Add(itemInfo.Item, itemInfo);
            }
        }

        private void AddPresenter(ItemInfoByConfig itemInfo)
        {
            int count = itemInfo.Count;
            string name = itemInfo.Item.Name;
            if (count <= 0) return;
            
            if (_presenters.TryGetValue(name, out ItemConfigCardPresenter presenter))
            {
                presenter.ChangeCount(count);
                return;
            }
            
            presenter = _shoppingPresentersFactory.CreateItemConfigCardPresenter(_view.ContentTransform);
            _presenters.Add(name, presenter);
            presenter.OnRightClick += OnIngredientRightClick;
            presenter.OnLeftClick += OnIngredientLeftClick;
            presenter.Show(itemInfo.Item, count, itemInfo.Price);
        }

        private void OnIngredientRightClick(ItemConfig config)
        {
            _shop.BuyByConfig(config);
        }

        private void OnIngredientLeftClick(ItemConfig config)
        {
            _dealInfoPresenter ??= _shoppingPresentersFactory.CreateDealInfoPresenter(_canvas);

            if (!_dealInfoPresenter.Show(_itemInfos[config])) return;
            
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
            _itemInfos.Clear();
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