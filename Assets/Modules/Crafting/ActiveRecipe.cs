using System;
using Modules.Inventories;
using Modules.Items;
using Modules.Timers;

namespace Modules.Crafting
{
    public abstract class ActiveRecipe<T> where T : Item
    {
        public abstract bool HaveAllIngredients { get; protected set; }
        public abstract bool CanCraft { get; }
        public bool Filled { get; private set; }

        private readonly IInventory<T> _outputInventory;
        private ItemRecipe<T> _recipe;
        protected readonly Countdown Timer;

        protected ActiveRecipe(IInventory<T> outputInventory)
        {
            _outputInventory = outputInventory;
            Timer = new Countdown();
            Filled = false;
        }

        public void Setup(ItemRecipe<T> recipe)
        {
            if (!OnSetupCheckRecipeType(recipe)) return;
            
            Reset();
            _recipe = recipe;
            Filled = true;
            OnSetup();
        }

        public void Reset()
        {
            Filled = false;
            OnReset();
        }

        public void Prepare()
        {
            if (Timer.IsPlaying()) return;
                     
             if (!CanCraft) return;

             int time = _recipe.CraftingTimeInSeconds;
             if (time <= 0)
             {
                 CreateResult();
                 return;
             }
                     
             Timer.Duration = time;
             Timer.Start();
             Timer.OnEnded += OnTimerEnded;
        }

        protected abstract void OnSetup();

        protected abstract bool OnSetupCheckRecipeType(ItemRecipe<T> recipe);

        protected abstract void OnReset();

        protected void Tick(float deltaTime) => Timer.Tick(deltaTime);

        protected abstract void OnCreateResult(T item);

        private void CreateResult()
        {
            T item = AddResultToInventory(_recipe);
            OnCreateResult(item);
        }

        private T AddResultToInventory(ItemRecipe<T> recipe)
        {
            var item = recipe.ResultItem.Item.Clone() as T;
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



