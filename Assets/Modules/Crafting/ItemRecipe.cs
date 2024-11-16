using Modules.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modules.Crafting
{
    public class ItemRecipe<T> : ScriptableObject 
        where T : Item
    {
        [SerializeField] 
        private ItemConfig<T> ResultItemConfig;

        [SerializeField]
        private int TimeInSeconds;

        
        
        public ItemConfig<T> ResultItem => ResultItemConfig;
        public int CraftingTimeInSeconds => TimeInSeconds;
        
    }
}