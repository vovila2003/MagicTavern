using Modules.Items;
using Sirenix.OdinInspector;
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
        
        
        [ShowInInspector, ReadOnly, PreviewField(50, ObjectFieldAlignment.Right)] 
        private Sprite Icon => ResultItemConfig?.Item.Metadata.Icon;
        
        [ShowInInspector, ReadOnly]
        private string RecipeName => ResultItemConfig?.Item.Metadata.Title;
    }
}