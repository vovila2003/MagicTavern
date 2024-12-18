using System;
using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening.Fertilizer
{
    [Serializable]
    public class ComponentHarvestBooster : IItemComponent
    {
        [SerializeField, Range(0, 100)] 
        private int HarvestBoostInPercent;

        public int Boost
        {
            get => HarvestBoostInPercent;
            private set => HarvestBoostInPercent = value;
        }

        public ComponentHarvestBooster()
        {
        }

        private ComponentHarvestBooster(int boost = 0)
        {
            HarvestBoostInPercent = boost;
        }

        public IItemComponent Clone()
        {
            return new ComponentHarvestBooster(HarvestBoostInPercent);
        }
    }
}