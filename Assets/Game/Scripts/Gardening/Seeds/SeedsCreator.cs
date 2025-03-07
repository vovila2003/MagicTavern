using System.Collections.Generic;
using Modules.Gardening;
using Modules.Items;
using Sirenix.OdinInspector;
using Tavern.ProductsAndIngredients;
using UnityEngine;

namespace Tavern.Gardening
{
    [CreateAssetMenu(
        fileName = "SeedsCreator",
        menuName = "Settings/Gardening/Seeds/Seeds Creator")]
    public class SeedsCreator : ScriptableObject
    {
        [SerializeField] 
        private SeedCatalog SeedCatalog;

        [SerializeField] 
        private PlantProductCatalog PlantProductCatalog;

        [SerializeField] 
        private PlantsCatalog PlantCatalog;

        private readonly HashSet<Plant> _plantsInSeedCatalog = new ();
        private readonly Dictionary<Plant, PlantConfig> _plantToConfig = new ();

        [Button]
        private void Create()
        {
            FillPlantsInSeedCatalog();
            FillPlantToConfig();
            
            foreach (ItemConfig config in PlantProductCatalog.Items)
            {
                if (config is not PlantProductItemConfig plantConfig) continue;
                
                if (!plantConfig.TryGet(out ComponentPlant componentPlant)) continue;

                Plant plant = componentPlant.Config.Plant;
                if (_plantsInSeedCatalog.Contains(plant)) continue;

                var seedItemConfig = CreateInstance<SeedItemConfig>();
                seedItemConfig.OnValidate();
                string assetName = SeedNameProvider.GetName(componentPlant.Config.Name);
                if (!seedItemConfig.TryGet(out ComponentPlant seedComponentPlant)) continue;
                
                seedComponentPlant.Config = _plantToConfig[plant];
                seedItemConfig.OnValidate();
                UnityEditor.AssetDatabase.CreateAsset(seedItemConfig, 
                    $"Assets/Game/Configs/Gardening/Seeds/{assetName}.asset");

                SeedCatalog.AddConfig(seedItemConfig);
                _plantsInSeedCatalog.Add(plant);
            }
        }

        private void FillPlantsInSeedCatalog()
        {
            _plantsInSeedCatalog.Clear();
            foreach (ItemConfig config in SeedCatalog.Items)
            {
                if (config is not SeedItemConfig seedConfig) continue;
                
                if (!seedConfig.TryGet(out ComponentPlant componentPlant)) continue;

                Plant plant = componentPlant.Config.Plant;

                if (_plantsInSeedCatalog.Contains(plant))
                {
                    Debug.Log($"Duplicate {plant.PlantName} in seed catalog");
                    continue;
                }

                _plantsInSeedCatalog.Add(componentPlant.Config.Plant);
            }
        }

        private void FillPlantToConfig()
        {
            foreach (PlantConfig config in PlantCatalog.PlantConfigs)
            {
                _plantToConfig.Add(config.Plant, config);
            }
        }
    }
}