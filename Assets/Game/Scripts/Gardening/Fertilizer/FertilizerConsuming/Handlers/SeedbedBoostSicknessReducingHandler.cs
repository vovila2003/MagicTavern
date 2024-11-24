using Modules.Consuming;
using Modules.Items;
using Tavern.Gardening.Medicine;
using UnityEngine;

namespace Tavern.Gardening.Fertilizer
{
    public class SeedbedBoostSicknessReducingHandler : IInventoryItemConsumeHandler
    {
        private Seedbed _target;
        public void SetTarget(object target)
        {
            _target = target as Seedbed;
        }

        public void OnConsume(Item item)
        {
            if (_target is null)
            {
                Debug.Log($"Target is not {nameof(Seedbed)}");
                return;
            }

            if (!item.HasComponent<ComponentHarvestSicknessReducing>()) return;

            var component = item.GetComponent<ComponentHarvestSicknessReducing>();
            _target.SeedbedImpl.SeedbedBoost.SicknessProbabilityReducingInPercent = component.Reducing;
        }
    }
}