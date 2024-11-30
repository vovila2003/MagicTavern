using Modules.GameCycle;
using Modules.Inventories;
using Sirenix.OdinInspector;
using Tavern.Looting;
using Tavern.Storages;
using UnityEngine;
using VContainer;

namespace Tavern.Gardening.Medicine
{
    public class MedicineCrafterContext : MonoBehaviour
    {
        private MedicineCrafter _medicineCrafter;
        
        [ShowInInspector, ReadOnly]
        private float TimerCurrentTime => _medicineCrafter?.TimerCurrentTime ?? -1;
        
        [Inject]
        private void Construct(
            IInventory<MedicineItem> medicineInventory,
            IInventory<LootItem> lootInventory,
            ISlopsStorage slopsStorage,
            GameCycle gameCycle)
        {
            _medicineCrafter = new MedicineCrafter(medicineInventory, lootInventory, slopsStorage);
            gameCycle.AddListener(_medicineCrafter);
        }

        private void OnEnable() => _medicineCrafter.OnCrafted += OnCrafted;

        private void OnDisable() => _medicineCrafter.OnCrafted -= OnCrafted;

        [Button]
        public void Craft(MedicineRecipe recipe)
        {
            if (_medicineCrafter.InProgress)
            {
                Debug.Log("Crafting is in progress. Wait for finish!");    
                return;
            }
            
            if (!_medicineCrafter.CanCraft(recipe))
            {
                Debug.Log($"Can't craft dish of name {recipe.ResultItem.Item.ItemName}");    
                return;
            } 
            
            _medicineCrafter.Craft(recipe);
        }

        private void OnCrafted(MedicineItem medicine)
        {
            Debug.Log($"Medicine with name {medicine.ItemName} is crafted!");
        }
    }
}