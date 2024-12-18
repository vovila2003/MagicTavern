using System;
using Modules.Crafting;
using Tavern.Looting;

namespace Tavern.Common
{
    [Serializable]
    public class LootIngredient : Ingredient<LootItem>
    {
        public LootItemConfig Loot => Item as LootItemConfig;
        public int LootAmount => ItemAmount;
    }
}