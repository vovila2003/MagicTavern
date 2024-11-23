using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening.Medicine
{
    [CreateAssetMenu(
        fileName = "MedicineConfig",
        menuName = "Settings/Gardening/Medicine/Medicine Config")]
    public class MedicineConfig : ItemConfig<MedicineItem>
    {
        [SerializeField] 
        private int SickProbabilityReducing;

        public int Reducing => SickProbabilityReducing;
    }
}