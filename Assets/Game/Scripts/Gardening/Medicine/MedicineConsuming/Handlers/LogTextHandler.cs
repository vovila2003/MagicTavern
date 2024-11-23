using Modules.Consuming;
using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening.Medicine
{
    public class LogTextHandler : IInventoryItemConsumeHandler
    {
        public void SetTarget(object target)
        {
        }

        public void OnConsume(Item item)
        {
            if (!item.HasComponent<Component_Test>()) return;

            var attribute = item.GetComponent<Component_Test>();
            Debug.Log($"{attribute.Text} for {item.ItemName}");
        }
    }
}