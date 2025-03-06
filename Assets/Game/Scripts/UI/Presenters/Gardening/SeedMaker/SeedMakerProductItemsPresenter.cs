using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Tavern.Gardening;
using Tavern.ProductsAndIngredients;
using Tavern.Utils;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class SeedMakerProductItemsPresenter : BasePresenter
    {
        private readonly IContainerView _view;
        private readonly CommonPresentersFactory _commonPresentersFactory;
        private readonly GardeningPresentersFactory _gardeningPresentersFactory;
        private readonly IInventory<PlantProductItem> _inventory;
        private readonly SeedMaker _seedMaker;
        private readonly Transform _canvas;
        private readonly Dictionary<Item, ItemCardPresenter> _presenters = new();

        private ConvertInfoPresenter _infoPresenter;

        public SeedMakerProductItemsPresenter(
            IContainerView view,
            CommonPresentersFactory commonPresentersFactory,
            GardeningPresentersFactory gardeningPresentersFactory,
            IInventory<PlantProductItem> inventory,
            SeedMaker seedMaker,
            Transform canvas
            ) : base(view)
        {
            _view = view;
            _commonPresentersFactory = commonPresentersFactory;
            _gardeningPresentersFactory = gardeningPresentersFactory;
            _inventory = inventory;
            _seedMaker = seedMaker;
            _canvas = canvas;
        }

        protected override void OnShow()
        {
            SetupCards();
            
            _inventory.OnItemAdded += Changed;
            _inventory.OnItemRemoved += Changed;
            _inventory.OnItemCountChanged += CountChanged;
        }

        protected override void OnHide()
        {
            ClearItems();
            
            _inventory.OnItemAdded -= Changed;
            _inventory.OnItemRemoved -= Changed;
            _inventory.OnItemCountChanged -= CountChanged;
        }

        private void SetupCards()
        {
            foreach (PlantProductItem item in _inventory.Items)
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
            presenter.OnRightClick += OnRightClick;
            presenter.OnLeftClick += OnLeftClick;
            presenter.Show(item, itemCount);
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
            presenter.OnRightClick -= OnRightClick;
            presenter.OnLeftClick -= OnLeftClick;
        }

        private void Changed(Item item, IInventoryBase inventory)
        {
            ClearItems();
            SetupCards();
        }

        private void CountChanged(Item item, int count)
        {
            if (!_presenters.TryGetValue(item, out ItemCardPresenter presenter)) return;
            
            presenter.ChangeCount(count);
        }

        private void OnRightClick(Item item)
        {
            if (item is not PlantProductItem plantProductItem) return;
            
            _seedMaker.MakeSeeds(plantProductItem);
        }

        private void OnLeftClick(Item item)
        {
            if (item.Config is not PlantProductItemConfig plantItemConfig) return;
            
            _infoPresenter ??= _gardeningPresentersFactory.CreateConvertInfoPresenter(_canvas);
            if (!_infoPresenter.Show(item, item.GetCount(), plantItemConfig.ProductToSeedRatio)) return;
            
            _infoPresenter.OnAccepted += OnAction;
            _infoPresenter.OnRejected += OnCancelled;
        }

        private void OnAction(Item item, int count)
        {
            UnsubscribeDealInfo();
            if (item is not PlantProductItem plantProductItem) return;
            
            _seedMaker.MakeSeeds(plantProductItem, count);
        }

        private void OnCancelled()
        {
            UnsubscribeDealInfo();
        }
        
        private void UnsubscribeDealInfo()
        {
            _infoPresenter.OnAccepted -= OnAction;
            _infoPresenter.OnRejected -= OnCancelled;
        }
    }
}