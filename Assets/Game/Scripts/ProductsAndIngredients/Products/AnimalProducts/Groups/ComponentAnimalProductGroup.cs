using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentAnimalProductGroup : IComponentGroup
    {
        public virtual IItemComponent Clone()
        {
            return new ComponentAnimalProductGroup();
        }
    }
}