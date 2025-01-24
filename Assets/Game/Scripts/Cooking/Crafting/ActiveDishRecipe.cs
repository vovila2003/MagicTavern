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
    public sealed class ActiveDishRecipe  
    {
        private const int MaxIngredientsCount = 7;

        public event Action OnChanged;
        
        private readonly IStackableInventory<ProductItem> _productInventory;
        private readonly IStackableInventory<LootItem> _lootInventory;

        private readonly List<ProductItem> _products = new();
        private readonly List<ProductItem> _fakeProducts = new();
        private readonly List<LootItem> _loots = new();
        private readonly List<LootItem> _fakeLoots = new();

        private DishRecipe _recipe;

        public IReadOnlyList<Item> Products => _products;

        public IReadOnlyList<Item> FakeProducts => _fakeProducts;

        public IReadOnlyList<Item> Loots => _loots;

        public IReadOnlyList<Item> FakeLoots => _fakeLoots;

        private bool CanAddIngredient => _products.Count + _loots.Count < MaxIngredientsCount;

        public ActiveDishRecipe(
            IStackableInventory<ProductItem> productInventory,
            IStackableInventory<LootItem> lootInventory)
        {
            _productInventory = productInventory;
            _lootInventory = lootInventory;
        }

        public bool CanTryCraft()
        {
            if (_recipe == null) return false;
            
            return _recipe.Products.Length == Products.Count
                   && _recipe.Loots.Length == Loots.Count
                   && FakeLoots.Count == 0
                   && FakeProducts.Count == 0;
        }

        public void AddProduct(ProductItem product) => AddItem(product, _productInventory, _products);

        public void AddLoot(LootItem loot) => AddItem(loot, _lootInventory, _loots);

        public void RemoveProduct(ProductItem product) => 
            RemoveItem(product, _productInventory, _products, _fakeProducts);

        public void RemoveLoot(LootItem loot) => 
            RemoveItem(loot, _lootInventory, _loots, _fakeLoots);

        public void Setup(DishRecipe recipe)
        {
            Reset();
            SetRecipe(recipe);
            GetProducts(_recipe);
            GetLoots(_recipe);
            
            OnChanged?.Invoke();
        }

        public void Reset()
        {
            _fakeProducts.Clear();
            _fakeLoots.Clear();
            SetRecipe(null);
            ReturnProducts();
            ReturnLoots();

            OnChanged?.Invoke();
        }

        public void SetRecipe(DishRecipe recipe) => _recipe = recipe;

        private void AddItem<T>(T item, IStackableInventory<T> inventory, List<T> collection) where T : Item
        {
            if (!CanAddIngredient) return;
            
            collection.Add(item);
            inventory.RemoveItem(item.ItemName);
            
            OnChanged?.Invoke();
        }

        private void RemoveItem<T>(T item, IStackableInventory<T> inventory, 
            List<T> collection, List<T> fakeCollection) where T : Item
        {
            if (collection.Remove(item))
            {
                inventory.AddItem(item);
            }
            
            fakeCollection.Remove(item);
            OnChanged?.Invoke();
        }

        private void GetProducts(DishRecipe recipe)
        {
            foreach (ProductItemConfig productConfig in recipe.Products)
            {
                ProductItem product = _productInventory.RemoveItem(productConfig.Item.ItemName);
                if (product != null)
                {
                    _products.Add(product);
                }
                else
                {
                    var fakeProduct = productConfig.Item.Clone() as ProductItem;
                    _fakeProducts.Add(fakeProduct);
                }
            }
        }

        private void GetLoots(DishRecipe recipe)
        {
            foreach (LootItemConfig lootConfig in recipe.Loots)
            {
                LootItem loot = _lootInventory.RemoveItem(lootConfig.Item.ItemName);
                if (loot != null)
                {
                    _loots.Add(loot);
                }
                else
                {
                    var fakeLoot = lootConfig.Item.Clone() as LootItem;
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
    }
}