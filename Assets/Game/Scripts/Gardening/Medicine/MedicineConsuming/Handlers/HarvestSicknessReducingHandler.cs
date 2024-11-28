using Modules.Consuming;
using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening.Medicine
{
    public class HarvestSicknessReducingHandler : IInventoryItemConsumeHandler
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

            if (!item.HasComponent<ComponentHarvestSicknessReducing>()) return;

            var attribute = item.GetComponent<ComponentHarvestSicknessReducing>();
            _target.ReduceHarvestSicknessProbability(attribute.Reducing);
        }
    }
}