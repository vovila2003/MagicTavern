using Modules.Items;
using Sirenix.OdinInspector;

namespace Tavern.Cooking
{
    public class ComponentDishExtra : IExtraItemComponent
    {
        [ShowInInspector, ReadOnly] 
        private bool _extra = true;
        
        public IExtraItemComponent Clone()
        {
            return new ComponentDishExtra();
        }
    }
}