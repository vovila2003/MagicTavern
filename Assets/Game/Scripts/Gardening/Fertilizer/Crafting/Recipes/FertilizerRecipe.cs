using Modules.Crafting;
using Sirenix.OdinInspector;
using Tavern.Looting;
using UnityEngine;

namespace Tavern.Gardening.Fertilizer
{
    [CreateAssetMenu(
        fileName = "FertilizerRecipe",
        menuName = "Settings/Gardening/Fertilizer/Fertilizer Recipe")]
    public class FertilizerRecipe : ItemRecipe
    {
        [Title("Ingredients"), Space(10)]
        [SerializeField] 
        private int SlopsAmount;
        
        [SerializeField] 
        private LootItemConfig[] LootIngredients;

        public LootItemConfig[] Loots => LootIngredients;
        public int Slops => SlopsAmount;
        
        private void OnValidate()
        {
            CheckSlopsAmount();
        }

        private void CheckSlopsAmount()
        {
            if (SlopsAmount <= 0)
            {
                Debug.LogWarning($"Amount of slops has to be greater than zero.");
            }
        }

        public override void Validate()
        {
            CheckSlopsAmount();
        }
    }
}