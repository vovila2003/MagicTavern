using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Inventories;
using Modules.Items;
using Tavern.Looting;
using Tavern.ProductsAndIngredients;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public sealed class ActiveDishRecipe : IActiveDishRecipeReader
    {
        private const int MinIngredientsCount = 3;
        private const int MaxIngredientsCount = 7;

        public event Action OnChanged;
        public event Action<List<PlantProductItem>, List<LootItem>> OnSpent;
        public event Action<bool> OnCanCraftStateChanged;
        
        private readonly IStackableInventory<PlantProductItem> _plantProductInventory;
        private readonly IStackableInventory<LootItem> _lootInventory;

        private readonly HashSet<PlantProductItem> _plantProducts = new();
        private readonly HashSet<PlantProductItem> _fakePlantProducts = new();
        private readonly HashSet<LootItem> _loots = new();
        private readonly HashSet<LootItem> _fakeLoots = new();
        private readonly HashSet<string> _items = new();
        private readonly List<PlantProductItem> _spentPlantProducts = new();
        private readonly List<LootItem> _spentLoots = new();

        public DishRecipe Recipe { get; private set; }
        public bool IsEmpty => Recipe == null;

        public IReadOnlyCollection<Item> PlantProducts => _plantProducts;

        public IReadOnlyCollection<Item> FakePlantProducts => _fakePlantProducts;

        public IReadOnlyCollection<Item> Loots => _loots;

        public IReadOnlyCollection<Item> FakeLoots => _fakeLoots;

        private bool CanAddIngredient => _plantProducts.Count + _loots.Count < MaxIngredientsCount;

        public ActiveDishRecipe(
            IStackableInventory<PlantProductItem> plantProductInventory,
            IStackableInventory<LootItem> lootInventory)
        {
            _plantProductInventory = plantProductInventory;
            _lootInventory = lootInventory;
        }
        
        public bool HasItem(string item) => _items.Contains(item);

        public void AddPlantProduct(PlantProductItem plantProduct) => 
            AddItem(plantProduct, _plantProductInventory, _plantProducts);

        public void AddLoot(LootItem loot) => AddItem(loot, _lootInventory, _loots);

        public void RemovePlantProduct(PlantProductItem plantProduct) => 
            RemoveItem(plantProduct, _plantProductInventory, _plantProducts, _fakePlantProducts);

        public void RemoveLoot(LootItem loot) => 
            RemoveItem(loot, _lootInventory, _loots, _fakeLoots);

        public void Setup(DishRecipe recipe)
        {
            ResetRecipe();
            Recipe = recipe;
            GetPlantProducts(Recipe);
            GetLoots(Recipe);
            CheckCanCraft();
            
            OnChanged?.Invoke();
        }

        public void Reset()
        {
            ResetRecipe();
            CheckCanCraft();

            OnChanged?.Invoke();
        }

        public (List<PlantProductItem>, List<LootItem>) SpendIngredients()
        {
            _fakePlantProducts.Clear();
            _fakeLoots.Clear();
            _items.Clear();
            Recipe = null;
            
            _spentPlantProducts.Clear();
            _spentPlantProducts.AddRange(_plantProducts);
            
            _spentLoots.Clear();
            _spentLoots.AddRange(_loots);
            
            _plantProducts.Clear();
            _loots.Clear();
            CheckCanCraft();

            OnSpent?.Invoke(_spentPlantProducts, _spentLoots);
            OnChanged?.Invoke();
            
            return (_spentPlantProducts, _spentLoots);
        }

        private void ResetRecipe()
        {
            _fakePlantProducts.Clear();
            _fakeLoots.Clear();
            _items.Clear();
            Recipe = null;
            ReturnPlantProducts();
            ReturnLoots();
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
                string productName = plantProductConfig.GetItem().ItemName;
                if (_plantProductInventory.IsItemExists(productName))
                {
                    _items.Add(productName);
                    PlantProductItem plantProduct = _plantProductInventory.RemoveItem(productName);
                    _plantProducts.Add(plantProduct);
                }
                else
                {
                    var fakePlantProduct = plantProductConfig.GetItem().Clone() as PlantProductItem;
                    _fakePlantProducts.Add(fakePlantProduct);
                }
            }
        }

        private void GetLoots(DishRecipe recipe)
        {
            foreach (LootItemConfig lootConfig in recipe.Loots)
            {
                string lootName = lootConfig.GetItem().ItemName;
                if (_lootInventory.IsItemExists(lootName))
                {
                    _items.Add(lootName);
                    LootItem loot = _lootInventory.RemoveItem(lootName);
                    _loots.Add(loot);
                }
                else
                {
                    var fakeLoot = lootConfig.GetItem().Clone() as LootItem;
                    _fakeLoots.Add(fakeLoot);
                }
            }
        }

        private void ReturnPlantProducts()
        {
            foreach (PlantProductItem plantProduct in _plantProducts)
            {
                _plantProductInventory.AddItem(plantProduct);
            }
            
            _plantProducts.Clear();
        }

        private void ReturnLoots()
        {
            foreach (LootItem loot in _loots)
            {
                _lootInventory.AddItem(loot);
            }
            
            _loots.Clear();
        }

        private void CheckCanCraft()
        {
            bool canCraft = PlantProducts.Count +  Loots.Count >= MinIngredientsCount 
               && FakeLoots.Count == 0
                && FakePlantProducts.Count == 0;
            
            OnCanCraftStateChanged?.Invoke(canCraft);
        }
    }
}