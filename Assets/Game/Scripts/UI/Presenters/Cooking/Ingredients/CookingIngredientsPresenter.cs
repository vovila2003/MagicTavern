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
        private readonly IActiveDishRecipeReader _recipe;
        private readonly Dictionary<string, ItemCardPresenter> _presenters = new();
        private InfoPresenter _infoPresenter;

        public CookingIngredientsPresenter(IContainerView view,
            IStackableInventory<ProductItem> productInventory,
            IStackableInventory<LootItem> lootInventory,
            PresentersFactory presentersFactory, 
            Transform canvas,
            IActiveDishRecipeReader recipe
            ) : base(view)
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

            _recipe.OnSpent += OnSpendIngredients;
        }

        protected override void OnHide()
        {
            _productInventory.OnItemCountChanged -= OnProductCountChanged;
            _productInventory.OnItemRemoved -= OnItemRemoved;
            
            _lootInventory.OnItemCountChanged -= OnLootCountChanged;
            _lootInventory.OnItemRemoved -= OnItemRemoved;
            
            _recipe.OnSpent -= OnSpendIngredients;

            foreach (ItemCardPresenter presenter in _presenters.Values)
            {
                UnsubscribeItemCard(presenter);
                presenter.Hide();
            }

            _presenters.Clear();
        }

        private void SetupCards()
        {
            AddProductPresenters(_productInventory.Items);
            AddLootPresenters(_lootInventory.Items);
        }

        private void AddLootPresenters(List<LootItem> items)
        {
            foreach (LootItem item in items)
            {
                AddPresenter(item, _lootInventory.GetItemCount(item.ItemName));
            }
        }

        private void AddProductPresenters(List<ProductItem> items)
        {
            foreach (ProductItem item in items)
            {
                AddPresenter(item, _productInventory.GetItemCount(item.ItemName));
            }
        }

        private void AddPresenter(Item item, int itemCount)
        {
            if (itemCount <= 0) return;
            
            if (_presenters.TryGetValue(item.ItemName, out ItemCardPresenter presenter))
            {
                presenter.ChangeCount(itemCount);
                return;
            }
            
            presenter = _presentersFactory.CreateItemCardPresenter(_parent);
            _presenters.Add(item.ItemName, presenter);
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
            
            if (!_presenters.ContainsKey(item.ItemName))
            {
                AddPresenter(item, inventory.GetItemCount(item.ItemName));
            }
            
            _presenters[item.ItemName].ChangeCount(count);
        }

        private void OnItemRemoved(Item item)
        {
            if (!_presenters.Remove(item.ItemName, out ItemCardPresenter presenter)) return;

            UnsubscribeItemCard(presenter);
            presenter.Hide();
        }

        private void OnSpendIngredients(List<ProductItem> products, List<LootItem> loots)
        {
            AddProductPresenters(products);
            AddLootPresenters(loots);
        }

        private void OnIngredientRightClick(Item item)
        {
            Item clone = item.Clone();
            clone.Get<ComponentStackable>().Value = 1;
            OnTryAddItem?.Invoke(clone);
        }

        private void OnIngredientLeftClick(Item item)
        {
            _infoPresenter ??= _presentersFactory.CreateInfoPresenter(_canvas);
            
            if (!_infoPresenter.Show(item, Add)) return;
            
            _infoPresenter.OnAccepted += AddItem;
            _infoPresenter.OnRejected += OnCancelled;
        }

        private void OnCancelled() => UnsubscribeItemInfo();

        private void AddItem(Item item)
        {
            UnsubscribeItemInfo();
            OnIngredientRightClick(item);
        }

        private void UnsubscribeItemInfo()
        {
            _infoPresenter.OnAccepted -= AddItem;
            _infoPresenter.OnRejected -= OnCancelled;
        }

        private void UnsubscribeItemCard(ItemCardPresenter presenter)
        {
            presenter.OnRightClick -= OnIngredientRightClick;
            presenter.OnLeftClick -= OnIngredientLeftClick;
        }
    }
}