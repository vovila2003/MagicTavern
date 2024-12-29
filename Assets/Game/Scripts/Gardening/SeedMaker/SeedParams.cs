using System;
using Modules.Gardening;
using UnityEngine;

namespace Tavern.Gardening
{
    [Serializable]
    public class SeedParams
    {
        [SerializeField] 
        private PlantConfig Plant;
            
        [SerializeField] 
        private int ProductToSeedRatio;
            
        public Plant Type => Plant.Plant;
        public int Ratio => ProductToSeedRatio;
    }
}