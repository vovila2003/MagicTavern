using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening.Medicine
{
    [CreateAssetMenu(
        fileName = "MedicineItemConfig",
        menuName = "Settings/Gardening/Medicine/Medicine Item Config")]
    public class MedicineItemConfig : ItemConfig<MedicineItem>
    {
        [SerializeField] 
        private int SickProbabilityReducing;

        public int Reducing => SickProbabilityReducing;
    }
}