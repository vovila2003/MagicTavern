using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Tavern.Shopping;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class ShopCharacterItemsPresenter : BasePresenter
    {
        private readonly IContainerView _view;
        private Shop _shop;
        private readonly ShoppingPresentersFactory _shoppingPresentersFactory;
        private readonly CommonPresentersFactory _commonPresentersFactory;
        private readonly Transform _canvas;
        private DealInfoPresenter _dealInfoPresenter;
        private readonly Dictionary<Item, ItemCardPresenter> _presenters = new();
        private IReadOnlyDictionary<Item, int> _itemPrices;

        public ShopCharacterItemsPresenter(
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

        protected override void OnShow()
        {
            SetupCharacterItemCards(_shop.NpcSeller.CharacterItemsPrices);
            _shop.OnNpcCharacterItemsChanged += OnItemsChanged;
        }

        protected override void OnHide()
        {
            ClearItems();
            _shop.OnNpcCharacterItemsChanged -= OnItemsChanged;
        }

        private void SetupCharacterItemCards(IReadOnlyDictionary<Item, int> npcSellerCharacterItemPrices)
        {
            _itemPrices = npcSellerCharacterItemPrices;
            foreach (KeyValuePair<Item, int> pair in npcSellerCharacterItemPrices)
            {
                AddPresenter(pair.Key);
            }
        }

        private void AddPresenter(Item item)
        {
            var count = 1;

            if (item.TryGet(out ComponentStackable componentStackable))
            {
                count = componentStackable.Value;
            }
            
            if (_presenters.TryGetValue(item, out ItemCardPresenter presenter))
            {
                presenter.ChangeCount(count);
                return;
            }

            presenter = _commonPresentersFactory.CreateItemCardPresenter(_view.ContentTransform);
            _presenters.Add(item, presenter);
            presenter.OnRightClick += OnIngredientRightClick;
            presenter.OnLeftClick += OnIngredientLeftClick;
            presenter.Show(item, count, true, _itemPrices[item]);
        }

        private void OnIngredientRightClick(Item item)
        {
            _shop.BuyOut(item);
        }

        private void OnIngredientLeftClick(Item item)
        {
            _dealInfoPresenter ??= _shoppingPresentersFactory.CreateDealInfoPresenter(_canvas);

            int maxCount = item.TryGet(out ComponentStackable componentStackable) ? componentStackable.Value : 1;
            int price = _itemPrices[item];
            
            if (!_dealInfoPresenter.Show(item, maxCount, price)) return;
            
            _dealInfoPresenter.OnAccepted += OnDeal;
            _dealInfoPresenter.OnRejected += OnCancelled;
        }

        private void OnDeal(Item item, int count)
        {
            UnsubscribeDealInfo();
            if (count <= 0) return;
            
            _shop.BuyOut(item, count);
        }

        private void UnsubscribeItemCard(ItemCardPresenter presenter)
        {
            presenter.OnRightClick -= OnIngredientRightClick;
            presenter.OnLeftClick -= OnIngredientLeftClick;
        }

        private void ClearItems()
        {
            foreach (ItemCardPresenter presenter in _presenters.Values)
            {
                UnsubscribeItemCard(presenter);
                presenter.Hide();
            }

            _presenters.Clear();
        }

        private void OnCancelled() => UnsubscribeDealInfo();

        private void UnsubscribeDealInfo()
        {
            _dealInfoPresenter.OnAccepted -= OnDeal;
            _dealInfoPresenter.OnRejected -= OnCancelled;
        }

        private void OnItemsChanged()
        {
            ClearItems();
            SetupCharacterItemCards(_shop.NpcSeller.CharacterItemsPrices);
        }
    }
}