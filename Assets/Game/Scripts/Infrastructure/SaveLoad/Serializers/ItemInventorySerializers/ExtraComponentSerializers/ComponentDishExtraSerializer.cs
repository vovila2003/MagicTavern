using Modules.Items;
using Tavern.Cooking;

namespace Tavern.Infrastructure
{
    public class ComponentDishExtraSerializer : IExtraSerializer
    {
        public string Serialize(IExtraItemComponent _)
        {
            return string.Empty;
        }

        public void Deserialize(Item item, string value)
        {
            item.AddExtraComponent(new ComponentDishExtra());
        }
    }
}