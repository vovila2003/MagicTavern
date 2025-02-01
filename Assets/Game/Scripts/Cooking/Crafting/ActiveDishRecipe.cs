using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Inventories;
using Modules.Items;
using Tavern.Gardening;
using Tavern.Looting;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public sealed class ActiveDishRecipe : IActiveDishRecipeReader
    {
        private const int MinIngredientsCount = 3;
        private const int MaxIngredientsCount = 7;

        public event Action OnChanged;
        public event Action<List<ProductItem>, List<LootItem>> OnSpent;
        public event Action<bool> OnCanCraftStateChanged;
        
        private readonly IStackableInventory<ProductItem> _productInventory;
        private readonly IStackableInventory<LootItem> _lootInventory;

        private readonly HashSet<ProductItem> _products = new();
        private readonly HashSet<ProductItem> _fakeProducts = new();
        private readonly HashSet<LootItem> _loots = new();
        private readonly HashSet<LootItem> _fakeLoots = new();
        private readonly HashSet<string> _items = new();
        private readonly List<ProductItem> _spentProducts = new();
        private readonly List<LootItem> _spentLoots = new();

        public DishRecipe Recipe { get; private set; }
        public bool IsEmpty => Recipe == null;

        public IReadOnlyCollection<Item> Products => _products;

        public IReadOnlyCollection<Item> FakeProducts => _fakeProducts;

        public IReadOnlyCollection<Item> Loots => _loots;

        public IReadOnlyCollection<Item> FakeLoots => _fakeLoots;

        private bool CanAddIngredient => _products.Count + _loots.Count < MaxIngredientsCount;

        public ActiveDishRecipe(
            IStackableInventory<ProductItem> productInventory,
            IStackableInventory<LootItem> lootInventory)
        {
            _productInventory = productInventory;
            _lootInventory = lootInventory;
        }
        
        public bool HasItem(string item) => _items.Contains(item);

        public void AddProduct(ProductItem product) => AddItem(product, _productInventory, _products);

        public void AddLoot(LootItem loot) => AddItem(loot, _lootInventory, _loots);

        public void RemoveProduct(ProductItem product) => 
            RemoveItem(product, _productInventory, _products, _fakeProducts);

        public void RemoveLoot(LootItem loot) => 
            RemoveItem(loot, _lootInventory, _loots, _fakeLoots);

        public void Setup(DishRecipe recipe)
        {
            ResetRecipe();
            Recipe = recipe;
            GetProducts(Recipe);
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

        public (List<ProductItem>, List<LootItem>) SpendIngredients()
        {
            _fakeProducts.Clear();
            _fakeLoots.Clear();
            _items.Clear();
            Recipe = null;
            
            _spentProducts.Clear();
            _spentProducts.AddRange(_products);
            
            _spentLoots.Clear();
            _spentLoots.AddRange(_loots);
            
            _products.Clear();
            _loots.Clear();
            CheckCanCraft();

            OnSpent?.Invoke(_spentProducts, _spentLoots);
            OnChanged?.Invoke();
            
            return (_spentProducts, _spentLoots);
        }

        private void ResetRecipe()
        {
            _fakeProducts.Clear();
            _fakeLoots.Clear();
            _items.Clear();
            Recipe = null;
            ReturnProducts();
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

        private void GetProducts(DishRecipe recipe)
        {
            foreach (ProductItemConfig productConfig in recipe.Products)
            {
                string productName = productConfig.GetItem().ItemName;
                if (_productInventory.IsItemExists(productName))
                {
                    _items.Add(productName);
                    ProductItem product = _productInventory.RemoveItem(productName);
                    _products.Add(product);
                }
                else
                {
                    var fakeProduct = productConfig.GetItem().Clone() as ProductItem;
                    _fakeProducts.Add(fakeProduct);
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

        private void ReturnProducts()
        {
            foreach (ProductItem product in _products)
            {
                _productInventory.AddItem(product);
            }
            
            _products.Clear();
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
            bool canCraft = Products.Count +  Loots.Count >= MinIngredientsCount 
               && FakeLoots.Count == 0
                && FakeProducts.Count == 0;
            
            OnCanCraftStateChanged?.Invoke(canCraft);
        }
    }
}