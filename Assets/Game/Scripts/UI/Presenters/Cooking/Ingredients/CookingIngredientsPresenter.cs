using System;
using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Tavern.Gardening;
using Tavern.Looting;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class CookingIngredientsPresenter : BasePresenter
    {
        public event Action<Item> OnTryAddItem;
        
        private readonly Transform _parent;
        private readonly IInventory<ProductItem> _productInventory;
        private readonly IInventory<LootItem> _lootInventory;
        private readonly PresentersFactory _presentersFactory;
        private readonly Dictionary<Item, ItemCardPresenter> _presenters = new();

        public CookingIngredientsPresenter(
            IContainerView view,
            IInventory<ProductItem> productInventory,
            IInventory<LootItem> lootInventory,
            PresentersFactory presentersFactory
            ) : base(view)
        {
            _parent = view.ContentTransform;
            _productInventory = productInventory;
            _lootInventory = lootInventory;
            _presentersFactory = presentersFactory;
        }

        public void RemoveProduct(ProductItem product)
        {
            _productInventory.RemoveItem(product);
        }

        public void RemoveLoot(LootItem loot)
        {
            _lootInventory.RemoveItem(loot);
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
            
            _lootInventory.OnItemCountChanged += OnLootCountChanged;
            _lootInventory.OnItemRemoved -= OnItemRemoved;

            foreach (ItemCardPresenter presenter in _presenters.Values)
            {
                presenter.OnRightClick -= OnIngredientRightClick;
                presenter.OnLeftClick -= OnIngredientLeftClick;
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

        private void OnProductCountChanged(Item item, int count)
        {
            if (!_presenters.ContainsKey(item))
            {
                AddPresenter(item, _productInventory.GetItemCount(item.ItemName));
            }
            
            _presenters[item].ChangeCount(count);
        }

        private void OnLootCountChanged(Item item, int count)
        {
            if (!_presenters.ContainsKey(item))
            {
                AddPresenter(item, _lootInventory.GetItemCount(item.ItemName));
            }
            
            _presenters[item].ChangeCount(count);
        }

        private void OnItemRemoved(Item item)
        {
            if (!_presenters.Remove(item, out ItemCardPresenter presenter)) return;
            
            presenter.OnRightClick -= OnIngredientRightClick;
            presenter.Hide();
        }

        private void OnIngredientRightClick(Item item)
        {
            OnTryAddItem?.Invoke(item);
        }

        private void OnIngredientLeftClick(Item item)
        {
            //TODO
            Debug.Log($"left click on {item.ItemName}");
        }
    }
}