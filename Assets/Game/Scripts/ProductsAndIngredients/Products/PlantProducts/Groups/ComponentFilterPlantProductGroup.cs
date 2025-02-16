using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentFilterPlantProductGroup : IComponentGroup
    {
        public virtual IItemComponent Clone()
        {
            return new ComponentFilterPlantProductGroup();
        }
    }
}