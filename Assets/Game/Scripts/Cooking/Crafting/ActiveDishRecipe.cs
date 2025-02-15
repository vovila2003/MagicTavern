using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Inventories;
using Modules.Items;
using Tavern.ProductsAndIngredients;
using UnityEngine;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public sealed class ActiveDishRecipe : IActiveDishRecipeReader
    {
        private const int MinIngredientsCount = 3;
        private const int MaxIngredientsCount = 7;

        public event Action OnChanged;
        public event Action<List<PlantProductItem>, List<AnimalProductItem>> OnSpent;
        public event Action<bool> OnCanCraftStateChanged;
        
        private readonly IStackableInventory<PlantProductItem> _plantProductInventory;
        private readonly IStackableInventory<AnimalProductItem> _animalProductInventory;

        private readonly HashSet<PlantProductItem> _plantProducts = new();
        private readonly HashSet<PlantProductItem> _fakePlantProducts = new();
        private readonly HashSet<AnimalProductItem> _animalProducts = new();
        private readonly HashSet<AnimalProductItem> _fakeAnimalProducts = new();
        private readonly HashSet<string> _items = new();
        private readonly List<PlantProductItem> _spentPlantProducts = new();
        private readonly List<AnimalProductItem> _spentAnimalProducts = new();


        public DishRecipe Recipe { get; private set; }
        public bool IsEmpty => Recipe == null;

        public IReadOnlyCollection<Item> PlantProducts => _plantProducts;

        public IReadOnlyCollection<Item> FakePlantProducts => _fakePlantProducts;

        public IReadOnlyCollection<Item> AnimalProducts => _animalProducts;

        public IReadOnlyCollection<Item> FakeAnimalProducts => _fakeAnimalProducts;

        public KitchenItemConfig RequiredKitchen { get; private set; }
        private bool CanAddIngredient => _plantProducts.Count + _animalProducts.Count < MaxIngredientsCount;

        public ActiveDishRecipe(
            IStackableInventory<PlantProductItem> plantProductInventory,
            IStackableInventory<AnimalProductItem> animalProductInventory)
        {
            _plantProductInventory = plantProductInventory;
            _animalProductInventory = animalProductInventory;
        }

        public void SetKitchen(KitchenItemConfig kitchenItem)
        {
            if (kitchenItem is null)
            {
                Debug.LogError("Empty required kitchen items. When cooking, only slop is obtained(");
                return;
            }
            
            RequiredKitchen = kitchenItem;
        }
        
        public bool HasItem(string item) => _items.Contains(item);

        public void AddPlantProduct(PlantProductItem product) => 
            AddItem(product, _plantProductInventory, _plantProducts);

        public void AddAnimalProduct(AnimalProductItem product) => 
            AddItem(product, _animalProductInventory, _animalProducts);

        public void RemovePlantProduct(PlantProductItem plantProduct) => 
            RemoveItem(plantProduct, _plantProductInventory, _plantProducts, _fakePlantProducts);

        public void RemoveAnimalProduct(AnimalProductItem loot) => 
            RemoveItem(loot, _animalProductInventory, _animalProducts, _fakeAnimalProducts);

        public void Setup(DishRecipe recipe)
        {
            ResetRecipe();
            Recipe = recipe;
            GetPlantProducts(Recipe);
            GetAnimalProducts(Recipe);
            CheckCanCraft();
            
            OnChanged?.Invoke();
        }

        public void Reset()
        {
            ResetRecipe();
            CheckCanCraft();

            OnChanged?.Invoke();
        }

        public (List<PlantProductItem>, List<AnimalProductItem>) SpendIngredients()
        {
            _fakePlantProducts.Clear();
            _fakeAnimalProducts.Clear();
            _items.Clear();
            Recipe = null;
            
            _spentPlantProducts.Clear();
            _spentPlantProducts.AddRange(_plantProducts);
            
            _spentAnimalProducts.Clear();
            _spentAnimalProducts.AddRange(_animalProducts);
            
            _plantProducts.Clear();
            _animalProducts.Clear();
            CheckCanCraft();

            OnSpent?.Invoke(_spentPlantProducts, _spentAnimalProducts);
            OnChanged?.Invoke();
            
            return (_spentPlantProducts, _spentAnimalProducts);
        }

        private void ResetRecipe()
        {
            _fakePlantProducts.Clear();
            _fakeAnimalProducts.Clear();
            _items.Clear();
            Recipe = null;
            ReturnPlantProducts();
            ReturnAnimalProducts();
        }

        private void AddItem<T>(T item, IStackableInventory<T> inventory, HashSet<T> collection) where T : Item
        {
            if (!CanAddIngredient) return;

            if (_items.Contains(item.ItemName)) return;
            
            collection.Add(item);
            _items.Add(item.ItemName);
            inventory.RemoveItem(item.ItemName);
            CheckCanCraft();
            Recipe = null;
            
            OnChanged?.Invoke();
        }

        private void RemoveItem<T>(T item, IStackableInventory<T> inventory, 
            HashSet<T> collection, HashSet<T> fakeCollection) where T : Item
        {
            if (collection.Remove(item))
            {
                _items.Remove(item.ItemName);
                inventory.AddItem(item);
            }
            
            fakeCollection.Remove(item);
            CheckCanCraft();
            Recipe = null;
            
            OnChanged?.Invoke();
        }

        private void GetPlantProducts(DishRecipe recipe)
        {
            foreach (PlantProductItemConfig plantProductConfig in recipe.PlantProducts)
            {
                string productName = plantProductConfig.Name;
                if (_plantProductInventory.IsItemExists(productName))
                {
                    _items.Add(productName);
                    var plantProduct = _plantProductInventory.RemoveItem(productName) as PlantProductItem;
                    _plantProducts.Add(plantProduct);
                }
                else
                {
                    var fakePlantProduct = plantProductConfig.Create() as PlantProductItem;
                    _fakePlantProducts.Add(fakePlantProduct);
                }
            }
        }

        private void GetAnimalProducts(DishRecipe recipe)
        {
            foreach (AnimalProductItemConfig itemConfig in recipe.AnimalProducts)
            {
                string productName = itemConfig.Name;
                if (_animalProductInventory.IsItemExists(productName))
                {
                    _items.Add(productName);
                    var loot = _animalProductInventory.RemoveItem(productName) as AnimalProductItem;
                    _animalProducts.Add(loot);
                }
                else
                {
                    var fakeAnimalProduct = itemConfig.Create() as AnimalProductItem;
                    _fakeAnimalProducts.Add(fakeAnimalProduct);
                }
            }
        }

        private void ReturnPlantProducts()
        {
            foreach (PlantProductItem product in _plantProducts)
            {
                _plantProductInventory.AddItem(product);
            }
            
            _plantProducts.Clear();
        }

        private void ReturnAnimalProducts()
        {
            foreach (AnimalProductItem product in _animalProducts)
            {
                _animalProductInventory.AddItem(product);
            }
            
            _animalProducts.Clear();
        }

        private void CheckCanCraft()
        {
            bool canCraft = PlantProducts.Count +  AnimalProducts.Count >= MinIngredientsCount 
               && FakeAnimalProducts.Count == 0
                && FakePlantProducts.Count == 0;
            
            OnCanCraftStateChanged?.Invoke(canCraft);
        }
    }
}