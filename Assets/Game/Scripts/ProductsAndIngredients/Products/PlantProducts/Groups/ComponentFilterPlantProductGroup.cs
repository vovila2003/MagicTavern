using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentFilterPlantProductGroup : IComponentGroup
    {
        public virtual string Name => "Растительные продукты";
        public virtual IItemComponent Clone()
        {
            return new ComponentFilterPlantProductGroup();
        }
    }
}