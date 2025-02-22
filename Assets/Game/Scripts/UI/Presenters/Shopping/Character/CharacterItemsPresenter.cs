using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Tavern.Shopping;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class CharacterItemsPresenter : BasePresenter
    {
        private readonly IContainerView _view;
        private readonly CommonPresentersFactory _commonPresentersFactory;
        private readonly CharacterSeller _characterSeller;
        private Shop _shop;
        private readonly Dictionary<Item, ItemCardPresenter> _presenters = new();

        public CharacterItemsPresenter(
            IContainerView view,
            CommonPresentersFactory commonPresentersFactory,
            CharacterSeller characterSeller
        ) : base(view)
        {
            _view = view;
            _commonPresentersFactory = commonPresentersFactory;
            _characterSeller = characterSeller;
        }

        public void Show(Shop shop)
        {
            _shop = shop;
            Show();
        }

        protected override void OnShow()
        {
            SetupCards();
            _characterSeller.OnSellableItemsChanged += OnItemsChanged;
        }

        protected override void OnHide()
        {
            ClearItems();
            _characterSeller.OnSellableItemsChanged -= OnItemsChanged;
        }

        private void SetupCards()
        {
            foreach (Item item in _characterSeller.SellableItems.Keys)
            {
                AddPresenter(item, GetCount(item));
            }
        }

        private void AddPresenter(Item item, int itemCount)
        {
            if (itemCount <= 0) return;
            
            if (_presenters.TryGetValue(item, out ItemCardPresenter presenter))
            {
                presenter.ChangeCount(itemCount);
                return;
            }
            
            presenter = _commonPresentersFactory.CreateItemCardPresenter(_view.ContentTransform);
            _presenters.Add(item, presenter);
            presenter.OnRightClick += OnIngredientRightClick;
            presenter.OnLeftClick += OnIngredientLeftClick;
            presenter.Show(item, itemCount);
        }

        private void OnIngredientRightClick(Item item)
        {
            //Debug.Log($"{item.ItemName} right button clicked");
            _shop.Sell(item);
        }

        private void OnIngredientLeftClick(Item item)
        {
            Debug.Log($"{item.ItemName} left button clicked");
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

        private int GetCount(Item item) => 
            !item.TryGet(out ComponentStackable componentStackable) ? 1 : componentStackable.Value;

        private void OnItemsChanged()
        {
            ClearItems();
            SetupCards();
        }
    }
}