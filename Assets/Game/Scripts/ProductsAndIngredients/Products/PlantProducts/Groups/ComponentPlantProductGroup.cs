using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentPlantProductGroup : IComponentGroup
    {
        public virtual IItemComponent Clone()
        {
            return new ComponentPlantProductGroup();
        }
    }
}