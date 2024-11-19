using Modules.Items;
using UnityEngine;

namespace Modules.Crafting
{
    public class ItemRecipe<T> : ScriptableObject 
        where T : Item
    {
        [SerializeField] 
        private ItemConfig<T> ResultItemConfig;

        [SerializeField]
        private int TimeInSeconds;

        public string Name => ResultItem.Item.ItemName;
        public ItemConfig<T> ResultItem => ResultItemConfig;
        public int CraftingTimeInSeconds => TimeInSeconds;
    }
}