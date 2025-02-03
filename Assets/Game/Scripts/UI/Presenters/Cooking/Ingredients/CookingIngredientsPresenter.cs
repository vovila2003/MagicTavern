using System;
using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Tavern.Cooking;
using Tavern.ProductsAndIngredients;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class CookingIngredientsPresenter : BasePresenter
    {
        private const string Add = "Добавить";
        public event Action<Item> OnTryAddItem;
        
        private readonly Transform _parent;
        private readonly IStackableInventory<PlantProductItem> _plantProductInventory;
        private readonly IStackableInventory<AnimalProductItem> _animalProductsInventory;
        private readonly CookingPresentersFactory _cookingPresentersFactory;
        private readonly Transform _canvas;
        private readonly IActiveDishRecipeReader _recipe;
        private readonly Dictionary<string, ItemCardPresenter> _presenters = new();
        private InfoPresenter _infoPresenter;

        public CookingIngredientsPresenter(IContainerView view,
            IStackableInventory<PlantProductItem> plantProductInventory,
            IStackableInventory<AnimalProductItem> animalProductsInventory,
            CookingPresentersFactory cookingPresentersFactory, 
            Transform canvas,
            IActiveDishRecipeReader recipe
            ) : base(view)
        {
            _parent = view.ContentTransform;
            _plantProductInventory = plantProductInventory;
            _animalProductsInventory = animalProductsInventory;
            _cookingPresentersFactory = cookingPresentersFactory;
            _canvas = canvas;
            _recipe = recipe;
        }

        protected override void OnShow()
        {
            SetupCards();
            
            _plantProductInventory.OnItemCountChanged += OnPlantProductCountChanged;
            _plantProductInventory.OnItemRemoved += OnItemRemoved;
            
            _animalProductsInventory.OnItemCountChanged += AnimalProductsCountChanged;
            _animalProductsInventory.OnItemRemoved += OnItemRemoved;

            _recipe.OnSpent += OnSpendIngredients;
        }

        protected override void OnHide()
        {
            _plantProductInventory.OnItemCountChanged -= OnPlantProductCountChanged;
            _plantProductInventory.OnItemRemoved -= OnItemRemoved;
            
            _animalProductsInventory.OnItemCountChanged -= AnimalProductsCountChanged;
            _animalProductsInventory.OnItemRemoved -= OnItemRemoved;
            
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
            AddPlantProductPresenters(_plantProductInventory.Items);
            AddAnimalProductsPresenters(_animalProductsInventory.Items);
        }

        private void AddAnimalProductsPresenters(List<AnimalProductItem> items)
        {
            foreach (AnimalProductItem item in items)
            {
                AddPresenter(item, _animalProductsInventory.GetItemCount(item.ItemName));
            }
        }

        private void AddPlantProductPresenters(List<PlantProductItem> items)
        {
            foreach (PlantProductItem item in items)
            {
                AddPresenter(item, _plantProductInventory.GetItemCount(item.ItemName));
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
            
            presenter = _cookingPresentersFactory.CreateItemCardPresenter(_parent);
            _presenters.Add(item.ItemName, presenter);
            presenter.OnRightClick += OnIngredientRightClick;
            presenter.OnLeftClick += OnIngredientLeftClick;
            presenter.Show(item, itemCount);
        }

        private void OnPlantProductCountChanged(Item item, int count) => 
            OnItemCountChanged(item, count, _plantProductInventory);

        private void AnimalProductsCountChanged(Item item, int count) => 
            OnItemCountChanged(item, count, _animalProductsInventory);

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

        private void OnSpendIngredients(List<PlantProductItem> plantProducts, List<AnimalProductItem> animalProducts)
        {
            AddPlantProductPresenters(plantProducts);
            AddAnimalProductsPresenters(animalProducts);
        }

        private void OnIngredientRightClick(Item item)
        {
            Item clone = item.Clone();
            clone.Get<ComponentStackable>().Value = 1;
            OnTryAddItem?.Invoke(clone);
        }

        private void OnIngredientLeftClick(Item item)
        {
            _infoPresenter ??= _cookingPresentersFactory.CreateInfoPresenter(_canvas);
            
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