using Modules.Items;

namespace Modules.Consuming
{
    public interface IInventoryItemConsumeHandler
    {
        void SetTarget(object target);
        void OnConsume(Item item);
    }
}