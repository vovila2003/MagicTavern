using JetBrains.Annotations;
using Modules.Crafting;
using Modules.Inventories;
using Tavern.Looting;
using Tavern.Storages;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public class DishCrafter : ItemCrafter<DishItem>
    {
        private IInventory<LootItem> _lootInventory;
        private readonly IProductsStorage _productsStorage;
        private readonly IInventory<KitchenItem> _kitchenInventory;

        public DishCrafter(
            IInventory<DishItem> dishInventory, 
            IInventory<LootItem> lootInventory,
            IProductsStorage productsStorage,
            IInventory<KitchenItem> kitchenInventory) 
            : base(dishInventory)
        {
            _lootInventory = lootInventory;
            _productsStorage = productsStorage;
            _kitchenInventory = kitchenInventory;
        }

        public override bool CanCraft(ItemRecipe<DishItem> recipe)
        {
            var dishRecipe = recipe as DishRecipe;
            //TODO
            return true;
        }

        protected override void RemoveIngredientsFromInventory(ItemRecipe<DishItem> recipe)
        {
            //TODO
        }
    }
}