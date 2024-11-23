using Modules.Inventories;
using UnityEngine;

namespace Tavern.Gardening.Medicine
{
    [CreateAssetMenu(
        fileName = "MedicineConfig",
        menuName = "Settings/Gardening/Medicine/Medicine Config")]
    public class MedicineConfig : StackableItemConfig<MedicineItem>
    {
        [SerializeField] 
        private int SickProbabilityReducing;

        public int Reducing => SickProbabilityReducing;
    }
}