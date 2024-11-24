using System;
using UnityEngine;

namespace Tavern.Gardening.Fertilizer
{
    [Serializable]
    public class ComponentGrowthAcceleration : ICloneable
    {
        [SerializeField, Range(0, 100)] 
        private int GrowthAccelerationInPercent;

        public int Acceleration
        {
            get => GrowthAccelerationInPercent;
            private set => GrowthAccelerationInPercent = value;
        }

        public ComponentGrowthAcceleration()
        {
        }

        private ComponentGrowthAcceleration(int acceleration = 0)
        {
            GrowthAccelerationInPercent = acceleration;
        }

        object ICloneable.Clone() => new ComponentGrowthAcceleration(GrowthAccelerationInPercent);
    }
}