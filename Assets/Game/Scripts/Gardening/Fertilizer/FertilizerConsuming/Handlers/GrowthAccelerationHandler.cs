using Modules.Consuming;
using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening.Fertilizer
{
    public class GrowthAccelerationHandler : IInventoryItemConsumeHandler
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

            if (!item.Has<ComponentGrowthAcceleration>()) return;

            var component = item.Get<ComponentGrowthAcceleration>();
            _target.Seedbed.AccelerateGrowth(component.Acceleration);
        }
    }
}