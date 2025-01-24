using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Modules.Crafting;
using Modules.GameCycle.Interfaces;
using Modules.Inventories;
using Modules.Items;
using Tavern.Gardening;
using Tavern.Looting;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public sealed class ActiveDishRecipe : 
        ActiveRecipe<DishItem>,
        IExitGameListener,
        IFinishGameListener,
        IPauseGameListener,
        IResumeGameListener,
        ITickable
    {
        private const int MaxIngredientsCount = 7;

        public event Action OnChanged;

        //TODO
        public event Action<DishRecipe> OnMatched;
        public event Action<DishRecipe> OnPrepared;

        private readonly IStackableInventory<ProductItem> _productInventory;
        private readonly IStackableInventory<LootItem> _lootInventory;
        private readonly IStackableInventory<KitchenItem> _kitchenInventory;

        private readonly List<ProductItem> _products = new();
        private readonly List<ProductItem> _fakeProducts = new();
        private readonly List<LootItem> _loots = new();
        private readonly List<LootItem> _fakeLoots = new();

        private DishRecipe _recipe;
        
        public override bool HaveAllIngredients { get; protected set; }
        public bool HaveAllKitchenItems { get; private set; }
        
        //TODO
        public override bool CanCraft => HaveAllIngredients && HaveAllKitchenItems;
        public IReadOnlyList<Item> Products => _products;
        public IReadOnlyList<Item> FakeProducts => _fakeProducts;
        public IReadOnlyList<Item> Loots => _loots;
        public IReadOnlyList<Item> FakeLoots => _fakeLoots;
        
        private bool CanAddIngredient => _products.Count + _loots.Count < MaxIngredientsCount;

        public ActiveDishRecipe(
            IInventory<DishItem> outputInventory,
            IStackableInventory<ProductItem> productInventory,
            IStackableInventory<LootItem> lootInventory,
            IStackableInventory<KitchenItem> kitchenInventory
            ) : base(outputInventory)
        {
            _productInventory = productInventory;
            _lootInventory = lootInventory;
            _kitchenInventory = kitchenInventory;
        }

        public void AddProduct(ProductItem product) => AddItem(product, _productInventory, _products);

        public void AddLoot(LootItem loot) => AddItem(loot, _lootInventory, _loots);

        public void RemoveProduct(ProductItem product) => 
            RemoveItem(product, _productInventory, _products, _fakeProducts);

        public void RemoveLoot(LootItem loot) => 
            RemoveItem(loot, _lootInventory, _loots, _fakeLoots);

        protected override void OnSetup()
        {
            GetProducts(_recipe);
            GetLoots(_recipe);
            
            //TODO
            HaveAllIngredients = Filled && _fakeProducts.Count == 0 && _fakeLoots.Count == 0;
            
            HaveAllKitchenItems = CheckKitchenItems(_recipe);
            
            OnChanged?.Invoke();
        }

        protected override bool OnSetupCheckRecipeType(ItemRecipe<DishItem> recipe)
        {
            _recipe = recipe as DishRecipe;
            return recipe is DishRecipe;
        }

        protected override void OnReset()
        {
            _fakeProducts.Clear();
            _fakeLoots.Clear();
            ReturnProducts();
            ReturnLoots();
            
            OnChanged?.Invoke();
        }

        protected override void OnCreateResult(DishItem item)
        {
            //TODO
            Debug.Log($"{item.ItemName} created");
        }

        private void AddItem<T>(T item, IStackableInventory<T> inventory, List<T> collection) where T : Item
        {
            if (!CanAddIngredient) return;

            collection.Add(item);
            inventory.RemoveItem(item.ItemName);
            OnChanged?.Invoke();
            CheckMatch();
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
            CheckMatch();
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

        private void CheckMatch()
        {
            //TODO    
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

        private bool CheckKitchenItems(DishRecipe recipe) => 
            recipe.KitchenItems.All(
                kitchenItemConfig => _kitchenInventory.GetItemCount(kitchenItemConfig.Item.ItemName) > 0);

        void IExitGameListener.OnExit() => Timer.Stop();

        void IFinishGameListener.OnFinish() => Timer.Stop();

        void IPauseGameListener.OnPause() => Timer.Pause();

        void IResumeGameListener.OnResume() => Timer.Resume();

        void ITickable.Tick() => Tick(Time.deltaTime);
    }
}