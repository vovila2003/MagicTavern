using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Gardening;
using Tavern.Settings;
using UnityEngine;

namespace Tavern.Gardening
{
    [CreateAssetMenu(
        fileName = "SeedMakerConfig", 
        menuName = "Settings/Gardening/SeedMaker Config")]
    public class SeedMakerSettings : ScriptableObject
    {
        [SerializeField]
        private SeedParams[] ConvertToSeedsParams;
        
        public IReadOnlyList<SeedParams> Params => ConvertToSeedsParams.ToList();

        private void OnValidate()
        {
            var collection = new Dictionary<Plant, bool>();
            foreach (SeedParams settings in ConvertToSeedsParams)
            {
                Plant plantType = settings.Type;
                if (collection.TryAdd(plantType, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate convert params of type {plantType.PlantName}");
            }
        }
    }
}