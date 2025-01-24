using System;
using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Tavern.Cooking;
using Tavern.Gardening;
using Tavern.Looting;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class CookingIngredientsPresenter : BasePresenter
    {
        private const string Add = "Добавить";
        public event Action<Item> OnTryAddItem;
        
        private readonly Transform _parent;
        private readonly IStackableInventory<ProductItem> _productInventory;
        private readonly IStackableInventory<LootItem> _lootInventory;
        private readonly PresentersFactory _presentersFactory;
        private readonly Transform _canvas;
        private readonly ActiveDishRecipe _recipe;
        private readonly Dictionary<Item, ItemCardPresenter> _presenters = new();
        private ItemInfoPresenter _itemInfoPresenter;

        public CookingIngredientsPresenter(IContainerView view,
            IStackableInventory<ProductItem> productInventory,
            IStackableInventory<LootItem> lootInventory,
            PresentersFactory presentersFactory, 
            Transform canvas,
            ActiveDishRecipe recipe) : base(view)
        {
            _parent = view.ContentTransform;
            _productInventory = productInventory;
            _lootInventory = lootInventory;
            _presentersFactory = presentersFactory;
            _canvas = canvas;
            _recipe = recipe;
        }

        protected override void OnShow()
        {
            SetupCards();
            
            _productInventory.OnItemCountChanged += OnProductCountChanged;
            _productInventory.OnItemRemoved += OnItemRemoved;
            
            _lootInventory.OnItemCountChanged += OnLootCountChanged;
            _lootInventory.OnItemRemoved += OnItemRemoved;
        }

        protected override void OnHide()
        {
            _productInventory.OnItemCountChanged -= OnProductCountChanged;
            _productInventory.OnItemRemoved -= OnItemRemoved;
            
            _lootInventory.OnItemCountChanged -= OnLootCountChanged;
            _lootInventory.OnItemRemoved -= OnItemRemoved;

            foreach (ItemCardPresenter presenter in _presenters.Values)
            {
                UnsubscribeItemCard(presenter);
                presenter.Hide();
            }

            _presenters.Clear();
        }

        private void SetupCards()
        {
            foreach (ProductItem item in _productInventory.Items)
            {
                AddPresenter(item, _productInventory.GetItemCount(item.ItemName));
            }
            
            foreach (LootItem item in _lootInventory.Items)
            {
                AddPresenter(item, _lootInventory.GetItemCount(item.ItemName));
            }
        }

        private void AddPresenter(Item item, int itemCount)
        {
            ItemCardPresenter presenter = _presentersFactory.CreateItemCardPresenter(_parent);
            _presenters.Add(item, presenter);
            presenter.OnRightClick += OnIngredientRightClick;
            presenter.OnLeftClick += OnIngredientLeftClick;
            presenter.Show(item, itemCount);
        }

        private void OnProductCountChanged(Item item, int count) => OnItemCountChanged(item, count, _productInventory);

        private void OnLootCountChanged(Item item, int count) => OnItemCountChanged(item, count, _lootInventory);

        private void OnItemCountChanged<T>(Item item, int count, IStackableInventory<T> inventory) where T : Item
        {
            if (_recipe.HasItem(item.ItemName))
            {
                OnItemRemoved(item);
                return;
            }
            
            if (!_presenters.ContainsKey(item))
            {
                AddPresenter(item, inventory.GetItemCount(item.ItemName));
            }
            
            _presenters[item].ChangeCount(count);
        }

        private void OnItemRemoved(Item item)
        {
            if (!_presenters.Remove(item, out ItemCardPresenter presenter)) return;

            UnsubscribeItemCard(presenter);
            presenter.Hide();
        }

        private void OnIngredientRightClick(Item item)
        {
            Item clone = item.Clone();
            clone.Get<ComponentStackable>().Value = 1;
            OnTryAddItem?.Invoke(clone);
        }

        private void OnIngredientLeftClick(Item item)
        {
            _itemInfoPresenter ??= _presentersFactory.CreateItemInfoPresenter(_canvas);
            
            if (!_itemInfoPresenter.Show(item, Add)) return;
            
            _itemInfoPresenter.OnAccepted += OnAddItem;
            _itemInfoPresenter.OnRejected += OnCancelled;
        }

        private void OnCancelled() => UnsubscribeItemInfo();

        private void OnAddItem(Item item)
        {
            UnsubscribeItemInfo();
            OnIngredientRightClick(item);
        }

        private void UnsubscribeItemInfo()
        {
            _itemInfoPresenter.OnAccepted -= OnAddItem;
            _itemInfoPresenter.OnRejected -= OnCancelled;
        }

        private void UnsubscribeItemCard(ItemCardPresenter presenter)
        {
            presenter.OnRightClick -= OnIngredientRightClick;
            presenter.OnLeftClick -= OnIngredientLeftClick;
        }
    }
}