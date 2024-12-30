using System;
using Modules.Gardening;
using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening
{
    [Serializable]
    public class ComponentPlant : IItemComponent
    {
        [SerializeField] 
        private PlantConfig PlantConfig;
        
        public PlantConfig Config => PlantConfig;

        public ComponentPlant()
        {
        }

        public ComponentPlant(PlantConfig config)
        {
            PlantConfig = config;
        }

        public IItemComponent Clone()
        {
            return new ComponentPlant(PlantConfig);
        }
    }
}