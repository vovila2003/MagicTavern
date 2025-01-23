using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Modules.Crafting;
using Modules.GameCycle.Interfaces;
using Modules.Inventories;
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
        public override bool HaveAllIngredients { get; protected set; }
        public bool HaveAllKitchenItems { get; private set; }
        public override bool CanCraft => HaveAllIngredients && HaveAllKitchenItems;

        private readonly IStackableInventory<ProductItem> _productInventory;
        private readonly IStackableInventory<LootItem> _lootInventory;
        private readonly IStackableInventory<KitchenItem> _kitchenInventory;

        private readonly List<ProductItem> _products = new();
        private readonly List<ProductItem> _fakeProducts = new();
        private readonly List<LootItem> _loots = new();
        private readonly List<LootItem> _fakeLoots = new();
        
        private DishRecipe _recipe;

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

        protected override bool CheckRecipeType(ItemRecipe<DishItem> recipe)
        {
            _recipe = recipe as DishRecipe;
            return recipe is DishRecipe;
        }

        protected override void GetIngredients()
        {
            GetProducts(_recipe);
            GetLoots(_recipe);
        }

        protected override void SetProperties()
        {
            HaveAllIngredients = Filled && _fakeProducts.Count == 0 && _fakeLoots.Count == 0;
            HaveAllKitchenItems = CheckKitchenItems(_recipe);
        }

        protected override void OnDispose()
        {
            _fakeProducts.Clear();
            _fakeLoots.Clear();
            ReturnProducts();
            ReturnLoots();
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

        private void GetProducts(DishRecipe recipe)
        {
            foreach (ProductItemConfig productConfig in recipe.Products)
            {
                ProductItem item = _productInventory.RemoveItem(productConfig.Item.ItemName);
                if (item != null)
                {
                    _products.Add(item);
                }
                else
                {
                    _fakeProducts.Add(productConfig.Item.Clone() as ProductItem);
                }
            }
        }

        private void GetLoots(DishRecipe recipe)
        {
            foreach (LootItemConfig lootConfig in recipe.Loots)
            {
                LootItem item = _lootInventory.RemoveItem(lootConfig.Item.ItemName);
                if (item != null)
                {
                    _loots.Add(item);
                }
                else
                {
                    _fakeLoots.Add(lootConfig.Item.Clone() as LootItem);
                }
            }
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