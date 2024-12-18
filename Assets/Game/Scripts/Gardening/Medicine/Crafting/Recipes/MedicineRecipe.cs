using System;
using System.Collections.Generic;
using Modules.Crafting;
using Sirenix.OdinInspector;
using Tavern.Common;
using UnityEngine;

namespace Tavern.Gardening.Medicine
{
    [CreateAssetMenu(
        fileName = "MedicineRecipe",
        menuName = "Settings/Gardening/Medicine/Medicine Recipe")]
    public class MedicineRecipe : ItemRecipe<MedicineItem>
    {
        [Title("Ingredients"), Space(10)]
        [SerializeField] 
        private int SlopsAmount;
        
        [SerializeField] 
        private LootIngredient[] LootIngredients;

        public LootIngredient[] Loots => LootIngredients;
        public int Slops => SlopsAmount;
        
        private void OnValidate()
        {
            CheckSlopsAmount();
            CheckLootDuplicates();
        }

        private void CheckSlopsAmount()
        {
            if (SlopsAmount <= 0)
            {
                Debug.LogWarning($"Amount of slops has to be greater than zero.");
            }
        }

        private void CheckLootDuplicates()
        {
            if (LootIngredients is null) return;
            
            var collection = new Dictionary<string, bool>();
            foreach (LootIngredient lootIngredient in LootIngredients)
            {
                if (lootIngredient.Loot is null) continue;
                
                string lootName = lootIngredient.Loot.Item.ItemName;
                if (lootIngredient.LootAmount <= 0)
                {
                    Debug.LogWarning($"Amount of loot of name {lootName} has to be greater than zero.");    
                }
                
                if (collection.TryAdd(lootName, true)) continue;

                throw new Exception($"Duplicate loot of name {lootName} in recipe of {Name}");
            }
        }
    }
}