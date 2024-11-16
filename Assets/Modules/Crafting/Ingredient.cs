using System;
using Modules.Items;

namespace Modules.Crafting
{
    [Serializable]
    public struct Ingredient<T> where T : Item
    {
        public ItemConfig<T> ItemConfig;
        public int Amount;

        public Ingredient(ItemConfig<T> itemConfig, int amount)
        {
            ItemConfig = itemConfig;
            Amount = amount;
        }
    }
}