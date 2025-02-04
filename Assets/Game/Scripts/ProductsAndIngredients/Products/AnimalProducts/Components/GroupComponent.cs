using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class GroupComponent : IItemComponent
    {
        public string GroupName;

        public GroupComponent()
        {
        }

        private GroupComponent(string groupName)
        {
            GroupName = groupName;
        }

        public IItemComponent Clone()
        {
            return new GroupComponent(GroupName);    
        }
    }
}