using System;
using Modules.Inventories;
using Modules.Items;
using Modules.Timers;

namespace Modules.Crafting
{
    public abstract class ItemCrafter<T> where T: Item
    {
        public event Action<T> OnCrafted;
        
        private readonly IInventory<T> _outputInventory;

        private ItemRecipe _currentRecipe;
        protected readonly Countdown Timer;

        protected ItemCrafter(IInventory<T> outputInventory)
        {
            _outputInventory = outputInventory;
            Timer = new Countdown();
        }

        public abstract bool CanCraft(ItemRecipe recipe);

        public void Craft(ItemRecipe recipe)
        {
            if (Timer.IsPlaying()) return;
            
            if (!CanCraft(recipe)) return;

            _currentRecipe = recipe;
            RemoveIngredientsFromInventories(_currentRecipe);

            int time = _currentRecipe.CraftingTimeInSeconds;
            if (time <= 0)
            {
                CreateResult();
                return;
            }
            
            Timer.Duration = time;
            Timer.Start();
            Timer.OnEnded += OnTimerEnded;
        }

        protected abstract void RemoveIngredientsFromInventories(ItemRecipe recipe);

        protected void Tick(float deltaTime) => Timer.Tick(deltaTime);

        private void CreateResult()
        {
            T item = AddResultToInventory(_currentRecipe);
            OnCrafted?.Invoke(item);
        }

        private T AddResultToInventory(ItemRecipe recipe)
        {
            var item = recipe.ResultItemConfig.Create() as T;
            _outputInventory.AddItem(item);
            return item;
        }
        
        private void OnTimerEnded()
        {
            Timer.OnEnded -= OnTimerEnded;
            Timer.Stop();
            CreateResult();
        }
    }
}