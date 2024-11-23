using Modules.Consuming;
using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening.Medicine
{
    public class SickProbabilityReducingHandler : IInventoryItemConsumeHandler
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

            if (!item.HasComponent<ComponentHarvestSickProbabilityReducing>()) return;

            var attribute = item.GetComponent<ComponentHarvestSickProbabilityReducing>();
            _target.Heal(attribute.Reducing);
        }
    }
}