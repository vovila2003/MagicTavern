using Modules.Consuming;
using Modules.Items;
using Tavern.Gardening.Medicine;
using UnityEngine;

namespace Tavern.Gardening.Fertilizer
{
    public class SeedbedBoostSicknessReducingHandler : IInventoryItemConsumeHandler
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

            var component = item.GetComponent<ComponentHarvestSicknessReducing>();
            _target.Seedbed.ReduceHarvestSicknessProbability(component.Reducing);
        }
    }
}