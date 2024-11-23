using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening.Fertilizer
{
    [CreateAssetMenu(
        fileName = "FertilizerConfig",
        menuName = "Settings/Gardening/Fertilizer/Fertilizer Config")]
    public class FertilizerConfig : ItemConfig<FertilizerItem>
    {
        [SerializeField, Range(0f, 100f)] 
        private int GrowthAccelerator;
        
        [SerializeField] 
        private int SickProbabilityReducing;

        [SerializeField, Range(0f, 100f)] 
        private int HarvestBooster;

        public int Accelerator => GrowthAccelerator;
        public int Reducing => SickProbabilityReducing;
        public int Booster => HarvestBooster;
    }
}