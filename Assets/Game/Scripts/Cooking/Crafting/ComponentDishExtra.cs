using Modules.Items;
using Sirenix.OdinInspector;

namespace Tavern.Cooking
{
    public class ComponentDishExtra : IExtraItemComponent
    {
        [ShowInInspector, ReadOnly] 
        private bool _extra = true;
        
        public string ComponentName => nameof(ComponentDishExtra);

        public IExtraItemComponent Clone()
        {
            return new ComponentDishExtra();
        }

    }
}