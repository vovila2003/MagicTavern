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
        private int TimeInSeconds = 5;

        public string Name => ResultItem.GetItem().ItemName;

        public ItemConfig<T> ResultItem => ResultItemConfig;

        public int CraftingTimeInSeconds => TimeInSeconds;
        
        
        [ShowInInspector, ReadOnly, PreviewField(50, ObjectFieldAlignment.Right)] 
        private Sprite Icon => ResultItemConfig?.GetItem().Metadata.Icon;
        
        [ShowInInspector, ReadOnly]
        private string RecipeName => ResultItemConfig?.GetItem().Metadata.Title;
    }
}