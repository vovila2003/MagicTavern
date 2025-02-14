using Modules.Items;
using Sirenix.OdinInspector;

namespace Tavern.Cooking
{
    public class ComponentDishExtra : IItemComponent
    {
        [ShowInInspector, ReadOnly] 
        private string _extra;
        
        public IItemComponent Clone()
        {
            return new ComponentDishExtra();
        }
    }
}