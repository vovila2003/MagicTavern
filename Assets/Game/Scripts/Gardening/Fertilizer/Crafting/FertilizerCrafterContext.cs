using Modules.GameCycle;
using Modules.Inventories;
using Sirenix.OdinInspector;
using Tavern.Looting;
using Tavern.Storages;
using UnityEngine;
using VContainer;

namespace Tavern.Gardening.Fertilizer
{
    public class FertilizerCrafterContext : MonoBehaviour
    {
        private FertilizerCrafter _fertilizerCrafter;
        
        [ShowInInspector, ReadOnly]
        private float TimerCurrentTime => _fertilizerCrafter?.TimerCurrentTime ?? -1;
        
        [Inject]
        private void Construct(
            IInventory<FertilizerItem> fertilizerInventory,
            IInventory<LootItem> lootInventory,
            ISlopsStorage slopsStorage,
            GameCycle gameCycle)
        {
            _fertilizerCrafter = new FertilizerCrafter(fertilizerInventory, lootInventory, slopsStorage);
            gameCycle.AddListener(_fertilizerCrafter);
        }

        private void OnEnable() => _fertilizerCrafter.OnCrafted += OnCrafted;

        private void OnDisable() => _fertilizerCrafter.OnCrafted -= OnCrafted;

        [Button]
        public void Craft(FertilizerRecipe recipe)
        {
            if (_fertilizerCrafter.InProgress)
            {
                Debug.Log("Crafting is in progress. Wait for finish!");    
                return;
            }
            
            if (!_fertilizerCrafter.CanCraft(recipe))
            {
                Debug.Log($"Can't craft dish of name {recipe.ResultItem.Item.ItemName}");    
                return;
            } 
            
            _fertilizerCrafter.Craft(recipe);
        }

        private void OnCrafted(FertilizerItem fertilizer)
        {
            Debug.Log($"fertilizer with name {fertilizer.ItemName} is crafted!");
        }
    }
}