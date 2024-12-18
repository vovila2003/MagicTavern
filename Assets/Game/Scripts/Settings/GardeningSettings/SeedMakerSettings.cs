using System;
using System.Collections.Generic;
using Modules.Gardening;
using UnityEngine;

namespace Tavern.Settings
{
    [CreateAssetMenu(
        fileName = "SeedMakerConfig", 
        menuName = "Settings/Gardening/SeedMaker Config")]
    public class SeedMakerSettings : ScriptableObject
    {
        [Serializable]
        private class SeedParams
        {
            [SerializeField] 
            private PlantConfig Plant;
            
            [SerializeField] 
            private int ProductToSeedRatio;
            
            public Plant Type => Plant.Plant;
            public int Ratio => ProductToSeedRatio;
        }
        
        [SerializeField]
        private SeedParams[] ConvertToSeedsParams;
        
        private readonly Dictionary<Plant, int> _seeds = new ();

        public bool TryGetSeedRatio(Plant plantType, out int convertRatio)
        {
            bool contains = _seeds.TryGetValue(plantType, out int ratio);
            convertRatio = ratio;
            return contains;
        }

        private void OnValidate()
        {
            var collection = new Dictionary<Plant, bool>();
            foreach (SeedParams settings in ConvertToSeedsParams)
            {
                Plant plantType = settings.Type;
                _seeds[plantType] = settings.Ratio;
                if (collection.TryAdd(plantType, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate convert params of type {plantType.PlantName}");
            }
        }
    }
}