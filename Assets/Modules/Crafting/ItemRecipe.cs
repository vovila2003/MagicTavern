using Modules.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Crafting
{
    public abstract class ItemRecipe : ScriptableObject 
    {
        [field: SerializeField] 
        public ItemConfig ResultItemConfig { get; private set; }

        [SerializeField]
        private int TimeInSeconds = 5;

        public string Name => ResultItemConfig?.Name;

        public int CraftingTimeInSeconds => TimeInSeconds;

        public abstract void Validate();
        
        
        [ShowInInspector, ReadOnly, PreviewField(50, ObjectFieldAlignment.Right)] 
        private Sprite Icon => ResultItemConfig?.Metadata.Icon;
        
        [ShowInInspector, ReadOnly]
        private string RecipeName => ResultItemConfig?.Metadata.Title;
    }
}