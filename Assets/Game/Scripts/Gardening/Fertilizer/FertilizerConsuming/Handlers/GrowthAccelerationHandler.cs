using Modules.Consuming;
using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening.Fertilizer
{
    public class GrowthAccelerationHandler : IInventoryItemConsumeHandler
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

            if (!item.HasComponent<ComponentGrowthAcceleration>()) return;

            var component = item.GetComponent<ComponentGrowthAcceleration>();
            _target.SeedbedImpl.SeedbedBoost.GrowthAccelerationInPercent = component.Acceleration;
        }
    }
}