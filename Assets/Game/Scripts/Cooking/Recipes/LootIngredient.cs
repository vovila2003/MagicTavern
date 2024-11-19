using System;
using Modules.Crafting;
using Tavern.Looting;

namespace Tavern.Cooking
{
    [Serializable]
    public class LootIngredient : Ingredient<LootItem>
    {
        public LootItemConfig Loot => Item as LootItemConfig;
        public int LootAmount => ItemAmount;
    }
}