using System;
using Cysharp.Threading.Tasks;
using Modules.Inventories;
using Modules.Items;

namespace Modules.Crafting
{
    public abstract class ItemCrafter<T> where T: Item
    {
        public event Action<T> OnCrafted;
        
        private readonly IInventory<T> _dishInventory;

        public ItemCrafter(IInventory<T> dishInventory)
        {
            _dishInventory = dishInventory;
        }

        public abstract bool CanCraft(ItemRecipe<T> recipe);
        
        public async UniTaskVoid Craft(ItemRecipe<T> recipe)
        {
            if (!CanCraft(recipe))
            {
                throw new Exception("Not enough items");
            }
        
            RemoveIngredientsFromInventory(recipe);

            await UniTask.WaitForSeconds(recipe.CraftingTimeInSeconds);
            
            T item = AddResultToInventory(recipe);
            OnCrafted?.Invoke(item);
        }

        protected abstract void RemoveIngredientsFromInventory(ItemRecipe<T> recipe);
        
        private T AddResultToInventory(ItemRecipe<T> recipe)
        {
            var item = recipe.ResultItem.Item.Clone() as T;
            _dishInventory.AddItem(item);
            return item;
        }
    }
}