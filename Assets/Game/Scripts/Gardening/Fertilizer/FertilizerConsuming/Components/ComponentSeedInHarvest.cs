using System;
using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening.Fertilizer
{
    [Serializable]
    public class ComponentSeedInHarvest : IItemComponent
    {
        [SerializeField, Range(0, 100)] 
        private int SeedInHarvestProbability;

        public int Probability
        {
            get => SeedInHarvestProbability;
            private set => SeedInHarvestProbability = value;
        }

        public ComponentSeedInHarvest()
        {
        }

        private ComponentSeedInHarvest(int probability = 0)
        {
            SeedInHarvestProbability = probability;
        }

        public IItemComponent Clone()
        {
            return new ComponentSeedInHarvest(SeedInHarvestProbability);
        }
    }
}