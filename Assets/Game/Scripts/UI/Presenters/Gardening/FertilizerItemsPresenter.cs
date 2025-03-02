using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Tavern.Gardening.Fertilizer;
using Tavern.Utils;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class FertilizerItemsPresenter : BasePresenter
    {
        private readonly IContainerView _view;
        private readonly CommonPresentersFactory _commonPresentersFactory;
        private readonly FertilizerInventoryContext _fertilizerInventoryContext;
        private readonly Dictionary<Item, ItemCardPresenter> _presenters = new();

        public FertilizerItemsPresenter(
            IContainerView view,
            CommonPresentersFactory commonPresentersFactory,
            FertilizerInventoryContext fertilizerInventoryContext
            ) : base(view)
        {
            _view = view;
            _commonPresentersFactory = commonPresentersFactory;
            _fertilizerInventoryContext = fertilizerInventoryContext;
        }
        
        public void SetActive(bool active)
        {
            foreach (ItemCardPresenter presenter in _presenters.Values)
            {
                presenter.SetActive(active);
            }
        }

        protected override void OnShow()
        {
            SetupCards();
            
            _fertilizerInventoryContext.Inventory.OnItemAdded += OnFertilizerChanged;
            _fertilizerInventoryContext.Inventory.OnItemRemoved += OnFertilizerChanged;
            _fertilizerInventoryContext.Inventory.OnItemCountChanged += OnFertilizerCountChanged;
        }
        
        protected override void OnHide()
        {
            ClearItems();
            
            _fertilizerInventoryContext.Inventory.OnItemAdded -= OnFertilizerChanged;
            _fertilizerInventoryContext.Inventory.OnItemRemoved -= OnFertilizerChanged;
            _fertilizerInventoryContext.Inventory.OnItemCountChanged -= OnFertilizerCountChanged;
        }
        
        private void SetupCards()
        {
            foreach (FertilizerItem item in _fertilizerInventoryContext.Inventory.Items)
            {
                AddPresenter(item, item.GetCount());
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
            presenter.OnRightClick += OnFertilizerRightClick;
            presenter.OnLeftClick += OnFertilizerLeftClick;
            presenter.Show(item, itemCount);
        }
        
        private void OnFertilizerLeftClick(Item item)
        {
            Debug.Log($"Left click to {item.ItemName}");
        }
        
        private void OnFertilizerRightClick(Item item)
        {
            Debug.Log($"Right click to {item.ItemName}");
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
        
        private void UnsubscribeItemCard(ItemCardPresenter presenter)
        {
            presenter.OnRightClick -= OnFertilizerRightClick;
            presenter.OnLeftClick -= OnFertilizerLeftClick;
        }
        
        private void OnFertilizerChanged(Item item, IInventoryBase inventoryBase)
        {
            ClearItems();
            SetupCards();
        }
        
        private void OnFertilizerCountChanged(Item item, int count)
        {
            if (!_presenters.TryGetValue(item, out ItemCardPresenter presenter)) return;
            
            presenter.ChangeCount(count);
        }
    }
}