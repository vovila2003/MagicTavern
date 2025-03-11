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
        private readonly IInventory<PlantProductItem> _plantProductInventory;
        private readonly IInventory<AnimalProductItem> _animalProductInventory;
        private readonly CommonPresentersFactory _commonPresentersFactory;
        private readonly Transform _canvas;
        private readonly IActiveDishRecipeReader _recipe;
        private readonly bool _enableMatching;
        private readonly Dictionary<string, ItemCardPresenter> _plantPresenters = new();
        private readonly Dictionary<string, ItemCardPresenter> _animalPresenters = new();
        private InfoPresenter _infoPresenter;

        public CookingIngredientsPresenter(IContainerView view,
            IInventory<PlantProductItem> plantProductInventory,
            IInventory<AnimalProductItem> animalProductInventory,
            CommonPresentersFactory commonPresentersFactory, 
            Transform canvas,
            IActiveDishRecipeReader recipe,
            bool enableMatching
            ) : base(view)
        {
            _parent = view.ContentTransform;
            _plantProductInventory = plantProductInventory;
            _animalProductInventory = animalProductInventory;
            _commonPresentersFactory = commonPresentersFactory;
            _canvas = canvas;
            _recipe = recipe;
            _enableMatching = enableMatching;
        }

        protected override void OnShow()
        {
            SetupCards();
            
            _plantProductInventory.OnItemCountChanged += OnPlantProductCountChanged;
            _plantProductInventory.OnItemAdded += OnPlantItemAdded;
            _plantProductInventory.OnItemRemoved += OnPlantItemRemoved;
            
            _animalProductInventory.OnItemCountChanged += AnimalProductCountChanged;
            _animalProductInventory.OnItemAdded += OnAnimalItemAdded;
            _animalProductInventory.OnItemRemoved += OnAnimalItemRemoved;

            _recipe.OnSpent += OnSpendIngredients;
        }

        protected override void OnHide()
        {
            _plantProductInventory.OnItemCountChanged -= OnPlantProductCountChanged;
            _plantProductInventory.OnItemAdded -= OnPlantItemAdded;
            _plantProductInventory.OnItemRemoved -= OnPlantItemRemoved;
            
            _animalProductInventory.OnItemCountChanged -= AnimalProductCountChanged;
            _animalProductInventory.OnItemAdded -= OnAnimalItemAdded;
            _animalProductInventory.OnItemRemoved -= OnAnimalItemRemoved;
            
            _recipe.OnSpent -= OnSpendIngredients;

            ClearCards(_plantPresenters);
            ClearCards(_animalPresenters);
        }

        private void SetupCards()
        {
            AddPlantProductPresenters(_plantProductInventory.Items);
            AddAnimalProductsPresenters(_animalProductInventory.Items);
        }

        private void ClearCards(Dictionary<string, ItemCardPresenter> presenters)
        {
            foreach (ItemCardPresenter presenter in presenters.Values)
            {
                UnsubscribeItemCard(presenter);
                presenter.Hide();
            }

            presenters.Clear();
        }

        private void AddAnimalProductsPresenters(List<AnimalProductItem> items)
        {
            foreach (AnimalProductItem item in items)
            {
                AddPresenter(item, _animalProductInventory.GetItemCount(item.ItemName), _animalPresenters);
            }
        }

        private void AddPlantProductPresenters(List<PlantProductItem> items)
        {
            foreach (PlantProductItem item in items)
            {
                AddPresenter(item, _plantProductInventory.GetItemCount(item.ItemName), _plantPresenters);
            }
        }

        private void AddPresenter(Item item, int itemCount, Dictionary<string, ItemCardPresenter> presenters)
        {
            if (itemCount <= 0) return;
            
            if (presenters.TryGetValue(item.ItemName, out ItemCardPresenter presenter))
            {
                presenter.ChangeCount(itemCount);
                return;
            }
            
            presenter = _commonPresentersFactory.CreateItemCardPresenter(_parent);
            presenters.Add(item.ItemName, presenter);
            if (_enableMatching)
            {
                presenter.OnRightClick += OnIngredientRightClick;
                presenter.OnLeftClick += OnIngredientLeftClick;
            }
            presenter.Show(item, itemCount);
        }

        private void OnPlantProductCountChanged(Item item, int count)
        {
            if (_recipe.HasItem(item.ItemName))
            {
                OnPlantItemRemoved(item, _plantProductInventory);
                return;
            }

            if (!_plantPresenters.ContainsKey(item.ItemName))
            {
                AddPresenter(item, _plantProductInventory.GetItemCount(item.ItemName), _plantPresenters);
            }

            _plantPresenters[item.ItemName].ChangeCount(count);
        }

        private void AnimalProductCountChanged(Item item, int count)
        {
            if (_recipe.HasItem(item.ItemName))
            {
                OnAnimalItemRemoved(item, _animalProductInventory);
                return;
            }

            if (!_animalPresenters.ContainsKey(item.ItemName))
            {
                AddPresenter(item, _animalProductInventory.GetItemCount(item.ItemName), _animalPresenters);
            }

            _animalPresenters[item.ItemName].ChangeCount(count);
        }
        
        private void OnPlantItemAdded(Item item, IInventoryBase inventory)
        {
            if (!_plantPresenters.ContainsKey(item.ItemName))
            {
                AddPresenter(item, _plantProductInventory.GetItemCount(item.ItemName), _plantPresenters);
            }        
        }
        
        private void OnAnimalItemAdded(Item item, IInventoryBase inventory)
        {
            if (!_animalPresenters.ContainsKey(item.ItemName))
            {
                AddPresenter(item, _animalProductInventory.GetItemCount(item.ItemName), _animalPresenters);
            }
        }

        private void OnPlantItemRemoved(Item item, IInventoryBase _)
        {
            if (!_plantPresenters.Remove(item.ItemName, out ItemCardPresenter presenter)) return;

            UnsubscribeItemCard(presenter);
            presenter.Hide();
        }
        
        private void OnAnimalItemRemoved(Item item, IInventoryBase _)
        {
            if (!_animalPresenters.Remove(item.ItemName, out ItemCardPresenter presenter)) return;

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
            if (clone.TryGet(out ComponentStackable component))
            {
                component.Value = 1;
            }
            
            OnTryAddItem?.Invoke(clone);
        }

        private void OnIngredientLeftClick(Item item)
        {
            _infoPresenter ??= _commonPresentersFactory.CreateInfoPresenter(_canvas);
            
            if (!_infoPresenter.Show(item, InfoPresenter.Mode.Dialog, Add)) return;
            
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
            if (!_enableMatching) return;
            
            presenter.OnRightClick -= OnIngredientRightClick;
            presenter.OnLeftClick -= OnIngredientLeftClick;
        }
    }
}