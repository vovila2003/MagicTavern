using System;
using Modules.Inventories;
using Modules.Items;
using Modules.Timers;

namespace Modules.Crafting
{
    public abstract class ActiveRecipe<T> where T : Item
    {
        public event Action<T> OnPrepared;
        public abstract bool HaveAllIngredients { get; protected set; }
        public abstract bool CanCraft { get; }
        public bool Filled { get; private set; }

        private readonly IInventory<T> _outputInventory;
        protected ItemRecipe<T> Recipe;
        protected readonly Countdown Timer;

        protected ActiveRecipe(IInventory<T> outputInventory)
        {
            _outputInventory = outputInventory;
            Timer = new Countdown();
            Filled = false;
        }

        public void Setup(ItemRecipe<T> recipe)
        {
            if (!CheckRecipeType(recipe)) return;
            
            Dispose();
            GetIngredients();
            SetProperties();
            Filled = true;
        }

        public void Dispose()
        {
            OnDispose();
            Filled = false;
        }

        public void Prepare()
        {
            if (Timer.IsPlaying()) return;
                     
             if (!CanCraft) return;

             int time = Recipe.CraftingTimeInSeconds;
             if (time <= 0)
             {
                 CreateResult();
                 return;
             }
                     
             Timer.Duration = time;
             Timer.Start();
             Timer.OnEnded += OnTimerEnded;
        }

        protected abstract bool CheckRecipeType(ItemRecipe<T> recipe);

        protected abstract void GetIngredients();
        
        protected abstract void SetProperties();

        protected abstract void OnDispose();

        protected void Tick(float deltaTime) => Timer.Tick(deltaTime);

        private void CreateResult()
        {
            T item = AddResultToInventory(Recipe);
            OnPrepared?.Invoke(item);
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



