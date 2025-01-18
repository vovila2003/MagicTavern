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

        protected override void OnShow()
        {
            SetupCards();
            
            _productInventory.OnItemAdded += OnProductAdded;
            _productInventory.OnItemRemoved += OnItemRemoved;
            
            _lootInventory.OnItemAdded += OnLootAdded;
            _lootInventory.OnItemRemoved += OnItemRemoved;
            
        }

        protected override void OnHide()
        {
            _productInventory.OnItemAdded -= OnProductAdded;
            _productInventory.OnItemRemoved -= OnItemRemoved;
            
            _lootInventory.OnItemAdded -= OnLootAdded;
            _lootInventory.OnItemRemoved -= OnItemRemoved;


            foreach (ItemCardPresenter presenter in _presenters.Values)
            {
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
            presenter.Show(item, itemCount);
        }

        private void OnProductAdded(Item item)
        {
            AddPresenter(item, _productInventory.GetItemCount(item.ItemName));
        }

        private void OnLootAdded(LootItem item)
        {
            AddPresenter(item, _lootInventory.GetItemCount(item.ItemName));
        }

        private void OnItemRemoved(Item item)
        {
            if (_presenters.Remove(item, out ItemCardPresenter presenter))
            {
                presenter.Hide();
            }
        }
    }
}