using Modules.Consuming;
using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening.Fertilizer
{
    public class SeedInHarvestHandler : IInventoryItemConsumeHandler
    {
        private Pot _target;
        public void SetTarget(object target)
        {
            _target = target as Pot;
        }

        public void OnConsume(Item item)
        {
            if (_target is null)
            {
                Debug.Log($"Target is not {nameof(Pot)}");
                return;
            }

            if (!item.HasComponent<ComponentSeedInHarvest>()) return;

            var component = item.GetComponent<ComponentSeedInHarvest>();
            _target.Seedbed.SetSeedInHarvestProbability(component.Probability);
        }
    }
}