using Sirenix.OdinInspector;
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
        private void Construct(FertilizerCrafter fertilizerCrafter)
        {
            _fertilizerCrafter = fertilizerCrafter;
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
                Debug.Log($"Can't craft dish of name {recipe.ResultItem.GetItem().ItemName}");    
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